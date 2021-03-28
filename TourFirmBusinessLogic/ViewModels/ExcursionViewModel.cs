using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ExcursionViewModel
    {
        public int? ID { get; set; }
        [DisplayName("Название")] public string Name { get; set; }
        [DisplayName("Продолжительность")] public decimal Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
    }
}
