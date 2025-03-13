//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
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

namespace GS.Services.DanhMuc
{
    public partial class NhomCongCuService : INhomCongCuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NhomCongCu> _itemRepository;
        private readonly IDonViService _donViService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public NhomCongCuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NhomCongCu> itemRepository,
            IDonViService donViService,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donViService = donViService;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<NhomCongCu> GetAllNhomCongCus(int DonViId = 0)
        {
            var query = _itemRepository.Table;
            if (DonViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == DonViId);
            }
            return query.ToList();
        }

        public virtual IPagedList<NhomCongCu> SearchNhomCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal donViId = 0)
        {
            var query = _itemRepository.Table;

            if (donViId > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            if (ParentId > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentId);
            }
            else if (String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.PARENT_ID == null);

            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                query = query.Where(p => p.TEN.ToUpper().Contains(Keysearch) || p.MA.ToUpper().Contains(Keysearch));
            }
            return new PagedList<NhomCongCu>(query.OrderBy(c => c.MA).ThenBy(c => c.TREE_NODE), pageIndex, pageSize); ;
        }

        public virtual NhomCongCu GetNhomCongCuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual NhomCongCu GetNhomCongCuByMa(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma).FirstOrDefault();
        }

        public virtual NhomCongCu GetNhomCongCu(decimal donViId, string ten = null, string ma = null)
        {
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == donViId);
            if (!string.IsNullOrEmpty(ten))
                query = query.Where(c => c.TEN == ten);
            if (!string.IsNullOrEmpty(ma))
                query = query.Where(c => c.MA == ma);
            return query.FirstOrDefault();
        }

        public virtual IList<NhomCongCu> GetNhomCongCuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<NhomCongCu> GetNhomCongCus(decimal? donViId = 0, List<decimal> list_exceptID = null, decimal? ParentId = 0)
        {
            var query = _itemRepository.Table;
            if (donViId > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            if (list_exceptID != null && list_exceptID.Count > 0)
                query = query.Where(c => !list_exceptID.Contains(c.ID));
            if (ParentId > 0)
                query = query.Where(c => c.PARENT_ID == ParentId);
            return query.ToList();
        }


        public virtual void InsertNhomCongCu(NhomCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PARENT_ID == 0)
                entity.PARENT_ID = null;
            if (entity.DON_VI_ID.GetValueOrDefault(0) == 0)
                entity.DON_VI_ID = _workContext.CurrentDonVi.ID;
            _itemRepository.Insert(entity);

            //sinh mã
            if (string.IsNullOrEmpty(entity.MA))
            {
                var donvi = _donViService.GetDonViById(entity.DON_VI_ID ?? 0);
                if (donvi != null)
                    entity.MA = donvi.MA + "-" + entity.ID;
            }
            //gentreenode
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNhomCongCu(NhomCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PARENT_ID == 0)
                entity.PARENT_ID = null;
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNhomCongCu(NhomCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public int CountNhomCongCuSub(decimal Id)
        {
            //var query = _itemRepository.Table.Where(c => c.PARENT_ID == Id);
            //return query.Count();
            return _itemRepository.Table.Count(c => c.PARENT_ID == Id);
        }

        public void GenTreeNode(NhomCongCu entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetNhomCongCuById(entity.PARENT_ID ?? 0);
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
        public virtual string GetStringNhomCongCu(List<decimal> Ids)
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
        #region Read only
        public virtual NhomCongCu GetReadOnlyNhomCongCu(decimal? donViId, string ten = null, string ma = null)
        {
            var query = _itemRepository.TableNoTracking;
            if (donViId > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            if (!string.IsNullOrEmpty(ten))
                query = query.Where(c => c.TEN == ten);
            if (!string.IsNullOrEmpty(ma))
                query = query.Where(c => c.MA == ma);
            return query.FirstOrDefault();
        }
        #endregion
    }
}

