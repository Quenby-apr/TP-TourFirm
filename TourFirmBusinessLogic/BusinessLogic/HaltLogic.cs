using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class HaltLogic
    {
        private readonly IHaltStorage _haltStorage;
        public HaltLogic(IHaltStorage haltStorage)
        {
            _haltStorage = haltStorage;
        }
        public List<HaltViewModel> Read(HaltBindingModel model)
        {
            if (model == null)
            {
                return _haltStorage.GetFullList();
            }
            if (model.ID.HasValue)
            {
                return new List<HaltViewModel> { _haltStorage.GetElement(model) };
            }
            return _haltStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(HaltBindingModel model)
        {
            var element = _haltStorage.GetElement(new HaltBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данная остановка уже зарегистрирована");
            }
            if (model.ID.HasValue)
            {
                _haltStorage.Update(model);
            }
            else
            {
                _haltStorage.Insert(model);
            }
        }
        public void Delete(HaltBindingModel model)
        {
            var element = _haltStorage.GetElement(new HaltBindingModel { ID = model.ID });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _haltStorage.Delete(model);
        }
    }
}
