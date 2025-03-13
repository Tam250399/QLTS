//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GS.Services.KT
{
    public partial class HaoMonTaiSanService : IHaoMonTaiSanService
    {
        #region Fields

        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<HaoMonTaiSan> _itemRepository;

        #endregion Fields

        #region Ctor

        public HaoMonTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<HaoMonTaiSan> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion Ctor

        #region Methods

        public virtual IList<HaoMonTaiSan> GetAllHaoMonTaiSans(bool IsDongBo = false)
        {
            var query = _itemRepository.Table;
            if (IsDongBo)
            {
                query = query.Where(m => m.TaiSan.MA_DB != null);
            }
            return query.ToList();
        }

        public virtual IPagedList<HaoMonTaiSan> SearchHaoMonTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal NamHaoMon = 0, decimal LoaiHinhTaiSan = 0, decimal DonViId = 0)
        {
            var query = _itemRepository.Table.Where(p => p.TAI_SAN_ID > 0 &&
            (
            p.TaiSan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || p.TaiSan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO
            ));
            query = query.Where(p => p.TaiSan.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && p.TaiSan.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU);
            if (DonViId > 0 && DonViId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                query = query.Where(p => p.TaiSan.DON_VI_ID == DonViId);
            if (!string.IsNullOrEmpty(Keysearch))
            {
                string keyUpper = Keysearch.ToUpper();
                query = query.Where(p => p.MA_TAI_SAN.ToUpper().Contains(keyUpper) || p.TaiSan.TEN.ToUpper().Contains(keyUpper));
            }
            if (NamHaoMon > 0)
                query = query.Where(p => p.NAM_HAO_MON == NamHaoMon);
            if (LoaiHinhTaiSan > 0)
                query = query.Where(p => p.TaiSan.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            return new PagedList<HaoMonTaiSan>(query.OrderBy(p => p.MA_TAI_SAN).ThenBy(p => p.NAM_HAO_MON), pageIndex, pageSize);
        }

        public virtual HaoMonTaiSan GetHaoMonTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual HaoMonTaiSan GetHaoMonTaiSanByTSIdandNamBaoCao(decimal? tsId = 0, decimal? namBaoCao = 0, string MaTaiSan = null)
        {
            if ((tsId > 0 && namBaoCao > 0))
            {
                var query = _itemRepository.Table.Where(c => (c.TAI_SAN_ID == tsId) && c.NAM_HAO_MON == namBaoCao).FirstOrDefault();
                return query;
            }
            if (!string.IsNullOrEmpty(MaTaiSan) && namBaoCao > 0)
            {
                var query = _itemRepository.Table.Where(c => (c.MA_TAI_SAN == MaTaiSan) && c.NAM_HAO_MON == namBaoCao).FirstOrDefault();
                return query;
            }
            return null;
        }

        public virtual IList<HaoMonTaiSan> GetHaoMonTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertHaoMonTaiSan(HaoMonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void InsertHaoMonTaiSan(IEnumerable<HaoMonTaiSan> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateHaoMonTaiSan(HaoMonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void UpdateHaoMonTaiSan(IEnumerable<HaoMonTaiSan> entity)
        {
            if (entity == null || (entity != null && entity.Count() == 0))
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteHaoMonTaiSan(HaoMonTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteHmtsByIdTaiSan(decimal IdTaiSan)
        {
            var listHmTaiSan = _itemRepository.Table.Where(c => c.TAI_SAN_ID == IdTaiSan);
            foreach(var item in listHmTaiSan)
            {
                DeleteHaoMonTaiSan(item);
            }    
        }
        /// <summary>
        /// Description: Insert hoặc update bản ghi chốt giá trị tài sản
        /// </summary>
        /// <param name="p_TaiSanId"></param>
        /// <param name="p_NamHaoMon"></param>
        /// <returns></returns>
        public virtual bool InsertOrUpdateRecordTblKTHM(decimal p_TaiSanId, decimal p_NamHaoMon)
        {
            OracleParameter pTaiSanId = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, p_TaiSanId, ParameterDirection.Input);
            OracleParameter pNamHaoMon = new OracleParameter("pNAM_HAO_MON", OracleDbType.Int32, p_NamHaoMon, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN SP_CALCULATE_CHOT_HMTS(:pTAI_SAN_ID, :pNAM_HAO_MON); END;", false, null, pTaiSanId, pNamHaoMon);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Description: Chốt hao mòn tài sản của đơn vị
        /// </summary>
        /// <param name="p_TaiSanId"></param>
        /// <param name="p_NamHaoMon"></param>
        /// <returns></returns>
        public virtual bool ChotToanBoHaoMonDonVi(decimal donViId)
        {
            OracleParameter pDonViId = new OracleParameter("PDON_VI_ID", OracleDbType.Int32, donViId, ParameterDirection.Input);;
            OracleParameter pNam = new OracleParameter("PNAM_KE_KHAI", OracleDbType.Int32, 0, ParameterDirection.Input);;
            OracleParameter pTaiSanId = new OracleParameter("PTAI_SAN_ID", OracleDbType.Int32, 0, ParameterDirection.Input);;
            OracleParameter pLoaiHinhTaiSan = new OracleParameter("PLOAI_HINH_TAI_SAN_ID", OracleDbType.Int32, 0, ParameterDirection.Input);;
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN sp_chot_toan_bo_tai_san_v2(:PDON_VI_ID,:PNAM_KE_KHAI,:PTAI_SAN_ID,:PLOAI_HINH_TAI_SAN_ID); END;", false, null, pDonViId,pNam,pTaiSanId,pLoaiHinhTaiSan);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Function thực hiện chức năng chốt giá trị tài sản từ năm đến năm hiện tại
        /// </summary>
        /// <param name="p_TaiSanId">id tài sản chốt</param>
        /// <param name="p_Year">năm bắt đầu chốt</param>
        public virtual void ChotGiaTriHaoMonTaiSan(decimal p_TaiSanId, DateTime p_Year)
        {
            //Cập nhật thông tin giá trị tài sản vào bảng KT

            var moment = DateTime.Now;
            if (p_Year.Year < moment.Year)
            {
                for (int namHaoMon = p_Year.Year; namHaoMon <= moment.Year; namHaoMon++)
                {
                    InsertOrUpdateRecordTblKTHM(p_TaiSanId: p_TaiSanId, p_NamHaoMon: namHaoMon);
                }
            }
            else
            {
                InsertOrUpdateRecordTblKTHM(p_TaiSanId: p_TaiSanId, p_NamHaoMon: p_Year.Year);
            }
        }

        public IList<HaoMonTaiSan> GetListHaoMonTaiSans(decimal? taiSanId = 0, decimal? namHaoMon = 0)
        {
            if (taiSanId == 0 && namHaoMon == 0)
                return null;
            var query = _itemRepository.Table;
            if (taiSanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            }
            if (namHaoMon > 0)
            {
                query = query.Where(c => c.NAM_HAO_MON == namHaoMon);
            }
            return query.ToList();
        }

        public HaoMonTaiSan GetHaoMonTaiSanGanNhat(decimal? taiSanId = 0)
        {
            if (taiSanId > 0)
            {
                return _itemRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId).OrderByDescending(p => p.NAM_HAO_MON).FirstOrDefault();
            }
            return null;
        }

        public decimal GetLKHMTruocDo(HaoMonTaiSan haoMonTaiSan)
        {
            decimal tongHaoMonLKTTruoc = 0;
            var haoMonTruoc = GetHaoMonTaiSanByTSIdandNamBaoCao(haoMonTaiSan.TAI_SAN_ID, haoMonTaiSan.NAM_HAO_MON - 1);
            if (haoMonTruoc != null)
                tongHaoMonLKTTruoc = haoMonTruoc.TONG_HAO_MON_LUY_KE.GetValueOrDefault();
            else
                tongHaoMonLKTTruoc = haoMonTaiSan.TONG_HAO_MON_LUY_KE.GetValueOrDefault() - haoMonTaiSan.GIA_TRI_HAO_MON.GetValueOrDefault();

            return tongHaoMonLKTTruoc;
        }

        public List<HaoMonTaiSan> GetListHMTSSauNam(decimal TaiSanId, decimal Nam)
        {
            var query = _itemRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId && p.NAM_HAO_MON > Nam).OrderBy(p => p.NAM_HAO_MON);
            return query.ToList();
        }


        #region Funtion thực hiện chốt giá trị tài sản (table: KT_*)

        #endregion Funtion thực hiện chốt giá trị tài sản (table: KT_*)
        #endregion Methods

        public List<HaoMonKho> GetHaoMonDongBo(string maDonVi = null, decimal? taiSanId = null, decimal? nguonTaiSan = null)
        {
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN = new OracleParameter("P_NGUON_TAI_SAN", OracleDbType.Decimal, nguonTaiSan, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<HaoMonKho>("PKG_DONG_BO.GET_INFO_DONG_BO_KT_HAO_MON", P_MA_DON_VI, P_NGUON_TAI_SAN, TBL_OUT);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AssetCodeHaoMon> GetHaoMonCanCheckLog(string maDonVi = null, decimal? nguonTaiSan = null, decimal? trangThaiDB = 1)
        {
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN = new OracleParameter("P_NGUON_TAI_SAN", OracleDbType.Decimal, nguonTaiSan, ParameterDirection.Input);
            OracleParameter P_TRANG_THAI = new OracleParameter("P_TRANG_THAI", OracleDbType.Decimal, trangThaiDB, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<AssetCodeHaoMon>("PKG_DONG_BO.GET_HAO_MON_BY_TRANG_THAI_DB", P_MA_DON_VI, P_NGUON_TAI_SAN, P_TRANG_THAI, TBL_OUT);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void UPDATE_TRANG_THAI_DONG_BO_HAO_MON(List<HaoMonKho> haoMonKhos, decimal trangThai = 0)
        {
            int total = haoMonKhos.Count();
            int pageSize = 1000;
            int TotalPages = total / pageSize;
            if (total % pageSize > 0)
                TotalPages++;
            for (int i = 0; i < TotalPages; i++)
            {
                List<HaoMonKho> lstSplit = haoMonKhos.Skip(i * pageSize).Take(pageSize).ToList();
                String sql = $"UPDATE KT_HAO_MON_TAI_SAN SET TRANG_THAI_DONG_BO = {trangThai} WHERE ";
                List<string> lstIds = haoMonKhos.Select(x => String.Format("(1,{0})", x.syncSourceId)).ToList();
                sql += string.Format("(1,ID) IN ({0})", string.Join(",", lstIds));
                try
                {
                    var rs = _dbContext.ExecuteSqlCommand(sql);
                }
                catch (Exception ex)
                {
                    var mss = ex;
                }
            }
        }
    }
}