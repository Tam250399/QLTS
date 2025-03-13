using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BaoCaos.TS_CDKT;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BaoCaos;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.CCDC;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.SHTD;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.CCDC;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;
using GS.Web.Models.BaoCaos.TSTD;
using GS.Web.Models.HeThong;
using GS.Web.Models.TaiSans;
using GS.Web.Reports.CCDC;
using GS.Web.Reports.CongCuKiemTra;
using GS.Web.Reports.TaiSanToanDan;
using GS.Web.Reports.Test;
using GS.Web.Reports.TheTaiSan;
using GS.Web.Reports.TS_BCCK;
using GS.Web.Reports.TS_BCCT;
using GS.Web.Reports.TS_BCDA;
using GS.Web.Reports.TS_BCKK;
using GS.Web.Reports.TS_BCQH;
using GS.Web.Reports.TS_BCTC;
using GS.Web.Reports.TS_BCTH;
using GS.Web.Reports.TS_CDKT;
using GS.Web.Reports.TS_PXNTT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace GS.Web.Controllers
{
	public class CongCuKiemTraController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IKiemKeModelFactory _itemModelFactory;
		private readonly IKiemKeService _kiemKeService;
		private readonly IKiemKeHoiDongService _kiemKeHoiDongService;
		private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
		private readonly IKiemKeCongCuService _kiemKeCongCuService;
		private readonly ICongCuDungCuService _congCuDungCuService;
		private readonly INhomCongCuService _nhomCongCuService;
		private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
		private readonly IDonViBoPhanService _donViBoPhanService;
		private readonly IDonViService _donViService;
		private readonly ICheDoKeToanService _cheDoKeToanService;
		private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		private readonly ICauHinhService _cauHinhService;
		private readonly IDonViModelFactory _donViModelFactory;
		private readonly ITaiSanToanDanService _taiSanToanDanService;
		private readonly INhatKyService _nhatKyService;
		private readonly IBaoCaoChiTietTaiSanService _taiSanBCCTService;
		private readonly IReportModelFactory _reportModelFactory;
		private readonly IQueueProcessService _queueProcessService;
		private readonly IQueueProcessModelFactory _queueProcessModelFactory;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly IBaoCaoTongHopTaiSanService _baoCaoTongHopTaiSanService;
		private readonly IDiaBanService _diaBanService;
		private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
		private readonly IQuyetDinhTichThuModelFactory _quyetDinhTichThuModelFactory;
		private readonly IBaoCaoCongKhaiService _baoCaoCongKhaiService;
		private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
		private readonly IBaoCaoTraCuuService _baoCaoTraCuuService;
		private readonly IBaoCaoKeKhaiServices _baoCaoKeKhaiServices;
		private readonly IBaoCaoQuocHoiService _baoCaoQuocHoiService;
		private readonly IBaoCaoDuAnService _baoCaoDuAnService;
		private readonly IInTheTaiSanServices _inTheTaiSanServices;
		private readonly ITaiSanKiemKeService _kiemKeTaiSanService;
		private readonly ITaiSanKiemKeHoiDongService _taiSanKiemKeHoiDongService;
		private readonly IBaoCaoDoiChieuDuLieuService _baoCaoDoiChieuDuLieuService;
		private readonly ITaiSanModelFactory _taiSanModelFactory;
		private readonly ILoaiTaiSanService _loaiTaiSanService;
		#endregion Fields
		#region Ctor
		public CongCuKiemTraController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IKiemKeModelFactory itemModelFactory,
			IKiemKeService kiemKeService,
			IKiemKeHoiDongService kiemKeHoiDongService,
			IDonViBoPhanModelFactory donViBoPhanModelFactory,
			IKiemKeCongCuService kiemKeCongCuService,
			ICongCuDungCuService congCuDungCuService,
			INhomCongCuService nhomCongCuService,
			INhomCongCuModelFactory nhomCongCuModelFactory,
			IDonViBoPhanService donViBoPhanService,
			IDonViService donViService,
			ICheDoKeToanService cheDoKeToanService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			ICauHinhService cauHinhService,
			IDonViModelFactory donViModelFactory,
			ITaiSanToanDanService taiSanToanDanService,
			INhatKyService nhatKyService,
			IBaoCaoChiTietTaiSanService taiSanBCCTService,
			IReportModelFactory reportModelFactory,
			IQueueProcessService queueProcessService,
			IQueueProcessModelFactory queueProcessModelFactory,
			IServiceScopeFactory serviceScopeFactory,
			IBaoCaoTongHopTaiSanService baoCaoTongHopTaiSanService,
			IDiaBanService diaBanService,
			ILyDoBienDongModelFactory lyDoBienDongModelFactory,
			IQuyetDinhTichThuModelFactory quyetDinhTichThuModelFactory,
			INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory,
			IBaoCaoCongKhaiService baoCaoCongKhaiService,
			IBaoCaoTraCuuService baoCaoTraCuuService,
			IBaoCaoKeKhaiServices baoCaoKeKhaiServices,
			IBaoCaoQuocHoiService baoCaoQuocHoiService,
			IBaoCaoDuAnService baoCaoDuAnService,
			IInTheTaiSanServices inTheTaiSanServices,
			ITaiSanKiemKeService kiemKeTaiSanService,
			ITaiSanKiemKeHoiDongService taiSanKiemKeHoiDongService,
			IBaoCaoDoiChieuDuLieuService baoCaoDoiChieuDuLieuService,
			ITaiSanModelFactory taiSanModelFactory,
			ILoaiTaiSanService loaiTaiSanService
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._kiemKeService = kiemKeService;
			this._kiemKeHoiDongService = kiemKeHoiDongService;
			this._donViBoPhanModelFactory = donViBoPhanModelFactory;
			this._kiemKeCongCuService = kiemKeCongCuService;
			this._congCuDungCuService = congCuDungCuService;
			this._nhomCongCuService = nhomCongCuService;
			this._nhomCongCuModelFactory = nhomCongCuModelFactory;
			this._donViBoPhanService = donViBoPhanService;
			this._donViService = donViService;
			this._cheDoKeToanService = cheDoKeToanService;
			this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			this._cauHinhService = cauHinhService;
			this._donViModelFactory = donViModelFactory;
			this._taiSanToanDanService = taiSanToanDanService;
			this._nhatKyService = nhatKyService;
			this._taiSanBCCTService = taiSanBCCTService;
			this._reportModelFactory = reportModelFactory;
			this._queueProcessService = queueProcessService;
			this._queueProcessModelFactory = queueProcessModelFactory;
			this._serviceScopeFactory = serviceScopeFactory;
			this._baoCaoTongHopTaiSanService = baoCaoTongHopTaiSanService;
			this._diaBanService = diaBanService;
			this._lyDoBienDongModelFactory = lyDoBienDongModelFactory;
			this._quyetDinhTichThuModelFactory = quyetDinhTichThuModelFactory;
			this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
			this._baoCaoCongKhaiService = baoCaoCongKhaiService;
			this._baoCaoTraCuuService = baoCaoTraCuuService;
			this._baoCaoKeKhaiServices = baoCaoKeKhaiServices;
			this._baoCaoQuocHoiService = baoCaoQuocHoiService;
			this._baoCaoDuAnService = baoCaoDuAnService;
			this._inTheTaiSanServices = inTheTaiSanServices;
			this._kiemKeTaiSanService = kiemKeTaiSanService;
			this._taiSanKiemKeHoiDongService = taiSanKiemKeHoiDongService;
			_baoCaoDoiChieuDuLieuService = baoCaoDoiChieuDuLieuService;
			this._taiSanModelFactory = taiSanModelFactory;
			this._loaiTaiSanService = loaiTaiSanService;
		}
		#endregion
		public IActionResult Index()
		{
			return View();
		}
		public virtual IActionResult CCKTTaiSanSoLieu()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTraCuuSoLieu))
				return AccessDeniedView();
			var searchModel = new BaoCaoTaiSanChiTietSearchModel();
			searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTraCuuSearchModel(searchModel, true);
			return View(searchModel);
		}

		[HttpPost]
		public virtual IActionResult ListCCKTTaiSanSoLieu(BaoCaoTaiSanChiTietSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTraCuuSoLieu))
				return AccessDeniedView();
			var m = new TaiSanSearchModel();
			searchModel.NgayKetThuc = DateTime.Now;
			var model = _taiSanModelFactory.LogicDanhSachTaiSan(searchModel);
			return Json(model);
		}

		public virtual IActionResult CCKTTaiSanSoLieu_(BaoCaoTaiSanChiTietSearchModel searchModel)
		{
			var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
			var loaiTaiSanEntity = _loaiTaiSanService.GetLoaiTaiSanById(searchModel.LoaiTaiSanId);
			if (cauHinh.BaoCao != null)
			{
				if (loaiTaiSanEntity != null && (loaiTaiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || loaiTaiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA))
				{
					cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCKTTaiSanSoLieuDaNhap_PL02).FirstOrDefault() ?? new CauHinhBaoCaoModel();
				}
				else
				{
					cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCKTTaiSanSoLieuDaNhap_PL03).FirstOrDefault() ?? new CauHinhBaoCaoModel();
				}
				
			}
			searchModel.NgayKetThuc = DateTime.Now;
			_reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
			XtraReport model = new rptCCKTSoLieuTaiSanDaNhap_PL03(searchModel, cauHinhModel, cauHinhChunghModel);
			if (loaiTaiSanEntity != null && (loaiTaiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || loaiTaiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA))
			{
				model = new rptCCKTSoLieuTaiSanDaNhap_PL02(searchModel, cauHinhModel, cauHinhChunghModel);
			}
			var obj = _baoCaoTraCuuService.KTLOGIC_SO_LIEU_DAU_KY(
				   NgayKetThuc: searchModel.NgayKetThuc,
				   DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
				   stringLoaiTaiSan: searchModel.StringLoaiTaiSan,
				   LoaiDonViId: searchModel.LoaiTaiSanId,
				   ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(),
				   SoTang_CompareSign: searchModel.SoTang_CompareSign,
				   SoTang_Value1: searchModel.SoTang_Value1,
				   SoTang_Value2: searchModel.SoTang_Value2,
				   DienTich_CompareSign: searchModel.DienTich_CompareSign,
				   DienTich_Value1: searchModel.DienTich_Value1,
				   DienTich_Value2: searchModel.DienTich_Value2,
				   SoChoNgoi_CompareSign: searchModel.SoChoNgoi_CompareSign,
				   SoChoNgoi_Value1: searchModel.SoChoNgoi_Value1,
				   SoChoNgoi_Value2: searchModel.SoChoNgoi_Value2,
				   TaiTrong_CompareSign: searchModel.TaiTrong_CompareSign,
				   TaiTrong_Value1: searchModel.TaiTrong_Value1,
				   TaiTrong_Value2: searchModel.TaiTrong_Value2,
				   TongNguyenGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
				   TongNguyenGia_Value1: searchModel.TongNguyenGia_Value1,
				   TongNguyenGia_Value2: searchModel.TongNguyenGia_Value2,
				   stringLoaiDonVi: searchModel.StringLoaiDonVi,
				   ChucDanhId: searchModel.CHUC_DANH_ID);
			/*var obj = _baoCaoTraCuuService.BaoCaoTraCuuTS_BCTC_03_DK_TSNN(
				DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
				NgayKetThuc: searchModel.NgayKetThuc,
				ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(),
				donViTien: searchModel.DonViTien,
				DonViDienTich: searchModel.DonViDienTich,
				BacTaiSan: searchModel.BacTaiSan,
				stringLoaiTaiSan: searchModel.StringLoaiTaiSan,
				stringLoaiDonVi: searchModel.StringLoaiDonVi,
				NhanXeId: searchModel.NHAN_XE_ID,
				NamSD_CompareSign: searchModel.NamSD_CompareSign,
				NamSD_Value1: searchModel.NamSD_Value1,
				NamSD_Value2: searchModel.NamSD_Value2,
				NamSx_CompareSign: searchModel.NamSx_CompareSign,
				NamSx_Value1: searchModel.NamSx_Value1,
				NamSx_Value2: searchModel.NamSx_Value2,
				SoTang_CompareSign: searchModel.SoTang_CompareSign,
				SoTang_Value1: searchModel.SoTang_Value1,
				SoTang_Value2: searchModel.SoTang_Value2,
				DienTich_CompareSign: searchModel.DienTich_CompareSign,
				DienTich_Value1: searchModel.DienTich_Value1,
				DienTich_Value2: searchModel.DienTich_Value2,
				SoChoNgoi_CompareSign: searchModel.SoChoNgoi_CompareSign,
				SoChoNgoi_Value1: searchModel.SoChoNgoi_Value1,
				SoChoNgoi_Value2: searchModel.SoChoNgoi_Value2,
				TaiTrong_CompareSign: searchModel.TaiTrong_CompareSign,
				TaiTrong_Value1: searchModel.TaiTrong_Value1,
				TaiTrong_Value2: searchModel.TaiTrong_Value2,
				NguyenGiaNS_CompareSign: searchModel.NguyenGiaNS_CompareSign,
				NguyenGiaNS_Value1: searchModel.NguyenGiaNS_Value1,
				NguyenGiaNS_Value2: searchModel.NguyenGiaNS_Value2,
				NguyenGiaKhac_CompareSign: searchModel.NguyenGiaKhac_CompareSign,
				NguyenGiaKhac_Value1: searchModel.NguyenGiaKhac_Value1,
				NguyenGiaKhac_Value2: searchModel.NguyenGiaKhac_Value2,
				NguyenGiaVienTro_CompareSign: searchModel.NguyenGiaVienTro_CompareSign,
				NguyenGiaVienTro_Value1: searchModel.NguyenGiaVienTro_Value1,
				NguyenGiaVienTro_Value2: searchModel.NguyenGiaVienTro_Value2,
				NguyenGiaODA_CompareSign: searchModel.NguyenGiaODA_CompareSign,
				NguyenGiaODA_Value1: searchModel.NguyenGiaODA_Value1,
				NguyenGiaODA_Value2: searchModel.NguyenGiaODA_Value2,
				TongNguyenGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
				TongNguyenGia_Value1: searchModel.TongNguyenGia_Value1,
				TongNguyenGia_Value2: searchModel.TongNguyenGia_Value2,
				TyLeChatLuong_CompareSign: searchModel.TyLeChatLuong_CompareSign,
				TyLeChatLuong_Value1: searchModel.TyLeChatLuong_Value1,
				TyLeChatLuong_Value2: searchModel.TyLeChatLuong_Value2,
				GTCL_CompareSign: searchModel.GTCL_CompareSign,
				GTCL_Value1: searchModel.GTCL_Value2,
				GTCL_Value2: searchModel.DienTich_Value2);*/
			model.DataSource = obj;
			return ShowViewReport(model);
		}
	}
}
