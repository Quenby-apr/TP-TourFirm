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

        public List<Tuple<string, int>> GetGuideStatistic(ReportBindingModel model, int _OperatorID)
        {
            var listGuides = new List<GuideViewModel>();
            if (_OperatorID != 0)
            {
                listGuides = implementer.GetGuidesStatistics(model, _OperatorID);
            }
            else
            {
                listGuides = implementer.GetAllGuidesStatistics(model);
            }

            var countExcursion = listGuides.OrderBy(rec => rec.Surname).GroupBy(rec => new { rec.Surname })
                .Select(rec => new Tuple<string, int>(rec.Key.Surname, rec.Count())).ToList();

            return countExcursion;
        }
    }
}
