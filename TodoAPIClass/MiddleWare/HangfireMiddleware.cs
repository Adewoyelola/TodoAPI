using Cronos;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;
using TodoAPIClass.Repositories;

namespace TodoAPIClass.MiddleWare
{
    public class HangfireMiddleware
    {
        private readonly RequestDelegate next;

        public HangfireMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, ITodoInterface _todointerface)
        {
            //var expression = CronExpression.Parse("* * * * *");
            //TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            //DateTimeOffset? next = expression.GetNextOccurrence(DateTimeOffset.UtcNow, easternTimeZone);
            try
            {
                Console.WriteLine("starting recurring job");
                RecurringJob.AddOrUpdate("getTodos", () => _todointerface.GetTodos(),
                   "0 30 5 L * ?");

                Console.WriteLine("ending recurring job");
            }
            catch(Exception x)
            {
                Log.Error(x, "An error occur at hangfire middleware");
            }
            
        }
    }
}
