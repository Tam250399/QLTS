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
using GS.Core.Domain.DB;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Services.TaiSans
{
    public partial class TaiSanService : ITaiSanService
    {
        #region Fields

        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSan> _itemRepository;
        private readonly IRepository<DonVi> _donviRepository;
        private readonly IRepository<YeuCau> _yeucauRepository;
        private readonly IRepository<BienDong> _biendongRepository;
        private readonly IRepository<HaoMonTaiSan> _haoMonTaiSanRepository;
        private readonly IRepository<YeuCauChiTiet> _yeuCauChiTietRepository;
        private readonly IWorkContext _workContext;
        private readonly IYeuCauService _yeuCauService;
        private readonly IDonViService _donViService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IRepository<KhaiThacTaiSan> _khaiThacTaiSanMappingRepository;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IRepository<KhaiThacChiTiet> _khaiThacChiTietRepository;
        private readonly IRepository<LyDoBienDong> _lyDoBienDongRepository;
        private readonly IRepository<KhaiThac> _khaiThacRepository;
        private readonly IRepository<LoaiTaiSan> _loaiTaiSanRepository;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;

        #endregion Fields

        #region Ctor

        public TaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSan> itemRepository,
            IWorkContext workContext,
            IRepository<YeuCau> yeucauRepository,
            IRepository<DonVi> donviRepository,
            IRepository<BienDong> biendongRepository,
            IRepository<HaoMonTaiSan> _haoMonTaiSanRepository,
            IYeuCauService yeuCauService,
            IDonViService donViService,
            IDonViBoPhanService donViBoPhanService,
            ICauHinhService cauHinhService,
            IRepository<YeuCauChiTiet> yeuCauChiTietRepository,
            IRepository<KhaiThacTaiSan> khaiThacTaiSanMappingRepository,
            ITaiSanNhatKyService taiSanNhatKyService,
            IRepository<KhaiThacChiTiet> khaiThacChiTietRepository,
            IRepository<LyDoBienDong> lyDoBienDongRepository,
            IRepository<KhaiThac> khaiThacRepository,
            IRepository<LoaiTaiSan> loaiTaiSanRepository,
            ILoaiDonViService loaiDonViService,
            ITaiSanHienTrangSuDungService taiSanHienTrangService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            IHienTrangService hienTrangService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._yeucauRepository = yeucauRepository;
            this._donviRepository = donviRepository;
            this._biendongRepository = biendongRepository;
            this._yeuCauService = yeuCauService;
            this._donViService = donViService;
            this._donViBoPhanService = donViBoPhanService;
            this._cauHinhService = cauHinhService;
            this._haoMonTaiSanRepository = _haoMonTaiSanRepository;
            this._yeuCauChiTietRepository = yeuCauChiTietRepository;
            this._khaiThacTaiSanMappingRepository = khaiThacTaiSanMappingRepository;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._khaiThacChiTietRepository = khaiThacChiTietRepository;
            _lyDoBienDongRepository = lyDoBienDongRepository;
            this._khaiThacRepository = khaiThacRepository;
            this._loaiTaiSanRepository = loaiTaiSanRepository;
            this._loaiDonViService = loaiDonViService;
            this._taiSanHienTrangService = taiSanHienTrangService;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._hienTrangService = hienTrangService;
        }

        #endregion Ctor

        #region Methods

        public virtual IList<TaiSan> GetAllTaiSans(int LoaiTaiSan = 0, int LoaiHinhTaiSan = 0, Decimal? DonViBoPhanId = 0, enumTRANG_THAI_TAI_SAN trangthai = enumTRANG_THAI_TAI_SAN.ALL, List<int> arrLoaiHinhTaiSan = null, Decimal? donViId = null, DateTime? ngayTaoTu = null, DateTime? ngayTaoDen = null)
        {
            var query = _itemRepository.Table;
            if (donViId.GetValueOrDefault(0) > 0)
            {
                string mabo = _donviRepository.GetById(donViId.Value)?.MA_BO;
                if (!string.IsNullOrEmpty(mabo))
                    query = query.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViId);
                else
                    query = query.Where(c => c.DON_VI_ID == donViId);
            }
            if (trangthai != enumTRANG_THAI_TAI_SAN.ALL)
            {
                query = query.Where(c => c.TRANG_THAI_ID == (Decimal)trangthai);
            }
            else
            {
                query = query.Where(c => c.TRANG_THAI_ID != (Decimal)enumTRANG_THAI_TAI_SAN.NHAP);
            }
            if (LoaiTaiSan > 0)
            {
                query = query.Where(c => c.LOAI_TAI_SAN_ID == LoaiTaiSan);
            }
            if (LoaiHinhTaiSan > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            }
            if (DonViBoPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            }
            if (arrLoaiHinhTaiSan != null && arrLoaiHinhTaiSan.Count > 0)
            {
                query = query.Where(c => arrLoaiHinhTaiSan.Contains((int)c.LOAI_HINH_TAI_SAN_ID.GetValueOrDefault(0)));
            }
            if (ngayTaoTu != null)
            {
                query = query.Where(c => c.NGAY_TAO >= ngayTaoTu);
            }
            if (ngayTaoDen != null)
            {
                query = query.Where(c => c.NGAY_TAO <= ngayTaoDen);
            }
            return query.ToList();
        }

        public virtual IList<TaiSan> GetAllTaiSanByMaBo(Decimal? donViId = null, string mabo = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(mabo))
                query = query.Where(x => x.MA_BO == mabo);
            if (donViId.GetValueOrDefault(0) > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            return query.ToList();
        }

        public virtual IList<TaiSan> SearchTaiSanDongBo(string keySearch)
        {
            if (String.IsNullOrEmpty(keySearch))
                return null;
            else
            {
                keySearch = keySearch.ToUpper();
                //var query = _itemRepository.Table.Where(c => (c.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.XOA &&
                //(c.MA_QLDKTS40 != null && c.MA_QLDKTS40.ToUpper().Contains(keySearch))
                //|| (c.MA_DB != null && c.MA_DB.ToUpper().Contains(keySearch))
                //|| c.TEN.ToUpper().Contains(keySearch)
                //|| c.MA.ToUpper().Contains(keySearch)));

                String sql = $"SELECT TS.* " +
                    $"FROM TS_TAI_SAN TS " +
                    $"WHERE MA_QLDKTS40 LIKE '%{keySearch}%'" +
                    $"OR MA_DB LIKE '%{keySearch}%'" +
                    $"OR UPPER(TEN) LIKE '%{keySearch}%'" +
                    $"OR MA LIKE '%{keySearch}%'" +
                    $"ORDER BY TRANG_THAI_ID";
                var query = _dbContext.EntityFromSqlNoParams<TaiSan>(sql);

                return query.ToList();
            }
        }

        /// <summary>
        /// Sử dụng danh sách duyệt tài sản
        /// </summary>
        /// <returns></returns>
        public virtual IPagedList<TaiSan> SearchTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal TRANG_THAI_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0, decimal? DON_VI_BO_PHAN_ID = 0, DateTime? Fromdate = null, DateTime? Todate = null, decimal? donviId = null, bool isDuyet = false, string strLoaiHinhTSIds = null, decimal NguoiTaoId = 0, decimal Loai_Ly_Do_ID = 0, enumTS_NGUYEN_GIA NguyenGia = enumTS_NGUYEN_GIA.TAT_CA, IList<int> ListTaiSanDaChon = null, decimal idKhaiThac = 0, decimal? donvikhaithacid = null, string tenTaiSan = null, string maTaiSan = null, decimal? LY_DO_BIEN_DONG_ID = null, DateTime? FromDateNgayTangMoi = null, DateTime? ToDateNgayTangMoi = null, bool isToanQuoc = false, bool IsExclueTSDKTS = false, bool isKhaiThac = false)
        {
            //var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET
            //    || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET
            //    || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI
            //    || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.NHAP_LIEU
            //    || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
            var query = _itemRepository.Table.Where(c => new[]
                    {
                        (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET,
                        (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET,
                        (decimal)enumTRANG_THAI_TAI_SAN.TRA_LAI,
                        (decimal)enumTRANG_THAI_TAI_SAN.NHAP_LIEU,
                        (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO
                    }.Contains(c.TRANG_THAI_ID.GetValueOrDefault(0)));

            if (!isDuyet)
            {
                query = query.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
            }
            if (NguoiTaoId > 0)
            {
                query = query.Where(p => p.NGUOI_TAO_ID == NguoiTaoId || p.NGUOI_TAO_ID == null);
            }
            if (Loai_Ly_Do_ID != 0)
            {
                if (Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                {
                    query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA);
                }
                else if (Loai_Ly_Do_ID != (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU);
                }
            }

            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower())
                || c.MA.ToLower().Contains(Keysearch.ToLower()));

            if (!String.IsNullOrEmpty(tenTaiSan))
                query = query.Where(c => c.TEN.ToLower().Contains(tenTaiSan.ToLower()));

            if (!String.IsNullOrEmpty(maTaiSan))
                query = query.Where(c => c.MA.ToLower().Contains(maTaiSan.ToLower()));

            if (!string.IsNullOrEmpty(strLoaiHinhTSIds))
            {
                string[] arrayLoaiHinhTaiSanId = strLoaiHinhTSIds.Split(",");
                query = query.Where(c => arrayLoaiHinhTaiSanId.Contains(c.LOAI_HINH_TAI_SAN_ID.ToString()));
            }
            if (LOAI_HINH_TAI_SAN_ID > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LOAI_HINH_TAI_SAN_ID);
            }
            if (DON_VI_BO_PHAN_ID > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == DON_VI_BO_PHAN_ID);
            }
            if (Fromdate.HasValue)
            {
                query = query.Where(x => x.NGAY_SU_DUNG >= Fromdate.Value.Date);
            }
            if (Todate.HasValue && Todate != DateTime.MinValue)
            {
                query = query.Where(x => x.NGAY_SU_DUNG < Todate.Value.Date.AddDays(1));
            }
            if (FromDateNgayTangMoi.HasValue)
            {
                query = query.Where(x => x.NGAY_NHAP >= FromDateNgayTangMoi.Value.Date);
            }
            if (ToDateNgayTangMoi.HasValue && ToDateNgayTangMoi != DateTime.MinValue)
            {
                query = query.Where(x => x.NGAY_NHAP < ToDateNgayTangMoi.Value.Date.AddDays(1));
            }
            if (donviId > 0 && (!isToanQuoc || donviId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
            {
                var donViTreeNode = _donviRepository.GetById(donviId.Value)?.TREE_NODE;
                if (!string.IsNullOrEmpty(donViTreeNode))
                {
                    query = query.Where(ts => ts.donvi.TREE_NODE.StartsWith(donViTreeNode));
                }
                //if (IsExclueTSDKTS)
                //{
                //    if (!(donVi.IS_XAC_NHAN_DU_LIEU??false))
                //    {
                //        query = query.Where(c => c.NGUON_TAI_SAN_ID != (int)enumNguonTaiSan.DKTS40);
                //    }
                //}
            }

            var nguyengia500 = 500000000;
            query = query.Where(p => p.NGUYEN_GIA_BAN_DAU != null);
            if (NguyenGia == enumTS_NGUYEN_GIA.DUOI_500_TRIEU)
            {
                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT &&
                                        p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA &&
                                        p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO &&
                                        p.NGUYEN_GIA_BAN_DAU < nguyengia500);
                //var donViCauHinh = _cauHinhService.LoadCauHinhDonViBo<CauHinhTuDongDuyet>(donviId ?? _workContext.CurrentDonVi.ID);
                //            if (donViCauHinh != null && !string.IsNullOrEmpty(donViCauHinh.IsAutoDuyetTaiSanDuoi500))
                //            {
                //                var lstCauHinhDuyet = donViCauHinh.IsAutoDuyetTaiSanDuoi500.toEntities<CauHinhDuyetTaiSan>();
                //                var lst_LoaiHinhTaiSanKhongAutoDuyet = lstCauHinhDuyet.Where(p => p.IS_AUTO_DUYET_DUOI_NG == false).Select(p => (decimal?)p.LOAI_HINH_TAI_SAN);
                //                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT &&
                //                                    p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA &&
                //                                    p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO &&
                //                                    p.NGUYEN_GIA_BAN_DAU < nguyengia500 &&
                //                                    !lst_LoaiHinhTaiSanKhongAutoDuyet.Contains(p.LOAI_HINH_TAI_SAN_ID));
                //            }
                //            else
                //            {
                //                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT &&
                //                                    p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA &&
                //                                    p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO &&
                //                                    p.NGUYEN_GIA_BAN_DAU < nguyengia500);
                //            }
            }
            else if (NguyenGia == enumTS_NGUYEN_GIA.TREN_500_TRIEU)
            {
                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT ||
                p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA ||
                p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO ||
                p.NGUYEN_GIA_BAN_DAU >= nguyengia500);
            }
            ///trạng thái tài sản
            ///Lẩy theo trạng thái của các biến động trong tài sản
            if (TRANG_THAI_ID > 0)
            {
                switch (TRANG_THAI_ID)
                {
                    // có yêu cầu trạng thái chờ duyệt
                    case (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET:
                        query = query.Where(p => p.YeuCaus.Any(yc => yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET));
                        break;
                    // có yêu cầu trạng thái trả lại
                    case (int)enumTRANG_THAI_TAI_SAN.TRA_LAI:
                        query = query.Where(p => p.YeuCaus.Any(yc => yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI));
                        break;
                    // tất cả yêu cầu là đã duyệt
                    case (int)enumTRANG_THAI_TAI_SAN.DA_DUYET:
                        query = query.Where(p => !p.YeuCaus.Any(yc => yc.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && yc.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP));
                        break;

                    case (int)enumTRANG_THAI_TAI_SAN.XOA:
                        query = query.Where(p => p.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.XOA);
                        break;

                    default:
                        break;
                }
            }
            if (donvikhaithacid > 0)
            {
                query = query.Where(m => m.DON_VI_ID == donvikhaithacid);
                if (ListTaiSanDaChon.Count > 0)
                {
                    query = query.Where(m => !ListTaiSanDaChon.Contains(Convert.ToInt32(m.ID)));
                }
            }
            if (LY_DO_BIEN_DONG_ID != null && LY_DO_BIEN_DONG_ID > 0)
            {
                query = query.Where(c => c.LY_DO_BIEN_DONG_ID == LY_DO_BIEN_DONG_ID);
            }

            #region Cho phép một tài sản nhà có thể thuộc nhiều hợp đồng khai thác tài sản

            // duongph 16/03/2022
            // Cho phép một tài sản nhà có thể thuộc nhiều hợp đồng khai thác tài sản
            if (isKhaiThac)
            {
                //var lstkhaithac = _khaiThacRepository.Table;
                //var lstKhaithacchitiet = _khaiThacChiTietRepository.Table;
                var lsttaisankhaithac = (from i in _khaiThacRepository.Table
                                         join j in _khaiThacChiTietRepository.Table on i.ID equals j.KHAI_THAC_ID
                                         join k in query on j.TAI_SAN_ID equals k.ID
                                         where (k.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA && i.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET)
                                         select k).ToList();

                query = query.Except(lsttaisankhaithac);
            }

            #endregion Cho phép một tài sản nhà có thể thuộc nhiều hợp đồng khai thác tài sản

            query = query.OrderByDescending(c => c.NGAY_SU_DUNG).ThenByDescending(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.MA);
            return new PagedList<TaiSan>(query, pageIndex, pageSize);
        }

        public IPagedList<DonVi> SearchAllDonViChuaNhapTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0)
        {
            var query = _donviRepository.Table.Where(c => c.TRANG_THAI_ID != false);
            IList<DonVi> lstDonVi = new List<DonVi>();
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (ParentID > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentID);
            }
            //if (!string.IsNullOrEmpty(MaBo))
            //{
            //    query = query.Where(c => c.MA_BO == MaBo);
            //}
            query = query.Where(c => c.LA_DON_VI_NHAP_LIEU == true);
            if (CapDonViSearch > 0)
                query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            if (listCapDonVis != null && listCapDonVis.Count() > 0)
            {
                query = query.Where(c => listCapDonVis.Contains((int)(c.CAP_DON_VI_ID ?? 0)));
            }

            if (donViId > 0)
            {
                if (donViId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var donVi = _donviRepository.GetById(donViId.Value);
                    query = query.Where(c => c.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                }
                foreach (var item in query)
                {
                    if (LoaiTaiSanId > 0)
                    {
                        var ts = GetTaiSanByDonViIdAndLoaiTaiSanId(item.ID, LoaiTaiSanId.Value);
                        if (ts.Count == 0)
                        {
                            lstDonVi.Add(item);
                        }
                    }
                    else
                    {
                        var ts = GetTaiSanByDonViId(item.ID);
                        if (ts.Count == 0)
                        {
                            lstDonVi.Add(item);
                        }
                    }
                }
            }
            //query = query.Where(c => c.CO_TAI_SAN == true);
            //lstDonVi = lstDonVi.OrderBy(c => c.MA);
            return new PagedList<DonVi>(lstDonVi, pageIndex, pageSize);
        }

        /// <summary>
        /// Note: Phục vụ danh sách tra cứu tài sản, danh sách tài sản
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        /// if (LoaiDonViSearch > 0)
        public IPagedList<DonVi> SearchAllDonViKiemTraTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0, decimal? LoaiDonViSearch = 0, decimal? MucDichSuDungSearch = 0, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = 0, decimal? DienTich_Value2 = 0)
        {
            var query = _donviRepository.Table.Where(c => c.TRANG_THAI_ID != false);
            IList<DonVi> lstDonVi = new List<DonVi>();
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (ParentID > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentID);
            }
            //if (!string.IsNullOrEmpty(MaBo))
            //{
            //    query = query.Where(c => c.MA_BO == MaBo);
            //}
            if (CapDonViSearch > 0)
                query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            if (LoaiDonViSearch > 0)
            {
                {
                    var listLoaiDonVi = _loaiDonViService.GetListDonViByTreeNode(LoaiDonViSearch);
                    var arrayID = listLoaiDonVi.Select(i => i.ID).ToArray();
                    query = query.Where(c => arrayID.Contains(c.LOAI_DON_VI_ID ?? 0));
                }
            }
            if (listCapDonVis != null && listCapDonVis.Count() > 0)
            {
                query = query.Where(c => listCapDonVis.Contains((int)(c.CAP_DON_VI_ID ?? 0)));
            }
            if (donViId > 0)
            {
                if (donViId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var donVi = _donviRepository.GetById(donViId.Value);
                    query = query.Where(c => c.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                }
                foreach (var item in query)
                {
                    if (LoaiTaiSanId > 0)
                    {
                        var tsdv = GetTaiSanByDonViIdAndLoaiTaiSanId(item.ID, LoaiTaiSanId.Value);
                        tsdv = tsdv.Where(c => c.TRANG_THAI_ID == 4).ToList();
                        bool IsAdd = false;
                        decimal? dtdat = 0;
                        decimal? dtnha = 0;
                        if (tsdv != null)
                        {
                            foreach (var ts in tsdv)
                            {
                                dtdat = 0;
                                var biendongs = _bienDongService.GetBienDongsByTaiSanId(ts.ID);
                                biendongs = biendongs.Where(c => c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).OrderBy(c => c.ID).ToList();
                                foreach (var bd in biendongs)
                                {
                                    var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);
                                    var ht = _taiSanHienTrangService.GetTaiSanHienTrangSuDungByBienDongId(bd.ID);
                                    if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                                    {
                                        dtdat = dtdat + bdct.DAT_TONG_DIEN_TICH;
                                    }
                                    else
                                    {
                                        dtnha = dtnha + bdct.NHA_TONG_DIEN_TICH_XD;
                                    }
                                    if (MucDichSuDungSearch > 0)
                                    {
                                        foreach (var htsd in ht)
                                        {
                                            var tenht = _hienTrangService.GetHienTrangById((decimal)MucDichSuDungSearch).TEN_HIEN_TRANG;
                                            if (htsd.TEN_HIEN_TRANG == tenht && htsd.GIA_TRI_NUMBER > 0)
                                            {
                                                IsAdd = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        IsAdd = true;
                                    }
                                }
                                if (!IsAdd)
                                {
                                    continue;
                                }
                                if (DienTich_CompareSign > 0 && DienTich_Value1 > 0)
                                {
                                    if (ts.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                                    {
                                        if (DienTich_CompareSign == (decimal)enumCompare.SmallerOrEqual && dtdat <= DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Smaller && dtdat < DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Equal && DienTich_Value1 == dtdat)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Larger && dtdat > DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.LargerOrEqual && dtdat >= DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.InRange && (dtdat >= DienTich_Value1 && dtdat <= DienTich_Value2))
                                        {
                                            IsAdd = true;
                                        }
                                        else
                                            IsAdd = false;
                                    }
                                    else
                                    {
                                        if (DienTich_CompareSign == (decimal)enumCompare.SmallerOrEqual && dtnha <= DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Smaller && dtnha < DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Equal && DienTich_Value1 == dtnha)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.Larger && dtnha > DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.LargerOrEqual && dtnha >= DienTich_Value1)
                                        {
                                            IsAdd = true;
                                        }
                                        else if (DienTich_CompareSign == (decimal)enumCompare.InRange && (dtnha >= DienTich_Value1 && dtnha <= DienTich_Value2))
                                        {
                                            IsAdd = true;
                                        }
                                        else IsAdd = false;
                                    }
                                }

                                if (IsAdd || tsdv.Count > 0 && MucDichSuDungSearch == 0 && DienTich_CompareSign == 0)
                                {
                                    lstDonVi.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return new PagedList<DonVi>(lstDonVi, pageIndex, pageSize);
        }

        public IPagedList<TaiSan> SearchAllTaiSanKiemTraTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0, decimal? LoaiDonViSearch = 0, decimal? MucDichSuDungSearch = 0, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = 0, decimal? DienTich_Value2 = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET
                   || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
            query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA);
            var lts = _loaiTaiSanRepository.GetById(LoaiTaiSanId);
            var donVi = _donviRepository.GetById(donViId.Value);
            decimal? dtdat = 0;
            decimal? dtnha = 0;
            bool IsAddFirst = false;
            bool IsAddSecond = false;
            IList<TaiSan> lstTaiSan = new List<TaiSan>();
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (donViId > 0)
            {
                if (donViId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    //var donVi = _donviRepository.GetById(donViId.Value);
                    query = query.Where(ts => ts.DON_VI_ID == donViId.Value || ts.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                }
            }
            //if (!string.IsNullOrEmpty(MaBo))
            //{
            //    query = query.Where(c => c.MA_BO == MaBo);
            //}
            if (CapDonViSearch > 0)
                query = query.Where(c => c.donvi.CAP_DON_VI_ID == CapDonViSearch);
            if (LoaiDonViSearch > 0)
            {
                {
                    var listLoaiDonVi = _loaiDonViService.GetListDonViByTreeNode(LoaiDonViSearch);
                    var arrayID = listLoaiDonVi.Select(i => i.ID).ToArray();
                    query = query.Where(c => arrayID.Contains(c.donvi.LOAI_DON_VI_ID ?? 0));
                }
            }
            if (listCapDonVis != null && listCapDonVis.Count() > 0)
            {
                query = query.Where(c => listCapDonVis.Contains((int)(c.donvi.CAP_DON_VI_ID ?? 0)));
            }
            if (LoaiTaiSanId > 0)
            {
                query = query.Where((c => c.LOAI_TAI_SAN_ID == LoaiTaiSanId || c.loaitaisan.TREE_NODE.StartsWith(lts.TREE_NODE + "-")));
            }
            if (MucDichSuDungSearch > 0 || DienTich_CompareSign > 0)
            {
                foreach (var ts in query)
                {
                    dtdat = 0;
                    dtnha = 0;
                    IsAddFirst = false;
                    IsAddSecond = false;
                    var biendongs = _bienDongService.GetBienDongsByTaiSanId(ts.ID);
                    biendongs = biendongs.Where(c => c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || c.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).OrderBy(c => c.ID).ToList();
                    foreach (var bd in biendongs)
                    {
                        var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);
                        var ht = _taiSanHienTrangService.GetTaiSanHienTrangSuDungByBienDongId(bd.ID);
                        if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            dtdat = dtdat + bdct.DAT_TONG_DIEN_TICH;
                        }
                        else
                        {
                            dtnha = dtnha + bdct.NHA_TONG_DIEN_TICH_XD;
                        }
                        if (MucDichSuDungSearch > 0)
                        {
                            foreach (var htsd in ht)
                            {
                                var tenht = _hienTrangService.GetHienTrangById((decimal)MucDichSuDungSearch).TEN_HIEN_TRANG;
                                if (htsd.TEN_HIEN_TRANG == tenht && htsd.GIA_TRI_NUMBER > 0)
                                {
                                    IsAddFirst = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            IsAddFirst = true;
                        }
                    }
                    if (DienTich_CompareSign > 0 && DienTich_Value1 > 0)
                    {
                        if (ts.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            if (DienTich_CompareSign == (decimal)enumCompare.SmallerOrEqual && dtdat <= DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Smaller && dtdat < DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Equal && DienTich_Value1 == dtdat)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Larger && dtdat > DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.LargerOrEqual && dtdat >= DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.InRange && (dtdat >= DienTich_Value1 && dtdat <= DienTich_Value2))
                            {
                                IsAddSecond = true;
                            }
                            else IsAddSecond = false;
                        }
                        else
                        {
                            if (DienTich_CompareSign == (decimal)enumCompare.SmallerOrEqual && dtnha <= DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Smaller && dtnha < DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Equal && DienTich_Value1 == dtnha)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.Larger && dtnha > DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.LargerOrEqual && dtnha >= DienTich_Value1)
                            {
                                IsAddSecond = true;
                            }
                            else if (DienTich_CompareSign == (decimal)enumCompare.InRange && (dtnha >= DienTich_Value1 && dtnha <= DienTich_Value2))
                            {
                                IsAddSecond = true;
                            }
                            else IsAddSecond = false;
                        }
                    }
                    else IsAddSecond = true;
                    if (IsAddFirst && IsAddSecond)
                    {
                        lstTaiSan.Add(ts);
                    }
                }
                return new PagedList<TaiSan>(lstTaiSan, pageIndex, pageSize);
            }
            else
            {
                return new PagedList<TaiSan>(query, pageIndex, pageSize);
            }
        }

        public virtual IPagedList<TaiSan> DanhSachTaiSans(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            string Keysearch = null,
            decimal TRANG_THAI_ID = 0,
            decimal? LOAI_HINH_TAI_SAN_ID = 0,
            decimal? DON_VI_BO_PHAN_ID = 0,
            DateTime? Fromdate = null,
            DateTime? Todate = null,
            decimal? donviId = 0,
            bool isDuyet = false,
            string[] strLoaiHinhTSIds = null,
            decimal NguoiTaoId = 0,
            decimal Loai_Ly_Do_ID = 0,
            enumTS_NGUYEN_GIA NguyenGia = enumTS_NGUYEN_GIA.TAT_CA,
            List<decimal> taiSanIdExclude = null,
            int? NguonTaiSanId = -1,
            bool isCheckDonVi = false,
            bool isToanQuoc = false,
            bool IsChonTaiSan = false,
            bool? IsDanhSachTaiSanDuAn = false,
            bool? IsFilter = false,
            bool? IsHaoMon = false
            )
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET
                        || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET
                        || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.TRA_LAI
                        || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.NHAP_LIEU
                        || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
            if (Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI
                || Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI
                || Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN
                || Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO
                || Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                query = query.Where(c => !c.YeuCaus.Any(yc => yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI)); // không hiển thị tài sản trạng thái từ chối
            }

            if (taiSanIdExclude != null && taiSanIdExclude.Count > 0)
                query = query.Where(c => !taiSanIdExclude.Contains(c.ID));
            if (!isDuyet)
            {
                query = query.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO);
            }
            if (NguonTaiSanId >= 0)
            {
                query = query.Where(p => p.NGUON_TAI_SAN_ID == NguonTaiSanId);
            }
            if (NguoiTaoId > 0)
            {
                query = query.Where(p => p.NGUOI_TAO_ID == NguoiTaoId || p.NGUOI_TAO_ID == null);
            }
            //if (NguoiTaoId > 0)
            //{
            //    query = query.Where(p => p.NGUOI_TAO_ID == NguoiTaoId);
            //}
            if (Loai_Ly_Do_ID != 0)
            {
                if (Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                {
                    query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA);
                }
                else if (Loai_Ly_Do_ID != (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU);
                }
            }
            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower())
                || c.MA.ToLower().Contains(Keysearch.ToLower()));

            if (strLoaiHinhTSIds != null)
            {
                query = query.Where(c => strLoaiHinhTSIds.Contains(c.LOAI_HINH_TAI_SAN_ID.ToString()));
            }
            if (LOAI_HINH_TAI_SAN_ID > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LOAI_HINH_TAI_SAN_ID);
            }
            if (DON_VI_BO_PHAN_ID > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == DON_VI_BO_PHAN_ID);
            }
            if (Fromdate.HasValue)
            {
                var _tungay = Fromdate.Value.Date;
                query = query.Where(x => x.NGAY_SU_DUNG >= _tungay);
            }
            if (Todate.HasValue && Todate != DateTime.MinValue)
            {
                var _denngay = Todate.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_SU_DUNG < _denngay);
            }
            if (donviId > 0)
            {
                if (!isToanQuoc || donviId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var donVi = _donviRepository.GetById(donviId.Value);
                    if (!isDuyet)
                    {
                        query = query.Where(c => c.MA_BO == donVi.MA_BO && c.DON_VI_ID == donviId);
                    }
                    else if (isDuyet)
                    {
                        query = query.Where(ts => ts.MA_BO == donVi.MA_BO && ts.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE));
                    }
                }
                // nếu chọn tài sản
                if (IsChonTaiSan)
                {
                    var donVi = _donviRepository.GetById(donviId.Value);
                    //if (!(donVi.IS_XAC_NHAN_DU_LIEU?? false))
                    //{
                    //    query = query.Where(c => c.NGUON_TAI_SAN_ID != (int)enumNguonTaiSan.DKTS40);
                    //}
                }
            }
            if (isCheckDonVi)
            {
                var donVi = _donviRepository.Table;
                query = from q in query
                        join dv in donVi
                            on q.DON_VI_ID equals dv.ID
                        where dv.LOAI_DON_VI_ID == (int)enumLoaiDonVi_TheoId.BAN_QUAN_LY_DU_AN
                        select q;
            }

            var nguyengia500 = 500000000;
            query = query.Where(p => p.NGUYEN_GIA_BAN_DAU != null);
            if (NguyenGia == enumTS_NGUYEN_GIA.DUOI_500_TRIEU)
            {
                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT &&
                p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA &&
                p.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO &&
                p.NGUYEN_GIA_BAN_DAU < nguyengia500);
            }
            else if (NguyenGia == enumTS_NGUYEN_GIA.TREN_500_TRIEU)
            {
                query = query.Where(p => p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT ||
                p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA ||
                p.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO ||
                p.NGUYEN_GIA_BAN_DAU >= nguyengia500);
            }
            ///trạng thái tài sản
            ///Lẩy theo trạng thái của các biến động trong tài sản
            if (TRANG_THAI_ID > 0)
            {
                switch (TRANG_THAI_ID)
                {
                    // có yêu cầu trạng thái chờ duyệt
                    case (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET:
                        query = query.Where(p => p.YeuCaus.Any(yc => yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET));
                        break;
                    // có yêu cầu trạng thái trả lại
                    case (int)enumTRANG_THAI_TAI_SAN.TRA_LAI:
                        query = query.Where(p => p.YeuCaus.Any(yc => yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.TU_CHOI));
                        break;
                    // tất cả yêu cầu là đã duyệt
                    case (int)enumTRANG_THAI_TAI_SAN.DA_DUYET:
                        query = query.Where(p => !p.YeuCaus.Any(yc => yc.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && yc.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP));
                        break;

                    case (int)enumTRANG_THAI_TAI_SAN.XOA:
                        query = query.Where(p => p.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.XOA);
                        break;

                    default:
                        break;
                }
            }
            if (IsDanhSachTaiSanDuAn == true)
            {
                query = query.Where(p => p.DU_AN_ID != null);
            }
            query = query.OrderByDescending(c => c.NGAY_SU_DUNG).ThenByDescending(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.MA);
            if (TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET)
            {
                //D_ Lấy thêm thông tin tài sản đã được tính hao mòn hay chưa
                var haoMonTaiSan = _haoMonTaiSanRepository.Table;
                query = from q in query
                        select new TaiSan(q)
                        {
                            SO_LUONG_HAO_MON_TAI_SAN = haoMonTaiSan.Where(h => h.NAM_HAO_MON < DateTime.Now.Year && h.TAI_SAN_ID == q.ID).Count(),
                        };
                if ((bool)IsFilter)
                {
                    query = (from q in query
                             where (q.NGAY_SU_DUNG != null && DateTime.Now.Year - 1 - q.NGAY_SU_DUNG.Value.Year < q.SO_LUONG_HAO_MON_TAI_SAN) == (bool)IsHaoMon
                             select q).AsQueryable();
                    //query = from q in query
                    //        where (q.NGAY_SU_DUNG != null && DateTime.Now.Year - 1 - q.NGAY_SU_DUNG.Value.Year < q.SO_LUONG_HAO_MON_TAI_SAN)
                    //        select q;
                }
            }
            var rs = new PagedList<TaiSan>(query, pageIndex, pageSize);
            return rs;
            //return new PagedList<TaiSan>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<TaiSan> GetTaiSanOtoChuyenDung(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donViId = null)
        {
            //xe ô tô chuyên dùng, đổi thành cấu hình sau
            var query = _itemRepository.Table.Where(p => p.LOAI_TAI_SAN_ID == (int)enumLoaiTaiSanOto.CHUYEN_DUNG && p.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.XOA && p.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.NHAP);
            if (donViId > 0)
            {
                var list_donViId = _donViService.GetListIdDonViChild(donViId.Value, true);
                query = from q in query
                        join dv in list_donViId
                            on q.DON_VI_ID equals dv
                        select q;
            }
            return new PagedList<TaiSan>(query, pageIndex, pageSize);
        }

        public virtual TaiSan GetTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual TaiSan GetTaiSanByMaDBPmCu(string ma_db, string MaDonVi)
        {
            var query = _itemRepository.Table.Where(c => c.MA_DB == ma_db && c.TRANG_THAI_ID == 4);
            if (!string.IsNullOrEmpty(MaDonVi))
            {
                query = query.Where(c => c.donvi.MA == MaDonVi);
            }
            return query.FirstOrDefault();
        }

        public virtual TaiSan GetTaiSanByGuId(Guid guid)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == guid);
            return query.FirstOrDefault();
        }

        //get tài sản ở trạng thái khác xóa
        public virtual IList<TaiSan> GetTaiSanByTrangThaiId(decimal TrangThaiId, decimal donViId)
        {
            var query = _itemRepository.Table;
            query = query.Where(c => c.TRANG_THAI_ID != TrangThaiId);
            var donVi = _donViService.GetDonViById(donViId);
            if (donVi != null)
                query = query.Where(p => p.DON_VI_ID == donVi.ID || p.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            return query.ToList();
        }

        public virtual IList<TaiSan> GetTaiSanByDonViId(decimal donViID)
        {
            string mabo = _donviRepository.GetById(donViID)?.MA_BO;
            if (!string.IsNullOrEmpty(mabo))
                return _itemRepository.Table.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViID).ToList();
            else
                return _itemRepository.Table.Where(c => c.DON_VI_ID == donViID).ToList();
        }

        public virtual IList<TaiSan> GetTaiSanByDonViIdAndLoaiTaiSanId(decimal donViId, decimal loaiTaiSanId)
        {
            var lts = _loaiTaiSanRepository.GetById(loaiTaiSanId);
            string mabo = _donviRepository.GetById(donViId)?.MA_BO;
            if (!string.IsNullOrEmpty(mabo))
                return _itemRepository.Table.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViId && c.loaitaisan.TREE_NODE.StartsWith(lts.TREE_NODE)).ToList();
            else
                return _itemRepository.Table.Where(c => c.DON_VI_ID == donViId && c.loaitaisan.TREE_NODE.StartsWith(lts.TREE_NODE)).ToList();
        }

        public virtual bool GetTaiSanByTrangThaiId(decimal donViId)
        {
            var donVi = _donViService.GetDonViById(donViId);
            var query = _itemRepository.Table.Any(p => p.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE) && (p.TRANG_THAI_ID != Convert.ToDecimal(enumTRANG_THAI_TAI_SAN.XOA) && p.TRANG_THAI_ID != Convert.ToDecimal(enumTRANG_THAI_TAI_SAN.NHAP)));
            return query;
        }

        public virtual bool GetTaiSanByDonViBoPhanId(decimal donViBoPhanId)
        {
            var dvbp = _donViBoPhanService.GetDonViBoPhanById(donViBoPhanId);
            var query = _itemRepository.Table.Any(p => p.donvibophan.ID == dvbp.ID);
            return query;
        }

        public virtual IList<TaiSan> GetTaiSanByNguoiTao(decimal nguoiTaoId, decimal donViId)
        {
            var query = _itemRepository.Table;

            if (nguoiTaoId == (int)enumNguoiTaoTaiSan.KhongNguoiQuanLy)
                query = query.Where(p => p.NGUOI_TAO_ID == null);
            else
                query = query.Where(p => p.NGUOI_TAO_ID == nguoiTaoId);

            var donVi = _donViService.GetDonViById(donViId);
            if (donVi != null)
                query = query.Where(p => p.DON_VI_ID == donVi.ID || p.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            return query.ToList();
        }

        public virtual TaiSan GetTaiSanByMa(string MaTS)
        {
            if (string.IsNullOrEmpty(MaTS))
                return null;
            var query = _itemRepository.Table.Where(c => c.MA == MaTS);
            return query.FirstOrDefault();
        }

        public virtual TaiSan GetTaiSanByMaDKTS(string MaDKTS)
        {
            if (string.IsNullOrEmpty(MaDKTS))
                return null;
            var query = _itemRepository.Table.Where(c => c.MA_QLDKTS40 == MaDKTS);
            return query.FirstOrDefault();
        }

        public virtual TaiSan GetTaiSanByTen(string TenTS, decimal? donViId = 0)
        {
            if (string.IsNullOrEmpty(TenTS))
                return null;
            var query = _itemRepository.Table.Where(c => c.TEN.ToLower() == TenTS.ToLower() && (c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET || c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET));
            if (donViId > 0)
            {
                string mabo = _donviRepository.GetById(donViId)?.MA_BO;
                if (!string.IsNullOrEmpty(mabo))
                    query = query.Where(c => c.MA_BO == mabo && c.DON_VI_ID == donViId);
                else
                    query = query.Where(c => c.DON_VI_ID == donViId);
            }

            return query.FirstOrDefault();
        }

        public virtual IList<TaiSan> GetTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<TaiSan> GetTaiSanByKhaiThacId(decimal KhaiThacId)
        {
            var query = from q in _itemRepository.Table
                        join qvt in _khaiThacTaiSanMappingRepository.Table on q.ID equals qvt.TAI_SAN_ID
                        where qvt.KHAI_THAC_ID == KhaiThacId
                        orderby q.ID
                        select q;
            return query.ToList();
        }

        public virtual void InsertTaiSan(TaiSan entity, bool isNguoiTaoNull = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            DonVi donVi = _donViService.GetDonViById(Convert.ToDecimal(entity.DON_VI_ID));
            if (donVi != null)
            {
                entity.MA_BO = donVi.MA.Substring(0, 3);
            }
            entity.NGAY_TAO = DateTime.Now;
            if (isNguoiTaoNull)
                entity.NGUOI_TAO_ID = null;
            else
                entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            entity.GUID = Guid.NewGuid();
            //entity.TRANG_THAI_ID = 1;
            //entity.LOAI_TAI_SAN_ID = null;
            //entity.LY_DO_BIEN_DONG_ID = null;
            //entity.NGAY_NHAP = null;
            //entity.NGAY_SU_DUNG = null;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual bool InsertTaiSanByProcedure(TaiSan entity, bool isNguoiTaoNull = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            if (isNguoiTaoNull)
                entity.NGUOI_TAO_ID = null;
            else
                entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            entity.GUID = Guid.NewGuid();
            OracleParameter GUID = new OracleParameter("GUID", OracleDbType.Raw, entity.GUID, ParameterDirection.Input);
            OracleParameter TEN = new OracleParameter("TEN", OracleDbType.Varchar2, entity.TEN, ParameterDirection.Input);

            try
            {
                IQueryable<TaiSan> result = _dbContext.EntityFromSql<TaiSan>("TEST_INSERT_TAI_SAN", GUID, TEN);
                //var x = CUR_OUT;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            //OracleParameter CHUNG_TU_NGAY = new OracleParameter("CHUNG_TU_NGAY", OracleDbType.Date, entity.CHUNG_TU_NGAY, ParameterDirection.Input);
            //OracleParameter CHUNG_TU_SO = new OracleParameter("CHUNG_TU_SO", OracleDbType.Varchar2, entity.CHUNG_TU_SO, ParameterDirection.Input);
            //OracleParameter DOI_TAC_ID = new OracleParameter("DOI_TAC_ID", OracleDbType.Decimal, entity.DOI_TAC_ID, ParameterDirection.Input);
            //OracleParameter DON_VI_BO_PHAN_ID = new OracleParameter("DON_VI_BO_PHAN_ID", OracleDbType.Decimal, entity.DON_VI_BO_PHAN_ID, ParameterDirection.Input);
            //OracleParameter DON_VI_ID = new OracleParameter("DON_VI_ID", OracleDbType.Decimal, entity.DON_VI_ID, ParameterDirection.Input);
            //OracleParameter DU_AN_ID = new OracleParameter("DU_AN_ID", OracleDbType.Decimal, entity.DU_AN_ID, ParameterDirection.Input);
            //OracleParameter GHI_CHU = new OracleParameter("GHI_CHU", OracleDbType.Varchar2, entity.GHI_CHU, ParameterDirection.Input);
            //OracleParameter GIA_MUA_TIEP_NHAN = new OracleParameter("GIA_MUA_TIEP_NHAN", OracleDbType.Varchar2, entity.GIA_MUA_TIEP_NHAN.HasValue ? entity.GIA_MUA_TIEP_NHAN.ToString() : "", ParameterDirection.Input);
            //OracleParameter GUID = new OracleParameter("GUID", OracleDbType.Raw, entity.GUID, ParameterDirection.Input);
            //OracleParameter LOAI_HINH_TAI_SAN_ID = new OracleParameter("LOAI_HINH_TAI_SAN_ID", OracleDbType.Decimal, entity.LOAI_HINH_TAI_SAN_ID, ParameterDirection.Input);
            //OracleParameter LOAI_TAI_SAN_ID = new OracleParameter("LOAI_TAI_SAN_ID", OracleDbType.Decimal, entity.LOAI_TAI_SAN_ID, ParameterDirection.Input);
            //OracleParameter LOAI_TAI_SAN_DON_VI_ID = new OracleParameter("LOAI_TAI_SAN_DON_VI_ID", OracleDbType.Decimal, entity.LOAI_TAI_SAN_DON_VI_ID, ParameterDirection.Input);
            //OracleParameter LY_DO_BIEN_DONG_ID = new OracleParameter("LY_DO_BIEN_DONG_ID", OracleDbType.Decimal, entity.LY_DO_BIEN_DONG_ID, ParameterDirection.Input);
            //OracleParameter MA = new OracleParameter("MA", OracleDbType.Varchar2, entity.MA, ParameterDirection.Input);
            //OracleParameter MA_DB = new OracleParameter("MA_DB", OracleDbType.Varchar2, entity.MA_DB, ParameterDirection.Input);
            //OracleParameter NAM_SAN_XUAT = new OracleParameter("NAM_SAN_XUAT", OracleDbType.Decimal, entity.NAM_SAN_XUAT, ParameterDirection.Input);
            //OracleParameter NGAY_DUYET = new OracleParameter("NGAY_DUYET", OracleDbType.Date, entity.NGAY_DUYET, ParameterDirection.Input);
            //OracleParameter NGAY_NHAP = new OracleParameter("NGAY_NHAP", OracleDbType.Date, entity.NGAY_NHAP, ParameterDirection.Input);
            //OracleParameter NGAY_SU_DUNG = new OracleParameter("NGAY_SU_DUNG", OracleDbType.Date, entity.NGAY_SU_DUNG, ParameterDirection.Input);
            //OracleParameter NGAY_TAO = new OracleParameter("NGAY_TAO", OracleDbType.Date, entity.NGAY_TAO, ParameterDirection.Input);
            //OracleParameter NGUOI_TAO_ID = new OracleParameter("NGUOI_TAO_ID", OracleDbType.Decimal, entity.NGUOI_TAO_ID, ParameterDirection.Input);

            //OracleParameter NGUYEN_GIA_BAN_DAU = new OracleParameter("NGUYEN_GIA_BAN_DAU", OracleDbType.Decimal, entity.NGUYEN_GIA_BAN_DAU, ParameterDirection.Input);
            //OracleParameter NUOC_SAN_XUAT_ID = new OracleParameter("NUOC_SAN_XUAT_ID", OracleDbType.Decimal, entity.NUOC_SAN_XUAT_ID, ParameterDirection.Input);
            //OracleParameter QUYET_DINH_NGAY = new OracleParameter("QUYET_DINH_NGAY", OracleDbType.Date, entity.QUYET_DINH_NGAY, ParameterDirection.Input);
            //OracleParameter QUYET_DINH_NGUOI_ID = new OracleParameter("QUYET_DINH_NGUOI_ID", OracleDbType.Decimal, entity.QUYET_DINH_NGUOI_ID, ParameterDirection.Input);
            //OracleParameter QUYET_DINH_SO = new OracleParameter("QUYET_DINH_SO", OracleDbType.Varchar2, entity.QUYET_DINH_SO, ParameterDirection.Input);
            //OracleParameter TEN = new OracleParameter("TEN", OracleDbType.Varchar2, entity.TEN, ParameterDirection.Input);
            //OracleParameter TRANG_THAI_ID = new OracleParameter("TRANG_THAI_ID", OracleDbType.Decimal, entity.TRANG_THAI_ID, ParameterDirection.Input);

            ////OracleParameter NGUYEN_GIA_BAN_DAU = new OracleParameter("NGUYEN_GIA_BAN_DAU", OracleDbType.Varchar2, entity.NGUYEN_GIA_BAN_DAU.HasValue ? entity.NGUYEN_GIA_BAN_DAU.ToString() : "", ParameterDirection.Input);

            //try
            //{
            //    IQueryable<TaiSan> result = _dbContext.EntityFromSql<TaiSan>("INSERT_TAI_SAN", CHUNG_TU_NGAY, CHUNG_TU_SO, DOI_TAC_ID, DON_VI_BO_PHAN_ID, DON_VI_ID, DU_AN_ID, GHI_CHU, GIA_MUA_TIEP_NHAN, GUID, LOAI_HINH_TAI_SAN_ID, LOAI_TAI_SAN_ID, LOAI_TAI_SAN_DON_VI_ID, LY_DO_BIEN_DONG_ID, MA, MA_DB, NAM_SAN_XUAT, NGAY_DUYET, NGAY_NHAP, NGAY_SU_DUNG, NGAY_TAO, NGUOI_TAO_ID, NGUYEN_GIA_BAN_DAU, NUOC_SAN_XUAT_ID, QUYET_DINH_NGAY, QUYET_DINH_NGUOI_ID, QUYET_DINH_SO, TEN, TRANG_THAI_ID);
            //    //var x = CUR_OUT;
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        public virtual void InsertTaiSan(List<TaiSan> entities, bool isNguoiTaoNull = false)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            if (isNguoiTaoNull)
                entities = entities.Select(c =>
                {
                    DonVi donVi = _donViService.GetDonViById(Convert.ToDecimal(c.DON_VI_ID));
                    if (donVi != null)
                    {
                        c.MA_BO = donVi.MA.Substring(0, 3);
                    }
                    c.NGAY_TAO = DateTime.Now;
                    c.NGUOI_TAO_ID = null;
                    c.GUID = Guid.NewGuid();
                    return c;
                }).ToList();
            else
                entities = entities.Select(c =>
                {
                    DonVi donVi = _donViService.GetDonViById(Convert.ToDecimal(c.DON_VI_ID));
                    if (donVi != null)
                    {
                        c.MA_BO = donVi.MA.Substring(0, 3);
                    }
                    c.NGAY_TAO = DateTime.Now;
                    c.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    c.GUID = Guid.NewGuid();
                    return c;
                }).ToList();

            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateTaiSan(TaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void UpdateTaiSan(List<TaiSan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteTaiSan(TaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public virtual IList<decimal> GetTaiSanIdsByTrangThaiYC(decimal? TrangThaiYC = 0, List<decimal?> exceptTrangThaiYC = null, decimal? donviId = 0)
        {
            var query = _yeucauRepository.Table;
            if (donviId > 0)
            {
                var list_donViId = _donViService.GetListIdDonViChild(donviId.Value, true);
                query = from q in query
                        join dv in list_donViId
                            on q.DON_VI_ID equals dv
                        select q;
            }
            if (TrangThaiYC > 0)
                query = query.Where(p => p.TRANG_THAI_ID == TrangThaiYC);
            if (exceptTrangThaiYC != null && exceptTrangThaiYC.Count > 0)
                query = query.Where(p => !exceptTrangThaiYC.Contains(p.TRANG_THAI_ID));
            return query.Select(p => p.TAI_SAN_ID).Distinct().ToList();
        }

        private IList<decimal> GetTaiSanIdsKhacDaDuyet(decimal? donviId = 0)
        {
            var query = _yeucauRepository.Table.Where(p => p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.NHAP && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.XOA && p.TRANG_THAI_ID != (int)enumTRANG_THAI_YEU_CAU.DA_DUYET);
            if (donviId > 0)
            {
                var list_donViId = _donViService.GetListIdDonViChild(donviId.Value, true);
                query = from q in query
                        join dv in list_donViId
                            on q.DON_VI_ID equals dv
                        select q;
            }
            return query.Select(p => p.TAI_SAN_ID).Distinct().ToList();
        }

        public bool CheckTonTaiByMaDB(string ma, decimal NguonTaiSanId)
        {
            if (string.IsNullOrEmpty(ma))
                return false;
            if (NguonTaiSanId == 0)
                return false;
            return _itemRepository.Table.Any(c => c.MA_DB == ma && c.NGUON_TAI_SAN_ID == NguonTaiSanId);
        }

        public bool CheckTonTaiByMaQLDKTS40(string ma)
        {
            if (string.IsNullOrEmpty(ma))
                return false;
            return _itemRepository.Table.Any(c => c.MA_QLDKTS40 == ma);
        }

        public virtual TaiSan GetTaiSanByMaDB(string Ma, Guid GUID = new Guid(), string MaDonVi = null, decimal NguonTaiSanId = 0)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Ma))
            {
                if (NguonTaiSanId == (decimal)enumNguonTaiSan.CSDLQGTSC)
                    query = _itemRepository.Table.Where(c => c.MA_DB == Ma);
                else
                    query = _itemRepository.Table.Where(c => c.MA_QLDKTS40 == Ma && c.NGUON_TAI_SAN_ID == NguonTaiSanId);
            }
            if (!string.IsNullOrEmpty(MaDonVi))
            {
                query = _itemRepository.Table.Where(c => c.donvi.MA == MaDonVi || c.donvi.MA_DVQHNS == MaDonVi);
            }
            if (!GUID.Equals(Guid.Empty))
            {
                query = _itemRepository.Table.Where(c => c.GUID == GUID);
            }
            return query.FirstOrDefault();
        }

        public virtual bool DeleteTaiSansLogic(decimal? p_DonViId = null, string strLoaiHinhTaiSan = null, bool IsDeleteDVCon = true, string strMaTaiSan = null, decimal? nguon_tai_san_id = null)
        {
            // Cập nhật trạng thái xóa
            OracleParameter p_ID_DON_VI = new OracleParameter("p_ID_DON_VI", OracleDbType.Int32, p_DonViId, ParameterDirection.Input);
            OracleParameter P_STR_LOAI_HINH_TAI_SAN = new OracleParameter("P_STR_LOAI_HINH_TAI_SAN", OracleDbType.Clob, strLoaiHinhTaiSan, ParameterDirection.Input);
            int _IsDeleteDVCon = 1;
            if (!IsDeleteDVCon)
                _IsDeleteDVCon = 0;
            OracleParameter P_ISDELETEDVCON = new OracleParameter("P_ISDELETEDVCON", OracleDbType.Int32, _IsDeleteDVCon, ParameterDirection.Input);
            OracleParameter P_STR_MA_TAI_SAN = new OracleParameter("P_STR_MA_TAI_SAN", OracleDbType.Clob, strMaTaiSan, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_ID = new OracleParameter("P_NGUON_TAI_SAN_ID", OracleDbType.Decimal, nguon_tai_san_id, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN DELETE_LOGIC_TS(:P_STR_MA_TAI_SAN,:p_ID_DON_VI, :P_STR_LOAI_HINH_TAI_SAN, :P_ISDELETEDVCON,:P_NGUON_TAI_SAN_ID); END;", false, null, P_STR_MA_TAI_SAN, p_ID_DON_VI, P_STR_LOAI_HINH_TAI_SAN, P_ISDELETEDVCON, P_NGUON_TAI_SAN_ID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool ChuyenNguonTaiSan(decimal? p_DonViId = null, string strLoaiHinhTaiSan = null, bool IsDVCon = true, string strMaTaiSan = null, decimal? nguon_tai_san_id_from = null, decimal? nguon_tai_san_id_to = null)
        {
            OracleParameter p_ID_DON_VI = new OracleParameter("p_ID_DON_VI", OracleDbType.Int32, p_DonViId, ParameterDirection.Input);
            OracleParameter P_STR_LOAI_HINH_TAI_SAN = new OracleParameter("P_STR_LOAI_HINH_TAI_SAN", OracleDbType.Clob, strLoaiHinhTaiSan, ParameterDirection.Input);
            int _IsDVCon = 1;
            if (!IsDVCon)
                _IsDVCon = 0;
            OracleParameter P_ISDVCON = new OracleParameter("P_ISDVCON", OracleDbType.Int32, _IsDVCon, ParameterDirection.Input);
            OracleParameter P_STR_MA_TAI_SAN = new OracleParameter("P_STR_MA_TAI_SAN", OracleDbType.Clob, strMaTaiSan, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_FROM = new OracleParameter("P_NGUON_TAI_SAN_FROM", OracleDbType.Decimal, nguon_tai_san_id_from, ParameterDirection.Input);
            OracleParameter P_NGUON_TAI_SAN_TO = new OracleParameter("P_NGUON_TAI_SAN_TO", OracleDbType.Decimal, nguon_tai_san_id_to, ParameterDirection.Input);
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN SP_CHUYEN_NGUON_TAI_SAN(:P_STR_MA_TAI_SAN,:p_ID_DON_VI, :P_STR_LOAI_HINH_TAI_SAN, :P_ISDVCON,:P_NGUON_TAI_SAN_FROM, :P_NGUON_TAI_SAN_TO); END;", false, null, P_STR_MA_TAI_SAN, p_ID_DON_VI, P_STR_LOAI_HINH_TAI_SAN, P_ISDVCON, P_NGUON_TAI_SAN_FROM, P_NGUON_TAI_SAN_TO);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual decimal Get_GTLC_Cua_TS(decimal p_idTaiSan, DateTime? p_NgayBienDong = null, bool p_isEqualDateTime = false)
        {
            var list_yc = _yeucauRepository.Table.Where(p =>
            (p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET && p.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO));
            if (p_isEqualDateTime)
                list_yc = list_yc.Where(p => p.NGAY_BIEN_DONG <= p_NgayBienDong);
            else
                list_yc = list_yc.Where(p => p.NGAY_BIEN_DONG < p_NgayBienDong);
            list_yc = list_yc.Where(p => p.TAI_SAN_ID == p_idTaiSan);
            var ng_yc = list_yc.Sum(p => p.NGUYEN_GIA ?? 0);

            var ts = _itemRepository.GetById(p_idTaiSan);
            var haoMonOfTS = _haoMonTaiSanRepository.Table.Where(p => p.TAI_SAN_ID == p_idTaiSan);

            if (p_NgayBienDong != null)
            {
                if (p_NgayBienDong.Value.Day == 31 && p_NgayBienDong.Value.Month == 12 && p_isEqualDateTime)
                {
                    var res = haoMonOfTS.Where(p => p.NAM_HAO_MON == p_NgayBienDong.Value.Year).OrderByDescending(p => p.ID).FirstOrDefault();
                    if (res != null)
                    {
                        var gtcl = res.TONG_GIA_TRI_CON_LAI ?? 0;
                        return gtcl + ng_yc;
                    }
                }
                else
                {
                    var res = haoMonOfTS.Where(p => p.NAM_HAO_MON == p_NgayBienDong.Value.Year - 1).OrderByDescending(p => p.ID).FirstOrDefault();
                    if (res != null)
                    {
                        var gtcl = res.TONG_GIA_TRI_CON_LAI ?? 0;
                        return gtcl + ng_yc;
                    }
                    else //nếu năm trước đó tài sản chưa được nhập, chưa có tỉ lệ hao mòn
                    {
                        var res_2 = haoMonOfTS.Where(p => p.NAM_HAO_MON == p_NgayBienDong.Value.Year).OrderByDescending(p => p.ID).FirstOrDefault();
                        if (res_2 != null)
                        {
                            var gtcl = res_2.TONG_GIA_TRI_CON_LAI ?? 0;
                            return gtcl + ng_yc;
                        }
                    }
                }
            }
            else // lấy ra gtcl đã chốt mới nhất của tài sản
            {
                var res = haoMonOfTS.OrderByDescending(p => p.NAM_HAO_MON).ThenByDescending(p => p.ID).FirstOrDefault();
                if (res != null)
                {
                    var gtcl = res.TONG_GIA_TRI_CON_LAI ?? 0;
                    return gtcl;
                }
            }
            return 0;
        }

        public virtual bool IsNotHasBDGiamTaiSan(Guid guidTS)
        {
            var TaiSan = GetTaiSanByGuId(guidTS);
            if (TaiSan != null && TaiSan.ID > 0)
            {
                int count_bd_giam = _biendongRepository.Table.Where(p => p.TAI_SAN_ID == TaiSan.ID && p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO).Count();
                if (count_bd_giam > 0)
                    return false;

                int count_yc_giam = _yeucauRepository.Table.Where(p => p.TAI_SAN_ID == TaiSan.ID && p.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET && p.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO).Count();
                if (count_yc_giam > 0)
                    return false;

                return true;
            }
            return false;
        }

        //public virtual List<TaiSan> GetTaiSanCanhBao(decimal? DonViId, int pageIndex = 0, int pageSize = int.MaxValue )
        //{
        //	var cauHinhCanhBaoTaiSan = _cauHinhService.LoadCauHinh<CauHinhCanhBaoTaiSan>();
        //	if (cauHinhCanhBaoTaiSan != null && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.SelectStatementQuery) && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.WhereStatementJson))
        //	{
        //		var CauHinhDieuKienLoc = _cauHinhService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
        //		var ListDieuKienLocTS = CauHinhDieuKienLoc.JsonValue.toEntities<DieuKienLocTaiSan>();
        //		var whereStatements = cauHinhCanhBaoTaiSan.WhereStatementJson.toEntities<WhereStatement>();
        //		var statement = cauHinhCanhBaoTaiSan.SelectStatementQuery + " where 1=1";
        //		foreach (var whereStament in whereStatements)
        //		{
        //			var dieuKienLoc = ListDieuKienLocTS.FirstOrDefault(p => p.MA_LOC_TAI_SAN == whereStament.MA_DIEU_KIEN);
        //			var where = string.Format(dieuKienLoc.VALUE, whereStament.TEN_TRUONG, whereStament.GIA_TRI_SO_SANH);
        //			statement = statement + where;
        //		}
        //		statement = $"{statement} and DON_VI_ID = {DonViId.ToString().Replace(',','.')}; ";
        //		//IQueryable<TaiSan> table = _itemRepository.Table.FromSql(statement);

        //		var taisans = _dbContext.EntityFromSqlNoParams<TaiSan>(statement);
        //		return new PagedList<TaiSan>(taisans, pageIndex, pageSize);
        //	}
        //	return null;
        //}
        public virtual IPagedList<TaiSan> GetTaiSanCanhBao(decimal? DonViId, string MA, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            var listCauHinhCanhBaoTaiSan = _cauHinhService.LoadCauHinh<ListCauHinhCanhBaoTaiSan>();
            if (listCauHinhCanhBaoTaiSan != null && !string.IsNullOrEmpty(listCauHinhCanhBaoTaiSan.JsonValue))
            {
                List<CauHinhCanhBaoTaiSan> cauHinhCanhBaoTaiSans = listCauHinhCanhBaoTaiSan.JsonValue.toEntities<CauHinhCanhBaoTaiSan>();
                var cauHinhCanhBaoTaiSan = cauHinhCanhBaoTaiSans?.Where(p => p.MA == MA).FirstOrDefault();
                if (cauHinhCanhBaoTaiSan != null && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.SelectStatementQuery) && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.WhereStatementJson))
                {
                    var CauHinhDieuKienLoc = _cauHinhService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
                    var ListDieuKienLocTS = CauHinhDieuKienLoc.JsonValue.toEntities<DieuKienLocTaiSan>();
                    var whereStatements = cauHinhCanhBaoTaiSan.WhereStatementJson.toEntities<WhereStatement>();
                    var statement = cauHinhCanhBaoTaiSan.SelectStatementQuery + " where 1=1";
                    foreach (var whereStament in whereStatements)
                    {
                        var dieuKienLoc = ListDieuKienLocTS.FirstOrDefault(p => p.MA_LOC_TAI_SAN == whereStament.MA_DIEU_KIEN);
                        var where = string.Format(dieuKienLoc.VALUE, whereStament.TEN_TRUONG, whereStament.GIA_TRI_SO_SANH);
                        statement = statement + where;
                    }
                    statement = $"{statement} ";
                    //IQueryable<TaiSan> table = _itemRepository.Table.FromSql(statement);
                    var taisans = _dbContext.EntityFromSqlNoParams<TaiSan>(statement);
                    if (!string.IsNullOrEmpty(KeySearch))
                        taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));
                    if (DonViId > 0)
                    {
                        var list_donViId = _donViService.GetIQueryableIdDonViChild(DonViId.Value, true);
                        taisans = from q in taisans
                                  join dv in list_donViId
                                    on q.DON_VI_ID equals dv
                                  select q;
                    }
                    return new PagedList<TaiSan>(taisans, pageIndex, pageSize);
                }
            }
            return new PagedList<TaiSan>(new List<TaiSan>(), pageIndex, pageSize);
        }

        public virtual async Task<bool> IsHasCanhBao(decimal? DonViId)
        {
            var listCanhBao = await CountTaiSanCanhBao(DonViId);

            if (listCanhBao != null && listCanhBao.Count > 0 && listCanhBao.Count(p => p.CountAlert > 0) > 0)
                return true;
            return false;
        }

        public virtual async Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBaoChung(decimal? DonViId)
        {
            var cauHinhCanhBaoTaiSans = new List<CauHinhCanhBaoTaiSan>();

            //check cảnh báo địa bàn
            var CanhBaoDiaBan = await CauHinhTaiSanThayDoiDiaBan(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoDiaBan);

            //check cảnh báo hiện trạng
            var CanhBaoHienTrang = await CauHinhTaiSanSaiHienTrang(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoHienTrang);

            //check cảnh báo nguyên giá < 10tr
            var CanhBaoNguyenGia = await CauHinhTaiSanDuoi10Trieu(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoNguyenGia);

            //Check cảnh báo tài sản điều chuyển
            var CanhBaoTSDC = await CauHinhTaiSanNhanDieuChuyen(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoTSDC);

            //Check cảnh báo ô tô sai số chỗ ngồi
            var CanhBaoOtoSaiChoNgoi = await CauHinhTaiSanOtoSaiSoChoNgoi(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoOtoSaiChoNgoi);

            //Check cảnh báo tài sản chưa tính hao mòn
            var CanhBaoTaiSanChuaTinhHaoMon = await CauHinhTaiSanChuaTinhHaoMon(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoTaiSanChuaTinhHaoMon);
            return cauHinhCanhBaoTaiSans;
        }

        public virtual async Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBao(decimal? DonViId)
        {
            List<CauHinhCanhBaoTaiSan> cauHinhCanhBaoTaiSans = new List<CauHinhCanhBaoTaiSan>();
            //Thông báo chung
            var CanhBaoChung = CauHinhCanhBaoChung(DonViId);
            cauHinhCanhBaoTaiSans.Add(CanhBaoChung);
            return cauHinhCanhBaoTaiSans;
        }

        public virtual async Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBaoFromCauHinhDonVi(decimal? DonViId)
        {            //xe ô tô chuyên dùng, đổi thành cấu hình sau
            var listCauHinhCanhBaoTaiSan = _cauHinhService.LoadCauHinh<ListCauHinhCanhBaoTaiSan>();
            if (listCauHinhCanhBaoTaiSan != null && !string.IsNullOrEmpty(listCauHinhCanhBaoTaiSan.JsonValue))
            {
                List<CauHinhCanhBaoTaiSan> cauHinhCanhBaoTaiSans = listCauHinhCanhBaoTaiSan.JsonValue.toEntities<CauHinhCanhBaoTaiSan>();
                foreach (var cauHinhCanhBaoTaiSan in cauHinhCanhBaoTaiSans)
                {
                    if (cauHinhCanhBaoTaiSan != null && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.SelectStatementQuery) && !string.IsNullOrEmpty(cauHinhCanhBaoTaiSan.WhereStatementJson))
                    {
                        var CauHinhDieuKienLoc = _cauHinhService.LoadCauHinh<CauHinhDieuKienLocTaiSan>();
                        var ListDieuKienLocTS = CauHinhDieuKienLoc.JsonValue.toEntities<DieuKienLocTaiSan>();
                        var whereStatements = cauHinhCanhBaoTaiSan.WhereStatementJson.toEntities<WhereStatement>();
                        var statement = cauHinhCanhBaoTaiSan.SelectStatementQuery + " where 1=1";
                        foreach (var whereStament in whereStatements)
                        {
                            var dieuKienLoc = ListDieuKienLocTS.FirstOrDefault(p => p.MA_LOC_TAI_SAN == whereStament.MA_DIEU_KIEN);
                            var where = string.Format(dieuKienLoc.VALUE, whereStament.TEN_TRUONG, whereStament.GIA_TRI_SO_SANH);
                            statement = statement + where;
                        }
                        statement = $"{statement} ";
                        //IQueryable<TaiSan> table = _itemRepository.Table.FromSql(statement);
                        var taisans = _dbContext.EntityFromSqlNoParams<TaiSan>(statement);
                        if (DonViId > 0)
                        {
                            var list_donViId = _donViService.GetIQueryableIdDonViChild(DonViId.Value, true);
                            taisans = from q in taisans
                                      join dv in list_donViId
                                        on q.DON_VI_ID equals dv
                                      select q;
                        }
                        cauHinhCanhBaoTaiSan.CountAlert = taisans.Count();
                    }
                }
                return cauHinhCanhBaoTaiSans;
            }

            return null;
        }

        #region Service cấu hình cảnh báo logic chung

        /// <summary>
        /// Bao gồm 3 loại cảnh báo: cảnh báo hiện trạng, cảnh báo địa bàn, cảnh báo nguyên giá dưới 10tr
        /// Hiển thị tại trang chủ để tránh lag
        /// </summary>
        /// <param name="DonViId"></param>
        /// <returns></returns>
        public virtual CauHinhCanhBaoTaiSan CauHinhCanhBaoChung(decimal? DonViId)
        {
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoLogicChung.ActionUrl;
            cauHinhCanhBaoTaiSan.CountAlert = CauHinhCanhBaoLogicChung.Count;
            cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoLogicChung.DisplayName;
            cauHinhCanhBaoTaiSan.IsHideCount = true;
            return cauHinhCanhBaoTaiSan;
        }

        #endregion Service cấu hình cảnh báo logic chung

        #region Service Cấu hình Thay đổi địa bàn tài sản

        public virtual async Task<int> CountTaiSanCanThayDoiDiaBan(string strDonViId = null, int LoaiHinhTaiSanId = 0)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: false, storeName: CauHinhCanhBaoDiaBan.StoreName);
            return await GetCountResultFromStore(storeCount, p1, p2, p3);
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanCanThayDoiDiaBan(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: false, storeName: CauHinhCanhBaoDiaBan.StoreName);
            string storeGetTaiSan = CauHinhCanhBaoLogicChung.GetStoreName(isCount: false, isDpac: false, storeName: CauHinhCanhBaoDiaBan.StoreName);

            var taisans = GetTaiSansFromStore(storeGetTaiSan, p1, p2, p3);
            var SlTaiSan = await GetCountResultFromStore(storeCount, p1, p2, p3);
            if (!string.IsNullOrEmpty(KeySearch))
                taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }

            return new PagedList<TaiSan>(taisans, pageIndex, pageSize, false, SlTaiSan);
        }

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanThayDoiDiaBan(decimal? DonViId)
        {
            var result = await CountTaiSanCanThayDoiDiaBan();
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoDiaBan.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoDiaBan.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        #endregion Service Cấu hình Thay đổi địa bàn tài sản

        #region Service cấu hình số chỗ ngồi ô tô

        public virtual async Task<int> CountTaiSanOtoSaiSoChoNgoi(string strDonViId = null, int LoaiHinhTaiSanId = 0)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoOtoSaiChoNgoi.StoreName);
            return await GetCountResultFromStore(storeCount, p1, p2, p3);
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanOtoSaiSoChoNgoi(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoOtoSaiChoNgoi.StoreName);
            string storeGetTaiSan = CauHinhCanhBaoLogicChung.GetStoreName(isCount: false, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoOtoSaiChoNgoi.StoreName);

            var taisans = GetTaiSansFromStore(storeGetTaiSan, p1, p2, p3);
            var SlTaiSan = await GetCountResultFromStore(storeCount, p1, p2, p3);
            if (!string.IsNullOrEmpty(KeySearch))
                taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }

            return new PagedList<TaiSan>(taisans, pageIndex, pageSize, false, SlTaiSan);
        }

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanOtoSaiSoChoNgoi(decimal? DonViId)
        {
            var result = await CountTaiSanOtoSaiSoChoNgoi();
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoOtoSaiChoNgoi.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoOtoSaiChoNgoi.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        #endregion Service cấu hình số chỗ ngồi ô tô

        #region Service cấu hình tài sản chưa tính hao mòn

        public virtual async Task<int> CountTaiSanChuaTinhHaoMon(string strDonViId = null, int LoaiHinhTaiSanId = 0)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanChuaTinhHaoMon.StoreName);
            return await GetCountResultFromStore(storeCount, p1, p2, p3);
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanChuaTinhHaoMon(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanChuaTinhHaoMon.StoreName);
            string storeGetTaiSan = CauHinhCanhBaoLogicChung.GetStoreName(isCount: false, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanChuaTinhHaoMon.StoreName);

            var taisans = GetTaiSansFromStore(storeGetTaiSan, p1, p2, p3);
            var SlTaiSan = await GetCountResultFromStore(storeCount, p1, p2, p3);
            if (!string.IsNullOrEmpty(KeySearch))
                taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }

            return new PagedList<TaiSan>(taisans, pageIndex, pageSize, false, SlTaiSan);
        }

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanChuaTinhHaoMon(decimal? DonViId)
        {
            var result = await CountTaiSanChuaTinhHaoMon();
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoTaiSanChuaTinhHaoMon.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoTaiSanChuaTinhHaoMon.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        #endregion Service cấu hình tài sản chưa tính hao mòn

        #region Serivice Cấu hình Thay đổi Hiện trạng tài sản

        public virtual async Task<int> CountTaiSanSaiHienTrang(string strDonViId = null, int LoaiHinhTaiSanId = 0)
        {
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);

            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            string storeName = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoHienTrang.StoreName);
            return await GetCountResultFromStore(storeName, p1, p2, p3);
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanSaiHienTrang(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            bool lIS_DPAC = CheckIsDpac(ref strDonViId);

            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            IQueryable<TaiSan> taisans;
            int SlTaiSan;
            // Get storeName
            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoHienTrang.StoreName);
            string storeGetTaiSan = CauHinhCanhBaoLogicChung.GetStoreName(isCount: false, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoHienTrang.StoreName);
            taisans = GetTaiSansFromStore(storeGetTaiSan, p1, p2, p3);
            SlTaiSan = await GetCountResultFromStore(storeCount, p1, p2, p3);

            if (!string.IsNullOrEmpty(KeySearch))
                taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }
            return new PagedList<TaiSan>(taisans, pageIndex, pageSize, false, SlTaiSan);
        }

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanSaiHienTrang(decimal? DonViId)
        {
            var result = await CountTaiSanSaiHienTrang();
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoHienTrang.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoHienTrang.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        #endregion Serivice Cấu hình Thay đổi Hiện trạng tài sản

        #region Serivice Cấu hình Canh Báo tài sản dưới 10 triệu

        public virtual async Task<int> CountTaiSanDuoi10Trieu(string strDonViId = null, int LoaiHinhTaiSanId = 0)
        {
            #region Chuẩn bị param

            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            int IsQuanLyNhuTaiSanCoDinh = 0;
            if (!lIS_DPAC)
            {
                var donvi = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
                if (donvi != null)
                {
                    IsQuanLyNhuTaiSanCoDinh = (donvi.IS_TSQUAN_LY_NHU_TSCD ?? false) ? 1 : 0;
                }
            }

            // Get storeName
            string storeName = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanDuoi10Trieu.StoreName);
            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pIS_QUAN_LY_NHU_TSCD", OracleDbType.Int32, IsQuanLyNhuTaiSanCoDinh, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            #endregion Chuẩn bị param

            #region Get result From Store

            var result = await GetCountResultFromStore(storeName, p1, p2, p3, p4);
            return result;

            #endregion Get result From Store
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanDuoi10Trieu(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            #region Chuẩn bị param

            bool lIS_DPAC = CheckIsDpac(ref strDonViId);
            int IsQuanLyNhuTaiSanCoDinh = 0;
            if (!lIS_DPAC)
            {
                var donvi = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
                if (donvi != null)
                {
                    IsQuanLyNhuTaiSanCoDinh = (donvi.IS_TSQUAN_LY_NHU_TSCD ?? false) ? 1 : 0;
                }
            }

            // Get storeName
            string storeCount = CauHinhCanhBaoLogicChung.GetStoreName(isCount: true, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanDuoi10Trieu.StoreName);
            string storeGetTaiSan = CauHinhCanhBaoLogicChung.GetStoreName(isCount: false, isDpac: lIS_DPAC, storeName: CauHinhCanhBaoTaiSanDuoi10Trieu.StoreName);

            OracleParameter p1 = new OracleParameter("pID_DON_VI", OracleDbType.Varchar2, strDonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("pLOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiHinhTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("pIS_QUAN_LY_NHU_TSCD", OracleDbType.Int32, IsQuanLyNhuTaiSanCoDinh, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            #endregion Chuẩn bị param

            IQueryable<TaiSan> taisans;
            int SlTaiSan;
            taisans = GetTaiSansFromStore(storeGetTaiSan, p1, p2, p3, p4);
            SlTaiSan = await GetCountResultFromStore(storeCount, p1, p2, p3, p4);

            if (!string.IsNullOrEmpty(KeySearch))
                taisans = taisans.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }
            return new PagedList<TaiSan>(taisans, pageIndex, pageSize, false, SlTaiSan);
        }

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanDuoi10Trieu(decimal? DonViId)
        {
            var result = await CountTaiSanDuoi10Trieu();
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoTaiSanDuoi10Trieu.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoTaiSanDuoi10Trieu.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        #region Hàm tiện ích

        public async Task<int> GetCountResultFromStore(string store, params object[] parameters)
        {
            try
            {
                var result = await _dbContext.EntityFromStore<CountView>(store, parameters).FirstOrDefaultAsync();
                return result.COUNT_ROW;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IQueryable<TaiSan> GetTaiSansFromStore(string store, params object[] parameters)
        {
            var taisans = _dbContext.EntityFromStore1<TaiSan>(store, parameters);
            return taisans;
        }

        public bool CheckIsDpac(ref string strDonViId)
        {
            bool lIS_DPAC = false;
            if (string.IsNullOrEmpty(strDonViId))
            {
                strDonViId = _workContext.CurrentDonVi.ID.ToString();
            }
            var listDonViId = strDonViId.ToListInt();
            if (listDonViId != null && listDonViId.Contains((int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG))
            {
                lIS_DPAC = true;
            }
            return lIS_DPAC;
        }

        #endregion Hàm tiện ích

        #endregion Serivice Cấu hình Canh Báo tài sản dưới 10 triệu

        #region Cấu hình tài sản nhận điều chuyển

        public virtual async Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanNhanDieuChuyen(decimal? DonViId)
        {
            var result = await CountTaiSanNhanDieuChuyen(DonViId ?? 0);
            CauHinhCanhBaoTaiSan cauHinhCanhBaoTaiSan = new CauHinhCanhBaoTaiSan();
            if (result > 0)
            {
                cauHinhCanhBaoTaiSan.ActionUrl = CauHinhCanhBaoTaiSanDieuChuyen.ActionUrl;
                cauHinhCanhBaoTaiSan.CountAlert = result;
                cauHinhCanhBaoTaiSan.DisplayName = CauHinhCanhBaoTaiSanDieuChuyen.DisplayName;
            }

            return cauHinhCanhBaoTaiSan;
        }

        public virtual async Task<int> CountTaiSanNhanDieuChuyen(decimal donViId)
        {
            var lyDoTang = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
            var idLyDoDieuChuyen = _lyDoBienDongRepository.Table.Where(c => c.MA == enum_LY_DO_BIEN_DONG.DIEU_CHUYEN).FirstOrDefault()?.ID ?? 0;
            var query = _itemRepository.Table;
            var yeuCaus = _yeucauRepository.Table;
            query = from q in query
                    join yc in yeuCaus on q.ID equals yc.TAI_SAN_ID
                    where yc.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen
                          && yc.LOAI_BIEN_DONG_ID == lyDoTang
                          && (q.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET)
                          && yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET
                    select q;

            if (donViId > 0)
            {
                var list_donViId = _donViService.GetIQueryableIdDonViChild(donViId, true);
                query = from q in query
                        join dv in list_donViId
                            on q.DON_VI_ID equals dv
                        select q;
            }

            return await query.CountAsync();
        }

        public virtual async Task<IPagedList<TaiSan>> GetTaiSanNhanDieuChuyen(decimal donViId = 0, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null)
        {
            var lyDoTang = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;

            var idLyDoDieuChuyen = _lyDoBienDongRepository.Table.Where(c => c.MA == enum_LY_DO_BIEN_DONG.TIEP_NHAN).FirstOrDefault()?.ID ?? 0;
            var query = _itemRepository.Table;
            var yeuCaus = _yeucauRepository.Table;
            query = from q in query
                    join yc in yeuCaus on q.ID equals yc.TAI_SAN_ID
                    where yc.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen
                          && yc.LOAI_BIEN_DONG_ID == lyDoTang
                          && (q.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET)
                          && yc.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET
                    select q;

            if (donViId > 0)
            {
                var list_donViId = _donViService.GetIQueryableIdDonViChild(donViId, true);
                query = from q in query
                        join dv in list_donViId
                            on q.DON_VI_ID equals dv
                        select q;
            }
            int SlTaiSan;
            // Get storeName

            SlTaiSan = await CountTaiSanNhanDieuChuyen(donViId);

            if (!string.IsNullOrEmpty(KeySearch))
                query = query.Where(p => p.MA.Contains(KeySearch) || p.TEN.Contains(KeySearch));

            if (SlTaiSan < pageSize)
            {
                pageSize = SlTaiSan;
            }
            return new PagedList<TaiSan>(query, pageIndex, pageSize, false, SlTaiSan);
        }

        #endregion Cấu hình tài sản nhận điều chuyển

        /// <summary>
        /// get tất cả tài sản cần đồng bộ
        /// </summary>
        /// <param name="DonViId"></param>
        /// <returns></returns>
        public List<TaiSan> GetTaiSanCanDongBo(decimal DonViId = 0, decimal NguonTaiSanId = 0)
        {
            List<TaiSan> taiSans = new List<TaiSan>();
            var query = _itemRepository.Table;
            if (NguonTaiSanId == 0)
            {
                query = query.Where(m => m.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.DKTS40);
            }
            else
            {
                query = query.Where(m => m.NGUON_TAI_SAN_ID == NguonTaiSanId);
            }
            if (DonViId > 0)
            {
                query = query.Where(m => m.DON_VI_ID == DonViId);
                //var list_donViId = _donViService.GetListIdDonViChild(DonViId, true);
                //query = from q in query
                //        join dv in list_donViId
                //            on q.DON_VI_ID equals dv
                //        select q;
            }
            var listNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(trangThai: (int)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO).Select(m => m.TAI_SAN_ID);
            query = from q in query
                    join nk in listNhatKy
                        on q.ID equals nk
                    select q;
            return query.ToList();
        }

        /// <summary>
        /// get tất cả tài sản cần lấy mã
        /// </summary>
        /// <param name="DonViId"></param>
        /// <returns></returns>
        public List<TaiSan> GetTaiSanCanLayMa(string DonViMa = null, decimal NguonTaiSanId = 0)
        {
            List<TaiSan> taiSans = new List<TaiSan>();
            var query = _itemRepository.Table.Where(x => x.MA_DB == null);
            if (!string.IsNullOrEmpty(DonViMa))
            {
                query = query.Where(m => m.MA.StartsWith(DonViMa));
            }
            if (NguonTaiSanId == 0)
            {
                query = query.Where(m => m.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.DKTS40);
            }
            else
            {
                query = query.Where(m => m.NGUON_TAI_SAN_ID == NguonTaiSanId);
            }
            //var listNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(trangThai: (int)enumTrangThaiTaiSanNhatKy.CHO_GET_MA).Select(m => m.TAI_SAN_ID);
            //query = from q in query
            //        join nk in listNhatKy
            //            on q.ID equals nk
            //        select q;
            return query.ToList();
        }

        #endregion Methods

        #region Khai thác tài sản

        public virtual IPagedList<TaiSan> SearchTaiSanKhaiThac(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal TRANG_THAI_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0, DateTime? Fromdate = null, DateTime? Todate = null, decimal? idKhaiThac = 0, decimal? donvikhaithacid = null, string tenTaiSan = null, string maTaiSan = null)
        {
            var query = _itemRepository.Table;
            var khaiThacChiTiet = _khaiThacChiTietRepository.Table;
            query = (from q in query
                     join ktct in khaiThacChiTiet on q.ID equals ktct.ID
                     select q).Distinct();

            if (!String.IsNullOrEmpty(Keysearch))
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower())
                || c.MA.ToLower().Contains(Keysearch.ToLower()));

            if (!String.IsNullOrEmpty(tenTaiSan))
                query = query.Where(c => c.TEN.ToLower().Contains(tenTaiSan.ToLower()));

            if (!String.IsNullOrEmpty(maTaiSan))
                query = query.Where(c => c.MA.ToLower().Contains(maTaiSan.ToLower()));

            if (LOAI_HINH_TAI_SAN_ID > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LOAI_HINH_TAI_SAN_ID);
            }
            if (Fromdate.HasValue)
            {
                var _tungay = Fromdate.Value.Date;
                query = query.Where(x => x.NGAY_SU_DUNG >= _tungay);
            }
            if (Todate.HasValue && Todate != DateTime.MinValue)
            {
                var _denngay = Todate.Value.Date.AddDays(1);
                query = query.Where(x => x.NGAY_SU_DUNG < _denngay);
            }
            if (donvikhaithacid > 0)
            {
                query = query.Where(m => m.DON_VI_ID == donvikhaithacid);
            }
            query = query.OrderByDescending(c => c.NGAY_SU_DUNG).ThenByDescending(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.MA);
            return new PagedList<TaiSan>(query, pageIndex, pageSize);
        }

        #endregion Khai thác tài sản

        #region Đồng bộ tài sản cũ

        public List<TaiSan> GetListTaiSanTheoDonVi(int DonViId)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET && c.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.DKTS40);
            var list_donViId = _donViService.GetListIdDonViChild(DonViId, true);
            query = from q in query
                    join dv in list_donViId
                        on q.DON_VI_ID equals dv
                    select q;
            return query.ToList();
        }

        #endregion Đồng bộ tài sản cũ

        #region Tool xóa tài sản

        public Boolean XoaTaiSanByDonViId(string DonViMa, bool IsDeleteDVCon, DateTime NgayTao)
        {
            OracleParameter p1 = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, DonViMa, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("P_IS_DELETE_DON_VI_CON", OracleDbType.Int32, IsDeleteDVCon.ToNumberInt32(), ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("P_NGAY_XOA", OracleDbType.Date, NgayTao, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var model = _dbContext.EntityFromStore<ResultCallStore>("PK_TS_NGHIEPVU.SP_DELETE_TAI_SAN", p1, p2, p3, p4).FirstOrDefault();
                if (model != null)
                {
                    return model.Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Tool xóa tài sản

        #region chotHM

        public virtual bool ChotToanBoHaoMonTS(decimal idTS)
        {
            OracleParameter pDonViId = new OracleParameter("PDON_VI_ID", OracleDbType.Int32, 0, ParameterDirection.Input); ;
            OracleParameter pNam = new OracleParameter("PNAM_KE_KHAI", OracleDbType.Int32, 0, ParameterDirection.Input); ;
            OracleParameter pTaiSanId = new OracleParameter("PTAI_SAN_ID", OracleDbType.Int32, idTS, ParameterDirection.Input); ;
            OracleParameter pLoaiHinhTaiSan = new OracleParameter("PLOAI_HINH_TAI_SAN_ID", OracleDbType.Int32, 0, ParameterDirection.Input); ;
            try
            {
                var result = _dbContext.ExecuteSqlCommand("BEGIN sp_chot_toan_bo_tai_san_v2(:PDON_VI_ID,:PNAM_KE_KHAI,:PTAI_SAN_ID,:PLOAI_HINH_TAI_SAN_ID); END;", false, null, pDonViId, pNam, pTaiSanId, pLoaiHinhTaiSan);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion chotHM
    }
}