using BookStore.BL.Providers.Interfaces;
using BookStore.BL.Services.Consumers;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Services.HostedServices
{
    public class DeliveryAndPurchaseHS : IHostedService
    {
        private readonly DeliveryConsumer deliveryConsumer;
        private readonly PurchaseConsumer purchaseConsumer;
        private readonly IBookRepo _bookRepo;
        private readonly IOptions<KafkaConfiguration> _options;
        private readonly IAdditionalInfoClientProvider _additionalInfoClientProvider;

        public DeliveryAndPurchaseHS(IBookRepo bookRepo, IOptions<KafkaConfiguration> options, IAdditionalInfoClientProvider additionalInfoClientProvider)
        {
            this._bookRepo = bookRepo;
            this._options = options;
            _additionalInfoClientProvider = additionalInfoClientProvider;
            deliveryConsumer = new DeliveryConsumer(_bookRepo, _options);
            purchaseConsumer = new PurchaseConsumer(_bookRepo, _options, _additionalInfoClientProvider);
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
