using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CustomerService.sample_object
{
    public class SampleCustomer
    {
        public SampleCustomer(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        public string Name { get; set; }
        public string Adress { get; set; }
    }
}
