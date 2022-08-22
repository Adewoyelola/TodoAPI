using System.Collections.Generic;
using TodoAPIClass.Models;

namespace TodoAPIClass.MockData
{
    public class TodoModelMockData
    {
        public static IEnumerable<Todo> Todos()
        {
            var todos = new List<Todo>()
            {
                new Todo
                {
                    Id=1,
                    IsCompleted=true,
                    Name="SDLC",
                    Description="Software Development Life Cycle"
                },
                new Todo
                {
                    Id=2,
                    IsCompleted=true,
                    Name="Git",
                    Description="Introduction to Git and Github"
                },
                new Todo
                {
                    Id=3,
                    IsCompleted=true,
                    Name="Advance Git",
                    Description="Accessing git from command line"
                },
                new Todo
                {
                    Id=4,
                    Name="Introduction to ASP.NET",
                    Description="Building webapi with ASPNet",
                    IsCompleted=false
                },
                new Todo
                {
                    Id=5,
                    Name="ASPNET webapi",
                    Description="Adding Database to webapi",
                    IsCompleted=false
                    
                }
            };

            return todos;
        }
    }
}
