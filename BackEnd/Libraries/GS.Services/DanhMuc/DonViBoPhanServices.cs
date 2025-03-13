//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class DonViBoPhanService : IDonViBoPhanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DonViBoPhan> _itemRepository;
        private readonly IRepository<NguoiDungDonViMapping> _itemNguoiDungDonViMappingRepository;
        private readonly IRepository<DonVi> _itemDonViRepository;
        #endregion

        #region Ctor

        public DonViBoPhanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DonViBoPhan> itemRepository,
            IRepository<NguoiDungDonViMapping> itemNguoiDungDonViMappingRepository,
            IRepository<DonVi> itemDonViRepository,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._itemNguoiDungDonViMappingRepository = itemNguoiDungDonViMappingRepository;
            this._itemDonViRepository = itemDonViRepository;
            this._workContext = workContext;

        }

        #endregion

        #region Methods

        public virtual IList<DonViBoPhan> GetDonViBoPhans(decimal? DonViId = 0, decimal TreeLevel = 0)
        {
            var query = _itemRepository.Table;
            if (DonViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == DonViId);
            }
            if(TreeLevel>0)
            {
                query = query.Where(c => c.TREE_LEVEL == TreeLevel);
            }    
            return query.OrderBy(c => c.TEN).ToList();
        }

        public virtual IPagedList<DonViBoPhan> SearchDonViBoPhans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? DonViId = 0, decimal? ParentId = 0)
        {
            var query = _itemRepository.Table;
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            if (ParentId > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentId);
            }
            else if (String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.PARENT_ID == null);
            if (DonViId > 0)
                query = query.Where(c => c.DON_VI_ID == DonViId);
            query.OrderBy(c => c.TREE_NODE).ThenBy(c => c.TEN);
            return new PagedList<DonViBoPhan>(query, pageIndex, pageSize);
        }
        public int CountPhongBanSub(decimal Id)
        {
            var query = _itemRepository.Table.Where(c => c.PARENT_ID == Id);
            return query.Count();
        }
        public virtual DonViBoPhan GetDonViBoPhanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DonViBoPhan> GetDonViBoPhanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDonViBoPhan(DonViBoPhan entity, bool isGenCode = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (String.IsNullOrEmpty(entity.MA))
                entity.MA = Guid.NewGuid().ToString();

            _itemRepository.Insert(entity);
            if (isGenCode)
                GenMaDonViBoPhan(entity);
            GenTreeNode(entity);
            _staticCacheManager.Remove(key);
            // đồng bộ danh mục sang kho

            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDonViBoPhan(DonViBoPhan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //GenMaDonViBoPhan(entity);
            GenTreeNode(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDonViBoPhan(DonViBoPhan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove(key);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public bool CheckTenDonViBoPhan(decimal? id = 0, string ten = null)
        {
            var _dv = _workContext.CurrentDonVi;
            return _itemRepository.TableNoTracking.Any(c => c.DON_VI_ID == _dv.ID && c.TEN == ten && c.ID != id);
        }

        public bool CheckMaDonViBoPhan(decimal? donViId, decimal? id = 0, string ma = null)
        {
            var rs = _itemRepository.TableNoTracking.Any(c => c.MA == ma && c.ID != id && c.DON_VI_ID == donViId);
            return rs;
        }

        public void GenTreeNode(DonViBoPhan entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetDonViBoPhanById(entity.PARENT_ID ?? 0);
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
        public void GenMaDonViBoPhan(DonViBoPhan entity)
        {
            var donVi = _itemDonViRepository.Table.Where(c => c.ID == entity.DON_VI_ID.Value).FirstOrDefault();
            entity.MA = CommonHelper.GenMaDonViBoPhan(donVi.MA, entity.ID);
            _itemRepository.Update(entity);

        }
        public IList<DonViBoPhan> GetListDonViBoPhanTreeNodeByRoot(decimal? donViId = 0)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }
        public virtual DonViBoPhan GetDonViBoPhanByMa(string MA)
        {
            if (string.IsNullOrEmpty(MA))
                return null;
            var query = _itemRepository.Table;
            return query.Where(m => m.MA == MA).FirstOrDefault();
        }
        public virtual DonViBoPhan GetDonViBoPhanByTen(string Ten)
        {
            if (string.IsNullOrEmpty(Ten))
                return null;
            var query = _itemRepository.Table;
            return query.Where(m => m.TEN.Equals(Ten)).FirstOrDefault();
        }

        public virtual string GetStringTenDonViBoPhan(List<decimal> Ids)
        {
            if (Ids != null && Ids.Count > 0)
            {
                var q = _itemRepository.Table.Where(p => Ids.Contains(p.ID)).Select(p => p.TEN).ToList();
                if (q != null && q.Count > 0)
                {
                    return string.Join(", ", q);
                }
            }
            return string.Empty;
        }
        #endregion
        #region Cache
        const string key = "DM_DON_VI_BO_PHAN";
        public virtual IList<DonViBoPhan> GetTable()
        {
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.ToList();
            });
        }
        public virtual DonViBoPhan GetCacheDonViBoPhanByMa(string MA)
        {
            if (string.IsNullOrEmpty(MA))
                return null;
            return GetTable().Where(m => m.MA == MA).FirstOrDefault();
        }
        public virtual DonViBoPhan GetCacheDonViBoPhanByTen(string Ten)
        {
            if (string.IsNullOrEmpty(Ten))
                return null;
            return GetTable().Where(m => m.TEN.Equals(Ten)).FirstOrDefault();
        }
        #endregion
        #region read only
        public virtual DonViBoPhan GetReadOnlyDonViBoPhanByMa(string MA)
        {
            if (string.IsNullOrEmpty(MA))
                return null;
            var query = _itemRepository.TableNoTracking;
            return query.Where(m => m.MA == MA).FirstOrDefault();
        }
        public virtual DonViBoPhan GetReadOnlyDonViBoPhanByMaAndDonViId(decimal? donViId = null, string MA = null)
        {
            var query = _itemRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(MA))
                query = query.Where(m => m.MA == MA);
            if (donViId.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            return query.FirstOrDefault();
        }

        #endregion
    }
}

