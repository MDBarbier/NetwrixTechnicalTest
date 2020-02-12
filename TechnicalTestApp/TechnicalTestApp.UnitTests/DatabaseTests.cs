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

namespace TechnicalTestApp.UnitTests
{
    public class DatabaseTests
    {
        [Fact]
        public void TestSqlLiteConnectivity()
        {          


            //var logger = Mock.Of<ILogger<HomeController>>();
            //var controller = new HomeController(logger);
            //var result = controller.Index() as ViewResult;




            //var mockSet = new Mock<DbSet<Customer>>();

            //var mockContext = new Mock<DatabaseContext>();
            //mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            //var service = new CustomerAccessMethods(mockContext.Object);
            //service.GetCustomerById(1);

            //mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }
    }
}
