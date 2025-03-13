//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(KhaiThacTaiSanValidator))]
	public class KhaiThacTaiSanModel :BaseGSEntityModel
	{
        public KhaiThacTaiSanModel(){
        
        }
		public Decimal KHAI_THAC_ID {get;set;}
        public Decimal KHAI_THAC_CHI_TIET_ID { get; set; }
        public Decimal? TAI_SAN_ID {get;set; }
		public string MA_TAI_SAN { get;set; }
        public string TEN_TAI_SAN { get; set; }
        public decimal? LOAI_TAI_SAN { get; set; }
        public decimal? DIEN_TICH { get; set; }
        public Decimal DIEN_TICH_KHAI_THAC {get;set;}
		public String LoaiTaiSanTen { get; set; }
		public String StrVNDienTich { get; set; }
		public String StrVNDienTichKhaiThac { get; set; }
        [UIHint("InputAddon")]
        public decimal? SO_TIEN { get; set; }
        public string StrVNSoTien { get; set; }
        public string GHI_CHU { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TU_NGAY { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DEN_NGAY { get; set; }
        public string StrNgayKhaiThac { get; set; }
        public KhaiThac KhaiThac { get; set; }
        public KhaiThacChiTiet KhaiThacChiTiet { get; set; }
    }
    public partial class KhaiThacTaiSanSearchModel :BaseSearchModel {
        public KhaiThacTaiSanSearchModel()
        {
        }
		public Decimal KHAI_THAC_ID { get; set; }
        public Decimal KHAI_THAC_CHI_TIET_ID { get; set; }
        public Decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
        public string KeySearch {get;set;}
    }
   public partial class KhaiThacTaiSanListModel : BasePagedListModel<KhaiThacTaiSanModel>
    {
       
    }
}

