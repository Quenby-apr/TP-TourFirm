using System.Collections.Generic;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.HelperModels
{
    class TouristListTravelGuidesInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportTravelGuidesViewModel> TravelGuides { get; set; }
    }
}
