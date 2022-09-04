
using NSubstitute;
using NUnit.Framework;
using SampleProject.Domain.Customers;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Infrastructure.Caching;
using SampleProject.Infrastructure.Domain.ForeignExchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.UnitTests.ForeignExchanges
{
    public  class ForeignExChangeTest
    {

        [Test]
        public void GetConversionRates_WhenCacheNotAvailable_ShouldReturnTwoValues()
        {
            //Arrange
            
            var _cacheStore = Substitute.For<ICacheStore>();
            var sut = Substitute.For<ForeignExchange>(_cacheStore);
            int expectedResult = 2;

            //Act

            var result = sut.GetConversionRates();

            //Assert

            result.Count().Equals(expectedResult);



        }



    }


}
