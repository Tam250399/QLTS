//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using GS.Web.Validators.HeThong;
using System;
namespace GS.Web.Models.HeThong
{
    [Validator(typeof(NhanHienThiValidator))]
    public class NhanHienThiModel : BaseGSEntityModel
    {
        public NhanHienThiModel()
        {
        }
        [GSResourceDisplayName("Mã")]
        public String MA { get; set; }
        [GSResourceDisplayName("Giá trị")]
        public String GIA_TRI { get; set; }
    }
    public partial class NhanHienThiSearchModel : BaseSearchModel
    {
        public NhanHienThiSearchModel()
        {
        }
        public String MA { get; set; }
        public String GIA_TRI { get; set; }
    }
    public partial class NhanHienThiListModel : BasePagedListModel<NhanHienThiModel>
    {

    }
}

