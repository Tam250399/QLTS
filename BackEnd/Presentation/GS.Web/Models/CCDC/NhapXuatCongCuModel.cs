//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(NhapXuatCongCuValidator))]
	public class NhapXuatCongCuModel :BaseGSEntityModel
	{
        public NhapXuatCongCuModel(){
        
        }
		public Decimal NHAP_XUAT_ID {get;set;}
		public Decimal CONG_CU_ID {get;set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG {get;set; }
        public Decimal? DON_GIA { get; set; }
        public Decimal? THANH_TIEN { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal? SoLuongCoThePhanBo { get; set; }
        public Decimal? NHAP_KHO_ID { get; set; }
        // more
        public String DonGiaText { get; set; }
        public String NhomCongCuText { get; set; }
        public String TenCongCuText { get; set; }
        public String MaCongCuText { get; set; }
        public String MaLoCongCuText { get; set; }
        public String TenTrangThai { get; set; }
    }
    public partial class NhapXuatCongCuSearchModel :BaseSearchModel {
        public NhapXuatCongCuSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class NhapXuatCongCuListModel : BasePagedListModel<NhapXuatCongCuModel>
    {
       
    }
}

