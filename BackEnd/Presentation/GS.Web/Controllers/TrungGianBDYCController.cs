using GS.Core;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BienDongs;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public class TrungGianBDYCController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IBienDongModelFactory _bienDongModelFactory;
		private readonly IBienDongService _bienDongService;
		private readonly IYeuCauService _yeuCauService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IYeuCauNhatKyService _yeuCauNhatKyService;
		private readonly ITaiSanService _taiSanService;
		private readonly IYeuCauNhatKyModelFactory _yeuCauNhatKyModelFactory;
		private readonly ITaiSanNguonVonService _taiSanNguonVonService;
		private readonly IYeuCauModelFactory _yeuCauModelFactory;
		private readonly IBienDongChiTietModelFactory _bienDongChiTietModelFactory;
		private readonly ITaiSanNguonVonModelFactory _taiSanNguonVonModelFactory;
		private readonly ITaiSanModelFactory _taiSanModelFactory;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly ITrungGianBDYCModelFactory _trungGianBDYCModelFactory;

		#endregion Fields

		#region Ctor

		public TrungGianBDYCController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IBienDongModelFactory bienDongModelFactory,
			IBienDongService bienDongService,
			IYeuCauService yeuCauService,
			IBienDongChiTietService bienDongChiTietService,
			IYeuCauChiTietService yeuCauChiTietService,
			IYeuCauNhatKyService yeuCauNhatKyService,
			ITaiSanService taiSanService,
			IYeuCauNhatKyModelFactory yeuCauNhatKyModelFactory,
			ITaiSanNguonVonService taiSanNguonVonService,
			IYeuCauModelFactory yeuCauModelFactory,
			IBienDongChiTietModelFactory bienDongChiTietModelFactory,
			ITaiSanNguonVonModelFactory taiSanNguonVonModelFactory,
			ITaiSanModelFactory taiSanModelFactory,
			ITrungGianBDYCService trungGianBDYCService,
			ITrungGianBDYCModelFactory trungGianBDYCModelFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._bienDongModelFactory = bienDongModelFactory;
			this._bienDongService = bienDongService;
			this._yeuCauService = yeuCauService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._yeuCauNhatKyService = yeuCauNhatKyService;
			this._taiSanService = taiSanService;
			this._yeuCauNhatKyModelFactory = yeuCauNhatKyModelFactory;
			this._taiSanNguonVonService = taiSanNguonVonService;
			this._yeuCauModelFactory = yeuCauModelFactory;
			this._bienDongChiTietModelFactory = bienDongChiTietModelFactory;
			this._taiSanNguonVonModelFactory = taiSanNguonVonModelFactory;
			this._taiSanModelFactory = taiSanModelFactory;
			this._trungGianBDYCModelFactory = trungGianBDYCModelFactory;
			this._trungGianBDYCService = trungGianBDYCService;
		}

		#endregion Ctor

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Id">Biến động or yêu cầu id</param>
		/// <param name="BDorYC"></param>
		/// <param name="hasThaoTac"></param>
		/// <returns></returns>
		public virtual IActionResult _ThongTinYeuCauBienDong(decimal Id, int BDorYC, bool? hasThaoTac = false)
		{
			TrungGianBDYC trungGianBDYC = null;
			if (BDorYC == (int)enumBDorYC.BIEN_DONG)
				trungGianBDYC = new TrungGianBDYC(_bienDongService.GetBienDongById(Id));
			else if (BDorYC == (int)enumBDorYC.YEU_CAU)
				trungGianBDYC = new TrungGianBDYC(_yeuCauService.GetYeuCauById(Id));
			var model = _trungGianBDYCModelFactory.PrepareTrungGianBDYCModel(new TrungGianBDYCModel(), trungGianBDYC, true);
			model.HasThaoTac = hasThaoTac;
			return PartialView(model);
		}
		/// <summary>
		/// Description: Xem thông tin nhà (danh sách nhà trên đất)
		/// </summary>
		/// <param name="Id">Biến động or yêu cầu id</param>
		/// <param name="hasThaoTac"></param>
		/// <returns></returns>
		public virtual IActionResult _ThongTinYeuCauBienDongNhaTrenDat(decimal Id, int trangThaiId, bool? hasThaoTac = false)
		{
			TrungGianBDYC trungGianBDYC = null;
			if (trangThaiId == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || trangThaiId == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
				trungGianBDYC = new TrungGianBDYC(_bienDongService.GetBienDongById(Id));
			else
				trungGianBDYC = new TrungGianBDYC(_yeuCauService.GetYeuCauById(Id));
			var model = _trungGianBDYCModelFactory.PrepareTrungGianBDYCModel(new TrungGianBDYCModel(), trungGianBDYC, true);
			model.HasThaoTac = hasThaoTac;
			return PartialView(model);
		}
		#endregion Methods
	}
}