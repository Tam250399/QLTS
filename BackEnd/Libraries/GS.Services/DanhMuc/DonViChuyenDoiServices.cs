//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
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
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial class DonViChuyenDoiService : IDonViChuyenDoiService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DonViChuyenDoi> _itemRepository;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public DonViChuyenDoiService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DonViChuyenDoi> itemRepository,
            IDonViService donViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donViService = donViService;
        }

        #endregion

        #region Methods
        public virtual IList<DonViChuyenDoi> GetAllDonViChuyenDois()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DonViChuyenDoi> SearchDonViChuyenDois(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int forDonVi = 0)
        {
            var query = _itemRepository.Table;
            if (forDonVi > 0)
            {
                query = query.Where(x => x.DON_VI_ID == forDonVi);
            }
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(x =>
                    x.TEN.ToLower().Contains(Keysearch.ToLower()) ||
                    x.QUYET_DINH_GIAO_VON_SO.ToLower().Contains(Keysearch.ToLower()) ||
                    x.QUYET_DINH_SO.ToLower().Contains(Keysearch.ToLower())
                    );
            };
            query = query.OrderByDescending(i => i.QUYET_DINH_NGAY);
            return new PagedList<DonViChuyenDoi>(query, pageIndex, pageSize); ;
        }

        public virtual DonViChuyenDoi GetDonViChuyenDoiById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DonViChuyenDoi> GetDonViChuyenDoiByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual DonViChuyenDoi GetDonViChuyenDoiFromDonViId(int id)
        {
            var donvi = _donViService.GetDonViById(id);
            var donViChuyenDoi = new DonViChuyenDoi();
            donViChuyenDoi.DON_VI_ID = donvi.ID;
            if (donViChuyenDoi.LOAI_DON_VI_ID != 0)
            {
                donViChuyenDoi.LOAI_DON_VI_ID = donvi.LOAI_DON_VI_ID.Value;
            }
            donViChuyenDoi.TEN = donvi.TEN;
            return donViChuyenDoi;
        }

        public virtual void InsertDonViChuyenDoi(DonViChuyenDoi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDonViChuyenDoi(DonViChuyenDoi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDonViChuyenDoi(DonViChuyenDoi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public DonViChuyenDoi GetDonViChuyenDoiByDonViId(decimal? donViId, DateTime? ngayBC, decimal namBC)
        {
            if (donViId == 0)
                return null;
            if (ngayBC != null)
            {
                var donViChuyenDoi = _itemRepository.Table.Where(c => c.DON_VI_ID == donViId && c.QUYET_DINH_NGAY < ngayBC);
                return donViChuyenDoi.OrderByDescending(c => c.QUYET_DINH_NGAY).FirstOrDefault();
            }
            if (namBC != 0)
            {
                var donViChuyenDoi = _itemRepository.Table.Where(c => c.DON_VI_ID == donViId && c.QUYET_DINH_NGAY.Value.Year <= namBC);
                return donViChuyenDoi.OrderByDescending(c => c.QUYET_DINH_NGAY).FirstOrDefault();
            }
            return null;
        }
        #endregion
    }
}

