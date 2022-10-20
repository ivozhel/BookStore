using BookStore.Models.Models;

namespace BookStore.BL.Providers.Interfaces
{
    public interface IAdditionalInfoClientProvider
    {
        public Task<Tuple<int,string>> GetAdditionalInfo(int authorId);
    }
}
