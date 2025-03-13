//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
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
using GS.Core.Domain.TaiSans;
using DevExpress.XtraRichEdit.Fields;
using System.Net.WebSockets;

namespace GS.Services.TaiSans
{
    public partial class KhaiThacService : IKhaiThacService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhaiThac> _itemRepository;
        #endregion

        #region Ctor

        public KhaiThacService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<KhaiThac> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<KhaiThac> GetAllKhaiThacs()
        {
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <KhaiThac> SearchKhaiThacs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, string QUYET_DINH_SO = null, decimal LOAI_HINH_KHAI_THAC_ID = 0, DateTime? QUYET_DINH_NGAY = null, string HOP_DONG_SO = null, DateTime? HOP_DONG_NGAY = null, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null,decimal donviId=0)
        {
             var query = _itemRepository.Table.Where(c=>c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);
            if(LOAI_HINH_KHAI_THAC_ID>0)
                query = query.Where(c => c.LOAI_HINH_KHAI_THAC_ID == LOAI_HINH_KHAI_THAC_ID);
            if(donviId > 0)
                query = query.Where(c => c.DON_VI_ID == donviId);
            if (!string.IsNullOrEmpty(QUYET_DINH_SO))
                query = query.Where(c=>c.QUYET_DINH_SO.ToUpper().Contains(QUYET_DINH_SO.ToUpper()));
            if (QUYET_DINH_NGAY!=null)
                query = query.Where(c => c.QUYET_DINH_NGAY == QUYET_DINH_NGAY);
            if (!string.IsNullOrEmpty(HOP_DONG_SO))
                query = query.Where(c => c.HOP_DONG_SO.ToUpper().Contains(HOP_DONG_SO.ToUpper()));
            if (HOP_DONG_NGAY != null)
                query = query.Where(c => c.HOP_DONG_NGAY == HOP_DONG_NGAY);
            if (KHAI_THAC_NGAY_TU.HasValue)
            {
                var _tungay = KHAI_THAC_NGAY_TU.Value.Date;
                query = query.Where(x => x.NGAY_KHAI_THAC >= _tungay);
            }
            if (KHAI_THAC_NGAY_DEN.HasValue && KHAI_THAC_NGAY_DEN != DateTime.MinValue)
            {
                var _denngay = KHAI_THAC_NGAY_DEN.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_KHAI_THAC < _denngay);
            }
            query = query.OrderByDescending(c => c.QUYET_DINH_NGAY);
            return new PagedList<KhaiThac>(query, pageIndex, pageSize);;
         }

        public virtual IPagedList<KhaiThac> GetKhaiThacsKhacNgay(int pageIndex = 0, int pageSize = int.MaxValue, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null, decimal donviId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);
           
            if (donviId > 0)
                query = query.Where(c => c.DON_VI_ID == donviId);
            if (KHAI_THAC_NGAY_TU.HasValue && KHAI_THAC_NGAY_DEN.HasValue)
            {
                var _tuNgay = KHAI_THAC_NGAY_TU.Value.Date;
                var _denNgay = KHAI_THAC_NGAY_DEN.Value.Date;
                var dung = query.Where(x => x.KHAI_THAC_NGAY_DEN < _tuNgay ||x.KHAI_THAC_NGAY_TU > _denNgay).Select(c=>c.ID);
                query = query.Where(c => !dung.Contains(c.ID));
            }
            
            return new PagedList<KhaiThac>(query, pageIndex, pageSize); ;
        }

        public virtual KhaiThac GetKhaiThacById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<KhaiThac> GetKhaiThacByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertKhaiThac(KhaiThac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhaiThac(KhaiThac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKhaiThac(KhaiThac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual KhaiThac GetKhaiThacByMa_DB(string ma_db)
        {
            if (string.IsNullOrEmpty(ma_db))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == ma_db).FirstOrDefault();
        }
        #endregion
    }
}

