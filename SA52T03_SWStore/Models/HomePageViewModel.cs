using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Product> Product { get; set; }

        public IEnumerable<Category> Category { get; set; }

        public Pager Pager { get; set; }
    }
}
