using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyProject.Api.Models
{
    public class Outfit
    {
        public int OutfitId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User? User { get; set; } // Делает User опциональным
        public ICollection<Comment>? Comments { get; set; } // Делает Comments опциональными
    }

}
