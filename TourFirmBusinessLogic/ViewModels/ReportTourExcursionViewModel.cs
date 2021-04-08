using System.Collections.Generic;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTourExcursionViewModel
    {
        public string TourName { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
    }
}
