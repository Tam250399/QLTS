using System;
using System.Collections.Generic;
using System.Text;
using GS.Core.Domain.BaoCaos.TSTD;

namespace GS.Services.BaoCaos
{
    public partial interface ITaiSanToanDanService
    {
        IList<BaoCaoThongTinTSTD> BaoCaoThongTinTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1);
        IList<BaoCaoPhuongAnXuLyTSTD> BaoCaoPhuongAnXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1, string NguonGoc = null, Decimal? HinhThucXuLyId = 0);
       IList<BaoCaoKetQuaXuLyTSTD> BaoCaoKetQuaXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1);
        IList<BaoCaoTinhHinhXuLyTSTD> BaoCaoTinhHinhXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1);
        IList<BaoCaoTongHopTSTD> BaoCaoTongHopTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1);
        IList<TSTD_MAU_01_BC_XLSHTD> TSTD_MAU_01_BC(int QuyetDinhId = 0, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null);
        IList<TSTD_MAU_02_BC_XLSHTD> TSTD_MAU_02_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null, int BacDonVi = 1, int MauSo = 1, List<int> CapDonVi = null, int BacNguonGoc = 1, String stringCapHanhChinh = null);
        IList<TSTD_MAU_03_BC_XLSHTD> TSTD_MAU_03_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, int BacNguonGoc = 1, string NguonGoc = null);
        IList<TSTD_MAU_04_BC_XLSHTD> TSTD_MAU_04_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null, int BacNguonGoc = 1, int MauGiamTSTD = 0);
        IList<TSTD_MAU_05_BC_XLSHTD> TSTD_MAU_05_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null, int LyDoGiam = 0);
    }
}
