using NSubstitute;
using NUnit.Framework;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Rules;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.Customers
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public void GivenCustomerEmailIsUnique_WhenCustomerIsRegistering_IsSuccessful()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            var emailDomainChecker = Substitute.For<IEmailMustBeDomainChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(true);
            emailDomainChecker.isDomain(email).Returns(false);
            // Act
            var customer = Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker, emailDomainChecker);

            // Assert
            AssertPublishedDomainEvent<CustomerRegisteredEvent>(customer);
        }

        [Test]
        public void GivenCustomerEmailIsNotUnique_WhenCustomerIsRegistering_BreaksCustomerEmailMustBeUniqueRule()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            var emailDomainChecker = Substitute.For<IEmailMustBeDomainChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(false);
            emailDomainChecker.isDomain(email).Returns(true);
            // Assert
            AssertBrokenRule<CustomerEmailMustBeUniqueRule>(() =>
            {
                // Act
                Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker, emailDomainChecker);
            });
        }


        [Test]
        public void EmailDomainName_MustBeNackademin()
        {
            // Arrange
            
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            const string email = "oskar@test.se";
            customerUniquenessChecker.IsUnique(email).Returns(true);
            EmailMustBeDomainChecker check = new EmailMustBeDomainChecker("nackademin.se", email);

            // Assert
            AssertBrokenRule<EmailMustIncludeDomainRule>(() =>
            {
                // Act
                Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker, check );
            });
        }
    }
}