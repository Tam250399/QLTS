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

namespace GS.Services.TaiSans
{
    public partial class MuaSamChiTietService:IMuaSamChiTietService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<MuaSamChiTiet> _itemRepository;
         #endregion
         
         #region Ctor

        public MuaSamChiTietService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<MuaSamChiTiet> itemRepository
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
        public virtual IList<MuaSamChiTiet> GetAllMuaSamChiTiets(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<MuaSamChiTiet> GetMuaSamChiTiets(decimal LoaiTaiSanId = 0, decimal? LoaiHinhTaiSanId = 0)
        {
            var query = _itemRepository.Table;
            if (LoaiTaiSanId > 0)
            {
                query = query.Where(c => c.LOAI_TAI_SAN_ID == LoaiTaiSanId);
            }
            if (LoaiHinhTaiSanId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiTaiSanId);
            }
            return query.ToList();
        }
        public virtual IPagedList <MuaSamChiTiet> SearchMuaSamChiTiets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal? muaSamId = 0){
             var query = _itemRepository.Table;
			if (muaSamId>0)
			{
                query = query.Where(c => c.MUA_SAM_ID == muaSamId);
            }
             return new PagedList<MuaSamChiTiet>(query, pageIndex, pageSize);;
         }    
        
        public virtual MuaSamChiTiet GetMuaSamChiTietById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual IList<MuaSamChiTiet> GetMapByMuaSamId(decimal muaSamId)
        {
            var query = _itemRepository.Table.Where(c => c.MUA_SAM_ID == muaSamId);
            return query.ToList();
        }
        public virtual IList<MuaSamChiTiet> GetMuaSamChiTietByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertMuaSamChiTiet(MuaSamChiTiet entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateMuaSamChiTiet(MuaSamChiTiet entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteMuaSamChiTiet(MuaSamChiTiet entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteMuaSamChiTietsByMuaSamId(decimal muaSamId)
        {
            var query = _itemRepository.Table.Where(c => c.MUA_SAM_ID == muaSamId);

            if (query == null)
                throw new ArgumentNullException(nameof(query));
            _itemRepository.Delete(query);
        }
        #endregion
    }
}		

