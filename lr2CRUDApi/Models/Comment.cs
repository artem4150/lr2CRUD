using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Внешний ключ к аутфиту
        public int OutfitId { get; set; }
        public Outfit ? Outfit { get; set; }
    }
}
