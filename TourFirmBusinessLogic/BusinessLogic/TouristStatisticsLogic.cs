using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TouristStatisticsLogic
    {
        private readonly ITravelStorage travelStorage;
        private readonly IExcursionStorage excursionStorage;

        public TouristStatisticsLogic(ITravelStorage travelStorage, IExcursionStorage excursionStorage)
        {
            this.travelStorage = travelStorage;
            this.excursionStorage = excursionStorage;
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

        public List<Tuple<string, int>> GetExcursionsInfo()
        {
            var listAllTravels = travelStorage.GetFullList();
            var listAllExcursions = excursionStorage.GetFullList();

            var result = new List<Tuple<string, int>>();

            foreach (var travel in listAllTravels)
            {
                foreach (var excuirsion in travel.TravelExcursions)
                {
                    var record = result.Where(rec => rec.Item1.Equals(excuirsion.Value)).Select(rec => new Tuple<string, int>(rec.Item1, rec.Item2)).ToList();

                    if (record.Count > 0)
                    {
                        record[0] = new Tuple<string, int>(excuirsion.Value, record[0].Item2 + 1);
                    }

                    else
                    {
                        result.Add(new Tuple<string, int>(excuirsion.Value.ToString(), 1));
                    }
                }
            }

            return result;
        }
    }
}