using System.Collections.Generic;

namespace TourFirmBusinessLogic.BindingModels
{
    public class ExcursionBindingModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
        public Dictionary<int,string> ExcursionGuides { get; set; }
    }
}
