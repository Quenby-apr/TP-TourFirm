using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmBusinessLogic.BindingModels
{
    public class GuideBindingModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkPlace { get; set; }
        public string MainLanguage { get; set; }
        public string AdditionalLanguage { get; set; }
        public int OperatorID { get; set; }
    }
}
