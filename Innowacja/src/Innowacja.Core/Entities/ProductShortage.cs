
namespace Innowacja.Core.Entities
{
    public class ProductShortage
    {
        public int ShortageId { get; set; } //id braku produktu
        public int shopShelfId { get; set; } //id regalu
        public string ProductName { get; set; } = null!; //nazwa produktu
        public int ProductNumber { get; set; } //numer produktu na półce od lewej strony
        public int ShelfNumber { get; set; } // numer półki od góry danego regału
        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
        public string FilePath { get; set; }  //sciezka do pliku

        public Shelf Shelf { get; set; } = null!;
    }

}
