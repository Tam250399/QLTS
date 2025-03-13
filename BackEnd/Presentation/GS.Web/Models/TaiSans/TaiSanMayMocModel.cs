//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanMayMocValidator))]
    public class TaiSanMayMocModel : BaseGSEntityModel
    {
        public TaiSanMayMocModel()
        {
            //LoaiTaiSanModel = new LoaiTaiSanModel();
            //TaiSanModel = new TaiSanModel();
            NVYeuCauChiTietModel = new YeuCauChiTietModel(); 
            ListHienTrangModel = new List<HienTrangModel>();
            lstHienTrang = new List<ObjHienTrang>();
            ListPhuKienHuuHinh = new List<TaiSanPhuKienHuuHinh>();
        }
        public Decimal TAI_SAN_ID { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public String PHU_KIEN_JSON { get; set; }
        public YeuCauChiTietModel NVYeuCauChiTietModel { get; set; }
        public TaiSanModel TaiSanModel { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public decimal LOAI_TAI_SAN_ID { get; set; }
        public List<HienTrangModel> ListHienTrangModel { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public bool TRANG_THAI_KH { get; set; }
        public IList<TaiSanPhuKienHuuHinh> ListPhuKienHuuHinh { get; set; }
        public String THONG_SO_KY_HIEU { get; set; }
    }
    public class TaiSanPhuKienHuuHinh
    {
        public String PHU_KIEN_MA { get; set; }
        public String PHU_KIEN_TEN { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHU_KIEN_SO_LUONG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHU_KIEN_DON_GIA { get; set; }
        public String PHU_KIEN_MO_TA { get; set; }
        public String PHU_KIEN_GHI_CHU { get; set; }
    }
    public partial class TaiSanMayMocSearchModel : BaseSearchModel
    {
        public TaiSanMayMocSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanMayMocListModel : BasePagedListModel<TaiSanMayMocModel>
    {

    }
}

