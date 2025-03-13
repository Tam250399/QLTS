//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Core.Domain.SHTD;

namespace GS.Services.DanhMuc
{
    public partial class NguonGocTaiSanService : INguonGocTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NguonGocTaiSan> _itemRepository;
        private readonly IRepository<QuyetDinhTichThu> _quyetDinhTichThuRepository;
        #endregion

        #region Ctor

        public NguonGocTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NguonGocTaiSan> itemRepository,
            IRepository<QuyetDinhTichThu> quyetDinhTichThuRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._quyetDinhTichThuRepository = quyetDinhTichThuRepository;
        }

        #endregion
        const string key = "DM_NGUON_GOC_TAI_SAN";
        #region Methods
        public virtual IList<NguonGocTaiSan> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual IList<NguonGocTaiSan> GetAllNguonGocTaiSans()
        {
            var query = GetTable();
            return query.ToList();
        }
        public virtual IList<NguonGocTaiSan> GetAllNguonGocTaiSansChuaDb()
        {
            var query = _itemRepository.Table.Where(c=>c.DB_ID==null);
            return query.OrderBy(c=>c.TREE_NODE).ToList();
        }
        /// <summary>
        /// check xem có con hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetCount(decimal id)
        {
            if(id ==0)
            {
                return 0;
            }
            var query = GetTable().AsQueryable<NguonGocTaiSan>().Where(c=>c.PARENT_ID == id).Select(c=>c.ID).Count();
            return query;
        }
        /// <summary>
        /// check xem có con hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckMaNguonGocTaiSan(decimal? id = 0, string ma = null)
        {
            return GetTable().Any(c => c.MA == ma && c.ID != id);
        }
        public virtual Boolean CheckParent(decimal id)
        {
            if (id == 0)
            {
                return false;
            }
            var query = GetTable().AsQueryable<NguonGocTaiSan>().Where(c => c.PARENT_ID == id).Select(c => c.ID).Count();
            if(query > 0)
            {
                return true;
            }
            return false;
        }
        public virtual IList<NguonGocTaiSan> GetNguonGocTaiSans(string Ma = null,string TreeNode = null)
        {
            var query = GetTable().AsQueryable<NguonGocTaiSan>();
            if (!String.IsNullOrWhiteSpace(Ma))
            {
                query = query.Where(c => c.MA == Ma.ToUpper());
            }
            if (!String.IsNullOrWhiteSpace(TreeNode))
            {
                query = query.Where(c => c.TREE_NODE.Contains(TreeNode));
            }           
            return query.ToList();
        }
        public virtual IList<NguonGocTaiSan> GetNguonGocTaiSansForDDL(bool isTichThu = false)
        {
            var query = GetTable().AsQueryable<NguonGocTaiSan>();
            var parent = _itemRepository.Table.Where(c => c.MA == enumMaLoaiQuyetDinh.TichThu).FirstOrDefault();
            if (parent != null)
            {
                var childs = _itemRepository.Table.Where(c => c.TREE_NODE.Contains(parent.TREE_NODE)).Select(c => c.ID).ToList();
                if (isTichThu)
                {
                    query = query.Where(c => childs.Contains(c.ID));
                }
                else
                {
                    query = query.Where(c => !childs.Contains(c.ID));
                }
            } 
            return query.ToList();
        }
        public virtual IPagedList<NguonGocTaiSan> SearchNguonGocTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = null)
        {

            var query = _itemRepository.Table;
			if (ParentId > 0)
			{
				query = query.Where(c => c.PARENT_ID == ParentId);
			}
			else if (String.IsNullOrEmpty(Keysearch))
				query = query.Where(c => c.PARENT_ID == null);
			if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
                
            }
            return new PagedList<NguonGocTaiSan>(query, pageIndex, pageSize); ;
        }

        public virtual NguonGocTaiSan GetNguonGocTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<NguonGocTaiSan> GetNguonGocTaiSanByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertNguonGocTaiSan(NguonGocTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            _staticCacheManager.Remove(key);

        }
        public virtual void UpdateNguonGocTaiSan(NguonGocTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);    
            _staticCacheManager.Remove(key);
        }
        public virtual void DeleteNguonGocTaiSan(NguonGocTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            _staticCacheManager.Remove(key);
        }
        public virtual NguonGocTaiSan GetNguonGocTaiSanByMa(string Ma = null)
        {
            if (string.IsNullOrEmpty(Ma))
                return null;
            var query = GetTable();
            return query.Where(m => m.MA == Ma).FirstOrDefault();

        }
        public virtual void UpdateListNguonGocTaiSan(List<NguonGocTaiSan> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
            _staticCacheManager.Remove(key);
        }
        public virtual void InsertListNguonGocTaiSan(List<NguonGocTaiSan> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            foreach(var entity in entities)
            {
                GenTreeNode(entity);
            }
            _staticCacheManager.Remove(key);
        }
        public virtual NguonGocTaiSan GetNguonGocTaiSanByDbID(int DB_PARENT_ID = 0)
        {
            if (DB_PARENT_ID == 0)
                return null;
            var query = GetTable();
            return query.Where(m => m.DB_ID == DB_PARENT_ID).FirstOrDefault();
        }

        public void GenTreeNode(NguonGocTaiSan entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetNguonGocTaiSanById(entity.PARENT_ID ?? 0);
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
		public int GetCountSub(decimal? ParentId = 0)
		{
            var query = _itemRepository.Table.Where(c => c.PARENT_ID == ParentId);
            return query.Count();
        }
        public bool GetUsed(decimal? Id = 0) 
        {
            return _quyetDinhTichThuRepository.Table.Any(c => c.NGUON_GOC_ID == Id);           
        }
        public bool checkIsTaiSanTichThu(decimal? nguonGocId)
		{
			var nguonGocTichThu= GetNguonGocTaiSanByMa("002");
			var nguonGoc = GetNguonGocTaiSanById(nguonGocId.GetValueOrDefault());
			if (nguonGoc!= null && (nguonGoc.TREE_NODE == nguonGocTichThu.TREE_NODE || nguonGoc.TREE_NODE.StartsWith(nguonGocTichThu.TREE_NODE+"-")))
			{
				return true;
			}
			return false;
		}
		#endregion
	}
}

