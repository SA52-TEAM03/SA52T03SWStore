using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Models
{
    public class ACode
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public string ACChain { get; set; }

        [ForeignKey("OrderDetailId")]
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
