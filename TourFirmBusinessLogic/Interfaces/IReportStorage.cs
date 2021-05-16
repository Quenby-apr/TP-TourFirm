using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IReportStorage
    {
        List<ReportGuidesViewModel> GetFullListGuides(ReportTourBindingModel model, int _OperatorID);
        ReportTourExcursionsViewModel GetTourExcursion(TourBindingModel model);
    }
}
