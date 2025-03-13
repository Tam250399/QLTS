using GS.Core;
using GS.Core.Domain.Api;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSan;
using GS.WebApi.Models.TaiSanXacLap;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace GS.WebApi.SoapServices
{
    public class TaiSanSvc : ITaiSanSvc
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
        private readonly IHinhThucXuLyService hinhThucXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        public TaiSanSvc(
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
            INguonGocTaiSanService nguonGocTaiSanService
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
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._taiSanTdService = taiSanTdService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this.hinhThucXuLyService = hinhThucXuLyService;
            this._donViService = donViService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._taiSanService = taiSanService;

        }
        #endregion
        #region Method
        #region TaiSan
        public TaiSanDBModel PrepareTaiSanDBModel(DBTaiSanModel model)
        {
            TaiSanDBModel dbModel = new TaiSanDBModel()
            {
                MA = model.QLDKTS_MA,
                //LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID,
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
                //dbModel.NGAY_NHAP = ts.NGAY_NHAP;
               // dbModel.NGAY_SU_DUNG = ts.NGAY_SU_DUNG;
                //dbModel.LY_DO_BIEN_DONG_MA = ts.LY_DO_BIEN_DONG_ID;
            }

            return dbModel;
        }
        public IList<DBTaiSanModel> GetAllTaiSans()
        {
            var tsnk = _taiSanNhatKyService.GetAllTaiSanNhatKys().Where(c => c.TRANG_THAI_ID == 1);
            var lstTSID = tsnk != null ? tsnk.Select(c => c.TAI_SAN_ID.Value).ToList() : new List<decimal>();
            var query = _taiSanService.GetAllTaiSans().Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.NHAP && !lstTSID.Contains(c.ID)/* && c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH*/).ToList();
            List<DBTaiSanModel> lst = new List<DBTaiSanModel>();
            foreach (var itm in query)
            {
                var dbTaiSan = _dbTaiSanService.GetTaiSanByMa(QLDKTS_MA: itm.MA);
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
                    MA = itm.MA,
                    TEN = itm.TEN,
                    //NGAY_NHAP = itm.NGAY_NHAP,
                    //NGAY_SU_DUNG = itm.NGAY_SU_DUNG,
                    LY_DO_BIEN_DONG_MA = itm.lydobiendong != null ? itm.lydobiendong.MA : null,
                    LOAI_TAI_SAN_MA = itm.loaitaisan != null ? itm.loaitaisan.MA : null,
                    LOAI_HINH_TAI_SAN_ID = itm.LOAI_HINH_TAI_SAN_ID,
                    MA_DON_VI = itm.donvi != null ? itm.donvi.MA : null,
                    TRANG_THAI_ID = itm.TRANG_THAI_ID,
                    GHI_CHU = itm.GHI_CHU,
                    //DON_VI_BO_PHAN_MA = itm.donvibophan != null ? itm.donvibophan.MA : null,
                    //NUOC_SAN_XUAT_MA = itm.nuocsanxuat != null ? itm.nuocsanxuat.MA : null,
                    NAM_SAN_XUAT = itm.NAM_SAN_XUAT.ToString(),
                    QUYET_DINH_SO = itm.QUYET_DINH_SO,
                  //  QUYET_DINH_NGAY = itm.QUYET_DINH_NGAY,
                    CHUNG_TU_SO = itm.CHUNG_TU_SO,
                   // CHUNG_TU_NGAY = itm.CHUNG_TU_NGAY,
                    LOAI_TAI_SAN_VO_HINH_MA = itm.loaitaisandonvi != null ? itm.loaitaisandonvi.MA : null
                };


                //if (lstnv != null)
                //    dbModel.LST_NGUON_VON = lstnv.Select(c =>
                //    {
                //        var x = c.ToModel<TaiSanNguonVonDBModel>();
                //        x.BIEN_DONG_GUID = c.biendong != null ? c.biendong.GUID : new Guid();
                //        x.TEN_NGUON_VON = c.nguonvon != null ? c.nguonvon.TEN : null;
                //        return x;
                //    }).ToList();
                dbModel.LST_BIEN_DONG = new List<BienDongDBModel>();
                if (lstbd != null)
                    foreach (var c in lstbd)
                    {
                        var x = c.ToModel<BienDongDBModel>();                     
                        x.LY_DO_BIEN_DONG_MA = c.lydobiendong != null ? c.lydobiendong.MA : null;
                        var xct = _bienDongChiTietService.GetBienDongChiTietByBDId(c.ID);
                        if (xct != null)
                        {
                            #region prepare BienDongChiTiet
                            x.HINH_THUC_MUA_SAM_MA = xct.hinhthucmuasam != null ? xct.hinhthucmuasam.MA : null;
                            x.MUC_DICH_SU_DUNG_MA = xct.mucdichsudung != null ? xct.mucdichsudung.MA : null;
                            x.NHAN_HIEU = xct.NHAN_HIEU;
                            x.SO_HIEU = xct.SO_HIEU;
                            var diaBan = _diaBanService.GetDiaBanById(xct.DIA_BAN_ID.HasValue ? xct.DIA_BAN_ID.Value : 0);
                            x.DIA_BAN_MA = diaBan != null ? diaBan.MA : null;
                            x.DAT_TONG_DIEN_TICH = xct.DAT_TONG_DIEN_TICH;
                            
                            x.HM_SO_NAM_CON_LAI = xct.HM_SO_NAM_CON_LAI;
                           
                        //    x.KH_NGAY_BAT_DAU = xct.KH_NGAY_BAT_DAU;
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
                            //x.OTO_CHUC_DANH = xct.OTO_CHUC_DANH;
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

                //if (lstht != null)
                //    dbModel.LST_HIEN_TRANG = lstht.Select(c =>
                //    {
                //        var x = c.ToModel<TaiSanHienTrangSuDungDBModel>();
                //        x.BIEN_DONG_GUID = c.BienDong != null ? c.BienDong.GUID : new Guid();
                //        return x;
                //    }).ToList();

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
                model.DATA_JSON = dbModel.toStringJson();
                lst.Add(model);
            }
            tsnk = tsnk.Select(c => { c.TRANG_THAI_ID = 0; return c; });
            _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk.ToList());
            return lst;
        }
        public MessageReturn UpdateTaiSan(DBTaiSanModel model)
        {
            if (string.IsNullOrEmpty(model.QLDKTS_MA) && string.IsNullOrEmpty(model.DB_MA))
                return MessageReturn.CreateErrorMessage("QLDKTS_MA and DB_MA invalid");
            if (string.IsNullOrEmpty(model.DATA_JSON))
                return MessageReturn.CreateErrorMessage("DATA_JSON invalid");
            var dbModel = model.DATA_JSON.toEntity<TaiSanDBModel>();
            #region validate dbModel
            if (string.IsNullOrEmpty(dbModel.MA))
                return MessageReturn.CreateErrorMessage("DATA_JSON => MA invalid");
            if (string.IsNullOrEmpty(dbModel.TEN))
                return MessageReturn.CreateErrorMessage("DATA_JSON => TEN invalid");
            //if (!dbModel.NGAY_NHAP.HasValue)
            //    return MessageReturn.CreateErrorMessage("DATA_JSON => NGAY_NHAP invalid");
            //if (!dbModel.NGAY_SU_DUNG.HasValue)
            //    return MessageReturn.CreateErrorMessage("DATA_JSON => NGAY_SU_DUNG invalid");
            if (string.IsNullOrEmpty(dbModel.LY_DO_BIEN_DONG_MA))
                return MessageReturn.CreateErrorMessage("DATA_JSON => LY_DO_BIEN_DONG_MA invalid");
            if (!dbModel.LOAI_HINH_TAI_SAN_ID.HasValue)
                return MessageReturn.CreateErrorMessage("DATA_JSON => LOAI_HINH_TAI_SAN_ID invalid");
            if (dbModel.LOAI_HINH_TAI_SAN_ID == null || (dbModel.LOAI_HINH_TAI_SAN_ID.HasValue && dbModel.LOAI_HINH_TAI_SAN_ID.Value == 0))
            {
                dbModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.VO_HINH;
                model.DATA_JSON = dbModel.toStringJson();
            }

            switch (dbModel.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    if (dbModel.TS_CLN == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_CLN invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    if (dbModel.TS_DAT == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_DAT invalid");
                    if (string.IsNullOrEmpty(dbModel.TS_DAT.DIA_CHI))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_DAT.DIA_CHI invalid");
                    if (dbModel.TS_DAT.DIEN_TICH == 0)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_DAT.DIEN_TICH invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    if (dbModel.TS_NHA == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA invalid");
                    if (string.IsNullOrEmpty(dbModel.TS_NHA.DIA_CHI) && string.IsNullOrEmpty(dbModel.TS_NHA.TAI_SAN_DAT_MA))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA.DIA_CHI and TS_NHA.TAI_SAN_DAT_MA invalid");
                    if (!dbModel.TS_NHA.NAM_XAY_DUNG.HasValue || (dbModel.TS_NHA.NAM_XAY_DUNG.HasValue && dbModel.TS_NHA.NAM_XAY_DUNG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA.NAM_XAY_DUNG invalid");
                    if (!dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue || (dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue && dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA.DIEN_TICH_SAN_XAY_DUNG invalid");
                    if (!dbModel.TS_NHA.DIEN_TICH_XAY_DUNG.HasValue || (dbModel.TS_NHA.DIEN_TICH_XAY_DUNG.HasValue && dbModel.TS_NHA.DIEN_TICH_XAY_DUNG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA.DIEN_TICH_XAY_DUNG invalid");
                    if (!dbModel.TS_NHA.NHA_SO_TANG.HasValue || (dbModel.TS_NHA.NHA_SO_TANG.HasValue && dbModel.TS_NHA.NHA_SO_TANG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_NHA.NHA_SO_TANG invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    if (dbModel.TS_OTO == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_OTO invalid");
                    if (string.IsNullOrEmpty(dbModel.TS_OTO.BIEN_KIEM_SOAT))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_OTO.BIEN_KIEM_SOAT invalid");
                    if (!dbModel.TS_OTO.SO_CHO_NGOI.HasValue || (dbModel.TS_OTO.SO_CHO_NGOI.HasValue && dbModel.TS_OTO.SO_CHO_NGOI == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_OTO.SO_CHO_NGOI invalid");
                    if (!dbModel.TS_OTO.TAI_TRONG.HasValue || (dbModel.TS_OTO.TAI_TRONG.HasValue && dbModel.TS_OTO.TAI_TRONG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_OTO.TAI_TRONG invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    if (dbModel.TS_PTK == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_PTK invalid");
                    if (!dbModel.TS_PTK.SO_CHO_NGOI.HasValue || (dbModel.TS_PTK.SO_CHO_NGOI.HasValue && dbModel.TS_PTK.SO_CHO_NGOI == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_PTK.SO_CHO_NGOI invalid");
                    if (!dbModel.TS_PTK.TAI_TRONG.HasValue || (dbModel.TS_PTK.TAI_TRONG.HasValue && dbModel.TS_PTK.TAI_TRONG == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_PTK.TAI_TRONG invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    if (dbModel.TS_VKT == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_VKT invalid");
                    if (!dbModel.TS_VKT.DIEN_TICH.HasValue || (dbModel.TS_VKT.DIEN_TICH.HasValue && dbModel.TS_VKT.DIEN_TICH == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON =>TS_VKT.DIEN_TICH invalid");
                    if (!dbModel.TS_VKT.CHIEU_DAI.HasValue || (dbModel.TS_VKT.CHIEU_DAI.HasValue && dbModel.TS_VKT.CHIEU_DAI == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_VKT.CHIEU_DAI invalid");
                    if (!dbModel.TS_VKT.THE_TICH.HasValue || (dbModel.TS_VKT.THE_TICH.HasValue && dbModel.TS_VKT.THE_TICH == 0))
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_VKT.THE_TICH invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    if (dbModel.TS_MAY_MOC == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_MAY_MOC invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    if (dbModel.TS_VO_HINH == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_VO_HINH invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    if (dbModel.TS_HUU_HINH_KHAC == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_HUU_HINH invalid");
                    break;
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    if (dbModel.TS_DAC_THU == null)
                        return MessageReturn.CreateErrorMessage("DATA_JSON => TS_DAC_THU invalid");
                    break;
            }

            #endregion
            var entity = _dbTaiSanService.GetTaiSanByMa(QLDKTS_MA: model.QLDKTS_MA, DB_MA: model.DB_MA, DON_VI_DONG_BO_ID: model.DON_VI_DONG_BO_ID);
            if (entity != null)
            {
                entity.QLDKTS_MA = model.QLDKTS_MA;//Mã tài sản trong phần mềm QLDKTS
                entity.DB_MA = model.DB_MA;//Mã tài sản của đơn vị đồng bộ
                entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID.HasValue ? model.LOAI_HINH_TAI_SAN_ID.Value : 0;
                entity.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
                entity.PHAN_MEM_DONG_BO_ID = 0;
                entity.DATA_JSON = model.DATA_JSON;
                entity.NGAY_DONG_BO = model.NGAY_DONG_BO;
                _dbTaiSanService.UpdateTaiSan(entity);
            }
            else
            {
                model.ID = 0;
                entity = model.ToEntity<DBTaiSan>();
                _dbTaiSanService.InsertTaiSan(entity);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn UpdateTaiSans(List<DBTaiSanModel> ListModel)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<DBTaiSan> LstAdd = new List<DBTaiSan>();
            List<DBTaiSan> LstEdit = new List<DBTaiSan>();
            foreach (var model in ListModel)
            {
                if (string.IsNullOrEmpty(model.QLDKTS_MA) && string.IsNullOrEmpty(model.DB_MA))
                {
                    err += "\nQLDKTS_MA and DB_MA invalid";
                    continue;
                }
                if (string.IsNullOrEmpty(model.DATA_JSON))
                {
                    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON invalid";
                    continue;
                }
                var dbModel = model.DATA_JSON.toEntity<TaiSanDBModel>();
                #region validate dbModel
                if (string.IsNullOrEmpty(dbModel.MA))
                {
                    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => MA invalid";
                    continue;
                }
                if (string.IsNullOrEmpty(dbModel.TEN))
                {
                    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TEN invalid";
                    continue;
                }
                //if (!dbModel.NGAY_NHAP.HasValue)
                //{
                //    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => NGAY_NHAP invalid";
                //    continue;
                //}
                //if (!dbModel.NGAY_SU_DUNG.HasValue)
                //{
                //    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => NGAY_SU_DUNG invalid";
                //    continue;
                //}
                if (string.IsNullOrEmpty(dbModel.LY_DO_BIEN_DONG_MA))
                {
                    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => LY_DO_BIEN_DONG_MA invalid";
                    continue;
                }
                if (!dbModel.LOAI_HINH_TAI_SAN_ID.HasValue)
                {
                    err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => LOAI_HINH_TAI_SAN_ID invalid";
                    continue;
                }

                if (dbModel.LOAI_HINH_TAI_SAN_ID == null || (dbModel.LOAI_HINH_TAI_SAN_ID.HasValue && dbModel.LOAI_HINH_TAI_SAN_ID.Value == 0))
                {
                    dbModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.VO_HINH;
                    model.DATA_JSON = dbModel.toStringJson();
                }
                switch (dbModel.LOAI_HINH_TAI_SAN_ID)
                {

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        if (dbModel.TS_CLN == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_CLN invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        if (dbModel.TS_DAT == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_DAT invalid";
                            continue;
                        }
                        if (string.IsNullOrEmpty(dbModel.TS_DAT.DIA_CHI))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_DAT.DIA_CHI invalid";
                            continue;
                        }
                        if (dbModel.TS_DAT.DIEN_TICH == 0)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_DAT.DIEN_TICH invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        if (dbModel.TS_NHA == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA invalid";
                            continue;
                        }
                        if (string.IsNullOrEmpty(dbModel.TS_NHA.DIA_CHI) && string.IsNullOrEmpty(dbModel.TS_NHA.TAI_SAN_DAT_MA))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA.DIA_CHI and TS_NHA.TAI_SAN_DAT_MA invalid";
                            continue;
                        }
                        if (!dbModel.TS_NHA.NAM_XAY_DUNG.HasValue || (dbModel.TS_NHA.NAM_XAY_DUNG.HasValue && dbModel.TS_NHA.NAM_XAY_DUNG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA.NAM_XAY_DUNG invalid";
                            continue;
                        }
                        if (!dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue || (dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue && dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA.DIEN_TICH_SAN_XAY_DUNG invalid";
                            continue;
                        }
                        if (!dbModel.TS_NHA.DIEN_TICH_XAY_DUNG.HasValue || (dbModel.TS_NHA.DIEN_TICH_XAY_DUNG.HasValue && dbModel.TS_NHA.DIEN_TICH_XAY_DUNG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA.DIEN_TICH_XAY_DUNG invalid";
                            continue;
                        }
                        if (!dbModel.TS_NHA.NHA_SO_TANG.HasValue || (dbModel.TS_NHA.NHA_SO_TANG.HasValue && dbModel.TS_NHA.NHA_SO_TANG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_NHA.NHA_SO_TANG invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        if (dbModel.TS_OTO == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_OTO invalid";
                            continue;
                        }
                        if (string.IsNullOrEmpty(dbModel.TS_OTO.BIEN_KIEM_SOAT))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_OTO.BIEN_KIEM_SOAT invalid";
                            continue;
                        }
                        if (!dbModel.TS_OTO.SO_CHO_NGOI.HasValue || (dbModel.TS_OTO.SO_CHO_NGOI.HasValue && dbModel.TS_OTO.SO_CHO_NGOI == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_OTO.SO_CHO_NGOI invalid";
                            continue;
                        }
                        if (!dbModel.TS_OTO.TAI_TRONG.HasValue || (dbModel.TS_OTO.TAI_TRONG.HasValue && dbModel.TS_OTO.TAI_TRONG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_OTO.TAI_TRONG invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                        if (dbModel.TS_PTK == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_PTK invalid";
                            continue;
                        }
                        if (!dbModel.TS_PTK.SO_CHO_NGOI.HasValue || (dbModel.TS_PTK.SO_CHO_NGOI.HasValue && dbModel.TS_PTK.SO_CHO_NGOI == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_PTK.SO_CHO_NGOI invalid";
                            continue;
                        }
                        if (!dbModel.TS_PTK.TAI_TRONG.HasValue || (dbModel.TS_PTK.TAI_TRONG.HasValue && dbModel.TS_PTK.TAI_TRONG == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_PTK.TAI_TRONG invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        if (dbModel.TS_VKT == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_VKT invalid";
                            continue;
                        }
                        if (!dbModel.TS_VKT.DIEN_TICH.HasValue || (dbModel.TS_VKT.DIEN_TICH.HasValue && dbModel.TS_VKT.DIEN_TICH == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON =>TS_VKT.DIEN_TICH invalid";
                            continue;
                        }
                        if (!dbModel.TS_VKT.CHIEU_DAI.HasValue || (dbModel.TS_VKT.CHIEU_DAI.HasValue && dbModel.TS_VKT.CHIEU_DAI == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_VKT.CHIEU_DAI invalid";
                            continue;
                        }
                        if (!dbModel.TS_VKT.THE_TICH.HasValue || (dbModel.TS_VKT.THE_TICH.HasValue && dbModel.TS_VKT.THE_TICH == 0))
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_VKT.THE_TICH invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        if (dbModel.TS_MAY_MOC == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_MAY_MOC invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        if (dbModel.TS_VO_HINH == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_VO_HINH invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                        if (dbModel.TS_HUU_HINH_KHAC == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_HUU_HINH invalid";
                            continue;
                        }
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                        if (dbModel.TS_DAC_THU == null)
                        {
                            err += $"\nDB_MA:{model.DB_MA}\t\tDATA_JSON => TS_DAC_THU invalid";
                            continue;
                        }
                        break;
                }

                #endregion
                var entity = _dbTaiSanService.GetTaiSanByMa(QLDKTS_MA: model.QLDKTS_MA, DB_MA: model.DB_MA, DON_VI_DONG_BO_ID: model.DON_VI_DONG_BO_ID);
                if (entity != null)
                {
                    entity.QLDKTS_MA = model.QLDKTS_MA;
                    entity.DB_MA = model.DB_MA;
                    entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID.HasValue ? model.LOAI_HINH_TAI_SAN_ID.Value : 0;
                    entity.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
                    entity.PHAN_MEM_DONG_BO_ID = 0;
                    entity.DATA_JSON = model.DATA_JSON;
                    entity.NGAY_DONG_BO = model.NGAY_DONG_BO;
                    LstEdit.Add(entity);
                }
                else
                {
                    model.ID = 0;
                    entity = model.ToEntity<DBTaiSan>();
                    entity.NGAY_DONG_BO = DateTime.Now;
                    entity.GUID = new Guid();
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _dbTaiSanService.InsertTaiSan(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _dbTaiSanService.UpdateTaiSan(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
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
                    ErrorMessage += $"\nMA_TAI_SAN invalid";
                }
                else
                {
                    // cập nhật nhật ký tài sản
                    TotalSuc++;

                  //  _taiSanNhatKyService.UpdateTrangThaiTaiSanNhatKy(taiSan.ID);
                }
            }
            foreach (var item in ListQuyetDinhTichThu)
            {
                var QuyetDinhTichThu = _quyetDinhTichThuService.GetQuyetDinhTichThu(item.QUYET_DINH_SO, item.QUYET_DINH_NGAY, MaDonVi);
                if (QuyetDinhTichThu == null)
                {
                    TotalErr++;
                    ErrorMessage += $"\nMA_TAI_SAN invalid";
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
        //public List<QuyetDinhTichThuModel> GetQuyetDinhTichThuModels()
        //{
        //    List<decimal> ListQuyetDinhId = _taiSanNhatKyService.GetTaiSanToaDanDB().Select(m => m.QUYET_DINH_TICH_THU_ID.Value).ToList();
        //    List<QuyetDinhTichThuModel> ListModel = new List<QuyetDinhTichThuModel>();
        //    var ListQuyetDinhTichThu = ListQuyetDinhId.Select(m => _quyetDinhTichThuService.GetQuyetDinhTichThuById(m)).Where(m => m != null);
        //    ListQuyetDinhTichThu = ListQuyetDinhTichThu.Distinct();
        //    foreach (var item in ListQuyetDinhTichThu)
        //    {
        //        QuyetDinhTichThuModel model = new QuyetDinhTichThuModel();
        //        model = item.ToModel<QuyetDinhTichThuModel>();
        //        model.MA_DON_VI = item.DON_VI_ID != null ? _donViService.GetDonViById(item.ID).MA : null;
        //        var ListTaiSanTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID).Where(m => m.TRANG_THAI_ID == 0);//0-tồn tại; 1- xóa;
        //        foreach (var ts in ListTaiSanTD)
        //        {
        //            TaiSanToanDanModel taiSanToanDanModel = new TaiSanToanDanModel();
        //            taiSanToanDanModel = ts.ToModel<TaiSanToanDanModel>();
        //            taiSanToanDanModel.MA_LOAI_TAI_SAN = ts.loaitaisan.MA;
        //            taiSanToanDanModel.MA_NGUON_GOC_TAI_SAN = ts.NGUON_GOC_TAI_SAN_ID != null ? _nguonGocTaiSanService.GetNguonGocTaiSanById(ts.NGUON_GOC_TAI_SAN_ID.Value).MA : null;
        //            // xử lý tài sản toàn dân
        //            var xuLyTS = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(ts.ID).Where(m => m.xuly.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua);//đã xử lý==2; Đề xuất =1;
        //            foreach (var xulyTaiSan in xuLyTS)
        //            {

        //                //  xử lý
        //                var XuLy = _xuLyService.GetXuLyById(xulyTaiSan.XU_LY_ID);
        //                XuLyModel xuLyModel = XuLy.ToModel<XuLyModel>();
        //                xuLyModel.MA_DON_VI = XuLy.DON_VI_ID != null ? _donViService.GetDonViById(XuLy.DON_VI_ID.Value).MA : null;
        //                // Xử lý  Tài sản toàn dân
        //                TSToanDanXuLyModel toanDanXuLyModel = xulyTaiSan.ToModel<TSToanDanXuLyModel>();
        //                toanDanXuLyModel.MA_DON_VI_CHUYEN = xulyTaiSan.DON_VI_CHUYEN_ID != null ? _donViService.GetDonViById(xulyTaiSan.DON_VI_CHUYEN_ID.Value).MA : null;
        //                toanDanXuLyModel.MA_HINH_THUC_XU_LY = xulyTaiSan.HINH_THUC_XU_LY_ID != null ? _phuongAnXuLyService.GetPhuongAnXuLyById(xulyTaiSan.HINH_THUC_XU_LY_ID.Value).MA : null;
        //                toanDanXuLyModel.MA_PHUONG_AN_XU_LY = xulyTaiSan.PHUONG_AN_XU_LY_ID != null ? hinhThucXuLyService.GetHinhThucXuLyById(xulyTaiSan.PHUONG_AN_XU_LY_ID.Value).MA : null;
        //                xuLyModel.SO_LUONG = toanDanXuLyModel.SO_LUONG;
        //                xuLyModel.DIEN_TICH = toanDanXuLyModel.DIEN_TICH;
        //                xuLyModel.NGUYEN_GIA = toanDanXuLyModel.NGUYEN_GIA;
        //                xuLyModel.GIA_TRI = toanDanXuLyModel.GIA_TRI;
        //                xuLyModel.GIA_TRI_GHI_TANG = toanDanXuLyModel.GIA_TRI_GHI_TANG;
        //                xuLyModel.GIA_TRI_NSNN = toanDanXuLyModel.GIA_TRI_NSNN;
        //                xuLyModel.GIA_TRI_TKTG = toanDanXuLyModel.GIA_TRI_TKTG;
        //                xuLyModel.MA_PHUONG_AN_XU_LY = toanDanXuLyModel.MA_PHUONG_AN_XU_LY;
        //                xuLyModel.MA_HINH_THUC_XU_LY = toanDanXuLyModel.MA_HINH_THUC_XU_LY;
        //                xuLyModel.CHI_PHI_XU_LY = toanDanXuLyModel.CHI_PHI_XU_LY;
        //                xuLyModel.HOP_DONG_SO = toanDanXuLyModel.HOP_DONG_SO;
        //                xuLyModel.GHI_CHU_CHI_TIET = toanDanXuLyModel.GHI_CHU;
        //                xuLyModel.MA_DON_VI_CHUYEN = toanDanXuLyModel.MA_DON_VI_CHUYEN;
        //                xuLyModel.GHI_CHU_CHI_TIET = toanDanXuLyModel.GHI_CHU;
        //                //Quyết định xử lý +tatf sản toàn dân
        //                taiSanToanDanModel.ListXuLy.Add(xuLyModel);
        //            }
        //            model.ListTaiSanToanDanModels.Add(taiSanToanDanModel);
        //            // update tài sản.
        //            TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(ts.ID);
        //            taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
        //            taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
        //            _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
        //        }
        //        ListModel.Add(model);
        //    }
        //    string JsonData = ListModel.toStringJson();
        //    return ListModel;
        //}
        //public MessageReturn UpdateTaiSanToanDan(QuyetDinhTichThuModel model)
        //{
        //    if (string.IsNullOrEmpty(model.QUYET_DINH_SO) || model.QUYET_DINH_NGAY == null)
        //    {
        //        return MessageReturn.CreateErrorMessage("QUYET_DINH_SO and QUYET_DINH_NGAY invalid");
        //    }
        //    String DonViMa;
        //    if (string.IsNullOrEmpty(model.MA_DON_VI))
        //    {
        //        return MessageReturn.CreateErrorMessage("MA_DON_VI  invalid");
        //    }
        //    else
        //    {
        //        var DonVi = _donViService.GetDonViByMa(model.MA_DON_VI);
        //        if (DonVi == null)
        //        {
        //            return MessageReturn.CreateErrorMessage("MA_DON_VI  invalid");
        //        }
        //        else
        //        {
        //            DonViMa = DonVi.MA;
        //        }
        //    }
        //    model.QUYET_DINH_SO = model.QUYET_DINH_SO.Trim();
        //    model.QUYET_DINH_SO = model.QUYET_DINH_SO.ToUpper();
        //    DBTaiSan dBTaiSan = new DBTaiSan();
        //    // prepare dữ liệu sang dữ liệu chuẩn
        //    QuyetDinhTichThuApi quyetDinhApi = new QuyetDinhTichThuApi();
        //    quyetDinhApi.QUYET_DINH_SO = model.QUYET_DINH_SO;
        //    quyetDinhApi.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;

        //    quyetDinhApi.TEN = model.TEN;
        //    quyetDinhApi.GHI_CHU = model.GHI_CHU;
        //    foreach (var item in model.ListTaiSanToanDanModels)
        //    {
        //        if (item == null)
        //        {
        //            continue;
        //        }
        //        TaiSanToanDanApi tsApi = new TaiSanToanDanApi();
        //        tsApi.TEN = item.TEN;
        //        tsApi.NGUYEN_GIA = item.NGUYEN_GIA;
        //        tsApi.GHI_CHU = item.GHI_CHU;
        //        tsApi.GIA_TRI = item.GIA_TRI;
        //        tsApi.KHOI_LUONG = item.KHOI_LUONG;
        //        tsApi.DIEN_TICH = item.DIEN_TICH;
        //        if (!string.IsNullOrEmpty(item.MA_LOAI_TAI_SAN))
        //        {
        //            var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(item.MA_LOAI_TAI_SAN);
        //            if (loaiTaiSan == null)
        //            {
        //                return MessageReturn.CreateErrorMessage("MA_LOAI_TAI_SAN  invalid");
        //            }
        //            else
        //            {
        //                tsApi.MA_LOAI_TAI_SAN = loaiTaiSan.MA;
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(item.MA_LOAI_TAI_SAN))
        //        {
        //            var nguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(item.MA_NGUON_GOC_TAI_SAN);
        //            if (nguonGocTaiSan == null)
        //            {
        //                return MessageReturn.CreateErrorMessage("MA_NGUON_GOC_TAI_SAN  invalid");
        //            }
        //            else
        //            {
        //                tsApi.MA_NGUON_GOC_TAI_SAN = nguonGocTaiSan.MA;
        //            }
        //        }

        //        tsApi.SO_LUONG = item.SO_LUONG;
        //        foreach (var xl in item.ListXuLy)
        //        {
        //            if (xl == null)
        //            {
        //                continue;
        //            }
        //            XuLyApi xuLyApi = new XuLyApi();
        //            xuLyApi.QUYET_DINH_SO = xl.QUYET_DINH_SO;
        //            xuLyApi.QUYET_DINH_NGAY = xl.QUYET_DINH_NGAY;
        //            xuLyApi.NGAY_XU_LY = xl.NGAY_XU_LY;
        //            if (!string.IsNullOrEmpty(xl.MA_DON_VI))
        //            {
        //                var donvi = _donViService.GetDonViByMa(xl.MA_DON_VI);
        //                if (donvi != null)
        //                {
        //                    xuLyApi.MA_DON_VI = donvi.MA;
        //                }
        //                else
        //                {
        //                    return MessageReturn.CreateErrorMessage("MA_DON_VI  invalid");
        //                }
        //            }
        //            xuLyApi.HINH_THUC = xl.HINH_THUC;
        //            xuLyApi.CHI_PHI = xl.CHI_PHI;
        //            xuLyApi.GHI_CHU = xl.GHI_CHU;
        //            xuLyApi.LOAI_XU_LY_ID = 2;// 2- đã xử lý; 1- đề xuất
        //                                      //Chi tiết xử lý từng tài sản
        //            xuLyApi.SO_LUONG = xl.SO_LUONG;
        //            xuLyApi.DIEN_TICH = xl.DIEN_TICH;
        //            xuLyApi.NGUYEN_GIA = xl.NGUYEN_GIA;
        //            xuLyApi.GIA_TRI = xl.GIA_TRI;
        //            xuLyApi.GIA_TRI_GHI_TANG = xl.GIA_TRI_GHI_TANG;
        //            xuLyApi.GIA_TRI_NSNN = xl.GIA_TRI_NSNN;
        //            xuLyApi.GIA_TRI_TKTG = xl.GIA_TRI_TKTG;
        //            if (!string.IsNullOrEmpty(xl.MA_PHUONG_AN_XU_LY))
        //            {
        //                var hinhThucXuLy = hinhThucXuLyService.GetHinhThucXuLyByMa(xl.MA_PHUONG_AN_XU_LY);
        //                if (hinhThucXuLy != null)
        //                {
        //                    xuLyApi.MA_PHUONG_AN_XU_LY = hinhThucXuLy.MA;
        //                }
        //                else
        //                {
        //                    return MessageReturn.CreateErrorMessage("MA_PHUONG_AN_XU_LY  invalid");
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(xl.MA_PHUONG_AN_XU_LY))
        //            {
        //                var phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(xl.MA_HINH_THUC_XU_LY);
        //                if (phuongAn_XyLy != null)
        //                {
        //                    xuLyApi.MA_HINH_THUC_XU_LY = phuongAn_XyLy.MA;
        //                }
        //                else
        //                {
        //                    return MessageReturn.CreateErrorMessage("MA_HINH_THUC_XU_LY  invalid");
        //                }
        //            }
        //            xuLyApi.CHI_PHI_XU_LY = xl.CHI_PHI_XU_LY;
        //            xuLyApi.HOP_DONG_SO = xl.HOP_DONG_SO;
        //            xuLyApi.GHI_CHU_CHI_TIET = xl.GHI_CHU_CHI_TIET;
        //            if (!string.IsNullOrEmpty(xl.MA_DON_VI_CHUYEN))
        //            {
        //                var donvichuyen = _donViService.GetDonViByMa(xl.MA_DON_VI_CHUYEN);
        //                if (donvichuyen != null)
        //                {
        //                    xuLyApi.MA_DON_VI_CHUYEN = donvichuyen.MA;
        //                }
        //                else
        //                {
        //                    return MessageReturn.CreateErrorMessage("MA_DON_VI_CHUYEN  invalid");
        //                }
        //            }
        //            tsApi.ListXuLy.Add(xuLyApi);
        //        }
        //        quyetDinhApi.ListTaiSanToanDanModels.Add(tsApi);
        //    }
        //    quyetDinhApi.QUYET_DINH_SO = model.QUYET_DINH_SO;
        //    dBTaiSan.DATA_JSON = quyetDinhApi.toStringJson();
        //    dBTaiSan.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.ALL;
        //    dBTaiSan.IS_TAI_SAN_TOAN_DAN = true;
        //    dBTaiSan.NGAY_DONG_BO = DateTime.Now;
        //    dBTaiSan.QUYET_DINH_TICH_THU_NGAY = model.QUYET_DINH_NGAY;
        //    dBTaiSan.QUYET_DINH_TICH_THU_SO = model.QUYET_DINH_SO;
        //    _dbTaiSanService.InsertTaiSan(dBTaiSan);
        //    return MessageReturn.CreateSuccessMessage("Success done");
        //}
        //public MessageReturn UpdateListTaiSanToanDan(List<QuyetDinhTichThuModel> ListModel)
        //{
        //    string err = "";
        //    int TotalErr = 0;
        //    int TotalSuc = 0;
        //    foreach (var model in ListModel)
        //    {
        //        if (string.IsNullOrEmpty(model.QUYET_DINH_SO))
        //        {
        //            err += $"\nQUYET_DINH_SO invalid";
        //            TotalErr++;
        //            continue;
        //        }
        //        if (model.QUYET_DINH_NGAY == null)
        //        {
        //            err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tQUYET_DINH_NGAY invalid";
        //            TotalErr++;
        //            continue;
        //        }
        //        string DonViMa;
        //        if (string.IsNullOrEmpty(model.MA_DON_VI))
        //        {
        //            err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_DON_VI invalid";
        //            TotalErr++;
        //            continue;
        //        }
        //        else
        //        {
        //            var DonVi = _donViService.GetDonViByMa(model.MA_DON_VI);
        //            if (DonVi == null)
        //            {
        //                err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_DON_VI invalid";
        //                TotalErr++;
        //                continue;
        //            }
        //            else
        //            {
        //                DonViMa = DonVi.MA;
        //            }
        //        }
        //        model.QUYET_DINH_SO = model.QUYET_DINH_SO.Trim();
        //        model.QUYET_DINH_SO = model.QUYET_DINH_SO.ToUpper();
        //        DBTaiSan dBTaiSan = new DBTaiSan();
        //        QuyetDinhTichThuApi quyetDinhApi = new QuyetDinhTichThuApi();
        //        quyetDinhApi.QUYET_DINH_SO = model.QUYET_DINH_SO;
        //        quyetDinhApi.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
        //        quyetDinhApi.MA_DON_VI = DonViMa;
        //        quyetDinhApi.TEN = model.TEN;
        //        quyetDinhApi.GHI_CHU = model.GHI_CHU;
        //        foreach (var item in model.ListTaiSanToanDanModels)
        //        {
        //            TaiSanToanDanApi tsApi = new TaiSanToanDanApi();
        //            tsApi.TEN = item.TEN;
        //            tsApi.NGUYEN_GIA = item.NGUYEN_GIA;
        //            tsApi.GHI_CHU = item.GHI_CHU;
        //            tsApi.GIA_TRI = item.GIA_TRI;
        //            tsApi.KHOI_LUONG = item.KHOI_LUONG;
        //            tsApi.DIEN_TICH = item.DIEN_TICH;
        //            if (!string.IsNullOrEmpty(item.MA_LOAI_TAI_SAN))
        //            {
        //                var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(item.MA_LOAI_TAI_SAN);
        //                if (loaiTaiSan == null)
        //                {
        //                    err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_LOAI_TAI_SAN invalid";
        //                    TotalErr++;
        //                    continue;
        //                }
        //                else
        //                {
        //                    tsApi.MA_LOAI_TAI_SAN = loaiTaiSan.MA;
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(item.MA_LOAI_TAI_SAN))
        //            {
        //                var nguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(item.MA_NGUON_GOC_TAI_SAN);
        //                if (nguonGocTaiSan == null)
        //                {
        //                    err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_NGUON_GOC_TAI_SAN invalid";
        //                    TotalErr++;
        //                    continue;
        //                }
        //                else
        //                {
        //                    tsApi.MA_NGUON_GOC_TAI_SAN = nguonGocTaiSan.MA;
        //                }
        //            }
        //            tsApi.SO_LUONG = item.SO_LUONG;
        //            foreach (var xl in item.ListXuLy)
        //            {
        //                XuLyApi xuLyApi = new XuLyApi();
        //                xuLyApi.QUYET_DINH_SO = xl.QUYET_DINH_SO;
        //                xuLyApi.QUYET_DINH_NGAY = xl.QUYET_DINH_NGAY;
        //                xuLyApi.NGAY_XU_LY = xl.NGAY_XU_LY;
        //                if (!string.IsNullOrEmpty(xl.MA_DON_VI))
        //                {
        //                    var donvi = _donViService.GetDonViByMa(xl.MA_DON_VI);
        //                    if (donvi != null)
        //                    {
        //                        xuLyApi.MA_DON_VI = donvi.MA;
        //                    }
        //                    else
        //                    {
        //                        err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_DON_VI invalid";
        //                        TotalErr++;
        //                        continue;

        //                    }
        //                }
        //                xuLyApi.HINH_THUC = xl.HINH_THUC;
        //                xuLyApi.CHI_PHI = xl.CHI_PHI;
        //                xuLyApi.GHI_CHU = xl.GHI_CHU;
        //                xuLyApi.LOAI_XU_LY_ID = 1;// 1- đã xử lý; 0- đề xuất
        //                                          //      Chi tiết
        //                xuLyApi.SO_LUONG = xl.SO_LUONG;
        //                xuLyApi.DIEN_TICH = xl.DIEN_TICH;
        //                xuLyApi.NGUYEN_GIA = xl.NGUYEN_GIA;
        //                xuLyApi.GIA_TRI = xl.GIA_TRI;
        //                xuLyApi.GIA_TRI_GHI_TANG = xl.GIA_TRI_GHI_TANG;
        //                xuLyApi.GIA_TRI_NSNN = xl.GIA_TRI_NSNN;
        //                xuLyApi.GIA_TRI_TKTG = xl.GIA_TRI_TKTG;
        //                if (!string.IsNullOrEmpty(xl.MA_PHUONG_AN_XU_LY))
        //                {
        //                    var hinhThucXuLy = hinhThucXuLyService.GetHinhThucXuLyByMa(xl.MA_PHUONG_AN_XU_LY);
        //                    if (hinhThucXuLy != null)
        //                    {
        //                        xuLyApi.MA_PHUONG_AN_XU_LY = hinhThucXuLy.MA;
        //                    }
        //                    else
        //                    {
        //                        err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_PHUONG_AN_XU_LY invalid";
        //                        TotalErr++;
        //                        continue;
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(xl.MA_PHUONG_AN_XU_LY))
        //                {
        //                    var phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(xl.MA_HINH_THUC_XU_LY);
        //                    if (phuongAn_XyLy != null)
        //                    {
        //                        xuLyApi.MA_HINH_THUC_XU_LY = phuongAn_XyLy.MA;
        //                    }
        //                    else
        //                    {
        //                        err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_HINH_THUC_XU_LY invalid";
        //                        TotalErr++;
        //                        continue;
        //                    }
        //                }
        //                xuLyApi.CHI_PHI_XU_LY = xl.CHI_PHI_XU_LY;
        //                xuLyApi.HOP_DONG_SO = xl.HOP_DONG_SO;
        //                xuLyApi.GHI_CHU_CHI_TIET = xl.GHI_CHU_CHI_TIET;
        //                if (!string.IsNullOrEmpty(xl.MA_DON_VI_CHUYEN))
        //                {
        //                    var donvichuyen = _donViService.GetDonViByMa(xl.MA_DON_VI_CHUYEN);
        //                    if (donvichuyen != null)
        //                    {
        //                        xuLyApi.MA_DON_VI_CHUYEN = donvichuyen.MA;
        //                    }
        //                    else
        //                    {
        //                        err += $"\nQUYET_DINH_SO: {model.QUYET_DINH_SO}\t\tMA_DON_VI_CHUYEN invalid";
        //                        TotalErr++;
        //                        continue;
        //                    }
        //                }
        //                tsApi.ListXuLy.Add(xuLyApi);

        //            }
        //            quyetDinhApi.ListTaiSanToanDanModels.Add(tsApi);
        //        }
        //        quyetDinhApi.QUYET_DINH_SO = model.QUYET_DINH_SO;
        //        dBTaiSan.DATA_JSON = quyetDinhApi.toStringJson();
        //        dBTaiSan.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.ALL;
        //        dBTaiSan.IS_TAI_SAN_TOAN_DAN = true;
        //        dBTaiSan.NGAY_DONG_BO = DateTime.Now;
        //        dBTaiSan.QUYET_DINH_TICH_THU_NGAY = model.QUYET_DINH_NGAY;
        //        dBTaiSan.QUYET_DINH_TICH_THU_SO = model.QUYET_DINH_SO;
        //        dBTaiSan.PHAN_MEM_DONG_BO_ID = 0;
        //        _dbTaiSanService.InsertTaiSan(dBTaiSan);
        //        //   insert nhật ký
        //        TotalSuc++;
        //    }
        //    return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        //}
        #endregion
        #endregion
    }
}
