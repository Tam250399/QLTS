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
using DevExpress.XtraRichEdit.Fields;

namespace GS.Services.TaiSans
{
    public partial class DeNghiXuLyTaiSanService : IDeNghiXuLyTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DeNghiXuLyTaiSan> _itemRepository;
        #endregion

        #region Ctor

        public DeNghiXuLyTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DeNghiXuLyTaiSan> itemRepository
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
        public virtual IList<DeNghiXuLyTaiSan> GetAllDeNghiXuLyTaiSans(Decimal? DeNghiXuLyID = null)
        {
            var query = _itemRepository.Table;
            if (DeNghiXuLyID.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DE_NGHI_XU_LY_ID == DeNghiXuLyID);
            return query.ToList();
        }

        public virtual IPagedList<DeNghiXuLyTaiSan> SearchDeNghiXuLyTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? DE_NGHI_XU_LY_ID = null)
        {
            var query = _itemRepository.TableNoTracking;
            if (DE_NGHI_XU_LY_ID.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DE_NGHI_XU_LY_ID == DE_NGHI_XU_LY_ID);
            return new PagedList<DeNghiXuLyTaiSan>(query.OrderByDescending(c => c.ID), pageIndex, pageSize);
        }

        public virtual DeNghiXuLyTaiSan GetDeNghiXuLyTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DeNghiXuLyTaiSan> GetDeNghiXuLyTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteDeNghiXuLyTaiSan(List<DeNghiXuLyTaiSan> entity)
        {
            if (entity == null || (entity != null && entity.Count == 0))
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        #endregion
    }
}

