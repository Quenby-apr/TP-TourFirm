using System.Collections.Generic;
using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ExcursionViewModel
    {
        public int ID { get; set; }
        [DisplayName("Название")] public string Name { get; set; }
        [DisplayName("Стоимость")] public decimal Price { get; set; }
        [DisplayName("Продолжительность")] public int Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
        public Dictionary<int, string> ExcursionGuides { get; set; }
    }
}
