using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.HelperModels
{
    class TouristPdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<ReportTravelsViewModel> TravelExcursionsGuides { get; set; }
    }
}
