using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TourFirmBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int ID { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

        [DisplayName("Место работы")]
        public string WorkPlace { get; set; }

        [DisplayName("Основной язык")]
        public string MainLanguage { get; set; }

        [DisplayName("Дополнительный язык")]
        public string AdditionalLanguage { get; set; }
        public Dictionary<int, string> GuideExcursions { get; set; }
        public int OperatorID { get; set; }
    }
}