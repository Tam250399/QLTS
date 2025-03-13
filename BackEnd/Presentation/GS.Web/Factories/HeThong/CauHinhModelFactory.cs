//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 27/9/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
	public class CauHinhModelFactory : ICauHinhModelFactory
	{
		#region Fields

		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ICauHinhService _itemService;
		private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;

		#endregion Fields

		#region Ctor

		public CauHinhModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ICauHinhService itemService,
			INhanHienThiService nhanHienThiService,
            IDonViService donViService

            )
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			_nhanHienThiService = nhanHienThiService;
            this._donViService = donViService;

        }

		#endregion Ctor

		#region CauHinh

		public CauHinhSearchModel PrepareCauHinhSearchModel(CauHinhSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public CauHinhListModel PrepareCauHinhListModel(CauHinhSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchCauHinhs(Ten: searchModel.TEN, GiaTri: searchModel.GIA_TRI, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new CauHinhListModel
			{
				//fill in model values from the entity
				Data = items.Select(c => c.ToModel<CauHinhModel>()),
				Total = items.TotalCount
			};
			return model;
		}

		public CauHinhModel PrepareCauHinhModel(CauHinhModel model, CauHinh item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<CauHinhModel>();
			}
			//more

			return model;
		}

		public CauHinhChungModel PrepareCauHinhChungModel()
		{
			var cauhinhchung = _itemService.LoadCauHinh<CauHinhChung>();

			//fill in model values from the entity
			var model = cauhinhchung.ToCauHinhModel<CauHinhChungModel>();
			return model;
		}

		public XacThucChungThuSomodel PrepareXacThucChungThuSomodel()
		{
			var cauhinhchung = _itemService.LoadCauHinh<XacThucChungThuSo>();

			//fill in model values from the entity
			var model = cauhinhchung.ToCauHinhModel<XacThucChungThuSomodel>();
			return model;
		}

		public ThongTinKetXuatDuLieuModel PrepareLichKetXuatDuLieuModel(ThongTinKetXuatDuLieuModel model)
		{
			var cauhinhKetXuatDuLieu = _itemService.LoadCauHinh<KetXuatDuLieu>();

			//fill in model values from the entity
			model = cauhinhKetXuatDuLieu.LichKetXuat.toEntity<ThongTinKetXuatDuLieuModel>();
			return model;
		}
		#region Cấu hình sổ tài sản

		public CauHinhSoTaiSanModel PrepareListCauHinhSoTaiSan(CauHinhSoTaiSanModel model, decimal DonViId = 0)
		{
			var cauHinhSoTaiSan = _itemService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(DonViId);
			var items = cauHinhSoTaiSan.KhoaSoHeThong.toEntities<TrangThaiNam>();
			model.list_TrangThaiNam = items.Select(p =>
			{
				var m = p.ToCauHinhModel<TrangThaiNamModel>();
				m.TenTrangThai = _nhanHienThiService.GetGiaTriEnum<enumTrangThaiNamTaiSan>((enumTrangThaiNamTaiSan)m.TrangThai);
				m.Nam = Convert.ToInt32(m.Nam);
				return m;
			}).ToList();
			return model;
		}
		public TrangThaiNamModel PrepareTrangThaiNamTaiSanModel(TrangThaiNamModel model, decimal Year, decimal DonViId = 0)
		{
			//prepare lần đầu
			if (model== null)
			{
				model = new TrangThaiNamModel();
				var cauHinhSoTaiSan = _itemService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(DonViId);
				var item = cauHinhSoTaiSan.KhoaSoHeThong.toEntities<TrangThaiNam>().FirstOrDefault(x => x.Nam == Year);
				if (item != null)
				{
					model = item.ToCauHinhModel<TrangThaiNamModel>();
				}
				else
				{
					model=  new TrangThaiNamModel
					{
						Nam = Year,
						TrangThai = (int)enumTrangThaiNamTaiSan.OPEN,
						NgayKhoaSo = new DateTime(Convert.ToInt32(Year) + 1, 1, 1),
						TenTrangThai = _nhanHienThiService.GetGiaTriEnum<enumTrangThaiNamTaiSan>(enumTrangThaiNamTaiSan.OPEN)
					};
				}
			}
			model.Nam = Convert.ToInt32(model.Nam);
			model.TenTrangThai = _nhanHienThiService.GetGiaTriEnum<enumTrangThaiNamTaiSan>((enumTrangThaiNamTaiSan)model.TrangThai);
			model.ddlTrangThai = (new enumTrangThaiNamTaiSan()).ToSelectList().ToList();
			return model;
		}
		public TrangThaiNam PrepareTrangThaiNamTaiSan(TrangThaiNamModel model,TrangThaiNam item)
		{
			item.NgayKhoaSo = model.NgayKhoaSo;
			item.TrangThai = model.TrangThai;
			return item;
		}
		#endregion
		public CauHinhTuDongDuyetModel PrepareCauHinhTuDongDuyetModel(CauHinhTuDongDuyetModel model)
		{
			var donViId = _workContext.CurrentDonVi.ID;
			var donViCauHinh = _itemService.LoadCauHinhDonViBo<CauHinhTuDongDuyet>(donViId);
			model = donViCauHinh.ToCauHinhModel<CauHinhTuDongDuyetModel>();
			model.cauHinhDuyetTaiSans = donViCauHinh.IsAutoDuyetTaiSanDuoi500.toEntities<CauHinhDuyetTaiSan>();

            var donvi = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);

            if (donvi != null)
            {
                if (donvi.ID == _workContext.CurrentDonVi.ID || _workContext.CurrentDonVi.ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
					model.IsShowCheckTSQLNhuTSCD = true;
					model.IsShowCheckTSQLChuKySo = true;

				}
                model.tsQuanLyNhuTSCD = donvi.IS_TSQUAN_LY_NHU_TSCD.GetValueOrDefault();
				model.tsQuanLyChuKySo = donvi.IS_TSQUAN_LY_CHU_KY_SO.GetValueOrDefault();
			}

            return model;
		}

		public ThongTinKetXuatDuLieuModel PrepareLichKetXuatDuLieu(ThongTinKetXuatDuLieuModel model)
		{
			switch (model.LoaiKetXuat)
			{
				case 1:
					model.Note = "HangThang";
					break;

				case 2:
					model.Note = "HangTuan";
					break;

				case 3:
					model.Note = "HangNgay";
					model.ValueNgay = 0;
					break;
			}
			return model;
		}

		public DinhMucChucDanhSearchModel PrepareDinhMucChucDanhSearchModel(DinhMucChucDanhSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public DinhMucChucDanhListModel PrepareDinhMucChucDanhListModel(DinhMucChucDanhSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			//var dinhmucchucdanh = _itemService.SearchDinhMucChucDanh(KeySearch: searchModel.KeySearch);
			//var dinhmucchucdanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>();
			//model = cauHinhSoTaiSan.KhoaSoHeThong.toEntities<TrangThaiNamModel>();
			//var items = _itemService.SearchCauHinhs(Ten: searchModel.TEN, GiaTri: searchModel.GIA_TRI, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			////prepare list model
			//var model = new CauHinhListModel
			//{
			//    //fill in model values from the entity
			//    Data = items.Select(c => c.ToModel<CauHinhModel>()),
			//    Total = items.TotalCount
			//};
			//return model;
			return new DinhMucChucDanhListModel();
		}

		public DinhMucChucDanhModel PrepareDinhMucChucDanhModel(DinhMucChucDanhModel model)
		{
			var cauhinhDinhMucChucDanh = _itemService.LoadCauHinh<CauHinhDinhMucChucDanh>(_workContext.CurrentDonVi.ID);

			//fill in model values from the entity
			model = cauhinhDinhMucChucDanh.dmcd.toEntity<DinhMucChucDanhModel>();
			return model;
		}

		public void PrepareCauHinh(CauHinhModel model, CauHinh item)
		{
			item.TEN = model.TEN;
			item.GIA_TRI = model.GIA_TRI;
			item.DON_VI_ID = model.DON_VI_ID;
		}

		public CauHinhThreadBaoCaoModel PrepareCauHinhThreadBaoCaoModel()
		{
			var cauHinhThreadBaoCao = _itemService.LoadCauHinh<CauHinhThreadBaoCao>();

			//fill in model values from the entity
			var model = cauHinhThreadBaoCao.ToCauHinhModel<CauHinhThreadBaoCaoModel>();
			return model;
		}

		#endregion CauHinh

		#region Method
		public virtual TrangThaiNam GetSoTaiSanByYear(decimal Year)
		{
			var donViId = _workContext.CurrentDonVi.ID;
			var cauHinhSoTaiSan = _itemService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(donViId);
			//prepare model
			
			var items = cauHinhSoTaiSan.KhoaSoHeThong.toEntities<TrangThaiNam>();
			return items.FirstOrDefault(p => p.Nam == Year);
		}
		/// <summary>
		/// Kiểm tra đã khóa sổ tài sản hay chưa
		/// </summary>
		/// <param name="Year">Năm truyền vào</param>
		/// <returns>true: đã khóa, false: chưa khóa</returns>
		public bool CheckYearIsLockSoTaiSan(decimal Year)
		{
			var ByYear = GetSoTaiSanByYear(Year);
			// trạng thái sổ là đóng và ngày hiện tại lớn hơn ngày khóa sổ
			// nếu sổ khóa nhưng chưa đến ngày khóa thì vẫn được sửa đổi
			if (ByYear != null && ByYear.TrangThai == (int)enumTrangThaiNamTaiSan.CLOSE && DateTime.Now> ByYear.NgayKhoaSo)
				return true;
			return false;
		}
		/// <summary>
		/// match các trường của 2 model
		/// </summary>
		/// <param name="model"></param>
		/// <param name="session"></param>
		public void PrePareModelBySession(object model,object session)
		{
			var ppt = session.GetType().GetProperties();
			foreach (var t in ppt)
			{
				var type = Nullable.GetUnderlyingType(t.PropertyType) ?? t.PropertyType;
				// loại bỏ những filed là DDL 
				if (type.GetProperty("Item") == null && type.Name != "SelectList")
				{
					// lấy field của model theo name field của session
					var typeM = model.GetType().GetProperty(t.Name);
					// set value theo field của session
					if (typeM != null)
					{
						typeM.SetValue(model, t.GetValue(session, null), null);
					}
				}
			}
		}
		#endregion Method
	}
}