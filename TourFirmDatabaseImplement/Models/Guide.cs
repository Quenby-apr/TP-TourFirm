using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TourFirmDatabaseImplement.Models
{
    public class Guide
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkPlace { get; set; }
        public string MainLanguage { get; set; }
        public string AdditionalLanguage { get; set; }
        public DateTime DateWork { get; set; }
        public int OperatorID { get; set; }
        [ForeignKey("GuideID")] public virtual List<ExcursionGuide> ExcursionGuides { get; set; }
        [ForeignKey("GuideID")] public virtual List<TourGuide> TourGuides { get; set; }
        public virtual Operator Operator { get; set; }
    }
}
