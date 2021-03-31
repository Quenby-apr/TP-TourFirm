using System;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TouristLogic
    {
        private readonly ITouristStorage _touristStorage;

        public TouristLogic(ITouristStorage touristStorage)
        {
            _touristStorage = touristStorage;
        }

        public void CreateOrUpdate(TouristBindingModel model)
        {
            var element = _touristStorage.GetElement(new TouristBindingModel
            {
                Login = model.Login
            });

            if (element != null && element.ID != model.ID)
            {
                throw new Exception("Пользователь с таким логином уже зарегистрирован");
            }

            if (model.ID.HasValue)
            {
                _touristStorage.Update(model);
            }
            else
            {
                _touristStorage.Insert(model);
            }
        }

        public void Delete(TouristBindingModel model)
        {
            var element = _touristStorage.GetElement(new TouristBindingModel { ID = model.ID });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _touristStorage.Delete(model);
        }
    }
}
