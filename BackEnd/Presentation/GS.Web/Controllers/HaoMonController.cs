using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.Security;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.KeToan;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.KeToan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public class HaoMonController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IHaoMonTaiSanModelFactory _haoMonTaiSanModelFactory;
		private readonly IHaoMonTaiSanService _haoMonTaiSanService;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly IDonViService _donViService;

        #endregion Fields

        #region Ctor

        public HaoMonController(IHoatDongService hoatdongService,
								INhanHienThiService nhanHienThiService,
								IQuyenService quyenService,
								IWorkContext workContext,
								CauHinhChung cauhinhChung,
								IHaoMonTaiSanModelFactory haoMonTaiSanModelFactory,
								IHaoMonTaiSanService haoMonTaiSanService,
								ICauHinhModelFactory cauHinhModelFactory,
								IDonViService donViService)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._haoMonTaiSanModelFactory = haoMonTaiSanModelFactory;
			this._haoMonTaiSanService = haoMonTaiSanService;
			this._cauHinhModelFactory = cauHinhModelFactory;
            this._donViService = donViService;
        }

		#endregion Ctor

		#region Method

		public IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhHaoMon))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new HaoMonTaiSanSearchModel();
			var searchModel = _haoMonTaiSanModelFactory.PrepareHaoMonTaiSanSearchModel(searchmodel);
			return View(searchModel);
		}

		[HttpPost]
		public IActionResult List(HaoMonTaiSanSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhHaoMon))
				return AccessDeniedKendoGridJson();
			var model = _haoMonTaiSanModelFactory.PrepareHaoMonTaiSanListModel(searchModel);
			return Json(model);
		}

		public IActionResult EditHaoMon(decimal Id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhHaoMon))
				return AccessDeniedView();
			if (Id > 0)
			{
				var item = _haoMonTaiSanService.GetHaoMonTaiSanById(Id);
				if (_cauHinhModelFactory.CheckYearIsLockSoTaiSan(item.NAM_HAO_MON))
					return AccessDeniedView();
				HaoMonTaiSanModel model = _haoMonTaiSanModelFactory.PrepareHaoMonForEditModel(null, item);
				return PartialView(model);
			}
			return PartialView(new HaoMonTaiSanModel());
		}

		[HttpPost]
		public IActionResult EditHaoMon(HaoMonTaiSanModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDieuChinhHaoMon))
				return JsonErrorMessage("Fail");
			if (model != null)
			{
				var item = _haoMonTaiSanService.GetHaoMonTaiSanById(model.ID);

				//lấy ra hao mòn lũy kế trước
				decimal tongHaoMonLKTTruoc = _haoMonTaiSanService.GetLKHMTruocDo(item);
				//cập nhật item đầu tiên
				item.TONG_GIA_TRI_CON_LAI = model.TONG_GIA_TRI_CON_LAI;
				item.GIA_TRI_HAO_MON = model.GIA_TRI_HAO_MON;
				item.TONG_HAO_MON_LUY_KE = tongHaoMonLKTTruoc + item.GIA_TRI_HAO_MON;
				_haoMonTaiSanService.UpdateHaoMonTaiSan(item);

				//cập nhật các item sau nó
				var list_HMTS =  _haoMonTaiSanService.GetListHMTSSauNam(item.TAI_SAN_ID, item.NAM_HAO_MON);
				if (list_HMTS!= null && list_HMTS.Count>0)
				{
					foreach (var hmts in list_HMTS)
					{
						//tính hao mòn lũy kế mới
						var hmLuyKeTruoc=  _haoMonTaiSanService.GetLKHMTruocDo(hmts);
						hmts.TONG_HAO_MON_LUY_KE = hmLuyKeTruoc + hmts.GIA_TRI_HAO_MON;

						//tính giá trị còn lại mới
						hmts.TONG_GIA_TRI_CON_LAI = hmts.TONG_NGUYEN_GIA - hmts.TONG_HAO_MON_LUY_KE;

						_haoMonTaiSanService.UpdateHaoMonTaiSan(hmts);
					}
				}
				return JsonSuccessMessage("Thành công");
			}
			return JsonErrorMessage("Fail");
		}

		[HttpPost]
		public IActionResult UpdateGTCLorHaoMon(bool isGTCL, decimal idHaoMonTaiSan, decimal giaTri)
		{
			if (isGTCL)
				return Json(CalculateHaoMonFromGTCL(idHaoMonTaiSan, giaTri));
			else
				return Json(CalculateGTCLFromHaoMon(idHaoMonTaiSan, giaTri));
		}
		public IActionResult ImportHaoMon()
        {
			return View();
        }

		
		#endregion Method

		#region Function

		/// <summary>
		/// tính giá trị còn lại từ hao mòn
		/// </summary>
		/// <param name="idHaoMonTaiSan"></param>
		/// <param name="giaTri"></param>
		/// <returns></returns>
		private PairGTCLAndHaoMon CalculateGTCLFromHaoMon(decimal idHaoMonTaiSan, decimal giaTri)
		{
			var haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanById(idHaoMonTaiSan);
			var giaTriHaoMotNam = giaTri;
			var nguyenGia = haoMonTaiSan.TONG_NGUYEN_GIA ?? 0;
			decimal tongHaoMonLKTTruoc = _haoMonTaiSanService.GetLKHMTruocDo(haoMonTaiSan);

			// giá trị còn lại = nguyên giá - (hao mòn trong năm + hao mòn lũy kế năm trước)
			var gtcl = nguyenGia - (giaTriHaoMotNam + tongHaoMonLKTTruoc);
			// tổng lũy kế hao mòn = lũy kế hao mòn năm trước + giá trị hao mòn năm hiện tại
			var tongLuyKeHM = tongHaoMonLKTTruoc + giaTriHaoMotNam;
			return new PairGTCLAndHaoMon() { GIA_TRI_HAO_MON = giaTriHaoMotNam, TONG_GIA_TRI_CON_LAI = gtcl, TONG_HAO_MON_LUY_KE = tongLuyKeHM };
		}

		/// <summary>
		/// tính hao mòn từ giá trị còn lại
		/// </summary>
		/// <param name="idHaoMonTaiSan"></param>
		/// <param name="giaTri"></param>
		/// <returns></returns>
		private PairGTCLAndHaoMon CalculateHaoMonFromGTCL(decimal idHaoMonTaiSan, decimal giaTri)
		{
			var haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanById(idHaoMonTaiSan);
			var gtcl = giaTri;
			var nguyenGia = haoMonTaiSan.TONG_NGUYEN_GIA ?? 0;
			decimal tongHaoMonLKTTruoc = _haoMonTaiSanService.GetLKHMTruocDo(haoMonTaiSan);

			// giá trị còn lại = nguyên giá - (hao mòn trong năm + hao mòn lũy kế năm trước)
			var giaTriHaoMotNam = nguyenGia - (gtcl + tongHaoMonLKTTruoc);
			// tổng lũy kế hao mòn = lũy kế hao mòn năm trước + giá trị hao mòn năm hiện tại
			var tongLuyKeHM = tongHaoMonLKTTruoc + giaTriHaoMotNam;
			return new PairGTCLAndHaoMon() { GIA_TRI_HAO_MON = giaTriHaoMotNam, TONG_GIA_TRI_CON_LAI = gtcl ,TONG_HAO_MON_LUY_KE = tongLuyKeHM };
		}

		#region Chốt hao mòn tài sản của đơn vị

		public virtual IActionResult ChotHaoMonTaiSanDonVi()
		{
			var model = new HaoMonTaiSanModel();
			return View(model);
		}
		[HttpPost]
		public virtual IActionResult ChotHaoMonTaiSanDonVi(string maDonVi)
		{
			var donVi = _donViService.GetDonViByMa(maDonVi);
			if (donVi != null)
			{
				var result = _haoMonTaiSanService.ChotToanBoHaoMonDonVi(donVi.ID);
				if (result)
				{
					return Json(MessageReturn.CreateSuccessMessage("Chốt hao mòn thành công", true));
				}
			}
			return Json(MessageReturn.CreateErrorMessage("Chốt hao mòn không thành công", false));
		}

		#endregion

		#endregion Function
	}
}