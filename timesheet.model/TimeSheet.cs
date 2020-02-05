using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.model
{
    public class TimeSheet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TimesheetID { get; set; }

        [StringLength(500)]
        public string Date { get; set; }

        public string Hours { get; set; }

        public virtual Days Day { get; set; }
        public virtual Task Task { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
