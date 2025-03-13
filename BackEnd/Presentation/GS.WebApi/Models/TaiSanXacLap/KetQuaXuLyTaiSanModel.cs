using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSanXacLap
{
	[Validator(typeof(KetQuaXuLyTaiSanValidator))]
	public class KetQuaXuLyTaiSanModel:BaseGSApiModel
    {
        public KetQuaXuLyTaiSanModel()
        {
            
        }
		//[Required(ErrorMessage = "TAI_SAN_TD_XU_LY_ID null")]
		public Decimal? TAI_SAN_TD_XU_LY_ID { get; set; }
		//[Required(ErrorMessage = "HINH_THUC_XU_LY_ID null")]
		public Decimal? HINH_THUC_XU_LY_ID { get; set; }
		public Decimal? DON_VI_CHUYEN_ID { get; set; }
		public Decimal? GIA_TRI_NSNN { get; set; }
		public Decimal? GIA_TRI_TKTG { get; set; }
		public Decimal? CHI_PHI_XU_LY { get; set; }
		public String HOP_DONG_SO { get; set; }
		public DateTime? HOP_DONG_NGAY { get; set; }
		public Guid? GUID { get; set; }
		public Decimal? GIA_TRI_BAN { get; set; }
		public String HO_SO_GIAY_TO_KHAC { get; set; }
		public DateTime? NGAY_XU_LY { get; set; }
		public Decimal? SO_LUONG { get; set; }
		public String DB_ID { get; set; }
		public String DB_TAI_SAN_TD_XU_LY_ID { get; set; }
		public Decimal? TYPE_ID { get; set; }
	}
}
