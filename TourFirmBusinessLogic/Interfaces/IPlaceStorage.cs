using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IPlaceStorage
    {
        List<PlaceViewModel> GetFullList();
        List<PlaceViewModel> GetFilteredList(PlaceBindingModel model);
        PlaceViewModel GetElement(PlaceBindingModel model);
        void Insert(PlaceBindingModel model);
        void Update(PlaceBindingModel model);
        void Delete(PlaceBindingModel model);
    }
}
