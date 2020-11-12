using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class OrderManager
    {
        public IUnitOfWork uow;

        public OrderManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddOrder(Order order)
        {
            uow.OrderRepository.add(order);
            uow.Complete();
        }
        public Order GetOrder(int ID)
        {
            return uow.OrderRepository.getById(ID);
        }
        public List<Order> Getall()
        {
            return uow.OrderRepository.getAll();
        }
        public List<Order> GetAllOrdersByCustomerId(int ID)
        {
            return uow.OrderRepository.GetallByCustomerID(ID);
        }
        public void DeleteOrderById(int ID)
        {
            uow.OrderRepository.delete(ID);
            uow.Complete();
        }
        public void DeleteAllOrders()
        {
            uow.OrderRepository.removeAll();
            uow.Complete();
        }
        public void UpdateOrder(Order order)
        {
            uow.OrderRepository.update(order.ID,order);
            uow.Complete();
        }
    }
}
