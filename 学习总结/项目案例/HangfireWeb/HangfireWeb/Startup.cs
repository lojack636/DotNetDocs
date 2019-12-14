using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;

namespace HangfireWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage("Server=127.0.0.1;Database=Hangfire_DB;User ID=sa;Password=123456"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.UseRouting();
            /*
             * 基于队列的任务处理(Fire-and-forget jobs)
             * 基于队列的任务处理是Hangfire中最常用的，客户端使用BackgroundJob类的静态方法Enqueue来调用，传入指定的方法（或是匿名函数），Job Queue等参数.
             * Enqueue 方法： 放入队列执行。执行后销毁
             * BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget"));
             * 
             * 延迟任务执行(Delayed jobs)
             * 延迟（计划）任务跟队列任务相似，客户端调用时需要指定在一定时间间隔后调用：
             * Schedule方法： 延迟执行。第二个参数设置延迟时间。
             * BackgroundJob.Schedule(() => Console.WriteLine("Delayed"), TimeSpan.FromDays(1));
             * 
             * 定时任务执行(Recurring jobs)
             * 定时（循环）任务代表可以重复性执行多次，支持CRON表达式：
             * AddOrUpdate方法：重复执行。第二个参数设置cronexpression表达式。
             * RecurringJob.AddOrUpdate(() => Console.Write("Recurring"), Cron.Daily);
             * 
             * 延续性任务执行(Continuations)
             * 延续性任务类似于.NET中的Task,可以在第一个任务执行完之后紧接着再次执行另外的任务：
             * BackgroundJob.ContinueWith(jobId, () => Console.WriteLine("Continuation!"));
             * 
             */

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Minutely());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
