using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
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
        public List<WorkViewModel> GetWorks(ReportBindingModel model)
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
           .ToList();
        }
    }
}
