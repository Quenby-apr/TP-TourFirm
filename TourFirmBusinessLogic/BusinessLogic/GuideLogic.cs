using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class GuideLogic
    {
        private readonly IGuideStorage _guideStorage;

        public GuideLogic(IGuideStorage guideStorage)
        {
            _guideStorage = guideStorage;
        }

        public List<GuideViewModel> Read(GuideBindingModel model)
        {
            if (model == null)
            {
                return _guideStorage.GetFullList();
            }
            if (model.ID.HasValue || model.Surname != null)
            {
                return new List<GuideViewModel> { _guideStorage.GetElement(model) };
            }
            return _guideStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(GuideBindingModel model)
        {
            var element = _guideStorage.GetElement(new GuideBindingModel
            {
                Name = model.Name,
                Surname=model.Surname,
                PhoneNumber=model.PhoneNumber
            });
            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Данный гид уже зарегистрирован");
            }
            if (model.ID.HasValue)
            {
                _guideStorage.Update(model);
            }
            else
            {
                _guideStorage.Insert(model);
            }
        }
        public void Delete(GuideBindingModel model)
        {
            var element = _guideStorage.GetElement(new GuideBindingModel { ID = model.ID });
            if (element == null) 
            {
                throw new Exception("Элемент не найден"); 
            }
            _guideStorage.Delete(model);
        }
    }
}
