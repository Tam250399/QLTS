using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    public partial class HoatDongModel : BaseGSEntityModel
    {
        #region Properties
        public decimal LOAI_HOAT_DONG_ID { get; set; }
        public decimal LOAI_DANH_MUC { get; set; }
        public decimal? NGUOI_DUNG_ID { get; set; }
        public string KHACH_HANG_EMAIL { get; set; }
        public string TEN_DOI_TUONG { get; set; }
        public string TEN_DANH_MUC { get; set; }
        public string TEN_KIEU_HOAT_DONG { get; set; }
        public string MO_TA { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string IP_TRUY_CAP { get; set; }
        [UIHint("JsonView")]
        public String DU_LIEU { get; set; }
        //Customer full name, customer's user
        public string TEN_DANG_NHAP { get; set; }
        public string TEN_DAY_DU { get; set; }


        #endregion
    }
    public partial class HoatDongSearchModel : BaseSearchModel
    {
        #region Ctor

        public HoatDongSearchModel()
        {
            KIEU_HOAT_DONG_List = new List<SelectListItem>();
        }

        #endregion

        #region Properties


        [UIHint("DateNullable")]
        public DateTime? FromDate { get; set; }


        [UIHint("DateNullable")]
        public DateTime? ToDate { get; set; }
        public int KIEU_HOAT_DONG { get; set; }
        public IList<SelectListItem> KIEU_HOAT_DONG_List { get; set; }

        public string DIA_CHI_MAY { get; set; }
        public string TEN_DOI_TUONG { get; set; }
        public string TEN_DANG_NHAP { get; set; }

        public decimal nguoiDungId { get; set; }
        #endregion
    }
    public partial class HoatDongListModel : BasePagedListModel<HoatDongModel> { }
}
