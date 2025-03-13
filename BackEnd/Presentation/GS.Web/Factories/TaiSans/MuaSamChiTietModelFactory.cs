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
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Core.Domain.DanhMuc;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
	public class MuaSamChiTietModelFactory : IMuaSamChiTietModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IMuaSamChiTietService _itemService;
		private readonly ILoaiTaiSanService _loaitaisanService;
		private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
		private readonly IHinhThucMuaSamModelFactory _hinhThucMuaSamModelFactory;
		private readonly ICheDoHaoMonService _cheDoHaoMonService;
		private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		#endregion

		#region Ctor

		public MuaSamChiTietModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ILoaiTaiSanService loaitaisanService,
			IHinhThucMuaSamService hinhThucMuaSamService,
			ICheDoHaoMonService cheDoHaoMonService,
			IMuaSamChiTietService itemService,
			ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
			IHinhThucMuaSamModelFactory hinhThucMuaSamModelFactory
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._loaitaisanService = loaitaisanService;
			this._hinhThucMuaSamService = hinhThucMuaSamService;
			this._cheDoHaoMonService = cheDoHaoMonService;
			this._itemService = itemService;
			this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			this._hinhThucMuaSamModelFactory = hinhThucMuaSamModelFactory;
		}
		#endregion

		#region MuaSamChiTiet
		public MuaSamChiTietSearchModel PrepareMuaSamChiTietSearchModel(MuaSamChiTietSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public MuaSamChiTietListModel PrepareMuaSamChiTietListModel(MuaSamChiTietSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchMuaSamChiTiets(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, muaSamId: searchModel.MUA_SAM_ID);

			//prepare list model
			var model = new MuaSamChiTietListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<MuaSamChiTietModel>();
					if (c.LOAI_TAI_SAN_ID > 0)
					{
						var loaiTS = _loaitaisanService.GetLoaiTaiSanById(c.LOAI_TAI_SAN_ID.Value);
						m.LoaiTaiSanTen = loaiTS.TEN;
					}
					if (c.HINH_THUC_MUA_SAM_ID > 0)
					{
						var hinhThucMS = _hinhThucMuaSamService.GetHinhThucMuaSamById(c.HINH_THUC_MUA_SAM_ID.Value);
						m.TenHinhThucMuaSam = hinhThucMS.TEN;
					}
					m.strVNDonGia = m.DON_GIA.ToVNStringNumber();
					m.strVNSoLuong = m.SO_LUONG.ToVNStringNumber();
					m.strVNDuToanDuocDuyet = m.DU_TOAN_DUOC_DUYET.ToVNStringNumber();
					m.strVNThanhTien = (m.DON_GIA * m.SO_LUONG).ToVNStringNumber();
					return m;
				}),
				Total = items.TotalCount
			};
			return model;
		}
		public MuaSamChiTietModel PrepareMuaSamChiTietModel(MuaSamChiTietModel model, MuaSamChiTiet item, bool excludeProperties = false)
		{
			var cd = _cheDoHaoMonService.GetCheDoHaoMonByMa(enumCheDoHaoMon_ThongTu.CDHM_TT45);
			List<decimal> valInclu = new List<decimal> { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA, (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.DAC_THU, (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC, (int)enumLOAI_HINH_TAI_SAN.VO_HINH };
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<MuaSamChiTietModel>();
				var chedohaomon = _cheDoHaoMonService.GetAllCheDoHaoMons().OrderByDescending(c => c.ID).FirstOrDefault();
				
				model.LoaiTaiSanAvailable = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected: item.LOAI_TAI_SAN_ID,cheDoId: cd?.ID, isAddFirst: true, isAll:true,listLoaiHinhTaiSanId: valInclu);
				model.HinhThucMuaSamAvailable = _hinhThucMuaSamModelFactory.PrepareSelectListHinhThucMuaSam(valSelected: item.HINH_THUC_MUA_SAM_ID, isAddFirst: true);
				model.THANH_TIEN = (item.SO_LUONG ?? 0) * (item.DON_GIA ?? 0);
			}
			else
			{
				var chedohaomon = _cheDoHaoMonService.GetAllCheDoHaoMons().OrderByDescending(c => c.ID).FirstOrDefault();
				model.LoaiTaiSanAvailable = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: cd?.ID, isAddFirst: true, isAll: true, listLoaiHinhTaiSanId: valInclu);
				model.HinhThucMuaSamAvailable = _hinhThucMuaSamModelFactory.PrepareSelectListHinhThucMuaSam(isAddFirst: true);
			}
			//moremodel.LoaiTaiSanAvailable
			return model;
		}
		public void PrepareMuaSamChiTiet(MuaSamChiTietModel model, MuaSamChiTiet item)
		{
			item.MUA_SAM_ID = model.MUA_SAM_ID;
			item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
			item.TEN_TAI_SAN = model.TEN_TAI_SAN;
			item.HINH_THUC_MUA_SAM_ID = model.HINH_THUC_MUA_SAM_ID;
			item.DAC_DIEM = model.DAC_DIEM;
			item.SO_LUONG = model.SO_LUONG;
			item.DON_GIA = model.DON_GIA;
			item.THOI_GIAN_DU_KIEN = model.THOI_GIAN_DU_KIEN;
			item.DU_TOAN_DUOC_DUYET = model.DU_TOAN_DUOC_DUYET;
			item.IS_TAI_SAN_NGUON_VIEN_TRO = model.IS_TAI_SAN_NGUON_VIEN_TRO;
			item.DE_XUAT = model.DE_XUAT;
			item.GHI_CHU = model.GHI_CHU;
		}
		#endregion
	}
}

