using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    [Validator(typeof(DanhMucDuAnValidator))]
    public class DuAnModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public Decimal? LOAI_DU_AN_ID { get; set; }
        public DateTime? NGAY_BAT_DAU { get; set; }
        public DateTime? NGAY_KET_THUC { get; set; }
        public Decimal? TONG_KINH_PHI { get; set; }
        public Decimal? HINH_THUC_ID { get; set; }
        public String SO_QUYET_DINH_PHE_DUYET { get; set; }
        public String CHU_DAU_TU { get; set; }
        public String DIA_DIEM { get; set; }
        public String NGUON_VON { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? NGUON_NS { get; set; }
        public Decimal? NGUON_ODA { get; set; }
        public Decimal? NGUON_VIEN_TRO { get; set; }
        public Decimal? NGUON_KHAC { get; set; }
        public String KIEU { get; set; }
        public String CO_QUAN_TAI_CHINH { get; set; }
        public String MA_LOAI_DU_AN { get; set; }
        public String MA_NHOM_DU_AN { get; set; }
        public String TEN_NHOM_DU_AN { get; set; }
        public String MA_HT { get; set; }
        public String TEN_HT { get; set; }
        public String HT_QUANLY { get; set; }
        public Boolean? HIEU_LUC { get; set; }
        public String MA_DVQHNS { get; set; }
        public String MA_DU_AN_CU { get; set; }
        public Boolean? TRANG_THAI_ID { get; set; }
        [Required(ErrorMessage = "DON_VI_ID null") ]
        public Decimal? DON_VI_ID { get; set; }
        public string MA_DON_VI_CU { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public Decimal? DB_ID { get; set; }
    }
}
