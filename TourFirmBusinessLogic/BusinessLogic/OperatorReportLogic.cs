using System;
using System.Collections.Generic;
using System.Linq;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.HelperModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class OperatorReportLogic
    {
        private readonly IGuideStorage _guideStorage;
        private readonly IExcursionStorage _excursionStorage;
        private readonly ITourStorage _tourStorage;
        private readonly ITravelStorage _travelStorage;
        private readonly IReportStorage reportStorage;

        public OperatorReportLogic(IGuideStorage guideStorage, IExcursionStorage excursionStorage, ITourStorage tourStorage, ITravelStorage travelStorage, IReportStorage reportStorage)
        {
            _guideStorage = guideStorage;
            _excursionStorage = excursionStorage;
            _tourStorage = tourStorage;
            _travelStorage = travelStorage;
            this.reportStorage = reportStorage;
        }

        // Получение списка смен гидов за определенный период с указанием туров и экскурсий 
        public List<ReportGuidesViewModel> GetGuides(ReportTourBindingModel model, int _OperatorID)
        {
            var list = reportStorage.GetFullListGuides( model, _OperatorID);
            return list;
        }
        /*public List<ReportTourExcursionsViewModel> GetTourExcursions(List<TourViewModel> tours)
        {
            var list = new List<ReportTourExcursionsViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportTourExcursionsViewModel
                {
                    TourName = tour.Name,
                    Excursions = new List<ExcursionViewModel>(),
                };
                foreach (var _guide in tour.TourGuides)
                {
                    var guide = _guideStorage.GetElement(new GuideBindingModel
                    {
                        ID = _guide.Key,
                    });
                    var listExcursions = guide.GuideExcursions.ToList();
                    for (int i = 0; i < listExcursions.Count; i++) {
                        record.Excursions.Add(_excursionStorage.GetElement(new ExcursionBindingModel {
                            ID = listExcursions[i].Key
                        }));
                    }
                }
                list.Add(record);
            }
            return list;
        }*/
        public List<ReportTourExcursionsViewModel> GetTourExcursions(List<TourViewModel> tours)
        {
            var list = new List<ReportTourExcursionsViewModel>();
            foreach (var tour in tours)
            {
                list.Add(reportStorage.GetTourExcursion(new TourBindingModel
                {
                    ID = tour.ID,
                    Name = tour.Name
                }));
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

        public void SaveGuidesToPdfFile(ReportTourBindingModel model, int _OperatorID)
        {
            OperatorSaveToPdf.CreateDoc(new OperatorPdfInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Guides = GetGuides(model, _OperatorID)
            });
        }
    }
}