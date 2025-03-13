//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
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
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial class DinhMucService : IDinhMucService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DinhMuc> _itemRepository;
        #endregion

        #region Ctor

        public DinhMucService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DinhMuc> itemRepository
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
        public virtual IList<DinhMuc> GetAllDinhMucs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DinhMuc> SearchDinhMucs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? donViID = 0, DateTime? TuNgay = null, DateTime? DenNgay = null, string SoQuyetDinh = null, DateTime? NgayQuyetDinh = null)
        {
            var query = _itemRepository.Table;
            if (donViID > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViID || !c.DON_VI_ID.HasValue);
            }
            if (!string.IsNullOrEmpty(SoQuyetDinh))
            {
                query = query.Where(c => c.QUYET_DINH_SO.ToLower().Contains(SoQuyetDinh.ToLower()));
            }
            if (TuNgay.HasValue)
            {
                query = query.Where(c => c.TU_NGAY >= TuNgay);
            }
            if (DenNgay.HasValue)
            {
                query = query.Where(c => c.DEN_NGAY <= DenNgay);
            }
            if (NgayQuyetDinh.HasValue)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY >= NgayQuyetDinh);
            }
            query = query.OrderByDescending(c => c.QUYET_DINH_NGAY).ThenBy(c => c.MA);
            return new PagedList<DinhMuc>(query, pageIndex, pageSize); ;
        }

        public virtual DinhMuc GetDinhMucById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DinhMuc> GetDinhMucByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual IList<DinhMuc> GetListDinhMucByDonViIds(decimal? donviId = 0)
        {
            var query = _itemRepository.Table;
            return query.Where(c => c.DON_VI_ID == donviId || !c.DON_VI_ID.HasValue).ToList();
        }
        public virtual void InsertDinhMuc(DinhMuc entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            GenMa(entity);
        }
        public virtual void UpdateDinhMuc(DinhMuc entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);   
        }
        public virtual void DeleteDinhMuc(DinhMuc entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual DinhMuc CheckMaDinhMuc(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
            {
                return null;
            }
            else
            {
                return _itemRepository.Table.Where(x => x.MA == Ma).FirstOrDefault();
            }
        }
        public virtual DinhMuc CheckSoQuyetDinhDinhMuc(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
            {
                return null;
            }
            else
            {
                return _itemRepository.Table.Where(x => x.QUYET_DINH_SO == Ma).FirstOrDefault();
            }
        }
        public void GenMa(DinhMuc entity)
        {
            entity.MA = CommonHelper.GenTreeNode(null, entity.ID, null);
            _itemRepository.Update(entity);
        }
        #endregion
    }
}

