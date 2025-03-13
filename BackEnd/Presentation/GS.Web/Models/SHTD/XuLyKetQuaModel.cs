using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.SHTD
{
	[Validator(typeof(XuLyKetQuaValidator))]
	public class XuLyKetQuaModel : BaseGSEntityModel
	{
		public XuLyKetQuaModel() {
			Is_Create = false;
		}
		public Decimal? XU_LY_ID { get; set; }
		public String CHUNG_TU_NOP_TIEN_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? CHUNG_TU_NOP_TIEN_NGAY { get; set; }
		[UIHint("InputAddon")]
		public Decimal? SO_TIEN { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_NOP_TIEN { get; set; }
		public String GHI_CHU { get; set; }
		public DateTime? NGAY_TAO { get; set; }
		public Decimal? NGUOI_TAO_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public String XuLyTen { get; set; }
		public Boolean Is_Create { get; set; }
	}
    public partial class XuLyKetQuaSearchModel : BaseSearchModel
    {
        public XuLyKetQuaSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class XuLyKetQuaListModel : BasePagedListModel<XuLyKetQuaModel>
    {

    }
}
