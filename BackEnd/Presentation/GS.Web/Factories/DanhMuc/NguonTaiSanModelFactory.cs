//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
	public class NguonTaiSanModelFactory : INguonTaiSanModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly INguonTaiSanService _itemService;
		#endregion

		#region Ctor

		public NguonTaiSanModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			INguonTaiSanService itemService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
		}
		#endregion

		#region NguonVon
		public NguonTaiSanSearchModel PrepareNguonTaiSanSearchModel(NguonTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public NguonTaiSanListModel PrepareNguonTaiSanListModel(NguonTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchNguonTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new NguonTaiSanListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<NguonTaiSanModel>()),
				Total = items.TotalCount
			};
			return model;
		}
		public NguonTaiSanModel PrepareNguonTaiSanModel(NguonTaiSanModel model, NguonTaiSan item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<NguonTaiSanModel>();
			}
			//more

			return model;
		}
		public void PrepareNguonTaiSan(NguonTaiSanModel model, NguonTaiSan item)
		{
			item.TEN = model.TEN;
			item.NGUOI_DUNG_ID = model.NGUOI_DUNG_ID;
			item.TRANG_THAI_ID = model.TRANG_THAI_ID;
		}
		//public IList<SelectListItem> PrepareSelectlistNguonTaiSan(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nguồn tài sản --")
		//{
		//	var lstLoaiTaiSan = _itemService.GetNguonTaiSan();
		//	var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
		//	{
		//		Text = c.TEN,
		//		Value = c.ID.ToString(),
		//		Selected = valSelected == c.ID
		//	}).OrderBy(c => c.Text).ToList();
		//	if (isAddFirst)
		//		selectList.AddFirstRow(strFirstRow);
		//	return selectList;
		//}
		//public IList<SelectListItem> PrepareMultiSelectNguonTaiSan(IList<int> valSelecteds = null)
		//{
		//	var lstLoaiTaiSan = _itemService.GetNguonTaiSan();
		//	var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
		//	{
		//		Text = c.TEN,
		//		Value = c.ID.ToString(),
		//		Selected = valSelecteds == null ? false : valSelecteds.Contains(c.ID.ToNumberInt32())
		//	}).OrderBy(c => c.Text).ToList();
		//	return selectList;
		//}

		//public IList<NguonTaiSanModel> PrepareListNguonTaiSanDefault()
		//{
		//	IList<NguonTaiSanModel> lstNguonTaiSanModel = new List<NguonTaiSanModel>();
		//	var selectedNguonTaiSanIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
		//	var _listNV = _itemService.GetNguonVonByIds(selectedNguonVonIds.Select(c => (decimal)c).ToArray());
		//	if (_listNV != null)
		//	{
		//		foreach (var _nguonVon in _listNV)
		//		{
		//			lstNguonVonModel.Add(new NguonVonModel()
		//			{
		//				ID = _nguonVon.ID,
		//				TEN = _nguonVon.TEN
		//			});
		//		}
		//	}
		//	return lstNguonVonModel;

		//}
		#endregion
	}
}

