using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSan;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSan
{

        [Validator(typeof(TestValidator))]
        public class TestAPIModel : BaseGSEntityModel
        {
            public TestAPIModel()
            {
                //DDLDonViBoPhan = new List<SelectListItem>();
                //ListHoiDongKiemKe = new List<HoiDongKiemKeModel>();
                //NGAY_KIEM_KE = DateTime.Now;
            }
            public Decimal DON_VI_ID { get; set; }
            public String SO_BIEN_BAN { get; set; }
            //[UIHint("DateNullable")]
            public DateTime NGAY_KIEM_KE { get; set; }
            public Decimal? DON_VI_BO_PHAN_ID { get; set; }
            public String NGUOI_LAP_BIEU { get; set; }
            public String DAI_DIEN_BPSD { get; set; }
            public String KE_TOAN { get; set; }
            public String TRUONG_BAN { get; set; }
            public DateTime? NGAY_TAO { get; set; }
            public Decimal? NGUOI_TAO_ID { get; set; }
            // more
            public string MaDonVi { get; set; }
            public string TenDonVi { get; set; }
            //public List<SelectListItem> DDLDonViBoPhan { get; set; }
            public String DonViBoPhanText { get; set; }
            public String NgayKiemKeText { get; set; }
            //public List<HoiDongKiemKeModel> ListHoiDongKiemKe { get; set; }
        }
        public partial class HoiDongKiemKeModel
        {
            public Decimal ID { get; set; }
            public String HO_TEN { get; set; }
            public String CHUC_VU { get; set; }
            public String DAI_DIEN { get; set; }
            public Decimal VI_TRI_ID { get; set; }
        }
        public partial class TaiSanKiemKeSearchModel : BaseSearchModel
        {
            public TaiSanKiemKeSearchModel()
            {
            }
        }
    
}
