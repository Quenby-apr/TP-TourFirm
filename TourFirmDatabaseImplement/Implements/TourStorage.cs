using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class TourStorage : ITourStorage
    {
        public TourViewModel GetElement(TourBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TourFirmDatabase())
            {
                var tour = context.Tours
               .Include(rec => rec.TourGuides)
              .ThenInclude(rec => rec.Guide)
              .FirstOrDefault(rec => rec.ID == model.ID);
                return tour != null ? new TourViewModel
                {
                    ID = tour.ID,
                    Name = tour.Name,
                    Country = tour.Country,
                    Price = tour.Price,
                    OperatorID = tour.OperatorID,
                    HaltID = tour.HaltID,
                    TourGuides = tour.TourGuides
                    .ToDictionary(recTG => recTG.GuideID, recTG => (recTG.Guide?.Surname))
                } :
                null;
            }
        }

        public List<TourViewModel> GetFilteredList(TourBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Tours
                    .Include(rec => rec.TourGuides)
                    .ThenInclude(rec => rec.Guide)
                    .Where(rec => (rec.OperatorID == model.OperatorID))
                    .ToList()
                    .Select(rec => new TourViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Country = rec.Country,
                        Price = rec.Price,
                        OperatorID = rec.OperatorID,
                        HaltID = rec.HaltID,
                        TourGuides = rec.TourGuides
                    .ToDictionary(recTG => recTG.GuideID, recTG => (recTG.Guide?.Surname))
                    })
                    .ToList();
            }
        }

        public List<TourViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Tours
                    .Include(rec => rec.TourGuides)
                    .ThenInclude(rec => rec.Guide)
                    .ToList()
                    .Select(rec => new TourViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Country = rec.Country,
                        Price = rec.Price,
                        OperatorID = rec.OperatorID,
                        HaltID = rec.HaltID,
                        TourGuides = rec.TourGuides
                    .ToDictionary(recTG => recTG.GuideID, recTG => (recTG.Guide?.Surname))
                    })
                    .ToList();
            }
        }

        public void Insert(TourBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Tour tour = new Tour
                        {
                            Name = model.Name,
                            Price = model.Price,
                            Country = model.Country,
                            HaltID = model.HaltID,
                            OperatorID = model.OperatorID
                        };
                        context.Tours.Add(tour);
                        context.SaveChanges();
                        CreateModel(model, tour, context);
                        context.SaveChanges();
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
        public void Update(TourBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Tours.FirstOrDefault(rec => rec.ID ==
                       model.ID);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
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
        public void Delete(TourBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Tour element = context.Tours.FirstOrDefault(rec => rec.ID ==
               model.ID);
                if (element != null)
                {
                    context.Tours.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Tour CreateModel(TourBindingModel model, Tour tour, TourFirmDatabase context)
        {
            tour.Name = model.Name;
            tour.Country = model.Country;
            tour.Price = model.Price;
            tour.HaltID = model.HaltID;
            tour.OperatorID = model.OperatorID;
            if (model.ID.HasValue)
            {
                var tourGuides = context.TourGuides.Where(rec =>
               rec.TourID == model.ID.Value).ToList();
                // удалили те, которых нет в модели
                context.TourGuides.RemoveRange(tourGuides.ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var tg in model.TourGuides)
            {
                context.TourGuides.Add(new TourGuide
                {
                    TourID = tour.ID,
                    GuideID = tg.Key,
                });
                context.SaveChanges();
            }
            return tour;
        }
    }
}
