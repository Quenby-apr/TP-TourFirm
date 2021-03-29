using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Tourist
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        [ForeignKey("TouristID")] 
        public virtual List<Travel> Travels { get; set; }

        [ForeignKey("TouristID")]
        public virtual List<Excursion> Excursions { get; set; }

        [ForeignKey("TouristID")]
        public virtual List<Place> Places { get; set; }
    }
}
