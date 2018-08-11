using System;
using System.IO;
using Travelling.Data;
using Travelling.ProcessData;
using Microsoft.EntityFrameworkCore;

namespace Travelling.App
{
    class StartUp
    {
        static void Main(string[] args)
        {

            using (var context = new TravellingContext())
            {

                context.Database.EnsureDeleted();

                context.Database.Migrate();


                string imoprtSessionsresult = Deserializer.ImportStations(context);

                Console.WriteLine(imoprtSessionsresult);

                string imoprtClassesResult = Deserializer.ImportClasses(context);

                Console.WriteLine(imoprtClassesResult);


                string imoprtTrainsResult = Deserializer.ImportTrains(context);

                Console.WriteLine(imoprtTrainsResult);

                string imoprtTripsResult = Deserializer.ImportTrips(context);

                Console.WriteLine(imoprtTripsResult);

                string imoprtPersonCardsResult = Deserializer.ImportPersonalCards(context);

                Console.WriteLine(imoprtPersonCardsResult);

                string imoprtTicketsResult = Deserializer.ImportTicketsCards(context);

                Console.WriteLine(imoprtTicketsResult);

                string delayedTrainsJsonString = Serializer.DelayedTrainsSerialize(context);

                Console.WriteLine(delayedTrainsJsonString);

                string cardsXmlString = Serializer.CardsAndCustomersSerialize(context);

                Console.WriteLine(cardsXmlString);

            }
        }
    }
}
