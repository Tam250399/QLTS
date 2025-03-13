//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
	public class TaiSanOtoModelFactory : ITaiSanOtoModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ITaiSanOtoService _itemService;
		private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly IHienTrangService _hienTrangService;
		private readonly INhanXeService _nhanXeService;
		private readonly INhanXeModelFactory _nhanXeModelFactory;
		private readonly IChucDanhModelFactory _chucDanhModelFactory;
		private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
		private readonly ITaiSanService _taiSanService;
		private readonly IYeuCauService _yeuCauService;
		private readonly IYeuCauChiTietService _yeuCauChiTietService;
		private readonly IDonViService _donViService;
		private readonly ITrungGianBDYCService _trungGianBDYCService;
		private readonly IDongXeModelFactory _dongXeModelFactory;
		private readonly IDongXeService _dongXeService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        #endregion

        #region Ctor

        public TaiSanOtoModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ITaiSanOtoService itemService,
			ILoaiTaiSanService loaiTaiSanService,
			IHienTrangService hienTrangService,
			INhanXeService nhanXeService,
			INhanXeModelFactory nhanXeModelFactory,
			IChucDanhModelFactory chucDanhModelFactory,
			IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
			ITaiSanService taiSanService,
			IYeuCauService yeuCauService,
			IYeuCauChiTietService yeuCauChiTietService,
			IDonViService donViService,
			ITrungGianBDYCService trungGianBDYCService,
			IDongXeModelFactory dongXeModelFactory,
			IDongXeService dongXeService,
			IHienTrangModelFactory hienTrangModelFactory
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._loaitaisanService = loaiTaiSanService;
			this._hienTrangService = hienTrangService;
			this._nhanXeService = nhanXeService;
			this._nhanXeModelFactory = nhanXeModelFactory;
			this._chucDanhModelFactory = chucDanhModelFactory;
			this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
			this._taiSanService = taiSanService;
			this._yeuCauService = yeuCauService;
			this._yeuCauChiTietService = yeuCauChiTietService;
			this._donViService = donViService;
			this._trungGianBDYCService = trungGianBDYCService;
			this._dongXeModelFactory = dongXeModelFactory;
			this._dongXeService = dongXeService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
		#endregion

		#region TaiSanOto
		public string genTenTaiSanOto(decimal? NhanXeId, decimal? dongXeId, string BKS)
		{
			var nhanXe = _nhanXeService.GetNhanXeById(NhanXeId.GetValueOrDefault());
			var soLoai = _dongXeService.GetDongXeById(dongXeId.GetValueOrDefault());
			if (nhanXe == null && soLoai == null)
			{
				return "Loại khác-" + BKS;
			}
			else
			{
				string maXe = BKS.Replace("_", string.Empty);
				if (soLoai != null)
				{
					maXe = soLoai.TEN + "-" + maXe;
				}
				if (nhanXe != null)
				{
					maXe = nhanXe.TEN + "-" + maXe;
				}
				return maXe;

			}
		}
		public TaiSanOtoSearchModel PrepareTaiSanOtoSearchModel(TaiSanOtoSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public TaiSanOtoListModel PrepareTaiSanOtoListModel(TaiSanOtoSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchTaiSanOtos(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new TaiSanOtoListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<TaiSanOtoModel>()),
				Total = items.TotalCount
			};
			return model;
		}
		public TaiSanOtoModel PrepareTaiSanOtoModel(TaiSanOtoModel model, TaiSanOto item, bool excludeProperties = false, bool isTangMoi = true)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<TaiSanOtoModel>();
				if (isTangMoi)// lấy ra biến động hoặc yêu cầu đầu tiên
				{
					var yeucauht = _yeuCauService.GetYeuCauCuNhatByTSId(model.TAI_SAN_ID);
					var ycctht = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucauht.ID);

					model.lstHienTrang = ycctht.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;

					// Nếu nhập sai hiện trạng thì mở tất ra cho sửa
					if (_hienTrangModelFactory.IsAnyHienTrangSai(model.lstHienTrang, _workContext.CurrentDonVi.ID))
					{
						model.lstHienTrang = model.lstHienTrang.Select(c =>
						{
							c.IsOpenAll = true;
							return c;
						}).ToList();
					}

					model.NVYeuCauChiTietModel = ycctht.ToModel<YeuCauChiTietModel>();
				}
				else //lấy ra biến động hoặc yêu cầu gần nhất
				{
					model.lstHienTrang = _trungGianBDYCService.GetHTSD_JSON_of_TS(model.TAI_SAN_ID).toEntity<HienTrangList>().lstObjHienTrang;
				}
			}

			//more
			// prepare loai tai san
			if (item.TAI_SAN_ID > 0)
			{
				var taisan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
				model.TaiSanModel = taisan.ToModel<TaiSanModel>();
			}
			if (model.LOAI_TAI_SAN_ID > 0)
			{
				var loaiTaiSan = new LoaiTaiSan();
				loaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID.GetValueOrDefault());
				model.LoaiTaiSanModel = loaiTaiSan.ToModel<LoaiTaiSanModel>();
				model.NVYeuCauChiTietModel = _yeuCauChiTietModelFactory.PrepareValueFromLoaiTStoYeuCauCT(loaiTaiSan: model.LoaiTaiSanModel, nvYeuCauChiTiet: model.NVYeuCauChiTietModel);
			}
			//hien trang
			var hienTrangList = _hienTrangService.ListHienTrangsByFields(loaiHinhTSId: item.taisan.LOAI_HINH_TAI_SAN_ID);
			model.ListHienTrangModel = hienTrangList.Select(c => c.ToModel<HienTrangModel>()).ToList();
			model.dllNhanXe = _nhanXeModelFactory.PrepareSelectListNhanXe(valSelected: model.NHAN_XE_ID, isAddFirst: true);

			#region Xử lý chọn nhiều chức danh khác --HưngNT

			model.SelectedChucDanhIds = new List<int>();
			// Chuẩn bị list chức danh đã chọn
			if (item.LIST_CHUC_DANH_ID != null)
			{
				model.SelectedChucDanhIds = item.LIST_CHUC_DANH_ID.ToListInt();
			}
			
			// chuẩn bị dropdown list
			model.dllChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(isAddFirst:true,valSelected:model.CHUC_DANH_ID,IsPhanCap: false);

            #endregion

            var yeucau = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID);
			//var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
			//model.NVYeuCauChiTietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
			if (model.NVYeuCauChiTietModel == null)
				model.NVYeuCauChiTietModel = new YeuCauChiTietModel();
			model.NVYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.NVYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
			var donvi = _workContext.CurrentDonVi;
			var donvikh = _donViService.GetDonViById(donvi.ID);
			//if (model.DONG_XE_ID > 0)
			//{
			model.ddlDongXe = _dongXeModelFactory.PrepareSelectDongXe(valSelected: model.DONG_XE_ID, nhanXeId: model.NHAN_XE_ID, isAddFirst: true);
			//}
			//else
			//{
			//	model.ddlDongXe.AddFirstRow(TextDisplay: "-- Chọn dòng xe --", ValueFirst: "");
			//}
			if (model.lstHienTrang != null)
			{
				foreach (var itemHT in model.lstHienTrang)
				{
					itemHT.TenHienTrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId ?? 0).TEN_HIEN_TRANG;
				}
			}
			if (item.BIEN_KIEM_SOAT != null && item.BIEN_KIEM_SOAT.Contains("-"))
			{
				var arrBKS = item.BIEN_KIEM_SOAT.Split("-", StringSplitOptions.None);
				model.Pre_BIEN_KIEM_SOAT = arrBKS[0];
				model.Suff_BIEN_KIEM_SOAT = arrBKS[1];
			}
			return model;
		}

		public void PrepareTaiSanOto(TaiSanOtoModel model, TaiSanOto item)
		{
			//item.TAI_SAN_ID = model.TAI_SAN_ID;
			if (model.BIEN_KIEM_SOAT != null)
				item.BIEN_KIEM_SOAT = model.BIEN_KIEM_SOAT.Replace('_', ' ').ToUpper();
			else
				item.BIEN_KIEM_SOAT = "";
			item.SO_CHO_NGOI = model.SO_CHO_NGOI;
			item.DUNG_TICH = model.DUNG_TICH;
			item.CHUC_DANH_ID = model.CHUC_DANH_ID;
			item.TAI_TRONG = model.TAI_TRONG;
			item.SO_KHUNG = model.SO_KHUNG;
			item.NHAN_XE_ID = model.NHAN_XE_ID;
			item.CONG_XUAT = model.CONG_XUAT;
			item.SO_MAY = model.SO_MAY;
			item.SO_CAU_XE = model.SO_CAU_XE;
			item.GCN_DANG_KY = model.GCN_DANG_KY;
			item.CO_QUAN_CAP_DANG_KY = model.CO_QUAN_CAP_DANG_KY;
			item.NGAY_DANG_KY = model.NGAY_DANG_KY;
			item.DONG_XE_ID = model.DONG_XE_ID;
			if (model.SelectedChucDanhIds.Count > 0)
			{
				if (!model.SelectedChucDanhIds.Contains(0))  
				{
					model.LIST_CHUC_DANH_ID = "";
					foreach (var lcd in model.SelectedChucDanhIds)
					{
						model.LIST_CHUC_DANH_ID += lcd + ",";
					}
					model.LIST_CHUC_DANH_ID = model.LIST_CHUC_DANH_ID.Remove(model.LIST_CHUC_DANH_ID.Length - 1);
				}
				else // nếu có 0 thì chỉ lưu 0 (tất cả)
				{
					model.LIST_CHUC_DANH_ID = "0";
				}

            }
            else
            {
				model.LIST_CHUC_DANH_ID = null;

			}
			item.LIST_CHUC_DANH_ID = model.LIST_CHUC_DANH_ID;
			
		}

		/// <summary>
		/// Desription: Check biển kiểm soát. <br></br>
		/// Hợp lệ: return true<br></br>
		/// Không hợp lệ: return false <br></br>
		/// </summary>
		/// <param name="bienKiemSoat">BKS cần check</param>
		/// <returns></returns>
		public bool checkBienKiemSoat(string bienKiemSoat, decimal? id = 0, bool isPhuongTienKhac = false)
		{

			if (String.IsNullOrEmpty(bienKiemSoat) || bienKiemSoat.Trim() == "-")
			{
				if (isPhuongTienKhac)
					return true;
				else
					return false;
			}
			else
			{
				var oto = _itemService.GetTaiSanOtoByBKS(bienKiemSoat);

				if (oto == null || oto.TAI_SAN_ID == id)
					return true;
				else
					return false;
			}


		}

		/// <summary>
		/// Kiểm tra số chỗ ngồi
		/// </summary>
		/// <param name="loaiTaiSanId"></param>
		/// <param name="soChoNgoi"></param>
		/// <returns></returns>
		public bool checkSoChoNgoi(ref string messageReturn, decimal loaiTaiSanId = 0, decimal soChoNgoi = 0, decimal taiTrong = 0)
		{
			var loaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(loaiTaiSanId);
			if (loaiTaiSan != null)
			{
				//theo tải trọng
				if (loaiTaiSan.OTO_LOAI_XE_ID == (int)enumLoaiXe.XeTai)
				{
					messageReturn = "Bạn nhập tải trọng chưa đúng với loại tài sản đã chọn.";
					if (loaiTaiSan.OTO_CHO_NGOI_TU != null && taiTrong < loaiTaiSan.OTO_CHO_NGOI_TU)
						return false;
					if (loaiTaiSan.OTO_CHO_NGOI_DEN != null && taiTrong > loaiTaiSan.OTO_CHO_NGOI_DEN)
						return false;
				}
				//theo số chỗ ngồi
				else
				{
					messageReturn = "Bạn nhập số chỗ ngồi chưa đúng với loại tài sản đã chọn.";
					if (loaiTaiSan.OTO_CHO_NGOI_TU != null && soChoNgoi < loaiTaiSan.OTO_CHO_NGOI_TU)
						return false;
					if (loaiTaiSan.OTO_CHO_NGOI_DEN != null && soChoNgoi > loaiTaiSan.OTO_CHO_NGOI_DEN)
						return false;
				}

			}
			return true;
		}
		#endregion
	}
}

