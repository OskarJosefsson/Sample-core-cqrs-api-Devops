
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
            
            var _cacheStore = Substitute.For<ICacheStore>();
            var conversionRates = new List<ConversionRate>();

            int expectedResult = 3;
            conversionRates.Add(new ConversionRate("USD", "EUR", (decimal)0.88));
            conversionRates.Add(new ConversionRate("EUR", "USD", (decimal)1.13));
            conversionRates.Add(new ConversionRate("SEK", "USD", (decimal)0.09));

            _cacheStore.Add(new ConversionRatesCache(conversionRates), new ConversionRatesCacheKey(), DateTime.Now.Date.AddDays(1));

            var sut = new ForeignExchange(_cacheStore);
            

            //Act

            var result = sut.GetConversionRates();

            //Assert

            Assert.AreEqual(expectedResult, result.Count);



        }


    }


}
