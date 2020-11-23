using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CustomerService.sample_object
{
    public class SampleCustomer
    {

        public String ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<String> OrderIds { get; set; } = new List<string>();
    }
}
