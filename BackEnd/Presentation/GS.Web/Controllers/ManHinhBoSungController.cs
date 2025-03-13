using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
	public class ManHinhBoSungController : BaseWorksController
	{
		private readonly IQuyenService _quyenService;
		private readonly IVaiTroModelFactory _vaiTroModelFactory;
		private readonly IQuyenModelFactory _quyenModelFactory;
		private readonly ICauHinhService _cauHinhService;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
		private readonly IVaiTroService _vaiTroService;
		private readonly IHoatDongModelFactory _hoatDongModelFactory;
		private readonly INhatKyModelFactory _nhatKyModelFactory;
		private readonly IWorkContext _workContext;
		private readonly INguoiDungModelFactory _nguoiDungModelFactory;
		private readonly ICheDoHaoMonModelFactory _cheDoHaoMonModelFactory;
		private readonly ICheDoHaoMonService _cheDoHaoMonService;

		public ManHinhBoSungController(IQuyenService quyenService,
			IVaiTroModelFactory vaiTroModelFactory,
			IQuyenModelFactory quyenModelFactory,
			ICauHinhService cauHinhService,
			ICauHinhModelFactory cauHinhModelFactory,
			IVaiTroService vaiTroService,
			IHoatDongModelFactory hoatDongModelFactory,
			INhatKyModelFactory nhatKyModelFactory,
			IWorkContext workContext,
			INguoiDungModelFactory nguoiDungModelFactory,
			ICheDoHaoMonModelFactory cheDoHaoMonModelFactory,
			ICheDoHaoMonService cheDoHaoMonService)
		{
			_quyenService = quyenService;
			_vaiTroModelFactory = vaiTroModelFactory;
			_quyenModelFactory = quyenModelFactory;
			_cauHinhService = cauHinhService;
			_cauHinhModelFactory = cauHinhModelFactory;
			_vaiTroService = vaiTroService;
			_hoatDongModelFactory = hoatDongModelFactory;
			_nhatKyModelFactory = nhatKyModelFactory;
			_workContext = workContext;
			_nguoiDungModelFactory = nguoiDungModelFactory;
			_cheDoHaoMonModelFactory = cheDoHaoMonModelFactory;
			_cheDoHaoMonService = cheDoHaoMonService;
		}

		#region Nhóm người dùng và phân quyền sử dụng chức năng hệ thống

		public virtual IActionResult ListNhomNguoiDung()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new VaiTroSearchModel();
			var model = _vaiTroModelFactory.PrepareVaiTroSearchModel(searchmodel);
			return View(model);
		}

		public IActionResult CreateNhomNguoiDung()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			//prepare model
			var model = _vaiTroModelFactory.PrepareVaiTroModel(new VaiTroModel(), null);
			return View(model);
		}

		public virtual IActionResult EditNhomNguoiDung(int Id, bool isPhanQuyen = false)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLVaiTro))
				return AccessDeniedView();
			var item = _vaiTroService.GetVaiTroById(Id);
			if (item == null)
				return RedirectToAction("ListNhomNguoiDung");
			//prepare model
			var model = _vaiTroModelFactory.PrepareVaiTroModel(null, item);
			model.IsPhanQuyen = isPhanQuyen;
			return View(model);
		}

		#endregion Nhóm người dùng và phân quyền sử dụng chức năng hệ thống

		#region Quyền bảo mật theo đơn vị

		public virtual IActionResult ListQuyenBaoMatTheoDonVi()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new QuyenSearchModel();
			var model = _quyenModelFactory.PrepareQuyenSearchModel(searchmodel);
			return View(model);
		}

		#endregion Quyền bảo mật theo đơn vị

		#region Cấu hình đồng bộ cập nhật dữ liệu

		public virtual IActionResult CauHinhDongBoCapNhatDuLieu()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var cauhinhKetXuatDuLieu = _cauHinhService.LoadCauHinh<KetXuatDuLieu>();
			//prepare model
			var model = new ThongTinKetXuatDuLieuModel();
			if (!string.IsNullOrEmpty(cauhinhKetXuatDuLieu.LichKetXuat))
			{
				model = _cauHinhModelFactory.PrepareLichKetXuatDuLieuModel(model);
			}
			return View(model);
		}

		#endregion Cấu hình đồng bộ cập nhật dữ liệu

		#region Theo dõi lịch sử đồng bộ dữ liệu

		public virtual IActionResult LichSuDongBoDuLieu()
		{
			//check
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
				return AccessDeniedView();

			//prepare model
			var searchmodel = new HoatDongSearchModel();
			var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
			return View(model);
		}

		#endregion Theo dõi lịch sử đồng bộ dữ liệu

		#region Ghi nhật ký sử dụng

		public virtual IActionResult GhiNhatKySuDung()
		{
			//if (!_permissionService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
			//    return AccessDeniedView();
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new NhatKySearchModel();
			var model = _nhatKyModelFactory.PrepareNhatKySearchModel(searchmodel);
			model.CAPDOlist = ((LogLevel)model.capdoLevel).ToSelectList();

			return View(model);
		}

		#endregion Ghi nhật ký sử dụng

		#region Tra cứu nhật ký sử dụng

		public virtual IActionResult TraCuuNhatKySuDung()
		{
			//check
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
				return AccessDeniedView();

			//prepare model
			var searchmodel = new HoatDongSearchModel();
			var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
			return View(model);
		}

		#endregion Tra cứu nhật ký sử dụng

		#region Sao lưu khôi phục dữ liệu

		public virtual IActionResult SaoLuuKhoiPhucDuLieu()
		{
			//check
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
				return AccessDeniedView();

			//prepare model
			var searchmodel = new HoatDongSearchModel();
			var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
			return View(model);
		}

		#endregion Sao lưu khôi phục dữ liệu

		#region Sao lưu khôi phục dữ liệu đơn vị

		public virtual IActionResult SaoLuuKhoiPhucDuLieuDonVi()
		{
			//check
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
				return AccessDeniedView();

			//prepare model
			var searchmodel = new HoatDongSearchModel();
			var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
			ViewBag.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
			return View(model);
		}

		#endregion Sao lưu khôi phục dữ liệu
		#region Sao lưu khôi phục dữ liệu toàn hệ thống

		public virtual IActionResult SaoLuuKhoiPhucDuLieuToanHeThong()
		{
			//check
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKyHoatDong))
				return AccessDeniedView();

			//prepare model
			var searchmodel = new HoatDongSearchModel();
			var model = _hoatDongModelFactory.PrepareHoatDongSearchModel(searchmodel);
			return View(model);
		}

		#endregion Sao lưu khôi phục dữ liệu
		#region Tài khoản đơn vị đồng bộ dữ liệu
		public virtual IActionResult TaiKhoanDVDB()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new NguoiDungSearchModel();
			var model = _nguoiDungModelFactory.PrepareNguoiDungSearchModel(searchmodel);
			return View(model);
		}
		#endregion

		#region Tài khoản cán bộ khai thác khác
		public virtual IActionResult TaiKhoanCBKT()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNguoiDung))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new NguoiDungSearchModel();
			var model = _nguoiDungModelFactory.PrepareNguoiDungSearchModel(searchmodel);
			return View(model);
		}
		#endregion
		#region Chế độ khấu hao
		public virtual IActionResult ListCheDoKhauHao()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new CheDoHaoMonSearchModel();
			var model = _cheDoHaoMonModelFactory.PrepareCheDoHaoMonSearchModel(searchmodel);
			return View(model);
		}
		public virtual IActionResult CreateCheDoKhauHao()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//prepare model
			var model = _cheDoHaoMonModelFactory.PrepareCheDoHaoMonModel(new CheDoHaoMonModel(), null);
			return View(model);
		}

		public virtual IActionResult EditCheDoKhauHao(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();

			var item = _cheDoHaoMonService.GetCheDoHaoMonById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _cheDoHaoMonModelFactory.PrepareCheDoHaoMonModel(null, item);
			return View(model);
		}
		#endregion
	}
}