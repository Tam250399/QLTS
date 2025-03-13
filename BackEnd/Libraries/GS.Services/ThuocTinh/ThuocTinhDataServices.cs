//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
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
using GS.Core.Domain.ThuocTinhs;

namespace GS.Services.ThuocTinhs
{
    public partial class ThuocTinhDataService:IThuocTinhDataService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<ThuocTinhData> _itemRepository;
         #endregion
         
         #region Ctor

        public ThuocTinhDataService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<ThuocTinhData> itemRepository
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
        public virtual IList<ThuocTinhData> GetAllThuocTinhDatas(int TaiSanId =0,int ThuocTinhId=0,List<Decimal?> ListTaiSanId = null,List<Decimal?> ListThuocTinhId = null,string Data = null){
            var query = _itemRepository.Table;
            if (TaiSanId != 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == TaiSanId);
            }
            if(ThuocTinhId != 0)
            {
                query = query.Where(c => c.THUOC_TINH_ID == ThuocTinhId);
            }
            if (ListTaiSanId != null)
            {
                query = query.Where(c => ListTaiSanId.Contains(c.TAI_SAN_ID));
            }
            if (ListThuocTinhId != null)
            {
                query = query.Where(c => ListThuocTinhId.Contains(c.THUOC_TINH_ID));
            }
            if (Data != null)
            {
                query = query.Where(c => c.DATA.Contains(Data));
            }
            return query.ToList();
         }
        public virtual ThuocTinhData GetThuocTinhDataByTaiSanIdAndData(string Data = null,int TaiSanId=0)
        {
           
            if (TaiSanId !=0 && Data != null)
            {
                var query = _itemRepository.Table.Where(c => c.DATA.Contains(Data) && c.TAI_SAN_ID == TaiSanId).FirstOrDefault();
                return query;
            }               
            return null;
        }
        public virtual ThuocTinhData GetThuocTinhDataByTaiSanXuLyTDIdAndData(string Data = null, int TaiSanXuLyTDId = 0)
        {

            if (TaiSanXuLyTDId != 0 && Data != null)
            {
                var query = _itemRepository.Table.Where(c => c.DATA.Contains(Data) && c.TAI_SAN_TD_XU_LY_ID == TaiSanXuLyTDId).FirstOrDefault();
                return query;
            }
            return null;
        }
        public virtual IPagedList <ThuocTinhData> SearchThuocTinhDatas(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<ThuocTinhData>(query, pageIndex, pageSize);;
         }    
        
        public virtual ThuocTinhData GetThuocTinhDataById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual List<ThuocTinhData> GetThuocTinhDataByTaiSanId(int TaiSanId=0,int TaiSanTdXuLyId=0)
        {
            var query = _itemRepository.Table;
            if (TaiSanId > 0)
            {
                query= query.Where(c => c.TAI_SAN_ID == TaiSanId);
            }
            if (TaiSanTdXuLyId > 0)
            {
                query = query.Where(c => c.TAI_SAN_TD_XU_LY_ID == TaiSanTdXuLyId);
            }
            return query.ToList();
        }
        public virtual IList<ThuocTinhData> GetThuocTinhDataByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertThuocTinhData(ThuocTinhData entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateThuocTinhData(ThuocTinhData entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteThuocTinhData(ThuocTinhData entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

