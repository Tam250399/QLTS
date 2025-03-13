//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using System;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class FileCongViecModelFactory : IFileCongViecModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IFileCongViecService _itemService;
        private readonly IGSFileProvider _fileProvider;
        #endregion

        #region Ctor

        public FileCongViecModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IFileCongViecService itemService,
            IGSFileProvider fileProvider
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._fileProvider = fileProvider;
        }
        #endregion

        #region FileCongViec
        public FileCongViecSearchModel PrepareFileCongViecSearchModel(FileCongViecSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public FileCongViecListModel PrepareFileCongViecListModel(FileCongViecSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchFileCongViecs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new FileCongViecListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<FileCongViecModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public FileCongViecModel PrepareFileCongViecModel(FileCongViecModel model, FileCongViec item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = model ?? item.ToModel<FileCongViecModel>();
            }
            //more

            return model;
        }
        public void PrepareFileCongViec(FileCongViecModel model, FileCongViec item)
        {
            item.ID = model.ID;
            //item.GUID = model.GUID;
            item.TEN_FILE = model.TEN_FILE;
            item.LOAI_FILE = model.LOAI_FILE;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGUOI_TAO = model.NGUOI_TAO;
            item.GHI_CHU = model.GHI_CHU;
            item.DA_XOA = model.DA_XOA;
            item.LOAI_FILE_ID = model.LOAI_FILE_ID;
            //item.NOI_DUNG_FILE = model.NOI_DUNG_FILE;
            item.DUOI_FILE = model.DUOI_FILE;
            item.KICH_THUOC = model.KICH_THUOC;

        }
        public void SaveWorkFileOnDisk(FileCongViec item, byte[] fileContent)
        {
            string _pathStore = item.NGAY_TAO.ToPathFolderStore();
            _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
            _fileProvider.CreateDirectory(_pathStore);
            var _fileStore = _fileProvider.Combine(_pathStore, item.GUID.ToString() + item.DUOI_FILE);
            _fileProvider.WriteAllBytes(_fileStore, fileContent);
        }
        #endregion
    }
}

