using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public int Complete();
        public void Dispose();
    }
}
