using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers.Rules
{
    public class EmailMustIncludeDomainRule : IBusinessRule
    {
        private string _email;

        public EmailMustIncludeDomainRule(string email)
        {
            _email = email;
        }

        public bool IsBroken() => !_email.ToLower().EndsWith("nackademin.se");

        public string Message => "Domain must be nackademin.se";

    }
}
