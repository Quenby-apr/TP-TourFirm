using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TourFirmDatabaseImplement.Models
{
    public class Excursion
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
        [ForeignKey("ExcursionID")]  public virtual List<ExcursionGuide> ExcursionGuides { get; set; }
    }
}
