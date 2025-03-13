using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Data;
using GS.Data.Extensions;
using GS.Services.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public class HoatDongServices : IHoatDongService
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IRepository<HoatDong> _activityLogRepository;
        private readonly IRepository<LoaiHoatDong> _activityLogTypeRepository;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor

        public HoatDongServices(IDbContext dbContext,
            IRepository<HoatDong> activityLogRepository,
            IRepository<LoaiHoatDong> activityLogTypeRepository,
            IStaticCacheManager cacheManager,
            IWebHelper webHelper,
            IWorkContext workContext,
            IHttpContextAccessor httpContextAccessor,
            INguoiDungService nguoiDungService
            )
        {
            this._dbContext = dbContext;
            this._activityLogRepository = activityLogRepository;
            this._activityLogTypeRepository = activityLogTypeRepository;
            this._cacheManager = cacheManager;
            this._webHelper = webHelper;
            this._workContext = workContext;
            this._nguoiDungService = nguoiDungService;
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Nested classes

        /// <summary>
        /// Activity log type for caching
        /// </summary>
        [Serializable]
        public class LoaiHoatDongForCaching
        {
            /// <summary>
            /// Identifier
            /// </summary>
            public decimal ID { get; set; }

            /// <summary>
            /// System keyword
            /// </summary>
            public string TU_KHOA_HE_THONG { get; set; }

            /// <summary>
            /// Name
            /// </summary>
            public string TEN { get; set; }

            /// <summary>
            /// Enabled
            /// </summary>
            public bool TINH_KHA_DUNG { get; set; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets all activity log types (class for caching)
        /// </summary>
        /// <returns>Activity log types</returns>
        protected virtual IList<LoaiHoatDongForCaching> GetAllLoaiHoatDongCached()
        {
            //cache

            return _cacheManager.Get(GSLoggingDefaults.ActivityTypePatternCacheKey, () =>
            {
                var result = new List<LoaiHoatDongForCaching>();
                var activityLogTypes = GetAllLoaiHoatDong();
                foreach (var alt in activityLogTypes)
                {
                    var altForCaching = new LoaiHoatDongForCaching
                    {
                        ID = alt.ID,
                        TU_KHOA_HE_THONG = alt.TU_KHOA_HE_THONG,
                        TEN = alt.TEN,
                        TINH_KHA_DUNG = alt.TINH_KHA_DUNG
                    };
                    result.Add(altForCaching);
                }

                return result;
            });

        }

        #endregion

        #region Methods

        /// <summary>
        /// Search activities through conditions
        /// </summary>
        /// <param name="pageIndex">page number</param>
        /// <param name="pageSize">number of records per page</param>
        /// <param name="KeySearch">KeySearch</param>
        /// <param name="KIEU_HOAT_DONG">LOAI_HOAT_DONG_ID</param>
        /// <param name="FromDate">CreateOnFrom</param>
        /// <param name="ToDate">CreateOnTo</param>
        /// <returns>PagedList<HoatDong></returns>
        /// Create by binhdc - 17/10/2019
        public virtual IPagedList<HoatDong> SearchActivities(int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, string TEN_DANG_NHAP = null, string TEN_DOI_TUONG = null, int KIEU_HOAT_DONG = 0, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            var query = _activityLogRepository.Table;
            if (!string.IsNullOrEmpty(KeySearch))
            {
                query = query.Where(c => c.IP_TRUY_CAP.Contains(KeySearch) || c.TEN_DOI_TUONG.Contains(KeySearch) || c.MO_TA.Contains(KeySearch));
            }
            if (!string.IsNullOrEmpty(TEN_DANG_NHAP))
            {
                NguoiDung _nguoiDung = _nguoiDungService.GetNguoiDungByUsername(TEN_DANG_NHAP);
                if (_nguoiDung != null)
                {
                    query = query.Where(c => c.NGUOI_DUNG_ID == _nguoiDung.ID);
                }
                else
                {
                    query = query.Where(c => c.NGUOI_DUNG_ID == 0);
                }
            }
            if (!string.IsNullOrEmpty(TEN_DOI_TUONG))
            {
                query = query.Where(c => c.TEN_DOI_TUONG.Contains(TEN_DOI_TUONG));
            }
            if (FromDate.HasValue)
            {
                var _fromDate = FromDate.Value.Date;
                query = query.Where(x => x.NGAY_TAO >= _fromDate);
            }
            if (ToDate.HasValue && ToDate != DateTime.MinValue)
            {
                var _toDate = ToDate.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_TAO < _toDate);
            }
            if (KIEU_HOAT_DONG > 0)
            {
                query = query.Where(s => s.LOAI_HOAT_DONG_ID == KIEU_HOAT_DONG);
            }
            query = query.OrderByDescending(x => x.ID);
            return new PagedList<HoatDong>(query, pageIndex, pageSize);
        }


        /// <summary>
        /// Inserts an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public virtual void InsertKieuHoatDong(LoaiHoatDong activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Insert(activityLogType);
            _cacheManager.RemoveByPattern(GSLoggingDefaults.ActivityTypePatternCacheKey);
        }

        /// <summary>
        /// Updates an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public virtual void UpdateKieuHoatDong(LoaiHoatDong activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Update(activityLogType);
            _cacheManager.RemoveByPattern(GSLoggingDefaults.ActivityTypePatternCacheKey);
        }

        /// <summary>
        /// Deletes an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        public virtual void DeleteKieuHoatDong(LoaiHoatDong activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Delete(activityLogType);
            _cacheManager.RemoveByPattern(GSLoggingDefaults.ActivityTypePatternCacheKey);
        }

        /// <summary>
        /// Gets all activity log type items
        /// </summary>
        /// <returns>Activity log type items</returns>
        public virtual IList<LoaiHoatDong> GetAllLoaiHoatDong()
        {
            var query = from alt in _activityLogTypeRepository.Table
                        orderby alt.TEN
                        select alt;
            var activityLogTypes = query.ToList();
            return activityLogTypes;
        }

        /// <summary>
        /// Gets an activity log type item
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        public virtual LoaiHoatDong GetLoaiHoatDongById(decimal activityLogTypeId)
        {
            if (activityLogTypeId == 0)
                return null;

            return _activityLogTypeRepository.GetById(activityLogTypeId);
        }


        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">System keyword</param>
        /// <param name="comment">Comment</param>
        /// <param name="entity">Entity</param>
        /// <returns>Activity log item</returns>
        public virtual HoatDong InsertHoatDong(string systemKeyword, string comment, object entity = null, string DoiTuong = null)
        {
            //lay thong tin id cua Object
            if (entity == null)
                return InsertHoatDong(_workContext.CurrentCustomer, systemKeyword, comment, 0, DoiTuong);
            var _idPro = entity.GetType().GetProperty("ID");
            decimal _entityId = 0;
            if (_idPro != null)
            {
                var id = _idPro.GetValue(entity, null);
                if (id != null && !decimal.TryParse(id.ToString(), out _entityId))
                    _entityId = 0;
            }
            else
                _entityId = 0;
            return InsertHoatDong(_workContext.CurrentCustomer, systemKeyword, comment, _entityId, DoiTuong, entity);
        }
        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="systemKeyword">System keyword</param>
        /// <param name="comment">Comment</param>
        /// <param name="entity">Entity</param>
        /// <returns>Activity log item</returns>
        public virtual HoatDong InsertHoatDong(NguoiDung customer, string systemKeyword, string comment, decimal entityId = 0, string DoiTuong = null, object obj = null)
        {
            if (customer == null)
                customer = _nguoiDungService.GetNguoiDungByUsername("admin");

            //try to get activity log type by passed system keyword
            var activityLogType = GetAllLoaiHoatDongCached().FirstOrDefault(c => c.TU_KHOA_HE_THONG.Equals(systemKeyword));
            //neu chua ton tai thi insert vao du lieu
            if (activityLogType == null)
            {
                var itemLogType = new LoaiHoatDong();
                itemLogType.TINH_KHA_DUNG = true;
                itemLogType.TEN = systemKeyword;
                itemLogType.TU_KHOA_HE_THONG = systemKeyword;
                InsertKieuHoatDong(itemLogType);
                //load lai
                activityLogType = GetAllLoaiHoatDongCached().FirstOrDefault(type => type.TU_KHOA_HE_THONG.Equals(systemKeyword));
            }
            if (!activityLogType?.TINH_KHA_DUNG ?? true)
                return null;
            var SessionID = _httpContextAccessor.HttpContext.Session.Id;
            //var Connection = _httpContextAccessor.HttpContext.Connection;
            //insert log item
            var logItem = new HoatDong
            {
                LOAI_HOAT_DONG_ID = activityLogType.ID,
                DOI_TUONG_ID = entityId,
                TEN_DOI_TUONG = DoiTuong,
                NGUOI_DUNG_ID = customer.ID,
                MO_TA = comment,
                NGAY_TAO = DateTime.Now,
                IP_TRUY_CAP = _webHelper.GetCurrentIpAddress(),
                DU_LIEU = obj.toStringJson(),
                SESSION_ID = SessionID
            };
            _activityLogRepository.Insert(logItem);

            return logItem;
        }
        /// <summary>
        /// Update an logs cho 1 số trường hợp đặc biệt
        /// </summary>
        /// <param name="activityLog"></param>
        /// <returns></returns>
        public virtual void UpdateHoatDong(HoatDong activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));
            _activityLogRepository.Update(activityLog);
        }
        /// <summary>
        /// Deletes an activity log item
        /// </summary>
        /// <param name="activityLog">Activity log type</param>
        public virtual void DeleteHoatDong(HoatDong activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            _activityLogRepository.Delete(activityLog);
        }

        /// <summary>
        /// Gets all activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; pass null to load all records</param>
        /// <param name="createdOnTo">Log item creation to; pass null to load all records</param>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="activityLogTypeId">Activity log type identifier; pass null to load all records</param>
        /// <param name="ipAddress">IP address; pass null or empty to load all records</param>
        /// <param name="entityName">Entity name; pass null to load all records</param>
        /// <param name="entityId">Entity identifier; pass null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Activity log items</returns>
        public virtual IPagedList<HoatDong> GetAllHoatDong(DateTime? createdOnFrom = null, DateTime? createdOnTo = null,
            int? customerId = null, int? activityLogTypeId = null, string ipAddress = null, string entityName = null, int? entityId = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _activityLogRepository.Table;

            //filter by IP
            if (!string.IsNullOrEmpty(ipAddress))
                query = query.Where(logItem => logItem.IP_TRUY_CAP.Contains(ipAddress));

            //filter by creation date
            if (createdOnFrom.HasValue)
                query = query.Where(logItem => createdOnFrom.Value <= logItem.NGAY_TAO);
            if (createdOnTo.HasValue)
                query = query.Where(logItem => createdOnTo.Value >= logItem.NGAY_TAO);

            //filter by log type
            if (activityLogTypeId.HasValue && activityLogTypeId.Value > 0)
                query = query.Where(logItem => activityLogTypeId == logItem.LOAI_HOAT_DONG_ID);

            //filter by customer
            if (customerId.HasValue && customerId.Value > 0)
                query = query.Where(logItem => customerId.Value == logItem.NGUOI_DUNG_ID);

            //filter by entity
            if (!string.IsNullOrEmpty(entityName))
                query = query.Where(logItem => logItem.TEN_DOI_TUONG.Equals(entityName));
            if (entityId.HasValue && entityId.Value > 0)
                query = query.Where(logItem => entityId.Value == logItem.DOI_TUONG_ID);

            query = query.OrderByDescending(logItem => logItem.NGAY_TAO).ThenBy(logItem => logItem.ID);

            return new PagedList<HoatDong>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        public virtual HoatDong GetHoatDongById(decimal activityLogId)
        {
            if (activityLogId == 0)
                return null;

            return _activityLogRepository.GetById(activityLogId);
        }

        /// <summary>
        /// Clears activity log
        /// </summary>
        public virtual void ClearAllHoatDong()
        {
            //do all databases support "Truncate command"?
            var activityLogTableName = _dbContext.GetTableName<HoatDong>();
            _dbContext.ExecuteSqlCommand($"TRUNCATE TABLE [{activityLogTableName}]");

            //var activityLog = _activityLogRepository.Table.ToList();
            //foreach (var activityLogItem in activityLog)
            //    _activityLogRepository.Delete(activityLogItem);
        }

        public IPagedList<LoaiHoatDong> SearchLoaiHoatDongs(int pageIndex = 0, int pageSize = int.MaxValue, string keywordSystem = null, string name = null)
        {
            var query = _activityLogTypeRepository.Table;
            if (!string.IsNullOrEmpty(keywordSystem))
                query = query.Where(c => c.TU_KHOA_HE_THONG.ToLower().Contains(keywordSystem.ToLower()));
            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.TEN.ToUpper().Contains(name.ToUpper()));
            return new PagedList<LoaiHoatDong>(query, pageIndex, pageSize); ;
        }

        public LoaiHoatDong GetLoaiHoatDongByCode(string code)
        {
            if (code == null || code.Equals(""))
                return new LoaiHoatDong();
            return _activityLogTypeRepository.Table.Where(c => c.TU_KHOA_HE_THONG == code).FirstOrDefault();
        }


        #endregion
    }
}
