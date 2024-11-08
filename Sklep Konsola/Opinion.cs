using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class Opinion
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public Opinion(string Name, string Description,int Rating) {
            this.Name = Name;
            this.Description = Description;
            this.Rating = Rating;
        }
    }
}
