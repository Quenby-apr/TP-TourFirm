using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IHaltStorage
    {
        List<HaltViewModel> GetFullList();
        List<HaltViewModel> GetFilteredList(HaltBindingModel model);
        HaltViewModel GetElement(HaltBindingModel model);
        void Insert(HaltBindingModel model);
        void Update(HaltBindingModel model);
        void Delete(HaltBindingModel model);
    }
}