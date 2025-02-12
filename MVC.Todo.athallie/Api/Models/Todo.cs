using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class Todo
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;
    }
}
