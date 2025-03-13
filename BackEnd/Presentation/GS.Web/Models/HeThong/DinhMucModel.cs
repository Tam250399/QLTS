//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.HeThong;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(DinhMucValidator))]
	public class DinhMucModel :BaseGSEntityModel
	{
        public DinhMucModel(){
            ChiTietDinhMuc = new List<DinhMucChiTietModel>();
        }
		public String MA {get;set;}
		public String QUYET_DINH_SO {get;set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY {get;set; }
        [UIHint("DateNullable")]
        public DateTime? TU_NGAY {get;set; }
        [UIHint("DateNullable")]
        public DateTime? DEN_NGAY {get;set;}
		public String THONG_TU {get;set;}
		public Decimal? DON_VI_ID {get;set;}
        //more
        public bool IS_HIEU_LUC { get; set; }
        public IList<DinhMucChiTietModel> ChiTietDinhMuc { get; set; }

    }
    public partial class DinhMucSearchModel :BaseSearchModel {
        public DinhMucSearchModel()
        {
        }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }

        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }

        public string SoQuyetDinh { get; set; }

        [UIHint("DateNullable")]
        public DateTime? NgayQuyetDinh { get; set; }
    }

   public partial class DinhMucListModel : BasePagedListModel<DinhMucModel>
    {
       
    }
    public class KiemTraDinhMucModel
    {
        public decimal? ChucDanhId { get; set; }
        public decimal? DonViId { get; set; }
        public decimal? LoaiTaiSanId { get; set; }
        public DateTime? NgayGhiTang { get; set; }
        public decimal? NguyenGia { get; set; }
    }

}

