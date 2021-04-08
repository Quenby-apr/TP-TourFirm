namespace TourFirmDatabaseImplement.Models
{
    public class TourGuide
    {
        public int ID { get; set; }
        public int TourID { get; set; }
        public int GuideID { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Guide Guide { get; set; }
    }
}