using GS.Core;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.NghiepVu;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.KeToan;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanImportModelFactory: ITaiSanImportModelFactory
    {
        #region Fields
        private readonly ITaiSanService _itemService;
        //private readonly ITaiSanDatModelFactory _taisandatModelFactory;
        //private readonly ITaiSanDatService _taisandatService;
        //private readonly IDiaBanModelFactory _diabanModelFactory;
        //private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
        private readonly ILoaiTaiSanService _loaitaisanService;
        private readonly IDonViService _donViService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IHienTrangService _hientrangService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly INguonVonService _nguonVonService;
        private readonly IHaoMonTaiSanModelFactory _haoMonTaiSanModelFactory;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        //private readonly ITaiSanImportService _taiSanImportService;
        //private readonly ITaiSanImportModelFactory _taiSanImportModelFactory;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ITaiSanKhauHaoService _taiSanKhauHaoService;
        #endregion

        #region Ctor
        public TaiSanImportModelFactory(
            //ITaiSanModelFactory itemModelFactory
            ITaiSanService itemService,
            //ITaiSanDatModelFactory taisandatModelFactory,
            //ITaiSanDatService taisandatService,
            //IDiaBanModelFactory diabanModelFactory,
            //ILoaiTaiSanModelFactory loaitaisanModelFactory,
            ILoaiTaiSanService loaitaisanService,
            IDonViService donViService,
            ILyDoBienDongService lyDoBienDongService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IQuocGiaService quocGiaService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            IHienTrangService hienTrangService,
            ITaiSanNguonVonService taiSanNguonVonService,
            INguonVonService nguonVonService,
            IHaoMonTaiSanModelFactory haoMonTaiSanModelFactory,
            IHaoMonTaiSanService haoMonTaiSanService,
            //ITaiSanImportService taiSanImportService,
            //ITaiSanImportModelFactory taiSanImportModelFactory
            ILoaiTaiSanDonViServices loaiTaiSanDonViServices,
            ITaiSanKhauHaoService taiSanKhauHaoService
            )
        {
            //this._hoatdongService = hoatdongService;
            //this._taisandatModelFactory = taisandatModelFactory;
            //this._taisandatService = taisandatService;
            //this._diabanModelFactory = diabanModelFactory;
            //this._loaitaisanModelFactory = loaitaisanModelFactory;
            this._loaitaisanService = loaitaisanService;
            //this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._donViService = donViService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._quocGiaService = quocGiaService;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._hientrangService = hienTrangService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._nguonVonService = nguonVonService;
            this._haoMonTaiSanModelFactory = haoMonTaiSanModelFactory;
            this._haoMonTaiSanService = haoMonTaiSanService;
            //this._taiSanImportService = taiSanImportService;
            //this._taiSanImportModelFactory = taiSanImportModelFactory;
            this._loaiTaiSanDonViServices = loaiTaiSanDonViServices;
            this._taiSanKhauHaoService = taiSanKhauHaoService;
        }
        #endregion
        public TaiSanModel InsertToTaiSan(ImportExcelTaiSan item, TaiSanModel model)
        {
            if (item != null)
            {
                model.MA_DB = item.MA_DB;
                model.TEN = item.TEN;
                model.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(item.LOAI_TAI_SAN_MA.Substring(0, 1));
                //model.LOAI_TAI_SAN_ID = _loaitaisanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA).ID;
                model.DON_VI_ID = _donViService.GetDonViByMa(item.DON_VI_MA).ID;
                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    model.LOAI_TAI_SAN_ID = _loaitaisanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA).ID;
                }
                else
                {
                    if (item.LOAI_TAI_SAN_MA != "9" && item.LOAI_TAI_SAN_MA != "10" && item.LOAI_TAI_SAN_MA != "902" && item.LOAI_TAI_SAN_MA != "903" && item.LOAI_TAI_SAN_MA != "904" && item.LOAI_TAI_SAN_MA != "905" && item.LOAI_TAI_SAN_MA != "906")
                    {
                        var donViLonNhat = _donViService.GetDonViLonNhat(model.DON_VI_ID ?? 0);
                        model.LOAI_TAI_SAN_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.ID;
                    }
                    else
                    {
                        var loaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa(item.LOAI_TAI_SAN_MA);
                        model.LOAI_TAI_SAN_ID = loaiTaiSanVoHinh?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = loaiTaiSanVoHinh?.ID;
                    }
                }
                model.NGUYEN_GIA_BAN_DAU = item.NGUYEN_GIA;
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.XOA;
                model.NGAY_SU_DUNG = item.NGAY_SU_DUNG;
                model.NGAY_TAO = DateTime.Now;
                model.NGAY_NHAP = item.NGAY_TANG;
                model.LY_DO_BIEN_DONG_ID = _lyDoBienDongService.GetLyDoBienDongByMa(item.LY_DO_TANG_MA).ID;
                if (item.PHUONG_THUC_MUA_SAM != null)
                {
                    model.PHUONG_THUC_MUA_SAM_ID = Convert.ToDecimal(item.PHUONG_THUC_MUA_SAM);
                }    
                if (item.HINH_THUC_MUA_SAM != null)
                {
                    model.HinhThucMuaSamId = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(item.HINH_THUC_MUA_SAM).ID;
                }
                if (item.DON_VI_MUA_SAM != null)
                {
                    model.DON_VI_MUA_SAM_TAP_TRUNG_ID = _donViService.GetDonViByMa(item.DON_VI_MUA_SAM).ID;
                }
                model.NAM_SAN_XUAT = item.NAM_SX ?? 0;
                if(item.NUOC_SX != null)
                {
                    model.NUOC_SAN_XUAT_ID = _quocGiaService.GetQuocGiaById(Convert.ToInt32(item.NUOC_SX)).ID;
                }
            }
            TaiSan ts = new TaiSan();
            ts = model.ToEntity<TaiSan>();
            ts.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            _itemService.InsertTaiSan(ts);
            ts.MA = CommonHelper.GenMaTaiSan(item.DON_VI_MA, item.LOAI_TAI_SAN_MA, ts.ID);
            _itemService.UpdateTaiSan(ts);
            return ts.ToModel<TaiSanModel>();
        }

        public BienDongModel InsertToBienDong (TaiSanModel item, BienDongModel model)
        {
            if (item != null)
            {
                model.TAI_SAN_TEN = item.TEN;
                model.TAI_SAN_ID = item.ID;
                model.TAI_SAN_MA = item.MA;
                model.NGUYEN_GIA = item.NGUYEN_GIA_BAN_DAU;
                model.LY_DO_BIEN_DONG_ID = item.LY_DO_BIEN_DONG_ID;
                model.LOAI_TAI_SAN_ID = item.LOAI_TAI_SAN_ID;
                //set trạng thái biến động là xóa
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA;
                model.DON_VI_ID = item.DON_VI_ID;
                model.NGAY_SU_DUNG = item.NGAY_SU_DUNG;
                model.NGAY_BIEN_DONG = Convert.ToDateTime(item.NGAY_NHAP);
                model.NGAY_TAO = DateTime.Now;
                model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                model.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
            }
            BienDong bd = new BienDong();
            bd = model.ToEntity<BienDong>();
            _bienDongService.InsertBienDong(bd);
            return model;
        }

        public BienDongChiTietModel InsertToBienDongChiTiet (ImportExcelTaiSanModel item, BienDongChiTietModel model, BienDongModel bd)
        {
            if(item != null)
            {
                model.OTO_BIEN_KIEM_SOAT = item.BIEN_KIEM_SOAT;
                model.DIA_CHI = item.DIA_CHI;
                if (item.HINH_THUC_MUA_SAM != null)
                {
                    model.HINH_THUC_MUA_SAM_ID = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(item.HINH_THUC_MUA_SAM).ID;
                }
                model.HTSD_HDSN_KINH_DOANH = item.HTSD_HDSN_KINH_DOANH;
                model.HTSD_HDSN_KINH_DOANH_KHONG = item.HTSD_HDSN_KHONG_KINH_DOANH;
                model.HTSD_LIEN_DOANH = item.HTSD_HDSN_LDLK;
                model.HTSD_CHO_THUE = item.HTSD_HDSN_CHO_THUE;
                model.HTSD_SU_DUNG_KHAC = item.HTSD_KHAC ;
                model.HTSD_SU_DUNG_HON_HOP = item.HTSD_SDHH;
                model.BIEN_DONG_ID = bd.ID;
                model.HTSD_QUAN_LY_NHA_NUOC = item.HTSD_TRU_SO_LAM_VIEC;
                model.OTO_TAI_TRONG = item.TAI_TRONG;
                model.OTO_SO_CAU_XE = item.SO_CAU_XE;
                model.OTO_SO_CHO_NGOI = item.SO_CHO_NGOI;
                model.NHA_SO_TANG = item.SO_TANG;
                model.NGUYEN_GIA = item.NGUYEN_GIA;
                model.HM_LUY_KE = item.HAO_MON_LUY_KE;
                model.HM_GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
                //khấu hao
                model.KH_NGAY_BAT_DAU = item.NGAY_TINH_KHAU_HAO;
                model.KH_GIA_TINH_KHAU_HAO = item.NGUYEN_GIA_TINH_KHAU_HAO;
                model.KH_GIA_TRI_TRICH_THANG = item.NGUYEN_GIA_TINH_KHAU_HAO * item.TY_LE_KHAU_HAO_THANG;
                model.KH_LUY_KE = item.GIA_TRI_KHAU_HAO;
                model.KH_CON_LAI = item.GIA_TRI_kHAU_HAO_CON_LAI;
                //-------------
                model.OTO_CHUC_DANH_ID = Convert.ToDecimal(item.CHUC_DANH_SU_DUNG);
                model.OTO_NHAN_XE_ID = Convert.ToDecimal(item.NHAN_XE);
                if(bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    model.DAT_TONG_DIEN_TICH = item.DIEN_TICH_DATNHA;
                }
                if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    model.NHA_DIEN_TICH_XD = item.DIEN_TICH_DATNHA;
                    model.NHA_TONG_DIEN_TICH_XD = item.TONG_DT_SAN_XD;
                }
            }
            BienDongChiTiet bdct = new BienDongChiTiet();
            bdct = model.ToEntity<BienDongChiTiet>();
            YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(bdct);
            yeuCau_json.DATA_JSON = null;
            bdct.DATA_JSON = yeuCau_json.toStringJson();
            var lstHienTrang = _hientrangService.GetHienTrangs(LoaiHinhTsId: bd.LOAI_HINH_TAI_SAN_ID, isTSDA: _donViService.isDonViBanQuanLyDuAn(bd.DON_VI_ID.GetValueOrDefault()));
            var lstObjHienTrang = new List<ObjHienTrang>();
            if(bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                foreach (var ht in lstHienTrang)
                {
                    var obj = new ObjHienTrang();
                    obj.HienTrangId = ht.ID;
                    obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                    obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                    obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                    switch (obj.HienTrangId)
                    {
                        case 72:
                            obj.GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC;
                            break;
                        case 73:
                            obj.GiaTriNumber = item.HTSD_HDSN_KHONG_KINH_DOANH;
                            break;
                        case 74:
                            obj.GiaTriNumber = item.HTSD_HDSN_KINH_DOANH;
                            break;
                        case 78:
                            obj.GiaTriNumber = item.HTSD_HDSN_CHO_THUE;
                            break;
                        case 79:
                            obj.GiaTriNumber = item.HTSD_HDSN_LDLK;
                            break;
                        case 81:
                            obj.GiaTriNumber = item.HTSD_SDHH;
                            break;
                        case 181:
                            obj.GiaTriNumber = item.HTSD_DE_O;
                            break;
                        case 182:
                            obj.GiaTriNumber = item.HTSD_BO_TRONG;
                            break;
                        case 183:
                            obj.GiaTriNumber = item.HTSD_BI_LAN_CHIEM;
                            break;
                        case 205:
                            obj.GiaTriNumber = item.HTSD_KHAC;
                            break;
                    }
                    lstObjHienTrang.Add(obj);
                }
            }
            else
            {
                if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    foreach (var ht in lstHienTrang)
                    {
                        var obj = new ObjHienTrang();
                        obj.HienTrangId = ht.ID;
                        obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                        obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                        obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                        switch (obj.HienTrangId)
                        {
                            case 82:
                                obj.GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC;
                                break;
                            case 83:
                                obj.GiaTriNumber = item.HTSD_HDSN_KHONG_KINH_DOANH;
                                break;
                            case 84:
                                obj.GiaTriNumber = item.HTSD_HDSN_KINH_DOANH;
                                break;
                            case 85:
                                obj.GiaTriNumber = item.HTSD_HDSN_CHO_THUE;
                                break;
                            case 86:
                                obj.GiaTriNumber = item.HTSD_HDSN_LDLK;
                                break;
                            case 87:
                                obj.GiaTriNumber = item.HTSD_SDHH;
                                break;
                            case 178:
                                obj.GiaTriNumber = item.HTSD_DE_O;
                                break;
                            case 179:
                                obj.GiaTriNumber = item.HTSD_BO_TRONG;
                                break;
                            case 180:
                                obj.GiaTriNumber = item.HTSD_BI_LAN_CHIEM;
                                break;
                            case 209:
                                obj.GiaTriNumber = item.HTSD_KHAC;
                                break;
                        }
                        lstObjHienTrang.Add(obj);
                    }
                }
                else
                {
                    foreach (var ht in lstHienTrang)
                    {
                        var obj = new ObjHienTrang();
                        obj.HienTrangId = ht.ID;
                        obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                        obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                        obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                        var htsd_id = item.HTSD_TAI_SAN_KHAC.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                        if (obj.HienTrangId == Convert.ToDecimal(htsd_id))
                        {
                            obj.GiaTriCheckbox = true;
                        }
                        lstObjHienTrang.Add(obj);
                    }
                }
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = bd.TAI_SAN_ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
            bdct.HTSD_JSON = yeuCau_json.HTSD_JSON;
            //model.BIEN_DONG_ID = bd.ID;
            _bienDongChiTietService.InsertToBienDongChiTiet(bdct);
            
            return bdct.ToModel<BienDongChiTietModel>();
        }
        public virtual void InsertTaiSanNguonVonFromBienDong(ImportExcelTaiSanModel item, TaiSanModel model, BienDongModel bd)
        {
            var nguonVon = ((enumNGUON_VON_IMPORT[])Enum.GetValues(typeof(enumNGUON_VON_IMPORT))).Select(c => (int)c).ToList();
            var _listNV = _nguonVonService.GetNguonVonByIds(nguonVon.Select(c => (decimal)c).ToArray());
            if (_listNV != null)
            {
                foreach (var _nguonVon in _listNV)
                {
                    model.lstNguonVonModel.Add(new NguonVonModel()
                    {
                        ID = _nguonVon.ID,
                        TEN = _nguonVon.TEN
                    });
                }
            }
            var nguonVonJson = model.lstNguonVonModel.toStringJson();
            var lstNguonVon = nguonVonJson.toEntities<NguonVonModel>();
            if (lstNguonVon != null && lstNguonVon.Count > 0)
            {
                List<TaiSanNguonVon> lst = new List<TaiSanNguonVon>();
                foreach (var nv in lstNguonVon)
                {
                    var tsnv = new TaiSanNguonVon();
                    tsnv.TAI_SAN_ID = bd.TAI_SAN_ID;
                    tsnv.NGUON_VON_ID = nv.ID;
                    switch (nv.ID)
                    {
                        case 1:
                            tsnv.GIA_TRI = (decimal)item.NV_NGAN_SACH;
                            break;
                        case 3:
                            tsnv.GIA_TRI = (decimal)item.NV_KHAC;
                            break;
                        case 4:
                            tsnv.GIA_TRI = (decimal)item.NV_VIEN_TRO;
                            break;
                        case 17:
                            tsnv.GIA_TRI = (decimal)item.NV_QUY_HDSN;
                            break;
                    }
                    tsnv.BIEN_DONG_ID = bd.ID;
                    lst.Add(tsnv);
                    _taiSanNguonVonService.InsertTaiSanNguonVon(tsnv);
                }
            }
        }
        public void InsertHaoMonFromTsImport(BienDong bienDong, BienDongChiTiet bienDongChiTiet, HaoMonTaiSanModel haoMonTaiSanModel)
        {
            var ktHaoMonTaiSan = _haoMonTaiSanModelFactory.PrepareHaoMonTaiSanImport(bienDong: bienDong, bienDongChiTiet: bienDongChiTiet, haoMonTaiSanModel: new HaoMonTaiSanModel());
            //----
            var moment = DateTime.Now;
            var _kt_old = new HaoMonTaiSan();
            if (bienDong.NGAY_BIEN_DONG.Value.Year < moment.Year)
            {
                for (int namHaoMon = bienDong.NGAY_BIEN_DONG.Value.Year; namHaoMon <= moment.Year; namHaoMon++)
                {
                    ktHaoMonTaiSan.NAM_HAO_MON = namHaoMon;
                    _kt_old = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: ktHaoMonTaiSan.TAI_SAN_ID, namBaoCao: ktHaoMonTaiSan.NAM_HAO_MON);
                    if (_kt_old != null)
                    {
                        _haoMonTaiSanModelFactory.PrepareHaoMonTaiSan(model: ktHaoMonTaiSan, item: _kt_old);
                        _haoMonTaiSanService.UpdateHaoMonTaiSan(_kt_old);
                    }
                    else
                    {
                        _haoMonTaiSanService.InsertHaoMonTaiSan(ktHaoMonTaiSan.ToEntity<HaoMonTaiSan>());
                    }
                }
            }
            else
            {
                _kt_old = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: ktHaoMonTaiSan.TAI_SAN_ID, namBaoCao: ktHaoMonTaiSan.NAM_HAO_MON);
                if (_kt_old != null)
                {
                    _haoMonTaiSanModelFactory.PrepareHaoMonTaiSan(model: ktHaoMonTaiSan, item: _kt_old);
                    _haoMonTaiSanService.UpdateHaoMonTaiSan(_kt_old);
                }
                else
                {
                    _haoMonTaiSanService.InsertHaoMonTaiSan(ktHaoMonTaiSan.ToEntity<HaoMonTaiSan>());
                }
            }
        }
        public void InsertKhauHaoFromTsImport(ImportExcelTaiSanModel item, BienDongModel bienDong, BienDongChiTietModel bienDongChiTiet, TaiSanKhauHaoModel taiSanKhauHaoModel) 
        {
            taiSanKhauHaoModel.NGAY_BAT_DAU = bienDongChiTiet.KH_NGAY_BAT_DAU;
            if (bienDongChiTiet.KH_NGAY_BAT_DAU != null)
            {
                taiSanKhauHaoModel.NGAY_KET_THUC = Convert.ToDateTime(bienDongChiTiet.KH_NGAY_BAT_DAU).AddMonths(Convert.ToInt32(item.SO_THANG_KHAU_HAO));
            }
            taiSanKhauHaoModel.SO_THANG_KHAU_HAO = item.SO_THANG_KHAU_HAO;
            taiSanKhauHaoModel.TAI_SAN_ID = bienDong.TAI_SAN_ID;
            taiSanKhauHaoModel.TY_LE_KHAU_HAO = item.TY_LE_KHAU_HAO_THANG * 100;
            taiSanKhauHaoModel.TY_LE_NGUYEN_GIA_KHAU_HAO = (item.NGUYEN_GIA_TINH_KHAU_HAO / item.NGUYEN_GIA) * 100;
            TaiSanKhauHao taiSanKhauHao = new TaiSanKhauHao();
            taiSanKhauHao = taiSanKhauHaoModel.ToEntity<TaiSanKhauHao>();
            _taiSanKhauHaoService.InsertTaiSanKhauHao(taiSanKhauHao);
        }
    }
}
