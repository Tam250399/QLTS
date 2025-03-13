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
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.DanhMuc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.NghiepVu
{
    public partial class YeuCauService : IYeuCauService
    {
        #region Fields

        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<YeuCau> _itemRepository;
        private readonly IRepository<BienDong> _bienDongRepository;
        private readonly IRepository<DonVi> _donviRepository;
        private readonly IRepository<NguoiDung> _nguoidungRepository;
        private readonly IRepository<TaiSan> _taiSanRepository;
        private readonly IRepository<NguoiDungDonViMapping> _nguoidungdonviMappingRepository;
        private readonly ILyDoBienDongService _lyDoBienDongService;

        #endregion Fields

        #region Ctor

        public YeuCauService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<YeuCau> itemRepository,
            IWorkContext workContext,
            IRepository<NguoiDungDonViMapping> nguoidungdonviMappingRepository,
            IRepository<DonVi> donviRepository,
            IRepository<NguoiDung> nguoidungRepository,
            IRepository<TaiSan> taiSanRepository,
            IRepository<BienDong> bienDongRepository,
            ILyDoBienDongService lyDoBienDongService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._nguoidungdonviMappingRepository = nguoidungdonviMappingRepository;
            this._donviRepository = donviRepository;
            this._nguoidungRepository = nguoidungRepository;
            this._bienDongRepository = bienDongRepository;
            this._taiSanRepository = taiSanRepository;
            this._lyDoBienDongService = lyDoBienDongService;
        }

        #endregion Ctor
        #region Methods

        public virtual IList<YeuCau> GetYeuCaus(decimal? TaiSanId = 0, decimal? TrangThaiId = 0)
        {
            var query = _itemRepository.Table;
            if (TaiSanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == TaiSanId);
            }
            if (TrangThaiId > 0)
            {
                query = query.Where(c => c.TRANG_THAI_ID == TrangThaiId);
            }
            return query.ToList();
        }

        public int CountYeuCauCuaTaiSan(decimal taisanId, List<decimal> statusIds = null, List<decimal> excludeLoaiBD = null, DateTime? NgayBienDong = null)
        {
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == taisanId && c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP);
            if (statusIds != null && statusIds.Count > 0)
            {
                query = query.Where(c => statusIds.Contains(c.TRANG_THAI_ID));
            }
            if (excludeLoaiBD != null && excludeLoaiBD.Count > 0)
            {
                query = query.Where(p => !excludeLoaiBD.Contains(p.LOAI_BIEN_DONG_ID ?? 0));
            }
            if (NgayBienDong.HasValue)
            {
                query = query.Where(p => p.NGAY_BIEN_DONG < NgayBienDong.Value);
            }
            return query.Count();
        }

        public virtual IPagedList<YeuCau> SearchYeuCaus(int pageIndex = 0, int pageSize = int.MaxValue,
                                        string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
                                        decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
                                        decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
                                         decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
                                         decimal? taisanId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP);
            if (trangThaiId > 0)
            {
                query = query.Where(c => c.TRANG_THAI_ID == trangThaiId);
            }
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
                query = query.Where(c => c.DON_VI_ID == donViId);
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
                query = query.Join(donvis, x => x.DON_VI_ID, y => y.ID, (x, y) => new { yecau = x, donvi = y }).Select(c => c.yecau);
            }
            if (taisanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == taisanId);
            }
            query = query.OrderBy(c => c.NGAY_BIEN_DONG);
            return new PagedList<YeuCau>(query, pageIndex, pageSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">tài sản id</param>
        /// <returns></returns>
        public virtual YeuCau GetYeuCauCuNhatByTSId(decimal id)
        {
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == id && c.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA).OrderBy(c => c.NGAY_BIEN_DONG).FirstOrDefault();
        }

        public virtual YeuCau GetYeuCauById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual YeuCau GetYeuCau(decimal taiSanid, DateTime ngayBiendong, decimal loaiBienDongId)
        {
            var query = _itemRepository.Table.Where(x => x.TAI_SAN_ID == taiSanid && x.NGAY_BIEN_DONG == ngayBiendong && x.LOAI_BIEN_DONG_ID == loaiBienDongId);
            return query.FirstOrDefault();
        }
        public virtual YeuCau GetYeuCauByGuid(Guid guid)
        {
            return _itemRepository.Table.Where(c => c.GUID == guid).FirstOrDefault();
        }

        public virtual int CountYeuCauTrungNgayBD(decimal taisanId, DateTime? NgayBienDong, decimal excludeIDYC = 0)
        {
            if (taisanId <= 0 || NgayBienDong == null)
                return 0;

            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.TAI_SAN_ID == taisanId && p.NGAY_BIEN_DONG == NgayBienDong);
            var query_bd = _bienDongRepository.Table.Where(p => p.TAI_SAN_ID == taisanId && p.NGAY_BIEN_DONG == NgayBienDong);
            if (excludeIDYC > 0)
                query = query.Where(p => p.ID != excludeIDYC);
            return query.Count() + query_bd.Count();
        }
        public virtual int CountYeuCauLonHonNgayBD(decimal taisanId, DateTime? Ngaynhap, decimal excludeIDYC = 0)
        {
            if (taisanId <= 0 || Ngaynhap == null)
                return 0;
            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.TAI_SAN_ID == taisanId && p.NGAY_BIEN_DONG < Ngaynhap);
            if (excludeIDYC > 0)
                query = query.Where(p => p.ID != excludeIDYC);
            return query.Count();
        }
        public virtual YeuCau GetYeuCauMoiNhatByTaiSanId(decimal TaiSanId, enumTRANG_THAI_YEU_CAU TrangThai = enumTRANG_THAI_YEU_CAU.TAT_CA, List<decimal> exceptTrangThais = null, DateTime? NgayBD = null)
        {
            if (TaiSanId == 0)
                return null;
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId);
            if (exceptTrangThais != null && exceptTrangThais.Count > 0)
                query = query.Where(p => !exceptTrangThais.Contains(p.TRANG_THAI_ID));
            if (NgayBD.HasValue)
                query = query.Where(p => p.NGAY_BIEN_DONG < NgayBD);
            if (TrangThai != enumTRANG_THAI_YEU_CAU.TAT_CA)
            {
                query = query.Where(p => p.TRANG_THAI_ID == (int)TrangThai);
            }
            return query.OrderByDescending(p => p.NGAY_BIEN_DONG).FirstOrDefault();
        }

        public virtual IList<YeuCau> GetYeuCauByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertYeuCau(YeuCau entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.DON_VI_ID == null)
                entity.DON_VI_ID = _workContext.CurrentDonVi.ID;
            //entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            entity.NGAY_TAO = DateTime.Now;
            entity.GUID = Guid.NewGuid();

            _itemRepository.Insert(entity);
            if (entity.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                var taiSanEntity = _taiSanRepository.Table.Where(c => c.ID == entity.TAI_SAN_ID).FirstOrDefault();
                taiSanEntity.NGUYEN_GIA_BAN_DAU = entity.NGUYEN_GIA;
                _taiSanRepository.Update(taiSanEntity);
            }
            var lst_yc_ts = GetYeuCaus(entity.TAI_SAN_ID);
            foreach (var item in lst_yc_ts)
            {
                item.NGUOI_TAO_ID = entity.NGUOI_TAO_ID;
            }
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateYeuCau(YeuCau entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            if (entity.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                var taiSanEntity = _taiSanRepository.Table.Where(c => c.ID == entity.TAI_SAN_ID).FirstOrDefault();
                taiSanEntity.NGUYEN_GIA_BAN_DAU = entity.NGUYEN_GIA;
                _taiSanRepository.Update(taiSanEntity);
            }
            var lst_yc_ts = GetYeuCaus(entity.TAI_SAN_ID);
            foreach (var item in lst_yc_ts)
            {
                item.NGUOI_TAO_ID = entity.NGUOI_TAO_ID;
            }
            _itemRepository.Update(lst_yc_ts);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void UpdateNguoiTaoYeuCaus(IList<YeuCau> entities, decimal NguoiTaoId)
        {
            if (entities != null && entities.Count > 0)
            {
                foreach (var yeuCau in entities)
                {
                    yeuCau.NGUOI_TAO_ID = NguoiTaoId;
                }
                _itemRepository.Update(entities);
            }
        }
        public virtual void DeleteYeuCau(YeuCau entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public YeuCau GetYeuCauDieuChuyenKem(decimal TaiSanId)
        {
            var idLyDoDieuChuyen = _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
            var query = _itemRepository.Table.Where(c => c.LY_DO_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO
            && c.LOAI_BIEN_DONG_ID == idLyDoDieuChuyen
            && c.TAI_SAN_ID == TaiSanId);
            return query.FirstOrDefault();
        }

        public int CountYeuCau(decimal TaiSanId = 0, decimal? TrangThaiId = 0, decimal? exceptTrangThaiId = 0)
        {
            var query = _itemRepository.Table;
            if (TaiSanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == TaiSanId);
            }
            if (TrangThaiId > 0)
            {
                query = query.Where(c => c.TRANG_THAI_ID == TrangThaiId);
            }
            if (exceptTrangThaiId > 0)
            {
                query = query.Where(c => c.TRANG_THAI_ID != exceptTrangThaiId);
            }
            return query.Count();
        }

        public bool IsYCNeedToEdit(Guid guid)
        {
            YeuCau yeuCau = GetYeuCauByGuid(guid);
            if (yeuCau == null)
                return false;
            //nếu yêu cầu không phải yêu cầu trạng thái từ chối thì không cần check
            if (yeuCau.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.TU_CHOI)
                return true;
            YeuCau yeuCauNeedToEdit = _itemRepository
                .Table
                .Where(yc => yc.TAI_SAN_ID == yeuCau.TAI_SAN_ID && yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI)
                .OrderBy(p => p.NGAY_BIEN_DONG)
                .FirstOrDefault();
            return yeuCauNeedToEdit.ID == yeuCau.ID;
        }

        public YeuCau GetYCNeedToEdit(decimal p_taiSanId)
        {
            YeuCau yeuCauNeedToEdit = _itemRepository
                .Table
                .Where(yc => yc.TAI_SAN_ID == p_taiSanId && yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI)
                .OrderBy(p => p.NGAY_BIEN_DONG)
                .FirstOrDefault();
            return yeuCauNeedToEdit;
        }

        public string GetStringTuChoi(decimal p_taiSanId)
        {
            var q = _itemRepository.Table.Where(p => p.TAI_SAN_ID == p_taiSanId && p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI).Select(p => p.LY_DO_KHONG_DUYET).ToList();
            return string.Join(", ", q);
        }
        public virtual YeuCau GetYeuCauByDB_Guid(Guid guid)
        {
            return _itemRepository.Table.Where(c => c.GUID == guid).FirstOrDefault();
        }

        #endregion Methods
    }
}