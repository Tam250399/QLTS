//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.CCDC;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(CongCuValidator))]
    public class CongCuModel : BaseGSEntityModel
    {
        public CongCuModel()
        {
            DDLNhomCongCu = new List<SelectListItem>();
            DDLDonViBoPhan = new List<SelectListItem>();
            ListLoCongCu = new List<LoCongCuModel>();
            DDLCongCuModel = new List<SelectListItem>();
            PageSize = 1;
        }
        public String MA { get; set; }
        public Decimal? NHOM_CONG_CU_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        // 1 - phân bổ lần đầu - 0 là không phân bổ -- Hungnt thêm phục vụ yêu cầu jira
        public int TRANG_THAI_PHAN_BO { get; set; }
        public SelectList TrangThaiPhanBoAvailable { get; set; }
        //
        public String CHUNG_TU_SO { get; set; }
        [UIHint("Date")]
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public String GHI_CHU { get; set; }
        public String NGUOI_TAO_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime NGAY_TAO { get; set; }
        public Decimal DON_VI_ID { get; set; }
        public String TEN { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DON_GIA { get; set; }
        [UIHint("InputAddon")]
        public Decimal? THANH_TIEN { get; set; }
        public Guid GUID { get; set; }
        public Decimal SoLuongCoThePhanBo { get; set; }

        //add more
        public string CurrentDonViMa { get; set; }
        public string CurrentDonViTen { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        public SelectList DDLTrangThai { get; set; }
        public enumTrangThaiCongCu TrangThaiCongCuEnum { get; set; }
        public List<LoCongCuModel> ListLoCongCu { get; set; }
        public Decimal? SoLuongPhanBo { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayXuatNhap { get; set; }
        public String GhiChuXuatNhap { get; set; }
        public String TenXuatNhap { get; set; }
        public String ChungTuSoXuatNhap { get; set; }
        [UIHint("DateNullable")]
        public DateTime? ChungTuNgayXuatNhap { get; set; }
        public Decimal XuatNhapId { get; set; }
        public Guid XuatNhapGuid { get; set; }
        public SelectList DDLLyDoTang { get; set; }
        public enumMucDichXuatNhap MucDichXuatNhapEnum { get; set; }
        public Decimal? MucDichXuatNhapId { get; set; }
        public String stringGuid { get; set; }
        public Decimal SoLuongKiemKe { get; set; }
        public List<SelectListItem> DDLCongCuModel { get; set; }
        public Decimal? MapId { get; set; }

        // show list view
        public string ChungTuNgayText { get; set; }
        public string NhomCongCuText { get; set; }
        public string DonViText { get; set; }
        public string DonGiaText { get; set; }
        public string ThanhTienText { get; set; }
        public Decimal? PageSize { get; set; }
    }
    public class LoCongCuModel : BaseGSEntityModel
    {
        public Guid GUID { get; set; }
        public Decimal? CongCuId { get; set; }
        public String MA { get; set; }
        public Decimal? NHOM_CONG_CU_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String GHI_CHU { get; set; }
        public String TEN { get; set; }
        public Decimal? DON_GIA { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public Decimal? MapId { get; set; }
        public Decimal? THOI_HAN_PHAN_BO { get; set; }
    }
    public partial class CongCuSearchModel : BaseSearchModel
    {
        public CongCuSearchModel()
        {
            isPhanBo = false;
            ListLoaiCongCu = new List<SelectListItem>();
        }
        public Decimal? PageIndex { get; set; }
        public string KeySearch { get; set; }
        public bool isPhanBo { get; set; }
        public Guid XuatNhapGuid { get; set; }
        public Decimal? BoPhanKiemKeId { get; set; }
        public Decimal? KiemKeId { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayNhapTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayNhapDen { get; set; }
        public String SoChungTu { get; set; }
        public int LoaiCongCu { get; set; }
        public List<SelectListItem> ListLoaiCongCu { get; set; }
    }
    public partial class CongCuListModel : BasePagedListModel<CongCuModel>
    {

    }
}

