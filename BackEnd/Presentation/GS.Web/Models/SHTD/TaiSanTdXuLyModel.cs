//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.SHTD;
using GS.Web.Framework.Models;
using GS.Web.Models.ThuocTinh;
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(TaiSanTdXuLyValidator))]
    public class TaiSanTdXuLyModel : BaseGSEntityModel
    {
        public TaiSanTdXuLyModel()
        {
            this.GUID = Guid.NewGuid();
            this.DDLTaiSanTD = new List<SelectListItem>();
            this.DDLPhuongAnXuLy = new List<SelectListItem>();
            this.DDLPhuongThucXuLy = new List<SelectListItem>();
            this.ListSL = new List<ListSoLuong>();
            this.listVuViec = new List<int>();
            this.is_vali = true;
            this.listTT = new List<modelThuocTinh>();
            this.ListModel = new List<TaiSanTdXuLyModel>();
            this.ListTaiSanId = new List<int>();
            this.DDLHinhThucXuLy = new List<SelectListItem>();
            this.isThemNhieuTaiSan = false;
            this.isKetQua = false;
            this.DDLDonVi = new List<SelectListItem>();
            ListKetQuaDC = new List<KetQuaModel>();
            this.ketQuaModel = new KetQuaModel();
        }
        public Guid GUID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal XU_LY_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG { get; set; }
        public int? PHUONG_AN_XU_LY_ID { get; set; }
        public int? HINH_THUC_XU_LY_ID { get; set; }
        public Decimal? DON_VI_DIEU_CHUYEN_ID { get; set; }
        public String GHI_CHU { get; set; }
        public string DB_ID { get; set; }
        public string DB_XU_LY_ID { get; set; }
        public string DB_TAI_SAN_ID { get; set; }

        public string TenHinhThuc { get; set; }
        public string TenPhuongAn { get; set; }
        public string TenTaiSan { get; set; }
        public string TenNguonGoc { get; set; }
        public string TenLoaiTaiSan { get; set; }
        public Guid GuidView { get; set; }
        // hungnt- add
        public string TenQuyetDinh { get; set; }
        public string SoQuyetDinh { get; set; }

        public string ThuocTinh { get; set; }
        public Guid XuLyGuid { get; set; }
        public decimal SoLuongCon { get; set; }

        public decimal SoLuongBanDau { get; set; }
        public decimal SLCoTheXuLy { get; set; }
        public string SoLuongBanDauText { get; set; }
        public string SoLuongText { get; set; }
        public Decimal? ChiPhiTong { get; set; }
        public Decimal NSNNTong { get; set; }
        public Decimal TKTGTong { get; set; }
        public IList<ListSoLuong> ListSL {get;set;}
        public List<int> listVuViec { get; set; }
        public List<modelThuocTinh> listTT { get; set; }
        public bool is_vali { get; set; }
        public Decimal ChiPhiCu { get; set; }
        public int  LoaiXuLy { get; set; }
        public string ChiPhiXuLy { get; set; }
        public string DonViTinh { get; set; }
        public string NguyenGia { get; set; }
        public string GiaTri { get; set; }
        public string GiaTriGT { get; set; }
        public string GiaTriNSNN { get; set; }
        public string GiaTriTKTG { get; set; }
        public IList<int> ListTaiSanId { get; set; }
        public bool isThemNhieuTaiSan { get; set; }
        public bool isKetQua { get; set; }
        public string ListSLTaiSanDaChon { get; set; }
        public DateTime? NgayTichThu { get; set; }
        public string JsonDDLHinhThucXuLy { get; set; }
        public string JsonDDLPhuongAnXuLy { get; set; }
        public Decimal DonViID { get; set; }
        public Decimal SoLuongDC { get; set; }
        public DateTime? NgayDieuChuyen { get; set; }
        public Decimal GiaTriXuLy { get; set; }
        public KetQuaModel ketQuaModel { get; set; }
        public string MaPhuongAnXuLy { get; set; }
        public List<TaiSanTdXuLyModel> ListModel { get; set; }
        public List<SelectListItem> DDLHinhThucXuLy { get; set; }
        public List<SelectListItem> DDLTaiSanTD { get; set; }
        public List<SelectListItem> DDLPhuongAnXuLy { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<SelectListItem> DDLPhuongThucXuLy { get; set; }
        public List<KetQuaModel> ListKetQuaDC { get; set; }
        #region Them
        public string DonViNhanDieuChuyenTen { get; set; }
        public decimal? DonViNhanDieuChuyenId { get; set; }
        #endregion



    }
    public partial class TaiSanTdXuLySearchModel : BaseSearchModel
    {
        public TaiSanTdXuLySearchModel()
        {
            TrangThaiXuLy = (int)enumTrangThaiXuLy.TonTai;
            isKetQua = false;
            ListQuyetDinh = new List<decimal?>();
        }
        public string KeySearch { get; set; }
        public int XuLyId { get; set; }
        public int TrangThaiXuLy { get; set; }
        public Guid XuLyGuid { get; set; }
        public bool isKetQua { get; set; }
        public List<SelectListItem> DDLPhuongAnXuLy { get; set; }
        //add more by hungnt
        public List<decimal?> ListQuyetDinh { get; set; }

    }
    public partial class TaiSanTdXuLyListModel : BasePagedListModel<TaiSanTdXuLyModel>
    {

    }
    public partial class TaiSanTdXuLyDongBoModel : BaseGSEntityModel
    {
        public Guid GUID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal XU_LY_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? PHUONG_AN_XU_LY_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public String GHI_CHU { get; set; }
        public string DB_ID { get; set; }
        public string DB_XU_LY_ID { get; set; }
        public string DB_TAI_SAN_ID { get; set; }
    }
}

