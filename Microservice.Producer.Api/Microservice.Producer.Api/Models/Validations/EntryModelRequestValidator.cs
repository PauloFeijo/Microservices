using FluentValidation;
using System;

namespace Microservice.Producer.Api.Models.Validations
{
    public class EntryModelRequestValidator : AbstractValidator<EntryModelRequest>
    {
        private readonly DateTime _minDate = DateTime.Parse("1900/01/01");
        private const char RevenueChar = 'R';
        private const char ExpenseChar = 'E';
        private const string MustBeNotEmpty = "{0} must be not empty";
        private const string MustBeBetween = "{0} should be between {1} and {2}.";
        private const string MustBeRevenueOrExpense = "{0} must be {1} for revenue or {2} for expense.";
        private const string MustBeGreaterThanZeroWhenRevenue = "For revenue, {0} must be greater than zero.";
        private const string MustBeLowerThanZeroWhenExpense = "For expense, {0} must be lower than zero.";

        public EntryModelRequestValidator()
        {
            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(x => string.Format(MustBeNotEmpty, nameof(x.UserName)));

            RuleFor(x => x.AccountDescription)
                .NotEmpty()
                .WithMessage(x => string.Format(MustBeNotEmpty, nameof(x.AccountDescription)));

            RuleFor(x => x.Moment)
                .InclusiveBetween(_minDate, DateTime.UtcNow)
                .WithMessage(x => string.Format(MustBeBetween, nameof(x.Moment),
                    $"{_minDate:yyyy/MM/dd HH:mm:ss}", $"{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}"));

            RuleFor(x => x.Type)
                .Must(x => x.Equals(RevenueChar) || x.Equals(ExpenseChar))
                .WithMessage(x => string.Format(MustBeRevenueOrExpense, nameof(x.Type), RevenueChar, ExpenseChar));

            RuleFor(x => x.Value)
                .Must(x => x > 0.0m)
                .When(y => y.Type.Equals(RevenueChar))
                .WithMessage(x => string.Format(MustBeGreaterThanZeroWhenRevenue, nameof(x.Value)));

            RuleFor(x => x.Value)
                .Must(x => x < 0.0m)
                .When(y => y.Type.Equals(ExpenseChar))
                .WithMessage(x => string.Format(MustBeLowerThanZeroWhenExpense, nameof(x.Value)));
        }
    }
}
