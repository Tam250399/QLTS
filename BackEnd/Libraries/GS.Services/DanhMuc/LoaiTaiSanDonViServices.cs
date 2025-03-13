//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GS.Services.DanhMuc
{

    public partial class LoaiTaiSanDonViServices : ILoaiTaiSanDonViServices
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<LoaiTaiSanDonVi> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public LoaiTaiSanDonViServices(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<LoaiTaiSanDonVi> itemRepository,
            IWorkContext workContext,
            IDonViService donViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            _donViService = donViService;
        }

        #endregion

        #region Methods
        public virtual IList<LoaiTaiSanDonVi> GetAllLoaiTaiSanVoHinhs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<LoaiTaiSanDonVi> SearchLoaiTaiSanVoHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? PARENTID = null, decimal? forDonVi = null, decimal? TREELEVEL = 1)
        {
            var query = _itemRepository.Table;
            if (forDonVi != null && forDonVi != 0)
            {
                query = query.Where(c => c.DON_VI_ID == forDonVi || c.DON_VI_ID == null);
            }
            if (!String.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.MA.ToLower().Contains(Keysearch.ToLower()) || c.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            else
            {
                if (PARENTID != 0)
                {
                    query = query.Where(x => x.PARENT_ID == PARENTID);
                }
                else
                {
                    query = query.Where(x => x.PARENT_ID == null);
                }
            }
            query = query.OrderBy(c => c.MA);
            return new PagedList<LoaiTaiSanDonVi>(query, pageIndex, pageSize); ;
        }

        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<LoaiTaiSanDonVi> GetLoaiTaiSanVoHinhByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMa(string ma)
        {
            if (String.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma).FirstOrDefault(); 
        }
        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaAndDonVi(string ma, decimal donViID)//truyền vào dvID
        {
            if (String.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma && c.DON_VI_ID == donViID).FirstOrDefault();
        }
        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaAndDonVi(string ma)
        {
            if (String.IsNullOrEmpty(ma))
                return null;
            return _itemRepository.Table.Where(c => c.MA == ma && (c.DON_VI_ID == _workContext.CurrentDonVi.ID || c.DON_VI_ID == null)).FirstOrDefault();
        }

        public IList<LoaiTaiSanDonVi> GetForInputSearch(string KeySearch, decimal? donViId = 0)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(KeySearch))
            {
                query = query.Where(c => c.TEN.ToLower().Contains(KeySearch.ToLower()) || c.MA.ToLower().Contains(KeySearch.ToLower()));
            }
			if (donViId>0)
			{
                query = query.Where(c => c.DON_VI_ID == donViId || c.DON_VI_ID == null);
			}
            return query.Take(10).ToList();
        }

        public virtual void InsertLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var entityParent = GetLoaiTaiSanVoHinhById(entity.PARENT_ID.Value);
            entity.LOAI_TAI_SAN_ID = entityParent.LOAI_TAI_SAN_ID;
            entity.LOAI_HINH_TAI_SAN_ID = entityParent.LOAI_HINH_TAI_SAN_ID;
            _itemRepository.Insert(entity);
            GenTreeNode(entity);
        }
        public virtual void InsertLoaiTaiSanVoHinh(List<LoaiTaiSanDonVi> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            foreach (var entity in entities)
            {
                var entityParent = GetLoaiTaiSanVoHinhById(entity.PARENT_ID.Value);
                entity.LOAI_TAI_SAN_ID = entityParent.LOAI_TAI_SAN_ID;
                entity.LOAI_HINH_TAI_SAN_ID = entityParent.LOAI_HINH_TAI_SAN_ID;
            }
            _itemRepository.Insert(entities);
            GenTreeNode(entities);
        }
        public virtual void UpdateLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var entityParent = GetLoaiTaiSanVoHinhById(entity.PARENT_ID.Value);
            entity.LOAI_TAI_SAN_ID = entityParent.LOAI_TAI_SAN_ID;
            entity.LOAI_HINH_TAI_SAN_ID = entityParent.LOAI_HINH_TAI_SAN_ID;
            GenTreeNode(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public void GenTreeNode(LoaiTaiSanDonVi entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetLoaiTaiSanVoHinhById(entity.PARENT_ID ?? 0);
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

        public void GenTreeNode(List<LoaiTaiSanDonVi> entities)
        {
            foreach (var entity in entities)
            {
                var parent = GetLoaiTaiSanVoHinhById(entity.PARENT_ID ?? 0);
                entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
                entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
            }
            _itemRepository.Update(entities);
        }

        public IList<LoaiTaiSanDonVi> GetListLoaiTaiSanVoHinhTreeNodeByRoot(decimal? cheDoHaoMonId = 0, decimal? LoaiHinhTaiSanId = 0, Decimal? donViId = 0)
        {
            var query = _itemRepository.Table;
            if (cheDoHaoMonId > 0)
            {
                query = query.Where(c => c.CHE_DO_HAO_MON_ID == cheDoHaoMonId)
                 .OrderBy(c => c.TREE_NODE);
            }
            if (LoaiHinhTaiSanId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSanId);
            }
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId || c.DON_VI_ID == null);
            }
            return query.ToList<LoaiTaiSanDonVi>();
        }
        public int GetCountSub(decimal? ParentId = 0, decimal? donViId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.PARENT_ID == ParentId);
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            return query.Count();
        }
        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByTen(string Ten)
        {
            if (String.IsNullOrEmpty(Ten))
                return null;
            return _itemRepository.Table.Where(c => c.TEN.Equals(Ten)).FirstOrDefault();
        }
        /// <summary>
        /// lấy LoaiTaiSanVoHinh bằng mã loại tài sản
        /// </summary>
        /// <param name="maLts"></param>
        /// <returns></returns>
        public virtual LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaLTS(string maLts)
        {
            if (String.IsNullOrEmpty(maLts))
                return null;
            return _itemRepository.Table.Where(c => c.LoaiTaiSan != null && c.LoaiTaiSan.MA == maLts).FirstOrDefault();
        }
        public virtual IList<LoaiTaiSanDonVi> GetAllLoaiTaiSanVoHinhCons()
        {
            var query = _itemRepository.Table.Where(m => m.TREE_LEVEL > 2);
            return query.ToList();
        }

        public bool CheckLoaiTaiSanDacThu(decimal? donViId =0)
        {
            var donViCapTren = _donViService.GetDonViLonNhat(donViId ?? 0);
            if (donViCapTren != null)
            {

                var listDonViDuocPhepNhapTSDV = _donViService.GetIQueryableAllDonViDuocNhapLoaiTaiSanDonVi().Select(c => c.ID).ToList();
                // nếu đơn vị cấp trên được phép nhập thì mới check
                if (listDonViDuocPhepNhapTSDV.Contains(donViCapTren.ID))
                {
                    return _itemRepository.Table.Any(c => c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU && c.DON_VI_ID == donViCapTren.ID);
                }
            }
                            
            return false;
        }

        #endregion
    }
}

