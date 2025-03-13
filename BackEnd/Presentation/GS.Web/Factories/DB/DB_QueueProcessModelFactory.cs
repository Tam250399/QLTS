//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.DB;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace GS.Web.Factories.DB
{
    public class DB_QueueProcessModelFactory : IDB_QueueProcessModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDB_QueueProcessService _itemService;

        #endregion Fields

        #region Ctor

        public DB_QueueProcessModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDB_QueueProcessService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }

        #endregion Ctor

        #region DB_QueueProcess

        public DB_QueueProcessSearchModel PrepareDB_QueueProcessSearchModel(DB_QueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DB_QueueProcessListModel PrepareDB_QueueProcessListModel(DB_QueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDB_QueueProcesss(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DB_QueueProcessListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DB_QueueProcessModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DB_QueueProcessModel PrepareDB_QueueProcessModelInlist(DB_QueueProcessModel model, DB_QueueProcess item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DB_QueueProcessModel>();
                switch (model.TRANG_THAI_ID)
                {
                    case (decimal)enumTrangThaiQueueProcessDB.GUI_REQUEST_THANH_CONG:
                        model.TRANG_THAI_TEN = "Gửi thành công";
                        break;
                }

            }

            //more

            return model;
        }
        public DB_QueueProcessModel PrepareDB_QueueProcessModel(DB_QueueProcessModel model, DB_QueueProcess item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DB_QueueProcessModel>();
            }
            //more

            return model;
        }

        public void PrepareDB_QueueProcess(DB_QueueProcessModel model, DB_QueueProcess item)
        {
            item.GUID = model.GUID;
            item.GUID_RESPONSE = model.GUID_RESPONSE;
            item.URL_CALL = model.URL_CALL;
            item.DATA_INPUT = model.DATA_INPUT;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.NGAY_TAO = model.NGAY_TAO;
            item.LAST_SEND_REQUEST = model.LAST_SEND_REQUEST;
            item.DON_VI_ID = model.DON_VI_ID;
        }
        public void InsertActionToQueue(string action, object obj, decimal DonViId, int level = (int)enumLevelQueueProcessDB.MEDIUM)
        {
            var queue = new DB_QueueProcess();
            if (_workContext.CurrentCustomer != null)
                queue.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            queue.CAP_DO_ID = level;
            queue.DON_VI_ID = DonViId;
            queue.URL_CALL = action;
            queue.DATA_INPUT = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            queue.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.DANG_CHO;
            _itemService.InsertDB_QueueProcess(entity: queue);
        }

        public decimal InsertActionToQueueReturnId(string action, object obj, decimal DonViId, int level = (int)enumLevelQueueProcessDB.MEDIUM, Decimal? DoiTuongId = null)
        {
            var queue = new DB_QueueProcess();
            if (DoiTuongId.HasValue)
                queue = _itemService.GetDB_QueueProcessByDoiTuongID(action, DoiTuongId.Value);
            if (queue == null)
                queue = new DB_QueueProcess();
            if (_workContext.CurrentCustomer != null)
                queue.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            queue.CAP_DO_ID = level;
            queue.DON_VI_ID = DonViId;
            queue.URL_CALL = action;
            queue.DOI_TUONG_ID = DoiTuongId;
            queue.DATA_INPUT = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            queue.TRANG_THAI_ID = (decimal)enumTrangThaiQueueProcessDB.DANG_CHO;
            if (queue.ID > 0)
                _itemService.UpdateDB_QueueProcess(queue);
            else
                _itemService.InsertDB_QueueProcess(entity: queue);

            return queue.ID;
        }
        #endregion DB_QueueProcess
    }
}