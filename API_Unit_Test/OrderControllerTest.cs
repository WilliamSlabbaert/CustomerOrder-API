using API_CustomerService.Controllers;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace API_Unit_Test
{
    public class OrderControllerTest
    {
        private readonly OrderController orderController;

        public OrderControllerTest()
        {
            this.orderController = new OrderController();
        }
        
    }
}
