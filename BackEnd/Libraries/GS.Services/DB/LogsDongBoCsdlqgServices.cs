//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
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
using GS.Core.Domain.DB;

namespace GS.Services.DB
{
    public partial class LogsDongBoCsdlqgService : ILogsDongBoCsdlqgService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<LogsDongBoCsdlqg> _itemRepository;
        #endregion

        #region Ctor

        public LogsDongBoCsdlqgService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<LogsDongBoCsdlqg> itemRepository
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
        public virtual IList<LogsDongBoCsdlqg> GetAllLogsDongBoCsdlqgs(string uuid=null,string matsc = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(uuid))
                query = query.Where(c => c.UUID == uuid);
            if (!string.IsNullOrEmpty(matsc))
                query = query.Where(c => c.MA_QLTSC == matsc);
            return query.ToList();
        }

        public virtual IPagedList<LogsDongBoCsdlqg> SearchLogsDongBoCsdlqgs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<LogsDongBoCsdlqg>(query, pageIndex, pageSize); ;
        }

        public virtual LogsDongBoCsdlqg GetLogsDongBoCsdlqgById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<LogsDongBoCsdlqg> GetLogsDongBoCsdlqgByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertLogsDongBoCsdlqg(LogsDongBoCsdlqg entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateLogsDongBoCsdlqg(LogsDongBoCsdlqg entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLogsDongBoCsdlqg(LogsDongBoCsdlqg entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        #endregion
    }
}

