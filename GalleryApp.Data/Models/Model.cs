using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data.Models
{
    public class Model : BaseModel
    {
        public string Name { get; set; }

        public int MarkaID { get; set; }

        public virtual Marka Marka { get; set; }
        public virtual ICollection<ModelDetail> ModelDetails { get; set; }

        public Model()
        {
            ModelDetails = new HashSet<ModelDetail>();
        }

        public Model Clone()
        {
            return new Model { ID = this.ID, Name = this.Name, MarkaID = this.MarkaID };
        }
    }
}
