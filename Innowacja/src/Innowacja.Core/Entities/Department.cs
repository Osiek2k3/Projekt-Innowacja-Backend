

namespace Innowacja.Core.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}
