using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface ITourStorage
    {
        List<TourViewModel> GetFullList();
        List<TourViewModel> GetFilteredList(TourBindingModel model);
        TourViewModel GetElement(TourBindingModel model);
        void Insert(TourBindingModel model);
        void Update(TourBindingModel model);
        void Delete(TourBindingModel model);
    }
}
