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
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanVktModelFactory : ITaiSanVktModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanVktService _itemService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly ITaiSanService _taiSanService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IDonViService _donViService;
        private readonly ITrungGianBDYCService _trungGianBDYCService;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        #endregion

        #region Ctor

        public TaiSanVktModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanVktService itemService,
            ILoaiTaiSanService loaiTaiSanService,
            IHienTrangService hienTrangService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            ITaiSanService taiSanService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            IDonViService donViService,
            ITrungGianBDYCService trungGianBDYCService,
            IHienTrangModelFactory hienTrangModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._hienTrangService = hienTrangService;
            this._loaitaisanModelFactory = loaiTaiSanModelFactory;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._taiSanService = taiSanService;
            this._yeuCauService = yeuCauService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._donViService = donViService;
            this._trungGianBDYCService = trungGianBDYCService;
            _hienTrangModelFactory = hienTrangModelFactory;
        }
        #endregion

        #region TaiSanVkt
        public TaiSanVktSearchModel PrepareTaiSanVktSearchModel(TaiSanVktSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanVktListModel PrepareTaiSanVktListModel(TaiSanVktSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanVkts(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new TaiSanVktListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<TaiSanVktModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanVktModel PrepareTaiSanVktModel(TaiSanVktModel model, TaiSanVkt item, bool excludeProperties = false, bool isTangMoi = true)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanVktModel>();
                if (isTangMoi)  // lấy ra biến động hoặc yêu cầu đầu tiên
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
            if (item.TAI_SAN_ID > 0)
            {
                var taisan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                model.TaiSanModel = taisan.ToModel<TaiSanModel>();
                //model.TaiSanModel = _taiSanModelFactory.PrepareTaiSanModel(model.TaiSanModel, taisan);
            }
            
            if (item.taisan.LOAI_TAI_SAN_ID > 0)
            {
                var loaiTaiSan = new LoaiTaiSan();
                //loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.LoaiTaiSanModel.ID);
                loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.taisan.LOAI_TAI_SAN_ID??0);
                model.LoaiTaiSanModel = loaiTaiSan.ToModel<LoaiTaiSanModel>();
                model.NVYeuCauChiTietModel = _yeuCauChiTietModelFactory.PrepareValueFromLoaiTStoYeuCauCT(loaiTaiSan: model.LoaiTaiSanModel, nvYeuCauChiTiet: model.NVYeuCauChiTietModel);
            }
            //var loaitaisan = _loaitaisanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID);
            //model.LoaiTaiSanModel = loaitaisan.ToModel<LoaiTaiSanModel>();
            var hienTrangList = _hienTrangService.ListHienTrangsByFields(loaiHinhTSId: item.taisan.LOAI_HINH_TAI_SAN_ID);
            model.ListHienTrangModel = hienTrangList.Select(c => c.ToModel<HienTrangModel>()).ToList();
            //var yeucau = _yeuCauService.GetYeuCauMoiNhatByTaiSanId(model.TAI_SAN_ID);
            //var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeucau.ID);
            //model.NVYeuCauChiTietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            if (model.NVYeuCauChiTietModel == null)
                model.NVYeuCauChiTietModel = new YeuCauChiTietModel();
            model.NVYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.NVYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
            var donvi = _workContext.CurrentDonVi;
            var donvikh = _donViService.GetDonViById(donvi.ID);
            model.CHE_DO_HACH_TOAN_ID = donvikh.CHE_DO_HACH_TOAN_ID ?? 0;
            if (model.NVYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO != null)
            {
                model.TRANG_THAI_KH = true;
            }
            if (model.TaiSanModel.QUYET_DINH_SO != null || model.TaiSanModel.QUYET_DINH_NGAY != null)
            {
                model.TaiSanModel.cohoso = true;
            }
            model.VKT_DIEN_TICH = model.DIEN_TICH;
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
        public void PrepareTaiSanVkt(TaiSanVktModel model, TaiSanVkt item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.DIEN_TICH = model.VKT_DIEN_TICH;
            item.THE_TICH = model.THE_TICH;
            item.CHIEU_DAI = model.CHIEU_DAI;

        }
        #endregion
    }
}

