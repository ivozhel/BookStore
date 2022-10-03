namespace BookStore.Models.Requests
{
    public class AuthorRequest
    {
        public string Name { get; init; }
        public string Nickname { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }

    }
}
