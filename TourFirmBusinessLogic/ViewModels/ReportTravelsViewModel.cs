using System;
using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportTravelsViewModel
    {
        [DisplayName("Путешествие")]
        public string TravelName { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Экскурсия")]
        public string ExcursionName { get; set; }

        [DisplayName("Фамилия гида")]
        public string GuideSurname { get; set; }

        [DisplayName("Имя гида")]
        public string GuideName { get; set; }
    }
}