using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                    .Select(p => new ProductInRangeDto
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Buyer = String.Concat(p.Buyer.FirstName," ",p.Buyer.LastName)
                    })
                    .OrderBy(p=>p.Price)
                    .ToArray();

                Directory.CreateDirectory("../../../../XmlExport");

                var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                XmlSerializer serializer = new XmlSerializer(typeof(ProductInRangeDto[]),new XmlRootAttribute("products"));
                serializer.Serialize(new StreamWriter("../../../../XmlExport/products-in-range.Xml"),filteredProducts,xmlNamespaces);
            }
        }

        public void SoldProductsExport()
        {
            using (var context = new ProductShopContext())
            {
                var filteredUsers = context
                    .Users
                    .Where(u => u.ProductsToSell.Count>0)
                    .Select(u => new SoldProductsUserDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        SoldProducts = u.ProductsToSell.Select(p=>new SoldProductsProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        }).ToArray()
                    })
                    .OrderBy(u => u.LastName)
                    .ThenBy(u=>u.FirstName)
                    .ToArray();

                Directory.CreateDirectory("../../../../XmlExport");

                var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});

                XmlSerializer serializer = new XmlSerializer(typeof(SoldProductsUserDto[]), new XmlRootAttribute("users"));
                serializer.Serialize(new StreamWriter("../../../../XmlExport/users-sold-products.Xml"), filteredUsers,xmlNamespaces);
            }
        }

        public void CategoryByProductExport()
        {
            using (var context = new ProductShopContext())
            {
                var categories = context.Categories
                                    .OrderByDescending(c => c.CategoryProducts.Count)
                                    .Select(c => new CategoryByProductDto()
                                    {
                                        Name = c.Name,
                                        ProductsCount = c.CategoryProducts.Count,
                                        AveragePrice = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Average(),
                                        AllRevenue = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Sum()
                                    }).ToArray();

                Directory.CreateDirectory("../../../../XmlExport");

                XmlSerializer serializer = new XmlSerializer(typeof(CategoryByProductDto[]),new XmlRootAttribute("categories"));

                XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                serializer.Serialize(new StreamWriter("../../../../XmlExport/categories-by-products.Xml"),categories,xmlNamespaces);


            }
        }

        public void UsersAndProductsExport()
        {
            using (var context = new ProductShopContext())
            {
                var usersCount = context.Users
                    .Where(u => u.ProductsToSell.Count > 1)
                    .Count();
                var usersProducts = new UsersAndProductsUsersDto()
                {
                    Count = usersCount,
                    Users = context.Users
                    .Where(u => u.ProductsToSell.Count > 1)
                    .OrderByDescending(u => u.ProductsToSell.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new UsersAndProductsUserDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age.ToString(),
                        SoldProducts = new UsersAndProductsProductsDto()
                        {
                            Count = u.ProductsToSell.Count(),
                            Products = u.ProductsToSell.Select(p => new UsersAndProductsProductDto()
                            {
                                Name = p.Name,
                                Price=p.Price                             
                            }).ToArray()                   
                        }
                    }).ToArray()
                };
     

                Directory.CreateDirectory("../../../../XmlExport");

                XmlSerializer serializer = new XmlSerializer(typeof(UsersAndProductsUsersDto), new XmlRootAttribute("users"));
                XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});

                serializer.Serialize(new StreamWriter("../../../../XmlExport/users-and-products.xml"), usersProducts, xmlNamespaces);
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
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));

            var deserializedCategories = (CategoryDto[])serializer.Deserialize(new StreamReader("../../../../XmlImport/categories.xml"));

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
            XmlSerializer serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            var productDtos = (ProductDto[])serializer.Deserialize(new StreamReader("../../../../XmlImport/products.xml"));

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
            XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            var deserializeUsers = (UserDto[])serializer.Deserialize(new StreamReader("../../../../XmlImport/users.xml"));

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
