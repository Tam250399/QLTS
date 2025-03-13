//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.DB;
using Newtonsoft.Json;
using GS.Services.TaiSans;

namespace GS.Services.DB
{
    public partial class DB_QueueProcessService : IDB_QueueProcessService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DB_QueueProcess> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public DB_QueueProcessService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DB_QueueProcess> itemRepository,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<DB_QueueProcess> GetAllDB_QueueProcesss(Guid? guid = null, String guidResponse = null)
        {
            var query = _itemRepository.Table;
            if (String.IsNullOrEmpty(guidResponse))
            {
                query = query.Where(x => x.GUID_RESPONSE == guidResponse);
            }
            return query.ToList();
        }

        public virtual IPagedList<DB_QueueProcess> SearchDB_QueueProcesss(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<DB_QueueProcess>(query, pageIndex, pageSize); ;
        }

        public virtual DB_QueueProcess GetDB_QueueProcessById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual DB_QueueProcess GetDB_QueueProcessByDoiTuongID(string urlCall, decimal doiTuongId)
        {
            return _itemRepository.Table.Where(x => x.URL_CALL == urlCall && x.DOI_TUONG_ID == doiTuongId).FirstOrDefault();
        }
        public virtual DB_QueueProcess GetDB_QueueProcessByIdNeedToSendRequest()
        {
            //if (_itemRepository.Table.Where(p => p.TRANG_THAI_ID == (int)enumTrangThaiQueueProcessDB.DA_GUI_REQUEST).Count() > 5)
            //    return null;
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != null && p.TRANG_THAI_ID == (decimal?)enumTrangThaiQueueProcessDB.DANG_CHO).OrderByDescending(p => p.CAP_DO_ID).ThenBy(c => c.ID).FirstOrDefault();
            return query;
        }

        public virtual IList<DB_QueueProcess> GetDB_QueueProcessByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDB_QueueProcess(DB_QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.GUID = Guid.NewGuid();
            entity.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.DANG_CHO;
            entity.NGAY_TAO = DateTime.Now;

            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDB_QueueProcess(DB_QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDB_QueueProcess(DB_QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public DB_QueueProcess InsertActionToQueue(string action, object obj, decimal DonViId=0, int level = (int)enumLevelQueueProcessDB.MEDIUM, Decimal? DoiTuongId = null)
        {
            var queue = new DB_QueueProcess();
            if (DoiTuongId.HasValue)
                queue = GetDB_QueueProcessByDoiTuongID(action, DoiTuongId.Value);
            if (queue == null)
                queue = new DB_QueueProcess();
                queue.NGUOI_TAO_ID = _workContext.CurrentCustomer?.ID;
            queue.CAP_DO_ID = level;
            queue.DON_VI_ID = DonViId;
            queue.URL_CALL = action;
            queue.DOI_TUONG_ID = DoiTuongId;
            queue.DATA_INPUT = obj.toStringJson(isIgnoreNull: true);
            //JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //});
            queue.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.DANG_CHO;
            if (queue.ID > 0)
                UpdateDB_QueueProcess(queue);
            else
                InsertDB_QueueProcess(entity: queue);
            return queue;
        }
        #endregion
    }
}

