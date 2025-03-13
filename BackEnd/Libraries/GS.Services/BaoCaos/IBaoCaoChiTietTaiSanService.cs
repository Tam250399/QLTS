using System;
using System.Collections.Generic;
using System.Text;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BaoCaos.TS_PXNTT;

namespace GS.Services.BaoCaos
{
    public partial interface IBaoCaoChiTietTaiSanService
    {
		IList<TS_BCCT_1A> BaoCaoTSCT_1A( DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, decimal idQueueBaoCao =0);

		IList<TS_BCCT_1B> BaoCaoTSCT_1B(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int donViTien = 1000, int DonViDienTichDat = 1, int DonViDienTichNha = 1, int BacTaiSan = 1, string stringLoaiTaiSan = null,decimal idQueueBaoCao = 0);
        IList<TS_BCCT_01C_DK_TSNN> BaoCaoTSCT_01C_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiTaiSan = 0, int donViTien = 1000, int donViDienTich= 1, int bacTaiSan = 1, string stringLoaiTaiSan = null, int LyDo = 0, string stringLyDo = null, decimal idQueueBaoCao = 0);

        IList<TS_BCCT_01D_DK_TSNN> BaoCaoTSCT_01D_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, int donViDienTich = 1, int loaiLyDoBienDong = 0, string stringLoaiTaiSan = null, string stringLyDo = null, decimal idQueueBaoCao = 0);

        IList<TS_BCCT_01E_DK_TSNN> BaoCaoTSCT_01E_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, int donViDienTich = 1,int loaiLyDoBienDong = 0, string stringLoaiTaiSan = null, string stringLyDo = null, decimal idQueueBaoCao = 0);

        IList<TS_BCCT_01F_DK_TSNN> BaoCaoTSCT_01F_DK_TSNN(Decimal? namBaoCao = 0, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0);
        IList<TS_BCCT_01Gand01H_DK_TSNN> BaoCaoTSCT_01G_AND_01H_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSanId = 0, int donViDienTich = 1, int donViTien = 1000, int bacTaiSan = 1, int lyDoBienDongId = 0, bool isDaThuTien = false, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0);

        IList<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH> BC_CCTT_B03_HUU_HINH_TheoDonViSuDung(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1000);
		IList<TS_BCCT_BCTDXNTS_DKTS> GetTS_BCCT_BCTDXNTS_DKTS(int DonViId, DateTime? NgayBatDau= null, DateTime? NgayKetThuc= null);
        IList<TS_PXNTT_MAU_01> PXNTT_MAU_01(decimal DonViId, DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int DonViTien = 1, int DonViDienTich = 1, string LoaiTaiSan = null);

    }
}
