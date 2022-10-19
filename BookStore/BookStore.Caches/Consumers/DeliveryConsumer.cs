using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookStore.Caches.KafkaService;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models;
using Generator.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Caches.Consumers
{
    public class DeliveryConsumer : KafkaConsumer<int, Delivery>
    {
        private readonly IBookRepo _bookRepo;
        private readonly IOptions<KafkaConfiguration> _options;
        private readonly TransformBlock<Delivery, string> _deliveryTransformBlock;
        private readonly ActionBlock<string> actionBlock;
        public DeliveryConsumer(IBookRepo bookRepo, IOptions<KafkaConfiguration> options) : base(options)
        {
            _bookRepo = bookRepo;
            _options = options;
            _deliveryTransformBlock = new TransformBlock<Delivery, string>(async del =>
            {
                var str = new StringBuilder();
                var book = await _bookRepo.GetByID(del.Book.Id);
                if (book != null)
                {
                    book.Quantity += del.Quantity;
                    await _bookRepo.UpdateBook(book);
                    str.AppendLine($"Book with id {book.Id} updated quantity to {book.Quantity}");
                }
                else
                {
                    await _bookRepo.AddBook(del.Book);
                    str.AppendLine($"Book with name {del.Book.Title} added");
                }
                return str.ToString();
            });
            actionBlock = new ActionBlock<string>(s =>
            {
                Console.WriteLine(s);
            });
            _deliveryTransformBlock.LinkTo(actionBlock);
        }
        public override Task Consume(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = base._consumer.Consume();
                    _deliveryTransformBlock.Post(result.Value);
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
