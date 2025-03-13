using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.TaiSans
{
    public class TSLogicSoLieuDauKyModel : BaseGSEntityModel
    {
		public TSLogicSoLieuDauKyModel()
		{
		}
		public int TAI_SAN_ID { get; set; }
		public int LOAI_HINH_TAI_SAN_ID { get; set; }
		public int BIEN_DONG_ID { get; set; }
		public int? SO_LUONG { get; set; }
		public decimal? DIEN_TICH { get; set; }
		//public decimal? NGUYEN_GIA_NGAN_SACH { get; set; }
		//public decimal? NGUYEN_GIA_KHAC { get; set; }
		public string TEN_DON_VI { get; set; }
		public string TEN_LOAI_TAI_SAN { get; set; }
		public string TAI_SAN_MA { get; set; }
		public string TAI_SAN_TEN { get; set; }
		public string LOAI_TAI_SAN_ID { get; set; }
		public int? NAM_DUA_VAO_SD { get; set; }
		public int? SO_CHO { get; set; }
		public int? TAI_TRONG { get; set; }
		public string THONG_SO_KY_THUAT { get; set; }
		public int? SO_TANG { get; set; }
		public decimal? GIA_TRI_CON_LAI { get; set; }
		public decimal? NGUYEN_GIA { get; set; } 
		public partial class TSLogicSoLieuDauKyListModel : BasePagedListModel<TSLogicSoLieuDauKyModel>
		{

		}
	}
}
