using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Infrastructure.Mapper;
using GS.Data;
using GS.NewAPI.Infrastructure;
using GS.NewAPI.Infrastructure.Mapper;
using GS.NewAPI.Middleware;
using GS.Services;
using GS.Services.Common;
using GS.Services.HeThong;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace GS.NewAPI
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GSObjectContext>(opt =>
            {
                opt.UseOracle(_configuration.GetSection("DataConnectionString").Value, oracleOptionsAction => oracleOptionsAction.CommandTimeout(600));
            });
            //add Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            //config depency inject 
            services.AddScoped<IStaticCacheManager, MemoryCacheManager>();
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IDataProvider, OracleDataProvider>();
            services.AddScoped<IHoatDongService, HoatDongServices>();
            services.AddScoped<IWebHelper, WebHelper>();
            services.AddScoped<IGSAPIService, GSAPIService>();
         /*   services.AddTransient<IValidator<TaiSanModel>, TaiSanValidator>(); */// Example registration for TaiSanModel validator

            // register IHttpContextAccessor and HttpContextAccessor with type TryAddSingleton
            services.AddHttpContextAccessor();
            //auto add scoped service and repository 
            Extensions.RegisterAssemblyServices(services);
            //Add auto mapper         
            AddAutoMapper();
            
            // add swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddHealthChecks();
            services.AddMvc();

            // return type IServiceProvider  Autofac
            return RegisterDependencies(services);
        }

        private IServiceProvider RegisterDependencies(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            //populate Autofac container builder with the set of registered service descriptors
            containerBuilder.Populate(services);

            DependencyRegistrar.Register(containerBuilder);
            //create service provider
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        private void AddAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AdminMapperConfiguration>(); // Ensure your profile is added here
            });
            //register automap
            AutoMapperConfiguration.Init(configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection(); // Điều hướng HTTP thành HTTPS nếu cần
            app.UseStaticFiles(); // Nếu có tệp tĩnh, bạn có thể sử dụng

            // Thêm UseMvc để cấu hình các API
            app.UseMvc();
        }


    }
}
