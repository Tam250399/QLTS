//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(DeNghiXuLyTaiSanValidator))]
    public class DeNghiXuLyTaiSanModel : BaseGSEntityModel
    {
        public DeNghiXuLyTaiSanModel()
        {
            ListLyDo = new List<SelectListItem>();
        }
        public Decimal? DE_NGHI_XU_LY_ID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public String PHUONG_THUC_XU_LY { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String TEN_TAI_SAN { get; set; }
        public DateTime? TAI_SAN_NGAY_SU_DUNG { get; set; }
        public String TAI_SAN_NGUYEN_GIA{ get; set; }
        public String TAI_SAN_GTCL { get; set; }
        public IList<SelectListItem> ListLyDo { get; set; }
    }
    public partial class DeNghiXuLyTaiSanSearchModel : BaseSearchModel
    {
        public DeNghiXuLyTaiSanSearchModel()
        {
        }
        public Decimal? DE_NGHI_XU_LY_ID { get; set; }
        public string KeySearch { get; set; }
    }
    public partial class DeNghiXuLyTaiSanListModel : BasePagedListModel<DeNghiXuLyTaiSanModel>
    {

    }
}

