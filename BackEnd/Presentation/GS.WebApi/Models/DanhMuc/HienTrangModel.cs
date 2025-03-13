
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class HienTrangModel : BaseGSApiModel
    {
        public String TEN_HIEN_TRANG { get; set; }
        public Decimal LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? KIEU_DU_LIEU_ID { get; set; }
        public int? DB_ID { get; set; }
        public string TrangThaiDongBo { get; set; }
        public string MA { get; set; }
        public string Error { get; set; }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public Decimal? SAP_XEP { get; set; }
        public Decimal? HIEN_THI_ID { get; set; }
        public String LOAI_DON_VI_AP_DUNG { get; set; }
    } 
    public enum enumKIEU_DU_LIEU
    {
        TextBox=0,
        Numberic=1,
        CheckBox=2,
        RadioButton=3
    }
    public enum enumNHOM_HIEN_TRANG
    {
        ALL = 0,
        KINH_DOANH_KHONG_KINH_DOANH = 1
    }
}

