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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(QuyenValidator))]
    public class QuyenModel : BaseGSEntityModel
    {
        public QuyenModel()
        {

        }
        [GSResourceDisplayName("Mã")]
        public String MA { get; set; }
        [GSResourceDisplayName("Tên")]
        public String TEN { get; set; }
        [GSResourceDisplayName("Phân hệ")]
        public String PHAN_HE { get; set; }
        public bool IdChon { get; set; }

    }
    public partial class QuyenSearchModel : BaseSearchModel
    {
        public QuyenSearchModel()
        {
            ListQuyenDaChon = new List<int>();
        }
        [GSResourceDisplayName("Từ khóa")]
        public string KeySearch { get; set; }
        public IList<int> ListQuyenDaChon { get; set; }
        public decimal idVaiTro { get; set; }


    }
    public partial class QuyenListModel : BasePagedListModel<QuyenModel>
    {

    }
}

