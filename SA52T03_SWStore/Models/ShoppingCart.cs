using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
