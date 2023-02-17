using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Users.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Fist name is required").MaximumLength(200).WithMessage("FistName can not over 200 character");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required").MaximumLength(200).WithMessage("LastName can not over 200 character");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100 years");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Last name is required").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format not match");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ComfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });
        }
    }
}