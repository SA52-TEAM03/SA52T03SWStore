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
                        Name = "ByteStore Total Protection 2021",
                        Description = "ByteStore Antivirus Total Protection 2021, 3-Device",
                        Price = 228,
                        Image = "/images/3_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "GartnerShield Antivirus 2021",
                        Description = "GartnerShield antivirus suite software, 5-licences",
                        Price = 88,
                        Image = "/images/4_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "Whitehat Hacker Pro 2021",
                        Description = "Anti-hacking tool that helps monitor network port traffic and automatically blocks suspicious network activity",
                        Price = 68,
                        Image = "/images/5_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "PenetrationProtect All-in-One 2021",
                        Description = "Baseline Security tool that allows user to set network and local security policies for servers and virtual instances",
                        Price = 188,
                        Image = "/images/6_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "WASD Network Security 2020",
                        Description = "Network monitoring software for Windows and Mac OS users",
                        Price = 115,
                        Image = "/images/7_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "SystemTech 180+180 ",
                        Description = "Holistic suite of antivirus, network security, password security and web anti-tracking tool for PC users. 5-licences, 1-year subscription",
                        Price = 399,
                        Image = "/images/8_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "AV AntiSpam Protection 2021",
                        Description = "Email anti-spam protection software. 1-year subscription",
                        Price = 38,
                        Image = "/images/9_product.png",
                        CategoryId = 4
                    },

                    new Product
                    {
                        Name = "HelloWorld Password Manager Suite 2021",
                        Description = "Secured password manager for both Windows OS and Mac OS users",
                        Price = 9.9,
                        Image = "/images/10_product.png",
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
                        Name = "Frankster Sketch 2021",
                        Description = "3D Modelling software for both designers and architects, 1-year subscription",
                        Price = 768,
                        Image = "/images/13_product.png",
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
                        Name = "Sandy MovieMax 321 v2021",
                        Description = "Movie editor for students",
                        Price = 189,
                        Image = "/images/16_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Min360 Office 2021",
                        Description = "Best-selling productivity software for students and home users",
                        Price = 229,
                        Image = "/images/17_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "FirePro Office 2021",
                        Description = "Productivity tool for home users",
                        Price = 179,
                        Image = "/images/18_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Min360 Project 2021",
                        Description = "Project management visualisation tool for scheduling and tracking of project milestones, project costs and activity lists",
                        Price = 2199,
                        Image = "/images/19_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Min360 Visual Studio 2020",
                        Description = "Integrated Design Environment for enterprise developers",
                        Price = 68,
                        Image = "/images/20_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Extol Photoshop CS9",
                        Description = "Photo editing tool",
                        Price = 138,
                        Image = "/images/21_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Extol Photoshop Lite",
                        Description = "Photo editing tool",
                        Price = 62,
                        Image = "/images/22_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Extol LightUp 2021",
                        Description = "Photo editing tool",
                        Price = 39,
                        Image = "/images/23_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Polars Photo Editor",
                        Description = "Photo editing tool",
                        Price = 29,
                        Image = "/images/24_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Illuminate Photo Editor",
                        Description = "Photo editing tool",
                        Price = 25.9,
                        Image = "/images/25_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Sketchy 2021",
                        Description = "Photo editing tool",
                        Price = 49.9,
                        Image = "/images/26_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "Draw 2021",
                        Description = "Photo editing tool",
                        Price = 8.9,
                        Image = "/images/27_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "VectorDesign Plus",
                        Description = "Photo editing tool",
                        Price = 12.9,
                        Image = "/images/28_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "CanvasPro",
                        Description = "Photo editing tool",
                        Price = 129,
                        Image = "/images/29_product.png",
                        CategoryId = 2
                    },

                    new Product
                    {
                        Name = "MakerTool Photo Pro",
                        Description = "Photo editing tool",
                        Price = 119,
                        Image = "/images/30_product.png",
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
                    },

                    new Product
                    {
                        Name = "The Chronicles Fantasy 9",
                        Description = "Windows PC, Single player Role-playing game",
                        Price = 99.9,
                        Image = "/images/33_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "Fast & Speeding: Street Race",
                        Description = "Windows PC, single player racing simulation game",
                        Price = 59.9,
                        Image = "/images/34_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "Fast & Speeding: Unwanted",
                        Description = "Windows PC, single player racing simulation game",
                        Price = 49.9,
                        Image = "/images/35_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "Corza 3: The Horizon",
                        Description = "Windows PC, single player racing simulation game",
                        Price = 89.9,
                        Image = "/images/36_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "MapleSyrup Story",
                        Description = "Windows PC, Mac OS, MMORPG",
                        Price = 129.9,
                        Image = "/images/37_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "World War Battlefield 6: Stars Arising",
                        Description = "Windows PC, Mac OS, MMORPG",
                        Price = 119.9,
                        Image = "/images/38_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "ISS City 2020",
                        Description = "Windows PC, simulation game, build your own avatar and see him/her mature",
                        Price = 79.9,
                        Image = "/images/39_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "ISS Theme Park 2020",
                        Description = "Windows PC, simulation game, build your own theme park and manage it",
                        Price = 39.9,
                        Image = "/images/40_product.png",
                        CategoryId = 1
                    },

                    new Product
                    {
                        Name = "Cooking Papa II",
                        Description = "Windows PC, multiplayer cooking simulation game",
                        Price = 99.9,
                        Image = "/images/41_product.png",
                        CategoryId = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
