using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace timesheet.model
{
   public class Days
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DayId { get; set; }

        [StringLength(255)]
        [Required]
        public string Day { get; set; }
    }
}
