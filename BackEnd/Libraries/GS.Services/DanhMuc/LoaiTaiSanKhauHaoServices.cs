//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
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
using Castle.Core.Internal;

namespace GS.Services.DanhMuc
{
    public partial class LoaiTaiSanKhauHaoService:ILoaiTaiSanKhauHaoService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<LoaiTaiSanKhauHao> _itemRepository;
            private readonly IRepository<LoaiTaiSan> _loaiTaiSanRepository;
        private readonly IRepository<DonVi> _donViRepository;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public LoaiTaiSanKhauHaoService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<LoaiTaiSanKhauHao> itemRepository,
            IRepository<LoaiTaiSan> loaiTaiSanRepository,
            IRepository<DonVi> donViRepository,
            IDonViService donViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._loaiTaiSanRepository = loaiTaiSanRepository;
            _donViRepository = donViRepository;
            _donViService = donViService;
        }

        #endregion
        
        #region Methods
        public virtual IList<LoaiTaiSanKhauHao> GetAllLoaiTaiSanKhauHaos(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <LoaiTaiSan> SearchLoaiTaiSanKhauHaos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0)
        {
            var query = _loaiTaiSanRepository.Table;
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
            if (ChedoId > 0)
            {
                query = query.Where(c => c.CHE_DO_HAO_MON_ID == ChedoId);
            }
            if (loaiHinhTaiSanId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId);
            }
            query = query.OrderBy(c => c.MA);
            var queryltskh = _itemRepository.Table;
            
            return new PagedList<LoaiTaiSan>(query, pageIndex, pageSize);
        }    
        
        public virtual LoaiTaiSanKhauHao GetLoaiTaiSanKhauHaoById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<LoaiTaiSanKhauHao> GetLoaiTaiSanKhauHaoByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        //public LoaiTaiSanKhauHao GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(decimal? loaiTaiSanId = 0, decimal? donViId = 0)
        //{
        //    if (loaiTaiSanId == 0 || donViId == 0)
        //        return null;
        //    else
        //    {
        //        var itemLoaiTaiSanKH = _itemRepository.Table.Where(c => c.LOAI_TAI_SAN_ID == loaiTaiSanId && c.DON_VI_ID == donViId).FirstOrDefault();
        //        return itemLoaiTaiSanKH;
        //    }
        //} 
        public LoaiTaiSanKhauHao GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(decimal? loaiTaiSanId = 0, decimal? donViId = 0)
        {
            if (loaiTaiSanId == 0 || donViId == 0)
                return null;
            else
            {
                var itemLoaiTaiSanKH = _itemRepository.Table.Where(c => c.LOAI_TAI_SAN_ID == loaiTaiSanId && (c.DON_VI_ID == donViId));
                if(itemLoaiTaiSanKH.IsNullOrEmpty())
                {
                    var donViLonNhat = _donViService.GetDonViLonNhat(donViId ?? 0);
                    itemLoaiTaiSanKH = _itemRepository.Table
                                            .Where(c => c.LOAI_TAI_SAN_ID == loaiTaiSanId && (c.DON_VI_ID == donViId || c.DON_VI_ID == donViLonNhat.ID));
                }
                if (itemLoaiTaiSanKH.Count() == 0)
                    return null;
                return itemLoaiTaiSanKH.FirstOrDefault();
            }
        }
        #endregion
    }
}		

