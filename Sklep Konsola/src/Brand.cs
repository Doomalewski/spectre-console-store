using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class Brand
    {
        private static int _idCounter = 1;

        public int BrandId { get; private set; }
        public string Name { get; set; }
        public int YearOfFoundation { get; set; }
        public string Description { get; set; }

        public Brand(string name, int yearOfFoundation, string description)
        {
            this.BrandId = _idCounter++;
            this.Name = name;
            this.YearOfFoundation = yearOfFoundation;
            this.Description = description;
        }
    }
}

