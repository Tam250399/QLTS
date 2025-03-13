//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class DiaBanService : IDiaBanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DiaBan> _itemRepository;
        #endregion

        #region Ctor

        public DiaBanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DiaBan> itemRepository
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

        public virtual IList<DiaBan> GetDiaBans(decimal? CapDiaban = 0, decimal? ParentId = 0, decimal? QuocGiaId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_DIABAN.KHADUNG);
            if (CapDiaban > 0)
            {
                query = query.Where(c => c.LOAI_DIA_BAN_ID == CapDiaban);
            }
            if (QuocGiaId > 0)
            {
                query = query.Where(c => c.QUOC_GIA_ID == QuocGiaId);
            }
            //khi load dl ban đầu cho tỉnh thì cần lấy hết và ko cần ParentId
            if (CapDiaban == (int)enumLOAI_DIABAN.TINH)
            {
                if (ParentId > 0)
                {
                    query = query.Where(c => c.PARENT_ID == ParentId);
                }
            }
            else
            {
                if (ParentId > 0)
                {
                    query = query.Where(c => c.PARENT_ID == ParentId);
                }
            }
            return query.ToList();
        }
        public virtual IList<DiaBan> GetDiaBansChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }
        public virtual IPagedList<DiaBan> SearchDiaBans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }

            if (ParentId > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentId);
            }
            else if (String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.PARENT_ID == null);
            return new PagedList<DiaBan>(query, pageIndex, pageSize); ;
        }
        public int GetCountDiaBanSub(decimal ParentId)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_DIABAN.KHADUNG)
                                             .Where(c => c.PARENT_ID == ParentId);
            return query.Count();
        }
        public virtual IList<DiaBan> GetDiaBansForInputSearch(string tenDiaBan = null)
        {

            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_DIABAN.KHADUNG);
            if (!String.IsNullOrEmpty(tenDiaBan))
            {
                tenDiaBan = tenDiaBan.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(tenDiaBan)).Take(10);
            }
            if (String.IsNullOrEmpty(tenDiaBan))
            {
                query = query.Take(10);
            }
            return query.ToList();
        }
        public virtual DiaBan GetDiaBanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual DiaBan GetDiaBanByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == Ma).FirstOrDefault();
        }
        public virtual IList<DiaBan> GetDiaBanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual void InsertDiaBan(DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            UpdateDiaBan(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDiaBan(DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            UpdateTreeNode(entity);
            _itemRepository.Update(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void InsertDiaBan(List<DiaBan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            UpdateDiaBan(entities);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDiaBan(List<DiaBan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
                UpdateTreeNode(entity);
            _itemRepository.Update(entities);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");

            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDiaBan(DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public void UpdateTreeNode(DiaBan item)
        {
            var node = item.ID.toCode(7);
            item.TREE_NODE = node;
            if (item.PARENT_ID > 0)
            {
                var DiabanParent = GetDiaBanById((decimal)item.PARENT_ID);
                item.TREE_NODE = DiabanParent.TREE_NODE + "-" + item.TREE_NODE;
                item.TREE_LEVEL = DiabanParent.TREE_LEVEL + 1;
            }
        }
        #endregion
        #region Cache
        const string key = "DM_DIA_BAN";
        public virtual IList<DiaBan> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual DiaBan GetCacheDiaBanByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
                return null;
            return GetTable().Where(c => c.MA == Ma).FirstOrDefault();
        }
        #endregion
    }
}

