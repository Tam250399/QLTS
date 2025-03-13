//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
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
using GS.Core.Domain.TaiSans;
using GS.Services.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.TaiSans
{
    public partial class TaiSanChoThueService : ITaiSanChoThueService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanChoThue> _itemRepository;
        private readonly IRepository<TaiSan> _itemTaiSanRepository;
        private readonly IWorkContext _workContext;
        private readonly ILoaiDonViService _loaiDonViService;
        #endregion

        #region Ctor

        public TaiSanChoThueService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanChoThue> itemRepository,
            IRepository<TaiSan> itemTaiSanRepository,
            IWorkContext workContext,
            ILoaiDonViService loaiDonViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._loaiDonViService = loaiDonViService;
            this._itemTaiSanRepository = itemTaiSanRepository;
        }

        #endregion

        #region Methods
        public virtual IList<TaiSanChoThue> GetAllTaiSanChoThues()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<TaiSanChoThue> SearchTaiSanChoThues(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal donViId = 0)
        {
            var query = _itemRepository.Table;

            if (!String.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.taisan.MA.Contains(Keysearch) || c.taisan.TEN.Contains(Keysearch));
            }
            if(donViId > 0)
            {
                query = from q in query
                        join taiSan in _itemTaiSanRepository.Table
                            on q.TAI_SAN_ID equals taiSan.ID
                        where taiSan.DON_VI_ID == donViId
                        select q;
            }
            return new PagedList<TaiSanChoThue>(query, pageIndex, pageSize); ;
        }

        public virtual TaiSanChoThue GetTaiSanChoThueById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual IList<TaiSanChoThue> GetTaiSanChoThueByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanChoThue(TaiSanChoThue entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanChoThue(TaiSanChoThue entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_UPDATE = DateTime.Now;
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanChoThue(TaiSanChoThue entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual TaiSanChoThue GetTaiSanChoThueByTaiSanIdandNgaychoThue(decimal taiSanId, DateTime? NgayChoThue)
        {
                var query = _itemRepository.Table.Where(x => x.TAI_SAN_ID == taiSanId );
            if(NgayChoThue!=null)
            {

                query = query.Where(x => x.NGAY_CHO_THUE_FROM <= NgayChoThue && NgayChoThue <= x.NGAY_CHO_THUE_TO);
            }
            return query.FirstOrDefault();
        }
        public virtual bool CheckLoaiDonViToChuc(decimal LoaiDonViId)
        {
            var ListMaCanhBao = new List<int> { (int)EnumMaLoaiDonViToChuc.ChinhTriXaHoiNgheNghiep,
                                                    (int)EnumMaLoaiDonViToChuc.XaHoiNgheNghiep,
                                                     (int)EnumMaLoaiDonViToChuc.ToChucKhac,
                                                     (int)EnumMaLoaiDonViToChuc.XaHoi

                                                 };
            var ListIdCanhBao = new List<decimal>();
            foreach (var item in ListMaCanhBao)
            {
                decimal? id = _loaiDonViService.GetLoaiDonViByMa(item.ToString())?.ID;
                if (id != null || id != 0)
                {
                    ListIdCanhBao.Add(id??0);
                }               
            }
            if (ListIdCanhBao.Contains(LoaiDonViId))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}

