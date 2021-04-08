using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TravelLogic
    {
        private readonly ITravelStorage _travelStorage;

        public TravelLogic(ITravelStorage travelStorage)
        {
            _travelStorage = travelStorage;
        }

        public List<TravelViewModel> Read(TravelBindingModel model)
        {
            if (model == null)
            {
                return _travelStorage.GetFullList();
            }

            if (model.ID.HasValue)
            {
                return new List<TravelViewModel> { _travelStorage.GetElement(model) };
            }
            return _travelStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TravelBindingModel model)
        {
            var element = _travelStorage.GetElement(new TravelBindingModel
            {
                Name = model.Name
            });

            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Уже есть путешествие с таким названием");
            }

            if (model.ID.HasValue)
            {
                _travelStorage.Update(model);
            }
            else
            {
                _travelStorage.Insert(model);
            }
        }

        public void Delete(TravelBindingModel model)
        {
            var element = _travelStorage.GetElement(new TravelBindingModel { ID = model.ID });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _travelStorage.Delete(model);
        }
    }
}