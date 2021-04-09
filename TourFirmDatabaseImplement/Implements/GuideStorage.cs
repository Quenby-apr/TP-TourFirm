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
    public class GuideStorage : IGuideStorage
    {
        public GuideViewModel GetElement(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TourFirmDatabase())
            {
                var guide = context.Guides
              .Include(rec => rec.GuideExcursions)
              .ThenInclude(rec => rec.Excursion)
              .FirstOrDefault(rec => rec.ID == model.ID || rec.Surname == model.Surname);
                return guide != null ? new GuideViewModel
                {
                    ID = guide.ID,
                    Name = guide.Name,
                    Surname = guide.Surname,
                    PhoneNumber = guide.PhoneNumber,
                    WorkPlace = guide.WorkPlace,
                    MainLanguage = guide.MainLanguage,
                    AdditionalLanguage = guide.AdditionalLanguage,
                    OperatorID = guide.OperatorID,
                    GuideExcursions = guide.GuideExcursions
                    .ToDictionary(recEX => recEX.ExcursionID, recEX => (recEX.Excursion?.Name))
                } :
                null;
            }
        }

        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                return context.Guides
                    .Include(rec => rec.GuideExcursions)
                    .ThenInclude(rec => rec.Excursion)
                    .Where(rec => (rec.OperatorID == model.OperatorID))
                    .ToList().
                    Select(rec => new GuideViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Surname = rec.Surname,
                        PhoneNumber = rec.PhoneNumber,
                        WorkPlace = rec.WorkPlace,
                        MainLanguage = rec.MainLanguage,
                        AdditionalLanguage = rec.AdditionalLanguage,
                        OperatorID = rec.OperatorID,
                        GuideExcursions = rec.GuideExcursions
                        .ToDictionary(recPC => recPC.ExcursionID, recPC => (recPC.Excursion?.Name))
                    }).ToList();
            }
        }

        public List<GuideViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Guides
                    .Include(rec => rec.GuideExcursions)
                    .ThenInclude(rec => rec.Excursion).ToList()
                   .Select(rec => new GuideViewModel
                   {
                       ID = rec.ID,
                       Name = rec.Name,
                       Surname = rec.Surname,
                       PhoneNumber = rec.PhoneNumber,
                       WorkPlace = rec.WorkPlace,
                       MainLanguage = rec.MainLanguage,
                       AdditionalLanguage = rec.AdditionalLanguage,
                       OperatorID = rec.OperatorID,
                       GuideExcursions = rec.GuideExcursions
                        .ToDictionary(recPC => recPC.ExcursionID, recPC => (recPC.Excursion?.Name))
                   })
                    .ToList();
            }
        }

        public void Insert(GuideBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Guide guide = new Guide
                        {
                            Name = model.Name,
                            Surname = model.Surname,
                            PhoneNumber = model.PhoneNumber,
                            WorkPlace = model.WorkPlace,
                            MainLanguage = model.MainLanguage,
                            AdditionalLanguage = model.AdditionalLanguage,
                            OperatorID = model.OperatorID
                        };
                        context.Guides.Add(guide);
                        context.SaveChanges();
                        CreateModel(model, guide, context);
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
        public void Update(GuideBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Guides.FirstOrDefault(rec => rec.ID ==
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
        public void Delete(GuideBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Guide element = context.Guides.FirstOrDefault(rec => rec.ID ==
               model.ID);
                if (element != null)
                {
                    context.Guides.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Guide CreateModel(GuideBindingModel model, Guide guide, TourFirmDatabase context)
        {
            guide.Name = model.Name;
            guide.Surname = model.Surname;
            guide.PhoneNumber = model.PhoneNumber;
            guide.WorkPlace = model.WorkPlace;
            guide.MainLanguage = model.MainLanguage;
            guide.AdditionalLanguage = model.AdditionalLanguage;
            guide.OperatorID = model.OperatorID;
            if (model.GuideExcursions != null)
            {
                if (model.ID.HasValue)
                {
                    var excursionGuides = context.GuideExcursions.Where(rec =>
                   rec.GuideID == model.ID.Value).ToList();
                    // удалили те, которых нет в модели
                    context.GuideExcursions.RemoveRange(excursionGuides.Where(rec =>
                   !model.GuideExcursions.ContainsKey(rec.ExcursionID)).ToList());
                    context.SaveChanges();
                }
                // добавили новые
                foreach (var eg in model.GuideExcursions)
                {
                    context.GuideExcursions.Add(new GuideExcursion
                    {
                        GuideID = guide.ID,
                        ExcursionID = eg.Key,
                    });
                    context.SaveChanges();
                }
            }
            return guide;
        }
    }
}
