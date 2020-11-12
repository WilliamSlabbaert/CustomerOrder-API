using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface ICustomerRepository
    {
        public void add(Customer customer);
        public void delete(int id);
        public List<Customer> getAll();
        public Customer getById(int id);
        public void removeAll();
        public void update(int ID, Customer customer);
        bool CustomerExist(String name, String adress);

    }
}
