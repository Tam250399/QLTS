//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
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
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
	public class KhaiThacModelFactory : IKhaiThacModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IKhaiThacService _itemService;
		private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;
		private readonly ITaiSanService _taiSanService;
		private readonly INhanHienThiService _nhanhienthiService;
		private readonly IBienDongService _biendongService;
		private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly IDonViService _donViService;
        private readonly IDoiTacModelFactory _doiTacModelFactory;
        private readonly IDoiTacService _doiTacService;

        #endregion

        #region Ctor

        public KhaiThacModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IKhaiThacService itemService,
			IKhaiThacTaiSanService khaiThacTaiSanService,
			ITaiSanService taiSanService,
			INhanHienThiService nhanHienThiService,
			IBienDongService biendongService,
			ILoaiTaiSanService loaitaisanService,
			IDonViService donViService,
			IDoiTacModelFactory doiTacModelFactory,
			IDoiTacService doiTacService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._khaiThacTaiSanService = khaiThacTaiSanService;
			this._taiSanService = taiSanService;
			this._nhanhienthiService = nhanHienThiService;
			this._biendongService = biendongService;
			this._loaitaisanService = loaitaisanService;
			this._donViService = donViService;
            this._doiTacModelFactory = doiTacModelFactory;
            this._doiTacService = doiTacService;
        }
		#endregion

		#region KhaiThac
		public KhaiThacSearchModel PrepareKhaiThacSearchModel(KhaiThacSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			searchModel.donviId = _workContext.CurrentDonVi.ID;
			searchModel.tendonvi = _workContext.CurrentDonVi.TEN_DON_VI;
			searchModel.SetGridPageSize();
			return searchModel;
		}

		public KhaiThacListModel PrepareKhaiThacListModel(KhaiThacSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			var donvi = _workContext.CurrentDonVi.ID;
			//get items
			var items = _itemService.SearchKhaiThacs(donviId: donvi, Keysearch: searchModel.KeySearch, QUYET_DINH_SO: searchModel.QUYET_DINH_SO, LOAI_HINH_KHAI_THAC_ID: searchModel.LOAI_HINH_KHAI_THAC_ID, QUYET_DINH_NGAY: searchModel.QUYET_DINH_NGAY, HOP_DONG_SO: searchModel.HOP_DONG_SO, HOP_DONG_NGAY: searchModel.HOP_DONG_NGAY, KHAI_THAC_NGAY_TU: searchModel.KHAI_THAC_NGAY_TU, KHAI_THAC_NGAY_DEN: searchModel.KHAI_THAC_NGAY_DEN, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new KhaiThacListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<KhaiThacModel>();
                    if (m.DOI_TAC_ID != null || m.DOI_TAC_ID > 0)
                    {
					   m.DOI_TAC_TEN = _doiTacService.GetDoiTacById((decimal)m.DOI_TAC_ID)?.TEN;
					}
					
					m.strVNSoTienTamTinh = m.SO_TIEN_TAM_TINH.ToVNStringNumber();
					
					return m;
				}).ToList(),
				Total = items.TotalCount

			};
			return model;
		}
		public KhaiThacModel PrepareKhaiThacModel(KhaiThacModel model, KhaiThac item, bool excludeProperties = false)
		{

			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<KhaiThacModel>();
				var donvi = _donViService.GetDonViById(item.DON_VI_ID??0);
				model.TEN_DON_VI = donvi.TEN;
				model.DON_VI_MA = donvi.MA;
			}
			else
			{
				var donvi = _workContext.CurrentDonVi;
				model.DON_VI_ID = donvi.ID;
				model.TEN_DON_VI = donvi.TEN_DON_VI;
				model.DON_VI_MA = donvi.MA_DON_VI;
			}
			model.PhuongThucChoThueAvaliable = model.enumPhuongThucChoThue.ToSelectList();
			model.HinhThucLDLKAvaliable = model.enumHinhThucLDLK.ToSelectList();

			if (model.LOAI_HINH_KHAI_THAC_ID == (decimal)enumHinhThucKhaiThac.CHO_THUE_TS)
			{
				model.DoiTacAvaliable = _doiTacModelFactory.PrepareSelectListDoiTac(0, model.DOI_TAC_ID, true, "---Chọn Đơn vị thuê---");
			}
			else if (model.LOAI_HINH_KHAI_THAC_ID == (decimal)enumHinhThucKhaiThac.LDLK)
			{
				model.DoiTacAvaliable = _doiTacModelFactory.PrepareSelectListDoiTac(0, model.DOI_TAC_ID, true, "---Chọn Đối tác thực hiện LDLK---");
			}
			else
			{
				model.DoiTacAvaliable = _doiTacModelFactory.PrepareSelectListDoiTac(0, model.DOI_TAC_ID, true, "---Chọn đối tác---");
			}
			model.SO_TIEN_KHAI_THAC_LUY_KE = _khaiThacTaiSanService.TinhSoTienKhaiThacLuyKe(model.ID);
            
			//more
			//if (model.ID != 0)
			//{
			//	var listTaiSan = _taiSanService.GetTaiSanByKhaiThacId(model.ID);
			//	if (model == null)
			//	{
			//		listTaiSan = _taiSanService.GetTaiSanByDonViId(_workContext.CurrentDonVi.ID);
			//	}

			//	model.lstKhaiThacTaiSan = _khaiThacTaiSanService.GetMapByKhaiThacId(model.ID).Select(c => Convert.ToInt32(c.KHAI_THAC_ID)).ToList();
			//	List<TaiSan> ListKhaiThacTaiSan = _khaiThacTaiSanService.GetMapByKhaiThacId(model.ID).Select(m => m.taiSan).ToList();
			//	foreach (var khaiThacTaiSan in ListKhaiThacTaiSan)
			//	{
			//		TaiSanModel taiSanModel = new TaiSanModel();
			//		taiSanModel = khaiThacTaiSan.ToModel<TaiSanModel>();
			//		var taiSanKhaiThac = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(model.ID, taiSanModel.ID);
			//		taiSanModel.DIEN_TICH_KHAI_THAC = taiSanKhaiThac.DIEN_TICH_KHAI_THAC;
			//		taiSanModel.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(taiSanModel.enumLoaiHinhTaiSan);
			//		var DienTichTS = _biendongService.Tinh_GiaTriTaiSan(taiSanModel.ID);
			//		taiSanModel.DIEN_TICH_KT = DienTichTS.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
			//		taiSanModel.TenLoaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(taiSanModel.LOAI_TAI_SAN_ID ?? 0).TEN;
			//		model.ListTaiSanModel.Add(taiSanModel);
			//	}
			//}
			return model;
		}
		public void PrepareKhaiThac(KhaiThacModel model, KhaiThac item)
		{
			item.DON_VI_ID = model.DON_VI_ID;
			item.LOAI_HINH_KHAI_THAC_ID = model.LOAI_HINH_KHAI_THAC_ID;
			item.NGAY_KHAI_THAC = model.NGAY_KHAI_THAC ?? DateTime.Now;
			item.QUYET_DINH_SO = model.QUYET_DINH_SO;
			item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY ?? DateTime.Now;
			item.NGUOI_DUYET = model.NGUOI_DUYET;
			item.NOI_DUNG = model.NOI_DUNG;
			item.GHI_CHU = model.GHI_CHU;
			item.TRANG_THAI_ID = model.TRANG_THAI_ID;
			item.KHAI_THAC_NGAY_TU = model.KHAI_THAC_NGAY_TU;
			item.KHAI_THAC_NGAY_DEN = model.KHAI_THAC_NGAY_DEN;
			item.HOP_DONG_SO = model.HOP_DONG_SO;
			item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
			item.DOI_TAC_ID = model.DOI_TAC_ID;
			item.SO_TIEN_TAM_TINH = model.SO_TIEN_TAM_TINH;
			item.CHO_THUE_GIA = model.CHO_THUE_GIA;
			item.CHO_THUE_PHUONG_THUC_ID = model.CHO_THUE_PHUONG_THUC_ID;
			item.LDLK_HINH_THUC_ID = model.LDLK_HINH_THUC_ID;
			item.NGAY_TAO = model.NGAY_TAO ?? DateTime.Now;
			item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
			item.TEN_PHAP_NHAN_MOI = model.TEN_PHAP_NHAN_MOI;
		}
		#endregion
	}
}

