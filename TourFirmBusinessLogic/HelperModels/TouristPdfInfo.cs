using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.HelperModels
{
    class TouristPdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportTravelsViewModel> TravelExcursionsGuides { get; set; }
    }
}
