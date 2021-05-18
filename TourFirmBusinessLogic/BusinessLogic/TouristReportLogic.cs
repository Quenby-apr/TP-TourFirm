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

        /*        private List<ReportTravelGuidesViewModel> GetTravelGuides(List<TravelViewModel> selectedTravels)
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
                                foreach (var guideExcursion in guide.GuideExcursions)
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
                }*/

        //Получение списка путешествий с расшифровкой по экскурсиям и гидам
        /*public List<ReportTravelsViewModel> GetTravelExcursionsGuides(ReportTravelBindingModel model)
        {
            var travels = travelStorage.GetFilteredList(new TravelBindingModel
            {
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                TouristID = model.TouristID
            });

            var guides = guideStorage.GetFullList();

            var travelsExcursionsGuides = new List<ReportTravelsViewModel>();

            foreach (var travel in travels)
            {
                foreach (var travelExcursion in travel.TravelExcursions)
                {
                    foreach (var guide in guides)
                    {
                        foreach (var guideExcursion in guide.GuideExcursions)
                        {
                            if (guideExcursion.Key == travelExcursion.Key)
                            {
                                var excursion = excursionStorage.GetElement(new ExcursionBindingModel
                                {
                                    ID = guideExcursion.Key
                                });

                                travelsExcursionsGuides.Add(new ReportTravelsViewModel
                                {
                                    TravelName = travel.Name,
                                    DateStart = travel.DateStart,
                                    DateEnd = travel.DateEnd,
                                    ExcursionName = excursion.Name,
                                    GuideName = guide.Name,
                                    GuideSurname = guide.Surname
                                });
                            }
                        }
                    }
                }
            }
            return travelsExcursionsGuides;
        }*/

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