using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public class TaiSanNhaController : BaseWorksController
	{
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly ITaiSanService _itemService;
		private readonly IYeuCauService _yeuCauService;
		private readonly IHienTrangService _hienTrangService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly ITaiSanModelFactory _itemModelFactory;
		private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
		private readonly ITaiSanNhaService _taiSanNhaService;
		private readonly ITaiSanNhaModelFactory _taiSanNhaModelFactory;
		public TaiSanNhaController(IQuyenService quyenService, 
			IWorkContext workContext, 
			ITaiSanService taiSanService, 
			IYeuCauService yeuCauService,
			IHienTrangService hienTrangService,
			IYeuCauChiTietService yeuCauChiTietService,
			ITaiSanModelFactory itemModelFactory,
			ILyDoBienDongModelFactory lyDoBienDongModelFactory,
			ITaiSanNhaService taiSanNhaService,
			ITaiSanNhaModelFactory taiSanNhaModelFactory)
		{
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._itemService = taiSanService;
			this._yeuCauService = yeuCauService;
			this._hienTrangService = hienTrangService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._itemModelFactory = itemModelFactory;
			this._lyDoBienDongModelFactory = lyDoBienDongModelFactory;
			this._taiSanNhaService = taiSanNhaService;
			this._taiSanNhaModelFactory = taiSanNhaModelFactory;
		}
		public virtual IActionResult Create(decimal LoaiHinhTSId, decimal? loailydobiendongId = 0)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
				return AccessDeniedView();
			var item = new TaiSan();
			var model = new TaiSanModel();
			model.LoaiLyDoBienDongId = loailydobiendongId;
			item.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(LoaiHinhTSId);
			item.TEN = " ";
			item.DON_VI_ID = _workContext.CurrentDonVi.ID;
			//item.NGAY_NHAP = DateTime.Now;
			item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.NHAP;
			_itemService.InsertTaiSan(item);
			//insert NV_YEU_CAU

			var yeucau = new YeuCau();
			yeucau.TAI_SAN_ID = item.ID;
			yeucau.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
			yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
			yeucau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
			_yeuCauService.InsertYeuCau(yeucau);

			// insert NV_YeuCauChiTiet
			var yeucauchitiet = new YeuCauChiTiet();
			yeucauchitiet.YEU_CAU_ID = yeucau.ID;
			var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: yeucau.LOAI_HINH_TAI_SAN_ID);
			var lstObjHienTrang = new List<ObjHienTrang>();
			foreach (var ht in lstHienTrang)
			{
				var obj = new ObjHienTrang();
				obj.HienTrangId = ht.ID;
				obj.TenHienTrang = ht.TEN_HIEN_TRANG;
				obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
				obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
				obj.GiaTriCheckbox = false;
				lstObjHienTrang.Add(obj);
			}
			var hientrangList = new HienTrangList()
			{
				TaiSanId = item.ID,
				lstObjHienTrang = lstObjHienTrang
			};
			yeucauchitiet.HTSD_JSON = hientrangList.toStringJson();
			yeucauchitiet.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
			_yeuCauChiTietService.InsertYeuCauChiTiet(yeucauchitiet);

			model = _itemModelFactory.PrepareTaiSanModel(model, item);
			model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: loailydobiendongId);
			model.LyDoTangAvailable.AddFirstRow("-- Chọn lý do tăng --");
			model.LOAI_HINH_TAI_SAN_ID = LoaiHinhTSId;

			//insert chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
			switch (item.LOAI_HINH_TAI_SAN_ID)
			{
				case (int)enumLOAI_HINH_TAI_SAN.NHA:
					var TsNha = new TaiSanNha();
					TsNha.TAI_SAN_ID = item.ID;
					TsNha.TAI_SAN_DAT_ID = 0;
					TsNha.DIEN_TICH_SAN_XAY_DUNG = 0;
					TsNha.DIEN_TICH_XAY_DUNG = item.NAM_SAN_XUAT;
					TsNha.NAM_XAY_DUNG = 0;
					TsNha.NGAY_SU_DUNG = null;
					_taiSanNhaService.InsertTaiSanNha(TsNha);
					model.taisannhaModel = TsNha.ToModel<TaiSanNhaModel>();
					model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
					break;
			}
			model.YeuCauId = yeucau.ID;
			model.SoLuongNhanBan = 1;

			var taiSanNha = _taiSanNhaService.GetTaiSanNhaByTaiSanId(model.ID);
			var tsNhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(new TaiSanNhaModel(), taiSanNha);
			//loại bỏ hiện trạng khác
			var htKhac = tsNhaModel.lstHienTrang.FirstOrDefault(p => p.HienTrangId == 88);
			tsNhaModel.lstHienTrang.Remove(htKhac);

			model.nvYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.nvYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
			model.taisannhaModel = tsNhaModel;
			return View(model);
		}
	}
}
