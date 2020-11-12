using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CustomerService_Test
{
    [TestClass]
    public class Customer_Test
    {
        [TestMethod]
        public void Test_Customers_Add()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            CManager.AddCustomer(new Customer("A Name", "A Adress value"));
        }
        [TestMethod]
        public void Test_Customers_Get()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            var temp = CManager.GetCustomer(10);
            Assert.IsNotNull(temp);
            Assert.AreEqual(temp.Adress, "Test Adress");
            Assert.AreEqual(temp.Name, "Test");
            Assert.AreEqual(temp.orderList.Count, 1);
        }
        [TestMethod]
        public void Test_Customers_Delete()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            CManager.DeleteCustomer(2);
            try
            {
                var temp = CManager.GetCustomer(2);
                Assert.Fail();
            }
            catch { }
        }
        [TestMethod]
        public void Test_Customers_GetAll()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));

            Assert.AreEqual(CManager.GetAllCustomers().Count, 3);
        }
        [TestMethod]
        public void Test_Customers_DeleteAll()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            CManager.DeleteAllCustomer();
            CManager.AddCustomer(new Customer("A Name", "A Adress value"));
            CManager.AddCustomer(new Customer("A Name1", "A Adress value1"));
            CManager.AddCustomer(new Customer("A Name2", "A Adress value2"));
            Assert.AreEqual(CManager.GetAllCustomers().Count, 3);
            CManager.DeleteAllCustomer();
            Assert.AreEqual(CManager.GetAllCustomers().Count, 0);
        }
        [TestMethod]
        public void Test_Customers_Update()
        {
            CustomerManager CManager = new CustomerManager(new UnitOfWork(new DataContext("test")));
            CManager.DeleteAllCustomer();
            CManager.AddCustomer(new Customer("A Name", "A Adress value"));

            var temp = CManager.GetCustomer(10);

            Assert.AreEqual(temp.Adress, "A Adress value");
            Assert.AreEqual(temp.Name, "A Name");


            CManager.UpdateCustomer(temp.ID, new Customer("Test", "Test Adress"));

            temp = CManager.GetCustomer(10);
            Assert.AreEqual(temp.Adress, "Test Adress");
            Assert.AreEqual(temp.Name, "Test");
        }


    }
}
