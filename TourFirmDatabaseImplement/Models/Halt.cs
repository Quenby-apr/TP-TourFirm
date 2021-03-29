using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Halt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int OperatorID { get; set; }
        [ForeignKey("HaltId")] public virtual List<Tour> Tours { get; set; }
        public virtual Operator Operator { get; set; }
    }
}
