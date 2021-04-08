using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class OperatorLogic
    {
        private readonly IOperatorStorage _operatorStorage;

        public OperatorLogic(IOperatorStorage operatorStorage)
        {
            _operatorStorage = operatorStorage;
        }

        public List<OperatorViewModel> Read(OperatorBindingModel model)
        {
            if (model == null)
            {
                return _operatorStorage.GetFullList();
            }
            if (model.ID.HasValue)
            {
                return new List<OperatorViewModel> { _operatorStorage.GetElement(model) };
            }
            return null;
        }

        public void CreateOrUpdate(OperatorBindingModel model)
        {
            var elementByLogin = _operatorStorage.GetElement(new OperatorBindingModel
            {
                Login = model.Login
            });
            if (elementByLogin != null && elementByLogin.ID != model.ID)
            {
                throw new Exception("Данный логин уже занят");
            }

            var elementByMail = _operatorStorage.GetElement(new OperatorBindingModel
            {
                Mail = model.Mail
            });
            if (elementByMail != null && elementByMail.ID != model.ID)
            {
                throw new Exception("Данный электронная почта уже занята");
            }

            if (model.ID.HasValue)
            {
                _operatorStorage.Update(model);
            }
            else
            {
                _operatorStorage.Insert(model);
            }
        }

        public void Delete(OperatorBindingModel model)
        {
            var element = _operatorStorage.GetElement(new OperatorBindingModel { ID = model.ID });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _operatorStorage.Delete(model);
        }
    }
}