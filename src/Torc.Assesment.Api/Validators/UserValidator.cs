using FluentValidation;
using Torc.Assesment.Api.Model;


namespace Torc.Assesment.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {

        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
        }

    }
}
