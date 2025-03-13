//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.BaoCao;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.BaoCao
{
    [Validator(typeof(BaoCaoDoiChieuValidator))]
    public class BaoCaoDoiChieuModel : BaseGSEntityModel
    {
        public BaoCaoDoiChieuModel()
        {

        }
        public Decimal? DON_VI_ID { get; set; }
        public String TEN_DON_VI { get; set; }
        public Decimal BAO_CAO_ID { get; set; }
        [UIHint("InputYear")]
        public decimal NAM_BAO_CAO { get; set; }
        public Decimal HE_THONG_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_TAO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public Decimal FILE_ID { get; set; }
        public String FILE_NAME { get; set; }
        //public Object FILE_CONTENT { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<SelectListItem> DDLHeThong { get; set; }
        public List<SelectListItem> DDLPhanBaoCao { get; set; }
        public string TenDonVi { get; set; }
        public string PhanBaoCao { get; set; }
        public string PhanMem { get; set; }
        public string TenFile { get; set; }
        public Decimal?  MauSo { get; set; }
    }
    public partial class BaoCaoDoiChieuSearchModel : BaseSearchModel
    {
        public BaoCaoDoiChieuSearchModel()
        {
            ddlDonVi = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        [UIHint("InputYear")]
        public decimal NamBaoCao { get; set; }
        public decimal DonVi { get; set; }
        public List<SelectListItem> ddlDonVi { get; set; }
    }
    public partial class BaoCaoDoiChieuListModel : BasePagedListModel<BaoCaoDoiChieuModel>
    {

    }
}
