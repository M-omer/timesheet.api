﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.model
{
    public class TaskEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public int? TaskID { get; set; }
        public virtual Task Task { get; set; }
    }
}
