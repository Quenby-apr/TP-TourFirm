using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Place
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int TouristID { get; set; }

        [ForeignKey("PlaceId")] 
        public virtual List<Excursion> Excursions { get; set; }

        public virtual Tourist Tourist { get; set; }
    }
}
