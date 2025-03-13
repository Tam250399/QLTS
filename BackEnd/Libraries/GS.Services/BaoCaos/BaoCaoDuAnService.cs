using GS.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using GS.Core.Data;
using GS.Data;
using GS.Core.Domain.BaoCaos.TS_BCDA;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Data;

namespace GS.Services.BaoCaos
{
    public class BaoCaoDuAnService:IBaoCaoDuAnService
    {
		#region Fields
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IQueueProcessService _queueProcessService;

		#endregion

		#region Ctor
		public BaoCaoDuAnService(
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IQueueProcessService queueProcessService)
		{

			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._queueProcessService = queueProcessService;
		}
		#endregion
		public virtual IList<TS_BCDA_02A_CTDV_TSDA> BCDA_02A_CTDV_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02A_CTDV_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02A_CTDV_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<TS_BCDA_02A_THC_TSDA> BCDA_02A_THC_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02A_THC_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02A_THC_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02B_TS_TSDA> BCDA_02B_TS_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02B_TS_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02B_TS_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02C_OT_TSDA> BCDA_02C_OT_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02C_OT_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02C_OT_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02D_TSK_TSDA> BCDA_02D_TSK_TSDA(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02D_TSK_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02D_TSK_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02E_KT_TSDA> BCDA_02E_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1,int LyDoBienDong = 0, string stringLyDo = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLY_DO_ID", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02E_KT_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02E_KT_TSDA", p1, p2, p3, p4, p5, p6,p7,p8,p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02F_KT_TSDA> BCDA_02F_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02F_KT_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02F_KT_TSDA", p1, p2, p3, p4, p5, p6, p7,p8).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02G_KT_TSDA> BCDA_02G_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);			
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02G_KT_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02G_KT_TSDA", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02I_KT_TSDA> BCDA_02I_KT_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02I_KT_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02I_KT_TSDA", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02H_TS_TSDA> BCDA_02H_TS_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1,string LyDoGiam = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("PLY_DO_GIAM", OracleDbType.Varchar2, LyDoGiam, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02H_TS_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02H_TS_TSDA", p1, p2, p3, p4, p5, p6, p7, p8,p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02K_TS_TSDA> BCDA_02K_TS_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("PLY_DO_TANG", OracleDbType.Varchar2, LyDoTang, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02K_TS_TSDA>("PK_TS_BCDA_REPORT.SP_BCDA_02K_TS_TSDA", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_01BC_TSDA> BCDA_01BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDU_AN_ID", OracleDbType.Varchar2, DuAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 =  new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_01BC_TSDA>("PK_TS_BCDA_REPORT.SP_TS_BCDA_01BC_TS_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_02BC_TSDA> BCDA_02BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDU_AN_ID", OracleDbType.Varchar2, DuAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_02BC_TSDA>("PK_TS_BCDA_REPORT.SP_TS_BCDA_02BC_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_03BC_TSDA> BCDA_03BC_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDU_AN_ID", OracleDbType.Varchar2, DuAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_03BC_TSDA>("PK_TS_BCDA_REPORT.SP_TS_BCDA_03BC_TSDA", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_04BC_HTSD_KHAC> BCDA_04BC_HTSD_KHAC(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDU_AN_ID", OracleDbType.Varchar2, DuAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_04BC_HTSD_KHAC>("PK_TS_BCDA_REPORT.SP_TS_BCDA_04BC_HTSD_KHAC", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_05BC_TANG_GIAM_TSDA>BCDA_05BC_TANG_GIAM_TSDA(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1000, int DonViDienTich = 1, string stringLoaiTaiSan = null, int MauSo = 1, string LyDoTang = null, int DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDU_AN_ID", OracleDbType.Varchar2, DuAnId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_05BC_TANG_GIAM_TSDA>("PK_TS_BCDA_REPORT.SP_TS_BCDA_05BC_TANG_GIAM_TSDA", p1, p2, p3, p4, p5, p6, p7).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO> GetTS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO>("PK_TS_BCDA_REPORT.sp_bao_cao_01A_DK_TSDA_KEKHAI_TRU_SO", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_01B_DK_TSDA_KEKHAI_OTO> GetTS_BCDA_01B_DK_TSDA_KEKHAI_OTO(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_01B_DK_TSDA_KEKHAI_OTO>("PK_TS_BCDA_REPORT.sp_bao_cao_01B_DK_TSDA_KEKHAI_OTO", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC> GetTS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_01C_DK_TSDA_KEKHAI_TAISANKHAC>("PK_TS_BCDA_REPORT.sp_bao_cao_01C_DK_TSDA_KEKHAI_TAISANKHAC", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_04A_TRUSODENGHIXULY> GetTS_BCDA_04A_TRUSODENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdu_an_id", OracleDbType.Int32, DuAnId, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_04A_TRUSODENGHIXULY>("PK_TS_BCDA_REPORT.sp_bao_cao_04A_TRUSODENGHIXULY", p1, p2, p3, p4, p5, pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_04B_OTODENGHIXULY> GetTS_BCDA_04B_OTODENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdu_an_id", OracleDbType.Int32, DuAnId, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_04B_OTODENGHIXULY>("PK_TS_BCDA_REPORT.sp_bao_cao_04B_OTODENGHIXULY", p1, p2, p3, p4, p5, pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCDA_04C_KHACDENGHIXULY> GetTS_BCDA_04C_KHACDENGHIXULY(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal DuAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdu_an_id", OracleDbType.Int32, DuAnId, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCDA_04C_KHACDENGHIXULY>("PK_TS_BCDA_REPORT.sp_bao_cao_04C_KHACDENGHIXULY", p1, p2, p3, p4, p5,pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
