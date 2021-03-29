using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Operator
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        [ForeignKey("OperatorId")] public virtual List<Guide> Guides { get; set; }
        [ForeignKey("OperatorId")] public virtual List<Halt> Halts { get; set; }
        [ForeignKey("OperatorId")] public virtual List<Tour> Tours { get; set; }
    }
}
