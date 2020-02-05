using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timesheet.model;

namespace timesheet.business
{
    public interface IEmployeeService
    {
        IQueryable<Employee> GetEmployees();
        IQueryable<TaskEmployee> GetTaskEmployees();
       
    }
}
