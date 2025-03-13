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
using GS.Core.Domain.DMDC;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class DiaBanServicever2 
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DMDC_DiaBan> _itemRepository;
        #endregion

        #region Ctor

        public DiaBanServicever2(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DMDC_DiaBan> itemRepository
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

        public virtual IList<DMDC_DiaBan> GetDiaBans(decimal? CapDiaban = 0, decimal? ParentId = 0, decimal? QuocGiaId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.HIEU_LUC == true);
            if (CapDiaban > 0)
            {
                query = query.Where(c => c.LOAI == CapDiaban);
            }
            //if (QuocGiaId > 0)
            //{
            //    query = query.Where(c => c. == QuocGiaId);
            //}
            //khi load dl ban đầu cho tỉnh thì cần lấy hết và ko cần ParentId
            if (CapDiaban == (int)enumLoaiDiaBanHanhChinh.Tinh)
            {
                if (ParentId > 0)
                {
                    query = query.Where(c => c.ID_CHA == ParentId);
                }
            }
            else
            {
                if (ParentId > 0)
                {
                    query = query.Where(c => c.ID_CHA == ParentId);
                }
            }
            return query.ToList();
        }
        public virtual IList<DMDC_DiaBan> GetDiaBansChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.ID == 0);
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }
        public virtual IPagedList<DMDC_DiaBan> SearchDiaBans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA_DB.ToUpper().Contains(Keysearch));
            }

            if (ParentId > 0)
            {
                query = query.Where(c => c.ID_CHA == ParentId);
            }
            else if (String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.ID_CHA == null);
            return new PagedList<DMDC_DiaBan>(query, pageIndex, pageSize); ;
        }
        public int GetCountDiaBanSub(decimal ParentId)
        {
            var query = _itemRepository.Table.Where(c => c.HIEU_LUC)
                                             .Where(c => c.ID_CHA == ParentId);
            return query.Count();
        }
        public virtual IList<DMDC_DiaBan> GetDiaBansForInputSearch(string tenDiaBan = null)
        {

            var query = _itemRepository.Table.Where(c => c.HIEU_LUC);
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
        public virtual DMDC_DiaBan GetDiaBanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual DMDC_DiaBan GetDiaBanByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA_DB == Ma).FirstOrDefault();
        }
        public virtual IList<DMDC_DiaBan> GetDiaBanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual void InsertDiaBan(DMDC_DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            UpdateDiaBan(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDiaBan(DMDC_DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            UpdateTreeNode(entity);
            _itemRepository.Update(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void InsertDiaBan(List<DMDC_DiaBan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            UpdateDiaBan(entities);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDiaBan(List<DMDC_DiaBan> entities)
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
        public virtual void DeleteDiaBan(DMDC_DiaBan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove("GET_ALL_DIA_BAN");
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public void UpdateTreeNode(DMDC_DiaBan item)
        {
            var node = item.ID.toCode(7);
            item.TREE_NODE = node;
            if (item.ID_CHA > 0)
            {
                var DiabanParent = GetDiaBanById((decimal)item.ID_CHA);
                item.TREE_NODE = DiabanParent.TREE_NODE + "-" + item.TREE_NODE;
                item.TREE_LEVEL = DiabanParent.TREE_LEVEL + 1;
            }
        }
        #endregion
        #region Cache
        const string key = "DM_DIA_BAN";
        public virtual IList<DMDC_DiaBan> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual DMDC_DiaBan GetCacheDiaBanByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
                return null;
            return GetTable().Where(c => c.MA_DB == Ma).FirstOrDefault();
        }
        #endregion
    }
}

