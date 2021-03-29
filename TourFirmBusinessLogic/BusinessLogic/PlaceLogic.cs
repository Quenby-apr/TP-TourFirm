using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class PlaceLogic
    {
        private readonly IPlaceStorage _placeStorage;

        public PlaceLogic(IPlaceStorage placeStorage)
        {
            _placeStorage = placeStorage;
        }

        public List<PlaceViewModel> Read(PlaceBindingModel model)
        {
            if (model == null)
            {
                return _placeStorage.GetFullList();
            }

            if (model.ID.HasValue)
            {
                return new List<PlaceViewModel> { _placeStorage.GetElement(model) };
            }
            return _placeStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PlaceBindingModel model)
        {
            var element = _placeStorage.GetElement(new PlaceBindingModel
            {
                Name = model.Name
            });

            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данное место уже зарегистрировано");
            }

            if (model.ID.HasValue)
            {
                _placeStorage.Update(model);
            }
            else
            {
                _placeStorage.Insert(model);
            }
        }

        public void Delete(PlaceBindingModel model)
        {
            var element = _placeStorage.GetElement(new PlaceBindingModel { ID = model.ID });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _placeStorage.Delete(model);
        }
    }
}
