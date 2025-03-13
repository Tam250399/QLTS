using System;
using DevExpress.Xpo;

namespace GS.Core.Domain.BaoCaos.Widget
{

    public class SoLuongTaiSanTheoLoaiHinh : BaseViewEntity
    {
        public decimal? LOAIHINHTAISAN { get; set; }
        public decimal? SOLUONG { get; set; }
    }

    public class SoLuongTaiSanTheoLoaiHinhVaDonVi : BaseViewEntity
    {
        public decimal? LOAIHINHTAISAN { get; set; }
        public decimal? SOLUONG { get; set; }
        public string TENDONVI { get; set; }
    }

    public class GiaTriTaiSanTheoLoaiHinh : BaseViewEntity
    {
        public decimal? LOAIHINHTAISAN { get; set; }
        public long? GIATRI { get; set; }
    }
	public class GiaTriSoLuongTaiSanTheoLoaiHinh :BaseViewEntity
	{
		public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public decimal? TONG_NGUYEN_GIA { get; set; }
		public decimal? SO_LUONG { get; set; }
	}
}