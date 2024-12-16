
namespace Innowacja.Core.Entities
{
    public class ProductShortage
    {
        public int ShortageId { get; set; }
        public int shopShelfId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductNumber { get; set; }
        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
        public string FilePath { get; set; }

        public Shelf Shelf { get; set; } = null!;
    }

}
