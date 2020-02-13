using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using TechnicalTestApp.Controllers;
using Xunit;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TechnicalTestApp.Models;
using TechnicalTestApp.Database;
using TechnicalTestApp.ServiceLayer;
using System.Linq;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;

namespace TechnicalTestApp.UnitTests
{
    public class DatabaseTests
    {
        [Fact]
        public void TestGetCustomerById()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Customer>>();
            var fixture = new Fixture();
            IQueryable<Customer> data = CreateCustomerQueryable(fixture);
            SetupCustomerMockSet(mockSet, data);
            var mockContext = new Mock<IApplicationDatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var service = new CustomerAccessMethods(mockContext.Object);

            //Act
            var customer = service.GetCustomerById(1234);

            //Assert
            customer.Name.Should().Be("Mr Smith");
        }        

        [Fact]
        public void TestGetInvoiceById()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Invoice>>();
            var fixture = new Fixture();
            IQueryable<Invoice> data = CreateInvoicesQueryable(fixture);
            SetupInvoiceMockSet(mockSet, data);
            var mockContext = new Mock<IApplicationDatabaseContext>();
            mockContext.Setup(m => m.Invoices).Returns(mockSet.Object);
            var service = new InvoiceAccessMethods(mockContext.Object);

            //Act
            var invoice = service.GetInvoiceById(457);

            //Assert
            invoice.Value.Should().Be(384);
        }

        private static IQueryable<Invoice> CreateInvoicesQueryable(Fixture fixture)
        {
            return new List<Invoice>()
            {
                fixture.Build<Invoice>().With(u => u.InvoiceId, 768).With(u => u.Value, 999).Create(),
                fixture.Build<Invoice>().With(u => u.InvoiceId, 457).With(u => u.Value, 384).Create()
            }.AsQueryable();
        }

        private static void SetupInvoiceMockSet(Mock<DbSet<Invoice>> mockSet, IQueryable<Invoice> data)
        {
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

        private static void SetupCustomerMockSet(Mock<DbSet<Customer>> mockSet, IQueryable<Customer> data)
        {
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
        private static IQueryable<Customer> CreateCustomerQueryable(Fixture fixture)
        {
            return new List<Customer>()
            {
                fixture.Build<Customer>().With(u => u.Name, "Mr Test").With(u => u.CustomerId, 5678).Create(),
                fixture.Build<Customer>().With(u => u.Name, "Mr Smith").With(u => u.CustomerId, 1234).Create()
            }.AsQueryable();
        }
    }
}
