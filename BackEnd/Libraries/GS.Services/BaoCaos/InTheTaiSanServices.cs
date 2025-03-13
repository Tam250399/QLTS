using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TheTaiSan;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.BaoCaos
{
	public class InTheTaiSanServices : IInTheTaiSanServices
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IQueueProcessService _queueProcessService;
        private readonly IWorkContext _workContext;

        #endregion

        #region ctor
        public InTheTaiSanServices(
			CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IQueueProcessService queueProcessService,
			IWorkContext workContext
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._queueProcessService = queueProcessService;
            _workContext = workContext;
        }
		#endregion ctor
		public IList<TS_THE_TAI_SAN_CO_DINH> TS_THE_TAI_SAN_CO_DINH(DateTime? ngayBaoCao = null, decimal? taiSanId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, taiSanId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_THE_TAI_SAN_CO_DINH>("PK_TS_THE_TAI_SAN_REPORT.SP_THE_TAI_SAN_CO_DINH", p1, p2, p3).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_THE_TAI_SAN_CO_DINH> TS_LIST_THE_TAI_SAN_CO_DINH(DateTime? ngayBaoCao = null, string listTaiSanId=null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Varchar2, listTaiSanId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_ID", OracleDbType.Int32, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_THE_TAI_SAN_CO_DINH>("PK_TS_THE_TAI_SAN_REPORT.SP_LIST_THE_TAI_SAN_CO_DINH", p1, p2, p3,p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<THE_TSCD_TT133> TS_THE_TAI_SAN_CO_DINH_TT133(DateTime? ngayBaoCao = null, decimal? taiSanId = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, taiSanId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<THE_TSCD_TT133>("PK_TS_THE_TAI_SAN_REPORT.SP_THE_TAI_SAN_CO_DINH_TT133", p1, p2, p3).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<THE_TSCD_TT133> TS_LIST_THE_TAI_SAN_CO_DINH_TT133(DateTime? ngayBaoCao = null, string listTaiSanId = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Varchar2, listTaiSanId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_ID", OracleDbType.Int32, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<THE_TSCD_TT133>("PK_TS_THE_TAI_SAN_REPORT.SP_LIST_THE_TAI_SAN_CO_DINH_TT133", p1, p2, p3,p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_THE_KIEM_KE_TAI_SAN> TS_THE_KIEM_KE(decimal? taiSanId = 0)
		{
			OracleParameter p1 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, taiSanId, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_THE_KIEM_KE_TAI_SAN>("PK_TS_THE_TAI_SAN_REPORT.SP_THE_KIEM_KE_TAI_SAN", p1, p2).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IList<TS_THE_KIEM_KE_TAI_SAN> TS_LIST_THE_KIEM_KE(string listTaiSanId = null)
		{
			OracleParameter p1 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Varchar2, listTaiSanId, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDON_VI_ID", OracleDbType.Int32, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<TS_THE_KIEM_KE_TAI_SAN>("PK_TS_THE_TAI_SAN_REPORT.SP_LIST_THE_KIEM_KE_TAI_SAN", p1, p2,p3).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
