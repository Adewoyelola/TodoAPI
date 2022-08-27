using System.Collections.Generic;
using System.Linq;
using TodoAPIClass.Models;

namespace TodoAPIClass.Repositories
{
    public class TodoRepository : ITodoInterface
    {
        private TodoDBContext _dbContext;
        public TodoRepository(TodoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResponseModel DeleteTodo(long id)
        {
            ResponseModel responseModel = new ResponseModel();
            var todoExist = GetTodo(id);
            if(todoExist != null)
            {
                _dbContext.Remove(todoExist);
                _dbContext.SaveChanges();

                responseModel.IsSuccess = true;
                responseModel.Message = "Todo has been deleted successfully";
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Todo with this id doesnt exist or not found";
            }
            return responseModel;   
        }

        public Todo GetTodo(long Id)
        {
            var todo = _dbContext.Set<Todo>().FirstOrDefault(x => x.Id == Id);
            return todo;
        }

        public List<Todo> GetTodos()
        {
            var todos = _dbContext.Set<Todo>().ToList();

            return todos;
        }

        public ResponseModel SaveTodo(Todo todo)
        {
            ResponseModel responseModel= new ResponseModel();
            var exists= _dbContext.Set<Todo>()
                .Where(t=>t.Name==todo.Name &&
                t.Description==todo.Description &&
                t.IsCompleted==todo.IsCompleted).ToList();

            if (exists.Count > 0)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "There is an item in the database that matches your entries";
            }
            else
            {
                _dbContext.Add(todo);
                _dbContext.SaveChanges();
                responseModel.IsSuccess = true;
            }
            return responseModel;
        }

        public ResponseModel UpdateTodo(Todo todo)
        {
            throw new System.NotImplementedException();
        }
    }
}
