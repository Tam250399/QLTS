using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.SHTD;
using GS.Data;
using GS.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Services.SHTD
{
    public class KetQuaTaiSanServices:IKetQuaTaiSanServices
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KetQuaTaiSan> _itemRepository;
        private readonly IDonViService _donViService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public KetQuaTaiSanServices(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<KetQuaTaiSan> itemRepository,
            IDonViService donViService,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donViService = donViService;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<KetQuaTaiSan> GetAllKetQuaTaiSans()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KetQuaTaiSan> SearchKetQuaTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,decimal XuLyKetQuaId=0,bool Is_Create = false)
        {
            var query = _itemRepository.Table.Where(c=>c.xulyketqua.xulyTD.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (Is_Create)
            {
                query = query.Where(c => c.xulyketqua.TRANG_THAI_ID == (int)enumTrangThaiXuLyKetQua.TONTAI);
            }
            if (XuLyKetQuaId > 0)
            {
                query = query.Where(c => c.XU_LY_KET_QUA_ID == XuLyKetQuaId);
            }
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.taisantdxuly.taisantd.TEN.Contains(Keysearch));
            }
            return new PagedList<KetQuaTaiSan>(query, pageIndex, pageSize); ;
        }

        public virtual KetQuaTaiSan GetKetQuaTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<KetQuaTaiSan> GetKetQuaTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<KetQuaTaiSan> GetKetQuaTaiSans(decimal XuLyKetQuaId = 0 , decimal TaiSanTdId = 0 ,decimal TaiSanTDXuLyId = 0)
        {
            var query = _itemRepository.Table;
            if (XuLyKetQuaId > 0)
            {
                query = query.Where(c => c.XU_LY_KET_QUA_ID == XuLyKetQuaId);
            }
            if (TaiSanTdId > 0)
            {
                query = query.Where(c => c.TAI_SAN_TD_ID == TaiSanTdId);
            }
            if (TaiSanTDXuLyId > 0)
            {
                query = query.Where(c => c.TAI_SAN_TD_XU_LY_ID == TaiSanTDXuLyId);
            }
            return query.ToList();
        }


        public virtual void InsertKetQuaTaiSan(KetQuaTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }
        public virtual void UpdateKetQuaTaiSan(KetQuaTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKetQuaTaiSan(KetQuaTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        #endregion
    }
}
