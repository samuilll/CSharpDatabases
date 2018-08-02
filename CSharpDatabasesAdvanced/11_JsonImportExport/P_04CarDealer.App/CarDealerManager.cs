using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P_04CarDealer.App.ModelsDto;
using P_04CarDealer.Data;
using P_04CarDealer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace P_04CarDealer.App
{
    public class CarDealerManager
    {
        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();

        public void ResetDatabase()
        {
            using (var context = new CarDealerContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();
            }
        }

        public void ImportData()
        {
            ImportSuppliers();

            ImportParts();

            ImportCars();

            ImportCarParts();

            ImportCustomers();

            ImportSales();
        }

        public void FerrariCarsByDistanceExport()
        {
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c =>c.Make=="Toyota")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c=>c.TravelledDistance)
                    .Select(c => new 
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(cars, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/toyota-cars.json";

                File.WriteAllText(path, jsonString);
            }
        }

        public void LocalSuppliersExport()
        {
            using (var context = new CarDealerContext())
            {
                var suppliers = context.Suppliers
                    .Where(s=>s.IsImporter==false)
                    .Select(s => new 
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount=s.Parts.Count()
                    })
                    .ToArray();


                var jsonString = JsonConvert.SerializeObject(suppliers, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/local-suppliers.json";

                File.WriteAllText(path, jsonString);
            }
        }

        public void TotalSalesByCustomerExport()
        {
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count() > 0)
                    .Select(c => new 
                    {
                        fullName = c.Name,
                        boughtCars = c.Sales.Count(),
                        spentMoney = c.Sales.Sum(s=>s.Car.PartCars.Sum(pc=>pc.Part.Price))
                    })
                    .OrderByDescending(c=>c.spentMoney)
                    .ThenByDescending(c=>c.boughtCars)
                    .ToArray();


                var jsonString = JsonConvert.SerializeObject(customers, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/customers-total-sales.json";

                File.WriteAllText(path, jsonString);
            }
        }

        public void CarsWithTheirListOfPartsExport()
        {
            using (var context = new CarDealerContext())
            {
                var carsWithParts = context.Cars
                     .Select(c => new 
                     {
                       car = new
                       {
                           Make = c.Make,
                           Model = c.Model,
                           TravelledDistance = c.TravelledDistance
                       },
                        parts = c.PartCars.Select(pc => new 
                         {
                             Name = pc.Part.Name,
                             Price = pc.Part.Price
                         }).ToArray()
                     })
                     .ToArray();


                var jsonString = JsonConvert.SerializeObject(carsWithParts, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/cars-and-parts.json";

                File.WriteAllText(path, jsonString);
            }
        }

        public void SalesWithAppliedDiscountExport()
        {
            using (var context = new CarDealerContext())
            {
            var sales = context.Sales
                      .Select(s => new SaleDto
                      {
                          Car = new SaleCarDto
                          {
                              Make = s.Car.Make,
                              Model = s.Car.Model,
                              TravelledDistance = s.Car.TravelledDistance
                          },
                          CustomerName = s.Customer.Name,
                          Discount = s.Discount,
                          Price = s.Car.PartCars.Select(pc => pc.Part.Price).Sum(),
                         // PriceWithDiscount = s.Pr
                      })
                      .ToArray();

                var jsonString = JsonConvert.SerializeObject(sales, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/sales-discounts.json";

                File.WriteAllText(path, jsonString);
            }
        }

        private static void ImportSales()
        {
            using (var context = new CarDealerContext())
            {
                decimal[] discounts = new decimal[] { 0, 0.05m, 0.1m, 0.15m, 0.20m, 0.30m, 0.40m, 0.50m };

                var carIds = context.Cars.Select(c => c.Id).ToList();
                var customers = context.Customers.ToList();

                var randomizer = new Random();

                var sales = new List<Sale>();

                for (int i = 0; i < 300; i++)
                {
                    var discount = discounts[randomizer.Next(0, discounts.Length)];
                    var carId = carIds[randomizer.Next(0, carIds.Count)];
                    var customer = customers[randomizer.Next(0, customers.Count)];
                    var customerId = customer.Id;

                    if (customer.IsYoungDriver)
                    {
                        discount += 0.05m;
                    }

                    var sale = new Sale
                    {
                        CustomerId = customerId,
                        CarId = carId,
                        Discount = discount
                    };

                    sales.Add(sale);
                }

                context.Sales.AddRange(sales);

                context.SaveChanges();
            }
        }

        private void ImportCustomers()
        {
            var path = "../../../../JsonImport/customers.json";

            var customerDtos = JsonConvert.DeserializeObject<CustomerDto[]>(File.ReadAllText(path), new JsonSerializerSettings()
            {
            });

            var customers = new List<Customer>();

            using (var context = new CarDealerContext())
            {
                foreach (var customerDto in customerDtos)
                {
                    if (!IsValid(customerDto))
                    {
                        Console.WriteLine("Invalid Customer");
                        continue;
                    }

                    var customer = this.mapper.Map<Customer>(customerDto);

                    customers.Add(customer);
                }

                context.Customers.AddRange(customers);

                context.SaveChanges();
            }
        }

        private static void ImportCarParts()
        {
            using (var context = new CarDealerContext())
            {
                var randomizer = new Random();

                var partsIds = context.Parts.Select(s => s.Id).ToList();

                var carsWithIds = context.Cars.ToList();

                var partCars = new List<PartCar>();

                foreach (var car in carsWithIds)
                {
                    var currentPartsIds = new List<int>(partsIds);

                    var partsCount = randomizer.Next(10, 20);

                    for (int i = 0; i < partsCount; i++)
                    {
                        var partId = currentPartsIds[randomizer.Next(0, currentPartsIds.Count)];

                        currentPartsIds.Remove(partId);

                        var partCar = new PartCar
                        {
                            CarId = car.Id,
                            PartId = partId
                        };

                        partCars.Add(partCar);
                    }

                }

                context.PartCars.AddRange(partCars);

                context.SaveChanges();
            }
        }

        private void ImportCars()
        {
            var path = "../../../../JsonImport/cars.json";

            var carDtos = JsonConvert.DeserializeObject<CarDto[]>(File.ReadAllText(path));

            var cars = new List<Car>();

            using (var context = new CarDealerContext())
            {
                foreach (var carDto in carDtos)
                {
                    if (!IsValid(carDto))
                    {
                        Console.WriteLine("Invalid Car");
                        continue;
                    }

                    var car = this.mapper.Map<Car>(carDto);

                    cars.Add(car);
                }

                context.Cars.AddRange(cars);

                context.SaveChanges();
            }
        }

        private void ImportParts()
        {
            var path = "../../../../JsonImport/parts.json";

            var partDtos = JsonConvert.DeserializeObject<PartDto[]>(File.ReadAllText(path));

            var parts = new List<Part>();

            using (var context = new CarDealerContext())
            {
                var suppliersIds = context.Suppliers.Select(s => s.Id).ToList();

                var randomizer = new Random();

                foreach (var partDto in partDtos)
                {
                    if (!IsValid(partDto))
                    {
                        Console.WriteLine("InvalidPart");
                        continue;
                    }
                    var supplierId = suppliersIds[randomizer.Next(0, suppliersIds.Count)];

                    partDto.SupplierId = supplierId;

                    var part = this.mapper.Map<Part>(partDto);

                    parts.Add(part);
                }

                context.Parts.AddRange(parts);

                context.SaveChanges();
            }
        }

        private void ImportSuppliers()
        {
            using (var context = new CarDealerContext())
            {
                var path = "../../../../JsonImport/suppliers.json";

                var suppliersDtos = JsonConvert.DeserializeObject<SupplierDto[]>(File.ReadAllText(path));

                var suppliers = new List<Supplier>();

                foreach (var supplierDto in suppliersDtos)
                {
                    if (!IsValid(supplierDto))
                    {
                        Console.WriteLine("Invalid supplier!");
                        continue;
                    }

                    var supplier = this.mapper.Map<Supplier>(supplierDto);

                    suppliers.Add(supplier);
                }

                context.AddRange(suppliers);

                context.SaveChanges();
            }
        }

        private bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }

        public void OrderedCustomersExport()
        {
            using (var context = new CarDealerContext())
            {

                var customers = context.Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BirtdDate = c.BirthDate,
                        IsYoungDriver = c.IsYoungDriver,
                        Sales = new List<string>()
                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(customers,new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                Console.WriteLine(jsonString);

                Directory.CreateDirectory("../../../../JsonExport");

                var path = "../../../../JsonExport/ordered-customers.json";

                File.WriteAllText(path,jsonString);
            }
        }
    }
}
