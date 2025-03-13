//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
//
using GS.Core.Domain.HeThong;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class VaiTroService : IVaiTroService
    {
        #region Fields
        private readonly CauHinhNguoiDung _customerSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        //private readonly IEventPublisher _eventPublisher;    
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<VaiTro> _itemRepository;
        #endregion

        #region Ctor

        public VaiTroService(CauHinhNguoiDung customerSettings,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            //IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IRepository<VaiTro> itemRepository
            )
        {
            this._customerSettings = customerSettings;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            //this._eventPublisher = eventPublisher;   
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<VaiTro> GetAllVaiTros()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<VaiTro> SearchVaiTros(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper()));
            }

            return new PagedList<VaiTro>(query, pageIndex, pageSize); ;
        }

        public virtual IList<VaiTro> GetVaiTros(IList<int> VaiTroIds = null)
        {
            var query = _itemRepository.Table;
            if (VaiTroIds.Count() > 0)
            {
                query = query.Where(c => VaiTroIds.Contains((int)c.ID));
            }
            return query.ToList();
        }
        public virtual VaiTro GetVaiTroById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual VaiTro GetVaiTroByMa(string ma)
        {
            if (ma == null || ma == "")
                return null;
            return _itemRepository.Table.Where(c => c.MA.Equals(ma)).FirstOrDefault();
        }

        public virtual VaiTro GetVaiTro(string Ma)
        {
            var query = _itemRepository.Table.Where(c => c.MA == Ma);

            return query.FirstOrDefault();
        }

        public virtual void InsertVaiTro(VaiTro entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateVaiTro(VaiTro entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteVaiTro(VaiTro entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public bool KiemTraTrungMa(string Ma, decimal Id)
        {
            return _itemRepository.Table.Any(c => c.MA == Ma && c.ID != Id);
        }

        #endregion
    }
}

