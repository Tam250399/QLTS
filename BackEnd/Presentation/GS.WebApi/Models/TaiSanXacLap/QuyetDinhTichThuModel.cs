using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSan;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSanXacLap
{
    public enum enumTYPE
    {
        INSERT = 1,
        UPDATE = 2,
        DELETE = 3
    }
    [Validator(typeof(QuyetDinhTichThuValidator))]
    public class QuyetDinhTichThuModel : BaseGSApiModel
    {
        
        public QuyetDinhTichThuModel()
        {
            
        }
        //[Required(ErrorMessage = "QUYET_DINH_SO null")]
        public String QUYET_DINH_SO { get; set; }
        //[Required(ErrorMessage = "QUYET_DINH_NGAY null")]
        public DateTime QUYET_DINH_NGAY { get; set; }
        //[Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public String GHI_CHU { get; set; }
        //[Required(ErrorMessage = "DON_VI_ID null")]
        public Decimal? DON_VI_ID { get; set; }
        //[Required(ErrorMessage = "NGUON_GOC_ID null")]
        public Decimal? NGUON_GOC_ID { get; set; }
        //[Required(ErrorMessage = "NGUOI_TAO_ID null")]
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
        public String NGUOI_QUYET_DINH { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? TYPE_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public IList<TaiSanToanDanModel> LST_TAI_SAN { get; set; }
        public string Error { get; set; }
        public String DB_ID { get; set; }
    }
    public class QuyetDinh
    {
        public string QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
    }
}
