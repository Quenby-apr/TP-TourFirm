using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TourFirmDatabaseImplement.Models
{
    public class Tour
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int OperatorID { get; set; }
        public int HaltID { get; set; }
        [ForeignKey("TourID")] public virtual List<TourGuide> TourGuides { get; set; }
        public virtual Operator Operator { get; set; }
        public virtual Halt Halt { get; set; }
    }
}
