using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.BaoCaos
{
    public interface IBaoCaoTraCuuService
    {
        IList<TS_BCTC_03_DK_TSNN> BaoCaoTraCuuTS_BCTC_03_DK_TSNN(DateTime? NgayKetThuc = null, Int32 DonViId = 0, decimal? NhanXeId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0, decimal? NamSD_CompareSign = 0, decimal? NamSD_Value1 = null, decimal? NamSD_Value2 = null, decimal? NamSx_CompareSign = 0, decimal? NamSx_Value1 = null, decimal? NamSx_Value2 = null, decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
            , decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
            , decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
            , decimal? NguyenGiaNS_CompareSign = 0, decimal? NguyenGiaNS_Value1 = null, decimal? NguyenGiaNS_Value2 = null
            , decimal? NguyenGiaKhac_CompareSign = 0, decimal? NguyenGiaKhac_Value1 = null, decimal? NguyenGiaKhac_Value2 = null
            , decimal? NguyenGiaODA_CompareSign = 0, decimal? NguyenGiaODA_Value1 = null, decimal? NguyenGiaODA_Value2 = null
            , decimal? NguyenGiaVienTro_CompareSign = 0, decimal? NguyenGiaVienTro_Value1 = null, decimal? NguyenGiaVienTro_Value2 = null
            , decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
            , decimal? TyLeChatLuong_CompareSign = 0, decimal? TyLeChatLuong_Value1 = null, decimal? TyLeChatLuong_Value2 = null
            , decimal? GTCL_CompareSign = 0, decimal? GTCL_Value1 = null, decimal? GTCL_Value2 = null, string stringLoaiDonVi = null
            , decimal? ChucDanhId = 0
            , decimal? SoCau_CompareSign = 0
            , decimal? SoCau_Value1 = null
            , decimal? SoCau_Value2 = null);

        IList<TS_BCTC_04_DK_TSNN> BaoCaoTraCuuTS_BCTC_04_DK_TSNN(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1, int DonViDienTich = 1, List<int> CapDonVi = null, String stringLoaiTaiSan = null, string stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi= 0, String stringCapHanhChinh = null);
        //IQueryable<TS_LOGIC_SO_LIEU_DAU_KY> KTLOGIC_SO_LIEU_DAU_KY(string store, params object[] parameters);
        /*IPagedList<TS_LOGIC_SO_LIEU_DAU_KY> GetTaiSanKTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
        , string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
        *//*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*//*, List<int> ListLoaiTaiSanId = null
        , decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
        , decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
        , decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
        , decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
        , decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
        , decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
        , string stringLoaiDonVi = null, int pageIndex = 0, int pageSize = 0);*/

        IList<TS_LOGIC_SO_LIEU_DAU_KY> KTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
            , string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
            /*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*/, List<int> ListLoaiTaiSanId = null
            , decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
            , decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
            , decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
            , decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
            , decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
            , decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
            , string stringLoaiDonVi = null, decimal? ChucDanhId = 0);
        IPagedList<TS_LOGIC_SO_LIEU_DAU_KY> PAGE_KTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
            , string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
            /*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*/, List<int> ListLoaiTaiSanId = null
            , decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
            , decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
            , decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
            , decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
            , decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
            , decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
            , string stringLoaiDonVi = null, decimal? ChucDanhId = 0
            , int pageIndex = 0, int pageSize = 0);
    }
}