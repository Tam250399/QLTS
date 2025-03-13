//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
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
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Core.Domain.CauHinh;
using GS.Services.NghiepVu;
using GS.Web.Models.DanhMuc;
using GS.Services.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.BienDongs;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanKhauHaoModelFactory:ITaiSanKhauHaoModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ITaiSanKhauHaoService _itemService;
            private readonly IDonViService _donViService;
            private readonly ICauHinhService _cauHinhService;
            private readonly ITaiSanService _taiSanService;
            private readonly INguoiDungService _nguoiDungService;
            private readonly INhanHienThiService _nhanhienthiService;
            private readonly IYeuCauService _yeucauService;
            private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViService;
            private readonly ILoaiTaiSanService _loaitaisanService;
			private readonly IDonViBoPhanService _donvibophanService;
			private readonly IBienDongService _biendongService;
			private readonly ITaiSanDatService _taiSanDatService;
			private readonly ITrungGianBDYCService _trungGianBDYCService;
			private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
			private readonly IHoatDongService _hoatDongService;

        #endregion

        #region Ctor

        public TaiSanKhauHaoModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            ITaiSanKhauHaoService itemService,
			IDonViService donViService,
			ICauHinhService cauHinhService,
			ITaiSanService taiSanService,
			INguoiDungService nguoiDungService,
			INhanHienThiService nhanHienThiService,
			IYeuCauService yeuCauService,
			ILoaiTaiSanDonViServices loaiTaiSanDonViService,
			ILoaiTaiSanService loaiTaiSanServices,
			IDonViBoPhanService donViBoPhanService,
			IBienDongService bienDongService,
			ITaiSanDatService taiSanDatService,
			ITrungGianBDYCService trungGianBDYCService,
            ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
            IHoatDongService hoatDongService

            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._donViService = donViService;
            this._cauHinhService = cauHinhService;
            this._taiSanService = taiSanService;
            this._nguoiDungService = nguoiDungService;
            this._nhanhienthiService = nhanHienThiService;
            this._yeucauService = yeuCauService;
            this._loaiTaiSanDonViService = loaiTaiSanDonViService;
            this._loaitaisanService = loaiTaiSanServices;
            this._donvibophanService = donViBoPhanService;
            this._biendongService = bienDongService;
            this._taiSanDatService = taiSanDatService;
            this._trungGianBDYCService = trungGianBDYCService;
            this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
            this._hoatDongService = hoatDongService;
        }
        #endregion
        
        #region TaiSanKhauHao
        public TaiSanKhauHaoSearchModel PrepareTaiSanKhauHaoSearchModel(TaiSanKhauHaoSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

        public TaiSanKhauHaoListModel PrepareTaiSanKhauHaoListModel(TaiSanKhauHaoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));         
            //get items
            var items = _itemService.SearchTaiSanKhauHaos(TaiSanId: searchModel.TaiSanId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var lastItem = _itemService.GetTaiSanKhauHaoMoiNhatByTaiSanId(searchModel.TaiSanId??0);
            //prepare list model
            var model = new TaiSanKhauHaoListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanKhauHaoModel>();
                    if (lastItem != null && lastItem.ID == c.ID)
                    {
                        m.IsLastTSKH = true;
                    }
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;

        } 
        public TaiSanModel PrepareListTaiSanKhauHaobyTaiSanModel(TaiSanModel taiSanModel, TaiSanKhauHaoModel taiSanKhauHaoModel)
        {
            if (taiSanModel == null)
                throw new ArgumentNullException(nameof(taiSanModel));

            //get items
            //var items = _taiSanService.SearchTaiSan(: taiSanModel.ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new TaiSanModel()
            //var model = new TaiSanKhauHaoListModel()
            //{
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<TaiSanKhauHaoModel>()),
                //Total = items.TotalCount
            //}
            ;
            return model;

        }
        public TaiSanKhauHaoModel PrepareTaiSanKhauHaoModel(TaiSanKhauHaoModel model, TaiSanKhauHao item, bool excludeProperties = false)
        {
            var tskhMax = _itemService.GetTaiSanKhauHaoMoiNhatByTaiSanId(model.TAI_SAN_ID ?? 0);
            if (item != null)
            {              
                //fill in model values from the entity
                model = item.ToModel<TaiSanKhauHaoModel>();
                if (tskhMax?.ID == item.ID)
                {
                    model.IsEnableNgayKH = true;
                }
            }
            else
            {
                model.IsEnableNgayKH = true;
                model.NGAY_BAT_DAU = DateTime.Today;
            }         
            return model;
        }
       public void PrepareTaiSanKhauHao(TaiSanKhauHaoModel model, TaiSanKhauHao item)
        {
		item.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
		item.NGAY_KET_THUC = model.NGAY_KET_THUC;
		item.SO_THANG_KHAU_HAO = model.SO_THANG_KHAU_HAO;
		item.TAI_SAN_ID = model.TAI_SAN_ID;
		item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
		item.TY_LE_NGUYEN_GIA_KHAU_HAO = model.TY_LE_NGUYEN_GIA_KHAU_HAO;
            
        }
        public void PrepareTaiSanModel(TaiSanModel model, TaiSan item)
        {
            //item.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
            //item.NGAY_KET_THUC = model.NGAY_KET_THUC;
            //item.SO_THANG_KHAU_HAO = model.SO_THANG_KHAU_HAO;
            //item.TAI_SAN_ID = model.TAI_SAN_ID;
            //item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
            //item.TY_LE_NGUYEN_GIA_KHAU_HAO = model.TY_LE_NGUYEN_GIA_KHAU_HAO;
            if(item!= null) { 
                model = item.ToModel<TaiSanModel>();
            }

        }
        public void insertOrUpdateTaiSanKhauHao(TaiSanKhauHao taiSanKhauHao_cu, TaiSanKhauHaoModel taiSanKhauHaoModel)
        {
            #region Cập nhật tài sản khấu hao
            //// lấy ra tài sản
            #endregion
            #region
            var taisankhauhao = _itemService.GetTaiSanKhauHaoById(taiSanKhauHaoModel.ID);
            if (taisankhauhao!=null)
            {
                //prepare
                PrepareTaiSanKhauHao(taiSanKhauHaoModel, taisankhauhao);
                //
                if (taiSanKhauHao_cu!= null) {
                    //taiSanKhauHao_cu.ngayketthuc = ngay-1
                    DateTime ngayketthuc = taiSanKhauHaoModel.NGAY_BAT_DAU ?? DateTime.Now;
                    taiSanKhauHao_cu.NGAY_KET_THUC = ngayketthuc.AddDays(-1);
                    _itemService.UpdateTaiSanKhauHao(taiSanKhauHao_cu);
                    _itemService.UpdateTaiSanKhauHao(taisankhauhao);
                }
                else
                {
                    _itemService.UpdateTaiSanKhauHao(taisankhauhao);
                }
                _hoatDongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", taisankhauhao.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
            }
            else
            {
                taisankhauhao = taiSanKhauHaoModel.ToEntity<TaiSanKhauHao>();
                if (taiSanKhauHao_cu!= null)
                {
                    //taiSanKhauHao_cu.ngayketthuc = ngay-1
                    DateTime ngayketthuc = taiSanKhauHaoModel.NGAY_BAT_DAU ?? DateTime.Now;
                    taiSanKhauHao_cu.NGAY_KET_THUC = ngayketthuc.AddDays(-1);
                    _itemService.UpdateTaiSanKhauHao(taiSanKhauHao_cu);
                    _itemService.InsertTaiSanKhauHao(taisankhauhao);
                }
                else
                {
                    _itemService.InsertTaiSanKhauHao(taisankhauhao);
                }
                _hoatDongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", taisankhauhao.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
            }
            #endregion
        }
        public void CreateOrUpdateTaiSanKhauHao( TaiSanKhauHaoModel model)
        {
            TaiSanKhauHao item ;
            TaiSanKhauHao closestItem =null;

            if (model.ID > 0)
            {
                item = _itemService.GetTaiSanKhauHaoById(model.ID);
                if (item != null)
                {
                    closestItem = _itemService.GetListTaiSanKhauHaoByTaiSanId(model.TAI_SAN_ID ?? 0)?
                                                    .Where(c => c.ID != item.ID)?.OrderByDescending(c => c.ID)?.FirstOrDefault();                  
                    PrepareTaiSanKhauHao(model, item);
                    _itemService.UpdateTaiSanKhauHao(item);
                    _hoatDongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                }
            }
            else
            {
                 item = model.ToEntity<TaiSanKhauHao>();
                closestItem = _itemService.GetTaiSanKhauHaoMoiNhatByTaiSanId(model.TAI_SAN_ID ?? 0);
                _itemService.InsertTaiSanKhauHao(item);
                _hoatDongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                        
            }

            
            if (closestItem != null)
            {
                closestItem.NGAY_KET_THUC = item.NGAY_BAT_DAU.Value.AddDays(-1);
                _itemService.UpdateTaiSanKhauHao(closestItem);
                _hoatDongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", closestItem.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
            }
            

        }
      
        #endregion
    }
}		

