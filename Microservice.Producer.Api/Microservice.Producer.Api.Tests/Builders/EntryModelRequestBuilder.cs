using AutoFixture;
using Microservice.Producer.Api.Models;
using System;

namespace Microservice.Producer.Api.Tests.Builders
{
    public class EntryModelRequestBuilder
    {
        private Fixture _builder;
        private string _userName;
        private DateTime _moment;
        private char _type;
        private string _accountDescription;
        private string _description;
        private decimal _value;

        public EntryModelRequestBuilder()
        {
            _builder = new Fixture();
            _userName = _builder.Create<string>();
            _moment = _builder.Create<DateTime>();
            _type = _builder.Create<char>();
            _accountDescription = _builder.Create<string>();
            _description = _builder.Create<string>();
            _value = _builder.Create<decimal>();
        }

        public EntryModelRequestBuilder WithValidValues()
        {
            _moment = DateTime.UtcNow.AddSeconds(-1);
            _type = 'R';
            _value = 10.00m;
            return this;
        }

        public EntryModelRequestBuilder WithType(char type)
        {
            _type = type;
            return this;
        }

        public EntryModelRequestBuilder WithUserName(string username)
        {
            _userName = username;
            return this;
        }

        public EntryModelRequestBuilder WithAccountingDescription(string accountingDescription)
        {
            _accountDescription = accountingDescription;
            return this;
        }

        public EntryModelRequestBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public EntryModelRequestBuilder WithMoment(DateTime moment)
        {
            _moment = moment;
            return this;
        }

        public EntryModelRequest Build()
        {
            return new EntryModelRequest()
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
