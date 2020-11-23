using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class OrderRepository : IOrderRepository
    {
        DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public void add(Order order)
        {
            Customer temp = context.CustomerData.Include(s =>s.orderList).ToList().Find(s => s.ID == order.Customer.ID);
            if (temp != null)
                temp.AddOrder(order);
            else
                throw new Exception("Customer doesnt exist");
        }

        public void delete(int id)
        {
            context.OrderData.Remove(getById(id));
        }
       
        public List<Order> getAll()
        {
            return context.OrderData.Include(m => m.Customer).ToList();
        }

        public List<Order> GetallByCustomerID(int id)
        {
            return context.OrderData.Include(m => m.Customer).Where(p => p.Customer.ID == id).ToList();
        }

        public Order getById(int id)
        {
            List<Order> temp = context.OrderData.Include(m => m.Customer).Where(p => p.ID == id).ToList();
            if (temp != null)
                return temp[0];
            else
                throw new Exception("Order doesn't exist");
        }

        public void removeAll()
        {
            foreach (var item in getAll())
                delete(item.ID);
        }

        public void update(int id, Order order)
        {
            var temp = getById(id);
            temp.SetCustomer(order.Customer);
            temp.SetProduct(order.products);
            temp.SetTotal(order.Total);

            context.Update(temp);
        }
    }
}
