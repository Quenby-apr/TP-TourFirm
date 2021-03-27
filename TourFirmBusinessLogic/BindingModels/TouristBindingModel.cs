using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmBusinessLogic.BindingModels
{
    public class TouristBindingModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
