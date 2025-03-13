using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Services.TaiSans
{
    public class TaiSanHienTrangSuDungService : ITaiSanHienTrangSuDungService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanHienTrangSuDung> _itemRepository;
        #endregion

        #region Ctor

        public TaiSanHienTrangSuDungService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanHienTrangSuDung> itemRepository
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
        public virtual IList<TaiSanHienTrangSuDung> GetAllTaiSanHienTrangSuDungs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<TaiSanHienTrangSuDung> SearchTaiSanHienTrangSuDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<TaiSanHienTrangSuDung>(query, pageIndex, pageSize); ;
        }

        public List<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByBienDongId(decimal BienDongId)
        {
            var query = _itemRepository.Table;
            query = query.Where(c => c.BIEN_DONG_ID == BienDongId);
            return query.ToList();
        }

        public virtual TaiSanHienTrangSuDung GetTaiSanHienTrangSuDungById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void InsertTaiSanHienTrangSuDungs(List<TaiSanHienTrangSuDung> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
        }
        public virtual void UpdateTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteTaiSanHienTrangSuDungs(IList<TaiSanHienTrangSuDung> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Delete(entities);
        }
        public List<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByTaiSanId(decimal taiSanId)
        {
            if (taiSanId == 0)
                return null;
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId).ToList();
        }
        public virtual IList<TaiSanHienTrangSuDung> GetHienTrangSuDungs(decimal TaiSanId = 0, decimal BienDongId = 0)
        {
            if (TaiSanId == 0 && BienDongId == 0)
            {
                return null;
            }
            var query = _itemRepository.Table;
            return query.Where(m => m.TAI_SAN_ID == TaiSanId && m.BIEN_DONG_ID == BienDongId).ToList();
        }
        #endregion
    }
}
