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
using GS.Core.Domain.BaoCaoDoiChieus;
namespace GS.Services.BaoCaoDoiChieus
{
    public partial class BaoCaoDoiChieuService : IBaoCaoDoiChieuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<BaoCaoDoiChieu> _itemRepository;
        #endregion

        #region Ctor

        public BaoCaoDoiChieuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<BaoCaoDoiChieu> itemRepository
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
        public virtual IList<BaoCaoDoiChieu> GetAllBaoCaoDoiChieus()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual List<BaoCaoDoiChieu> GetBaoCaoDoiChieu(decimal? donViId = null,decimal? namBaoCao = null,decimal? baoCaoId = null,decimal? heThongId = null)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
                query = query.Where(x => x.DON_VI_ID == donViId);
            if (namBaoCao > 0)
                query = query.Where(x => x.NAM_BAO_CAO == namBaoCao);
            if (baoCaoId.HasValue)
                query = query.Where(x => x.BAO_CAO_ID == baoCaoId);
            if (heThongId.HasValue)
                query = query.Where(x => x.HE_THONG_ID == heThongId);
            return query.ToList();
        }
        public virtual IPagedList<BaoCaoDoiChieu> SearchBaoCaoDoiChieus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal donVi = 0)
        {
            var query = _itemRepository.Table;
            //if (!String.IsNullOrEmpty(Keysearch))
            //{
            //    Keysearch = Keysearch.ToUpper();
            //    query = query.Where(c => c.TenFile.ToUpper().Contains(Keysearch));
            //}
            if (donviId > 0 && donviId != 99284)
            {
                query = query.Where(c => c.DON_VI_ID == donviId);

            }
            if (namBaoCao > 0)
                query = query.Where(p => p.NAM_BAO_CAO == namBaoCao);
            if (donVi > 0)
                query = query.Where(p => p.DON_VI_ID == donVi);
            return new PagedList<BaoCaoDoiChieu>(query, pageIndex, pageSize);
        }

        public virtual BaoCaoDoiChieu GetBaoCaoDoiChieuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<BaoCaoDoiChieu> GetBaoCaoDoiChieuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertBaoCaoDoiChieu(BaoCaoDoiChieu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateBaoCaoDoiChieu(BaoCaoDoiChieu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteBaoCaoDoiChieu(BaoCaoDoiChieu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteBaoCaoDoiChieu(List<BaoCaoDoiChieu> entity)
        {
            if (entity == null||(entity!=null&&entity.Count==0))
                throw new ArgumentNullException(nameof(BaoCaoDoiChieu));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual BaoCaoDoiChieu GetBaoCaoDoiChieuBySeachModel(Decimal? donViId = 0, Decimal? baoCaoId = 0, Decimal? heThongId = 0, Decimal? nam = 0)
        {
            var item = _itemRepository.Table.Where(c => c.BAO_CAO_ID == baoCaoId && c.DON_VI_ID == donViId && c.HE_THONG_ID == heThongId && c.NAM_BAO_CAO == nam).FirstOrDefault();
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            return item;
        }
        public virtual BaoCaoDoiChieu GetBaoCaoDoiChieu1ABySeachModel(Decimal? donViId = 0, Decimal? heThongId = 0, Decimal? nam = 0)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            if (heThongId > 0)
            {
                query = query.Where(c => c.HE_THONG_ID == heThongId);
            }
            if (nam > 0)
            {
                query = query.Where(c => c.NAM_BAO_CAO == nam);
            }
            return query.FirstOrDefault();
        }
        #endregion
    }
}

