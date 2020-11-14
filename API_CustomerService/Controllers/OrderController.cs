using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_CustomerService.sample_object;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CustomerService.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public CustomerManager CManager { get; set; }
        public OrderManager OManager { get; set; }
        public OrderController()
        {
            CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
        }
        [HttpGet("Orders")]
        public ActionResult<List<SampleOrder>> GetAllOrders()
        {
            try
            {
                return OManager.Getall().Select(s => new SampleOrder() { ID = s.ID, CustomerID = s.Customer.ID, total = s.Total, products = s.products }).ToList();
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpGet("{id}/Orders")]
        public ActionResult<List<SampleOrder>> GetAllCustomerOrders(int id)
        {
            try
            {
                CManager.GetCustomer(id);
                return OManager.GetAllOrdersByCustomerId(id).Select(s => new SampleOrder() { ID = s.ID,CustomerID = s.Customer.ID,total = s.Total,products = s.products}).ToList();
            }
            catch
            {
                return NotFound("Customer doesn't exist");
            }
        }
        [HttpGet("Orders/{Order_id}")]
        public ActionResult<SampleOrder> GetCustomerOrder(int Order_id)
        {
            try
            {
                var temp = OManager.GetOrder(Order_id);
                return new SampleOrder() { ID = temp.ID,CustomerID = temp.Customer.ID,total = temp.Total,products = temp.products};
            }
            catch
            {
                return NotFound("Order doesn't exist");
            }
        }
        [HttpPost("{id}/Orders")]
        public ActionResult<Order> PostCustomerOrders(int id, [FromBody] SampleOrder order)
        {
            try
            {
                var tempCustomer = CManager.GetCustomer(id);
                Order temp = new Order(tempCustomer, order.products, order.total);
                OManager.AddOrder(temp);

                return Ok(temp);
            }
            catch
            {
                return NotFound("Customer doesn't exist");
            }
        }
        [HttpPut("{id}/Orders/{Order_ID}")]
        public ActionResult<Order> PutCustomerOrders(int id,int Order_ID, [FromBody] SampleOrder order)
        {
            try {
                var tempOrder = CManager.GetCustomer(id).orderList.FirstOrDefault(s => s.ID == Order_ID);

                if (tempOrder == null)
                    return NotFound("Order doesn't exist");
                if (order.CustomerID != 0)
                    tempOrder.SetCustomer(CManager.GetCustomer(order.CustomerID));

                tempOrder.SetProduct(order.products);
                tempOrder.SetTotal(order.total);
                OManager.UpdateOrder(tempOrder);

                return CManager.GetCustomer(id).orderList.FirstOrDefault(s => s.ID == Order_ID);
            }
            catch { return NotFound("Customer doesn't exist"); }
        }
        [HttpDelete("{id}/Orders/{Order_ID}")]
        public ActionResult DeleteCustomerOrders(int id, int Order_ID)
        {
            try
            {
                var tempCustomer = CManager.GetCustomer(id);
                var tempOrder = tempCustomer.orderList.FirstOrDefault(s => s.ID == Order_ID);
                if (tempOrder == null)
                    return NotFound("Order doesn't exist");

                OManager.DeleteOrderById(tempOrder.ID);
                return NoContent();

            }
            catch { return NotFound("Customer doesn't exist"); }
        }

        [HttpDelete("Orders")]
        public ActionResult DeleteAllCustomerOrders()
        {
            try
            {
                OManager.DeleteAllOrders();
                return NoContent();

            }
            catch { return BadRequest(); }
        }
    }
}
