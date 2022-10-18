using BookStore.Models.Models.Interfaces;
using MessagePack;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public class Book : ICacheItem<int>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Title { get; set; }
        [Key(2)]
        public int AuthorId { get; init; }
        [Key(3)]
        public int Quantity { get; set; }
        [Key(4)]
        public DateTime LastUpdated { get; set; }
        [Key(5)]
        public decimal Price { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
