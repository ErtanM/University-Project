using GalleryApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data.Models
{
    public class ModelDetail : BaseModel
    {
        public int ModelID { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public GearBoxTypeEnum GearBoxType { get; set; }
        public ColourEnum Colour { get; set; }
        public BodyTypeEnum BodyType { get; set; }
        public StatusEnum Status { get; set; }
        public double Price { get; set; }
        public virtual Model Model { get; set; }
    }
}
