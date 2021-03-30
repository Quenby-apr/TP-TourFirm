using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Halt
    {
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Address { get; set; }
        public int OperatorID { get; set; }
        [ForeignKey("HaltID")] public virtual List<Tour> Tours { get; set; }
        public virtual Operator Operator { get; set; }
    }
}
