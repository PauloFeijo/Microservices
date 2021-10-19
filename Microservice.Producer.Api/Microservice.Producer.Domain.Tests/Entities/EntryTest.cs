using FluentAssertions;
using Microservice.Producer.Domain.Dtos;
using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Exceptions;
using Microservice.Producer.Domain.Tests.Builders;
using System;
using Xunit;

namespace Microservice.Producer.Domain.Tests.Entities
{
    public class EntryTest
    {
        [Fact]
        public void Create_WithValidDto_ShouldCreated()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .Build();

            var entry = new Entry(entryDto);

            entry.UserName.Should().Equals(entryDto.UserName);
            entry.Moment.Should().Equals(entryDto.Moment);
            entry.Type.Should().Equals(entryDto.Type);
            entry.AccountDescription.Should().Equals(entryDto.AccountDescription);
            entry.Description.Should().Equals(entryDto.Description);
            entry.Value.Should().Equals(entryDto.Value);
            entry.Id.Should().NotBeEmpty();
            entry.CreatedAt.Date.Should().Equals(DateTime.Today);
        }

        [Fact]
        public void Create_WithUserNameEmpty_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithUserName(string.Empty)
                .Build();
            var expectedMessage = "Username could not be empty.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void Create_WithAccountDescriptionEmpty_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithAccountingDescription(string.Empty)
                .Build();
            var expectedMessage = "AccountDescription could not be empty.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void Create_WithInvalidType_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithType('X')
                .Build();
            var expectedMessage = "Type must be R for revenue or E for expense.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_WithTypeRevenueAndValueZeroOrLessThanZero_ShouldThrowAnException(decimal value)
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithType('R')
                .WithValue(value)
                .Build();
            var expectedMessage = "For revenue, Value must be greater than zero.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Create_WithTypeExpenseAndValueZeroOrGreaterThanZero_ShouldThrowAnException(decimal value)
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithType('E')
                .WithValue(value)
                .Build();
            var expectedMessage = "For expense, Value must be lower than zero.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void Create_WithMomentLessThanMinDate_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithMoment(new DateTime(1899, 12, 31))
                .Build();
            var expectedMessage = $"Moment should be greater than 1900/01/01 00:00:00 and lower or equal than {DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void Create_WithMomentGreaterThanToday_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .WithMoment(DateTime.UtcNow.AddDays(1))
                .Build();
            var expectedMessage = $"Moment should be greater than 1900/01/01 00:00:00 and lower or equal than {DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}.";

            Func<Entry> func = () => { return new Entry(entryDto); };

            func.Should().Throw<ValidationException>().WithMessage(expectedMessage);
        }
    }
}
