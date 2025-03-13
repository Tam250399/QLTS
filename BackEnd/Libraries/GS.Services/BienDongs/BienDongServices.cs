//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.DanhMuc;
using GS.Services.KT;
using GS.Services.NghiepVu;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.BienDongs
{
    public partial class BienDongService : IBienDongService
    {
        #region Fields

        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<BienDong> _itemRepository;
        private readonly IRepository<DonVi> _donviRepository;
        private readonly IRepository<YeuCau> _yeuCauRepository;
        private readonly IRepository<TaiSan> _taiSanRepository;
        private readonly IRepository<NguoiDungDonViMapping> _nguoidungdonviMappingRepository;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IHaoMonTaiSanLogService _haoMonTaiSanLogService;
        private readonly ILyDoBienDongService _lyDoBienDongService;

        #endregion Fields

        #region Ctor

        public BienDongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NguoiDungDonViMapping> nguoidungdonviMappingRepository,
            IRepository<BienDong> itemRepository,
            IRepository<DonVi> donviRepository,
            IRepository<YeuCau> yeuCauRepository,
            IRepository<TaiSan> taiSanRepository,
            IYeuCauChiTietService yeuCauChiTietService,
            IBienDongChiTietService bienDongChiTietService,
            IHaoMonTaiSanLogService haoMonTaiSanLogService,
            ILyDoBienDongService lyDoBienDongService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donviRepository = donviRepository;
            this._nguoidungdonviMappingRepository = nguoidungdonviMappingRepository;
            this._yeuCauRepository = yeuCauRepository;
            this._taiSanRepository = taiSanRepository;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._haoMonTaiSanLogService = haoMonTaiSanLogService;
            this._lyDoBienDongService = lyDoBienDongService;
        }

        #endregion Ctor

        #region Methods

        public virtual IList<BienDong> GetAllBienDongs(decimal? donViId = null, decimal? donViChaId = null, decimal? trangThai = null, decimal? loaiBienDong = null, decimal? loaiHinhTaiSan = null, decimal? taiSanId = null, Decimal? nguonTaiSan = null)
        {
            var query = _itemRepository.Table;
            if (loaiBienDong > 0)
                query = query.Where(c => c.LOAI_BIEN_DONG_ID == loaiBienDong);
            if (trangThai > 0)
                query = query.Where(c => c.TRANG_THAI_ID == trangThai);
            if (nguonTaiSan > 0)
                query = query.Where(c => c.taisan.NGUON_TAI_SAN_ID == nguonTaiSan);
            if (donViChaId > 0)
            {
                DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
                List<decimal> donViIds = _donviRepository.Table.Where(c => c.TREE_NODE.StartsWith(donViCha.TREE_NODE)).Select(x => x.ID).ToList();
                if (!string.IsNullOrEmpty(donViCha.MA_BO))

                    query = query.Where(c => c.MA_BO == donViCha.MA_BO && donViIds.Contains(c.DON_VI_ID.GetValueOrDefault(0)));
                else
                    query = query.Where(c => donViIds.Contains(c.DON_VI_ID.GetValueOrDefault(0)));
            }
            else if (donViId > 0)
            {
                string mabo = _donviRepository.GetById(donViId.Value)?.MA_BO;
                if (!string.IsNullOrEmpty(mabo))
                    query = query.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViId);
                else
                    query = query.Where(c => c.DON_VI_ID == donViId);
            }
            if (loaiHinhTaiSan > 0)
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSan);
            if (taiSanId > 0)
                query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            return query.ToList();
        }

        public virtual IPagedList<BienDong> SearchBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaDonVi = null
                , IList<int> ListLoaiHinhTaiSan = null, IList<int> ListLoaiBienDong = null, DateTime? fromdate = null, DateTime? todate = null, IList<int> ListTrangThaiDongBo = null, decimal? nguonTaiSanId = null)
        {
            //var query = _itemRepository.Table;
            //if (!String.IsNullOrEmpty(MaDonVi) & !MaDonVi.Equals(enumDonViTongHop.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
            //    query = query.Where(c => c.TAI_SAN_MA.StartsWith(MaDonVi));
            //if (ListLoaiHinhTaiSan != null && ListLoaiHinhTaiSan.Count > 0)
            //    query = query.Where(c => ListLoaiHinhTaiSan.Contains(Convert.ToInt32(c.LOAI_HINH_TAI_SAN_ID)));
            //if (ListLoaiBienDong != null && ListLoaiBienDong.Count > 0)
            //    query = query.Where(c => ListLoaiBienDong.Contains(Convert.ToInt32(c.LOAI_BIEN_DONG_ID)));
            //if (!String.IsNullOrEmpty(Keysearch))
            //{
            //    Keysearch = Keysearch.ToUpper();
            //    query = query.Where(c => c.TAI_SAN_TEN.ToUpper().Contains(Keysearch) || c.TAI_SAN_MA.ToUpper().Contains(Keysearch));
            //}
            //if (fromdate.HasValue)
            //{
            //    var _tungay = fromdate.Value.Date;
            //    query = query.Where(x => x.NGAY_BIEN_DONG >= _tungay);
            //}
            //if (todate.HasValue && todate != DateTime.MinValue)
            //{
            //    var _denngay = todate.Value.Date.AddDays(1);
            //    query = query.Where(x => x.NGAY_BIEN_DONG < _denngay);
            //}
            //if (ListTrangThaiDongBo.Count == 1 && ListTrangThaiDongBo.Contains(1))
            //    query = query.Where(c => c.TRANG_THAI_DONG_BO == (int)enumDongBoBienDong.DA_DONG_BO);
            //if (ListTrangThaiDongBo.Count == 1 && ListTrangThaiDongBo.Contains(2))
            //    query = query.Where(c => c.TRANG_THAI_DONG_BO != (int)enumDongBoBienDong.DA_DONG_BO);

            //query = query.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET).OrderBy(c => c.TAI_SAN_MA).ThenBy(n => n.NGAY_BIEN_DONG);

            //return new PagedList<BienDong>(query, pageIndex, pageSize);

            List<string> param = new List<string>();
            if (!String.IsNullOrEmpty(MaDonVi) & !MaDonVi.Equals(enumDonViTongHop.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
                param.Add(String.Format(" BD.TAI_SAN_MA LIKE '{0}%'", MaDonVi));

            if (ListLoaiHinhTaiSan != null && ListLoaiHinhTaiSan.Count > 0)
                param.Add(String.Format(" BD.LOAI_HINH_TAI_SAN_ID IN ({0})", String.Join(",", ListLoaiHinhTaiSan)));

            if (ListLoaiBienDong != null && ListLoaiBienDong.Count > 0)
                param.Add(String.Format(" BD.LOAI_BIEN_DONG_ID IN ({0})", String.Join(",", ListLoaiBienDong)));

            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                param.Add(String.Format(" (UPPER(BD.TAI_sAN_TEN) LIKE '%{0}%' OR UPPER(BD.TAI_SAN_MA) LIKE '%{1}%')", Keysearch, Keysearch));
            }

            if (fromdate.HasValue)
            {
                var _tungay = fromdate.Value.Date;
                param.Add(String.Format(" BD.NGAY_BIEN_DONG >= TO_DATE({0},'dd/MM/yyyy')", _tungay.toDateVNString()));
            }
            if (todate.HasValue && todate != DateTime.MinValue)
            {
                var _denngay = todate.Value.Date.AddDays(1);
                param.Add(String.Format(" BD.NGAY_BIEN_DONG < TO_DATE({0},'dd/MM/yyyy')", _denngay.toDateVNString()));
            }
            if (ListTrangThaiDongBo.Count == 1 && ListTrangThaiDongBo.Contains(1))
                param.Add(String.Format(" BD.TRANG_THAI_DONG_BO = {0}", (int)enumDongBoBienDong.DA_DONG_BO));

            if (ListTrangThaiDongBo.Count == 1 && ListTrangThaiDongBo.Contains(2))
                param.Add(String.Format(" BD.TRANG_THAI_DONG_BO != {0}", (int)enumDongBoBienDong.DA_DONG_BO));

            string sql = "SELECT BD.* FROM BD_BIEN_DONG BD ";
            param.Add($" BD.TRANG_THAI_ID = {(int)enumTRANG_THAI_YEU_CAU.DA_DUYET}");
            if (nguonTaiSanId > 0)
            {
                sql += " LEFT JOIN TS_TAI_SAN TS ON TS.ID = BD.TAI_SAN_ID ";
                param.Add(String.Format(" TS.NGUON_TAI_SAN_ID = {0}", nguonTaiSanId));
            }

            sql += " WHERE " + String.Join(" AND ", param);
            sql += " ORDER BY BD.TAI_SAN_MA ASC, BD.NGAY_BIEN_DONG DESC";

            var query = _dbContext.EntityFromSqlNoParams<BienDong>(sql);

            return new PagedList<BienDong>(query, pageIndex, pageSize);
        }

        public virtual BienDong GetBienDongById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<BienDong> GetBienDongByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            //if (Ids != null && Ids.Count() > 0)
            //{
            //    var i = Ids.Select(c => new { key = 1, value = c }).ToList();
            //    return query.Where(c => i.Contains(new { key = 1, value = c.ID })).ToList();
            //}
            //return null;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<BienDong> GetBienDongMoiNhatByNgayBienDong(DateTime? ngayBienDong = null, int DonViId = 0, int DonViBoPhanId = 0)
        {
            if (ngayBienDong == null)
            {
                return new List<BienDong>();
            }
            //lấy hết biến động kp là lý do giảm toàn bộ;
            var listlydogiam = _lyDoBienDongService.GetLyDoBienDongByLoaiLyDoBienDong(enum_LOAI_LY_DO_BIEN_DONG.LY_DO_GIAM).Select(p => p.ID as decimal?);
            string mabo = _donviRepository.GetById(DonViId)?.MA_BO;
            var query = _itemRepository.Table.Where(c => c.MA_BO == mabo && c.DON_VI_ID == DonViId && c.NGAY_BIEN_DONG <= ngayBienDong);
            var listTSGTB = query.Where(c => listlydogiam.Contains(c.LY_DO_BIEN_DONG_ID)).Select(c => c.TAI_SAN_ID).ToList();
            if (listTSGTB.Count() > 0)
                query = query.Where(c => !listTSGTB.Contains(c.TAI_SAN_ID));
            if (DonViBoPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            }
            query = query.GroupBy(c => c.TAI_SAN_ID).Select(c => _itemRepository.Table.Where(n => n.NGAY_BIEN_DONG == c.Max(m => m.NGAY_BIEN_DONG) && n.TAI_SAN_ID == c.Key).FirstOrDefault());
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).ToList();
        }

        public virtual IList<BienDong> GetBienDongMoiNhatByNgayBienDongForKiemKe(DateTime? ngayBienDong = null, int DonViId = 0, int DonViBoPhanId = 0)
        {
            if (ngayBienDong == null)
            {
                return new List<BienDong>();
            }
            //lấy hết biến động kp là lý do giảm toàn bộ;
            var listlydogiam = new List<decimal> { (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO };
            string mabo = _donviRepository.GetById(DonViId)?.MA_BO;
            var query = _itemRepository.Table.Where(c => c.MA_BO == mabo && c.DON_VI_ID == DonViId && c.NGAY_BIEN_DONG <= ngayBienDong);
            //var listTSGTB = query.Where(c => listlydogiam.Contains((int)c.LOAI_BIEN_DONG_ID)).Select(c => c.TAI_SAN_ID).ToList();
            //if (listTSGTB.Count() > 0)
            //	query = query.Where(c => !listTSGTB.Contains(c.TAI_SAN_ID));
            var listTSGTB = query.Where(c => listlydogiam.Contains((int)c.LOAI_BIEN_DONG_ID)).Select(c => c.TAI_SAN_ID);
            if (listTSGTB.Count() > 0)
                query = from q in query
                        where !listTSGTB.Any(tsGiamId => tsGiamId == q.TAI_SAN_ID)
                        select q;

            if (DonViBoPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            }
            var bd_max = query.GroupBy(c => c.TAI_SAN_ID).Select(p => p.Max(c => c.ID));
            query = from q in query
                    join bd in bd_max
                    on q.ID equals bd
                    select q;

            //query = query.GroupBy(c => c.TAI_SAN_ID).Select(c => _itemRepository.Table.Where(n => n.NGAY_BIEN_DONG == c.Max(m => m.NGAY_BIEN_DONG) && n.TAI_SAN_ID == c.Key).FirstOrDefault());
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).ToList();
        }

        public virtual void InsertBienDong(BienDong entity, bool IsDongBo = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.ID = 0;
            DonVi donVi = _donviRepository.GetById(Convert.ToDecimal(entity.DON_VI_ID));
            if (donVi != null)
            {
                entity.MA_BO = donVi.MA.Substring(0, 3);
            }
            if (!IsDongBo)
            {
                entity.GUID = Guid.NewGuid();
            }
            entity.IS_BIENDONG_CUOI = true;
            //trước khi insert. cập nhật lại tất cả biến động cũ
            var list_oldBD = GetBienDongsByTaiSanId(entity.TAI_SAN_ID);
            if (list_oldBD != null && list_oldBD.Count > 0)
            {
                foreach (var oldBD in list_oldBD)
                {
                    oldBD.IS_BIENDONG_CUOI = false;
                    UpdateBienDong(oldBD);
                }
            }
            _itemRepository.Insert(entity);
            GhiLogHaoMonTaiSan(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateBienDong(BienDong entity, bool IsGhiLogHaoMon = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            if (IsGhiLogHaoMon)
            {
                GhiLogHaoMonTaiSan(entity);
            }

            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void UpdateBienDong(IList<BienDong> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
        }

        public virtual void UpdateNguoiTaoBienDongs(IList<BienDong> entities, decimal NguoiTaoId)
        {
            if (entities != null && entities.Count > 0)
            {
                foreach (var bienDong in entities)
                {
                    bienDong.NGUOI_TAO_ID = NguoiTaoId;
                }
                _itemRepository.Update(entities);
            }
        }

        public virtual void DeleteBienDong(BienDong entity = null, bool IsChotHaoMon = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //thay đổi is biến động cuối
            var bd_MoiNhat = GetBienDongMoiNhatByTaiSanId(entity.TAI_SAN_ID);
            if (bd_MoiNhat != null)
            {
                bd_MoiNhat.IS_BIENDONG_CUOI = true;
                UpdateBienDong(bd_MoiNhat);
                if (entity.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && IsChotHaoMon)
                {
                    _bienDongChiTietService.ChotHMKHTaiSan(bienDongId: bd_MoiNhat.ID, taiSanId: bd_MoiNhat.TAI_SAN_ID);
                }
            }
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public virtual void DeleteBienDongs(IList<BienDong> entities)
        {
            if (entities == null || (entities != null && entities.Count > 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Delete(entities);

            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public IList<BienDong> GetBienDongsByTaiSanId(decimal? taiSanId = 0, DateTime? NgayBDDen = null)
        {
            if (taiSanId <= 0)
                return new List<BienDong>();
            //if (taiSanId == 5240)
            //    taiSanId = 5240;
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            if (NgayBDDen.HasValue)
                query = query.Where(c => c.NGAY_BIEN_DONG < NgayBDDen.Value);
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).ToList();
        }

        public int CountBienDongsByTaiSanId(decimal? taiSanId = 0, DateTime? NgayBDDen = null)
        {
            if (taiSanId <= 0)
                return 0;
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            if (NgayBDDen.HasValue)
                query = query.Where(c => c.NGAY_BIEN_DONG < NgayBDDen.Value);
            return query.Count();
        }

        public BienDong GetBienDongCuNhatByTaiSanId(decimal taiSanId)
        {
            if (taiSanId > 0)
                return _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanId && c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
            else
                return null;
        }

        public BienDong GetBienDongMoiNhatByTaiSanId(decimal? taiSanId = 0, DateTime? ngayBienDong = null, int? LoaiBienDong = null)
        {
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            if (taiSanId > 0)
                query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            if (ngayBienDong != null)
            {
                query = query.Where(c => c.NGAY_BIEN_DONG < ngayBienDong);
            }
            if (LoaiBienDong.HasValue)
            {
                query = query.Where(p => p.LOAI_BIEN_DONG_ID == LoaiBienDong);
            }
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).FirstOrDefault();
        }

        public IList<BienDong> SearchListBienDongs(string keySearch = null, DateTime? fromdate = null, DateTime? todate = null, decimal? taiSanId = 0, decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null, decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0, decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0)
        {
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            if (!String.IsNullOrEmpty(keySearch))
            {
                query = query.Where(c => c.TAI_SAN_TEN.Contains(keySearch) || c.TAI_SAN_MA.Contains(keySearch));
            }
            if (loaiHinhTSId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
            }
            if (loaiTSId > 0)
            {
                query = query.Where(c => c.LOAI_TAI_SAN_ID == loaiTSId);
            }
            if (nguoiTaoId > 0)
            {
                query = query.Where(c => c.NGUOI_TAO_ID == nguoiTaoId);
            }
            if (lyDoBienDongId > 0)
            {
                query = query.Where(c => c.LY_DO_BIEN_DONG_ID == lyDoBienDongId);
            }
            if (boPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == boPhanId);
            }
            if (donViId > 0)
            {
                string mabo = _donviRepository.GetById(donViId)?.MA_BO;
                query = query.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViId);
            }
            if (loaiBienDongId > 0)
            {
                query = query.Where(c => c.LOAI_BIEN_DONG_ID == loaiBienDongId);
            }
            if (fromdate.HasValue)
            {
                var _tungay = fromdate.Value.Date;
                query = query.Where(x => x.NGAY_BIEN_DONG >= _tungay);
            }
            if (todate.HasValue && todate != DateTime.MinValue)
            {
                var _denngay = todate.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_BIEN_DONG < _denngay);
            }
            if (nguoiTaoId > 0)
            {
                var DonViTreeNodes = _nguoidungdonviMappingRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiTaoId)
                     .Select(c => c.donvi.TREE_NODE);
                var donvis = from dv in _donviRepository.Table
                             from tn in DonViTreeNodes
                             where EF.Functions.Like(dv.TREE_NODE, tn + "%")
                             select dv;
                query = query.Join(donvis, x => x.DON_VI_ID, y => y.ID, (x, y) => new { biendong = x, donvi = y }).Select(c => c.biendong);
            }
            query = query.OrderByDescending(c => c.NGAY_BIEN_DONG);
            return query.ToList();
        }

        public GiaTriTaiSan Tinh_GiaTriTaiSan(decimal TaiSanID, DateTime? To_Date_BienDong = null, bool isIncludeYC = true)
        {
            if (TaiSanID <= 0)
                return new GiaTriTaiSan();
            /// lấy danh sách biến động theo tài sản
            var list_BienDongCuaTaiSan = _itemRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanID).Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            /// lấy danh sách yêu cầu, trong đó:
            ///		Theo tài sản
            ///		Trạng thái != nháp (loại bỏ những biến động đã bị xóa)
            ///		Trạng thái là chờ duyệt hoặc (!đã duyệt mà là biến động tăng toàn bộ) để luôn hiển thị giá trị khi tài sản bị từ chối
            var list_YeuCauCuaTaiSan = _yeuCauRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanID &&
                                        p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP &&
                                        p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA &&
                                        (
                                            p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET ||
                                            (p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                                        ));
            if (To_Date_BienDong != null)
            {
                list_BienDongCuaTaiSan = list_BienDongCuaTaiSan.Where(c => c.NGAY_BIEN_DONG < To_Date_BienDong);
                list_YeuCauCuaTaiSan = list_YeuCauCuaTaiSan.Where(c => c.NGAY_BIEN_DONG < To_Date_BienDong);
            }
            var list_BienDongCTCuaTaiSan = _bienDongChiTietService.GetBienDongChiTietByBDIds(list_BienDongCuaTaiSan.Select(p => p.ID).ToList());
            var listYeuCauCTCuaTaiSan = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauIds(list_YeuCauCuaTaiSan.Select(p => p.ID).ToList());

            var res = new GiaTriTaiSan();
            if (isIncludeYC)
                res.NGUYEN_GIA_CU = list_BienDongCuaTaiSan.Sum(p => p.NGUYEN_GIA ?? 0) + list_YeuCauCuaTaiSan.Sum(p => p.NGUYEN_GIA ?? 0);
            else
                res.NGUYEN_GIA_CU = list_BienDongCuaTaiSan.Sum(p => p.NGUYEN_GIA ?? 0);

            decimal Nha_TongDT_BD = 0, Nha_TongDT_YC = 0, Nha_DT_BD = 0, Nha_DT_YC = 0, Dat_TongDT_BD = 0, Dat_TongDT_YC = 0, Dat_GiaTri_SuDung = 0;
            decimal VKT_TONG_CHIEU_DAI_BD = 0, VKT_TONG_DIEN_TICH_BD = 0, VKT_TONG_THE_TICH_BD = 0, VKT_TONG_CHIEU_DAI_YC = 0, VKT_TONG_DIEN_TICH_YC = 0, VKT_TONG_THE_TICH_YC = 0;
            if (list_BienDongCTCuaTaiSan != null)
            {
                Nha_TongDT_BD = list_BienDongCTCuaTaiSan.Sum(p => p.NHA_TONG_DIEN_TICH_XD ?? 0);
                Nha_DT_BD = list_BienDongCTCuaTaiSan.Sum(p => p.NHA_DIEN_TICH_XD ?? 0);

                Dat_TongDT_BD = list_BienDongCTCuaTaiSan.Sum(p => p.DAT_TONG_DIEN_TICH ?? 0);
                Dat_GiaTri_SuDung = list_BienDongCTCuaTaiSan.Sum(p => p.DAT_GIA_TRI_QUYEN_SD_DAT ?? 0);

                VKT_TONG_CHIEU_DAI_BD = list_BienDongCTCuaTaiSan.Sum(p => p.VKT_CHIEU_DAI.GetValueOrDefault());
                VKT_TONG_DIEN_TICH_BD = list_BienDongCTCuaTaiSan.Sum(p => p.VKT_DIEN_TICH.GetValueOrDefault());
                VKT_TONG_THE_TICH_BD = list_BienDongCTCuaTaiSan.Sum(p => p.VKT_THE_TICH.GetValueOrDefault());
            }
            if (listYeuCauCTCuaTaiSan != null && isIncludeYC)
            {
                Nha_TongDT_YC = listYeuCauCTCuaTaiSan.Sum(p => p.NHA_TONG_DIEN_TICH_XD ?? 0);
                Nha_DT_YC = listYeuCauCTCuaTaiSan.Sum(p => p.NHA_DIEN_TICH_XD ?? 0);
                Dat_TongDT_YC = listYeuCauCTCuaTaiSan.Sum(p => p.DAT_TONG_DIEN_TICH ?? 0);

                VKT_TONG_CHIEU_DAI_YC = listYeuCauCTCuaTaiSan.Sum(p => p.VKT_CHIEU_DAI.GetValueOrDefault());
                VKT_TONG_DIEN_TICH_YC = listYeuCauCTCuaTaiSan.Sum(p => p.VKT_DIEN_TICH.GetValueOrDefault());
                VKT_TONG_THE_TICH_YC = listYeuCauCTCuaTaiSan.Sum(p => p.VKT_THE_TICH.GetValueOrDefault());
            }

            res.NHA_DIEN_TICH_XD_CU = Nha_DT_BD + Nha_DT_YC;

            res.NHA_TONG_DIEN_TICH_XD_CU = Nha_TongDT_BD + Nha_TongDT_YC;

            res.DAT_TONG_DIEN_TICH_CU = Dat_TongDT_BD + Dat_TongDT_YC;
            res.DAT_GIA_TRI_QUYEN_SU_DUNG = Dat_GiaTri_SuDung;

            res.VKT_CHIEU_DAI_CU = VKT_TONG_CHIEU_DAI_BD + VKT_TONG_CHIEU_DAI_YC;

            res.VKT_DIEN_TICH_CU = VKT_TONG_DIEN_TICH_BD + VKT_TONG_DIEN_TICH_YC;

            res.VKT_THE_TICH_CU = VKT_TONG_THE_TICH_BD + VKT_TONG_THE_TICH_YC;
            var ChiTietBienDongCuoi = list_BienDongCTCuaTaiSan.LastOrDefault();
            res.HM_GIA_TRI_CON_LAI_CU = ChiTietBienDongCuoi != null ? ChiTietBienDongCuoi.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0) : 0;
            return res;
        }

        public BienDong GetBienDongCuoiByTaiSanId(decimal? taiSanId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_BIENDONG_CUOI == true);
            if (taiSanId > 0)
                query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).FirstOrDefault();
        }

        /// <summary>
        /// Tính tổng nguyên giá tài sản
        /// </summary>
        /// <param name="listBienDongTS"></param>
        /// <param name="listYeuCauTS"></param>
        /// <param name="taiSanId"></param>
        /// <param name="To_Date_BienDong">Ngày tính nguyên giá tài sản đến</param>
        /// <returns></returns>
        public Decimal TinhNguyenGiaTaiSan(IList<BienDong> listBienDongTS = null, IList<YeuCau> listYeuCauTS = null, Decimal? taiSanId = 0, DateTime? To_Date_BienDong = null, bool isEqualDate = true)
        {
            if (taiSanId > 0 && listBienDongTS == null && listYeuCauTS == null)
            {
                listBienDongTS = _itemRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).ToList();
                listYeuCauTS = _yeuCauRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && (p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET || (p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO))).ToList();
            }
            if (listBienDongTS == null)
                listBienDongTS = _itemRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId).ToList();
            if (listYeuCauTS == null)
                listYeuCauTS = _yeuCauRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && (p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET || (p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO))).ToList();
            if (To_Date_BienDong.HasValue)
            {
                if (isEqualDate)
                {
                    listBienDongTS = listBienDongTS.Where(c => c.NGAY_BIEN_DONG <= To_Date_BienDong).ToList();
                    listYeuCauTS = listYeuCauTS.Where(c => c.NGAY_BIEN_DONG <= To_Date_BienDong).ToList();
                }
                else
                {
                    listBienDongTS = listBienDongTS.Where(c => c.NGAY_BIEN_DONG < To_Date_BienDong).ToList();
                    listYeuCauTS = listYeuCauTS.Where(c => c.NGAY_BIEN_DONG < To_Date_BienDong).ToList();
                }
            }
            var nguyenGia = listBienDongTS.Sum(p => p.NGUYEN_GIA ?? 0) + listYeuCauTS.Sum(p => p.NGUYEN_GIA ?? 0);
            return nguyenGia;
        }

        /// <summary>
        /// lấy tổng nguyên giá của các biến động trước ngày biến động
        /// </summary>
        /// <param name="taiSanId"></param>
        /// <param name="To_Date_BienDong"></param>
        /// <returns></returns>
        public decimal TinhNguyenGiaTaiSanOnlyBD(Decimal taiSanId, DateTime? To_Date_BienDong = null, bool IncludeYeuCau = true)
        {
            if (taiSanId <= 0)
                return 0;
            var listBienDongTS = _itemRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            if (To_Date_BienDong != null)
            {
                listBienDongTS = listBienDongTS.Where(p => p.NGAY_BIEN_DONG < To_Date_BienDong);
            }
            //nếu tồn tại biến động
            if (listBienDongTS != null && listBienDongTS.Count() > 0)
                return listBienDongTS.Sum(p => p.NGUYEN_GIA ?? 0);
            if (IncludeYeuCau)
            {
                //nếu không có biến động thì lấy nguyên giá yêu cầu tăng mới
                var yc = _yeuCauRepository.Table.Where(p => p.TAI_SAN_ID == taiSanId && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).FirstOrDefault();
                if (yc != null)
                    return yc.NGUYEN_GIA.Value;
            }

            return 0;
        }

        public virtual BienDong GetBienDongByGuid(Guid? Guid)
        {
            if (Guid == null)
                return null;
            var query = _itemRepository.Table;
            return query.Where(m => m.GUID == Guid).FirstOrDefault();
        }

        public virtual void DB_InsertBienDong(BienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.ID = 0;
            //entity.IS_BIENDONG_CUOI = true;
            if (entity.GUID == new Guid())
            {
                entity.GUID = Guid.NewGuid();
            }
            if (entity.IS_BIENDONG_CUOI == null)
            {
                entity.IS_BIENDONG_CUOI = true;
                //trước khi insert.cập nhật lại tất cả biến động cũ
                var list_oldBD = GetBienDongsByTaiSanId(entity.TAI_SAN_ID);
                if (list_oldBD != null && list_oldBD.Count > 0)
                {
                    foreach (var oldBD in list_oldBD)
                    {
                        oldBD.IS_BIENDONG_CUOI = false;
                        UpdateBienDong(oldBD);
                    }
                }
            }
            _itemRepository.Insert(entity);
        }

        public virtual void DB_UpdateBienDong(BienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //entity.IS_BIENDONG_CUOI = true;
            if (entity.GUID == new Guid())
            {
                entity.GUID = Guid.NewGuid();
            }
            entity.IS_BIENDONG_CUOI = true;
            //trước khi insert.cập nhật lại tất cả biến động cũ
            var list_oldBD = GetBienDongsByTaiSanId(entity.TAI_SAN_ID);
            if (list_oldBD != null && list_oldBD.Count > 1)
            {
                foreach (var oldBD in list_oldBD)
                {
                    oldBD.IS_BIENDONG_CUOI = false;
                    UpdateBienDong(oldBD);
                }
            }
            UpdateBienDong(entity);
        }

        public virtual decimal CountBDSau(decimal TaiSanId, DateTime NgayBD)
        {
            var query = _itemRepository.Table.Where(p => p.TAI_SAN_ID == TaiSanId);
            query = query.Where(p => p.NGAY_BIEN_DONG > NgayBD && p.TRANG_THAI_ID == (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET);
            return query.Count();
        }

        public virtual BienDong GetBienDongByID_DB(string ID_DB)
        {
            if (string.IsNullOrEmpty(ID_DB))
                return null;
            var query = _itemRepository.Table;
            return query.Where(m => m.ID_DB == ID_DB).FirstOrDefault();
        }

        public virtual decimal CountBDByLyDoOfTS(decimal p_idTaiSan, enumLOAI_LY_DO_TANG_GIAM p_loaiBD)
        {
            var res = _itemRepository.Table.Where(p => p.TAI_SAN_ID == p_idTaiSan &&
            p.LOAI_BIEN_DONG_ID == (int)p_loaiBD &&
            p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).Count();
            return res;
        }

        /// <summary>
        /// Ghi log hao mòn
        ///
        /// </summary>
        /// <param name="bienDong"></param>
        public virtual void GhiLogHaoMonTaiSan(BienDong bienDong)
        {
            var hmTSLog = _haoMonTaiSanLogService.GetHaoMonTaiSanLogByTaiSanId(bienDong.TAI_SAN_ID);
            if (hmTSLog != null)
            {
                if (bienDong.NGAY_BIEN_DONG.Value.Year < hmTSLog.NAM_TINH)
                {
                    hmTSLog.NAM_TINH = bienDong.NGAY_BIEN_DONG.Value.Year;
                    hmTSLog.NGAY_TINH = bienDong.NGAY_BIEN_DONG;
                    _haoMonTaiSanLogService.UpdateHaoMonTaiSanLog(hmTSLog);
                }
            }
            else // chưa tồn tại thì thêm mới
            {
                // Ghi log thông tin tính giá trị chốt tài sản
                var haoMonTaiSanLog = new HaoMonTaiSanLog()
                {
                    TAI_SAN_ID = bienDong.TAI_SAN_ID,
                    NGAY_TINH = bienDong.NGAY_BIEN_DONG,
                    NAM_TINH = bienDong.NGAY_BIEN_DONG.Value.Year
                };
                _haoMonTaiSanLogService.InsertHaoMonTaiSanLog(haoMonTaiSanLog);
            }
        }

        public virtual void updateBienDongCuoi(decimal? tai_san_id)
        {
            if (tai_san_id > 0)
            {
                var bds = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && c.TAI_SAN_ID == tai_san_id).OrderByDescending(c => c.NGAY_DUYET).ToList();
                if (bds != null && bds.Count() > 0)
                {
                    var bdc = bds.FirstOrDefault();
                    if (bdc != null)
                    {
                        bds = bds.Where(c => c.IS_BIENDONG_CUOI == true).ToList();
                        if (bds != null && bds.Count() > 0)
                            foreach (var itm in bds)
                            {
                                itm.IS_BIENDONG_CUOI = false;
                                UpdateBienDong(itm);
                            }
                        bdc.IS_BIENDONG_CUOI = true;
                        UpdateBienDong(bdc);
                    }
                }
            }
        }

        /// <summary>
        /// lấy biến động cho kiểm kê tài sản
        /// </summary>
        /// <param name="taiSanId"></param>
        /// <param name="NgayBDDen"></param>
        /// <param name="NgayBDTu"></param>
        /// <returns></returns>
        public IList<BienDong> GetBienDongsByTaiSanIdForKK(decimal? taiSanId = 0, DateTime? NgayBDDen = null, DateTime? NgayBDTu = null)
        {
            if (taiSanId <= 0)
                return new List<BienDong>();
            //if (taiSanId == 5240)
            //    taiSanId = 5240;
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA);
            query = query.Where(c => c.TAI_SAN_ID == taiSanId);
            if (NgayBDDen.HasValue)
                query = query.Where(c => c.NGAY_BIEN_DONG <= NgayBDDen.Value);
            if (NgayBDTu.HasValue)
                query = query.Where(c => c.NGAY_BIEN_DONG >= NgayBDTu.Value);
            return query.OrderByDescending(c => c.NGAY_BIEN_DONG).ToList();
        }

        //public decimal getGiaTriConLai(decimal taiSanID)
        //{
        //    TaiSan taiSan = _taiSanRepository.GetById(taiSanID);
        //    if (taiSan == null)
        //        return 0;

        //    if (taiSan.TRANG_THAI_ID == (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET || taiSan.TRANG_THAI_ID == (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
        //    {
        //        BienDong bienDong = _itemRepository.Table.Where(c => c.TAI_SAN_ID == taiSanID && c.IS_BIENDONG_CUOI == true).FirstOrDefault();
        //        if (bienDong == null)
        //            return 0;
        //        BienDongChiTiet bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
        //        if (bienDongChiTiet == null)
        //            return 0;
        //        return bienDongChiTiet.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0);
        //    }
        //    else
        //    {
        //        YeuCau yeuCau = _yeuCauRepository.Table.Where(c => c.TAI_SAN_ID == taiSanID && c.IS_BIENDONG_CUOI == true).FirstOrDefault();
        //        if (yeuCau == null)
        //            return 0;
        //        YeuCauChiTiet yeuCauChiTiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
        //        if (yeuCauChiTiet == null)
        //            return 0;
        //        return yeuCauChiTiet.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0);
        //    }

        //    return 0;
        //}
        public virtual List<BienDong> GetAllBienDongTangMoiCanDongBo(decimal? donViChaId = null, int TakeNumber = 100, Decimal? nguonTaiSan = null)
        {
            //var query = _itemRepository.Table.Where(x =>
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_CHAY_JOB &&
            //                                            (x.taisan.TRANG_THAI_ID == (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET ||
            //                                            x.taisan.TRANG_THAI_ID == (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO) &&
            //                                            x.taisan.MA_DB == null &&
            //                                            (x.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
            //                                            x.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY));
            //if (nguonTaiSan != null)
            //    query = query.Where(c => c.taisan.NGUON_TAI_SAN_ID == nguonTaiSan);
            //if (donViChaId > 0)
            //{
            //    DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
            //    //List<decimal> donViIds = _donviRepository.Table.Where(c => c.TREE_NODE.StartsWith(donViCha.TREE_NODE)).Select(x => x.ID).ToList();
            //    query = query.Where(c => c.donvi.TREE_NODE.StartsWith(donViCha.TREE_NODE));
            //}
            //return query.OrderBy(x => x.LOAI_HINH_TAI_SAN_ID).Take(TakeNumber).ToList();

            string maDonVi = null;
            if (donViChaId > 0)
            {
                DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
                maDonVi = donViCha?.MA;
            }
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_ID = new OracleParameter("P_NGUON_TAI_SAN_ID", OracleDbType.Decimal, nguonTaiSan, ParameterDirection.Input);
            OracleParameter P_TAKE_NUMBER = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, TakeNumber, ParameterDirection.Input);
            OracleParameter P_LOAI_BIEN_DONG_ID = new OracleParameter("P_LOAI_BIEN_DONG_ID", OracleDbType.Decimal, 1, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore1<BienDong>("PKG_DONG_BO.GET_ALL_BIEN_DONG_CAN_DONG_BO", P_MA_DON_VI, P_NGUON_TAI_SAN_ID, P_TAKE_NUMBER, P_LOAI_BIEN_DONG_ID, TBL_OUT);
                //var result = _dbContext.EntityFromStore<DongBoApi_BienDongTaiSan>("GET_INFO_DONG_BO_BIEN_DONG", P_BIEN_DONG_ID, TBL_OUT).ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual List<BienDong> GetAllBienDongTangMoi(decimal? donViChaId = null, int TakeNumber = 100, Decimal? nguonTaiSan = null)
        {
            //var query = _itemRepository.Table.Where(x => x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_CHAY_JOB &&
            //                                            x.taisan.TRANG_THAI_ID != (decimal)enumTRANG_THAI_TAI_SAN.XOA &&
            //                                            x.taisan.MA_DB == null &&
            //                                            (x.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
            //                                            x.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY));

            //if (donViChaId > 0)
            //{
            //    DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
            //    //List<decimal> donViIds = _donviRepository.Table.Where(c => c.TREE_NODE.StartsWith(donViCha.TREE_NODE)).Select(x => x.ID).ToList();
            //    query = query.Where(c => c.donvi.TREE_NODE.StartsWith(donViCha.TREE_NODE));
            //}
            //if (nguonTaiSan != null)
            //{
            //    var listNguon = (new enumNguonTaiSan()).ToSelectList().Where(x => x.Value != nguonTaiSan.ToString()).Select(c => Convert.ToDecimal(c.Value)).ToList();
            //    query = query.Where(c => !listNguon.Contains(c.taisan.NGUON_TAI_SAN_ID));

            //}

            //return query.OrderBy(x => x.LOAI_HINH_TAI_SAN_ID).Take(TakeNumber).ToList();

            string maDonVi = null;
            if (donViChaId > 0)
            {
                DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
                maDonVi = donViCha?.MA;
            }
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_ID = new OracleParameter("P_NGUON_TAI_SAN_ID", OracleDbType.Decimal, nguonTaiSan, ParameterDirection.Input);
            OracleParameter P_TAKE_NUMBER = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, TakeNumber, ParameterDirection.Input);
            OracleParameter P_LOAI_BIEN_DONG_ID = new OracleParameter("P_LOAI_BIEN_DONG_ID", OracleDbType.Decimal, 1, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore1<BienDong>("PKG_DONG_BO.GET_ALL_BIEN_DONG_CAN_DONG_BO", P_MA_DON_VI, P_NGUON_TAI_SAN_ID, P_TAKE_NUMBER, P_LOAI_BIEN_DONG_ID, TBL_OUT);
                //var result = _dbContext.EntityFromStore<DongBoApi_BienDongTaiSan>("GET_INFO_DONG_BO_BIEN_DONG", P_BIEN_DONG_ID, TBL_OUT).ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual List<BienDong> GetAllBienDongKhacTangMoiCanDongBo(decimal? donViChaId = null, int TakeNumber = 100, Decimal? nguonTaiSan = null, Decimal? loaiBienDongId = null)
        {
            //var query = _itemRepository.Table.Where(x =>
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DA_DONG_BO &&
            //                                            x.TRANG_THAI_DONG_BO != (decimal)enumDongBoBienDong.DANG_CHAY_JOB &&
            //                                            x.taisan.TRANG_THAI_ID != (decimal)enumTRANG_THAI_TAI_SAN.XOA &&
            //                                            x.taisan.MA_DB != null &&
            //                                            x.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
            //                                            x.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY);
            //if (nguonTaiSan != null)
            //{
            //    var listNguon = (new enumNguonTaiSan()).ToSelectList().Where(x => x.Value != nguonTaiSan.ToString()).Select(c => Convert.ToDecimal(c.Value)).ToList();
            //    query = query.Where(c => !listNguon.Contains(c.taisan.NGUON_TAI_SAN_ID));
            //    //query = query.Where(c => c.taisan.NGUON_TAI_SAN_ID == nguonTaiSan);
            //}

            //if (loaiBienDongId > 0)
            //    query = query.Where(c => c.LOAI_BIEN_DONG_ID == loaiBienDongId);
            //if (donViChaId > 0)
            //{
            //    DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
            //    //List<decimal> donViIds = _donviRepository.Table.Where(c => c.TREE_NODE.StartsWith(donViCha.TREE_NODE)).Select(x => x.ID).ToList();
            //    query = query.Where(c => c.donvi.TREE_NODE.StartsWith(donViCha.TREE_NODE));
            //}
            //return query.OrderBy(x => x.LOAI_HINH_TAI_SAN_ID).Take(TakeNumber).ToList();
            string maDonVi = null;
            if (donViChaId > 0)
            {
                DonVi donViCha = _donviRepository.GetById(donViChaId.Value);
                maDonVi = donViCha?.MA;
            }
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_ID = new OracleParameter("P_NGUON_TAI_SAN_ID", OracleDbType.Decimal, nguonTaiSan, ParameterDirection.Input);
            OracleParameter P_TAKE_NUMBER = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, TakeNumber, ParameterDirection.Input);
            OracleParameter P_LOAI_BIEN_DONG_ID = new OracleParameter("P_LOAI_BIEN_DONG_ID", OracleDbType.Decimal, loaiBienDongId, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore1<BienDong>("PKG_DONG_BO.GET_ALL_BIEN_DONG_CAN_DONG_BO", P_MA_DON_VI, P_NGUON_TAI_SAN_ID, P_TAKE_NUMBER, P_LOAI_BIEN_DONG_ID, TBL_OUT);
                //var result = _dbContext.EntityFromStore<DongBoApi_BienDongTaiSan>("GET_INFO_DONG_BO_BIEN_DONG", P_BIEN_DONG_ID, TBL_OUT).ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Methods

        #region Calculate Function

        public virtual GiaTriTaiSan ProcTinhGiaTriTaiSan(Decimal? taiSanId = 0, DateTime? ngayBienDong = null, Decimal? bienDongId = null)
        {
            //var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

            OracleParameter p1 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Decimal, taiSanId, ParameterDirection.Input);
            OracleParameter pBIEN_DONG_ID = new OracleParameter("pBIEN_DONG_ID", OracleDbType.Decimal, bienDongId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BIEN_DONG", OracleDbType.Date, ngayBienDong, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<GiaTriTaiSan>("PKG_CALCULATE_VAL.PROC_TINH_GIA_TRI_TAI_SAN", p1, pBIEN_DONG_ID, p2, p3).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual GiaTriNguonVon ProcTinhGiaTriNguonVon(Decimal? taiSanId = 0, DateTime? ngayBienDong = null)
        {
            //var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

            OracleParameter p1 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Int32, taiSanId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BIEN_DONG", OracleDbType.Date, ngayBienDong, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<GiaTriNguonVon>("PKG_CALCULATE_VAL.PROC_TINH_NGUON_VON", p1, p2, p3).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual GiaTriNguyenGia ProcTinhNguyenGia(Decimal? taiSanId = 0, DateTime? ngayBienDong = null)
        {
            //var stringLoaiTaiSan = ListLoaiTaiSanId.Count() > 0 ? String.Join(",", ListLoaiTaiSanId) : null;

            OracleParameter p1 = new OracleParameter("pTAI_SAN_ID", OracleDbType.Decimal, taiSanId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pNGAY_BIEN_DONG", OracleDbType.Date, ngayBienDong, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<GiaTriNguyenGia>("PKG_CALCULATE_VAL.PROC_TINH_NGUYEN_GIA", p1, p2, p3).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Calculate Function

        public virtual DongBoApi_BienDongTaiSan GET_INFO_BIEN_DONG_BY_ID(decimal BienDongId)
        {
            OracleParameter P_BIEN_DONG_ID = new OracleParameter("P_BIEN_DONG_ID", OracleDbType.Decimal, BienDongId, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<DongBoApi_BienDongTaiSan>("PKG_DONG_BO.GET_INFO_DONG_BO_BIEN_DONG", P_BIEN_DONG_ID, TBL_OUT);
                //var result = _dbContext.EntityFromStore<DongBoApi_BienDongTaiSan>("GET_INFO_DONG_BO_BIEN_DONG", P_BIEN_DONG_ID, TBL_OUT).ToList();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual void UPDATE_TRANG_THAI_DONG_BO_BIEN_DONG(List<BienDong> bienDongs, decimal trangThai)
        {
            String sql = $"UPDATE BD_BIEN_DONG SET TRANG_THAI_DONG_BO = {trangThai} WHERE ";
            List<string> lstIds = bienDongs.Select(x => String.Format("(1,{0})", x.ID)).ToList();
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

        public virtual IList<BienDong> GetAllBienDongsByTrangThaiDongBo(string maDonVi = null, decimal nguonTaiSan = 0, decimal? trangThaiDongBoId = null, decimal? loaiBienDongId = null)
        {
            var query = _itemRepository.Table.Where(x => x.taisan.NGUON_TAI_SAN_ID == nguonTaiSan && x.TRANG_THAI_ID != 6);
            if (!String.IsNullOrEmpty(maDonVi))
                query = query.Where(c => c.taisan.MA.StartsWith(maDonVi));
            if (loaiBienDongId.HasValue)
                query = query.Where(c => c.LOAI_BIEN_DONG_ID == loaiBienDongId);
            if (trangThaiDongBoId.HasValue)
                query = query.Where(c => c.TRANG_THAI_DONG_BO == trangThaiDongBoId);
            return query.ToList();
        }
    }
}