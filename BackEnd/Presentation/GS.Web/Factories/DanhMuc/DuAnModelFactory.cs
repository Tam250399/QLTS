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
	public class DuAnModelFactory : IDuAnModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IDuAnService _itemService;
		private readonly IDonViService _donViService;
		private readonly IDonViModelFactory _donViModelFactory;
		#endregion

		#region Ctor

		public DuAnModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IDuAnService itemService,
			IDonViService donViService,
			IDonViModelFactory donViModelFactory
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._donViService = donViService;
			this._donViModelFactory = donViModelFactory;
		}
		#endregion

		#region DuAn
		public DuAnSearchModel PrepareDuAnSearchModel(DuAnSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			searchModel.TenDonViCha = _workContext.CurrentDonVi.TEN_DON_VI;
			searchModel.donviId = _workContext.CurrentDonVi.ID;
			return searchModel;
		}

		public bool CheckMaDuAn(decimal id = 0, string ma = null, decimal? donViID = null)
		{
			return !_itemService.CheckMaDuAn(id: id, ma: ma, donViID: donViID);
		}
		public DateTime? CheckNgayDuAn(decimal id = 0)
		{
			return _itemService.GetDuAnById(id).NGAY_BAT_DAU;
		}
		public DuAnListModel PrepareDuAnListModel(DuAnSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchDuAns(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donViId: searchModel.donviId);

			//prepare list model
			var model = new DuAnListModel
			{
				//fill in model values from the entity
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<DuAnModel>();
					m.pageIndex = searchModel.Page;
					m.strVNTongKinhPhi = c.TONG_KINH_PHI.ToVNStringNumber();
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public DuAnModel PrepareDuAnModel(DuAnModel model, DuAn item, bool excludeProperties = false, bool? isCreateChildDV = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<DuAnModel>();
			}
			else
			{
				bool isBanQLDA = _workContext.CurrentDonVi.LA_BAN_QL_DU_AN ?? false;
				if (isBanQLDA)
				{
					model.DON_VI_ID = _workContext.CurrentDonVi.ID;
				}
				else
				{
					DonVi donViQuanLyTSDA = _donViService.GetDonViBanQuanLyDuAn(donViId: _workContext.CurrentDonVi.ID, isDonViBanQuanLyDuAn: true);
					if (donViQuanLyTSDA != null)
					{
						model.DON_VI_ID = donViQuanLyTSDA.ID;
					}
					if (donViQuanLyTSDA == null && isCreateChildDV.Value)
					{
						//Nếu đơn vị này k có đơn vị con quản lý tsc và tsda
						//Tạo 2 đơn vị con
						DonVi donViTSC = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN:false);
						_donViService.InsertDonVi(donViTSC);
						DonVi donViTSDA = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN:true);
						_donViService.InsertDonVi(donViTSDA);
						model.DON_VI_ID = donViTSDA.ID;
					}
					
				}

			}
			//more
			return model;
		}
		public void PrepareDuAn(DuAnModel model, DuAn item)
		{
			item.MA = model.MA;
			item.TEN = model.TEN;
			item.LOAI_DU_AN_ID = model.LOAI_DU_AN_ID;
			item.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
			item.NGAY_KET_THUC = model.NGAY_KET_THUC;
			item.TONG_KINH_PHI = model.TONG_KINH_PHI;
			item.HINH_THUC_ID = model.HINH_THUC_ID;
			item.SO_QUYET_DINH_PHE_DUYET = model.SO_QUYET_DINH_PHE_DUYET;
			item.CHU_DAU_TU = model.CHU_DAU_TU;
			item.DIA_DIEM = model.DIA_DIEM;
			item.NGUON_VON = model.NGUON_VON;
			item.GHI_CHU = model.GHI_CHU;
			item.NGUON_NS = model.NGUON_NS;
			item.NGUON_ODA = model.NGUON_ODA;
			item.NGUON_VIEN_TRO = model.NGUON_VIEN_TRO;
			item.NGUON_KHAC = model.NGUON_KHAC;
			item.KIEU = model.KIEU;
			item.CO_QUAN_TAI_CHINH = model.CO_QUAN_TAI_CHINH;
			item.MA_LOAI_DU_AN = model.MA_LOAI_DU_AN;
			item.MA_NHOM_DU_AN = model.MA_NHOM_DU_AN;
			item.TEN_NHOM_DU_AN = model.TEN_NHOM_DU_AN;
			item.MA_HT = model.MA_HT;
			item.TEN_HT = model.TEN_HT;
			item.HT_QUANLY = model.HT_QUANLY;
			item.HIEU_LUC = model.HIEU_LUC;
			//item.MA_DVQHNS = model.MA_DVQHNS;
			item.MA_DU_AN_CU = model.MA_DU_AN_CU;
			item.TRANG_THAI_ID = model.TRANG_THAI_ID;
			item.DON_VI_ID = model.DON_VI_ID;
			item.DON_VI_CU_ID = model.DON_VI_CU_ID;

		}
		#endregion
		public IList<SelectListItem> PrepareSelectListDuAn(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "--Chọn dự án", string valueFirstRow = "", Decimal? donViId = 0)
		{
			var lstDuAn = _itemService.GetDuAnByDonViId(donViId: donViId.Value);
			List<SelectListItem> selectList = new List<SelectListItem>();
			selectList = lstDuAn.Select(c => new SelectListItem
			{
				Text = c.TEN,
				Value = c.ID.ToString(),
				Selected = valSelected == c.ID
			}).ToList();
			if (isAddFirst)
				selectList.AddFirstRow(TextDisplay: strFirstRow, ValueFirst: valueFirstRow);
			return selectList;
		}
	}
}