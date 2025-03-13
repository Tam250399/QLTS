//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(LoaiDonViValidator))]
    public class LoaiDonViModel : BaseGSEntityModel
    {
        public LoaiDonViModel()
        {
            dllTreeLaiHinhDonVi = new List<SelectListItem>();
        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public Decimal? SAP_XEP { get; set; }
        public String MA_PHAN_CAP { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? CHE_DO_HOACH_TOAN_ID { get; set; }

        //add more
        public int LOAI_DON_VI_SUB_COUNT { get; set; }
        public IList<SelectListItem> dllTreeLaiHinhDonVi { get; set; }
        public enumCHE_DO_HACH_TOAN CheDoHoachToanEnum { get; set; }
        public SelectList dllCheDoHachToan { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class LoaiDonViSearchModel : BaseSearchModel
    {

        const decimal TreeLevelStart = 1;
        public LoaiDonViSearchModel()
        {
            TreeLevel = TreeLevelStart;
        }
        public string KeySearch { get; set; }
        public decimal ParentID { get; set; }
        public decimal? TreeLevel { get; set; }
    }
    public partial class LoaiDonViListModel : BasePagedListModel<LoaiDonViModel>
    {

    }
}

