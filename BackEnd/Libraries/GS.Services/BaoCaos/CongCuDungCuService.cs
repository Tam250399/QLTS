using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.CCDC;
using GS.Core.Domain.BaoCaos.TS_CDKT;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GS.Services.BaoCaos
{
    public partial class CongCuDungCuService : ICongCuDungCuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public CongCuDungCuService(
            CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        public virtual IList<BaoCaoKiemKe> BaoCaoKiemKeCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000)
        {

            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Decimal, DonViBoPhan, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoKiemKe>("PK_CCDC_REPORT.SP_KIEM_KE", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoKiemKe> BaoCaoKiemKePhongBanCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000)
        {

            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pLIST_BO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoKiemKe>("PK_CCDC_REPORT.SP_KIEM_KE_PHONG_BAN", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoKiemKe> BaoCaoTongHopKiemKe(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Decimal, DonViBoPhan, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoKiemKe>("PK_CCDC_REPORT.SP_KIEM_KE", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<LietKeCCDC> BaoCaoLietKeCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<LietKeCCDC>("PK_CCDC_REPORT.SP_LIET_KE_CONG_CU", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NgayFrom"></param>
        /// <param name="NgayTo"></param>
        /// <param name="DonViId"></param>
        /// <param name="BoPhanId"></param>
        /// <param name="NhomCongCuId"></param>
        /// <param name="TangOrGiam">0: ca hai; 1: tang; 2: giam</param>
        /// <returns></returns>
        public virtual IList<TangGiamCongCu> BaoCaoTangGiamCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal TangOrGiam = 0, int DonViTien = 1000, string ListLyDoTang = null, string ListLyDoGiam = null)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayFrom, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayTo, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pXUAT_OR_NHAP", OracleDbType.Int32, TangOrGiam, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);          
            OracleParameter p8 = new OracleParameter("pLIST_LY_DO_TANG", OracleDbType.Varchar2, ListLyDoTang, ParameterDirection.Input);
            OracleParameter p9 = new OracleParameter("pLIST_LY_DO_GIAM", OracleDbType.Varchar2, ListLyDoGiam, ParameterDirection.Input);
            OracleParameter p10 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<TangGiamCongCu>("PK_CCDC_REPORT.SP_TANG_GIAM_CONG_CU", p1, p2, p3, p4, p5, p6, p7, p8,p9,p10).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoSuaChua> BaoCaoSuaChuaCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayFrom, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayTo, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoSuaChua>("PK_CCDC_REPORT.SP_SUA_CHUA_CONG_CU", p1, p2, p3, p4, p5, p6, p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoHongMat> BaoCaoHongMatCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayFrom, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayTo, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoHongMat>("PK_CCDC_REPORT.SP_HONG_MAT_CONG_CU", p1, p2, p3, p4, p5, p6, p7).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BaoCaoPhanBo> BaoCaoPhanBoCongCu(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BaoCaoPhanBo>("PK_CCDC_REPORT.SP_PHAN_BO_CONG_CU", p1, p2, p3, p4, p5, p6).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<BienBanKiemKeCCDC> BienBanKiemke(int KiemKeId = 0, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("p_KIEM_KE_ID", OracleDbType.Int32, KiemKeId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<BienBanKiemKeCCDC>("PK_CCDC_REPORT.SP_BIEN_BAN_KIEM_KE", p1, p2, p3).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<TongHopTon> BaoCaoTongHopTon(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, int DonViTien = 1000)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO_FROM", OracleDbType.Date, NgayFrom, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BAO_CAO_TO", OracleDbType.Date, NgayTo, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pID_DON_VI", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<TongHopTon>("PK_CCDC_REPORT.sp_tong_hop_ton_ccdc", p1, p2, p3, p4, p5).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IList<TongHopCCDC> BaoCaoTongHopCCDC(DateTime NgayBaoCao, String StringDonVi = null, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000, int MauSo = 1, int BacDonVi = 1)
        {
            OracleParameter p1 = new OracleParameter("pNGAY_BAO_CAO", OracleDbType.Date, NgayBaoCao, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, StringDonVi, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pBO_PHAN_ID", OracleDbType.Varchar2, BoPhanId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("pNHOM_CONG_CU_ID", OracleDbType.Varchar2, NhomCongCuId, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("pDON_VI_TIEN", OracleDbType.Int32, DonViTien, ParameterDirection.Input);
            OracleParameter p6 = new OracleParameter("pMAU_SO", OracleDbType.Int32, MauSo, ParameterDirection.Input);
            OracleParameter p7 = new OracleParameter("pBAC_DON_VI", OracleDbType.Int32, BacDonVi, ParameterDirection.Input);
            OracleParameter p8 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<TongHopCCDC>("PK_CCDC_REPORT.SP_TONG_HOP_CONG_CU", p1, p2, p3, p4, p5, p6, p7, p8).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
