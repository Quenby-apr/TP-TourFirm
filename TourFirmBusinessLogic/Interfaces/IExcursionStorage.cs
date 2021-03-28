using System;
using System.Collections.Generic;
using System.Text;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IExcursionStorage
    {
        List<ExcursionViewModel> GetFullList();
        List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model);
        ExcursionViewModel GetElement(ExcursionBindingModel model);
        void Insert(ExcursionBindingModel model);
        void Update(ExcursionBindingModel model);
        void Delete(ExcursionBindingModel model);
    }
}
