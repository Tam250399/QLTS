namespace GS.Core.Domain.BaoCaos.CCDC
{
	public class TongHopTon : BaseViewEntity
	{
		public string MA { get; set; }
		public string TEN { get; set; }
		public string DON_VI_TINH_ID { get; set; }
		public decimal? so_luong_tang_ky_truoc { get; set; }
		public decimal? thanh_tien_tang_ky_truoc { get; set; }
		public decimal? so_luong_tang { get; set; }
		public decimal? thanh_tien_tang { get; set; }
		public decimal? so_phan_bo { get; set; }
		public decimal? thanh_tien_phan_bo { get; set; }
		public decimal? so_luong_giam { get; set; }
		public decimal? thanh_tien_giam { get; set; }
	}
}