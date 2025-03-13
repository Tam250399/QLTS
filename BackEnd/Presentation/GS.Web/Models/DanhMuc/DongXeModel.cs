//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(DongXeValidator))]
    public class DongXeModel : BaseGSEntityModel
    {
        public DongXeModel()
        {
            dllNhanXe = new List<SelectListItem>();
        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        public String TEN_NHAN_XE { get; set; }
        public IList<SelectListItem> dllNhanXe { get; set; }
        public int? pageIndex { get; set; }
        public int? DB_ID { get; set; }

    }
    public partial class DongXeSearchModel : BaseSearchModel
    {
        public DongXeSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public int? pageIndex { get; set; }
    }
    public partial class DongXeListModel : BasePagedListModel<DongXeModel>
    {

    }
}

