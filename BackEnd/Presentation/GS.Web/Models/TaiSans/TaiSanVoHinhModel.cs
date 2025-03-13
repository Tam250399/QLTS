//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using System;
using System.Collections.Generic;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanVoHinhValidator))]
    public class TaiSanVoHinhModel : BaseGSEntityModel
    {
        public TaiSanVoHinhModel()
        {

        }
        public Decimal TAI_SAN_ID { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        //more
        public YeuCauChiTietModel NVYeuCauChiTietModel { get; set; }
        public TaiSanModel TaiSanModel { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public decimal LOAI_TAI_SAN_ID { get; set; }
        public List<HienTrangModel> ListHienTrangModel { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public bool TRANG_THAI_KH { get; set; }
    }
    public partial class TaiSanVoHinhSearchModel : BaseSearchModel
    {
        public TaiSanVoHinhSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanVoHinhListModel : BasePagedListModel<TaiSanVoHinhModel>
    {

    }
}

