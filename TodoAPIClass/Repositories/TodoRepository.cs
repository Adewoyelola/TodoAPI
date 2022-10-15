using System;
using System.Collections.Generic;
using System.Linq;
using TodoAPIClass.Models;
using TodoAPIClass.ViewModel;

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
                todoExist.IsDeleted = true;
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
            var todo = _dbContext.Set<Todo>().Where(t=>t.IsDeleted==false).FirstOrDefault(x => x.Id == Id);
            if(todo != null)
            {
                return todo;
            }
            return null;
        }

        public List<Todo> GetTodos()
        {
            var todos = _dbContext.Set<Todo>().Where(t=>t.IsDeleted == false).ToList();

            Console.WriteLine(todos);
            return todos;
        }

        public ResponseModel SaveTodo(TodoModel todo)
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
                var createtodo = new Todo
                {
                    Description = todo.Description,
                    IsCompleted = todo.IsCompleted,
                    Name=todo.Name
                };
                _dbContext.Add(createtodo);
                _dbContext.SaveChanges();
                responseModel.IsSuccess = true;
            }
            return responseModel;
        }

        public ResponseModel UpdateTodo(TodoModel todo)
        {
            ResponseModel responseModel = new ResponseModel();
            var exists = _dbContext.Set<Todo>().Where(t => t.Id == todo.Id).FirstOrDefault();
            if(exists != null)
            {
                
                
                    exists.Description = todo.Description;
                    exists.IsCompleted = todo.IsCompleted;
                    exists.Name = todo.Name;                    

                    _dbContext.SaveChanges();
                    responseModel.IsSuccess = true;
            }
            else
            {
                responseModel.IsSuccess=false;
                responseModel.Message = "This todo does not exist or has been deleted";
            }

            return responseModel;
        }
    }
}
