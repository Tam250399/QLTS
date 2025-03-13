using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using FluentValidation.Attributes;
using GS.Core;
using GS.Web.Framework.Models;
using GS.Web.Validators.BaoCaoDienTu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaoDienTu
{
    [Validator(typeof(BaoCaoDienTuValidator))]
    public class BaoCaoDienTuModel : BaseGSEntityModel
    {
        public BaoCaoDienTuModel()
        {

        }
        public Decimal? DON_VI_ID { get; set; }
        public string TEN { get; set; }
        public string DonViMa { get; set; }
        public string DonViTen { get; set; }
        public string NguoiTaoTen { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BAO_CAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Decimal? NGUOI_DUYET_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public List<SelectListItem> DDLTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public XtraReport ReportOnline { get; set; }
        public string SO_VAN_BAN { get; set; }
        public string TINH_HINH_BAN_HANH_VAN_BAN { get; set; }
        public string THUC_TRANG { get; set; }
        public string TINH_HINH_TANG_GIAM { get; set; }
        public string CONG_TAC_CHI_DAO { get; set; }
        public string TINH_HINH_THUC_HIEN { get; set; }
        public string DANH_GIA_TICH_CUC { get; set; }
        public string DANH_GIA_TINH_HINH { get; set; }
        public string KET_QUA_KHAC { get; set; }
        public string KIEN_NGHI { get; set; }
        public string NOI_NHAN { get; set; }
        public string TenNguoiDuyet { get; set; }
        public string LY_DO_TU_CHOI { get; set; }
        public string TenFile { get; set; }
        public Decimal FILE_ID { get; set; }
        public Guid GUID { get; set; }
    }
    public partial class BaoCaoDienTuSearchModel : BaseSearchModel
    {
        public BaoCaoDienTuSearchModel()
        {
            ddlTrangThai = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BAO_CAO { get; set; }
        public decimal TRANG_THAI_ID { get; set; }
        public List<SelectListItem> ddlTrangThai { get; set; }
        public decimal DonVi { get; set; }
        public string TEN_DON_VI { get; set; }
        public string LY_DO_TU_CHOI { get; set; }
        public string MA_DON_VI { get; set; }
        public XlsExportMode enumDinhDanhXlsExcel { get; set; }
        public XlsxExportMode enumDinhDanhXlsxExcel { get; set; }
        public string SO_VAN_BAN { get; set; }
        public string TINH_HINH_BAN_HANH_VAN_BAN { get; set; }
        public string THUC_TRANG { get; set; }
        public string TINH_HINH_TANG_GIAM { get; set; }
        public string CONG_TAC_CHI_DAO { get; set; }
        public string TINH_HINH_THUC_HIEN { get; set; }
        public string DANH_GIA_TICH_CUC { get; set; }
        public string DANH_GIA_TINH_HINH { get; set; }
        public string KET_QUA_KHAC { get; set; }
        public string KIEN_NGHI { get; set; }
        public string NOI_NHAN { get; set; }
        public enumHanhDongListDuyetBaoCao enumHanhDongListDuyetBaoCao { get; set; }
        public Decimal? FILE_ID { get; set; }
    }
    public partial class BaoCaoDienTuListModel : BasePagedListModel<BaoCaoDienTuModel>
    {

    }
}
