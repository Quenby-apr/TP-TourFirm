using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmDatabaseImplement.Models
{
    public class Tour
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int OperatorID { get; set; }
        public int HaltID { get; set; }
        public Dictionary<int, (string, string)> TourGuides { get; set; }
    }
}
