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
    public class ExcursionStorage : IExcursionStorage
    {

        public ExcursionViewModel GetElement(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TourFirmDatabase())
            {
                var excursion = context.Excursions
               .Include(rec => rec.ExcursionGuides)
              .ThenInclude(rec => rec.Guide)
              .FirstOrDefault(rec => rec.Name == model.Name || rec.ID == model.ID);
                return excursion != null ? new ExcursionViewModel
                {
                    ID = excursion.ID,
                    Name = excursion.Name,
                    Price = excursion.Price,
                    Duration = excursion.Duration,
                    PlaceID=excursion.PlaceID,
                    TouristID=excursion.TouristID,
                    ExcursionGuides=excursion.ExcursionGuides
                    .ToDictionary(recEX => recEX.GuideID, recEX => (recEX.Guide?.Name))
                } :
                null;
            }
        }

        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ExcursionViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Excursions
                    .Include(rec => rec.ExcursionGuides)
                    .ThenInclude(rec => rec.Guide).ToList().Select(rec => new ExcursionViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Price = rec.Price,
                        Duration = rec.Duration,
                        PlaceID = rec.PlaceID,
                        TouristID = rec.TouristID,
                        ExcursionGuides = rec.ExcursionGuides
                        .ToDictionary(recPC => recPC.GuideID, recPC => (recPC.Guide?.Name))
                    })
                    .ToList();
            }
        }

        public List<ExcursionViewModel> GetUserList(int UserID)
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Excursions.Include(rec => rec.ExcursionGuides)
                    .ThenInclude(rec => rec.Guide)
                    .Where(rec => rec.TouristID.Equals(UserID))
                    .ToList().Select(rec => new ExcursionViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Price = rec.Price,
                        Duration = rec.Duration,
                        PlaceID = rec.PlaceID,
                        TouristID = rec.TouristID,
                        ExcursionGuides = rec.ExcursionGuides
                        .ToDictionary(recPC => recPC.GuideID, recPC => (recPC.Guide?.Name))
                    })
                    .ToList();
            }
        }

        public void Insert(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Excursion excursion = new Excursion
                        {
                            Name = model.Name,
                        };
                        context.Excursions.Add(excursion);
                        context.SaveChanges();
                        CreateModel(model, excursion, context);
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
        public void Update(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Excursions.FirstOrDefault(rec => rec.ID ==
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
        public void Delete(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Excursion element = context.Excursions.FirstOrDefault(rec => rec.ID ==
               model.ID);
                if (element != null)
                {
                    context.Excursions.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Excursion CreateModel(ExcursionBindingModel model, Excursion excursion, TourFirmDatabase context)
        {
            excursion.Name = model.Name;
            excursion.Price = model.Price;
            excursion.Duration = model.Duration;
            excursion.PlaceID = model.PlaceID;
            excursion.TouristID = model.TouristID;
            if (model.ID.HasValue)
            {
                var excursionGuides = context.ExcursionGuides.Where(rec =>
               rec.ExcursionID == model.ID.Value).ToList();
                // удалили те, которых нет в модели
                context.ExcursionGuides.RemoveRange(excursionGuides.Where(rec =>
               !model.ExcursionGuides.ContainsKey(rec.GuideID)).ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var eg in model.ExcursionGuides)
            {
                context.ExcursionGuides.Add(new ExcursionGuide
                {
                    ExcursionID = excursion.ID,
                    GuideID = eg.Key,
                });
                context.SaveChanges();
            }
            return excursion;
        }
    }
}
