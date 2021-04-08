using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BindingModels
{
    public class ReportTravelBindingModel
    {
        public string FileName { get; set; }
        public int TouristID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<TravelViewModel> Travels { get; set; }
    }
}