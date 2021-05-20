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
                listTours = implementer.GetTourByMonthStatistic(model, _OperatorID);
            }
            else
            {
                listTours = implementer.GetAllTourByMonthStatistic(model);
            }

            var countTours = listTours.OrderBy(rec => rec.Country).GroupBy(rec => new { rec.Country })
                .Select(rec => new Tuple<string, int>(rec.Key.Country, rec.Count())).ToList().OrderByDescending(rec => rec.Item2).ToList();

            var result = new List<Tuple<string, int>>();
            if (countTours.Count > 5)
            {
                int sumcount = 0;
                for (int i = 4; i < countTours.Count - 1; i++)
                {
                    sumcount += countTours[i].Item2;
                }
                result.Add(new Tuple<string, int>("Другие", sumcount));
            }

            return countTours;
        }
        public List<Tuple<string, decimal>> GetTourByMonthBenefitStatistic(StatisticBindingModel model, int _OperatorID)
        {
            var listTours = new List<TourViewModel>();
            if (_OperatorID != 0)
            {
                listTours = implementer.GetTourByMonthStatistic(model, _OperatorID);
            }
            else
            {
                listTours = implementer.GetAllTourByMonthStatistic(model);
            }

            var tours = listTours.OrderByDescending(rec => rec.Price).GroupBy(rec =>  rec.Country, rec => rec.Price )
                .Select(rec => new Tuple<string, decimal>(rec.Key, rec.Sum())).ToList();

            var result = listTours.OrderByDescending(rec => rec.Price).GroupBy(rec => rec.Country, rec => rec.Price)
                .Select(rec => new Tuple<string, decimal>(rec.Key, rec.Sum())).Take(5).ToList();

            if (tours.Count > 5)
            {
                decimal sumprice = 0;
                for (int i = 4; i < tours.Count - 1; i++)
                {
                    sumprice += tours[i].Item2;
                }
                result.Add(new Tuple<string, decimal>("Другие", sumprice));
            }
            return result;
        }
    }
}
