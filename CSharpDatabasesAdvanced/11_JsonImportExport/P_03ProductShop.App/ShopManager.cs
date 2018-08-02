using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P_03ProductShop.App.ModelsDto;
using P_03ProductShop.Data;
using P_03ProductShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace P_03ProductShop.App
{
  public  class ShopManager
    {
        IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();

        public void ResetDatabase()
        {
            using (var context = new ProductShopContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();
            }
        }

        public void ImportData()
        {
            using (var context = new ProductShopContext())
            {
                List<int> userIds = ImportUsers(mapper, context);

                ImportCategories(mapper, context);

                ImportProducts(mapper, context, userIds);

                ImportCategoryProducts(context);
            }
            
        }

        public void ProductInRangeExport()
        {
            using (var context = new ProductShopContext())
            {
                var filteredProducts = context
                    .Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p => new 
                    {
                        name = p.Name,
                        price = p.Price,
                        buyer = String.Concat(p.Buyer.FirstName," ",p.Buyer.LastName)
                    })
                    .OrderBy(p=>p.price)
                    .ToArray();

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/products-in-range.json";

                string jsonString = JsonConvert.SerializeObject(filteredProducts, Newtonsoft.Json.Formatting.Indented);

                Console.WriteLine(jsonString);

                File.WriteAllText(path, jsonString);
            }
        }

        public void SoldProductsExport()
        {
            using (var context = new ProductShopContext())
            {
                var filteredUsers = context
                    .Users
                    .Where(u => u.ProductsToSell.Any(p=>p.Buyer!=null))
                    .Select(u => new 
                    {          
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        soldProducts = u.ProductsToSell.Where(p=>p.Buyer!=null).Select(p=>new 
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        }).ToArray()
                       
                    })
                    .OrderBy(u => u.lastName)
                    .ThenBy(u=>u.firstName)
                    .ToArray();

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/users-sold-products.json";

                string jsonString = JsonConvert.SerializeObject(filteredUsers, Newtonsoft.Json.Formatting.Indented);

                Console.WriteLine(jsonString);

                File.WriteAllText(path, jsonString);
            }
        }

        public void CategoryByProductExport()
        {
            using (var context = new ProductShopContext())
            {
                var categories = context.Categories
                                    .OrderByDescending(c => c.CategoryProducts.Count)
                                    .Select(c => new
                                    {
                                        category = c.Name,
                                        productsCount = c.CategoryProducts.Count,
                                        averagePrice = c.CategoryProducts.Sum(cp => cp.Product.Price)/c.CategoryProducts.Count(),
                                        totalRevenue = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Sum()
                                    }).ToArray();

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/categories-by-products.json";

                string jsonString = JsonConvert.SerializeObject(categories, Newtonsoft.Json.Formatting.Indented);

                Console.WriteLine(jsonString);

                File.WriteAllText(path, jsonString);


            }
        }

        public void UsersAndProductsExport()
        {
            using (var context = new ProductShopContext())
            {
                var usersCount = context.Users
                    .Where(u => u.ProductsToSell.Count > 0)
                    .Count();
                var usersProducts = new 
                {
                    usersCount = usersCount,
                    users = context.Users
                    .Where(u => u.ProductsToSell.Count > 0)
                    .OrderByDescending(u => u.ProductsToSell.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age.ToString(),
                        soldProducts = new 
                        {
                            count = u.ProductsToSell.Count(),
                            products = u.ProductsToSell.Select(p => new 
                            {
                                name = p.Name,
                                price=p.Price                             
                            }).ToArray()                   
                        }
                    }).ToArray()
                };


                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/users-and-products.json";

                string jsonString = JsonConvert.SerializeObject(usersProducts, Newtonsoft.Json.Formatting.Indented);

                Console.WriteLine(jsonString);

                File.WriteAllText(path, jsonString);
            }
        }

        private  void ImportCategoryProducts(ProductShopContext context)
        {
            var categoriesIds = context.Categories.Select(c => c.Id).ToList();

            var productIds = context.Products.Select(p => p.Id).ToList();

            var categoriesProducts = new List<CategoryProduct>();

            var rmz = new Random();

            foreach (var productId in productIds)
            {
                var categoryId = categoriesIds[rmz.Next(0, categoriesIds.Count())];

                var categoryProduct = new CategoryProduct() { ProductId = productId, CategoryId = categoryId };

                categoriesProducts.Add(categoryProduct);
            }

            context.CategoriesProducts.AddRange(categoriesProducts);

            context.SaveChanges();
        }

        private  void ImportCategories(IMapper mapper, ProductShopContext context)
        {
            var path = "../../../../JsonImport/categories.json";

            string jsonText = File.ReadAllText(path);

            var deserializedCategories = JsonConvert.DeserializeObject<CategoryDto[]>(jsonText);

            var categories = new List<Category>();

            foreach (var categoryDto in deserializedCategories)
            {
                if (!IsValid(categoryDto))
                {
                    Console.WriteLine("Category not valid!!");
                    continue;
                }
                var category = mapper.Map<Category>(categoryDto);

                categories.Add(category);
            }

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        private  void ImportProducts(IMapper mapper, ProductShopContext context, List<int> userIds)
        {
            var path = "../../../../JsonImport/products.json";

            string jsonText = File.ReadAllText(path);

            var productDtos = JsonConvert.DeserializeObject<ProductDto[]>(jsonText);

            var products = new List<Product>();

            var counter = 0;

            foreach (var productDto in productDtos)
            {
                if (!IsValid(productDto))
                {
                    Console.WriteLine("Product not valid!");
                    continue;
                }

                var product = mapper.Map<Product>(productDto);

                var sellerId = userIds[new Random().Next(0, userIds.Count)];
                var buyerId = userIds[new Random().Next(0, userIds.Count)];

                product.SellerId = sellerId;
                product.BuyerId = buyerId;


                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                counter++;

                products.Add(product);
            }
            context.Products.AddRange(products);

            context.SaveChanges();
        }

        private  List<int> ImportUsers(IMapper mapper, ProductShopContext context)
        {
            var path = "../../../../JsonImport/users.json";

            string jsonText = File.ReadAllText(path);

            var deserializeUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonText);

            var users = new List<User>();

            foreach (var userDto in deserializeUsers)
            {
                if (!IsValid(userDto))
                {
                    Console.WriteLine("User not valid!!");
                    continue;
                }
                var user = mapper.Map<User>(userDto);

                users.Add(user);
            }

            context.Users.AddRange(users);

            context.SaveChanges();

            return users.Select(u => u.Id).ToList();
        }

        private  bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationColleciton = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationColleciton, true);
        }
    }
}
