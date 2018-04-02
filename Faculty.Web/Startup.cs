using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Faculty.EFCore.Data;
using Faculty.EFCore.Infrastucture;
using Microsoft.EntityFrameworkCore;
using Faculty.Web.Infrastructure;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Faculty.Web
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
            services.AddDbContext<FacultyContext>(opt =>
            {
                opt
                    //.UseSqlServer(Configuration.GetConnectionString("Main"))
                    .UseInMemoryDatabase("db");
            });
            services.AddSingleton<IMapper, Mapper>();
            services.AddSingleton<IEntityExpressionsBuilder, EntityExpressionsBuilder>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware();
            }

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spa", new
                {
                    controller = "Home",
                    action = "Index"
                });
            });
        }
    }
}
