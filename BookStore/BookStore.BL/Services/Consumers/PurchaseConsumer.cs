using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookStore.Caches.KafkaService;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Generator.Models;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Services.Consumers
{
    public class PurchaseConsumer : KafkaConsumer<Guid,Purchase>
    {
        private readonly IBookRepo _bookRepo;
        private readonly IOptions<KafkaConfiguration> _options;
        private readonly TransformBlock<Purchase, string> _purchaseTransformBlock;
        private readonly ActionBlock<string> actionBlock;
        public PurchaseConsumer(IBookRepo bookRepo, IOptions<KafkaConfiguration> options) : base(options)
        {
            _bookRepo = bookRepo;
            _options = options;
            _purchaseTransformBlock = new TransformBlock<Purchase, string>(async pur =>
            {
                var str = new StringBuilder();
                foreach (var bookToAdd in pur.Books)
                {
                    var book = await _bookRepo.GetByID(bookToAdd.Id);
                    var bookCount = pur.Books.Count(x => x.Id == book.Id);
                    if (book != null)
                    {
                        if (book.Quantity < bookCount)
                        {
                            str.AppendLine($"Book with id {book.Id} did not have enough quantity for purchase with id {pur.Id}");
                            continue;
                        }
                        else
                        {
                            book.Quantity -= bookCount;
                            str.AppendLine($"Book with id {book.Id} have {book.Quantity} quantity left");
                        }
                    }
                    else
                    {
                        str.AppendLine($"Book with id {bookToAdd.Id} dose not exist");
                        continue;
                    }

                    await _bookRepo.UpdateBook(book);
                }

                return str.ToString();
            });

            actionBlock = new ActionBlock<string>(s =>
            {
                Console.WriteLine(s);
            });
            _purchaseTransformBlock.LinkTo(actionBlock);
        }

        public override Task Consume(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = base._consumer.Consume();
                    _purchaseTransformBlock.Post(result.Value);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
