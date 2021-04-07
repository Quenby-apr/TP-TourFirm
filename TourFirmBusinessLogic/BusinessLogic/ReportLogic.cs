using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.HelperModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IGuideStorage _guideStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly ITourStorage _tourStorage;
        private readonly ITravelStorage _travelStorage;

        public ReportLogic(IGuideStorage guideStorage, IExcursionStorage excursionStorage, ITourStorage tourStorage, ITravelStorage travelStorage)
        {
            _guideStorage = guideStorage;
            _excursionStorage = excursionStorage;
            _tourStorage = tourStorage;
            _travelStorage = travelStorage;
        }
        /// <summary>
        /// Получение списка смен гидов за определенный период с указанием туров и экскурсий 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportGuideViewModel> GetGuides(ReportBindingModel model)
        {
            var list = new List<ReportGuideViewModel>();
            var travels = _travelStorage.GetFilteredList(new TravelBindingModel
            {
                DateStart = model.DateFrom.Value,
                DateEnd = model.DateTo.Value
            });
            foreach (var travel in travels)
            {
                foreach (var TT in travel.TravelTours)
                {
                    var tour = _tourStorage.GetElement(new TourBindingModel
                    {
                        ID = TT.Key
                    });
                    foreach (var TG in tour.TourGuides)
                    {
                        var guide = _guideStorage.GetElement(new GuideBindingModel
                        {
                            ID = TG.Key
                        });
                        foreach (var GE in guide.ExcursionGuides)
                        {
                            var excursion = _excursionStorage.GetElement(new ExcursionBindingModel
                            {
                                ID = GE.Key
                            });
                            var record = new ReportGuideViewModel
                            {
                                DateStartTravel = travel.DateStart,
                                GuideSurname = guide.Surname,
                                GuideName = guide.Name,
                                GuideWorkPlace = guide.WorkPlace,
                                ExcursionName = excursion.Name,
                                TourName = tour.Name
                            };
                            list.Add(record);
                        }
                    }
                }
            }
            return list;
        }
        public List<ReportTourExcursionViewModel> GetTourExcursions(List<TourViewModel> tours)
        {
            var list = new List<ReportTourExcursionViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportTourExcursionViewModel
                {
                    TourName = tour.Name,
                    Excursions = new List<ExcursionViewModel>(),
                };
                foreach (var _guide in tour.TourGuides)
                {
                    var guide = _guideStorage.GetElement(new GuideBindingModel
                    {
                        ID = _guide.Key
                    });
                    var listExcursions = guide.ExcursionGuides.ToList();
                    for (int i = 0; i < listExcursions.Count; i++) {
                        record.Excursions.Add(_excursionStorage.GetElement(new ExcursionBindingModel {
                            ID = listExcursions[i].Key
                        }));
                    }
                }
                list.Add(record);
            }
            return list;
        }
        public void SaveTourExcurionToWordFile(ReportTourBindingModel model)
        {
            OperatorSaveToWord.CreateDoc(new OperatorWordInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий по указанным турам",
                TourExcursions = GetTourExcursions(model.Tours)
            });
        }

        public void SaveTourExcurionToExcelFile(ReportTourBindingModel model)
        {
            OperatorSaveToExcel.CreateDoc(new OperatorExcelInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий по указанным турам",
                TourExcursions = GetTourExcursions(model.Tours)
            });
        }
        public void SaveGuidesToPdfFile(ReportBindingModel model)
        {
            OperatorSaveToPdf.CreateDoc(new OperatorPdfInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Guides = GetGuides(model)
            });
        }
    }
}
