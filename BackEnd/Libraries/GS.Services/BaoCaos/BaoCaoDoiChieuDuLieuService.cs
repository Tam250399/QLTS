using GS.Core.Domain.BaoCaos.BC_DOI_CHIEU_DATA;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.BaoCaos
{
	public class BaoCaoDoiChieuDuLieuService : IBaoCaoDoiChieuDuLieuService
	{
		private readonly IDbContext _dbContext;

		public BaoCaoDoiChieuDuLieuService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IList<TS_DOICHIEU_DATA_02A_DK_TSNN> BaoCaoDoiChieuData_02A(DateTime? NgayKetThuc = null, string stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int NguonTaiSanId = 1, int BacDonVi = 1, bool IsHideDetail = false)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pnguon_tai_san", OracleDbType.Int32, NguonTaiSanId, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            if (IsHideDetail)
            {
				return _dbContext.EntityFromStore<TS_DOICHIEU_DATA_02A_DK_TSNN>("PKG_TS_BC_DOICHIEU_CHUYENDOI_DATA_REPORT.sp_bao_cao_02a_dk_tsnn_dpac", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
			}
            else
            {
				return _dbContext.EntityFromStore<TS_DOICHIEU_DATA_02A_DK_TSNN>("PKG_TS_BC_DOICHIEU_CHUYENDOI_DATA_REPORT.sp_bao_cao_02a_dk_tsnn", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
			}
			
		}

		public IList<TS_DOICHIEU_DATA_02B_DK_TSNN> BaoCaoDoiChieuData_02B(DateTime? NgayKetThuc = null, string stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int BacDonVi = 1, int NguonTaiSanId = 1)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);		
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pnguon_tai_san", OracleDbType.Int32, NguonTaiSanId, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			var result = _dbContext.EntityFromStore<TS_DOICHIEU_DATA_02B_DK_TSNN>("PKG_TS_BC_DOICHIEU_CHUYENDOI_DATA_REPORT.sp_bao_cao_02b_dk_tsnn", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
			return result;
		}
		public IList<TS_BCCT_1A> BaoCaoTSCT_1A(DateTime? NgayKetThuc = null, int DonViId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, int NguonTaiSanId = 1)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pnguon_tai_san", OracleDbType.Int32, NguonTaiSanId, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			var result = _dbContext.EntityFromStore<TS_BCCT_1A>("PKG_TS_BC_DOICHIEU_CHUYENDOI_DATA_REPORT.SP_BAO_CAO_1A", p1, p2, p4, p5, p6, p7, p8, p9).ToList();
			return result;
		}
	}
}