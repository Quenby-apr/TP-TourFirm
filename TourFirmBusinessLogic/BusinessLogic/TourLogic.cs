using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TourLogic
    {
        private readonly ITourStorage _tourStorage;
        public TourLogic(ITourStorage tourStorage)
        {
            _tourStorage = tourStorage;
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            if (model == null)
            {
                return _tourStorage.GetFullList();
            }
            if (model.ID.HasValue)
            {
                return new List<TourViewModel> { _tourStorage.GetElement(model) };
            }
            return _tourStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TourBindingModel model)
        {
            var element = _tourStorage.GetElement(new TourBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данный тур уже зарегистрирован");
            }
            if (model.ID.HasValue)
            {
                _tourStorage.Update(model);
            }
            else
            {
                _tourStorage.Insert(model);
            }
        }
        public void Delete(TourBindingModel model)
        {
            var element = _tourStorage.GetElement(new TourBindingModel { ID = model.ID });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _tourStorage.Delete(model);
        }
    }
}
