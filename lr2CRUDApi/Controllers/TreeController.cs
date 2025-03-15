using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data;
using MyProject.Api.Models;

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TreeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Загрузка корневых узлов – пользователей
        [HttpGet("root")]
        public async Task<IActionResult> GetRootNodes()
        {
            var users = await _context.Users.ToListAsync();
            var treeNodes = users.Select(u => new TreeNodeDto
            {
                Id = u.UserId,
                Name = u.Username,
                NodeType = "User",
                HasChildren = _context.Outfits.Any(o => o.UserId == u.UserId)
            }).ToList();
            return Ok(treeNodes);
        }

        // Ленивое получение дочерних узлов: для User – Outfits, для Outfit – Comments
        [HttpGet("{nodeType}/{id}/children")]
        public async Task<IActionResult> GetChildren(string nodeType, int id)
        {
            List<TreeNodeDto> children = new List<TreeNodeDto>();

            if (nodeType.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                var outfits = await _context.Outfits.Where(o => o.UserId == id).ToListAsync();
                children = outfits.Select(o => new TreeNodeDto
                {
                    Id = o.OutfitId,
                    Name = o.Title,
                    NodeType = "Outfit",
                    HasChildren = _context.Comments.Any(c => c.OutfitId == o.OutfitId)
                }).ToList();
            }
            else if (nodeType.Equals("Outfit", StringComparison.OrdinalIgnoreCase))
            {
                var comments = await _context.Comments.Where(c => c.OutfitId == id).ToListAsync();
                children = comments.Select(c => new TreeNodeDto
                {
                    Id = c.CommentId,
                    Name = c.Text,
                    NodeType = "Comment",
                    HasChildren = false
                }).ToList();
            }

            return Ok(children);
        }

        // Обновление родительской связи (drag & drop)
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNode([FromBody] UpdateNodeDto dto)
        {
            if (dto.NodeType.Equals("Outfit", StringComparison.OrdinalIgnoreCase))
            {
                var outfit = await _context.Outfits.FindAsync(dto.NodeId);
                if (outfit == null)
                    return NotFound();
                outfit.UserId = dto.NewParentId;
            }
            else if (dto.NodeType.Equals("Comment", StringComparison.OrdinalIgnoreCase))
            {
                var comment = await _context.Comments.FindAsync(dto.NodeId);
                if (comment == null)
                    return NotFound();
                comment.OutfitId = dto.NewParentId;
            }
            else
            {
                return BadRequest("Неверный тип узла для обновления.");
            }

            await _context.SaveChangesAsync();
            return Ok("Узел обновлён.");
        }
    }
}
