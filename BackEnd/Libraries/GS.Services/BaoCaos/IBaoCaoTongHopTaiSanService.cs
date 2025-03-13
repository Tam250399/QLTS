using System;
using System.Collections.Generic;
using System.Text;
using GS.Core.Domain.BaoCaos.TS_BCTH;

namespace GS.Services.BaoCaos
{
    public partial interface IBaoCaoTongHopTaiSanService
    {
        IList<TS_BCTH_02A_DK_TSNN> BaoCaoTongHopTS_02A(DateTime? NgayKetThuc = null, String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0,int BacDonVi =0, bool isHideDetail = false, int BacTaiSan = 1);
		IList<TS_BCTH_02B_DK_TSNN> BaoCaoTongHopTS_02B(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int MauSo = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);
        IList<TS_BCTH_02C_DK_TSNN> BaoCaoTongHopTS_02C(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);
        IList<TS_BCTH_02D_DK_TSNN> BaoCaoTongHopTS_02D(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int LoaiLyDoBienDongId = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, string stringLyDo = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);
        IList<TS_BCTH_02E_DK_TSNN> BaoCaoTongHopTS_02E(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int LoaiLyDoBienDongId = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, string stringLyDo = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);
        IList<TS_BCTH_02F_DK_TSNN> BaoCaoTongHopTS_02F(Decimal? namBaoCao = 2020, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);

        IList<TS_BCTH_02G_DK_TNSS> BaoCaoTongHopTS_02G(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1, int DonViDienTich = 1, int BanOrThanhLy = 0, DateTime? NgayBatDau = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);
        IList<TS_BCTH_02H_DK_TSNN> BaoCaoTongHopTS_02H(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1, int DonViDienTich = 1, int BanOrThanhLy = 0, DateTime? NgayBatDau = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0);

        IList<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH> BC_CCTT_B03_HUU_HINH(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, decimal idQueueBaoCao = 0,int BacDonVi = 1 );
		IList<TS_BCTH_TT_CUNGCAP_TAICHINH> BC_CCTT_B03_VO_HINH(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, decimal idQueueBaoCao = 0);

		IList<TS_BCTH_08A_DK_TSC> BaoCaoTongHopTS_08A(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int MauSo = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0, bool IsHideDetail=false);
        IList<TS_BCTH_08B_DK_TSC> BaoCaoTongHopTS_08B(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, int BacDonvi = 0, string stringLyDo = null, decimal idQueueBaoCao = 0,bool IsHideDetail=false);
	}
}
