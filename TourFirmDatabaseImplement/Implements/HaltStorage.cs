using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Models;

namespace TourFirmDatabaseImplement.Implements
{
    public class HaltStorage : IHaltStorage
    {
        public HaltViewModel GetElement(HaltBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TourFirmDatabase())
            {
                var halt = context.Halts.FirstOrDefault
                    (rec => rec.Name == model.Name || rec.ID == model.ID);
                return halt != null ? new HaltViewModel
                {
                    ID = halt.ID,
                    Name = halt.Name,
                    Address = halt.Address,
                    OperatorID = halt.OperatorID
                } :
                null;
            }
        }

        public List<HaltViewModel> GetFilteredList(HaltBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<HaltViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Halts.Select(rec => new HaltViewModel
                {
                    ID = rec.ID,
                    Name = rec.Name,
                    Address = rec.Address,
                    OperatorID = rec.OperatorID
                }).ToList();
            }
        }

        public List<HaltViewModel> GetUserList(int UserID)
        {  
             using (var context = new TourFirmDatabase())
            {
                return context.Halts
                    .Where(rec => rec.OperatorID.Equals(UserID))
                    .Select(rec => new HaltViewModel
                {
                    ID = rec.ID,
                    Name = rec.Name,
                    Address = rec.Address,
                    OperatorID = rec.OperatorID
                }).ToList();
            }
        }

        public void Insert(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Halt halt = new Halt
                        {
                            Name = model.Name,
                        };
                        context.Halts.Add(halt);
                        context.SaveChanges();
                        CreateModel(model, halt, context);
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
        public void Update(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Halts.FirstOrDefault(rec => rec.ID ==
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
        public void Delete(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Halt element = context.Halts.FirstOrDefault(rec => rec.ID ==
               model.ID);
                if (element != null)
                {
                    context.Halts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Halt CreateModel(HaltBindingModel model, Halt halt, TourFirmDatabase context)
        {
            halt.Name = model.Name;
            halt.Address = model.Address;
            halt.OperatorID = model.OperatorID;
            return halt;
        }
    }
}
