//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using GS.Web.Validators.DanhMuc;
using System;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(NhanXeValidator))]
    public class NhanXeModel : BaseGSEntityModel
    {
        public NhanXeModel()
        {

        }
        [GSResourceDisplayName("Mã")]
        public String MA { get; set; }
        [GSResourceDisplayName("Tên")]
        public String TEN { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class NhanXeSearchModel : BaseSearchModel
    {
        public NhanXeSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class NhanXeListModel : BasePagedListModel<NhanXeModel>
    {

    }
}

