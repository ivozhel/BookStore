using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Caches.KafkaService;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Hosting;

namespace BookStore.BL.Services
{
    public class ConsumerHostedService<TKey,TValue> : IHostedService
    {
        private readonly KafkaConsumer<TKey, TValue> _consumer;
        public ConsumerHostedService(KafkaConsumer<TKey, TValue> consumer)
        {
            _consumer = consumer;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Consume(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
