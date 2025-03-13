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
	public class NguonVonModelFactory : INguonVonModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly INguonVonService _itemService;
		#endregion

		#region Ctor

		public NguonVonModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			INguonVonService itemService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
		}
		#endregion

		#region NguonVon
		public NguonVonSearchModel PrepareNguonVonSearchModel(NguonVonSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public NguonVonListModel PrepareNguonVonListModel(NguonVonSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchNguonVons(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new NguonVonListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<NguonVonModel>()),
				Total = items.TotalCount
			};
			return model;
		}
		public NguonVonModel PrepareNguonVonModel(NguonVonModel model, NguonVon item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<NguonVonModel>();
			}
			//more

			return model;
		}
		public void PrepareNguonVon(NguonVonModel model, NguonVon item)
		{
			item.TEN = model.TEN;
			item.AP_DUNG_ID = model.AP_DUNG_ID;

		}
		public IList<SelectListItem> PrepareSelectlistNguonVon(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nguồn vốn --")
		{
			var lstLoaiTaiSan = _itemService.GetNguonVons();
			var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
			{
				Text = c.TEN,
				Value = c.ID.ToString(),
				Selected = valSelected == c.ID
			}).OrderBy(c => c.Text).ToList();
			if (isAddFirst)
				selectList.AddFirstRow(strFirstRow);
			return selectList;
		}
		public IList<SelectListItem> PrepareMultiSelectNguonVon(IList<int> valSelecteds = null)
		{
			var lstLoaiTaiSan = _itemService.GetNguonVons();
			var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
			{
				Text = c.TEN,
				Value = c.ID.ToString(),
				Selected = valSelecteds == null ? false : valSelecteds.Contains(c.ID.ToNumberInt32())
			}).OrderBy(c => c.Text).ToList();
			return selectList;
		}

		public IList<NguonVonModel> PrepareListNguonVonDefault()
		{
			IList<NguonVonModel> lstNguonVonModel = new List<NguonVonModel>();
			var selectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
			var _listNV = _itemService.GetNguonVonByIds(selectedNguonVonIds.Select(c => (decimal)c).ToArray());
			if (_listNV != null)
			{
				foreach (var _nguonVon in _listNV)
				{
					lstNguonVonModel.Add(new NguonVonModel()
					{
						ID = _nguonVon.ID,
						TEN = _nguonVon.TEN
					});
				}
			}
			return lstNguonVonModel;

		}
		#endregion
	}
}

