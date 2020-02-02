using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data.ViewModels
{
    public class GridModel
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public int markaID { get; set; }
        public string MarkaName { get; set; }
        public int Year { get; set; }
        public string EngineName { get; set; }
        public string FuelTypeName { get; set; }
        public string GearBoxTypeName { get; set; }
        public string ColourName { get; set; }
        public string BodyTypeName { get; set; }

        public double Price { get; set; }

    }
}
