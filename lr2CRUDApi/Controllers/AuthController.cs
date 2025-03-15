using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data;
using MyProject.Api.Models;
using MyProject.Api.Services;

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Пользователь с таким Email уже существует.");

            if (model.Password.Length < 6)
                return BadRequest("Пароль слишком простой.");

            var salt = PasswordHelper.GenerateSalt();
            var hash = PasswordHelper.ComputeHash(model.Password, salt);

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Salt = salt,
                PasswordHash = hash,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Регистрация прошла успешно.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Unauthorized("Неверные данные для входа.");

            var hash = PasswordHelper.ComputeHash(model.Password, user.Salt);
            if (hash != user.PasswordHash)
                return Unauthorized("Неверные данные для входа.");

            // Здесь можно сгенерировать JWT-токен
            return Ok(new { Message = "Вход выполнен успешно", UserId = user.UserId, Username = user.Username });
        }
    }
}
