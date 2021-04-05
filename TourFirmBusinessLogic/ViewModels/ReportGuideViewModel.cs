using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TourFirmBusinessLogic.ViewModels
{
    public class ReportGuideViewModel
    {
        [DisplayName("Начало путешествия")] public DateTime DateStartTravel { get; set; }
        [DisplayName("Фамилия гида")] public string GuideSurname { get; set; }
        [DisplayName("Имя гида")] public string GuideName { get; set; }
        [DisplayName("Город работы")] public string GuideWorkPlace { get; set; }
        [DisplayName("Название экскурсии")] public string ExcursionName { get; set; }
        [DisplayName("Название тура")] public string TourName { get; set; }
    }
}
