using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Security;
using GS.Data;
using GS.Services;
using GS.Services.DanhMuc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GS.NewAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //var iSvc = services.ConfigureApplicationServices(Configuration);
            services.AddDbContext<GSObjectContext>(opt =>
            {
                opt.UseOracle("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.6)(PORT=1521)))(CONNECT_DATA=(SID=gs19c)));User ID=QLDKTS_CORE;Password=GS_QLDKTS_51", oracleOptionsAction => oracleOptionsAction.CommandTimeout(600));
            });
            //soat service
            services.AddCors();
            //config depency inject 
            services.AddScoped<GS.Core.Domain.CauHinh.CauHinhNguoiDung>();
            services.AddSingleton<IDbContext, GSObjectContext>();
            services.AddScoped<IDonViService, DonViService>();
            services.AddScoped<IStaticCacheManager, MemoryCacheManager>();
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IDataProvider, SqlServerDataProvider>();
            services.AddScoped<IDataProvider, OracleDataProvider>();
            services.AddSingleton<SecuritySettings>();
            services.AddSingleton<CauHinhChung>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //auto add scoped service and repository 
            Extensions.RegisterAssemblyServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return services.BuildServiceProvider();
        }
        //code cũ
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
