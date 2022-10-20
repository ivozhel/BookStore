using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookStore.BL.Providers.Interfaces;
using BookStore.Caches.KafkaService;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Generator.Models;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Services.Consumers
{
    public class PurchaseConsumer : KafkaConsumer<Guid, Purchase>
    {
        private readonly IBookRepo _bookRepo;
        private readonly IOptions<KafkaConfiguration> _options;
        private readonly TransformBlock<Purchase, Purchase> _purchaseTransformBlockAuthorInfo;
        private readonly TransformBlock<Purchase, string> _purchaseTransformBlock;
        private readonly ActionBlock<string> actionBlock;
        private readonly IAdditionalInfoClientProvider _additionalInfoClientProvider;
        public PurchaseConsumer(IBookRepo bookRepo, IOptions<KafkaConfiguration> options, IAdditionalInfoClientProvider additionalInfoClientProvider) : base(options)
        {
            _additionalInfoClientProvider = additionalInfoClientProvider;
            _bookRepo = bookRepo;
            _options = options;
            _purchaseTransformBlockAuthorInfo = new TransformBlock<Purchase, Purchase>(async pur =>
            {
                var tasks = pur.Books.DistinctBy(x => x.AuthorId).Select(x => _additionalInfoClientProvider.GetAdditionalInfo(x.AuthorId));
                var result = await Task.WhenAll(tasks);
                foreach (var item in result)
                {
                    pur.AdditionalInfo.Add(item.Item1, item.Item2);
                }
                return pur;
            });
            _purchaseTransformBlock = new TransformBlock<Purchase, string>(async pur =>
            {
                var str = new StringBuilder();
                foreach (var bookToAdd in pur.Books)
                {
                    var book = await _bookRepo.GetByID(bookToAdd.Id);
                    if (book != null)
                    {
                        var bookCount = pur.Books.Count(x => x.Id == book.Id);
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
            _purchaseTransformBlockAuthorInfo.LinkTo(_purchaseTransformBlock);
            _purchaseTransformBlock.LinkTo(actionBlock);
        }

        public override Task Consume(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = base._consumer.Consume();
                        _purchaseTransformBlockAuthorInfo.Post(result.Value);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
