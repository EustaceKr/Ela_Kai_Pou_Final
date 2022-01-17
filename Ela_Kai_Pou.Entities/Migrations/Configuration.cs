namespace Ela_Kai_Pou.Entities.Migrations
{
    using Ela_Kai_Pou.Entities.Enums;
    using Ela_Kai_Pou.Entities.Interfaces;
    using Ela_Kai_Pou.Entities.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ela_Kai_Pou.Entities.CoffeeShopDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoffeeShopDb context)
        {
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Espresso",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Espresso",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Cappuccino",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Cappuccino",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Latte",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Latte",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Macchiato",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Macchiato",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Flatwhite",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Flatwhite",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Freddo Espresso",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Freddo Espresso",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Freddo Cappuccino",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Freddo Cappuccino",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Iced Latte",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Iced Latte",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Iced Macchiato",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Iced Macchiato",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Iced Flatwhite",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Iced Flatwhite",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }

            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Elenoncino",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "The Best coffee EVERRRR",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }

            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Fredo Petricio",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Fredo Petricio",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }

            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Caffe Chounta",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Caffe Chounta",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Besbalntano",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Besbalntano",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
            foreach (var size in Enum.GetValues(typeof(Size)))
            {
                foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
                {
                    context.Products.AddOrUpdate(
                    new Coffee
                    {
                        Name = "Ristretto Stathato",
                        Price = 1m + Convert.ToDecimal(size) / 2,
                        Description = "Ristretto Stathato",
                        Size = (Size)size,
                        Sweetness = (Sweetness)sweetness
                    });
                }
            }
        }
    }
}
