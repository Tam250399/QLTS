using GS.Core.Domain.BaoCaos.TS_CDKT;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.BaoCaos
{
	public partial class CheDoKeToanService : ICheDoKeToanService
	{
		#region Fields

		private readonly IDbContext _dbContext;
		private readonly IQueueProcessService _queueProcessService;

		#endregion Fields

		#region Ctor

		public CheDoKeToanService(IDbContext dbContext, IQueueProcessService queueProcessService)
		{
			this._dbContext = dbContext;
			this._queueProcessService = queueProcessService;
		}

		#endregion Ctor

		#region Methods

		public virtual IList<BaoCaoKiemKeTaiSan> BaoCaoKiemKeTaiSan(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Decimal DonViId = 0, string MaNhomTaiSan = null, string BoPhanIds = null, decimal? DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pbo_phan_ids", OracleDbType.Varchar2, BoPhanIds, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Decimal, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<BaoCaoKiemKeTaiSan>("PK_TS_CDKT_REPORT.SP_KIEM_KE_TAI_SAN", p1, p2, p3, p4, p5, p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<BaoCaoKiemKeTaiSan> BaoCaoKiemKeTaiSanPhongBan(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null,Decimal DonViId = 0, string MaNhomTaiSan = null, string ListDonViBoPhan = null, decimal? DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Decimal, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLIST_BO_PHAN_ID", OracleDbType.Varchar2, ListDonViBoPhan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Decimal, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<BaoCaoKiemKeTaiSan>("PK_TS_CDKT_REPORT.SP_KIEM_KE_TAI_SAN_PHONG_BAN", p1, p2, p3, p4, p5, p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<B04HQD19> CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD(int Year, Int32 donViId = 0, int loaiTaiSan = 0, int donViTien = 1000, int donViDienTich = 1, int bacTaiSan = 1, string stringLoaiTaiSan = null, int LyDo = 0,  decimal idQueueBaoCao = 0,string ListDonViBoPhan = null)
		{
			OracleParameter p1 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pLIST_BO_PHAN_ID", OracleDbType.Varchar2, ListDonViBoPhan, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("ptu_ngay", OracleDbType.Date, new DateTime(Year, 1, 1), ParameterDirection.Input);    // đầu năm
			OracleParameter p4 = new OracleParameter("pden_ngay", OracleDbType.Date, new DateTime(Year, 12, 31), ParameterDirection.Input); // cuối năm
			OracleParameter p5 = new OracleParameter("ply_do", OracleDbType.Int32, LyDo, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pnhom_tai_san_id", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_CDKT_REPORT.SP_BS03_MS_B04H_BC_THTANGGIAM_TSCD", p1, p2, p3, p4, p5, p6, p7);
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
				var result = _dbContext.EntityFromStore<B04HQD19>("PK_TS_CDKT_REPORT.SP_BS03_MS_B04H_BC_THTANGGIAM_TSCD", p1, p2, p3, p4, p5, p6, p7, p_out).ToList();
				return result;
			}
			
		}

		public virtual IList<C55AHDQD19> CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);


			try
			{
				var result = _dbContext.EntityFromStore<C55AHDQD19>("PK_TS_CDKT_REPORT.SP_BS04_MS_C55A_HD_BANG_TINH_HAO_MON", p1, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<C55AHDQD19> CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<C55AHDQD19>("PK_TS_CDKT_REPORT.SP_BS05_BANG_TINH_KHAU_HAO_TSCD", p1, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoGhiTangTaiSan> CDKT_BS06_SOGHITANG_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, string stringLyDo = null)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoGhiTangTaiSan>("PK_TS_CDKT_REPORT.SP_BS06_SOGHITANG_TSCD", p1, p2, p3, p4, p5,p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoGhiGiamTaiSan> CDKT_BS07_SOGHIGIAM_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, string stringLyDo = null)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoGhiGiamTaiSan>("PK_TS_CDKT_REPORT.SP_BS07_SOGHIGIAM_TSCD", p1, p2, p3, p4, p5,p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoTaiSanS32HQD19> CDKT_B09_S32H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, bool isInTrongKy = false)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pisInTrongKy", OracleDbType.Int32, isInTrongKy ? 1 : 0, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS32HQD19>("PK_TS_CDKT_REPORT.SP_B09_S32H_SO_TS", p1, p2, p3, p4, p5,p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoTaiSanS32HQD19> CDKT_B10_S32H_CCDC(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS32HQD19>("PK_TS_CDKT_REPORT.SP_B10_S32H_SO_TS", p1, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoTaiSanS32HQD19> CDKT_B11_S32H_SO_TS_CDCC(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Int32, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS32HQD19>("PK_TS_CDKT_REPORT.SP_B11_S32H_SO_TS_CDCC", p1, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<SoTaiSanS31HQD19> CDKT_BTC_B08_S24H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, string ListDonViBoPhan = null, int DonViTien = 1000, bool isInTrongKy = false)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLIST_BO_PHAN_ID", OracleDbType.Varchar2, ListDonViBoPhan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pisInTrongKy", OracleDbType.Int32, isInTrongKy ? 1 : 0, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS31HQD19>("PK_TS_CDKT_REPORT.SP_BTC_B08_S24H_SO_TS", p1, p2, p3, p4, p5,p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<SoTaiSanS31HQD19> CDKT_BTC_B08_S31H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, string ListDonViBoPhan = null, int DonViTien = 1000, bool isInTrongKy = false)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLIST_BO_PHAN_ID", OracleDbType.Varchar2, ListDonViBoPhan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pisInTrongKy", OracleDbType.Int32, isInTrongKy ? 1 : 0, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS31HQD19>("PK_TS_CDKT_REPORT.SP_BTC_B08_S31H_SO_TS", p1, p2, p3, p4, p5, p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<SoTaiSanS32HQD19> CDKT_BTC_B11_S24H_SO_TS_CDCC(int Nam, Decimal DonViId, string MaNhomTaiSan, string BoPhanId = null, int DonViTien = 1000, bool isInTrongKy = false)
		{
			OracleParameter p1 = new OracleParameter("pNAM", OracleDbType.Int32, Nam, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pNHOM_TAI_SAN_ID", OracleDbType.Varchar2, MaNhomTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pisInTrongKy", OracleDbType.Int32, isInTrongKy?1:0, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			try
			{
				var result = _dbContext.EntityFromStore<SoTaiSanS32HQD19>("PK_TS_CDKT_REPORT.SP_BTC_B11_S24H_SO_TS_CDCC_union", p1, p2, p3, p4, p5, p6, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<BienBanKiemKe> BienBanKiemke(int KiemKeId = 0, int DonViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pKIEM_KE_ID", OracleDbType.Int32, KiemKeId, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<BienBanKiemKe>("PK_TS_CDKT_REPORT.SP_BIEN_BAN_KIEM_KE", p1, p2, p3).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion Methods
	}
}