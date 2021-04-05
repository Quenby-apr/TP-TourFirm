using System.Collections.Generic;
using System.Linq;
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

        public TouristReportLogic(ITravelStorage travelStorage, IExcursionStorage excursionStorage, IGuideStorage guideStorage)
        {
            this.travelStorage = travelStorage;
            this.excursionStorage = excursionStorage;
            this.guideStorage = guideStorage;
        }

        private List<ReportTravelGuidesViewModel> GetTravelGuides(List<TravelViewModel> selectedTravels)
        {
            var guides = guideStorage.GetFullList();
            var list = new List<ReportTravelGuidesViewModel>();

            foreach (var travel in selectedTravels)
            {
                var record = new ReportTravelGuidesViewModel
                {
                    TravelName = travel.Name,
                    Guides = new List<GuideViewModel>(),
                };

                var listGuides = new List<GuideViewModel>();

                foreach (var travelExcursion in travel.TravelExcursions)
                {
                    var excursion = excursionStorage.GetElement(new ExcursionBindingModel
                    {
                        ID = travelExcursion.Key
                    });

                    foreach (var guide in guides)
                    {
                        foreach (var guideExcursion in guide.ExcursionGuides)
                        {
                            if (guideExcursion.Key == excursion.ID)
                            {
                                if (!listGuides.Contains(guide))
                                {
                                    listGuides.Add(guide);
                                }
                            }
                        }
                    }
                }

                foreach (var guide in listGuides)
                {
                    var guideRecord = guideStorage.GetElement(new GuideBindingModel
                    {
                        ID = guide.ID
                    });
                    record.Guides.Add(guideRecord);
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveTravelGuidesToWord(ReportTravelBindingModel model)
        {
            TouristSaveToWord.CreateDoc(new TouristListTravelGuidesInfo
            {
                FileName = model.FileName,
                Title = "Список гидов по выбранным путешествиям",
                TravelGuides = GetTravelGuides(model.Travels)
            });
        }

        public void SaveTravelGuidesToExcel(ReportTravelBindingModel model)
        {
            TouristSaveToExcel.CreateDoc(new TouristListTravelGuidesInfo
            {
                FileName = model.FileName,
                Title = "Список гидов по выбранным путешествиям",
                TravelGuides = GetTravelGuides(model.Travels)
            });
        }
    }
}
