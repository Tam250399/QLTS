using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(GiamDieuChuyenValidator))]
    public partial class GiamDieuChuyenModel : BaseGSEntityModel
    {
        public GiamDieuChuyenModel()
        {
            ListCongCuDieuChuyenModel = new List<CongCuDieuChuyenModel>();
            DDLLoaiXuatNhap = new List<SelectListItem>();
            IsDieuChuyenNgoai = false;
        }
        public String SO_QUYET_DINH { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_QUYET_DINH { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_DIEU_CHUYEN { get; set; }
        public String SO_CHUNG_TU { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_CHUNG_TU { get; set; }
        public String GHI_CHU { get; set; }
        public String MaCongCuText { get; set; }
        public String MaLoCongCuText { get; set; }
        public String TenCongCuText { get; set; }
        public String NhomCongCuText { get; set; }
        public String BoPhanSuDungText { get; set; }
        public String NgayXuatNhapText { get; set; }
        public String DonGiaText { get; set; }
        public String SoLuongText { get; set; }
        public Decimal MapId { get; set; }
        public Decimal DON_VI_ID { get; set; }
        public String stringMapId { get; set; }
        public List<CongCuDieuChuyenModel> ListCongCuDieuChuyenModel { get; set; }
        public String TenDonViTo { get; set; }
        public bool IsDieuChuyenNgoai { get; set; }
        public bool IsDieuChuyen { get; set; }
        public int LoaiXuatNhapId { get; set; }
        public List<SelectListItem> DDLLoaiXuatNhap { get; set; }
        public string TenLoaiXuatNhap { get; set; }
        public string LyDo { get; set; }
        public DateTime? NgayPhanBo { get; set; }
    }

    public class CongCuDieuChuyenModel
    {
        public String MaCongCu { get; set; }
        public String TenCongCu { get; set; }
        public String NhomCongCuText { get; set; }
        public Decimal SoLuongCoTheChuyen { get; set; }
        public String SoLuongCoTheChuyenText { get; set; }
        public Decimal MapId { get; set; }
        [UIHint("InputAddon")]
        public Decimal SoLuongDieuChuyen { get; set; }
        public Decimal? DonGia { get; set; }
        public String DonGiaText { get; set; }
        public DateTime? NgayPhanBo { get; set; }
    }

    public partial class GiamDieuChuyenListModel : BasePagedListModel<GiamDieuChuyenModel>
    {

    }

    public partial class GiamDieuChuyenSearchModel : BaseSearchModel
    {
        public GiamDieuChuyenSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLLoaiXuatNhap = new List<SelectListItem>();
            DDLNhomCongCu = new List<SelectListItem>();
            DenNgay = DateTime.Now;
        }
        public string KeySearch { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLLoaiXuatNhap { get; set; }
        public bool IsDieuChuyenNgoai { get; set; }
        public Decimal LoaiDieuChuyen { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        public Decimal LoaiCongCu { get; set; }
    }
    public partial class GiamKhacSearchModel : BaseSearchModel
    {
        public GiamKhacSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLLoaiXuatNhap = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public Decimal DonViBoPhanId { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLLoaiXuatNhap { get; set; }
        public int LoaiXuatNhap { get; set; }
        public Decimal LoaiDieuChuyen { get; set; }
    }
}
