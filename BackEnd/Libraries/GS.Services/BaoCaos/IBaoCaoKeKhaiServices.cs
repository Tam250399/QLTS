using GS.Core.Domain.BaoCaos.TS_BCKK;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BaoCaos
{
	public partial interface IBaoCaoKeKhaiServices
	{
		IList<TS_BCKK_01_DK_TSNN> TS_BCKK_01_DK_TSNN(DateTime? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000, Decimal? donViDT = 1);
		IList<TS_BCKK_02_DK_TSNN> TS_BCKK_02_DK_TSNN(DateTime ? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000);
		IList<TS_BCKK_03_DK_TSNN> TS_BCKK_03_DK_TSNN(DateTime? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000);
		IList<TS_BCKK_04A_DK_TSC> TS_BCKK_04A_DK_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
		IList<TS_BCKK_04B_DK_TSC> TS_BCKK_04B_DK_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
		IList<TS_BCKK_04C_DK_TSC> TS_BCKK_04C_DK_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, string LoaiHinhTaiSnId = null);
		IList<TS_BCKK_05A_DK_TSDA> TS_BCKK_05A_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0);
		IList<TS_BCKK_05B_DK_TSDA> TS_BCKK_05B_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0);
		IList<TS_BCKK_05C_DK_TSDA> TS_BCKK_05C_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0);
		IList<TS_BCKK_06A_DK_TSC> TS_BCKK_06A_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, decimal donViId = 0);
		IList<TS_BCKK_06B_DK_TSC> TS_BCKK_06B_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
		IList<TS_BCKK_06C_DK_TSC> TS_BCKK_06C_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
		IList<TS_BCKK_06D_DK_TSC> TS_BCKK_06D_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal LyDoID = 0, string LoaiHinhTaiSnId = null);
		IList<TS_BCKK_07_DKTSC_XOATS_CSDL> TS_BCKK_07_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, decimal donViId = 0, string LoaiHinhTaiSnId= null);
		IList<TS_BCKK_03_DMTSNN_GIAM_KHAC> TS_BCKK_03_DMTSNN(DateTime? ngayBaoCao=null, decimal donViId=0, string LoaiHinhTaiSnId=null, int LoaiLyDoBienDong=0, int donViDienTich=0,int donViTien = 1000, string stringLyDo = null, string stringLoaiTaiSan = null);
		IList<TS_BCKK_01_DMTSNN_GIAM_NHA_DAT> TS_BCKK_01_DMTSNN(DateTime? ngayBaoCao = null, decimal donViId = 0, string LoaiHinhTaiSnId = null, int LoaiLyDoBienDong = 0, int donViDienTich = 0, int donViTien = 1000, string stringLyDo = null);
		IList<TS_BCKK_02_DMTSNN_GIAM_OTO> TS_BCKK_02_DMTSNN(DateTime? ngayBaoCao = null, decimal donViId = 0, string LoaiHinhTaiSnId = null, int LoaiLyDoBienDong = 0, int donViDienTich = 0, int donViTien = 1000, string stringLyDo = null);
	}
}
