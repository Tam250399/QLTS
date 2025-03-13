using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.NghiepVu;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.BienDongs
{
	public partial class TrungGianBDYCService : ITrungGianBDYCService
	{
		#region Fields

		private readonly IWorkContext _workContext;
		private readonly IRepository<YeuCau> _yeuCauRepository;
		private readonly IRepository<BienDong> _bienDongRepository;
		private readonly IRepository<DonVi> _donviRepository;
		private readonly IRepository<TaiSan> _taiSanRepository;
		private readonly IRepository<YeuCauChiTiet> _yeuCauChiTietRepository;
		private readonly IRepository<BienDongChiTiet> _bienDongChiTietRepository;
		private readonly IRepository<NguoiDungDonViMapping> _nguoidungdonviMappingRepository;
		private readonly IBienDongChiTietService _bienDongChiTietService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IBienDongService _bienDongService;
		private readonly IYeuCauService _yeuCauService;

		#endregion Fields

		#region Ctor

		public TrungGianBDYCService(
			IRepository<YeuCau> yeuCauRepository,
			IWorkContext workContext,
			IRepository<NguoiDungDonViMapping> nguoidungdonviMappingRepository,
			IRepository<DonVi> donviRepository,
			IRepository<TaiSan> taiSanRepository,
			IRepository<BienDong> bienDongRepository,
			IBienDongChiTietService bienDongChiTietService,
			IYeuCauChiTietService yeuCauChiTietService,
			IBienDongService bienDongService,
			IYeuCauService yeuCauService,
			IRepository<YeuCauChiTiet> yeuCauChiTietRepository,
			IRepository<BienDongChiTiet> bienDongChiTietRepository
			)
		{
			this._yeuCauRepository = yeuCauRepository;
			this._workContext = workContext;
			this._nguoidungdonviMappingRepository = nguoidungdonviMappingRepository;
			this._donviRepository = donviRepository;
			this._bienDongRepository = bienDongRepository;
			this._taiSanRepository = taiSanRepository;
			this._bienDongChiTietService = bienDongChiTietService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._bienDongService = bienDongService;
			this._yeuCauService = yeuCauService;
			this._bienDongChiTietRepository = bienDongChiTietRepository;
			this._yeuCauChiTietRepository = yeuCauChiTietRepository;
		}

		#endregion Ctor

		#region Methods

		public IPagedList<TrungGianBDYC> SearchYeuCauVaBienDong(int pageIndex = 0, int pageSize = int.MaxValue,
										string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
										decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
										decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
										 IList<decimal?> loaiBienDongIds = null, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
										 decimal? taisanId = 0, bool isIgnoreTraLai = false, string quyetDinhSo = null, bool isDuyet = false)

		{
			var yc = _yeuCauRepository.Table.Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
			var bd = _bienDongRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
			if (taisanId > 0)
			{
				yc = yc.Where(c => c.TAI_SAN_ID == taisanId);
				bd = bd.Where(c => c.TAI_SAN_ID == taisanId);
			}
			if (trangThaiId > 0)
			{
				if (trangThaiId == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
				{
					//đã duyệt thì không lấy yêu cầu vì đã có biến động
					yc = yc.Where(p => p.ID == 0);
				}
				else
				{
					yc = yc.Where(c => c.TRANG_THAI_ID == trangThaiId);
					//biến động tức là đã duyệt. Không lấy trạng thái đã duyệt tức không lấy biến động
					bd = bd.Where(p => p.ID == 0);
				}
			}
			else// lấy tất cả trạng thái
			{
				//không lấy đã duyệt vì đã có biến động còn đâu lấy tất
				yc = yc.Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET);
			}
			if (!String.IsNullOrEmpty(keySearch))
			{
				yc = yc.Where(c => c.TAI_SAN_TEN.Contains(keySearch, StringComparison.OrdinalIgnoreCase) || c.TAI_SAN_MA.Contains(keySearch, StringComparison.OrdinalIgnoreCase));
				bd = bd.Where(c => c.TAI_SAN_TEN.Contains(keySearch, StringComparison.OrdinalIgnoreCase) || c.TAI_SAN_MA.Contains(keySearch, StringComparison.OrdinalIgnoreCase));
			}
			if (loaiHinhTSId > 0)
			{
				yc = yc.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
				bd = bd.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
			}
			if (loaiTSId > 0)
			{
				yc = yc.Where(c => c.LOAI_TAI_SAN_ID == loaiTSId);
				bd = bd.Where(c => c.LOAI_TAI_SAN_ID == loaiTSId);
			}
			if (nguoiTaoId > 0)
			{
				yc = yc.Where(c => c.NGUOI_TAO_ID == nguoiTaoId || c.NGUOI_TAO_ID == null);
				bd = bd.Where(c => c.NGUOI_TAO_ID == nguoiTaoId || c.NGUOI_TAO_ID == null);
			}
			if (lyDoBienDongId > 0)
			{
				yc = yc.Where(c => c.LY_DO_BIEN_DONG_ID == lyDoBienDongId);
				bd = bd.Where(c => c.LY_DO_BIEN_DONG_ID == lyDoBienDongId);
			}
			if (boPhanId > 0)
			{
				yc = yc.Where(c => c.DON_VI_BO_PHAN_ID == boPhanId);
				bd = bd.Where(c => c.DON_VI_BO_PHAN_ID == boPhanId);
			}
			if (donViId > 0)
			{
				yc = yc.Where(c => c.DON_VI_ID == donViId);
				bd = bd.Where(c => c.DON_VI_ID == donViId);
			}
			if (loaiBienDongIds != null && loaiBienDongIds.Count > 0)
			{
				yc = yc.Where(c => loaiBienDongIds.Contains(c.LOAI_BIEN_DONG_ID));
				bd = bd.Where(c => loaiBienDongIds.Contains(c.LOAI_BIEN_DONG_ID));
			}
			if (fromdate.HasValue)
			{
				var _tungay = fromdate.Value.Date;
				yc = yc.Where(x => x.NGAY_BIEN_DONG >= _tungay);
				bd = bd.Where(x => x.NGAY_BIEN_DONG >= _tungay);
			}
			if (todate.HasValue && todate != DateTime.MinValue)
			{
				var _denngay = todate.Value.Date.AddDays(1);
				yc = yc.Where(x => x.NGAY_BIEN_DONG < _denngay);
				bd = bd.Where(x => x.NGAY_BIEN_DONG < _denngay);
			}
			if (!string.IsNullOrEmpty(chungTuSo))
			{
				yc = yc.Where(p => p.CHUNG_TU_SO.ToUpper().Contains(chungTuSo.ToUpper()));
				bd = bd.Where(p => p.CHUNG_TU_SO.ToUpper().Contains(chungTuSo.ToUpper()));
			}
			if (!string.IsNullOrEmpty(quyetDinhSo))
			{
				yc = yc.Where(p => p.QUYET_DINH_SO.ToUpper().Contains(quyetDinhSo.ToUpper()));
				bd = bd.Where(p => p.QUYET_DINH_SO.ToUpper().Contains(quyetDinhSo.ToUpper()));
			}
			//if (nguoiTaoId > 0)
			//{
			//	var DonViTreeNodes = _nguoidungdonviMappingRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiTaoId)
			//		 .Select(c => c.donvi.TREE_NODE);
			//	var donvis = from dv in _donviRepository.Table
			//				 from tn in DonViTreeNodes
			//				 where EF.Functions.Like(dv.TREE_NODE, tn + "%")
			//				 select dv;
			//	yc = yc.Join(donvis, x => x.DON_VI_ID, y => y.ID, (x, y) => new { yecau = x, donvi = y }).Select(c => c.yecau);
			//	bd = bd.Join(donvis, x => x.DON_VI_ID, y => y.ID, (x, y) => new { biendong = x, donvi = y }).Select(c => c.biendong);
			//}

			//không hiển thị tài sản trả lại
			if (isIgnoreTraLai)
				yc = yc.Where(p => p.TAI_SAN_ID > 0 && p.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);

			var res = yc.Select(p => new TrungGianBDYC(p)).Union(bd.Select(p => new TrungGianBDYC(p)));
			if (isDuyet)
				res = res.OrderBy(c => c.NGAY_BIEN_DONG);
			else
				res = res.OrderByDescending(c => c.NGAY_BIEN_DONG).ThenBy(c => c.TAI_SAN_MA);

			return new PagedList<TrungGianBDYC>(res, pageIndex, pageSize);
		}
		public IPagedList<TrungGianBDYC> SearchYCBDThanhLyChuaCapNhatTien(int pageIndex = 0, int pageSize = int.MaxValue,
										string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
										decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
										decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
										 decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
										 decimal? taisanId = 0, bool isIgnoreTraLai = false, string quyetDinhSo = null, bool isDuyet = false)
		{
			var yc = _yeuCauRepository.Table.Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
			var bd = _bienDongRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
			#region MyRegion

			yc = yc.Where(p => p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO);
			bd = bd.Where(p => p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO);

			//chỉ lấy ra những biến động có lý do này và chưa trả tiền 
			var list_ly_do_giam_can_thu_tien = Enum.GetValues(typeof(enumLY_DO_GIAM_THU_TIEN)).Cast<enumLY_DO_GIAM_THU_TIEN>().Select(p => (decimal?)p);
			yc = yc.Where(p => list_ly_do_giam_can_thu_tien.Contains(p.LY_DO_BIEN_DONG_ID));
			bd = bd.Where(p => list_ly_do_giam_can_thu_tien.Contains(p.LY_DO_BIEN_DONG_ID));

			#endregion
			if (taisanId > 0)
			{
				yc = yc.Where(c => c.TAI_SAN_ID == taisanId);
				bd = bd.Where(c => c.TAI_SAN_ID == taisanId);
			}
			if (trangThaiId > 0)
			{
				if (trangThaiId == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
				{
					//đã duyệt thì không lấy yêu cầu vì đã có biến động
					yc = yc.Where(p => p.ID == 0);
				}
				else
				{
					yc = yc.Where(c => c.TRANG_THAI_ID == trangThaiId);
					//biến động tức là đã duyệt. Không lấy trạng thái đã duyệt tức không lấy biến động
					bd = bd.Where(p => p.ID == 0);
				}
			}
			else// lấy tất cả trạng thái
			{
				//không lấy đã duyệt vì đã có biến động còn đâu lấy tất
				yc = yc.Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET);
			}
			if (!String.IsNullOrEmpty(keySearch))
			{
				yc = yc.Where(c => c.TAI_SAN_TEN.Contains(keySearch, StringComparison.OrdinalIgnoreCase) || c.TAI_SAN_MA.Contains(keySearch, StringComparison.OrdinalIgnoreCase));
				bd = bd.Where(c => c.TAI_SAN_TEN.Contains(keySearch, StringComparison.OrdinalIgnoreCase) || c.TAI_SAN_MA.Contains(keySearch, StringComparison.OrdinalIgnoreCase));
			}
			if (loaiHinhTSId > 0)
			{
				yc = yc.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
				bd = bd.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
			}
			if (loaiTSId > 0)
			{
				yc = yc.Where(c => c.LOAI_TAI_SAN_ID == loaiTSId);
				bd = bd.Where(c => c.LOAI_TAI_SAN_ID == loaiTSId);
			}
			if (nguoiTaoId > 0)
			{
				yc = yc.Where(c => c.NGUOI_TAO_ID == nguoiTaoId || c.NGUOI_TAO_ID == null);
				bd = bd.Where(c => c.NGUOI_TAO_ID == nguoiTaoId || c.NGUOI_TAO_ID == null);
			}

			if (boPhanId > 0)
			{
				yc = yc.Where(c => c.DON_VI_BO_PHAN_ID == boPhanId);
				bd = bd.Where(c => c.DON_VI_BO_PHAN_ID == boPhanId);
			}
			if (donViId > 0)
			{
				yc = yc.Where(c => c.DON_VI_ID == donViId);
				bd = bd.Where(c => c.DON_VI_ID == donViId);
			}
			if (fromdate.HasValue)
			{
				var _tungay = fromdate.Value.Date;
				yc = yc.Where(x => x.NGAY_BIEN_DONG >= _tungay);
				bd = bd.Where(x => x.NGAY_BIEN_DONG >= _tungay);
			}
			if (todate.HasValue && todate != DateTime.MinValue)
			{
				var _denngay = todate.Value.Date.AddDays(1);
				yc = yc.Where(x => x.NGAY_BIEN_DONG < _denngay);
				bd = bd.Where(x => x.NGAY_BIEN_DONG < _denngay);
			}
			if (!string.IsNullOrEmpty(chungTuSo))
			{
				yc = yc.Where(p => p.CHUNG_TU_SO.ToUpper().Contains(chungTuSo.ToUpper()));
				bd = bd.Where(p => p.CHUNG_TU_SO.ToUpper().Contains(chungTuSo.ToUpper()));
			}
			if (!string.IsNullOrEmpty(quyetDinhSo))
			{
				yc = yc.Where(p => p.QUYET_DINH_SO.ToUpper().Contains(quyetDinhSo.ToUpper()));
				bd = bd.Where(p => p.QUYET_DINH_SO.ToUpper().Contains(quyetDinhSo.ToUpper()));
			}
			//không hiển thị tài sản trả lại
			if (isIgnoreTraLai)
				yc = yc.Where(p => p.TAI_SAN_ID > 0 && p.taisan.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.TRA_LAI);
			//chỉ hiển thị biến động có ngày bán thanh lý >= 01/01/2016
			var mung1_thang1_2016 = new DateTime(2016, 1, 1);
			yc = from y in yc
				 join ycct in _yeuCauChiTietRepository.Table
				 on y.ID equals ycct.YEU_CAU_ID
				 where ycct.IS_BAN_THANH_LY == false
				 && y.NGAY_BIEN_DONG >= mung1_thang1_2016
				 select y;
			bd = from b in bd
				 join bdct in _bienDongChiTietRepository.Table
				 on b.ID equals bdct.BIEN_DONG_ID
				 where bdct.IS_BAN_THANH_LY == false
				 && b.NGAY_BIEN_DONG >= mung1_thang1_2016
				 select b;

			var res = yc.Select(p => new TrungGianBDYC(p)).Union(bd.Select(p => new TrungGianBDYC(p)));

			res = res.OrderByDescending(c => c.NGAY_BIEN_DONG).ThenBy(c => c.TAI_SAN_MA);

			return new PagedList<TrungGianBDYC>(res, pageIndex, pageSize);
		}
		public TrungGianBDYC GetYCBDGanNhat(decimal TaiSanId)
		{
			var bienDong = _bienDongRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.IS_BIENDONG_CUOI == true).FirstOrDefault();
			if (bienDong != null)
				return new TrungGianBDYC(bienDong);
			var yeuCau = _yeuCauRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.TAI_SAN_ID == TaiSanId).OrderByDescending(p => p.NGAY_BIEN_DONG).FirstOrDefault();
			return new TrungGianBDYC(yeuCau);
		}
		public List<TrungGianBDYC> GetAllYCBD(decimal TaiSanId)
		{
			var listTG = new List<TrungGianBDYC>();
			var bienDongs = _bienDongRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).ToList();
			if (bienDongs.Count() > 0)
			{
				listTG = listTG.Concat(bienDongs.Select(c => new TrungGianBDYC(c))).ToList();
			}
			var yeuCaus = _yeuCauRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.TAI_SAN_ID == TaiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET).ToList();
			if (yeuCaus.Count() > 0)
			{
				listTG = listTG.Concat(yeuCaus.Select(c => new TrungGianBDYC(c))).ToList();
			}
			return listTG;
		}
		public List<TrungGianBDYC> GetAllYCBDKhacTuChoi(decimal TaiSanId)
		{
			var listTG = new List<TrungGianBDYC>();
			List<BienDong> bienDongs = _bienDongRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).ToList();
			if (bienDongs.Count() > 0)
			{
				listTG = listTG.Concat(bienDongs.Select(c => new TrungGianBDYC(c))).ToList();
			}
			List<YeuCau> yeuCaus = _yeuCauRepository.Table.Where(p =>
			p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP &&
			p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA &&
			p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.TU_CHOI &&
			p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET &&
			p.TAI_SAN_ID == TaiSanId).ToList();
			if (yeuCaus.Count() > 0)
			{
				listTG = listTG.Concat(yeuCaus.Select(c => new TrungGianBDYC(c))).ToList();
			}
			return listTG;
		}
		public TrungGianBDYC GetYCBDorYCById(int ID, int BDorYC)
		{
			if (BDorYC == (int)enumBDorYC.BIEN_DONG)
			{
				var bienDong = _bienDongRepository.GetById((decimal)ID);
				if (bienDong != null)
					return new TrungGianBDYC(bienDong);
				else
					return null;
			}
			if (BDorYC == (int)enumBDorYC.YEU_CAU)
			{
				var yeuCau = _yeuCauRepository.GetById((decimal)ID);
				if (yeuCau != null)
					return new TrungGianBDYC(yeuCau);
				else
					return null;
			}
			else return null;
		}
		/// <summary>
		/// Trả về HTSD_Json của tài sản
		/// Lấy theo
		///     - Biến động gần nhất
		///     - Nếu không có biến động thì lấy yêu cầu gần nhất
		/// </summary>
		/// <param name="taiSanId"></param>
		/// <returns></returns>
		public virtual string GetHTSD_JSON_of_TS(decimal taiSanId)
		{
			if (taiSanId > 0)
			{
				var BDYC = GetYCBDGanNhat(taiSanId);
				if (BDYC != null)
				{
					switch (BDYC.EnumBDorYC)
					{
						case enumBDorYC.BIEN_DONG:

							var _bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(BDYC.ID);
							if (_bdct != null)
								return _bdct.HTSD_JSON;
							break;
						case enumBDorYC.YEU_CAU:
							var _ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(BDYC.ID);
							if (_ycct != null)
								return _ycct.HTSD_JSON;
							break;
					}
				}
			}
			return "";
		}

		public virtual decimal SumHTSDDienTich(decimal taiSanId)
		{
			decimal res = 0;
			if (taiSanId > 0)
			{
				var BDYC = GetYCBDGanNhat(taiSanId);
				if (BDYC != null)
				{
					string HTSD = "";
					switch (BDYC.EnumBDorYC)
					{
						case enumBDorYC.BIEN_DONG:

							var _bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(BDYC.ID);
							if (_bdct != null)
								HTSD = _bdct.HTSD_JSON;
							break;
						case enumBDorYC.YEU_CAU:
							var _ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(BDYC.ID);
							if (_ycct != null)
								HTSD = _ycct.HTSD_JSON;
							break;
					}
					if (!string.IsNullOrEmpty(HTSD))
					{
						var htList = HTSD.toEntity<HienTrangList_Entity>();
						if (htList.lstObjHienTrang != null && htList.lstObjHienTrang.Count > 0)
							res = htList.lstObjHienTrang.Sum(p => p.GiaTriNumber ?? 0);
					}
				}
			}
			return res;
		}
		public virtual int CountBDYCTaiSan(decimal taiSanId)
		{
			var countYC = _yeuCauRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP_LIEU).Count();
			var countBD = _bienDongRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).Count();
			return countYC + countBD;
		}
		public virtual decimal GetGTCLGanNhatDaDuyet(decimal taiSanId)
		{
			var taiSan = _taiSanRepository.GetById(taiSanId);
			if (taiSan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
			{
				return _bienDongService.TinhNguyenGiaTaiSan(null, null, taiSanId);
			}
			else
			{
				var bd = _bienDongService.GetBienDongCuoiByTaiSanId(taiSanId);
				if (bd != null)
				{

					var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);
					if (bdct != null)
					{
						if (bdct.HM_GIA_TRI_CON_LAI > 0)
							return bdct.HM_GIA_TRI_CON_LAI ?? 0 ;
						else return bdct.KH_CON_LAI ?? 0;
					}
					


				}
				else
				{
					var yc = _yeuCauService.GetYeuCauCuNhatByTSId(taiSanId);
					if (yc != null)
					{
						var ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yc.ID);
						if (ycct != null)
						{
							if (ycct.HM_GIA_TRI_CON_LAI > 0)
								return ycct.HM_GIA_TRI_CON_LAI ?? 0;
							else return ycct.KH_CON_LAI ?? 0;
						}
						
					}
				}
			}

			return 0;
		}
		#endregion Methods
	}
}