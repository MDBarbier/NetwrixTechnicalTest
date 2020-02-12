using Microsoft.AspNetCore.Mvc;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.Database;

namespace TechnicalTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private CustomerAccessMethods CustomerAccessMethods;

        public CustomerAddressController()
        {
            CustomerAccessMethods = new CustomerAccessMethods(new DatabaseContext());
        }
            
        // GET: api/CustomerAddress/{customerId}
        [HttpGet("{customerId}", Name = "Get")]
        public string[] Get(int customerId)
        {
            var customerRecord = CustomerAccessMethods.GetCustomerById(customerId);

            var customerAddressComposite = customerRecord.Address1;

            if (!string.IsNullOrEmpty(customerRecord.Address2))
            {
                customerAddressComposite += $", {customerRecord.Address2}";
            }

            customerAddressComposite += $", {customerRecord.Postcode}";

            return new string[] { customerRecord.Name, customerAddressComposite };            
            
        }
    }
}
