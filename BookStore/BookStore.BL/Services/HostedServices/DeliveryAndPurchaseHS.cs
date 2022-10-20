using BookStore.BL.Services.Consumers;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Services.HostedServices
{
    public class DeliveryAndPurchaseHS : IHostedService
    {
        private DeliveryConsumer deliveryConsumer;
        private PurchaseConsumer purchaseConsumer;
        private IBookRepo _bookRepo;
        private IOptions<KafkaConfiguration> _options;

        public DeliveryAndPurchaseHS(IBookRepo bookRepo, IOptions<KafkaConfiguration> options)
        {
            this._bookRepo = bookRepo;
            this._options = options;
            deliveryConsumer = new DeliveryConsumer(_bookRepo, _options);
            purchaseConsumer = new PurchaseConsumer(_bookRepo, _options);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            deliveryConsumer.Consume(cancellationToken);
            purchaseConsumer.Consume(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
