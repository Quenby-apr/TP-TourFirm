using System;
using System.Collections.Generic;
using System.Linq;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class TouristStorage : ITouristStorage
    {
        public List<TouristViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Tourists
                   .Select(rec => new TouristViewModel
                   {
                       ID = rec.ID,
                       Name = rec.Name,
                       Surname = rec.Surname,
                       PhoneNumber = rec.PhoneNumber,
                       Login = rec.Login,
                       Password = rec.Password
                   })
                    .ToList();
            }
        }

        public TouristViewModel GetElement(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                var tourist = context.Tourists.FirstOrDefault(rec => rec.ID == model.ID);
                return tourist != null ? new TouristViewModel
                {
                    ID = tourist.ID,
                    Name = tourist.Name,
                    Surname = tourist.Surname,
                    PhoneNumber = tourist.PhoneNumber,
                    Login = tourist.Login,
                    Password = tourist.Password
                } :
                null;
            }
        }

        public void Insert(TouristBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                CreateModel(model, new Tourist());
                context.SaveChanges();
            }
        }

        public void Update(TouristBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Tourist element = context.Tourists.FirstOrDefault(rec => rec.ID == model.ID);

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

        public void Delete(TouristBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Tourist element = context.Tourists.FirstOrDefault(rec => rec.ID == model.ID);

                if (element != null)
                {
                    context.Tourists.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        
        private Tourist CreateModel(TouristBindingModel model, Tourist tourist)
        {
            tourist.Name = model.Name;
            tourist.Surname = model.Surname;
            tourist.PhoneNumber = model.PhoneNumber;
            tourist.Mail = model.Mail;
            tourist.Login = model.Login;
            tourist.Password = model.Password;
            return tourist;
        }
    }
}
