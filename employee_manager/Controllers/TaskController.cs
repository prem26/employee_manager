using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.Extensions;

namespace employee_manager.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private IRepositoryWrapper _repository;

        public TaskController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/task
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                var tasks = _repository.Task.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetAllTasks action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/task/5
        [HttpGet("{id}", Name = "TaskById")]
        public IActionResult GetTaskById(Guid id)
        {
            try
            {
                var task = _repository.Task.GetTaskById(id);

                if (task.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(task);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetTaskById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/task/user/userId
        [HttpGet("user/{userId}", Name = "TasksByUserId")]
        public IActionResult GetTasksByUserId(string userId)
        {
            try
            {
                var tasks = _repository.Task.ListTasksByUserId(userId);
                return Ok(tasks);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetTasksByUserId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // POST api/task
        [HttpPost]
        public IActionResult CreateTask([FromBody]Entities.Models.Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Task.CreateTask(task);
                return CreatedAtRoute("TaskById", new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside CreateTask action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, [FromBody]Entities.Models.Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var dbTask = _repository.Task.GetTaskById(id);

                if (dbTask.IsEmptyObject())
                {
                    return NotFound();
                }

                task.Id = id;
                _repository.Task.UpdateTask(dbTask, task);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside UpdateTask action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
