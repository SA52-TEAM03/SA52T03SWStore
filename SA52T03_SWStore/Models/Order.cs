using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
