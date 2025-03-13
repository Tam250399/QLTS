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
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(HienTrangValidator))]
    public class HienTrangModel : BaseGSEntityModel
    {
        public HienTrangModel()
        {
            this.ddlLoaiDonVi = new List<SelectListItem>();
        }
        public String TEN_HIEN_TRANG { get; set; }
        public Decimal LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal KIEU_DU_LIEU_ID { get; set; }

        //add more
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_TEXT_BOX { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public enumKIEU_DU_LIEU enumKieuDuLieu { get; set; }
        public SelectList KieuDuLieuAvaliable { get; set; }
        public enumNHOM_HIEN_TRANG enumNhomHienTrang { get; set; }
        public string TEN_NHOM_HIEN_TRANG { get; set; }
        public enumHienTrang_HIEN_THI_ID enumHienThiId { get; set; }
        public SelectList NhomHienTrangAvaliable { get; set; }
        public SelectList HienTrangHienThiAvaliable { get; set; }
        public String TEN_LOAI_TAI_SAN { get; set; }
        public String TEN_KIEU_DU_LIEU { get; set; }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public string DB_ID_JSON { get; set; }
        public string DB_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SAP_XEP { get; set; }
        public Decimal? HIEN_THI_ID { get; set; }
        public IList<SelectListItem> ddlLoaiDonVi { get; set; }
        public IList<int> SelectedLoaiDonViIds { get; set; }
        public String LOAI_DON_VI_AP_DUNG { get; set; }
    }
    public partial class HienTrangSearchModel : BaseSearchModel
    {
        public HienTrangSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public Decimal LoaiHinhTSId { get; set; }
        public Decimal KieuDuLieuId { get; set; }
        public Decimal? NhomHienTrangId { get; set; }
        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public SelectList KieuDuLieuAvailable { get; set; }
    }
    public partial class HienTrangListModel : BasePagedListModel<HienTrangModel>
    {

    }

}

