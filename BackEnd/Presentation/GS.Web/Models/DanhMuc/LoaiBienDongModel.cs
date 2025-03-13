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
    [Validator(typeof(LoaiBienDongValidator))]
    public class LoaiBienDongModel : BaseGSEntityModel
    {
        public LoaiBienDongModel()
        {

        }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
    }
    public partial class LoaiBienDongSearchModel : BaseSearchModel
    {
        public LoaiBienDongSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class LoaiBienDongListModel : BasePagedListModel<LoaiBienDongModel>
    {

    }
}

