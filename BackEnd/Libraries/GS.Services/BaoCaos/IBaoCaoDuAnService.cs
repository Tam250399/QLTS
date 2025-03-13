using GS.Core.Domain.BaoCaos.TS_BCDA;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BaoCaos
{
    public partial interface IBaoCaoDuAnService
    {
        IList<TS_BCDA_02A_CTDV_TSDA> BCDA_02A_CTDV_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null);
        IList<TS_BCDA_02A_THC_TSDA> BCDA_02A_THC_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null);
        IList<TS_BCDA_02B_TS_TSDA> BCDA_02B_TS_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null);
        IList<TS_BCDA_02C_OT_TSDA> BCDA_02C_OT_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null);
        IList<TS_BCDA_02D_TSK_TSDA> BCDA_02D_TSK_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null);
        IList<TS_BCDA_02E_KT_TSDA> BCDA_02E_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, int LyDoBienDong = 0, string stringLyDo = null);
        IList<TS_BCDA_02F_KT_TSDA> BCDA_02F_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1);
        IList<TS_BCDA_02G_KT_TSDA> BCDA_02G_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0);
        IList<TS_BCDA_02I_KT_TSDA> BCDA_02I_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1);
        IList<TS_BCDA_02H_TS_TSDA> BCDA_02H_TS_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoGiam = null);
        IList<TS_BCDA_02K_TS_TSDA> BCDA_02K_TS_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null);
       
		IList<TS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO> GetTS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
		IList<TS_BCDA_01B_DK_TSDA_KEKHAI_OTO> GetTS_BCDA_01B_DK_TSDA_KEKHAI_OTO(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
        IList<TS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC> GetTS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0);
        IList<TS_BCDA_04A_TRUSODENGHIXULY> GetTS_BCDA_04A_TRUSODENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0);
		IList<TS_BCDA_04B_OTODENGHIXULY> GetTS_BCDA_04B_OTODENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0);
		IList<TS_BCDA_04C_KHACDENGHIXULY> GetTS_BCDA_04C_KHACDENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0);
	
        IList<TS_BCDA_01BC_TSDA> BCDA_01BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0);
        IList<TS_BCDA_02BC_TSDA> BCDA_02BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0);
        IList<TS_BCDA_03BC_TSDA> BCDA_03BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0);
        IList<TS_BCDA_04BC_HTSD_KHAC> BCDA_04BC_HTSD_KHAC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0);
        IList<TS_BCDA_05BC_TANG_GIAM_TSDA> BCDA_05BC_TANG_GIAM_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0);
    }
}
