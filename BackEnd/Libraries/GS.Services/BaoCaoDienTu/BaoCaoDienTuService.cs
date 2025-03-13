//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
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
using GS.Core.Domain.BaoCaoDienTu;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.BaoCaoDienTus
{
    public partial class BaoCaoDienTuService : IBaoCaoDienTuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<BaoCaoDienTu> _itemRepository;
        private readonly IRepository<DonVi> _donviRepository;
        #endregion

        #region Ctor

        public BaoCaoDienTuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<BaoCaoDienTu> itemRepository,
            IRepository<DonVi> donviRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donviRepository = donviRepository;
        }

        #endregion

        #region Methods
        public virtual IList<BaoCaoDienTu> GetAllBaoCaoDienTu()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual List<BaoCaoDienTu> GetBaoCaoDienTu(decimal? donViId = null,decimal? namBaoCao = null,decimal? baoCaoId = null,decimal? heThongId = null)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
                query = query.Where(x => x.DON_VI_ID == donViId);
            return query.ToList();
        }
        public virtual IPagedList<BaoCaoDienTu> SearchBaoCaoDienTu(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal? donVi = 0, DateTime? NGAY_BAO_CAO = null, decimal TRANG_THAI_ID = 0)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch));
            }
            if (NGAY_BAO_CAO != null)
            {
                query = query.Where(m => m.NGAY_BAO_CAO.Value.Date == NGAY_BAO_CAO.Value.Date);
            }
            if (TRANG_THAI_ID >= 0)
            {
                query = query.Where(m => m.TRANG_THAI_ID == TRANG_THAI_ID);
            }
            if (donVi >= 0)
            {
                query = query.Where(m => m.DON_VI_ID == donVi);
            }
            //if (donVi > 0 && ( donVi.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
            //{
            //    var donvi = _donviRepository.GetById(donVi.Value);
            //    query = query.Where(c => c.DON_VI_ID == donVi.Value || c.donvi.TREE_NODE.StartsWith(donvi.TREE_NODE + "-"));
            //    //if (IsExclueTSDKTS)
            //    //{
            //    //    if (!(donVi.IS_XAC_NHAN_DU_LIEU??false))
            //    //    {
            //    //        query = query.Where(c => c.NGUON_TAI_SAN_ID != (int)enumNguonTaiSan.DKTS40);
            //    //    }
            //    //}
            //}
            query = query.OrderBy(c => c.TRANG_THAI_ID);
            return new PagedList<BaoCaoDienTu>(query, pageIndex, pageSize);
        }
        public virtual IPagedList<BaoCaoDienTu> SearchBaoCaoChoDuyet(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal? donVi = 0, DateTime? NGAY_BAO_CAO = null, decimal TRANG_THAI_ID = 0)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch));
            }
            if (NGAY_BAO_CAO != null)
            {
                query = query.Where(m => m.NGAY_BAO_CAO.Value.Date == NGAY_BAO_CAO.Value.Date);
            }
            if (TRANG_THAI_ID >= 0)
            {
                query = query.Where(m => m.TRANG_THAI_ID == TRANG_THAI_ID);
            }
            if (donVi > 0 && (donVi.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
            {
                var donvi = _donviRepository.GetById(donVi.Value);
                query = query.Where(c => c.DON_VI_ID == donVi.Value || c.donvi.TREE_NODE.StartsWith(donvi.TREE_NODE + "-"));
                //if (IsExclueTSDKTS)
                //{
                //    if (!(donVi.IS_XAC_NHAN_DU_LIEU??false))
                //    {
                //        query = query.Where(c => c.NGUON_TAI_SAN_ID != (int)enumNguonTaiSan.DKTS40);
                //    }
                //}
            }
            return new PagedList<BaoCaoDienTu>(query, pageIndex, pageSize);
        }
        public virtual BaoCaoDienTu GetBaoCaoDienTuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<BaoCaoDienTu> GetBaoCaoDienTuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertBaoCaoDienTu(BaoCaoDienTu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateBaoCaoDienTu(BaoCaoDienTu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteBaoCaoDienTu(BaoCaoDienTu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteBaoCaoDienTu(List<BaoCaoDienTu> entity)
        {
            if (entity == null||(entity!=null&&entity.Count==0))
                throw new ArgumentNullException(nameof(BaoCaoDienTu));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual BaoCaoDienTu GetBaoCaoDienTuBySeachModel(Decimal? donViId = 0, Decimal? baoCaoId = 0, Decimal? heThongId = 0, Decimal? nam = 0, string TenBaoCao = null)
        {
            var item = _itemRepository.Table.Where(c => c.TEN == TenBaoCao).FirstOrDefault();
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            return item;
        }
        
        #endregion
    }
}

