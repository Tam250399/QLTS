//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
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
    public partial class DeNghiXuLyService : IDeNghiXuLyService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DeNghiXuLy> _itemRepository;
        #endregion

        #region Ctor

        public DeNghiXuLyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DeNghiXuLy> itemRepository
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
        public virtual IList<DeNghiXuLy> GetAllDeNghiXuLys()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DeNghiXuLy> SearchDeNghiXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string soPhieu = null, DateTime? ngayDeNghi = null, DateTime? ngayXuLy = null, Decimal? donViId = null)
        {
            var query = _itemRepository.Table;
            if (donViId.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            if (!string.IsNullOrEmpty(soPhieu))
                query = query.Where(c => c.SO_PHIEU == soPhieu);
            if (ngayDeNghi != null)
                query = query.Where(c => c.NGAY_DE_NGHI == ngayDeNghi);
            if (ngayXuLy != null)
                query = query.Where(c => c.NGAY_XU_LY == ngayXuLy);
            return new PagedList<DeNghiXuLy>(query, pageIndex, pageSize); ;
        }

        public virtual DeNghiXuLy GetDeNghiXuLyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DeNghiXuLy> GetDeNghiXuLyByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDeNghiXuLy(DeNghiXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDeNghiXuLy(DeNghiXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDeNghiXuLy(DeNghiXuLy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual bool CheckTrungSoPhieuTrongNgay(Decimal ID, String soPhieu, DateTime ngay, Decimal donViId)
        {
            return _itemRepository.Table.Any(c => c.ID == ID && c.SO_PHIEU == soPhieu && c.NGAY_DE_NGHI == ngay && c.DON_VI_ID == donViId);
        }
        #endregion
    }
}

