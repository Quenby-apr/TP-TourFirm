﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TourFirmBusinessLogic.BindingModels
{
    public class ExcursionBindingModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int PlaceID { get; set; }
        public int TouristID { get; set; }
        public Dictionary<int, (string, string)> ExcursionGuides { get; set; }
    }
}