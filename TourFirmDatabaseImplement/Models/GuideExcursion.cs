namespace TourFirmDatabaseImplement.Models
{
    public class GuideExcursion
    {
        public int ID { get; set; }
        public int ExcursionID { get; set; }
        public int GuideID { get; set; }
        public virtual Excursion Excursion { get; set; }
        public virtual Guide Guide { get; set; }
    }
}