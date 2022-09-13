using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers
{
    public interface IEmailMustBeDomainChecker
    {
        bool isDomain(string customerEmail);
    }
}
