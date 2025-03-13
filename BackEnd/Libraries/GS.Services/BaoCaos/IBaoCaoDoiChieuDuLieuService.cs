using GS.Core.Domain.BaoCaos.BC_DOI_CHIEU_DATA;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using System;
using System.Collections.Generic;

namespace GS.Services.BaoCaos
{
	public interface IBaoCaoDoiChieuDuLieuService
	{
		IList<TS_BCCT_1A> BaoCaoTSCT_1A(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, int NguonTaiSanId = 1);

		IList<TS_DOICHIEU_DATA_02A_DK_TSNN> BaoCaoDoiChieuData_02A(DateTime? NgayKetThuc = null, String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int NguonTaiSanId = 1, int BacDonVi = 1, bool isHideDetail=false);
		IList<TS_DOICHIEU_DATA_02B_DK_TSNN> BaoCaoDoiChieuData_02B(DateTime? NgayKetThuc = null, String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int BacDonVi = 1, int NguonTaiSanId = 1);
	}
}