using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Timesheet")]
    public class Timesheet : IEntity
    {
        [Key]
        [Column("TimesheetId")]
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public string StartTime { get; set; }

        public DateTime EndDate { get; set; }

        public string EndTime { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Task Id is required")]
        public string TaskId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
