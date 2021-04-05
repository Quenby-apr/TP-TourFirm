using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.HelperModels
{
    class OperatorExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportTourExcursionViewModel> TourExcursions { get; set; }
    }
}
