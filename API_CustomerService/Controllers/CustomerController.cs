using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API_CustomerService.sample_object;
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
        public List<SampleCustomer> GetAllCustomers()
        {
            try { return CManager.GetAllCustomers().Select(temp => new SampleCustomer { ID = "https://localhost:44316/api/Customers/" + temp.ID.ToString(), Name = temp.Name, Adress = temp.Adress, OrderIds = temp.orderList.Select(s => "https://localhost:44316/api/Customers/Orders/" + s.ID.ToString()).ToList() }).ToList(); }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }
        [HttpGet("{id}")]
        public ActionResult<SampleCustomer> GetCustomer(int id)
        {
            try
            {
                var temp = CManager.GetCustomer(id);
                return  new SampleCustomer { ID = "https://localhost:44316/api/Customers/" + temp.ID.ToString(), Name = temp.Name, Adress = temp.Adress, OrderIds = temp.orderList.Select(s => "https://localhost:44316/api/Customers/Orders/" + s.ID.ToString()).ToList() };
            }
            catch { return NotFound("Customer doesn't exist"); }
        }

        [HttpPost]
        public ActionResult<Customer> PostCustomer([FromBody] sample_object.SampleCustomer cus)
        {
            try
            {
                Customer customer = new Customer(cus.Name, cus.Adress);
                if (CManager.ExistCustomerCheck(customer))
                    return BadRequest("Customer already exists");
                CManager.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.ID }, customer);
            }
            catch
            {
                throw new Exception("invalid values");
            }
        }
        [HttpPut("{ID}")]
        public ActionResult<Customer> PutCustomer(int ID, [FromBody] sample_object.SampleCustomer cus)
        {
            try
            {
                Customer customer = CManager.GetCustomer(ID);
                var temp = new Customer(cus.Name, cus.Adress);
                if (CManager.ExistCustomerCheck(temp))
                    return BadRequest("Customer already exists");

                customer.SetAdress(cus.Adress);
                customer.SetName(cus.Name);

                CManager.UpdateCustomer(ID, customer);


                return CreatedAtAction(nameof(GetCustomer), new { id = ID }, CManager.GetCustomer(ID));
            }
            catch { return NotFound("Customer doesn't exist"); }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                var temp = CManager.GetCustomer(id);
                if (temp.orderList.Count == 0)
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
