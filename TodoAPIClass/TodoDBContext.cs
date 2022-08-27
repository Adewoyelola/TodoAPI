using Microsoft.EntityFrameworkCore;
using TodoAPIClass.Models;

namespace TodoAPIClass
{
    public class TodoDBContext:DbContext
    {
        public TodoDBContext(DbContextOptions options):base(options)
        {

        }
        DbSet<Todo> Todos { get; set; }


    }
}
