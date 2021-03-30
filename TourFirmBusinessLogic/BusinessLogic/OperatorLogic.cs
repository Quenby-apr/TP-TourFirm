using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class OperatorLogic
    {
        private readonly IOperatorStorage _operatorStorage;

        public OperatorLogic(IOperatorStorage operatorStorage)
        {
            _operatorStorage = operatorStorage;
        }
        public void CreateOrUpdate(OperatorBindingModel model)
        {
            var element = _operatorStorage.GetElement(new OperatorBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данный пользователь уже зарегистрирован");
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

