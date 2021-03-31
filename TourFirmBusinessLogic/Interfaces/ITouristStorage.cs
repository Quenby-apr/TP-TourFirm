using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    //Нуждается в переработке ??
    public interface ITouristStorage
    {
        List<TouristViewModel> GetFullList();
        /*        ?
                List<PlaceViewModel> GetFilteredList(TouristViewModel model);*/
        TouristViewModel GetElement(TouristBindingModel model);
        void Insert(TouristBindingModel model);
        void Update(TouristBindingModel model);
        void Delete(TouristBindingModel model);
    }
}
