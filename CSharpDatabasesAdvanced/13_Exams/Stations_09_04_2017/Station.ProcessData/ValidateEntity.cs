using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Travelling.Data;
using Travelling.Models;

namespace Travelling.ProcessData
{
   public static class ValidateEntity
    {
         public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }

        internal static bool DateTimeValidation(DateTime arrivalTime, DateTime departureTime)
        {
            return departureTime.CompareTo(arrivalTime)==-1;
        }

        internal static bool IsValidSeat(TravellingContext context, Ticket ticket)
        {
            var train = context.Trains.FirstOrDefault(t => t.TrainNumber == ticket.Trip.Train.TrainNumber);

            var seatAbbreviature = ticket.SeatingPlace.Substring(0,2);

            var successNumberParse = int.TryParse(ticket.SeatingPlace.Substring(2), out int number);

            if (!successNumberParse)
            {
                return false;
            }

            var trainSeat = train.TrainSeatClasses.FirstOrDefault(tsc => tsc.SeatingClass.Abbreviation == seatAbbreviature);

            if (trainSeat==null)
            {
                return false;
            }

            var numberMatch = number <= trainSeat.Quantity;

            if (!numberMatch)
            {
                return false;
            }

            return true;
        }
    }
}
