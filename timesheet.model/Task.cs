using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.model
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TaskID { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<TimeSheet> TimeSheets { get; set; }
        public ICollection<TaskEmployee> TaskEmployees { get; set; }
    }
}
