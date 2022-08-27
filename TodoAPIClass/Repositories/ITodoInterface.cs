using System.Collections.Generic;
using TodoAPIClass.Models;

namespace TodoAPIClass.Repositories
{
    public interface ITodoInterface
    {
        List<Todo> GetTodos();
        Todo GetTodo(long Id);
        ResponseModel SaveTodo(Todo todo);
        ResponseModel DeleteTodo(long id);
        ResponseModel UpdateTodo(Todo todo);
    }
}
