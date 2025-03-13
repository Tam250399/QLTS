using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.TaiSans;
using GS.Web.Factories.HeThong;
using GS.Web.Models.KeToan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.KeToan
{
	public class HaoMonTaiSanLogModelFactory : IHaoMonTaiSanLogModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ILoaiTaiSanService _loaiTaiSanService;
		private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
		private readonly IBienDongService _bienDongService;
		private readonly IHaoMonTaiSanLogService _itemService;
		private readonly IHaoMonTaiSanService _haoMonTaiSanService;
		private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
		private readonly ICauHinhService _cauHinhService;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
		private readonly ITaiSanService _taiSanService;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
		// private readonly ITaiSanModelFactory _taiSanModelFactory;
		#endregion
		#region Ctor
		public HaoMonTaiSanLogModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ILoaiTaiSanService loaiTaiSanService,
			ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
			IBienDongService bienDongService,
			IHaoMonTaiSanLogService itemService,
			ICauHinhService cauHinhService,
			ICauHinhModelFactory cauHinhModelFactory,
			ITaiSanService  taiSanService,
			IBienDongChiTietService bienDongChiTietService,
			IHaoMonTaiSanService haoMonTaiSanService,
			ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
			IKhauHaoTaiSanService khauHaoTaiSanService

		)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._loaiTaiSanService = loaiTaiSanService;
			this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
			this._bienDongService = bienDongService;
			this._itemService = itemService;
			this._cauHinhService = cauHinhService;
			this._cauHinhModelFactory = cauHinhModelFactory;
			this._taiSanService = taiSanService;
			this._bienDongChiTietService = bienDongChiTietService;
			this._haoMonTaiSanService = haoMonTaiSanService;
			this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
			this._khauHaoTaiSanService = khauHaoTaiSanService;
		}
		#endregion ctor
		public HaoMonTaiSanModel ChotHaoMon(decimal taiSanId, decimal namHaoMon)
		{
			if (!(taiSanId > 0))
				return null;
			HaoMonTaiSanModel haoMonTaiSan_new = new HaoMonTaiSanModel();
			bool has_hmts_LastYear = false;
			bool has_BDChot_ThisYear = false;
			DateTime BDChot_NgayBienDong = new DateTime(); //ngày biến động được người nhập chốt gtcl => k cần tính lại gtcl của bd này. Tính tiếp từ ngày này
			BienDongChiTiet BDChot_bdct = new BienDongChiTiet(); //Lưu thông tin biến động chốt giá trị còn lại
																 //Lấy các thông tin liên quan tới tài sản: các biến động, loại tài sản,...
			KTGiaTriTaiSan ktGiaTriTaiSan = new KTGiaTriTaiSan(); //Lưu trữ các thông tin cần để tính toán các giá trị
			TaiSan taiSanEntity = _taiSanService.GetTaiSanById(taiSanId);
			LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(taiSanEntity.LOAI_TAI_SAN_ID??0);
			var listBienDongTaiSan = _bienDongService.GetBienDongsByTaiSanId(taiSanId);
			var listBienDongChiTietTaiSan = _bienDongChiTietService.GetAllBienDongChiTiets().Where(c => c.biendong.TAI_SAN_ID == taiSanId && c.biendong.NGAY_BIEN_DONG.Value.Year <= namHaoMon);
			//Lấy biến động đầu tiên (bd tăng mới, nhập số dư sinh ra ts)
			BienDong bdTangTS = listBienDongTaiSan.OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
			BienDongChiTiet bdctTangTS = _bienDongChiTietService.GetBienDongChiTietByBDId(bdTangTS.ID);
			//Tỷ lệ nguyên giá hao mòn, khấu hao
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH = ((bdctTangTS.KH_GIA_TINH_KHAU_HAO ?? 0) / bdTangTS.NGUYEN_GIA);
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM = 1 - (ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			//
			ktGiaTriTaiSan.LoaiHinhTaiSanId = taiSanEntity.LOAI_HINH_TAI_SAN_ID;
			if (taiSanEntity != null && taiSanEntity.HM_TY_LE > 0)
			{
				ktGiaTriTaiSan.HM_TyLe = taiSanEntity.HM_TY_LE;
			}
			else
			{
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
				{
					ktGiaTriTaiSan.HM_TyLe = taiSanEntity.loaitaisandonvi.HM_TY_LE;
				}
				else
				{
					ktGiaTriTaiSan.HM_TyLe = taiSanEntity.loaitaisan.HM_TY_LE;
				}
			}
			//Lấy bản ghi hao mòn kt năm trước (nếu có)
			HaoMonTaiSan hmts_lastYear = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId:taiSanId,namBaoCao:(namHaoMon-1));

			//không có bản ghi trước
			if (hmts_lastYear == null)
			{
				has_hmts_LastYear = false;
				hmts_lastYear = new HaoMonTaiSan();
				hmts_lastYear.TONG_NGUYEN_GIA = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
				{
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = bdTangTS.NGUYEN_GIA;
					hmts_lastYear.TONG_HAO_MON_LUY_KE = 0;
				}
				else
				{
					//bdctDauTien.HM_Gia_tri_con_lai: là giá trị còn lại sau khấu hao lẫn hao mòn
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
					hmts_lastYear.TONG_HAO_MON_LUY_KE = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM - bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
				}
				BDChot_NgayBienDong = bdTangTS.NGAY_BIEN_DONG.Value;
			}
			else
			{
				has_hmts_LastYear = true;
			}
			//check bien dong trong năm
			var listBDCTthisYear = listBienDongChiTietTaiSan.Where(c => c.biendong.NGAY_BIEN_DONG.Value.Year == namHaoMon);
			if (listBDCTthisYear != null && listBDCTthisYear.Count() > 0)
			{
				//lấy biến động đã được chốt giá trị
				var listBDCTChotGTCL = listBDCTthisYear.Where(c => c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY && c.HM_GIA_TRI_CON_LAI != null).OrderBy(c => c.biendong.NGAY_BIEN_DONG);
				if (listBDCTChotGTCL != null && listBDCTChotGTCL.Count() > 0)
				{
					has_BDChot_ThisYear = true;
					BDChot_bdct = listBDCTChotGTCL.FirstOrDefault();
					hmts_lastYear.TONG_GIA_TRI_CON_LAI = BDChot_bdct.HM_GIA_TRI_CON_LAI;
					BDChot_NgayBienDong = BDChot_bdct.biendong.NGAY_BIEN_DONG.Value;
				}
				else
				{
					has_BDChot_ThisYear = false;
				}
				//lấy các thông tin chốt + các biến động tiếp sau
				var list_bdctSauChot = listBDCTthisYear.Where(c => c.biendong.NGAY_BIEN_DONG > BDChot_NgayBienDong);
				if (list_bdctSauChot != null && list_bdctSauChot.Count() > 0)
				{
					ktGiaTriTaiSan.TS_NguyenGiaBienDong = list_bdctSauChot.Sum(c => c.biendong.NGUYEN_GIA);
					hmts_lastYear.TONG_HAO_MON_LUY_KE = ((hmts_lastYear.TONG_NGUYEN_GIA ?? 0) + (ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM) - (hmts_lastYear.TONG_GIA_TRI_CON_LAI ?? 0);
				}
			}
			//Tính toán
			ktGiaTriTaiSan.HM_GiaTriTinh = hmts_lastYear.TONG_GIA_TRI_CON_LAI + ((ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM);
			ktGiaTriTaiSan.TS_NguyenGiaTinhHM = (listBienDongTaiSan.Where(c => c.NGAY_BIEN_DONG.Value.Year <= namHaoMon).Sum(c => c.NGUYEN_GIA)) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM;
			haoMonTaiSan_new.TONG_NGUYEN_GIA = ktGiaTriTaiSan.TS_NguyenGiaTinhHM;
			haoMonTaiSan_new.TY_LE_HAO_MON = ktGiaTriTaiSan.HM_TyLe;
			haoMonTaiSan_new.MA_TAI_SAN = taiSanEntity.MA;
			if (BDChot_NgayBienDong.Day == 31 && BDChot_NgayBienDong.Month == 12)
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = 0;
			}
			else
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = Math.Round((ktGiaTriTaiSan.HM_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhHM ?? 0) / 100);
			}
			if (haoMonTaiSan_new.GIA_TRI_HAO_MON > ktGiaTriTaiSan.HM_GiaTriTinh)
			{
				haoMonTaiSan_new.TONG_HAO_MON_LUY_KE = hmts_lastYear.TONG_HAO_MON_LUY_KE + ktGiaTriTaiSan.HM_GiaTriTinh;
				haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI = 0;
			}
			else
			{
				haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI = ktGiaTriTaiSan.HM_GiaTriTinh??0 - haoMonTaiSan_new.GIA_TRI_HAO_MON??0;
				//haoMonTaiSan_new.TONG_HAO_MON_LUY_KE = hmts_lastYear.TONG_HAO_MON_LUY_KE + haoMonTaiSan_new.GIA_TRI_HAO_MON;
				haoMonTaiSan_new.TONG_HAO_MON_LUY_KE = hmts_lastYear.TONG_NGUYEN_GIA - haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI;
			}
			if (!has_hmts_LastYear || has_BDChot_ThisYear)
				haoMonTaiSan_new.GIA_TRI_HAO_MON = Math.Round((ktGiaTriTaiSan.HM_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhHM ?? 0) / 100);
			if (ktGiaTriTaiSan.HM_GiaTriTinh == 0 || hmts_lastYear.TONG_HAO_MON_LUY_KE == ktGiaTriTaiSan.TS_NguyenGiaTinhHM)
				haoMonTaiSan_new.GIA_TRI_HAO_MON = 0;
			if (haoMonTaiSan_new.TONG_GIA_TRI_CON_LAI == 0 && ktGiaTriTaiSan.HM_GiaTriTinh > 0)
			{
				haoMonTaiSan_new.GIA_TRI_HAO_MON = ktGiaTriTaiSan.HM_GiaTriTinh;
			}
			//thông tin tài sản, năm
			haoMonTaiSan_new.TAI_SAN_ID = taiSanId;
			haoMonTaiSan_new.NAM_HAO_MON = namHaoMon;
			return haoMonTaiSan_new;
		}

		public KhauHaoTaiSanModel ChotKhauHao(decimal taiSanId, decimal namKhauHao, decimal thangKhauHao)
		{
			if (!(taiSanId > 0))
				return null;
			KhauHaoTaiSanModel khauHaoTaiSan_new = new KhauHaoTaiSanModel();
			//Điều kiện lấy bản ghi tháng trước
			var preMonth = thangKhauHao;
			var preYear = namKhauHao;
			bool has_kh_lastMonth = false;
			bool has_BDChot_thisMonth = false;
			if ((thangKhauHao - 1) <= 0)
			{
				preMonth = 12;
				preYear = namKhauHao - 1;
			}
			else
			{
				preMonth = thangKhauHao - 1;
				preYear = namKhauHao;
			}
			DateTime BDChot_NgayBienDong = new DateTime(); //ngày biến động được người nhập chốt gtcl => k cần tính lại gtcl của bd này. Tính tiếp từ ngày này
			BienDongChiTiet BDChot_bdct = new BienDongChiTiet(); //Lưu thông tin biến động chốt giá trị còn lại
																 //Lấy các thông tin liên quan tới tài sản: các biến động, loại tài sản,...
			KTGiaTriTaiSan ktGiaTriTaiSan = new KTGiaTriTaiSan(); //Lưu trữ các thông tin cần để tính toán các giá trị
			TaiSan taiSanEntity = _taiSanService.GetTaiSanById(taiSanId);
			LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(taiSanEntity.LOAI_TAI_SAN_ID ?? 0);
			LoaiTaiSanKhauHao loaiTaiSanKhauHao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId:taiSanEntity.LOAI_TAI_SAN_ID,donViId: taiSanEntity.DON_VI_ID);
			var listBienDongTaiSan = _bienDongService.GetBienDongsByTaiSanId(taiSanId);
			var listBienDongChiTietTaiSan = _bienDongChiTietService.GetAllBienDongChiTiets().Where(c => c.biendong.TAI_SAN_ID == taiSanId && c.biendong.NGAY_BIEN_DONG.Value.Year <= namKhauHao);
			//Lấy biến động đầu tiên (bd tăng mới, nhập số dư sinh ra ts)
			BienDong bdTangTS = listBienDongTaiSan.OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
			BienDongChiTiet bdctTangTS = _bienDongChiTietService.GetBienDongChiTietByBDId(bdTangTS.ID);
			//Tỷ lệ nguyên giá hao mòn, khấu hao
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH = ((bdctTangTS.KH_GIA_TINH_KHAU_HAO ?? 0) / bdTangTS.NGUYEN_GIA);
			ktGiaTriTaiSan.TS_TyLeNguyenGiaTinhHM = 1 - (ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			ktGiaTriTaiSan.KH_ThangConLai = bdctTangTS.KH_THANG_CON_LAI;

			//
			ktGiaTriTaiSan.LoaiHinhTaiSanId = taiSanEntity.LOAI_HINH_TAI_SAN_ID;
			if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
			{
				ktGiaTriTaiSan.KH_TyLe = taiSanEntity.loaitaisandonvi.KH_TY_LE;
			}
			else
			{
				if (loaiTaiSanKhauHao != null)
				{
					ktGiaTriTaiSan.KH_TyLe = loaiTaiSanKhauHao.TY_LE_KHAU_HAO;
				}
				else
				{
					ktGiaTriTaiSan.KH_TyLe = loaiTaiSan.HM_TY_LE;
				}
			}
			//Lấy bản ghi khấu hao kt tháng trước (nếu có)
			KhauHaoTaiSan khts_lastMonth = _khauHaoTaiSanService.GetListKhauHaoTaiSans(taiSanId:taiSanId,namKhauHao:preYear,thangKhauHao:preMonth).FirstOrDefault();
			if (khts_lastMonth == null)
			{
				has_kh_lastMonth = false;
				khts_lastMonth = new KhauHaoTaiSan();
				khts_lastMonth.TONG_NGUYEN_GIA = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
				if (taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taiSanEntity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
				{
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = bdTangTS.NGUYEN_GIA;
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = 0;
				}
				else
				{
					//bdctDauTien.HM_Gia_tri_con_lai: là giá trị còn lại sau khấu hao lẫn hao mòn
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = bdTangTS.NGUYEN_GIA * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH - bdctTangTS.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
				}
				BDChot_NgayBienDong = bdTangTS.NGAY_BIEN_DONG.Value;

			}
			else
			{
				has_kh_lastMonth = true;

			}
			//check bien dong trong tháng
			var listBDCTThisMonth = listBienDongChiTietTaiSan.Where(c => c.biendong.NGAY_BIEN_DONG.Value.Year == namKhauHao && c.biendong.NGAY_BIEN_DONG.Value.Month == thangKhauHao);
			if (listBDCTThisMonth != null && listBDCTThisMonth.Count() > 0)
			{
				//lấy biến động đã được chốt giá trị
				var listBDCTChotGTCL = listBDCTThisMonth.Where(c => c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && c.biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY && c.HM_GIA_TRI_CON_LAI != null).OrderBy(c => c.biendong.NGAY_BIEN_DONG);
				if (listBDCTChotGTCL != null && listBDCTChotGTCL.Count() > 0)
				{
					has_BDChot_thisMonth = true;
					BDChot_bdct = listBDCTChotGTCL.FirstOrDefault();
					khts_lastMonth.TONG_GIA_TRI_CON_LAI = BDChot_bdct.HM_GIA_TRI_CON_LAI * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
					BDChot_NgayBienDong = BDChot_bdct.biendong.NGAY_BIEN_DONG.Value;
				}
				else
				{
					has_BDChot_thisMonth = false;
				}
				//lấy các thông tin chốt + các biến động tiếp sau(trong tháng)
				var list_bdctSauChot = listBDCTThisMonth.Where(c => c.biendong.NGAY_BIEN_DONG > BDChot_NgayBienDong);
				if (list_bdctSauChot != null && list_bdctSauChot.Count() > 0)
				{
					ktGiaTriTaiSan.TS_NguyenGiaBienDong = list_bdctSauChot.Sum(c => c.biendong.NGUYEN_GIA);
					khts_lastMonth.TONG_KHAU_HAO_LUY_KE = ((khts_lastMonth.TONG_NGUYEN_GIA ?? 0) + (ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH) - (khts_lastMonth.TONG_GIA_TRI_CON_LAI ?? 0);
				}
			}

			//Tính toán
			ktGiaTriTaiSan.KH_GiaTriTinh = khts_lastMonth.TONG_GIA_TRI_CON_LAI + ((ktGiaTriTaiSan.TS_NguyenGiaBienDong ?? 0) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH);
			ktGiaTriTaiSan.TS_NguyenGiaTinhKH = listBienDongTaiSan.Where(c => c.NGAY_BIEN_DONG.Value.Year < namKhauHao || (c.NGAY_BIEN_DONG.Value.Year == namKhauHao && c.NGAY_BIEN_DONG.Value.Month <= thangKhauHao)).Sum(c => c.NGUYEN_GIA) * ktGiaTriTaiSan.TS_TyLeNguyenGiaTrichKH;
			khauHaoTaiSan_new.TONG_NGUYEN_GIA = ktGiaTriTaiSan.TS_NguyenGiaTinhKH;
			khauHaoTaiSan_new.TY_LE_KHAU_HAO = ktGiaTriTaiSan.KH_TyLe;
			khauHaoTaiSan_new.MA_TAI_SAN = taiSanEntity.MA;
			if (BDChot_NgayBienDong.Day == 31 && BDChot_NgayBienDong.Month == 12)
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = 0;
			}
			else
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = Math.Round((ktGiaTriTaiSan.KH_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhKH ?? 0) / 100);
			}
			if (khauHaoTaiSan_new.GIA_TRI_KHAU_HAO > ktGiaTriTaiSan.HM_GiaTriTinh)
			{
				khauHaoTaiSan_new.TONG_KHAU_HAO_LUY_KE = khts_lastMonth.TONG_KHAU_HAO_LUY_KE + ktGiaTriTaiSan.KH_GiaTriTinh;
				khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI = 0;
			}
			else
			{
				khauHaoTaiSan_new.TONG_KHAU_HAO_LUY_KE = khts_lastMonth.TONG_KHAU_HAO_LUY_KE + khauHaoTaiSan_new.GIA_TRI_KHAU_HAO;
				khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI = ktGiaTriTaiSan.KH_GiaTriTinh - khauHaoTaiSan_new.GIA_TRI_KHAU_HAO;
			}
			if (!has_kh_lastMonth || has_BDChot_thisMonth)
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = Math.Round((ktGiaTriTaiSan.KH_TyLe ?? 0) * (ktGiaTriTaiSan.TS_NguyenGiaTinhKH ?? 0) / 100);
			if (ktGiaTriTaiSan.KH_GiaTriTinh == 0 || khts_lastMonth.TONG_KHAU_HAO_LUY_KE == ktGiaTriTaiSan.TS_NguyenGiaTinhKH)
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = 0;
			if (khauHaoTaiSan_new.TONG_GIA_TRI_CON_LAI == 0 && ktGiaTriTaiSan.KH_GiaTriTinh > 0)
			{
				khauHaoTaiSan_new.GIA_TRI_KHAU_HAO = ktGiaTriTaiSan.KH_GiaTriTinh;
			}
			//thông tin tài sản, tháng, nam
			khauHaoTaiSan_new.TAI_SAN_ID = taiSanId;
			khauHaoTaiSan_new.THANG_KHAU_HAO = thangKhauHao;
			khauHaoTaiSan_new.NAM_KHAU_HAO = namKhauHao;
			return khauHaoTaiSan_new;
		}

		public bool ChotToanBoTaiSan(decimal? donViId = 0, decimal? namKeKhai = 0, decimal? taiSanId = 0, decimal? loaiHinhTaiSanId = 0, bool checkLogHaoMon = true)
		{
			DateTime currentDate = DateTime.Now;			
			var taiSans_list = _taiSanService.GetAllTaiSans().Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
			if (checkLogHaoMon)
			{
			var list_log_ts_id = _itemService.GetAllHaoMonTaiSanLogs().Select(c => c.TAI_SAN_ID).ToList();
				taiSans_list = taiSans_list.Where(c => list_log_ts_id.Contains(c.ID));
			}
			//var taiSans_list = _taiSanRepository.Table.Where(c => c.IS_DUYET == true);
			if (taiSanId > 0)
				taiSans_list = taiSans_list.Where(c => c.ID == taiSanId);
			if (donViId > 0)
				taiSans_list = taiSans_list.Where(c => c.DON_VI_ID == donViId);
			if (namKeKhai > 0)
				taiSans_list = taiSans_list.Where(c => c.NGAY_NHAP.Value.Year == namKeKhai);
			if (loaiHinhTaiSanId > 0)
				taiSans_list = taiSans_list.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId);
			foreach (var item in taiSans_list)
			{
				//chốt hao mòn qua các năm
				for (int year = item.NGAY_NHAP.Value.Year; year <= currentDate.Year; year++)
				{
					HaoMonTaiSanModel hmtsModel_new = new HaoMonTaiSanModel();
					HaoMonTaiSan hmts_old = new HaoMonTaiSan();
					hmtsModel_new = ChotHaoMon(item.ID, year);
					hmts_old = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId:item.ID,namBaoCao:year);
					if (hmts_old != null)
					{
						hmts_old = PrepareHMTSEntity(hmtsEntity: hmts_old, hmtsData: hmtsModel_new);
						_haoMonTaiSanService.UpdateHaoMonTaiSan(hmts_old);
					}
					else
					{
						hmts_old = PrepareHMTSEntity(hmtsEntity: hmts_old, hmtsData: hmtsModel_new);
						_haoMonTaiSanService.InsertHaoMonTaiSan(hmts_old);
					}
				}
				//chốt khấu hao
				BienDongChiTiet bdctTangTS = _bienDongChiTietService.GetAllBienDongChiTiets().Where(c => c.biendong.TAI_SAN_ID == item.ID && (c.biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || c.biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)).FirstOrDefault();
				if (bdctTangTS != null && bdctTangTS.KH_GIA_TINH_KHAU_HAO > 0)
				{

					for (int month = item.NGAY_NHAP.Value.Month; month <= 12; month++)
					{
						//Tính khấu hao từ ngày nhập => tháng 12 cùng năm
						KhauHaoTaiSanModel khtsModel_new = new KhauHaoTaiSanModel();
						khtsModel_new = ChotKhauHao(item.ID, item.NGAY_NHAP.Value.Year, month);
						KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
						khts_old = _khauHaoTaiSanService.GetListKhauHaoTaiSans(taiSanId: item.ID, namKhauHao: item.NGAY_NHAP.Value.Year, thangKhauHao: month).FirstOrDefault();
						if (khts_old != null)
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
							_khauHaoTaiSanService.UpdateKhauHaoTaiSan(khts_old);
						}
						else
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
							_khauHaoTaiSanService.InsertKhauHaoTaiSan(khts_old);
						}
					}
					for (int year = (item.NGAY_NHAP.Value.Year + 1); year < currentDate.Year; year++)
					{
						//Tính khấu hao các năm tiếp theo => năm hiện tại - 1
						for (int month = item.NGAY_NHAP.Value.Month; month <= 12; month++)
						{
							KhauHaoTaiSanModel khtsModel_new = new KhauHaoTaiSanModel();
							KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
							khtsModel_new = ChotKhauHao(item.ID, year, month);
							khts_old = _khauHaoTaiSanService.GetListKhauHaoTaiSans(taiSanId: item.ID, namKhauHao: year, thangKhauHao: month).FirstOrDefault();
							if (khts_old != null)
							{
								khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
								_khauHaoTaiSanService.UpdateKhauHaoTaiSan(khts_old);
							}
							else
							{
								khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
								_khauHaoTaiSanService.InsertKhauHaoTaiSan(khts_old);
							}
						}
					}
					for (int month = 1; month <= 12; month++)
					{
						//Tính khấu hao các tháng năm hiện tại
						KhauHaoTaiSanModel khtsModel_new = new KhauHaoTaiSanModel();
						KhauHaoTaiSan khts_old = new KhauHaoTaiSan();
						khtsModel_new = ChotKhauHao(item.ID, currentDate.Year, month);
						khts_old = _khauHaoTaiSanService.GetListKhauHaoTaiSans(taiSanId: item.ID, namKhauHao: currentDate.Year, thangKhauHao: month).FirstOrDefault();
						if (khts_old != null)
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
							_khauHaoTaiSanService.UpdateKhauHaoTaiSan(khts_old);
						}
						else
						{
							khts_old = PrepareKHTSEntity(khtsEntity: khts_old, khtsData: khtsModel_new);
							_khauHaoTaiSanService.InsertKhauHaoTaiSan(khts_old);
						}
					}
				}
				//Khi chốt xong xóa log trong haoMonTaiSanLog nếu có
				HaoMonTaiSanLog logHMTS = _itemService.GetHaoMonTaiSanLogByTaiSanId(item.ID);
				if (logHMTS != null)
				{
					_itemService.DeleteHaoMonTaiSanLog(logHMTS);
				}
				//update lại trạng thái chốt giá trị ()
				//TaiSan taiSanEntity = _taiSanRepository.GetById(item.ID);
				//taiSanEntity.IS_DUYET = true;
				//_taiSanRepository.Update(taiSanEntity);
			}
			return true;
		}

		public KhauHaoTaiSan PrepareKHTSEntity(KhauHaoTaiSan khtsEntity, KhauHaoTaiSanModel khtsData)
		{
			khtsEntity.MA_TAI_SAN = khtsData.MA_TAI_SAN;
			khtsEntity.GIA_TRI_KHAU_HAO = khtsData.GIA_TRI_KHAU_HAO;
			khtsEntity.TONG_KHAU_HAO_LUY_KE = khtsData.TONG_KHAU_HAO_LUY_KE;
			khtsEntity.TONG_GIA_TRI_CON_LAI = khtsData.TONG_GIA_TRI_CON_LAI;
			khtsEntity.TY_LE_KHAU_HAO = khtsData.TY_LE_KHAU_HAO;
			khtsEntity.TONG_NGUYEN_GIA = khtsData.TONG_NGUYEN_GIA;
			khtsEntity.TAI_SAN_ID = khtsData.TAI_SAN_ID;
			khtsEntity.THANG_KHAU_HAO = khtsData.THANG_KHAU_HAO;
			khtsEntity.NAM_KHAU_HAO = khtsData.NAM_KHAU_HAO;
			return khtsEntity;
		}
		public HaoMonTaiSan PrepareHMTSEntity(HaoMonTaiSan hmtsEntity, HaoMonTaiSanModel hmtsData)
		{
			hmtsEntity.MA_TAI_SAN = hmtsData.MA_TAI_SAN;
			hmtsEntity.GIA_TRI_HAO_MON = hmtsData.GIA_TRI_HAO_MON;
			hmtsEntity.TONG_HAO_MON_LUY_KE = hmtsData.TONG_HAO_MON_LUY_KE;
			hmtsEntity.TONG_GIA_TRI_CON_LAI = hmtsData.TONG_GIA_TRI_CON_LAI;
			hmtsEntity.TY_LE_HAO_MON = hmtsData.TY_LE_HAO_MON;
			hmtsEntity.TONG_NGUYEN_GIA = hmtsData.TONG_NGUYEN_GIA;
			hmtsEntity.TAI_SAN_ID = hmtsData.TAI_SAN_ID;
			hmtsEntity.NAM_HAO_MON = hmtsData.NAM_HAO_MON;
			return hmtsEntity;
		}
	}
}
