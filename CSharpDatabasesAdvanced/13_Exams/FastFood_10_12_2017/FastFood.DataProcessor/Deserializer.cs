using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var employeesToValidate = JsonConvert.DeserializeObject<EmployeeToImportDto[]>(jsonString);

            var validEmployees = new List<Employee>();

            var validPositions = new List<Position>();

            foreach (var employeeToValidate in employeesToValidate)
            {
                if (!IsValid(employeeToValidate))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Position position=null;

                if (validPositions.Any(p=>p.Name==employeeToValidate.Position))
                {
                    position = validPositions.First(p => p.Name == employeeToValidate.Position);
                }
                else
                {
                     position = new Position()
                    {
                        Name = employeeToValidate.Position
                    };

                    validPositions.Add(position);
                }

                var validEmployee = new Employee
                {
                    Age = employeeToValidate.Age,
                    Name = employeeToValidate.Name,
                    Position = position
                };

                validEmployees.Add(validEmployee);

                sb.AppendLine(string.Format(SuccessMessage, validEmployee.Name));
            }

            context.Positions.AddRange(validPositions);

            context.Employees.AddRange(validEmployees);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\r','\n');
		}

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var itemsToValidate = JsonConvert.DeserializeObject<ItemToImportDto[]>(jsonString);

            var validItems = new List<Item>();

            var validCategories = new List<Category>();

            foreach (var itemToValidate in itemsToValidate)
            {
                if (!IsValid(itemToValidate) || validItems.Any(i=>i.Name==itemToValidate.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Category category = null;

                if (validCategories.Any(c => c.Name == itemToValidate.Category))
                {
                    category = validCategories.First(c => c.Name == itemToValidate.Category);
                }
                else
                {
                    category = new Category()
                    {
                        Name = itemToValidate.Category
                    };

                    validCategories.Add(category);
                }

                var validItem = new Item
                {
                    Category = category,
                    Name = itemToValidate.Name,
                    Price = itemToValidate.Price
                };

                validItems.Add(validItem);

                sb.AppendLine(string.Format(SuccessMessage, validItem.Name));
            }

            context.Categories.AddRange(validCategories);

            context.Items.AddRange(validItems);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\r', '\n');
        }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            var sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(OrderToImportDto[]),new XmlRootAttribute("Orders"));

            var ordersToValidate = (OrderToImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var items = context.Items.ToList();

            var employees = context.Employees.ToList();

            var validOrders = new List<Order>();

            var orderItemsToImport = new List<OrderItem>();

            foreach (var orderToValidate in ordersToValidate)
            {
                var date = DateTime.ParseExact(orderToValidate.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

                var isTypeValid = Enum.TryParse<OrderType>(orderToValidate.Type, out OrderType type);

                var isOrderValid = true;

                var employee = employees.FirstOrDefault(e => e.Name == orderToValidate.Employee);

                if (!IsValid(orderToValidate) || employee==null)
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var validOrder = new Order()
                {
                    Customer = orderToValidate.Customer,
                    DateTime = date,
                    Employee = employee,
                    Type = type
                };

                var orderItems = new List<OrderItem>();

                foreach (var itemToValidate in orderToValidate.OrderItems)
                {
                    var item = context.Items.FirstOrDefault(i => i.Name == itemToValidate.Name);

                    if (!IsValid(itemToValidate)||item==null)
                    {
                        isOrderValid = false;

                        continue;
                    }

                    var validOrderItem = new OrderItem
                    {
                        Item = item,
                        Quantity = itemToValidate.Quantity,
                        Order = validOrder
                    };

                    orderItems.Add(validOrderItem);
                }

                if (isOrderValid==false)
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                validOrder.OrderItems.ToList().AddRange(orderItems);

                orderItemsToImport.AddRange(orderItems);

                validOrders.Add(validOrder);

                sb.AppendLine($"Order for {validOrder.Customer} on {validOrder.DateTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InstalledUICulture)} added");
            }

            context.OrderItems.AddRange(orderItemsToImport);

            context.Orders.AddRange(validOrders);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\r', '\n');
		}
	}
}