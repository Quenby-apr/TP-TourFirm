using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface ITravelStorage
    {
        List<TravelViewModel> GetFullList();
        List<TravelViewModel> GetFilteredList(TravelBindingModel model);
        TravelViewModel GetElement(TravelBindingModel model);
        void Insert(TravelBindingModel model);
        void Update(TravelBindingModel model);
        void Delete(TravelBindingModel model);
    }
}