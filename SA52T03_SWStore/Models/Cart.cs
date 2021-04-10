using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SA52T03_SWStore.Data;
using System.ComponentModel.DataAnnotations;

namespace SA52T03_SWStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int qty { get; set;}
        [Key]
        public string CustomerID { get; set; }
      
     
    }
}
