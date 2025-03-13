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

namespace GS.Services.TaiSans
{
    public partial class KhaiThacTaiSanService : IKhaiThacTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhaiThacTaiSan> _itemRepository;
        #endregion

        #region Ctor

        public KhaiThacTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<KhaiThacTaiSan> itemRepository
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
        public virtual IList<KhaiThacTaiSan> GetAllKhaiThacTaiSans(int HinhThucKhaiThacId=0)
        {
            var query = _itemRepository.Table;
            if(HinhThucKhaiThacId>0)
            {
                query = query.Where(m => m.khaiThac.LOAI_HINH_KHAI_THAC_ID == HinhThucKhaiThacId);
            }
            return query.ToList();
        }

        public virtual IPagedList<KhaiThacTaiSan> SearchKhaiThacTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? KhaiThacId = 0, Decimal? KhaiThacChiTietId = 0 )
        {
            var query = _itemRepository.Table;
            if (KhaiThacId > 0)
            {
                query = query.Where(c => c.KHAI_THAC_ID == KhaiThacId);
            }
            if (KhaiThacChiTietId > 0)
            {
                query = query.Where(c => c.KHAI_THAC_CHI_TIET_ID == KhaiThacChiTietId);
            }
            return new PagedList<KhaiThacTaiSan>(query.OrderBy(p=>p.TU_NGAY), pageIndex, pageSize); ;
        }

        public virtual KhaiThacTaiSan GetKhaiThacTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        /// <summary>
        /// Lấy 1 KhaiThacTaiSan theo mã tài sản hoặc id khai thác
        /// </summary>
        /// <param name="ma_tai_san"></param>
        /// <param name="khaiThacId"></param>
        /// <returns></returns>
        public virtual KhaiThacTaiSan GetKhaiThacTaiSanByMaTaiSanAndKhaiThacID(string ma_tai_san, decimal khaiThacId = 0)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(ma_tai_san))
                query = query.Where(c => c.taiSan != null && c.taiSan.MA == ma_tai_san);
            if (khaiThacId > 0)
                query = query.Where(c => c.KHAI_THAC_ID == khaiThacId);
            return query.FirstOrDefault();
        }
        /// <summary>
        /// Lấy 1 KhaiThacTaiSan theo mã tài sản đồng bộ hoặc id khai thác
        /// </summary>
        /// <param name="ma_tai_san_DB"></param>
        /// <param name="khaiThacId"></param>
        /// <returns></returns>
        public virtual KhaiThacTaiSan GetKhaiThacTaiSanByMaTaiSanDBAndKhaiThacID(string ma_tai_san_DB, decimal khaiThacId = 0)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(ma_tai_san_DB))
                query = query.Where(c => c.taiSan != null && c.taiSan.MA_DB == ma_tai_san_DB);
            if (khaiThacId > 0)
                query = query.Where(c => c.KHAI_THAC_ID == khaiThacId);
            return query.FirstOrDefault();
        }
        public virtual IList<KhaiThacTaiSan> GetKhaiThacTaiByKhaiThacChiTietID( decimal khaiThacChiTietId = 0)
        {
            var query = _itemRepository.Table;
            
            if (khaiThacChiTietId > 0)
                query = query.Where(c => c.KHAI_THAC_CHI_TIET_ID == khaiThacChiTietId);
            return query.ToList();
        }

        public virtual IList<KhaiThacTaiSan> GetKhaiThacTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual IList<KhaiThacTaiSan> GetKhaiThacTaiSanBytKhaiThacIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.KHAI_THAC_ID)).ToList();
        }
        public virtual IList<KhaiThacTaiSan> GetKhaiThacTaiSanKhacByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => !Ids.Contains(c.ID)).ToList();
        }
        public virtual IList<KhaiThacTaiSan> GetMapByKhaiThacId(decimal khaiThacId)
        {
            var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == khaiThacId);
            return query.ToList();
        }
        public virtual KhaiThacTaiSan GetMapByKhaiThacIdAbTaiSanId(decimal khaiThacId, decimal taiSanId)
        {
            var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == khaiThacId && c.TAI_SAN_ID == taiSanId);
            return query.FirstOrDefault();
        }
        public virtual void InsertKhaiThacTaiSan(KhaiThacTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhaiThacTaiSan(KhaiThacTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKhaiThacTaiSan(KhaiThacTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public void DeleteKhaiThacTaiSans(IEnumerable<KhaiThacTaiSan> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));
            if (entities.Count() > 0)
                _itemRepository.Delete(entities);
        }
        public bool checkTrungKhaiThacTaiSan(decimal KhaiThacId, decimal TaiSanId)
        {
            var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == KhaiThacId && c.TAI_SAN_ID == TaiSanId);
            if (query.Count() > 0)
            { return true; }
            else
                return false;
        }
        public virtual void DeleteKhaiThacTaiSanId(decimal KhaiThacId, decimal TaiSanId)
        {
            var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == KhaiThacId && c.TAI_SAN_ID == TaiSanId);

            if (query == null)
                throw new ArgumentNullException(nameof(query));
            _itemRepository.Delete(query);
        }
        public decimal? TinhSoTienKhaiThacLuyKe(decimal? KhaiThacId)
        {
            decimal? soTienLuyKe = GetMapByKhaiThacId(KhaiThacId ?? 0)?.Sum(c => c.SO_TIEN);
            return soTienLuyKe;
        }

        #endregion
    }
}

