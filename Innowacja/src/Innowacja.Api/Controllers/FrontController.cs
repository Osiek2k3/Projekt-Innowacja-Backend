using Innowacja.Application.DTO;
using Innowacja.Core.Entities;
using Innowacja.Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace Innowacja.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrakiProduktowController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BrakiProduktowController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/BrakiProduktow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrontDto>>> GetAll()
        {
            var result = await _context.BrakiProduktow
                .Select(b => new FrontDto
                {
                    IdBraku = b.IdBraku.ToString(),
                    NumerPolki = b.NumerPolki.ToString(),
                    NumerProduktuNaPolce = b.NumerProduktu.ToString(),
                    SciezkaDoPliku = b.SciezkaDoPliku
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/BrakiProduktow/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FrontDto>> GetByIdWithGeneratedImage(int id)
        {
            var brak = await _context.BrakiProduktow
                .Where(b => b.IdBraku == id)
                .FirstOrDefaultAsync();

            if (brak == null)
            {
                return NotFound($"Brak z ID {id} nie został znaleziony.");
            }

            string sourceFilePath = brak.SciezkaDoPliku;

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
            if (!fileNameWithoutExtension.EndsWith($"_{id}"))
            {
                string outputFilePath = Path.Combine(
                    Path.GetDirectoryName(sourceFilePath),
                    $"{fileNameWithoutExtension}_{id}{Path.GetExtension(sourceFilePath)}"
                );

                
                DrawRectangle(sourceFilePath, (int)brak.Xmin, (int)brak.Xmax, (int)brak.Ymin, (int)brak.Ymax, outputFilePath);

                brak.SciezkaDoPliku = outputFilePath;

                _context.BrakiProduktow.Update(brak);
                await _context.SaveChangesAsync();
            }

            var result = new FrontDto
            {
                IdBraku = brak.IdBraku.ToString(),
                NumerPolki = brak.NumerPolki.ToString(),
                NumerProduktuNaPolce = brak.NumerProduktu.ToString(),
                SciezkaDoPliku = brak.SciezkaDoPliku
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
    }

    

}
