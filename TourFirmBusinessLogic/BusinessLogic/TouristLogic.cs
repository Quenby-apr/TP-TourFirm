using System;
using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.BusinessLogic
{
    public class TouristLogic
    {
        private readonly ITouristStorage _touristStorage;

        public TouristLogic(ITouristStorage touristStorage)
        {
            _touristStorage = touristStorage;
        }

        public List<TouristViewModel> Read(TouristBindingModel model)
        {
            if (model == null)
            {
                return _touristStorage.GetFullList();
            }

            if (model.ID.HasValue)
            {
                return new List<TouristViewModel> { _touristStorage.GetElement(model) };
            }
            return null;
        }

        public void CreateOrUpdate(TouristBindingModel model)
        {
            var elementByLogin = _touristStorage.GetElement(new TouristBindingModel
            {
                Login = model.Login
            });
            if (elementByLogin != null && elementByLogin.ID != model.ID)
            {
                throw new Exception("Данный логин уже занят");
            }

            var elementByMail = _touristStorage.GetElement(new TouristBindingModel
            {
                Mail = model.Mail
            });
            if (elementByMail != null && elementByMail.ID != model.ID)
            {
                throw new Exception("Данный электронная почта уже занята");
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