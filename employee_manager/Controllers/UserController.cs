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
    public class UserController : Controller
    {
        private IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        // GET: api/user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/user/5
        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

                if (user.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/user
        [HttpPost]
        public IActionResult CreateUser([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.User.CreateUser(user);
                return CreatedAtRoute("UserById", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/user/authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            try
            {
                Console.WriteLine(Convert.ToBase64String(user.Password));
                Console.WriteLine(user.Email);
                var u = _repository.User.Authenticate(user.Email, Convert.ToBase64String(user.Password));

                if (u == null)
                    return Unauthorized();

                return Ok(u);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside Authenticate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var dbUser = _repository.User.GetUserById(id);

                if (dbUser.IsEmptyObject())
                {
                    return NotFound();
                }

                user.Id = id;
                _repository.User.UpdateUser(dbUser, user);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error"); 
            }
            
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
