using System.Collections.Generic;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTourExcursionsViewModel
    {
        public string  TourName { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
    }
}