//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DB;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DB
{
    [Validator(typeof(DBTaiSanValidator))]
    public class DBTaiSanModel : BaseGSEntityModel
    {
        public DBTaiSanModel()
        {

        }
        public Guid GUID { get; set; }
        public String QLDKTS_MA { get; set; }
        public String DB_MA { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public String LOAI_HINH_TAI_SAN_TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public String LOAI_TAI_SAN_TEN { get; set; }
        public Decimal PHAN_MEM_DONG_BO_ID { get; set; }
        public String DATA_JSON { get; set; }
        public DateTime NGAY_DONG_BO { get; set; }
        public Boolean? IS_TAI_SAN_TOAN_DAN { get; set; }
        public String QUYET_DINH_TICH_THU_SO { get; set; }
        public DateTime? QUYET_DINH_TICH_THU_NGAY { get; set; }
        public Decimal? TAI_KHOAN_DONG_BO_ID { get; set; }
        public Boolean? IS_BIEN_DONG { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String TRANG_THAI_TEN { get; set; }
        [UIHint("JsonView")]
        public String RESPONSE { get; set; }
    }
    public partial class DBTaiSanSearchModel : BaseSearchModel
    {
        public DBTaiSanSearchModel()
        {
            this.IS_TaiSanToanDan = 0;
            this.IS_BienDong = 0;
        }
        public string KeySearch { get; set; }
        public string MaTaiSan { get; set; }
        public string MaTaiSanDB { get; set; }
        public decimal? LoaiHinhTaiSan { get; set; }
        public decimal? LoaiTaiSan { get; set; }
        public decimal? TrangThai { get; set; }
        [UIHint("DateNullAble")]
        public DateTime? NgayDB { get; set; }
        public decimal IS_TaiSanToanDan { get; set; }
        public decimal IS_BienDong { get; set; }
        public List<SelectListItem> ddlLoaiHinhTaiSan { get; set; }
        public List<SelectListItem> ddlLoaiTaiSan { get; set; }
        public List<SelectListItem> ddlTrangThai { get; set; }
    }
    public partial class DBTaiSanListModel : BasePagedListModel<DBTaiSanModel>
    {

    }
}

