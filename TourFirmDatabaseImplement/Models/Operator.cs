using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Operator
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

        [ForeignKey("OperatorID")]
        public virtual List<Guide> Guides { get; set; }

        [ForeignKey("OperatorID")]
        public virtual List<Halt> Halts { get; set; }

        [ForeignKey("OperatorID")]
        public virtual List<Tour> Tours { get; set; }
    }
}