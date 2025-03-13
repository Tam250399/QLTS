//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.KT;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Extensions;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.NghiepVu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.BienDongs
{
    public class BienDongModelFactory : IBienDongModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IBienDongService _itemService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IDonViService _donViService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ITaiSanService _taiSanService;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        private readonly ITrungGianBDYCService _trungGianBDYCService;
        private readonly ITaiSanModelFactory _taiSanModelFactory;

        #endregion Fields

        #region Ctor

        public BienDongModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IBienDongService itemService,
            ITaiSanNguonVonService taiSanNguonVonService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            IHaoMonTaiSanService haoMonTaiSanService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            ILoaiTaiSanService loaiTaiSanService,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            ITaiSanNhatKyService taiSanNhatKyService,
            ILyDoBienDongService lyDoBienDongService,
            IDongBoFactory dongBoFactory,
            IDonViService donViService,
            IHienTrangService hienTrangService,
            ITaiSanService taiSanService,
            IDB_QueueProcessService dB_QueueProcessService,
            ITrungGianBDYCService trungGianBDYCService,
            ITaiSanModelFactory taiSanModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._dongBoFactory = dongBoFactory;
            this._lyDoBienDongService = lyDoBienDongService;
            this._donViService = donViService;
            this._hienTrangService = hienTrangService;
            this._taiSanService = taiSanService;
            this._dB_QueueProcessService = dB_QueueProcessService;
            this._trungGianBDYCService = trungGianBDYCService;
            this._taiSanModelFactory = taiSanModelFactory;
        }

        #endregion Ctor

        #region BienDong

        public BienDongSearchModel PrepareBienDongSearchModel(BienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.ALL, (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };

            searchModel.LoaiBienDongAvailable = ((enumLOAI_LY_DO_TANG_GIAM)searchModel.enumLoaiHinhTaiSan).ToSelectList().ToList();
            searchModel.LoaiHinhTaiSanAvailable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DON_VI_ID = _workContext.CurrentDonVi.ID;

            //Trạng thái đồng bộ
            searchModel.TrangThaiDongBoAvailabe.Insert(0, (new SelectListItem { Text = "Đã đồng bộ", Value = "1" }));
            searchModel.TrangThaiDongBoAvailabe.Insert(1, (new SelectListItem { Text = "Chưa đồng bộ", Value = "2" }));
            searchModel.NguonTaiSanAvailable = (enumNguonTaiSan.TAT_CA).ToSelectList();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public BienDongListModel PrepareBienDongListModel(BienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            if (string.IsNullOrEmpty(searchModel.MaDonVi))
                searchModel.MaDonVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID)?.MA;
            var items = _itemService.SearchBienDongs(Keysearch: searchModel.KeySearch, MaDonVi: searchModel.MaDonVi, ListLoaiHinhTaiSan: searchModel.LstLoaiHinhTaiSan
                , ListLoaiBienDong: searchModel.LstLoaiBienDong, fromdate: searchModel.FromDate, todate: searchModel.ToDate, ListTrangThaiDongBo: searchModel.LstTrangThaiDongBo, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, nguonTaiSanId: searchModel.NguonTaiSanId);

            //prepare list model
            var model = new BienDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<BienDongModel>();
                    m.TenLoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID.GetValueOrDefault())?.TEN;
                    m.TenLyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(m.LY_DO_BIEN_DONG_ID.GetValueOrDefault())?.TEN;
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }

        public BienDongModel PrepareBienDongModel(BienDongModel model, BienDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<BienDongModel>();
            }
            //more

            return model;
        }

        public void PrepareBienDong(BienDongModel model, BienDong item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.TAI_SAN_MA = model.TAI_SAN_MA;
            item.TAI_SAN_TEN = model.TAI_SAN_TEN;
            item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.NGUYEN_GIA = model.NGUYEN_GIA;
            item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
            item.CHUNG_TU_SO = model.CHUNG_TU_SO;
            item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
            item.NGAY_BIEN_DONG = model.NGAY_BIEN_DONG;
            item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
            item.LOAI_BIEN_DONG_ID = model.LOAI_BIEN_DONG_ID;
            item.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.DON_VI_ID = model.DON_VI_ID;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.NGAY_TAO = model.NGAY_TAO;
            item.GUID = model.GUID;
        }

        public virtual void PrepareBienDongFromBDTDTT(BienDong item, BienDong bienDongTDTT = null)
        {
            if (bienDongTDTT == null)
                bienDongTDTT = _itemService.GetBienDongMoiNhatByTaiSanId(item.TAI_SAN_ID, item.NGAY_BIEN_DONG, (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN);

            if (bienDongTDTT != null && item.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            {
                item.TAI_SAN_TEN = bienDongTDTT.TAI_SAN_TEN;
                item.LOAI_TAI_SAN_ID = bienDongTDTT.LOAI_TAI_SAN_ID;
                item.DON_VI_BO_PHAN_ID = bienDongTDTT.DON_VI_BO_PHAN_ID;
                item.LOAI_TAI_SAN_DON_VI_ID = bienDongTDTT.LOAI_TAI_SAN_DON_VI_ID;
            }
        }

        public virtual void DeleteBienDong(BienDong bienDong, BienDongChiTiet bienDongChiTiet = null, bool isChotHaoMon = true)
        {
            //nguồn vốn
            var list_taiSanNguonVon = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: bienDong.TAI_SAN_ID, BienDongID: bienDong.ID);
            if (list_taiSanNguonVon != null && list_taiSanNguonVon.Count > 0)
                _taiSanNguonVonService.DeleteTaiSanNguonVons(list_taiSanNguonVon);
            //hiện trạng sử dụng
            var list_hienTrangSuDung = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(bienDong.ID);
            if (list_hienTrangSuDung != null && list_hienTrangSuDung.Count > 0)
                _taiSanHienTrangSuDungService.DeleteTaiSanHienTrangSuDungs(list_hienTrangSuDung);
            //xóa biến động chi tiết;
            if (bienDongChiTiet == null)
                bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
            _bienDongChiTietService.DeleteBienDongChiTiet(bienDongChiTiet);
            //xóa biến động
            // Tạm thời không chốt hao mòn 
            _itemService.DeleteBienDong(bienDong, isChotHaoMon);
            //cập nhật tài sản nhật ký- đồng bộ tài sản
            TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(bienDong.TAI_SAN_ID);
            if (taiSanNhatKy != null)
            {
                switch (taiSanNhatKy.TRANG_THAI_ID)
                {
                    case (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO:
                        {
                            taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.KHONG_DONG_BO;
                            break;
                        }
                    case (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO:
                        {
                            //taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            //taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.KHONG_DONG_BO;
                            break;
                        }
                    case (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI:
                        {
                            taiSanNhatKy.BD_IDS = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                            break;
                        }
                    case (decimal)enumTrangThaiTaiSanNhatKy.CHO_DONG_BO:
                        {
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                            break;
                        }
                    case (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA:
                        {
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.KHONG_DONG_BO;
                            break;
                        }
                    case (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI:
                        {
                            taiSanNhatKy.BD_IDS_DANG_DB = _taiSanNhatKyService.GenArrBienDongId(taiSanNhatKy.BD_IDS, new List<decimal>() { bienDong.ID });
                            if (bienDong.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                            {
                                taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.KHONG_DONG_BO;
                            }
                            else
                            {
                                taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO;
                            }
                            break;
                        }
                    default:
                        break;
                }
                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                if (!string.IsNullOrEmpty(bienDong.taisan.MA_DB))
                {
                    ParameterXoaBienDong prameterXoa = new ParameterXoaBienDong()
                    {
                        BienDongGuid = bienDong.GUID.ToString(),
                        DonViId = (int)bienDong.DON_VI_ID,
                        NgayBienDong = bienDong.NGAY_BIEN_DONG.Value,
                        MaTaiSanDb = bienDong.taisan.MA_DB
                    };
                    //_dongBoFactory.XoaBienDong(prameterXoa);
                    _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.DongBoXoaBienDong, obj: prameterXoa, level: (int)enumLevelQueueProcessDB.HIGHEST);
                }
                //Gửi thông tin xóa biến động
            }
        }

        /// <summary>
        /// Description: Fuction thực hiện tính lại giá trị của biến động khi được duyệt
        /// </summary>
        /// <param name="bienDong"></param>
        /// <param name="bienDongChiTiet"></param>
        public virtual void TinhGiaTriConLaiBienDong(BienDong bienDong, BienDongChiTiet bienDongChiTiet)
        {
            //những biến động này không cần tính lại GTCL
            //điều chỉnh giá trị tài sản là set lại giá trị còn lại
            if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
                _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.GIAM_DANH_GIA_LAI_GIA_TRI_TAI_SAN) == bienDong.LY_DO_BIEN_DONG_ID.Value ||
                _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.TANG_DANH_GIA_LAI_GIA_TRI) == bienDong.LY_DO_BIEN_DONG_ID.Value
                )
                return;

            var bienDongTruoc = _itemService.GetBienDongCuoiByTaiSanId(bienDong.TAI_SAN_ID);

            if (bienDongTruoc != null)
            {
                var BDChiTietTruoc = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongTruoc.ID);

                #region đối với 2 loại tài sản là đất và đặc thù

                //tài sản đất và đặc thù k có GTCL. Đặt mặc định là nguyên giá
                if (
                    bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT ||
                    bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    bienDongChiTiet.HM_GIA_TRI_CON_LAI = BDChiTietTruoc.HM_GIA_TRI_CON_LAI.GetValueOrDefault() + bienDong.NGUYEN_GIA.GetValueOrDefault();
                    return;
                }

                #endregion đối với 2 loại tài sản là đất và đặc thù

                else
                {
                    //Lấy giá trị còn lại của tài sản
                    decimal giaTriConLaiCu = 0;
                    var l_ngayBienDongTruoc = bienDong.NGAY_BIEN_DONG;
                    if (bienDong.NGAY_BIEN_DONG.Value.Year != bienDongTruoc.NGAY_BIEN_DONG.Value.Year)
                    {
                        //Lấy giá trị còn lại của tài sản từ bảng kt hao mòn vào kt khấu hao
                        //Tại thời điểm xây dựng, module khấu hao chưa hoàn thiện - cần bổ sung sau
                        var haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: bienDong.TAI_SAN_ID, namBaoCao: (bienDong.NGAY_BIEN_DONG.Value.Year - 1));
                        if (haoMonTaiSan != null)
                            giaTriConLaiCu = haoMonTaiSan.TONG_GIA_TRI_CON_LAI ?? 0;
                        l_ngayBienDongTruoc = new DateTime(bienDong.NGAY_BIEN_DONG.Value.Year - 1, 12, 31);
                    }
                    else
                    {
                        giaTriConLaiCu = BDChiTietTruoc.HM_GIA_TRI_CON_LAI ?? 0;
                        l_ngayBienDongTruoc = bienDongTruoc.NGAY_BIEN_DONG ?? DateTime.Now;
                    }
                    if (bienDong.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                    {
                        var bd_nguyen_gia = bienDong.NGUYEN_GIA ?? 0;
                        var giaTriConLai_New = giaTriConLaiCu;
                        var nguyenGiaMoi = _itemService.TinhNguyenGiaTaiSanOnlyBD(taiSanId: bienDong.TAI_SAN_ID, To_Date_BienDong: bienDong.NGAY_BIEN_DONG);
                        //Lấy tỷ lệ hao mòn từ loại tài sản id

                        if (bienDong.NGAY_BIEN_DONG.Value.Day == 31 && bienDong.NGAY_BIEN_DONG.Value.Month == 12)
                        {
                            nguyenGiaMoi = nguyenGiaMoi + bd_nguyen_gia;
                            decimal hm_tyLe = 0;
                            if (bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                            {
                                var lstVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(bienDong.LOAI_TAI_SAN_DON_VI_ID.Value);
                                if (lstVH != null)
                                    hm_tyLe = lstVH.HM_TY_LE ?? 0;
                            }
                            else
                            {
                                var lst = _loaiTaiSanService.GetLoaiTaiSanById(bienDong.LOAI_TAI_SAN_ID.Value);
                                if (lst != null)
                                    hm_tyLe = lst.HM_TY_LE ?? 0;
                            }
                            giaTriConLai_New = (CommonCalculate.GiaTriConLaiCu(giaTriConLaiCu, bienDong.NGAY_BIEN_DONG ?? DateTime.Now, l_ngayBienDongTruoc ?? DateTime.Now, nguyenGiaMoi, hm_tyLe)) ?? 0;
                        }
                        bienDongChiTiet.HM_GIA_TRI_CON_LAI = giaTriConLai_New + bd_nguyen_gia;
                    }
                    else
                        bienDongChiTiet.HM_GIA_TRI_CON_LAI = _yeuCauChiTietModelFactory.CalculateGTCLDieuChuyenMotPhan(bienDong.TAI_SAN_ID, bienDong.NGAY_BIEN_DONG, giaTriConLaiCu, Math.Abs(bienDong.NGUYEN_GIA ?? 0));
                }
            }
        }
        public List<BienDong> GetBienDongSaiHienTrangByTaiSan(Guid guidTS)
        {
            var taiSanItem = _taiSanService.GetTaiSanByGuId(guidTS);
            var listBienDong = new List<BienDong>();

            if (taiSanItem == null)
            {
                return null;
            }
            var bienDongs = _itemService.GetBienDongsByTaiSanId(taiSanItem.ID);
            if (bienDongs != null && bienDongs.Count() > 0)
            {
                foreach (var bd in bienDongs)
                {
                    if (bd != null)
                    {
                        var taiSanHienTrangs = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(bd.ID);
                        if (taiSanHienTrangs != null)
                        {
                            if (_hienTrangService.CheckIsListHienTrangNhapSai(taiSanItem.DON_VI_ID ?? 0, taiSanHienTrangs))
                            {
                                listBienDong.Add(bd);
                            }
                        }

                    }
                }
            }
            return listBienDong;
        }
        public BienDongListModel PrepareForListBienDongSaiHienTrang(BienDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = GetBienDongSaiHienTrangByTaiSan(searchModel.TaiSanGuid);

            //prepare list model
            var model = new BienDongListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<BienDongModel>();
                    m.TenLoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID.GetValueOrDefault())?.TEN;
                    m.TenLyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(m.LY_DO_BIEN_DONG_ID.GetValueOrDefault())?.TEN;
                    return m;
                }),
                Total = items.Count()
            };
            return model;
        }
        public YeuCauChiTietModel PrepareForBienDongSaiHienTrangModel(decimal BienDongId)
        {
            var model = new YeuCauChiTietModel();
            var bienDong = _itemService.GetBienDongById(BienDongId);
            var bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(BienDongId);

            if (bienDongChiTiet != null && bienDong != null)
            {
                model.BienDongSaiHienTrangId = BienDongId;
                model.DonViSaiHienTrangId = bienDong.DON_VI_ID;
                model.TaiSanSaiHienTrangId = bienDong.TAI_SAN_ID;
                model.LoaiHinhTaiSanSaiHienTrangId = bienDong.LOAI_HINH_TAI_SAN_ID ?? 0;
                var taiSanHienTrangs = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(BienDongId);
                if (taiSanHienTrangs != null && taiSanHienTrangs.Count() > 0)
                {
                    model.lstHienTrang = taiSanHienTrangs.Select(c =>
                    {
                        ObjHienTrang m = c.ToObjHienTrang();
                        m.TenHienTrang = _hienTrangService.GetHienTrangById(c.HIEN_TRANG_ID ?? 0)?.TEN_HIEN_TRANG;
                        m.IsOpenAll = true;
                        return m;
                    }).ToList();
                }
                model.NHA_TONG_DIEN_TICH_XD_CU = bienDongChiTiet.NHA_TONG_DIEN_TICH_XD;
                if (!(model.NHA_TONG_DIEN_TICH_XD_CU > 0)) // Có trường hợp biến động chi tiết truyền sang bị lỗi không có tổng diện tích
                {
                    model.NHA_TONG_DIEN_TICH_XD_CU = model.lstHienTrang.Sum(c => c.GiaTriNumber);
                }
                model.NHA_DIEN_TICH_XD_CU = bienDongChiTiet.NHA_DIEN_TICH_XD;

                model.DAT_TONG_DIEN_TICH_CU = bienDongChiTiet.DAT_TONG_DIEN_TICH;
                if (!(model.DAT_TONG_DIEN_TICH_CU > 0)) // Có trường hợp biến động chi tiết truyền sang bị lỗi không có tổng diện tích
                {
                    model.DAT_TONG_DIEN_TICH_CU = model.lstHienTrang.Sum(c => c.GiaTriNumber);
                }
                model.VKT_CHIEU_DAI_CU = bienDongChiTiet.VKT_CHIEU_DAI;
                model.VKT_DIEN_TICH_CU = bienDongChiTiet.VKT_DIEN_TICH;
                model.VKT_THE_TICH_CU = bienDongChiTiet.VKT_THE_TICH;
                model.TongHienTrangCheckbox = model.lstHienTrang.Sum(c => ((c.GiaTriCheckbox ?? false) ? 1 : 0));

            }
            return model;
        }


        #endregion BienDong
        #region validate 
        public List<string> ValidateListImportBdGiamTaiSan(ImportBdGiamTaiSanModel model)
        {
            List<string> ListError = new List<string>();
            string Error = $"Hàng {model.Row}:";
            int countError = 0;
            //var lydogiam = _lyDoBienDongService.GetLyDoBienDongById(Convert.ToDecimal(model.LY_DO_GIAM_ID));
            //lydogiam?.LOAI_LY_DO_ID == 5
            var LY_DO_GIAM_ID = Convert.ToInt32(model.LY_DO_GIAM_ID);
            if (Convert.ToInt32(model.LOAI_BIEN_DONG_ID) == 5)
            {
                if(LY_DO_GIAM_ID == (decimal)enumLY_DO_GIAM_THU_TIEN.THANH_LY || LY_DO_GIAM_ID == (decimal)enumLY_DO_GIAM_THU_TIEN.BAN_CHUYEN_NHUONG)
                {
                    if (string.IsNullOrEmpty(model.HINH_THUC_XU_LY_ID))
                    {
                        Error = String.Concat(Error, $" - Hình thức không được để trống");
                        countError++;
                    }
                    if (model.IS_BAN_THANH_LY == null)
                    {
                        Error = String.Concat(Error, $" - Chưa nhập đã bán hoặc thanh lý");
                        countError++;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.CHUNG_TU_SO))
                        {
                            Error = String.Concat(Error, $" - Số hợp đồng không được để trống");
                            countError++;
                        }
                        if (model.CHUNG_TU_NGAY == null)
                        {
                            Error = String.Concat(Error, $" - Ngày hợp đồng không được để trống");
                            countError++;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(model.CHUNG_TU_SO))
                    {
                        Error = String.Concat(Error, $" - Số biên bản không được để trống");
                        countError++;
                    }
                    if (model.CHUNG_TU_NGAY == null)
                    {
                        Error = String.Concat(Error, $" - Ngày biên bản không được để trống");
                        countError++;
                    }
                    if (LY_DO_GIAM_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.DIEU_CHUYEN)
                    {
                        if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                        {
                            Error = String.Concat(Error, $" - Đơn vị nhận điều chuyển không được để trống");
                            countError++;
                        }
                    }
                }
            }    
            if (countError > 0)
            {
                ListError.Add(Error);
            }
            return ListError;
        }

        public BienDongModel InsertToBienDongGiamTS(ImportBdGiamTaiSanModel item, BienDongModel model)
        {
            if (item != null)
            {
                model.TAI_SAN_ID = item.TAI_SAN_ID;
                model.TAI_SAN_MA = item.TAI_SAN_MA;
                model.TAI_SAN_TEN = item.TEN;
                model.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(item.LOAI_TAI_SAN_MA.Substring(0, 1));
                model.DON_VI_ID = _donViService.GetDonViByMa(item.DON_VI_MA).ID;
                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    model.LOAI_TAI_SAN_ID = _loaiTaiSanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA).ID;
                }
                else
                {
                    if (item.LOAI_TAI_SAN_MA != "9" && item.LOAI_TAI_SAN_MA != "10" && item.LOAI_TAI_SAN_MA != "902" && item.LOAI_TAI_SAN_MA != "903" && item.LOAI_TAI_SAN_MA != "904" && item.LOAI_TAI_SAN_MA != "905" && item.LOAI_TAI_SAN_MA != "906")
                    {
                        var donViLonNhat = _donViService.GetDonViLonNhat(model.DON_VI_ID ?? 0);
                        model.LOAI_TAI_SAN_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.ID;
                    }
                    else
                    {
                        var loaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa(item.LOAI_TAI_SAN_MA);
                        model.LOAI_TAI_SAN_ID = loaiTaiSanVoHinh?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = loaiTaiSanVoHinh?.ID;
                    }
                    /*var donViLonNhat = _donViService.GetDonViLonNhat(model.DON_VI_ID ?? 0);
                    model.LOAI_TAI_SAN_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.LOAI_TAI_SAN_ID;
                    model.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.ID;*/
                }
                model.QUYET_DINH_SO = item.QUYET_DINH_SO;
                model.QUYET_DINH_NGAY = item.QUYET_DINH_NGAY;
                model.LOAI_BIEN_DONG_ID = Convert.ToDecimal(item.LOAI_BIEN_DONG_ID);
                model.LY_DO_BIEN_DONG_ID = Convert.ToDecimal(item.LY_DO_GIAM_ID);
                model.NGAY_BIEN_DONG = item.NGAY_GIAM;//(int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO;
                //model.NGUYEN_GIA = - item.NGUYEN_GIA;
                model.CHUNG_TU_SO = item.CHUNG_TU_SO;
                model.CHUNG_TU_NGAY = item.CHUNG_TU_NGAY;
                model.NGAY_SU_DUNG = item.NGAY_SU_DUNG;
                //set trạng thái biến động là xóa
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA;
                model.NGAY_TAO = DateTime.Now;
                model.GHI_CHU = item.GHI_CHU;
            }
            BienDong bd = new BienDong();
            bd = model.ToEntity<BienDong>();
            //bd.DAT_TONG_DIEN_TICH = -item.DAT_TONG_DIEN_TICH;
            //bd.NHA_TONG_DIEN_TICH_XD = -item.NHA_TONG_DIEN_TICH_XD;
            //bd.VKT_DIEN_TICH = -item.VKT_DIEN_TICH;
            _itemService.InsertBienDong(bd);
            return model;
        }
        public BienDongChiTietModel InsertToBienDongChiTiet (ImportBdGiamTaiSanModel item, BienDongChiTietModel model, BienDongModel bd)
        {
            if(item != null)
            {
                model.BIEN_DONG_ID = bd.ID;
                model.HINH_THUC_XU_LY_ID = Convert.ToDecimal(item.HINH_THUC_XU_LY_ID);
                model.IS_BAN_THANH_LY = Convert.ToBoolean(item.IS_BAN_THANH_LY);
                model.DON_VI_NHAN_DIEU_CHUYEN_ID = item.DON_VI_NHAN_DIEU_CHUYEN_ID;
                model.PHI_THU = item.PHI_THU;
                model.PHI_BU_DAP = item.PHI_BU_DAP;
                model.PHI_NOP_NGAN_SACH = item.PHI_NOP_NGAN_SACH;
            }
            //giảm 1 phần
            /*if(bd.LOAI_BIEN_DONG_ID == 6)
            {
                model.NGUYEN_GIA = bd.NGUYEN_GIA;
                if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    model.DAT_TONG_DIEN_TICH = - item.DAT_TONG_DIEN_TICH;
                }
                if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    model.NHA_DIEN_TICH_XD = - item.NHA_DIEN_TICH_XD;
                    model.NHA_TONG_DIEN_TICH_XD = - item.NHA_TONG_DIEN_TICH_XD;
                }
                if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
                {
                    model.VKT_DIEN_TICH = -item.VKT_DIEN_TICH;
                }
            }*/
            BienDongChiTiet bdct = new BienDongChiTiet();
            bdct = model.ToEntity<BienDongChiTiet>();
            YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(bdct);
            yeuCau_json.DATA_JSON = null;
            bdct.DATA_JSON = yeuCau_json.toStringJson();
            //hiện trạng của biến động chi tiết
            var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: bd.LOAI_HINH_TAI_SAN_ID, isTSDA: _donViService.isDonViBanQuanLyDuAn(bd.DON_VI_ID.GetValueOrDefault()));
            var lstObjHienTrang = new List<ObjHienTrang>();
            
            foreach (var ht in lstHienTrang)
            {
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                lstObjHienTrang.Add(obj);
            }

            var hientrangList = new HienTrangList()
            {
                TaiSanId = bd.TAI_SAN_ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
            bdct.HTSD_JSON = yeuCau_json.HTSD_JSON;
            _bienDongChiTietService.InsertToBienDongChiTiet(bdct);

            ////nếu là điều chuyển thì phải thêm tài sản mới
            ///
            /*BienDong biendong = bd.ToEntity<BienDong>();
            YeuCau yeuCau = new YeuCau(biendong);
            if (bd.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && bd.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.DIEU_CHUYEN)
                _taiSanModelFactory.DieuChuyenTaiSan((int)yeuCau.TAI_SAN_ID, bdct);
            else if (bd.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                _taiSanModelFactory.DieuChuyenMotPhan(yeuCau, bdct);*/

            return bdct.ToModel<BienDongChiTietModel>();
        }
        #endregion validate
        public BienDong InsertToBdTangGiamNguyenGia(ImportBdTangGiamNguyenGiaModel item, BienDongModel model)
        {
            if (item != null) 
            {
                model.TAI_SAN_ID = item.TAI_SAN_ID;
                model.TAI_SAN_MA = item.TAI_SAN_MA;
                model.TAI_SAN_TEN = item.TEN;
                model.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(item.LOAI_TAI_SAN_MA.Substring(0, 1));
                model.DON_VI_ID = _donViService.GetDonViByMa(item.DON_VI_MA).ID;
                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                {
                    model.LOAI_TAI_SAN_ID = _loaiTaiSanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA).ID;
                }
                else
                {
                    if (item.LOAI_TAI_SAN_MA != "9" && item.LOAI_TAI_SAN_MA != "10" && item.LOAI_TAI_SAN_MA != "902" && item.LOAI_TAI_SAN_MA != "903" && item.LOAI_TAI_SAN_MA != "904" && item.LOAI_TAI_SAN_MA != "905" && item.LOAI_TAI_SAN_MA != "906")
                    {
                        var donViLonNhat = _donViService.GetDonViLonNhat(model.DON_VI_ID ?? 0);
                        model.LOAI_TAI_SAN_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat.ID)?.ID;
                    }
                    else
                    {
                        var loaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa(item.LOAI_TAI_SAN_MA);
                        model.LOAI_TAI_SAN_ID = loaiTaiSanVoHinh?.LOAI_TAI_SAN_ID;
                        model.LOAI_TAI_SAN_DON_VI_ID = loaiTaiSanVoHinh?.ID;
                    }
                }
                model.QUYET_DINH_SO = item.QUYET_DINH_SO;
                model.QUYET_DINH_NGAY = item.QUYET_DINH_NGAY;
                model.LOAI_BIEN_DONG_ID = Convert.ToDecimal(item.LOAI_BIEN_DONG_ID);
                model.LY_DO_BIEN_DONG_ID = _lyDoBienDongService.GetLyDoBienDongByMa(item.LY_DO_BIEN_DONG_MA).ID;
                model.NGAY_BIEN_DONG = item.NGAY_BIEN_DONG;
                if(model.LOAI_BIEN_DONG_ID == (decimal?)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI)
                {
                    model.NGUYEN_GIA = - Math.Abs(item.NV_NGAN_SACH + item.NV_KHAC);
                    if(model.LOAI_HINH_TAI_SAN_ID == (decimal?)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        model.DAT_TONG_DIEN_TICH = - Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
                    }
                    if (model.LOAI_HINH_TAI_SAN_ID == (decimal?)enumLOAI_HINH_TAI_SAN.NHA)
                    {
                        model.NHA_TONG_DIEN_TICH_XD = - Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
                    }
                }
                else
                {
                    model.NGUYEN_GIA = Math.Abs(item.NV_NGAN_SACH + item.NV_KHAC);
                    if (model.LOAI_HINH_TAI_SAN_ID == (decimal?)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        model.DAT_TONG_DIEN_TICH = Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
                    }
                    if (model.LOAI_HINH_TAI_SAN_ID == (decimal?)enumLOAI_HINH_TAI_SAN.NHA)
                    {
                        model.NHA_TONG_DIEN_TICH_XD = Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
                    }
                }
                model.NGAY_SU_DUNG = item.NGAY_SU_DUNG;
                //set trạng thái biến động là xóa
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA;
                model.NGAY_TAO = DateTime.Now;
                model.GHI_CHU = item.GHI_CHU;
            }
            BienDong bd = new BienDong();
            bd = model.ToEntity<BienDong>();
            _itemService.InsertBienDong(bd);
            return bd;
        }
        public BienDongChiTietModel InsertToBdTangGiamNguyenGiaChiTiet(ImportBdTangGiamNguyenGiaModel item, BienDongChiTietModel model, BienDongModel bd)
        {
            if (item != null)
            {
                model.BIEN_DONG_ID = bd.ID;
            }
            model.NGUYEN_GIA = bd.NGUYEN_GIA;
            decimal? kthm;
            if (bd.NGAY_BIEN_DONG.Day == 31 && bd.NGAY_BIEN_DONG.Month == 12)
            {
                kthm = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(bd.TAI_SAN_ID, bd.NGAY_BIEN_DONG.Year, null)?.TONG_GIA_TRI_CON_LAI ?? 0;
            }
            else
            {
                var namBDCuNhat = _itemService.GetBienDongCuNhatByTaiSanId(bd.TAI_SAN_ID)?.NGAY_BIEN_DONG.Value.Year;
                if (namBDCuNhat < bd.NGAY_BIEN_DONG.Year)
                {
                    kthm = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(bd.TAI_SAN_ID, bd.NGAY_BIEN_DONG.Year - 1, null)?.TONG_GIA_TRI_CON_LAI ?? 0;
                }
                else
                {
                    kthm = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(bd.TAI_SAN_ID, bd.NGAY_BIEN_DONG.Year, null)?.TONG_GIA_TRI_CON_LAI ?? 0;
                }
            }
            model.HM_GIA_TRI_CON_LAI = (kthm + bd.NGUYEN_GIA) > 0 ? (kthm + bd.NGUYEN_GIA) : 0;
            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
            {
                model.DAT_TONG_DIEN_TICH = bd.DAT_TONG_DIEN_TICH;//-Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
            }
            if (bd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
            {
                model.NHA_TONG_DIEN_TICH_XD = bd.NHA_TONG_DIEN_TICH_XD;//-Math.Abs(Convert.ToDecimal(item.DIEN_TICH_DATNHA_TANG));
            }
            BienDongChiTiet bdct = new BienDongChiTiet();
            bdct = model.ToEntity<BienDongChiTiet>();
            YeuCauChiTiet yeuCau_json = new YeuCauChiTiet(bdct);
            yeuCau_json.DATA_JSON = null;
            bdct.DATA_JSON = yeuCau_json.toStringJson();
            //hiện trạng của biến động chi tiết
            var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: bd.LOAI_HINH_TAI_SAN_ID, isTSDA: _donViService.isDonViBanQuanLyDuAn(bd.DON_VI_ID.GetValueOrDefault()));
            var lstObjHienTrang = new List<ObjHienTrang>();

            if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                foreach (var ht in lstHienTrang)
                {
                    var obj = new ObjHienTrang();
                    obj.HienTrangId = ht.ID;
                    obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                    obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                    obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                    switch (obj.HienTrangId)
                    {
                        case 72:
                            obj.GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC;
                            break;
                        case 73:
                            obj.GiaTriNumber = item.HTSD_HDSN_KHONG_KINH_DOANH;
                            break;
                        case 74:
                            obj.GiaTriNumber = item.HTSD_HDSN_KINH_DOANH;
                            break;
                        case 78:
                            obj.GiaTriNumber = item.HTSD_HDSN_CHO_THUE;
                            break;
                        case 79:
                            obj.GiaTriNumber = item.HTSD_HDSN_LDLK;
                            break;
                        case 81:
                            obj.GiaTriNumber = item.HTSD_SDHH;
                            break;
                        case 181:
                            obj.GiaTriNumber = item.HTSD_DE_O;
                            break;
                        case 182:
                            obj.GiaTriNumber = item.HTSD_BO_TRONG;
                            break;
                        case 183:
                            obj.GiaTriNumber = item.HTSD_BI_LAN_CHIEM;
                            break;
                        case 205:
                            obj.GiaTriNumber = item.HTSD_KHAC;
                            break;
                    }
                    lstObjHienTrang.Add(obj);
                }

                var hientrangList = new HienTrangList()
                {
                    TaiSanId = bd.TAI_SAN_ID,
                    lstObjHienTrang = lstObjHienTrang
                };
                yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
                bdct.HTSD_JSON = yeuCau_json.HTSD_JSON;
            }
            else
            {
                if (bd.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    foreach (var ht in lstHienTrang)
                    {
                        var obj = new ObjHienTrang();
                        obj.HienTrangId = ht.ID;
                        obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                        obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                        obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                        switch (obj.HienTrangId)
                        {
                            case 82:
                                obj.GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC;
                                break;
                            case 83:
                                obj.GiaTriNumber = item.HTSD_HDSN_KHONG_KINH_DOANH;
                                break;
                            case 84:
                                obj.GiaTriNumber = item.HTSD_HDSN_KINH_DOANH;
                                break;
                            case 85:
                                obj.GiaTriNumber = item.HTSD_HDSN_CHO_THUE;
                                break;
                            case 86:
                                obj.GiaTriNumber = item.HTSD_HDSN_LDLK;
                                break;
                            case 87:
                                obj.GiaTriNumber = item.HTSD_SDHH;
                                break;
                            case 178:
                                obj.GiaTriNumber = item.HTSD_DE_O;
                                break;
                            case 179:
                                obj.GiaTriNumber = item.HTSD_BO_TRONG;
                                break;
                            case 180:
                                obj.GiaTriNumber = item.HTSD_BI_LAN_CHIEM;
                                break;
                            case 209:
                                obj.GiaTriNumber = item.HTSD_KHAC;
                                break;
                        }
                        lstObjHienTrang.Add(obj);
                    }

                    var hientrangList = new HienTrangList()
                    {
                        TaiSanId = bd.TAI_SAN_ID,
                        lstObjHienTrang = lstObjHienTrang
                    };
                    yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
                    bdct.HTSD_JSON = yeuCau_json.HTSD_JSON;
                }
                /*else
                {
                    bdct.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(bd.TAI_SAN_ID);
                }*/
            }

            /*var hientrangList = new HienTrangList()
            {
                TaiSanId = bd.TAI_SAN_ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeuCau_json.HTSD_JSON = hientrangList.toStringJson();
            bdct.HTSD_JSON = yeuCau_json.HTSD_JSON;*/
            _bienDongChiTietService.InsertToBienDongChiTiet(bdct);

            return bdct.ToModel<BienDongChiTietModel>();
        }
        public List<string> ValidateBdTangGiamNguyenGia(ImportBdTangGiamNguyenGiaModel model)
        { 
            List<string> ListError = new List<string>();
            string Error = $"Hàng {model.Row}:";
            int countError = 0;
            if (countError > 0)
            {
                ListError.Add(Error);
            }
            return ListError;
        }
        public virtual void InsertTaiSanNguonVonFromBienDong(ImportBdTangGiamNguyenGiaModel item, BienDongModel bd)
        {
            var nguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
            foreach (var nvID in nguonVonIds)
            {
                var tsnv = new TaiSanNguonVon();
                tsnv.TAI_SAN_ID = bd.TAI_SAN_ID;
                tsnv.NGUON_VON_ID = nvID;
                switch (nvID)
                {
                    case 1:
                        if (bd.LOAI_BIEN_DONG_ID == (decimal?)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI)
                        {
                            tsnv.GIA_TRI = -Math.Abs(item.NV_NGAN_SACH);
                        }
                        else
                        {
                            tsnv.GIA_TRI = item.NV_NGAN_SACH;
                        }
                        break;
                    case 3:
                        if (bd.LOAI_BIEN_DONG_ID == (decimal?)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI)
                        {
                            tsnv.GIA_TRI = -Math.Abs(item.NV_KHAC);
                        }
                        else
                        {
                            tsnv.GIA_TRI = item.NV_KHAC;
                        }
                        break;
                }
                tsnv.BIEN_DONG_ID = bd.ID;
                _taiSanNguonVonService.InsertTaiSanNguonVon(tsnv);
            }
        }
    }
}