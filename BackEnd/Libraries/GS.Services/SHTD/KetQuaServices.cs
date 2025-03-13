//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
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
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial class KetQuaService:IKetQuaService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<KetQua> _itemRepository;
         #endregion
         
         #region Ctor

        public KetQuaService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<KetQua> itemRepository
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
        public virtual IList<KetQua> GetAllKetQuas(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<KetQua> GetAllKetQuaChuaDongBo()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.ToList();
        }
        public virtual IList<KetQua> GetKetQuaBys(decimal TaiSanTDXuLy = 0, List<decimal> ListTaiSanTDXuLy=null)
        {
            var query = _itemRepository.Table;
            if (TaiSanTDXuLy > 0)
            {
                query = query.Where(c => c.TAI_SAN_TD_XU_LY_ID == TaiSanTDXuLy);
            }
            if(ListTaiSanTDXuLy != null)
            {
                query = query.Where(c => ListTaiSanTDXuLy.Contains(c.TAI_SAN_TD_XU_LY_ID));
            }
            return query.ToList();
        }
        public virtual IPagedList <KetQua> SearchKetQuas(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<KetQua>(query, pageIndex, pageSize);;
         }    
        
        public virtual KetQua GetKetQuaById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public virtual KetQua GetKetQuaByTSPAXLID(decimal tsTDXuLyId)
        {
            if (tsTDXuLyId == 0)
                return null;
            return _itemRepository.Table.Where(c => c.TAI_SAN_TD_XU_LY_ID == tsTDXuLyId).FirstOrDefault();
        }
        public virtual KetQua GetKetQuaByDB_ID(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == DB_Id).FirstOrDefault();
        }
        public virtual IList<KetQua> GetKetQuaByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertKetQua(KetQua entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateKetQua(KetQua entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKetQua(KetQua entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

