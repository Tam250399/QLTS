using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.BaoCaos
{
	public class BaoCaoTongHopTaiSanService : IBaoCaoTongHopTaiSanService
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
		public BaoCaoTongHopTaiSanService(
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
		public virtual IList<TS_BCTH_02A_DK_TSNN> BaoCaoTongHopTS_02A(DateTime? NgayKetThuc = null, String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null, decimal idQueueBaoCao = 0, int BacDonVi = 0, bool IsHideDetail = false, int BacTaiSan = 1)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pBAC_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02A_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p11);
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
				if (IsHideDetail)
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_02A_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02A_DK_TSNN_DPAC_V2", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11).ToList();
					return result;
				}
				else
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_02A_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02A_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p11).ToList();
					return result;
				}
				
			}
		}
		public virtual IList<TS_BCTH_02B_DK_TSNN> BaoCaoTongHopTS_02B(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int MauSo = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null, decimal idQueueBaoCao = 0, int BacDonVi = 0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02B_DK_TSNN", p1, p2, p3, p5, p6, p7, p8, p9, p10,p11);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02B_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02B_DK_TSNN", p1, p2, p3, p5, p6, p7, p8, p9, p10,p11).ToList();
				return result;
			}
		}
		public virtual IList<TS_BCTH_02C_DK_TSNN> BaoCaoTongHopTS_02C(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null, int LyDo = 0, string stringLyDo = null, decimal idQueueBaoCao = 0, int BacDonVi = 0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("pBAC_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p13 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02C_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p13);
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
				string[] arrListStr = stringDonVi.Split(',');
				bool checkDVTHTQ = arrListStr.Any(c => c == "99284");
				if (checkDVTHTQ)
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_02C_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02C_DK_TSNN_DPAC", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13).ToList();
					return result;
				}
                else
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_02C_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02C_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p13).ToList();
					return result;
				}
			}
		}
		public virtual IList<TS_BCTH_02D_DK_TSNN> BaoCaoTongHopTS_02D(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int LoaiLyDoBienDongId = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null,string stringLyDo = null, decimal idQueueBaoCao = 0,int BacDonVi =0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02D_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02D_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02D_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12).ToList();
				 return result;
			}
		}
		public virtual IList<TS_BCTH_02E_DK_TSNN> BaoCaoTongHopTS_02E(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, int LoaiLyDoBienDongId = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null, string stringLyDo = null, decimal idQueueBaoCao = 0, int BacDonVi = 0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pLOAI_LY_DO_BIEN_DONG", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02E_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02E_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02E_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12).ToList();
				return result;
			}
		}

		public virtual IList<TS_BCTH_02F_DK_TSNN> BaoCaoTongHopTS_02F(Decimal? namBaoCao = 2020, int MauSo = 1, String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02F_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8,p9);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02F_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02F_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8,p9).ToList();
				return result;
			}
		}


		public virtual IList<TS_BCTH_02G_DK_TNSS> BaoCaoTongHopTS_02G(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1, int DonViDienTich = 1, int BanOrThanhLy = 0, DateTime? NgayBatDau = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi=null, decimal idQueueBaoCao = 0, int BacDonVi =0)
        {
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("pLY_DO_BIEN_DONG_ID", OracleDbType.Int32, BanOrThanhLy, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("pIS_CAP_NHAP_TIEN", OracleDbType.Int32, 1, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p13 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02G_AND_02H_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12,p13);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02G_DK_TNSS>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02G_AND_02H_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12,p13).ToList();
				return result;
			}
		}
        public virtual IList<TS_BCTH_02H_DK_TSNN> BaoCaoTongHopTS_02H(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int MauSo = 1, int DonViTien = 1, int DonViDienTich = 1, int BanOrThanhLy = 0, DateTime? NgayBatDau = null, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi= null ,decimal idQueueBaoCao = 0, int BacDonVi = 0)
        {
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAT_DAU", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_KET_THUC", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("pLY_DO_BIEN_DONG_ID", OracleDbType.Int32, BanOrThanhLy, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("pIS_CAP_NHAP_TIEN", OracleDbType.Int32, 0, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p13 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_02G_AND_02H_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12,p13);
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
				var result = _dbContext.EntityFromStore<TS_BCTH_02H_DK_TSNN>("PK_TS_BCTH_REPORT.SP_BAO_CAO_02G_AND_02H_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12,p13).ToList();
				return result;
			}
		}

		public virtual IList<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH> BC_CCTT_B03_HUU_HINH(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, decimal idQueueBaoCao = 0, int BacDonVi = 0)
		{
			DateTime startDate = new DateTime(Convert.ToInt32(namBaoCao), 01, 01);
			DateTime endDate = new DateTime(Convert.ToInt32(namBaoCao), 12, 31);
			OracleParameter p1 = new OracleParameter("PTU_NGAY", OracleDbType.Date, startDate, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("PDEN_NGAY", OracleDbType.Date, endDate, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_TAI_CHINH_HUU_HINH", p1, p2, p3, p4, p5, p6,p7);
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
				return _dbContext.EntityFromStore<TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH>("PK_TS_BCTH_REPORT.SP_BAO_CAO_TAI_CHINH_HUU_HINH", p1, p2, p3, p4, p5, p6,p7, pout).ToList();
			}
		}
		public virtual IList<TS_BCTH_TT_CUNGCAP_TAICHINH> BC_CCTT_B03_VO_HINH(decimal? namBaoCao = 2020, decimal? donViId = 0, int donViTien = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, decimal idQueueBaoCao = 0)
		{
			DateTime startDate = new DateTime(Convert.ToInt32(namBaoCao), 01, 01);
			DateTime endDate = new DateTime(Convert.ToInt32(namBaoCao), 12, 31);
			OracleParameter p1 = new OracleParameter("PTU_NGAY", OracleDbType.Date, startDate, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("PDEN_NGAY", OracleDbType.Date, endDate, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pNAM_BAO_CAO", OracleDbType.Int32, namBaoCao, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, donViId, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter pout = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			try
			{
				var result = _dbContext.EntityFromStore<TS_BCTH_TT_CUNGCAP_TAICHINH>("PK_TS_BCTH_REPORT.SP_BAO_CAO_TAI_CHINH_VO_HINH", p1, p2, p3, p4, p5,p6,pout).ToList();
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public virtual IList<TS_BCTH_08A_DK_TSC> BaoCaoTongHopTS_08A(DateTime? NgayKetThuc = null,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int MauSo = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi = 0, bool IsHideDetail =false)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_08A_DK_TSC", p1, p2, p3, p5, p6, p7, p8, p9, p10,p11);
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

				if (IsHideDetail)
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_08A_DK_TSC>("PK_TS_BCTH_REPORT.SP_BAO_CAO_08A_DK_TSC_DPAC", p1, p2, p3, p5, p6, p7, p8, p9, p10, p11).ToList();
					return result;
				}
				else
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_08A_DK_TSC>("PK_TS_BCTH_REPORT.SP_BAO_CAO_08A_DK_TSC", p1, p2, p3, p5, p6, p7, p8, p9, p10, p11).ToList();
					return result;
				}
			}
		}
		public virtual IList<TS_BCTH_08B_DK_TSC> BaoCaoTongHopTS_08B(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, int MauSo = 1,  String stringDonVi = null, int LoaiTaiSan = 0, int DonViTien = 1, int DonViDienTich = 1, int BacTaiSan = 1, List<int> CapDonVi = null, String StringCapHanhChinh = null, List<int> ListLoaiTaiSanId = null, String stringLoaiTaiSan = null, String stringLoaiDonVi = null, int LyDo = 0, int BacDonVi = 0, string stringLyDo = null, decimal idQueueBaoCao = 0, bool IsHideDetail = false)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pTU_NGAY", OracleDbType.Date, NgayBatDau, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pDEN_NGAY", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, stringDonVi, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, StringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pLOAI_DON_VI", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("PLY_DO", OracleDbType.Varchar2, stringLyDo, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("PBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTH_REPORT.SP_BAO_CAO_08B_DK_TSC", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
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

				if (IsHideDetail)
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_08B_DK_TSC>("PK_TS_BCTH_REPORT.SP_BAO_CAO_08B_DK_TSC_DPAC", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12).ToList();
					return result;
				}
				else
				{
					var result = _dbContext.EntityFromStore<TS_BCTH_08B_DK_TSC>("PK_TS_BCTH_REPORT.SP_BAO_CAO_08B_DK_TSC", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12).ToList();
					return result;
				}
				
			}
		}
		#endregion

	}
}
