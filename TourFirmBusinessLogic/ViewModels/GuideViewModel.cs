using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TourFirmBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int ID { get; set; }
        [DisplayName("Имя")] public string Name { get; set; }
        [DisplayName("Фамилия")] public string Surname { get; set; }
        [DisplayName("Номер телефона")] public string PhoneNumber { get; set; }
        [DisplayName("Место работы")]  public string WorkPlace { get; set; }
        [DisplayName("Основной язык")]  public string MainLanguage { get; set; }
        [DisplayName("Дополнительный язык")]  public string AdditionalLanguage { get; set; }
        [DisplayName("Дата экскурсии")] public DateTime DateWork { get; set; }
        public int OperatorID { get; set; }
    }
}
