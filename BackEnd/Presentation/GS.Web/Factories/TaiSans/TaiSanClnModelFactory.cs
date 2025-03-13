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
using GS.Services.DanhMuc;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanClnModelFactory : ITaiSanClnModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanClnService _itemService;
        private readonly ILoaiTaiSanService _loaitaisanService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly ITaiSanService _taiSanService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IDonViService _donViService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        #endregion

        #region Ctor

        public TaiSanClnModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanClnService itemService,
            ILoaiTaiSanService loaiTaiSanService,
            IHienTrangService hienTrangService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            ITaiSanService taiSanService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            IDonViService donViService,
            IHienTrangModelFactory hienTrangModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._loaitaisanService = loaiTaiSanService;
            this._hienTrangService = hienTrangService;
            this._loaitaisanModelFactory = loaiTaiSanModelFactory;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._taiSanService = taiSanService;
            this._yeuCauService = yeuCauService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._donViService = donViService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
        #endregion

        #region TaiSanCln
        public TaiSanClnSearchModel PrepareTaiSanClnSearchModel(TaiSanClnSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanClnListModel PrepareTaiSanClnListModel(TaiSanClnSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanClns(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new TaiSanClnListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<TaiSanClnModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanClnModel PrepareTaiSanClnModel(TaiSanClnModel model, TaiSanCln item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanClnModel>();
                var yeucauht = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID);
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
            }
            //more
            if (item.TAI_SAN_ID > 0)
            {
                var taisan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                model.TaiSanModel = taisan.ToModel<TaiSanModel>();
                //model.TaiSanModel = _taiSanModelFactory.PrepareTaiSanModel(model.TaiSanModel, taisan);
            }
            
            if (item.taisan.LOAI_TAI_SAN_ID > 0)
            {
                var loaiTaiSan = new LoaiTaiSan();  
                loaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(item.taisan.LOAI_TAI_SAN_ID??0);
                model.LoaiTaiSanModel = loaiTaiSan.ToModel<LoaiTaiSanModel>();
                model.NVYeuCauChiTietModel = _yeuCauChiTietModelFactory.PrepareValueFromLoaiTStoYeuCauCT(loaiTaiSan: model.LoaiTaiSanModel, nvYeuCauChiTiet: model.NVYeuCauChiTietModel);
            }
            // hien trang
            var hienTrangList = _hienTrangService.ListHienTrangsByFields(loaiHinhTSId: item.taisan.LOAI_HINH_TAI_SAN_ID);
            model.ListHienTrangModel = hienTrangList.Select(c => c.ToModel<HienTrangModel>()).ToList();
            var yeucau = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID);
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
            model.NVYeuCauChiTietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            model.NVYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.NVYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
            var donvi = _workContext.CurrentDonVi;
            var donvikh = _donViService.GetDonViById(donvi.ID);
            model.CHE_DO_HACH_TOAN_ID = donvikh.CHE_DO_HACH_TOAN_ID ?? 0;
            if(model.NVYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO != null)
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
        public void PrepareTaiSanCln(TaiSanClnModel model, TaiSanCln item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.NAM_SINH = model.NAM_SINH;

        }
        #endregion
    }
}

