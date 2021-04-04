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

        public ReportLogic(IGuideStorage guideStorage, IExcursionStorage excursionStorage, ITourStorage tourStorage)
        {
            _guideStorage = guideStorage;
            _excursionStorage = excursionStorage;
            _tourStorage = tourStorage;
        }
        /// <summary>
        /// Получение списка смен гидов за определенный период с указанием туров и экскурсий 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /*public List<WorkViewModel> GetWorks(ReportBindingModel model)
        {
            var tours = _tourStorage.GetFullList();
            var excursions = _excursionStorage.GetFullList();
            return _guideStorage.GetFilteredList(new GuideBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => 
            new WorkViewModel
            {
                DateStart = x.DateWork,
                DateEnd= x.DateWork + new TimeSpan (4,0,0),//заменить на значение у экскурсии
                GuideName = x.Name,
                GuideSurname = x.Surname,
                PhoneNumber = x.PhoneNumber,
                MainLanguage = x.MainLanguage,
                AdditionalLanguage = x.AdditionalLanguage,
                TourName = 
            })
           .ToList();*/
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
    }
}
