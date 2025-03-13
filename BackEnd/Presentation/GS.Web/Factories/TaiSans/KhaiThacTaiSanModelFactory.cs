//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
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
using GS.Services.BienDongs;

namespace GS.Web.Factories.TaiSans
{
	public class KhaiThacTaiSanModelFactory : IKhaiThacTaiSanModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IKhaiThacTaiSanService _itemService;
		private readonly ITaiSanService _taiSanService;
		private readonly IBienDongService _bienDongService;
		#endregion

		#region Ctor

		public KhaiThacTaiSanModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			IKhaiThacTaiSanService itemService,
			ITaiSanService	taiSanService,
			IBienDongService bienDongService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._taiSanService = taiSanService;
			_bienDongService = bienDongService;
		}
		#endregion

		#region KhaiThacTaiSan
		public KhaiThacTaiSanSearchModel PrepareKhaiThacTaiSanSearchModel(KhaiThacTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public KhaiThacTaiSanListModel PrepareKhaiThacTaiSanListModel(KhaiThacTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchKhaiThacTaiSans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KhaiThacId:searchModel.KHAI_THAC_ID, KhaiThacChiTietId: searchModel.KHAI_THAC_CHI_TIET_ID);

			//prepare list model
			var model = new KhaiThacTaiSanListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<KhaiThacTaiSanModel>();
					var DienTichTS = _bienDongService.Tinh_GiaTriTaiSan(m.TAI_SAN_ID??0);
					var taiSan = _taiSanService.GetTaiSanById(m.TAI_SAN_ID??0);
					m.MA_TAI_SAN = taiSan?.MA;
					m.TEN_TAI_SAN = taiSan?.TEN;
					m.LoaiTaiSanTen = taiSan?.loaitaisan.TEN;
					//m.DIEN_TICH = DienTichTS?.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
					//m.StrVNDienTich = m.DIEN_TICH.ToVNStringNumber();
					//m.StrVNDienTichKhaiThac = m.DIEN_TICH_KHAI_THAC.ToVNStringNumber();
					m.StrVNSoTien = c.SO_TIEN?.ToVNStringNumber();
					m.StrNgayKhaiThac = $"{m.TU_NGAY?.toDateVNString()} - {m.DEN_NGAY?.toDateVNString()}";
					return m;
				}),
				Total = items.TotalCount
			};
			return model;
		}
		public KhaiThacTaiSanModel PrepareKhaiThacTaiSanModel(KhaiThacTaiSanModel model, KhaiThacTaiSan item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<KhaiThacTaiSanModel>();
			}
			//more

			return model;
		}
		public void PrepareKhaiThacTaiSan(KhaiThacTaiSanModel model, KhaiThacTaiSan item)
		{
			item.KHAI_THAC_ID = model.KHAI_THAC_ID;
			item.KHAI_THAC_CHI_TIET_ID = model.KHAI_THAC_CHI_TIET_ID;
			item.TAI_SAN_ID = model.TAI_SAN_ID;
			item.DIEN_TICH_KHAI_THAC = model.DIEN_TICH_KHAI_THAC;
			item.GHI_CHU = model.GHI_CHU;
			item.TU_NGAY = model.TU_NGAY;
			item.DEN_NGAY = model.DEN_NGAY;
			item.SO_TIEN = model.SO_TIEN;

		}

		
        #endregion
    }
}

