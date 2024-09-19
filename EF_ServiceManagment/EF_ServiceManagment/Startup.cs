using EF_ServiceManagement.BLL.Interfaces.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EF_ServcieManagement.DAL;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServcieManagement.DAL.Interfaces;
using FluentValidation.AspNetCore;
using EF_ServcieManagement.DAL.Data.Repositories;
using EF_ServcieManagement.DAL.Data;
using EF_ServiceManagement.BLL.Services;
using EF_ServiceManagment.WEBAPI.ExceptionHandling;
using EF_ServiceManagement.BLL.Validations;
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.DTO.Service;
using EF_ServiceManagement.BLL.DTO.Tag;
using EF_ServiceManagment.WEBAPI.Filters;
using EF_ServcieManagement.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddDbContext<ServiceManagementContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<ITagRepository, TagRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IServiceManager, ServiceManager>();
            services.AddTransient<ITagService, TagService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<CategoryRequest>, CategoryRequestValidator>();
            services.AddScoped<IValidator<ServiceRequest>, ServiceRequestValidator>();
            services.AddScoped<IValidator<TagRequest>, TagRequestValidator>();

            services.Configure<ApiBehaviorOptions>(options
                        => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<ValidationFilter>();

            services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ServiceManagement.API",
                    Version = "v1"
                });
            });

            services.AddProblemDetails();

            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<EntityNotFoundExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceManagement.API");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseExceptionHandler();
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
