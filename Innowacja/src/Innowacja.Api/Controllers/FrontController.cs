using Innowacja.Application.DTO;
using Innowacja.Core.Entities;
using Innowacja.Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Drawing;

namespace Innowacja.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductShortagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductShortagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductShortages
        //zwraca wszystkie braki produktów
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrontDto>>> GetAll()
        {
            var result = await _context.ProductShortages
                .Select(ps => new FrontDto
                {
                    ShortageId = ps.ShortageId.ToString(),
                    ProductName = ps.ProductName,
                    ShelfUnit = ps.Shelf.ShelfUnit.ToString(),
                    ShelfNumber = ps.ShelfNumber.ToString(),
                    ProductNumber = ps.ProductNumber.ToString(),
                    FilePath = ps.FilePath
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/ProductShortages/{id}
        //zwraca brak produktu po ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FrontDto>> GetByIdWithGeneratedImage(int id)
        {
            var shortage = await _context.ProductShortages
                .Include(ps => ps.Shelf)
                .Where(ps => ps.ShortageId == id)
                .FirstOrDefaultAsync();

            if (shortage == null)
            {
                return NotFound($"Shortage with ID {id} was not found.");
            }

            string sourceFilePath = shortage.FilePath;

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
            if (!fileNameWithoutExtension.EndsWith($"_{id}"))
            {
                string outputFilePath = Path.Combine(
                    Path.GetDirectoryName(sourceFilePath),
                    $"{fileNameWithoutExtension}_{id}{Path.GetExtension(sourceFilePath)}"
                );

                DrawRectangle(sourceFilePath, (int)shortage.Xmin, (int)shortage.Xmax, (int)shortage.Ymin, (int)shortage.Ymax, outputFilePath);

                shortage.FilePath = outputFilePath;

                _context.ProductShortages.Update(shortage);
                await _context.SaveChangesAsync();
            }

            var result = new FrontDto
            {
                ShortageId = shortage.ShortageId.ToString(),
                ProductName = shortage.ProductName,
                ShelfUnit = shortage.Shelf.ShelfUnit.ToString(),
                ShelfNumber = shortage.ShelfNumber.ToString(),
                ProductNumber = shortage.ProductNumber.ToString(),
                FilePath = shortage.FilePath
            };


            return Ok(result);
        }

        private static void DrawRectangle(string filePath, int xmin, int xmax, int ymin, int ymax, string outputFilePath)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 6))
                    {
                        int width = xmax - xmin;
                        int height = ymax - ymin;
                        graphics.DrawRectangle(pen, xmin, ymin, width, height);
                    }
                }

                bitmap.Save(outputFilePath);
            }
        }

        // GET: api/ProductShortages/categories
        //zwraca wszystkie kategorie
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllCategories()
        {
            var categories = await _context.Departments
                .Select(d => new { d.DepartmentId, d.DepartmentName })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/ProductShortages/categories/{categoryId}/products
        //zwraca wszystkie braki produktów w danej kategori po ID 
        [HttpGet("categories/{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.ProductShortages
                .Include(ps => ps.Shelf)
                .Where(ps => ps.Shelf.DepartmentId == categoryId)
                .Select(ps => new
                {
                    ps.ShortageId,
                    ps.ProductName,
                    ps.Shelf.ShelfUnit,
                    ps.ShelfNumber,
                    ps.ProductNumber,
                    ps.FilePath
                })
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound($"No products found in category with ID {categoryId}.");
            }

            return Ok(products);
        }
    }
}