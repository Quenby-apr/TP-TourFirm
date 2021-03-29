using System.Collections.Generic;
using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int ID { get; set; }
        [DisplayName("Название")]  public string Name { get; set; }
        [DisplayName("Страна")]  public string Country { get; set; }
        [DisplayName("Цена")]  public decimal Price { get; set; }
        public int OperatorID { get; set; }
        public int HaltID { get; set; }
        public Dictionary<int, string> TourGuides { get; set; }
    }
}
