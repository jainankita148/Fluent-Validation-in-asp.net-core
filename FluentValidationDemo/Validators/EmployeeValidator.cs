using FluentValidation;
using FluentValidationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationDemo.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(p => p.FirstName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(4, 50)
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");

            RuleFor(p => p.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!").Length(4, 50)
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");

            RuleFor(p => p.Email).EmailAddress().WithMessage("{PropertyName} is not a valid Email!");

            RuleFor(x => x.DOB).Must(Validate_Age).WithMessage("Age Must be 18 or Greater");

            RuleFor(x => x.Salary).GreaterThanOrEqualTo(1000).LessThanOrEqualTo(50000);


        }

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        private bool Validate_Age(DateTime Age_)
        {
            DateTime Current = DateTime.Today;
            int age = Current.Year - Convert.ToDateTime(Age_).Year;

            if (age < 18)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
