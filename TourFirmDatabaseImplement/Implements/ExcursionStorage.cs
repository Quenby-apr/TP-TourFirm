using System;
using System.Collections.Generic;
using System.Linq;
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
              .FirstOrDefault(rec => rec.Name == model.Name || rec.ID == model.ID);
                return excursion != null ? new ExcursionViewModel
                {
                    ID = excursion.ID,
                    Name = excursion.Name,
                    Price = excursion.Price,
                    Duration = excursion.Duration,
                    PlaceID = excursion.PlaceID,
                    TouristID = excursion.TouristID,
                } :
                null;
            }
        }

        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                return context.Excursions
                    .Where(rec => rec.TouristID == model.TouristID)
                    .Select(rec => new ExcursionViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Price = rec.Price,
                        Duration = rec.Duration,
                        PlaceID = rec.PlaceID,
                        TouristID = rec.TouristID,
                    })
                    .ToList();
            }
        }

        public List<ExcursionViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Excursions
                    .Select(rec => new ExcursionViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Price = rec.Price,
                        Duration = rec.Duration,
                        PlaceID = rec.PlaceID,
                        TouristID = rec.TouristID,
                    })
                    .ToList();
            }
        }

        public void Insert(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                context.Excursions.Add(CreateModel(model, new Excursion()));
                context.SaveChanges();
            }
        }

        public void Update(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var element = context.Excursions.FirstOrDefault(rec => rec.ID == model.ID);

                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ExcursionBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Excursion element = context.Excursions.FirstOrDefault(rec => rec.ID == model.ID);

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

        private Excursion CreateModel(ExcursionBindingModel model, Excursion excursion)
        {
            excursion.Name = model.Name;
            excursion.Price = model.Price;
            excursion.Duration = model.Duration;
            excursion.PlaceID = model.PlaceID;
            excursion.TouristID = model.TouristID;
            return excursion;
        }
    }
}