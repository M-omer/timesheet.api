using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class TaskService : ITaskService
    {
        public TimesheetDb db { get; }
        public TaskService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public IQueryable<Task> GetTasksAsync()
        {
            return this.db.Tasks;
        }

        public IQueryable<TimeSheet> GetTasksTimesheets(int wk)
        {
            return this.db.TimeSheets.Include(e=>e.Employee).Include(t =>t.Task).Include(d=>d.Day).Where(d => ToIntCoverter(d.Date) == wk);
        }

        public IQueryable<Task> GetTasksAsyncByID(int employeeID,int wk)
        {
            List<Task> tasks = new List<Task>();
            var te = db.TaskEmployees.Where(x => x.Employee.EmployeeID == employeeID);
            if (te != null)
            {
                foreach (var item in te)
                {
                    var tsList = db.Tasks.Include(x => x.TimeSheets)
                                        .ThenInclude(d => ((TimeSheet)d).Day)
                                      .Where(x => x.TaskID == item.TaskID);

                  Task ts = tsList.Where(t => t.TimeSheets.Where(d => ToIntCoverter(d.Date) == wk).Count() != 0).FirstOrDefault();

                    if (ts != null)
                    {
                        tasks.Add(ts);
                    }
                    
                }
            }

            return tasks.AsQueryable();
        }

        public int ToIntCoverter(string Date)
        {
            DateTime oDate = Convert.ToDateTime(Date);
            int Weeknum = 1 + oDate.DayOfYear / 7;
            return Weeknum;
        }
        public bool EditTaskByObj(JArray castObj, int id)
        {
            var items = castObj.ToObject<List<ItemGridSource>>();
            var emp = db.Employees.Where(i => i.EmployeeID == id).First();

            foreach (var item in items)
            {

                var ToItemDic = item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(item, null));
                ToItemDic.Remove("Id");
                ToItemDic.Remove("Name");


                foreach (var day in ToItemDic)
                {
                    if (day.Value != null)
                    {
                        var daynum = ((int)Enum.Parse(typeof(DayOfWeek), day.Key)).ToString();
                        var Count = db.TimeSheets.Where(e => e.Employee.EmployeeID == id && e.Task.TaskID == item.Id && e.Day.Day == daynum).Count();
                        if (Count != 0)
                        {
                            try
                            {
                                var tasksheets = db.TimeSheets.Include(d => d.Day).Where(e => e.Employee.EmployeeID == id && e.Task.TaskID == item.Id);

                                var sheets = tasksheets.Where(e => e.Day.Day == daynum).FirstOrDefault();
                                sheets.Hours = day.Value.ToString();

                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }
                        else
                        {
                            var task = db.Tasks.Include(t => t.TimeSheets).ThenInclude(d => ((TimeSheet)d).Day).Where(i => i.TaskID == item.Id).First();

                            var dayObj = db.Days.Where(d => d.Day == day.Key).FirstOrDefault();
                            if (dayObj == null)
                            {
                                dayObj = new Days
                                {
                                    Day = ((int)Enum.Parse(typeof(DayOfWeek), day.Key)).ToString()
                                };
                            }
                            TimeSheet timeSheet = new TimeSheet
                            {
                                Day = dayObj,
                                Hours = day.Value.ToString(),
                                Employee = emp,
                                Date = DateTime.Today.ToString()

                            };
                            task.TimeSheets.Add(timeSheet);
                            db.SaveChanges();

                            var taskEmp = db.TaskEmployees.Where(t => t.TaskID == task.TaskID && t.EmployeeID == emp.EmployeeID).FirstOrDefault();
                            if (taskEmp == null)
                            {
                                taskEmp = new TaskEmployee
                                {
                                    Employee = emp,
                                    Task = task
                                };
                                db.TaskEmployees.Add(taskEmp);
                                db.SaveChanges();

                            }
                        }
                    }
                    

                }



            }

            return true;
        }
    }
}
