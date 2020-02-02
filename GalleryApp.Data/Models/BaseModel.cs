using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data.Models
{
    public class BaseModel
    {
        [Key]
        
        public int ID { get; set; }
    }
}
