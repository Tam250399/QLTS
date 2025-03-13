using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.BaoCaos
{
	public class BaoCaoTraCuuService : IBaoCaoTraCuuService
	{
		private readonly IDbContext _dbContext;
		private readonly IQueueProcessService _queueProcessService;

		public BaoCaoTraCuuService(IDbContext dbContext,
			IQueueProcessService queueProcessService)
		{
			this._dbContext = dbContext;
			this._queueProcessService = queueProcessService;
		}

		public virtual IList<TS_BCTC_03_DK_TSNN> BaoCaoTraCuuTS_BCTC_03_DK_TSNN(DateTime? NgayKetThuc = null, Int32 DonViId = 0, decimal? NhanXeId = 0, int LoaiTaiSan = 0, int BacTaiSan = 1, int donViTien = 1000, int DonViDienTich = 1, List<int> ListLoaiTaiSanId = null, string stringLoaiTaiSan = null, decimal idQueueBaoCao = 0, decimal? NamSD_CompareSign = 0, decimal? NamSD_Value1 = null, decimal? NamSD_Value2 = null, decimal? NamSx_CompareSign = 0, decimal? NamSx_Value1 = null, decimal? NamSx_Value2 = null, decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
			, decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
			, decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
			, decimal? NguyenGiaNS_CompareSign = 0, decimal? NguyenGiaNS_Value1 = null, decimal? NguyenGiaNS_Value2 = null
			, decimal? NguyenGiaKhac_CompareSign = 0, decimal? NguyenGiaKhac_Value1 = null, decimal? NguyenGiaKhac_Value2 = null
			, decimal? NguyenGiaODA_CompareSign = 0, decimal? NguyenGiaODA_Value1 = null, decimal? NguyenGiaODA_Value2 = null
			, decimal? NguyenGiaVienTro_CompareSign = 0, decimal? NguyenGiaVienTro_Value1 = null, decimal? NguyenGiaVienTro_Value2 = null
			, decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
			, decimal? TyLeChatLuong_CompareSign = 0, decimal? TyLeChatLuong_Value1 = null, decimal? TyLeChatLuong_Value2 = null
			, decimal? GTCL_CompareSign = 0, decimal? GTCL_Value1 = null, decimal? GTCL_Value2 = null, string stringLoaiDonVi = null
            , decimal? ChucDanhId = 0, decimal? SoCau_CompareSign = 0, decimal? SoCau_Value1 = null, decimal? SoCau_Value2 = null)
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pstr_loai_tai_san_id", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pBAC_CHI_TIET_TAI_SAN", OracleDbType.Int32, BacTaiSan, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("PID_NHAN_XE", OracleDbType.Decimal, NhanXeId, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pNamSD_CompareSign", OracleDbType.Decimal, NamSD_CompareSign, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pNamSD_Value1", OracleDbType.Decimal, NamSD_Value1, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pNamSD_Value2", OracleDbType.Decimal, NamSD_Value2, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("pNamSx_CompareSign", OracleDbType.Decimal, NamSx_CompareSign, ParameterDirection.Input);
			OracleParameter p13 = new OracleParameter("pNamSx_Value1", OracleDbType.Decimal, NamSx_Value1, ParameterDirection.Input);
			OracleParameter p14 = new OracleParameter("pNamSx_Value2", OracleDbType.Decimal, NamSx_Value2, ParameterDirection.Input);
			OracleParameter p15 = new OracleParameter("pSoTang_CompareSign", OracleDbType.Decimal, SoTang_CompareSign, ParameterDirection.Input);
			OracleParameter p16 = new OracleParameter("pSoTang_Value1", OracleDbType.Decimal, SoTang_Value1, ParameterDirection.Input);
			OracleParameter p17 = new OracleParameter("pSoTang_Value2", OracleDbType.Decimal, SoTang_Value2, ParameterDirection.Input);
			OracleParameter p18 = new OracleParameter("pDienTich_CompareSign", OracleDbType.Decimal, DienTich_CompareSign, ParameterDirection.Input);
			OracleParameter p19 = new OracleParameter("pDienTich_Value1", OracleDbType.Decimal, DienTich_Value1, ParameterDirection.Input);
			OracleParameter p20 = new OracleParameter("pDienTich_Value2", OracleDbType.Decimal, DienTich_Value2, ParameterDirection.Input);

			OracleParameter p21 = new OracleParameter("psochongoi_comparesign", OracleDbType.Decimal, SoChoNgoi_CompareSign, ParameterDirection.Input);
			OracleParameter p22 = new OracleParameter("psochongoi_value1", OracleDbType.Decimal, SoChoNgoi_Value1, ParameterDirection.Input);
			OracleParameter p23 = new OracleParameter("psochongoi_value2", OracleDbType.Decimal, SoChoNgoi_Value2, ParameterDirection.Input);

			OracleParameter p24 = new OracleParameter("ptaitrong_comparesign", OracleDbType.Decimal, TaiTrong_CompareSign, ParameterDirection.Input);
			OracleParameter p25 = new OracleParameter("ptaitrong_value1", OracleDbType.Decimal, TaiTrong_Value1, ParameterDirection.Input);
			OracleParameter p26 = new OracleParameter("ptaitrong_value2", OracleDbType.Decimal, TaiTrong_Value2, ParameterDirection.Input);

			OracleParameter p27 = new OracleParameter("pnguyengians_comparesign", OracleDbType.Decimal, NguyenGiaNS_CompareSign, ParameterDirection.Input);
			OracleParameter p28 = new OracleParameter("pnguyengians_value1", OracleDbType.Decimal, NguyenGiaNS_Value1, ParameterDirection.Input);
			OracleParameter p29 = new OracleParameter("pnguyengians_value2", OracleDbType.Decimal, NguyenGiaNS_Value2, ParameterDirection.Input);

			OracleParameter p30 = new OracleParameter("pnguyengiakhac_comparesign", OracleDbType.Decimal, NguyenGiaKhac_CompareSign, ParameterDirection.Input);
			OracleParameter p31 = new OracleParameter("pnguyengiakhac_value1", OracleDbType.Decimal, NguyenGiaKhac_Value1, ParameterDirection.Input);
			OracleParameter p32 = new OracleParameter("pnguyengiakhac_value2", OracleDbType.Decimal, NguyenGiaKhac_Value2, ParameterDirection.Input);

			OracleParameter p33 = new OracleParameter("pnguyengiaoda_comparesign", OracleDbType.Decimal, NguyenGiaODA_CompareSign, ParameterDirection.Input);
			OracleParameter p34 = new OracleParameter("pnguyengiaoda_value1", OracleDbType.Decimal, NguyenGiaODA_Value1, ParameterDirection.Input);
			OracleParameter p35 = new OracleParameter("pnguyengiaoda_value2", OracleDbType.Decimal, NguyenGiaODA_Value2, ParameterDirection.Input);

			OracleParameter p36 = new OracleParameter("pnguyengiavientro_comparesign", OracleDbType.Decimal, NguyenGiaVienTro_CompareSign, ParameterDirection.Input);
			OracleParameter p37 = new OracleParameter("pnguyengiavientro_value1", OracleDbType.Decimal, NguyenGiaVienTro_Value1, ParameterDirection.Input);
			OracleParameter p38 = new OracleParameter("pnguyengiavientro_value2", OracleDbType.Decimal, NguyenGiaVienTro_Value2, ParameterDirection.Input);
						    
			OracleParameter p39 = new OracleParameter("ptongnguyengia_comparesign", OracleDbType.Decimal, TongNguyenGia_CompareSign, ParameterDirection.Input);
			OracleParameter p40 = new OracleParameter("ptongnguyengia_value1", OracleDbType.Decimal, TongNguyenGia_Value1, ParameterDirection.Input);
			OracleParameter p41 = new OracleParameter("ptongnguyengia_value2", OracleDbType.Decimal, TongNguyenGia_Value2, ParameterDirection.Input);

			OracleParameter p42 = new OracleParameter("pgtcl_comparesign", OracleDbType.Decimal, GTCL_CompareSign, ParameterDirection.Input);
			OracleParameter p43 = new OracleParameter("pgtcl_value1", OracleDbType.Decimal, GTCL_Value1, ParameterDirection.Input);
			OracleParameter p44 = new OracleParameter("pgtcl_value2", OracleDbType.Decimal, GTCL_Value2, ParameterDirection.Input);

			OracleParameter p45 = new OracleParameter("pTyLeChatLuong_comparesign", OracleDbType.Decimal, TyLeChatLuong_CompareSign, ParameterDirection.Input);
			OracleParameter p46 = new OracleParameter("pTyLeChatLuong_value1", OracleDbType.Decimal, TyLeChatLuong_Value1, ParameterDirection.Input);
			OracleParameter p47 = new OracleParameter("pTyLeChatLuong_value2", OracleDbType.Decimal, TyLeChatLuong_Value2, ParameterDirection.Input);

           
            OracleParameter p48 = new OracleParameter("pLOAI_DON_VI_ID", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);


            OracleParameter p49 = new OracleParameter("PID_CHUC_DANH", OracleDbType.Decimal, ChucDanhId, ParameterDirection.Input);
            OracleParameter p50 = new OracleParameter("pSoCau_comparesign", OracleDbType.Decimal, SoCau_CompareSign, ParameterDirection.Input);
            OracleParameter p51 = new OracleParameter("pSoCau_value1", OracleDbType.Decimal, SoCau_Value1, ParameterDirection.Input);
            OracleParameter p52 = new OracleParameter("pSoCau_value2", OracleDbType.Decimal, SoCau_Value2, ParameterDirection.Input);


            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTC_REPORT.SP_BAO_CAO_03_DK_TSNN", p1, p2, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48,p49, p50, p51,p52, pOut);

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
				var result = _dbContext.EntityFromStore<TS_BCTC_03_DK_TSNN>("PK_TS_BCTC_REPORT.SP_BAO_CAO_03_DK_TSNN_V2", p1, p2, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, pOut).ToList();
				return result;
			}
		}

		public virtual IList<TS_BCTC_04_DK_TSNN> BaoCaoTraCuuTS_BCTC_04_DK_TSNN(DateTime? NgayKetThuc = null, Int32 DonViId = 0, int DonViTien = 1, int DonViDienTich = 1, List<int> CapDonVi = null, String stringLoaiTaiSan = null, string stringLoaiDonVi = null, decimal idQueueBaoCao = 0, int BacDonVi= 0, String stringCapHanhChinh = null)
		{
			//var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p3 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pCAP_DON_VI", OracleDbType.Varchar2, stringCapHanhChinh, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pLOAI_DON_VI_ID", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);
			OracleParameter p8 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
			if (idQueueBaoCao > 0)
			{
				//không truyền vào TBL_OUT
				//
				string statement = _queueProcessService.GenerateStatment("PK_TS_BCTC_REPORT.SP_BAO_CAO_04_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9);
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
				var result = _dbContext.EntityFromStore<TS_BCTC_04_DK_TSNN>("PK_TS_BCTC_REPORT.SP_BAO_CAO_04_DK_TSNN", p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
				return result;
			}
		} 
		/*public virtual IQueryable<TS_LOGIC_SO_LIEU_DAU_KY> KTLOGIC_SO_LIEU_DAU_KY(string store, params object[] parameters)
		{

			var result = _dbContext.EntityFromStore<TS_LOGIC_SO_LIEU_DAU_KY>(store, parameters);
			return result;
		}*/
		public virtual IList<TS_LOGIC_SO_LIEU_DAU_KY> KTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
			, string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
			/*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*/, List<int> ListLoaiTaiSanId = null
			, decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
			, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
			, decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
			, decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
			, decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
			, decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
			, string stringLoaiDonVi = null, decimal? ChucDanhId = 0)
		{
			OracleParameter p1 = new OracleParameter("PNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("PID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pstr_loai_tai_san_id", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_tai_san_id", OracleDbType.Int32, LoaiDonViId, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("PDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("PDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			
			OracleParameter p15 = new OracleParameter("psotang_comparesign", OracleDbType.Decimal, SoTang_CompareSign, ParameterDirection.Input);
			OracleParameter p16 = new OracleParameter("psotang_value1", OracleDbType.Decimal, SoTang_Value1, ParameterDirection.Input);
			OracleParameter p17 = new OracleParameter("psotang_value2", OracleDbType.Decimal, SoTang_Value2, ParameterDirection.Input);

			OracleParameter p18 = new OracleParameter("pdientich_comparesign", OracleDbType.Decimal, DienTich_CompareSign, ParameterDirection.Input);
			OracleParameter p19 = new OracleParameter("pdientich_value1", OracleDbType.Decimal, DienTich_Value1, ParameterDirection.Input);
			OracleParameter p20 = new OracleParameter("pdientich_value2", OracleDbType.Decimal, DienTich_Value2, ParameterDirection.Input);

			OracleParameter p21 = new OracleParameter("psochongoi_comparesign", OracleDbType.Decimal, SoChoNgoi_CompareSign, ParameterDirection.Input);
			OracleParameter p22 = new OracleParameter("psochongoi_value1", OracleDbType.Decimal, SoChoNgoi_Value1, ParameterDirection.Input);
			OracleParameter p23 = new OracleParameter("psochongoi_value2", OracleDbType.Decimal, SoChoNgoi_Value2, ParameterDirection.Input);

			OracleParameter p24 = new OracleParameter("ptaitrong_comparesign", OracleDbType.Decimal, TaiTrong_CompareSign, ParameterDirection.Input);
			OracleParameter p25 = new OracleParameter("ptaitrong_value1", OracleDbType.Decimal, TaiTrong_Value1, ParameterDirection.Input);
			OracleParameter p26 = new OracleParameter("ptaitrong_value2", OracleDbType.Decimal, TaiTrong_Value2, ParameterDirection.Input);

			OracleParameter p39 = new OracleParameter("ptongnguyengia_comparesign", OracleDbType.Decimal, TongNguyenGia_CompareSign, ParameterDirection.Input);
			OracleParameter p40 = new OracleParameter("ptongnguyengia_value1", OracleDbType.Decimal, TongNguyenGia_Value1, ParameterDirection.Input);
			OracleParameter p41 = new OracleParameter("ptongnguyengia_value2", OracleDbType.Decimal, TongNguyenGia_Value2, ParameterDirection.Input);

			OracleParameter p42 = new OracleParameter("pdongia_comparesign", OracleDbType.Decimal, DonGia_CompareSign, ParameterDirection.Input);
			OracleParameter p43 = new OracleParameter("pdongia_value1", OracleDbType.Decimal, DonGia_Value1, ParameterDirection.Input);
			OracleParameter p44 = new OracleParameter("pdongia_value2", OracleDbType.Decimal, DonGia_Value2, ParameterDirection.Input);

			OracleParameter p51 = new OracleParameter("pLOAI_DON_VI_ID", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);

			OracleParameter p52 = new OracleParameter("PID_CHUC_DANH", OracleDbType.Decimal, ChucDanhId, ParameterDirection.Input);

			OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			var result = _dbContext.EntityFromStore<TS_LOGIC_SO_LIEU_DAU_KY>("PK_TS_BCTC_REPORT.PROC_LOGIC_SO_LIEU_DAU_KY", p1, p2, p4, p5, p6, p7, /*p8, p9, p10, p11, p12, p13, p14,*/ p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, /*p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38,*/ p39, p40, p41, p42, p43, p44,/* p45, p46, p47, p48, p49, p50,*/ p51, p52, pOut).ToList();
			return result;

			//return new PagedList<TS_LOGIC_SO_LIEU_DAU_KY>(result, pageIndex, pageSize);
		}
        public virtual IPagedList<TS_LOGIC_SO_LIEU_DAU_KY> PAGE_KTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
			, string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
			/*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*/, List<int> ListLoaiTaiSanId = null
			, decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
			, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
			, decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
			, decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
			, decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
			, decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
			, string stringLoaiDonVi = null, decimal? ChucDanhId = 0
			, int pageIndex = 0, int pageSize = 0) 
		{
			var result = KTLOGIC_SO_LIEU_DAU_KY(
				   NgayKetThuc: NgayKetThuc,
				   DonViId: DonViId,
				   stringLoaiTaiSan: stringLoaiTaiSan,
				   LoaiDonViId: LoaiDonViId,
				   ListLoaiTaiSanId: ListLoaiTaiSanId,
				   SoTang_CompareSign: SoTang_CompareSign,
				   SoTang_Value1: SoTang_Value1,
				   SoTang_Value2: SoTang_Value2,
				   DienTich_CompareSign: DienTich_CompareSign,
				   DienTich_Value1: DienTich_Value1,
				   DienTich_Value2: DienTich_Value2,
				   SoChoNgoi_CompareSign: SoChoNgoi_CompareSign,
				   SoChoNgoi_Value1: SoChoNgoi_Value1,
				   SoChoNgoi_Value2: SoChoNgoi_Value2,
				   TaiTrong_CompareSign: TaiTrong_CompareSign,
				   TaiTrong_Value1: TaiTrong_Value1,
				   TaiTrong_Value2: TaiTrong_Value2,
				   TongNguyenGia_CompareSign: TongNguyenGia_CompareSign,
				   TongNguyenGia_Value1: TongNguyenGia_Value1,
				   TongNguyenGia_Value2: TongNguyenGia_Value2,
				   stringLoaiDonVi: stringLoaiDonVi,
				   ChucDanhId: ChucDanhId);

			return new PagedList<TS_LOGIC_SO_LIEU_DAU_KY>(result, pageIndex, pageSize);
        }
        /*public virtual IPagedList<TS_LOGIC_SO_LIEU_DAU_KY> GetTaiSanKTLOGIC_SO_LIEU_DAU_KY(DateTime? NgayKetThuc = null, Int32 DonViId = 0
			, string stringLoaiTaiSan = null, int? LoaiDonViId = null, int donViTien = 1000, int DonViDienTich = 1
			*//*, decimal? NhanXeId = 0, int LoaiTaiSan = 0*//*, List<int> ListLoaiTaiSanId = null
			, decimal? SoTang_CompareSign = 0, decimal? SoTang_Value1 = null, decimal? SoTang_Value2 = null
			, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = null, decimal? DienTich_Value2 = null
			, decimal? SoChoNgoi_CompareSign = 0, decimal? SoChoNgoi_Value1 = null, decimal? SoChoNgoi_Value2 = null
			, decimal? TaiTrong_CompareSign = 0, decimal? TaiTrong_Value1 = null, decimal? TaiTrong_Value2 = null
			, decimal? TongNguyenGia_CompareSign = 0, decimal? TongNguyenGia_Value1 = null, decimal? TongNguyenGia_Value2 = null
			, decimal? DonGia_CompareSign = 0, decimal? DonGia_Value1 = null, decimal? DonGia_Value2 = null
			, string stringLoaiDonVi = null, int pageIndex = 0, int pageSize = 0)
		*//*, decimal? NamSD_CompareSign = 0, decimal? NamSD_Value1 = null, decimal? NamSD_Value2 = null
		, decimal? NamSx_CompareSign = 0, decimal? NamSx_Value1 = null, decimal? NamSx_Value2 = null*/
        /*, decimal? NguyenGiaNS_CompareSign = 0, decimal? NguyenGiaNS_Value1 = null, decimal? NguyenGiaNS_Value2 = null
		, decimal? NguyenGiaKhac_CompareSign = 0, decimal? NguyenGiaKhac_Value1 = null, decimal? NguyenGiaKhac_Value2 = null
		, decimal? NguyenGiaODA_CompareSign = 0, decimal? NguyenGiaODA_Value1 = null, decimal? NguyenGiaODA_Value2 = null
		, decimal? NguyenGiaVienTro_CompareSign = 0, decimal? NguyenGiaVienTro_Value1 = null, decimal? NguyenGiaVienTro_Value2 = null*/
        /*, decimal? TyLeChatLuong_CompareSign = 0, decimal? TyLeChatLuong_Value1 = null, decimal? TyLeChatLuong_Value2 = null
		, decimal? GTCL_CompareSign = 0, decimal? GTCL_Value1 = null, decimal? GTCL_Value2 = null
		, decimal? ChucDanhId = 0, decimal? SoCau_CompareSign = 0, decimal? SoCau_Value1 = null, decimal? SoCau_Value2 = null)*//*
		{
			OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayKetThuc, ParameterDirection.Input);
			OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
			OracleParameter p4 = new OracleParameter("pstr_loai_tai_san_id", OracleDbType.Varchar2, stringLoaiTaiSan, ParameterDirection.Input);
			OracleParameter p5 = new OracleParameter("ploai_tai_san_id", OracleDbType.Int32, LoaiDonViId, ParameterDirection.Input);
			OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, donViTien, ParameterDirection.Input);
			OracleParameter p7 = new OracleParameter("pDON_VI_DIEN_TICH", OracleDbType.Int32, DonViDienTich, ParameterDirection.Input);
			*//*OracleParameter p8 = new OracleParameter("PID_NHAN_XE", OracleDbType.Decimal, NhanXeId, ParameterDirection.Input);
			OracleParameter p9 = new OracleParameter("pNamSD_CompareSign", OracleDbType.Decimal, NamSD_CompareSign, ParameterDirection.Input);
			OracleParameter p10 = new OracleParameter("pNamSD_Value1", OracleDbType.Decimal, NamSD_Value1, ParameterDirection.Input);
			OracleParameter p11 = new OracleParameter("pNamSD_Value2", OracleDbType.Decimal, NamSD_Value2, ParameterDirection.Input);
			OracleParameter p12 = new OracleParameter("pNamSx_CompareSign", OracleDbType.Decimal, NamSx_CompareSign, ParameterDirection.Input);
			OracleParameter p13 = new OracleParameter("pNamSx_Value1", OracleDbType.Decimal, NamSx_Value1, ParameterDirection.Input);
			OracleParameter p14 = new OracleParameter("pNamSx_Value2", OracleDbType.Decimal, NamSx_Value2, ParameterDirection.Input);*//*
			OracleParameter p15 = new OracleParameter("pSoTang_CompareSign", OracleDbType.Decimal, SoTang_CompareSign, ParameterDirection.Input);
			OracleParameter p16 = new OracleParameter("pSoTang_Value1", OracleDbType.Decimal, SoTang_Value1, ParameterDirection.Input);
			OracleParameter p17 = new OracleParameter("pSoTang_Value2", OracleDbType.Decimal, SoTang_Value2, ParameterDirection.Input);

			OracleParameter p18 = new OracleParameter("pDienTich_CompareSign", OracleDbType.Decimal, DienTich_CompareSign, ParameterDirection.Input);
			OracleParameter p19 = new OracleParameter("pDienTich_Value1", OracleDbType.Decimal, DienTich_Value1, ParameterDirection.Input);
			OracleParameter p20 = new OracleParameter("pDienTich_Value2", OracleDbType.Decimal, DienTich_Value2, ParameterDirection.Input);

			OracleParameter p21 = new OracleParameter("psochongoi_comparesign", OracleDbType.Decimal, SoChoNgoi_CompareSign, ParameterDirection.Input);
			OracleParameter p22 = new OracleParameter("psochongoi_value1", OracleDbType.Decimal, SoChoNgoi_Value1, ParameterDirection.Input);
			OracleParameter p23 = new OracleParameter("psochongoi_value2", OracleDbType.Decimal, SoChoNgoi_Value2, ParameterDirection.Input);

			OracleParameter p24 = new OracleParameter("ptaitrong_comparesign", OracleDbType.Decimal, TaiTrong_CompareSign, ParameterDirection.Input);
			OracleParameter p25 = new OracleParameter("ptaitrong_value1", OracleDbType.Decimal, TaiTrong_Value1, ParameterDirection.Input);
			OracleParameter p26 = new OracleParameter("ptaitrong_value2", OracleDbType.Decimal, TaiTrong_Value2, ParameterDirection.Input);

			*//*OracleParameter p27 = new OracleParameter("pnguyengians_comparesign", OracleDbType.Decimal, NguyenGiaNS_CompareSign, ParameterDirection.Input);
			OracleParameter p28 = new OracleParameter("pnguyengians_value1", OracleDbType.Decimal, NguyenGiaNS_Value1, ParameterDirection.Input);
			OracleParameter p29 = new OracleParameter("pnguyengians_value2", OracleDbType.Decimal, NguyenGiaNS_Value2, ParameterDirection.Input);

			OracleParameter p30 = new OracleParameter("pnguyengiakhac_comparesign", OracleDbType.Decimal, NguyenGiaKhac_CompareSign, ParameterDirection.Input);
			OracleParameter p31 = new OracleParameter("pnguyengiakhac_value1", OracleDbType.Decimal, NguyenGiaKhac_Value1, ParameterDirection.Input);
			OracleParameter p32 = new OracleParameter("pnguyengiakhac_value2", OracleDbType.Decimal, NguyenGiaKhac_Value2, ParameterDirection.Input);

			OracleParameter p33 = new OracleParameter("pnguyengiaoda_comparesign", OracleDbType.Decimal, NguyenGiaODA_CompareSign, ParameterDirection.Input);
			OracleParameter p34 = new OracleParameter("pnguyengiaoda_value1", OracleDbType.Decimal, NguyenGiaODA_Value1, ParameterDirection.Input);
			OracleParameter p35 = new OracleParameter("pnguyengiaoda_value2", OracleDbType.Decimal, NguyenGiaODA_Value2, ParameterDirection.Input);

			OracleParameter p36 = new OracleParameter("pnguyengiavientro_comparesign", OracleDbType.Decimal, NguyenGiaVienTro_CompareSign, ParameterDirection.Input);
			OracleParameter p37 = new OracleParameter("pnguyengiavientro_value1", OracleDbType.Decimal, NguyenGiaVienTro_Value1, ParameterDirection.Input);
			OracleParameter p38 = new OracleParameter("pnguyengiavientro_value2", OracleDbType.Decimal, NguyenGiaVienTro_Value2, ParameterDirection.Input);*//*

			OracleParameter p39 = new OracleParameter("ptongnguyengia_comparesign", OracleDbType.Decimal, TongNguyenGia_CompareSign, ParameterDirection.Input);
			OracleParameter p40 = new OracleParameter("ptongnguyengia_value1", OracleDbType.Decimal, TongNguyenGia_Value1, ParameterDirection.Input);
			OracleParameter p41 = new OracleParameter("ptongnguyengia_value2", OracleDbType.Decimal, TongNguyenGia_Value2, ParameterDirection.Input);

			OracleParameter p42 = new OracleParameter("pdongia_comparesign", OracleDbType.Decimal, DonGia_CompareSign, ParameterDirection.Input);
			OracleParameter p43 = new OracleParameter("pdongia_value1", OracleDbType.Decimal, DonGia_Value1, ParameterDirection.Input);
			OracleParameter p44 = new OracleParameter("pdongia_value2", OracleDbType.Decimal, DonGia_Value2, ParameterDirection.Input);

			*//*OracleParameter p45 = new OracleParameter("pgtcl_comparesign", OracleDbType.Decimal, GTCL_CompareSign, ParameterDirection.Input);
			OracleParameter p46 = new OracleParameter("pgtcl_value1", OracleDbType.Decimal, GTCL_Value1, ParameterDirection.Input);
			OracleParameter p47 = new OracleParameter("pgtcl_value2", OracleDbType.Decimal, GTCL_Value2, ParameterDirection.Input);

			OracleParameter p48 = new OracleParameter("pTyLeChatLuong_comparesign", OracleDbType.Decimal, TyLeChatLuong_CompareSign, ParameterDirection.Input);
			OracleParameter p49 = new OracleParameter("pTyLeChatLuong_value1", OracleDbType.Decimal, TyLeChatLuong_Value1, ParameterDirection.Input);
			OracleParameter p50 = new OracleParameter("pTyLeChatLuong_value2", OracleDbType.Decimal, TyLeChatLuong_Value2, ParameterDirection.Input);*//*


			OracleParameter p51 = new OracleParameter("pLOAI_DON_VI_ID", OracleDbType.Varchar2, stringLoaiDonVi, ParameterDirection.Input);


			*//*OracleParameter p52 = new OracleParameter("PID_CHUC_DANH", OracleDbType.Decimal, ChucDanhId, ParameterDirection.Input);
			OracleParameter p53 = new OracleParameter("pSoCau_comparesign", OracleDbType.Decimal, SoCau_CompareSign, ParameterDirection.Input);
			OracleParameter p54 = new OracleParameter("pSoCau_value1", OracleDbType.Decimal, SoCau_Value1, ParameterDirection.Input);
			OracleParameter p55 = new OracleParameter("pSoCau_value2", OracleDbType.Decimal, SoCau_Value2, ParameterDirection.Input);*//*
			OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

			var taisans = _dbContext.EntityFromStore<TS_LOGIC_SO_LIEU_DAU_KY>("PK_TS_BCTC_REPORT.PROC_LOGIC_SO_LIEU_DAU_KY", p1, p2, p4, p5, p6, p7,*//* p8, p9, p10, p11, p12, p13, p14, *//*p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, *//*p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38,*//* p39, p40, p41, p42, p43, p44, *//*p45, p46, p47, p48, p49, p50,*//* p51, *//*p52, p53, p54, p55, *//*pOut);

			return new PagedList<TS_LOGIC_SO_LIEU_DAU_KY>(taisans, pageIndex, pageSize);

		}*/
    }
}