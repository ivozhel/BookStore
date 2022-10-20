using System.Diagnostics.CodeAnalysis;
using MessagePack;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public record Purchase
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
        [Key(4)]
        public Dictionary<int, string> AdditionalInfo { get; set; } = new Dictionary<int, string>();

    }
}
