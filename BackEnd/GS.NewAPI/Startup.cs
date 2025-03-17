using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GS.Core.Caching;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Security;
using GS.Core.Infrastructure.DependencyManagement;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.Mapper;
using GS.Data;
using GS.NewAPI.Factories;
using GS.NewAPI.Infrastructure;
using GS.NewAPI.Infrastructure.Mapper;
using GS.Services;
using GS.Services.DanhMuc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using GS.Services.Authentication;

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
            //soat service
            services.AddCors();
            //config depency inject 
            services.AddScoped<GS.Core.Domain.CauHinh.CauHinhNguoiDung>();
            services.AddScoped<IStaticCacheManager, MemoryCacheManager>();
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IDataProvider, SqlServerDataProvider>();
            services.AddScoped<IDataProvider, OracleDataProvider>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticationService, CookieAuthenticationService>();
            services.AddSingleton<SecuritySettings>();
            services.AddSingleton<CauHinhChung>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //auto add scoped service and repository 
            Extensions.RegisterAssemblyServices(services);
            //Add auto mapper         
            AddAutoMapper();
            // fix erorr devexpress hidden swagger
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager(x => {
                    var parts = x.ApplicationParts;
                    var aspNetCoreReportingAssemblyName = typeof(DevExpress.AspNetCore.Reporting.WebDocumentViewer.WebDocumentViewerController).Assembly.GetName().Name;
                    var reportingPart = parts.FirstOrDefault(part => part.Name == aspNetCoreReportingAssemblyName);
                    if (reportingPart != null)
                    {
                        parts.Remove(reportingPart);
                    }
            });
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
            // return type IServiceProvider  Autofac
            return RegisterDependencies(services);
            //return services.BuildServiceProvider();
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
        //code cũ
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        private void AddAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AdminMapperConfiguration>(); // Ensure your profile is added here
            });
            //register
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

            app.UseHttpsRedirection(); // Điều hướng HTTP thành HTTPS nếu cần
            app.UseStaticFiles(); // Nếu có tệp tĩnh, bạn có thể sử dụng

            // Thêm UseMvc để cấu hình các API
            app.UseMvc(routes =>
            {
                // Cấu hình tuyến đường mặc định cho API
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }


    }
}
