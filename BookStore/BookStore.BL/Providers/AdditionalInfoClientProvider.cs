using System.Diagnostics.Metrics;
using BookStore.BL.Providers.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Providers
{
    public class AdditionalInfoClientProvider : IAdditionalInfoClientProvider
    {
        private HttpClient client;
        private readonly IOptions<AdditionalInfoEndPoint> options;
        public AdditionalInfoClientProvider(IOptions<AdditionalInfoEndPoint> options)
        {
            client = new HttpClient();
            this.options = options;
        }

        public async Task<Tuple<int, string>> GetAdditionalInfo(int authorId)
        {
            var info = await client.GetStringAsync($"{options.Value.Url}{authorId}");

            return new Tuple<int, string>(authorId, info);

        }
    }
}
