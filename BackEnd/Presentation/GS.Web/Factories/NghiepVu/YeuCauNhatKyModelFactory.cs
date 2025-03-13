//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.NghiepVu;
using GS.Services.NghiepVu;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.BienDongs;
using GS.Web.Models.NghiepVu;
using System;
using System.Linq;

namespace GS.Web.Factories.NghiepVu
{
	public class YeuCauNhatKyModelFactory : IYeuCauNhatKyModelFactory
	{
		#region Fields

		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IYeuCauNhatKyService _itemService;

		#endregion Fields

		#region Ctor

		public YeuCauNhatKyModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IYeuCauNhatKyService itemService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
		}

		#endregion Ctor

		#region YeuCauNhatKy

		public YeuCauNhatKySearchModel PrepareYeuCauNhatKySearchModel(YeuCauNhatKySearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public YeuCauNhatKyListModel PrepareYeuCauNhatKyListModel(YeuCauNhatKySearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchYeuCauNhatKys(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new YeuCauNhatKyListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<YeuCauNhatKyModel>()),
				Total = items.TotalCount
			};
			return model;
		}

		public YeuCauNhatKyModel PrepareYeuCauNhatKyModel(YeuCauNhatKyModel model, YeuCauNhatKy item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<YeuCauNhatKyModel>();
			}
			//more

			return model;
		}

		public YeuCauNhatKy PrepareYeuCauNhatKy(YeuCauModel yeuCauModel, YeuCauNhatKy item)
		{
			item.YEU_CAU_ID = yeuCauModel.ID;
			item.LOAI_HINH_TAI_SAN_ID = yeuCauModel.LOAI_HINH_TAI_SAN_ID;
			item.DATA_JSON = yeuCauModel.toStringJson();
			item.NGUOI_TAO_ID = yeuCauModel.NGUOI_TAO_ID;
			item.DON_VI_ID = yeuCauModel.DON_VI_ID;
			item.DON_VI_BO_PHAN_ID = yeuCauModel.DON_VI_BO_PHAN_ID;
			item.DATA_JSON = yeuCauModel.toStringJson();
			item.TAI_SAN_ID = yeuCauModel.TAI_SAN_ID;
			return item;
		}
		public YeuCauNhatKy PrepareYeuCauNhatKy(BienDongModel bienDongModel, YeuCauNhatKy item)
		{
			item.YEU_CAU_ID = bienDongModel.ID;
			item.LOAI_HINH_TAI_SAN_ID = bienDongModel.LOAI_HINH_TAI_SAN_ID;
			item.DATA_JSON = bienDongModel.toStringJson();
			item.NGUOI_TAO_ID = bienDongModel.NGUOI_TAO_ID;
			item.DON_VI_ID = bienDongModel.DON_VI_ID;
			item.DON_VI_BO_PHAN_ID = bienDongModel.DON_VI_BO_PHAN_ID;
			item.DATA_JSON = bienDongModel.toStringJson();
			item.TAI_SAN_ID = bienDongModel.TAI_SAN_ID;
			return item;
		}
		public void InsertYeuCauNhatKy(YeuCauModel yeuCauModel, enumLOAI_NHATKY_YEUCAU LOAI_NHATKY_YEUCAU)
		{
			var yeuCauNhatKy = PrepareYeuCauNhatKy(yeuCauModel, new YeuCauNhatKy());
			yeuCauNhatKy.LOAI_NHAT_KY_ID = (int)LOAI_NHATKY_YEUCAU;
			_itemService.InsertYeuCauNhatKy(yeuCauNhatKy);
		}
		public void InsertYeuCauNhatKy(BienDongModel bienDongModel, enumLOAI_NHATKY_YEUCAU LOAI_NHATKY_YEUCAU)
		{
			var yeuCauNhatKy = PrepareYeuCauNhatKy(bienDongModel, new YeuCauNhatKy());
			yeuCauNhatKy.LOAI_NHAT_KY_ID = (int)LOAI_NHATKY_YEUCAU;
			_itemService.InsertYeuCauNhatKy(yeuCauNhatKy);
		}
		#endregion YeuCauNhatKy
	}
}