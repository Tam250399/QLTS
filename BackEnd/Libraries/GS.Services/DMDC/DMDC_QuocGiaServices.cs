//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
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
using GS.Core.Domain.DMDC;

namespace GS.Services.DMDC
{
    public partial class DMDC_QuocGiaService:IDMDC_QuocGiaService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<DMDC_QuocGia> _itemRepository;
         #endregion
         
         #region Ctor

        public DMDC_QuocGiaService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<DMDC_QuocGia> itemRepository
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
        public virtual IList<DMDC_QuocGia> GetAllDMDC_QuocGias(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <DMDC_QuocGia> SearchDMDC_QuocGias(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<DMDC_QuocGia>(query, pageIndex, pageSize);;
         }    
        
        public virtual DMDC_QuocGia GetDMDC_QuocGiaById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public DMDC_QuocGia GetDMDC_QuocGiaByMa(string Ma)
        {
            if (string.IsNullOrEmpty(Ma))
            {
                return null;
            }
            return _itemRepository.Table.Where(m => m.MA == Ma).FirstOrDefault();
        }

        public virtual IList<DMDC_QuocGia> GetDMDC_QuocGiaByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertDMDC_QuocGia(DMDC_QuocGia entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }

        public virtual void UpdateDMDC_QuocGia(DMDC_QuocGia entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDMDC_QuocGia(DMDC_QuocGia entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

