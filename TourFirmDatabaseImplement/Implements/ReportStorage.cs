using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class ReportStorage : IReportStorage
    {
        public List<ReportGuidesViewModel> GetFullListGuides(ReportTourBindingModel model, int _OperatorID)
        {
            using (var context = new TourFirmDatabase())
            {
                var guides = from tour in context.Tours
                             join travelTours in context.TravelTours
                             on tour.ID equals travelTours.TourID
                             where tour.OperatorID == _OperatorID
                             join travel in context.Travels
                             on travelTours.TravelID equals travel.ID
                             join tourguide in context.TourGuides
                             on tour.ID equals tourguide.TourID
                             join guide in context.Guides
                             on tourguide.GuideID equals guide.ID
                             where guide.OperatorID == _OperatorID
                             join guideExcursion in context.GuideExcursions
                             on guide.ID equals guideExcursion.GuideID
                             join excursion in context.Excursions
                             on guideExcursion.ExcursionID equals excursion.ID
                             select new ReportGuidesViewModel
                             {
                                 DateStartTravel = travel.DateStart,
                                 GuideSurname = guide.Surname,
                                 GuideName = guide.Name,
                                 GuideWorkPlace = guide.WorkPlace,
                                 ExcursionName = excursion.Name,
                                 TourName = tour.Name
                             };
                return guides.ToList();
            }
        }
        public ReportTourExcursionsViewModel GetTourExcursion(TourBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var excusrions = from tour in context.Tours
                                    where tour.ID == model.ID
                                    join tourguide in context.TourGuides
                                    on tour.ID equals tourguide.TourID
                                    join guide in context.Guides
                                    on tourguide.GuideID equals guide.ID
                                    join guideexcurion in context.GuideExcursions
                                    on guide.ID equals guideexcurion.GuideID
                                    join excursion in context.Excursions
                                    on guideexcurion.ExcursionID equals excursion.ID
                                    select new ExcursionViewModel
                                    {
                                        Name = excursion.Name,
                                        Price = excursion.Price,
                                        Duration = excursion.Duration
                                    };
                return new ReportTourExcursionsViewModel
                {
                    TourName = model.Name,
                    Excursions = excusrions.ToList()
                };
            }
        }
    }
}
