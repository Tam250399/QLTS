//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;

namespace GS.Web.Models.DB
{
    [Validator(typeof(LogsDongBoCsdlqgValidator))]
    public class LogsDongBoCsdlqgModel : BaseGSEntityModel
    {
        public LogsDongBoCsdlqgModel()
        {

        }
        public String UUID { get; set; }
        public String MA_QLTSC { get; set; }
        public String MA_CSDLQG { get; set; }
        public String MO_TA { get; set; }
        public Decimal? TRANG_THAI { get; set; }
    }
    public partial class LogsDongBoCsdlqgSearchModel : BaseSearchModel
    {
        public LogsDongBoCsdlqgSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class LogsDongBoCsdlqgListModel : BasePagedListModel<LogsDongBoCsdlqgModel>
    {

    }
}

