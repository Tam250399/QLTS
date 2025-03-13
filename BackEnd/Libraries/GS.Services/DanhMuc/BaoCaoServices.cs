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
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.BaoCaos;
using GS.Services.HeThong;

namespace GS.Services.DanhMuc
{
    public partial class BaoCaoService:IBaoCaoService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<BaoCao> _itemRepository;
            private readonly ICauHinhService _cauHinhService;
         #endregion
         
         #region Ctor

        public BaoCaoService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<BaoCao> itemRepository,
            ICauHinhService cauHinhService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._cauHinhService = cauHinhService;
        }

        #endregion
        
        #region Methods
        public virtual IList<BaoCao> GetAllBaoCaos(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <BaoCao> SearchBaoCaos(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<BaoCao>(query, pageIndex, pageSize);;
         }    
        
        public virtual BaoCao GetBaoCaoById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<BaoCao> GetBaoCaoByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual bool CheckMaBaoCao(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA_BAO_CAO == ma && c.ID != id);
        }

        public virtual BaoCao GetBaoCaoByMa(String ma)
        {
            return _itemRepository.Table.Where(c => c.MA_BAO_CAO == ma).SingleOrDefault();
        }

        public virtual void InsertBaoCao(BaoCao entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateBaoCao(BaoCao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteBaoCao(BaoCao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual CauHinhBaoCao GetCauHinhBaoCaoByMa(string MaBC = null, decimal idCurrentDonVi = 0)
        {
            if (MaBC == null)
                return null;
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: idCurrentDonVi).BaoCao;
            if (cauHinh != null)
            {
                 var item = cauHinh.toEntities<CauHinhBaoCao>().FindAll(c => c.MaBaoCao.ToUpper().Contains(MaBC.ToUpper())).FirstOrDefault();
                return item;
            }
            return null;
        }
        #endregion	
     }
}		

