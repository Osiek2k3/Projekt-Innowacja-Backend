
namespace Innowacja.Application.DTO
{
    public class FrontDto
    {
        public string ShortageId { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public string? ShelfUnit { get; set; }
        public string? ShelfNumber { get; set; } 
        public string? ProductNumber { get; set; }
        public string? FilePath { get; set; }
    }
}
