using System.Security.Cryptography.Xml;
using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AuthorReqValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorReqValidator()
        {
            When(x => x.Nickname == string.Empty, () =>
             {
                 RuleFor(x => x.Nickname).MinimumLength(2);
                 RuleFor(x => x.Nickname).MaximumLength(50);
             });
            RuleFor(x => x.Age)
                .GreaterThan(0)
                .LessThanOrEqualTo(120);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime. MaxValue)
                .GreaterThan(DateTime.MinValue);

        }
    }
}
