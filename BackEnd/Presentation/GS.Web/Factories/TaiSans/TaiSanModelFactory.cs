//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using Castle.Core.Internal;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BaoCaos;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Framework.Extensions;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GS.Web.Models.TaiSans.TSLogicSoLieuDauKyModel;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanModelFactory : ITaiSanModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanService _itemService;
        private readonly IHinhThucMuaSamModelFactory _hinhthucmuasamModelFactory;
        private readonly IMucDichSuDungModelFactory _mucdichsudungModelFactory;
        private readonly IDonViBoPhanModelFactory _donvibophanModelFactory;
        private readonly IQuocGiaModelFactory _quocgiaModelFactory;
        private readonly INguonVonModelFactory _nguonvonModelFactory;
        private readonly IYeuCauNhatKyModelFactory _yeuCauNhatKyModelFactory;
        private readonly INguonVonService _nguonvonService;
        private readonly IDonViService _donviService;
        private readonly ILoaiTaiSanService _loaitaisanService;
        private readonly ITaiSanNguonVonService _taisannguonvonService;
        private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
        private readonly ILyDoBienDongModelFactory _lydobiendongModelFactory;
        private readonly ILyDoBienDongService _lydobiendongService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IYeuCauNhatKyService _yeuCauNhatKyService;
        private readonly IHoatDongService _hoatDongService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IDeNghiXuLyTaiSanService _deNghiXuLyTaiSanService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IKhaiThacChiTietServices _khaiThacChiTietServices;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly ITaiSanKhauHaoService _taiSanKhauHaoService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly ILoaiDonViModelFactory _loaiDonViModelFactory;
        //Yeu cau
        private readonly IYeuCauService _yeucauService;

        private readonly IYeuCauModelFactory _yeucauModelFactory;
        private readonly IYeuCauChiTietService _yeucauchitietService;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly INhanHienThiService _nhanhienthiService;
        private readonly IDonViBoPhanService _donvibophanService;
        private readonly ICheDoHaoMonService _chedohaomonService;
        private readonly IYeuCauNhatKyModelFactory _yeucaunhatkyModelFactory;
        private readonly IYeuCauNhatKyService _yeucaunhatkyService;
        private readonly ITrungGianBDYCService _trungGianBDYCService;

        //cac loai tai san
        private readonly ITaiSanNhaService _taisannhaService;

        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly IBienDongService _biendongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ILoaiTaiSanVoHinhModelFactory _loaiTaiSanVoHinhModelFactory;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ICauHinhService _cauHinhService;

        //----
        private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;

        private readonly IHaoMonTaiSanService _haoMonTaiSanService;

        private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;
        private readonly IDuAnModelFactory _duAnModelFactory;
        private readonly IDiaBanService _diaBanService;

        private readonly IKhaiThacService _khaiThacService;
        private readonly IBaoCaoTraCuuService _baoCaoTraCuuService;

        #endregion Fields

        #region Ctor

        public TaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanService itemService,
            IHinhThucMuaSamModelFactory hinhthucmuasamModelFactory,
            IMucDichSuDungModelFactory mucdichsudungModelFactory,
            IDonViBoPhanModelFactory donvibophanModelFactory,
            IQuocGiaModelFactory quocgiaModelFactory,
            INguonVonModelFactory nguonvonModelFactory,
            INguonVonService nguonvonService,
            ILoaiTaiSanService loaiTaiSanService,
            ITaiSanNguonVonService taisannguonvonService,
            ILoaiTaiSanModelFactory loaitaisanModelFactory,
            IDonViService donviService,
            ILyDoBienDongModelFactory lydobiendongModelFactory,
            IYeuCauService yeucauService,
            IYeuCauChiTietService yeucauchitietService,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            INhanHienThiService nhanhienthiService,
            IDonViBoPhanService donvibophanService,
            IYeuCauModelFactory yeucauModelFactory,
            ICheDoHaoMonService chedohaomonService,
            ITaiSanNhaService taisannhaService,
            ILyDoBienDongService lydobiendongService,
            IBienDongService biendongService,
            INguoiDungService nguoiDungService,
            ITaiSanDatService taiSanDatService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanVktService taiSanVktService,
            ITaiSanClnService taiSanClnService,
            ITaiSanMayMocService taiSanMayMocService,
            IBienDongChiTietService bienDongChiTietService,
            IYeuCauNhatKyModelFactory yeuCauNhatKyModelFactory,
            IYeuCauNhatKyService yeuCauNhatKyService,
            IHoatDongService hoatDongService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ILoaiTaiSanVoHinhModelFactory loaiTaiSanVoHinhModelFactory,
            IYeuCauNhatKyModelFactory yeucaunhatkyModelFactory,
            IYeuCauNhatKyService yeucaunhatkyService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            ICauHinhService cauHinhService,
            ITrungGianBDYCService trungGianBDYCService,
            ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhaiThacTaiSanService khaiThacTaiSanService,
            IDuAnModelFactory duAnModelFactory,
            IDiaBanService diaBanService,
            IDonViModelFactory donViModelFactory,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IKhaiThacService khaiThacService,
            IDeNghiXuLyTaiSanService deNghiXuLyTaiSanService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IKhaiThacChiTietServices khaiThacChiTietServices,
            ILoaiDonViService loaiDonViService,
            ITaiSanKhauHaoService taiSanKhauHaoService,
            IBaoCaoTraCuuService baoCaoTraCuuService,
            INhanHienThiService nhanHienThiService,
            ILoaiDonViModelFactory loaiDonViModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._hinhthucmuasamModelFactory = hinhthucmuasamModelFactory;
            this._mucdichsudungModelFactory = mucdichsudungModelFactory;
            this._donvibophanModelFactory = donvibophanModelFactory;
            this._quocgiaModelFactory = quocgiaModelFactory;
            this._nguonvonModelFactory = nguonvonModelFactory;
            this._nguonvonService = nguonvonService;
            this._loaitaisanService = loaiTaiSanService;
            this._taisannguonvonService = taisannguonvonService;
            this._loaitaisanModelFactory = loaitaisanModelFactory;
            this._donviService = donviService;
            this._lydobiendongModelFactory = lydobiendongModelFactory;
            this._yeucauService = yeucauService;
            this._yeucauchitietService = yeucauchitietService;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._nhanhienthiService = nhanhienthiService;
            this._donvibophanService = donvibophanService;
            this._yeucauModelFactory = yeucauModelFactory;
            this._chedohaomonService = chedohaomonService;
            this._taisannhaService = taisannhaService;
            this._lydobiendongService = lydobiendongService;
            this._biendongService = biendongService;
            this._nguoiDungService = nguoiDungService;
            this._taiSanDatService = taiSanDatService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanClnService = taiSanClnService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._yeuCauNhatKyModelFactory = yeuCauNhatKyModelFactory;
            this._yeuCauNhatKyService = yeuCauNhatKyService;
            this._hoatDongService = hoatDongService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._loaiTaiSanVoHinhModelFactory = loaiTaiSanVoHinhModelFactory;
            this._yeucaunhatkyModelFactory = yeucaunhatkyModelFactory;
            this._yeucaunhatkyService = yeucaunhatkyService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._cauHinhService = cauHinhService;
            this._trungGianBDYCService = trungGianBDYCService;
            this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khaiThacTaiSanService = khaiThacTaiSanService;
            this._duAnModelFactory = duAnModelFactory;
            this._diaBanService = diaBanService;
            this._donViModelFactory = donViModelFactory;
            this._khaiThacService = khaiThacService;
            this._deNghiXuLyTaiSanService = deNghiXuLyTaiSanService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._khaiThacChiTietServices = khaiThacChiTietServices;
            this._loaiDonViService = loaiDonViService;
            _taiSanKhauHaoService = taiSanKhauHaoService;
            this._baoCaoTraCuuService = baoCaoTraCuuService;
            this._nhanHienThiService = nhanHienThiService;
            this._loaiDonViModelFactory = loaiDonViModelFactory;
        }

        #endregion Ctor

        #region TaiSan

        public TaiSanSearchModel PrepareTaiSanSearchModel(TaiSanSearchModel searchModel, bool isDuyetBienDong = false, bool isExcutedTSDT = false)  // isExcutedTSDT : Loại bỏ tài sản đặc thù nếu đơn vị không có
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //điều chuyển 1 phần chỉ điều chuyển đất nhà
            if (searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
            {
                List<int> excludeLHTS = new List<int>() {
                    (int)enumLOAI_HINH_TAI_SAN.DAC_THU,
                    (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.OTO,
                    (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC,
                    (int)enumLOAI_HINH_TAI_SAN.VO_HINH,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.KHAC,
                    // bỏ 2 nhóm tài sản cđ khác và tài sản quản lý như tscđ
                    (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD

                };
                searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: excludeLHTS.ToArray());
            }
            //tài sản đặc thù chỉ được thêm ở giảm toàn bộ
            else if (searchModel.Loai_Ly_Do_ID != (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && searchModel.Loai_Ly_Do_ID != 0)
            {
                searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID)
                                                        .ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                                                                                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                                                                                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                                                                                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                                                                                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                                                                                    (int)enumLOAI_HINH_TAI_SAN.DAC_THU, (int)enumLOAI_HINH_TAI_SAN.KHAC,
                                                                                     (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD });
            }
            else
            {
                // nếu đơn vị không quy định tài sản đặc thù thì ẩn đi
                if (isExcutedTSDT)
                {
                    searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID)
                                                                   .ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.DAC_THU});
                }
                else
                {
                    searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID)
                                                                   .ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC,
                                                                                                               (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD            });
                }

            }

            int[] trangThaiNhapId = new int[] { };
            if (searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI
                || searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI
                || searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN
                || searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO
                || searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                trangThaiNhapId = new int[] { (int)enumTRANG_THAI_TAI_SAN.NHAP, (int)enumTRANG_THAI_TAI_SAN.NHAP_LIEU, (int)enumTRANG_THAI_TAI_SAN.XOA, (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO, (int)enumTRANG_THAI_TAI_SAN.CHO_DONG_BO, (int)enumTRANG_THAI_TAI_SAN.TRA_LAI };
            }
            else
            {
                trangThaiNhapId = new int[] { (int)enumTRANG_THAI_TAI_SAN.NHAP, (int)enumTRANG_THAI_TAI_SAN.NHAP_LIEU, (int)enumTRANG_THAI_TAI_SAN.XOA, (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO, (int)enumTRANG_THAI_TAI_SAN.CHO_DONG_BO };
            }
            searchModel.Trangthailist = ((enumTRANG_THAI_TAI_SAN)searchModel.enumtrangthaitaisan).ToSelectList(valuesToExclude: trangThaiNhapId);
            if (searchModel.donviId == null || searchModel.donviId < 0)
                searchModel.donviId = _workContext.CurrentDonVi.ID;
            searchModel.isNhapLieu = _workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU;
            searchModel.BoPhanSuDungAvailable = _donvibophanModelFactory.PrepareSelectListDonViBoPhan(DonViId: searchModel.donviId, isAddFirst: true, valSelected: searchModel.DON_VI_BO_PHAN_ID);
            if (isDuyetBienDong)
            {
                searchModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.ALL;

                var dv = _donviService.GetDonViById(searchModel.donviId ?? 0);
                searchModel.TenDonVi = dv.TEN;
            }

            #region Cấu hình duyệt

            var donViId = _workContext.CurrentDonVi.ID;
            var donViCauHinh = _cauHinhService.LoadCauHinhDonViBo<CauHinhTuDongDuyet>(DON_VI_ID: donViId);
            if (donViCauHinh != null)
            {
                var list = donViCauHinh.IsAutoDuyetTaiSanDuoi500.toEntities<CauHinhDuyetTaiSan>();
                //nếu tất cả loại tàn sản đều để tự động duyệt thì sẽ không hiển thị trên 500 dưới 500
                if (list.Where(p => p.IS_AUTO_DUYET_DUOI_NG == false).Count() == 0)
                    searchModel.IsAutoDuyetTaiSanDuoi500 = true;
            }

            #endregion Cấu hình duyệt

            if (searchModel.idKhaiThac > 0)
            {
                List<int> excludeLHTS = new List<int>() {
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                    (int)enumLOAI_HINH_TAI_SAN.KHAC
                };
                searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: excludeLHTS.ToArray());
                var donvi = _workContext.CurrentDonVi;
            }
            searchModel.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            searchModel.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList().ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        /// <summary>
        /// Sử dụng danh sách duyệt tài sản
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public TaiSanListModel PrepareTaiSanListModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.donviId == null)
                searchModel.donviId = _workContext.CurrentDonVi.ID;

            //get items
            bool isDieuChuyenMotPhan = false;
            if (searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                isDieuChuyenMotPhan = true;
            enumTS_NGUYEN_GIA nguyenGia = enumTS_NGUYEN_GIA.TAT_CA;
            if (searchModel.isduoi500)
                nguyenGia = enumTS_NGUYEN_GIA.DUOI_500_TRIEU;
            else if (searchModel.istren500)
                nguyenGia = enumTS_NGUYEN_GIA.TREN_500_TRIEU;
            var items = _itemService.SearchTaiSans(Keysearch: searchModel.KeySearch, TRANG_THAI_ID: searchModel.TRANG_THAI_ID,
                LOAI_HINH_TAI_SAN_ID: searchModel.LOAI_HINH_TAI_SAN_ID, DON_VI_BO_PHAN_ID: searchModel.DON_VI_BO_PHAN_ID,
                Fromdate: searchModel.Fromdate, Todate: searchModel.Todate, donviId: searchModel.donviId,
                isDuyet: searchModel.isDuyet, strLoaiHinhTSIds: searchModel.strLoaiHinhTSIds, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, NguoiTaoId: searchModel.NguoiTaoId, Loai_Ly_Do_ID: searchModel.Loai_Ly_Do_ID, isToanQuoc: searchModel.isToanQuoc, IsExclueTSDKTS: searchModel.IsChonTaiSan,NguyenGia: nguyenGia);
            //prepare list model

            var listTaiSanModel = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = searchModel.Page;
                    if (c.NGUOI_TAO_ID != null)
                    {
                        var nguoidung = _nguoiDungService.GetNguoiDungById(c.NGUOI_TAO_ID);
                        m.NguoiTaoTen = nguoidung.TEN_DANG_NHAP;
                    }
                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                    {
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);
                    }
                    string l_lyDoHuy = _yeucauService.GetStringTuChoi(c.ID);
                    m.strLyDoTuChoi = l_lyDoHuy;
                    if (c.TRANG_THAI_ID != null)
                    {
                        m.tentrangthai = _nhanhienthiService.GetGiaTriEnum(c.TrangThaiTaiSan);
                    }
                    if (c.LOAI_TAI_SAN_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                            loaitaisan = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }
                    if (c.DON_VI_BO_PHAN_ID > 0)
                    {
                        var bophansudung = _donvibophanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
                        if (bophansudung != null)
                        {
                            m.TenBoPhanSuDung = bophansudung.TEN;
                        }
                    }
                    m.strNguyenGiaVN = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID).ToVNStringNumber();

                    //giá trị còn lại
                    m.strHM_GIA_TRI_CON_LAI = _itemService.Get_GTLC_Cua_TS(m.ID, DateTime.Now, true).ToVNStringNumber();

                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DAT_DIA_CHI = tsDat.DIA_CHI;
                    }
                    m.CountYeuCauTs = _trungGianBDYCService.CountBDYCTaiSan(taiSanId: c.ID);
                    var countCanDuyet = _yeucauService.CountYeuCauCuaTaiSan(taisanId: c.ID, statusIds: new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET });
                    if (countCanDuyet > 0)
                        m.IsHaveChuaDuyet = true;
                    var countDaDuyet = _biendongService.CountBienDongsByTaiSanId(taiSanId: c.ID);
                    if (countDaDuyet > 0)
                        m.IsHaveDaDuyet = true;

                    m.IsDisableTSDKTS = IsDisableTSDKTS(c.NGUON_TAI_SAN_ID);
                    m.TenNguonTaiSan = _nhanhienthiService.GetGiaTriEnum<enumNguonTaiSan>((enumNguonTaiSan)c.NGUON_TAI_SAN_ID);
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return listTaiSanModel;
        }
        private bool IsDisableTSDKTS(decimal nguonTS)
        {
            try
            {
                var TenCauHinh = "cauhinhchung.ChoPhepThayDoiThongTinDKTS";
                if (nguonTS != (int)enumNguonTaiSan.DKTS40)
                {
                    return false;
                }
                var cauHinh = _cauHinhService.SearchCauHinhs(Ten: TenCauHinh).FirstOrDefault();
                return !(bool.Parse(cauHinh.GIA_TRI));
            }
            catch (Exception)
            {

                return false;
            }


        }
        /// <summary>
        /// Note: Phục vụ danh sách tra cứu tài sản, danh sách tài sản
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public TaiSanListModel PrepareDanhSachTaiSan(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.donviId == null)
                searchModel.donviId = _workContext.CurrentDonVi.ID;
            //Xử lý xem có phải đang lọc theo Hao Mòn Hay không
            //Nếu isHaoMon != null thì loc -> ts thuộc đã duyệt
            //nếu isHaoMon != null có lọc tính hoặc chưa tính hao mòn
            bool isFilter = false;
            if (searchModel.isHaoMon != null)
            {
                searchModel.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                isFilter = true;
            }
            else
            {
                searchModel.isHaoMon = false;
            }
            //get items
            bool isDieuChuyenMotPhan = false;
            if (searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                isDieuChuyenMotPhan = true;
            List<decimal> taiSanIdExclude = new List<decimal>();
            if (searchModel.DE_NGHI_XU_LY_ID > 0)
                taiSanIdExclude = _deNghiXuLyTaiSanService.GetAllDeNghiXuLyTaiSans(DeNghiXuLyID: searchModel.DE_NGHI_XU_LY_ID).Select(c => c.TAI_SAN_ID).ToList();
            string[] arrayLoaiHinhTaiSanId = null;
            if (searchModel.strLoaiHinhTSIds != null)
                arrayLoaiHinhTaiSanId = searchModel.strLoaiHinhTSIds.Split(",");

            var items = _itemService.DanhSachTaiSans(Keysearch: searchModel.KeySearch, TRANG_THAI_ID: searchModel.TRANG_THAI_ID,
                LOAI_HINH_TAI_SAN_ID: searchModel.LOAI_HINH_TAI_SAN_ID, DON_VI_BO_PHAN_ID: searchModel.DON_VI_BO_PHAN_ID,
                Fromdate: searchModel.Fromdate, Todate: searchModel.Todate, donviId: searchModel.donviId,
                isDuyet: searchModel.isDuyet, strLoaiHinhTSIds: arrayLoaiHinhTaiSanId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, NguoiTaoId: searchModel.NguoiTaoId, Loai_Ly_Do_ID: searchModel.Loai_Ly_Do_ID, taiSanIdExclude: taiSanIdExclude, NguonTaiSanId: searchModel.NguonTaiSanId, isCheckDonVi: searchModel.isCheckDonVi, isToanQuoc: searchModel.isToanQuoc, IsChonTaiSan: searchModel.IsChonTaiSan, IsDanhSachTaiSanDuAn: searchModel.IsDanhSachTaiSanDuAn,
                IsFilter: isFilter,
                IsHaoMon: searchModel.isHaoMon
                );

            //prepare list model
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = searchModel.Page;
                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);
                    if (c.TRANG_THAI_ID != null)
                    {
                        m.tentrangthai = _nhanhienthiService.GetGiaTriEnum(c.TrangThaiTaiSan);
                    }
                    string l_lyDoHuy = _yeucauService.GetStringTuChoi(c.ID);
                    m.strLyDoTuChoi = l_lyDoHuy;
                    if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                        {
                            var x = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
                            if (x != null)
                                loaitaisan = x.ToModel<LoaiTaiSanModel>();
                        }

                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }
                    if (c.DON_VI_BO_PHAN_ID > 0)
                    {
                        var bophansudung = _donvibophanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
                        if (bophansudung != null)
                        {
                            m.TenBoPhanSuDung = bophansudung.TEN;
                        }
                    }
                    var NguyenGiaTS = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID);
                    m.strNguyenGiaVN = NguyenGiaTS.ToVNStringNumber();

                    //giá trị còn lại
                    m.strHM_GIA_TRI_CON_LAI = _itemService.Get_GTLC_Cua_TS(m.ID, DateTime.Now, true).ToVNStringNumber();
                    //if (searchModel.TRANG_THAI_ID == (int)GS.Core.Domain.TaiSans.enumTRANG_THAI_TAI_SAN.DA_DUYET)
                    //{
                    //  m.NguyenGiaTaiSan = _biendongService.GetBienDongMoiNhatByTaiSanId(c.ID).NGUYEN_GIA;
                    //}
                    //lay nguyengia ngoai list tai san
                    //if (m.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET)
                    //{
                    //    m.strNguyenGiaVN = _yeucauService.GetYeuCauByTaiSanId(c.ID).NGUYEN_GIA.ToVNStringNumber();
                    //}
                    //else
                    //{
                    //   m.strNguyenGiaVN = _biendongService.GetBienDongMoiNhatByTaiSanId(c.ID).NGUYEN_GIA.ToVNStringNumber();
                    //}
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DAT_DIA_CHI = tsDat.DIA_CHI;
                    }
                    m.CountYeuCauTs = _yeucauService.CountYeuCauCuaTaiSan(taisanId: c.ID);
                    var countCanDuyet = _yeucauService.CountYeuCauCuaTaiSan(taisanId: c.ID, statusIds: new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET });
                    if (countCanDuyet > 0)
                    {
                        m.IsHaveChuaDuyet = true;
                    }
                    m.IsDisableTSDKTS = IsDisableTSDKTS(c.NGUON_TAI_SAN_ID);
                    if (c.NGAY_SU_DUNG != null && DateTime.Now.Year - 1 - c.NGAY_SU_DUNG.Value.Year < c.SO_LUONG_HAO_MON_TAI_SAN)
                    {
                        m.IsTinhHaoMon = true;
                    }
                    else
                    {
                        m.IsTinhHaoMon = false;
                    }
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public TSLogicSoLieuDauKyListModel LogicDanhSachTaiSan(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var items = _baoCaoTraCuuService.PAGE_KTLOGIC_SO_LIEU_DAU_KY(
                   NgayKetThuc: searchModel.NgayKetThuc,
                   DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
                   stringLoaiTaiSan: searchModel.StringLoaiTaiSan,
                   LoaiDonViId: searchModel.LoaiTaiSanId,
                   //donViTien: searchModel.DonViTien,
                   //DonViDienTich: searchModel.DonViDienTich,
                   ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(),
                   SoTang_CompareSign: searchModel.SoTang_CompareSign,
                   SoTang_Value1: searchModel.SoTang_Value1,
                   SoTang_Value2: searchModel.SoTang_Value2,
                   DienTich_CompareSign: searchModel.DienTich_CompareSign,
                   DienTich_Value1: searchModel.DienTich_Value1,
                   DienTich_Value2: searchModel.DienTich_Value2,
                   SoChoNgoi_CompareSign: searchModel.SoChoNgoi_CompareSign,
                   SoChoNgoi_Value1: searchModel.SoChoNgoi_Value1,
                   SoChoNgoi_Value2: searchModel.SoChoNgoi_Value2,
                   TaiTrong_CompareSign: searchModel.TaiTrong_CompareSign,
                   TaiTrong_Value1: searchModel.TaiTrong_Value1,
                   TaiTrong_Value2: searchModel.TaiTrong_Value2,
                   TongNguyenGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
                   TongNguyenGia_Value1: searchModel.TongNguyenGia_Value1,
                   TongNguyenGia_Value2: searchModel.TongNguyenGia_Value2,
                   /*DonGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
                   DonGia_Value1: searchModel.TongNguyenGia_Value1,
                   DonGia_Value2: searchModel.TongNguyenGia_Value2,*/
                   stringLoaiDonVi: searchModel.StringLoaiDonVi,
                   ChucDanhId: searchModel.CHUC_DANH_ID,
                   pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            /*NhanXeId: searchModel.NHAN_XE_ID,
            NamSD_CompareSign: searchModel.NamSD_CompareSign,
            NamSD_Value1: searchModel.NamSD_Value1,
            NamSD_Value2: searchModel.NamSD_Value2,
            NamSx_CompareSign: searchModel.NamSx_CompareSign,
            NamSx_Value1: searchModel.NamSx_Value1,
            NamSx_Value2: searchModel.NamSx_Value2,*/
            //NguyenGiaNS_CompareSign: searchModel.NguyenGiaNS_CompareSign,
            //NguyenGiaNS_Value1: searchModel.NguyenGiaNS_Value1,
            //NguyenGiaNS_Value2: searchModel.NguyenGiaNS_Value2,
            //NguyenGiaKhac_CompareSign: searchModel.NguyenGiaKhac_CompareSign,
            //NguyenGiaKhac_Value1: searchModel.NguyenGiaKhac_Value1,
            //NguyenGiaKhac_Value2: searchModel.NguyenGiaKhac_Value2,
            //NguyenGiaVienTro_CompareSign: searchModel.NguyenGiaVienTro_CompareSign,
            //NguyenGiaVienTro_Value1: searchModel.NguyenGiaVienTro_Value1,
            //NguyenGiaVienTro_Value2: searchModel.NguyenGiaVienTro_Value2,
            //NguyenGiaODA_CompareSign: searchModel.NguyenGiaODA_CompareSign,
            //NguyenGiaODA_Value1: searchModel.NguyenGiaODA_Value1,
            //NguyenGiaODA_Value2: searchModel.NguyenGiaODA_Value2,
            //TyLeChatLuong_CompareSign: searchModel.TyLeChatLuong_CompareSign,
            //TyLeChatLuong_Value1: searchModel.TyLeChatLuong_Value1,
            //TyLeChatLuong_Value2: searchModel.TyLeChatLuong_Value2,
            //GTCL_CompareSign: searchModel.GTCL_CompareSign,
            //GTCL_Value1: searchModel.GTCL_Value2,
            //GTCL_Value2: searchModel.DienTich_Value2);
            //var a = new PagedList<TS_LOGIC_SO_LIEU_DAU_KY>(items, searchModel.Page, searchModel.PageSize);

            /*TSLogicSoLieuDauKyListModel models = new TSLogicSoLieuDauKyListModel
            {
                Data = items.Select(c => { var m = c.ToModel<TSLogicSoLieuDauKyModel>(); }.ToList(),
                Total = items.Count()
            };*/
            //var a = items.ToList();
            List<TSLogicSoLieuDauKyModel> list = new List<TSLogicSoLieuDauKyModel>();
            foreach (var i in items)
            {
                TSLogicSoLieuDauKyModel model = new TSLogicSoLieuDauKyModel();
                model.TAI_SAN_ID = i.TAI_SAN_ID;
                model.LOAI_HINH_TAI_SAN_ID = i.LOAI_HINH_TAI_SAN_ID;
                model.BIEN_DONG_ID = i.BIEN_DONG_ID;
                model.SO_LUONG = i.SO_LUONG;
                model.DIEN_TICH = i.DIEN_TICH;
                //model.NGUYEN_GIA_NGAN_SACH = i.NGUYEN_GIA_NGAN_SACH;
                //model.NGUYEN_GIA_KHAC = i.NGUYEN_GIA_KHAC;
                model.TEN_DON_VI = i.TEN_DON_VI;
                model.TEN_LOAI_TAI_SAN = i.TEN_LOAI_TAI_SAN;
                model.TAI_SAN_MA = i.TAI_SAN_MA;
                model.TAI_SAN_TEN = i.TAI_SAN_TEN;
                model.LOAI_TAI_SAN_ID = i.LOAI_TAI_SAN_ID;
                model.NAM_DUA_VAO_SD = i.NAM_DUA_VAO_SD;
                model.SO_CHO = i.SO_CHO;
                model.TAI_TRONG = i.TAI_TRONG;
                model.THONG_SO_KY_THUAT = i.THONG_SO_KY_THUAT;
                model.SO_TANG = i.SO_TANG;
                model.GIA_TRI_CON_LAI = i.GIA_TRI_CON_LAI;
                model.NGUYEN_GIA = i.NGUYEN_GIA;
                list.Add(model);
            }
            //IPagedList<TSLogicSoLieuDauKyModel> model = ;
            var models = new TSLogicSoLieuDauKyListModel
            {
                //fill in model values from the entity
                Data = list.ToList(),
                Total = items.TotalCount
            };
            return models;
        }
        public List<TaiSanExport> PrepareExportTaiSan(TaiSanSearchModel searchModel)
        {
            List<TaiSanExport> rs = new List<TaiSanExport>();
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.donviId == null)
                searchModel.donviId = _workContext.CurrentDonVi.ID;

            //get items
            bool isDieuChuyenMotPhan = false;
            if (searchModel.Loai_Ly_Do_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                isDieuChuyenMotPhan = true;
            List<decimal> taiSanIdExclude = new List<decimal>();
            if (searchModel.DE_NGHI_XU_LY_ID > 0)
                taiSanIdExclude = _deNghiXuLyTaiSanService.GetAllDeNghiXuLyTaiSans(DeNghiXuLyID: searchModel.DE_NGHI_XU_LY_ID).Select(c => c.TAI_SAN_ID).ToList();
            string[] arrayLoaiHinhTaiSanId = null;
            if (searchModel.strLoaiHinhTSIds != null)
                arrayLoaiHinhTaiSanId = searchModel.strLoaiHinhTSIds.Split(",");

            var items = _itemService.DanhSachTaiSans(Keysearch: searchModel.KeySearch, TRANG_THAI_ID: searchModel.TRANG_THAI_ID,
                LOAI_HINH_TAI_SAN_ID: searchModel.LOAI_HINH_TAI_SAN_ID, DON_VI_BO_PHAN_ID: searchModel.DON_VI_BO_PHAN_ID,
                Fromdate: searchModel.Fromdate, Todate: searchModel.Todate, donviId: searchModel.donviId,
                isDuyet: searchModel.isDuyet, strLoaiHinhTSIds: arrayLoaiHinhTaiSanId, pageIndex: searchModel.Page - 1, pageSize: int.MaxValue, NguoiTaoId: searchModel.NguoiTaoId, Loai_Ly_Do_ID: searchModel.Loai_Ly_Do_ID, taiSanIdExclude: taiSanIdExclude, NguonTaiSanId: searchModel.NguonTaiSanId, isToanQuoc: searchModel.isToanQuoc);

            //prepare list model
            rs = items.Select(c =>
               {
                   var NguyenGiaTS = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID);
                   var m = new TaiSanExport()
                   {
                       TEN = c.TEN,
                       MA = c.MA,
                       NGAY_SU_DUNG = c.NGAY_SU_DUNG,
                       NGUYEN_GIA = NguyenGiaTS
                   };
                   /*if (c.DON_VI_BO_PHAN_ID > 0)
                   {
                       var bophansudung = _donvibophanService.GetDonViBoPhanById(c.DON_VI_BO_PHAN_ID ?? 0);
                       if (bophansudung != null)
                       {
                           m.DON_VI_SU_DUNG = bophansudung.TEN;
                       }
                   }*/
                   if (c.DON_VI_ID > 0)
                   {
                       var donVi = _donviService.GetDonViById(c.DON_VI_ID ?? 0);
                       if (donVi != null)
                       {
                           m.DON_VI_SU_DUNG = donVi.TEN;
                       }
                   }
                   if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                   {
                       var loaitaisan = new LoaiTaiSanModel();
                       if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && c.LOAI_TAI_SAN_DON_VI_ID > 0)
                           loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(c.LOAI_TAI_SAN_DON_VI_ID ?? c.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                       else
                           loaitaisan = _loaitaisanService.GetLoaiTaiSanById(c.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                       m.LOAI_TAI_SAN = loaitaisan.TEN;
                   }
                   return m;
               }).ToList();
            return rs;
        }

        public List<TaiSanExport> PrepareExportTaiSanKiemTra(TaiSanSearchModel searchModel)
        {
            List<TaiSanExport> rs = new List<TaiSanExport>();
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.donviId == null)
                searchModel.donviId = _workContext.CurrentDonVi.ID;

            //get items
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }

            var items = _itemService.SearchAllTaiSanKiemTraTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: int.MaxValue, CapDonViSearch: searchModel.CapDonViSearch, listCapDonVis: searchModel.SelectedCapDonVis, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, donViId: searchModel.donviId, LoaiDonViSearch: searchModel.LoaiDonViSearch, MucDichSuDungSearch: searchModel.MucDichSuDungSearch, DienTich_CompareSign: searchModel.DienTich_CompareSign, DienTich_Value1: searchModel.DienTich_Value1, DienTich_Value2: searchModel.DienTich_Value2);

            //prepare list model
            rs = items.Select(c =>
            {
                var NguyenGiaTS = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID);
                var m = new TaiSanExport()
                {
                    TEN = c.TEN,
                    MA = c.MA,
                    NGAY_SU_DUNG = c.NGAY_SU_DUNG,
                    NGUYEN_GIA = NguyenGiaTS
                };
                if (c.DON_VI_ID > 0)
                {
                    var donVi = _donviService.GetDonViById(c.DON_VI_ID ?? 0);
                    if (donVi != null)
                    {
                        m.DON_VI_SU_DUNG = donVi.TEN;
                    }
                }
                if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                {
                    var loaitaisan = new LoaiTaiSanModel();
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && c.LOAI_TAI_SAN_DON_VI_ID > 0)
                        loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(c.LOAI_TAI_SAN_DON_VI_ID ?? c.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                    else
                        loaitaisan = _loaitaisanService.GetLoaiTaiSanById(c.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                    m.LOAI_TAI_SAN = loaitaisan.TEN;
                }
                return m;
            }).ToList();
            return rs;
        }
        //public TaiSanListModel PrepareDanhSachTaiSanGiamNhieu(TaiSanSearchModel searchModel)
        //{
        //	if (searchModel == null)
        //		throw new ArgumentNullException(nameof(searchModel));
        //}
        public TaiSanModel PrepareTaiSanModel(TaiSanModel model, TaiSan item, bool excludeProperties = false, List<decimal> lstTaiSanChon = null, bool? isCreateTSDA = false, bool? isCreateTaiSan = false, bool? tsQLNTSCD = false)
        {
            // don vi hien tai
            if (item == null)
            {
                var hasChildDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasChildDonViQLDA, isCreateTSDA: isCreateTSDA);
                model.DonViMa = donvi.MA;
                model.DonViTen = donvi.TEN;
                model.DON_VI_ID = donvi.ID;
                model.IsBanQuanLyDuAn = _donviService.isDonViBanQuanLyDuAn(model.DON_VI_ID.GetValueOrDefault());

                #region Nguồn vốn

                if (model.IsBanQuanLyDuAn == true)
                {
                    var lstNguonVon = _nguonvonService.GetAllNguonVonActive();
                    if (lstNguonVon != null && lstNguonVon.Count > 0)
                    {
                        model.lstNguonVonModel = lstNguonVon.Select(p => p.ToModel<NguonVonModel>()).ToList();
                        model.SelectedNguonVonIds = lstNguonVon.Select(p => Decimal.ToInt32(p.ID)).ToList();
                    }
                    //Prepare danh mục dự án
                    model.DuAnAvailable = _duAnModelFactory.PrepareSelectListDuAn(isAddFirst: true, donViId: model.DON_VI_ID);
                }
                else
                {
                    //Prepare add nguon von id,
                    //1: Nguồn ngân sách
                    //3: Nguồn khác
                    model.SelectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
                    model.lstNguonVonModel = _nguonvonModelFactory.PrepareListNguonVonDefault();
                }

                #endregion Nguồn vốn

                switch (model.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        model.LoaiTaiSanAvailable.AddFirstRow("--Chọn mục đích sử dụng");
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        model.LoaiTaiSanAvailable.AddFirstRow("--Chọn cấp nhà");
                        break;

                    default:
                        model.LoaiTaiSanAvailable.AddFirstRow("--Chọn loại tài sản");
                        break;
                }
                model.HinhThucMuaSamAvailable = _hinhthucmuasamModelFactory.PrepareSelectListHinhThucMuaSam(isAddFirst: true);
                model.MucDichDuocGiaoAvailable = _mucdichsudungModelFactory.PrepareSelectListMucDichSuDung(isAddFirst: true);
                model.BoPhanSuDungAvailable = _donvibophanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: model.DON_VI_BO_PHAN_ID);
                model.QuocGiaAvailable = _quocgiaModelFactory.PrepareSelectListQuocGias(valSelected: model.NUOC_SAN_XUAT_ID, IsAddFirst: true);
                model.LoaiHinhTaiSanAvailable = model.enumLoaiHinhTaiSan.ToSelectList();

                var donvikh = _donviService.GetDonViById(_workContext.CurrentDonVi.ID);
                model.CHE_DO_HACH_TOAN_ID = donvikh.CHE_DO_HACH_TOAN_ID ?? 0;
            }
            // trường hơp tăng mới toàn bộ
            else
            {
                //more
                var yeuCauId = model.YeuCauId;
                //fill in model values from the entity
                model = item.ToModel<TaiSanModel>();
                var dv = _donviService.GetDonViById(item.DON_VI_ID ?? 0);
                model.IsBanQuanLyDuAn = _donviService.isDonViBanQuanLyDuAn(dv.ID);

                model.DonViMa = dv.MA;
                model.DonViTen = dv.TEN;
                var yeucau = new YeuCau();
                if (yeuCauId > 0)
                    yeucau = _yeucauService.GetYeuCauById(yeuCauId ?? 0);
                else
                {
                    yeucau = _yeucauService.GetYeuCauCuNhatByTSId(item.ID);
                    model.YeuCauId = yeucau.ID;
                }
                model.HOA_HONG_NOP_NSNN = yeucau.HOA_HONG_NOP_NSNN;
                model.HOA_HONG_DE_LAI_DON_VI = yeucau.HOA_HONG_DE_LAI_DON_VI;
                model.TONG_HOA_HONG_CHIET_KHAU = (yeucau.HOA_HONG_NOP_NSNN ?? 0) + (yeucau.HOA_HONG_DE_LAI_DON_VI ?? 0);
                if (!String.IsNullOrEmpty(yeucau.NGUON_VON_JSON))
                {
                    model.lstNguonVonModel = yeucau.NGUON_VON_JSON.toEntities<NguonVonModel>();
                    model.SelectedNguonVonIds = model.lstNguonVonModel.Select(c => (int)c.ID).ToList();
                    model.strTaiSanNguonVonIds = String.Join(",", model.SelectedNguonVonIds);
                }
                else
                {
                    //Prepare add nguon von id
                    if (model.IsBanQuanLyDuAn == true)
                    {
                        var lstNguonVon = _nguonvonService.GetAllNguonVonActive();
                        if (lstNguonVon != null && lstNguonVon.Count > 0)
                        {
                            model.lstNguonVonModel = lstNguonVon.Select(p => p.ToModel<NguonVonModel>()).ToList();
                            model.SelectedNguonVonIds = lstNguonVon.Select(p => Decimal.ToInt32(p.ID)).ToList();
                        }
                        //Prepare danh mục dự án
                    }
                    else
                    {
                        //Prepare add nguon von id,
                        //1: Nguồn ngân sách
                        //3: Nguồn khác
                        model.SelectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
                        model.lstNguonVonModel = _nguonvonModelFactory.PrepareListNguonVonDefault();
                    }
                }
                if (item.NGAY_NHAP.HasValue)
                {
                    if (item.NGAY_NHAP.Value < new DateTime(2018, 01, 01))
                    {
                        model.isTangMoi = false;
                    }
                    else
                    {
                        model.isTangMoi = true;
                    }
                }
                model.nvYeuCauChiTietModel = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeucau.ID).ToModel<YeuCauChiTietModel>();
                if (model.IsBanQuanLyDuAn.GetValueOrDefault(false))
                {
                    if (item.DU_AN_ID > 0)
                        model.DuAnAvailable = _duAnModelFactory.PrepareSelectListDuAn(valSelected: item.DU_AN_ID, isAddFirst: true, donViId: model.DON_VI_ID);
                    else
                        model.DuAnAvailable = _duAnModelFactory.PrepareSelectListDuAn(isAddFirst: true, donViId: model.DON_VI_ID);
                }
                model.HinhThucMuaSamAvailable = _hinhthucmuasamModelFactory.PrepareSelectListHinhThucMuaSam(isAddFirst: true, valSelected: model.nvYeuCauChiTietModel.HINH_THUC_MUA_SAM_ID);
                model.MucDichDuocGiaoAvailable = _mucdichsudungModelFactory.PrepareSelectListMucDichSuDung(isAddFirst: true, valSelected: model.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID);
                model.BoPhanSuDungAvailable = _donvibophanModelFactory.PrepareSelectListDonViBoPhan(DonViId: model.DON_VI_ID, isAddFirst: true, valSelected: model.DON_VI_BO_PHAN_ID);
                model.QuocGiaAvailable = _quocgiaModelFactory.PrepareSelectListQuocGias(valSelected: model.NUOC_SAN_XUAT_ID, IsAddFirst: true);
                model.LyDoTangAvailable = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: item.LOAI_HINH_TAI_SAN_ID, isAddFirst: true, valSelected: yeucau.LY_DO_BIEN_DONG_ID, loailydoId: yeucau.LOAI_BIEN_DONG_ID, isTangMoi: model.isTangMoi);

                model.CHE_DO_HACH_TOAN_ID = dv.CHE_DO_HACH_TOAN_ID ?? 0;
                if (model.nvYeuCauChiTietModel.KH_GIA_TINH_KHAU_HAO != null)
                {
                    model.TRANG_THAI_KH = true;
                }
            }
            if (lstTaiSanChon != null)
            {
                if (lstTaiSanChon.Where(c => c == item.ID).Count() > 0)
                {
                    model.IdChon = true;
                }
            }
            //loại tài sản
            decimal CheDoId = _chedohaomonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                model.LoaiTaiSanAvailable = _loaitaisanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, valSelected: model.LOAI_TAI_SAN_ID, cheDoId: CheDoId, isCreateOrEditTaiSan: isCreateTaiSan, donViId: model.DON_VI_ID, tsQLNTSCD: tsQLNTSCD);
            else
                model.LoaiTaiSanAvailable = _loaiTaiSanVoHinhModelFactory.PrepareSelectListLoaiTaiSanDonVi(isAddFirst: true, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, valSelected: model.LOAI_TAI_SAN_DON_VI_ID, cheDoId: CheDoId, donViId: item.DON_VI_ID);

            if (model.LOAI_HINH_TAI_SAN_ID > 0)
            {
                model.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(model.enumLoaiHinhTaiSan);
            }
            model.NguonVonAvailable = _nguonvonModelFactory.PrepareMultiSelectNguonVon(valSelecteds: model.SelectedNguonVonIds);
            var don_vi = _donviService.GetDonViById(model.DON_VI_ID.Value);
            if (don_vi != null)
                model.DON_VI_CHE_DO_HACH_TOAN_ID = don_vi.CHE_DO_HACH_TOAN_ID ?? 0;

            if (model.HM_TY_LE.HasValue && !(model.HM_TY_LE.Value >= 0))
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    if (model.LOAI_TAI_SAN_DON_VI_ID.HasValue)
                    {
                        var loaiTSDV = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.LOAI_TAI_SAN_DON_VI_ID.Value);
                        model.HM_TY_LE = loaiTSDV?.HM_TY_LE;
                    }
                }
                else
                {
                    if (model.LOAI_TAI_SAN_ID.HasValue)
                    {
                        var lts = _loaitaisanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID.Value);
                        model.HM_TY_LE = lts?.HM_TY_LE;
                    }
                }
            }
            model.nvYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.nvYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
            //Kiểm tra tài sản đất có hồ sơ hay không
            //if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
            //{
            //	if (model.nvYeuCauChiTietModel.HS_QUYET_DINH_BAN_GIAO != null || model.nvYeuCauChiTietModel.HS_QUYET_DINH_CHO_THUE_SO != null || model.nvYeuCauChiTietModel.HS_QUYET_DINH_GIAO_SO != null || model.nvYeuCauChiTietModel.HS_CNQSD_SO != null || model.nvYeuCauChiTietModel.HS_HOP_DONG_CHO_THUE_SO != null || model.nvYeuCauChiTietModel.HS_PHAP_LY_KHAC != null )
            //	{
            //		model.taisandatModel.cohoso = true;
            //	}
            //}
            // Chuẩn bị phương thức mua sắm
            model.DDLPhuongThucMuaSam = ((enumPHUONG_THUC_MUA_SAM)(model.PHUONG_THUC_MUA_SAM_ID ?? 0)).ToSelectList().ToListSelectListItem();
            model.DDLPhuongThucMuaSam.Insert(0, new SelectListItem() { Value = "0", Text = "---Chọn phương thức mua sắm---", Selected = model.PHUONG_THUC_MUA_SAM_ID == null ? true : false });
            model.TenDonViMuaSamTapTrung = _donviService.GetDonViById(model.DON_VI_MUA_SAM_TAP_TRUNG_ID ?? 0)?.TEN;
            model.isCreateTSDA = isCreateTSDA;
            return model;
        }

        /// <summary>
        ///  Description: Hàm lấy thông tin tài sản phục vụ view thông tin tài sản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public TaiSanModel PrepareTaiSanModelView(TaiSanModel model, TaiSan item)
        {
            model = item.ToModel<TaiSanModel>();
            var dv = _donviService.GetDonViById(item.DON_VI_ID ?? 0);
            model.DonViMa = dv.MA;
            model.DonViTen = dv.TEN;
            model.TenPhuongThucMuaSam = _nhanhienthiService.GetGiaTriEnum((enumPHUONG_THUC_MUA_SAM)(model.PHUONG_THUC_MUA_SAM_ID ?? 0));
            model.TenDonViMuaSamTapTrung = _donviService.GetDonViById(model.DON_VI_MUA_SAM_TAP_TRUNG_ID ?? 0)?.TEN;
            var nguoiTao = _nguoiDungService.GetNguoiDungById(item.NGUOI_TAO_ID);
            if (nguoiTao != null)
            {
                model.NguoiTaoTen = nguoiTao.TEN_DAY_DU;
            }
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && item.loaitaisandonvi != null)
                model.TenLoaiTaiSan = item.loaitaisandonvi != null ? item.loaitaisandonvi.TEN : "";
            else
                model.TenLoaiTaiSan = item.loaitaisan != null ? item.loaitaisan.TEN : "";
            return model;
        }

        public TaiSanModel PrepareTaiSanModelChon(TaiSanModel model, TaiSan item, bool excludeProperties = false, List<decimal> lstTaiSanChon = null, decimal idKhaiThac = 0)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanModel>();
            }
            //more
            if (lstTaiSanChon != null && lstTaiSanChon.Count(c => c == item.ID) > 0)
                model.IdChon = true;
            model.DIEN_TICH_KHAI_THAC = 0;
            if (model.LOAI_HINH_TAI_SAN_ID > 0)
            {
                var DienTichTS = _biendongService.Tinh_GiaTriTaiSan(model.ID);
                model.DIEN_TICH_KT = DienTichTS.DAT_TONG_DIEN_TICH_CU + DienTichTS.NHA_TONG_DIEN_TICH_XD_CU;
                var NguyenGiaTS = _biendongService.TinhNguyenGiaTaiSan(null, null, model.ID);
                model.strNguyenGiaVN = NguyenGiaTS.ToVNStringNumber();
                model.TenLoaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID ?? (item.LOAI_TAI_SAN_DON_VI_ID ?? 0))?.TEN;
                model.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(model.enumLoaiHinhTaiSan);
                var taiSanKhaiThac = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(idKhaiThac, model.ID);
                if (taiSanKhaiThac != null)
                    model.DIEN_TICH_KHAI_THAC = taiSanKhaiThac.DIEN_TICH_KHAI_THAC;
                if (idKhaiThac > 0)
                {
                    model.KHAI_THAC_ID = idKhaiThac;
                    model.idKhaiThac = idKhaiThac;
                }

            }
            model.IsDisableTSDKTS = IsDisableTSDKTS(item.NGUON_TAI_SAN_ID);
            return model;
        }

        public void PrepareTaiSan(TaiSanModel model, TaiSan item)
        {
            item.MA = model.MA;
            var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID.Value);
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
            {
                item.TEN = model.taisandatModel.DIA_CHI;
                var tinhItem = _diaBanService.GetDiaBanById(model.taisandatModel.TinhId);
                var huyenItem = _diaBanService.GetDiaBanById(model.taisandatModel.HuyenId);
                var xaItem = _diaBanService.GetDiaBanById(model.taisandatModel.XaId);
                if (xaItem != null)
                    item.TEN = item.TEN.Trim() + ", " + xaItem.TEN;
                if (huyenItem != null)
                    item.TEN = item.TEN + ", " + huyenItem.TEN;
                if (tinhItem != null)
                    item.TEN = item.TEN + ", " + tinhItem.TEN;
            }

            else
            {
                item.TEN = model.TEN;
            }


            item.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;

            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                // get tài sản vô hình  gốc
                decimal? parentId = model.LOAI_TAI_SAN_DON_VI_ID;
                decimal? tree_level = 0;
                var maLoaiTSVH = "";
                LoaiTaiSanDonVi taiSanVoHinh = new LoaiTaiSanDonVi();
                do
                {
                    if (parentId == null)
                        break;
                    taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    tree_level = taiSanVoHinh.TREE_LEVEL;
                    parentId = taiSanVoHinh.PARENT_ID;
                    maLoaiTSVH = taiSanVoHinh.MA;
                } while (tree_level > 2);
                item.LOAI_TAI_SAN_ID = taiSanVoHinh.LOAI_TAI_SAN_ID;
            }
            else
                item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            item.LOAI_TAI_SAN_DON_VI_ID = model.LOAI_TAI_SAN_DON_VI_ID;
            item.DU_AN_ID = model.DU_AN_ID;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
            item.CHUNG_TU_SO = model.CHUNG_TU_SO;
            item.QUYET_DINH_NGUOI_ID = model.QUYET_DINH_NGUOI_ID;
            item.NUOC_SAN_XUAT_ID = model.NUOC_SAN_XUAT_ID;
            item.DOI_TAC_ID = model.DOI_TAC_ID;
            item.NGAY_DUYET = model.NGAY_DUYET;
            item.NAM_SAN_XUAT = model.NAM_SAN_XUAT;
            item.NGAY_NHAP = model.NGAY_NHAP;
            item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
            item.GHI_CHU = model.GHI_CHU;
            item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.DON_VI_ID = model.DON_VI_ID;
            item.GIA_MUA_TIEP_NHAN = model.GIA_MUA_TIEP_NHAN;
            item.GIA_HOA_DON = model.GIA_HOA_DON;
            item.IS_MIEN_THUE = model.IS_MIEN_THUE;
            item.PHUONG_THUC_MUA_SAM_ID = model.PHUONG_THUC_MUA_SAM_ID;
            item.DON_VI_MUA_SAM_TAP_TRUNG_ID = model.DON_VI_MUA_SAM_TAP_TRUNG_ID;
            if (item.IS_MIEN_THUE == true)
                item.MIEN_THUE_SO_TIEN = model.MIEN_THUE_SO_TIEN;
            else
                item.MIEN_THUE_SO_TIEN = 0;
            if ((model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU) && (lyDoBienDong.MA == enum_LY_DO_BIEN_DONG.KIEM_KE_PHAT_HIEN_THUA))
            {
                item.HM_SO_NAM_CON_LAI = model.HM_SO_NAM_CON_LAI;
                if (model.HM_SO_NAM_CON_LAI > 0)
                {
                    item.HM_TY_LE = Math.Round(((1 / model.HM_SO_NAM_CON_LAI.Value) * 100), 2);
                }
            }
            else
            {
                item.HM_TY_LE = null;
                item.HM_SO_NAM_CON_LAI = null;
            }

            //item.NV =model.nvYeuCauChiTietModel;
        }

        public TaiSanListModel PrepareTaiSanchonListModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //get items
            var items = _itemService.SearchTaiSans(Keysearch: searchModel.KeySearch,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ListTaiSanDaChon: searchModel.ListTaiSanDaChon,
                idKhaiThac: searchModel.idKhaiThac ?? 0, donviId: searchModel.donviId, donvikhaithacid: searchModel.donvikhaithacid,
                LOAI_HINH_TAI_SAN_ID: searchModel.LOAI_HINH_TAI_SAN_ID, tenTaiSan: searchModel.tenTaiSan, maTaiSan: searchModel.maTaiSan, TRANG_THAI_ID: searchModel.TRANG_THAI_ID, Fromdate: searchModel.Fromdate, Todate: searchModel.Todate, ToDateNgayTangMoi: searchModel.ngayDen, FromDateNgayTangMoi: searchModel.ngayTu, isKhaiThac: true);

            //var items = _itemService.DanhSachTaiSans(Keysearch:searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,idKha);
            if (searchModel.idKhaiThac != 0)
            {
                var lstTaiSanChon = _khaiThacTaiSanService.GetMapByKhaiThacId(searchModel.idKhaiThac ?? 0).Select(c => c.KHAI_THAC_ID).ToList();

                var Listkhaithacdung = _khaiThacService.GetKhaiThacsKhacNgay(KHAI_THAC_NGAY_TU: searchModel.ngayTu, KHAI_THAC_NGAY_DEN: searchModel.ngayDen, donviId: searchModel.donvikhaithacid ?? 0).Where(c => c.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET).ToList();
                decimal[] Listkhaithackhac = Listkhaithacdung.Where(c => c.ID != searchModel.idKhaiThac).Select(c => c.ID).ToArray();
                //Distinct
                decimal[] ListTaiSanKhaiThac = _khaiThacChiTietServices.GetKhaiThacChiTietByKhaiThacId(searchModel.idKhaiThac ?? 0).Select(c => c.TAI_SAN_ID).ToArray();
                var taiSanchonKhaithac = items.Where(c => !ListTaiSanKhaiThac.Contains(c.ID));
                if (searchModel.ngayDen.HasValue)
                {
                    taiSanchonKhaithac = taiSanchonKhaithac.Where(c => c.NGAY_NHAP < searchModel.ngayDen.Value);
                }
                var lstTaiSan = taiSanchonKhaithac.Select(c => PrepareTaiSanModelChon(new TaiSanModel(), c, false, lstTaiSanChon, searchModel.idKhaiThac ?? 0));
                var model = new TaiSanListModel
                {
                    Data = lstTaiSan,
                    Total = items.TotalCount
                };
                return model;
            }
            //trường hợp tạo mới chưa có id khai thác
            //prepare list model
            else
            {
                var model = new TaiSanListModel
                {
                    //fill in model values from the entity
                    //Data = items.Select(c => c.ToModel<TaiSanModel>()),
                    //Total = items.TotalCount
                    Data = items.Select(c =>
                    {
                        var m = c.ToModel<TaiSanModel>();
                        m.KHAI_THAC_ID = searchModel.idKhaiThac;
                        m.idKhaiThac = searchModel.idKhaiThac;
                        return m;
                    }),
                    Total = items.TotalCount
                };
                return model;
            }
        }

        #endregion TaiSan

        #region Tinh toan gia tri tai san

        /// <summary>
        /// Description: Tính toán giá trị tài sản khi nhập số dư.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TinhGiaTriTSModel TinhToanGiaTriTS(TinhGiaTriTSModel model)
        {
            //prepate gia tri theo quyet dinh
            var itemLoaiTaiSan = new LoaiTaiSanModel();
            if (model.LoaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LoaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                itemLoaiTaiSan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.LoaiTaiSanDonViId.Value).ToModel<LoaiTaiSanModel>();
            else
            {
                itemLoaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(model.LoaiTaiSanId.Value).ToModel<LoaiTaiSanModel>();

                var itemLoaiTaiSanKH = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(model.LoaiTaiSanId.Value, _workContext.CurrentDonVi.ID);
                if (itemLoaiTaiSanKH != null)
                {
                    itemLoaiTaiSan.KH_TY_LE = itemLoaiTaiSanKH.TY_LE_KHAU_HAO ?? 0;
                    itemLoaiTaiSan.KH_THOI_HAN_SU_DUNG = itemLoaiTaiSanKH.THOI_HAN_SU_DUNG;
                }
            }
            model.HM_NamTheoQD = itemLoaiTaiSan.HM_THOI_HAN_SU_DUNG ?? 0;
            model.HM_TyLe = itemLoaiTaiSan.HM_TY_LE ?? 0;
            if (model.KH_TyLe == null || model.KH_TyLe <= 0)
            {
                model.KH_ThangTheoQD = itemLoaiTaiSan.KH_THOI_HAN_SU_DUNG ?? 0;
                model.KH_TyLe = itemLoaiTaiSan.KH_TY_LE ?? 0;
            }

            //TH1: khong trich KH
            if (model.TS_NgayBatDauTinh != null && model.TS_NgayKetThucTinh != null && (model.TS_TyLeNguyenGiaTrichKH == null || model.TS_TyLeNguyenGiaTrichKH == 0))
            {
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID != 2)
                {
                    model.TS_NguyenGiaTinhHM = model.TS_NguyenGia;
                    model.HM_GiaTriTinh = model.TS_GiaTriHienTai ?? model.TS_NguyenGiaTinhHM;
                    model = TinhHaoMon(model);
                    if (model.DON_VI_CHE_DO_HACH_TOAN_ID == 3)
                    {
                        model.HMKM_GiaTriConLai = (model.HM_GiaTriConLai ?? 0) + (model.KH_GiaTriConLai ?? 0);
                    }
                }
            }
            else if (model.TS_NgayBatDauTinh != null && model.TS_NgayKetThucTinh != null && model.KH_NgayBatDau <= model.TS_NgayKetThucTinh && model.TS_TyLeNguyenGiaTrichKH > 0)
            {
                //TH2: Ngay su dung < ngay trich KH < ngay nhap
                var ngayNhap = model.TS_NgayKetThucTinh;
                //tinh HM den Thoi gian trich kh
                model.HM_GiaTriTinh = model.TS_GiaTriHienTai ?? model.TS_NguyenGia;
                model.TS_NguyenGiaTinhHM = model.TS_NguyenGia;
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID == 2)
                {
                    model.TS_NgayBatDauTinh = model.KH_NgayBatDau;
                }
                model.TS_NgayKetThucTinh = model.KH_NgayBatDau;
                model = TinhHaoMon(model);
                var hm_luyKeGD1 = model.HM_LuyKe;
                //gia tri ts tai thoi diem trich KH
                model.TS_GiaTriHienTai = model.HM_GiaTriConLai;
                model.TS_NguyenGiaTinhHM = model.TS_NguyenGia * model.TS_TyLeNguyenGiaTinhHM / 100;
                model.TS_NguyenGiaTinhKH = model.TS_NguyenGia * model.TS_TyLeNguyenGiaTrichKH / 100;
                model.HM_GiaTriTinh = model.TS_GiaTriHienTai * model.TS_TyLeNguyenGiaTinhHM / 100;
                model.KH_GiaTriTinh = model.TS_GiaTriHienTai * model.TS_TyLeNguyenGiaTrichKH / 100;
                model.TS_NgayKetThucTinh = ngayNhap;
                model.TS_NgayBatDauTinh = model.KH_NgayBatDau;
                model = TinhHaoMon(model);
                model.HM_LuyKe = model.HM_LuyKe + hm_luyKeGD1;
                model = TrichKhauHao(model);
                //set gia tri hm con lai = hm_gia_tr
                if (model.KH_GiaTriConLai == null)
                {
                    model.KH_GiaTriConLai = 0;
                }
                if (model.HM_GiaTriConLai == null)
                {
                    model.HM_GiaTriConLai = 0;
                }
                //model.HM_GiaTriConLai = model.HM_GiaTriConLai.Value + model.KH_GiaTriConLai.Value;
                model.HMKM_GiaTriConLai = model.HM_GiaTriConLai + model.KH_GiaTriConLai;
                //
            }
            return model;
        }

        public TinhGiaTriTSModel TinhHaoMon(TinhGiaTriTSModel model)
        {
            if (!model.HM_TyLe.HasValue)
                model.HM_TyLe = CommonCalculate.TyLeKhauHao(model.HM_NamTheoQD);
            model.HM_GiaTriHaoMonMotNam = CommonCalculate.ValuePerTime(tyLe: model.HM_TyLe / 100, giaTriTS: model.TS_NguyenGiaTinhHM);
            model.HM_NamSuDung = CommonCalculate.YearsOfUse(model.TS_NgayKetThucTinh, model.TS_NgayBatDauTinh);
            model.HM_LuyKe = CommonCalculate.TinhLuyKe(thoiGian: model.HM_NamSuDung, valuePerTime: model.HM_GiaTriHaoMonMotNam);
            if (model.HM_LuyKe > model.TS_NguyenGiaTinhHM)
            {
                model.HM_LuyKe = model.TS_NguyenGiaTinhHM;
            }
            model.HM_NamConLai = model.HM_NamTheoQD - model.HM_NamSuDung;
            model.HM_GiaTriConLai = model.HM_GiaTriTinh - model.HM_LuyKe;
            return model;
        }

        public TinhGiaTriTSModel TrichKhauHao(TinhGiaTriTSModel model)
        {
            //prepare gia tri theo quy dinh
            if (!model.KH_TyLe.HasValue)
                model.KH_TyLe = CommonCalculate.TyLeKhauHao(model.KH_ThangTheoQD);
            model.KH_GiaTriTrichMotThang = CommonCalculate.ValuePerTime(tyLe: model.KH_TyLe / 100, giaTriTS: model.KH_GiaTriTinh);
            model.KH_ThangSuDung = CommonCalculate.MonthsOfUse(model.TS_NgayKetThucTinh, model.KH_NgayBatDau);
            model.KH_LuyKe = CommonCalculate.TinhLuyKe(thoiGian: model.KH_ThangSuDung, valuePerTime: model.KH_GiaTriTrichMotThang);
            model.KH_GiaTriConLai = model.KH_GiaTriTinh - model.KH_LuyKe;
            if (model.KH_ThangConLai == null)
            {
                model.KH_ThangConLai = model.KH_ThangTheoQD - model.KH_ThangSuDung;
                if (model.KH_ThangConLai <= 0 || model.KH_GiaTriConLai <= 0)
                {
                    model.KH_ThangConLai = 0;
                    model.KH_LuyKe = model.KH_GiaTriTinh;
                    model.KH_GiaTriConLai = 0;
                }
            }
            else
            {
                model.KH_TyLe = CommonCalculate.TyLeKhauHao(model.KH_ThangConLai);
                model.KH_GiaTriTrichMotThang = CommonCalculate.ValuePerTime(tyLe: model.KH_TyLe, giaTriTS: model.KH_GiaTriConLai);
            }
            return model;
        }

        public bool CheckAutoDuyet(decimal? LoaiHinhTaiSan, decimal? NguyenGia)
        {
            //đất, nhà, oto duyệt tay
            if (LoaiHinhTaiSan == (int)enumLOAI_HINH_TAI_SAN.DAT ||
                LoaiHinhTaiSan == (int)enumLOAI_HINH_TAI_SAN.NHA ||
                LoaiHinhTaiSan == (int)enumLOAI_HINH_TAI_SAN.OTO)
                return false;
            var donViId = _workContext.CurrentDonVi.ID;
            var cauHinhDonVi = _cauHinhService.LoadCauHinhDonViBo<CauHinhTuDongDuyet>(donViId);
            if (cauHinhDonVi != null)
            {
                var cauHinhDuyetTaiSans = cauHinhDonVi.IsAutoDuyetTaiSanDuoi500.toEntities<CauHinhDuyetTaiSan>();
                if (cauHinhDuyetTaiSans != null && cauHinhDuyetTaiSans.Count > 0)
                {
                    var CauHinhDuyetTS = cauHinhDuyetTaiSans.Where(p => p.LOAI_HINH_TAI_SAN == LoaiHinhTaiSan).FirstOrDefault();
                    //nếu tự động duyệt thì xét nguyên giá xem đủ tự động duyệt không
                    if (CauHinhDuyetTS != null && CauHinhDuyetTS.IS_AUTO_DUYET_DUOI_NG)
                        return NguyenGia < CauHinhDuyetTS.NGUYEN_GIA;
                }
            }
            return false;
        }

        #endregion Tinh toan gia tri tai san

        #region Bien Dong Tang Giam Nguyen Gia

        /// <summary>
        /// Description: Tính giá trị tài sản sau khi tăng, giảm nguyên giá
        /// </summary>
        /// <returns></returns>
        public TinhGiaTriTSModel TinhGiaTriBDNguyenGia(TinhGiaTriTSModel model)
        {
            //If giá trị tài sản đã chốt 31/12 năm trước

            //else

            //xử lý công thức khi biến động tăng, giảm nguyên giá

            return model;
        }

        #endregion Bien Dong Tang Giam Nguyen Gia

        public bool CheckMaTaiSan(string Ma, decimal? id = 0)
        {
            var taisan = _itemService.GetTaiSanByMa(Ma);
            if (taisan != null && taisan.ID != id)
                return false;
            else return true;
        }

        public bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0)
        {
            var taisan = _itemService.GetTaiSanByTen(TenTS: ten, donViId: donViId);
            if (taisan != null && taisan.ID != id)
                return false;
            else return true;
        }

        /// <summary>
        /// Note: chỉ sử dụng tăng mới, nhập số dư.
        /// </summary>
        /// <param name="yeuCauChiTiet"></param>
        /// <param name="model"></param>
        public void PrepareSaveHMKHTaiSan(YeuCauChiTiet yeuCauChiTiet, TaiSanModel model)
        {
            var ngTinhKH = (model.nvYeuCauChiTietModel.NGUYEN_GIA) * (model.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO / 100);
            var ngTinhHM = model.nvYeuCauChiTietModel.NGUYEN_GIA - ngTinhKH;
            TinhGiaTriTSModel tinhGiaTriTSModel = new TinhGiaTriTSModel()
            {
                TaiSanId = model.ID,
                DON_VI_CHE_DO_HACH_TOAN_ID = model.DON_VI_CHE_DO_HACH_TOAN_ID,
                LoaiTaiSanId = model.LOAI_TAI_SAN_ID,
                LoaiTaiSanDonViId = model.LOAI_TAI_SAN_DON_VI_ID,
                LoaiHinhTaiSanId = model.LOAI_HINH_TAI_SAN_ID,
                TS_NgayKetThucTinh = model.NGAY_NHAP,
                TS_NgayBatDauTinh = model.NGAY_SU_DUNG,
                KH_NgayBatDau = model.nvYeuCauChiTietModel.KH_NGAY_BAT_DAU,
                TS_NguyenGia = model.nvYeuCauChiTietModel.NGUYEN_GIA,
                TS_TyLeNguyenGiaTrichKH = model.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO,
                TS_TyLeNguyenGiaTinhHM = 100 - model.nvYeuCauChiTietModel.KH_TY_LE_NGUYEN_GIA_KHAU_HAO,
                TS_NguyenGiaTinhKH = ngTinhKH,
                TS_NguyenGiaTinhHM = ngTinhHM,
                KH_ThangSuDung = model.nvYeuCauChiTietModel.KH_THANG_THEO_QD,
                KH_TyLe = model.nvYeuCauChiTietModel.KH_TY_LE
            };
            tinhGiaTriTSModel = TinhToanGiaTriTS(tinhGiaTriTSModel);
            yeuCauChiTiet.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = tinhGiaTriTSModel.TS_TyLeNguyenGiaTrichKH;
            yeuCauChiTiet.KH_THANG_CON_LAI = tinhGiaTriTSModel.KH_ThangConLai;
            yeuCauChiTiet.KH_NGAY_BAT_DAU = tinhGiaTriTSModel.KH_NgayBatDau;
            yeuCauChiTiet.KH_LUY_KE = tinhGiaTriTSModel.KH_LuyKe;
            yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG = tinhGiaTriTSModel.KH_GiaTriTrichMotThang;
            yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO = tinhGiaTriTSModel.KH_GiaTriTinh;
            yeuCauChiTiet.KH_CON_LAI = tinhGiaTriTSModel.KH_GiaTriConLai;
            yeuCauChiTiet.HM_LUY_KE = tinhGiaTriTSModel.HM_LuyKe;
            yeuCauChiTiet.HM_SO_NAM_CON_LAI = tinhGiaTriTSModel.HM_NamConLai;
            yeuCauChiTiet.HM_TY_LE_HAO_MON = tinhGiaTriTSModel.HM_TyLe;
            //yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.HM_GiaTriConLai;
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.TS_NguyenGia;
            }
            else
            {
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID == (int)enumCHE_DO_HACH_TOAN.HAO_MON)
                {
                    if (model.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI != tinhGiaTriTSModel.HM_GiaTriConLai)
                    {
                        yeuCauChiTiet.HM_LUY_KE = model.nvYeuCauChiTietModel.NGUYEN_GIA - yeuCauChiTiet.HM_GIA_TRI_CON_LAI;
                    }
                    else
                    {
                        yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.HM_GiaTriConLai;
                    }
                }
                #region bỏ 03-01-2024
                //else
                //{

                //if (model.DON_VI_CHE_DO_HACH_TOAN_ID == (int)enumCHE_DO_HACH_TOAN.HAO_MON_VA_KHAU_HAO)
                //{
                //    if (model.Checktinhkhauhao == false)
                //    {
                //        if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI != tinhGiaTriTSModel.HM_GiaTriConLai)
                //        {
                //            yeuCauChiTiet.HM_LUY_KE = model.nvYeuCauChiTietModel.NGUYEN_GIA - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //            yeuCauChiTiet.HM_GIA_TRI_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //        }
                //        else
                //        {
                //            yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.HM_GiaTriConLai;
                //        }
                //    }
                //    else
                //    {
                //        yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.HM_GiaTriConLai;
                //    }
                //}
                //if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI < (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                //{
                //    /*yeuCauChiTiet.HM_GIA_TRI_CON_LAI = (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI * tinhGiaTriTSModel.TS_TyLeNguyenGiaTinhHM / 100);
                //    yeuCauChiTiet.KH_CON_LAI = (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI * tinhGiaTriTSModel.TS_TyLeNguyenGiaTrichKH / 100);

                //    yeuCauChiTiet.HM_LUY_KE = tinhGiaTriTSModel.HM_GiaTriTinh - yeuCauChiTiet.HM_GIA_TRI_CON_LAI;
                //    yeuCauChiTiet.KH_LUY_KE = tinhGiaTriTSModel.KH_GiaTriTinh - yeuCauChiTiet.KH_CON_LAI;


                //    yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI - (int)(yeuCauChiTiet.HM_LUY_KE / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);*/
                //    /*yeuCauChiTiet.HM_LUY_KE = tinhGiaTriTSModel.HMKM_GiaTriConLai - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //    yeuCauChiTiet.KH_CON_LAI = tinhGiaTriTSModel.KH_GiaTriConLai - yeuCauChiTiet.HM_LUY_KE;
                //    yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI - (int)(yeuCauChiTiet.HM_LUY_KE / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);*/
                //    yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //    yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                //    var chenhLech = tinhGiaTriTSModel.HMKM_GiaTriConLai - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //    try
                //    {
                //        yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI - (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                //    }
                //    catch(Exception ex) { }
                //}
                //if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI > (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                //{
                //    yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                //    yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                //    if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI == yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO)
                //    {
                //        yeuCauChiTiet.KH_THANG_CON_LAI = tinhGiaTriTSModel.KH_ThangTheoQD;
                //    }
                //    else
                //    {
                //        var chenhLech = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI - (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0);
                //        if (yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG != null)
                //        {
                //            yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI + (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                //        }
                //    }
                //}
                //}
                #endregion bỏ 03-01-2024
                #region anhnt 03-01-2024 sửa lại tính giá trị còn lại
                else if (model.DON_VI_CHE_DO_HACH_TOAN_ID == (int)enumCHE_DO_HACH_TOAN.HAO_MON_VA_KHAU_HAO)
                {

                    if (model.Checktinhkhauhao == false)
                    {
                        if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI != tinhGiaTriTSModel.HM_GiaTriConLai)
                        {
                            yeuCauChiTiet.HM_LUY_KE = model.nvYeuCauChiTietModel.NGUYEN_GIA - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                            yeuCauChiTiet.HM_GIA_TRI_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                        }
                        else
                        {
                            yeuCauChiTiet.HM_GIA_TRI_CON_LAI = tinhGiaTriTSModel.HM_GiaTriConLai;
                        }
                    }
                    else
                    {
                        yeuCauChiTiet.HM_GIA_TRI_CON_LAI = 0;
                        if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI <= (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                        {
                            yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                            yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                            var chenhLech = tinhGiaTriTSModel.HMKM_GiaTriConLai - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                            try
                            {
                                yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI - (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                            }
                            catch (Exception ex) { }
                        }
                        if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI > (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                        {
                            yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                            yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                            if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI == yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO)
                            {
                                yeuCauChiTiet.KH_THANG_CON_LAI = tinhGiaTriTSModel.KH_ThangTheoQD;
                            }
                            else
                            {
                                var chenhLech = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI - (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0);
                                if (yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG != null)
                                {
                                    yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI + (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                                }
                            }
                        }
                    }


                }
                else if (model.DON_VI_CHE_DO_HACH_TOAN_ID == (int)enumCHE_DO_HACH_TOAN.KHAU_HAO)
                {

                    yeuCauChiTiet.HM_GIA_TRI_CON_LAI = 0;
                    if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI <= (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                    {
                        yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                        yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                        var chenhLech = tinhGiaTriTSModel.HMKM_GiaTriConLai - model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                        try
                        {
                            yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI - (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                        }
                        catch (Exception ex) { }
                    }
                    else if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI > (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0))
                    {
                        yeuCauChiTiet.KH_CON_LAI = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI;
                        yeuCauChiTiet.KH_LUY_KE = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO - yeuCauChiTiet.KH_CON_LAI;
                        if (model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI == yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO)
                        {
                            yeuCauChiTiet.KH_THANG_CON_LAI = tinhGiaTriTSModel.KH_ThangTheoQD;
                        }
                        else
                        {
                            var chenhLech = model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI - (tinhGiaTriTSModel.HMKM_GiaTriConLai ?? 0);
                            if (yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG != null)
                            {
                                yeuCauChiTiet.KH_THANG_CON_LAI = yeuCauChiTiet.KH_THANG_CON_LAI + (int)(chenhLech / yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG);
                            }
                        }
                    }

                }
                #endregion anhnt 03-01-2024 sửa lại tính giá trị còn lại
            }


            //Insert bảng tài sản khấu hao
            try
            {
                var currentDonVi = _donviService.GetDonViById(_workContext.CurrentDonVi.ID);
                if (currentDonVi.CHE_DO_HACH_TOAN_ID != (int)enumCHE_DO_HACH_TOAN.HAO_MON)
                {
                    if (yeuCauChiTiet.KH_TY_LE_NGUYEN_GIA_KHAU_HAO != null && yeuCauChiTiet.KH_NGAY_BAT_DAU != null)
                    {
                        //var tyleKH = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(model.LOAI_TAI_SAN_ID, _workContext.CurrentDonVi.ID);
                        var tskh = new TaiSanKhauHao();
                        tskh.NGAY_BAT_DAU = yeuCauChiTiet.KH_NGAY_BAT_DAU;
                        tskh.SO_THANG_KHAU_HAO = model.nvYeuCauChiTietModel.KH_THANG_THEO_QD;//tyleKH?.THOI_HAN_SU_DUNG;
                        tskh.TAI_SAN_ID = model.ID;
                        tskh.TY_LE_KHAU_HAO = model.nvYeuCauChiTietModel.KH_TY_LE;
                        tskh.TY_LE_NGUYEN_GIA_KHAU_HAO = yeuCauChiTiet.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                        _taiSanKhauHaoService.InsertTaiSanKhauHao(tskh);
                    }
                }
            }
            catch (Exception e)
            {


            }

        }

        #region Giam Tai San - DieuChuyen - Điều chuyển 1 phần

        /// <summary>
        /// Clone tất cả những gì liên quan đến tài sản sang đơn vị mới. Bao gồm biến động, yêu cầu, chi tiết tài sản.
        /// </summary>
        /// <param name="TaiSanID">Tài sản Id</param>
        /// <param name="bienDong">biến động chi tiet giảm tài sản (Điều chuyển)</param>
        public virtual void DieuChuyenTaiSan(int TaiSanID, BienDongChiTiet bdct)
        {
            if (TaiSanID <= 0)
                return;
            var _oldTS = _itemService.GetTaiSanById(TaiSanID);
            if (_oldTS == null)
                return;
            //thêm mới tài sản điều chuyển mới
            var _newTS = DieuChuyen_InsertNewTS(_oldTS, bdct.DON_VI_NHAN_DIEU_CHUYEN_ID, bdct.biendong.NGAY_BIEN_DONG);

            //thêm mới các bảng thông tin chi tiết tài sản
            var InfoTsNow = _biendongService.Tinh_GiaTriTaiSan(TaiSanID: _oldTS.ID, To_Date_BienDong: bdct.biendong.NGAY_BIEN_DONG);
            DieuChuyen_InsertNewChiTietTS(_newTS, _oldTS.ID, InfoTsNow);

            //thêm mới yêu cầu cho tài sản mới được điều chuyển
            DieuChuyen_InsertYeuCauAndYCCT(_newTS, _oldTS.ID, bdct, InfoTsNow);

            // nếu là điều chuyển có kèm theo nhà hoặc đất thì sinh thêm tài sản cùng yêu cầu biến động kèm theo
        }

        /// <summary>
        /// điều chuyển một phần
        /// </summary>
        /// <param name="yeuCau"></param>
        /// <param name="bienDongChiTiet"></param>
        public virtual void DieuChuyenMotPhan(YeuCau yeuCau, BienDongChiTiet bienDongChiTiet)
        {
            if (yeuCau == null || yeuCau.TAI_SAN_ID <= 0)
                return;
            var _oldTS = _itemService.GetTaiSanById(yeuCau.TAI_SAN_ID);
            if (_oldTS == null)
                return;
            //thêm mới tài sản điều chuyển mới
            var _newTS = DieuChuyen_InsertNewTS(_oldTS, bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID, bienDongChiTiet.biendong.NGAY_BIEN_DONG);
            //thêm mới các bảng thông tin chi tiết tài sản
            DieuChuyenMotPhan_InsertNewChiTietTS(_newTS, _oldTS.ID, bienDongChiTiet);
            //thêm mới các bảng thông tin chi tiết tài sản
            DieuChuyenMotPhan_InsertYeuCauAndYCTT(_newTS, yeuCau, bienDongChiTiet);
        }

        /// <summary>
        /// Điều chuyển bảng tài sản
        /// </summary>
        /// <param name="_oldTS"></param>
        /// <param name="DON_VI_NHAN_DIEU_CHUYEN_ID"></param>
        /// <returns></returns>
        public virtual TaiSan DieuChuyen_InsertNewTS(TaiSan _oldTS, decimal? DON_VI_NHAN_DIEU_CHUYEN_ID, DateTime? NgayDieuChuyen)
        {
            var _newTS = new TaiSan(_oldTS);
            _newTS.ID = 0;
            _newTS.DON_VI_ID = DON_VI_NHAN_DIEU_CHUYEN_ID;
            _newTS.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
            _newTS.MA = null;
            _newTS.MA_DB = null;
            _newTS.MA_QLDKTS40 = null;
            _newTS.DON_VI_BO_PHAN_ID = null;
            _newTS.NGAY_NHAP = NgayDieuChuyen;
            _newTS.LY_DO_BIEN_DONG_ID = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.TIEP_NHAN);
            //thêm mới tài sản điều chuyển mới
            _itemService.InsertTaiSan(entity: _newTS, isNguoiTaoNull: true);
            //ghi log
            _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Thêm mới tài sản ", _newTS.ToModel<TaiSanModel>(), "TaiSan");
            var _dv = _donviService.GetDonViById(_newTS.DON_VI_ID ?? 0);
            if (_newTS.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var lts = _loaitaisanService.GetLoaiTaiSanById(_newTS.LOAI_TAI_SAN_ID ?? 0);
                if (lts != null)
                    _newTS.MA = CommonHelper.GenMaTaiSan(_dv.MA, lts.MA, _newTS.ID);
            }
            else
            {
                // get tài sản vô hình  gốc
                decimal? parentId = _newTS.LOAI_TAI_SAN_DON_VI_ID;
                decimal? tree_level = 0;
                LoaiTaiSanDonVi taiSanVoHinh = new LoaiTaiSanDonVi();
                do
                {
                    if (parentId == null)
                        break;
                    taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    tree_level = taiSanVoHinh.TREE_LEVEL;
                    parentId = taiSanVoHinh.PARENT_ID;
                } while (tree_level > 2);
                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                if (taiSanVoHinh != null)
                    _newTS.MA = CommonHelper.GenMaTaiSan(_dv.MA, taiSanVoHinh.MA, _newTS.ID);
            }

            _itemService.UpdateTaiSan(_newTS);
            return _newTS;
        }

        /// <summary>
        /// Điều chuyển chi tiết tài sản
        /// </summary>
        /// <param name="_newTS"></param>
        /// <param name="_oldTSID"></param>
        /// <param name="InfoTsNow"></param>
        public virtual void DieuChuyen_InsertNewChiTietTS(TaiSan _newTS, decimal _oldTSID, GiaTriTaiSan InfoTsNow)
        {
            switch (_newTS.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(_oldTSID);
                    var _newTsDat = new TaiSanDat(TsDat);
                    _newTsDat.TAI_SAN_ID = _newTS.ID;
                    _newTsDat.DIEN_TICH = InfoTsNow.DAT_TONG_DIEN_TICH_CU ?? 0;
                    _taiSanDatService.InsertTaiSanDat(_newTsDat);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(_oldTSID);
                    var _newTsNha = new TaiSanNha(TsNha);
                    _newTsNha.TAI_SAN_ID = _newTS.ID;
                    _newTsNha.DIEN_TICH_XAY_DUNG = InfoTsNow.NHA_DIEN_TICH_XD_CU;
                    _newTsNha.DIEN_TICH_SAN_XAY_DUNG = InfoTsNow.NHA_TONG_DIEN_TICH_XD_CU;
                    _newTsNha.TAI_SAN_DAT_ID = null;
                    _taisannhaService.InsertTaiSanNha(_newTsNha);

                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    var TsVH = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(_oldTSID);
                    var _newTsVH = new TaiSanVoHinh(TsVH);
                    _newTsVH.TAI_SAN_ID = _newTS.ID;
                    _taiSanVoHinhService.InsertTaiSanVoHinh(_newTsVH);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    var TsOto = _taiSanOtoService.GetTaiSanOtoById(_oldTSID);
                    var _newTsOto = new TaiSanOto(TsOto);
                    _newTsOto.TAI_SAN_ID = _newTS.ID;
                    _taiSanOtoService.InsertTaiSanOto(_newTsOto);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    var TsCayLauNam = _taiSanClnService.GetTaiSanClnByTaiSanId(_oldTSID);
                    var _newTsCln = new TaiSanCln(TsCayLauNam);
                    _newTsCln.TAI_SAN_ID = _newTS.ID;
                    _taiSanClnService.InsertTaiSanCln(_newTsCln);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    var TsMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(_oldTSID);
                    var _newTsMayMoc = new TaiSanMayMoc(TsMayMoc);
                    _newTsMayMoc.TAI_SAN_ID = _newTS.ID;
                    _taiSanMayMocService.InsertTaiSanMayMoc(_newTsMayMoc);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var TsVatKienTruc = _taiSanVktService.GetTaiSanVktByTaiSanId(_oldTSID);
                    var _newTsVKT = new TaiSanVkt(TsVatKienTruc);
                    _newTsVKT.TAI_SAN_ID = _newTS.ID;
                    _taiSanVktService.InsertTaiSanVkt(_newTsVKT);
                    break;
            }
        }

        public virtual void DieuChuyenMotPhan_InsertNewChiTietTS(TaiSan _newTS, decimal _oldTSID, BienDongChiTiet _oldYCCT)
        {
            switch (_newTS.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(_oldTSID);
                    var _newTsDat = new TaiSanDat(TsDat);
                    _newTsDat.TAI_SAN_ID = _newTS.ID;
                    _newTsDat.DIEN_TICH = -1 * (_oldYCCT.DAT_TONG_DIEN_TICH ?? 0);
                    _taiSanDatService.InsertTaiSanDat(_newTsDat);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(_oldTSID);
                    var _newTsNha = new TaiSanNha(TsNha);
                    _newTsNha.TAI_SAN_ID = _newTS.ID;
                    _newTsNha.DIEN_TICH_XAY_DUNG = -1 * (_oldYCCT.NHA_DIEN_TICH_XD ?? 0);
                    _newTsNha.DIEN_TICH_SAN_XAY_DUNG = -1 * (_oldYCCT.NHA_TONG_DIEN_TICH_XD ?? 0);
                    _newTsNha.TAI_SAN_DAT_ID = null;
                    _taisannhaService.InsertTaiSanNha(_newTsNha);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var TsVKT = _taiSanVktService.GetTaiSanVktById(_oldTSID);
                    var _newTsVKT = new TaiSanVkt(TsVKT);
                    _newTsVKT.TAI_SAN_ID = _newTS.ID;
                    _newTsVKT.CHIEU_DAI = -1 * (_oldYCCT.VKT_CHIEU_DAI);
                    _newTsVKT.DIEN_TICH = -1 * (_oldYCCT.VKT_DIEN_TICH);
                    _newTsVKT.THE_TICH = -1 * (_oldYCCT.VKT_THE_TICH);
                    _taiSanVktService.InsertTaiSanVkt(_newTsVKT);
                    break;
            }
        }

        #region yêu cầu điều chuyển

        private void DieuChuyen_InsertYeuCauAndYCCT(TaiSan _newTS, decimal _oldTSID, BienDongChiTiet bdct, GiaTriTaiSan InfoTsNow)
        {
            //thêm yêu cầu
            var yeucau_tsDieuChuyen = DieuChuyen_InsertYeuCau(_newTS, bdct, InfoTsNow);
            //thêm yêu cầu chi tiết
            DieuChuyen_InsertYeuCauChiTiet(_newTS, _oldTSID, bdct, InfoTsNow, yeucau_tsDieuChuyen);
        }

        private YeuCau DieuChuyen_InsertYeuCau(TaiSan _newTS, BienDongChiTiet bdct, GiaTriTaiSan InfoTsNow)
        {
            var biendongGiam = _biendongService.GetBienDongById(bdct.BIEN_DONG_ID);
            var yeucau_tsDieuChuyen = new YeuCau(biendongGiam);
            yeucau_tsDieuChuyen.ID = 0;
            yeucau_tsDieuChuyen.DON_VI_ID = _newTS.DON_VI_ID;
            yeucau_tsDieuChuyen.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
            yeucau_tsDieuChuyen.TAI_SAN_MA = _newTS.MA;
            yeucau_tsDieuChuyen.TAI_SAN_TEN = _newTS.TEN;
            yeucau_tsDieuChuyen.TAI_SAN_ID = _newTS.ID;
            yeucau_tsDieuChuyen.DON_VI_BO_PHAN_ID = null;
            yeucau_tsDieuChuyen.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
            yeucau_tsDieuChuyen.NGUYEN_GIA = InfoTsNow.NGUYEN_GIA_CU;
            yeucau_tsDieuChuyen.NGUOI_TAO_ID = biendongGiam.NGUOI_TAO_ID;

            var lstNguonVonModel = new List<NguonVonModel>();
            IList<TaiSanNguonVon> taiSanNguonVons = _taisannguonvonService.GetAllSumNguonVonNumberOfTaiSan(biendongGiam.TAI_SAN_ID);
            if (taiSanNguonVons != null)
                foreach (var item in taiSanNguonVons)
                {
                    var nguonVon = _nguonvonService.GetNguonVonById(item.NGUON_VON_ID);
                    string tenNguonVon = nguonVon != null ? nguonVon.TEN : "";
                    NguonVonModel nguonVonModel = new NguonVonModel
                    {
                        ID = item.NGUON_VON_ID,
                        TEN = tenNguonVon,
                        GiaTri = Math.Abs(item.GIA_TRI)
                    };
                    lstNguonVonModel.Add(nguonVonModel);
                }

            yeucau_tsDieuChuyen.NGUON_VON_JSON = lstNguonVonModel.toStringJson();
            yeucau_tsDieuChuyen.LY_DO_BIEN_DONG_ID = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.TIEP_NHAN);
            yeucau_tsDieuChuyen.LOAI_TAI_SAN_ID = _newTS.LOAI_TAI_SAN_ID;
            yeucau_tsDieuChuyen.LOAI_TAI_SAN_DON_VI_ID = _newTS.LOAI_TAI_SAN_DON_VI_ID;

            _yeucauService.InsertYeuCau(yeucau_tsDieuChuyen);
            _yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucau_tsDieuChuyen.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
            _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", yeucau_tsDieuChuyen.ToModel<YeuCauModel>(), "YeuCau");
            return yeucau_tsDieuChuyen;
        }

        private void DieuChuyen_InsertYeuCauChiTiet(TaiSan _newTS, decimal _oldTS_ID, BienDongChiTiet bdct, GiaTriTaiSan InfoTsNow, YeuCau yeucau_tsDieuChuyen)
        {
            var ycct_tsDieuChuyen = new YeuCauChiTiet(bdct);
            ycct_tsDieuChuyen.YEU_CAU_ID = yeucau_tsDieuChuyen.ID;
            //hiện trạng sử dụng
            var HTSD = ycct_tsDieuChuyen.HTSD_JSON.toEntity<HienTrangList>();
            if (HTSD != null)
                HTSD.TaiSanId = _newTS.ID;
            ycct_tsDieuChuyen.HTSD_JSON = HTSD.toStringJson();
            ycct_tsDieuChuyen.NGUYEN_GIA = yeucau_tsDieuChuyen.NGUYEN_GIA;

            //hao mòn
            var hmTS = _haoMonTaiSanService.GetHaoMonTaiSanGanNhat(_oldTS_ID);
            ycct_tsDieuChuyen.HM_LUY_KE = hmTS != null ? hmTS.TONG_HAO_MON_LUY_KE : null;
            switch (_newTS.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(_newTS.ID);
                    ycct_tsDieuChuyen.DIA_BAN_ID = tsDat.DIA_BAN_ID;
                    ycct_tsDieuChuyen.DAT_TONG_DIEN_TICH = InfoTsNow.DAT_TONG_DIEN_TICH_CU;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    ycct_tsDieuChuyen.NHA_DIEN_TICH_XD = InfoTsNow.NHA_DIEN_TICH_XD_CU;
                    ycct_tsDieuChuyen.NHA_TONG_DIEN_TICH_XD = InfoTsNow.NHA_TONG_DIEN_TICH_XD_CU;
                    break;
            }
            ycct_tsDieuChuyen.DATA_JSON = yeucau_tsDieuChuyen.ToModel<YeuCauModel>().toStringJson();
            ycct_tsDieuChuyen.NGUYEN_GIA = yeucau_tsDieuChuyen.NGUYEN_GIA;
            ycct_tsDieuChuyen.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = _oldTS_ID;
            _yeucauchitietService.InsertYeuCauChiTiet(ycct_tsDieuChuyen);
        }

        #endregion yêu cầu điều chuyển

        #region yêu cầu điều chuyển một phần

        private void DieuChuyenMotPhan_InsertYeuCauAndYCTT(TaiSan _newTS, YeuCau _oldYeuCau, BienDongChiTiet _oldYCCT)
        {
            //thêm yêu cầu
            var yeucau_tsDieuChuyen = DieuChuyenMotPhan_InsertYeuCau(_newTS, _oldYeuCau);
            //thêm yêu cầu chi tiết
            DieuChuyenMotPhan_InsertYeuCauChiTiet(_newTS, _oldYCCT, yeucau_tsDieuChuyen, _oldYeuCau.TAI_SAN_ID);
        }

        private YeuCau DieuChuyenMotPhan_InsertYeuCau(TaiSan _newTS, YeuCau _oldYeuCau)
        {
            YeuCau yeucau_tsDieuChuyen = new YeuCau(_oldYeuCau);
            yeucau_tsDieuChuyen.ID = 0;
            yeucau_tsDieuChuyen.DON_VI_ID = _newTS.DON_VI_ID;
            yeucau_tsDieuChuyen.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
            yeucau_tsDieuChuyen.TAI_SAN_MA = _newTS.MA;
            yeucau_tsDieuChuyen.TAI_SAN_TEN = _newTS.TEN;
            yeucau_tsDieuChuyen.TAI_SAN_ID = _newTS.ID;
            yeucau_tsDieuChuyen.DON_VI_BO_PHAN_ID = null;
            yeucau_tsDieuChuyen.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
            yeucau_tsDieuChuyen.NGUYEN_GIA = (_oldYeuCau.NGUYEN_GIA * -1);
            yeucau_tsDieuChuyen.NGUOI_TAO_ID = _oldYeuCau.NGUOI_TAO_ID;
            //nguồn vốn
            var lstNguonVonModel = _oldYeuCau.NGUON_VON_JSON.toEntities<NguonVonModel>();
            if (lstNguonVonModel != null && lstNguonVonModel.Count > 0)
            {
                foreach (var item in lstNguonVonModel)
                    item.GiaTri = Math.Abs(item.GiaTri ?? 0);
                yeucau_tsDieuChuyen.NGUON_VON_JSON = lstNguonVonModel.toStringJson();
            }
            //yeucau_tsDieuChuyen.LY_DO_BIEN_DONG_ID = GetLyDoTiepNhan(_newTS.LOAI_HINH_TAI_SAN_ID);
            yeucau_tsDieuChuyen.LY_DO_BIEN_DONG_ID = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.TIEP_NHAN);
            yeucau_tsDieuChuyen.LOAI_TAI_SAN_ID = _newTS.LOAI_TAI_SAN_ID;
            yeucau_tsDieuChuyen.LOAI_TAI_SAN_DON_VI_ID = _newTS.LOAI_TAI_SAN_DON_VI_ID;

            _yeucauService.InsertYeuCau(yeucau_tsDieuChuyen);
            _yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucau_tsDieuChuyen.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
            _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin biến động", yeucau_tsDieuChuyen.ToModel<YeuCauModel>(), "YeuCau");
            return yeucau_tsDieuChuyen;
        }

        private void DieuChuyenMotPhan_InsertYeuCauChiTiet(TaiSan _newTS, BienDongChiTiet _oldYCCT, YeuCau yeucau_tsDieuChuyen, Decimal? _oldTaiSanId = 0)
        {
            var ycct_tsDieuChuyen = new YeuCauChiTiet(_oldYCCT);
            ycct_tsDieuChuyen.YEU_CAU_ID = yeucau_tsDieuChuyen.ID;

            switch (_newTS.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(_newTS.ID);
                    ycct_tsDieuChuyen.DIA_BAN_ID = tsDat.DIA_BAN_ID;
                    ycct_tsDieuChuyen.DAT_TONG_DIEN_TICH = _oldYCCT.DAT_TONG_DIEN_TICH * -1;
                    ycct_tsDieuChuyen.NGUYEN_GIA = _oldYCCT.NGUYEN_GIA * -1;
                    //hiện trạng
                    var lstHienTrang_Dat = _oldYCCT.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    if (lstHienTrang_Dat != null && lstHienTrang_Dat.Count > 0)
                    {
                        foreach (var hienTrang in lstHienTrang_Dat)
                        {
                            if (hienTrang.HienTrangId == (int)enumHienTrang.DAT_TRU_SO_LAM_VIEC)
                                hienTrang.GiaTriNumber = ycct_tsDieuChuyen.DAT_TONG_DIEN_TICH;
                            else hienTrang.GiaTriNumber = 0;
                        }
                        var HienTrangList = new HienTrangList()
                        {
                            TaiSanId = _newTS.ID,
                            lstObjHienTrang = lstHienTrang_Dat,
                        };
                        ycct_tsDieuChuyen.HTSD_JSON = HienTrangList.toStringJson();
                    }
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    //ycct_tsDieuChuyen.NHA_DIEN_TICH_XD = _oldYCCT.NHA_DIEN_TICH_XD_CU;
                    ycct_tsDieuChuyen.NHA_TONG_DIEN_TICH_XD = _oldYCCT.NHA_TONG_DIEN_TICH_XD * -1;
                    ycct_tsDieuChuyen.NGUYEN_GIA = _oldYCCT.NGUYEN_GIA * -1;
                    //hiện trạng
                    var lstHienTrang_Nha = _oldYCCT.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    if (lstHienTrang_Nha != null && lstHienTrang_Nha.Count > 0)
                    {
                        foreach (var hienTrang in lstHienTrang_Nha)
                        {
                            if (hienTrang.HienTrangId == (int)enumHienTrang.NHA_TRU_SO_LAM_VIEC)
                                hienTrang.GiaTriNumber = ycct_tsDieuChuyen.NHA_TONG_DIEN_TICH_XD;
                            else hienTrang.GiaTriNumber = 0;
                        }
                        var HienTrangList = new HienTrangList()
                        {
                            TaiSanId = _newTS.ID,
                            lstObjHienTrang = lstHienTrang_Nha,
                        };
                        ycct_tsDieuChuyen.HTSD_JSON = HienTrangList.toStringJson();
                    }
                    ///tính toán giá trị còn lại
                    ///lấy ra biến động trước điều chuyển
                    ///

                    #region tính giá trị còn lại

                    ycct_tsDieuChuyen.HM_GIA_TRI_CON_LAI = TinhGiaTriConLaiDieuChuyen1Phan(TAI_SAN_ID: _oldYCCT.biendong?.TAI_SAN_ID, NGAY_BIEN_DONG_TRUOC_DIEU_CHUYEN: _oldYCCT.biendong?.NGAY_BIEN_DONG, NGAY_DIEU_CHUYEN: yeucau_tsDieuChuyen.NGAY_BIEN_DONG, GIA_TRI_CON_LAI_TRUOC_DIEU_CHUYEN: _oldYCCT.HM_GIA_TRI_CON_LAI, NGUYEN_GIA_SAU_DIEU_CHUYEN: yeucau_tsDieuChuyen.NGUYEN_GIA);

                    #endregion tính giá trị còn lại

                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    ycct_tsDieuChuyen.NGUYEN_GIA = _oldYCCT.NGUYEN_GIA * -1;
                    ycct_tsDieuChuyen.VKT_CHIEU_DAI = _oldYCCT.VKT_CHIEU_DAI * -1;
                    ycct_tsDieuChuyen.VKT_DIEN_TICH = _oldYCCT.VKT_DIEN_TICH * -1;
                    ycct_tsDieuChuyen.VKT_THE_TICH = _oldYCCT.VKT_THE_TICH * -1;
                    //hiện trạng
                    var lstHienTrang_VKT = _oldYCCT.HTSD_JSON.toEntity<HienTrangList>().lstObjHienTrang;
                    if (lstHienTrang_VKT != null && lstHienTrang_VKT.Count > 0)
                    {
                        var HienTrangList = new HienTrangList()
                        {
                            TaiSanId = _newTS.ID,
                            lstObjHienTrang = lstHienTrang_VKT,
                        };
                        ycct_tsDieuChuyen.HTSD_JSON = HienTrangList.toStringJson();
                    }
                    ///tính toán giá trị còn lại
                    ///lấy ra biến động trước điều chuyển
                    ///

                    #region tính giá trị còn lại

                    ycct_tsDieuChuyen.HM_GIA_TRI_CON_LAI = TinhGiaTriConLaiDieuChuyen1Phan(TAI_SAN_ID: _oldYCCT.biendong?.TAI_SAN_ID, NGAY_BIEN_DONG_TRUOC_DIEU_CHUYEN: _oldYCCT.biendong?.NGAY_BIEN_DONG, NGAY_DIEU_CHUYEN: yeucau_tsDieuChuyen.NGAY_BIEN_DONG, GIA_TRI_CON_LAI_TRUOC_DIEU_CHUYEN: _oldYCCT.HM_GIA_TRI_CON_LAI, NGUYEN_GIA_SAU_DIEU_CHUYEN: yeucau_tsDieuChuyen.NGUYEN_GIA);

                    #endregion tính giá trị còn lại

                    break;
            }

            if (_oldTaiSanId > 0)
                ycct_tsDieuChuyen.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = _oldTaiSanId;
            _yeucauchitietService.InsertYeuCauChiTiet(ycct_tsDieuChuyen);
        }

        #endregion yêu cầu điều chuyển một phần

        #endregion Giam Tai San - DieuChuyen - Điều chuyển 1 phần

        #region Chuyển quyền sử dụng tài sản

        public TaiSanSearchModel PrepareQuyenXuLyTaiSanSearchModel(TaiSanSearchModel searchModel, bool isDuyetBienDong = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            #region Loại hình tài sản

            var ltsExclude = new int[] {
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                (int)enumLOAI_HINH_TAI_SAN.KHAC };

            searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: ltsExclude);

            #endregion Loại hình tài sản

            #region Trạng thái tài sản

            int[] trangThaiNhapId = { (int)enumTRANG_THAI_TAI_SAN.NHAP, (int)enumTRANG_THAI_TAI_SAN.NHAP_LIEU, (int)enumTRANG_THAI_TAI_SAN.XOA, (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO, (int)enumTRANG_THAI_TAI_SAN.CHO_DONG_BO };
            searchModel.Trangthailist = ((enumTRANG_THAI_TAI_SAN)searchModel.enumtrangthaitaisan).ToSelectList(valuesToExclude: trangThaiNhapId);

            #endregion Trạng thái tài sản

            searchModel.donviId = _workContext.CurrentDonVi.ID;

            searchModel.isNhapLieu = _workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU;

            searchModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.ALL;

            var dv = _donviService.GetDonViById(searchModel.donviId ?? 0);
            searchModel.TenDonVi = dv.TEN;

            var nguoiDungNhanSelectList = _nguoiDungService.getNguoiDungByDonVi(searchModel.donviId.GetValueOrDefault())?.Select(p => new SelectListItem
            {
                Text = p.TEN_DANG_NHAP,
                Value = p.ID.ToString()
            }).ToList();
            searchModel.NguoiDungNhanAvailable = nguoiDungNhanSelectList;

            var nguoiDungSelectList = nguoiDungNhanSelectList.Select(p => new SelectListItem(p.Text, p.Value)).ToList();
            nguoiDungSelectList.Insert(nguoiDungSelectList.Count, new SelectListItem { Text = "Tài sản chưa có người xử lý", Value = ((int)enumNguoiTaoTaiSan.KhongNguoiQuanLy).ToString() });
            searchModel.NguoiDungAvailable = nguoiDungSelectList;

            searchModel.SetGridPageSize();
            return searchModel;
        }

        #endregion Chuyển quyền sử dụng tài sản

        #region CCKT Logic số liệu đầu kỳ

        public TaiSanListModel PrepareCCKTListTaiSan(BaoCaoTaiSanChiTietSearchModel searchModel)
        {

            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.DonVi == null)
                searchModel.DonVi = _workContext.CurrentDonVi.ID;

            //get items

            var items = _itemService.DanhSachTaiSans();

            //prepare list model
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = searchModel.Page;

                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);
                    if (c.TRANG_THAI_ID != null)
                    {
                        m.tentrangthai = _nhanhienthiService.GetGiaTriEnum(c.TrangThaiTaiSan);
                    }
                    string l_lyDoHuy = _yeucauService.GetStringTuChoi(c.ID);
                    m.strLyDoTuChoi = l_lyDoHuy;
                    if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                            loaitaisan = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }
                    if (c.DON_VI_BO_PHAN_ID > 0)
                    {
                        var bophansudung = _donvibophanService.GetDonViBoPhanById(m.DON_VI_BO_PHAN_ID ?? 0);
                        if (bophansudung != null)
                        {
                            m.TenBoPhanSuDung = bophansudung.TEN;
                        }
                    }
                    var NguyenGiaTS = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID);
                    m.strNguyenGiaVN = NguyenGiaTS.ToVNStringNumber();

                    //giá trị còn lại
                    m.strHM_GIA_TRI_CON_LAI = _itemService.Get_GTLC_Cua_TS(m.ID, DateTime.Now, true).ToVNStringNumber();
                    //if (searchModel.TRANG_THAI_ID == (int)GS.Core.Domain.TaiSans.enumTRANG_THAI_TAI_SAN.DA_DUYET)
                    //{
                    //  m.NguyenGiaTaiSan = _biendongService.GetBienDongMoiNhatByTaiSanId(c.ID).NGUYEN_GIA;
                    //}
                    //lay nguyengia ngoai list tai san
                    //if (m.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET)
                    //{
                    //    m.strNguyenGiaVN = _yeucauService.GetYeuCauByTaiSanId(c.ID).NGUYEN_GIA.ToVNStringNumber();
                    //}
                    //else
                    //{
                    //   m.strNguyenGiaVN = _biendongService.GetBienDongMoiNhatByTaiSanId(c.ID).NGUYEN_GIA.ToVNStringNumber();
                    //}
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DAT_DIA_CHI = tsDat.DIA_CHI;
                    }
                    m.CountYeuCauTs = _yeucauService.CountYeuCauCuaTaiSan(taisanId: c.ID);
                    var countCanDuyet = _yeucauService.CountYeuCauCuaTaiSan(taisanId: c.ID, statusIds: new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET });
                    if (countCanDuyet > 0)
                    {
                        m.IsHaveChuaDuyet = true;
                    }
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        #endregion CCKT Logic số liệu đầu kỳ

        #region Khai thác tài sản
        public TaiSanListModel PrepareTaiSanKhaiThacListModel(TaiSanSearchModel searchModel)
        {
            var items = _itemService.SearchTaiSanKhaiThac(Keysearch: searchModel.KeySearch,
                LOAI_HINH_TAI_SAN_ID: searchModel.LOAI_HINH_TAI_SAN_ID,
                Fromdate: searchModel.Fromdate, Todate: searchModel.Todate,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var listTaiSanModel = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = searchModel.Page;

                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                    {
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);
                    }

                    if (c.LOAI_TAI_SAN_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                            loaitaisan = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }

                    m.strNguyenGiaVN = _biendongService.TinhNguyenGiaTaiSan(null, null, c.ID).ToVNStringNumber();

                    //giá trị còn lại
                    m.strHM_GIA_TRI_CON_LAI = _itemService.Get_GTLC_Cua_TS(m.ID, DateTime.Now, true).ToVNStringNumber();

                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DAT_DIA_CHI = tsDat.DIA_CHI;
                    }
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return listTaiSanModel;
        }



        #endregion
        public TaiSanSearchModel PrepareKiemTraTaiSanSearchModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ddlLoaiDonVi = _loaiDonViModelFactory.PrepareSelectListLoaiDonVi(isAddFirst: true);
            searchModel.DDLCompareSign = new enumCompare().ToSelectList().ToList();
            var curentDonvi = _donviService.GetDonViById(_workContext.CurrentDonVi.ID);
            int capDonvi = (int)(curentDonvi.CAP_DON_VI_ID == 0 ? 99 : curentDonvi.CAP_DON_VI_ID);
            int[] valuesToExcute = new int[] { (int)CapEnum.TongCuc, (int)CapEnum.ChiCuc, (int)CapEnum.Cuc };

            // vì BoCoQuanTrungUong enum =0 nên phải đặt giá trị khác để kendomultiselect không mặc định 0 là tất cả
            foreach (CapEnum _enum in (CapEnum[])Enum.GetValues(typeof(CapEnum)))
            {
                if (!valuesToExcute.Contains((int)_enum))
                {
                    if (_enum != CapEnum.BoCoQuanTrungUong)
                    {
                        searchModel.ddlCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = ((int)_enum).ToString(), Selected = false });
                    }
                    else
                    {

                        searchModel.ddlCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = "99", Selected = true });
                    }

                }
            }
            searchModel.SelectedCapDonVis = new List<int>() { capDonvi };
            //decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (int)enumLOAI_HINH_TAI_SAN.DAC_THU, (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
              (int)enumLOAI_HINH_TAI_SAN.VO_HINH, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO}).Select(c => c.Value.ToNumberInt32())).ToArray();
            searchModel.DDLLoaiTaiSan = _loaitaisanModelFactory.PrepareSelectListTaiSanDatNha(isAddFirst: true, isDisabled: false, listLoaiHinhTaiSanId: listEx).ToList();
            searchModel.DDLMucDichSuDung = new enumMucDichSuDung().ToSelectList().ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public TaiSanListModel PrepareKiemTraTaiSanListModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }

            var items = _itemService.SearchAllTaiSanKiemTraTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, CapDonViSearch: searchModel.CapDonViSearch, listCapDonVis: searchModel.SelectedCapDonVis, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, donViId: searchModel.donviId, LoaiDonViSearch: searchModel.LoaiDonViSearch, MucDichSuDungSearch: searchModel.MucDichSuDungSearch, DienTich_CompareSign: searchModel.DienTich_CompareSign, DienTich_Value1: searchModel.DienTich_Value1, DienTich_Value2: searchModel.DienTich_Value2);

            //prepare list model
            //var model = new DonViListModel
            //{
            //    //fill in model values from the entity
            //    Data = items.Select(c => c.ToModel<DonViModel>()),

            //    Total = items.TotalCount
            //};
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    //m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;
                    if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                        {
                            var x = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
                            if (x != null)
                                loaitaisan = x.ToModel<LoaiTaiSanModel>();
                        }

                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DiaChi = tsDat.DIA_CHI;
                    }
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                    {
                        var tsNha = _taisannhaService.GetTaiSanNhaById(c.ID);
                        m.DiaChi = tsNha.DIA_CHI;
                    }
                    var biendongs = _biendongService.GetBienDongsByTaiSanId(c.ID);
                    biendongs = biendongs.Where(rs => rs.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || rs.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI || rs.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || rs.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).OrderBy(rs => rs.ID).ToList();
                    decimal? dt = 0;
                    foreach (var bd in biendongs)
                    {
                        var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);
                        if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            dt += bdct.DAT_TONG_DIEN_TICH ?? 0;
                        }
                        else
                        {
                            dt += bdct.NHA_TONG_DIEN_TICH_XD ?? 0;
                        }
                        m.TongDienTich = dt;
                    }
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }
        #region Cấu hình cảnh báo tài sản
        public async Task<TaiSanListModel> PrepareDanhSachTaiSanThayDoiDiaBan(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (string.IsNullOrEmpty(searchModel.StrDonViId))
                searchModel.StrDonViId = _workContext.CurrentDonVi.ID.ToString();

            var items = await _itemService.GetTaiSanCanThayDoiDiaBan(strDonViId: searchModel.StrDonViId, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = searchModel.Page;

                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);
                    if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();

                        var lts = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0);
                        if (lts != null)
                        {
                            m.TenLoaiTaiSan = lts.TEN;
                        }

                    }
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var tsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(c.ID);
                        m.DAT_DIA_CHI = tsDat.DIA_CHI;
                    }
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                    {
                        var bienDongCuoi = _biendongService.GetBienDongCuoiByTaiSanId(c.ID);

                        if (bienDongCuoi != null)
                        {
                            m.DAT_DIA_CHI = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongCuoi.ID)?.DIA_CHI;
                        }

                    }
                    if (c.DON_VI_ID > 0)
                    {
                        var donVi = _donviService.GetDonViById(c.DON_VI_ID ?? 0);
                        if (donVi != null)
                        {
                            var ldv = _loaiDonViService.GetLoaiDonViById(donVi.LOAI_DON_VI_ID ?? 0)?.TREE_NODE?.Split('-').FirstOrDefault();
                            m.TenDonVi = donVi.TEN;
                            m.TenLoaiDonVi = _loaiDonViService.GetLoaiDonViById(int.Parse(ldv ?? "0"))?.TEN;
                        }
                    }
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public async Task<TaiSanListModel> PrepareDanhSachTaiSanSaiHienTrang(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (string.IsNullOrEmpty(searchModel.StrDonViId))
                searchModel.StrDonViId = _workContext.CurrentDonVi.ID.ToString();

            var items = await _itemService.GetTaiSanSaiHienTrang(strDonViId: searchModel.StrDonViId, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = PrepareCauHinhCanhBaoTaiSanListModel(items, searchModel.Page);
            return model;
        }

        public async Task<TaiSanListModel> PrepareDanhSachTaiSanNhanDieuChuyen(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = await _itemService.GetTaiSanNhanDieuChuyen(donViId: _workContext.CurrentDonVi.ID, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = PrepareCauHinhCanhBaoTaiSanListModel(items, searchModel.Page);
            return model;
        }
        public async Task<TaiSanListModel> PrepareDanhSachTaiSanSaiDuoi10Trieu(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (string.IsNullOrEmpty(searchModel.StrDonViId))
                searchModel.StrDonViId = _workContext.CurrentDonVi.ID.ToString();

            var items = await _itemService.GetTaiSanDuoi10Trieu(strDonViId: searchModel.StrDonViId, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = PrepareCauHinhCanhBaoTaiSanListModel(items, searchModel.Page);
            return model;
        }

        public async Task<TaiSanListModel> PrepareDanhSachOtoSaiChoNgoi(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (string.IsNullOrEmpty(searchModel.StrDonViId))
                searchModel.StrDonViId = _workContext.CurrentDonVi.ID.ToString();

            var items = await _itemService.GetTaiSanOtoSaiSoChoNgoi(strDonViId: searchModel.StrDonViId, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = PrepareCauHinhCanhBaoTaiSanListModel(items, searchModel.Page);
            return model;
        }

        public async Task<TaiSanListModel> PrepareDanhSachTaiSanChuaTinhHaoMon(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (string.IsNullOrEmpty(searchModel.StrDonViId))
                searchModel.StrDonViId = _workContext.CurrentDonVi.ID.ToString();

            var items = await _itemService.GetTaiSanChuaTinhHaoMon(strDonViId: searchModel.StrDonViId, LoaiHinhTaiSanId: (int)searchModel.LOAI_HINH_TAI_SAN_ID, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.KeySearch);

            //prepare list model
            var model = PrepareCauHinhCanhBaoTaiSanListModel(items, searchModel.Page);
            return model;
        }

        private TaiSanListModel PrepareCauHinhCanhBaoTaiSanListModel(IPagedList<TaiSan> items, int Page)
        {
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                Data = items?.Select(c =>
                {
                    var m = c.ToModel<TaiSanModel>();
                    m.pageIndex = Page;
                    m.GUID = c.GUID;
                    m.strNguyenGiaVN = c.NGUYEN_GIA_BAN_DAU?.ToVNStringNumber();

                    if (c.LOAI_HINH_TAI_SAN_ID != null)
                        m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.enumLoaiHinhTaiSan);

                    if (c.LOAI_TAI_SAN_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(c.LOAI_TAI_SAN_DON_VI_ID ?? c.LOAI_TAI_SAN_ID.Value)?.ToModel<LoaiTaiSanModel>();
                        else
                            loaitaisan = _loaitaisanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0)?.ToModel<LoaiTaiSanModel>();
                        m.TenLoaiTaiSan = loaitaisan?.TEN;
                    }

                    if (c.DON_VI_ID > 0)
                    {
                        var donVi = _donviService.GetDonViById(c.DON_VI_ID ?? 0);
                        if (donVi != null)
                        {
                            var ldv = _loaiDonViService.GetLoaiDonViById(donVi.LOAI_DON_VI_ID ?? 0)?.TREE_NODE?.Split('-')?.FirstOrDefault();
                            m.TenDonVi = donVi.TEN;
                            m.TenLoaiDonVi = _loaiDonViService.GetLoaiDonViById(int.Parse(ldv ?? "0"))?.TEN;
                        }
                    }

                    // lấy tên đơn vị điều chuyển sang
                    var lyDoTang = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                    var idLyDoDieuChuyen = _lydobiendongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.TIEP_NHAN);
                    var yeuCau = _yeucauService.GetYeuCauMoiNhatByTaiSanId(c.ID);
                    if (yeuCau != null && yeuCau.LOAI_BIEN_DONG_ID == lyDoTang && yeuCau.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen)
                    {
                        var yctt = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
                        var tsdc = _itemService.GetTaiSanById(yctt.TAI_SAN_TRUOC_DIEU_CHUYEN_ID ?? 0);
                        if (tsdc != null)
                        {
                            m.TenDonViDieuChuyen = _donviService.GetDonViById(tsdc.DON_VI_ID ?? 0)?.TEN;
                        }

                    }

                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }

        #endregion

        #region Check logic tài sản 
        public bool CheckDonViSuNghiepKhongNhapXeChucDanh(TaiSanModel model)
        {
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
            {
                // loại đơn vị
                var LoaiHinhDonViId = _donviService.GetDonViById(model.DON_VI_ID ?? 0)?.LOAI_DON_VI_ID;
                var MaLoaiHinhDonVi = _loaiDonViService.GetLoaiDonViById(LoaiHinhDonViId ?? 0)?.TREE_NODE;
                var ListTreeNode = MaLoaiHinhDonVi?.Split('-').Select(c => int.Parse(c)).FirstOrDefault(); // mã cấp 1 
                var IdLoaiDonViSuNgiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_SU_NGHIEP).ID;
                if (ListTreeNode == IdLoaiDonViSuNgiep)
                {// loại ô tô        
                    var loaiOto = _loaitaisanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID ?? 0)?.OTO_LOAI_XE_ID ?? 0;
                    if (loaiOto == (int)enumLoaiXe.XeChucDanh)
                    {
                        return false;
                    }

                }
            }

            return true;
        }
        #endregion

        #region private Region

        private decimal? TinhGiaTriConLaiDieuChuyen1Phan(decimal? TAI_SAN_ID, DateTime? NGAY_BIEN_DONG_TRUOC_DIEU_CHUYEN, DateTime? NGAY_DIEU_CHUYEN, decimal? GIA_TRI_CON_LAI_TRUOC_DIEU_CHUYEN, decimal? NGUYEN_GIA_SAU_DIEU_CHUYEN)
        {
            var bienDongTruocDieuChuyen = _biendongService.GetBienDongMoiNhatByTaiSanId(TAI_SAN_ID, NGAY_BIEN_DONG_TRUOC_DIEU_CHUYEN);
            if (bienDongTruocDieuChuyen != null)
            {
                var bienDongChiTietTruocDieuChuyen = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruocDieuChuyen.ID);
                decimal giaTriConLaiCu = 0;
                if (NGAY_DIEU_CHUYEN.Value.Year != bienDongTruocDieuChuyen.NGAY_BIEN_DONG.Value.Year)
                {
                    //Lấy giá trị còn lại của tài sản từ bảng kt hao mòn vào kt khấu hao
                    //Tại thời điểm viết, module khấu hao chưa hoàn thiện - cần bổ sung sau
                    var haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: bienDongTruocDieuChuyen.TAI_SAN_ID, namBaoCao: (NGAY_BIEN_DONG_TRUOC_DIEU_CHUYEN.Value.Year - 1));
                    if (haoMonTaiSan != null)
                        giaTriConLaiCu = haoMonTaiSan.TONG_GIA_TRI_CON_LAI ?? 0;
                }
                else
                {
                    giaTriConLaiCu = bienDongChiTietTruocDieuChuyen.HM_GIA_TRI_CON_LAI ?? 0;
                }
                return giaTriConLaiCu - GIA_TRI_CON_LAI_TRUOC_DIEU_CHUYEN;
            }
            else
            {
                return NGUYEN_GIA_SAU_DIEU_CHUYEN;
            }
        }

        private enumTS_NGUYEN_GIA SetEnumTS_NGUYEN_GIA(bool isDuoi500, bool isTren500)
        {
            enumTS_NGUYEN_GIA enumTS_NG = enumTS_NGUYEN_GIA.TAT_CA;
            if (!isTren500 && isDuoi500 || isTren500 && !isDuoi500)
                if (isTren500)
                    enumTS_NG = enumTS_NGUYEN_GIA.TREN_500_TRIEU;
                else if (isDuoi500)
                    enumTS_NG = enumTS_NGUYEN_GIA.DUOI_500_TRIEU;
            return enumTS_NG;
        }

        public string LoadMaTaiSan(decimal? DonViId = 0, decimal? TaiSanId = 0, decimal? LoaiTaiSanId = 0, decimal? loaiHinhTaiSanId = 0)
        {
            var donVi = _donviService.GetDonViById(DonViId ?? 0);
            var loaiTS = new LoaiTaiSanModel();
            if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                // get tài sản vô hình, đặc thù  gốc
                decimal? parentId = LoaiTaiSanId;
                decimal? tree_level = 0;
                LoaiTaiSanDonVi taiSanDonVi = new LoaiTaiSanDonVi();
                do
                {
                    if (parentId == null)
                        break;
                    taiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    tree_level = taiSanDonVi.TREE_LEVEL;
                    parentId = taiSanDonVi.PARENT_ID;
                } while (tree_level > 2);
                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                loaiTS = taiSanDonVi.ToModel<LoaiTaiSanModel>();
            }
            else
                loaiTS = _loaitaisanService.GetLoaiTaiSanById(LoaiTaiSanId ?? 0).ToModel<LoaiTaiSanModel>();
            var MaTs = "";

            if (donVi != null && loaiTS != null)
            {
                MaTs = CommonHelper.GenMaTaiSan(donVi.MA, loaiTS.MA, TaiSanId ?? 0);
            }
            return MaTs;
        }


        #endregion private Region
    }
}