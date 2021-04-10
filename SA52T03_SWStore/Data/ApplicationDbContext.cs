using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SA52T03_SWStore.Models;

namespace SA52T03_SWStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SA52T03_SWStore.Models.Category> Category { get; set; }
        public DbSet<SA52T03_SWStore.Models.Product> Product { get; set; }
        public DbSet<SA52T03_SWStore.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SA52T03_SWStore.Models.Cart> ShoppingCart { get; set; }
    }
}
