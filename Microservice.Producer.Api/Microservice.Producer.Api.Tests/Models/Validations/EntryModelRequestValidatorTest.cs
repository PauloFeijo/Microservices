using FluentAssertions;
using Microservice.Producer.Api.Models.Validations;
using Microservice.Producer.Api.Tests.Builders;
using System;
using System.Linq;
using Xunit;

namespace Microservice.Producer.Api.Tests.Models.Validations
{
    public class EntryModelRequestValidatorTest
    {
        private readonly EntryModelRequestValidator _validator;

        private readonly DateTime _minDate = DateTime.Parse("1900/01/01");
        private const char RevenueChar = 'R';
        private const char ExpenseChar = 'E';
        private const string MustBeNotEmpty = "{0} must be not empty";
        private const string MustBeBetween = "{0} should be between {1} and";
        private const string MustBeRevenueOrExpense = "{0} must be {1} for revenue or {2} for expense.";
        private const string MustBeGreaterThanZeroWhenRevenue = "For revenue, {0} must be greater than zero.";
        private const string MustBeLowerThanZeroWhenExpense = "For expense, {0} must be lower than zero.";

        public EntryModelRequestValidatorTest()
        {
            _validator = new EntryModelRequestValidator();
        }


        [Fact]
        public void Validate_WithValidModelRequest_ShouldValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .Build();

            var result = _validator.Validate(entryModelRequest);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WithUserNameEmpty_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithUserName(string.Empty)
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Be(string.Format(MustBeNotEmpty, nameof(entryModelRequest.UserName)));
        }

        [Fact]
        public void Validate_WithAccountDescriptionEmpty_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithAccountingDescription(string.Empty)
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Be(string.Format(MustBeNotEmpty, nameof(entryModelRequest.AccountDescription)));
        }

        [Fact]
        public void Validate_WithMomentLessThenMinDate_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithMoment(_minDate.AddMinutes(-1))
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Contain(string.Format(MustBeBetween, nameof(entryModelRequest.Moment), $"{_minDate:yyyy/MM/dd HH:mm:ss}"));
        }

        [Fact]
        public void Validate_WithMomentGreatherThenUtcNow_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithMoment(DateTime.UtcNow.AddMinutes(1))
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Contain(string.Format(MustBeBetween, nameof(entryModelRequest.Moment), $"{_minDate:yyyy/MM/dd HH:mm:ss}"));
        }

        [Fact]
        public void Validate_WithTypeInvalid_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithType('X')
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Contain(string.Format(MustBeRevenueOrExpense, nameof(entryModelRequest.Type), RevenueChar, ExpenseChar));
        }

        [Fact]
        public void Validate_WithRevenueTypeAndValueInvalid_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithType('R')
                .WithValue(-1m)
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Contain(string.Format(MustBeGreaterThanZeroWhenRevenue, nameof(entryModelRequest.Value), RevenueChar));
        }

        [Fact]
        public void Validate_WithExpenseTypeAndValueInvalid_ShouldNotValidate()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .WithType('E')
                .WithValue(1m)
                .Build();

            var result = new EntryModelRequestValidator().Validate(entryModelRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Contain(string.Format(MustBeLowerThanZeroWhenExpense, nameof(entryModelRequest.Value), ExpenseChar));
        }
    }
}
