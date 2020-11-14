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
        [Fact]
        public void Get_API_Customer()
        {
            var temp = customerController.GetCustomer(10);
            Assert.Equal((temp.Value as Customer).Adress, "Sample adress");
            Assert.Equal((temp.Value as Customer).Name, "Sample name");
        }
        [Fact]
        public void GetAll_API_Customer()
        {
            var temp = customerController.GetAllCustomers();
            Assert.Equal(temp.Count, 2);
        }
        [Fact]
        public void Add_API_Customer()
        {
            var temp = new API_CustomerService.sample_object.SampleCustomer("Test-Name","Test-Adress");
            customerController.PostCustomer(temp);

            var tempCustomer = customerController.GetCustomer(13);
            Assert.Equal((tempCustomer.Value as Customer).Adress, "Test-Adress");
            Assert.Equal((tempCustomer.Value as Customer).Name, "Test-Name");
        }
        [Fact]
        public void Put_API_Customer()
        {
            var tempCustomer = customerController.GetCustomer(13);
            Assert.Equal((tempCustomer.Value as Customer).Adress, "Test-Adress");
            Assert.Equal((tempCustomer.Value as Customer).Name, "Test-Name");

            var temp = new API_CustomerService.sample_object.SampleCustomer("Test_Name", "Test_Adress");
            customerController.PutCustomer(13,temp);

            tempCustomer = customerController.GetCustomer(13);
            Assert.Equal((tempCustomer.Value as Customer).Adress, "Test_Adress");
            Assert.Equal((tempCustomer.Value as Customer).Name, "Test_Name");
        }
        [Fact]
        public void Delete_API_Customer()
        {
            var tempCustomer = customerController.GetCustomer(13);
            Assert.Equal((tempCustomer.Value as Customer).Adress, "Test_Adress");
            Assert.Equal((tempCustomer.Value as Customer).Name, "Test_Name");
            var temp = customerController.GetAllCustomers();
           
            Assert.Equal(temp.Count, 3);
            customerController.DeleteCustomer(13);

            temp = customerController.GetAllCustomers();
            Assert.Equal(temp.Count, 2);
        }
    }
}
