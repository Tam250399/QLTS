using GS.Core.Caching;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.CauHinh;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Text;
using GS.Core.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace GS.Services.BaoCaos
{
	public class BaoCaoQuocHoiService : IBaoCaoQuocHoiService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IQueueProcessService _queueProcessService;

		#endregion
		#region Ctor
		public BaoCaoQuocHoiService(
			CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IQueueProcessService queueProcessService
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._queueProcessService = queueProcessService;
		}
		#endregion
		public IList<TS_BCQH_MAU01_THTSNN> BCQH_MAU01_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU01_THTSNN", p1, p2, p3, p4);
				var queue = _queueProcessService.GetQueueProcessById(idQueueBaoCao);
				if (queue != null)
				{
					queue.STATEMENT = statement;
					_queueProcessService.UpdateQueueProcess(queue);
				}
				return null;
			}
			else
				return _dbContext.EntityFromStore<TS_BCQH_MAU01_THTSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU01_THTSNN", p1, p2, p3, p4, p5).ToList();
		}
		public IList<TS_BCQH_MAU02_CCTSNN> BCQH_MAU02_CCTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("SP_BAO_CAO_TONG_HOP_THEO_KHOA.SP_TS_BCQH_MAU02_CCTSNN", p1, p2, p3, p4, p5);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU02_CCTSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU02_CCTSNN", p1, p2, p3, p4, p5).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU04_TS_LTS> BCQH_MAU04_TS_LTS(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU04_TS_LTS", p1, p2, p3, p4, p5);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU04_TS_LTS>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU04_TS_LTS", p1, p2, p3, p4, p5).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU03_QUYDAT_MDSD> BCQH_MAU03_QUYDAT_MDSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU03_QUYDAT_MDSD", p1, p2, p3, p4, p5);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU03_QUYDAT_MDSD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU03_QUYDAT_MDSD", p1, p2, p3, p4, p5).ToList();
				return result;
			}
		}

		public IList<TS_BCQH_MAU05_TS_CQ_TC_DV> BCQH_MAU05_TS_CQ_TC_DV(String stringDonVi = null, DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU05_TS_CQ_TC_DV", p1, p2, p3, p4);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU05_TS_CQ_TC_DV>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU05_TS_CQ_TC_DV", p1, p2, p3, p4).ToList();
				return result;
			}

		}
		public IList<TS_BCQH_MAU06_TS_CAPQL> BCQH_MAU06_TS_CAPQL(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU06_TS_CAPQL", p1, p2, p3, p4, p5);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU06_TS_CAPQL>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU06_TS_CAPQL", p1, p2, p3, p4, p5).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU07_OTO_SD> BCQH_MAU07_OTO_SD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU07_OTO_SD", p1, p2, p3, p4);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU07_OTO_SD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU07_OTO_SD", p1, p2, p3, p4).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU08_OTO_VSD> BCQH_MAU08_OTO_VSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU08_OTO_VSD", p1, p2, p3);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU08_OTO_VSD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU08_OTO_VSD", p1, p2, p3).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU09_DAT_SD> BCQH_MAU09_DAT_SD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU09_DAT_SD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU09_DAT_SD", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCQH_MAU10_TS_TREN500_MDSD> BCQH_MAU10_TS_TREN500_MDSD(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU10_TS_TREN500_MDSD", p1, p2, p3, p4);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU10_TS_TREN500_MDSD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU10_TS_TREN500_MDSD", p1, p2, p3, p4).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU11A_THTSNN> BCQH_MAU11A_TS_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU11A_THTSNN", p1, p2, p3, p4, pOut);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU11A_THTSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU11A_THTSNN", p1, p2, p3, p4, pOut).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_MAU11B_THTSNN> BCQH_MAU11B_TS_THTSNN(String stringDonVi = null, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000, bool is_Tinh = false, bool is_Huyen = false, bool is_Xa = false, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donviDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pIs_tinh", OracleDbType.Int32, is_Tinh ? 1 : 0, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pis_huyen", OracleDbType.Int32, is_Huyen ? 1 : 0, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pis_xa", OracleDbType.Int32, is_Xa ? 1 : 0, ParameterDirection.Input);
			OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU11B_THTSNN", p1, p2, p3, p4, p5, p6, p7, pOut);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_MAU11B_THTSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU11B_THTSNN", p1, p2, p3, p4, p5, p6, p7, pOut).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_QH14_OTO_CHUCDANH> BCQH_QH14_OTO_CHUCDANH(String stringDonVi = null, DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_QH14_OTO_CHUCDANH", p1, p2, p3, p4);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_QH14_OTO_CHUCDANH>("PK_TS_BCQH_REPORT.SP_TS_BCQH_QH14_OTO_CHUCDANH", p1, p2, p3, p4).ToList();
				return result;
			}
		}
		public IList<TS_BCQH_PL02_TANG_GIAM_TSNN> TS_BCQH_PL02_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL02_TANG_GIAM_TSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL02_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#region Services phụ lục bcqh
		public IList<TS_BCQH_PL03_TANG_GIAM_TSNN> TS_BCQH_PL03_TANG_GIAM_QUY_DAT(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL03_TANG_GIAM_TSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL03_TANG_GIAM_QUY_DAT", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV> TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL05_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCQH_PL06_TANG_GIAM_TSNN> TS_BCQH_PL06_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL06_TANG_GIAM_TSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL06_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCQH_PL07_TANG_GIAM_TSNN> TS_BCQH_PL07_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL07_TANG_GIAM_TSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL07_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCQH_PL08_OTO_VSD> TS_BCQH_PL08_OTO_VSD(decimal donViId, DateTime? ngayBaoCao = null, int donviDienTich = 1, int donViTien = 1000000000)
		{

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL08_OTO_VSD>("PK_TS_BCQH_REPORT.SP_TS_BCQH_MAU08_OTO_VSD", p1, p2, p3).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCQH_PL09_TANG_GIAM_TS_TREN500> BCQH_PL09_TANG_GIAM_TS_TREN500(DateTime? ngayBaoCao = null, Decimal? donViId = 0, Decimal? loaiTaiSanId = 0, int donViTien = 1000000000)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, loaiTaiSanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL09_TANG_GIAM_TS_TREN500>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL09_TANG_GIAM_TS_TREN500", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_BCQH_PL10_TANG_GIAM_TSNN> TS_BCQH_PL10_TANG_GIAM_TSNN(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null, decimal idQueueBaoCao = 0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pSTR_ID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL10_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
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
				var result = _dbContext.EntityFromStore<TS_BCQH_PL10_TANG_GIAM_TSNN>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL10_TANG_GIAM_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
				return result;
			}
		}

		public IList<TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV> TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 3, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null, int LyDo = 0, string stringLyDo = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV>("PK_TS_BCQH_REPORT.SP_TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC> BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC>("PK_TS_BCQH_REPORT.SP_TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC", p1, p2, p3, p5, p6, p7, p8, p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCQH_TH_QUAN_LY_SD_NHA_TSC> BCQH_TH_QUAN_LY_SD_NHA_TSC(DateTime? NgayKetThuc = null, int DonViId = 0, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int CapDonVi = 0, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, string stringLoaiDonVi = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pCAP_DON_VI", OracleDbType.Int32, CapDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_TH_QUAN_LY_SD_NHA_TSC>("PK_TS_BCQH_REPORT.SP_TS_BCQH_TH_QUAN_LY_SD_NHA_TSC", p1, p2, p3, p5, p6, p7, p8, p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC> BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000000000, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC>("PK_TS_BCQH_REPORT.SP_TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
		#endregion Services phụ lục bcqh
	}
}
