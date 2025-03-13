using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSan;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSan
{

    [Validator(typeof(KhaiTacTaiSanValidator))]
    public class DBKhaiThacTaiSanModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "DB_ID null")]
        public string DB_ID { get; set; }
        public string MA_TAI_SAN { get; set; }
        //[Required(ErrorMessage = "LST_MA_TAI_SAN null")]
        public List<string> LST_MA_TAI_SAN { get; set; }
        [Required(ErrorMessage = "LST_MA_TAI_SAN_DB null")]
        public List<string> LST_MA_TAI_SAN_DB { get; set; }
        public Decimal? DIEN_TICH_KHAI_THAC { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public string DON_VI_MA { get; set; }
        public Decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
        [Required(ErrorMessage = "NGAY_KHAI_THAC null")]
        public String NGAY_KHAI_THAC { get; set; }
        [Required(ErrorMessage = "QUYET_DINH_SO null")]
        public String QUYET_DINH_SO { get; set; }
        [Required(ErrorMessage = "QUYET_DINH_NGAY null")]
        public String QUYET_DINH_NGAY { get; set; }
        [Required(ErrorMessage = "NGUOI_DUYET null")]
        public String NGUOI_DUYET { get; set; }
        public String NOI_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        [Required(ErrorMessage = "KHAI_THAC_NGAY_TU null")]
        public String KHAI_THAC_NGAY_TU { get; set; }
        [Required(ErrorMessage = "KHAI_THAC_NGAY_DEN null")]
        public String KHAI_THAC_NGAY_DEN { get; set; }
        public String HOP_DONG_SO { get; set; }
        public String HOP_DONG_NGAY { get; set; }
        public String DOI_TAC { get; set; }
        [Required(ErrorMessage = "SO_TIEN_TAM_TINH null")]
        public Decimal? SO_TIEN_TAM_TINH { get; set; }
        public Decimal? CHO_THUE_GIA { get; set; }
        public Decimal? CHO_THUE_PHUONG_THUC_ID { get; set; }
        public Decimal? LDLK_HINH_THUC_ID { get; set; }
        public String NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public String MA_DB { get; set; }
    }
}
