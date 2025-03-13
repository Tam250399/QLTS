using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TSTD;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.BaoCaos
{
    public class TaiSanToanDanService :ITaiSanToanDanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        
        #endregion

        #region Ctor
        public TaiSanToanDanService(
            CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager         
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            
        }
        #endregion

        #region Methods
        public virtual IList<TSTD_MAU_01_BC_XLSHTD> TSTD_MAU_01_BC(int QuyetDinhId = 0, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null)
        {
            OracleParameter p1 = new OracleParameter("p_QUYET_DINH_ID", OracleDbType.Int32, QuyetDinhId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2, NguonGoc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, DonViKhoiLuong, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<TSTD_MAU_01_BC_XLSHTD>("PK_TSTD_REPORT.SP_BAOCAO_TSTD_KEKHAI", p1, p2, p3, p4, p5, p6, p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<TSTD_MAU_02_BC_XLSHTD> TSTD_MAU_02_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null, int BacDonVi = 1, int MauSo = 1, List<int> CapDonVi = null, int BacNguonGoc = 1, String stringCapHanhChinh = null)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAO_CAO_TU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_BAO_CAO_DEN", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2,  NguonGoc, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_DON_VI_ID", OracleDbType.Varchar2, DonViId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, DonViKhoiLuong, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
            OracleParameter p10 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, stringCapHanhChinh, ParameterDirection.Input);
            OracleParameter p11 = new OracleParameter("pBAC_NGUON_GOC", OracleDbType.Int32, BacNguonGoc, ParameterDirection.Input);
            OracleParameter p12 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<TSTD_MAU_02_BC_XLSHTD>("PK_TSTD_REPORT.SP_BAOCAO_TSTD_TANGGIAM", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<TSTD_MAU_03_BC_XLSHTD> TSTD_MAU_03_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, int BacNguonGoc = 1, string NguonGoc = null)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2, NguonGoc, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, DonViKhoiLuong, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("p_BAC_NGUON_GOC", OracleDbType.Int32, BacNguonGoc, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<TSTD_MAU_03_BC_XLSHTD>("PK_TSTD_REPORT.SP_BCXLSHTD_M03", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual IList<TSTD_MAU_04_BC_XLSHTD> TSTD_MAU_04_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 0, string NguonGoc = null, int MauGiamTSTD = 0, int BacNguonGoc = 1)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2, NguonGoc, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, DonViKhoiLuong, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("pIS_GROUPBY_HTXL", OracleDbType.Int32, MauGiamTSTD, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("p_BAC_NGUON_GOC", OracleDbType.Int32, BacNguonGoc, ParameterDirection.Input);
            OracleParameter p10 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<TSTD_MAU_04_BC_XLSHTD>("PK_TSTD_REPORT.SP_BCXLSHTD_M04_06", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual IList<TSTD_MAU_05_BC_XLSHTD> TSTD_MAU_05_BC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1000, int DonViDienTich = 1, int DonViKhoiLuong = 1, string NguonGoc = null, int LyDoGiam = 0)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2, NguonGoc, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, DonViKhoiLuong, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("P_LY_DO_ID", OracleDbType.Int32, LyDoGiam, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<TSTD_MAU_05_BC_XLSHTD>("PK_TSTD_REPORT.sp_b05_bc_xlshtd", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual IList<BaoCaoThongTinTSTD> BaoCaoThongTinTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0,int DonViTien = 1000, int DonViDienTich =1)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_LOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiTaiSan, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoThongTinTSTD>("PK_TSTD_REPORT.SP_TSTD_THONGTIN_TAISAN_TD_BAOCAO", p1, p2, p3, p4,p5,p6,p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoPhuongAnXuLyTSTD> BaoCaoPhuongAnXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1, string NguonGoc = null, Decimal? HinhThucXuLyId = 0)
        {
            OracleParameter p1 = new OracleParameter("p_QUYET_DINH_ID", OracleDbType.Int32, 0, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGUON_GOC_ID", OracleDbType.Varchar2, NguonGoc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_DON_VI_KHOI_LUONG", OracleDbType.Int32, 1, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, 1, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("p_HINH_THUC_XU_LY", OracleDbType.Int32, HinhThucXuLyId , ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoPhuongAnXuLyTSTD>("PK_TSTD_REPORT.SP_BAOCAO_TSTD_PAXL", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoKetQuaXuLyTSTD> BaoCaoKetQuaXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1)
        {
            OracleParameter p1 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_BAO_CAO_TU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_NGAY_BAO_CAO_DEN", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoKetQuaXuLyTSTD>("PK_TSTD_REPORT.SP_BAOCAO_TSTD_KQXL", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoTinhHinhXuLyTSTD> BaoCaoTinhHinhXuLyTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiXuLyId = 0, int DonViTien = 1000, int DonViDienTich = 1)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_LOAI_XU_LY_ID", OracleDbType.Int32, LoaiXuLyId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoTinhHinhXuLyTSTD>("PK_TSTD_REPORT.SP_TSTD_QUANLY_TAISAN_TD_BAOCAO", p1, p2, p3, p4, p5, p6, p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoTongHopTSTD> BaoCaoTongHopTSTDs(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0,int LoaiXuLyId=0, int DonViTien = 1000, int DonViDienTich = 1)
        {
            OracleParameter p1 = new OracleParameter("p_NGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_NGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("p_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("p_LOAI_XU_LY_ID", OracleDbType.Int32, LoaiXuLyId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_DON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("p_DON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoTongHopTSTD>("PK_TSTD_REPORT.SP_TSTD_TONGHOP_TAISAN_TD_BAOCAO", p1, p2, p3, p4,p5, p6, p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion
    }
}
