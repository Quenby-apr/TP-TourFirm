using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class TravelViewModel
    {
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        public int TouristID { get; set; }
        public Dictionary<int, string> TravelTours { get; set; }
        public Dictionary<int, string> TravelExcursions { get; set; }
    }
}
