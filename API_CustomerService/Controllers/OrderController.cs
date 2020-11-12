using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
       

    }
}
