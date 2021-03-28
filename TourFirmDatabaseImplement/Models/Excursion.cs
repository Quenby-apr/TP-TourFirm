using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmDatabaseImplement.Models
{
    public class Excursion
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
        public virtual List<ExcursionGuide> ExcursionGuides { get; set; }
        [ForeignKey("ExcursionID")]
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
