using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SA52T03_SWStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Category.Any() || context.Product.Any())
                {
                    return;   // DB has been seeded
                }

                context.Category.AddRange(
                    new Category { Name = "Game"},
                    new Category { Name = "Media"},
                    new Category { Name = "Productivity" },
                    new Category { Name = "Security" }
                );

                context.Product.AddRange(

                    new Product
                    {
                        Name = "Min360 Protection 2021",
                        Description = "Min360 Protection 2021, 5 Device, Internet Security Software, Password Manager, Privacy, 1 Year - Download Code",
                        Price = 255,
                        Image = "/images/1_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "FirePro Internet Security",
                        Description = "Internet Security Software, 1-year subscription",
                        Price = 168,
                        Image = "/images/2_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "AutoChair 3D Modelling 2021",
                        Description = "3D Modelling software for both designers and architects, 1-year subscription",
                        Price = 899,
                        Image = "/images/11_product.png",
                        CategoryId = 3
                    },

                    new Product
                    {
                        Name = "AutoChair CAD 2021",
                        Description = "Computer aided design software, 1-year subscription",
                        Price = 499,
                        Image = "/images/12_product.png",
                        CategoryId = 3
                    },

                    new Product
                    {
                        Name = "Extol Premium Pro 2021",
                        Description = "Best selling movie production and editor software tool",
                        Price = 359,
                        Image = "/images/14_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Extol Movie Basic Suite 2021",
                        Description = "Movie production software for light users",
                        Price = 199,
                        Image = "/images/15_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Angry Civilisation V",
                        Description = "Windows PC, real-time strategy game",
                        Price = 69.9,
                        Image = "/images/31_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "BattleStar Royale RPG",
                        Description = "Wndows PC, Multiplayer Role-playing game",
                        Price = 89.9,
                        Image = "/images/32_product.png",
                        CategoryId = 1
                    }

                );

                context.SaveChanges();
            }
        }
    }
}
