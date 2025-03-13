//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
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
using GS.Core.Domain.DM;

namespace GS.Services.DM
{
    public partial class LoaiLyDoBienDongService : ILoaiLyDoBienDongService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<LoaiLyDoBienDong> _itemRepository;
        #endregion

        #region Ctor

        public LoaiLyDoBienDongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<LoaiLyDoBienDong> itemRepository
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
        public virtual IList<LoaiLyDoBienDong> GetAllLoaiLyDoBienDongs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual IList<LoaiLyDoBienDong> GetAllLoaiLyDoBienDongsChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }

        public virtual IPagedList<LoaiLyDoBienDong> SearchLoaiLyDoBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<LoaiLyDoBienDong>(query, pageIndex, pageSize); ;
        }

        public virtual LoaiLyDoBienDong GetLoaiLyDoBienDongById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual LoaiLyDoBienDong GetLoaiLyDoBienDongByMa(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.FirstOrDefault(p => p.MA == ma);
        }

        public virtual IList<LoaiLyDoBienDong> GetLoaiLyDoBienDongByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertLoaiLyDoBienDong(LoaiLyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateLoaiLyDoBienDong(LoaiLyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLoaiLyDoBienDong(LoaiLyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual IQueryable<LoaiLyDoBienDong> GetIQueryableLoaiLyDoBienDongByParent(decimal ParentId, string ParentTreeNode = "")
        {
            if (string.IsNullOrEmpty(ParentTreeNode))
            {
                var parentObject = GetLoaiLyDoBienDongById(ParentId);
                if (parentObject == null)
                    return null;
                ParentTreeNode = parentObject.TREE_NODE;
            }
            var list = _itemRepository.Table.Where(p => p.TREE_NODE.Contains(ParentTreeNode + "-") || p.TREE_NODE == ParentTreeNode);
            return list;
        }
        #endregion
    }
}

