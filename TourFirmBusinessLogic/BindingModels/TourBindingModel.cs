using System.Collections.Generic;

namespace TourFirmBusinessLogic.BindingModels
{
    public class TourBindingModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int OperatorID { get; set; }
        public int HaltID { get; set; }
        public Dictionary<int, (string, string)> TourGuides { get; set; }
    }
}
