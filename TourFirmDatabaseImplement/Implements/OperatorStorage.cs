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
    public class OperatorStorage : IOperatorStorage
    {
        public List<OperatorViewModel> GetFullList()
        {
            using (var context = new TourFirmDatabase())
            {
                return context.Operators
                   .Select(rec => new OperatorViewModel
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

        public OperatorViewModel GetElement(OperatorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TourFirmDatabase())
            {
                var _operator = context.Operators
              .FirstOrDefault(rec => rec.ID == model.ID || rec.Login == model.Login);
                return _operator != null ? new OperatorViewModel
                {
                    ID = _operator.ID,
                    Name = _operator.Name,
                    Surname = _operator.Surname,
                    PhoneNumber = _operator.PhoneNumber,
                    Login = _operator.Login,
                    Password = _operator.Password
                } :
                null;
            }
        }
        public void Insert(OperatorBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Operators.Add(CreateModel(model, new Operator(), context));
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
        public void Update(OperatorBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Operators.FirstOrDefault(rec => rec.ID ==
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
        public void Delete(OperatorBindingModel model)
        {
            using (var context = new TourFirmDatabase())
            {
                var element = context.Operators.FirstOrDefault(rec => rec.ID ==
               model.ID);
                if (element != null)
                {
                    context.Operators.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Operator CreateModel(OperatorBindingModel model, Operator _operator, TourFirmDatabase context)
        {
            _operator.Name = model.Name;
            _operator.Surname = model.Surname;
            _operator.PhoneNumber = model.PhoneNumber;
            _operator.Mail = model.Mail;
            _operator.Login = model.Login;
            _operator.Password = model.Password;
            return _operator;
        }
    }
}
