//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.TaiSans;
using GS.Services.DanhMuc;
using GS.Web.Models.BienDongs;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
	public class MuaSamModelFactory : IMuaSamModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IMuaSamService _itemService;
		private readonly IDonViService _donViService;
		private readonly IMuaSamChiTietService _muaSamChiTietService;
		private readonly IMuaSamChiTietModelFactory _muaSamChiTietModelFactory;
		private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
		private readonly IHoatDongService _hoatDongService;
		private readonly INguoiDungService _nguoiDungService;
		private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        #endregion

        #region Ctor

        public MuaSamModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IMuaSamService itemService,
			IDonViService donViService,
			IMuaSamChiTietService muaSamChiTietService,
			IMuaSamChiTietModelFactory muaSamChiTietModelFactory,
			ILoaiTaiSanService loaitaisanService,
			IHoatDongService hoatDongService,
			IHinhThucMuaSamService hinhThucMuaSamService,
			INguoiDungService nguoiDungService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			ICheDoHaoMonService cheDoHaoMonService,
			ILoaiTaiSanDonViServices loaiTaiSanDonViServices
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._donViService = donViService;
			this._muaSamChiTietService = muaSamChiTietService;
			this._muaSamChiTietModelFactory = muaSamChiTietModelFactory;
			this._loaitaisanService = loaitaisanService;
			this._hinhThucMuaSamService = hinhThucMuaSamService;
			this._hoatDongService = hoatDongService;
			this._nguoiDungService = nguoiDungService;
			this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			this._cheDoHaoMonService = cheDoHaoMonService;
            this._loaiTaiSanDonViServices = loaiTaiSanDonViServices;
        }
		#endregion

		#region MuaSam
		public MuaSamSearchModel PrepareMuaSamSearchModel(MuaSamSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));
			var donvi = _workContext.CurrentDonVi;
			var donvisd = _donViService.GetDonViById(donvi.ID);
			searchModel.DonviSD_ID = donvi.ID;	
			searchModel.TenDonViSD = donvi.TEN_DON_VI;
			searchModel.MaDonViSD = donvi.MA_DON_VI;
			//loại tài sản
			//var cd = _cheDoHaoMonService.GetCheDoHaoMonByMa(enumCheDoHaoMon_ThongTu.CDHM_TT45);
			//searchModel.ListLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected:searchModel.LOAI_TAI_SAN_ID,cheDoId: cd?.ID, isAddFirst: true, isAll: true);
			var isExcuteTSDT = !_loaiTaiSanDonViServices.CheckLoaiTaiSanDacThu(_workContext.CurrentDonVi?.ID); 
			searchModel.ListLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectlistListLoaiHinhTaiSan(isExcutedTSDT: isExcuteTSDT); // chỉ lấy 10 nhóm lớn và check xem đơn vị có ts đặc thù không
			searchModel.SetGridPageSize();
			return searchModel;
		}

		public MuaSamListModel PrepareMuaSamListModel(MuaSamSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchMuaSams(Keysearch: searchModel.KeySearch, Tu_ngay: searchModel.Tu_ngay, Den_ngay: searchModel.Den_ngay, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, trangThaiId: searchModel.TRANG_THAI_ID, donViSD_ID: searchModel.DonviSD_ID, LoaiTaiSanId:searchModel.LOAI_TAI_SAN_ID, LoaiHinhTaiSanId:searchModel.LOAI_HINH_TAI_SAN_ID);

			//prepare list model
			var model = new MuaSamListModel
			{
				//fill in model values from the entity
				//Data = items.Select(c => c.ToModel<MuaSamModel>()),
				//Total = items.TotalCount
				Data = items.Select(c =>
				{
					var m = c.ToModel<MuaSamModel>();
					m.DVSDTS_Ten = _donViService.GetDonViById(m.DVSDTS_ID).TEN;
					if (m.NGUOI_TAO_ID > 0)
					{
						var nguoiTao = _nguoiDungService.GetNguoiDungById(m.NGUOI_TAO_ID ?? 0);
						m.TenNguoiTao = nguoiTao.TEN_DAY_DU;
					}
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public MuaSamModel PrepareMuaSamModel(MuaSamModel model, MuaSam item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<MuaSamModel>();
			}
			//more

			var donvi = _workContext.CurrentDonVi;
			var nguoitao = _workContext.CurrentCustomer;
			var donvisd = _donViService.GetDonViById(donvi.ID);
			var iddvc1 = donvisd.TREE_NODE.Split("-").ToList();
			model.DVSDTS_ID = donvi.ID;
			model.DVSDTS_Ten = donvi.TEN_DON_VI;
			model.DVSDTS_Ma = donvi.MA_DON_VI;
			model.NGAY_DANG_KY = DateTime.Now;
			model.DVLQTS_ID = Convert.ToDecimal(iddvc1.FirstOrDefault());
			model.NGUOI_TAO_ID = nguoitao.ID;
			if (model.ID != 0)
			{
				//get loại tài sản & list tài sản đăng ký mua sắm từ bảng mua sắm chi tiết
				List<MuaSamChiTiet> ListMuaSamChiTiet = _muaSamChiTietService.GetMapByMuaSamId(model.ID).ToList();
				foreach (var muaSamChiTiet in ListMuaSamChiTiet)
				{
					MuaSamChiTietModel muaSamChiTietModel = new MuaSamChiTietModel();
					muaSamChiTietModel = muaSamChiTiet.ToModel<MuaSamChiTietModel>();
					var LoaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(muaSamChiTietModel.LOAI_TAI_SAN_ID ?? 0);
					muaSamChiTietModel.LoaiTaiSanTen = LoaiTaiSan.TEN;
					if (muaSamChiTiet.HINH_THUC_MUA_SAM_ID > 0)
					{
						var HinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(muaSamChiTiet.HINH_THUC_MUA_SAM_ID ?? 0);
						muaSamChiTietModel.TenHinhThucMuaSam = HinhThucMuaSam.TEN;
					}
					model.ListMuaSamChiTietModel.Add(muaSamChiTietModel);
				}
			}
			return model;
		}
		public void PrepareMuaSam(MuaSamModel model, MuaSam item)
		{
			//item.GUID = model.GUID;
			//item.DVLQTS_ID = model.DVLQTS_ID;
			//item.DVSDTS_ID = model.DVSDTS_ID;
			item.NGAY_DANG_KY = model.NGAY_DANG_KY ?? DateTime.Now;
			item.NAM  = model.NGAY_DANG_KY.Value.Year;
			//item.NGAY_TAO = model.NGAY_TAO;
			//item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;

		}
		public ResultAction DuyetMuaSamFunc(decimal MuaSamId, MuaSam muaSam = null)
		{
			if (muaSam == null)
				muaSam = _itemService.GetMuaSamById(MuaSamId);

			//kiểm tra tính đúng đắn về Hiện trạng sử dụng
			muaSam.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
			muaSam.NGUOI_DUYET_ID = _workContext.CurrentCustomer.ID;
			muaSam.NGAY_DUYET = DateTime.Now;
			_itemService.UpdateMuaSam(muaSam);
			//yêu cầu nhật ký
			_hoatDongService.InsertHoatDong(enumHoatDong.Duyet, "Duyệt dữ liệu", muaSam.ToModel<MuaSamModel>(), "MuaSam");

			return new ResultAction(true, "Duyệt thành công");
		}
		public ResultAction KhongDuyetMuaSamFunc(decimal MuaSamId, string Note)
		{
			MuaSam muaSam = _itemService.GetMuaSamById(MuaSamId);
			if (muaSam == null)
				return new ResultAction(false, "Đợt mua sắm không tồn tại");

			muaSam.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.TRA_LAI;
			muaSam.NGUOI_DUYET_ID = _workContext.CurrentCustomer.ID;
			muaSam.NGAY_DUYET = DateTime.Now;
			muaSam.GHI_CHU = Note;
			_itemService.UpdateMuaSam(muaSam);

			_hoatDongService.InsertHoatDong(enumHoatDong.HuyDuyet, "Không duyệt yêu cầu", muaSam.ToModel<MuaSamModel>(), "MuaSam");
			return new ResultAction(true, "Từ chối thành công");
		}
		#endregion
	}
}

