using AutoFixture;
using Microservice.Producer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Producer.Domain.Tests.Builders
{
    public class EntryDtoBuilder
    {
        private Fixture _builder;
        private string _userName;
        private DateTime _moment;
        private char _type;
        private string _accountDescription;
        private string _description;
        private decimal _value;

        public EntryDtoBuilder()
        {
            _builder = new Fixture();
            _userName = _builder.Create<string>();
            _moment = _builder.Create<DateTime>();
            _type = _builder.Create<char>();
            _accountDescription = _builder.Create<string>();
            _description = _builder.Create<string>();
            _value = _builder.Create<decimal>();
        }

        public EntryDtoBuilder WithValidValues()
        {
            _moment = DateTime.UtcNow;
            _type = 'R';
            _value = 10.00m;
            return this;
        }

        public EntryDtoBuilder WithType(char type)
        {
            _type = type;
            return this;
        }

        public EntryDtoBuilder WithUserName(string username)
        {
            _userName = username;
            return this;
        }

        public EntryDtoBuilder WithAccountingDescription(string accountingDescription)
        {
            _accountDescription = accountingDescription;
            return this;
        }

        public EntryDtoBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public EntryDtoBuilder WithMoment(DateTime moment)
        {
            _moment = moment;
            return this;
        }

        public EntryDto Build()
        {
            return new EntryDto()
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
