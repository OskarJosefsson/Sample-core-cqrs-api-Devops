
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
            var sut = new ForeignExchange(_cacheStore);
            int expectedResult = 2;

            //Act

            var result = sut.GetConversionRates();

            //Assert

            

            Assert.AreEqual(expectedResult, result.Count);

        }

        [Test]
        public void GetConversionRates_WhenCacheAvailable_ShouldReturnThreeValues()
        {

            //Arrange
            int expectedResult = 1;
            var _cacheStore = Substitute.For<ICacheStore>();
            
            var conversionRates = new List<ConversionRate>();
            conversionRates.Add(new ConversionRate("USD", "EUR", (decimal)0.88));
            ConversionRatesCache cache = new ConversionRatesCache(conversionRates);
            

           _cacheStore.Get<ConversionRatesCache>(Arg.Any<ConversionRatesCacheKey>()).Returns(cache);

           

            var sut = new ForeignExchange(_cacheStore);
            

            //Act

            var result = sut.GetConversionRates();

            //Assert

            Assert.AreEqual(expectedResult, result.Count);



        }


    }


}
