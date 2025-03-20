using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using System.Collections.Generic;
using System;
using GS.Services.BienDongs;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using GS.Core;

namespace GS.NewAPI.Factories
{
    public class BienDongChiTietModelFactory : IBienDongChiTietModelFactory
    {
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IDonViService _donViService;
        public BienDongChiTietModelFactory(
            IBienDongChiTietService bienDongChiTietService, 
            IHienTrangService hienTrangService, 
            IDonViService donViService)
        {
            _bienDongChiTietService = bienDongChiTietService;
            _hienTrangService = hienTrangService;
            _donViService = donViService;
        }
        public BienDongChiTietModel InsertToBienDongChiTiet(TaiSanModel item, BienDongChiTietModel model, BienDongModel bd)
        {
            if (item != null)
            {
                //test
                model.HTSD_HDSN_KINH_DOANH = item.HIEN_TRANG_SU_DUNG.HD_SN_KINH_DOANH;
                model.HTSD_HDSN_KINH_DOANH_KHONG = item.HIEN_TRANG_SU_DUNG.HD_SN_KHONG_KINH_DOANH;
                model.HTSD_LIEN_DOANH = item.HIEN_TRANG_SU_DUNG.HD_SN_LIEN_DOANH_LK;
                model.HTSD_CHO_THUE = item.HIEN_TRANG_SU_DUNG.HD_SN_CHO_THUE;
                model.HTSD_SU_DUNG_KHAC = item.HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC;
                model.HTSD_SU_DUNG_HON_HOP = item.HIEN_TRANG_SU_DUNG.SU_DUNG_HON_HOP;
                model.BIEN_DONG_ID = (decimal)bd.ID;
                model.HTSD_QUAN_LY_NHA_NUOC = item.HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC;
                model.NGUYEN_GIA = item.NGUYEN_GIA;
                switch (item.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        model.DIA_CHI = item.DIA_CHI;
                        model.HS_CNQSD_SO = item.HO_SO_GIAY_TO.CHUNG_NHAN_QUYEN_SD_DAT;
                        model.HS_CNQSD_NGAY = item.HO_SO_GIAY_TO.NGAY_CAP.CHUNG_NHAN_QUYEN_SD_DAT;
                        model.HS_QUYET_DINH_GIAO_SO = item.HO_SO_GIAY_TO.QD_GIAO_DAT;
                        model.HS_QUYET_DINH_GIAO_NGAY = item.HO_SO_GIAY_TO.NGAY_CAP.QD_GIAO_DAT;
                        model.HS_QUYET_DINH_CHO_THUE_SO = item.HO_SO_GIAY_TO.QD_CHO_THUE_DAT;
                        model.HS_QUYET_DINH_CHO_THUE_NGAY = item.HO_SO_GIAY_TO.NGAY_CAP.QD_CHO_THUE_DAT;
                        model.HS_HOP_DONG_CHO_THUE_SO = item.HO_SO_GIAY_TO.HOP_DONG_CHO_THUE_DAT;
                        model.HS_HOP_DONG_CHO_THUE_NGAY = item.HO_SO_GIAY_TO.NGAY_CAP.HOP_DONG_CHO_THUE_DAT;
                        model.HS_KHAC = item.HO_SO_GIAY_TO.GIAY_TO_KHAC;
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        // dữ liệu kiểu nhà
                        //model.NHA_SO_TANG = item.SO_TANG;
                        //model.NHA_DIEN_TICH_XD = item.DIEN_TICH_DATNHA;
                        //model.NHA_TONG_DIEN_TICH_XD = item.TONG_DT_SAN_XD;
                        break;
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        // dữ liệu kiểu oto
                        //model.OTO_TAI_TRONG = item.TAI_TRONG;
                        //model.OTO_SO_CAU_XE = item.SO_CAU_XE;
                        //model.OTO_SO_CHO_NGOI = item.SO_CHO_NGOI;
                        //model.OTO_BIEN_KIEM_SOAT = item.BIEN_KIEM_SOAT;
                        //model.OTO_CHUC_DANH_ID = Convert.ToDecimal(item.CHUC_DANH_SU_DUNG);
                        //model.OTO_NHAN_XE_ID = Convert.ToDecimal(item.NHAN_XE);
                        break;
                    default:
                        break;
                }
                
                // chưa có hình thức mua sắm hiện tại
                //if (item.HINH_THUC_MUA_SAM != null)
                //{
                //    model.HINH_THUC_MUA_SAM_ID = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(item.HINH_THUC_MUA_SAM).ID;
                //}      
                //model.HM_LUY_KE = item.HAO_MON_LUY_KE;
                //model.HM_GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
                //khấu hao
                //model.KH_NGAY_BAT_DAU = item.NGAY_TINH_KHAU_HAO;
                //model.KH_GIA_TINH_KHAU_HAO = item.NGUYEN_GIA_TINH_KHAU_HAO;
                //model.KH_GIA_TRI_TRICH_THANG = item.NGUYEN_GIA_TINH_KHAU_HAO * item.TY_LE_KHAU_HAO_THANG;
                //model.KH_LUY_KE = item.GIA_TRI_KHAU_HAO;
                //model.KH_CON_LAI = item.GIA_TRI_kHAU_HAO_CON_LAI;
                //-------------
           
                // đã đưa vào switch case
                //if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                //{
                //    model.DAT_TONG_DIEN_TICH = item.DIEN_TICH_DATNHA;
                //}
                //if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                //{
                //    model.NHA_DIEN_TICH_XD = item.DIEN_TICH_DATNHA;
                //    model.NHA_TONG_DIEN_TICH_XD = item.TONG_DT_SAN_XD;
                //}
            }
            BienDongChiTiet bdct = new BienDongChiTiet();
            bdct = model.ToEntity<BienDongChiTiet>();
            // bỏ không dùng 2 bảng này
            YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(bdct);
            yeuCau_json.DATA_JSON = null;
            bdct.DATA_JSON = yeuCau_json.toStringJson();
            var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: bd.LOAI_HINH_TAI_SAN_ID, isTSDA: _donViService.isDonViBanQuanLyDuAn(bd.DON_VI_ID.GetValueOrDefault()));
            var lstObjHienTrang = new List<ObjHienTrang>();
            if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
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
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC;
                            break;
                        case 73:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_KHONG_KINH_DOANH;
                            break;
                        case 74:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_KINH_DOANH;
                            break;
                        case 78:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_CHO_THUE;
                            break;
                        case 79:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_LIEN_DOANH_LK;
                            break;
                        case 81:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.SU_DUNG_HON_HOP;
                            break;
                        case 181:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.DE_O;
                            break;
                        case 182:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.BO_TRONG;
                            break;
                        case 183:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.BI_LAN_CHIEM;
                            break;
                        case 205:
                            obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC;
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
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC;
                                break;
                            case 83:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_KHONG_KINH_DOANH;
                                break;
                            case 84:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_KINH_DOANH;
                                break;
                            case 85:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_CHO_THUE;
                                break;
                            case 86:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.HD_SN_LIEN_DOANH_LK;
                                break;
                            case 87:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.SU_DUNG_HON_HOP;
                                break;
                            case 178:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.DE_O;
                                break;
                            case 179:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.BO_TRONG;
                                break;
                            case 180:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.BI_LAN_CHIEM;
                                break;
                            case 209:
                                obj.GiaTriNumber = item.HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC;
                                break;
                        }
                        lstObjHienTrang.Add(obj);
                    }
                }
                else
                {
                    // chưa cần đền vì đang chưa làm tài sản khác 
                    //foreach (var ht in lstHienTrang)
                    //{
                    //    var obj = new ObjHienTrang();
                    //    obj.HienTrangId = ht.ID;
                    //    obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                    //    obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                    //    obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                    //    var htsd_id = item.HTSD_TAI_SAN_KHAC.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    //    if (obj.HienTrangId == Convert.ToDecimal(htsd_id))
                    //    {
                    //        obj.GiaTriCheckbox = true;
                    //    }
                    //    lstObjHienTrang.Add(obj);
                    //}
                }
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = bd.TAI_SAN_ID,
                lstObjHienTrang = lstObjHienTrang
            };
            //yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
            bdct.HTSD_JSON = hientrangList.toStringJson();
            //model.BIEN_DONG_ID = bd.ID;
            _bienDongChiTietService.InsertToBienDongChiTiet(bdct = null);

            return bdct.ToModel<BienDongChiTietModel>();
        }
    }
}
