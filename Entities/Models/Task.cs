using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Task")]
    public class Task : IEntity
    {
        [Key]
        [Column("TaskId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public int EstimatedTime { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
