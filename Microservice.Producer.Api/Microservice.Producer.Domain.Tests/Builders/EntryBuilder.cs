using AutoFixture;
using Microservice.Producer.Domain.Entities;
using System;

namespace Microservice.Producer.Domain.Tests.Builders
{
    public class EntryBuilder
    {
        private readonly Fixture _builder;
        private readonly string _userName;
        private DateTime _moment;
        private char _type;
        private readonly string _accountDescription;
        private readonly string _description;
        private decimal _value;

        public EntryBuilder()
        {
            _builder = new Fixture();
            _userName = _builder.Create<string>();
            _moment = _builder.Create<DateTime>();
            _type = _builder.Create<char>();
            _accountDescription = _builder.Create<string>();
            _description = _builder.Create<string>();
            _value = _builder.Create<decimal>();
        }

        public EntryBuilder WithValidValues()
        {
            _moment = DateTime.UtcNow;
            _type = 'R';
            _value = 10.00m;
            return this;
        }

        public Entry Build()
        {
            return new Entry()
            {
                UserName = _userName,
                Moment = _moment,
                Type = _type,
                AccountDescription = _accountDescription,
                Description = _description,
                Value = _value
            };
        }
    }
}
