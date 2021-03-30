using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Tour
    {
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Country { get; set; }
        [Required] public decimal Price { get; set; }
        public int OperatorID { get; set; }
        public int HaltID { get; set; }

        [ForeignKey("TourID")] 
        public virtual List<TourGuide> TourGuides { get; set; }

        [ForeignKey("TourID")]
        public virtual List<TravelTour> TravelTours { get; set; }

        public virtual Operator Operator { get; set; }
        public virtual Halt Halt { get; set; }
    }
}
