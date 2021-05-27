using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.HelperModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TouristReportLogic
    {
        private readonly ITravelStorage travelStorage;
        private readonly IExcursionStorage excursionStorage;
        private readonly IGuideStorage guideStorage;
        private readonly IReportStorage reportStorage;

        public TouristReportLogic(ITravelStorage travelStorage, IExcursionStorage excursionStorage, IGuideStorage guideStorage, IReportStorage reportStorage)
        {
            this.travelStorage = travelStorage;
            this.excursionStorage = excursionStorage;
            this.guideStorage = guideStorage;
            this.reportStorage = reportStorage;
        }

        public List<ReportTravelGuidesViewModel> GetTravelGuides(List<TravelViewModel> selectedTravels)
        {
            var list = new List<ReportTravelGuidesViewModel>();

            foreach (var travel in selectedTravels)
            {
                list.Add(reportStorage.GetTravelGuides(new TravelBindingModel
                {
                    ID = travel.ID,
                    Name = travel.Name
                }));
            }
            return list;
        }

        public List<ReportTravelsViewModel> GetTravelExcursionsGuides(ReportTravelBindingModel model, int _TouristID)
        {
            var list = reportStorage.GetFullListTravels(model, _TouristID);
            return list;
        }

        public void SaveTravelGuidesToWord(ReportTravelBindingModel model)
        {
            TouristSaveToWord.CreateDoc(new TouristWordExcelInfo
            {
                FileName = model.FileName,
                Title = "Список гидов по выбранным путешествиям",
                TravelGuides = GetTravelGuides(model.Travels)
            });
        }

        public void SaveTravelGuidesToExcel(ReportTravelBindingModel model)
        {
            TouristSaveToExcel.CreateDoc(new TouristWordExcelInfo
            {
                FileName = model.FileName,
                Title = "Список гидов по выбранным путешествиям",
                TravelGuides = GetTravelGuides(model.Travels)
            });
        }

        public void SaveTravelsExcursionsGuidesToPdf(ReportTravelBindingModel model, int _TouristID)
        {
            TouristSaveToPdf.CreateDoc(new TouristPdfInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий и гидов по выбранным путешествиям",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                TravelExcursionsGuides = GetTravelExcursionsGuides(model, _TouristID)
            });
        }
    }
}