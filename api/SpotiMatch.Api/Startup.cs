using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SpotiMatch.Database;
using SpotiMatch.Database.Repositories;
using SpotiMatch.Database.Repositories.Interfaces;
using SpotiMatch.Logic.Mappings;
using SpotiMatch.Logic.Services;
using SpotiMatch.Logic.Services.Interfaces;

namespace SpotiMatch.Api
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
            // Mapper
            services.AddAutoMapper(
                typeof(EntitiesProfile),
                typeof(DtosProfile)
            );

            // Database context
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));
           
            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            // Services
            services.AddSingleton<IUserService, UserService>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
