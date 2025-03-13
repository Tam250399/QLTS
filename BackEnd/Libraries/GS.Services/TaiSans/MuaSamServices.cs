//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
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

namespace GS.Services.TaiSans
{
    public partial class MuaSamService:IMuaSamService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<MuaSam> _itemRepository;
            private readonly IDonViService _donViService;
            private readonly IWorkContext _workContext;
            private readonly IMuaSamChiTietService _muaSamChiTietService;
        #endregion

        #region Ctor

        public MuaSamService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext,
            IDonViService donViService,
            IRepository<MuaSam> itemRepository,
            IMuaSamChiTietService muaSamChiTietService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._donViService = donViService;
            this._muaSamChiTietService = muaSamChiTietService;
        }

        #endregion
        
        #region Methods
        public virtual IList<MuaSam> GetAllMuaSams(){
            var query = _itemRepository.Table;
             return query.ToList();
         }

        public virtual IPagedList<MuaSam> SearchMuaSams(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, DateTime? Tu_ngay = null, DateTime? Den_ngay = null, decimal? trangThaiId = 0, Decimal? donViSD_ID = 0, Decimal? donViQL_ID = 0, Decimal? LoaiTaiSanId = 0, decimal? LoaiHinhTaiSanId = 0)
        {
             var query = _itemRepository.Table.Where(c=>c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);
            if(Tu_ngay.HasValue)
            {
                var _tungay = Tu_ngay.Value.Date;
                query = query.Where(x => x.NGAY_DANG_KY >= _tungay);
            }
            if (Den_ngay.HasValue && Den_ngay != DateTime.MinValue)
            {
                var _denngay = Den_ngay.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_DANG_KY < _denngay);
            }
			if (trangThaiId > 0)
			{
                query = query.Where(x => x.TRANG_THAI_ID == trangThaiId.Value);
            }
			if (donViSD_ID > 0)
			{
                query = query.Where(x => x.DVSDTS_ID == donViSD_ID.Value);
            }
            if (donViQL_ID > 0)
            {
                query = query.Where(x => x.DVLQTS_ID == donViQL_ID.Value);
            }
            //loại tài sản
            if (LoaiTaiSanId != null &&  LoaiTaiSanId > 0)
            {
                var MuaSamIds = _muaSamChiTietService.GetMuaSamChiTiets(LoaiTaiSanId:(decimal)LoaiTaiSanId).Select(c => c.MUA_SAM_ID).ToList();
                query = query.Where(c => MuaSamIds.Contains(c.ID));
            }
            if (LoaiHinhTaiSanId != null && LoaiHinhTaiSanId > 0)
            {
                var MuaSamIds = _muaSamChiTietService.GetMuaSamChiTiets(LoaiHinhTaiSanId: LoaiHinhTaiSanId).Select(c => c.MUA_SAM_ID).ToList();
                query = query.Where(c => MuaSamIds.Contains(c.ID));
            }
            return new PagedList<MuaSam>(query, pageIndex, pageSize);;
         }    
        
        public virtual MuaSam GetMuaSamById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<MuaSam> GetMuaSamByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertMuaSam(MuaSam entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.GUID = Guid.NewGuid();
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateMuaSam(MuaSam entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteMuaSam(MuaSam entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

