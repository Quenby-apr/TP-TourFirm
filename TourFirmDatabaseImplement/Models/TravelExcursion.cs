namespace TourFirmDatabaseImplement.Models
{
    public class TravelExcursion
    {
        public int ID { get; set; }
        public int TravelID { get; set; }
        public int ExcursionID { get; set; }
        public virtual Travel Travel { get; set; }
        public virtual Excursion Excursion { get; set; }
    }
}
