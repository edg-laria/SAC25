using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidad.Models
{
    public class TreeViewModel
    {
        public string text { get; set; }
        public string href { get; set; }
        public string tags { get; set; }
        public ICollection<TreeViewModel> nodes { get; set; }
       
    }

}
