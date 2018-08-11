using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var employee = context.Employees
                .Include(e=>e.Orders)
                .ThenInclude(o=>o.OrderItems)
                .ThenInclude(oi=>oi.Item)
                .First(e => e.Name == employeeName);            

            var employeeOrders = employee
                .Orders
                .Where(o => o.Type == Enum.Parse<OrderType>(orderType))
                .ToList();

            var objectToExport = new
            {
                Name = employee.Name,
                Orders = employeeOrders.Select(o => new
                {
                    Customer = o.Customer,
                    Items = o.OrderItems.Select(oi => new
                    {
                        Name = oi.Item.Name,
                        Price = oi.Item.Price,
                        Quantity = oi.Quantity
                    }),
                    TotalPrice =o.TotalPrice
                })
                .OrderByDescending(o=>o.TotalPrice)
                .ThenBy(o=>o.Items.Count()),
                TotalMade = employeeOrders.Sum(o=>o.TotalPrice)
            };

            return JsonConvert.SerializeObject(objectToExport, Newtonsoft.Json.Formatting.Indented);
        }

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categoriesStringSeparated = categoriesString.Split(',').ToArray();

            var categories = context
                .Categories
                .Where(c => categoriesStringSeparated.Contains(c.Name))
                .Include(c => c.Items)
                .ThenInclude(i => i.OrderItems)
                .ToList()
                .Select(c => new CategoryToExportDto
                {
                    Name = c.Name,
                    ItemToExport = new ItemToExportDto()
                    {
                        Name = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price)).First().Name,
                        TimesSold = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price)).First().OrderItems.Sum(oi => oi.Quantity),
                        TotalMade = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price))
                        .Select(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price))
                        .First()
                    }

                }
                )
                .OrderByDescending(o=>o.ItemToExport.TotalMade)
                .ThenBy(o=>o.ItemToExport.TotalMade)
                .ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new []{ XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(CategoryToExportDto[]),new XmlRootAttribute("Categories"));

            var writer = new StringWriter();

            serializer.Serialize(writer,categories, xmlNameSpaces);

            return writer.ToString();
		}
	}
}