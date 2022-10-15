using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoAPIClass.Models;

namespace TodoAPIClass
{
    public class TodoDBContext:IdentityDbContext<User>
    {
        public TodoDBContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }


        DbSet<Todo> Todos { get; set; }

    }
}
