using System.Collections.Generic;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.HelperModels
{
    class OperatorWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportTourExcursionViewModel> TourExcursions { get; set; }
    }
}
