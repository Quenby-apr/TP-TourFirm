using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class TravelStorage : ITravelStorage
    {
        public List<TravelViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Travels
                    .Include(rec => rec.TravelTours)
                    .ThenInclude(rec => rec.Tour)
                    .Include(rec => rec.TravelExcursions)
                    .ThenInclude(rec => rec.Excursion)
                    .Include(rec => rec.Tourist)
                    .ToList()
                    .Select(rec => new TravelViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        DateStart = rec.DateStart,
                        DateEnd = rec.DateEnd,
                        TouristID = rec.TouristID,
                        TravelTours = rec.TravelTours.ToDictionary(recTT => recTT.TourID, recTT => (recTT.Tour?.Name)),
                        TravelExcursions = rec.TravelExcursions.ToDictionary(recTE => recTE.ExcursionID, recTE => (recTE.Excursion?.Name))
                    }).ToList();
            }
        }

        public List<TravelViewModel> GetFilteredList(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                return context.Travels
                    .Include(rec => rec.TravelTours)
                    .ThenInclude(rec => rec.Tour)
                    .Include(rec => rec.TravelExcursions)
                    .ThenInclude(rec => rec.Excursion)
                    .Include(rec => rec.Tourist)
                    .Where(rec => rec.Name.Contains(model.Name))
                    .ToList().
                    Select(rec => new TravelViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        DateStart = rec.DateStart,
                        DateEnd = rec.DateEnd,
                        TouristID = rec.TouristID,
                        TravelTours = rec.TravelTours.ToDictionary(recTT => recTT.TourID, recTT => (recTT.Tour?.Name)),
                        TravelExcursions = rec.TravelExcursions.ToDictionary(recTE => recTE.ExcursionID, recTE => (recTE.Excursion?.Name))
                    }).ToList();
            }
        }

        public TravelViewModel GetElement(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                Travel travel = context.Travels
                    .Include(rec => rec.TravelTours)
                    .ThenInclude(rec => rec.Tour)
                    .Include(rec => rec.TravelExcursions)
                    .ThenInclude(rec => rec.Excursion)
                    .Include(rec => rec.Tourist)
                    .FirstOrDefault(rec => rec.Name == model.Name || rec.ID == model.ID);
                return travel != null ? new TravelViewModel
                {
                    ID = travel.ID,
                    Name = travel.Name,
                    DateStart = travel.DateStart,
                    DateEnd = travel.DateEnd,
                    TouristID = travel.TouristID,
                    TravelTours = travel.TravelTours.ToDictionary(recTT => recTT.TourID, recTT => (recTT.Tour?.Name)),
                    TravelExcursions = travel.TravelExcursions.ToDictionary(recTE => recTE.ExcursionID, recTE => (recTE.Excursion?.Name))
                } : null;
            }
        }

        public List<TravelViewModel> GetUserList(int UserID)
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Travels
                    .Include(rec => rec.TravelTours)
                    .ThenInclude(rec => rec.Tour)
                    .Include(rec => rec.TravelExcursions)
                    .ThenInclude(rec => rec.Excursion)
                    .Where(rec => rec.TouristID.Equals(UserID))
                    .ToList()
                    .Select(rec => new TravelViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        DateStart = rec.DateStart,
                        DateEnd = rec.DateEnd,
                        TouristID = rec.TouristID,
                        TravelTours = rec.TravelTours.ToDictionary(recTT => recTT.TourID, recTT => (recTT.Tour?.Name)),
                        TravelExcursions = rec.TravelExcursions.ToDictionary(recTE => recTE.ExcursionID, recTE => (recTE.Excursion?.Name))
                    }).ToList();
            }
        }

        public void Insert(TravelBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Travel(), context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(TravelBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Travel element = context.Travels.FirstOrDefault(rec => rec.ID == model.ID);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element, context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(TravelBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Travel element = context.Travels.FirstOrDefault(rec => rec.ID == model.ID);

                if (element != null)
                {
                    context.Travels.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Travel CreateModel(TravelBindingModel model, Travel travel, TourFirmDatabase context)
        {
            travel.Name = model.Name;
            travel.DateStart = model.DateStart;
            travel.DateEnd = model.DateEnd;
            travel.TouristID = model.TouristID;

            if (travel.ID == 0)
            {
                context.Travels.Add(travel);
                context.SaveChanges();
            }

            if (model.ID.HasValue)
            {
                List<TravelTour> travelTours = context.TravelTours
                    .Where(rec => rec.TravelID == model.ID.Value).ToList();
                context.TravelTours.RemoveRange(travelTours
                    .Where(rec => !model.TravelTours.ContainsKey(rec.TourID)).ToList());

                List<TravelExcursion> travelExcursions = context.TravelExcursions
                    .Where(rec => rec.TravelID == model.ID.Value).ToList();
                context.TravelExcursions.RemoveRange(travelExcursions
                    .Where(rec => !model.TravelExcursions.ContainsKey(rec.ExcursionID)).ToList());

                context.SaveChanges();

                // Убираем повторы
                foreach (var travelTour in travelTours)
                {
                    if (model.TravelTours.ContainsKey(travelTour.TourID))
                    {
                        model.TravelTours.Remove(travelTour.TourID);
                    }
                }

                foreach (var travelExcursion in travelExcursions)
                {
                    if (model.TravelExcursions.ContainsKey(travelExcursion.ExcursionID))
                    {
                        model.TravelTours.Remove(travelExcursion.ExcursionID);
                    }
                }
                context.SaveChanges();
            }

            foreach (var tt in model.TravelTours)
            {
                context.TravelTours.Add(new TravelTour
                {
                    TravelID = travel.ID,
                    TourID = tt.Key,
                });
                context.SaveChanges();
            }

            foreach (var te in model.TravelExcursions)
            {
                context.TravelExcursions.Add(new TravelExcursion
                {
                    TravelID = travel.ID,
                    ExcursionID = te.Key,
                });
                context.SaveChanges();
            }
            return travel;
        }
    }
}
