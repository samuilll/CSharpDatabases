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

        public void CarsByDistanceExport()
        {
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.TravelledDistance > 2000000)
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .Select(c => new CarDto
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    })
                    .ToArray();


                XmlSerializer serializer = new XmlSerializer(typeof(CarDto[]),new XmlRootAttribute("cars"));
                XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/cars.xml";

                serializer.Serialize(new StreamWriter(path), cars, xmlNameSpaces);
            }

        }

        public void FerrariCarsByDistanceExport()
        {
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c =>c.Make=="Ferrari")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c=>c.TravelledDistance)
                    .Select(c => new FerrariCarDto
                    {
                        Id = c.Id,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    })
                    .ToArray();


                XmlSerializer serializer = new XmlSerializer(typeof(FerrariCarDto[]), new XmlRootAttribute("cars"));
                XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/ferrari-cars.xml";

                serializer.Serialize(new StreamWriter(path), cars, xmlNameSpaces);
            }
        }

        public void LocalSuppliersExport()
        {
            using (var context = new CarDealerContext())
            {
                var suppliers = context.Suppliers
                    .Where(s=>s.IsImporter==false)
                    .Select(s => new LocalSupplierDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount=s.Parts.Count()
                    })
                    .ToArray();


                XmlSerializer serializer = new XmlSerializer(typeof(LocalSupplierDto[]), new XmlRootAttribute("suppliers"));
                XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/local-suppliers.xml";

                serializer.Serialize(new StreamWriter(path), suppliers, xmlNameSpaces);
            }
        }

        public void TotalSalesByCustomerExport()
        {
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count() > 0)
                    .Select(c => new CustomerWithCarsDto
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count(),
                        SpentMoney = c.Sales.Select(s=>s.Car.PartCars.Select(pc=>pc.Part.Price).Sum()).Sum()
                    })
                    .OrderByDescending(c=>c.SpentMoney)
                    .ThenByDescending(c=>c.BoughtCars)
                    .ToArray();


                XmlSerializer serializer = new XmlSerializer(typeof(CustomerWithCarsDto[]), new XmlRootAttribute("customers"));
                XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/customers-total-sales.xml";

                serializer.Serialize(new StreamWriter(path), customers, xmlNameSpaces);
            }
        }

        public void CarsWithTheirListOfPartsExport()
        {
            using (var context = new CarDealerContext())
            {
                var carsWithParts = context.Cars
                     .Select(c => new CarWithPartsDto
                     {
                         Make = c.Make,
                         Model = c.Model,
                         TravelledDistance = c.TravelledDistance,
                         Parts = c.PartCars.Select(pc => new PartDto
                         {
                             Name = pc.Part.Name,
                             Price = pc.Part.Price
                         }).ToArray()
                     })
                     .ToArray();



                XmlSerializer serializer = new XmlSerializer(typeof(CarWithPartsDto[]),new XmlRootAttribute("cars"));
                XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/cars-and-parts.xml";

                serializer.Serialize(new StreamWriter(path), carsWithParts, xmlNamespaces);
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
                       Price = s.Car.PartCars.Select(pc=>pc.Part.Price).Sum(),
                     })
                     .ToArray();

                XmlSerializer serializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("sales"));
                XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                Directory.CreateDirectory("../../../../XmlExport");

                var path = "../../../../XmlExport/sales-discounts.xml";

                serializer.Serialize(new StreamWriter(path), sales, xmlNamespaces);
            }
        }

        private static void ImportSales()
        {
            using (var context = new CarDealerContext())
            {
                decimal[] discounts = new decimal[] { 0, 0.05m, 0.1m, 0.15m, 0.20m, 0.30m, 0.40m, 0.50m };

                var carIds = context.Cars.Select(c => c.Id).ToList();
                var customerIds = context.Customers.Select(c => c.Id).ToList();

                var randomizer = new Random();

                var sales = new List<Sale>();

                for (int i = 0; i < 300; i++)
                {
                    var discount = discounts[randomizer.Next(0, discounts.Length)];
                    var carId = carIds[randomizer.Next(0, carIds.Count)];
                    var customerId = customerIds[randomizer.Next(0, customerIds.Count)];

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
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));

            var path = "../../../../XmlImport/customers.xml";

            var customerDtos = (CustomerDto[])serializer.Deserialize(new StreamReader(path));

            var customers = new List<Customer>();

            using (var context = new CarDealerContext())
            {
                foreach (var customerDto in customerDtos)
                {
                    if (!IsValid(customerDto))
                    {
                        Console.WriteLine("Invalid Car");
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
            XmlSerializer serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));

            var path = "../../../../XmlImport/cars.xml";

            var carDtos = (CarDto[])serializer.Deserialize(new StreamReader(path));

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
            XmlSerializer serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));

            var path = "../../../../XmlImport/parts.xml";

            var partDtos = (PartDto[])serializer.Deserialize(new StreamReader(path));

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
                XmlSerializer serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));

                string path = "../../../../XmlImport/suppliers.xml";

                var suppliersDtos = (SupplierDto[])serializer.Deserialize(new StreamReader(path));

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
    }
}
