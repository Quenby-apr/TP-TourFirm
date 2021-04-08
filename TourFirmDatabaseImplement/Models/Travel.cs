using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Travel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public int TouristID { get; set; }

        [ForeignKey("TravelID")]
        public virtual List<TravelTour> TravelTours { get; set; }

        [ForeignKey("TravelID")]
        public virtual List<TravelExcursion> TravelExcursions { get; set; }

        public virtual Tourist Tourist { get; set; }
    }
}