//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(XuLyValidator))]
    public class XuLyModel : BaseGSEntityModel
    {
        public XuLyModel() {

            this.DDLTaiSanTD = new List<SelectListItem>();
            this.DDLPhuongAn = new List<SelectListItem>();
            this.DDLHinhThuc = new List<SelectListItem>();
            this.DDLBoNganh = new List<SelectListItem>();
            this.DDLPhuongAnXuLyTS = new List<SelectListItem>();
            this.taisanxuly = new TaiSanTdXuLyModel();
            this.listmodel = new List<XuLyModel>();
            this.searchTaiSanXuLyModel = new TaiSanTdSearchModel();
            this.listTaiSanXuLyModel = new List<TaiSanTdXuLyModel>();
            this.GUID_VIEW = Guid.NewGuid();
            this.GUID = Guid.NewGuid();
            this.Is_Copy = false;
            this.listQuyetDinh = new List<int>();
            this.DDLQuyetDinhTichThu = new List<SelectListItem>();
        }
        public Guid? GUID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? CHI_PHI { get; set; }
        public String GHI_CHU { get; set; }
        //public Decimal? LOAI_XU_LY_ID { get; set; }
        public String NGUOI_QUYET_DINH { get; set; }
        public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String CO_QUAN_BAN_HANH_TEN { get; set; }

        public string DB_ID { get; set; }
        public int TaiSanTDId { get; set; }
        public Guid GUID_VIEW { get; set; }
        public int HinhThucXuLyId {get;set;}
        public int HinhThucId { get; set; }       
        public TaiSanTdXuLyModel taisanxuly { get; set; }
        public string TenDonVi { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string ListVuViec { get; set; }
        public bool Is_Copy { get; set; }
        public string ChiPhi { get; set; }
        public string ListQuyetDinhId { get; set; }
        public DateTime? NgayTichThu { get; set; }
        public List<SelectListItem> DDLBoNganh { get; set; }
        public List<SelectListItem> DDLHinhThuc { get; set; }
        public List<SelectListItem> DDLPhuongAn { get; set; }
        public List<SelectListItem> DDLPhuongAnXuLyTS { get; set; }
        public List<SelectListItem> DDLTaiSanTD { get; set; }
        public List<SelectListItem> DDLQuyetDinhTichThu { get; set; }
        public List<XuLyModel> listmodel { get; set; }
        // remove validate
        public List<int> listQuyetDinh { get; set; }
        public decimal PhuongAnId { get; set; }
        public decimal PhuongAnXuLyTsID { get; set; }
        public TaiSanTdSearchModel searchTaiSanXuLyModel { get; set; }
        public List<TaiSanTdXuLyModel> listTaiSanXuLyModel { get; set; }

    }
    public partial class XuLySearchModel : BaseSearchModel {
        public XuLySearchModel()
        {
            this.DDLLoaiTaiSan = new List<SelectListItem>();
            this.DDLPhuongAnXuLy = new List<SelectListItem>();
            this.DDLHinhThucXuLy = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public int LOAI_XU_LY_ID { get; set; }
        public int LoaiTaiSanId { get; set; }
        public string SoQuyetDinh {get;set;}
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinhDen { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayXuLyTu { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayXuLyDen { get; set; }
        public int PhuongAnXuLy { get; set; }
        public int PhuongThucXuLy { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLPhuongAnXuLy { get; set; }
        public List<SelectListItem> DDLHinhThucXuLy { get; set; }
    }
    public partial class XuLyListModel : BasePagedListModel<XuLyModel>
    {

    }
    public partial class XuLyModelDongBo : BaseGSModel
    {

    }
}

