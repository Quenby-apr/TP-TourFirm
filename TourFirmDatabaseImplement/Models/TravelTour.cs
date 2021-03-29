namespace TourFirmDatabaseImplement.Models
{
    public class TravelTour
    {
        public int ID { get; set; }
        public int TravelID { get; set; }
        public int TourID { get; set; }
        public virtual Travel Travel { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
