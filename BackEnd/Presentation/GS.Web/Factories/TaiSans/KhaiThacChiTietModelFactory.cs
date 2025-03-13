using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.TaiSans
{
	public class KhaiThacChiTietModelFactory : IKhaiThacChiTietModelFactory
	{
		#region Fields    		
		private readonly IWorkContext _workContext;
		private readonly IKhaiThacChiTietServices _itemService;
		private readonly IKhaiThacService _khaiThacService;
		private readonly IKhaiThacModelFactory _khaiThacModelFactory;
		private readonly IBienDongService _bienDongService;
		private readonly ITaiSanService _taiSanService;
		private readonly IDoiTacService _doiTacService;
		private readonly INhanHienThiService _NhanHienThiService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IDoiTacModelFactory _doiTacModelFactory;
        private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;

        #endregion

        #region Ctor

        public KhaiThacChiTietModelFactory(
			IWorkContext workContext,
			IKhaiThacChiTietServices itemService,
			IKhaiThacService khaiThacService,
			IKhaiThacModelFactory khaiThacModelFactory,
			IBienDongService bienDongService,
			ITaiSanService taiSanService,
			IDoiTacService doiTacService,
			INhanHienThiService nhanHienThiService,
			ILoaiTaiSanService loaiTaiSanService,
			IDoiTacModelFactory doiTacModelFactory,
			IKhaiThacTaiSanService khaiThacTaiSanService
			)
		{
			this._workContext = workContext;
			this._itemService = itemService;
			_khaiThacService = khaiThacService;
			_khaiThacModelFactory = khaiThacModelFactory;
			_bienDongService = bienDongService;
			_taiSanService = taiSanService;
			_doiTacService = doiTacService;
			_NhanHienThiService = nhanHienThiService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._doiTacModelFactory = doiTacModelFactory;
            this._khaiThacTaiSanService = khaiThacTaiSanService;
        }
		#endregion

		#region KhaiThacChiTiet
		public KhaiThacChiTietSearchModel PrepareKhaiThacChiTietSearchModel(KhaiThacChiTietSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			searchModel.SetGridPageSize();
			return searchModel;
		}

		public KhaiThacChiTietListModel PrepareKhaiThacChiTietListModel(KhaiThacChiTietSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			var donvi = _workContext.CurrentDonVi.ID;
			//get items
			var items = _itemService.SearchKhaiThacChiTiets(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, khaiThacId: searchModel.KHAI_THAC_ID);

			//prepare list model
			var model = new KhaiThacChiTietListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<KhaiThacChiTietModel>();
					m.strVNSoTienThuDuoc = c.SO_TIEN_THU_DUOC.ToVNStringNumber();
					var DienTichTS = _bienDongService.Tinh_GiaTriTaiSan(m.TAI_SAN_ID ?? 0);
					var taiSan = _taiSanService.GetTaiSanById(m.TAI_SAN_ID ?? 0);
					m.TAI_SAN_MA = taiSan.MA;
					m.TAI_SAN_TEN = taiSan.TEN;
					m.LoaiTaiSanTen = taiSan.loaitaisan.TEN;
					m.DIEN_TICH = DienTichTS.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
					m.StrVNDienTich = m.DIEN_TICH.ToVNStringNumber();
					m.StrVNDienTichKhaiThac = m.DIEN_TICH_KHAI_THAC.ToVNStringNumber();
					m.NgayKhaiThacString = $"{c.NGAY_KHAI_THAC.toDateVNString()} - {c.NGAY_KHAI_THAC_DEN.toDateVNString()}";
					if (c.DOI_TAC_ID > 0) { 
						m.DoiTacTen = _doiTacService.GetDoiTacById(c.DOI_TAC_ID ?? 0).TEN;
					}
					m.phuongthucchothueten = _NhanHienThiService.GetGiaTriEnum(m.enumPhuongThucChoThue);
					m.HinhThucLDLKTen = _NhanHienThiService.GetGiaTriEnum(m.enumHinhThucLDLK);
					return m;
				}).ToList(),
				Total = items.TotalCount

			};
			return model;
		}
		public KhaiThacChiTietListModel PrepareCapNhatSoTienKhaiThacChiTietListModel(KhaiThacSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			var donvi = _workContext.CurrentDonVi.ID;
			//get items
			 var items = _itemService.SearchKhaiThacChiTietsForCapNhatSoTien(donviId: donvi, Keysearch: searchModel.KeySearch, QUYET_DINH_SO: searchModel.QUYET_DINH_SO, LOAI_HINH_KHAI_THAC_ID: searchModel.LOAI_HINH_KHAI_THAC_ID, QUYET_DINH_NGAY: searchModel.QUYET_DINH_NGAY, HOP_DONG_SO: searchModel.HOP_DONG_SO, HOP_DONG_NGAY: searchModel.HOP_DONG_NGAY, KHAI_THAC_NGAY_TU: searchModel.KHAI_THAC_NGAY_TU, KHAI_THAC_NGAY_DEN: searchModel.KHAI_THAC_NGAY_DEN, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new KhaiThacChiTietListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<KhaiThacChiTietModel>();
					m.strVNSoTienThuDuoc = c.SO_TIEN_THU_DUOC.ToVNStringNumber();
					var DienTichTS = _bienDongService.Tinh_GiaTriTaiSan(m.TAI_SAN_ID ?? 0);
					var taiSan = _taiSanService.GetTaiSanById(m.TAI_SAN_ID ?? 0);
					m.TAI_SAN_MA = taiSan.MA;
					m.TAI_SAN_TEN = taiSan.TEN;
					var kt = _khaiThacService.GetKhaiThacById(m.KHAI_THAC_ID);
					m.LOAI_HINH_KHAI_THAC_ID = kt.LOAI_HINH_KHAI_THAC_ID;
					m.LoaiTaiSanTen = taiSan.loaitaisan.TEN;
					m.DIEN_TICH = DienTichTS.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
					m.StrVNDienTich = m.DIEN_TICH.ToVNStringNumber();
					m.StrVNDienTichKhaiThac = m.DIEN_TICH_KHAI_THAC.ToVNStringNumber();
					m.NgayKhaiThacString = $"{c.NGAY_KHAI_THAC.toDateVNString()} - {c.NGAY_KHAI_THAC_DEN.toDateVNString()}";
					if (c.DOI_TAC_ID > 0)
					{
						m.DoiTacTen = _doiTacService.GetDoiTacById(c.DOI_TAC_ID ?? 0).TEN;
					}
					m.phuongthucchothueten = _NhanHienThiService.GetGiaTriEnum(m.enumPhuongThucChoThue);
					m.HinhThucLDLKTen = _NhanHienThiService.GetGiaTriEnum(m.enumHinhThucLDLK);
					m.HinhThucKhaiThac = _NhanHienThiService.GetGiaTriEnum(m.enumHinhThucKhaiThac);
					return m;
				}).ToList(),
				Total = items.TotalCount

			};
			return model;
		}
		public KhaiThacChiTietModel PrepareKhaiThacChiTietModel(KhaiThacChiTietModel model, KhaiThacChiTiet item, bool excludeProperties = false)
		{

			if (item != null)
			{
				model = item.ToModel<KhaiThacChiTietModel>();
				
			}
            if (model.DOI_TAC_ID > 0)
            {
				model.DoiTacTen = _doiTacService.GetDoiTacById(model.DOI_TAC_ID ?? 0)?.TEN;
            }
			if (model.KHAI_THAC_ID > 0)
			{
				var khaiThac = _khaiThacService.GetKhaiThacById(model.KHAI_THAC_ID);
				model.LOAI_HINH_KHAI_THAC_ID = khaiThac?.LOAI_HINH_KHAI_THAC_ID;
				model.KhaiThac = khaiThac ?? new KhaiThac();
				if (model.TAI_SAN_ID > 0)
				{
					var taisan = _taiSanService.GetTaiSanById(model.TAI_SAN_ID ?? 0);				
					if (taisan != null)
					{
						model.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
						model.TAI_SAN_MA = taisan.MA;
						model.TAI_SAN_TEN = taisan.TEN;
						model.NGAY_SU_DUNG = taisan.NGAY_SU_DUNG;
						model.TAI_SAN_ID = taisan.ID;
						model.LoaiTaiSanTen = _loaiTaiSanService.GetLoaiTaiSanById(taisan.LOAI_TAI_SAN_ID ?? 0).TEN;
						if (taisan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || taisan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
						{
							var DienTichTS = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID ?? 0);
							model.DIEN_TICH = DienTichTS.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
						}
						
					}
					model.DON_VI_ID = _workContext.CurrentDonVi.ID;
					var firstRow = "";
					if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
					{
						firstRow = "---Chọn đơn vị thuê--- ";

					}
					else
					{
						firstRow = "---Chọn đối tác LDLK--- ";

					}
					model.DoiTacAvailable = _doiTacModelFactory.PrepareSelectListDoiTac(0, model.DOI_TAC_ID, true, firstRow);
					model.PhuongThucChoThueAvailable = enumPhuongThucChoThue.DAU_GIA.ToSelectList(valuesToExclude: new int[] { (int)enumPhuongThucChoThue.all })
																	.Select(c => new SelectListItem() { Text = c.Text, Value = c.Value, Selected = true }).ToList();
					model.PhuongThucChoThueAvailable.Insert(0, new SelectListItem() { Text = "---Chọn phương thức cho thuê---", Value = "0", Selected = true });
					model.HinhThucLDLKAvaliable = enumHinhThucLDLK.GOP_VON.ToSelectList()
																	.Select(c => new SelectListItem() { Text = c.Text, Value = c.Value }).ToList();
					model.HinhThucLDLKAvaliable.Insert(0, new SelectListItem() { Text = "---Chọn hình thức LDLK---", Value = "0", Selected = true });
					model.SO_TIEN_KHAI_THAC_LUY_KE = _khaiThacTaiSanService.GetKhaiThacTaiByKhaiThacChiTietID(model.ID)?.Sum(c => c.SO_TIEN);

				}
			}
			return model;
		}
		public void PrepareKhaiThacChiTiet(KhaiThacChiTietModel model, KhaiThacChiTiet item)
		{
			item.KHAI_THAC_ID = model.KHAI_THAC_ID;
			item.NGAY_KHAI_THAC = model.NGAY_KHAI_THAC;
			item.SO_TIEN_THU_DUOC = model.SO_TIEN_THU_DUOC;
			item.GHI_CHU = model.GHI_CHU;
			item.HOP_DONG_SO = model.HOP_DONG_SO;
			item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
			item.DOI_TAC_ID = model.DOI_TAC_ID;
			item.NGAY_KHAI_THAC_DEN = model.NGAY_KHAI_THAC_DEN;
			item.CHO_THUE_GIA = model.CHO_THUE_GIA;
			item.DIEN_TICH_KHAI_THAC = model.DIEN_TICH_KHAI_THAC;
			item.CHO_THUE_PHUONG_THUC_ID = model.CHO_THUE_PHUONG_THUC_ID;
			item.LDLK_HINH_THUC_ID = model.LDLK_HINH_THUC_ID;
			item.TEN_PHAP_NHAN_MOI = model.TEN_PHAP_NHAN_MOI;
		}
		#endregion
		public bool ValidateNgayKhaiThac(KhaiThacChiTietModel model)
		{
			var khaiThac = _khaiThacService.GetKhaiThacById(model.KHAI_THAC_ID);
			if (khaiThac != null)
				if (model.NGAY_KHAI_THAC < khaiThac.QUYET_DINH_NGAY)
					return false;
			return true;
		}
	}

}