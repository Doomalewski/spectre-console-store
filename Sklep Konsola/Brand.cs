using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class Brand
    {
        public string Name;
        public int YearOfFoundation;
        public string Description;
        public Brand(string Name, int YearOfFoundation, string Description) {
            this.Name = Name;
            this.YearOfFoundation = YearOfFoundation;
            this.Description = Description;
        }
    }
}
