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
        private IEmailMustBeDomainChecker _domainChecker;

        public EmailMustIncludeDomainRule(IEmailMustBeDomainChecker domainChecker,string email)
        {
            _email = email;
            _domainChecker = domainChecker;
        }

        public bool IsBroken() => !_domainChecker.isDomain(_email);

        public string Message => "Domain must be nackademin.se";

    }
}
