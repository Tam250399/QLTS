//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(ChucDanhValidator))]
    public class ChucDanhModel : BaseGSEntityModel
    {
        public ChucDanhModel()
        {
        }
        public String TEN_CHUC_DANH { get; set; }
        public String MO_TA { get; set; }
        public String MA_CHUC_DANH { get; set; }
        public Decimal? KHOI_DON_VI_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DINH_MUC { get; set; }

        //add more
        public KhoiDonViEnum KhoiDonViEnumId
        {
            get => (KhoiDonViEnum)KHOI_DON_VI_ID;
            set => KHOI_DON_VI_ID = (int)value;
        }
        public String TEN_KHOI_DON_VI { get; set; }
        public KhoiDonViEnum KhoiDonViEnum { get; set; }
        public SelectList ddlKhoiDonVi { get; set; }
        public int? DB_ID { get; set; }

    }
    public partial class ChucDanhSearchModel : BaseSearchModel
    {
        public ChucDanhSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public decimal KhoiDonViIdSearch { get; set; }
        //public SelectList KhoiDonViAvaliable { get; set; }
        public KhoiDonViEnum KhoiDonViEnum { get; set; }
        public SelectList ddlKhoiDonVi { get; set; }
    }
    public partial class ChucDanhListModel : BasePagedListModel<ChucDanhModel>
    {

    }
}

