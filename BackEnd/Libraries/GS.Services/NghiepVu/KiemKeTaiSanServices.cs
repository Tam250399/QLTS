using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.NghiepVu;
using GS.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.NghiepVu
{
	public class KiemKeTaiSanServices : IKiemKeTaiSanServices
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;

		#endregion

		#region ctor
		public KiemKeTaiSanServices(
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
		#endregion ctor
		public Boolean KIEM_KE_TAI_SAN(Decimal DonViId = 0, Decimal? DonViBoPhanId = null, DateTime? NgayKiemKe = null, Decimal KiemKeId = 0)
		{
			OracleParameter p1 = new OracleParameter("p_ID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("p_DON_VI_BO_PHAN_ID", OracleDbType.Int32, DonViBoPhanId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("p_NGAY_KIEM_KE", OracleDbType.Date, NgayKiemKe, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("p_KIEM_KE_ID", OracleDbType.Int32, KiemKeId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var model = _dbContext.EntityFromStore<ResultCallStore>("PK_TS_NGHIEPVU.SP_KIEM_KE_TAI_SAN", p1, p2, p3, p4, p5).FirstOrDefault();
				if (model != null)
				{
					return model.Result;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
