//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
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
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial class DinhMucChiTietService:IDinhMucChiTietService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<DinhMucChiTiet> _itemRepository;
         #endregion
         
         #region Ctor

        public DinhMucChiTietService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<DinhMucChiTiet> itemRepository
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
        public virtual IList<DinhMucChiTiet> GetAllDinhMucChiTiets(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IPagedList <DinhMucChiTiet> SearchDinhMucChiTiets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<DinhMucChiTiet>(query, pageIndex, pageSize);;
         }    
        
        public virtual DinhMucChiTiet GetDinhMucChiTietById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public virtual IList<DinhMucChiTiet> GetDinhMucChiTietByDinhMucId(decimal? Id =0)
        {
            var query = _itemRepository.Table;
            if(Id>0)
            {
                query = query.Where(c => c.DINH_MUC_ID == Id);
            }
            return query.ToList();
        }
        public virtual IList<DinhMucChiTiet> GetDinhMucChiTietByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual DinhMucChiTiet GetDinhMucChiTietByDinhMucIdChucDanhIdAndLoaiTaiSanId(decimal DinhMucId, decimal ChucDanhId, decimal LoaiTaiSanId)
        {
            var query = _itemRepository.Table.Where(c=>c.DINH_MUC_ID == DinhMucId && c.CHUC_DANH_ID == ChucDanhId && c.LOAI_TAI_SAN_ID == LoaiTaiSanId);
            return query.FirstOrDefault();
        }
        public virtual void InsertDinhMucChiTiet(DinhMucChiTiet entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateDinhMucChiTiet(DinhMucChiTiet entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDinhMucChiTiet(DinhMucChiTiet entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

