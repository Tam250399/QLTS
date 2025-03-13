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
    public partial class ThuocTinhTaiSanService:IThuocTinhTaiSanService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<ThuocTinhTaiSan> _itemRepository;
         #endregion
         
         #region Ctor

        public ThuocTinhTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<ThuocTinhTaiSan> itemRepository
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
        public virtual IList<ThuocTinhTaiSan> GetAllThuocTinhTaiSans(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <ThuocTinhTaiSan> SearchThuocTinhTaiSans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<ThuocTinhTaiSan>(query, pageIndex, pageSize);;
         }    
        
        public virtual ThuocTinhTaiSan GetThuocTinhTaiSanById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual ThuocTinhTaiSan GetThuocTinhTaiSanByLoaiTaiSanIdAndLoaiHinhTaiSanAndThuocTinhId(int LoaiTaiSan = 0, int LoaiHinhTaiSan = 0, int ThuocTinhId = 0)
        {
            if (LoaiTaiSan != 0 && LoaiHinhTaiSan != 0 && ThuocTinhId != 0)
            {
                var query = _itemRepository.Table.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan && c.LOAI_TAI_SAN_ID == LoaiTaiSan && c.THUOC_TINH_ID == ThuocTinhId).FirstOrDefault();
                return query;
            }
            return null;
        }

        public virtual IList<ThuocTinhTaiSan> GetThuocTinhTaiSanByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertThuocTinhTaiSan(ThuocTinhTaiSan entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateThuocTinhTaiSan(ThuocTinhTaiSan entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteThuocTinhTaiSan(ThuocTinhTaiSan entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        public virtual List<ThuocTinhTaiSan> GetThuocTinhTaiSan (int LoaiHinhTaiSan = 0,int LoaiTaiSan = 0,List<decimal?>ListTaiSanId = null,List<decimal?>ListHinhTaiSan = null)
        {
            var query = _itemRepository.Table;
            if (LoaiHinhTaiSan != 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            }
            if (LoaiTaiSan != 0)
            {
                query = query.Where(c => c.LOAI_TAI_SAN_ID == LoaiTaiSan);
            }
            if (ListTaiSanId != null)
            {
                query = query.Where(c => ListTaiSanId.Contains(c.LOAI_TAI_SAN_ID));
            }
            if (ListHinhTaiSan != null)
            {
                query = query.Where(c => ListHinhTaiSan.Contains(c.LOAI_HINH_TAI_SAN_ID));
            }
            return query.ToList();
        }
        #endregion
    }
}		

