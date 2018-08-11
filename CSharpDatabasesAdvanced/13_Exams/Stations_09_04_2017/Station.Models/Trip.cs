using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travelling.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public int OriginStationId { get; set; }

        public virtual Station OriginStation { get; set; }

        [Required]
        public int DestinationStationid { get; set; }

        public virtual Station DestinationStation { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int TrainId { get; set; }

        public virtual Train Train { get; set; }

        public TripStatus Status { get; set; }

        public TimeSpan? TimeDifference { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }


    }
}
