//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 16/3/2020
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
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using GS.Services.NghiepVu;
using GS.Web.Models.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Models.TaiSans;
using GS.Web.Factories.NghiepVu;
using GS.Services;
using GS.Core.Domain.TaiSans;
using GS.Web.Factories.DanhMuc;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanVoHinhModelFactory : ITaiSanVoHinhModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanVoHinhService _itemService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly ITaiSanService _taiSanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly IHienTrangService _hienTrangService;
        private readonly IDonViService _donViService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;

        #endregion Fields

        #region Ctor

        public TaiSanVoHinhModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanVoHinhService itemService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            ITaiSanService taiSanService,
            ILoaiTaiSanService loaiTaiSanService,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            IHienTrangService hienTrangService,
            IDonViService donViService,
            IHienTrangModelFactory hienTrangModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._yeuCauService = yeuCauService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._taiSanService = taiSanService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._hienTrangService = hienTrangService;
            this._donViService = donViService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }

        #endregion Ctor

        #region TaiSanVoHinh

        public TaiSanVoHinhSearchModel PrepareTaiSanVoHinhSearchModel(TaiSanVoHinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanVoHinhListModel PrepareTaiSanVoHinhListModel(TaiSanVoHinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanVoHinhs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new TaiSanVoHinhListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<TaiSanVoHinhModel>()),
                Total = items.TotalCount
            };
            return model;
        }

        public TaiSanVoHinhModel PrepareTaiSanVoHinhModel(TaiSanVoHinhModel model, TaiSanVoHinh item, bool excludeProperties = false)
        {
            decimal taisanid = 0;
            if (item != null) taisanid = item.TAI_SAN_ID;
            else if (model != null) taisanid = model.TAI_SAN_ID;

            var yeucau = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(taisanid);
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
            if (model == null) model = new TaiSanVoHinhModel();
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanVoHinhModel>();
                //var yeucauht = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID);
                //var ycctht = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucauht.ID);
                model.lstHienTrang = yeucauchitiet.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;

                // Nếu nhập sai hiện trạng thì mở tất ra cho sửa
                if (_hienTrangModelFactory.IsAnyHienTrangSai(model.lstHienTrang, _workContext.CurrentDonVi.ID))
                {
                    model.lstHienTrang = model.lstHienTrang.Select(c =>
                    {
                        c.IsOpenAll = true;
                        return c;
                    }).ToList();
                }
            }
            //more
            if (item.TAI_SAN_ID > 0)
            {
                var taisan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                model.TaiSanModel = taisan.ToModel<TaiSanModel>();
            }
            if (model.LOAI_TAI_SAN_ID > 0)
            {
                var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID);
                model.LoaiTaiSanModel = loaiTaiSan.ToModel<LoaiTaiSanModel>();
                model.NVYeuCauChiTietModel = _yeuCauChiTietModelFactory.PrepareValueFromLoaiTStoYeuCauCT(loaiTaiSan: model.LoaiTaiSanModel, nvYeuCauChiTiet: model.NVYeuCauChiTietModel);
            }
            var hienTrangList = _hienTrangService.ListHienTrangsByFields(loaiHinhTSId: item.taisan.LOAI_HINH_TAI_SAN_ID);
            model.ListHienTrangModel = hienTrangList.Select(c => c.ToModel<HienTrangModel>()).ToList();
            model.NVYeuCauChiTietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            model.NVYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.NVYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();

            var donvikh = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            model.CHE_DO_HACH_TOAN_ID = donvikh.CHE_DO_HACH_TOAN_ID ?? 0;
            if (model.NVYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO != null)
            {
                model.TRANG_THAI_KH = true;
            }
            //Prepare tên hiện trạng
            if (model.lstHienTrang != null)
            {
                foreach (var itemHT in model.lstHienTrang)
                {
                    itemHT.TenHienTrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId ?? 0).TEN_HIEN_TRANG;
                }
            }
            return model;
        }

        public void PrepareTaiSanVoHinh(TaiSanVoHinhModel model, TaiSanVoHinh item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.THONG_SO_KY_THUAT = model.THONG_SO_KY_THUAT;
        }

        #endregion TaiSanVoHinh
    }
}