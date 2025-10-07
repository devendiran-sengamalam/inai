using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inai.Core.Models
{
    public class Reminder
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid TaskItemId { get; set; }

        [ForeignKey(nameof(TaskItemId))]
        public TaskItem? TaskItem { get; set; }

        [Required]
        public DateTime RemindAt { get; set; }

        public bool IsSent { get; set; } = false;

        public string? Message { get; set; } // Optional, can store reminder text
    }
}
