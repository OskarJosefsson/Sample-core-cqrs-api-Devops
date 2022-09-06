using SampleProject.Domain.Customers.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers
{
    public class EmailMustBeDomainChecker : IEmailMustBeDomainChecker
    {


        private readonly string _domain;
        private readonly string _email;

        public EmailMustBeDomainChecker(string domain, string email)
        {
            _domain = domain;
            _email = email;
        }
       
        public bool isDomain(string customerEmail)
        {
            if (_email.EndsWith(_domain))
            {
                return true;
            }

            else
            {
                return false;
            }
            
        }
    }
}
