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
    public class TouristStatisticsLogic
    {
        private readonly ITravelStorage travelStorage;

        public TouristStatisticsLogic(ITravelStorage travelStorage)
        {
            this.travelStorage = travelStorage;
        }

        public List<Tuple<string, int>> GetCountByMonths(int touristID)
        {
            var listAllTravels = new List<TravelViewModel>();

            if (touristID != 0)
            {
                listAllTravels = travelStorage.GetFilteredList(new TravelBindingModel
                {
                    TouristID = touristID
                });
            }
            else
            {
                listAllTravels = travelStorage.GetFullList();
            }

            var countByMonths = listAllTravels.OrderBy(rec => rec.DateStart.Month).GroupBy(rec => new { rec.DateStart.Month })
                .Select(rec => new Tuple<string, int>(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(rec.Key.Month), rec.Count())).ToList();

            return countByMonths;
        }
    }
}

