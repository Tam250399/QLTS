using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.KeToan;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.BienDongs
{
    public class TrungGianBDYCModelFactory : ITrungGianBDYCModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IYeuCauService _yeuCauService;
        private readonly ILyDoBienDongService _lydobiendongService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly IDonViService _donViService;
        private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
        private readonly INguonVonModelFactory _nguonVonModelFactory;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly INhanHienThiService _nhanhienthiService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanNhaService _taisannhaService;
        private readonly ITaiSanDatService _taisandatService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly INguonVonService _NguonVonService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ITrungGianBDYCService _itemService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly INhanXeService _nhanXeService;
        private readonly IDiaBanService _diaBanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDongXeService _dongXeService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IDuAnService _duAnService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
        #endregion Fields

        #region Ctor

        public TrungGianBDYCModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IYeuCauService yeuCauService,
            ILyDoBienDongService lydobiendongService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViBoPhanService donViBoPhanService,
            ILoaiTaiSanService loaiTaiSanService,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            IYeuCauChiTietService yeuCauChiTietService,
            IDonViService donViService,
            ILyDoBienDongModelFactory lyDoBienDongModelFactory,
            INguonVonModelFactory nguonVonModelFactory,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ICheDoHaoMonService cheDoHaoMonService,
            INhanHienThiService nhanhienthiService,
            INguoiDungService nguoiDungService,
            ITaiSanService taiSanService,
            ITaiSanNhaService taisannhaService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanDatService taisandatService,
            INguonVonService nguonVonService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            ITrungGianBDYCService itemService,
            IMucDichSuDungService mucDichSuDungService,
            ITaiSanVktService taiSanVktService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanClnService taiSanClnService,
            INhanXeService nhanXeService,
            IDiaBanService diaBanService,
            IQuocGiaService quocGiaService,
            INhanHienThiService nhanHienThiService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IChucDanhService chucDanhService,
            IDongXeService dongXeService,
            IHienTrangService hienTrangService,
            IDuAnService duAnService,
            ICauHinhService cauHinhService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhauHaoTaiSanService khauHaoTaiSanService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._yeuCauService = yeuCauService;
            this._lydobiendongService = lydobiendongService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViBoPhanService = donViBoPhanService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._donViService = donViService;
            this._lyDoBienDongModelFactory = lyDoBienDongModelFactory;
            this._nguonVonModelFactory = nguonVonModelFactory;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._nhanhienthiService = nhanhienthiService;
            this._nguoiDungService = nguoiDungService;
            this._taiSanService = taiSanService;
            this._taisannhaService = taisannhaService;
            this._taisandatService = taisandatService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._NguonVonService = nguonVonService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._itemService = itemService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanClnService = taiSanClnService;
            this._nhanXeService = nhanXeService;
            this._diaBanService = diaBanService;
            this._quocGiaService = quocGiaService;
            this._nhanHienThiService = nhanHienThiService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._chucDanhService = chucDanhService;
            this._dongXeService = dongXeService;
            this._hienTrangService = hienTrangService;
            this._duAnService = duAnService;
            _cauHinhService = cauHinhService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khauHaoTaiSanService = khauHaoTaiSanService;
        }

        #endregion Ctor

        #region YeuCau

        public virtual TrungGianBDYCSearchModel PrepareTrungGianBDYCSearchModel(TrungGianBDYCSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.enumLoaiHinhTaiSan).ToSelectList();
            int[] trangThaiNhapId = { (int)enumTRANG_THAI_TAI_SAN.NHAP };
            searchModel.Trangthailist = ((enumTRANG_THAI_YEU_CAU)searchModel.enumtrangthaiyeucau).ToSelectList(valuesToExclude: trangThaiNhapId);
            var donviID = _workContext.CurrentDonVi.ID;
            searchModel.BoPhanSuDungAvailable = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: donviID, isAddFirst: true, valSelected: searchModel.DON_VI_BO_PHAN_ID);
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public virtual TrungGianBDYCListModel PrepareTrungGianBDYCListModel(TrungGianBDYCSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            IList<decimal?> lstLoaiBienDong = new List<decimal?>();
            if (searchModel.LOAI_LY_DO_BD_ID > 0)
            {
                lstLoaiBienDong.Add(searchModel.LOAI_LY_DO_BD_ID);
            }
            if (searchModel.LOAI_LY_DO_BD_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
            {
                lstLoaiBienDong.Add((int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO);
            }
            else if (searchModel.LOAI_LY_DO_BD_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
            {
                lstLoaiBienDong.Add((int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN);
            }

            //get items
            var items = _itemService.SearchYeuCauVaBienDong(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, keySearch: searchModel.KeySearch, fromdate: searchModel.FromDate, todate: searchModel.ToDate, loaiHinhTSId: searchModel.LOAI_HINH_TAI_SAN_ID, loaiTSId: searchModel.LOAI_TAI_SAN_ID, chungTuSo: searchModel.CHUNG_TU_SO, nguoiTaoId: searchModel.NGUOI_TAO_ID, boPhanId: searchModel.DON_VI_BO_PHAN_ID, donViId: searchModel.DON_VI_ID, loaiBienDongIds: lstLoaiBienDong, lyDoBienDongId: searchModel.LY_DO_BIEN_DONG_ID, taisanId: searchModel.taisanId, isIgnoreTraLai: searchModel.isIgnoreTraLai, trangThaiId: searchModel.TRANG_THAI_ID, quyetDinhSo: searchModel.QUYET_DINH_SO, isDuyet: searchModel.isDuyet);

            //prepare list model
            var model = new TrungGianBDYCListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareModelForList(c, searchModel.Page)).ToList(),
                Total = items.TotalCount
            };
            return model;
        }

        public virtual TrungGianBDYCModel PrepareTrungGianBDYCModel(TrungGianBDYCModel model, TrungGianBDYC item, bool isInfoBienDong = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TrungGianBDYCModel>();
                model.thongTinChungTaiSanModel = PrepareTTChung(model);
                if (item.DON_VI_BO_PHAN_ID > 0)
                {
                    var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(model.DON_VI_BO_PHAN_ID ?? 0);
                    model.TenDonViBoPhan = donViBoPhan.TEN;
                }
                if (item.LY_DO_BIEN_DONG_ID.HasValue && item.LY_DO_BIEN_DONG_ID > 0)
                {
                    var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID ?? 0);
                    model.TenLyDoBienDong = lyDoBienDong.TEN;
                    model.MaLyDoBienDong = lyDoBienDong.MA;
                    model.TenLoaiLyDoBienDong = _nhanhienthiService.GetGiaTriEnum(lyDoBienDong.loaiLyDoTangGiam);
                }

                if (item.DON_VI_ID.HasValue && item.DON_VI_ID > 0)
                {
                    var donVi = _donViService.GetDonViById(model.DON_VI_ID ?? 0);
                    model.TenDonVi = donVi.TEN;
                    model.MaDonVi = donVi.MA;
                }
                model.NguyenGiaVNStringNumber = model.NGUYEN_GIA.ToVNStringNumber();
                var taiSan = _taiSanService.GetTaiSanById(item.TAI_SAN_ID);
                if (taiSan != null)
                {
                    model.DU_AN_ID = taiSan.DU_AN_ID;
					if (model.DU_AN_ID > 0)
					{
                        DuAn duAnEntity = _duAnService.GetDuAnById(model.DU_AN_ID.Value);
                        model.TenDuAn = duAnEntity.TEN;
					}
                    model.TaiSanGuid = taiSan.GUID;
                    model.NamSanXuat = taiSan.NAM_SAN_XUAT;
                    model.TenNuocSanXuat = taiSan.nuocsanxuat != null ? taiSan.nuocsanxuat.TEN : null;
                    model.GIA_MUA_TIEP_NHAN = taiSan.GIA_MUA_TIEP_NHAN;
                    model.IS_MIEN_THUE = taiSan.IS_MIEN_THUE;
                    model.MIEN_THUE_SO_TIEN = taiSan.MIEN_THUE_SO_TIEN;
                    model.PHUONG_THUC_MUA_SAM_ID = taiSan.PHUONG_THUC_MUA_SAM_ID;
                    if (taiSan.PHUONG_THUC_MUA_SAM_ID != null && taiSan.PHUONG_THUC_MUA_SAM_ID != 0)
                    {
                        model.TenPhuongThucMuaSam = _nhanHienThiService.GetGiaTriEnum((enumPHUONG_THUC_MUA_SAM)(taiSan.PHUONG_THUC_MUA_SAM_ID ?? 0));
                    }
                     model.TenDonViMuaSam = _donViService.GetDonViById(taiSan.DON_VI_MUA_SAM_TAP_TRUNG_ID ?? 0)?.TEN;
                }
                if (model.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
                {
                    var yeuCauCTEntity = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(item.ID);
                    model.TrungGianBDYCChiTietModel = (new TrungGianBDYCChiTiet(yeuCauCTEntity)).ToModel<TrungGianBDYCChiTietModel>();
                    if (!String.IsNullOrEmpty(yeuCauCTEntity.HTSD_JSON))
                    {
                        model.TrungGianBDYCChiTietModel.lstHienTrang = yeuCauCTEntity.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    }
                }
                else
                {
                    var bienDongCTEntity = _bienDongChiTietService.GetBienDongChiTietByBDId(item.ID);
                    model.TrungGianBDYCChiTietModel = (new TrungGianBDYCChiTiet(bienDongCTEntity)).ToModel<TrungGianBDYCChiTietModel>();
                    var kthm = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(item.TAI_SAN_ID, item.NGAY_BIEN_DONG.Value.Year - 1, null);
                    model.TrungGianBDYCChiTietModel.HM_GIA_TRI_CON_LAI_CU = kthm?.TONG_GIA_TRI_CON_LAI;
                    if (!String.IsNullOrEmpty(bienDongCTEntity.HTSD_JSON))
                    {
                        var obj_ht = bienDongCTEntity.HTSD_JSON.toEntity<HienTrangList>();
                        if (obj_ht != null)
                        {
                            model.TrungGianBDYCChiTietModel.lstHienTrang = obj_ht.lstObjHienTrang;
                        }
                    }
                }
                //Prepare tên hiện trạng
                if (model.TrungGianBDYCChiTietModel.lstHienTrang != null)
                {
                    foreach (var itemHT in model.TrungGianBDYCChiTietModel.lstHienTrang)
                    {
                        itemHT.TenHienTrang = _hienTrangService.GetHienTrangById(itemHT.HienTrangId ?? 0).TEN_HIEN_TRANG;
                    }
                }
                if (model.TrungGianBDYCChiTietModel != null)
                {
                    if (model.TrungGianBDYCChiTietModel.MUC_DICH_SU_DUNG_ID > 0)
                        model.TenMucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(model.TrungGianBDYCChiTietModel.MUC_DICH_SU_DUNG_ID ?? 0).TEN;
                    if (model.TrungGianBDYCChiTietModel.HINH_THUC_MUA_SAM_ID > 0)
                        model.TenHinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(model.TrungGianBDYCChiTietModel.HINH_THUC_MUA_SAM_ID ?? 0).TEN;
                    if ((item.LOAI_TAI_SAN_ID.HasValue && item.LOAI_TAI_SAN_ID > 0) || (item.LOAI_TAI_SAN_DON_VI_ID.HasValue && item.LOAI_TAI_SAN_DON_VI_ID > 0))
                    {
                        if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                        {
                            var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(item.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                            if (loaiTSVH != null)
                            {
                                var loaiTSCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(loaiTSVH.PARENT_ID ?? 0);
                                model.TenLoaiTaiSan = loaiTSVH.TEN;
                                model.TrungGianBDYCChiTietModel.HM_TY_LE_HAO_MON = loaiTSVH.HM_TY_LE ?? 0;
                                if (loaiTSCha != null)
                                    model.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                            }
                        }
                        else
                        {
                            var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID ?? 0);
                            var loaiTSCha = _loaiTaiSanService.GetLoaiTaiSanById(loaiTaiSan.PARENT_ID ?? 0);
                            model.TrungGianBDYCChiTietModel.HM_TY_LE_HAO_MON = loaiTaiSan.HM_TY_LE ?? 0;
                            model.TenLoaiTaiSan = loaiTaiSan.TEN;
                            model.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                        }
                    }
                    if (model.TrungGianBDYCChiTietModel.OTO_CHUC_DANH_ID.HasValue && model.TrungGianBDYCChiTietModel.OTO_CHUC_DANH_ID > 0)
                    {
                        var chucDanh = _chucDanhService.GetChucDanhById(model.TrungGianBDYCChiTietModel.OTO_CHUC_DANH_ID.Value);
                        if (chucDanh != null)
                            model.TrungGianBDYCChiTietModel.OtoTenChucDanh = chucDanh.TEN_CHUC_DANH;
                    }
                }
                if (!string.IsNullOrEmpty(model.TrungGianBDYCChiTietModel.DATA_JSON))
                {
                    var ycct_json = model.TrungGianBDYCChiTietModel.DATA_JSON.toEntity<YeuCauChiTiet>();
                    if (ycct_json != null)
                    {
                        model.TrungGianBDYCChiTietModel.HS_CNQSD_SO = ycct_json.HS_CNQSD_SO;
                        model.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = ycct_json.TAI_SAN_TRUOC_DIEU_CHUYEN_ID;
                        //If tài sản nhận điều chuyển
                        if (model.TAI_SAN_TRUOC_DIEU_CHUYEN_ID > 0)
                        {
                            var taiSanTrcDC = _taiSanService.GetTaiSanById(model.TAI_SAN_TRUOC_DIEU_CHUYEN_ID ?? 0);
                            if (taiSanTrcDC != null)
                            {
                                model.TenDonViDieuChuyen = taiSanTrcDC.donvi.TEN;
                                model.MaTaiSanDieuChuyen = taiSanTrcDC.MA;
                            }
                        }
                    }
                }
                model.lstNguonVonModel = item.NGUON_VON_JSON.toEntities<NguonVonModel>();
                PrepareTTNguonVon(model);
                PrepareGiaTriCu(model);
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                    PrepareTTGiamToanBo(model);
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                    if (model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
                        model.TrungGianBDYCChiTietModel.DonViNhanDieuChuyenTen = _donViService.GetDonViById(model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0).TEN;
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && (model.TrungGianBDYCChiTietModel.KH_GIA_TINH_KHAU_HAO == null || model.TrungGianBDYCChiTietModel.KH_GIA_TINH_KHAU_HAO == 0))
                {
                    model.TrungGianBDYCChiTietModel.HM_LUY_KE = (model.TrungGianBDYCChiTietModel.NGUYEN_GIA ?? 0) - (model.TrungGianBDYCChiTietModel.HM_GIA_TRI_CON_LAI ?? 0);
                }
            }
            if (isInfoBienDong)
                PrepareThongTinChiTietTaiSan(model);
            //more
            return model;
        }

        public virtual TrungGianBDYCModel PrepareThongTinChiTietTaiSan(TrungGianBDYCModel model)
        {
            switch (model.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var tsDat = _taisandatService.GetTaiSanDatById(model.TAI_SAN_ID);
                    model.taisanDatModel = tsDat.ToModel<TaiSanDatModel>();
                    PrepareStringDiaBan(model);
                    //prepare list tài sản nhà
                    var _listTSNha = _taisannhaService.GetTaiSanNhaByDatId(model.TAI_SAN_ID).Select(c => (decimal)c.TAI_SAN_ID).ToList();
                    model.taisanDatModel.ListTaiSanNhaTrenDat = _taiSanService.GetTaiSanByIds(_listTSNha.ToArray()).Select(c => c.ToModel<TaiSanModel>()).ToList();
                    if (model.taisanDatModel.ListTaiSanNhaTrenDat != null && model.taisanDatModel.ListTaiSanNhaTrenDat.Count() > 0)
                    {
                        foreach (var item in model.taisanDatModel.ListTaiSanNhaTrenDat)
                        {
                            item.TenLoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID ?? 0).TEN;
                            item.NguyenGiaTaiSan = _bienDongService.TinhNguyenGiaTaiSanOnlyBD(item.ID);
                            var bienDong = _bienDongService.Tinh_GiaTriTaiSan(item.ID);
                            item.strDienTichSanVN = bienDong.NHA_TONG_DIEN_TICH_XD_CU.ToVNStringNumber();
                            //get id biến động tăng mới tài sản
                            if (item.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || item.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
                            {
                                var _bienDongTangMoi = _bienDongService.GetBienDongsByTaiSanId(item.ID).Where(c => c.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).FirstOrDefault();
                                if (_bienDongTangMoi != null)
                                    item.YeuCauId = _bienDongTangMoi.ID;
                            }
                            else
                            {
                                item.YeuCauId = _yeuCauService.GetYeuCauCuNhatByTSId(item.ID).ID;
                            }
                        }
                    }
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var tsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.TAI_SAN_ID);
                    model.taisanNhaModel = tsNha.ToModel<TaiSanNhaModel>();
                    if (tsNha.TAI_SAN_DAT_ID > 0)
                    {
                        var _tsDat = _taiSanService.GetTaiSanById(tsNha.TAI_SAN_DAT_ID.Value);
                        model.taisanNhaModel.MaTaiSanDat = _tsDat?.MA;
                        model.taisanNhaModel.TenTaiSanDat = _tsDat?.TEN;
                    }
                    //else
                    //{
                    //    PrepareStringDiaBan(model);

                    //    if (!string.IsNullOrEmpty(model.TenTinh))
                    //    {
                    //        model.TrungGianBDYCChiTietModel.DIA_CHI = model.TrungGianBDYCChiTietModel.DIA_CHI + ", " + model.TenTinh;
                    //    }
                    //    if (!string.IsNullOrEmpty(model.TenHuyen))
                    //    {
                    //        model.TrungGianBDYCChiTietModel.DIA_CHI = model.TrungGianBDYCChiTietModel.DIA_CHI + ", " + model.TenHuyen;
                    //    }
                    //    if (!string.IsNullOrEmpty(model.TenXa))
                    //    {
                    //        model.TrungGianBDYCChiTietModel.DIA_CHI = model.TrungGianBDYCChiTietModel.DIA_CHI + ", " + model.TenXa;
                    //    }
                    //}
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    var tsOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(model.TAI_SAN_ID);
                    model.taiSanOtoModel = tsOto.ToModel<TaiSanOtoModel>();
                    if (model.TrungGianBDYCChiTietModel.OTO_NHAN_XE_ID > 0)
                    {
                        var nhanXe = _nhanXeService.GetNhanXeById(model.TrungGianBDYCChiTietModel.OTO_NHAN_XE_ID.Value);
                        if (nhanXe != null)
                            model.TenNhanXe = nhanXe.TEN;
                        model.taiSanOtoModel.TenChucDanh = tsOto.chucDanh != null ? tsOto.chucDanh.TEN_CHUC_DANH : "";
                    }
                    if (tsOto.DONG_XE_ID > 0)
                    {
                        var DongXe = _dongXeService.GetDongXeById(tsOto.DONG_XE_ID.GetValueOrDefault());
                        if (DongXe != null)
                        {
                            model.taiSanOtoModel.TenDongXe = DongXe.TEN;
                        }
                    }
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    var tsMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(model.TAI_SAN_ID);
                    model.taiSanMayMocModel = tsMayMoc.ToModel<TaiSanMayMocModel>();
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    var tsVoHinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.TAI_SAN_ID);
                    model.taiSanVoHinhModel = tsVoHinh.ToModel<TaiSanVoHinhModel>();
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    var tsCLN = _taiSanClnService.GetTaiSanClnByTaiSanId(model.TAI_SAN_ID);
                    model.taiSanClnModel = tsCLN.ToModel<TaiSanClnModel>();
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var tsVKT = _taiSanVktService.GetTaiSanVktByTaiSanId(model.TAI_SAN_ID);
                    model.taisanVktModel = tsVKT.ToModel<TaiSanVktModel>();
                    break;

                default:
                    break;
            }
            return model;
        }

        public virtual TrungGianBDYCModel PrepareTTNguonVon(TrungGianBDYCModel model)
        {
            if (model.BDorYC == (int)enumBDorYC.YEU_CAU)
                model.lstNguonVonModel = model.NGUON_VON_JSON.toEntities<NguonVonModel>();
            else
            {
                var nguonVons = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: model.TAI_SAN_ID, BienDongID: model.ID);
                model.lstNguonVonModel = nguonVons.Select(c => c.ToModel<NguonVonModel>()).ToList();
            }
            if (model.lstNguonVonModel != null)
            {
                foreach (var item in model.lstNguonVonModel)
                {
                    item.TEN = _NguonVonService.GetNguonVonById(item.ID).TEN;
                }
            }
            return model;
        }

        public virtual ThongTinChungTaiSanModel PrepareTTChung(TrungGianBDYCModel model)
        {
            ThongTinChungTaiSanModel thongTinChungTaiSanModel = new ThongTinChungTaiSanModel();
            if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
            {
                var TrungGianBDYC = _itemService.GetYCBDGanNhat(model.TAI_SAN_ID);
                thongTinChungTaiSanModel.TAI_SAN_TEN = TrungGianBDYC.TAI_SAN_TEN;
                thongTinChungTaiSanModel.TAI_SAN_MA = TrungGianBDYC.TAI_SAN_MA;
                thongTinChungTaiSanModel.NGAY_SU_DUNG = TrungGianBDYC.NGAY_SU_DUNG;
                thongTinChungTaiSanModel.LOAI_HINH_TAI_SAN_ID = TrungGianBDYC.LOAI_HINH_TAI_SAN_ID;
                if (TrungGianBDYC.DON_VI_BO_PHAN_ID > 0)
                {
                    var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(TrungGianBDYC.DON_VI_BO_PHAN_ID ?? 0);
                    thongTinChungTaiSanModel.TenDonViBoPhan = donViBoPhan.TEN;
                }
                if (TrungGianBDYC.LOAI_TAI_SAN_ID.HasValue && TrungGianBDYC.LOAI_TAI_SAN_ID > 0)
                {
                    var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(TrungGianBDYC.LOAI_TAI_SAN_ID ?? 0);
                    var loaiTSCha = _loaiTaiSanService.GetLoaiTaiSanById(loaiTaiSan.PARENT_ID ?? 0);
                    thongTinChungTaiSanModel.TenLoaiTaiSan = loaiTaiSan.TEN;
                    thongTinChungTaiSanModel.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                }
                else if (TrungGianBDYC.LOAI_TAI_SAN_DON_VI_ID > 0)
                {
                    var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(TrungGianBDYC.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                    var loaiTSCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(loaiTSVH.PARENT_ID ?? 0);
                    thongTinChungTaiSanModel.TenLoaiTaiSan = loaiTSVH.TEN;
                    thongTinChungTaiSanModel.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                }
                if (TrungGianBDYC.DON_VI_ID.HasValue && TrungGianBDYC.DON_VI_ID > 0)
                {
                    var donVi = _donViService.GetDonViById(TrungGianBDYC.DON_VI_ID ?? 0);
                    thongTinChungTaiSanModel.TenDonVi = donVi.TEN;
                }
            }
            else
            {
                thongTinChungTaiSanModel.TAI_SAN_TEN = model.TAI_SAN_TEN;
                thongTinChungTaiSanModel.TAI_SAN_MA = model.TAI_SAN_MA;
                thongTinChungTaiSanModel.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
                if (model.DON_VI_BO_PHAN_ID > 0)
                {
                    var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(model.DON_VI_BO_PHAN_ID ?? 0);
                    thongTinChungTaiSanModel.TenDonViBoPhan = donViBoPhan.TEN;
                }
                if (model.LOAI_TAI_SAN_ID.HasValue && model.LOAI_TAI_SAN_ID > 0)
                {
                    var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID ?? 0);
                    var loaiTSCha = _loaiTaiSanService.GetLoaiTaiSanById(loaiTaiSan.PARENT_ID ?? 0);
                    thongTinChungTaiSanModel.TenLoaiTaiSan = loaiTaiSan.TEN;
                    thongTinChungTaiSanModel.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                }
                else if (model.LOAI_TAI_SAN_DON_VI_ID > 0)
                {
                    var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                    var loaiTSCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(loaiTSVH.PARENT_ID ?? 0);
                    thongTinChungTaiSanModel.TenLoaiTaiSan = loaiTSVH.TEN;
                    thongTinChungTaiSanModel.TenLoaiTaiSanParent = loaiTSCha != null ? loaiTSCha.TEN : null;
                }
                if (model.DON_VI_ID.HasValue && model.DON_VI_ID > 0)
                {
                    var donVi = _donViService.GetDonViById(model.DON_VI_ID ?? 0);
                    thongTinChungTaiSanModel.TenDonVi = donVi.TEN;
                }
            }
            return thongTinChungTaiSanModel;
        }

        public virtual TrungGianBDYCModel PrepareTTGiamToanBo(TrungGianBDYCModel model)
        {
            if (model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
                model.TrungGianBDYCChiTietModel.DonViNhanDieuChuyenTen = _donViService.GetDonViById(model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0).TEN;
            if (model.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID > 0)
                model.TenPhuongAnXuLy = _nhanHienThiService.GetGiaTriEnum((enumHINH_THUC_XU_LY_TSC)model.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID);
            if (model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
            {
                var donVi = _donViService.GetDonViById(model.TrungGianBDYCChiTietModel.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0);
                model.TrungGianBDYCChiTietModel.DonViNhanDieuChuyenTen = donVi.TEN;
            }
            return model;
        }

        public virtual void PrepareGiaTriCu(TrungGianBDYCModel model)
        {
            var giaTriCu = _bienDongService.Tinh_GiaTriTaiSan(model.TAI_SAN_ID, model.NGAY_BIEN_DONG);
            model.TrungGianBDYCChiTietModel.NGUYEN_GIA_CU = giaTriCu.NGUYEN_GIA_CU;
            model.TrungGianBDYCChiTietModel.DAT_TONG_DIEN_TICH_CU = giaTriCu.DAT_TONG_DIEN_TICH_CU;
            model.TrungGianBDYCChiTietModel.NHA_DIEN_TICH_XD_CU = giaTriCu.NHA_DIEN_TICH_XD_CU;
            model.TrungGianBDYCChiTietModel.NHA_TONG_DIEN_TICH_XD_CU = giaTriCu.NHA_TONG_DIEN_TICH_XD_CU;
            model.TrungGianBDYCChiTietModel.VKT_CHIEU_DAI_CU = giaTriCu.VKT_CHIEU_DAI_CU;
            model.TrungGianBDYCChiTietModel.VKT_DIEN_TICH_CU = giaTriCu.VKT_DIEN_TICH_CU;
            model.TrungGianBDYCChiTietModel.VKT_THE_TICH_CU = giaTriCu.VKT_THE_TICH_CU;

            model.TrungGianBDYCChiTietModel.NGUYEN_GIA_SAU_BD = model.TrungGianBDYCChiTietModel.NGUYEN_GIA_CU + model.NGUYEN_GIA;

            decimal datTongDT = model.TrungGianBDYCChiTietModel.DAT_TONG_DIEN_TICH ?? 0;
            decimal datTongDTCu = model.TrungGianBDYCChiTietModel.DAT_TONG_DIEN_TICH_CU ?? 0;
            model.TrungGianBDYCChiTietModel.DAT_TONG_DIEN_TICH_SAU_BD = datTongDT + datTongDTCu;

            decimal nhaDTXD = model.TrungGianBDYCChiTietModel.NHA_DIEN_TICH_XD ?? 0;
            decimal nhaDTXDCu = model.TrungGianBDYCChiTietModel.NHA_DIEN_TICH_XD_CU ?? 0;
            model.TrungGianBDYCChiTietModel.NHA_DIEN_TICH_XD_SAU_BD = nhaDTXD + nhaDTXDCu;

            decimal nhaTongDT = model.TrungGianBDYCChiTietModel.NHA_TONG_DIEN_TICH_XD ?? 0;
            decimal nhaTongDTCu = model.TrungGianBDYCChiTietModel.NHA_TONG_DIEN_TICH_XD_CU ?? 0;
            model.TrungGianBDYCChiTietModel.NHA_TONG_DIEN_TICH_XD_SAU_BD = nhaTongDT + nhaTongDTCu;

            model.TrungGianBDYCChiTietModel.VKT_THE_TICH_SAU_BD = model.TrungGianBDYCChiTietModel.VKT_THE_TICH_CU.GetValueOrDefault() + model.TrungGianBDYCChiTietModel.VKT_THE_TICH.GetValueOrDefault();
            model.TrungGianBDYCChiTietModel.VKT_DIEN_TICH_SAU_BD = model.TrungGianBDYCChiTietModel.VKT_DIEN_TICH_CU.GetValueOrDefault() + model.TrungGianBDYCChiTietModel.VKT_DIEN_TICH.GetValueOrDefault();
            model.TrungGianBDYCChiTietModel.VKT_CHIEU_DAI_SAU_BD = model.TrungGianBDYCChiTietModel.VKT_CHIEU_DAI_CU.GetValueOrDefault() + model.TrungGianBDYCChiTietModel.VKT_CHIEU_DAI.GetValueOrDefault();
        }

        public virtual void PrepareStringDiaBan(TrungGianBDYCModel model)
        {
            if (model.TrungGianBDYCChiTietModel.DIA_BAN_ID > 0)
            {
                var diaBan_Xa = _diaBanService.GetDiaBanById(model.TrungGianBDYCChiTietModel.DIA_BAN_ID.Value);
                List<decimal> listIDDiaBan = diaBan_Xa.TREE_NODE.Split('-').Select(p => decimal.Parse(p)).ToList();
                if (listIDDiaBan != null && listIDDiaBan.Count > 0)
                {
                    var listDB = _diaBanService.GetDiaBanByIds(listIDDiaBan.ToArray());
                    if (listDB != null && listDB.Count > 0)
                    {
                        foreach (var diaBan in listDB)
                        {
                            switch (diaBan.LoaiDiaBan)
                            {
                                case enumLOAI_DIABAN.TINH:
                                    model.TenTinh = diaBan.TEN;
                                    break;

                                case enumLOAI_DIABAN.HUYEN:
                                    model.TenHuyen = diaBan.TEN;
                                    break;

                                case enumLOAI_DIABAN.XA:
                                    model.TenXa = diaBan.TEN;
                                    break;

                                default:
                                    break;
                            }
                        }
                        model.TenQuocGia = listDB.FirstOrDefault().Quocgia.TEN;

                    }
                }
            }
        }
        private TrungGianBDYCModel PrepareModelForList(TrungGianBDYC entity, int? pageIndex = null)
        {
            var m = entity.ToModel<TrungGianBDYCModel>();
            m.pageIndex = pageIndex;
            if (m.NGUOI_TAO_ID > 0)
            {
                var nguoidung = _nguoiDungService.GetNguoiDungById(m.NGUOI_TAO_ID);
                m.TenNguoiTao = nguoidung.TEN_DANG_NHAP;
            }
            m.TenTrangThai = _nhanhienthiService.GetGiaTriEnum(entity.TrangThaiYeuCau);
            if (m.DON_VI_BO_PHAN_ID > 0)
            {
                var donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
                m.TenDonViBoPhan = donViBoPhan != null ? donViBoPhan.TEN : "";
            }
            if (m.DON_VI_ID > 0)
            {
                var donVi = _donViService.GetDonViById(m.DON_VI_ID.Value);
                m.TenDonVi = donVi != null ? donVi.TEN : "";
            }
            if (m.LY_DO_BIEN_DONG_ID.HasValue && m.LY_DO_BIEN_DONG_ID > 0)
            {
                var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(m.LY_DO_BIEN_DONG_ID ?? 0);
                m.TenLyDoBienDong = lyDoBienDong.TEN;
                m.TenLoaiLyDoBienDong = _nhanhienthiService.GetGiaTriEnum(lyDoBienDong.loaiLyDoTangGiam);
                m.MaLyDoBienDong = lyDoBienDong.MA;
            }

            if (m.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                m.TenLoaiTaiSan = loaiTSVH != null ? loaiTSVH.TEN : null;
            }
            else
            {
                var loaiTS = _loaiTaiSanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
                m.TenLoaiTaiSan = loaiTS != null ? loaiTS.TEN : "";
            }

            m.NguyenGiaVNStringNumber = m.NGUYEN_GIA.ToVNStringNumber();
            if (m.TAI_SAN_ID > 0)
            {
                var taisan = _taiSanService.GetTaiSanById(m.TAI_SAN_ID);
                m.TaiSanGuid = taisan.GUID;
                m.TAI_SAN_TRANG_THAI_ID = taisan.TRANG_THAI_ID ?? 0;
                ///Không cho phép đơn vị thay đổi thông tin trước đối chiếu
                // check xem có được duyệt không nếu là tài sản nguồn từ dkts sang
                m.IsDisableTSDKTS = IsDisableTSDKTS(taisan.NGUON_TAI_SAN_ID);
            }
            m.SoYCTruocChuaDuyet = _yeuCauService.CountYeuCauCuaTaiSan(m.TAI_SAN_ID, new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET, (int)enumTRANG_THAI_YEU_CAU.TU_CHOI }, null, m.NGAY_BIEN_DONG);
            return m;
        }
        private bool IsDisableTSDKTS(decimal nguonTS)
        {
            try
            {
                var TenCauHinh = "cauhinhchung.ChoPhepThayDoiThongTinDKTS";
                if (nguonTS != (int)enumNguonTaiSan.DKTS40)
                {
                    return false;
                }
                var cauHinh = _cauHinhService.SearchCauHinhs(Ten: TenCauHinh).FirstOrDefault();
                return !(bool.Parse(cauHinh.GIA_TRI));
            }
            catch (Exception)
            {

                return false;
            }


        }
        public virtual TrungGianBDYCListModel PrepareListGiamBanThanhLy(TrungGianBDYCSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchYCBDThanhLyChuaCapNhatTien(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, keySearch: searchModel.KeySearch, fromdate: searchModel.FromDate, todate: searchModel.ToDate, loaiHinhTSId: searchModel.LOAI_HINH_TAI_SAN_ID, loaiTSId: searchModel.LOAI_TAI_SAN_ID, chungTuSo: searchModel.CHUNG_TU_SO, nguoiTaoId: searchModel.NGUOI_TAO_ID, boPhanId: searchModel.DON_VI_BO_PHAN_ID, donViId: searchModel.DON_VI_ID, loaiBienDongId: searchModel.LOAI_LY_DO_BD_ID, lyDoBienDongId: searchModel.LY_DO_BIEN_DONG_ID, taisanId: searchModel.taisanId, isIgnoreTraLai: searchModel.isIgnoreTraLai, trangThaiId: searchModel.TRANG_THAI_ID, quyetDinhSo: searchModel.QUYET_DINH_SO, isDuyet: searchModel.isDuyet);

            //prepare list model
            var model = new TrungGianBDYCListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareModelForList(c, searchModel.Page)).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public virtual TrungGianBDYCModel PrepareBienDongModelGiamTaiSan(TrungGianBDYCModel model, TrungGianBDYC item)
        {
            model.TrungGianBDYCChiTietModel = new TrungGianBDYCChiTietModel();
            if (item != null)
            {
                model = item.ToModel<TrungGianBDYCModel>();
                if (model.EnumBDorYC == enumBDorYC.BIEN_DONG)
                {
                    var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(item.ID);
                    var trungGianCT = new TrungGianBDYCChiTiet(bdct);
                    model.TrungGianBDYCChiTietModel = trungGianCT.ToModel<TrungGianBDYCChiTietModel>();
                    model.HINH_THUC_XU_LY_ID = trungGianCT.HINH_THUC_XU_LY_ID;
                    model.DonViNhanDieuChuyenId = trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID;
                    if (trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
                    {
                        var dvNhanDieuChuyen = _donViService.GetDonViById(trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0);
                        model.DonViNhanDieuChuyenTen = dvNhanDieuChuyen.TEN;
                    }
                }
                else if (model.EnumBDorYC == enumBDorYC.YEU_CAU)
                {
                    var ycct = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(item.ID);
                    var trungGianCT = new TrungGianBDYCChiTiet(ycct);
                    model.TrungGianBDYCChiTietModel = trungGianCT.ToModel<TrungGianBDYCChiTietModel>();
                    model.HINH_THUC_XU_LY_ID = trungGianCT.HINH_THUC_XU_LY_ID;
                    model.DonViNhanDieuChuyenId = trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID;
                    if (trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID > 0)
                    {
                        var dvNhanDieuChuyen = _donViService.GetDonViById(trungGianCT.DON_VI_NHAN_DIEU_CHUYEN_ID ?? 0);
                        model.DonViNhanDieuChuyenTen = dvNhanDieuChuyen.TEN;
                    }
                }


            }
            var ts = _taiSanService.GetTaiSanById(model.TAI_SAN_ID);
            model.LOAI_HINH_TAI_SAN_ID = ts.LOAI_HINH_TAI_SAN_ID;
            model.LOAI_TAI_SAN_ID = ts.LOAI_TAI_SAN_ID;
            model.TenDonVi = ts.donvi.TEN;
            model.TAI_SAN_MA = ts.MA;
            model.TAI_SAN_TEN = ts.TEN;
            if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                model.TenLoaiTaiSan = ts.loaitaisan.TEN;
            else
            {
                var loaiTSVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                if (loaiTSVoHinh != null)
                    model.TenLoaiTaiSan = loaiTSVoHinh.TEN;

            }
            if (ts.DON_VI_BO_PHAN_ID > 0)
            {
                model.TenDonViBoPhan = ts.donvibophan.TEN;
            }
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
            {
                //int[] hinhThucLoaiBo = { (int)enumHINH_THUC_XU_LY_TSC.PHA_DO_HUY_BO };
                //model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList(valuesToExclude: hinhThucLoaiBo);
                model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList();
            }
            else
                model.ddlPhuongAnXuLy = ((enumHINH_THUC_XU_LY_TSC)model.enumPhuongAnXuLy).ToSelectList();
            model.LyDoTangAvailable = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: model.LOAI_BIEN_DONG_ID, isAddFirst: true, valSelected: model.LY_DO_BIEN_DONG_ID);
            model.TrungGianBDYCChiTietModel.NGUYEN_GIA_CU = _bienDongService.TinhNguyenGiaTaiSanOnlyBD(taiSanId: model.TAI_SAN_ID, To_Date_BienDong: model.NGAY_BIEN_DONG ?? DateTime.Now);
            return model;
        }
        #endregion YeuCau
    }
}