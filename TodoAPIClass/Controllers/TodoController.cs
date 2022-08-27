using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoAPIClass.MockData;
using TodoAPIClass.Models;
using TodoAPIClass.Repositories;

namespace TodoAPIClass.Controllers
{
    public class TodoController : ControllerBase
    {
        //CRUD
        //C-Create //POST method //httpverb - HTTPPOST
        //R- Read  //GET method //httpverb -HTTPGET
        //U -update //PATCH method, PUT method
        //D-delete //DELETE

        private readonly ITodoInterface _todoInterface;
        public TodoController(ITodoInterface todoInterface)
        {
            _todoInterface = todoInterface;
        }
        [HttpGet("todos")]
        public IActionResult GetTodos()
        {
            var todos = _todoInterface.GetTodos();
            if(todos == null)
            {
                return NotFound();
            }

            //returned success status, payload of what was requested
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public IActionResult Todo(long Id)
        {
            var todos = _todoInterface.GetTodo(Id);
            if (todos == null)
            {
                return NotFound();
            }
            return Ok(todos);
        }

        [HttpPost("createtodo")]
        public IActionResult SaveNewTodo([FromBody]Todo todo)
        {
            if (!ModelState.IsValid) { return BadRequest("Check to ensure your input follows the right pattern and data type"); }
            var create = _todoInterface.SaveTodo(todo);
            if (create.IsSuccess == true)
            {
                return Ok(create);
            }
            return BadRequest();
        }
    }
}
