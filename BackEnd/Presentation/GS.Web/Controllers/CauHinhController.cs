//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 27/9/2019
//----------------------------------------------------------------------------------------------------------------------
//using AspNetCore;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.CauHinh;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class CauHinhController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICauHinhModelFactory _itemModelFactory;
		private readonly ICauHinhService _itemService;
		private readonly IDonViService _donViService;
		private readonly IDbContext _dbContext;
		private readonly IChucDanhService _chucDanhService;
		private readonly ILoaiTaiSanService _loaiTaiSanService;
		private readonly IChucDanhModelFactory _chucDanhModelFactory;
		private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		private readonly IStaticCacheManager _staticCacheManager;

		#endregion Fields

		#region Ctor

		public CauHinhController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			ICauHinhModelFactory itemModelFactory,
			ICauHinhService itemService,
			IDonViService donViService,
			IDbContext dbContext,
			IChucDanhService chucDanhService,
			IChucDanhModelFactory chucDanhModelFactory,
			ILoaiTaiSanService loaiTaiSanService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			IStaticCacheManager staticCacheManager
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._donViService = donViService;
			this._dbContext = dbContext;
			this._chucDanhService = chucDanhService;
			this._loaiTaiSanService = loaiTaiSanService;
			this._chucDanhModelFactory = chucDanhModelFactory;
			this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			this._staticCacheManager = staticCacheManager;
		}

		#endregion Ctor

		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new CauHinhSearchModel();
			var model = _itemModelFactory.PrepareCauHinhSearchModel(searchmodel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(CauHinhSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareCauHinhListModel(searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareCauHinhModel(new CauHinhModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(CauHinhModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<CauHinh>();
				_itemService.InsertCauHinh(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<CauHinhModel>(), "CauHinh");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}

			//prepare model
			model = _itemModelFactory.PrepareCauHinhModel(model, null);
			return View(model);
		}

		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			var item = _itemService.GetCauHinhById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareCauHinhModel(null, item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(CauHinhModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetCauHinhById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareCauHinh(model, item);
				_itemService.UpdateCauHinh(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<CauHinhModel>(), "CauHinh");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareCauHinhModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetCauHinhById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				_itemService.DeleteCauHinh(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<CauHinhModel>(), "CauHinh");
				//activity log
				SuccessNotification("Xoá dữ liệu thành công");
				return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}

		#endregion Methods

		#region Tham so he thong

		public virtual IActionResult ThamSoHeThong()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareCauHinhChungModel();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult ThamSoHeThong(CauHinhChungModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var cauhinhchung = _itemService.LoadCauHinh<CauHinhChung>();
				cauhinhchung = model.ToCauHinh(cauhinhchung);
				_itemService.SaveCauHinh(cauhinhchung);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật tham số hệ thống", model, "CauHinhChung");
				SuccessNotification("Cập nhật dữ liệu thành công !");
			}

			//prepare model
			model = _itemModelFactory.PrepareCauHinhChungModel();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult ClearCache(string returnUrl = "")
		{
			_staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
			_itemService.ClearCache();

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");

            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
		}
		#endregion Tham so he thong

		#region Cấu hình Thread Báo cáo

		public virtual IActionResult CauHinhThreadBaoCao()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareCauHinhThreadBaoCaoModel();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult CauHinhThreadBaoCao(CauHinhThreadBaoCaoModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			if (ModelState.IsValid)
			{
				var cauHinhThreadBaoCao = _itemService.LoadCauHinh<CauHinhThreadBaoCao>();
				cauHinhThreadBaoCao = model.ToCauHinh(cauHinhThreadBaoCao);
				_itemService.SaveCauHinh(cauHinhThreadBaoCao);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật cấu hình luồng báo cáo", model, "CauHinhThreadBaoCao");
				SuccessNotification("Cập nhật dữ liệu thành công !");
			}
			model = _itemModelFactory.PrepareCauHinhThreadBaoCaoModel();
			return View(model);
		}

		#endregion Cấu hình Thread Báo cáo

		#region XacThucChungThuSo

		public virtual IActionResult XacThucChungThuSo()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareXacThucChungThuSomodel();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult XacThucChungThuSo(XacThucChungThuSomodel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var XacThucChungThuSo = _itemService.LoadCauHinh<XacThucChungThuSo>();
				XacThucChungThuSo = model.ToCauHinh(XacThucChungThuSo);
				_itemService.SaveCauHinh(XacThucChungThuSo);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật xác thực chứng thư số", model, "XacThucChungThuSo");
				SuccessNotification("Cập nhật dữ liệu thành công !");
			}
			//prepare model
			model = _itemModelFactory.PrepareXacThucChungThuSomodel();
			return View(model);
		}

		#endregion XacThucChungThuSo

		#region ThongTinLichKetXuatDuLieu

		public virtual IActionResult LichKetXuatDuLieu()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var cauhinhKetXuatDuLieu = _itemService.LoadCauHinh<KetXuatDuLieu>();
			//prepare model
			var model = new ThongTinKetXuatDuLieuModel();
			if (!String.IsNullOrEmpty(cauhinhKetXuatDuLieu.LichKetXuat))
			{
				model = _itemModelFactory.PrepareLichKetXuatDuLieuModel(model);
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult LichKetXuatDuLieu(ThongTinKetXuatDuLieuModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var _LichKetXuatDuLieu = _itemService.LoadCauHinh<KetXuatDuLieu>();
				model = _itemModelFactory.PrepareLichKetXuatDuLieu(model);
				_LichKetXuatDuLieu.LichKetXuat = model.toStringJson();
				_itemService.SaveCauHinh(_LichKetXuatDuLieu);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật lịch kết xuất dữ liệu", model, "KetXuatDuLieu");
				SuccessNotification("Cập nhật dữ liệu thành công !");
			}
			return View(model);
		}

		#endregion ThongTinLichKetXuatDuLieu

		#region Cấu hình đơn vị

		public virtual IActionResult CauHinhDuyetTaiSan()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var model = _itemModelFactory.PrepareCauHinhTuDongDuyetModel(new CauHinhTuDongDuyetModel());
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult CauHinhDuyetTaiSan(List<CauHinhDuyetTaiSan> cauHinhDuyetTaiSans, bool tsQuanLyNhuTSCD, bool tsQuanLyChuKySo)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var cauHinhTuDongDuyet  = _itemService.LoadCauHinhDonViBo<CauHinhTuDongDuyet>(_workContext.CurrentDonVi.ID);
			cauHinhTuDongDuyet.IsAutoDuyetTaiSanDuoi500 = cauHinhDuyetTaiSans.toStringJson();
			_itemService.SaveCauHinhDonViBo(cauHinhTuDongDuyet,_workContext.CurrentDonVi.ID);

            var donvi = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
           
            if ((donvi != null && donvi.ID == _workContext.CurrentDonVi.ID)) {
                donvi.IS_TSQUAN_LY_NHU_TSCD = tsQuanLyNhuTSCD;
				donvi.IS_TSQUAN_LY_CHU_KY_SO = tsQuanLyChuKySo;

				_donViService.UpdateDonVi(donvi);
            }

            if (_workContext.CurrentDonVi.ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
				var dpac = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
				dpac.IS_TSQUAN_LY_NHU_TSCD = tsQuanLyNhuTSCD;
				dpac.IS_TSQUAN_LY_CHU_KY_SO = tsQuanLyChuKySo;
				_donViService.UpdateDonVi(dpac);
			}
               
            return JsonSuccessMessage("CauHinhDuyetTaiSan");
		}

		#endregion Cấu hình đơn vị

		#region Cấu hình số tài sản

		public virtual IActionResult CauHinhSoTaiSan()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLSoTaiSan))
				return AccessDeniedView();
			var donViId = _workContext.CurrentDonVi.ID;
			var cauHinhSoTaiSan = _itemService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(donViId);
			//prepare model
			var model = new CauHinhSoTaiSanModel();
			if (!String.IsNullOrEmpty(cauHinhSoTaiSan.KhoaSoHeThong))
			{
				model = _itemModelFactory.PrepareListCauHinhSoTaiSan(model, donViId);
			}
			if (model.list_TrangThaiNam == null)
				model.list_TrangThaiNam = new List<TrangThaiNamModel>();
			for (int year = 2008; year <= DateTime.Now.Year; year++)
			{
				if (model.list_TrangThaiNam.Count(p => p.Nam == year) == 0)
				{
					model.list_TrangThaiNam.Insert(0, new TrangThaiNamModel {
						Nam = year,
						TrangThai = (int)enumTrangThaiNamTaiSan.OPEN ,
						NgayKhoaSo = new DateTime(year+1,1,1),
						TenTrangThai = _nhanHienThiService.GetGiaTriEnum<enumTrangThaiNamTaiSan>(enumTrangThaiNamTaiSan.OPEN)
					});
				}
			}
			model.list_TrangThaiNam = model.list_TrangThaiNam.OrderByDescending(p => p.Nam).ToList();
			return View(model);
		}
		public virtual IActionResult EditTrangThaiNamTaiSan(int Year)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLSoTaiSan))
				return AccessDeniedView();
			var model =_itemModelFactory.PrepareTrangThaiNamTaiSanModel(model: null, Year: Year, DonViId: _workContext.CurrentDonVi.ID);
			return View(model);
		}
		[HttpPost]
		public virtual IActionResult EditTrangThaiNamTaiSan(TrangThaiNamModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLSoTaiSan))
				return AccessDeniedView();
			if (ModelState.IsValid)
			{
				var donViId = _workContext.CurrentDonVi.ID;
				var cauHinhSoTaiSan = _itemService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(donViId);
				//prepare model
				if (!String.IsNullOrEmpty(cauHinhSoTaiSan.KhoaSoHeThong))
				{
					var items = cauHinhSoTaiSan.KhoaSoHeThong.toEntities<TrangThaiNam>();
					var item = items.FirstOrDefault(x => x.Nam == model.Nam);
					if (item != null)
					{
						items.Remove(item);
						
					}
					else
					{
						item = new TrangThaiNam {
							Nam= model.Nam
						};
					}
					item = _itemModelFactory.PrepareTrangThaiNamTaiSan(model: model, item: item);
					items.Add(item);
					cauHinhSoTaiSan.KhoaSoHeThong = items.toStringJson();
					_itemService.SaveCauHinhDonViBo(cauHinhSoTaiSan, donViId);
					SuccessNotification("Cập nhật thành công");
					return RedirectToAction("CauHinhSoTaiSan");
				}
			}
			model = _itemModelFactory.PrepareTrangThaiNamTaiSanModel(model: model, Year: model.Nam, DonViId: _workContext.CurrentDonVi.ID);
			return View(model);
			
		}
		[HttpPost]
		public virtual IActionResult CauHinhSoTaiSan(List<TrangThaiNamModel> trangThaiNamModels)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLSoTaiSan))
				return AccessDeniedView();
			var json = trangThaiNamModels.toStringJson();
			var donViId = _workContext.CurrentDonVi.ID;
			var tenCauHinh = enumTENCAUHINH.TS_KHOA_SO_HE_THONG;
			// Insert or update cấu hình đơn vị hiện tại và đơn vị con
			OracleParameter p_ID_DON_VI = new OracleParameter("p_ID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p_JSON_GIA_TRI = new OracleParameter("p_JSON_GIA_TRI", OracleDbType.Clob, json, ParameterDirection.Input);
			OracleParameter p_TenCauHinh = new OracleParameter("p_TenCauHinh", OracleDbType.Varchar2, tenCauHinh, ParameterDirection.Input);
			try
			{
				var result = _dbContext.ExecuteSqlCommand("BEGIN PK_TS_NGHIEPVU.SP_INSERT_UPDATE_CAU_HINH_DONVI(:p_ID_DON_VI, :p_JSON_GIA_TRI, :p_TenCauHinh); END;", false, 600, p_ID_DON_VI, p_JSON_GIA_TRI, p_TenCauHinh);
				_itemService.ClearCache();
				return JsonSuccessMessage("Cập nhật thành công");
			}
			catch
			{
				return JsonErrorMessage("Có lỗi xảy ra");
			}
		}
		//public virtual IActionResult UpdateCauHinhTrangThaiNam()
		#endregion Cấu hình số tài sản

		#region Định mức chức danh

		public virtual IActionResult ListDMCD()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var searchModel = new DinhMucChucDanhSearchModel();
			var model = _itemModelFactory.PrepareDinhMucChucDanhSearchModel(searchModel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult ListDMCD(DinhMucChucDanhSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedKendoGridJson();
			//prepare model
			var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var donvitinh = _donViService.GetDonViLonNhat(donvi.ID,donvi.TREE_NODE);
			var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
			var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
			if (listCauHinh.Count()>0)
			{
				if (searchModel.NgayQuyetDinh != null)
				{
					listCauHinh = listCauHinh.Where(c => c.NGAY_QUYET_DINH == searchModel.NgayQuyetDinh).ToList();
				}
				if (searchModel.SoQuyetDinh != null)
				{
					listCauHinh = listCauHinh.Where(c => c.SO_QUYET_DINH.Contains(searchModel.SoQuyetDinh)).ToList();
				}
				if (searchModel.TuNgay != null)
				{
					listCauHinh = listCauHinh.Where(c => c.TU_NGAY == searchModel.TuNgay).ToList();
				}
				if (searchModel.DenNgay != null)
				{
					listCauHinh = listCauHinh.Where(c => c.DEN_NGAY == searchModel.DenNgay).ToList();
				}
			}
			var items = new PagedList<DinhMucChucDanhModel>(listCauHinh, searchModel.Page - 1, searchModel.PageSize);
			var model = new
			{
				//fill in model values from the entity
				Data = items,
				Total = items.TotalCount
			};
			return Json(model);
		}

		public virtual IActionResult CreateDMCD()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareDinhMucChucDanhModel(new DinhMucChucDanhModel());
			if (model.DEN_NGAY.HasValue)
			{
				model.IS_HIEU_LUC = true;
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult CreateDMCD(DinhMucChucDanhModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			if (ModelState.IsValid)
			{

				var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
				var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
				var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
				var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
				foreach (var ct in model.ChiTietDinhMuc)
				{
					if (ct.NHOM_TAI_SAN_ID != 0)
					{
						ct.TEN_NHOM_TAI_SAN = _loaiTaiSanService.GetLoaiTaiSanById(ct.NHOM_TAI_SAN_ID.Value).TEN;
					}
					if (ct.CHUC_DANH_ID != 0)
					{
						ct.TEN_CHUC_DANH = _chucDanhService.GetChucDanhById(ct.CHUC_DANH_ID.Value).TEN_CHUC_DANH;
					}
				}
				var s = new DinhMucChucDanhModel();
				s.ChiTietDinhMuc = model.ChiTietDinhMuc;
				s.MA = model.MA;
				s.DEN_NGAY = model.DEN_NGAY;
				s.TU_NGAY = model.TU_NGAY;
				s.THONG_TU_NGHI_DINH = model.THONG_TU_NGHI_DINH;
				s.SO_QUYET_DINH = model.SO_QUYET_DINH;
				s.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
				listCauHinh.Add(s);
				cauhinhdinhmucchucdanh.dmcd = listCauHinh.toStringJson();
				_itemService.SaveCauHinhDonViBo(cauhinhdinhmucchucdanh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật cấu hình định mức chức danh", model, "DinhMucChucDanh");
				return JsonSuccessMessage("Cập nhật thành công");
			}
			return View(model);
		}

		public virtual IActionResult EditDMCD(string Ma)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();

			var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
			var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
			var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
			var model = listCauHinh.Where(c => c.MA == Ma).FirstOrDefault();
			if (model.DEN_NGAY.HasValue)
			{
				model.IS_HIEU_LUC = false;
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult EditDMCD(DinhMucChucDanhModel model)
		{
			var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
			var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
			var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
			var cauhinh = listCauHinh.Where(c => c.MA == model.MA).FirstOrDefault();
			if (cauhinh != null)
			{
				listCauHinh.Remove(cauhinh);
				foreach (var ct in model.ChiTietDinhMuc)
				{
					if (ct.NHOM_TAI_SAN_ID != 0)
					{
						ct.TEN_NHOM_TAI_SAN = _loaiTaiSanService.GetLoaiTaiSanById(ct.NHOM_TAI_SAN_ID.Value).TEN;
					}
					if (ct.CHUC_DANH_ID != 0)
					{
						ct.TEN_CHUC_DANH = _chucDanhService.GetChucDanhById(ct.CHUC_DANH_ID.Value).TEN_CHUC_DANH;
					}
				}
				var s = new DinhMucChucDanhModel();
				s.ChiTietDinhMuc = model.ChiTietDinhMuc;
				s.MA = model.MA;
				s.DEN_NGAY = model.DEN_NGAY;
				s.TU_NGAY = model.TU_NGAY;
				s.THONG_TU_NGHI_DINH = model.THONG_TU_NGHI_DINH;
				s.SO_QUYET_DINH = model.SO_QUYET_DINH;
				s.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
				listCauHinh.Add(s);
				cauhinhdinhmucchucdanh.dmcd = listCauHinh.toStringJson();
				//_itemService.SaveCauHinh(cauhinhdinhmucchucdanh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_itemService.SaveCauHinhDonViBo(cauhinhdinhmucchucdanh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật cấu hình định mức chức danh", model, "DinhMucChucDanh");
				return JsonSuccessMessage("Cập nhật thành công");
			}
			else
			{
				foreach (var ct in model.ChiTietDinhMuc)
				{
					if (ct.NHOM_TAI_SAN_ID != 0)
					{
						ct.TEN_NHOM_TAI_SAN = _loaiTaiSanService.GetLoaiTaiSanById(ct.NHOM_TAI_SAN_ID.Value).TEN;
					}
					if (ct.CHUC_DANH_ID != 0)
					{
						ct.TEN_CHUC_DANH = _chucDanhService.GetChucDanhById(ct.CHUC_DANH_ID.Value).TEN_CHUC_DANH;
					}
				}
				var s = new DinhMucChucDanhModel();
				s.ChiTietDinhMuc = model.ChiTietDinhMuc;
				s.MA = model.MA;
				s.DEN_NGAY = model.DEN_NGAY;
				s.TU_NGAY = model.TU_NGAY;
				s.THONG_TU_NGHI_DINH = model.THONG_TU_NGHI_DINH;
				s.SO_QUYET_DINH = model.SO_QUYET_DINH;
				s.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
				listCauHinh.Add(s);
				cauhinhdinhmucchucdanh.dmcd = listCauHinh.toStringJson();
				_itemService.SaveCauHinhDonViBo(cauhinhdinhmucchucdanh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật cấu hình định mức chức danh", model, "DinhMucChucDanh");
				return JsonSuccessMessage("Cập nhật thành công");
			}
		}

		[HttpPost]
		public virtual IActionResult _AddNewChiTietDMCD(string MaDMCD, string MaChiTiet)
		{
			if (!String.IsNullOrEmpty(MaDMCD))
			{
				if (!String.IsNullOrEmpty(MaChiTiet))
				{
					var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
					var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
					var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
					var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>().Where(c => c.MA == MaDMCD).FirstOrDefault();
					var listChiTiet = listCauHinh.ChiTietDinhMuc;
					var chiTiet = listChiTiet.Where(c => c.NHOM_TAI_SAN_ID.ToString() + "-" + c.CHUC_DANH_ID.ToString() == MaChiTiet).ToList().FirstOrDefault();
					//chiTiet.

					var model = new ChiTietDinhMucChucDanhModel();
					model.NHOM_TAI_SAN_ID = chiTiet.NHOM_TAI_SAN_ID;
					model.SO_LUONG = chiTiet.SO_LUONG;
					model.DINH_MUC = chiTiet.DINH_MUC;
					model.TEN_CHUC_DANH = chiTiet.TEN_CHUC_DANH;
					model.TEN_NHOM_TAI_SAN = chiTiet.TEN_NHOM_TAI_SAN;
					model.CHUC_DANH_ID = chiTiet.CHUC_DANH_ID;
					model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(model.CHUC_DANH_ID);

					model.DDLNhomTaiSan = new List<SelectListItem>();
					model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
					var loaiHinhTaiSanModels = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel();
					model.DDLloaiHinhTaiSan = loaiHinhTaiSanModels.Select(c => new SelectListItem
					{
						Text = c.Name,
						Value = c.Id.ToString(),
						Selected = model.LOAI_HINH_TAI_SAN_ID == c.Id
					}).OrderBy(c => c.Text).ToList();

					if (model.NHOM_TAI_SAN_ID.HasValue && model.NHOM_TAI_SAN_ID > 0)
					{
						var loaihinhtaisanid = _loaiTaiSanService.GetLoaiTaiSanById(model.NHOM_TAI_SAN_ID ?? 0).LOAI_HINH_TAI_SAN_ID;
						model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected: model.NHOM_TAI_SAN_ID);
					}
					//model.DDLloaiHinhTaiSan model.LOAI_HINH_TAI_SAN_ID
					return PartialView(model);
				}
				else
				{
					var model = new ChiTietDinhMucChucDanhModel();
					model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh();
					//model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan();
					model.DDLChucDanh = new List<SelectListItem>();
					model.DDLChucDanh.AddFirstRow("--Chọn loại tài sản");
					model.DDLNhomTaiSan = new List<SelectListItem>();
					model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
					model.DDLloaiHinhTaiSan = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel().Select(c => new SelectListItem
					{
						Text = c.Name,
						Value = c.Id.ToString()
					}).ToList(); 
					return PartialView(model);
				}
			}
			else
			{
				var model = new ChiTietDinhMucChucDanhModel();
				model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh();
				//model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan();
				model.DDLNhomTaiSan = new List<SelectListItem>();
				model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
				model.DDLloaiHinhTaiSan = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel().Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}).ToList();
				return PartialView(model);
			}
		}

		[HttpPost]
		public virtual IActionResult DeleteDMCD(string Ma)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
			var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
			var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
			var item = listCauHinh.Where(c => c.MA == Ma).FirstOrDefault();
			listCauHinh.Remove(item);
			return JsonSuccessMessage("Xóa thành công");
		}

		[HttpPost]
		public virtual IActionResult CheckCollisionDateRange(DateTime tungay, DateTime denngay)
		{
			var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
			var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
			var cauhinhdinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(donvitinh.ID);
			var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
			foreach (var cauhinh in listCauHinh)
			{
				if (DateTime.Compare(tungay, cauhinh.DEN_NGAY.Value) < 0 && DateTime.Compare(denngay, cauhinh.TU_NGAY.Value) > 0)
				{
					return JsonErrorMessage("collided");
				}
			}
			return JsonSuccessMessage("ok");
		}
		[HttpPost]
		public virtual IActionResult getddlloaitaisan(decimal loaihinhtaisan, string str = null)
		{
			var selectList = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: loaihinhtaisan, strFirstRow: str); 
			if(loaihinhtaisan == (int)enumLOAI_HINH_TAI_SAN.OTO) {
				var loaitaisanoto = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(2);
				selectList = loaitaisanoto.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaihinhtaisan && c.CHE_DO_HAO_MON_ID == 23)
					.Select(c => new SelectListItem
					{
						Text = c.TEN,
						Value = c.ID.ToString()
					}).ToList();
			}
			selectList.AddFirstRow("Chọn loại tài sản");
			return Json(selectList);
		}
		#endregion Định mức chức danh

		#region Cấu hình thông báo xử lý tài sản
		public virtual IActionResult ListCauHinhDieuKienLocTaiSan()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new DieuKienLocTaiSanSearchModel();
			searchmodel.SetGridPageSize();
			#region Insert điều kiện demo
			//IList<WhereStatement> list = new List<WhereStatement>()
			//{
			//	new WhereStatement
			//	{
			//		GIA_TRI_SO_SANH= "588",
			//		MA_DIEU_KIEN = "Dropdownlist.In",
			//		TEN_TRUONG = "ts.LOAI_TAI_SAN_ID",
			//	},
			//	new WhereStatement
			//	{
			//		GIA_TRI_SO_SANH = ((int)enumTRANG_THAI_TAI_SAN.NHAP).ToString(),
			//		MA_DIEU_KIEN = "Number.Different",
			//		TEN_TRUONG = "ts.TRANG_THAI_ID"
			//	},
			//	new WhereStatement
			//	{
			//		GIA_TRI_SO_SANH = ((int)enumTRANG_THAI_TAI_SAN.XOA).ToString(),
			//		MA_DIEU_KIEN = "Number.Different",
			//		TEN_TRUONG = "ts.TRANG_THAI_ID"
			//	},
			//};
			//List<CauHinhCanhBaoTaiSan> a = new List<CauHinhCanhBaoTaiSan>
			//{
			//	new CauHinhCanhBaoTaiSan {
			//	MA="CAPNHAT_LOAITS",
			//	SelectStatementQuery = "select * from ts_tai_san ts ",
			//	WhereStatementJson = list.toStringJson(),
			//	ActionUrl= "/TaiSan/CapNhapTaiSanChuyenDung",
			//	DisplayName = "Cập nhật tài sản"
			//}};
			//ListCauHinhCanhBaoTaiSan b = new ListCauHinhCanhBaoTaiSan() {JsonValue= a.toStringJson() };

			//_itemService.SaveCauHinh(b); 
			#endregion

			return View(searchmodel);
		}
		[HttpPost]
		public virtual IActionResult ListCauHinhDieuKienLocTaiSan(DieuKienLocTaiSanSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var cauHinhDieuKienLocTaiSan = _itemService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
			if (cauHinhDieuKienLocTaiSan != null && !string.IsNullOrEmpty(cauHinhDieuKienLocTaiSan.JsonValue))
			{
				var items = cauHinhDieuKienLocTaiSan.JsonValue.toEntities<DieuKienLocTaiSan>().AsQueryable();
				if (items != null && items.Count() > 0)
				{
					if (searchModel.KeySearch != null)
						items = items.Where(p => p.TEN_HIEN_THI.Contains(searchModel.KeySearch) || p.MA_LOC_TAI_SAN.Contains(searchModel.KeySearch) || p.VALUE.Contains(searchModel.KeySearch));

					if (searchModel.LOAI_DU_LIEU_ID > 0)
						items = items.Where(p => p.LOAI_DU_LIEU_ID == searchModel.LOAI_DU_LIEU_ID);
					var data = new PagedList<DieuKienLocTaiSan>(items, searchModel.Page - 1, searchModel.PageSize);
					var model = new DieuKienLocTaiSanListModel
					{
						Data = data.Select(p =>
						{
							var item = p.ToModel<DieuKienLocTaiSanModel>();
							if (item.LOAI_DU_LIEU_ID > 0)
							{
								item.TEN_LOAI_DU_LIEU = _nhanHienThiService.GetGiaTriEnum((ENUMLOAI_DU_LIEU_DIEU_KIEN_LOC_TS)item.LOAI_DU_LIEU_ID.Value);
							}
							return item;
						}),
						Total = items.Count()
					};
					return Json(model);
				}
			}
			return Json(new DieuKienLocTaiSanListModel()
			{
				Data = new List<DieuKienLocTaiSanModel>(),
				Total = 0
			});
		}
		public virtual IActionResult CreateCauHinhDieuKienLocTaiSan()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var model = new DieuKienLocTaiSanModel();
			model.ddlLoaiDuLieuId = (new ENUMLOAI_DU_LIEU_DIEU_KIEN_LOC_TS()).ToSelectList().ToList();
			return View(model);
		}
		[HttpPost]
		public virtual IActionResult CreateCauHinhDieuKienLocTaiSan(DieuKienLocTaiSanModel model)
		{
			if (ModelState.IsValid)
			{
				var cauHinhDieuKienLocTaiSan = _itemService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
				var items = cauHinhDieuKienLocTaiSan?.JsonValue.toEntities<DieuKienLocTaiSan>();
				if (items == null)
					items = new List<DieuKienLocTaiSan>();
				items.Add(model.ToEntity<DieuKienLocTaiSan>());
				if (cauHinhDieuKienLocTaiSan == null)
					cauHinhDieuKienLocTaiSan = new CauHinhDieuKienLocTaiSan();
				cauHinhDieuKienLocTaiSan.JsonValue = items.toStringJson();
				_itemService.SaveCauHinh(cauHinhDieuKienLocTaiSan);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", model, "CauHinhDieuKienLocTaiSan");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return RedirectToAction("ListCauHinhDieuKienLocTaiSan");
			}
			return View(model);
		}
		public virtual IActionResult EditCauHinhDieuKienLocTaiSan(string MA_LOC_TAI_SAN)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return AccessDeniedView();
			var cauHinhDieuKienLocTaiSan = _itemService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
			if (cauHinhDieuKienLocTaiSan != null)
			{
				var entity = cauHinhDieuKienLocTaiSan.JsonValue.toEntities<DieuKienLocTaiSan>().FirstOrDefault(p => p.MA_LOC_TAI_SAN == MA_LOC_TAI_SAN);
				if (entity != null)
				{
					var model = entity.ToModel<DieuKienLocTaiSanModel>();
					model.ddlLoaiDuLieuId = (new ENUMLOAI_DU_LIEU_DIEU_KIEN_LOC_TS()).ToSelectList().ToList();
					return View(model);
				}
			}
			return View(new DieuKienLocTaiSanModel());
		}
		[HttpPost]
		public virtual IActionResult EditCauHinhDieuKienLocTaiSan(DieuKienLocTaiSanModel model)
		{
			if (ModelState.IsValid)
			{
				var cauHinhDieuKienLocTaiSan = _itemService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
				var items = cauHinhDieuKienLocTaiSan?.JsonValue.toEntities<DieuKienLocTaiSan>();
				if (items == null)
					items = new List<DieuKienLocTaiSan>();
				items.Remove(items.FirstOrDefault(p => p.MA_LOC_TAI_SAN == model.MA_LOC_TAI_SAN));
				items.Add(model.ToEntity<DieuKienLocTaiSan>());
				if (cauHinhDieuKienLocTaiSan == null)
					cauHinhDieuKienLocTaiSan = new CauHinhDieuKienLocTaiSan();
				cauHinhDieuKienLocTaiSan.JsonValue = items.toStringJson();
				_itemService.SaveCauHinh(cauHinhDieuKienLocTaiSan);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", model, "CauHinhDieuKienLocTaiSan");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return RedirectToAction("ListCauHinhDieuKienLocTaiSan");
			}
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult DeleteCauHinhDieuKienLocTaiSan(string MA_LOC_TAI_SAN)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLCauHinh))
				return JsonErrorMessage();
			var cauHinhDieuKienLocTaiSan = _itemService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
			var items = cauHinhDieuKienLocTaiSan?.JsonValue.toEntities<DieuKienLocTaiSan>();
			if (items == null)
				items = new List<DieuKienLocTaiSan>();
			var item = items.FirstOrDefault(p => p.MA_LOC_TAI_SAN == MA_LOC_TAI_SAN);
			items.Remove(item);
			cauHinhDieuKienLocTaiSan.JsonValue = items.toStringJson();
			_itemService.SaveCauHinh(cauHinhDieuKienLocTaiSan);
			_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa thông tin ", item, "CauHinhDieuKienLocTaiSan");
			SuccessNotification("Cập nhật dữ liệu thành công !");
			return JsonSuccessMessage();
		}
		#endregion
	}
}