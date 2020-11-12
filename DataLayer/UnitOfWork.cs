using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    
    public class UnitOfWork : IUnitOfWork
    {
        DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
            CustomerRepository = new CustomerRepository(context);
            OrderRepository = new OrderRepository(context);
        }

        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
