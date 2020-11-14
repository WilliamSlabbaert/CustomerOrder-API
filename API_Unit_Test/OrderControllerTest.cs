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
        [Fact]
        public void GetAll_API_Orders()
        {
            var temp = orderController.GetAllOrders();

            Assert.Equal(temp.Value.Count, 2);
        }
        [Fact]
        public void Add_API_Orders()
        {
            orderController.PostCustomerOrders(12, new API_CustomerService.sample_object.SampleOrder() { products = "Duvel", total = 15 });
            var temp = orderController.GetCustomerOrder(21);

            Assert.Equal(temp.Value.total, 15);
            Assert.Equal(temp.Value.products, "Duvel");

        }
        [Fact]
        public void Get_API_Orders()
        {
           
            var temp = orderController.GetCustomerOrder(15);

            Assert.Equal(temp.Value.total, 70);
            Assert.Equal(temp.Value.products, "Leffe");

        }

        [Fact]
        public void GetAllCustomerOrders_API_Orders()
        {
            var temp = orderController.GetAllCustomerOrders(12);

            Assert.Equal(temp.Value.Count, 1);
        }

        [Fact]
        public void Put_API_Orders()
        {
            var temp = orderController.GetCustomerOrder(15);

            Assert.Equal(temp.Value.total, 70);
            Assert.Equal(temp.Value.products, "Leffe");
            Assert.Equal(temp.Value.CustomerID, 10);

            orderController.PutCustomerOrders(10,15,new API_CustomerService.sample_object.SampleOrder() { products = "Orval",total = 22,CustomerID =12});

            temp = orderController.GetCustomerOrder(15);

            Assert.Equal(temp.Value.total, 22);
            Assert.Equal(temp.Value.products, "Orval");
            Assert.Equal(temp.Value.CustomerID, 12);
        }

        [Fact]
        public void Delete_API_Orders()
        {
            var temp = orderController.GetAllCustomerOrders(12);

            Assert.Equal(temp.Value.Count, 2);

            orderController.DeleteCustomerOrders(12, 21);

            temp = orderController.GetAllCustomerOrders(12);

            Assert.Equal(temp.Value.Count, 1);

        }
        
        [Fact]
        public void DeleteAll_API_Orders()
        {

            var temp = orderController.GetAllOrders();

            Assert.Equal(temp.Value.Count, 2);

            orderController.DeleteAllCustomerOrders();

            temp = orderController.GetAllOrders();

            Assert.Equal(temp.Value.Count, 0);
        }
    }
}

