//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.NghiepVu;
using System;

namespace GS.Web.Models.NghiepVu
{
    [Validator(typeof(YeuCauNhatKyValidator))]
    public class YeuCauNhatKyModel : BaseGSEntityModel
    {
        public YeuCauNhatKyModel()
        {

        }
        public Decimal? YEU_CAU_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_NHAT_KY_ID { get; set; }
        public String DATA_JSON { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
    }
    public partial class YeuCauNhatKySearchModel : BaseSearchModel
    {
        public YeuCauNhatKySearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class YeuCauNhatKyListModel : BasePagedListModel<YeuCauNhatKyModel>
    {

    }
}

