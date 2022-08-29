using System.Collections.Generic;
using TodoAPIClass.Models;
using TodoAPIClass.ViewModel;

namespace TodoAPIClass.Repositories
{
    public interface ITodoInterface
    {
        List<Todo> GetTodos();
        Todo GetTodo(long Id);
        ResponseModel SaveTodo(TodoModel todo);
        ResponseModel DeleteTodo(long id);
        ResponseModel UpdateTodo(TodoModel todo);
    }
}
