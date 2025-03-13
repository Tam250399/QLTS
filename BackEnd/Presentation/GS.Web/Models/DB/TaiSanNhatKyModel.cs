//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DB
{
    [Validator(typeof(TaiSanNhatKyValidator))]
    public class TaiSanNhatKyModel : BaseGSEntityModel
    {
        public TaiSanNhatKyModel()
        {
        }
        public Decimal? TAI_SAN_ID { get; set; }
        public String LOAI_HINH_TAI_SAN_TEN { get; set; }
        public String TRANG_THAI_TEN { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String MA_TAI_SAN_DB { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_DONG_BO { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public Boolean? IS_TAI_SAN_TOAN_DAN { get; set; }
        public Decimal? QUYET_DINH_TICH_THU_ID { get; set; }
        public String BD_IDS { get; set; }
        public String RESPONSE { get; set; }
        public String BD_IDS_DANG_DB { get; set; }
        public String URL_VIEWLOG { get; set; }
    }
    public partial class TaiSanNhatKySearchModel : BaseSearchModel
    {
        public TaiSanNhatKySearchModel()
        {
            this.IS_TaiSanToanDan = 0;
        }
        public string KeySearch { get; set; }
        public string MaTaiSan { get; set; }
        public string MaTaiSanDB { get; set; }
        public decimal? LoaiHinhTaiSan { get; set; }
        public decimal? TrangThai { get; set; }
        [UIHint("DateNullAble")]
        public DateTime? NgayDB { get; set; }
        public decimal IS_TaiSanToanDan { get; set; }
        public List<SelectListItem> ddlLoaiHinhTaiSan { get; set; }
        public List<SelectListItem> ddlTrangThai { get; set; }
    }
    public partial class TaiSanNhatKyListModel : BasePagedListModel<TaiSanNhatKyModel>
    {

    }
}

