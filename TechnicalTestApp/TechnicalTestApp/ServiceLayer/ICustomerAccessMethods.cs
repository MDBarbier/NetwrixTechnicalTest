using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    public interface ICustomerAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public abstract Customer GetCustomerById(int customerId);

        public abstract Dictionary<int, CustomerViewModel> GetCustomers();
    }
}
