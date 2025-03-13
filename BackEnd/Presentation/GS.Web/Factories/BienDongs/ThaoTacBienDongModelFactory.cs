using GS.Core;
using GS.Core.Caching;
using GS.Core.Configuration;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.BienDongs
{
    public class ThaoTacBienDongModelFactory : IThaoTacBienDongModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IBienDongService _bienDongService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauNhatKyModelFactory _yeuCauNhatKyModelFactory;
        private readonly IBienDongModelFactory _bienDongModelFactory;
        private readonly ITaiSanService _taiSanService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly IBienDongChiTietModelFactory _bienDongChiTietModelFactory;
        private readonly ITaiSanNguonVonModelFactory _taiSanNguonVonModelFactory;
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly IYeuCauModelFactory _yeuCauModelFactory;
        private readonly ITaiSanHienTrangSuDungModelFactory _taiSanHienTrangSuDungModelFactory;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IHaoMonTaiSanModelFactory _haoMonTaiSanModelFactory;
        private readonly ITrungGianBDYCService _trungGianBDYCService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly GS.Services.Authentication.IAuthenticationService _authenticationService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly GSConfig _gSConfig;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
        private readonly ITaiSanKhauHaoService _taiSanKhauHaoService;
        private readonly ITaiSanKhauHaoModelFactory _taiSanKhauHaoModelFactory;
        private readonly IDiaBanService _diaBanService;

        #endregion Fields

        #region Ctor

        public ThaoTacBienDongModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IBienDongService bienDongService,
            ITaiSanNguonVonService taiSanNguonVonService,
            IBienDongChiTietService bienDongChiTietService,
            IYeuCauService yeuCauService,
            IYeuCauNhatKyModelFactory yeuCauNhatKyModelFactory,
            IBienDongModelFactory bienDongModelFactory,
            ITaiSanService taiSanService,
            IYeuCauChiTietService yeuCauChiTietService,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            IHoatDongService hoatDongService,
            IBienDongChiTietModelFactory bienDongChiTietModelFactory,
            ITaiSanNguonVonModelFactory taiSanNguonVonModelFactory,
            ITaiSanModelFactory taiSanModelFactory,
            IYeuCauModelFactory yeuCauModelFactory,
            ITaiSanHienTrangSuDungModelFactory taiSanHienTrangSuDungModelFactory,
            ITaiSanNhatKyService taiSanNhatKyService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IHaoMonTaiSanModelFactory haoMonTaiSanModelFactory,
            ITrungGianBDYCService trungGianBDYCService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanDatService taiSanDatService,
            ITaiSanOtoService taiSanOtoService,
            GS.Services.Authentication.IAuthenticationService authenticationService,
            INguoiDungService nguoiDungService,
            GSConfig gSConfig,
            IDongBoFactory dongBoFactory,
            ICauHinhModelFactory cauHinhModelFactory,
            ILyDoBienDongService lyDoBienDongService,
            ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
            ITaiSanKhauHaoService taiSanKhauHaoService,
            ITaiSanKhauHaoModelFactory taiSanKhauHaoModelFactory,
            IDiaBanService diaBanService

            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._bienDongService = bienDongService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._yeuCauService = yeuCauService;
            this._yeuCauNhatKyModelFactory = yeuCauNhatKyModelFactory;
            this._bienDongModelFactory = bienDongModelFactory;
            this._taiSanService = taiSanService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._hoatDongService = hoatDongService;
            this._bienDongChiTietModelFactory = bienDongChiTietModelFactory;
            this._taiSanNguonVonModelFactory = taiSanNguonVonModelFactory;
            this._taiSanModelFactory = taiSanModelFactory;
            this._yeuCauModelFactory = yeuCauModelFactory;
            this._taiSanHienTrangSuDungModelFactory = taiSanHienTrangSuDungModelFactory;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._haoMonTaiSanModelFactory = haoMonTaiSanModelFactory;
            this._trungGianBDYCService = trungGianBDYCService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanDatService = taiSanDatService;
            this._taiSanOtoService = taiSanOtoService;
            this._dongBoFactory = dongBoFactory;
            this._authenticationService = authenticationService;
            this._nguoiDungService = nguoiDungService;
            this._gSConfig = gSConfig;
            this._dongBoFactory = dongBoFactory;
            this._cauHinhModelFactory = cauHinhModelFactory;
            this._lyDoBienDongService = lyDoBienDongService;
            this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
            this._taiSanKhauHaoService = taiSanKhauHaoService;
            this._taiSanKhauHaoModelFactory = taiSanKhauHaoModelFactory;
            this._diaBanService = diaBanService;
        }

        #endregion Ctor

        #region Function

        public ResultAction DuyetYeuCauFunc(decimal YeuCauId, YeuCau yeuCau = null)
        {
            if (yeuCau == null)
                yeuCau = _yeuCauService.GetYeuCauById(YeuCauId);

            #region isValid

            var CountYC_Chua_Duyet_TruocDo = _yeuCauService.CountYeuCauCuaTaiSan(yeuCau.TAI_SAN_ID, new List<decimal> { (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET, (int)enumTRANG_THAI_YEU_CAU.TU_CHOI }, null, yeuCau.NGAY_BIEN_DONG);
            if (CountYC_Chua_Duyet_TruocDo > 0)
                return new ResultAction(false, "Phải duyệt biến động trước đó.");

            #region kiểm tra tài sản khấu hao đã có bản ghi lớn hơn ngày duyệt yêu cầu
            //check ts có tính khấu hao

            var yeucauchitietts = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            if (yeucauchitietts.KH_GIA_TINH_KHAU_HAO > 0 && yeucauchitietts.KH_GIA_TINH_KHAU_HAO.HasValue)
            {
                var CountTaiSanKhauHao = _taiSanKhauHaoService.CountTaiSanKhauHao(yeuCau.TAI_SAN_ID, ngaytinhkhauhao: DateTime.Now);
                if (CountTaiSanKhauHao > 0)
                    return new ResultAction(false, "Tài sản đã thay đổi chế độ khấu hao sau ngày duyệt biến dộng");
            }
            #endregion
            #endregion isValid

            #region Cập nhật tạm thời trạng thái của yêu cầu

            //cập nhật lại yêu cầu
            yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.DA_DUYET;
            _yeuCauService.UpdateYeuCau(yeuCau);

            #endregion Cập nhật tạm thời trạng thái của yêu cầu

            #region Cập nhật tài sản

            //cập nhật tài sản
            var ts = _taiSanService.GetTaiSanById(yeuCau.TAI_SAN_ID);
            if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                ts.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO;
            else
                ts.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            if (ts.NGUYEN_GIA_BAN_DAU < 50000000)
            {
                ts.PHAN_LOAI_TAI_SAN = 1;
            }

            _taiSanService.UpdateTaiSan(ts);

            #endregion Cập nhật tài sản

            #region Thêm mới biến động

            //lấy ra biến động gần nhất trước đó
            var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(yeuCau.TAI_SAN_ID);
            //insert biendong

            //khởi tạo biến động từ yêu cầu
            var yeucauModel = yeuCau.ToModel<YeuCauModel>();
            var biendong = yeucauModel.ToEntity<BienDong>();

            //gán lại giá trị mặc định
            biendong.ID = 0;
            biendong.NGAY_DUYET = DateTime.Now;
            biendong.NGUOI_DUYET_ID = _workContext.CurrentCustomer.ID;
            biendong.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.DA_DUYET;

            //nếu có biến động trước là thay đổi thông tin
            //gán lại giá trị đã thay đổi của biến động
            _bienDongModelFactory.PrepareBienDongFromBDTDTT(biendong, bienDongTruoc);

            //khởi tạo biến động chi tiết từ yêu cầu chi tiết
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();
            //gán giá trị diện tích sang bảng biến động
            biendong.DAT_TONG_DIEN_TICH = biendongchitiet.DAT_TONG_DIEN_TICH;
            biendong.NHA_TONG_DIEN_TICH_XD = biendongchitiet.NHA_TONG_DIEN_TICH_XD;
            biendong.VKT_DIEN_TICH = biendongchitiet.VKT_DIEN_TICH;

            //hiện trạng của biến động chi tiết
            if (biendongchitiet.HTSD_JSON == null) biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(ts.ID);

            //tính lại giá trị còn lại của biến động
            _bienDongModelFactory.TinhGiaTriConLaiBienDong(biendong, biendongchitiet);
            _bienDongService.InsertBienDong(biendong);

            #endregion Thêm mới biến động

            #region Thêm mới biến động chi tiết
            // gán giá trị mặc định
            biendongchitiet.BIEN_DONG_ID = biendong.ID;

            // nếu có biến động trước
            // giữ lại một số trường dùng chung
            // thay đổi thông tin thì lấy trường mới
            if (bienDongTruoc != null && biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                var bdct_Truoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruoc.ID);
                _bienDongChiTietModelFactory.PrepareBienDongChiTietFromBDCT_TDTT(biendongchitiet, bdct_Truoc);
            }

            //lấy thông tin *không thay đổi* từ biến động đầu
            if (biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                var bienDongDau = _bienDongService.GetBienDongCuNhatByTaiSanId(biendong.TAI_SAN_ID);
                if (bienDongDau != null)
                {
                    var bdCT = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongDau.ID);
                    if (bdCT != null)
                    {
                        biendongchitiet.THONG_SO_KY_THUAT = bdCT.THONG_SO_KY_THUAT;
                        biendongchitiet.NHA_SO_TANG = bdCT.NHA_SO_TANG;
                        biendongchitiet.NHA_NAM_XAY_DUNG = bdCT.NHA_NAM_XAY_DUNG;
                        biendongchitiet.HINH_THUC_MUA_SAM_ID = bdCT.HINH_THUC_MUA_SAM_ID;
                        biendongchitiet.MUC_DICH_SU_DUNG_ID = bdCT.MUC_DICH_SU_DUNG_ID;
                        biendongchitiet.OTO_SO_KHUNG = bdCT.OTO_SO_KHUNG;
                        biendongchitiet.OTO_SO_MAY = bdCT.OTO_SO_MAY;
                        biendongchitiet.OTO_XI_LANH = bdCT.OTO_XI_LANH;
                    }
                }
            }
            _bienDongChiTietService.InsertBienDongChiTiet(biendongchitiet, false);

            #endregion Thêm mới biến động chi tiết

            #region Thêm mới tài sản nguồn vốn

            //insert Nguon Von cua bien dong
            _taiSanNguonVonModelFactory.InsertTaiSanNguonVonFromYeuCau(yeuCau, biendong);

            #endregion Thêm mới tài sản nguồn vốn

            #region Thêm mới tài sản hiện trạng sử dụng

            //insert Hiện trạng sử dụng
            _taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong(biendong.ID, biendong.TAI_SAN_ID, biendongchitiet.HTSD_JSON);

            #endregion Thêm mới tài sản hiện trạng sử dụng

            #region Một số trường hợp với loại biến động đặc biệt

            #region Điều chuyển/ Giảm 1 phần

            ////nếu là điều chuyển thì phải thêm tài sản mới
            ///
            var idLyDoDieuChuyen = _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
            if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && yeuCau.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen)
                _taiSanModelFactory.DieuChuyenTaiSan((int)biendong.TAI_SAN_ID, biendongchitiet);
            else if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                _taiSanModelFactory.DieuChuyenMotPhan(yeuCau, biendongchitiet);

            #endregion Điều chuyển/ Giảm 1 phần

            #region Thay đổi thông tin

            //nếu là thay đổi thông tin thì phải update lại tài sản
            else if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                UpdateThayDoiThongTinTS(ts, biendong, biendongchitiet);

            #endregion Thay đổi thông tin

            #endregion Một số trường hợp với loại biến động đặc biệt

            #region Xóa yêu cầu

            //xóa yêu cầu
            //yeuCau.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA;
            //_yeuCauService.UpdateYeuCau(yeuCau);
            _yeuCauChiTietService.DeleteYeuCauChiTiet(yeucauchitiet);
            _yeuCauService.DeleteYeuCau(yeuCau);

            #endregion Xóa yêu cầu

            #region Thêm mới yêu cầu nhật ký và hoạt động

            //yêu cầu nhật ký
            _yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucauModel, enumLOAI_NHATKY_YEUCAU.DUYET);
            //hoạt động
            _hoatDongService.InsertHoatDong(enumHoatDong.Duyet, "Duyệt dữ liệu", yeucauModel, "YeuCau");

            #endregion Thêm mới yêu cầu nhật ký và hoạt động

            #region Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho

            //nếu duyệt biến động tăng mới thì thêm tài sản vào bảng DB_NHAT_KY_TAI_SAN phục vụ cho gửi dữ liệu tài sản sang KHO
            if (biendong.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(biendong.TAI_SAN_ID);
                if (taiSanNhatKy == null)
                {
                    taiSanNhatKy = new TaiSanNhatKy();
                    taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
                    taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
                    _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
                }
                else
                {
                    taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
                    taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
            }
            // nếu không phải biến động tăng mới thì cập nhật trạng thái nhật ký đồng bộ tài sản
            else
            {
                TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(ts.ID);
                if (tsnk != null)
                {
                    // nếu trước đó đã có một biến động đồng bộ thất bại thì sẽ không đồng bộ nữa.
                    if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI)
                    {
                        return new ResultAction(true, "Duyệt thành công");
                    }
                    tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idAdd: new List<decimal>() { biendong.ID });
                    if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
                    {
                        tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                    }
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                }
                //_taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
            }

            #endregion Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho

            #region Đồng bộ biến động

            PrameterDongBoTaiSanModel prameterDongBo = new PrameterDongBoTaiSanModel()
            {
                LoaiBienDongId = biendong.LOAI_BIEN_DONG_ID,
                TaiSanId = biendong.TAI_SAN_ID,
                LoaiHinhTaiSanId = biendong.LOAI_HINH_TAI_SAN_ID,
                BienDongId = biendong.ID
            };
            _dongBoFactory.DongBoBienDong(prameterDongBo);

            #endregion Đồng bộ biến động

            #region Cập nhật tài sản khấu hao
            // lấy ra tài sản
            //check tài sản có tính khấu hao
            var loaiTSkhauhao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(ts.LOAI_TAI_SAN_ID, ts.DON_VI_ID);
            // check TS_taisankhauhao có tồn tại bản ghi
            if (loaiTSkhauhao != null)
            {
                TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(ts.ID);
                DateTime? ngaybatdaukhauhao = new DateTime();
                if (biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    ngaybatdaukhauhao = biendongchitiet.KH_NGAY_BAT_DAU;
                }
                else
                {
                    ngaybatdaukhauhao = biendong.NGAY_BIEN_DONG;
                }
                var ts_taisanKhauhaoNew = new TaiSanKhauHaoModel
                {
                    TAI_SAN_ID = ts.ID,
                    NGAY_BAT_DAU = ngaybatdaukhauhao,
                    TY_LE_KHAU_HAO = loaiTSkhauhao.TY_LE_KHAU_HAO ?? 0,
                    SO_THANG_KHAU_HAO = loaiTSkhauhao.THOI_HAN_SU_DUNG ?? 0,
                    TY_LE_NGUYEN_GIA_KHAU_HAO = (biendongchitiet.NGUYEN_GIA ?? 0) / (biendongchitiet.KH_GIA_TINH_KHAU_HAO ?? 0) * 100
                };

                if (taiSanKhauHao != null)
                {
                    if (taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO != null)
                    {
                        ts_taisanKhauhaoNew.TY_LE_NGUYEN_GIA_KHAU_HAO = taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO;
                    }
                    _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(taiSanKhauHao, ts_taisanKhauhaoNew);
                }
                else
                    _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(null, ts_taisanKhauhaoNew);
                //
            }
            #endregion

            #region chốt hm
            if (ts.ID > 0)
            {
                var hm = _taiSanService.ChotToanBoHaoMonTS(ts.ID);
            }
            #endregion
            return new ResultAction(true, "Duyệt thành công");
        }
        public ResultAction DuyetYeuCauFunc1(decimal bienDongId)
        {
            BienDong biendong = _bienDongService.GetBienDongById(bienDongId);
            YeuCau yeuCau = _yeuCauService.GetYeuCau(biendong.TAI_SAN_ID, biendong.NGAY_BIEN_DONG.Value, biendong.LOAI_BIEN_DONG_ID.Value);
            TaiSan ts = _taiSanService.GetTaiSanById(biendong.TAI_SAN_ID);
            #region Thêm mới biến động

            //lấy ra biến động gần nhất trước đó
            var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(biendong.TAI_SAN_ID);
            //insert biendong

            //khởi tạo biến động từ yêu cầu
            //var yeucauModel = yeuCau.ToModel<YeuCauModel>();



            //khởi tạo biến động chi tiết từ yêu cầu chi tiết
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();

            //hiện trạng của biến động chi tiết
            if (biendongchitiet.HTSD_JSON == null) biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(biendong.TAI_SAN_ID);

            //tính lại giá trị còn lại của biến động
            _bienDongModelFactory.TinhGiaTriConLaiBienDong(biendong, biendongchitiet);

            #endregion Thêm mới biến động

            #region Thêm mới biến động chi tiết
            // gán giá trị mặc định
            biendongchitiet.BIEN_DONG_ID = biendong.ID;

            // nếu có biến động trước
            // giữ lại một số trường dùng chung
            // thay đổi thông tin thì lấy trường mới
            if (bienDongTruoc != null && biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                var bdct_Truoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruoc.ID);
                _bienDongChiTietModelFactory.PrepareBienDongChiTietFromBDCT_TDTT(biendongchitiet, bdct_Truoc);
            }

            //lấy thông tin *không thay đổi* từ biến động đầu
            if (biendong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                var bienDongDau = _bienDongService.GetBienDongCuNhatByTaiSanId(biendong.TAI_SAN_ID);
                if (bienDongDau != null)
                {
                    var bdCT = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongDau.ID);
                    if (bdCT != null)
                    {
                        biendongchitiet.THONG_SO_KY_THUAT = bdCT.THONG_SO_KY_THUAT;
                        biendongchitiet.NHA_SO_TANG = bdCT.NHA_SO_TANG;
                        biendongchitiet.NHA_NAM_XAY_DUNG = bdCT.NHA_NAM_XAY_DUNG;
                        biendongchitiet.HINH_THUC_MUA_SAM_ID = bdCT.HINH_THUC_MUA_SAM_ID;
                        biendongchitiet.MUC_DICH_SU_DUNG_ID = bdCT.MUC_DICH_SU_DUNG_ID;
                        biendongchitiet.OTO_SO_KHUNG = bdCT.OTO_SO_KHUNG;
                        biendongchitiet.OTO_SO_MAY = bdCT.OTO_SO_MAY;
                        biendongchitiet.OTO_XI_LANH = bdCT.OTO_XI_LANH;
                    }
                }
            }
            _bienDongChiTietService.InsertBienDongChiTiet(biendongchitiet, false);

            #endregion Thêm mới biến động chi tiết

            #region Thêm mới tài sản nguồn vốn

            //insert Nguon Von cua bien dong
            _taiSanNguonVonModelFactory.InsertTaiSanNguonVonFromYeuCau(yeuCau, biendong);

            #endregion Thêm mới tài sản nguồn vốn

            #region Thêm mới tài sản hiện trạng sử dụng

            //insert Hiện trạng sử dụng
            _taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong(biendong.ID, biendong.TAI_SAN_ID, biendongchitiet.HTSD_JSON);

            #endregion Thêm mới tài sản hiện trạng sử dụng

            #region Một số trường hợp với loại biến động đặc biệt

            #region Điều chuyển/ Giảm 1 phần

            ////nếu là điều chuyển thì phải thêm tài sản mới
            ///
            var idLyDoDieuChuyen = _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
            if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && yeuCau.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen)
                _taiSanModelFactory.DieuChuyenTaiSan((int)biendong.TAI_SAN_ID, biendongchitiet);
            else if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                _taiSanModelFactory.DieuChuyenMotPhan(yeuCau, biendongchitiet);

            #endregion Điều chuyển/ Giảm 1 phần

            #region Thay đổi thông tin

            //nếu là thay đổi thông tin thì phải update lại tài sản
            else if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                UpdateThayDoiThongTinTS(ts, biendong, biendongchitiet);

            #endregion Thay đổi thông tin

            #endregion Một số trường hợp với loại biến động đặc biệt

            #region Xóa yêu cầu

            //xóa yêu cầu
            yeuCau.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA;
            _yeuCauService.UpdateYeuCau(yeuCau);
            //_yeuCauChiTietService.DeleteYeuCauChiTiet(yeucauchitiet);
            //_yeuCauService.DeleteYeuCau(yeuCau);

            #endregion Xóa yêu cầu

            #region Thêm mới yêu cầu nhật ký và hoạt động

            //yêu cầu nhật ký
            //_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucauModel, enumLOAI_NHATKY_YEUCAU.DUYET);
            //hoạt động
            //_hoatDongService.InsertHoatDong(enumHoatDong.Duyet, "Duyệt dữ liệu", yeucauModel, "YeuCau");

            #endregion Thêm mới yêu cầu nhật ký và hoạt động

            #region Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho

            //nếu duyệt biến động tăng mới thì thêm tài sản vào bảng DB_NHAT_KY_TAI_SAN phục vụ cho gửi dữ liệu tài sản sang KHO
            if (biendong.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(biendong.TAI_SAN_ID);
                if (taiSanNhatKy == null)
                {
                    taiSanNhatKy = new TaiSanNhatKy();
                    taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
                    taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
                    _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
                }
                else
                {
                    taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
                    taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
            }
            // nếu không phải biến động tăng mới thì cập nhật trạng thái nhật ký đồng bộ tài sản
            else
            {
                TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(ts.ID);
                // nếu trước đó đã có một biến động đồng bộ thất bại thì sẽ không đồng bộ nữa.
                if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI)
                {
                    return new ResultAction(true, "Duyệt thành công");
                }
                if (tsnk != null)
                {
                    tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idAdd: new List<decimal>() { biendong.ID });
                    if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
                    {
                        tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                    }
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
                }
                _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
            }

            #endregion Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho

            #region Đồng bộ biến động

            PrameterDongBoTaiSanModel prameterDongBo = new PrameterDongBoTaiSanModel()
            {
                LoaiBienDongId = biendong.LOAI_BIEN_DONG_ID,
                TaiSanId = biendong.TAI_SAN_ID,
                LoaiHinhTaiSanId = biendong.LOAI_HINH_TAI_SAN_ID,
                BienDongId = biendong.ID
            };
            _dongBoFactory.DongBoBienDong(prameterDongBo);

            #endregion Đồng bộ biến động

            #region Cập nhật tài sản khấu hao
            // lấy ra tài sản
            //check tài sản có tính khấu hao
            var loaiTSkhauhao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(ts.LOAI_TAI_SAN_ID, ts.DON_VI_ID);
            // check TS_taisankhauhao có tồn tại bản ghi
            if (loaiTSkhauhao != null)
            {
                TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(ts.ID);
                DateTime? ngaybatdaukhauhao = new DateTime();
                if (biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    ngaybatdaukhauhao = biendongchitiet.KH_NGAY_BAT_DAU;
                }
                else
                {
                    ngaybatdaukhauhao = biendong.NGAY_BIEN_DONG;
                }
                var ts_taisanKhauhaoNew = new TaiSanKhauHaoModel
                {
                    TAI_SAN_ID = ts.ID,
                    NGAY_BAT_DAU = ngaybatdaukhauhao,
                    TY_LE_KHAU_HAO = loaiTSkhauhao.TY_LE_KHAU_HAO,
                    SO_THANG_KHAU_HAO = loaiTSkhauhao.THOI_HAN_SU_DUNG,
                    TY_LE_NGUYEN_GIA_KHAU_HAO = biendongchitiet.NGUYEN_GIA / biendongchitiet.KH_GIA_TINH_KHAU_HAO * 100
                };

                if (taiSanKhauHao != null)
                {
                    if (taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO != null)
                    {
                        ts_taisanKhauhaoNew.TY_LE_NGUYEN_GIA_KHAU_HAO = taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO;
                    }
                    _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(taiSanKhauHao, ts_taisanKhauhaoNew);
                }
                else
                    _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(null, ts_taisanKhauhaoNew);
                //
            }
            #endregion

            return new ResultAction(true, "Duyệt thành công");
        }
        public ResultAction CapNhatTaiSanThayDoiDiaBanHienTrang(decimal YeuCauId, YeuCau yeuCau = null)
        {
            #region Chuẩn bị thông tin 
            if (yeuCau == null)
                yeuCau = _yeuCauService.GetYeuCauById(YeuCauId);

            var ts = _taiSanService.GetTaiSanById(yeuCau.TAI_SAN_ID);



            //lấy ra biến động gần nhất trước đó
            var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(yeuCau.TAI_SAN_ID);

            //khởi tạo biến động từ yêu cầu
            var yeucauModel = yeuCau.ToModel<YeuCauModel>();
            var biendong = yeucauModel.ToEntity<BienDong>();

            //khởi tạo biến động chi tiết từ yêu cầu chi tiết
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();
            #endregion Chuẩn bị thông tin 

            #region Update Hiện trạng tài sản
            //hiện trạng của biến động chi tiết
            if (biendongchitiet.HTSD_JSON == null)
            {
                biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(ts.ID);
            }

            // Update Hiện trạng của biến động gần nhất
            if (bienDongTruoc != null)
            {
                //Update hiện trạng json ở biến động
                _taiSanHienTrangSuDungModelFactory.UpdateHienTrangSuDungObjOfBienDongChiTiet(bienDongTruoc.ID, biendongchitiet.HTSD_JSON);
                //Update bảng tài sản hiện trạng theo biến động
                _taiSanHienTrangSuDungModelFactory.UpdateTaSanHienTrangForBienDong(bienDongTruoc.ID, ts.ID, biendongchitiet.HTSD_JSON);
            }

            #endregion

            #region Thay đổi thông tin địa bàn tài sản
            // update bảng tài sản
            UpdateThayDoiThongTinTS(ts, biendong, biendongchitiet);

            // update tất cả các biến động trước đó của tài sản
            var listBienDongCu = _bienDongService.GetBienDongsByTaiSanId(taiSanId: ts.ID);
            if (listBienDongCu?.Count > 0)
            {
                foreach (var bienDongCu in listBienDongCu)
                {
                    if (bienDongCu != null)
                    {
                        // nếu tài sản đất thì sửa cả tên
                        if (bienDongCu.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            bienDongCu.TAI_SAN_TEN = biendong.TAI_SAN_TEN;
                            _bienDongService.UpdateBienDong(bienDongCu, false);
                        }
                        var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongCu.ID);
                        if (bdct != null)
                        {
                            bdct.DIA_BAN_ID = biendongchitiet.DIA_BAN_ID;
                            bdct.DIA_CHI = biendongchitiet.DIA_CHI;
                            bdct.NHA_DIA_CHI = biendongchitiet.NHA_DIA_CHI;
                            _bienDongChiTietService.UpdateBienDongChiTiet(bdct, false);
                        }
                    }
                }
            }

            #endregion Thay đổi thông tin địa bàn tài sản

            #region Xóa yêu cầu

            //xóa yêu cầu
            _yeuCauChiTietService.DeleteYeuCauChiTiet(yeucauchitiet);
            _yeuCauService.DeleteYeuCau(yeuCau);

            #endregion Xóa yêu cầu

            #region  -Phần này tạm thời chưa làm, sau này thống nhất cách update thông tin tài sản sang kho sau
            //#region Thêm mới yêu cầu nhật ký và hoạt động

            ////yêu cầu nhật ký
            //_yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucauModel, enumLOAI_NHATKY_YEUCAU.DUYET);
            ////hoạt động
            //_hoatDongService.InsertHoatDong(enumHoatDong.Duyet, "Duyệt dữ liệu", yeucauModel, "YeuCau");

            //#endregion Thêm mới yêu cầu nhật ký và hoạt động

            //#region Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho 

            //////nếu duyệt biến động tăng mới thì thêm tài sản vào bảng DB_NHAT_KY_TAI_SAN phục vụ cho gửi dữ liệu tài sản sang KHO
            ////if (biendong.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            ////{
            ////    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(biendong.TAI_SAN_ID);
            ////    if (taiSanNhatKy == null)
            ////    {
            ////        taiSanNhatKy = new TaiSanNhatKy();
            ////        taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
            ////        taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
            ////        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
            ////        taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
            ////        taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
            ////        taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
            ////        _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
            ////    }
            ////    else
            ////    {
            ////        taiSanNhatKy.TAI_SAN_ID = biendong.TAI_SAN_ID;
            ////        taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = biendong.LOAI_HINH_TAI_SAN_ID;
            ////        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
            ////        taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
            ////        taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: null, idAdd: new List<decimal>() { biendong.ID });
            ////        taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
            ////        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            ////    }
            ////}
            ////// nếu không phải biến động tăng mới thì cập nhật trạng thái nhật ký đồng bộ tài sản
            ////else
            ////{
            ////    TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(ts.ID);
            ////    // nếu trước đó đã có một biến động đồng bộ thất bại thì sẽ không đồng bộ nữa.
            ////    if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI)
            ////    {
            ////        return new ResultAction(true, "Duyệt thành công");
            ////    }
            ////    if (tsnk != null)
            ////    {
            ////        tsnk.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(StringArrBDID: tsnk.BD_IDS, idAdd: new List<decimal>() { biendong.ID });
            ////        if (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
            ////        {
            ////            tsnk.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
            ////        }
            ////        _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
            ////    }
            ////    _taiSanNhatKyService.UpdateTaiSanNhatKy(tsnk);
            ////}

            //#endregion Nhật ký tài sản - phục vụ đồng bộ dữ liệu sang kho

            //#region Đồng bộ biến động

            //PrameterDongBoTaiSanModel prameterDongBo = new PrameterDongBoTaiSanModel()
            //{
            //    LoaiBienDongId = biendong.LOAI_BIEN_DONG_ID,
            //    TaiSanId = biendong.TAI_SAN_ID,
            //    LoaiHinhTaiSanId = biendong.LOAI_HINH_TAI_SAN_ID,
            //    BienDongId = biendong.ID
            //};
            //_dongBoFactory.DongBoBienDong(prameterDongBo);

            //#endregion Đồng bộ biến động

            //#region Cập nhật tài sản khấu hao
            //// lấy ra tài sản
            ////check tài sản có tính khấu hao
            //var loaiTSkhauhao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(ts.LOAI_TAI_SAN_ID, ts.DON_VI_ID);
            //// check TS_taisankhauhao có tồn tại bản ghi
            //if (loaiTSkhauhao != null)
            //{
            //    TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(ts.ID);
            //    DateTime? ngaybatdaukhauhao = new DateTime();
            //    if (biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || biendong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            //    {
            //        ngaybatdaukhauhao = biendongchitiet.KH_NGAY_BAT_DAU;
            //    }
            //    else
            //    {
            //        ngaybatdaukhauhao = biendong.NGAY_BIEN_DONG;
            //    }
            //    var ts_taisanKhauhaoNew = new TaiSanKhauHaoModel
            //    {
            //        TAI_SAN_ID = ts.ID,
            //        NGAY_BAT_DAU = ngaybatdaukhauhao,
            //        TY_LE_KHAU_HAO = loaiTSkhauhao.TY_LE_KHAU_HAO,
            //        SO_THANG_KHAU_HAO = loaiTSkhauhao.THOI_HAN_SU_DUNG,
            //        TY_LE_NGUYEN_GIA_KHAU_HAO = biendongchitiet.NGUYEN_GIA / biendongchitiet.KH_GIA_TINH_KHAU_HAO * 100
            //    };

            //    if (taiSanKhauHao != null)
            //    {
            //        if (taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO != null)
            //        {
            //            ts_taisanKhauhaoNew.TY_LE_NGUYEN_GIA_KHAU_HAO = taiSanKhauHao.TY_LE_NGUYEN_GIA_KHAU_HAO;
            //        }
            //        _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(taiSanKhauHao, ts_taisanKhauhaoNew);
            //    }
            //    else
            //        _taiSanKhauHaoModelFactory.insertOrUpdateTaiSanKhauHao(null, ts_taisanKhauhaoNew);
            //    //
            //}
            //#endregion
            #endregion

            return new ResultAction(true, "Duyệt thành công");
        }

        public ResultAction CapNhatSoChoNgoiOto(decimal YeuCauId, YeuCau yeuCau = null)
        {
            #region Chuẩn bị thông tin 
            if (yeuCau == null)
                yeuCau = _yeuCauService.GetYeuCauById(YeuCauId);

            var ts = _taiSanService.GetTaiSanById(yeuCau.TAI_SAN_ID);



            //lấy ra biến động gần nhất trước đó
            var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(yeuCau.TAI_SAN_ID);

            //khởi tạo biến động từ yêu cầu
            var yeucauModel = yeuCau.ToModel<YeuCauModel>();
            var biendong = yeucauModel.ToEntity<BienDong>();

            //khởi tạo biến động chi tiết từ yêu cầu chi tiết
            var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();
            #endregion Chuẩn bị thông tin 

            #region Update Hiện trạng tài sản
            //hiện trạng của biến động chi tiết
            if (biendongchitiet.HTSD_JSON == null)
            {
                biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(ts.ID);
            }

            // Update Hiện trạng của biến động gần nhất
            if (bienDongTruoc != null)
            {
                //Update hiện trạng json ở biến động
                _taiSanHienTrangSuDungModelFactory.UpdateHienTrangSuDungObjOfBienDongChiTiet(bienDongTruoc.ID, biendongchitiet.HTSD_JSON);
                //Update bảng tài sản hiện trạng theo biến động
                _taiSanHienTrangSuDungModelFactory.UpdateTaSanHienTrangForBienDong(bienDongTruoc.ID, ts.ID, biendongchitiet.HTSD_JSON);
            }

            #endregion

            #region Thay đổi thông tin số chỗ ngồi ô tô
            // update bảng tài sản
            UpdateThayDoiThongTinTS(ts, biendong, biendongchitiet);

            // update tất cả các biến động trước đó của tài sản
            var listBienDongCu = _bienDongService.GetBienDongsByTaiSanId(taiSanId: ts.ID);
            if (listBienDongCu?.Count > 0)
            {
                foreach (var bienDongCu in listBienDongCu)
                {
                    if (bienDongCu != null)
                    {
                        // nếu tài sản đất thì sửa cả tên
                        if (bienDongCu.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            bienDongCu.TAI_SAN_TEN = biendong.TAI_SAN_TEN;
                            _bienDongService.UpdateBienDong(bienDongCu, false);
                        }
                        var bdct = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongCu.ID);
                        if (bdct != null)
                        {
                            bdct.OTO_SO_CHO_NGOI = biendongchitiet.OTO_SO_CHO_NGOI;
                            _bienDongChiTietService.UpdateBienDongChiTiet(bdct, false);
                        }
                    }
                }
            }

            #endregion Thay đổi thông tin địa bàn tài sản

            #region Xóa yêu cầu

            //xóa yêu cầu
            _yeuCauChiTietService.DeleteYeuCauChiTiet(yeucauchitiet);
            _yeuCauService.DeleteYeuCau(yeuCau);

            #endregion Xóa yêu cầu
            return new ResultAction(true, "Duyệt thành công");
        }

        public ResultAction CapNhatBienDongSaiHienTrang(YeuCauChiTietModel model)
        {
            var bienDong = _bienDongService.GetBienDongById(model.BienDongSaiHienTrangId);
            var bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(model.BienDongSaiHienTrangId);
            var ts = _taiSanService.GetTaiSanById(model.TaiSanSaiHienTrangId);

            if (bienDongChiTiet != null && bienDong != null && ts != null)
            {
                try
                {
                    var hientrangList = new HienTrangList()
                    {
                        TaiSanId = model.TaiSanSaiHienTrangId,
                        lstObjHienTrang = model.lstHienTrang,
                    };
                    bienDongChiTiet.HTSD_JSON = hientrangList.toStringJson();
                    _taiSanHienTrangSuDungModelFactory.UpdateHienTrangSuDungObjOfBienDongChiTiet(bienDong.ID, bienDongChiTiet.HTSD_JSON);
                    //Update bảng tài sản hiện trạng theo biến động
                    _taiSanHienTrangSuDungModelFactory.UpdateTaSanHienTrangForBienDong(bienDong.ID, ts.ID, bienDongChiTiet.HTSD_JSON);
                    return new ResultAction(true, "Cập nhật thành công");
                }
                catch (Exception e)
                {

                    return new ResultAction(false, e.Message);
                }


            }
            return new ResultAction(false, "Không tồn tại biến động");
        }

        public ResultAction HuyDuyetBienDongFunc(decimal BienDongId, string Note)
        {
            BienDong bienDong = _bienDongService.GetBienDongById(BienDongId);

            #region IsValid

            if (bienDong == null)
                return new ResultAction(false, "Biến động không tồn tại");
            TaiSan taiSan = _taiSanService.GetTaiSanById(bienDong.TAI_SAN_ID);
            if (taiSan == null)
                return new ResultAction(false, "Tài sản không tồn tại");
            var countBD_Sau = _bienDongService.CountBDSau(taiSan.ID, bienDong.NGAY_BIEN_DONG.Value);
            if (countBD_Sau > 0)
                return new ResultAction(false, "Phải bỏ duyệt biến động đã duyệt trước đó");
            if (_cauHinhModelFactory.CheckYearIsLockSoTaiSan(bienDong.NGAY_BIEN_DONG.Value.Year))
                return new ResultAction(false, "Năm này đã bị khóa sổ");

            #region kiểm tra tài sản khấu hao đã có bản ghi lớn hơn ngày duyệt yêu cầu
            //check ts có tính khấu hao
            /*var bienDongChiTiets = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
            if (bienDongChiTiets.KH_GIA_TINH_KHAU_HAO > 0 && bienDongChiTiets.KH_GIA_TINH_KHAU_HAO.HasValue)
            {
                var CountTaiSanKhauHao = _taiSanKhauHaoService.CountTaiSanKhauHao(bienDong.TAI_SAN_ID, ngaytinhkhauhao: bienDongChiTiets.KH_NGAY_BAT_DAU);
                if (CountTaiSanKhauHao > 0)
                    return new ResultAction(false, "Tài sản đã thay đổi chế độ khấu hao sau ngày duyệt biến dộng");
            }*/
            #endregion
            #endregion IsValid

            #region Update trạng thái tài sản
            bool IsBdTangMoi = false;
            if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            {
                IsBdTangMoi = true;
                taiSan.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.TRA_LAI;
                _taiSanService.UpdateTaiSan(taiSan);
            }
            else if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
            {
                taiSan.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                _taiSanService.UpdateTaiSan(taiSan);
            }

            #endregion Update trạng thái tài sản

            BienDongChiTiet bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);

            #region Chuyển biến động thành yêu cầu

            //yêu cầu
            var yeuCau = new YeuCau(bienDong);
            yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.TU_CHOI;
            yeuCau.ID = 0;
            yeuCau.GUID = Guid.NewGuid();
            yeuCau.LY_DO_KHONG_DUYET = Note;
            yeuCau.IS_BIENDONG_CUOI = false;
            yeuCau.NGUOI_TAO_ID = bienDong.NGUOI_TAO_ID;
            yeuCau.NGUON_VON_JSON = _taiSanNguonVonModelFactory.GetNguonVonJsonFromListNguonVon(bienDong.ID);
            _yeuCauService.InsertYeuCau(yeuCau);
            //yêu cầu chi tiết
            YeuCauChiTiet yeuCauChiTiet = new YeuCauChiTiet(bienDongChiTiet);
            yeuCauChiTiet.YEU_CAU_ID = yeuCau.ID;
            _yeuCauChiTietService.InsertYeuCauChiTiet(yeuCauChiTiet);
            //yêu cầu nhật ký
            _yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(bienDong.ToModel<BienDongModel>(), enumLOAI_NHATKY_YEUCAU.TRA_LAI);

            #endregion Chuyển biến động thành yêu cầu

            #region Xóa biến động cũ

            //xóa tài sản nguồn vốn
            _bienDongModelFactory.DeleteBienDong(bienDong, bienDongChiTiet, false);

            #endregion Xóa biến động cũ

            #region Cập nhật chi tiết tài sản

            if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                var bdCuoi = _bienDongService.GetBienDongCuoiByTaiSanId(taiSan.ID);
                if (bdCuoi != null)
                {
                    var bdctCuoi = _bienDongChiTietService.GetBienDongChiTietByBDId(bdCuoi.ID);
                    UpdateThayDoiThongTinTS(taiSan, bdCuoi, bdctCuoi);
                }
            }

            #endregion Cập nhật chi tiết tài sản
            #region xoá tài sản khấu hao
            var taisankhauhao = _taiSanKhauHaoService.GetListTaiSanKhauHaoByTaiSanIdAndNgayBatDau(bienDong.TAI_SAN_ID, bienDong.NGAY_BIEN_DONG);
            if (taisankhauhao != null)
            {
                _taiSanKhauHaoService.DeleteTaiSanKhauHao(taisankhauhao);
                var getTaiSanKhauHao_cu = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(taisankhauhao.TAI_SAN_ID ?? 0);
                if (getTaiSanKhauHao_cu != null)
                {
                    getTaiSanKhauHao_cu.NGAY_KET_THUC = null;
                    _taiSanKhauHaoService.UpdateTaiSanKhauHao(getTaiSanKhauHao_cu);
                }
            }
            #endregion
            #region xóa bảng kt_hao_mon_tai_san
            // nếu biến động từ chối là biến động tăng mới hoặc nhập số dư đầu kỳ thì xóa bảng kt hao mòn
            if (IsBdTangMoi)
            {
                _haoMonTaiSanService.DeleteHmtsByIdTaiSan(bienDong.TAI_SAN_ID);
            }
            #endregion
            // _taiSanNhatKyService.UpdateTrangThaiTaiSanNhatKy(taiSan.ID);
            return new ResultAction(true, "Bỏ duyệt thành công");
        }

        public ResultAction KhongDuyetYeuCauFunc(decimal YeuCauId, string Note)
        {
            YeuCau yeucau = _yeuCauService.GetYeuCauById(YeuCauId);
            if (yeucau == null)
                return new ResultAction(false, "Biến động không tồn tại");
            var taisan = _taiSanService.GetTaiSanById(yeucau.TAI_SAN_ID);
            if (taisan == null)
                return new ResultAction(false, "Tài sản không tồn tại");

            #region Nếu không duyệt là biến động tăng mới

            if (yeucau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
            {
                taisan.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.TRA_LAI;
                _taiSanService.UpdateTaiSan(taisan);
            }

            #endregion Nếu không duyệt là biến động tăng mới

            #region Thay đổi trạng thái yêu cầu

            _yeuCauModelFactory.UpdateYeuCauTuChoi(yeucau, Note);
            _yeuCauNhatKyModelFactory.InsertYeuCauNhatKy(yeucau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.TRA_LAI);
            _hoatDongService.InsertHoatDong(enumHoatDong.HuyDuyet, "Không duyệt yêu cầu", yeucau.ToModel<YeuCauModel>(), "YeuCau");

            #endregion Thay đổi trạng thái yêu cầu

            return new ResultAction(true, "Từ chối thành công");
        }

        /// <summary>
        /// Cập nhật lại thuộc tính của tài sản theo biến động đã được duyệt
        /// </summary>
        /// <param name="taiSan"></param>
        /// <param name="bienDong"></param>
        /// <param name="bienDongChiTiet"></param>
        private void UpdateThayDoiThongTinTS(TaiSan taiSan, BienDong bienDong, BienDongChiTiet bienDongChiTiet)
        {
            if (taiSan != null && bienDong != null && bienDongChiTiet != null)
            {
                //thông tin chung
                taiSan.TEN = bienDong.TAI_SAN_TEN;
                taiSan.DON_VI_BO_PHAN_ID = bienDong.DON_VI_BO_PHAN_ID;
                if (taiSan.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                {
                    taiSan.LOAI_TAI_SAN_ID = bienDong.LOAI_TAI_SAN_ID;
                    taiSan.LOAI_TAI_SAN_DON_VI_ID = bienDong.LOAI_TAI_SAN_DON_VI_ID;
                }
                else
                    taiSan.LOAI_TAI_SAN_ID = bienDong.LOAI_TAI_SAN_ID;
                _taiSanService.UpdateTaiSan(taiSan);
                switch (taiSan.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var taiSanDat = _taiSanDatService.GetTaiSanDatByTaiSanId(taiSan.ID);
                        if (taiSanDat != null)
                        {
                            taiSanDat.DIA_BAN_ID = bienDongChiTiet.DIA_BAN_ID;
                            taiSanDat.DIA_CHI = bienDongChiTiet.DIA_CHI;
                            _taiSanDatService.GetDiaBanInfoByMaDiaBan(bienDongChiTiet.DIA_BAN_ID, taiSanDat);
                            _taiSanDatService.UpdateTaiSanDat(taiSanDat);
                        }
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var taiSanNha = _taiSanNhaService.GetTaiSanNhaById(taiSan.ID);
                        if (taiSanNha != null)
                        {
                            taiSanNha.DIA_CHI = bienDongChiTiet.DIA_CHI;
                            taiSanNha.DIA_BAN_ID = bienDongChiTiet.DIA_BAN_ID;
                            // trường hợp nhà không đất thì có thể thay đổi địa chỉ nhà nên  phải cập nhật lại cả Địa chỉ nhà nếu có thay đổi
                            if ((taiSanNha.TAI_SAN_DAT_ID ?? 0) <= 0)
                            {
                                taiSanNha.DIA_CHI = bienDongChiTiet.NHA_DIA_CHI;
                            }
                            _taiSanNhaService.GetDiaBanInfoByMaDiaBan(bienDongChiTiet.DIA_BAN_ID, taiSanNha);
                            _taiSanNhaService.UpdateTaiSanNha(taiSanNha);
                        }
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var taiSanOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(taiSan.ID);
                        if (taiSanOto != null)
                        {
                            taiSanOto.BIEN_KIEM_SOAT = bienDongChiTiet.OTO_BIEN_KIEM_SOAT;
                            taiSanOto.CONG_XUAT = bienDongChiTiet.OTO_CONG_XUAT;
                            taiSanOto.SO_CHO_NGOI = bienDongChiTiet.OTO_SO_CHO_NGOI;
                            taiSanOto.SO_CAU_XE = bienDongChiTiet.OTO_SO_CAU_XE;
                            taiSanOto.TAI_TRONG = bienDongChiTiet.OTO_TAI_TRONG;
                            taiSanOto.NHAN_XE_ID = bienDongChiTiet.OTO_NHAN_XE_ID;
                            taiSanOto.CHUC_DANH_ID = bienDongChiTiet.OTO_CHUC_DANH_ID;
                            _taiSanOtoService.UpdateTaiSanOto(taiSanOto);
                        }
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC:
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.KHAC:
                        break;

                    default:
                        break;
                }
            }
        }

        public ResultAction CapNhatQuyenXuLyTaiSan(TaiSan taiSan, decimal idToUser)
        {
            //biến động
            var bienDongs = _bienDongService.GetBienDongsByTaiSanId(taiSan.ID);
            _bienDongService.UpdateNguoiTaoBienDongs(bienDongs, idToUser);

            //yêu cầu
            var yeuCaus = _yeuCauService.GetYeuCaus(taiSan.ID);
            _yeuCauService.UpdateNguoiTaoYeuCaus(yeuCaus, idToUser);

            //tài sản
            taiSan.NGUOI_TAO_ID = idToUser;
            _taiSanService.UpdateTaiSan(taiSan);

            return new ResultAction(true, "");
        }

        #endregion Function
    }
}