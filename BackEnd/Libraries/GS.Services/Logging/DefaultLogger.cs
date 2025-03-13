using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Data;
using GS.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.Logging
{
    /// <summary>
    /// Default logger
    /// </summary>
    public partial class DefaultLogger : ILogger
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IRepository<NhatKy> _logRepository;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public DefaultLogger(
            IDbContext dbContext,
            IRepository<NhatKy> logRepository,
            IWebHelper webHelper)
        {
            this._dbContext = dbContext;
            this._logRepository = logRepository;
            this._webHelper = webHelper;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets a value indicating whether this message should not be logged
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Result</returns>
        protected virtual bool IgnoreLog(string message)
        {
            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        public virtual void DeleteLog(NhatKy log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            _logRepository.Delete(log);
        }

        /// <summary>
        /// Deletes a log items
        /// </summary>
        /// <param name="logs">Log items</param>
        public virtual void DeleteLogs(IList<NhatKy> logs)
        {
            if (logs == null)
                throw new ArgumentNullException(nameof(logs));

            _logRepository.Delete(logs);
        }

        /// <summary>
        /// Clears a log
        /// </summary>
        public virtual void ClearLog()
        {
            //do all databases support "Truncate command"?
            var logTableName = _dbContext.GetTableName<NhatKy>();
            _dbContext.ExecuteSqlCommand($"TRUNCATE TABLE [{logTableName}]");

            //var log = _logRepository.Table.ToList();
            //foreach (var logItem in log)
            //    _logRepository.Delete(logItem);
        }

        /// <summary>
        /// Gets all log items
        /// </summary>
        /// <param name="fromUtc">Log item creation from; null to load all records</param>
        /// <param name="toUtc">Log item creation to; null to load all records</param>
        /// <param name="message">Message</param>
        /// <param name="logLevel">Log level; null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Log item items</returns>
        public virtual IPagedList<NhatKy> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _logRepository.Table;
            if (fromUtc.HasValue)
                query = query.Where(l => fromUtc.Value <= l.NGAY_TAO);
            if (toUtc.HasValue)
                query = query.Where(l => toUtc.Value >= l.NGAY_TAO);
            if (logLevel.HasValue)
            {
                var logLevelId = (int)logLevel.Value;
                query = query.Where(l => logLevelId == l.CAP_DO_ID);
            }

            if (!string.IsNullOrEmpty(message))
                query = query.Where(l => l.MO_TA.Contains(message) || l.NOI_DUNG.Contains(message));
            query = query.OrderByDescending(l => l.NGAY_TAO);

            var log = new PagedList<NhatKy>(query, pageIndex, pageSize);
            return log;
        }

        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public virtual NhatKy GetLogById(decimal logId)
        {
            if (logId == 0)
                return null;

            return _logRepository.GetById(logId);
        }


        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customer">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        public virtual NhatKy InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", NguoiDung customer = null, String dataJson = null)
        {
            //check ignore word/phrase list?
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;

            var log = new NhatKy
            {
                CAP_DO_ID = (int)logLevel,
                MO_TA = shortMessage,
                NOI_DUNG = fullMessage,
                IP_ADDRESS = _webHelper.GetCurrentIpAddress(),
                TEN_DANG_NHAP = customer != null ? customer.TEN_DANG_NHAP : "",
                TEN_DAY_DU = customer != null ? customer.TEN_DAY_DU : "",
                PAGE_URL = _webHelper.GetThisPageUrl(true),
                //ReferrerUrl = _webHelper.GetUrlReferrer(),
                NGAY_TAO = DateTime.Now,
                DU_LIEU = dataJson
            };

            _logRepository.Insert(log);

            return log;
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="customer">Customer</param>
        public virtual void Information(string message, Exception exception = null, NguoiDung customer = null, String dataJson = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Information))
                InsertLog(LogLevel.Information, message, exception?.ToString() ?? string.Empty, customer, dataJson);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="customer">Customer</param>
        public virtual void Warning(string message, Exception exception = null, NguoiDung customer = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Warning))
                InsertLog(LogLevel.Warning, message, exception?.ToString() ?? string.Empty, customer);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="customer">Customer</param>
        public virtual void Error(string message, Exception exception = null, NguoiDung customer = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Error))
                InsertLog(LogLevel.Error, message, exception?.ToString() ?? string.Empty, customer);
        }

        #endregion
    }
}