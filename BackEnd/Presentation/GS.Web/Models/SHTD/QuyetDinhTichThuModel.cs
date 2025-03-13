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
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(QuyetDinhTichThuValidator))]
	public class QuyetDinhTichThuModel :BaseGSEntityModel
	{
        public QuyetDinhTichThuModel(){
            GUID = Guid.NewGuid();
            this.is_vali = true;
            this.is_edit = true;
            this.is_delete = true;
            this.is_detail = false;
            this.is_tichthu = true;
            this.is_tuchoi = false;
            this.DDLNguonGoc = new List<SelectListItem>();
            this.DDLBoNganh = new List<SelectListItem>();
        }
		public Guid GUID {get;set;}
		public String QUYET_DINH_SO {get;set;}
        [UIHint("DateNullable")]
		public DateTime? QUYET_DINH_NGAY {get;set;}
		public String TEN {get;set;}
        [UIHint("InputAddon")]
        public Decimal? TONG_GIA_TRI { get; set; }
        public String GHI_CHU {get;set;}
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? DON_VI_ID { get; set; }       
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
        public String NGUOI_QUYET_DINH { get; set; }
        public String CO_QUAN_BAN_HANH_TEN { get; set; }
        public Decimal? NGUON_GOC_ID { get; set; }      
        public String DB_ID { get; set; }
        public String TenNguonGoc { get; set; }
        public bool is_vali { get; set; }
        public bool is_edit { get; set; }
        public bool is_delete { get; set; }
        public bool is_boduyet { get; set; }
        public bool is_tuchoi { get; set; }
        public bool is_detail { get; set; }
        public bool is_tichthu { get; set; }
        public IList<SelectListItem> DDLNguonGoc { get; set; }
        public IList<SelectListItem> DDLBoNganh { get; set; }
    }
    public partial class QuyetDinhTichThuSearchModel :BaseSearchModel {
        public QuyetDinhTichThuSearchModel()
        {
            is_tichthu = true;
            this.DDLLoaiTaiSan = new List<SelectListItem>();
            this.DDLNguonGocTaiSan = new List<SelectListItem>();
            DDLTrangThai = new List<SelectListItem>();
            DDLLoaiHinhTaiSan = new List<SelectListItem>();
        }
        public string KeySearch {get;set;}
        public int LoaiXuLy { get; set; }
        public int LoaiTaiSanId { get; set; }
        public int LoaiHinhTaiSanId { get; set; }
        public string SoQuyetDinh { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhDen { get; set; }
        public int NguonGocTaiSanId { get; set; }
        public int TrangThaiId { get; set; }
        public enumTRANGTHAI_QUYETDINH_TSTD enumTrangThaiTSTD { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLLoaiHinhTaiSan { get; set; }
        public List<SelectListItem> DDLTrangThai { get; set; }
        public List<SelectListItem> DDLNguonGocTaiSan { get; set; }
        public bool? is_tichthu { get; set; }
    }
   public partial class QuyetDinhTichThuListModel : BasePagedListModel<QuyetDinhTichThuModel>
    {
       
    }
    public partial class QuyetDinhTichThuDongBoModel : BaseGSEntityModel
    {
        public Guid GUID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String TEN { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
        public String NGUOI_QUYET_DINH { get; set; }
        public Decimal? NGUON_GOC_ID { get; set; }
        public String DB_ID { get; set; }
    }
}

