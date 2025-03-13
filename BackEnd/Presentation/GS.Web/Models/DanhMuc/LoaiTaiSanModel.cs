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
    [Validator(typeof(LoaiTaiSanValidator))]
    public class LoaiTaiSanModel : BaseGSEntityModel
    {
        public LoaiTaiSanModel()
        {
            CheDoHaoMonAvaliable = new List<SelectListItem>();
        }
        
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_TY_LE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_THOI_HAN_SU_DUNG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_TY_LE { get; set; }
        public String MO_TA { get; set; }
        public Decimal? CHE_DO_HAO_MON_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public string DON_VI_TINH { get; set; }
        public decimal? SO_THU_TU { get; set; }
        //Add more
        public String HM_TyLe { get; set; }
        public String KH_TyLe { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public IList<SelectListItem> CheDoHaoMonAvaliable { get; set; }
        public int CountSub { get; set; }
        public string TenLoaiHinhTaiSan { get; set; }
        public string TaiSanChaName { get; set; }
        //
        public String TenDonVi { get; set; }
        public decimal DonViID { get; set; }
        public Decimal ThoiHanSuDung { get; set; }
        [UIHint("InputAddon")]
        public Decimal? OTO_CHO_NGOI_TU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? OTO_CHO_NGOI_DEN { get; set; }
        public decimal? OTO_LOAI_XE_ID { get; set; }
        public IList<SelectListItem> DDLLoaiXe { get; set; }
        public string DB_ID_JSON { get; set; }
        public bool isDisabled { get; set; }
        public string DB_ID { get; set; }

    }

    public partial class LoaiTaiSanSearchModel : BaseSearchModel
    {
        public LoaiTaiSanSearchModel()
        {
            CheDoAvaliable = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public decimal? CheDoId { get; set; }
        public decimal? Id { get; set; }
        public IList<SelectListItem> CheDoAvaliable { get; set; }
        public decimal? ParentId { get; set; }
        public decimal DonViId { get; set; }

		/// <summary>
		/// có lấy loại tài sản bé nhất không
		/// </summary>
		public bool IsHasNoChildren { get; set; }

	}
    public partial class LoaiTaiSanListModel : BasePagedListModel<LoaiTaiSanModel>
    {

    }
   
}

namespace GS.Web.Models.DanhMuc
{
    public partial class LoaiHinhTaiSanModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isTangMoi { get; set; }
        public bool isCreateTSDA { get; set; }
		public decimal? LoaiBienDongId { get; set; }
        //check TS quản lý như tscd khác
        public bool isTSQL { get; set; }
    }
}

namespace GS.Web.Models.DanhMuc
{
    public enum enumNhapLieu
    {
        TaiSanCong = 1,
        TaiSanXacLap = 2,
        CongCuDungCu = 3,
        KhaiThac = 4
    }
    public partial class NhapLieuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enumNhapLieu EnumNhapLieu { get; set; }
        public Boolean IS_LA_DON_VI_NHAP_LIEU { get; set; }
    }
}