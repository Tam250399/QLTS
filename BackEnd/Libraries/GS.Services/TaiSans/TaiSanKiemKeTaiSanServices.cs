//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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

namespace GS.Services.TaiSans
{
    public partial class TaiSanKiemKeTaiSanService:ITaiSanKiemKeTaiSanService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanKiemKeTaiSan> _itemRepository;
        private readonly IRepository<TaiSan> _taiSanRepository;
         #endregion
         
         #region Ctor

        public TaiSanKiemKeTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<TaiSanKiemKeTaiSan> itemRepository,
            IRepository<TaiSan> taiSanRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._taiSanRepository = taiSanRepository;
        }

        #endregion
        
        #region Methods
        public virtual IList<TaiSanKiemKeTaiSan> GetAllTaiSanKiemKeTaiSans(){
            var query = _itemRepository.Table;
             return query.ToList();
         }

        public virtual IList<TaiSanKiemKeTaiSan> GetTaiSanKiemKeTaiSans(Decimal KiemKeId = 0)
        {
            var query = _itemRepository.Table;
            if (KiemKeId > 0)
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            return query.ToList();
        }

        public virtual IPagedList <TaiSanKiemKeTaiSan> SearchTaiSanKiemKeTaiSans(bool isTaiSanThua, int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal KiemKeId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_PHAT_HIEN_THUA == isTaiSanThua);
            if (Keysearch != null && isTaiSanThua == false)
            {
                var listTaiSan = _taiSanRepository.Table.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper())).Select(c => c.ID);
                query = query.Where(c => listTaiSan.Contains((Decimal)c.TAI_SAN_ID));
            }
            if (KiemKeId > 0)
                query = query.Where(c => c.KIEM_KE_ID == KiemKeId);
            if (isTaiSanThua && Keysearch != null)
            {
                query = query.Where(c => c.TEN_TAI_SAN_THUA.ToUpper().Contains(Keysearch.ToUpper()));
            }
            if (isTaiSanThua)
            {
                query = query.OrderBy(c => c.TEN_TAI_SAN_THUA);
            }
            return new PagedList<TaiSanKiemKeTaiSan>(query.OrderBy(c => c.taiSan.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.taiSan.loaitaisan.MA), pageIndex, pageSize);
        }    
        
        public virtual TaiSanKiemKeTaiSan GetTaiSanKiemKeTaiSanById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<TaiSanKiemKeTaiSan> GetTaiSanKiemKeTaiSanByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
			if (entity.IS_PHAT_HIEN_THUA && entity.DON_VI_BO_PHAN_ID == 0)
			{
				entity.DON_VI_BO_PHAN_ID = null;
			}
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 

        public virtual void DeleteMultiTaiSanKiemKeTaiSan(decimal?[] taisanId)
        {
            foreach (var id in taisanId)
            {
                var entity = _itemRepository.GetById(id);
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                _itemRepository.Delete(entity);
            }
        }

        #endregion
    }
}		

