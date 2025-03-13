//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.TaiSans;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
	public class TaiSanChoThueModelFactory : ITaiSanChoThueModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ITaiSanChoThueService _itemService;
		private readonly ITaiSanService _taisanService;
		private readonly INhanHienThiService _nhanhienthiService;
		private readonly IDoiTacService _doiTacService;
        private readonly ILoaiDonViModelFactory _loaiDonViModelFactory;
        #endregion

        #region Ctor

        public TaiSanChoThueModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ITaiSanChoThueService itemService,
			ITaiSanService taisanService,
			INhanHienThiService nhanhienthiService,
			IDoiTacService doiTacService,
			ILoaiDonViModelFactory loaiDonViModelFactory
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._taisanService = taisanService;
			this._nhanhienthiService = nhanhienthiService;
			this._doiTacService = doiTacService;
            this._loaiDonViModelFactory = loaiDonViModelFactory;
        }
		#endregion

		#region TaiSanChoThue
		public TaiSanChoThueSearchModel PrepareTaiSanChoThueSearchModel(TaiSanChoThueSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public TaiSanChoThueListModel PrepareTaiSanChoThueListModel(TaiSanChoThueSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchTaiSanChoThues(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donViId: _workContext.CurrentDonVi.ID);

			//prepare list model
			var model = new TaiSanChoThueListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<TaiSanChoThueModel>();
					m.DonGiaChoThueText = c.DON_GIA_CHO_THUE.ToVNStringNumber();
					m.ChiPhiSuDung = c.THU_TU_CHO_THUE.ToVNStringNumber();
					m.LoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.taisan.enumLoaiHinhTaiSan);
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public TaiSanChoThueModel PrepareTaiSanChoThueModel(TaiSanChoThueModel model, TaiSanChoThue item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<TaiSanChoThueModel>();
			}
			var ts = _taisanService.GetTaiSanById(model.TAI_SAN_ID);
			model.TenTaiSan = ts.TEN;
			model.MaTaiSan = ts.MA;
			model.LoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(ts.enumLoaiHinhTaiSan);
			model.DonViSuDungTaiSan = ts.donvi.TEN;

			// prepare Loại đối tác
			model.LoaiDoiTacAvaiable = _loaiDonViModelFactory.PrepareSelectListLoaiDoiTac(isAddFirst: true, ValSelected: model.LOAI_DOI_TAC_ID);
			//

			if (ts.DON_VI_BO_PHAN_ID > 0)
			{
				model.BoPhanSuDung = ts.donvibophan.TEN;
			}
			//more
			if (model.DOI_TAC_ID > 0)
			{
				model.TenDoiTac = _doiTacService.GetDoiTacById(model.DOI_TAC_ID ?? 0).TEN;
			}
			else
				model.TenDoiTac = "";
			return model;
		}
		public void PrepareTaiSanChoThue(TaiSanChoThueModel model, TaiSanChoThue item)
		{
			item.DOI_TAC_ID = model.DOI_TAC_ID;
			item.TAI_SAN_ID = model.TAI_SAN_ID;
			item.HOP_DONG_SO = model.HOP_DONG_SO;
			item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
			item.HOA_DON_SO = model.HOA_DON_SO;
			item.HOA_DON_NGAY = model.HOA_DON_NGAY;
			item.DON_GIA_CHO_THUE = model.DON_GIA_CHO_THUE;
			item.THU_TU_CHO_THUE = model.THU_TU_CHO_THUE;
			item.NOP_NGAN_SACH = model.NOP_NGAN_SACH;
			item.GIU_LAI_DON_VI = model.GIU_LAI_DON_VI;
			item.GHI_CHU = model.GHI_CHU;
			item.NGAY_TAO = model.NGAY_TAO;
			item.NGAY_UPDATE = model.NGAY_UPDATE;
			item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
			item.NGAY_CHO_THUE_FROM = model.NGAY_CHO_THUE_FROM;
			item.NGAY_CHO_THUE_TO = model.NGAY_CHO_THUE_TO;
			item.LOAI_DOI_TAC_ID = model.LOAI_DOI_TAC_ID;
			item.DOI_TAC_TEN = model.DOI_TAC_TEN;

		}
		public bool CheckTimeChoThue(decimal taiSanId, DateTime? NgayChoThue, decimal tsChoThueId)
		{
			var taiSanChoThue = _itemService.GetTaiSanChoThueByTaiSanIdandNgaychoThue(taiSanId, NgayChoThue);
			if (taiSanChoThue == null || taiSanChoThue.ID == tsChoThueId)
			{
				return true;
			}
			return false;
		}
		#endregion
	}
}

