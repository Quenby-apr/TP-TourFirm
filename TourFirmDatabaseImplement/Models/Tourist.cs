using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Tourist
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Mail { get; set; }

        [ForeignKey("TouristID")]
        public virtual List<Travel> Travels { get; set; }

        [ForeignKey("TouristID")]
        public virtual List<Excursion> Excursions { get; set; }

        [ForeignKey("TouristID")]
        public virtual List<Place> Places { get; set; }
    }
}