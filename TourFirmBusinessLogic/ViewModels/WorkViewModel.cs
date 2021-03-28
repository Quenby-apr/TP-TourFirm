using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TourFirmBusinessLogic.ViewModels
{
    public class WorkViewModel
    {
        public int? ID { get; set; }
        [DisplayName("Имя")]  public string GuideName { get; set; }
        [DisplayName("Фамилия")] public string GuideSurname { get; set; }
        [DisplayName("Номер телефона")]  public string PhoneNumber { get; set; }
        [DisplayName("Основной язык")] public string MainLanguage { get; set; }
        [DisplayName("Дополнительный язык")] public string AdditionalLanguage { get; set; }
        [DisplayName("Название тура")] public string TourName { get; set; }
        [DisplayName("Названия экскурсии")]  public string ExcursionName { get; set; }
        [DisplayName("Начало экскурсии")] public DateTime DateStart { get; set; }
        [DisplayName("Окончание экскурсии")] public DateTime DateEnd { get; set; }
    }
}
