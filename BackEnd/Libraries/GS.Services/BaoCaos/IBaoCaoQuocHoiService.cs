using GS.Core.Domain.BaoCaos.TS_BCQH;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BaoCaos
{
	public partial interface IBaoCaoQuocHoiService
	{
		IList<TS_BCQH_MAU01_THTSNN> BCQH_MAU01_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU02_CCTSNN> BCQH_MAU02_CCTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU03_QUYDAT_MDSD> BCQH_MAU03_QUYDAT_MDSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU04_TS_LTS> BCQH_MAU04_TS_LTS(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU05_TS_CQ_TC_DV> BCQH_MAU05_TS_CQ_TC_DV(String stringDonVi = null, DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU06_TS_CAPQL> BCQH_MAU06_TS_CAPQL(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU07_OTO_SD> BCQH_MAU07_OTO_SD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU08_OTO_VSD> BCQH_MAU08_OTO_VSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU09_DAT_SD> BCQH_MAU09_DAT_SD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU10_TS_TREN500_MDSD> BCQH_MAU10_TS_TREN500_MDSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU11A_THTSNN> BCQH_MAU11A_TS_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_MAU11B_THTSNN> BCQH_MAU11B_TS_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, bool is_Tinh = false, bool is_Huyen = false, bool is_Xa = false, decimal idQueueBaoCao = 0);
		#region services phụ lục
		IList<TS_BCQH_PL02_TANG_GIAM_TSNN> TS_BCQH_PL02_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);
		IList<TS_BCQH_PL03_TANG_GIAM_TSNN> TS_BCQH_PL03_TANG_GIAM_QUY_DAT(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);
		IList<TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV> TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);
		IList<TS_BCQH_PL06_TANG_GIAM_TSNN> TS_BCQH_PL06_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);
		IList<TS_BCQH_PL07_TANG_GIAM_TSNN> TS_BCQH_PL07_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);
		IList<TS_BCQH_PL08_OTO_VSD> TS_BCQH_PL08_OTO_VSD(Decimal donViId, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000);
		IList<TS_BCQH_PL09_TANG_GIAM_TS_TREN500> BCQH_PL09_TANG_GIAM_TS_TREN500(DateTime? ngayBaoCao = null, Decimal? donViId = 0, Decimal? loaiTaiSanId = 0, int donViTien = 1000000000);

		IList<TS_BCQH_PL10_TANG_GIAM_TSNN> TS_BCQH_PL10_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null, decimal idQueueBaoCao = 0);

		IList<TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV> TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null);

		IList<TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC> BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000000000, int DonViDienTich = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null);

		IList<TS_BCQH_TH_QUAN_LY_SD_NHA_TSC> BCQH_TH_QUAN_LY_SD_NHA_TSC(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000000000, int DonViDienTich = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null);
		#endregion services phụ lục

		IList<TS_BCQH_QH14_OTO_CHUCDANH> BCQH_QH14_OTO_CHUCDANH(String stringDonVi = null, DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal idQueueBaoCao = 0);
		IList<TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC> BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal donViId = 0);
	}
}
