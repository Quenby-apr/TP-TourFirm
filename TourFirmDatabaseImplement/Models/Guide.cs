using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourFirmDatabaseImplement.Models
{
    public class Guide
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string WorkPlace { get; set; }

        [Required]
        public string MainLanguage { get; set; }

        public string AdditionalLanguage { get; set; }
        public int OperatorID { get; set; }

        [ForeignKey("GuideID")]
        public virtual List<GuideExcursion> GuideExcursions { get; set; }

        [ForeignKey("GuideID")]

        public virtual List<TourGuide> TourGuides { get; set; }
        public virtual Operator Operator { get; set; }
    }
}