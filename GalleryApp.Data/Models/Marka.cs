using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data.Models
{
    public class Marka : BaseModel
    {
        public string Name { get; set; }

        //1-M baglantiyi entity frameworkun anlamasi icin properity
        public virtual ICollection<Model> Models { get; set; }

        public Marka()
        {
            //ICollection tipindeki foregin key baglanti properitiylerini objje olarak olusturmak gerekiyor.
            Models = new HashSet<Model>();
        }

        public Marka Clone()
        {
            return new Marka { ID = this.ID, Name = this.Name };
        }
    }
}
