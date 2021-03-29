using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class ExcursionLogic
    {
        private readonly IExcursionStorage _excursionStorage;

        public ExcursionLogic(IExcursionStorage excursionStorage)
        {
            _excursionStorage = excursionStorage;
        }

        public List<ExcursionViewModel> Read(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return _excursionStorage.GetFullList();
            }
            if (model.ID.HasValue)
            {
                return new List<ExcursionViewModel> { _excursionStorage.GetElement(model) };
            }
            return _excursionStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ExcursionBindingModel model)
        {
            var element = _excursionStorage.GetElement(new ExcursionBindingModel
            {
                Name = model.Name,
            });
            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данная экскурсия уже зарегистрирована");
            }
            if (model.ID.HasValue)
            {
                _excursionStorage.Update(model);
            }
            else
            {
                _excursionStorage.Insert(model);
            }
        }

        public void Delete(ExcursionBindingModel model)
        {
            var element = _excursionStorage.GetElement(new ExcursionBindingModel { ID = model.ID });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _excursionStorage.Delete(model);
        }
    }
}
