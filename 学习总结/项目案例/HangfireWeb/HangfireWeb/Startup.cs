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
             * ���ڶ��е�������(Fire-and-forget jobs)
             * ���ڶ��е���������Hangfire����õģ��ͻ���ʹ��BackgroundJob��ľ�̬����Enqueue�����ã�����ָ���ķ���������������������Job Queue�Ȳ���.
             * Enqueue ������ �������ִ�С�ִ�к�����
             * BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget"));
             * 
             * �ӳ�����ִ��(Delayed jobs)
             * �ӳ٣��ƻ�������������������ƣ��ͻ��˵���ʱ��Ҫָ����һ��ʱ��������ã�
             * Schedule������ �ӳ�ִ�С��ڶ������������ӳ�ʱ�䡣
             * BackgroundJob.Schedule(() => Console.WriteLine("Delayed"), TimeSpan.FromDays(1));
             * 
             * ��ʱ����ִ��(Recurring jobs)
             * ��ʱ��ѭ���������������ظ���ִ�ж�Σ�֧��CRON���ʽ��
             * AddOrUpdate�������ظ�ִ�С��ڶ�����������cronexpression���ʽ��
             * RecurringJob.AddOrUpdate(() => Console.Write("Recurring"), Cron.Daily);
             * 
             * ����������ִ��(Continuations)
             * ����������������.NET�е�Task,�����ڵ�һ������ִ����֮��������ٴ�ִ�����������
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
