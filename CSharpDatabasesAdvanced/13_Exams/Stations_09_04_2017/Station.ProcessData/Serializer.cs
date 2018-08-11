using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Travelling.Data;
using Travelling.Models;
using Travelling.ProcessData.Dtos;

namespace Travelling.ProcessData
{
    public static class Serializer
    {
        public static string DelayedTrainsSerialize(TravellingContext context)
        {
   
            Console.WriteLine("Please insert date with dd/MM/yyyy format");

            var dateTimeString = Console.ReadLine();

            var date = DateTime.ParseExact(dateTimeString, "dd/MM/yyyy",CultureInfo.InvariantCulture);

            var delayedTrains = context.Trips
                .Where(tr => tr.Status == TripStatus.Delayed
                         && tr.DepartureTime.CompareTo(date) < 1)
                         .GroupBy(tr=>tr.Train.TrainNumber)
                         .ToList()
                         .Select(tr => new
                         {
                             TrainNumber = tr.Key,
                             DelayedTimes =tr.Count() ,
                             MaxDelayedTime = tr.Max(t=>t.TimeDifference)
                         })
                         .OrderByDescending(t=>t.DelayedTimes)
                         .ThenByDescending(t=>t.MaxDelayedTime)
                         .ThenBy(t=>t.TrainNumber)
                       .ToList();
            ;

            string jsonString = JsonConvert.SerializeObject(delayedTrains, Newtonsoft.Json.Formatting.Indented);

            var fileName = $"delayed-trips-{date.ToString("dd-MM-yyyy")}.json";
            
            var path = $"../../../../{fileName}";

            File.WriteAllText(path,jsonString);


            return jsonString;
        }

        public static string CardsAndCustomersSerialize(TravellingContext context)
        {
            Console.WriteLine("Insert the card type please:");

            var cardTypeString = Console.ReadLine();
               
            var cardsForExport = context.Tickets
                            .Where(t => t.PersonalCard.CardType.ToString() == cardTypeString)
                .GroupBy(t => t.PersonalCard)
                .ToArray()
                .Select(kvp => new CardForExport
                {
                    Name = kvp.Key.Name,
                    Type = kvp.Key.CardType.ToString(),
                    Tickets = kvp.Select(tr => new TicketForExport
                    {
                        OriginStation = tr.Trip.OriginStation.Name,
                        DestinationStation = tr.Trip.DestinationStation.Name,
                        DepartureTime = tr.Trip.DepartureTime.ToString()
                     })
                     .ToArray()
                }
                )
                .OrderBy(f=>f.Name)
                .ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
          
            XmlSerializer serializer = new XmlSerializer(typeof(CardForExport[]),new XmlRootAttribute("Cards"));

            var path = "../../../../cards.xml";

            var writer = new StreamWriter(path);

            serializer.Serialize(writer,cardsForExport,xmlNameSpaces);

            writer.Close();

            Process.Start("notepad.exe", path);

            return File.ReadAllText(path);
        }
    }
}
