using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_CustomerService.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomerController : ControllerBase
    {
        public CustomerManager CManager { get; set; }
        public OrderManager OManager { get; set; }

        public CustomerController()
        {
            CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
        }

        [HttpGet]
        public ICollection<Customer> GetAllCustomers()
        {
            try { return CManager.GetAllCustomers(); }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            try { return CManager.GetCustomer(id); }
            catch { return NotFound("Customer doesn't exist"); }
        }

        [HttpPost]
        public ActionResult<Customer> PostCustomer([FromBody] sample_object.SampleCustomer cus)
        {
            Customer customer = new Customer(cus.Name, cus.Adress);
            if (CManager.ExistCustomerCheck(customer))
                return BadRequest("Customer already exists");
            CManager.AddCustomer(customer);
            Response.StatusCode = 201;
            return customer;
        }
        [HttpPut("{ID}")]
        public ActionResult<Customer> PutCustomer(int ID, [FromBody] sample_object.SampleCustomer cus)
        {
            try
            {
                Customer customer = new Customer(cus.Name, cus.Adress);
                if (CManager.ExistCustomerCheck(customer))
                    return BadRequest("Customer already exists");
                CManager.UpdateCustomer(ID, customer);
                return CManager.GetCustomer(ID);
            }
            catch { return NotFound("Customer doesn't exist"); }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                var temp = CManager.GetCustomer(id);
                if(temp.orderList.Count == 0)
                {
                    CManager.DeleteCustomer(id);
                    return NoContent();
                }
                else
                    return BadRequest("Orders still linked to customer");
            }
            catch { return NotFound("Customer doesn't exist"); }
            
        }

    }
}
