//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class FileCongViecService : IFileCongViecService
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<FileCongViec> _itemRepository;

        #endregion Fields

        #region Ctor

        public FileCongViecService(
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<FileCongViec> itemRepository
            )
        {
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion Ctor

        #region Methods

        public virtual IList<FileCongViec> GetFileCongViecByIds(string ids)
        {
            var itemIds = ids.Split(',').Select(c => Convert.ToDecimal(c)).ToArray();
            var query = _itemRepository.Table.Where(c => itemIds.Contains(c.ID));
            return query.ToList();
        }

        public virtual IPagedList<FileCongViec> SearchFileCongViecs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToLower();
                query = query.Where(x => x.TEN_FILE.ToLower().Contains(Keysearch));
            }
            return new PagedList<FileCongViec>(query.OrderByDescending(p => p.NGAY_TAO), pageIndex, pageSize);
        }

        public virtual FileCongViec GetFileCongViecById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual FileCongViec GetFileByGuid(Guid guid)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == guid);
            return query.FirstOrDefault();
        }

        public virtual FileCongViec GetFileByName(String name)
        {
            var query = _itemRepository.Table.Where(c => c.TEN_FILE == name && c.LOAI_FILE_ID == 11);
            return query.FirstOrDefault();
        }

        public virtual bool CheckFileImpTaiSanDone(String name)
        {
            return _itemRepository.Table.Any(c => c.TEN_FILE == name && c.LOAI_FILE_ID == 11);
        }

        public virtual byte[] GetWorkFileBits(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }

        public virtual void InsertFileCongViec(FileCongViec entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateFileCongViec(FileCongViec entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteFileCongViec(FileCongViec entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        #endregion Methods
    }
}