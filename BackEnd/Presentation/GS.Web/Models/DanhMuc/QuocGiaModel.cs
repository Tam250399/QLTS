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
    [Validator(typeof(QuocGiaValidator))]
    public class QuocGiaModel : BaseGSEntityModel
    {
        public QuocGiaModel()
        {

        }
        [GSResourceDisplayName("Mã")]
        public String MA { get; set; }
        [GSResourceDisplayName("Tên")]
        public String TEN { get; set; }
        [GSResourceDisplayName("Mô tả")]
        public String MO_TA { get; set; }
        //add more
        public int pageIndex { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class QuocGiaSearchModel : BaseSearchModel
    {
        public QuocGiaSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class QuocGiaListModel : BasePagedListModel<QuocGiaModel>
    {

    }
}

