//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(DeNghiXuLyValidator))]
    public class DeNghiXuLyModel : BaseGSEntityModel
    {
        public DeNghiXuLyModel()
        {

        }
        public Decimal DON_VI_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime NGAY_DE_NGHI { get; set; }
        [UIHint("DateNullable")]
        public DateTime NGAY_XU_LY { get; set; }
        public String NOI_DUNG { get; set; }
        public String MA_DON_VI { get; set; }
        public String TEN_DON_VI { get; set; }
        public String GHI_CHU { get; set; }
        public String SO_PHIEU { get; set; }
        public List<DeNghiXuLyTaiSanModel> TAI_SAN_DE_NGHI { get; set; }
    }
    public partial class DeNghiXuLySearchModel : BaseSearchModel
    {
        public DeNghiXuLySearchModel()
        {
        }
        public string KeySearch { get; set; }
        public string SO_PHIEU { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_DE_NGHI { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_XU_LY { get; set; }
        public Decimal? DON_VI_ID { get; set; }
    }
    public partial class DeNghiXuLyListModel : BasePagedListModel<DeNghiXuLyModel>
    {

    }
}

