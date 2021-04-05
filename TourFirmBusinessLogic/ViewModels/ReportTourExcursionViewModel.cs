using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTourExcursionViewModel
    {
        public string  TourName { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
    }
}
