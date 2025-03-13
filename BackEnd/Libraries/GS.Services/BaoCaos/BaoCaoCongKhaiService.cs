using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;


namespace GS.Services.BaoCaos
{
	public class BaoCaoCongKhaiService : IBaoCaoCongKhaiService
	{

		#region Fields
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IQueueProcessService _queueProcessService;

		#endregion

		#region Ctor
		public BaoCaoCongKhaiService(
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

		public virtual IList<TS_BCCK_09A_CK_TSC> BaoCaoCongKhai_09A_CK_TSC(Decimal donViId, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, String strLoaiLyDoId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLY_DO_TANG_ID", OracleDbType.Varchar2, strLoaiLyDoId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_09A_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_09A_CK_TSC", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<TS_BCCK_09B_CK_TSC> BaoCaoCongKhai_09B_CK_TSC(Decimal donViId, Decimal namBaoCao = 0, String strLoaiTaiSanId = null, Decimal donViTien = 1000, Decimal donViDienTich = 1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_09B_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_09B_CK_TSC", p1, p2, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_09C_CK_TSC> BaoCaoCongKhai_09C_CK_TSC(decimal donViId, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_09C_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_09C_CK_TSC", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_09D_CK_TSC> BaoCaoCongKhai_09D_CK_TSC(decimal donViId, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_09D_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_09D_CK_TSC", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCCK_09DD_CK_TSC> BaoCaoCongKhai_09DD_CK_TSC(decimal donViId, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_09DD_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_09DD_CK_TSC", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_07_CK_TSC> BaoCaoCongKhai_07_CK_TSC(decimal donViId, decimal namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_07_CK_TSC>("PK_TS_BCCK_REPORT.SP_BAO_CAO_07_CK_TSC", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_TDMS> BaoCaoCongKhai_TDMS_TSC(decimal donViId, decimal namBaoCao = 0, string strLoaiTaiSanId = null, int donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_TDMS>("PK_TS_BCCK_REPORT.SP_BAO_CAO_TIEN_DO_MUA_SAM", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_THMS> BaoCaoCongKhai_THMS_TSC(decimal donViId, decimal namBaoCao = 0, string strLoaiTaiSanId = null, int donViTien = 1000, int bacTaiSan = 1 )
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_THMS>("PK_TS_BCCK_REPORT.SP_BAO_CAO_TINH_HINH_MUA_SAM", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_DIEUCHUYEN_BAN_GIAO> BaoCaoCongKhai_DIEU_CHUYEN_BAN_GIAO_TSC(decimal donViId, int donViTien = 1000, decimal namBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_DIEUCHUYEN_BAN_GIAO>("PK_TS_BCCK_REPORT.SP_BAO_CAO_DIEU_CHUYEN_BAN_GIAO_CK_TSC", p1, p2, p3, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCCK_10A_CK_TSC> BaoCaoCongKhai_10A_CK_TSC(string stringDonVi = null, int capDonVi = 0, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi =1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, bacDonVi, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_10A_CK_TSC>("PK_TS_BCCK_REPORT.sp_bao_cao_10a_ck_tsc", p1, p2, p3, p4, p5, p6,p7).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCCK_10B_CK_TSC> BaoCaoCongKhai_10B_CK_TSC(string stringDonVi = null, int capDonVi = 0, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi = 1, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, capDonVi, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, bacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCK_REPORT.sp_bao_cao_10b_ck_tsc", p1, p2, p3, p4, p5, p6, p7, p8);
				var queue = _queueProcessService.GetQueueProcessById(idQueueBaoCao);
				if (queue != null)
				{
					queue.STATEMENT = statement;
					_queueProcessService.UpdateQueueProcess(queue);
				}
				return null;
			}
			else
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_10B_CK_TSC>("PK_TS_BCCK_REPORT.sp_bao_cao_10b_ck_tsc", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
		}

		public IList<TS_BCCK_10C_CK_TSC> BaoCaoCongKhai_10C_CK_TSC(string stringDonVi = null, int capDonVi = 0, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int BacDonVi = 1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_10C_CK_TSC>("PK_TS_BCCK_REPORT.sp_bao_cao_10c_ck_tsc", p1, p2, p3, p4, p5, p6, p7).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCCK_10D_CK_TSC> BaoCaoCongKhai_10D_CK_TSC(string stringDonVi = null, int capDonVi = 0, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int BacDonVi = 1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, capDonVi, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_10D_CK_TSC>("PK_TS_BCCK_REPORT.sp_bao_cao_10d_ck_tsc", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_11A_CK_TSC> BaoCaoCongKhai_11A_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int BacDonVi = 1, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCK_REPORT.SP_BCCK_11A_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8);
				var queue = _queueProcessService.GetQueueProcessById(idQueueBaoCao);
				if (queue != null)
				{
					queue.STATEMENT = statement;
					_queueProcessService.UpdateQueueProcess(queue);
				}
				return null;
			}
			else
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_11A_CK_TSC>("PK_TS_BCCK_REPORT.SP_BCCK_11A_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
		}
		public IList<TS_BCCK_11B_CK_TSC> BaoCaoCongKhai_11B_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1,int BacDonVi = 1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_11B_CK_TSC>("PK_TS_BCCK_REPORT.SP_BCCK_11B_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCCK_11C_CK_TSC> BaoCaoCongKhai_11C_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int BacDonVi = 1, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCK_REPORT.SP_BCCK_11C_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8);
				var queue = _queueProcessService.GetQueueProcessById(idQueueBaoCao);
				if (queue != null)
				{
					queue.STATEMENT = statement;
					_queueProcessService.UpdateQueueProcess(queue);
				}
				return null;
			}
			else
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_11C_CK_TSC>("PK_TS_BCCK_REPORT.SP_BCCK_11C_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
		}
		public IList<TS_BCCK_11D_CK_TSC> BaoCaoCongKhai_11D_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int BacDonVi = 1)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, strLoaiTaiSanId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCK_11D_CK_TSC>("PK_TS_BCCK_REPORT.SP_BCCK_11D_CK_TSC", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
