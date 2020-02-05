using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using timesheet.model;
namespace timesheet.business
{
    public interface ITaskService
    {
        IQueryable<Task> GetTasksAsyncByID(int employeeID, int wk);
        IQueryable<Task> GetTasksAsync();
        bool EditTaskByObj(JArray castObj,int id);
        IQueryable<TimeSheet> GetTasksTimesheets(int wk);
    }
}
