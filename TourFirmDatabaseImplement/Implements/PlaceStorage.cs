using System;
using System.Collections.Generic;
using System.Linq;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class PlaceStorage : IPlaceStorage
    {
        public List<PlaceViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Places
                    .ToList()
                    .Select(rec => new PlaceViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Type = rec.Type,
                        TouristID = rec.TouristID
                    }).ToList();
            }
        }

        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                return context.Places
                    .Where(rec => rec.Name.Contains(model.Name))
                    .ToList()
                    .Select(rec => new PlaceViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Type = rec.Type,
                        TouristID = rec.TouristID
                    }).ToList();
            }
        }

        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                Place place = context.Places
                    .FirstOrDefault(rec => rec.Name == model.Name || rec.ID == model.ID);
                return place != null ? new PlaceViewModel
                {
                    ID = place.ID,
                    Name = place.Name,
                    Type = place.Type,
                    TouristID = place.TouristID
                } : null;
            }
        }

        public void Insert(PlaceBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                context.Places.Add(CreateModel(model, new Place()));
                context.SaveChanges();
            }
        }

        public void Update(PlaceBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Place element = context.Places.FirstOrDefault(rec => rec.ID == model.ID);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element);
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

        public void Delete(PlaceBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Place element = context.Places.FirstOrDefault(rec => rec.ID == model.ID);

                if (element != null)
                {
                    context.Places.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Place CreateModel(PlaceBindingModel model, Place place)
        {
            place.Name = model.Name;
            place.Type = model.Type;
            place.TouristID = model.TouristID;
            return place;
        }
    }
}
