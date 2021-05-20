using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IImplementingStatistics
    {
        List<GuideViewModel> GetGuidesStatistics(ReportBindingModel model, int _OperatorID);
        List<GuideViewModel> GetAllGuidesStatistics(ReportBindingModel model, int _OperatorID);
        List<TourViewModel> GetTourByMonthStatistic(StatisticBindingModel model, int _OperatorID);
        List<TourViewModel> GetAllTourByMonthStatistic(StatisticBindingModel model);
    }
}
