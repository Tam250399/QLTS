//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(DonViChuyenDoiValidator))]
	public class DonViChuyenDoiModel :BaseGSEntityModel
	{
        public DonViChuyenDoiModel(){
        
        }
		public Decimal DON_VI_ID {get;set; }
        public String TENCU { get; set; }
        public String TEN {get;set;}
        public String TEN_LOAI_DON_VI_CU { get; set; } 
        public Decimal LOAI_DON_VI_ID {get;set;}
		public String QUYET_DINH_SO {get;set;}
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY {get;set;}
		public String QUYET_DINH_GIAO_VON_SO {get;set;}
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_GIAO_VON_NGAY {get;set;}
		public DateTime NGAY_TAO {get;set;}
		public DateTime? NgayTaoDonVi {get;set;}
		public Decimal NGUOI_TAO_ID {get;set;}
		public String GHI_CHU {get;set;}
        public IList<SelectListItem> LoaiDonVi { get; set; }
        public string MA { get; set; }
        public string DIA_CHI { get; set; }
        public string TenDonViCha { get; set; }
        public string TenDonViChaMoi { get; set; } 
        public bool DA_CO_QUYET_DINH_GIAO_VON { get; set; }
        public string LOAI_DON_VI { get; set; }
        public string NgayQuyetDinhGiaoVon { get; set; }
        public string NgayQuyetDinhDuDieuKien { get; set; }
        public String MA_DVQHNS { get; set; }
        public String MA_DVQHNS_MOI { get; set; }
        public String TEN_LOAI_CAP_DON_VI_CU { get; set; }
        public String TEN_CAP_DON_VI_CU { get; set; }
        public SelectList ddlLoaiCapDonViMoi { get; set; }
        public Decimal? LOAI_CAP_DON_VI_ID_MOI { get; set; }
        public SelectList dllCapDonViMoi { get; set; }
        public Decimal? CAP_DON_VI_ID_MOI { get; set; }
        //public Boolean LA_DON_VI_NHAP_LIEU { get; set; }

    }
    public partial class DonViChuyenDoiSearchModel :BaseSearchModel {
        public DonViChuyenDoiSearchModel()
        {
        }
        public DonViChuyenDoiSearchModel(int id)
        {
            DonViId = id;
        }
        public int DonViId { get; set; }
        public string KeySearch {get;set;}
        public string MA { get; set; }
        public string TEN { get; set; }
        public string LOAI_DON_VI { get; set; }
        public string DIA_CHI { get; set; }
        public string DON_VI_CAP_TREN { get; set; }
    }
   public partial class DonViChuyenDoiListModel : BasePagedListModel<DonViChuyenDoiModel>
    {
        public string MA { get; set; }
        public string TEN { get; set; }
        public string LOAI_DON_VI { get; set; }
        public string DIA_CHI { get; set; }
        public string DON_VI_CAP_TREN { get; set; }
    }
}

