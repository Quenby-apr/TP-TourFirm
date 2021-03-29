using System;
using System.Collections.Generic;

namespace TourFirmBusinessLogic.BindingModels
{
    public class TravelBindingModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int TouristID { get; set; }
        public Dictionary<int, string> TravelTours { get; set; }
        public Dictionary<int, string> TravelExcursions { get; set; }
    }
}
