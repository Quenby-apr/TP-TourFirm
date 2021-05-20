using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class OperatorStatisticLogic
    {
        private readonly IImplementingStatistics implementer;

        public OperatorStatisticLogic(IImplementingStatistics implementer)
        {
            this.implementer = implementer;
        }

        public List<Tuple<string, int>> GetGuideStatistic(ReportBindingModel model, int _OperatorID, bool all)
        {
            var listGuides = new List<GuideViewModel>();
            if (!all)
            {
                listGuides = implementer.GetGuidesStatistics(model, _OperatorID);
            }
            else
            {
                listGuides = implementer.GetAllGuidesStatistics(model, _OperatorID);
            }

            var countExcursion = listGuides.OrderBy(rec => rec.Surname).GroupBy(rec => new { rec.PhoneNumber, rec.Surname })
                .Select(rec => new Tuple<string, int>(rec.Key.Surname, rec.Count())).ToList();

            return countExcursion;
        }
        public List<Tuple<string, int>> GetTourByMonthStatistic(StatisticBindingModel model, int _OperatorID)
        {
            var listTours = new List<TourViewModel>();
            if (_OperatorID != 0)
            {
                listTours = implementer.GetAllTourByMonthStatistic(model);
            }
            else
            {
                listTours = implementer.GetTourByMonthStatistic(model, _OperatorID);
            }

            var countTours = listTours.OrderBy(rec => rec.Country).GroupBy(rec => new { rec.Country })
                .Select(rec => new Tuple<string, int>(rec.Key.Country, rec.Count())).ToList();

            return countTours;
        }
    }
}
