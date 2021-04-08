namespace TourFirmBusinessLogic.BindingModels
{
    public class PlaceBindingModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int TouristID { get; set; }
    }
}