//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
    public partial class KiemKeCongCuService:IKiemKeCongCuService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<KiemKeCongCu> _itemRepository;
         #endregion
         
         #region Ctor

        public KiemKeCongCuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<KiemKeCongCu> itemRepository
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
        public virtual IList<KiemKeCongCu> GetAllKiemKeCongCus(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <KiemKeCongCu> SearchKiemKeCongCus(bool isPhatHienThua, int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal KiemKeId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_PHAT_HIEN_THUA == isPhatHienThua);
            if (Keysearch != null)
            {
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower()) || (c.TEN_CONG_CU_THUA.ToLower().Contains(Keysearch.ToLower()) && c.IS_PHAT_HIEN_THUA == true));
            }
            if (KiemKeId > 0)
            {
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            }
            if (isPhatHienThua == false)
            {
                query = query.OrderBy(c => c.CongCu.TEN);
            }
            return new PagedList<KiemKeCongCu>(query, pageIndex, pageSize);;
        }
        
        public virtual KiemKeCongCu GetKiemKeCongCuById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public virtual KiemKeCongCu GetKiemKeCongCu(Decimal KiemKeId, Decimal CongCuId, Decimal NhapXuatId)
        {
            var query = _itemRepository.Table.Where(c => c.KIEM_KE_ID == KiemKeId && c.CONG_CU_ID == CongCuId && c.XUAT_NHAP_ID == NhapXuatId);
            return query.FirstOrDefault();
        }

        public virtual IList<KiemKeCongCu> GetKiemKeCongCus(Decimal? KiemKeId = 0, Decimal XuatNhapId = 0)
        {
            var query = _itemRepository.Table;
            if (KiemKeId > 0)
            {
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            }
            if (XuatNhapId > 0)
            {
                query = query.Where(c => c.XUAT_NHAP_ID == XuatNhapId);
            }

            return query.ToList();
        }

        public virtual IList<KiemKeCongCu> GetKiemKeCongCuByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertKiemKeCongCu(KiemKeCongCu entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateKiemKeCongCu(KiemKeCongCu entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKiemKeCongCu(KiemKeCongCu entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

