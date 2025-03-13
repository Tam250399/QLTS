//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(CheDoHaoMonValidator))]
    public class CheDoHaoMonModel : BaseGSEntityModel
    {
        public CheDoHaoMonModel()
        {

        }
        public String MA_CHE_DO { get; set; }
        public String TEN_CHE_DO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TU_NGAY { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DEN_NGAY { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class CheDoHaoMonSearchModel : BaseSearchModel
    {
        public CheDoHaoMonSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class CheDoHaoMonListModel : BasePagedListModel<CheDoHaoMonModel>
    {

    }
}

