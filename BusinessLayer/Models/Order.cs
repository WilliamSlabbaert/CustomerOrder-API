using BusinessLayer.utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Order
    {
        public int ID { get; set; }
        public virtual Customer Customer { get;  private set; }
        public String products { get; private set; }
        public int Total { get; private set; }

        public Order() { }

        public Order(Customer customer, string product, int total)
        {
            SetTotal(total);
            SetProduct(product);
            SetCustomer(customer);
        }

        public void SetProduct(string product)
        {
            string temp = product.ToLower().Replace(" ", "");
            if (temp == "westmalle" || temp == "duvel" || temp == "orval" || temp == "leffe")
                this.products = product;
            else
                throw new BaseException("Product is invalid");
        }

        public void SetTotal(int total)
        {
            if (total > 0)
                this.Total = total;
            else
                throw new BaseException("Total is invalid");
        }

        public void SetCustomer(Customer cus)
        {
            if (!cus.Equals(null))
                this.Customer = cus;
            else
                throw new BaseException("Customer is null");
        }
    }
}
