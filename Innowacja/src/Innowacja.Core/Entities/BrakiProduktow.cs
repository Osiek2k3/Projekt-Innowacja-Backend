using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innowacja.Core.Entities
{
    public class BrakProduktow
    {
        public int IdBraku { get; set; }
        public int NumerPolki { get; set; }
        public int NumerProduktu { get; set; }
        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
        public string SciezkaDoPliku { get; set; }
    }
}
