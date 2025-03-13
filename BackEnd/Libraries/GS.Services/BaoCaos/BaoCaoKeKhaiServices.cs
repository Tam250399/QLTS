using GS.Core.Data;
using GS.Core.Caching;
using GS.Core.Domain.BaoCaos.TS_BCKK;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GS.Services.BaoCaos
{
	public class BaoCaoKeKhaiServices : IBaoCaoKeKhaiServices
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;

		#endregion
		#region Ctor
		public BaoCaoKeKhaiServices(
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
		public IList<TS_BCKK_01_DK_TSNN> TS_BCKK_01_DK_TSNN(DateTime? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000, Decimal? donViDT = 1)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDT, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_01_DK_TSNN>("PK_TS_BCKK_REPORT.SP_BAO_CAO_01_DK_TSNN", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_01_DMTSNN_GIAM_NHA_DAT> TS_BCKK_01_DMTSNN(DateTime? ngayBaoCao = null, decimal donViId = 0, string LoaiHinhTaiSnId = null, int LoaiLyDoBienDong = 0, int donViDienTich = 0, int donViTien = 1000, string stringLyDo = null)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_ly_do_bien_dong", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_01_DMTSNN_GIAM_NHA_DAT>("PK_TS_BCKK_REPORT.sp_bao_cao_01_dm_tsnn_giam_dat_nha", p1, p2, p3, p4, p5, pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_02_DK_TSNN> TS_BCKK_02_DK_TSNN(DateTime? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_02_DK_TSNN>("PK_TS_BCKK_REPORT.SP_BAO_CAO_02_DK_TSNN", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_02_DMTSNN_GIAM_OTO> TS_BCKK_02_DMTSNN(DateTime? ngayBaoCao = null, decimal donViId = 0, string LoaiHinhTaiSnId = null, int LoaiLyDoBienDong = 0, int donViDienTich = 0, int donViTien = 1000, string stringLyDo = null)
		{

			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_ly_do_bien_dong", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_02_DMTSNN_GIAM_OTO>("PK_TS_BCKK_REPORT.sp_bao_cao_02_dm_tsnn_giam_oto", p1, p2, p3, p4, p5, pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			};
		}

		public IList<TS_BCKK_03_DK_TSNN> TS_BCKK_03_DK_TSNN(DateTime? ngayKetThuc = null, Int32 donViId = 0, Decimal? donViTien = 1000)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_03_DK_TSNN>("PK_TS_BCKK_REPORT.SP_BAO_CAO_03_DK_TSNN", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_03_DMTSNN_GIAM_KHAC> TS_BCKK_03_DMTSNN(DateTime? ngayBaoCao = null, decimal donViId = 0, string LoaiHinhTaiSnId = null, int LoaiLyDoBienDong = 0, int donViDienTich = 0, int donViTien = 1000, string stringLyDo = null, string stringLoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_ly_do_bien_dong", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_03_DMTSNN_GIAM_KHAC>("PK_TS_BCKK_REPORT.sp_bao_cao_03_dm_tsnn_giam_khac", p1, p2, p3, p4, p5, pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_04A_DK_TSC> TS_BCKK_04A_DK_TSC(DateTime? ngayBaoCao = null, int donViTien=1000,int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_04A_DK_TSC>("PK_TS_BCKK_REPORT.sp_bao_cao_04a_dk_tsc", p1, p2, p3, p4,p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_04B_DK_TSC> TS_BCKK_04B_DK_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_04B_DK_TSC>("PK_TS_BCKK_REPORT.sp_bao_cao_04b_dk_tsc", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_04C_DK_TSC> TS_BCKK_04C_DK_TSC(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, string LoaiHinhTaiSnId = null)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_hinh_ts_id", OracleDbType.Varchar2, LoaiHinhTaiSnId, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_04C_DK_TSC>("PK_TS_BCKK_REPORT.sp_bao_cao_04c_dk_tsc", p1, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_05A_DK_TSDA> TS_BCKK_05A_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdu_an_id", OracleDbType.Int32, duAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_05A_DK_TSDA>("PK_TS_BCKK_REPORT.sp_bao_cao_05a_dk_tsda", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_05B_DK_TSDA> TS_BCKK_05B_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdu_an_id", OracleDbType.Int32, duAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_05B_DK_TSDA>("PK_TS_BCKK_REPORT.sp_bao_cao_05b_dk_tsda", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_05C_DK_TSDA> TS_BCKK_05C_DK_TSDA(DateTime? ngayBaoCao = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0, decimal? duAnId = 0)
		{
			OracleParameter p1 = new OracleParameter("pngay_bao_cao", OracleDbType.Date, ngayBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pdu_an_id", OracleDbType.Int32, duAnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_05C_DK_TSDA>("PK_TS_BCKK_REPORT.sp_bao_cao_05c_dk_tsda", p1, p2, p3, p4, p5, p6).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_06A_DK_TSC> TS_BCKK_06A_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, decimal donViId = 0)
		{
			OracleParameter pngayTu = new OracleParameter("pngay_bao_cao_tu", OracleDbType.Date, ngayTu, ParameterDirection.Input);
			OracleParameter pngayDen = new OracleParameter("pngay_bao_cao_den", OracleDbType.Date, ngayDen, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_06A_DK_TSC>("PK_TS_BCKK_REPORT.SP_TS_BCKK_06A_DK_TSC", pngayTu, pngayDen, p2, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_06B_DK_TSC> TS_BCKK_06B_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter pngayTu = new OracleParameter("pngay_bao_cao_tu", OracleDbType.Date, ngayTu, ParameterDirection.Input);
			OracleParameter pngayDen = new OracleParameter("pngay_bao_cao_den", OracleDbType.Date, ngayDen, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_06B_DK_TSC>("PK_TS_BCKK_REPORT.SP_TS_BCKK_06B_DK_TSC", pngayTu, pngayDen, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_06C_DK_TSC> TS_BCKK_06C_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0)
		{
			OracleParameter pngayTu = new OracleParameter("pngay_bao_cao_tu", OracleDbType.Date, ngayTu, ParameterDirection.Input);
			OracleParameter pngayDen = new OracleParameter("pngay_bao_cao_den", OracleDbType.Date, ngayDen, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_06C_DK_TSC>("PK_TS_BCKK_REPORT.SP_TS_BCKK_06C_DK_TSC", pngayTu, pngayDen, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_06D_DK_TSC> TS_BCKK_06D_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, int donViTien = 1000, int donViDienTich = 1, decimal donViId = 0,decimal LyDoID = 0, string LoaiHinhTaiSnId = null)
		{
			OracleParameter pngayTu = new OracleParameter("pngay_bao_cao_tu", OracleDbType.Date, ngayTu, ParameterDirection.Input);
			OracleParameter pngayDen = new OracleParameter("pngay_bao_cao_den", OracleDbType.Date, ngayDen, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_hinh_ts_id", OracleDbType.Varchar2, LoaiHinhTaiSnId, ParameterDirection.Input);
			OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_06D_DK_TSC>("PK_TS_BCKK_REPORT.SP_TS_BCKK_06D_DK_TSC", pngayTu, pngayDen, p2, p3, p4, p5, p_out).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IList<TS_BCKK_07_DKTSC_XOATS_CSDL> TS_BCKK_07_DK_TSC(DateTime? ngayTu = null, DateTime? ngayDen = null, decimal donViId = 0, string LoaiHinhTaiSnId = null)
		{
			OracleParameter pngayTu = new OracleParameter("pngay_bao_cao_tu", OracleDbType.Date, ngayTu, ParameterDirection.Input);
			OracleParameter pngayDen = new OracleParameter("pngay_bao_cao_den", OracleDbType.Date, ngayDen, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pid_don_vi", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("ploai_hinh_ts_id", OracleDbType.Varchar2, LoaiHinhTaiSnId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCKK_07_DKTSC_XOATS_CSDL>("PK_TS_BCKK_REPORT.sp_bao_cao_07_dk_tsc", pngayTu,pngayDen, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
