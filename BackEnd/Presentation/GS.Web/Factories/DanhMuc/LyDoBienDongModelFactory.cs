//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
	public class LyDoBienDongModelFactory : ILyDoBienDongModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ILyDoBienDongService _itemService;
		private readonly INhanHienThiService _nhanhienthiService;
		private readonly ILoaiDonViService _loaiDonViService;
		private readonly ILoaiDonViModelFactory _loaiDonViModelFactory;
		private readonly IDonViService _donViService;
		#endregion

		#region Ctor

		public LyDoBienDongModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ILyDoBienDongService itemService,
			INhanHienThiService nhanHienThiService,
			ILoaiDonViService loaiDonViService,
			ILoaiDonViModelFactory loaiDonViModelFactory,
			IDonViService donViService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._nhanhienthiService = nhanHienThiService;
			this._loaiDonViService = loaiDonViService;
			this._loaiDonViModelFactory = loaiDonViModelFactory;
			this._donViService = donViService;
		}
		#endregion

		#region LyDoBienDong
		public LyDoBienDongSearchModel PrepareLyDoBienDongSearchModel(LyDoBienDongSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			//searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.KHAC });
			int[] valueExclude = new int[] {(int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
			searchModel.LoaiHinhTaiSanAvaliable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList();
			searchModel.LoaiLyDoTangGiamAvaliable = searchModel.enumLoaiLyDoTangGiam.ToSelectList(valuesToExclude: valueExclude);
			return searchModel;
		}

		public LyDoBienDongListModel PrepareLyDoBienDongListModel(LyDoBienDongSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchLyDoBienDongs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, loaiHinhTSId: searchModel.LOAI_HINH_TAI_SAN_ID, loaiLyDoId: searchModel.LOAI_LY_DO_ID, strLoaiHinhTSIds: searchModel.strLoaiHinhTSIds);
			//loai bo loai ly do cua

			//prepare list model
			var model = new LyDoBienDongListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<LyDoBienDongModel>();
					if (c.LOAI_HINH_TAI_SAN_ID != null)
					{
						//m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
					}
					if (c.LOAI_HINH_TAI_SAN_AP_DUNG_ID != null)
					{
						m.LOAI_HINH_TAI_SAN_AP_DUNG_ID = c.LOAI_HINH_TAI_SAN_AP_DUNG_ID;
						m.TenLoaiHinhTaiSan = _nhanhienthiService.GetListGiaTriEnum1(m.enumListLoaiHinhTaiSan);
					}
					else
					{
						m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(enumLOAI_HINH_TAI_SAN.ALL);
					}
					if (c.LOAI_LY_DO_ID != null)
					{
						m.TenLyDoBienDong = _nhanhienthiService.GetGiaTriEnum(c.loaiLyDoTangGiam);
					}
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public LyDoBienDongModel PrepareLyDoBienDongModel(LyDoBienDongModel model, LyDoBienDong item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<LyDoBienDongModel>();
				model.SelectedLoaiDonViIds = new List<int>();
				//model.SelectedLoaiDonViIds.Add(0);
				if (item.LOAI_DON_VI != null)
				{
					if (item.LOAI_DON_VI.Length > 0)
					{
						var listDonVi = item.LOAI_DON_VI.Split(',').ToList();
						foreach (var i in listDonVi)
						{
							if (!String.IsNullOrEmpty(i))
							{
								model.SelectedLoaiDonViIds.Add(Convert.ToInt32(i));
							}
						}
					}
				}
				else
				{
					model.SelectedLoaiDonViIds.Add(0);
				}

				model = item.ToModel<LyDoBienDongModel>();
				model.SelectedLoaiHinhTSIds = new List<int>();
				//model.SelectedLoaiDonViIds.Add(0);
				if (item.LOAI_HINH_TAI_SAN_AP_DUNG_ID != null)
				{
					if (item.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Length > 0)
					{

						List<int> LoaiHinhTS = JsonConvert.DeserializeObject<List<int>>(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID);
						model.SelectedLoaiHinhTSIds = LoaiHinhTS;
					}
				}
				else
				{
					model.SelectedLoaiHinhTSIds.Add(0);
				}
			}
			//more
			int[] valueExclude = new int[] {(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
			model.LoaiHinhTaiSanAvaliable = model.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude:valueExclude);

			if (model.ID > 0)
			{
				model.LyDoTangGiamSanAvaliable = ((enumLOAI_LY_DO_TANG_GIAM)model.LOAI_LY_DO_ID).ToSelectList();
				//model.LoaiHinhTaiSanAvaliable = ((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Split('-').ToNumberInt32()).ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.KHAC });
				string tree = "";
				var valSelecteds = new List<string>();
				if (model.LOAI_HINH_TAI_SAN_AP_DUNG_ID != null) {
					List<string> LoaiHinhTS = JsonConvert.DeserializeObject<List<string>>(item.LOAI_HINH_TAI_SAN_AP_DUNG_ID);
					valSelecteds = LoaiHinhTS;
				}
				else
				{
					valSelecteds.Add(model.LOAI_HINH_TAI_SAN_ID.ToString());
				}
				var selectList = model.LoaiHinhTaiSanAvaliable.Select(c => new SelectListItem
				{
					Text = c.Text,
					Value = c.Value.ToString(),
					Selected = valSelecteds == null ? false : valSelecteds.Contains(c.Value)
				}).ToList();
			}
			else
			{

			model.LyDoTangGiamSanAvaliable = model.enumLyDoTangGiam.ToSelectList();
			//model.LoaiHinhTaiSanAvaliable = model.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.KHAC });
			}
			model.LoaiHinhDonViAvailable = _loaiDonViModelFactory.PrepareMultiSelectLoaiDonVi(model.SelectedLoaiDonViIds);
			
			return model;
		}
		public bool CheckMaLyDoBienDong(decimal id = 0, string ma = null)
		{
			return !_itemService.CheckMaLyDoBienDong(id: id, ma: ma);
		}

		public void PrepareLyDoBienDong(LyDoBienDongModel model, LyDoBienDong item)
		{
			item.MA = model.MA;
			item.TEN = model.TEN;
			item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
			item.LOAI_LY_DO_ID = model.LOAI_LY_DO_ID;
			item.LOAI_DON_VI = model.LOAI_DON_VI;
			item.TRUONG_SAP_XEP = model.TRUONG_SAP_XEP;
			item.LOAI_HINH_TAI_SAN_AP_DUNG_ID = model.LOAI_HINH_TAI_SAN_AP_DUNG_ID;

		}
		public IList<SelectListItem> PrepareSelectListLyDoBienDong(decimal? valSelected = 0, decimal? loailydoId = 0, decimal? LoaiHinhTaiSanId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn lý do biến động --", bool? isTangMoi = null)
		{
			//if (loailydoId == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
			//{
			//	if (isTangMoi == false)
			//	{
			//		loailydoId = (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY;
			//	}
			//}
			var lstLoaiTaiSan = _itemService.GetLyDoBienDongs(LoaiHinhTaiSanId: LoaiHinhTaiSanId, loailydoId: loailydoId);
			//nếu là tăng mới ta k lấy lý do đăng ký lần đầu
			if (isTangMoi != null && isTangMoi==true)
			{
				lstLoaiTaiSan = lstLoaiTaiSan.Where(c => c.MA != "001").ToList();
			}
			else if(isTangMoi != null && isTangMoi == false)
			{
				lstLoaiTaiSan = lstLoaiTaiSan.Where(c => c.MA == "001").ToList();
			}
			var currentDonVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var selectList = lstLoaiTaiSan.Select(c => new SelectListItem
			{
				Text = c.TEN,
				Value = c.ID.ToString(),
				Selected = valSelected == c.ID
			}).OrderBy(c => c.Value).ToList();
			if (isAddFirst)
				selectList.AddFirstRow(strFirstRow);
			return selectList;
		}
		public IList<SelectListItem> PrepareSelectListLyDoBienDongByBaoCao(decimal? valSelected = 0, decimal? loailydoId = 0, decimal? LoaiHinhTaiSanId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn lý do biến động --")
		{
			var lstLoaiTaiSan = _itemService.GetLyDoBienDongs(loailydoId: loailydoId).Select(c => new SelectListItem
			{
				Text = c.TEN,
				Value = c.ID.ToString(),
				Selected = valSelected == c.ID
			}).Distinct();
			var currentDonVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var selectList = new List<SelectListItem>();
			if (isAddFirst)
				selectList.AddFirstRow(strFirstRow);
			return selectList;

		}

		public bool CheckLyDoTheoEnum(decimal id = 0, string ma = null)
		{
			var lyDoItem = _itemService.GetLyDoBienDongById(id);
			if (lyDoItem != null)
			{
				if (lyDoItem.MA == ma)
					return true;
				else
					return false;
			}
			else return false;
		}
		#endregion
	}
}

