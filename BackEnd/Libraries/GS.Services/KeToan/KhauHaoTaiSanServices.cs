//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.KT;
using Oracle.ManagedDataAccess.Client;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;

namespace GS.Services.KT
{
    public partial class KhauHaoTaiSanService : IKhauHaoTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhauHaoTaiSan> _itemRepository;
        #endregion

        #region Ctor

        public KhauHaoTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<KhauHaoTaiSan> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<KhauHaoTaiSan> GetAllKhauHaoTaiSans()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhauHaoTaiSan> SearchKhauHaoTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal ThangKhauHao = 0, decimal NamKhauHao = 0, decimal LoaiHinhTaiSan = 0, decimal DonViId = 0)
        {
            var query = _itemRepository.Table.Where(p => p.TAI_SAN_ID > 0 && (p.TaiSan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || p.TaiSan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO));

            query = query.Where(p => p.TaiSan.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU);

            if (DonViId > 0 && DonViId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                query = query.Where(p => p.TaiSan.DON_VI_ID == DonViId);

            if (!string.IsNullOrEmpty(Keysearch))
            {
                string keyUpper = Keysearch.ToUpper();
                query = query.Where(p => p.MA_TAI_SAN.ToUpper().Contains(keyUpper) || p.TaiSan.TEN.ToUpper().Contains(keyUpper));
                query = query.OrderBy(p => p.NAM_KHAU_HAO).ThenBy(p => p.THANG_KHAU_HAO);
            }
            if (ThangKhauHao > 0)
                query = query.Where(p => p.THANG_KHAU_HAO == ThangKhauHao);
            if (NamKhauHao > 0)
                query = query.Where(p => p.NAM_KHAU_HAO == NamKhauHao);
            if (LoaiHinhTaiSan > 0)
                query = query.Where(p => p.TaiSan.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            return new PagedList<KhauHaoTaiSan>(query.OrderBy(x => x.MA_TAI_SAN), pageIndex, pageSize); ;
        }

        public virtual KhauHaoTaiSan GetKhauHaoTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<KhauHaoTaiSan> GetKhauHaoTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertKhauHaoTaiSan(KhauHaoTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void InsertKhauHaoTaiSan(IEnumerable<KhauHaoTaiSan> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhauHaoTaiSan(KhauHaoTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateKhauHaoTaiSan(IEnumerable<KhauHaoTaiSan> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKhauHaoTaiSan(KhauHaoTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteKhauHaoTaiSanByNgay(decimal? taiSanId, decimal namKH, decimal? thangKH)
        {
            var lstkhTaiSan = _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId && ((c.NAM_KHAU_HAO == namKH && c.THANG_KHAU_HAO > thangKH) || (c.NAM_KHAU_HAO > namKH))).ToList();
            foreach (var item in lstkhTaiSan)
            {
                _itemRepository.Delete(item);
            }
        }

        /// <summary>
        /// Description: Insert hoặc update bản ghi chốt giá trị tài sản (Khấu hao)
        /// </summary>
        /// <param name="p_TaiSanId"></param>
        /// <param name="p_NamKhauHao"></param>
        /// <param name="p_ThangKhauHao"></param>
        /// <returns></returns>
        public virtual bool InsertOrUpdateRecordTblKTKH(decimal p_TaiSanId, decimal p_NamKhauHao, decimal p_ThangKhauHao)
        {
            OracleParameter pTaiSanId = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, p_TaiSanId, ParameterDirection.Input);
            OracleParameter pNamKhauHao = new OracleParameter("pNAM_KHAU_HAO", OracleDbType.Int32, p_NamKhauHao, ParameterDirection.Input);
            OracleParameter pThangKhauHao = new OracleParameter("pTHANG_KHAU_HAO", OracleDbType.Int32, p_ThangKhauHao, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN SP_CALCULATE_CHOT_KHTS(:pTAI_SAN_ID, :pNAM_KHAU_HAO,:pTHANG_KHAU_HAO); END;", false, null, pTaiSanId, pNamKhauHao, pThangKhauHao);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual void ChotGiaTriKhauHaoTaiSan(decimal p_TaiSanId, DateTime p_FromDate)
        {
            var moment = DateTime.Now;
            for (int month = p_FromDate.Month; month <= 12; month++)
            {
                InsertOrUpdateRecordTblKTKH(p_TaiSanId: p_TaiSanId, p_NamKhauHao: p_FromDate.Year, p_ThangKhauHao: month);
            }
            for (int year = p_FromDate.Year + 1; year < moment.Year; year++)
            {
                //Lặp theo tháng
                for (int month = 1; month <= 12; month++)
                {
                    InsertOrUpdateRecordTblKTKH(p_TaiSanId: p_TaiSanId, p_NamKhauHao: year, p_ThangKhauHao: month);
                }
            }
            for (int month = 1; month <= moment.Month; month++)
            {
                InsertOrUpdateRecordTblKTKH(p_TaiSanId: p_TaiSanId, p_NamKhauHao: moment.Year, p_ThangKhauHao: month);
            }
        }

        public IList<KhauHaoTaiSan> GetListKhauHaoTaiSans(decimal? taiSanId = 0, decimal? namKhauHao = 0, decimal? thangKhauHao = 0, string MaTaiSan = null)
        {
            if (taiSanId == 0 && MaTaiSan == null && thangKhauHao == 0 && namKhauHao == 0)
                return null;
            var query = _itemRepository.Table;
            if (taiSanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            }
            if (!string.IsNullOrEmpty(MaTaiSan))
            {
                query = query.Where(c => c.MA_TAI_SAN == MaTaiSan);
            }
            if (namKhauHao > 0)
            {
                query = query.Where(c => c.NAM_KHAU_HAO == namKhauHao);
            }
            if (thangKhauHao > 0)
            {
                query = query.Where(c => c.THANG_KHAU_HAO == thangKhauHao);
            }
            return query.ToList();
        }

        /*kh thang sau*/
        public List<KhauHaoTaiSan> GetListKHTSSau(decimal TaiSanId, decimal? Thang, decimal Nam)
        {
            var query = _itemRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId && (p.NAM_KHAU_HAO > Nam || (p.THANG_KHAU_HAO > Thang && p.NAM_KHAU_HAO == Nam))).OrderBy(p => p.NAM_KHAU_HAO).ThenBy(p => p.THANG_KHAU_HAO);
            return query.ToList();
        }

        public virtual KhauHaoTaiSan GetKhauHaoTaiSanByTSIdandThangNam(decimal? taiSanId = 0, decimal? namKhauHao = 0, decimal? thangKhauHao = 0, string MaTaiSan = null)
        {
            if (thangKhauHao - 1 <= 0)
            {
                thangKhauHao = 12;
                namKhauHao = namKhauHao - 1;
            }
            else
            {
                thangKhauHao = thangKhauHao - 1;
            }

            if (taiSanId > 0 && namKhauHao > 0 && thangKhauHao > 0)
            {
                var query = _itemRepository.Table.Where(c => (c.TAI_SAN_ID == taiSanId) && c.THANG_KHAU_HAO == thangKhauHao && c.NAM_KHAU_HAO == namKhauHao).FirstOrDefault();
                return query;
            }
            if (!string.IsNullOrEmpty(MaTaiSan) && namKhauHao > 0 && thangKhauHao > 0)
            {
                var query = _itemRepository.Table.Where(c => (c.MA_TAI_SAN == MaTaiSan) && c.THANG_KHAU_HAO == thangKhauHao && c.NAM_KHAU_HAO == namKhauHao).FirstOrDefault();
                return query;
            }
            return null;
        }

        public decimal GetLKKHTruocDo(KhauHaoTaiSan khauHaoTaiSan)
        {
            decimal tongHaoMonLKTTruoc = 0;
            var khauHaoTruoc = GetKhauHaoTaiSanByTSIdandThangNam(khauHaoTaiSan.TAI_SAN_ID, khauHaoTaiSan.NAM_KHAU_HAO, khauHaoTaiSan.THANG_KHAU_HAO, khauHaoTaiSan.MA_TAI_SAN);
            if (khauHaoTruoc != null)
                tongHaoMonLKTTruoc = khauHaoTruoc.TONG_KHAU_HAO_LUY_KE.GetValueOrDefault();
            else
                tongHaoMonLKTTruoc = khauHaoTaiSan.TONG_KHAU_HAO_LUY_KE.GetValueOrDefault() - khauHaoTaiSan.GIA_TRI_KHAU_HAO.GetValueOrDefault();

            return tongHaoMonLKTTruoc;
        }
        #endregion

        #region chốt khau hao theo don vi
        public virtual bool ChotToanBoKhauHaoDonVi(decimal donViId)
        {
            OracleParameter pDonViId = new OracleParameter("PDON_VI_ID", OracleDbType.Int32, donViId, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN sp_calculate_chot_khts_by_don_vi_2022(:PDON_VI_ID); END;", false, null, pDonViId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region chốt khau hao theo id tai sản
        public virtual bool ChotKhauHaoOneTs(decimal taiSanId, decimal namBatDau, decimal thangBatDau, decimal namKetThuc, decimal thangKetThuc)
        {
            OracleParameter pTaiSanId = new OracleParameter("ptai_san_id", OracleDbType.Int32, taiSanId, ParameterDirection.Input);
            OracleParameter P_KhNamBatDau = new OracleParameter("pkh_nam_bat_dau", OracleDbType.Int32, namBatDau, ParameterDirection.Input);
            OracleParameter P_KhThangBatDau = new OracleParameter("pkh_thang_bat_dau", OracleDbType.Int32, thangBatDau, ParameterDirection.Input);
            OracleParameter P_KhNamKetThuc = new OracleParameter("pkh_nam_ket_thuc", OracleDbType.Int32, namKetThuc, ParameterDirection.Input);
            OracleParameter P_KhThangKetThuc = new OracleParameter("pkh_thang_ket_thuc", OracleDbType.Int32, thangKetThuc, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN sp_calculate_chot_khts_by_mats_2022(:PTAI_SAN_ID,:PKH_NAM_BAT_DAU,:PKH_THANG_BAT_DAU,:PKH_NAM_KET_THUC,:PKH_THANG_KET_THUC); END;", false, null, pTaiSanId, P_KhNamBatDau, P_KhThangBatDau, P_KhNamKetThuc, P_KhThangKetThuc);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

