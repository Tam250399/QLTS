using DevExpress.Data;
using DevExpress.XtraRichEdit.Layout.Engine;
using GS.Core;
using GS.Core.Domain.Api;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.WebApi.Common;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Oracle.ManagedDataAccess.Client;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;
using GS.Core.Configuration;
using DevExpress.XtraRichEdit.Fields;

namespace GS.WebApi.Factories
{
    public class TaiSanSvcFactory : ITaiSanSvcFactory
    {
        #region Ctor
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly IDiaBanService _diaBanService;
        private readonly INhanXeService _nhanXeService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly ITaiSanService _taiSanService;
        private readonly IDBTaiSanService _dbTaiSanService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IXuLyService _xuLyService;
        private readonly IDonViService _donViService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IDbContext _dbContext;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IKhaiThacService _khaiThacService;
        private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;
        private readonly IHoatDongService _hoatDongService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IKetQuaService _ketQuaService;
        private readonly IThuChiService _thuChiService;
        private readonly IValidateFactory _validateFactory;
        private readonly ILogsDongBoCsdlqgService _logsDongBoCsdlqgService;
        private readonly GSConfig _gsConfig;
        public TaiSanSvcFactory(
            IYeuCauChiTietService yeuCauChiTietService,
            IYeuCauService yeuCauService,
            INhanXeService nhanXeService,
            IDiaBanService diaBanService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            IBienDongService bienDongService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanClnService taiSanClnService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanVktService taiSanVktService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanService taiSanService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanDatService taiSanDatService,
            IDBTaiSanService dbTaiSanService,
            ITaiSanNhatKyService taiSanNhatKyService,
             IQuyetDinhTichThuService quyetDinhTichThuService,
            ITaiSanTdService taiSanTdService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IDonViService donViService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IHinhThucXuLyService hinhThucXuLyService,
            ILoaiTaiSanService loaiTaiSanService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IDbContext dbContext,
            ILyDoBienDongService lyDoBienDongService,
            IDonViBoPhanService donViBoPhanService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IMucDichSuDungService mucDichSuDungService,
            IChucDanhService chucDanhService,
            IHienTrangService hienTrangService,
            IKhaiThacService khaiThacService,
            IKhaiThacTaiSanService khaiThacTaiSanService,
            IHoatDongService hoatDongService,
            INguoiDungService nguoiDungService,
            ILoaiTaiSanDonViServices loaiTaiSanDonViService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhauHaoTaiSanService khauHaoTaiSanService,
            ICauHinhService cauHinhService,
            IKetQuaService ketQuaService,
            IThuChiService thuChiService,
            IValidateFactory validateFactory,
            ILogsDongBoCsdlqgService logsDongBoCsdlqgService,
            GSConfig gSConfig
            )
        {
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._yeuCauService = yeuCauService;
            this._nhanXeService = nhanXeService;
            this._diaBanService = diaBanService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._bienDongService = bienDongService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._taiSanClnService = taiSanClnService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanDatService = taiSanDatService;
            this._dbTaiSanService = dbTaiSanService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._taiSanTdService = taiSanTdService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._hinhThucXuLyService = hinhThucXuLyService;
            this._donViService = donViService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._taiSanService = taiSanService;
            this._dbContext = dbContext;
            this._lyDoBienDongService = lyDoBienDongService;
            this._donViBoPhanService = donViBoPhanService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._chucDanhService = chucDanhService;
            this._hienTrangService = hienTrangService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._khaiThacService = khaiThacService;
            this._khaiThacTaiSanService = khaiThacTaiSanService;
            this._hoatDongService = hoatDongService;
            this._nguoiDungService = nguoiDungService;
            this._loaiTaiSanDonViService = loaiTaiSanDonViService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khauHaoTaiSanService = khauHaoTaiSanService;
            this._cauHinhService = cauHinhService;
            this._ketQuaService = ketQuaService;
            this._thuChiService = thuChiService;
            this._validateFactory = validateFactory;
            this._logsDongBoCsdlqgService = logsDongBoCsdlqgService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._gsConfig = gSConfig;
        }
        #endregion
        #region Class DB
        public class ItemDB
        {
            public string DB_ID { get; set; }
            public string ID { get; set; }
        }
        #endregion
        #region Method
        #region TaiSan
        public TaiSanDBModel PrepareTaiSanDBModel(DBTaiSanModel model)
        {
            TaiSanDBModel dbModel = new TaiSanDBModel()
            {
                MA = model.QLDKTS_MA,
                LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID,
            };
            TaiSan ts = new TaiSan();
            ts = _taiSanService.GetTaiSanByMa(model.QLDKTS_MA);
            if (ts != null)
            {
                switch (model.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        dbModel.TS_CLN = model.DATA_JSON.toEntity<TaiSanClnDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        dbModel.TS_DAT = model.DATA_JSON.toEntity<TaiSanDatDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        dbModel.TS_NHA = model.DATA_JSON.toEntity<TaiSanNhaDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        dbModel.TS_OTO = model.DATA_JSON.toEntity<TaiSanOtoDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                        dbModel.TS_OTO = model.DATA_JSON.toEntity<TaiSanOtoDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        dbModel.TS_VKT = model.DATA_JSON.toEntity<TaiSanVktDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                        dbModel.TS_VKT = model.DATA_JSON.toEntity<TaiSanVktDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                        dbModel.TS_VKT = model.DATA_JSON.toEntity<TaiSanVktDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        dbModel.TS_MAY_MOC = model.DATA_JSON.toEntity<TaiSanMayMocDBModel>();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        dbModel.TS_VO_HINH = model.DATA_JSON.toEntity<TaiSanVoHinhDBModel>();
                        break;
                }
                dbModel.MA_DON_VI = ts.donvi.MA;
                dbModel.NGAY_NHAP = ts.NGAY_NHAP.toStringApi();
                dbModel.NGAY_SU_DUNG = ts.NGAY_SU_DUNG.toStringApi();
            }
            return dbModel;
        }
        public ResultTaiSan GetAllTaiSans(int LoaiHinhTaiSanId = 0, int pageSize = int.MaxValue, int pageIndex = 0, int DonViId = 0)
        {
            if (LoaiHinhTaiSanId == 0)
                return null;
            var query = _taiSanService.DanhSachTaiSans(pageIndex: pageIndex, pageSize: pageSize, LOAI_HINH_TAI_SAN_ID: LoaiHinhTaiSanId, TRANG_THAI_ID: (int)enumTRANG_THAI_TAI_SAN.DA_DUYET, isDuyet: true, donviId: DonViId);
            ResultTaiSan resultTaiSan = new ResultTaiSan();
            List<TaiSanDBModel> lst = new List<TaiSanDBModel>();
            foreach (var itm in query)
            {
                var dbTaiSan = _dbTaiSanService.GetTaiSanById(itm.ID);
                DBTaiSanModel model = new DBTaiSanModel();
                if (dbTaiSan != null)
                    model = dbTaiSan.ToModel<DBTaiSanModel>();
                else
                    model = new DBTaiSanModel()
                    {
                        LOAI_HINH_TAI_SAN_ID = itm.LOAI_HINH_TAI_SAN_ID,
                        LOAI_TAI_SAN_ID = itm.LOAI_TAI_SAN_ID,
                        QLDKTS_MA = itm.MA,
                        NGAY_DONG_BO = DateTime.Now,
                    };
                var lstnv = _taiSanNguonVonService.GetTaiSanNguonVons(itm.ID);
                var lstbd = _bienDongService.GetBienDongsByTaiSanId(itm.ID);
                var lstht = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByTaiSanId(itm.ID);
                TaiSanDBModel dbModel = new TaiSanDBModel()
                {
                    GUID = itm.GUID.ToString(),
                    MA = itm.MA,
                    TEN = itm.TEN,
                    NGAY_NHAP = itm.NGAY_NHAP.toStringApi(),
                    NGAY_SU_DUNG = itm.NGAY_SU_DUNG.toStringApi(),
                    LY_DO_BIEN_DONG_MA = itm.lydobiendong != null ? itm.lydobiendong.MA : null,
                    LOAI_TAI_SAN_MA = itm.loaitaisan != null ? itm.loaitaisan.MA : null,
                    LOAI_HINH_TAI_SAN_ID = itm.LOAI_HINH_TAI_SAN_ID,
                    MA_DON_VI = itm.donvi != null ? itm.donvi.MA : null,
                    TRANG_THAI_ID = itm.TRANG_THAI_ID,
                    GHI_CHU = itm.GHI_CHU,
                    NAM_SAN_XUAT = itm.NAM_SAN_XUAT.ToString(),
                    QUYET_DINH_SO = itm.QUYET_DINH_SO,
                    QUYET_DINH_NGAY = itm.QUYET_DINH_NGAY.toStringApi(),
                    CHUNG_TU_SO = itm.CHUNG_TU_SO,
                    CHUNG_TU_NGAY = itm.CHUNG_TU_NGAY.toStringApi(),
                    LOAI_TAI_SAN_VO_HINH_MA = itm.loaitaisandonvi != null ? itm.loaitaisandonvi.MA : null
                };
                if (itm.donvibophan != null)
                {
                    dbModel.DON_VI_BO_PHAN_ID = itm.donvibophan.ID;
                    dbModel.DB_DON_VI_BO_PHAN_ID = itm.donvibophan.DB_ID;
                }
                dbModel.LST_BIEN_DONG = new List<BienDongDBModel>();
                if (lstbd != null)
                    foreach (var c in lstbd)
                    {
                        var x = c.ToModel<BienDongDBModel>();
                        if (c.donvibophan != null)
                        {
                            x.DON_VI_BO_PHAN_ID = c.DON_VI_BO_PHAN_ID;
                        }

                        var xct = _bienDongChiTietService.GetBienDongChiTietByBDId(c.ID);
                        if (xct != null)
                        {
                            #region prepare BienDongChiTiet
                            x.HINH_THUC_MUA_SAM_MA = xct.hinhthucmuasam != null ? xct.hinhthucmuasam.MA : null;
                            x.MUC_DICH_SU_DUNG_MA = xct.mucdichsudung != null ? xct.mucdichsudung.MA : null;
                            x.NHAN_HIEU = xct.NHAN_HIEU;
                            x.SO_HIEU = xct.SO_HIEU;
                            var diaBan = _diaBanService.GetDiaBanById(xct.DIA_BAN_ID.HasValue ? xct.DIA_BAN_ID.Value : 0);
                            x.DAT_TONG_DIEN_TICH = xct.DAT_TONG_DIEN_TICH;

                            x.HM_SO_NAM_CON_LAI = xct.HM_SO_NAM_CON_LAI;
                            x.KH_NGAY_BAT_DAU = xct.KH_NGAY_BAT_DAU.toStringApi();
                            x.KH_THANG_CON_LAI = xct.KH_THANG_CON_LAI;
                            x.KH_GIA_TINH_KHAU_HAO = xct.KH_GIA_TINH_KHAU_HAO;
                            x.KH_GIA_TRI_TRICH_THANG = xct.KH_GIA_TRI_TRICH_THANG;
                            x.KH_LUY_KE = xct.KH_LUY_KE;
                            x.KH_CON_LAI = xct.KH_CON_LAI;
                            x.NHA_SO_TANG = xct.NHA_SO_TANG;
                            x.NHA_NAM_XAY_DUNG = xct.NHA_NAM_XAY_DUNG;
                            x.NHA_DIEN_TICH_XD = xct.NHA_DIEN_TICH_XD;
                            x.NHA_TONG_DIEN_TICH_XD = xct.NHA_TONG_DIEN_TICH_XD;
                            x.VKT_DIEN_TICH = xct.VKT_DIEN_TICH;
                            x.VKT_THE_TICH = xct.VKT_THE_TICH;
                            x.VKT_CHIEU_DAI = xct.VKT_CHIEU_DAI;
                            x.OTO_BIEN_KIEM_SOAT = xct.OTO_BIEN_KIEM_SOAT;
                            x.OTO_SO_CHO_NGOI = xct.OTO_SO_CHO_NGOI;
                            var nhanXe = _nhanXeService.GetNhanXeById(xct.OTO_NHAN_XE_ID.HasValue ? xct.OTO_NHAN_XE_ID.Value : 0);
                            x.OTO_NHAN_XE_MA = nhanXe != null ? nhanXe.MA : null;
                            x.OTO_TAI_TRONG = xct.OTO_TAI_TRONG;
                            x.OTO_CONG_XUAT = xct.OTO_CONG_XUAT;
                            x.OTO_XI_LANH = xct.OTO_XI_LANH;
                            x.OTO_SO_KHUNG = xct.OTO_SO_KHUNG;
                            x.OTO_SO_MAY = xct.OTO_SO_MAY;
                            x.THONG_SO_KY_THUAT = xct.THONG_SO_KY_THUAT;
                            x.CLN_SO_NAM = xct.CLN_SO_NAM;

                            x.DON_VI_NHAN_DIEU_CHUYEN_MA = xct.donvinhandieuchuyen != null ? xct.donvinhandieuchuyen.MA : null;
                            var hinhThucXL = _phuongAnXuLyService.GetPhuongAnXuLyById(xct.HINH_THUC_XU_LY_ID.HasValue ? xct.HINH_THUC_XU_LY_ID.Value : 0);

                            #endregion
                        }
                        dbModel.LST_BIEN_DONG.Add(x);
                    }
                switch (model.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        var tsCln = _taiSanClnService.GetTaiSanClnByTaiSanId(itm.ID);
                        dbModel.TS_CLN = tsCln != null ? tsCln.ToModel<TaiSanClnDBModel>() : new TaiSanClnDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(itm.ID);
                        if (tsDat != null)
                        {
                            dbModel.TS_DAT = tsDat.ToModel<TaiSanDatDBModel>();
                            dbModel.TS_DAT.DIA_BAN_MA = tsDat.diaban != null ? tsDat.diaban.MA : null;
                        }
                        else
                            dbModel = new TaiSanDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var tsNha = _taiSanNhaService.GetTaiSanNhaByTaiSanId(itm.ID);
                        if (tsNha != null)
                        {
                            dbModel.TS_NHA = tsNha.ToModel<TaiSanNhaDBModel>();
                            dbModel.TS_NHA.TAI_SAN_DAT_MA = tsNha.taisan != null ? tsNha.taisan.MA : null;
                        }
                        else
                            dbModel = new TaiSanDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var tsOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(itm.ID);
                        if (tsOto != null)
                        {
                            dbModel.TS_OTO = tsOto.ToModel<TaiSanOtoDBModel>();
                            dbModel.TS_OTO.CHUC_DANH_MA = tsOto.chucDanh != null ? tsOto.chucDanh.MA_CHUC_DANH : null;
                            dbModel.TS_OTO.NHAN_XE_MA = tsOto.nhanxe != null ? tsOto.nhanxe.MA : null;
                        }
                        else
                            dbModel = new TaiSanDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                        var tsPTK = _taiSanOtoService.GetTaiSanOtoByTaiSanId(itm.ID);
                        if (tsPTK != null)
                        {
                            dbModel.TS_PTK = tsPTK.ToModel<TaiSanOtoDBModel>();
                            dbModel.TS_PTK.CHUC_DANH_MA = tsPTK.chucDanh != null ? tsPTK.chucDanh.MA_CHUC_DANH : null;
                            dbModel.TS_PTK.NHAN_XE_MA = tsPTK.nhanxe != null ? tsPTK.nhanxe.MA : null;
                        }
                        else
                            dbModel = new TaiSanDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        var tsVKT = _taiSanVktService.GetTaiSanVktByTaiSanId(itm.ID);
                        dbModel.TS_VKT = tsVKT != null ? tsVKT.ToModel<TaiSanVktDBModel>() : new TaiSanVktDBModel();

                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        var tsMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(itm.ID);
                        dbModel.TS_MAY_MOC = tsMayMoc != null ? tsMayMoc.ToModel<TaiSanMayMocDBModel>() : new TaiSanMayMocDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        var tsVH = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(itm.ID);
                        dbModel.TS_VO_HINH = tsVH != null ? tsVH.ToModel<TaiSanVoHinhDBModel>() : new TaiSanVoHinhDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                        var tsHH = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(itm.ID);
                        dbModel.TS_HUU_HINH_KHAC = tsHH != null ? tsHH.ToModel<TaiSanMayMocDBModel>() : new TaiSanMayMocDBModel();
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                        var tsDacThu = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(itm.ID);
                        dbModel.TS_DAC_THU = tsDacThu != null ? tsDacThu.ToModel<TaiSanMayMocDBModel>() : new TaiSanMayMocDBModel();
                        break;
                }
                lst.Add(dbModel);
            }
            resultTaiSan.ListTaiSan = lst;
            resultTaiSan.Total = query.TotalCount;
            resultTaiSan.TotalPage = query.TotalPages;
            return resultTaiSan;
        }
        public MessageReturn UpdateTaiSan(TaiSanDBModel dbModel, NguoiDung currentUser)
        {
            if (dbModel == null)
            {
                return MessageReturn.CreateErrorMessage("Data INVALID");
            }
            decimal nguontaisan = 0;
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameKhoCSDL)
            {
                nguontaisan = (decimal)enumNguonTaiSan.CSDLQGTSC;
            }
            else
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameQLTSNN)
            {
                nguontaisan = (decimal)enumNguonTaiSan.QLTSNN;
            }
            else
            {
                nguontaisan = (decimal)enumNguonTaiSan.KHAC;
            }
            dbModel.NGUON_TAI_SAN_ID = nguontaisan;

            TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(dbModel.DB_MA, NguonTaiSanId: nguontaisan);
            if (taiSan != null)
            {
                return MessageReturn.CreateErrorMessage("TAI_SAN already exist");
            }
            else
            {
                DBTaiSan _dBTaiSan = _dbTaiSanService.GetTaiSanByMa(DB_MA: dbModel.MA, nguonTaiSanId: nguontaisan);
                if (_dBTaiSan != null)
                {
                    _dbTaiSanService.DeleteTaiSan(_dBTaiSan);
                }
            }

            //DBTaiSan _dBTaiSan = _dbTaiSanService.GetTaiSanByMa(DB_MA: dbModel.MA, nguonTaiSanId: nguontaisan);
            //if (_dBTaiSan != null)
            //{
            //    TaiSan taiSan = _taiSanService.GetTaiSanByMa(_dBTaiSan.QLDKTS_MA);
            //    if (taiSan != null)
            //    {
            //        return MessageReturn.CreateErrorMessage("TAI_SAN already exist");
            //    }
            //    else
            //    {
            //        _dbTaiSanService.DeleteTaiSan(_dBTaiSan);
            //    }
            //}
            #region validate tài sản  
            MessageReturn result = ValidateThongTinTaiSan(dbModel);
            if (result.Code != MessageReturn.SUCCESS_CODE)
            {
                return result;
            }
            #endregion
            #region validate Biến động
            if (dbModel.LST_BIEN_DONG.Count == 0)
            {
                dbModel.Error = "LST_BIEN_DONG null";
            }
            List<BienDongDBModel> ListBienDong = dbModel.LST_BIEN_DONG.OrderBy(m => m.NGAY_BIEN_DONG.toDateSys()).ToList();
            foreach (var item in ListBienDong)
            {
                result = ValidateThongTinBienDong(item);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    dbModel.Error = result.Message;
                    break;
                }
            }

            taiSan = InsertTaiSanTemp(dbModel, currentUser, nguontaisan);
            dbModel.GUID = taiSan.GUID.ToString();
            dbModel.MA = taiSan.MA;
            return new MessageReturn()
            {
                Message = "SuccessDone",
                Code = MessageReturn.SUCCESS_CODE,
                IdRecord = taiSan.GUID.ToString(),
                ObjectInfo = dbModel
            };
            #endregion

        }
        public MessageReturn UpdateListTaiSan(List<TaiSanDBModel> ListModel, NguoiDung currentUser, decimal nguontaisan)
        {
            int TotalErr = 0;
            int TotalSuc = 0;
            int TotalExist = 0;
            List<DBTaiSan> LstAdd = new List<DBTaiSan>();
            List<DBTaiSan> LstEdit = new List<DBTaiSan>();
            List<TaiSanDBModel> LstErr = new List<TaiSanDBModel>();
            bool IsVaild = true;

            foreach (var dbModel in ListModel)
            {
                // kiểm tra tài sản đã đồng bộ trước đó.              
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(Ma: dbModel.DB_MA, NguonTaiSanId: nguontaisan);
                if (taiSan != null)
                {
                    dbModel.Error = $"DB_MA '{dbModel.DB_MA}' TAI_SAN đã tồn tại";
                    LstErr.Add(dbModel);
                    //TotalErr++;
                    List<BienDongDBModel> ListBienDongs = dbModel.LST_BIEN_DONG.OrderBy(m => m.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB)).ToList();
                    dbModel.LST_BIEN_DONG = new List<BienDongDBModel>();
                    dbModel.LST_BIEN_DONG = ListBienDongs;
                    TotalExist++;
                    continue;
                }
                dbModel.NGUON_TAI_SAN_ID = nguontaisan;
                #region validate tài sản    
                var result = ValidateThongTinTaiSan(dbModel);
                //if(result.Code != MessageReturn.SUCCESS_CODE)
                //    LstErr.Add(dbModel);
                #endregion
                #region validate Biến động
                List<BienDongDBModel> ListBienDong = dbModel.LST_BIEN_DONG.OrderBy(m => m.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB)).ToList();
                foreach (var item in ListBienDong)
                {
                    result = ValidateThongTinBienDong(item);
                }
                dbModel.LST_BIEN_DONG = new List<BienDongDBModel>();
                dbModel.LST_BIEN_DONG = ListBienDong;
                // nguyên giá ban đầu
                BienDongDBModel bienDongFirst = ListBienDong.Where(m => m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).FirstOrDefault();
                dbModel.NGUYEN_GIA_BAN_DAU = bienDongFirst != null ? bienDongFirst.NGUYEN_GIA : 0;
                #endregion
                #region validate hao mòn
                if (dbModel.LST_HAO_MON != null)
                {
                    foreach (HaoMonInTaiSanDBModel hm in dbModel.LST_HAO_MON)
                    {
                        var rs = _validateFactory.CheckChiTietHaoMonInTaiSan(hm);
                        int i = dbModel.LST_HAO_MON.Count(c => c.NAM_HAO_MON == hm.NAM_HAO_MON);
                        if (i > 1)
                            rs.Add($"LST_HAO_MON => NAM_HAO_MON {hm.NAM_HAO_MON} bị lặp");
                        if (rs.Count > 0)
                            IsVaild = false;
                        if (!IsVaild)
                        {
                            TotalErr++;
                            dbModel.Error = string.Join("; ", rs);
                            LstErr.Add(dbModel);
                            break;
                        }
                    }
                }
                #endregion
                #region validate khấu hao
                if (dbModel.LST_KHAU_HAO != null)
                {
                    foreach (KhauHaoInTaiSanDBModel kh in dbModel.LST_KHAU_HAO)
                    {
                        var rs = _validateFactory.CheckChiTietKhauHaoInTaiSan(kh);
                        int i = dbModel.LST_KHAU_HAO.Count(c => c.NAM_KHAU_HAO == kh.NAM_KHAU_HAO && c.THANG_KHAU_HAO == kh.THANG_KHAU_HAO);
                        if (i > 1)
                            rs.Add($"LST_KHAU_HAO => THANG_KHAU_HAO {Convert.ToInt32(kh.THANG_KHAU_HAO)} NAM_KHAU_HAO {Convert.ToInt32(kh.NAM_KHAU_HAO)} bị lặp");
                        if (rs.Count > 0)
                            IsVaild = false;
                        if (!IsVaild)
                        {
                            TotalErr++;
                            dbModel.Error = string.Join("; ", rs);
                            LstErr.Add(dbModel);
                            break;
                        }
                    }
                }
                #endregion
                if (IsVaild)
                {
                    taiSan = InsertTaiSanTemp(dbModel, currentUser, nguontaisan);
                    dbModel.GUID = taiSan.GUID.ToString();
                    dbModel.MA = taiSan.MA;
                    TotalSuc++;
                }
            }
            if (TotalErr > 0)
            {
                foreach (TaiSanDBModel item in LstErr)
                {
                    DBTaiSan _dBTaiSan = _dbTaiSanService.GetTaiSanByMa(DB_MA: item.DB_MA, nguonTaiSanId: nguontaisan);
                    if (_dBTaiSan != null)
                    {
                        _dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                        _dBTaiSan.RESPONSE = item.toStringJson(isIgnoreNull: true);
                        _dbTaiSanService.UpdateTaiSan(_dBTaiSan);
                    }
                    item.ID = 0;
                }
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {TotalSuc} success - Total {TotalErr} error - Total {TotalExist} Exist",
                    ObjectInfo = LstErr
                };
            }
            else
            {
                foreach (TaiSanDBModel item in ListModel)
                {
                    DBTaiSan _dBTaiSan = _dbTaiSanService.GetTaiSanByMa(DB_MA: item.DB_MA, nguonTaiSanId: nguontaisan);
                    if (_dBTaiSan != null)
                    {
                        _dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.ChuaInsert;
                        _dBTaiSan.RESPONSE = item.toStringJson(isIgnoreNull: true);
                        _dbTaiSanService.UpdateTaiSan(_dBTaiSan);
                    }
                }
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = $"Success Done. Total {TotalSuc} success - Total {TotalExist} Exist",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteTaiSan(string MaTaiSan)
        {
            if (MaTaiSan == null)
                return MessageReturn.CreateNotFoundMessage("MaTaiSan");
            try
            {

                var TaiSan = _taiSanService.GetTaiSanByMa(MaTaiSan);
                if (TaiSan != null)
                {
                    if (!CheckSoTaiSan(TaiSan.NGAY_DUYET.Value.Year, TaiSan.DON_VI_ID.Value))
                    {
                        return MessageReturn.CreateErrorMessage($"Đơn vị {TaiSan.donvi.TEN} đã khóa sổ tài sản năm {TaiSan.NGAY_DUYET.Value.Year}");
                    }
                    OracleParameter p_Ma = new OracleParameter("p_MA", OracleDbType.Varchar2, MaTaiSan, ParameterDirection.Input);
                    var result = _dbContext.ExecuteSqlCommand("BEGIN DELETE_TAI_SAN_DONGBO(:p_MA); END;", false, null, p_Ma);
                    return MessageReturn.CreateSuccessMessage("Success done");
                }
                else
                {
                    // kiểm tra tài sản trong bảng 
                    return MessageReturn.CreateNotFoundMessage("TAI_SAN INVALID");
                }
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("Error");
            }

        }
        public MessageReturn UpdateBienDong(List<BienDongDBModel> ListBienDong, NguoiDung currentUser, decimal nguontaisan)
        {
            List<string> ListErr = new List<string>();
            ListBienDong = ListBienDong.OrderBy(m => DateTime.Parse(m.NGAY_BIEN_DONG)).ToList();
            foreach (var model in ListBienDong)
            {
                //TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(Ma: model.MA_TAI_SAN_DB, NguonTaiSanId: nguontaisan);
                //model.MA_TAI_SAN = taiSan.MA;
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN);

                if (CheckSoTaiSan(model.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB).Value.Year, taiSan.DON_VI_ID.Value))
                {
                    return MessageReturn.CreateErrorMessage($"Đơn vị {taiSan.donvi.TEN} đã khóa sổ tài sản năm {model.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB).Value.Year}");
                }

            }
            if (ListErr.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.ERROR_CODE,
                    ObjectInfo = ListErr
                };
            }
            else
            {
                return MessageReturn.CreateSuccessMessage("done");
            }
        }
        public bool CheckSoTaiSan(int nam, decimal donviID)
        {
            var cauHinh = _cauHinhService.LoadCauHinhDonViBo<CauHinhSoTaiSan>(donviID);
            if (cauHinh != null)
            {
                List<TrangThaiNamModel> lstCH = cauHinh.KhoaSoHeThong.toEntities<TrangThaiNamModel>();
                if (lstCH != null)
                {
                    List<int> lstnam = lstCH.Select(c => c.Nam).ToList();
                    return !lstnam.Contains(nam);
                }

            }
            return true;
        }
        public MessageReturn DeleteBienDong(string ID_DB)
        {
            BienDong bienDong = _bienDongService.GetBienDongByID_DB(ID_DB);
            if (bienDong == null)
            {
                return MessageReturn.CreateErrorMessage($"ID_DB={ID_DB} không tồn tại");
            }
            if (!CheckSoTaiSan(bienDong.NGAY_BIEN_DONG.Value.Year, bienDong.DON_VI_ID.Value))
            {
                return MessageReturn.CreateErrorMessage($"Đơn vị {bienDong.donvi.TEN} đã khóa sổ tài sản năm {bienDong.NGAY_BIEN_DONG.Value.Year}");
            }
            BienDongChiTiet bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
            var list_taiSanNguonVon = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: bienDong.TAI_SAN_ID, BienDongID: bienDong.ID);
            if (list_taiSanNguonVon != null && list_taiSanNguonVon.Count > 0)
                _taiSanNguonVonService.DeleteTaiSanNguonVons(list_taiSanNguonVon);
            //hiện trạng sử dụng
            var list_hienTrangSuDung = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(bienDong.ID);
            if (list_hienTrangSuDung != null && list_hienTrangSuDung.Count > 0)
                _taiSanHienTrangSuDungService.DeleteTaiSanHienTrangSuDungs(list_hienTrangSuDung);
            //xóa biến động chi tiết;
            if (bienDongChiTiet == null)
                bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
            _bienDongChiTietService.DeleteBienDongChiTiet(bienDongChiTiet);
            //xóa biến động
            _bienDongService.DeleteBienDong(bienDong);
            //cập nhật tài sản nhật ký- đồng bộ tài sản
            return MessageReturn.CreateSuccessMessage("Done");
        }
        public MessageReturn PrepareBienDong(BienDong bienDong, BienDongDBModel model)
        {
            if (bienDong == null)
            {
                bienDong = new BienDong();
            }
            bienDong.NGUYEN_GIA = model.NGUYEN_GIA;
            bienDong.CHUNG_TU_SO = model.CHUNG_TU_SO;
            bienDong.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            bienDong.NGAY_BIEN_DONG = model.NGAY_BIEN_DONG.toDateSys("yyyy-MM-dd HH:mm:ss");
            bienDong.NGAY_DUYET = model.NGAY_DUYET.toDateSys("yyyy-MM-dd HH:mm:ss");
            bienDong.NGAY_SU_DUNG = model.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
            bienDong.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
            bienDong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
            bienDong.GHI_CHU = model.GHI_CHU;
            bienDong.QUYET_DINH_NGAY = Convert.ToDateTime(model.QUYET_DINH_NGAY);
            bienDong.QUYET_DINH_SO = model.QUYET_DINH_SO;
            return MessageReturn.CreateSuccessMessage("Succes done");
        }
        public MessageReturn PrepareBienDongChiTiet(BienDongChiTiet bienDongChiTiet, BienDongDBModel model)
        {
            if (!string.IsNullOrEmpty(model.HINH_THUC_MUA_SAM_MA))
            {
                var HinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(model.HINH_THUC_MUA_SAM_MA);
                if (HinhThucMuaSam == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_MUA_SAM_MA NOT EXIST");
                }
                bienDongChiTiet.HINH_THUC_MUA_SAM_ID = HinhThucMuaSam.ID;
            }
            if (!string.IsNullOrEmpty(model.MUC_DICH_SU_DUNG_MA))
            {
                var MucDichSuDung = _mucDichSuDungService.GetMucDichSuDungByMa(model.MUC_DICH_SU_DUNG_MA);
                if (MucDichSuDung == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-MUC_DICH_SU_DUNG_MA NOT EXIST");
                }
                bienDongChiTiet.MUC_DICH_SU_DUNG_ID = MucDichSuDung.ID;
            }
            bienDongChiTiet.NHAN_HIEU = model.NHAN_HIEU;
            bienDongChiTiet.SO_HIEU = model.SO_HIEU;
            if (model.DIA_BAN_ID > 0)
            {
                var DiaBan = _diaBanService.GetDiaBanById(model.DIA_BAN_ID.Value);
                if (DiaBan == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DIA_BAN_MA NOT EXIST");
                }
                bienDongChiTiet.DIA_BAN_ID = DiaBan.ID;
            }
            bienDongChiTiet.NGUYEN_GIA = model.NGUYEN_GIA;
            bienDongChiTiet.DAT_TONG_DIEN_TICH = model.DAT_TONG_DIEN_TICH;
            bienDongChiTiet.HM_SO_NAM_CON_LAI = model.HM_SO_NAM_CON_LAI;
            bienDongChiTiet.HM_TY_LE_HAO_MON = model.HM_TY_LE_HAO_MON;
            bienDongChiTiet.HM_LUY_KE = model.HM_LUY_KE;
            bienDongChiTiet.HM_GIA_TRI_CON_LAI = model.GIA_TRI_CON_LAI;
            bienDongChiTiet.KH_NGAY_BAT_DAU = Convert.ToDateTime(model.KH_NGAY_BAT_DAU);
            bienDongChiTiet.KH_THANG_CON_LAI = model.KH_THANG_CON_LAI;
            bienDongChiTiet.KH_GIA_TINH_KHAU_HAO = model.KH_GIA_TINH_KHAU_HAO;
            bienDongChiTiet.KH_GIA_TRI_TRICH_THANG = model.KH_GIA_TRI_TRICH_THANG;
            bienDongChiTiet.KH_LUY_KE = model.KH_LUY_KE;
            bienDongChiTiet.KH_CON_LAI = model.KH_CON_LAI;
            bienDongChiTiet.NHA_SO_TANG = model.NHA_SO_TANG;
            bienDongChiTiet.NHA_NAM_XAY_DUNG = model.NHA_NAM_XAY_DUNG;
            bienDongChiTiet.NHA_DIEN_TICH_XD = model.NHA_DIEN_TICH_XD;
            bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = model.NHA_TONG_DIEN_TICH_XD;
            bienDongChiTiet.VKT_DIEN_TICH = model.VKT_DIEN_TICH.GetValueOrDefault(0);
            bienDongChiTiet.VKT_THE_TICH = model.VKT_THE_TICH;
            bienDongChiTiet.VKT_CHIEU_DAI = model.VKT_CHIEU_DAI;
            bienDongChiTiet.OTO_BIEN_KIEM_SOAT = model.OTO_BIEN_KIEM_SOAT;
            bienDongChiTiet.OTO_SO_CHO_NGOI = model.OTO_SO_CHO_NGOI;
            if (!string.IsNullOrEmpty(model.OTO_CHUC_DANH_MA))
            {
                var ChucDanh = _chucDanhService.GetChucDanhByMa(model.OTO_CHUC_DANH_MA);
                if (ChucDanh == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_CHUC_DANH_MA NOT EXIST");
                }
                bienDongChiTiet.OTO_CHUC_DANH_ID = ChucDanh.ID;
            }

            bienDongChiTiet.THONG_SO_KY_THUAT = model.THONG_SO_KY_THUAT;
            if (!string.IsNullOrEmpty(model.OTO_NHAN_XE_MA))
            {
                var nhanXe = _nhanXeService.GetNhanXeByMaTen(model.OTO_NHAN_XE_MA);
                if (nhanXe == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_NHAN_XE_MA NOT EXIST");
                }
                bienDongChiTiet.OTO_NHAN_XE_ID = nhanXe.ID;
            }
            bienDongChiTiet.OTO_TAI_TRONG = model.OTO_TAI_TRONG;
            bienDongChiTiet.OTO_CONG_XUAT = model.OTO_CONG_XUAT;
            bienDongChiTiet.OTO_XI_LANH = model.OTO_XI_LANH;
            bienDongChiTiet.OTO_SO_KHUNG = model.OTO_SO_KHUNG;
            bienDongChiTiet.OTO_SO_MAY = model.OTO_SO_MAY;
            bienDongChiTiet.THONG_SO_KY_THUAT = model.THONG_SO_KY_THUAT;
            bienDongChiTiet.CLN_SO_NAM = model.CLN_SO_NAM;
            if (!string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
            {
                var DonVi = _donViService.GetDonViByMa(model.DON_VI_NHAN_DIEU_CHUYEN_MA);
                if (DonVi == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA NOT EXIST");
                }
                bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID = DonVi.ID;
            }
            if (!string.IsNullOrEmpty(model.HINH_THUC_XU_LY_MA))
            {
                var PhuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.HINH_THUC_XU_LY_MA);
                if (PhuongAnXuLy == null)
                {
                    bienDongChiTiet = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_XU_LY_MA NOT EXIST");
                }
                bienDongChiTiet.HINH_THUC_XU_LY_ID = PhuongAnXuLy.ID;
            }
            bienDongChiTiet.IS_BAN_THANH_LY = model.IS_BAN_THANH_LY;
            bienDongChiTiet.DATA_JSON = model.HO_SO_GIAY_TO == null ? (new HoSoGiayTo()).toStringJson() : model.HO_SO_GIAY_TO.toStringJson(isIgnoreNull: true);
            return MessageReturn.CreateSuccessMessage("Succes done");
        }
        public string PrepareHienTrangSuDungJson(List<TaiSanHienTrangSuDung> ListHienTrang, BienDong bienDong)
        {
            var _ListHienTrang = ListHienTrang.Select(m =>
             {
                 if (bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                 {
                     if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                         m.GIA_TRI_NUMBER = -Math.Abs(m.GIA_TRI_NUMBER.GetValueOrDefault(0));
                     else
                         m.GIA_TRI_NUMBER = m.GIA_TRI_NUMBER;
                 }
                 else
                     m.GIA_TRI_NUMBER = m.GIA_TRI_NUMBER;
                 return new ObjHienTrang_Entity()
                 {
                     HienTrangId = m.HIEN_TRANG_ID,
                     GiaTriText = m.GIA_TRI_TEXT,
                     GiaTriNumber = m.GIA_TRI_NUMBER,
                     GiaTriCheckbox = m.GIA_TRI_CHECKBOX.GetValueOrDefault(false),
                     NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                     TenHienTrang = m.TEN_HIEN_TRANG,
                     KieuDuLieuId = m.KIEU_DU_LIEU_ID
                 };
             }).ToList();
            var HienTrangList = new HienTrangList_Entity()
            {
                TaiSanId = bienDong.TAI_SAN_ID,
                lstObjHienTrang = _ListHienTrang
            };
            return HienTrangList.toStringJson(isIgnoreNull: true);
        }
        TaiSanHienTrangSuDung PrepareTaiSanHienTrangSuDung(decimal HienTrangId, bool? GiaTriCheckbox, decimal? GiaTriNumber, decimal TaiSanID = 0)
        {
            TaiSanHienTrangSuDung item = new TaiSanHienTrangSuDung();
            item.HIEN_TRANG_ID = HienTrangId;
            if (GiaTriCheckbox != null)
            {
                item.GIA_TRI_CHECKBOX = GiaTriCheckbox.Value;
            }
            if (GiaTriNumber != null)
            {
                item.GIA_TRI_NUMBER = GiaTriNumber;
            }
            item.TAI_SAN_ID = TaiSanID;
            return item;
        }
        public void UpDateNguonVon(BienDongDBModel model)
        {

        }
        public List<string> GetAllMaTaiSan(int LoaiHinhTaiSanId = 0)
        {
            if (LoaiHinhTaiSanId == 0)
                return null;
            var ListMaTaiSan = _taiSanService.GetAllTaiSans(LoaiHinhTaiSan: LoaiHinhTaiSanId, trangthai: enumTRANG_THAI_TAI_SAN.DA_DUYET).Select(m => m.MA);
            return ListMaTaiSan.ToList();
        }
        #endregion
        #region TaiSanNhatKy
        public virtual MessageReturn UpdateTaiSanDaDongBo(List<string> ListMaTaiSan, List<QuyetDinh> ListQuyetDinhTichThu, string MaDonVi)
        {
            int TotalErr = 0;
            int TotalSuc = 0;
            string ErrorMessage = "";
            foreach (var item in ListMaTaiSan)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item);
                if (taiSan == null)
                {
                    TotalErr++;
                    ErrorMessage += $"\nMA_TAI_SAN INVALID";
                }
                else
                {
                    // cập nhật nhật ký tài sản
                    TotalSuc++;
                }
            }
            foreach (var item in ListQuyetDinhTichThu)
            {
                var QuyetDinhTichThu = _quyetDinhTichThuService.GetQuyetDinhTichThu(item.QUYET_DINH_SO, item.QUYET_DINH_NGAY, MaDonVi);
                if (QuyetDinhTichThu == null)
                {
                    TotalErr++;
                    ErrorMessage += $"\nQUYET_DINH_TICH_THU INVALID";
                }
                else
                {
                    _taiSanNhatKyService.UpdateQuyetDinhTichThuNhatKY(QuyetDinhTichThu.ID);
                    TotalSuc++;
                }
            }
            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{ErrorMessage}" : ""));
        }
        #endregion
        #region Kiên-Tài sản toàn dân
        public List<QuyetDinhTichThuModel> GetQuyetDinhTichThuModels()
        {
            List<decimal> ListQuyetDinhId = _taiSanNhatKyService.GetTaiSanToaDanDB().Select(m => m.QUYET_DINH_TICH_THU_ID.Value).Distinct().ToList();
            List<QuyetDinhTichThuModel> ListModel = new List<QuyetDinhTichThuModel>();
            var ListQuyetDinhTichThu = ListQuyetDinhId.Select(m => _quyetDinhTichThuService.GetQuyetDinhTichThuById(m)).Where(m => m != null);
            foreach (var item in ListQuyetDinhTichThu)
            {
                QuyetDinhTichThuModel model = new QuyetDinhTichThuModel();
                model = item.ToModel<QuyetDinhTichThuModel>();
                model.DON_VI_ID = item.DON_VI_ID;
                var ListTaiSanTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID).Where(m => m.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI);//0-tồn tại; 1- xóa;
                foreach (var ts in ListTaiSanTD)
                {
                    TaiSanToanDanModel taiSanToanDanModel = new TaiSanToanDanModel();
                    taiSanToanDanModel = ts.ToModel<TaiSanToanDanModel>();
                    taiSanToanDanModel.LOAI_TAI_SAN_ID = ts.LOAI_TAI_SAN_ID;
                    //đã xử lý==2; Đề xuất =1;
                    var xuLyTS = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(ts.ID);
                    foreach (var xulyTaiSan in xuLyTS)
                    {

                        //  xử lý
                        var XuLy = _xuLyService.GetXuLyById(xulyTaiSan.XU_LY_ID);
                        XuLyModel xuLyModel = XuLy.ToModel<XuLyModel>();
                        //    xuLyModel.MA_DON_VI = XuLy.DON_VI_ID != null ? _donViService.GetDonViById(XuLy.DON_VI_ID.Value).MA : null;
                        // Xử lý  Tài sản toàn dân
                        TSToanDanXuLyModel toanDanXuLyModel = xulyTaiSan.ToModel<TSToanDanXuLyModel>();
                        //toanDanXuLyModel.MA_DON_VI_CHUYEN = xulyTaiSan.DON_VI_CHUYEN_ID != null ? _donViService.GetDonViById(xulyTaiSan.DON_VI_CHUYEN_ID.Value).MA : null;
                        toanDanXuLyModel.MA_HINH_THUC_XU_LY = xulyTaiSan.HINH_THUC_XU_LY_ID != null ? _phuongAnXuLyService.GetPhuongAnXuLyById(xulyTaiSan.HINH_THUC_XU_LY_ID.Value).MA : null;
                        toanDanXuLyModel.MA_PHUONG_AN_XU_LY = xulyTaiSan.PHUONG_AN_XU_LY_ID != null ? _hinhThucXuLyService.GetHinhThucXuLyById(xulyTaiSan.PHUONG_AN_XU_LY_ID.Value).MA : null;
                        xuLyModel.GHI_CHU_CHI_TIET = toanDanXuLyModel.GHI_CHU;
                    }
                    // update tài sản.
                    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(ts.ID);
                    taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
                ListModel.Add(model);
            }
            string JsonData = ListModel.toStringJson(isIgnoreNull: true);
            return ListModel;
        }
        public MessageReturn UpdateQuyetDinhTichThuModel(List<QuyetDinhTichThuModel> ListModel)
        {
            var ListDB = new List<ItemDB>();
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };

            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {
                        result = InsertQuyetDinhTichThu(md);
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            //ListDB.Add(new ItemDB {DB_ID = md.DB_ID.ToString(),ID= result.ObjectInfo});
                        }
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdateQuyetDinhTichThu(md);
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            //result = UpdateTaiSanToanDanModel(ListModel: md.LST_TAI_SAN.ToList(), null);
                        }
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeleteQuyetDinhTichThu(md);
                    }
                }
            }
            return result;
        }
        public MessageReturn UpdateTaiSanToanDanModel(List<TaiSanToanDanModel> ListModel)
        {
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };
            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {
                        //md.QUYET_DINH_TICH_THU_ID = QuyetDinhTinhThuId;
                        result = InsertTaiSanToanDan(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdateTaiSanTd(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeleteTaiSanToanDan(md);
                    }
                }
            }
            return result;
        }
        public MessageReturn UpdatePhuongAnXuLyModel(List<XuLyModel> ListModel)
        {
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };
            //if(model!= null)
            //{
            //    ListModel = new List<QuyetDinhTichThuModel>() { model };
            //}
            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {
                        result = InsertPhuongAnXuLy(md);
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            //md.ID = result.ObjectInfo;
                            //  result = UpdateTaiSanToanDanXuLyModel(ListModel: md.LST_TS_XU_LY.ToList(), md.ID);

                        }
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdatePhuongAnXuLy(md);
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            //result = UpdateTaiSanToanDanXuLyModel(ListModel: md.LST_TS_XU_LY.ToList(),null);
                        }
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeletePhuongAnXuLy(md);
                    }
                }
            }
            return result;
        }
        public MessageReturn UpdateTaiSanToanDanXuLyModel(List<TSToanDanXuLyModel> ListModel)
        {
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };
            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {
                        //    md.XU_LY_ID = (decimal)XuLyId;
                        result = InsertTaiSanXuLy(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdateTaiSanXuLy(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeleteTaiSanXuLy(md);
                    }
                }
            }
            return result;
        }
        public MessageReturn UpdateKetQuaXuLyModel(List<KetQuaXuLyTaiSanModel> ListModel)
        {
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };
            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {

                        result = InsertKetQuaXuLy(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdateKetQuaXuLy(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeleteKetQuaXuLy(md);
                    }
                }
            }
            return result;
        }
        public MessageReturn UpdateThuChiModel(List<ThuChiModel> ListModel)
        {
            var result = new MessageReturn() { Code = MessageReturn.ERROR_CODE, Message = "UPDATE FAIL" };
            if (ListModel != null && ListModel.Count() > 0)
            {
                foreach (var md in ListModel)
                {
                    if (md.TYPE_ID == (int)enumTYPE.INSERT)
                    {

                        result = InsertThuChi(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.UPDATE)
                    {
                        result = UpdateThuChi(md);
                    }
                    else if (md.TYPE_ID == (int)enumTYPE.DELETE)
                    {
                        result = DeleteThuChi(md);
                    }
                }
            }
            return result;
        }

        #region DML cho từng model
        public MessageReturn InsertQuyetDinhTichThu(QuyetDinhTichThuModel model)
        {
            var item = new QuyetDinhTichThu();
            var dv = _donViService.GetDonViById((decimal)model.DON_VI_ID);
            if (dv == null)
            {
                return MessageReturn.CreateErrorMessage("DON_VI_ID INVALID");
            }
            else if (_quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            else
            {
                item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
                item.QUYET_DINH_SO = model.QUYET_DINH_SO;
                item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
                item.DON_VI_ID = model.DON_VI_ID;
                item.GHI_CHU = model.GHI_CHU;
                item.NGAY_TAO = model.NGAY_TAO;
                item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
                item.NGUON_GOC_ID = model.NGUON_GOC_ID;
                item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
                item.TEN = model.TEN;
                item.TRANG_THAI_ID = model.TRANG_THAI_ID;
                item.DB_ID = model.DB_ID;
                try
                {
                    _quyetDinhTichThuService.InsertQuyetDinhTichThu(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS", item.ID);
        }
        public MessageReturn UpdateQuyetDinhTichThu(QuyetDinhTichThuModel model)
        {
            var item = _quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            if (model.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("TRANG_THAI_ID ERROR");
            }
            else
            {
                item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
                item.QUYET_DINH_SO = model.QUYET_DINH_SO;
                item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
                item.DON_VI_ID = model.DON_VI_ID;
                item.GHI_CHU = model.GHI_CHU;
                item.NGAY_TAO = model.NGAY_TAO;
                item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
                item.NGUON_GOC_ID = model.NGUON_GOC_ID;
                item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
                item.TEN = model.TEN;
                item.TRANG_THAI_ID = model.TRANG_THAI_ID;
                item.DB_ID = model.DB_ID;
                try
                {
                    _quyetDinhTichThuService.UpdateQuyetDinhTichThu(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeleteQuyetDinhTichThu(QuyetDinhTichThuModel model)
        {
            var item = _quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            //đã tồn tại trong xử lý
            if (_taiSanTdXuLyService.GetTaiSanTdXuLys(quyetDinhId: item.ID).Count() > 0)
            {
                return MessageReturn.CreateErrorMessage("ĐÃ TỒN TẠI TRONG PHƯƠNG ÁN XỬ LÝ");
            }
            item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA;
            try
            {
                _quyetDinhTichThuService.UpdateQuyetDinhTichThu(item);
                //update trạng thái tstd
                var listTSTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
                foreach (var tstd in listTSTD)
                {
                    DeleteTaiSanToanDan(tstd.ToModel<TaiSanToanDanModel>());
                }
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("UPDATE FAIL");
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn InsertTaiSanToanDan(TaiSanToanDanModel model)
        {
            var qd = _quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_QUYET_DINH_TICH_THU_ID);
            if (qd == null)
            {
                return MessageReturn.CreateErrorMessage("DB_QUYET_DINH_TICH_THU_ID INVALID");
            }
            if (_taiSanTdService.GetTaiSanTdByDB_ID(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA && model.DB_TAI_SAN_DAT_ID != null)
            {
                var tsd = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_TAI_SAN_DAT_ID.ToString());
                if (tsd == null)
                {
                    return MessageReturn.CreateErrorMessage("DB_TAI_SAN_DAT_ID INVALID");
                }
                else
                {
                    model.TAI_SAN_DAT_ID = tsd.ID;
                }
            }
            var item = new TaiSanTd() { };
            item.TEN = model.TEN_TAI_SAN;
            item.BIEN_KIEM_SOAT = model.BIEN_KIEM_SOAT;
            item.DIA_CHI = model.DIA_CHI;
            item.DON_VI_TINH = model.DON_VI_TINH;
            item.GHI_CHU = model.GHI_CHU;
            item.GIA_TRI_TINH = model.GIA_TRI_TINH;
            item.GIA_TRI_XAC_LAP = model.GIA_TRI_XAC_LAP;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            item.NHAN_XE_ID = model.NHAN_XE_ID;
            item.QUYET_DINH_TICH_THU_ID = qd.ID;
            item.SO_CAU_XE = model.SO_CAU_XE;
            item.SO_CHO_NGOI = model.SO_CHO_NGOI;
            item.SO_LUONG = model.SO_LUONG;
            item.TAI_SAN_DAT_ID = model.TAI_SAN_DAT_ID;
            item.TAI_TRONG = model.TAI_TRONG;
            item.TEN_LOAI_TAI_SAN = model.TEN_LOAI_TAI_SAN;
            item.DB_ID = model.DB_ID;
            item.DB_QUYET_DINH_TICH_THU_ID = model.DB_QUYET_DINH_TICH_THU_ID;
            item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
            {
                item.GIA_TRI_TINH = model.SO_LUONG;
            }
            else
            {
                model.SO_LUONG = 1;
            }
            if (model.NAM_SU_DUNG != null)
            {
                item.NGAY_SU_DUNG = new DateTime((int)model.NAM_SU_DUNG, 01, 01);
            }
            try
            {
                _taiSanTdService.InsertTaiSanTd(item);
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("UPDATE FAIL");
            }

            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn UpdateTaiSanTd(TaiSanToanDanModel model)
        {
            var item = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            var qd = _quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_QUYET_DINH_TICH_THU_ID);
            if (qd == null)
            {
                return MessageReturn.CreateErrorMessage("DB_QUYET_DINH_TICH_THU_ID INVALID");
            }
            if (qd.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            if (model.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("TRANG_THAI_ID ERROR");
            }
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA && model.DB_TAI_SAN_DAT_ID != null)
            {
                var tsd = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_TAI_SAN_DAT_ID.ToString());
                if (tsd == null)
                {
                    return MessageReturn.CreateErrorMessage("DB_TAI_SAN_DAT_ID INVALID");
                }
                else
                {
                    model.TAI_SAN_DAT_ID = tsd.ID;
                }
            }

            item.TEN = model.TEN_TAI_SAN;
            item.BIEN_KIEM_SOAT = model.BIEN_KIEM_SOAT;
            item.DIA_CHI = model.DIA_CHI;
            item.DON_VI_TINH = model.DON_VI_TINH;
            item.GHI_CHU = model.GHI_CHU;
            item.GIA_TRI_TINH = model.GIA_TRI_TINH;
            item.GIA_TRI_XAC_LAP = model.GIA_TRI_XAC_LAP;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            item.NHAN_XE_ID = model.NHAN_XE_ID;
            item.QUYET_DINH_TICH_THU_ID = qd.ID;
            item.SO_CAU_XE = model.SO_CAU_XE;
            item.SO_CHO_NGOI = model.SO_CHO_NGOI;
            item.SO_LUONG = model.SO_LUONG;
            item.TAI_SAN_DAT_ID = model.TAI_SAN_DAT_ID;
            item.TAI_TRONG = model.TAI_TRONG;
            item.TEN_LOAI_TAI_SAN = model.TEN_LOAI_TAI_SAN;
            item.DB_ID = model.DB_ID;
            item.DB_QUYET_DINH_TICH_THU_ID = item.DB_QUYET_DINH_TICH_THU_ID;
            item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
            {
                item.GIA_TRI_TINH = model.SO_LUONG;
            }
            else
            {
                model.SO_LUONG = 1;
            }
            if (model.NAM_SU_DUNG != null)
            {
                item.NGAY_SU_DUNG = new DateTime((int)model.NAM_SU_DUNG, 01, 01);
            }
            try
            {
                _taiSanTdService.UpdateTaiSanTd(item);
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("UPDATE FAIL");
            }

            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeleteTaiSanToanDan(TaiSanToanDanModel model)
        {
            var item = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            var qd = _quyetDinhTichThuService.GetQuyetDinhTichThuByDB_ID(model.DB_QUYET_DINH_TICH_THU_ID);
            if (qd.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            //đã tồn tại trong xử lý
            if (_taiSanTdXuLyService.GetTaiSanTdXuLys(TaiSanId: (int)item.ID).Count() > 0)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            else
            {

                item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.XOA;
                try
                {
                    _taiSanTdService.UpdateTaiSanTd(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn InsertPhuongAnXuLy(XuLyModel model)
        {
            var item = new XuLy();
            if (_xuLyService.GetXuLyByDB_Id(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
            item.DON_VI_ID = model.DON_VI_ID;
            item.GHI_CHU = model.GHI_CHU;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
            item.DB_ID = model.DB_ID;
            item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.TonTai;
            _xuLyService.InsertXuLy(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS", item.ID);
        }
        public MessageReturn UpdatePhuongAnXuLy(XuLyModel model)
        {
            var item = _xuLyService.GetXuLyByDB_Id(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            if (model.TRANG_THAI_ID != (int)enumTrangThaiXuLy.TonTai)
            {
                return MessageReturn.CreateErrorMessage("TRANG_THAI_ID ERROR");
            }
            else
            {
                item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
                item.QUYET_DINH_SO = model.QUYET_DINH_SO;
                item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
                item.DON_VI_ID = model.DON_VI_ID;
                item.GHI_CHU = model.GHI_CHU;
                item.NGAY_TAO = model.NGAY_TAO;
                item.DB_ID = model.DB_ID;
                item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
                item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                try
                {
                    _xuLyService.UpdateXuLy(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeletePhuongAnXuLy(XuLyModel model)
        {
            var item = _xuLyService.GetXuLyByDB_Id(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            //đã tồn tại trong kết quả
            var listtsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: item.ID);
            var kq = _ketQuaService.GetKetQuaBys(ListTaiSanTDXuLy: listtsxl.Select(c => c.ID).ToList());
            if (kq.Count() > 0)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            else
            {
                try
                {
                    //xóa xử lý tài sản
                    foreach (var tsxl in listtsxl)
                    {
                        _taiSanTdXuLyService.DeleteTaiSanTdXuLy(tsxl);
                    }
                    _xuLyService.DeleteXuLy(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn InsertTaiSanXuLy(TSToanDanXuLyModel model)
        {
            var xl = _xuLyService.GetXuLyByDB_Id(model.DB_XU_LY_ID);
            if (xl == null)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            if (_taiSanTdXuLyService.GetTaiSanTdXuLyByDB_ID(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            var tstd = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_TAI_SAN_ID);
            if (tstd == null)
            {
                return MessageReturn.CreateErrorMessage("DB_TAI_SAN_ID INVALID");

            }
            //check số lượng
            var soluong = _taiSanTdService.GetSoLuongConByTaiSanId(tstd.ID, xulyid: xl.ID);
            if (soluong < model.SO_LUONG)
            {
                return MessageReturn.CreateErrorMessage("SO_LUONG INVALID");
            }
            //check xem có tồn tại phương án 
            var paxl = _phuongAnXuLyService.GetPhuongAnXuLyById((decimal)model.PHUONG_AN_XU_LY_ID);
            if (paxl == null)
            {
                return MessageReturn.CreateErrorMessage("PHUONG_AN_XU_LY_ID INVALID");

            }
            var item = new TaiSanTdXuLy();
            item.GHI_CHU = model.GHI_CHU;
            item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
            item.SO_LUONG = model.SO_LUONG;
            item.TAI_SAN_ID = tstd.ID;
            item.XU_LY_ID = xl.ID;
            item.DB_ID = model.DB_ID;
            item.DB_TAI_SAN_ID = model.DB_TAI_SAN_ID;
            item.DB_XU_LY_ID = model.DB_XU_LY_ID;
            _taiSanTdXuLyService.InsertTaiSanTdXuLy(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn UpdateTaiSanXuLy(TSToanDanXuLyModel model)
        {
            var item = _taiSanTdXuLyService.GetTaiSanTdXuLyByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            var xl = _xuLyService.GetXuLyByDB_Id(model.DB_XU_LY_ID);
            if (xl == null)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            var tstd = _taiSanTdService.GetTaiSanTdByDB_ID(model.DB_TAI_SAN_ID);
            if (tstd == null)
            {
                return MessageReturn.CreateErrorMessage("DB_TAI_SAN_ID INVALID");
            }
            //check số lượng
            var soluong = _taiSanTdService.GetSoLuongConByTaiSanId(tstd.ID, xulyid: xl.ID);
            if (soluong < model.SO_LUONG)
            {
                return MessageReturn.CreateErrorMessage("SO_LUONG INVALID");
            }
            //check xem có tồn tại phương án 
            var paxl = _phuongAnXuLyService.GetPhuongAnXuLyById((decimal)model.PHUONG_AN_XU_LY_ID);
            if (paxl == null)
            {
                return MessageReturn.CreateErrorMessage("PHUONG_AN_XU_LY_ID INVALID");

            }
            //item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            item.GHI_CHU = model.GHI_CHU;
            item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
            item.SO_LUONG = model.SO_LUONG;
            item.TAI_SAN_ID = tstd.ID;
            item.XU_LY_ID = xl.ID;
            item.DB_ID = model.DB_ID;
            item.DB_TAI_SAN_ID = model.DB_TAI_SAN_ID;
            item.DB_XU_LY_ID = model.DB_XU_LY_ID;
            _taiSanTdXuLyService.UpdateTaiSanTdXuLy(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeleteTaiSanXuLy(TSToanDanXuLyModel model)
        {
            var item = _taiSanTdXuLyService.GetTaiSanTdXuLyByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            //đã tồn tại trong kết quả
            var kq = _ketQuaService.GetKetQuaBys(TaiSanTDXuLy: item.ID).ToList();
            if (kq.Count() > 0)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            else
            {
                try
                {
                    _taiSanTdXuLyService.DeleteTaiSanTdXuLy(item);
                }
                catch (Exception ex)
                {
                    return MessageReturn.CreateErrorMessage("UPDATE FAIL");
                }
            }
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn InsertKetQuaXuLy(KetQuaXuLyTaiSanModel model)
        {
            var tsxl = _taiSanTdXuLyService.GetTaiSanTdXuLyByDB_ID(model.DB_TAI_SAN_TD_XU_LY_ID);
            if (tsxl == null || tsxl.xuly.TRANG_THAI_ID != (int)enumTrangThaiXuLy.TonTai)
            {
                return MessageReturn.CreateErrorMessage("DB_TAI_SAN_TD_XU_LY_ID INVALID");
            }
            if (_ketQuaService.GetKetQuaByDB_ID(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            //check xem tồn tại hình thức xử lý không
            var htxl = _hinhThucXuLyService.GetHinhThucXuLys(PhuongAnId: (int)tsxl.PHUONG_AN_XU_LY_ID).Where(c => c.ID == model.HINH_THUC_XU_LY_ID).FirstOrDefault();
            if (htxl == null)
            {
                return MessageReturn.CreateErrorMessage("HINH_THUC_XU_LY_ID INVALID");

            }
            //update tài sản xử lý
            tsxl.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            _taiSanTdXuLyService.UpdateTaiSanTdXuLy(tsxl);
            //insert kết quả
            var item = new KetQua();
            item.TAI_SAN_TD_XU_LY_ID = tsxl.ID;
            item.CHI_PHI_XU_LY = model.CHI_PHI_XU_LY;
            item.DON_VI_CHUYEN_ID = model.DON_VI_CHUYEN_ID;
            item.GIA_TRI_BAN = model.GIA_TRI_BAN;
            item.GIA_TRI_NSNN = model.GIA_TRI_NSNN;
            item.GIA_TRI_TKTG = model.GIA_TRI_TKTG;
            item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.HOP_DONG_SO = model.HOP_DONG_SO;
            item.HO_SO_GIAY_TO_KHAC = model.HO_SO_GIAY_TO_KHAC;
            item.NGAY_XU_LY = model.NGAY_XU_LY;
            item.DB_ID = model.DB_ID;
            item.DB_TAI_SAN_XU_LY_ID = model.DB_TAI_SAN_TD_XU_LY_ID;
            _ketQuaService.InsertKetQua(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn UpdateKetQuaXuLy(KetQuaXuLyTaiSanModel model)
        {
            var item = _ketQuaService.GetKetQuaByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            var tsxl = _taiSanTdXuLyService.GetTaiSanTdXuLyByDB_ID(model.DB_TAI_SAN_TD_XU_LY_ID);
            if (tsxl == null || tsxl.xuly.TRANG_THAI_ID != (int)enumTrangThaiXuLy.TonTai)
            {
                return MessageReturn.CreateErrorMessage("DB_TAI_SAN_TD_XU_LY_ID INVALID");
            }
            //check xem tồn tại hình thức xử lý không
            var htxl = _hinhThucXuLyService.GetHinhThucXuLys(PhuongAnId: (int)tsxl.PHUONG_AN_XU_LY_ID).Where(c => c.ID == model.HINH_THUC_XU_LY_ID).FirstOrDefault();
            if (htxl == null)
            {
                return MessageReturn.CreateErrorMessage("HINH_THUC_XU_LY_ID INVALID");

            }
            //update tài sản xử lý
            tsxl.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            _taiSanTdXuLyService.UpdateTaiSanTdXuLy(tsxl);
            //insert kết quả
            item.TAI_SAN_TD_XU_LY_ID = tsxl.ID;
            item.CHI_PHI_XU_LY = model.CHI_PHI_XU_LY;
            item.DON_VI_CHUYEN_ID = model.DON_VI_CHUYEN_ID;
            item.GIA_TRI_BAN = model.GIA_TRI_BAN;
            item.GIA_TRI_NSNN = model.GIA_TRI_NSNN;
            item.GIA_TRI_TKTG = model.GIA_TRI_TKTG;
            item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.HOP_DONG_SO = model.HOP_DONG_SO;
            item.HO_SO_GIAY_TO_KHAC = model.HO_SO_GIAY_TO_KHAC;
            item.NGAY_XU_LY = model.NGAY_XU_LY;
            item.SO_LUONG = model.SO_LUONG;
            item.DB_ID = model.DB_ID;
            item.DB_TAI_SAN_XU_LY_ID = model.DB_TAI_SAN_TD_XU_LY_ID;
            _ketQuaService.UpdateKetQua(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeleteKetQuaXuLy(KetQuaXuLyTaiSanModel model)
        {
            var item = _ketQuaService.GetKetQuaByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            _ketQuaService.DeleteKetQua(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn InsertThuChi(ThuChiModel model)
        {
            var xl = _xuLyService.GetXuLyByDB_Id(model.DB_XU_LY_ID);
            if (xl == null || xl.TRANG_THAI_ID != (int)enumTrangThaiXuLy.TonTai)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            if (_thuChiService.GetThuChiByDB_ID(model.DB_ID) != null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID ĐÃ TỒN TẠI");
            }
            // check xem tồn tại trong bảng kết quả 
            var tsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: xl.ID).Select(c => c.ID).ToList();
            var ketqua = _ketQuaService.GetKetQuaBys(ListTaiSanTDXuLy: tsxl);
            if (ketqua == null || ketqua.Count() == 0)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            var item = new ThuChi();
            item.CHI_PHI = model.CHI_PHI;
            item.KET_QUA_ID = model.KET_QUA_ID;
            item.NGAY_THU = model.NGAY_THU;
            item.SO_TIEN_PHAI_THU = model.SO_TIEN_PHAI_THU;
            item.SO_TIEN_CON_PHAI_THU = model.SO_TIEN_CON_PHAI_THU;
            item.SO_TIEN_THU = model.SO_TIEN_THU;
            item.TG_NGAY_NOP = model.TG_NGAY_NOP;
            item.TG_SO_TIEN = model.TG_SO_TIEN;
            item.XU_LY_ID = xl.ID;
            item.DB_ID = model.DB_ID;
            item.DB_XU_LY_ID = model.DB_XU_LY_ID;
            _thuChiService.InsertThuChi(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn UpdateThuChi(ThuChiModel model)
        {
            var item = _thuChiService.GetThuChiByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            var xl = _xuLyService.GetXuLyByDB_Id(model.DB_XU_LY_ID);
            if (xl == null || xl.TRANG_THAI_ID != (int)enumTrangThaiXuLy.TonTai)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            var tsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: xl.ID).Select(c => c.ID).ToList();
            var ketqua = _ketQuaService.GetKetQuaBys(ListTaiSanTDXuLy: tsxl);
            if (ketqua == null || ketqua.Count() == 0)
            {
                return MessageReturn.CreateErrorMessage("DB_XU_LY_ID INVALID");
            }
            item.CHI_PHI = model.CHI_PHI;
            item.KET_QUA_ID = model.KET_QUA_ID;
            item.NGAY_THU = model.NGAY_THU;
            item.SO_TIEN_PHAI_THU = model.SO_TIEN_PHAI_THU;
            item.SO_TIEN_CON_PHAI_THU = model.SO_TIEN_CON_PHAI_THU;
            item.SO_TIEN_THU = model.SO_TIEN_THU;
            item.TG_NGAY_NOP = model.TG_NGAY_NOP;
            item.TG_SO_TIEN = model.TG_SO_TIEN;
            item.XU_LY_ID = xl.ID;
            item.DB_ID = model.DB_ID;
            item.DB_XU_LY_ID = model.DB_XU_LY_ID;
            _thuChiService.UpdateThuChi(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        public MessageReturn DeleteThuChi(ThuChiModel model)
        {
            var item = _thuChiService.GetThuChiByDB_ID(model.DB_ID);
            if (item == null)
            {
                return MessageReturn.CreateErrorMessage("DB_ID INVALID");
            }
            _thuChiService.DeleteThuChi(item);
            return MessageReturn.CreateSuccessMessage("UPDATE SUCCESS");
        }
        #endregion
        #endregion
        #region KhaiThacTaiSan
        public bool CheckTonTaiDonVi(decimal DonViId)
        {
            DonVi donVi = new DonVi();

            donVi = _donViService.GetDonViById(DonViId);
            if (donVi == null)
            {
                return false;
            }

            return true;
        }
        public MessageReturn UpdateKhaiThacTaiSan(DBKhaiThacTaiSanModel model)
        {
            KhaiThac item = _khaiThacService.GetKhaiThacById(model.ID);
            if (item == null)
                item = new KhaiThac();
            #region preare khai thac
            item.NGAY_KHAI_THAC = model.NGAY_KHAI_THAC.toDateSys(CommonHelper.DATE_FORMAT_DB).Value;
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY.toDateSys(CommonHelper.DATE_FORMAT_DB).Value;
            item.NGUOI_DUYET = model.NGUOI_DUYET;
            item.SO_TIEN_TAM_TINH = model.SO_TIEN_TAM_TINH.Value;
            item.KHAI_THAC_NGAY_TU = model.KHAI_THAC_NGAY_TU.toDateSys(CommonHelper.DATE_FORMAT_DB);
            item.KHAI_THAC_NGAY_DEN = model.KHAI_THAC_NGAY_DEN.toDateSys(CommonHelper.DATE_FORMAT_DB);
            item.DON_VI_ID = model.DON_VI_ID;
            item.LOAI_HINH_KHAI_THAC_ID = model.LOAI_HINH_KHAI_THAC_ID;
            item.NOI_DUNG = model.NOI_DUNG;
            item.GHI_CHU = model.GHI_CHU;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            // chờ đồng bộ --hungnt
            //item.DOI_TAC_ID = model.DOI_TAC_ID;
            item.CHO_THUE_GIA = model.CHO_THUE_GIA;
            item.CHO_THUE_PHUONG_THUC_ID = model.CHO_THUE_PHUONG_THUC_ID;
            item.LDLK_HINH_THUC_ID = model.LDLK_HINH_THUC_ID;
            item.NGAY_TAO = item.NGAY_TAO ?? DateTime.Now;
            item.DB_ID = model.DB_ID;
            if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK || model.LOAI_HINH_KHAI_THAC_ID == 0/*cập nhật số tiền khai thác*/)
            {
                item.HOP_DONG_SO = model.HOP_DONG_SO;
                if (!string.IsNullOrEmpty(model.HOP_DONG_NGAY))
                    item.HOP_DONG_NGAY = model.HOP_DONG_NGAY.toDateSys(CommonHelper.DATE_FORMAT_DB);
            }
            #endregion
            if (item.ID == 0)
            {
                _khaiThacService.InsertKhaiThac(item);
                model.ID = (long)item.ID;
            }
            else
                _khaiThacService.UpdateKhaiThac(item);
            model.ID = (int)item.ID;
            foreach (var ts in model.LST_MA_TAI_SAN_DB)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(ts, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
                KhaiThacTaiSan ktts = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(taiSan.ID, item.ID);
                if (ktts == null)
                    ktts = new KhaiThacTaiSan();
                ktts.KHAI_THAC_ID = item.ID;
                ktts.TAI_SAN_ID = taiSan.ID;
                ktts.DIEN_TICH_KHAI_THAC = model.DIEN_TICH_KHAI_THAC;
                if (ktts != null && ktts.ID > 0)
                    _khaiThacTaiSanService.UpdateKhaiThacTaiSan(ktts);
                else
                    _khaiThacTaiSanService.InsertKhaiThacTaiSan(ktts);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        #endregion
        #region Hao mòn- khấu hao tài sản
        public MessageReturn UpdateListHaoMonTaiSan(List<HaoMonDBModel> ListModel, NguoiDung currentUser)
        {
            decimal nguontaisan = 0;
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameKhoCSDL)
            {
                nguontaisan = (decimal)enumNguonTaiSan.CSDLQGTSC;
            }
            else
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameQLTSNN)
            {
                nguontaisan = (decimal)enumNguonTaiSan.QLTSNN;
            }
            else
            {
                nguontaisan = (decimal)enumNguonTaiSan.KHAC;
            }
            foreach (var item in ListModel)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(item.MA_TAI_SAN_DB, NguonTaiSanId: nguontaisan);
                List<string> ListError = new List<string>();
                if (taiSan == null)
                {
                    ListError.Add(String.Format("MA_TAI_SAN_DB {0} không tồn tại", item.MA_TAI_SAN_DB));
                }
                if (ListError != null && ListError.Count > 0)
                    return MessageReturn.CreateErrorMessage("Lỗi", ListError);
            }
            foreach (var item in ListModel)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(item.MA_TAI_SAN_DB, NguonTaiSanId: nguontaisan);
                HaoMonTaiSan haoMonTaiSan = new HaoMonTaiSan();
                if (item.ID > 0)
                {
                    haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanById(item.ID);
                    if (haoMonTaiSan != null)
                    {
                        haoMonTaiSan = item.ToEntity(haoMonTaiSan);

                        haoMonTaiSan.TAI_SAN_ID = taiSan.ID;
                        haoMonTaiSan.MA_TAI_SAN = taiSan.MA;
                        _haoMonTaiSanService.UpdateHaoMonTaiSan(haoMonTaiSan);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    haoMonTaiSan = item.ToEntity<HaoMonTaiSan>();
                    haoMonTaiSan.TAI_SAN_ID = taiSan.ID;
                    haoMonTaiSan.MA_TAI_SAN = taiSan.MA;
                    _haoMonTaiSanService.InsertHaoMonTaiSan(haoMonTaiSan);
                    item.ID = (long)haoMonTaiSan.ID;
                }
            }
            return MessageReturn.CreateSuccessMessage("Done", ListModel);
        }
        public MessageReturn DeleteHaoMonTaiSan(List<decimal> ListId)
        {
            try
            {
                var listHaoMon = _haoMonTaiSanService.GetHaoMonTaiSanByIds(ListId.ToArray());
                foreach (var item in listHaoMon)
                {
                    _haoMonTaiSanService.DeleteHaoMonTaiSan(item);
                }
            }
            catch (Exception ex)
            {

            }
            return MessageReturn.CreateSuccessMessage("Done");
        }
        public MessageReturn UpdateListKhauHaoTaiSan(List<KhauHaoDBModel> ListModel, NguoiDung currentUser)
        {
            decimal nguontaisan = 0;
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameKhoCSDL)
            {
                nguontaisan = (decimal)enumNguonTaiSan.CSDLQGTSC;
            }
            else
            if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameQLTSNN)
            {
                nguontaisan = (decimal)enumNguonTaiSan.QLTSNN;
            }
            else
            {
                nguontaisan = (decimal)enumNguonTaiSan.KHAC;
            }
            foreach (var item in ListModel)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(item.MA_TAI_SAN_DB, NguonTaiSanId: nguontaisan);
                List<string> ListError = new List<string>();
                if (taiSan == null)
                {
                    ListError.Add(String.Format("MA_TAI_SAN_DB {0} không tồn tại", item.MA_TAI_SAN_DB));
                }
                if (ListError != null && ListError.Count > 0)
                    return MessageReturn.CreateErrorMessage("Lỗi", ListError);
            }
            foreach (var item in ListModel)
            {
                KhauHaoTaiSan khauHaoTaiSan = new KhauHaoTaiSan();
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(item.MA_TAI_SAN_DB, NguonTaiSanId: nguontaisan);
                if (item.ID > 0)
                {
                    khauHaoTaiSan = _khauHaoTaiSanService.GetKhauHaoTaiSanById(item.ID);
                    if (khauHaoTaiSan != null)
                    {
                        khauHaoTaiSan = item.ToEntity(khauHaoTaiSan);

                        khauHaoTaiSan.TAI_SAN_ID = taiSan.ID;
                        khauHaoTaiSan.MA_TAI_SAN = taiSan.MA;
                        _khauHaoTaiSanService.UpdateKhauHaoTaiSan(khauHaoTaiSan);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    khauHaoTaiSan = item.ToEntity<KhauHaoTaiSan>();
                    khauHaoTaiSan.TAI_SAN_ID = taiSan.ID;
                    khauHaoTaiSan.MA_TAI_SAN = taiSan.MA;
                    _khauHaoTaiSanService.InsertKhauHaoTaiSan(khauHaoTaiSan);
                    item.ID = (long)khauHaoTaiSan.ID;
                }
            }
            return MessageReturn.CreateSuccessMessage("Done", ListModel);
        }
        public MessageReturn DeleteKhauHaoTaiSan(List<decimal> ListId)
        {
            try
            {
                var listKhauHao = _khauHaoTaiSanService.GetKhauHaoTaiSanByIds(ListId.ToArray());
                foreach (var item in listKhauHao)
                {
                    _khauHaoTaiSanService.DeleteKhauHaoTaiSan(item);
                }
            }
            catch (Exception ex)
            {

            }
            return MessageReturn.CreateSuccessMessage("Done");
        }
        #endregion
        #region Validate Thong in dong bo
        public MessageReturn ValidateThongTinTaiSan(TaiSanDBModel dbModel)
        {
            #region Thông tin chung
            decimal? DonViId = null;
            LoaiTaiSan loaiTaiSan = new LoaiTaiSan();
            decimal LoaiHinhTaiSanId = 0;
            if (string.IsNullOrEmpty(dbModel.DB_MA))
                return MessageReturn.CreateNotFoundMessage("DB_MA null");
            if (string.IsNullOrEmpty(dbModel.TEN))
            {
                return MessageReturn.CreateNotFoundMessage("TEN null");
            }
            else
            {
                foreach (var item in dbModel.LST_BIEN_DONG)
                {
                    if (string.IsNullOrEmpty(item.TEN_TAI_SAN))
                    {
                        item.TEN_TAI_SAN = dbModel.TEN;
                    }
                    if (!(item.LOAI_TAI_SAN_ID > 0))
                    {
                        item.LOAI_TAI_SAN_ID = dbModel.LOAI_TAI_SAN_ID;
                    }
                }
            }

            if (dbModel.LOAI_TAI_SAN_ID > 0)
            {
                loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(dbModel.LOAI_TAI_SAN_ID.Value);
                dbModel.LOAI_TAI_SAN_MA = loaiTaiSan.MA;
                LoaiHinhTaiSanId = loaiTaiSan.LOAI_HINH_TAI_SAN_ID.Value;
                dbModel.LOAI_HINH_TAI_SAN_ID = LoaiHinhTaiSanId;
            }
            else dbModel.LOAI_TAI_SAN_ID = null;
            if (dbModel.LOAI_TAI_SAN_DON_VI_ID > 0)
            {
                LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViService.GetLoaiTaiSanVoHinhById(dbModel.LOAI_TAI_SAN_DON_VI_ID.Value);
                dbModel.LOAI_TAI_SAN_MA = loaiTaiSanDonVi.MA;
                LoaiHinhTaiSanId = loaiTaiSanDonVi.LOAI_HINH_TAI_SAN_ID.Value;
                dbModel.LOAI_HINH_TAI_SAN_ID = LoaiHinhTaiSanId;
            }
            else dbModel.LOAI_TAI_SAN_DON_VI_ID = null;
            if (String.IsNullOrEmpty(dbModel.NGAY_SU_DUNG))
                return MessageReturn.CreateNotFoundMessage("NGAY_SU_DUNG null");
            //if (String.IsNullOrEmpty(dbModel.NGAY_DANG_KY))
            //    return MessageReturn.CreateNotFoundMessage("NGAY_DANG_KY null");
            if (string.IsNullOrEmpty(dbModel.MA_DON_VI))
            {
                return MessageReturn.CreateNotFoundMessage("MA_DON_VI null");
            }
            else
            {
                DonVi donVi = _donViService.GetDonViByMa(dbModel.MA_DON_VI);
                if (donVi == null)
                {
                    return MessageReturn.CreateErrorMessage("MA_DON_VI not exist");
                }
                DonViId = donVi.ID;
            }
            if (dbModel.LST_BIEN_DONG.Count == 0)
            {
                return MessageReturn.CreateNotFoundMessage("LST_BIEN_DONG null");
            }
            // đơn vị bộ phận
            if (dbModel.DON_VI_BO_PHAN_ID > 0)
            {
                DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(dbModel.DON_VI_BO_PHAN_ID.Value);
                if (donViBoPhan == null)

                {
                    return MessageReturn.CreateNotFoundMessage("DON_VI_BO_PHAN_ID not exist");
                }
            }
            #endregion
            #region thông tin từng loại tài sản
            if (dbModel.TS_CLN == null && dbModel.TS_DAC_THU == null && dbModel.TS_DAT == null && dbModel.TS_NHA == null && dbModel.TS_MAY_MOC == null && dbModel.TS_HUU_HINH_KHAC == null && dbModel.TS_OTO == null && dbModel.TS_PTK == null && dbModel.TS_VO_HINH == null && dbModel.TS_KHAC == null && dbModel.TS_VKT == null)
            {
                return MessageReturn.CreateNotFoundMessage("TAI_SAN null");
            }
            switch (LoaiHinhTaiSanId)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        if (dbModel.TS_DAT != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_DAT.DIA_CHI))
                                return MessageReturn.CreateNotFoundMessage("TS_DAT.DIA_CHI null");
                            if (!(dbModel.TS_DAT.DIEN_TICH > 0))
                                return MessageReturn.CreateErrorMessage("TS_DAT.DIEN_TICH null");
                            if (dbModel.TS_DAT.DIA_BAN_ID > 0)
                            {
                                var diaban = _diaBanService.GetDiaBanById(dbModel.TS_DAT.DIA_BAN_ID.Value);
                                if (diaban == null)
                                {
                                    return MessageReturn.CreateErrorMessage("TS_DAT.DIEN_TICH null");
                                }
                            }

                        }
                        else
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        if (dbModel.TS_NHA != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_NHA.DIA_CHI))
                                return MessageReturn.CreateNotFoundMessage("TS_NHA.DIA_CHI null");
                            if (!dbModel.TS_NHA.NAM_XAY_DUNG.HasValue || (dbModel.TS_NHA.NAM_XAY_DUNG.HasValue && dbModel.TS_NHA.NAM_XAY_DUNG == 0))
                                return MessageReturn.CreateErrorMessage("TS_NHA.NAM_XAY_DUNG null");
                            if (!dbModel.TS_NHA.NHA_SO_TANG.HasValue || (dbModel.TS_NHA.NHA_SO_TANG.HasValue && dbModel.TS_NHA.NHA_SO_TANG == 0))
                                return MessageReturn.CreateErrorMessage("TS_NHA.NHA_SO_TANG null");
                            if (!string.IsNullOrEmpty(dbModel.TS_NHA.TAI_SAN_DAT_MA))
                            {
                                TaiSan _taiSanDat = _taiSanService.GetTaiSanByMa(dbModel.TS_NHA.TAI_SAN_DAT_MA);
                                if (_taiSanDat == null)
                                {
                                    return MessageReturn.CreateErrorMessage("TS_NHA.TAI_SAN_DAT_MA not exist");
                                }
                            }
                        }
                        else
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        if (dbModel.TS_OTO != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_OTO.BIEN_KIEM_SOAT))
                                return MessageReturn.CreateNotFoundMessage("TS_OTO.BIEN_KIEM_SOAT null");
                            if (dbModel.TS_OTO.SO_CHO_NGOI > 0 && dbModel.TS_OTO.TAI_TRONG > 0)
                            {
                                return MessageReturn.CreateErrorMessage("TS_OTO.SO_CHO_NGOI - TS_OTO.TAI_TRONG invalid");
                            }
                        }
                        else
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    {
                        if (dbModel.TS_PTK != null)
                        {
                            if (dbModel.TS_PTK.SO_CHO_NGOI > 0 && dbModel.TS_PTK.TAI_TRONG > 0)
                            {
                                return MessageReturn.CreateErrorMessage("TS_PTK.SO_CHO_NGOI - TS_PTK.TAI_TRONG invalid");
                            }
                        }
                        else
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    {
                        if (dbModel.TS_VKT == null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    {
                        if (dbModel.TS_CLN != null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông thin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    {
                        if (dbModel.TS_MAY_MOC != null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông thin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    {
                        if (dbModel.TS_DAC_THU != null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông thin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    {
                        if (dbModel.TS_HUU_HINH_KHAC != null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông thin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    {
                        if (dbModel.TS_VO_HINH != null)
                        {
                            return MessageReturn.CreateErrorMessage("Loại tài sản và thông thin tài sản không đúng");
                        }
                        break;
                    }
                default:
                    break;
            }
            #endregion

            return MessageReturn.CreateSuccessMessage("done");
        }
        public MessageReturn ValidateThongTinBienDong(BienDongDBModel model)
        {
            MessageReturn result;
            decimal LoaiHinhTaiSanId = 0;
            if (string.IsNullOrEmpty(model.ID_DB))
            {
                return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-ID_DB null");
            }
            if (string.IsNullOrEmpty(model.NGAY_BIEN_DONG))
            {
                return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-NGAY_BIEN_DONG null");
            }
            if (!(model.LOAI_BIEN_DONG_ID > 0))
            {
                return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LOAI_BIEN_DONG_ID null");
            }
            if (model.LOAI_TAI_SAN_ID > 0)
            {
                decimal LoaiTaiSanId = model.LOAI_TAI_SAN_ID.Value;
                var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSanId);
                if (loaiTaiSan == null)
                {
                    return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LOAI_TAI_SAN_ID not exist");
                }
                else
                {
                    if (!(loaiTaiSan.LOAI_HINH_TAI_SAN_ID > 0))
                    {
                        return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LOAI_TAI_SAN_ID not exist");
                    }
                    LoaiHinhTaiSanId = loaiTaiSan.LOAI_HINH_TAI_SAN_ID.Value;
                    model.LOAI_TAI_SAN_ID = LoaiTaiSanId;
                }
            }
            else model.LOAI_TAI_SAN_ID = null;
            if (model.LOAI_TAI_SAN_DON_VI_ID > 0)
            {
                var loaiTaiSanDonVi = _loaiTaiSanDonViService.GetLoaiTaiSanVoHinhById(model.LOAI_TAI_SAN_DON_VI_ID.Value);
                LoaiHinhTaiSanId = loaiTaiSanDonVi.LOAI_HINH_TAI_SAN_ID.Value;
            }
            else model.LOAI_TAI_SAN_DON_VI_ID = null;
            if (string.IsNullOrEmpty(model.TEN_TAI_SAN))
            {
                if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
                {
                    model.TEN_TAI_SAN = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN).TEN;
                }
            }
            if (!(model.LY_DO_BIEN_DONG_ID > 0))
            {
                return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LY_DO_BIEN_DONG_ID null");
            }
            else
            {
                decimal LyDoBienDongId = model.LY_DO_BIEN_DONG_ID.Value;
                LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(LyDoBienDongId);
                if (lyDoBienDong == null)
                {
                    return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LY_DO_BIEN_DONG_ID not exist");
                }
                model.LY_DO_BIEN_DONG_ID = LyDoBienDongId;
                model.LY_DO_BIEN_DONG_MA = lyDoBienDong.MA;
            }
            if (string.IsNullOrEmpty(model.NGAY_DUYET))
            {
                return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-NGAY_DUYET null");
            }

            if (string.IsNullOrEmpty(model.ID_DB))
            {
                return MessageReturn.CreateNotFoundMessage("ID_DB null");
            }
            List<decimal> ListHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTaiSanId)?.Select(c => c.ID).ToList();
            if (ListHienTrang != null)
            {
                var lst = model.LST_HIEN_TRANG.Where(c => !ListHienTrang.Contains(c.HIEN_TRANG_ID.GetValueOrDefault(0))).Select(x => x.HIEN_TRANG_ID).ToList();
                if (lst != null && lst.Count > 0)
                    return MessageReturn.CreateNotFoundMessage($"BIEN_DONG:ID_DB= { model.ID_DB}-LST_HIEN_TRANG ({String.Join(",", lst)}) NOT EXISTS WHEN {(model.LOAI_TAI_SAN_ID > 0 ? "LOAI_TAI_SAN_ID" : "LOAI_TAI_SAN_DON_VI_ID")} = {(model.LOAI_TAI_SAN_ID ?? model.LOAI_TAI_SAN_DON_VI_ID)}");

            }
            result = ValidateNguonVon(model, LoaiHinhTaiSanId);

            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn ValidateNguonVon(BienDongDBModel model, decimal LoaiHinhTaiSanId)
        {
            if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                if (model.NV_HDSN > 0 || model.NV_NGUON_KHAC > 0 || model.NV_VIEN_TRO > 0)
                {
                    return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB} -Tài sản đất chỉ có nguồn vốn ngân sách");
                }
            }
            decimal TongNguonVon = model.NV_VIEN_TRO.GetValueOrDefault(0) + model.NV_NGUON_KHAC.GetValueOrDefault(0) + model.NV_HDSN.GetValueOrDefault(0) + model.NV_NGAN_SACH.GetValueOrDefault(0);
            if (!(TongNguonVon > 0))
            {
                return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB} -Nguồn vốn invalid");
            }
            else
            {
                if (TongNguonVon != model.NGUYEN_GIA)
                {
                    return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB} -Nguồn vốn phải bằng nguyên giá");
                }
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn ValidateHienTrang(BienDongDBModel model, decimal LoaiHinhTaiSanId, decimal TongGiaTriTruocBienDong = 0, string MaTaiSan = null, bool IsGiam = false)
        {
            switch (LoaiHinhTaiSanId)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
                        {
                            decimal TaiSanId = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN).ID;
                            List<decimal> bdIds = _bienDongService.GetBienDongsByTaiSanId(taiSanId: TaiSanId, NgayBDDen: model.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB)).Select(c => c.ID).ToList();
                            if (bdIds != null && bdIds.Count() > 0)
                                TongGiaTriTruocBienDong += _bienDongChiTietService.GetBienDongChiTietByBDIds(bdIds).Sum(c => c.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                        }
                        decimal TongHienTrang = model.LST_HIEN_TRANG.Sum(m => m.GIA_TRI_NUMBER.GetValueOrDefault(0));
                        decimal TongHienTrangBienDong = 0;
                        if (IsGiam)
                        {
                            TongHienTrangBienDong = TongGiaTriTruocBienDong - model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                        }
                        else
                        {
                            TongHienTrangBienDong = model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0) + TongGiaTriTruocBienDong;
                        }
                        if (TongHienTrang != TongHienTrangBienDong)
                        {
                            return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB} - Tổng DAT_TONG_DIEN_TICH các biến động trước phải bằng tổng hiện trạng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
                        {
                            decimal TaiSanId = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN).ID;
                            List<decimal> bdIds = _bienDongService.GetBienDongsByTaiSanId(taiSanId: TaiSanId, NgayBDDen: model.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB)).Select(c => c.ID).ToList();
                            if (bdIds != null && bdIds.Count() > 0)
                                TongGiaTriTruocBienDong += _bienDongChiTietService.GetBienDongChiTietByBDIds(bdIds).Sum(c => c.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
                        }
                        decimal TongHienTrang = model.LST_HIEN_TRANG.Sum(m => m.GIA_TRI_NUMBER.GetValueOrDefault(0));
                        decimal TongHienTrangBienDong = 0;
                        if (IsGiam)
                        {
                            TongHienTrangBienDong = TongGiaTriTruocBienDong - model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                        }
                        else
                        {
                            TongHienTrangBienDong = model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0) + TongGiaTriTruocBienDong;
                        }
                        if (TongHienTrang != TongHienTrangBienDong)
                        {
                            return MessageReturn.CreateErrorMessage($"BIEN_DONG:ID_DB= { model.ID_DB} - Tổng NHA_TONG_DIEN_TICH_XD các biến động trước phải bằng tổng hiện trạng sử dụng");
                        }
                        break;
                    }

            }

            return MessageReturn.CreateSuccessMessage("Success done");
        }
        #endregion
        public TaiSan InsertTaiSanTemp(TaiSanDBModel dbModel, NguoiDung currentUser, decimal nguontaisan)
        {
            DBTaiSan _dBTaiSan = _dbTaiSanService.GetTaiSanByMa(DB_MA: dbModel.DB_MA, nguonTaiSanId: nguontaisan);
            DBTaiSan dBTaiSan = new DBTaiSan();
            string MaLoaiTaiSan = null;
            if (dbModel.LOAI_TAI_SAN_ID > 0)
            {
                LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(dbModel.LOAI_TAI_SAN_ID.Value);
                MaLoaiTaiSan = loaiTaiSan != null ? loaiTaiSan.MA : null;
            }
            else if (dbModel.LOAI_TAI_SAN_DON_VI_ID > 0)
            {
                LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViService.GetLoaiTaiSanVoHinhById(dbModel.LOAI_TAI_SAN_DON_VI_ID.Value);
                MaLoaiTaiSan = loaiTaiSanDonVi != null ? loaiTaiSanDonVi.MA : null;
            }
            if (_dBTaiSan != null)
            {
                _dbTaiSanService.DeleteTaiSan(_dBTaiSan);
            }
            foreach (var item in dbModel.LST_BIEN_DONG)
            {
                item.GUID = Guid.NewGuid().ToString();
            }
            dBTaiSan.PHAN_MEM_DONG_BO_ID = nguontaisan;
            dBTaiSan.DATA_JSON = dbModel.toStringJson(isIgnoreNull: true);
            dBTaiSan.IS_TAI_SAN_TOAN_DAN = false;
            dBTaiSan.IS_BIEN_DONG = false;
            dBTaiSan.DB_MA = dbModel.DB_MA;
            dBTaiSan.LOAI_TAI_SAN_ID = dbModel.LOAI_TAI_SAN_ID > 0 ? dbModel.LOAI_TAI_SAN_ID : null;
            dBTaiSan.LOAI_TAI_SAN_DON_VI_ID = dbModel.LOAI_TAI_SAN_DON_VI_ID > 0 ? dbModel.LOAI_TAI_SAN_DON_VI_ID : null;
            dBTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.ChuaInsert;
            _dbTaiSanService.InsertTaiSan(dBTaiSan);
            // insert tài sản chờ đồng bộ để lấy mã và guid tài sản.
            TaiSan taiSan = new TaiSan();
            taiSan.TEN = dbModel.TEN;
            taiSan.MA_BO = dbModel.MA_DON_VI.Substring(0,3);
            _taiSanService.InsertTaiSan(taiSan, true);
            taiSan.MA = CommonHelper.GenMaTaiSan(dbModel.MA_DON_VI, MaLoaiTaiSan, taiSan.ID);
            taiSan.MA_DB = dbModel.NGUON_TAI_SAN_ID == 2 ? dbModel.DB_MA : null;
            taiSan.MA_QLDKTS40 = dbModel.NGUON_TAI_SAN_ID != 2 ? dbModel.DB_MA : null;
            taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DONG_BO;
            taiSan.NGUON_TAI_SAN_ID = dbModel.NGUON_TAI_SAN_ID;
            _taiSanService.UpdateTaiSan(taiSan);
            dBTaiSan.QLDKTS_MA = taiSan.MA;
            dBTaiSan.GUID = taiSan.GUID;
            dbModel.ID = (long)taiSan.ID;
            dBTaiSan.DATA_JSON = dbModel.toStringJson(isIgnoreNull: true);
            _dbTaiSanService.UpdateTaiSan(dBTaiSan);
            // insert log
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới giữ liệu đồng bộ t", 0, "DB_TAI_SAN", dBTaiSan);
            return taiSan;
        }

        public MessageReturn UpdateLogsTaiSanDongBo(LogsDongBoCsdlqgModel model)
        {
            try
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(model.MA_QLTSC);
                if (taiSan == null)
                    return MessageReturn.CreateErrorMessage("Mã tài sản không tồn tại", model);
                LogsDongBoCsdlqg item = _logsDongBoCsdlqgService.GetAllLogsDongBoCsdlqgs(model.UUID, model.MA_QLTSC).FirstOrDefault();
                if (item == null)
                {
                    item = new LogsDongBoCsdlqg()
                    {
                        MA_CSDLQG = model.MA_CSDLQG,
                        MA_QLTSC = model.MA_QLTSC,
                        MO_TA = model.MO_TA,
                        UUID = model.UUID,
                        TRANG_THAI = model.MA_QLTSC == "0" ? (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.LOI_CAU_TRUC : (String.IsNullOrEmpty(model.MA_CSDLQG) ? (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.LOI : (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.THANH_CONG)
                    };
                    _logsDongBoCsdlqgService.InsertLogsDongBoCsdlqg(item);
                }
                else
                {
                    item.MA_CSDLQG = model.MA_CSDLQG;
                    item.MA_QLTSC = model.MA_QLTSC;
                    item.MO_TA = model.MO_TA;
                    item.UUID = model.UUID;
                    item.TRANG_THAI = model.MA_QLTSC == "0" ? (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.LOI_CAU_TRUC : (String.IsNullOrEmpty(model.MA_CSDLQG) ? (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.LOI : (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.THANH_CONG);
                    _logsDongBoCsdlqgService.UpdateLogsDongBoCsdlqg(item);
                }

                BienDong bienDongTangMoi = _bienDongService.GetBienDongsByTaiSanId(taiSan.ID).Where(c => c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).FirstOrDefault();

                if (item.TRANG_THAI != (Decimal)enumLOGS_DONG_BO_CSDLQG_TRANG_THAI.THANH_CONG)
                {
                    if (bienDongTangMoi != null)
                    {
                        bienDongTangMoi.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.THAT_BAI;
                        _bienDongService.UpdateBienDong(bienDongTangMoi);
                    }
                    // cập nhật trạng thái tài sản
                    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                    if (taiSanNhatKy == null)
                    {
                        return MessageReturn.CreateErrorMessage("Nhật ký không tồn tại", item);
                    }
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS, idAdd: new List<decimal>() { bienDongTangMoi.ID });
                    taiSanNhatKy.BD_IDS_DANG_DB = null;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
                else
                {
                    if (bienDongTangMoi != null)
                    {
                        bienDongTangMoi.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DA_DONG_BO;
                        _bienDongService.UpdateBienDong(bienDongTangMoi);
                    }
                    taiSan.MA_DB = item.MA_CSDLQG;
                    _taiSanService.UpdateTaiSan(taiSan);
                    //Insert hoạt động
                    _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ biến động thành công, Update mã tài sản kho", taiSan, "TaiSan");
                    // cập nhật trạng thái tài sản
                    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(taiSan.ID);
                    if (taiSanNhatKy == null)
                    {
                        return MessageReturn.CreateErrorMessage("Nhật ký không tồn tại", item);
                    }
                    taiSanNhatKy.TRANG_THAI_ID = taiSanNhatKy.BD_IDS != null ? (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI : (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: taiSanNhatKy.BD_IDS, idDel: new List<decimal>() { bienDongTangMoi.ID });
                    taiSanNhatKy.BD_IDS_DANG_DB = null;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
            }
            catch (Exception e)
            {
                return MessageReturn.CreateErrorMessage(e.Message, model);
            }
            return MessageReturn.CreateSuccessMessage("Done", model);
        }
        #endregion
    }
}
