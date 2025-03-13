using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BaoCaos.TS_PXNTT;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
namespace GS.Services.BaoCaos
{
	public class BaoCaoChiTietTaiSanService : IBaoCaoChiTietTaiSanService
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
		public BaoCaoChiTietTaiSanService(
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

		#region Methods
		public virtual IList<TS_BCCT_1B> BaoCaoTSCT_1B(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int donViTien = 1000, int DonViDienTichDat = 1, int DonViDienTichNha = 1, int BacTaiSan = 1, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH_DAT", OracleDbType.Int32, DonViDienTichDat, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH_NHA", OracleDbType.Int32, DonViDienTichNha, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCT_REPORT.SP_BAO_CAO_1B", p1, p2, p3, p4, p5, p6, p7, p8);
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
				var result = _dbContext.EntityFromStore<TS_BCCT_1B>("PK_TS_BCCT_REPORT.SP_BAO_CAO_1B", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
				return result;
			}
		}
		public virtual IList<TS_BCCT_1A> BaoCaoTSCT_1A(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0)
		{


			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCT_REPORT.SP_BAO_CAO_1A", p1, p2, p4, p5, p6, p7);
				var queue = _queueProcessService.GetQueueProcessById(idQueueBaoCao);
				if (queue!= null)
				{
					queue.STATEMENT = statement;
					_queueProcessService.UpdateQueueProcess(queue);
				}
				return null;
			}
			else
			{
				var result = _dbContext.EntityFromStore<TS_BCCT_1A>("PK_TS_BCCT_REPORT.SP_BAO_CAO_1A", p1, p2, p4, p5, p6, p7, p8).ToList();
				return result;
			}

		}
		public virtual IList<TS_BCCT_01C_DK_TSNN> BaoCaoTSCT_01C_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiTaiSan = 0, int donViTien = 1000, int donViDienTich = 1, int bacTaiSan = 1, string stringLoaiTaiSan = null,int LyDo = 0,string stringLyDo = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, ngayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, bacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH_DAT", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCT_REPORT.SP_BAO_CAO_01C_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9);
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
				var result = _dbContext.EntityFromStore<TS_BCCT_01C_DK_TSNN>("PK_TS_BCCT_REPORT.SP_BAO_CAO_01C_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
		}

		public virtual IList<TS_BCCT_01D_DK_TSNN> BaoCaoTSCT_01D_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, int donViDienTich = 1, int loaiLyDoBienDong = 0, string stringLoaiTaiSan = null, string stringLyDo = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, ngayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, bacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCCT_REPORT.SP_BAO_CAO_01D_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9);
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
				var result = _dbContext.EntityFromStore<TS_BCCT_01D_DK_TSNN>("PK_TS_BCCT_REPORT.SP_BAO_CAO_01D_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
		}

		public virtual IList<TS_BCCT_01E_DK_TSNN> BaoCaoTSCT_01E_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, int donViDienTich = 1, int loaiLyDoBienDong = 0, string stringLoaiTaiSan = null, string stringLyDo = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, ngayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, bacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCT_01E_DK_TSNN>("PK_TS_BCCT_REPORT.SP_BAO_CAO_01E_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public virtual IList<TS_BCCT_01F_DK_TSNN> BaoCaoTSCT_01F_DK_TSNN(Decimal? namBaoCao = 0, Int32 donViId = 0, int loaiHinhTaiSan = 0, int bacTaiSan = 1, int donViTien = 1000, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCT_01F_DK_TSNN>("PK_TS_BCCT_REPORT.SP_BAO_CAO_01F_DK_TSNN", p1, p2, p3, p4, p5).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<TS_BCCT_01Gand01H_DK_TSNN> BaoCaoTSCT_01G_AND_01H_DK_TSNN(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null, Int32 donViId = 0, int loaiHinhTaiSanId = 0, int donViDienTich = 1, int donViTien = 1000, int bacTaiSan = 1, int lyDoBienDongId = 0, bool isDaThuTien = false, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, ngayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, ngayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, donViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pLY_DO_BIEN_DONG_ID", OracleDbType.Int32, lyDoBienDongId, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pIS_CAP_NHAP_TIEN", OracleDbType.Int32, (isDaThuTien == true ? 1 : 0), ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCT_01Gand01H_DK_TSNN>("PK_TS_BCCT_REPORT.SP_TS_BCCT_01G_AND_01H_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual IList<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH> BC_CCTT_B03_HUU_HINH_TheoDonViSuDung(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1000)
		{

			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH>("PK_TS_BCCT_REPORT.SP_B03_CCTT_HUU_HINH", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<TS_BCCT_BCTDXNTS_DKTS> GetTS_BCCT_BCTDXNTS_DKTS(int DonViId, DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pDON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCCT_BCTDXNTS_DKTS>("PK_TS_BCCT_REPORT.SP_TS_BCCT_BCTDXNTS_DKTS", p1, p2, p3, p4).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#region Phiếu xác nhận thông tin tài sản
		public virtual IList<TS_PXNTT_MAU_01> PXNTT_MAU_01(decimal DonViId, DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null,int DonViTien =1 , int DonViDienTich = 1, string LoaiTaiSan = null)
		{
			OracleParameter p1 = new OracleParameter("pngay_bat_dau", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pngay_ket_thuc", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pid_don_vi", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("ploai_tai_san_id", OracleDbType.Varchar2, LoaiTaiSan ,ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pdon_vi_tien", OracleDbType.Int32, DonViTien,ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pdon_vi_dien_tich", OracleDbType.Int32, DonViDienTich,ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("tbl_out", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_PXNTT_MAU_01>("pk_ts_phieu_xac_nhan.sp_01_pxntt_dkts", p1, p2, p3, p4,p5,p6,p7).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
		#endregion
	}
}
