using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerService_Test
{
    [TestClass]
    public class Order_Test
    {
        [TestMethod]
        public void Test_Order_Add()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            CManager.AddCustomer(new Customer("test A Name", "test A Adress value"));

            Customer temp = CManager.GetCustomer(10);
            Order temp_order = new Order(temp, "Leffe", 70);

            OManager.AddOrder(temp_order);

            Assert.AreEqual(temp.Name, "Test");
            Assert.AreEqual(temp.Adress, "Test Adress");
        }

        [TestMethod]
        public void Test_Order_Get()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            
            var temp = OManager.GetOrder(1);
            Assert.AreEqual(temp.products, "Leffe");
            Assert.AreEqual(temp.Customer.ID, 1);
            Assert.AreEqual(temp.Total, 70);
        }

        [TestMethod]
        public void Test_Order_GetAllOrdersByCustomerID()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            Customer temp = CManager.GetCustomer(1);
            OManager.AddOrder(new Order(temp, "Westmalle", 70));
            OManager.AddOrder(new Order(temp, "Leffe", 70));

            var tempList = OManager.GetAllOrdersByCustomerId(1);
            Assert.AreEqual(tempList[0].products, "Westmalle");
            Assert.AreEqual(tempList[1].products, "Leffe");
            Assert.AreEqual(tempList.Count, 2);
        }

        [TestMethod]
        public void Test_Order_DeleteByID()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            Customer temp = CManager.GetCustomer(1);
            OManager.AddOrder(new Order(temp, "Leffe", 70));

            Assert.AreEqual(OManager.Getall().Count, 1);
            OManager.DeleteOrderById(12);
            Assert.AreEqual(OManager.Getall().Count, 0);
        }

        [TestMethod]
        public void Test_Order_DeleteByAll()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            OManager.DeleteAllOrders();
            Customer temp = CManager.GetCustomer(1);
            OManager.AddOrder(new Order(temp, "Leffe", 70));
            OManager.AddOrder(new Order(temp, "Westmalle", 12));
            OManager.AddOrder(new Order(temp, "Duvel", 2));

            Assert.AreEqual(OManager.Getall().Count, 3);
            OManager.DeleteAllOrders();
            Assert.AreEqual(OManager.Getall().Count, 0);
        }

        [TestMethod]
        public void Test_Order_Update()
        {
            OrderManager OManager = new OrderManager(new UnitOfWork(new DataContext("test")));
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));

           

            var tempOrder = OManager.GetOrder(14);
            Assert.AreEqual(tempOrder.Total, 70);
            Assert.AreEqual(tempOrder.products, "Leffe");

            tempOrder.SetProduct("Westmalle");
            tempOrder.SetTotal(15);

            OManager.UpdateOrder(tempOrder);

            tempOrder = OManager.GetOrder(14);
            Assert.AreEqual(tempOrder.Total, 15);
            Assert.AreEqual(tempOrder.products, "Westmalle");
            OManager.DeleteAllOrders();
        }
    }
}
