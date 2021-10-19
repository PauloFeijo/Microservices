using Microservice.Producer.Domain.Dtos;
using Microservice.Producer.Domain.Exceptions;
using System;

namespace Microservice.Producer.Domain.Entities
{
    public class Entry
    {
        private readonly DateTime _minDate = DateTime.Parse("1900/01/01");
        private const char RevenueChar = 'R';
        private const char ExpenseChar = 'E';

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Entry(EntryDto entryDto)
        {
            Id = Guid.NewGuid();
            UserName = entryDto.UserName;
            Moment = entryDto.Moment;
            Value = entryDto.Value;
            Type = entryDto.Type;
            AccountDescription = entryDto.AccountDescription;
            Description = entryDto.Description;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            if (UserName == string.Empty)
            {
                throw new ValidationException($"{nameof(UserName)} could not be empty.");
            }

            if (Moment <= _minDate || Moment > DateTime.UtcNow)
            {
                throw new ValidationException($"{nameof(Moment)} should be greater than {_minDate:yyyy/MM/dd HH:mm:ss} and lower or equal than {DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}.");
            }

            if (!Type.Equals(RevenueChar) && !Type.Equals(ExpenseChar))
            {
                throw new ValidationException($"{nameof(Type)} must be {RevenueChar} for revenue or {ExpenseChar} for expense.");
            }

            if (Type.Equals(RevenueChar) && Value <= 0.00m)
            {
                throw new ValidationException($"For revenue, {nameof(Value)} must be greater than zero.");
            }

            if (Type.Equals(ExpenseChar) && Value >= 0.00m)
            {
                throw new ValidationException($"For expense, {nameof(Value)} must be lower than zero.");
            }

            if (AccountDescription == string.Empty)
            {
                throw new ValidationException($"{nameof(AccountDescription)} could not be empty.");
            }
        }
    }
}
