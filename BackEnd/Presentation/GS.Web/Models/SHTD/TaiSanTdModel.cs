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
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.SHTD
{
    //public enum enumLOAI_HINH_TAI_SAN_TOAN_DAN
    //{
    //    ALL = 0,
    //    DAT = 1,
    //    NHA = 2,
    //    OTO = 3,
    //    TAI_SAN_KHAC = 4
    //}
    [Validator(typeof(TaiSanTdValidator))]
	public class TaiSanTdModel :BaseGSEntityModel
	{
        public TaiSanTdModel(){

            this.GUID = Guid.NewGuid();
            this.DDLLoaiTaiSan = new List<SelectListItem>();
            this.DDLNguonGocTaiSan = new List<SelectListItem>();
            this.DDLDat = new List<SelectListItem>();
            this.DDLNHA = new List<SelectListItem>();
            this.quyetdinh = new QuyetDinhTichThuModel();
            this.listModel = new List<TaiSanTdModel>();
            this.ListTaiSanNha = new List<int>();
            this.ListNhaNhapId = new List<decimal>();
            this.is_delete = true;
            this.is_dat = true;
            this.is_detail = false;
        }
		public Guid GUID {get;set;}
		public Decimal? QUYET_DINH_TICH_THU_ID {get;set;}
		public String TEN {get;set;}
		public Decimal? NGUON_GOC_TAI_SAN_ID {get;set;}
		public Decimal? LOAI_TAI_SAN_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG {get;set;}
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_XAC_LAP {get;set;}
		public String GHI_CHU {get;set;}
        [UIHint("InputAddon")]
        public Decimal? GIA_TRI_TINH { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public string TEN_LOAI_TAI_SAN { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SU_DUNG { get; set; }
        [UIHint("InputBKS")]
        public string BIEN_KIEM_SOAT { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TAI_TRONG { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_CHO_NGOI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_CAU_XE { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        public String DIA_CHI { get; set; }
        public String DON_VI_TINH { get; set; }
        public string DB_ID { get; set; }
        public string DB_QUYET_DINH_TICH_THU_ID { get; set; }

        public IList<int> ListTaiSanNha { get; set; }
        public IList<decimal> ListNhaNhapId { get; set; }
        public bool is_delete { get; set; }
        public bool is_detail { get; set; }
        public bool is_dat { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SoLuongCon { get; set; }
        [UIHint("InputYear")]
        public Decimal? NamSuDung { get; set; }
        public string TenLoaiHinhTaiSan { get; set; }
        public int SoLuongTaiSanDaChon { get; set; }
        public string JsonDDL { get; set; }
        public string TenNguonGoc { get; set; }
        public string TenLoaiTaiSan { get; set; }
        public string NguyenGia { get; set; }
        public string GiaTri { get; set; }
        public string GiaTriTinh { get; set; }
        public String Pre_BIEN_KIEM_SOAT { get; set; }
        public String Suff_BIEN_KIEM_SOAT { get; set; }
        public List<SelectListItem> DDLNguonGocTaiSan { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLDat { get; set; }
        public List<SelectListItem> DDLNHA { get; set; }
        public List<SelectListItem> DDLNhanXe { get; set; }
        public QuyetDinhTichThuModel quyetdinh { get; set; }
        public List<TaiSanTdModel> listModel { get; set; }
    }
    public partial class TaiSanTdSearchModel :BaseSearchModel {
        public TaiSanTdSearchModel()
        {
            this.DDLLoaiTaiSan = new List<SelectListItem>();
            this.DDLNguonGocTaiSan = new List<SelectListItem>();
            this.TrangThaiId = (int)enumTRANGTHAITSTD.TONTAI;
            this.ListNhaNhapId = new List<decimal>();
            is_detail = false;
        }
        public string KeySearch {get;set;}
        public string TenTaiSan { get; set; }
        public int QuyetDinhId { get; set; }
        public int LoaiTaiSanId { get; set; }
        public string SoQuyetDinh { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhDen { get; set; }
        public int NguonGocTaiSan { get; set; }
        public int TaiSanDatId { get; set; }
        public int TrangThaiId { get; set; }
        public bool is_detail { get; set; }
        public IList<decimal> ListNhaNhapId { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLNguonGocTaiSan { get; set; }
    }
   public partial class TaiSanTdListModel : BasePagedListModel<TaiSanTdModel>
    {
       
    }
    public partial class TaiSanTdDongBoModel: BaseGSEntityModel
    {
        public Guid GUID { get; set; }
        public String TEN { get; set; }
        public String TEN_LOAI_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? QUYET_DINH_TICH_THU_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? GIA_TRI_XAC_LAP { get; set; }
        public Decimal? GIA_TRI_TINH { get; set; }
        public String DON_VI_TINH { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String BIEN_KIEM_SOAT { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public Decimal? TAI_TRONG { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? SO_CAU_XE { get; set; }
        public string DB_ID { get; set; }
        public string DB_QUYET_DINH_TICH_THU_ID { get; set; }
    }
}

