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
    public class TimesheetController : Controller
    {
        private IRepositoryWrapper _repository;

        public TimesheetController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/timesheet
        [HttpGet]
        public IActionResult GetAllTimesheets()
        {
            try
            {
                var timesheets = _repository.Timesheet.GetAllTimesheets();
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetAllTimesheets action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/timesheet/5
        [HttpGet("{id}", Name = "TimesheetById")]
        public IActionResult GetTimesheetById(Guid id)
        {
            try
            {
                var timesheet = _repository.Timesheet.GetTimesheetById(id);

                if (timesheet.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(timesheet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetTimesheetById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/timesheet/user/{userId}/{startDate}/{endDate}
        [HttpGet("user/{id}/{startDate}/{endDate}", Name = "TimesheetByUserAndPeriod")]
        public IActionResult GetTimesheetByUserAndDates(string id, string startDate, string endDate)
        {
            try
            {
                var timesheet = _repository.Timesheet.ListTimesheetsByUserWithInTwoDates(id, startDate, endDate);
                return Ok(timesheet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetTimesheetByUserAndDates action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/timesheet
        [HttpPost]
        public IActionResult CreateTimesheet([FromBody]Timesheet timesheet)
        {
            try
            {
                if (timesheet == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Timesheet.CreateTimesheet(timesheet);
                return CreatedAtRoute("TimesheetById", new { id = timesheet.Id }, timesheet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside CreateTimesheet action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/timesheet/5
        [HttpPut("{id}")]
        public IActionResult UpdateTimesheet(Guid id, [FromBody]Timesheet timesheet)
        {
            try
            {
                if (timesheet == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var dbTimesheet = _repository.Timesheet.GetTimesheetById(id);

                if (dbTimesheet.IsEmptyObject())
                {
                    return NotFound();
                }

                timesheet.Id = id;
                _repository.Timesheet.UpdateTimesheet(dbTimesheet, timesheet);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside UpdateTimesheet action: {ex.Message}");
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
