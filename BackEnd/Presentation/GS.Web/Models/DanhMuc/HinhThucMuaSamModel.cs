//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using System;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(HinhThucMuaSamValidator))]
    public class HinhThucMuaSamModel : BaseGSEntityModel
    {
        public HinhThucMuaSamModel()
        {

        }
        public String MA { get; set; }
        public String TEN { get; set; }
    }
    public partial class HinhThucMuaSamSearchModel : BaseSearchModel
    {
        public HinhThucMuaSamSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class HinhThucMuaSamListModel : BasePagedListModel<HinhThucMuaSamModel>
    {

    }
}

