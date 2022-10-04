using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class BookReqValidator : AbstractValidator<BookRequest>
    {
        public BookReqValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.AuthorId).GreaterThan(0);

        }
    }
}
