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
    [Validator(typeof(DiaBanValidator))]
    public class DiaBanModel : BaseGSEntityModel
    {
        public DiaBanModel()
        {
            QuocGiaAvaliable = new List<SelectListItem>();
        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public String MA_CHA { get; set; }
        public decimal? VUNG_KINH_TE_ID { get; set; }
        public String MA_PHAN_CAP { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? LOAI_DIA_BAN_ID { get; set; }
        public Decimal? QUOC_GIA_ID { get; set; }
        //add more
        public int? DiabanSubCount { get; set; }
        public string TenQuocGia { get; set; }
        public string TrangThai { get; set; }
        public string TenLoaiDiaBan { get; set; }
        public enumLOAI_DIABAN enumLoaiDiaBan { get; set; }
        public SelectList LoaiDiaBanAvaliable { get; set; }
        public enumTRANG_THAI_DIABAN enumTrangThai { get; set; }
        public SelectList TrangThaiAvaliable { get; set; }
        public string TenDiaBanCha { get; set; }
        public IList<SelectListItem> QuocGiaAvaliable { get; set; }
        public int? DB_ID { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class DiaBanSearchModel : BaseSearchModel
    {
        public DiaBanSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public decimal? ParentId { get; set; }
        public int? pageIndex { get; set; }
    }
    public partial class DiaBanListModel : BasePagedListModel<DiaBanModel>
    {

    }
}

