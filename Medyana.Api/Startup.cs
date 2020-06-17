using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Medyana.BM;
using Medyana.Contract;
using Medyana.Model;

namespace Medyana.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            InjectRepositories(services);
            ConfigureDb(services);
            //services.AddDbContext<BloggingContext>(options => options.UseSqlite("Data Source=blog.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile(Configuration.GetSection("SeriLogNameFormat").GetValue<string>("FilePath"));
            //"Logs/myapp-{Date}.txt"

        }

        private void InjectRepositories(IServiceCollection services)
        {
            services.AddSingleton<IClinicRepository<Clinic>, ClinicRepository>();
            services.AddSingleton<IEquipmentRepository<Equipment>, EquipmentRepository>();
        }

        private void ConfigureDb(IServiceCollection services)
        {
            //services.AddDbContext<MedyanaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));
        }
    }
}
