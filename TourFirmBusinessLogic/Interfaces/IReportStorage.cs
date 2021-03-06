using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IReportStorage
    {
        List<ReportGuidesViewModel> GetFullListGuides(ReportTourBindingModel model, int _OperatorID);
        ReportTourExcursionsViewModel GetTourExcursion(TourBindingModel model);
        ReportTravelGuidesViewModel GetTravelGuides(TravelBindingModel model);
        List<ReportTravelsViewModel> GetFullListTravels(ReportTravelBindingModel model, int _TouristID);
    }
}