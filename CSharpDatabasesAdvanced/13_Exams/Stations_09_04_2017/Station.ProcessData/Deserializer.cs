using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Travelling.Data;
using Travelling.Models;
using Travelling.ProcessData.Dtos;

namespace Travelling.ProcessData
{
    public static class Deserializer
    {
        public static string ImportStations(TravellingContext context)
        {
            var stationsPath = "../../../../resourses/stations.json";

            var reader = new StreamReader(stationsPath);

            var sb = new StringBuilder();

            string json = reader.ReadToEnd();

            var stationsToCheck = JsonConvert.DeserializeObject<StationDto[]>(json).ToHashSet<StationDto>(new SameStationsComparer());

            var stationsToImport = new List<Station>();

            foreach (var stationToCheck in stationsToCheck)
            {
                if (!ValidateEntity.IsValid(stationToCheck))
                {
                    sb.AppendLine("InvalidDataFormat");
                    continue;
                }

                if (stationToCheck.Town == null)
                {
                    stationToCheck.Town = stationToCheck.Name;
                }

                var station = new Station
                {
                    Name = stationToCheck.Name,
                    Town = stationToCheck.Town
                };

                stationsToImport.Add(station);

                sb.AppendLine($"Record {stationToCheck.Name} successfully imported.");
            }

            context.Stations.AddRange(stationsToImport.ToList());

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        public static string ImportTicketsCards(TravellingContext context)
        {
            var sb = new StringBuilder();

            var ticketsPath = "../../../../resourses/tickets.xml";

            var ticketsReader = new StreamReader(ticketsPath);

            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));

            var ticketsDtos = (TicketDto[])serializer.Deserialize(ticketsReader);

            var validTickets = new List<Ticket>();

            foreach (var ticketDto in ticketsDtos)
            {
                if (!ValidateEntity.IsValid(ticketDto))
                {
                    sb.AppendLine("Invalid ticket");
                    continue;
                }

                var isDateValid = DateTime.TryParseExact(ticketDto.Trip.DepartureTime,
                                                         "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                                                         DateTimeStyles.None,
                                                         out DateTime date);

                if (!isDateValid)
                {
                    sb.AppendLine("InvalidDate!!");
                    continue;
                }

                var trip = GetTripOrNull(context, ticketDto.Trip, date);


                if (trip == null)
                {
                    sb.AppendLine("InvalidTrip!!!");
                    continue;
                }

                var hasCard = true;

                var cardIsValid = true;

                CustomerCard card = null;

                if (ticketDto.Card == null)
                {
                    hasCard = false;
                }

                if (hasCard)
                {
                    card = GetCardOrNull(context, ticketDto.Card);

                    if (card == null)
                    {
                        cardIsValid = false;
                    }
                }

                if (!cardIsValid)
                {
                    sb.AppendLine("InvalidCard");

                    continue;
                }


                var ticket = new Ticket
                {
                    SeatingPlace = ticketDto.Seat,
                    PersonalCard = card,
                    Trip = trip,
                    Price = ticketDto.Price,
                };

                if (!ValidateEntity.IsValidSeat(context, ticket))
                {
                    sb.AppendLine("InvalidSeat!!!");
                    continue;
                }

                validTickets.Add(ticket);

                sb.AppendLine($"Ticket from {ticket.Trip.OriginStation.Name} to {ticket.Trip.DestinationStation.Name} Sever departing at {ticket.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm")} imported.");

            }


            context.Tickets.AddRange(validTickets);

            context.SaveChanges();
            return sb.ToString().TrimEnd('\r', '\n');

        }

        private static CustomerCard GetCardOrNull(TravellingContext context, CardForTicketDto card)
        {
            return context.CustomerCards.SingleOrDefault(c => c.Name == card.Name);
        }

        private static Trip GetTripOrNull(TravellingContext context, TripForTicketDto trip, DateTime date)
        {

            return context.Trips.SingleOrDefault(t => t.OriginStation.Name == trip.OriginStation
            && t.DestinationStation.Name == trip.DestinationStation
            && string.Compare(t.DepartureTime.ToString("dd/MM/yyyy HH:mm"), date.ToString("dd/MM/yyyy HH:mm")) == 0
            );
        }

        public static string ImportPersonalCards(TravellingContext context)
        {
            var sb = new StringBuilder();

            var cardsPath = "../../../../resourses/cards.xml";

            var cardsReader = new StreamReader(cardsPath);

            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));

            var cardsDtos = (CardDto[])serializer.Deserialize(cardsReader);

            var validCards = new List<CustomerCard>();

            foreach (var cardDto in cardsDtos)
            {
                if (!ValidateEntity.IsValid(cardDto))
                {
                    sb.AppendLine("Invalid card");

                    continue;
                }

                var validCard = new CustomerCard
                {
                    Name = cardDto.Name,
                    Age = cardDto.Age,
                };

                if (cardDto.CardType != null)
                {
                    validCard.CardType = cardDto.CardType.Value;
                }

                validCards.Add(validCard);

                sb.AppendLine($"Record {validCard.Name} successfully imported.");
            }

            context.CustomerCards.AddRange(validCards);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string ImportTrips(TravellingContext context)
        {
            var tripsPath = "../../../../resourses/trips.json";

            var tripsReader = new StreamReader(tripsPath);

            var sb = new StringBuilder();

            var tripsToCheck = JsonConvert.DeserializeObject<TripDto[]>(tripsReader.ReadToEnd(), new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy HH:mm",
                NullValueHandling = NullValueHandling.Ignore,
            });

            var validTrips = new List<Trip>();


            foreach (var tripToCheck in tripsToCheck)
            {
                var train = GetTrainOrNull(context, tripToCheck.Train);

                var originStation = GetStationOrNull(context, tripToCheck.OriginStation);

                var destinationStation = GetStationOrNull(context, tripToCheck.DestinationStation);

                if (!ValidateEntity.IsValid(tripToCheck)
                                     || train == null
                                     || originStation == null
                                     || destinationStation == null)
                {
                    sb.AppendLine("Invalid data format.");

                    continue;
                }

                if (!ValidateEntity.DateTimeValidation(tripToCheck.ArrivalTime.Value, tripToCheck.DepartureTime.Value))
                {
                    sb.AppendLine("Invalid data format.");

                    continue;
                }

                var trip = new Trip
                {
                    ArrivalTime = tripToCheck.ArrivalTime.Value,
                    DepartureTime = tripToCheck.DepartureTime.Value,
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    TimeDifference = tripToCheck.TimeDifference,
                    Status = tripToCheck.Status
                };

                validTrips.Add(trip);

                sb.AppendLine($"Trip from {trip.OriginStation.Name} to {trip.DestinationStation.Name} imported.");

            }

            context.Trips.AddRange(validTrips);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');

        }

        private static Station GetStationOrNull(TravellingContext context, string stationName)
        {
            return context.Stations.FirstOrDefault(s => s.Name == stationName);
        }

        private static Train GetTrainOrNull(TravellingContext context, string trainNumber)
        {
            return context.Trains.FirstOrDefault(t => t.TrainNumber == trainNumber);
        }

        public static string ImportTrains(TravellingContext context)
        {

            var trainsPath = "../../../../resourses/trains.json";

            var trainsReader = new StreamReader(trainsPath);

            var sb = new StringBuilder();

            var trainsToCheck = JsonConvert.DeserializeObject<TrainDto[]>(trainsReader.ReadToEnd(), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }
            )
            .ToHashSet<TrainDto>(new TrainComparer());

            var seats = context.SeatingClasses.ToList();

            var trains = new List<Train>();

            var trainSeatsToAdd = new List<TrainSeats>();

            foreach (var trainToCheck in trainsToCheck)
            {
                var IsSeatsValid = true;

                if (trainToCheck.TrainNumber == null)
                {
                    continue;
                }

                if (!ValidateEntity.IsValid(trainToCheck))
                {
                    sb.AppendLine("InvalidTrain");

                    continue;
                }

                var seatsToAdd = new List<TrainSeats>();

                if (trainToCheck.Seats != null)
                {
                    foreach (var seatsToCheck in trainToCheck.Seats)
                    {
                        if (!ValidateEntity.IsValid(seatsToCheck))
                        {
                            sb.AppendLine("InvalidSeats");

                            IsSeatsValid = false;

                            continue;
                        }

                        var currentSeat = isSeatExist(context, seatsToCheck, seats);

                        if (currentSeat == null)
                        {
                            sb.AppendLine("InvalidSeats");

                            IsSeatsValid = false;

                            continue;
                        }

                        if (!seatsToAdd.Any(s => s.SeatingClass.Name == currentSeat.Name))
                        {
                            seatsToAdd.Add(new TrainSeats
                            {
                                SeatingClass = currentSeat,
                                SeatingClassId = currentSeat.Id,
                                Quantity = seatsToCheck.Quantity
                            });
                        }

                    }

                    if (!IsSeatsValid)
                    {
                        continue;
                    }
                }

                var check = Enum.TryParse<TrainType>(trainToCheck.Type, out TrainType result);

                var train = new Train
                {
                    TrainNumber = trainToCheck.TrainNumber,
                };

                if (check)
                {
                    train.Type = result;
                }

                context.Trains.Add(train);

                context.SaveChanges();

                for (int i = 0; i < seatsToAdd.Count; i++)
                {
                    var seat = seatsToAdd[i];
                    seat.TrainId = train.Id;
                    trainSeatsToAdd.Add(seat);
                }

                trains.Add(train);

                sb.AppendLine($"Record {train.TrainNumber} successfully imported.");

            }

            context.TrainSeatClasses.AddRange(trainSeatsToAdd);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\r', '\n');

        }

        private static SeatingClass isSeatExist(TravellingContext context, ClassForTrainDto seatsToCheck, List<SeatingClass> seats)
        {
            return seats.FirstOrDefault(s => s.Name == seatsToCheck.Name && s.Abbreviation == seatsToCheck.Abbreviation);
        }

        public static string ImportClasses(TravellingContext context)
        {

            var classesPath = "../../../../resourses/classes.json";

            var classesReader = new StreamReader(classesPath);

            var sb = new StringBuilder();

            string json = classesReader.ReadToEnd();

            var classesToCheck = JsonConvert.DeserializeObject<ClassDto[]>(json).ToHashSet<ClassDto>(new SameClassesComparer());

            var classesToImport = new List<SeatingClass>();

            foreach (var classToCheck in classesToCheck)
            {
                if (!ValidateEntity.IsValid(classToCheck))
                {
                    sb.AppendLine("InvalidDataFormat");
                    continue;
                }


                var currentClass = new SeatingClass
                {
                    Name = classToCheck.Name,
                    Abbreviation = classToCheck.Abbreviation
                };

                classesToImport.Add(currentClass);

                sb.AppendLine($"Record {currentClass.Name} successfully imported.");
            }

            context.SeatingClasses.AddRange(classesToImport);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }

    internal class TrainComparer : IEqualityComparer<TrainDto>
    {
        public bool Equals(TrainDto x, TrainDto y)
        {
            return x.TrainNumber == y.TrainNumber;
        }

        public int GetHashCode(TrainDto obj)
        {
            return base.GetHashCode();
        }
    }

    internal class SameClassesComparer : IEqualityComparer<ClassDto>
    {
        public bool Equals(ClassDto x, ClassDto y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(ClassDto obj)
        {
            return base.GetHashCode();
        }
    }

    internal class SameStationsComparer : IEqualityComparer<StationDto>
    {
        public SameStationsComparer()
        {
        }

        public bool Equals(StationDto x, StationDto y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(StationDto obj)
        {
            return base.GetHashCode();
        }
    }
}
