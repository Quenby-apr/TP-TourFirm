using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class HaltViewModel
    {
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }
        public int OperatorID { get; set; }
    }
}