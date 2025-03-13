using GS.Web.Framework.Infrastructure.Extensions;
using GS.WebApi.SoapServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using SoapCore;
using System;
using System.ServiceModel;

namespace GS.WebApi
{
    public class Startup
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion Fields

        #region Ctor

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            LogManager.LoadConfiguration(System.String.Concat(hostingEnvironment.ContentRootPath, "/nlog.config"));
        }

        #endregion Ctor

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var iSvc = services.ConfigureApplicationServices(_configuration);
            //soat service
            services.AddSoapCore();
            services.AddScoped<IDMDC_DanhMucSvc, DMDC_DanhMucSvc>();
            services.AddCors();
            return iSvc;
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            application.ConfigureRequestPipeline();
            application.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            application.UseSoapEndpoint<IDanhMucSvc>("/Danhmuc.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
            application.UseSoapEndpoint<IDanhMucSvc>("/Danhmuc.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            application.UseSoapEndpoint<ITaiSanSvc>("/Taisan.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
            application.UseSoapEndpoint<ITaiSanSvc>("/Taisan.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            // dmdc
            //application.UseSoapEndpoint<IDMDC_DanhMucSvc>("/DMDC_Danhmucsvc.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
            //application.UseSoapEndpoint<IDMDC_DanhMucSvc>("/DMDC_Danhmucsvc.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            application.UseSoapEndpoint<IDMDC_DanhMucSvc>("/DMDC_Danhmucsvc.svc", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            //application.UseSoapEndpoint<IDMDC_DanhMucSvc>("/DMDC_Danhmucsvc.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
        }
    }
}