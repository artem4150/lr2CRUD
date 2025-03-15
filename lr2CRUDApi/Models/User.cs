using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Соль для хэширования пароля
        public string Salt { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь с аутфитами
        public ICollection<Outfit> Outfits { get; set; }
    }
}
