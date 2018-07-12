using System;
using System.Collections.Generic;

namespace Demo_01.Data.Models
{
    public partial class Towns
    {
        public Towns()
        {
            Addresses = new HashSet<Addresses>();
        }

        public int TownId { get; set; }
        public string Name { get; set; }

        public ICollection<Addresses> Addresses { get; set; }
    }
}
