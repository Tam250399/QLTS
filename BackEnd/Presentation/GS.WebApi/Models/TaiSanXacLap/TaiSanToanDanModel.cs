using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSanXacLap
{
    [Validator(typeof(TaiSanToanDanValidator))]
    public class TaiSanToanDanModel : BaseGSApiModel
    {
        public TaiSanToanDanModel()
        {
           // this.ListXuLy = new List<XuLyModel>();
        }
        //[Required(ErrorMessage = "QUYET_DINH_TICH_THU_ID null")]
        public Decimal? QUYET_DINH_TICH_THU_ID { get; set; }
        //[Required(ErrorMessage ="TEN_TAI_SAN null")]
        public String TEN_TAI_SAN { get; set; }
        //[Required(ErrorMessage = "LOAI_HINH_TAI_SAN_ID null")]
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        //[Required(ErrorMessage = "TEN_LOAI_TAI_SAN null")]
        public String TEN_LOAI_TAI_SAN { get; set; }
        //[Required(ErrorMessage = "LOAI_TAI_SAN_ID null")]
        public decimal? LOAI_TAI_SAN_ID { get; set; }
        public decimal? DB_LOAI_TAI_SAN_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        //[Required(ErrorMessage = "GIA_TRI_XAC_LAP null")]
        public Decimal? GIA_TRI_XAC_LAP { get; set; }
        //[Required(ErrorMessage = "NGAY_SU_DUNG null")]
        public Decimal? NAM_SU_DUNG { get; set; }
        //[Required(ErrorMessage = "GIA_TRI_TINH null")]
        public Decimal? GIA_TRI_TINH { get; set; }
        public String DON_VI_TINH { get; set; }
        public String BIEN_KIEM_SOAT { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public Decimal? TAI_TRONG { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        public string DB_TAI_SAN_DAT_ID { get; set; }       
        public String DIA_CHI { get; set; }
        public Decimal? SO_CAU_XE { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? TYPE_ID { get; set; }
        public List<XuLyModel> LST_XU_LY { get; set; }
        public string DB_ID { get; set; }
        public string DB_QUYET_DINH_TICH_THU_ID { get; set; }
    }
}
