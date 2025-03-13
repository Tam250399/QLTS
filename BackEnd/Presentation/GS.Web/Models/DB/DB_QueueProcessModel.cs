//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;

namespace GS.Web.Models.DB
{
    [Validator(typeof(DB_QueueProcessValidator))]
    public class DB_QueueProcessModel : BaseGSEntityModel
    {
        public DB_QueueProcessModel()
        {

        }
        public Guid? GUID { get; set; }
        public String GUID_RESPONSE { get; set; }
        public String URL_CALL { get; set; }
        public String DATA_INPUT { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public DateTime? LAST_SEND_REQUEST { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public string TRANG_THAI_TEN { get; set; }
    }
    public partial class DB_QueueProcessSearchModel : BaseSearchModel
    {
        public DB_QueueProcessSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class DB_QueueProcessListModel : BasePagedListModel<DB_QueueProcessModel>
    {

    }
}

