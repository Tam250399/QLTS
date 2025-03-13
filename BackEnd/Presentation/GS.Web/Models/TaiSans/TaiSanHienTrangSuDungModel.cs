using GS.Web.Framework.Models;

namespace GS.Web.Models.TaiSans
{
	public class TaiSanHienTrangSuDungModel : BaseGSEntityModel
	{
		public decimal TAI_SAN_ID { get; set; }
		public decimal BIEN_DONG_ID { get; set; }
		public decimal? NHOM_HIEN_TRANG_ID { get; set; }
		public decimal KIEU_DU_LIEU_ID { get; set; }
		public string TEN_HIEN_TRANG { get; set; }
		public string GIA_TRI_TEXT { get; set; }
		public decimal? GIA_TRI_NUMBER { get; set; }
		public bool? GIA_TRI_CHECKBOX { get; set; }
		public decimal? HIEN_TRANG_ID { get; set; }
	}

	public partial class TaiSanHienTrangSuDungSearchModel : BaseSearchModel
	{
		public TaiSanHienTrangSuDungSearchModel()
		{
		}

		public string KeySearch { get; set; }
	}

	public partial class TaiSanHienTrangSuDungListModel : BasePagedListModel<TaiSanHienTrangSuDungModel>
	{
	}
}