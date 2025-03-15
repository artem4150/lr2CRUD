using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data;
using MyProject.Api.Models;

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutfitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OutfitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOutfits()
        {
            var outfits = await _context.Outfits.Include(o => o.Comments).ToListAsync();
            return Ok(outfits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOutfit(int id)
        {
            var outfit = await _context.Outfits.Include(o => o.Comments).FirstOrDefaultAsync(o => o.OutfitId == id);
            if (outfit == null)
                return NotFound();
            return Ok(outfit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOutfit([FromBody] Outfit outfit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            outfit.CreatedAt = DateTime.UtcNow;
            _context.Outfits.Add(outfit);
            await _context.SaveChangesAsync();
            return Ok(outfit);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOutfit(int id, [FromBody] Outfit updatedOutfit)
        {
            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit == null)
                return NotFound();

            outfit.Title = updatedOutfit.Title;
            outfit.Description = updatedOutfit.Description;
            outfit.ImageUrl = updatedOutfit.ImageUrl;
            await _context.SaveChangesAsync();
            return Ok(outfit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutfit(int id)
        {
            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit == null)
                return NotFound();
            _context.Outfits.Remove(outfit);
            await _context.SaveChangesAsync();
            return Ok("Аутфит удалён.");
        }
    }
}
