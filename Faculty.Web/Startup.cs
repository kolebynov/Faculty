using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.EFCore.Services.Users;
using Faculty.Web.ApiResults;
using Microsoft.EntityFrameworkCore;
using Faculty.Web.Infrastructure;
using Faculty.Web.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.AspNetCore.Routing;

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
                    .UseInMemoryDatabase("db_inmemory");
            });

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FacultyContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return new JsonResult(ApiResult.ErrorResult(new[] {new ApiError {Message = "Unauthorized"}}))
                        .ExecuteResultAsync(new ActionContext(context.HttpContext, new RouteData(), new ActionDescriptor()));
                };
            });

            services.AddSingleton<IMapper, Mapper>();
            services.AddSingleton<IEntityExpressionsBuilder, EntityExpressionsBuilder>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddScoped<IUserService, UserService>();

            services.AddSession();

            services.AddMvc(config =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware();
            }

            app.UseSession();
            app.UseAuthentication();

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