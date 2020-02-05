using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using timesheet.business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace timesheet.api.controllers
{
    [Route("api/v1/Tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("GetTaskByID")]
        public IActionResult GetTaskByID(int id,int wk)
        {
            var items = _taskService.GetTasksAsyncByID(id,wk);
          
            var toJson =JsonConvert.SerializeObject(items,Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return new ObjectResult(toJson);
        }

        [HttpGet("GetSheetByWeek")]
        public IActionResult GetSheetByWeek(int wk)
        {
            var items = _taskService.GetTasksTimesheets(wk);

            var toJson = JsonConvert.SerializeObject(items, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return new ObjectResult(toJson);
        }
        [HttpGet("GetTasks")]
        public IActionResult GetTasks()
        {
            var items = _taskService.GetTasksAsync();
            return new ObjectResult(items);
        }
        [HttpPost("EditTasks")]
        public IActionResult EditTasks(object obj,int id)
        {
            var CastObj = (JArray)obj;
            var items = _taskService.EditTaskByObj(CastObj,id);
            return Ok();
        }
    }
}
