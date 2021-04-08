using System.Collections.Generic;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.ViewModels;

namespace TourFirmBusinessLogic.Interfaces
{
    public interface IOperatorStorage
    {
        List<OperatorViewModel> GetFullList();
        OperatorViewModel GetElement(OperatorBindingModel model);
        void Insert(OperatorBindingModel model);
        void Update(OperatorBindingModel model);
        void Delete(OperatorBindingModel model);
    }
}
