using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class CustomerManager
    {
        public IUnitOfWork uow;
        public CustomerManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddCustomer(Customer customer)
        {
            if (uow.CustomerRepository.CustomerExist(customer.Name, customer.Adress))
                throw new Exception("Customer exists");
            else
            {
                uow.CustomerRepository.add(customer);
                uow.Complete();
            }
        }

        public Customer GetCustomer(int Id)
        {
            return uow.CustomerRepository.getById(Id);
        }

        public void DeleteCustomer(int Id)
        {
            uow.CustomerRepository.delete(Id);
            uow.Complete();
        }

        public List<Customer> GetAllCustomers()
        {
            return uow.CustomerRepository.getAll();
        }
        public void DeleteAllCustomer()
        {
            uow.CustomerRepository.removeAll();
            uow.Complete();
        }
        public void UpdateCustomer(int ID,Customer customer)
        {
            uow.CustomerRepository.update(ID,customer);
            uow.Complete();
        }

        public bool ExistCustomerCheck(Customer customer)
        {
            return uow.CustomerRepository.CustomerExist(customer.Name, customer.Adress);
        }
    }
}
