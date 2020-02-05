using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class EmployeeService : IEmployeeService
    {
        public TimesheetDb db { get; }
        public EmployeeService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return this.db.Employees;
        }

        public IQueryable<TaskEmployee> GetTaskEmployees()
        {
            return this.db.TaskEmployees;
        }

       
    }
}
