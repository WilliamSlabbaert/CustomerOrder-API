using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CustomerService.sample_object
{
    public class SampleOrder
    {
        public String ID { get; set; }
        public String CustomerID { get; set; }
        public String products { get; set; }
        public int total { get; set; }
        
    }
}
