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
              .FirstOrDefault(rec =>  rec.ID == model.ID);
                return guide != null ? new GuideViewModel
                {
                    ID = guide.ID,
                    Name = guide.Name,
                    Surname = guide.Surname,
                    PhoneNumber = guide.PhoneNumber,
                    WorkPlace = guide.WorkPlace,
                    MainLanguage = guide.MainLanguage,
                    AdditionalLanguage = guide.AdditionalLanguage,
                    DateWork = guide.DateWork,
                    OperatorID = guide.OperatorID,
                } :
                null;
            }
        }

        public List<GuideViewModel> GetFilteredList(GuideBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<GuideViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Guides
                   .Select(rec => new GuideViewModel
                   {
                       ID = rec.ID,
                       Name = rec.Name,
                       Surname = rec.Surname,
                       PhoneNumber = rec.PhoneNumber,
                       WorkPlace = rec.WorkPlace,
                       MainLanguage = rec.MainLanguage,
                       AdditionalLanguage = rec.AdditionalLanguage,
                       DateWork = rec.DateWork,
                       OperatorID = rec.OperatorID,
                   })
                    .ToList();
            }
        }

        public List<GuideViewModel> GetUserList(int UserID)
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Guides
                   .Where(rec => rec.OperatorID.Equals(UserID))
                   .Select(rec => new GuideViewModel
                   {
                       ID = rec.ID,
                       Name = rec.Name,
                       Surname = rec.Surname,
                       PhoneNumber = rec.PhoneNumber,
                       WorkPlace = rec.WorkPlace,
                       MainLanguage = rec.MainLanguage,
                       AdditionalLanguage = rec.AdditionalLanguage,
                       DateWork = rec.DateWork,
                       OperatorID = rec.OperatorID,
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
            guide.DateWork = model.DateWork;
            guide.OperatorID = model.OperatorID;
            return guide;
        }
    }
    }
}
