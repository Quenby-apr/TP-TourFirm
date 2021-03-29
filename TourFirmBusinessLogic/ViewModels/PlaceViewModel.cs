using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class PlaceViewModel
    {
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Тип")]
        public string Type { get; set; }

        public int TouristID { get; set; }
    }
}
