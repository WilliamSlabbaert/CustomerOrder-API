using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext context;
        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public void add(Customer customer)
        {
            context.CustomerData.Add(customer);
        }

        public void delete(int id)
        {
            context.CustomerData.Remove(getById(id));
        }

        public List<Customer> getAll()
        {
            return context.CustomerData.Include(s =>s.orderList).ThenInclude(s =>s.Customer).ToList();
        }

        public Customer getById(int id)
        {
            Customer temp = context.CustomerData.Include(s => s.orderList).Where(s => s.ID == id).ToList()[0];
            if (temp != null)
                return temp;
            else
                return null;
        }

        public void removeAll()
        {
            foreach (var item in getAll())
                delete(item.ID);
        }

        public void update( int ID, Customer customer)
        {
            Customer x = getById(ID);
            x.SetAdress(customer.Adress);
            x.SetName(customer.Name);
        }
        public bool CustomerExist(String name,String adress)
        {
            Customer x = getAll().FirstOrDefault(s => s.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", "") && s.Adress.ToLower().Replace(" ", "") == adress.ToLower().Replace(" ", ""));
            if (x == null)
                return false;
            return true;
        }
    }
}
