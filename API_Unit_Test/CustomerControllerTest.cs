using API_CustomerService.Controllers;
using API_CustomerService.sample_object;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Add_Customer_controller()
        {
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            customerController.CManager.DeleteAllCustomer();
        }
        [Fact]
        public void Get_Customer_controller()
        {
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            SampleCustomer tempSample = customerController.GetCustomer(temp.ID).Value as SampleCustomer;
            Assert.Equal("UnitTestAdress", tempSample.Adress);
            Assert.Equal("UnitTest", tempSample.Name);

            customerController.CManager.DeleteAllCustomer();
        }

        [Fact]
        public void Getall_Customer_controller()
        {
            customerController.CManager.DeleteAllCustomer();

            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" });
            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest1", Adress = "UnitTestAdress1" });
            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest2", Adress = "UnitTestAdress2" });
            List<SampleCustomer> temp = customerController.GetAllCustomers();


            Assert.Equal("UnitTestAdress", temp[0].Adress);
            Assert.Equal("UnitTest", temp[0].Name);

            Assert.Equal("UnitTestAdress1", temp[1].Adress);
            Assert.Equal("UnitTest1", temp[1].Name);

            Assert.Equal("UnitTestAdress2", temp[2].Adress);
            Assert.Equal("UnitTest2", temp[2].Name);

            customerController.CManager.DeleteAllCustomer();
        }
        [Fact]
        public void Put_Customer_controller()
        {
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            SampleCustomer tempSample = customerController.GetCustomer(temp.ID).Value as SampleCustomer;
            Assert.Equal("UnitTestAdress", tempSample.Adress);
            Assert.Equal("UnitTest", tempSample.Name);

            var cus1 = new SampleCustomer { Name = "UnitTest1", Adress = "UnitTestAdress1" };
            customerController.PutCustomer(temp.ID, cus1);
            tempSample = customerController.GetCustomer(temp.ID).Value as SampleCustomer;
            Assert.Equal("UnitTestAdress1", tempSample.Adress);
            Assert.Equal("UnitTest1", tempSample.Name);

            customerController.CManager.DeleteAllCustomer();
        }
        [Fact]
        public void Delete_Customer_controller()
        {
            customerController.CManager.DeleteAllCustomer();

            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" });
            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest1", Adress = "UnitTestAdress1" });
            customerController.PostCustomer(new SampleCustomer { Name = "UnitTest2", Adress = "UnitTestAdress2" });
            List<SampleCustomer> temp = customerController.GetAllCustomers();


            Assert.Equal("UnitTestAdress", temp[0].Adress);
            Assert.Equal("UnitTest", temp[0].Name);

            Assert.Equal("UnitTestAdress1", temp[1].Adress);
            Assert.Equal("UnitTest1", temp[1].Name);

            Assert.Equal("UnitTestAdress2", temp[2].Adress);
            Assert.Equal("UnitTest2", temp[2].Name);

            var ID = temp[1].ID.Split("/")[5];
            customerController.DeleteCustomer(Int32.Parse(ID));
            temp = customerController.GetAllCustomers();
            Assert.Equal(2, temp.Count);


            customerController.CManager.DeleteAllCustomer();
        }
    }
}
