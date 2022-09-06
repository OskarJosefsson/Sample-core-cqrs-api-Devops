using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers.Rules
{
    public class EmailDomainRule : IBusinessRule
    {

        private readonly string _email;

        public EmailDomainRule(string email)
        {
            _email = email;
        }

        public bool IsBroken() => !_email.ToLower().EndsWith("nackademin.se");

        public string Message => "Email should have domainname nackademin.se";
    }
}

