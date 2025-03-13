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
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanVktValidator))]
    public class TaiSanVktModel : BaseGSEntityModel
    {
        public TaiSanVktModel()
        {
            lstHienTrang = new List<ObjHienTrang>();
            NVYeuCauChiTietModel = new YeuCauChiTietModel();
            ListHienTrangModel = new List<HienTrangModel>();
        }
        public Decimal TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DIEN_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? THE_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? CHIEU_DAI { get; set; }
        public YeuCauChiTietModel NVYeuCauChiTietModel { get; set; }
        public TaiSanModel TaiSanModel { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public decimal LOAI_TAI_SAN_ID { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public List<HienTrangModel> ListHienTrangModel { get; set; }
        public bool TRANG_THAI_KH { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }

    }
    public partial class TaiSanVktSearchModel : BaseSearchModel
    {
        public TaiSanVktSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanVktListModel : BasePagedListModel<TaiSanVktModel>
    {

    }
}

