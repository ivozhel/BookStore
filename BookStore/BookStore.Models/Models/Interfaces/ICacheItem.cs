namespace BookStore.Models.Models.Interfaces
{
    public interface ICacheItem<out T>
    {
        T GetKey();
    }
}
