using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoAPIClass.MockData;

namespace TodoAPIClass.Controllers
{
    public class TodoController : ControllerBase
    {
        //CRUD
        //C-Create //POST method //httpverb - HTTPPOST
        //R- Read  //GET method //httpverb -HTTPGET
        //U -update //PATCH method, PUT method
        //D-delete //DELETE
        [HttpGet("todos")]
        public IActionResult GetTodos()
        {
            var todos = TodoModelMockData.Todos();

            //returned success status, payload of what was requested
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public IActionResult GetSingleTodo(long Id)
        {
            var todo = TodoModelMockData.Todos().Where(t => t.Id == Id).FirstOrDefault();
            return Ok(todo);
        }
    }
}
