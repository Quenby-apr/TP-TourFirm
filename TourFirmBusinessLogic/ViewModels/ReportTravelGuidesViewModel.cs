using System.Collections.Generic;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTravelGuidesViewModel
    {
        public string TravelName { get; set; }
        public List<GuideViewModel> Guides { get; set; }
    }
}