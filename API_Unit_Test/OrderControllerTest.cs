using API_CustomerService.Controllers;
using API_CustomerService.sample_object;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace API_Unit_Test
{
    public class OrderControllerTest
    {
        private readonly OrderController orderController;
        private readonly CustomerController customerController;

        public OrderControllerTest()
        {
            this.orderController = new OrderController();
            this.customerController = new CustomerController();
        }
        [Fact]
        public void Add_API_Orders()
        {
            orderController.DeleteAllCustomerOrders();
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Duvel", total = 1 });

            Order tempOrder = orderController.OManager.Getall().Last();

            Assert.Equal("Duvel", tempOrder.products);
            Assert.Equal(1, tempOrder.Total);
            orderController.DeleteAllCustomerOrders();
        }
        [Fact]
        public void Get_API_Orders()
        {
            orderController.DeleteAllCustomerOrders();
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Duvel", total = 1 });

            Order tempOrder = orderController.OManager.Getall().Last();
            SampleOrder tempOrder2 = orderController.GetCustomerOrder(tempOrder.ID).Value;
            Assert.Equal("Duvel", tempOrder2.products);
            Assert.Equal(1, tempOrder2.total);
            orderController.DeleteAllCustomerOrders();
        }
        [Fact]
        public void GetAll_API_Orders()
        {
            orderController.DeleteAllCustomerOrders();
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Duvel", total = 1 });
            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Orval", total = 2 });
            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Leffe", total = 3 });

            var tempOrder = orderController.GetAllOrders().Value;

            Assert.Equal("Duvel", tempOrder[0].products);
            Assert.Equal(1, tempOrder[0].total);
            Assert.Equal("Orval", tempOrder[1].products);
            Assert.Equal(2, tempOrder[1].total);
            Assert.Equal("Leffe", tempOrder[2].products);
            Assert.Equal(3, tempOrder[2].total);

            orderController.DeleteAllCustomerOrders();
        }
        [Fact]
        public void Put_API_Orders()
        {
            orderController.DeleteAllCustomerOrders();
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Duvel", total = 1 });

            Order tempOrder = orderController.OManager.Getall().Last();

            Assert.Equal("Duvel", tempOrder.products);
            Assert.Equal(1, tempOrder.Total);

            orderController.PutCustomerOrders(temp.ID, tempOrder.ID, new SampleOrder { products = "Leffe", total = 2 });

            var tempOrder2 = orderController.OManager.Getall().Last();
            Assert.Equal("Leffe", tempOrder2.products);
            Assert.Equal(2, tempOrder2.Total);

            orderController.DeleteAllCustomerOrders();
        }
        [Fact]
        public void Delete_API_Orders()
        {
            orderController.DeleteAllCustomerOrders();
            customerController.CManager.DeleteAllCustomer();

            var cus = new SampleCustomer { Name = "UnitTest", Adress = "UnitTestAdress" };
            customerController.PostCustomer(cus);
            Customer temp = customerController.CManager.GetAllCustomers().Last();

            Assert.Equal("UnitTestAdress", temp.Adress);
            Assert.Equal("UnitTest", temp.Name);

            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Duvel", total = 1 });
            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Orval", total = 2 });
            orderController.PostCustomerOrders(temp.ID, new SampleOrder { products = "Leffe", total = 3 });

            var tempOrder = orderController.GetAllOrders().Value;

            Assert.Equal("Duvel", tempOrder[0].products);
            Assert.Equal(1, tempOrder[0].total);
            Assert.Equal("Orval", tempOrder[1].products);
            Assert.Equal(2, tempOrder[1].total);
            Assert.Equal("Leffe", tempOrder[2].products);
            Assert.Equal(3, tempOrder[2].total);
            var ID = tempOrder[1].ID.Split("/")[6];

            orderController.DeleteCustomerOrders(temp.ID,  Int32.Parse(ID));
            tempOrder = orderController.GetAllOrders().Value;
            Assert.Equal(2, tempOrder.Count);


            orderController.DeleteAllCustomerOrders();
        }

    }
}

