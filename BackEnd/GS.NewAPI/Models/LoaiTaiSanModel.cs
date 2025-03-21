using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace GS.NewAPI.Models
{
    public class LoaiTaiSanModel : BaseGSApiModel
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
}
