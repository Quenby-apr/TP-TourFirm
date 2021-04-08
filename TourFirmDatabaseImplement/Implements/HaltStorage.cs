using System;
using System.Collections.Generic;
using System.Linq;
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
                var halt = context.Halts.FirstOrDefault(rec => rec.Name == model.Name || rec.ID == model.ID);
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
            if (model == null)
            {
                return null;
            }

            using (var context = new TourFirmDatabase())
            {
                return context.Halts
                    .Where(rec => (rec.OperatorID == model.OperatorID))
                    .ToList().
                    Select(rec => new HaltViewModel
                    {
                        ID = rec.ID,
                        Name = rec.Name,
                        Address = rec.Address,
                        OperatorID = rec.OperatorID
                    }).ToList();
            }
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

        public void Insert(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                context.Halts.Add(CreateModel(model, new Halt()));
                context.SaveChanges();
            }
        }

        public void Update(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var element = context.Halts.FirstOrDefault(rec => rec.ID == model.ID);

                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(HaltBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                Halt element = context.Halts.FirstOrDefault(rec => rec.ID == model.ID);

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

        private Halt CreateModel(HaltBindingModel model, Halt halt)
        {
            halt.Name = model.Name;
            halt.Address = model.Address;
            halt.OperatorID = model.OperatorID;
            return halt;
        }
    }
}