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
    public class XuLyKetQuaServices : IXuLyKetQuaServices
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<XuLyKetQua> _itemRepository;
        private readonly IDonViService _donViService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public XuLyKetQuaServices(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<XuLyKetQua> itemRepository,
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
        public virtual IList<XuLyKetQua> GetAllXuLyKetQuas()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<XuLyKetQua> SearchXuLyKetQuas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table.Where(c=>c.TRANG_THAI_ID == (int)enumTrangThaiXuLyKetQua.TONTAI && c.xulyTD.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (!string.IsNullOrWhiteSpace(Keysearch))
            {
                query = query.Where(c => c.CHUNG_TU_NOP_TIEN_SO.Contains(Keysearch));
            }
            return new PagedList<XuLyKetQua>(query, pageIndex, pageSize); ;
        }

        public virtual XuLyKetQua GetXuLyKetQuaById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<XuLyKetQua> GetXuLyKetQuaByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<XuLyKetQua> GetXuLyKetQuas()
        {
            var query = _itemRepository.Table; ;
            return query.ToList();
        }


        public virtual void InsertXuLyKetQua(XuLyKetQua entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }
        public virtual void UpdateXuLyKetQua(XuLyKetQua entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteXuLyKetQua(XuLyKetQua entity)
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