using BusinessLayer.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer
{
    public class Customer
    {
        public int ID { get; private set;}
        public String Name { get; private set; }
        public String Adress { get; private set; }
        public virtual ICollection<Order> orderList  { get; private set; }

        public Customer(string name, string adress)
        {
            SetName(name);
            SetAdress(adress);
        }

        public Customer()
        {
        }

        public void SetName(String name)
        {
            if (name is null || name is "")
                throw new BaseException("Name is null");
            else
                Name = name;
        }
        public void SetList(List<Order> orders)
        {
            this.orderList = orders;
        }
        public void SetAdress(String adress)
        {
            if (adress is null || adress is "")
                throw new BaseException("Adress is null");
            else if (adress.Length < 10)
                throw new BaseException("Adress is lenght is under 10 characters");
            else
                Adress = adress;
        }

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new BaseException("Order is null");
            else
                orderList.Add(order);
        }
    }
}
