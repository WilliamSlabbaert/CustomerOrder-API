using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IOrderRepository
    {
        void add(Order order);
        void delete(int id);
        List<Order> getAll();
        Order getById(int id);
        void removeAll();
        void update(int id, Order order);
        List<Order> GetallByCustomerID(int id);
    }
}
