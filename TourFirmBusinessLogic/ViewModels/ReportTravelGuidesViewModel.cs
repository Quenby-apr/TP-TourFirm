using System.Collections.Generic;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTravelGuidesViewModel
    {
        public string TravelName { get; set; }
        public Dictionary<int, string> Guides { get; set; }
    }
}
