using Store_management_App.Modules;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Store_management_App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreManagementDB())
            {

                //CleanCagories();
                //InsertCagories();

                //CleanProducers();
                //InsertProducers();

                //CleanProducts();
                //InsertProducts();

                //CleanLabelProduct();
                //InsertLabelProduct();

                Console.WriteLine($"Etichetele din baza de date sunt: ");
                db.LabelProduct.Include(x => x.Product).ToList().ForEach(x => { Console.WriteLine($"{x.Id}, {x.Barcode}, {x.Price}, {x.Product.ToString()}"); });

                Console.WriteLine($"Total stock: " + GetTotalStockValue());
                Console.WriteLine($"Total stock for LG: " + GetTotalStockValueForProducer(16));

                Console.WriteLine($"Total stock per category: " + GetTotalStockValueForProducer(16));

                var totalPerCat = GetTotalStockValuePerCategory();

                foreach (var category in totalPerCat)
                {
                    Console.WriteLine($"Category: {category.Key}, Total Stock Value: {category.Value}");
                }

                var stockPerCategoryPerManufacturer = GetStockValueByCategoryAndManufacturer();

                foreach (var kvp in stockPerCategoryPerManufacturer)
                {
                    Console.WriteLine($"Category: {kvp.Key.Item1}, Producer: {kvp.Key.Item2}, Total Stock Value: {kvp.Value}");
                }




                Console.ReadLine();


            }
        }


        #region Categories
        public static void InsertCagories()
        {
            using (var db = new StoreManagementDB())
            {
                var categories = new[]
                {
                        new Category { Name = "Electronics", Icon = "https://www.example.com/electronics.png" },
                        new Category { Name = "Clothing", Icon = "https://www.example.com/clothing.png" },
                        new Category { Name = "Footwear", Icon = "https://www.example.com/footwear.png" },
                        new Category { Name = "Home Appliances", Icon = "https://www.example.com/home-appliances.png" },
                        new Category { Name = "Personal Care", Icon = "https://www.example.com/personal-care.png" },
                        new Category { Name = "Sports Equipment", Icon = "https://www.example.com/sports-equipment.png" },
                        new Category { Name = "Beauty Products", Icon = "https://www.example.com/beauty-products.png" },
                        new Category { Name = "Furniture", Icon = "https://www.example.com/furniture.png" },
                        new Category { Name = "Toys and Games", Icon = "https://www.example.com/toys-and-games.png" },
                        new Category { Name = "Books and Stationery", Icon = "https://www.example.com/books-and-stationery.png" },
                };


                db.Category.AddRange(categories);
                db.SaveChanges();
            }
        }

        public static void CleanCagories()
        {
            using (var db = new StoreManagementDB())
            {
                TruncateTable<Category>(db);
                db.SaveChanges();
            }
        }

        public void AddCategory(string categoryName, string categoryIcon)
        {
            using (var db = new StoreManagementDB())
            {
                var category = new Category { Name = categoryName, Icon = categoryIcon };
                db.Category.Add(category);
                db.SaveChanges();
            }
        }

        #endregion

        #region Producers

        public static void InsertProducers()
        {
            using (var db = new StoreManagementDB())
            {
                var producers = new[]
                {
                    new Producer { Name = "Sony", Address = "Tokyo, Japan", CUI = "1234567890" },
                    new Producer { Name = "Apple", Address = "Cupertino, CA, USA", CUI = "0987654321" },
                    new Producer { Name = "Nike", Address = "Beaverton, OR, USA", CUI = "1357908642" },
                    new Producer { Name = "Samsung", Address = "Seoul, South Korea", CUI = "2468013579" },
                    new Producer { Name = "Adidas", Address = "Herzogenaurach, Germany", CUI = "9876543210" },
                    new Producer { Name = "LG", Address = "Seoul, South Korea", CUI = "0123456789" },
                    new Producer { Name = "Under Armour", Address = "Baltimore, MD, USA", CUI = "4567890123" },
                    new Producer { Name = "Puma", Address = "Herzogenaurach, Germany", CUI = "7890123456" },
                    new Producer { Name = "Microsoft", Address = "Redmond, WA, USA", CUI = "1593572468" },
                    new Producer { Name = "Levi's", Address = "San Francisco, CA, USA", CUI = "7539514682" },
                };

                db.Producer.AddRange(producers);
                db.SaveChanges();
            }
        }

        public static void CleanProducers()
        {
            using (var db = new StoreManagementDB())
            {
                TruncateTable<Producer>(db);
                db.SaveChanges();
            }
        }

        public void AddProducer(string producerName, string producerCountry, string producerCUI)
        {
            using (var db = new StoreManagementDB())
            {
                var producer = new Producer { Name = producerName, Address = producerCountry, CUI = producerCUI };
                db.Producer.Add(producer);
                db.SaveChanges();
            }
        }
        #endregion

        #region Products
        public static void InsertProducts()
        {
            using (var db = new StoreManagementDB())
            {
                var products = new[]
                {
                    new Product { Name = "Sony PlayStation 5", Stock = 10, ProducerId = 11, CategoryId = 71 },
                    new Product { Name = "Apple iPhone 13", Stock = 20, ProducerId = 12, CategoryId = 71 },
                    new Product { Name = "Nike Air Max 2090", Stock = 15, ProducerId = 13, CategoryId = 73 },
                    new Product { Name = "Samsung Galaxy S21", Stock = 8, ProducerId = 14, CategoryId = 71 },
                    new Product { Name = "Adidas Ultraboost 21", Stock = 12, ProducerId = 15, CategoryId = 73 },
                    new Product { Name = "LG OLED CX", Stock = 5, ProducerId = 16, CategoryId = 71 },
                    new Product { Name = "Under Armour HOVR Phantom", Stock = 18, ProducerId = 17, CategoryId = 73 },
                    new Product { Name = "Puma RS-X", Stock = 10, ProducerId = 18, CategoryId = 73 },
                    new Product { Name = "Microsoft Surface Laptop 4", Stock = 17, ProducerId = 19, CategoryId = 71 },
                    new Product { Name = "Levi's 501 Original Fit Jeans", Stock = 25, ProducerId = 20, CategoryId = 72 }
                };

                db.Product.AddRange(products);
                db.SaveChanges();
            }
        }

        public static void CleanProducts()
        {
            using (var db = new StoreManagementDB())
            {
                TruncateTable<Product>(db);
                db.SaveChanges();
            }
        }

        #endregion

        #region LabelProduct
        public static void InsertLabelProduct()
        {
            using (var db = new StoreManagementDB())
            {
                var labelProducts = new[]
                {
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 499.99M, ProductId = 12 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 1099.99M, ProductId = 13 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 149.99M, ProductId = 14 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 799.99M, ProductId = 15 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 159.99M, ProductId = 16 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 1499.99M, ProductId = 17 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 129.99M, ProductId = 18 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 89.99M, ProductId = 19 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 1199.99M, ProductId = 20 },
                    new LabelProduct { Barcode = Guid.NewGuid().ToString(), Price = 59.99M, ProductId = 21 }
                };

                db.LabelProduct.AddRange(labelProducts);
                db.SaveChanges();
            }
        }

        public static void CleanLabelProduct()
        {
            using (var db = new StoreManagementDB())
            {
                TruncateTable<LabelProduct>(db);
                db.SaveChanges();
            }
        }

        public static void UpdateProductPrice(int productId, decimal newPrice)
        {
            using (var db = new StoreManagementDB())
            {
                var product = db.Product.Find(productId);
                if (product == null)
                {
                    Console.WriteLine($"Product with ID {productId} does not exist.");
                    return;
                }

                var labelProduct = db.LabelProduct.FirstOrDefault(lp => lp.ProductId == productId);
                if (labelProduct == null)
                {
                    Console.WriteLine($"LabelProduct for product with ID {productId} does not exist.");
                    return;
                }

                labelProduct.Price = newPrice;
                db.SaveChanges();
                Console.WriteLine($"Price of product with ID {productId} has been updated to {newPrice}.");
            }
        }

        #endregion

        #region Method Delete
        public static void TruncateTable<T>(DbContext db) where T : class
        {
            IEntityType entityType = db.Model.FindEntityType(typeof(T));
            string tableName = entityType.GetTableName();
            db.Database.ExecuteSqlRaw($"Delete From {tableName}");
        }
        #endregion

        public static decimal GetTotalStockValue()
        {
            using (var db = new StoreManagementDB())
            {
                var totalValue = db.LabelProduct.Where(x => x.ProductId == x.Product.Id).Sum(x => x.Price * x.Product.Stock);

                return totalValue;
            }
        }


        public static decimal GetTotalStockValueForProducer(int producerId)
        {
            using (var db = new StoreManagementDB())
            {
                var totalValue = db.LabelProduct.Where(x => x.ProductId == producerId).Sum(x => x.Price * x.Product.Stock);

                return totalValue;
            }
        }


        public static Dictionary<string, decimal> GetTotalStockValuePerCategory()
        {
            using (var db = new StoreManagementDB())
            {
                var result = db.LabelProduct.Include(x => x.Product)
                    .GroupBy(p => p.Product.Category.Name)
                    .Select(g => new { CategoryName = g.Key, TotalValue = g.Sum(p => p.Product.Stock * p.Price) })
                    .ToDictionary(x => x.CategoryName, x => x.TotalValue);

                return result;
            }
        }

        public static Dictionary<(string,int), decimal> GetStockValueByCategoryAndManufacturer()
        {
            using (var db = new StoreManagementDB())
            {
                var result = db.LabelProduct.Include(lp => lp.Product)
                    .GroupBy(lp => new { lp.Product.Category.Name, lp.Product.Producer.Id })
                    .Select(g => new
                    {
                        CategoryName = g.Key.Name,
                        ProducerName = g.Key.Id,
                        TotalValue = g.Sum(lp => lp.Product.Stock * lp.Price)
                    })
                    .ToDictionary(x => (x.CategoryName, x.ProducerName), x => x.TotalValue);

                return result;
            }
        }
    }

 }