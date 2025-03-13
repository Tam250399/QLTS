//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.DB;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Services.SHTD;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.Common;
using GS.Core.Domain.Api;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.NghiepVu;
using System.IO;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.FileProviders;
using System.Text;
using GS.Core.Domain.Api.DanhMuc;

namespace GS.Services.DB
{
    public partial class DongBoServices : IDongBoServices
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DBTaiSan> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IXuLyService _xuLyService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDonViService _donViService;
        private readonly ITaiSanService _taiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly INhanXeService _nhanXeService;
        private readonly IChucDanhService _chucDanhService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IHinhThucXuLyService _hinhThucXuLyServiceService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IGSFileProvider _fileProvider;
        #endregion

        #region Ctor

        public DongBoServices(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DBTaiSan> itemRepository,
            IWorkContext workContext,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            ITaiSanTdService taiSanTdService,
            IXuLyService xuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ILoaiTaiSanService loaiTaiSanService,
            IQuocGiaService quocGiaService,
            ILyDoBienDongService lyDoBienDongService,
            IDonViService donViService,
            ITaiSanService taiSanService,
            IDiaBanService diaBanService,
            INhanXeService nhanXeService,
            IChucDanhService chucDanhService,
            ITaiSanClnService taiSanClnService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanVktService taiSanVktService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanDatService taiSanDatService,
            IDonViBoPhanService donViBoPhanService,
            INguoiDungService nguoiDungService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IMucDichSuDungService mucDichSuDungService,
            IHinhThucXuLyService hinhThucXuLyServiceService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IPhuongAnXuLyService phuongAnXuLyService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IGSFileProvider fileProvider
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._xuLyService = xuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._quocGiaService = quocGiaService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._donViService = donViService;
            this._taiSanService = taiSanService;
            this._diaBanService = diaBanService;
            this._nhanXeService = nhanXeService;
            this._chucDanhService = chucDanhService;
            this._taiSanClnService = taiSanClnService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanDatService = taiSanDatService;
            this._donViBoPhanService = donViBoPhanService;
            this._nguoiDungService = nguoiDungService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._yeuCauService = yeuCauService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._hinhThucXuLyServiceService = hinhThucXuLyServiceService;
            this._bienDongService = bienDongService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._fileProvider = fileProvider;
        }

        #endregion

        #region Methods
        public Kho_TaiSan_BienDong PrepareBienDongDongBo(DongBoApi_BienDongTaiSan item, decimal nguonId)
        {
            try
            {
                Kho_TaiSan_BienDong bienDong = new Kho_TaiSan_BienDong();
                bienDong.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                bienDong.assetCode = item.MA_TAI_SAN_DB;
                bienDong.syncSourceAssetId = item.MA_TAI_SAN;
                if (!string.IsNullOrEmpty(item.NGAY_BIEN_DONG))
                {
                    bienDong.mutationDate = item.NGAY_BIEN_DONG;
                }
                if (item.DB_LY_DO_BIEN_DONG_ID.HasValue)
                    bienDong.mutationCauseId = (int)item.DB_LY_DO_BIEN_DONG_ID.Value;
                bienDong.approvedDate = item.NGAY_DUYET;
                bienDong.approverName = item.NGUOI_DUYET;
                bienDong.departmentId = (int?)item.DB_DON_VI_BO_PHAN_ID;
                bienDong.documentValue = (long?)item.GIA_MUA_TIEP_NHAN;
                //bienDong.amortizationRate = 0;
                decimal NguyenGiaTruocBienDong = 0;
                if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                    NguyenGiaTruocBienDong = (decimal)_bienDongService.ProcTinhNguyenGia(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"))?.NGUYEN_GIA.GetValueOrDefault(0);
                GiaTriTaiSan giatriCu = new GiaTriTaiSan();
                if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    giatriCu = _bienDongService.ProcTinhGiaTriTaiSan(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"), item.ID);
                    if (giatriCu == null)
                        return null;
                    bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
                }
                bienDong.originalValueOld = (long?)NguyenGiaTruocBienDong;
                switch (item.LOAI_BIEN_DONG_ID)
                {
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangMoi;
                            bienDong.originalValue = (long)(NguyenGiaTruocBienDong + (item.NGUYEN_GIA.GetValueOrDefault(0)));
                            bienDong.originalValueOld = 0;
                            bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
                            break;
                        }
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangNguyenGia;
                            bienDong.originalValue = (long)(NguyenGiaTruocBienDong + Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
                            bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
                            break;
                        }
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.GiamNguyenGia;
                            bienDong.originalValue = (long)(NguyenGiaTruocBienDong - Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
                            bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
                            break;
                        }
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin;
                            bienDong.originalValue = (long)NguyenGiaTruocBienDong;
                            bienDong.changeReason = item.TEN_LY_DO;
                            bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
                            break;
                        }
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.DieuChuyen;
                            bienDong.transferUnitCode = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
                            bienDong.transferUnitId = (int?)item.DON_VI_NHAN_DIEU_CHUYEN_DB_ID;
                            bienDong.transferUnitName = item.DON_VI_NHAN_DIEU_CHUYEN_TEN;
                            bienDong.originalValue = (long)(NguyenGiaTruocBienDong - Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
                            bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);

                            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
                            break;
                        }
                    case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
                        {
                            bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.Giam;
                            bienDong.originalValue = 0;
                            bienDong.originalValueIncreasement = (long)NguyenGiaTruocBienDong;
                            if (item.LY_DO_BIEN_DONG_MA == enum_LY_DO_BIEN_DONG.DIEU_CHUYEN)
                            {
                                bienDong.transferUnitCode = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
                                bienDong.transferUnitId = (int?)item.DON_VI_NHAN_DIEU_CHUYEN_DB_ID;
                                bienDong.transferUnitName = item.DON_VI_NHAN_DIEU_CHUYEN_TEN;
                            }
                            bienDong.amortizationAccumulatedValue = (Double?)((Double)NguyenGiaTruocBienDong - bienDong.residualValueOld);
                            break;
                        }
                    default:
                        break;
                }
                bienDong.inputDate = item.NGAY_NHAP_TAI_SAN;
                bienDong.name = item.TEN_TAI_SAN_BD ?? item.TEN_TAI_SAN;
                bienDong.syncSourceId = item.GUID.ToString();
                bienDong.projectId = (long?)item.DB_DU_AN_ID;
                bienDong.unitId = (int)item.DB_DON_VI_ID.Value;
                bienDong.unitName = item.TEN_DON_VI;
                bienDong.assetTypeId = (int)item.DB_LOAI_TAI_SAN_ID.Value;
                bienDong.dateOfUse = item.NGAY_SU_DUNG;
                bienDong.usagePurposeId = (long?)item.DB_MUC_DICH_SU_DUNG_ID;
                if (nguonId != (decimal)(enumNguonTaiSan.QLTSC))
                    if (item.DB_DON_VI_ID.HasValue)
                        bienDong.syncUnitId = (int)item.DB_DON_VI_ID;
                    else return null;
                else
                    bienDong.syncUnitId = 1;//khoTaiSan.syncUnitId;
                bienDong.documentDecisionDate = item.QUYET_DINH_NGAY;
                bienDong.documentDecisionNumber = item.QUYET_DINH_SO;
                bienDong.amortizationRate = (float?)item.HM_TY_LE_TAI_SAN;
                bienDong.documentReceipt = item.CHUNG_TU_SO;
                bienDong.documentDateReceipt = item.CHUNG_TU_NGAY;
                bienDong.procurementForm = item.HINH_THUC_MUA_SAM_TEN;
                bienDong.notes = item.GHI_CHU;
                bienDong.countryOfOrigin = item.NUOC_SAN_XUAT;
                bienDong.plantYear = (int?)item.NAM_SAN_XUAT;
                bienDong.residualValue = (long)item.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0);
                if (item.IS_BAN_THANH_LY.GetValueOrDefault(0) == 1)
                {
                    bienDong.assetHandlingFormSale = true;
                    bienDong.assetHandlingFormValueObtained = (long)item.PHI_THU.GetValueOrDefault(0);
                }

                if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                        item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                    {
                        bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
                        bienDong.landAreaOld = (double?)giatriCu.DAT_TONG_DIEN_TICH_CU;
                    }
                    bienDong.landUseRightValue = (long?)item.NGUYEN_GIA_BAN_DAU;//(long?)item.DAT_GIA_TRI_QUYEN_SD_DAT;
                    bienDong.landAddress = item.TEN_TAI_SAN_BD ?? item.TEN_TAI_SAN;
                    if (bienDong.landAddress == null)
                        bienDong.landAddress = item.DIA_BAN_TEN;
                    bienDong.landRegionId = (int?)item.DB_DIA_BAN_ID;
                    bienDong.landCountryId = (long?)item.DB_QUOC_GIA_ID;
                    if (bienDong.landCountryId.GetValueOrDefault(0) == 0)
                    {
                        QuocGia quocGia = _quocGiaService.GetQuocGiaByMa("VN");
                        bienDong.landCountryId = quocGia != null ? quocGia.DB_ID : null;
                    }
                    if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
                    {
                        bienDong.landArea = bienDong.landAreaOld.GetValueOrDefault(0) - (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                        bienDong.landAreaIncreasement = (double)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
                    {
                        bienDong.landArea = bienDong.landAreaOld.GetValueOrDefault(0) + (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                        bienDong.landAreaIncreasement = (double)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                    {
                        bienDong.landAreaIncreasement = bienDong.landAreaOld;
                        bienDong.landArea = 0;
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
                    {
                        bienDong.landAreaIncreasement = 0;
                        bienDong.landArea = bienDong.landAreaOld;
                    }
                    //else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                    //{
                    //    bienDong.landAreaIncreasement = 0;
                    //}               

                    #region Hồ sơ giấy tờ
                    bienDong.landDocumentLandUseRight = item.HS_CNQSD_SO;
                    if (!string.IsNullOrEmpty(item.HS_CNQSD_NGAY))
                    {
                        bienDong.landDocumentDateLandUseRightDate = item.HS_CNQSD_NGAY;
                    }
                    bienDong.landDocumentLandAllocate = item.HS_QUYET_DINH_GIAO_SO;
                    if (!string.IsNullOrEmpty(item.HS_QUYET_DINH_GIAO_NGAY))
                    {
                        bienDong.landDocumentDateLandAllocate = item.HS_QUYET_DINH_GIAO_NGAY;
                    }
                    bienDong.documentLeaseContract = item.HS_HOP_DONG_CHO_THUE_SO;
                    if (!string.IsNullOrEmpty(item.HS_HOP_DONG_CHO_THUE_NGAY))
                        bienDong.documentDateLeaseContract = item.HS_HOP_DONG_CHO_THUE_NGAY;
                    bienDong.documentValue = (long?)item.GIA_MUA_TIEP_NHAN;
                    bienDong.isTaxExemption = item.MIEN_THUE_SO_TIEN.GetValueOrDefault(0) > 0 ? true : false;
                    bienDong.taxExamptionValue = (long?)item.MIEN_THUE_SO_TIEN;
                    bienDong.originalValueDepreciation = (long?)item.KH_GIA_TRI_TRICH_THANG;

                    bienDong.procurementFormId = (int?)item.HINH_THUC_MUA_SAM_DB_ID;
                    bienDong.landDocumentLandLease = item.HS_QUYET_DINH_CHO_THUE_SO;
                    if (!string.IsNullOrEmpty(item.HS_QUYET_DINH_CHO_THUE_NGAY))
                    {
                        bienDong.landDocumentDateLandLease = item.HS_QUYET_DINH_CHO_THUE_NGAY;
                    }
                    bienDong.documentOther = item.HS_PHAP_LY_KHAC;
                    #endregion

                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                        item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                    {
                        bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(giatriCu.NHA_TONG_DIEN_TICH_XD_CU.GetValueOrDefault(0));

                        bienDong.houseAreaFloorOld = (double)giatriCu.NHA_TONG_DIEN_TICH_XD_CU;
                    }
                    else
                    {
                        bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                        bienDong.houseAreaFloorOld = 0;
                    }
                    bienDong.houseAddress = item.DIA_CHI;
                    bienDong.houseAreaBuilding = (double?)item.TS_NHA_DIEN_TICH_XAY_DUNG.GetValueOrDefault(0);

                    bienDong.houseNumberOfFloor = (long)item.NHA_SO_TANG.GetValueOrDefault(0);
                    bienDong.houseBuiltYear = (int?)item.NHA_NAM_XAY_DUNG;
                    bienDong.houseLandCode = item.TS_NHA_TAI_SAN_DAT_MA;

                    if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
                    {
                        bienDong.houseAreaFloor = bienDong.houseAreaFloorOld - (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
                        bienDong.houseAreaFloorIncreasement = (double)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
                    {
                        bienDong.houseAreaFloor = bienDong.houseAreaFloorOld + (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
                        bienDong.houseAreaFloorIncreasement = (double)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                    {
                        bienDong.houseAreaFloorIncreasement = bienDong.houseAreaFloorOld;
                        bienDong.houseAreaFloor = 0;
                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
                    {
                        bienDong.houseAreaFloorIncreasement = 0;
                        bienDong.houseAreaFloor = bienDong.houseAreaFloorOld;
                    }
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.OTO || item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
                {
                    bienDong.vehicleRegistrationPlateNumber = item.OTO_BIEN_KIEM_SOAT;
                    bienDong.enginePower = (double?)item.OTO_CONG_XUAT;
                    if (item.OTO_XI_LANH.HasValue)
                        bienDong.motorCylinder = item.OTO_XI_LANH.GetValueOrDefault(0).ToString();
                    else if (item.TS_OTO_DUNG_TICH.HasValue)
                    {
                        bienDong.motorCylinder = item.TS_OTO_DUNG_TICH.GetValueOrDefault(0).ToString();
                    }
                    bienDong.vehicleSize = (long?)item.OTO_SO_CHO_NGOI;

                    bienDong.vehicleCapacity = (long)item.OTO_TAI_TRONG.GetValueOrDefault(0);

                    bienDong.vehicleChassisNumber = item.TS_OTO_SO_MAY;
                    bienDong.vehicleEngineNumber = item.TS_OTO_SO_KHUNG;
                    bienDong.brandName = item.OTO_NHAN_XE_TEN;
                    bienDong.vehicleUserTitle = item.TS_OTO_CHUC_DANH_TEN;
                    bienDong.vehicleUserTitleId = (int?)item.OTO_CHUC_DANH_DB_ID;
                    bienDong.brandId = (int?)item.OTO_NHAN_XE_DB_ID;
                    bienDong.vehicleNumberOfWheelDrives = (int?)item.TS_OTO_SO_CAU;
                    bienDong.vehicleRegistrationDocumentNumber = item.TS_OTO_GCN_DANG_KY;
                    bienDong.vehicleRegistrationIssuedAuthority = item.TS_OTO_CO_QUAN_CAP_DANG_KY;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
                {
                    if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                     item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                    {
                        bienDong.volumeIncreasement = (double)Math.Abs(item.VKT_THE_TICH.GetValueOrDefault(giatriCu.VKT_THE_TICH_CU.GetValueOrDefault(0)));
                        bienDong.lengthIncreasement = (double)Math.Abs(item.VKT_CHIEU_DAI.GetValueOrDefault(giatriCu.VKT_CHIEU_DAI_CU.GetValueOrDefault(0)));
                        bienDong.volumeOld = (double?)giatriCu.VKT_THE_TICH_CU;
                        bienDong.lengthOld = (double?)giatriCu.VKT_CHIEU_DAI_CU;
                        bienDong.landAreaOld = (double?)giatriCu.VKT_DIEN_TICH_CU;
                    }
                    else
                    {
                        bienDong.volumeIncreasement = (double)Math.Abs(item.VKT_THE_TICH.GetValueOrDefault(0));
                        bienDong.lengthIncreasement = (double)Math.Abs(item.VKT_CHIEU_DAI.GetValueOrDefault(0));
                        bienDong.landAreaIncreasement = (double)Math.Abs(item.VKT_DIEN_TICH.GetValueOrDefault(0));
                        bienDong.landAreaOld = 0;
                        bienDong.volumeOld = 0;
                        bienDong.lengthOld = 0;
                    }
                    if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                    {
                        bienDong.volumeIncreasement = bienDong.volumeOld;
                        bienDong.volume = 0;
                        bienDong.lengthIncreasement = bienDong.lengthOld;
                        bienDong.length = 0;
                        bienDong.landAreaIncreasement = bienDong.landAreaOld;
                        bienDong.landArea = 0;

                    }
                    else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
                    {
                        bienDong.volumeIncreasement = 0;
                        bienDong.volume = (float?)bienDong.volumeOld;
                        bienDong.lengthIncreasement = 0;
                        bienDong.length = (float?)bienDong.lengthOld;
                        bienDong.landAreaIncreasement = 0;
                        bienDong.landArea = (float?)bienDong.landAreaOld;
                    }
                    bienDong.volume = (float?)item.VKT_THE_TICH;
                    bienDong.length = (float?)item.TS_VKT_CHIEU_DAI;
                    bienDong.landArea = (double?)item.TS_VKT_DIEN_TICH;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI)
                {
                    bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV)
                {
                    bienDong.plantYear = (int)item.TS_CLN_NAM_SINH;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
                {
                    bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
                {
                    bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
                }
                else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
                }
                #region Hiện trạng sử dụng
                Kho_assetMutationAssetUsageStates khoHienTrang = new Kho_assetMutationAssetUsageStates();
                if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA || item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    if (item.LST_HIEN_TRANG != null && item.LST_HIEN_TRANG.Count > 0)
                    {
                        bienDong.assetMutationAssetUsageStates = item.LST_HIEN_TRANG.Where(c => c.GIA_TRI_NUMBER > 0).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HIEN_TRANG_DB_ID, usageValue = (double)x.GIA_TRI_NUMBER }).ToList();
                    }
                    else
                    {
                        List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID.GetValueOrDefault(0));
                        bienDong.assetMutationAssetUsageStates = ListHienTrang.Where(c => c.GIA_TRI_NUMBER > 0).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HienTrang.DB_ID, usageValue = (double)x.GIA_TRI_NUMBER }).ToList();
                    }
                }
                else
                {
                    if (item.LST_HIEN_TRANG != null && item.LST_HIEN_TRANG.Count > 0)
                    {
                        bienDong.assetMutationAssetUsageStates = item.LST_HIEN_TRANG.Where(c => c.GIA_TRI_CHECKBOX.HasValue && c.GIA_TRI_CHECKBOX == 1).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HIEN_TRANG_DB_ID, usageValue = 1 }).ToList();
                    }
                    else
                    {
                        List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID.GetValueOrDefault(0));
                        bienDong.assetMutationAssetUsageStates = ListHienTrang.Where(c => c.GIA_TRI_CHECKBOX.HasValue && c.GIA_TRI_CHECKBOX == true).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HienTrang.DB_ID, usageValue = 1 }).ToList();
                    }
                }
                #endregion
                #region Nguồn vốn
                bienDong.originalValueSourceStateBudgetIncreasement = (long)Math.Abs(item.NV_NGAN_SACH.GetValueOrDefault(0));
                bienDong.originalValueSourceOtherIncreasement = (long)Math.Abs(item.NV_NGUON_KHAC.GetValueOrDefault(0));
                bienDong.originalValueSourceBusinessIncreasement = (long)Math.Abs(item.NV_HDSN.GetValueOrDefault(0));
                bienDong.originalValueSourceOdaIncreasement = (long)Math.Abs(item.NV_VIEN_TRO.GetValueOrDefault(0));

                GiaTriNguonVon giaTriNguonVon = _bienDongService.ProcTinhGiaTriNguonVon(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"));
                if (giaTriNguonVon == null)
                    return null;
                bienDong.originalValueSourceBusiness = (long)giaTriNguonVon.NEW_NGUON_VON_SU_NGHIEP;
                bienDong.originalValueSourceStateBudget = (long)giaTriNguonVon.NEW_NGUON_VON_NS;
                bienDong.originalValueSourceOther = (long)giaTriNguonVon.NEW_NGUON_VON_KHAC;
                bienDong.originalValueSourceOda = (long)giaTriNguonVon.NEW_NGUON_VON_VIEN_TRO;

                ///Ngồn cũ
                bienDong.originalValueSourceBusinessOld = (long)giaTriNguonVon.OLD_NGUON_VON_SU_NGHIEP;
                bienDong.originalValueSourceStateBudgetOld = (long)giaTriNguonVon.OLD_NGUON_VON_NS;
                bienDong.originalValueSourceOtherOld = (long)giaTriNguonVon.OLD_NGUON_VON_KHAC;
                bienDong.originalValueSourceOdaOld = (long)giaTriNguonVon.OLD_NGUON_VON_VIEN_TRO;


                #endregion
                return bienDong;
            }
            catch (Exception ex)
            {
                //var st = new StackTrace(ex, true);
                //var frame = st.GetFrame(0);
                //var line = frame.GetFileLineNumber();
                //_hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Lỗi đồng bộ biến động dòng {line}", item, "BienDong");
                return null;
            }



        }
        /// <summary>
        /// bỏ các trường null trong  body gửi sang kho
        /// do bên kho check đủ các trường ko để thừa
        /// các service đang dùng chung một model
        /// </summary>
        /// <param name="kho_DongBoTaiSan"></param>
        /// <param name="LoaiBienDongId"></param>
        /// <returns></returns>
        public Kho_DongBoTaiSan ConfigDataByLoaiHinhTaiSan(Kho_DongBoTaiSan kho_DongBoTaiSan, decimal LoaiBienDongId)
        {
            switch (LoaiBienDongId)
            {
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY:
                    {

                        foreach (var bd in kho_DongBoTaiSan.data)
                        {
                            //bd.originalValueSourceBorrowIncreasement = null;
                            //bd.originalValueSourceBusinessIncreasement = null;
                            //bd.originalValueSourceOdaIncreasement = null;
                            //bd.originalValueSourceOtherIncreasement = null;
                            //bd.originalValueSourceStateBudgetIncreasement = null;
                            //bd.originalValueIncreasement = null;
                            //bd.originalValueSourceStateBudgetOld = null;
                            //bd.originalValueSourceOdaOld = null;
                            //bd.originalValueSourceBusinessOld = null;
                            //bd.originalValueSourceBorrowOld = null;
                            //bd.originalValueSourceOtherOld = null;

                            bd.landAreaIncreasement = null;
                            bd.landAreaOld = null;
                            bd.houseAreaFloorIncreasement = null;
                            bd.houseAreaFloorOld = null;
                            bd.residualValue = bd.residualValue < 0 ? 0 : bd.residualValue;
                            bd.amortizationAccumulatedValue = Math.Abs(bd.amortizationAccumulatedValue.GetValueOrDefault(0));
                            #region DAT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                            {
                                bd.landArea = Math.Abs(bd.landArea.GetValueOrDefault(0));
                                bd.landCountryId = bd.landCountryId.GetValueOrDefault(0) == 0 ? null : bd.landCountryId;
                                bd.landRegionId = bd.landRegionId.GetValueOrDefault(0) == 0 ? null : bd.landRegionId;
                                bd.landAddress = bd.landAddress ?? "Việt Nam";
                            }
                            else
                            {
                                bd.landArea = null;
                                bd.landCountryId = null;
                                bd.landRegionId = null;
                                bd.landAddress = null;
                                bd.landUseRightValue = null;
                                bd.landDocumentLandUseRight = null;
                                bd.landDocumentDateLandUseRightDate = null;
                                bd.landDocumentLandAllocate = null;
                                bd.landDocumentDateLandAllocate = null;
                                bd.landDocumentLandUseRightTransfer = null;
                                bd.landDocumentDateLandUseRightTransfer = null;
                                bd.landDocumentLandLease = null;
                                bd.landDocumentDateLandLease = null;
                                bd.landDocumentLandUseRightEvaluation = null;
                                bd.landDocumentDateLandUseRightEvaluation = null;
                            }
                            #endregion

                            #region NHA
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                            {
                                bd.houseBuiltYear = bd.houseBuiltYear ?? 0;
                                bd.houseNumberOfFloor = bd.houseNumberOfFloor ?? 0;
                                bd.houseAreaFloor = bd.houseAreaFloor ?? 0;

                            }
                            else
                            {
                                bd.houseAddress = null;
                                bd.houseBuiltYear = null;
                                bd.houseNumberOfFloor = null;
                                bd.houseAreaBuilding = null;
                                bd.houseAreaFloor = null;
                                bd.houseLandCode = null;
                            }
                            #endregion

                            #region PTVT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                || bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                                bd.vehicleRegistrationPlateNumber = bd.vehicleRegistrationPlateNumber ?? "";
                                bd.vehicleCapacity = bd.vehicleCapacity ?? 0;

                            }
                            else
                            {
                                bd.vehicleChassisNumber = null;
                                bd.vehicleEngineNumber = null;
                                bd.vehicleRegistrationPlateNumber = null;
                                bd.vehicleSize = null;
                                bd.vehicleCapacity = null;
                                bd.countryOfOrigin = null;
                                bd.brandName = null;
                                bd.specifications = null;
                                bd.serialNumber = null;
                                bd.supplier = null;
                                bd.enginePower = null;
                                bd.motorCylinder = null;
                                bd.vehicleUserTitle = null;
                                bd.vehicleNumberOfWheelDrives = null;
                                bd.vehicleRegistrationDocumentNumber = null;
                                bd.vehicleRegistrationIssuedAuthority = null;
                            }
                            #endregion

                            #region KHAC
                            if (bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                            }
                            else
                            {
                                bd.volume = null;
                                bd.length = null;
                            }
                            #endregion
                            bd.LOAI_HINH_TAI_SAN_ID = null;
                        }

                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                    {

                        foreach (var bd in kho_DongBoTaiSan.data)
                        {
                            bd.usagePurposeId = null;
                            bd.originalValueSourceBorrow = Math.Abs(bd.originalValueSourceBorrow);
                            bd.originalValueSourceBorrowIncreasement = Math.Abs(bd.originalValueSourceBorrowIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceBusiness = Math.Abs(bd.originalValueSourceBusiness);
                            bd.originalValueSourceBusinessIncreasement = Math.Abs(bd.originalValueSourceBusinessIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceOda = Math.Abs(bd.originalValueSourceOda);
                            bd.originalValueSourceOdaIncreasement = Math.Abs(bd.originalValueSourceOdaIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceOther = Math.Abs(bd.originalValueSourceOther);
                            bd.originalValueSourceOtherIncreasement = Math.Abs(bd.originalValueSourceOtherIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceStateBudget = Math.Abs(bd.originalValueSourceStateBudget);
                            bd.originalValueSourceStateBudgetIncreasement = Math.Abs(bd.originalValueSourceStateBudgetIncreasement.GetValueOrDefault(0));
                            bd.originalValueIncreasement = Math.Abs(bd.originalValueIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceBorrowOld = 0;
                            bd.amortizationAccumulatedValue = Math.Abs(bd.amortizationAccumulatedValue.GetValueOrDefault(0));
                            bd.originalValue = Math.Abs(bd.originalValue);
                            bd.originalValueSourceStateBudget = Math.Abs(bd.originalValueSourceStateBudget);

                            // bd.dateOfUse = null;
                            //bd.landDocumentDateLandAllocate = null;
                            //bd.landDocumentDateLandLease = null;
                            //bd.landDocumentDateLandUseRightDate = null;
                            //bd.landDocumentLandUseRight = null;
                            //bd.landDocumentLandAllocate = null;
                            //bd.landDocumentLandLease = null;
                            //bd.vehicleRegistrationPlateNumber = null;
                            //bd.vehicleSize = null;
                            //bd.vehicleCapacity = null;
                            #region DAT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                            {
                                bd.landArea = Math.Abs(bd.landArea.GetValueOrDefault(0));
                                bd.landCountryId = bd.landCountryId.GetValueOrDefault(0) == 0 ? null : bd.landCountryId;
                                bd.landRegionId = bd.landRegionId.GetValueOrDefault(0) == 0 ? null : bd.landRegionId;
                                bd.landAreaOld = bd.landAreaOld ?? 0;
                                bd.landAreaIncreasement = bd.landAreaIncreasement ?? 0;// Math.Abs(bd.landAreaIncreasement.GetValueOrDefault(0));
                                bd.landAddress = bd.landAddress ?? "Việt Nam";

                            }
                            else
                            {
                                bd.landArea = null;
                                bd.landCountryId = null;
                                bd.landAreaOld = null;
                                bd.landAreaIncreasement = null;
                                bd.landRegionId = null;
                                bd.landAddress = null;
                                bd.landUseRightValue = null;
                                bd.landDocumentLandUseRight = null;
                                bd.landDocumentDateLandUseRightDate = null;
                                bd.landDocumentLandAllocate = null;
                                bd.landDocumentDateLandAllocate = null;
                                bd.landDocumentLandUseRightTransfer = null;
                                bd.landDocumentDateLandUseRightTransfer = null;
                                bd.landDocumentLandLease = null;
                                bd.landDocumentDateLandLease = null;
                                bd.landDocumentLandUseRightEvaluation = null;
                                bd.landDocumentDateLandUseRightEvaluation = null;
                            }
                            #endregion

                            #region NHA
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                            {
                                bd.houseBuiltYear = bd.houseBuiltYear ?? 0;
                                bd.houseNumberOfFloor = bd.houseNumberOfFloor ?? 0;
                                bd.houseAreaFloor = bd.houseAreaFloor ?? 0;
                                bd.houseAreaFloorIncreasement = Math.Abs(bd.houseAreaFloorIncreasement.GetValueOrDefault(0));
                                bd.houseAreaFloorOld = bd.houseAreaFloorOld ?? 0;
                            }
                            else
                            {
                                bd.houseAddress = null;
                                bd.houseBuiltYear = null;
                                bd.houseNumberOfFloor = null;
                                bd.houseAreaBuilding = null;
                                bd.houseAreaFloor = null;
                                bd.houseLandCode = null;
                                bd.houseAreaFloorIncreasement = null;
                                bd.houseAreaFloorOld = null;
                            }
                            #endregion

                            #region PTVT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                || bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                                bd.vehicleCapacity = bd.vehicleCapacity ?? 0;
                            }
                            else
                            {
                                bd.vehicleChassisNumber = null;
                                bd.vehicleEngineNumber = null;
                                bd.vehicleRegistrationPlateNumber = null;
                                bd.vehicleSize = null;
                                bd.vehicleCapacity = null;
                                bd.countryOfOrigin = null;
                                bd.brandName = null;
                                bd.specifications = null;
                                bd.serialNumber = null;
                                bd.supplier = null;
                                bd.enginePower = null;
                                bd.motorCylinder = null;
                                bd.vehicleUserTitle = null;
                                bd.vehicleNumberOfWheelDrives = null;
                                bd.vehicleRegistrationDocumentNumber = null;
                                bd.vehicleRegistrationIssuedAuthority = null;
                            }
                            #endregion

                            #region KHAC
                            if (bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                            }
                            else
                            {
                                bd.volume = null;
                                bd.length = null;
                            }
                            #endregion
                            bd.LOAI_HINH_TAI_SAN_ID = null;
                        }

                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                    {

                        foreach (var bd in kho_DongBoTaiSan.data)
                        {
                            bd.amortizationAccumulatedValue = Math.Abs(bd.amortizationAccumulatedValue.GetValueOrDefault(0));
                            #region DAT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                            {
                                bd.landArea = Math.Abs(bd.landArea.GetValueOrDefault(0));
                                bd.landCountryId = bd.landCountryId.GetValueOrDefault(0) == 0 ? null : bd.landCountryId;
                                bd.landRegionId = bd.landRegionId.GetValueOrDefault(0) == 0 ? null : bd.landRegionId;
                                bd.landAddress = bd.landAddress ?? "Việt Nam";
                            }
                            else
                            {
                                bd.landArea = null;
                                bd.landCountryId = null;
                                bd.landRegionId = null;
                                bd.landAddress = null;
                                bd.landUseRightValue = null;
                                bd.landDocumentLandUseRight = null;
                                bd.landDocumentDateLandUseRightDate = null;
                                bd.landDocumentLandAllocate = null;
                                bd.landDocumentDateLandAllocate = null;
                                bd.landDocumentLandUseRightTransfer = null;
                                bd.landDocumentDateLandUseRightTransfer = null;
                                bd.landDocumentLandLease = null;
                                bd.landDocumentDateLandLease = null;
                                bd.landDocumentLandUseRightEvaluation = null;
                                bd.landDocumentDateLandUseRightEvaluation = null;
                            }
                            #endregion

                            #region NHA
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                            {
                                bd.houseBuiltYear = bd.houseBuiltYear ?? 0;
                                bd.houseNumberOfFloor = bd.houseNumberOfFloor ?? 0;
                                bd.houseAreaFloor = bd.houseAreaFloor ?? 0;
                            }
                            else
                            {
                                bd.houseAddress = null;
                                bd.houseBuiltYear = null;
                                bd.houseNumberOfFloor = null;
                                bd.houseAreaBuilding = null;
                                bd.houseAreaFloor = null;
                                bd.houseLandCode = null;
                            }
                            #endregion

                            #region PTVT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                || bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                                bd.vehicleCapacity = bd.vehicleCapacity ?? 0;
                            }
                            else
                            {
                                bd.vehicleChassisNumber = null;
                                bd.vehicleEngineNumber = null;
                                bd.vehicleRegistrationPlateNumber = null;
                                bd.vehicleSize = null;
                                bd.vehicleCapacity = null;
                                bd.countryOfOrigin = null;
                                bd.brandName = null;
                                bd.specifications = null;
                                bd.serialNumber = null;
                                bd.supplier = null;
                                bd.enginePower = null;
                                bd.motorCylinder = null;
                                bd.vehicleUserTitle = null;
                                bd.vehicleNumberOfWheelDrives = null;
                                bd.vehicleRegistrationDocumentNumber = null;
                                bd.vehicleRegistrationIssuedAuthority = null;
                            }
                            #endregion

                            #region KHAC
                            if (bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                            }
                            else
                            {
                                bd.volume = null;
                                bd.length = null;
                            }
                            #endregion
                            bd.LOAI_HINH_TAI_SAN_ID = null;
                            bd.residualValue = bd.residualValue < 0 ? 0 : bd.residualValue;
                        }
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
                    {

                        foreach (var bd in kho_DongBoTaiSan.data)
                        {
                            bd.usagePurposeId = null;
                            //   bd.dateOfUse = null;
                            bd.originalValue = 0;

                            bd.originalValueSourceBorrowIncreasement = Math.Abs(bd.originalValueSourceBorrowIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceBusinessIncreasement = Math.Abs(bd.originalValueSourceBusinessIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceOdaIncreasement = Math.Abs(bd.originalValueSourceOdaIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceOtherIncreasement = Math.Abs(bd.originalValueSourceOtherIncreasement.GetValueOrDefault(0));
                            bd.originalValueSourceStateBudgetIncreasement = Math.Abs(bd.originalValueSourceStateBudgetIncreasement.GetValueOrDefault(0));
                            bd.originalValueIncreasement = Math.Abs(bd.originalValueIncreasement.GetValueOrDefault(0));

                            bd.originalValueSourceBorrow = 0;
                            bd.originalValueSourceBusiness = 0;
                            bd.originalValueSourceOda = 0;
                            bd.originalValueSourceOther = 0;
                            bd.amortizationAccumulatedValue = Math.Abs(bd.amortizationAccumulatedValue.GetValueOrDefault(0));
                            // bd.residualValue = null;
                            bd.residualValue = bd.residualValue < 0 ? 0 : bd.residualValue;
                            #region DAT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                            {
                                bd.landArea = Math.Abs(bd.landArea.GetValueOrDefault(0));
                                bd.landCountryId = bd.landCountryId.GetValueOrDefault(0) == 0 ? null : bd.landCountryId;
                                bd.landRegionId = bd.landRegionId.GetValueOrDefault(0) == 0 ? null : bd.landRegionId;
                                bd.landAddress = bd.landAddress ?? "Việt Nam";
                                bd.landUseRightValue = bd.landUseRightValue ?? 0;
                                bd.landAreaIncreasement = Math.Abs(bd.landAreaIncreasement.GetValueOrDefault(0));
                            }
                            else
                            {
                                bd.landArea = null;
                                bd.landCountryId = null;
                                bd.landRegionId = null;
                                bd.landAddress = null;
                                bd.landAreaIncreasement = null;
                                bd.landUseRightValue = null;
                                bd.landDocumentLandUseRight = null;
                                bd.landDocumentDateLandUseRightDate = null;
                                bd.landDocumentLandAllocate = null;
                                bd.landDocumentDateLandAllocate = null;
                                bd.landDocumentLandUseRightTransfer = null;
                                bd.landDocumentDateLandUseRightTransfer = null;
                                bd.landDocumentLandLease = null;
                                bd.landDocumentDateLandLease = null;
                                bd.landDocumentLandUseRightEvaluation = null;
                                bd.landDocumentDateLandUseRightEvaluation = null;
                            }
                            #endregion

                            #region NHA
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                            {
                                bd.houseBuiltYear = bd.houseBuiltYear ?? 0;
                                bd.houseNumberOfFloor = bd.houseNumberOfFloor ?? 0;
                                bd.houseAreaFloor = bd.houseAreaFloor ?? 0;
                                bd.houseAreaFloorIncreasement = Math.Abs(bd.houseAreaFloorIncreasement.GetValueOrDefault(0));
                                bd.houseAreaFloorOld = bd.houseAreaFloorOld ?? 0;
                            }
                            else
                            {
                                bd.houseAddress = null;
                                bd.houseBuiltYear = null;
                                bd.houseNumberOfFloor = null;
                                bd.houseAreaBuilding = null;
                                bd.houseAreaFloor = null;
                                bd.houseLandCode = null;
                                bd.houseAreaFloorIncreasement = null;
                                bd.houseAreaFloorOld = null;
                            }
                            #endregion

                            #region PTVT
                            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                || bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                                bd.vehicleCapacity = bd.vehicleCapacity ?? 0;
                            }
                            else
                            {
                                bd.vehicleChassisNumber = null;
                                bd.vehicleEngineNumber = null;
                                bd.vehicleRegistrationPlateNumber = null;
                                bd.vehicleSize = null;
                                bd.vehicleCapacity = null;
                                bd.countryOfOrigin = null;
                                bd.brandName = null;
                                bd.specifications = null;
                                bd.serialNumber = null;
                                bd.supplier = null;
                                bd.enginePower = null;
                                bd.motorCylinder = null;
                                bd.vehicleUserTitle = null;
                                bd.vehicleNumberOfWheelDrives = null;
                                bd.vehicleRegistrationDocumentNumber = null;
                                bd.vehicleRegistrationIssuedAuthority = null;
                            }
                            #endregion

                            #region KHAC
                            if (bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                && bd.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO)
                            {
                            }
                            else
                            {
                                bd.volume = null;
                                bd.length = null;
                            }
                            #endregion
                            bd.LOAI_HINH_TAI_SAN_ID = null;
                        }
                        break;
                    }
                default:
                    break;
            }
            foreach (var bd in kho_DongBoTaiSan.data)
            {
                bd.LOAI_HINH_TAI_SAN_ID = null;
            }
            return kho_DongBoTaiSan;
        }

        #region danh mục
        public Kho_DonVi_Api PrepareKhoDonVi(decimal id)
        {
            Kho_DonVi_Api kho_DonVi = new Kho_DonVi_Api();
            DonVi donVi = _donViService.GetDonViById(id);
            kho_DonVi.actionType = donVi.DB_ID > 0 ? (int)enumLoaiDongBo.CapNhat : (int)enumLoaiDongBo.ThemMoi;
            kho_DonVi.id = donVi.DB_ID;
            if (donVi.PARENT_ID > 0)
            {
                kho_DonVi.parentId = _donViService.GetDonViById(donVi.PARENT_ID.Value)?.DB_ID;
            }
            kho_DonVi.name = donVi.TEN;
            kho_DonVi.nationalBudgetCode = donVi.MA_DVQHNS;
            kho_DonVi.code = donVi.MA;
            kho_DonVi.unitLevelId = MapCapDonVi(donVi.CAP_DON_VI_ID, donVi.LOAI_CAP_DON_VI_ID.Value);
            //kho_DonVi.unitTypeId = (int?)m.DB_LOAI_DON_VI_ID;
            kho_DonVi.unitTypeId = (int?)donVi.LoaiDonVi.DB_ID;
            kho_DonVi.address = donVi.DIA_CHI;
            kho_DonVi.fax = donVi.FAX;
            kho_DonVi.accountingStandard = (donVi.CHE_DO_HACH_TOAN_ID > 0) ? (int)donVi.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
            kho_DonVi.isActive = donVi.TRANG_THAI_ID;
            kho_DonVi.syncId = donVi.ID.ToString();
            kho_DonVi.syncParentId = donVi.PARENT_ID > 0 ? ((int?)donVi.PARENT_ID).ToString() : null;
            kho_DonVi.phoneNumber = donVi.DIEN_THOAI;
            kho_DonVi.administrativeLevel = (int)donVi.LOAI_CAP_DON_VI_ID;
            if (donVi.LA_DON_VI_NHAP_LIEU != null)
            {
                kho_DonVi.functionalUnitType = donVi.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;
            }
            return kho_DonVi;
        }
        public int MapCapDonVi(decimal? CapDonViQLTS, decimal LoaiCapDonVi)
        {
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapTrungUong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.BoCoQuanTrungUong:
                        return 1;
                    //case (int)CapEnum.TongCuc:
                    //    return 2;
                    //case (int)CapEnum.Cuc:
                    //    return 3;
                    //case (int)CapEnum.ChiCuc:
                    //    return 4;
                    default:
                        return 1;
                }
            }
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapDiaPhuong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.Tinh:
                        return 1;
                    case (int)CapEnum.Huyen:
                        return 2;
                    case (int)CapEnum.Xa:
                        return 3;
                    default:
                        return 3;
                }
            }
            return 0;
        }
        #endregion danh mục

        #endregion
    }
}

