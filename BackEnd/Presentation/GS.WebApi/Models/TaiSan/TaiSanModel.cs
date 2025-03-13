//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.WebApi.Models.TaiSan
{
    public class TaiSanModel : BaseGSApiModel
    {
        public TaiSanModel()
        {
        }


        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
        public Decimal? NUOC_SAN_XUAT_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? DOI_TAC_ID { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public Decimal? NAM_SAN_XUAT { get; set; }
        public DateTime? NGAY_NHAP { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Guid GUID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public bool? IS_XAC_NHAN { get; set; }
        public DateTime? NGAY_XAC_NHAN { get; set; }
        public bool? IS_MIEN_THUE { get; set; }
        public decimal? GIA_HOA_DON { get; set; }
        public decimal? MIEN_THUE_SO_TIEN { get; set; }
        public String MA_QLDKTS40 { get; set; }
        public bool? IS_DUYET { get; set; }
       
        public String TS_NGUYEN_GIA_MOI_NHAT { get; set; }
        public String TS_GTCL_MOI_NHAT { get; set; }
        public String DAT_DIA_CHI { get; set; }
        public decimal? HM_SO_NAM_CON_LAI { get; set; }
        public decimal? HM_TY_LE { get; set; }
      
        public Decimal DON_VI_CHE_DO_HACH_TOAN_ID { get; set; }
        //khai thac
        public decimal? KHAI_THAC_ID { get; set; }
        public Decimal? DIEN_TICH_KT { get; set; }
        public Decimal? DIEN_TICH_KHAI_THAC { get; set; }
      
        //Thêm Phương Thức mua sắm
        public decimal? PHUONG_THUC_MUA_SAM_ID { get; set; }
        public decimal? DON_VI_MUA_SAM_TAP_TRUNG_ID { get; set; }
        public Decimal? TONG_HOA_HONG_CHIET_KHAU { get; set; }
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public bool TRANG_THAI_KH { get; set; }
    }

}

