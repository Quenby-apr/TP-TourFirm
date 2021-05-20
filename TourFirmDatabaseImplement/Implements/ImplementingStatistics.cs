using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmDatabaseImplement.Implements
{
    public class ImplementingStatistics : IImplementingStatistics
    {
        public List<GuideViewModel> GetGuidesStatistics (ReportBindingModel model, int _OperatorID)
        {
            using (var context = new TourFirmDatabase())
            {
                var guides = from tour in context.Tours
                             where tour.OperatorID == _OperatorID
                             join travelTours in context.TravelTours
                             on tour.ID equals travelTours.TourID
                             join travel in context.Travels
                             on travelTours.TravelID equals travel.ID
                             where travel.DateStart >= model.DateFrom
                             where travel.DateEnd <= model.DateTo
                             join tourguide in context.TourGuides
                             on tour.ID equals tourguide.TourID
                             join guide in context.Guides
                             on tourguide.GuideID equals guide.ID
                             where guide.OperatorID == _OperatorID
                             join guideExcursion in context.GuideExcursions
                             on guide.ID equals guideExcursion.GuideID
                             join excursion in context.Excursions
                             on guideExcursion.ExcursionID equals excursion.ID
                             select new GuideViewModel
                             {
                                 Surname = guide.Surname,
                                 Name = guide.Name,
                                 PhoneNumber = guide.PhoneNumber
                             };
                return guides.ToList();
            }
        }
        public List<GuideViewModel> GetAllGuidesStatistics(ReportBindingModel model, int _OperatorID)
        {
            using (var context = new TourFirmDatabase())
            {
                var guides = from tour in context.Tours
                             where tour.OperatorID != _OperatorID
                             join travelTours in context.TravelTours
                             on tour.ID equals travelTours.TourID
                             join travel in context.Travels
                             on travelTours.TravelID equals travel.ID
                             where travel.DateStart >= model.DateFrom
                             where travel.DateEnd <= model.DateTo
                             join tourguide in context.TourGuides
                             on tour.ID equals tourguide.TourID
                             join guide in context.Guides
                             on tourguide.GuideID equals guide.ID
                             join guideExcursion in context.GuideExcursions
                             on guide.ID equals guideExcursion.GuideID
                             join excursion in context.Excursions
                             on guideExcursion.ExcursionID equals excursion.ID
                             select new GuideViewModel
                             {
                                 Surname = guide.Surname,
                                 Name = guide.Name,
                                 PhoneNumber = guide.PhoneNumber
                             };
                return guides.ToList();
            }
        }
        public List<TourViewModel> GetAllTourByMonthStatistic(StatisticBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var tours = from tour in context.Tours
                             join travelTours in context.TravelTours
                             on tour.ID equals travelTours.TourID
                             join travel in context.Travels
                             on travelTours.TravelID equals travel.ID
                             where travel.DateStart.Month == model.month
                             select new TourViewModel
                             {
                                 Country = tour.Country,
                                 Price = tour.Price
                             };
                return tours.ToList();
            }
        }
        public List<TourViewModel> GetTourByMonthStatistic(StatisticBindingModel model, int _OperatorID)
        {
            using (var context = new TourFirmDatabase())
            {
                var tours = from tour in context.Tours
                            where tour.OperatorID == _OperatorID
                            join travelTours in context.TravelTours
                            on tour.ID equals travelTours.TourID
                            join travel in context.Travels
                            on travelTours.TravelID equals travel.ID
                            where travel.DateStart.Month == model.month
                            select new TourViewModel
                            {
                                Country = tour.Country,
                                Price = tour.Price
                            };
                return tours.ToList();
            }
        }
        public List<ExcursionViewModel> GetAllExcursionStatistics(ReportBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var excursions = from tour in context.Tours
                             join travelTours in context.TravelTours
                             on tour.ID equals travelTours.TourID
                             join travel in context.Travels
                             on travelTours.TravelID equals travel.ID
                             where travel.DateStart >= model.DateFrom
                             where travel.DateEnd <= model.DateTo
                             join tourguide in context.TourGuides
                             on tour.ID equals tourguide.TourID
                             join guide in context.Guides
                             on tourguide.GuideID equals guide.ID
                             join guideExcursion in context.GuideExcursions
                             on guide.ID equals guideExcursion.GuideID
                             join excursion in context.Excursions
                             on guideExcursion.ExcursionID equals excursion.ID
                             select new ExcursionViewModel
                             {
                                 Price = excursion.Price,
                                 Duration = excursion.Duration
                             };
                return excursions.ToList();
            }
        }
    }
}
