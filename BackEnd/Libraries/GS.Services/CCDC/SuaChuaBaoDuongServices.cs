//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
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
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial class SuaChuaBaoDuongService:ISuaChuaBaoDuongService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IWorkContext _workContext;
            private readonly IRepository<SuaChuaBaoDuong> _itemRepository;
            private readonly IXuatNhapService _xuatNhapService;
        #endregion

        #region Ctor

        public SuaChuaBaoDuongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext,
            IRepository<SuaChuaBaoDuong> itemRepository,
            IXuatNhapService xuatNhapService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._xuatNhapService = xuatNhapService;
        }

        #endregion
        
        #region Methods
        public virtual IList<SuaChuaBaoDuong> GetAllSuaChuaBaoDuongs(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<SuaChuaBaoDuong> GetSuaChuaBaoDuongs(decimal XuatNhapId = 0)
        {
            var query = _itemRepository.Table;
            if (XuatNhapId > 0)
            {
                query = query.Where(c => c.NHAP_XUAT_ID == XuatNhapId);
            }
            return query.ToList();
        }
        public virtual IPagedList <SuaChuaBaoDuong> SearchSuaChuaBaoDuongs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null,Decimal CongCuId = 0, Decimal BoPhanId = 0,DateTime? NgayDen =null, DateTime? NgayTu = null, decimal? donViId =0)
        {
            var query = _itemRepository.Table.Where(c => c.XuatNhap.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (Keysearch != null)
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower()));
            if (CongCuId >0)
            {
                query = query.Where(c => c.CONG_CU_ID == CongCuId);
            }
            if (BoPhanId >0)
            {
                var listXN = _xuatNhapService.GetXuatNhaps(isXuat: true, BoPhanId: BoPhanId).Select(c => c.ID).ToList();
                query = query.Where(c => listXN.Contains(c.NHAP_XUAT_ID));
            }
            if (NgayDen != null)
            {
                query = query.Where(c => c.NGAY_SUA_CHUA_FROM <= NgayDen);
            }
            if (NgayTu != null)
            {
                query = query.Where(c => c.NGAY_SUA_CHUA_FROM >= NgayTu);
            }


            return new PagedList<SuaChuaBaoDuong>(query.OrderByDescending(c=>c.NGAY_SUA_CHUA_FROM).ThenBy(c=>c.CongCu.MA), pageIndex, pageSize);;
         }    
        
        public virtual SuaChuaBaoDuong GetSuaChuaBaoDuongById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<SuaChuaBaoDuong> GetSuaChuaBaoDuongByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertSuaChuaBaoDuong(SuaChuaBaoDuong entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateSuaChuaBaoDuong(SuaChuaBaoDuong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteSuaChuaBaoDuong(SuaChuaBaoDuong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

