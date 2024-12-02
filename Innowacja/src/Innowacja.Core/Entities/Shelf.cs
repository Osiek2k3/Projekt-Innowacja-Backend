
namespace Innowacja.Core.Entities
{
    public class Shelf
    {
        public int shopShelfId { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        public ICollection<ProductShortage> ProductShortages { get; set; } = new List<ProductShortage>();
    }
}
