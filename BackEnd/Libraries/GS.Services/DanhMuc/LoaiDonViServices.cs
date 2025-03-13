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
    public partial class LoaiDonViService : ILoaiDonViService
    {
        const string key = "DM_LOAI_DON_VI";

        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<LoaiDonVi> _itemRepository;
        #endregion

        #region Ctor

        public LoaiDonViService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<LoaiDonVi> itemRepository
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
        public virtual IList<LoaiDonVi> GetAllLoaiDonVis()
        {
            var lstLoaiDonVi = _cacheManager.Get(key, () =>
            {
                var queryCache = from l in _itemRepository.Table
                                 select l;
                return queryCache.OrderBy(s => s.TREE_NODE).ToList();
            });
            return lstLoaiDonVi;
        }
        public virtual IList<LoaiDonVi> GetAllLoaiDonVisChuaDb()
        {
            var lstLoaiDonVi = _itemRepository.Table.Where(c => c.DB_ID == null);
            return lstLoaiDonVi.OrderBy(s => s.TREE_NODE).ToList();
        }
        public virtual IList<LoaiDonVi> GetLoaiDonViForBaoCao()
        {
            var lstLoaiDonVi = _cacheManager.Get(key, () =>
            {
                var queryCache = from l in _itemRepository.Table
                                 select l;
                return queryCache.OrderBy(s => s.TREE_NODE).ToList();
            });
            return lstLoaiDonVi;
        }

        public virtual IPagedList<LoaiDonVi> SearchLoaiDonVis(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal ParentID = 0, decimal? TreeLevel = 1)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper()));
            else if (TreeLevel > 0)
                query = query.Where(c => c.TREE_LEVEL == TreeLevel);
            if (ParentID > 0)
                query = query.Where(c => c.PARENT_ID == ParentID);

            query = query.OrderBy(c => c.SAP_XEP);
            return new PagedList<LoaiDonVi>(query, pageIndex, pageSize);
        }

        public virtual LoaiDonVi GetLoaiDonViById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<LoaiDonVi> GetLoaiDonViByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public bool CheckMaLoaiDonVi(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id);
        }


        public virtual void InsertLoaiDonVi(LoaiDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateLoaiDonVi(LoaiDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLoaiDonVi(LoaiDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public int GetCountLoaiDonViSub(decimal ParentID = 0, string KeySearch = null)
        {
            var query = _itemRepository.Table;
            if (ParentID > 0)
                query = query.Where(c => c.PARENT_ID == ParentID);
            return query.Count();
        }

        public IList<LoaiDonVi> GetListDonViByTreeNode(decimal? id = 0)
        {
            var donViCha = GetLoaiDonViById(id ?? 0);
            if (donViCha == null)
                return new List<LoaiDonVi>();
            var query = _itemRepository.Table;
            query = query.Where(c => c.TREE_NODE.Contains(donViCha.TREE_NODE))
                         .OrderBy(c => c.TREE_NODE);
            return query.ToList();
        }

        public void GenTreeNode(LoaiDonVi entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetLoaiDonViById(entity.PARENT_ID ?? 0);
                entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
                entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
                _itemRepository.Update(entity);
            }
            else
            {
                entity.TREE_LEVEL = 1;
                entity.TREE_NODE = CommonHelper.GenTreeNode(null, entity.ID, null);
                _itemRepository.Update(entity);
            }
        }
        public LoaiDonVi GetLoaiDonViByMa(string Ma = null)
        {
            if (string.IsNullOrEmpty(Ma))
                return null;
            else
            {
                var query = _itemRepository.Table;
                return query.Where(m => m.MA == Ma).FirstOrDefault();

            }
        }
        public virtual void InsertListLoaiDonVi(List<LoaiDonVi> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            UpdateListLoaiDonVi(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateListLoaiDonVi(List<LoaiDonVi> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
                if (entity.PARENT_ID != null)
                {
                    var parent = GetLoaiDonViById(entity.PARENT_ID.Value);
                    entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
                    entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
                }
                else
                {
                    entity.TREE_NODE = CommonHelper.GenTreeNode(null, entity.ID, null);
                    entity.TREE_LEVEL = 1;
                }
            _itemRepository.Update(entities);
        }
        public virtual LoaiDonVi GetLoaiDonViByIdDb(int ID_DB = 0)
        {
            if (ID_DB == 0)
                return null;
            return _itemRepository.Table.Where(m => m.DB_ID == ID_DB).FirstOrDefault();
        }
		public virtual string GetTenLoaiDonViByIds(IList<decimal> ids)
		{
			if (ids== null || ids.Count ==0)
				return null;
			var query =_itemRepository.Table.Where(p => ids.Contains(p.ID)).Select(p=>p.TEN);
			return string.Join(", ", query);
		}
		#endregion
	}
}

