using API_CustomerService.Controllers;
using BusinessLayer;
using System;
using Xunit;

namespace API_Unit_Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerController customerController;

        public CustomerControllerTest()
        {
            this.customerController = new CustomerController();
        }
    }
}
