using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Excursion
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }

        public int PlaceID { get; set; }
        public int TouristID { get; set; }

        [ForeignKey("ExcursionID")]
        public virtual List<GuideExcursion> GuideExcursions { get; set; }

        [ForeignKey("ExcursionID")]

        public virtual List<TravelExcursion> TravelExcursions { get; set; }
        public virtual Place Place { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}