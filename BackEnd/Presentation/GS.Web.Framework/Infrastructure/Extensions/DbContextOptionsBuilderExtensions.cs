﻿using GS.Core.Configuration;
using GS.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of DbContextOptionsBuilder
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// SQL Server specific extension method for Microsoft.EntityFrameworkCore.DbContextOptionsBuilder
        /// </summary>
        /// <param name="optionsBuilder">Database context options builder</param>
        /// <param name="services">Collection of service descriptors</param>
        public static void UseSqlServerWithLazyLoading(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services)
        {
            var nopConfig = services.BuildServiceProvider().GetRequiredService<GSConfig>();

            var dataSettings = DataSettingsManager.LoadSettings();
            if (!dataSettings?.IsValid ?? true)
                return;

            var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
            if (dataSettings.DataProvider == DataProviderType.Oracle)
                dbContextOptionsBuilder.UseOracle(dataSettings.DataConnectionString);
            else
                dbContextOptionsBuilder.UseSqlServer(dataSettings.DataConnectionString);
            //if (nopConfig.UseRowNumberForPaging)
            //    dbContextOptionsBuilder.UseSqlServer(dataSettings.DataConnectionString, option => option.UseRowNumberForPaging());
            //else
            //    dbContextOptionsBuilder.UseSqlServer(dataSettings.DataConnectionString);
        }
    }
}
