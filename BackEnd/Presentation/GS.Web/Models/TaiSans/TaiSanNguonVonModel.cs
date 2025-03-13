//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using System;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanNguonVonValidator))]
    public class TaiSanNguonVonModel : BaseGSEntityModel
    {
        public TaiSanNguonVonModel()
        {

        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal NGUON_VON_ID { get; set; }
        public Decimal GIA_TRI { get; set; }
        //add more 
        public String TenNguonvon { get; set; }
    }
    public partial class TaiSanNguonVonSearchModel : BaseSearchModel
    {
        public TaiSanNguonVonSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanNguonVonListModel : BasePagedListModel<TaiSanNguonVonModel>
    {

    }
}

