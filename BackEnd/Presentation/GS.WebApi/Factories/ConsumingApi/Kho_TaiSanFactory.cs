using DevExpress.CodeParser;
using DevExpress.XtraCharts.Native;
using GS.Core;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.TaiSans;
using GS.WebApi.Common;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.DanhMucApi;
using GS.WebApi.Models.ConsumingApi.KhaiThacTaiSan;
using GS.WebApi.Models.ConsumingApi.KhauHao_HaoMon;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using GS.WebApi.Models.TaiSan;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common.ConsumingApi
{
    public class Kho_TaiSanFactory : IKho_TaiSanFactory
    {
        private readonly IDbContext _dbContext;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly IDonViService _donViService;
        private readonly INhanXeService _nhanXeService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IDiaBanService _diaBanService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IGSAPIService _gSAPIService;
        private readonly ICommonFactory _commonFactory;
        private readonly IDonViBoPhanService _donViBoPhan;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IHoatDongService _hoatDongService;
        private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDuAnService _duAnService;
        private readonly IDongBoServices _dongBoServices;
        public Kho_TaiSanFactory(
            ILoaiTaiSanService loaiTaiSanService,
            IMucDichSuDungService mucDichSuDungService,
            ITaiSanService taiSanService,
            ITaiSanDatService taiSanDatService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVktService taiSanVktService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanClnService taiSanClnService,
            ILyDoBienDongService lyDoBienDongService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            IHienTrangService hienTrangService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ITaiSanNguonVonService taiSanNguonVonService,
            IDonViService donViService,
            INhanXeService nhanXeService,
            IQuocGiaService quocGiaService,
            IDiaBanService diaBanService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IGSAPIService gSAPIService,
            ICommonFactory commonFactory,
            IDonViBoPhanService donViBoPhan,
            IHaoMonTaiSanService haoMonTaiSanService,
            IHoatDongService hoatDongService,
            ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
            IChucDanhService chucDanhService,
            IDuAnService duAnService,
            IDbContext dbContext,
            IDongBoServices dongBoServices
            )
        {
            this._loaiTaiSanService = loaiTaiSanService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._taiSanService = taiSanService;
            this._taiSanDatService = taiSanDatService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanClnService = taiSanClnService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._hienTrangService = hienTrangService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._donViService = donViService;
            this._nhanXeService = nhanXeService;
            this._quocGiaService = quocGiaService;
            this._diaBanService = diaBanService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._gSAPIService = gSAPIService;
            this._commonFactory = commonFactory;
            this._donViBoPhan = donViBoPhan;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._hoatDongService = hoatDongService;
            this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
            this._chucDanhService = chucDanhService;
            this._duAnService = duAnService;
            this._dbContext = dbContext;
            this._dongBoServices = dongBoServices;

        }
        #region Dữ liệu mới
        /// <summary>
        /// Lấy danh sách biến động của tài sản mới
        /// </summary>
        /// <param name="DonViId"></param>
        /// <param name="LoaiBienDong"></param>
        /// <param name="LoaiHinhTaiSanId"></param>
        /// <param name="ngayCapNhatTu"></param>
        /// <param name="ngayCapNhatDen"></param>
        /// <returns></returns>
        public List<BienDong> GetListBienDongDongBoDLMoi(int DonViId = 0, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0, DateTime? ngayCapNhatTu = null, DateTime? ngayCapNhatDen = null)
        {
            List<BienDong> lstBienDong = new List<BienDong>();
            List<TaiSanNhatKy> dbTaiSanNhatKy = new List<TaiSanNhatKy>();
            if (LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
                LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            {
                dbTaiSanNhatKy = _taiSanNhatKyService.GetAllTaiSanNhatKys(trangThai: (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO, ngayCapNhatTu: ngayCapNhatTu, ngayCapNhatDen: ngayCapNhatDen).Where(m => m.BD_IDS != null).ToList();
            }
            else
            {
                dbTaiSanNhatKy = _taiSanNhatKyService.GetAllTaiSanNhatKys(trangThais: new List<decimal>() { (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO, (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI, (decimal)enumTrangThaiTaiSanNhatKy.DONG_BO_THAT_BAI }, ngayCapNhatTu: ngayCapNhatTu, ngayCapNhatDen: ngayCapNhatDen).Where(m => m.BD_IDS != null).ToList();
            }
            if (dbTaiSanNhatKy != null && dbTaiSanNhatKy.Count > 0)
            {
                List<string> lstBD_IDS = dbTaiSanNhatKy.Where(c => !string.IsNullOrEmpty(c.BD_IDS) && string.IsNullOrEmpty(c.BD_IDS_DANG_DB)).Select(c => c.BD_IDS).ToList();
                decimal[] bdids = string.Join(',', lstBD_IDS).Split(',').Select(c => Convert.ToDecimal(c)).ToArray();
                List<BienDong> bds = _bienDongService.GetBienDongByIds(bdids).ToList();
                lstBienDong = bds.Where(c => bdids.Contains(c.ID)).ToList();
                if (lstBienDong != null && lstBienDong.Count > 0)
                {
                    IEnumerable<BienDong> q = new List<BienDong>();
                    if (DonViId > 0)
                        q = lstBienDong.Where(c => c.DON_VI_ID == DonViId);
                    if (LoaiBienDong > 0)
                        q = lstBienDong.Where(c => c.LOAI_BIEN_DONG_ID == LoaiBienDong);
                    lstBienDong = q.ToList();
                }
            }
            return lstBienDong;
        }
        #endregion
        /// <summary>
        /// Lấy thông tin tài sản cần đồng bộ
        /// </summary>
        /// <param name="DonViId"></param>
        /// <param name="LoaiBienDong"></param>
        /// <param name="LoaiHinhTaiSanId"></param>
        /// <returns></returns>
        public async Task<Kho_DongBoTaiSan> GetListTaiSanDongBo(int DonViId = 0, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0, decimal DonViChaId = 0, decimal TaiSanId = 0, List<decimal> ListTaiSanId = null, List<TaiSan> _ListTaiSan = null, decimal BienDongId = 0)
        {
            List<TaiSanDBModel> ListTaiSanDongBo = PrepareTaiSanDongBo(DonViId, LoaiBienDong, LoaiHinhTaiSanId, taiSanId: TaiSanId, ListTaiSanId: ListTaiSanId, _ListTaiSan: _ListTaiSan, BienDongId: BienDongId);
            Kho_DongBoTaiSan Kho_DongBoTaiSan = new Kho_DongBoTaiSan();
            foreach (var item in ListTaiSanDongBo)
            {
                try
                {
                    var duLieu = await PrepareDuLieuDongBo(item, LoaiBienDong, LoaiHinhTaiSanId);
                    Kho_DongBoTaiSan.data.AddRange(duLieu);
                    Kho_DongBoTaiSan.CapNhatIDBienDongs.Add(new CapNhatIDBienDong()
                    {
                        TaiSanId = item.ID,
                        ListBienDongId = item.LST_BIEN_DONG.Select(m => Convert.ToDecimal(m.ID)).ToList()
                    });
                }
                catch (Exception ex)
                {

                    continue;
                }
            }
            return Kho_DongBoTaiSan;
        }
        /// <summary>
        /// Lấy dữ liệu đồng bộ biến động theo tài sản id
        /// </summary>
        /// <param name="taiSanId"></param>
        /// <param name="loaiBienDong"></param>
        /// <returns></returns>
        public Kho_DongBoTaiSan GetTaiSanDongBoByTaiSanId(decimal taiSanId = 0, decimal loaiBienDong = 0, decimal loaiHinhTaiSanId = 0)
        {
            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            List<TaiSanDBModel> lstTaiSanDongBo = PrepareTaiSanDongBo(taiSanId: taiSanId, LoaiBienDong: loaiBienDong, LoaiHinhTaiSanId: loaiHinhTaiSanId);
            if (lstTaiSanDongBo != null && lstTaiSanDongBo.Count > 0)
            {
                TaiSanDBModel tsDongBo = lstTaiSanDongBo.FirstOrDefault();
                // duLieuDongBo = PrepareDuLieuDongBo(model: tsDongBo, LoaiBienDong: loaiBienDong, LoaiHinhTaiSanId: tsDongBo.LOAI_HINH_TAI_SAN_ID.Value);
                //if (duLieuDongBo != null)
                //    duLieuDongBo.lstIdBdDongBo = tsDongBo.LST_BIEN_DONG.Select(c => Convert.ToDecimal(c.ID)).ToList();
            }
            return duLieuDongBo;
        }
        /// <summary>
        /// chuẩn bị các dữ liệu tài sản và biến động theo cấu trúc của kho
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<Kho_TaiSan_BienDong>> PrepareDuLieuDongBo(TaiSanDBModel model, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0)
        {
            List<Kho_TaiSan_BienDong> duLieu = new List<Kho_TaiSan_BienDong>();
            #region Thông tin biến động
            foreach (var item in model.LST_BIEN_DONG)
            {
                Kho_TaiSan_BienDong kho_TaiSan_BienDong = await PrepareThongTinBienDong(item, model, LoaiBienDong, LoaiHinhTaiSanId);
                //duLieu.assetMutations = kho_TaiSan_BienDong;
                duLieu.Add(kho_TaiSan_BienDong);
            }
            #endregion
            return duLieu;
        }
        #region Dùng chung
        /// <summary>
        /// lấy dữ liệu đổ sang TaiSanDBModel
        /// </summary>
        /// <param name="DonViId"></param>
        /// <param name="LoaiBienDong"></param>
        /// <param name="LoaiHinhTaiSanId"></param>
        /// <returns></returns>
        public List<TaiSanDBModel> PrepareTaiSanDongBo(int DonViId = 0, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0, DateTime? ngayCapNhatTu = null, DateTime? ngayCapNhatDen = null, decimal taiSanId = 0, List<decimal> ListTaiSanId = null, List<TaiSan> _ListTaiSan = null, Decimal BienDongId = 0)
        {
            //if (LoaiHinhTaiSanId == 0)
            //    return null;
            List<decimal> taiSanChuaDBIds = new List<decimal>();
            List<BienDong> bienDongs = new List<BienDong>();
            decimal[] arrIdTaiSan = bienDongs.Select(c => c.TAI_SAN_ID).Distinct().ToArray();
            List<TaiSan> ListTaiSan = new List<TaiSan>();
            if (taiSanId > 0)
            {
                TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(taiSanId);
                if (BienDongId == 0)
                {
                    if ((tsnk == null) || ((tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DA_DONG_BO || tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.CHO_DONG_BO) && (string.IsNullOrEmpty(tsnk.BD_IDS))) || (!string.IsNullOrEmpty(tsnk.BD_IDS_DANG_DB)))
                        return new List<TaiSanDBModel>();
                    bienDongs = _bienDongService.GetBienDongByIds(tsnk.BD_IDS.Split(',').Select(c => Convert.ToDecimal(c)).ToArray()).ToList();
                    bienDongs = bienDongs.Where(x => x.LOAI_BIEN_DONG_ID == LoaiBienDong).ToList();
                }
                else
                {
                    TaiSan ts = _taiSanService.GetTaiSanById(taiSanId);
                    if (ts == null)
                        return null;
                    ListTaiSan.Add(ts);
                    bienDongs.Add(_bienDongService.GetBienDongById(BienDongId));
                }
            }
            if (taiSanId == 0 && ListTaiSanId == null && _ListTaiSan == null)
            {
                bienDongs = GetListBienDongDongBoDLMoi(DonViId, LoaiBienDong, LoaiHinhTaiSanId);
                arrIdTaiSan = bienDongs.Select(c => c.TAI_SAN_ID).Distinct().ToArray();
                ListTaiSan = arrIdTaiSan.Count() > 0 ? _taiSanService.GetTaiSanByIds(arrIdTaiSan).ToList() : new List<TaiSan>();
            }
            if (_ListTaiSan != null && _ListTaiSan.Count > 0)
            {
                ListTaiSan = _ListTaiSan;
                //foreach (var item in ListTaiSan)
                //{
                //    var lstBienDOng = _bienDongService.GetBienDongsByTaiSanId(item.ID);
                //    bienDongs.AddRange(lstBienDOng);
                //}
            }
            else if (ListTaiSanId != null && ListTaiSanId.Count > 0)
            {
                foreach (var item in ListTaiSanId)
                {
                    var lstBienDOng = _bienDongService.GetBienDongsByTaiSanId(item);
                    bienDongs.AddRange(lstBienDOng);
                }
                ListTaiSan = _taiSanService.GetTaiSanByIds(ListTaiSanId.ToArray()).ToList();
            }
            List<TaiSanDBModel> ListTaiSanDongBo = new List<TaiSanDBModel>();
            if (LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                ListTaiSan = ListTaiSan.Where(c => c.MA_DB != null && c.MA_DB != c.MA_QLDKTS40).ToList();
            foreach (var item in ListTaiSan)
            {
                try
                {
                    TaiSanDBModel model = new TaiSanDBModel();
                    #region Thông tin chung tài sản
                    model.ID = (long)item.ID;
                    model.MA = item.MA;
                    model.TEN = item.TEN;
                    //get tài sản nhật ký
                    TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanId(item.ID);
                    if (taiSanNhatKy.TRANG_THAI_ID != (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO)
                    {
                        model.DB_MA = item.MA_DB;
                    }
                    if ((LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY) && !string.IsNullOrEmpty(model.DB_MA))// nếu đã có mã đồng bộ và là biến động tăng mới thì bỏ qua
                    {
                        continue;
                    }
                    LoaiTaiSan loaiTaiSan = item.loaitaisan;/* _loaiTaiSanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID.Value)*/
                    model.DB_LOAI_TAI_SAN_ID = (int?)loaiTaiSan.DB_ID;
                    model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                    model.MA_DON_VI = item.donvi.MA;
                    model.DON_VI_ID = item.donvi.ID;
                    model.DB_DON_VI_ID = item.donvi.DB_ID;
                    model.DU_AN_ID = item.DU_AN_ID;
                    if (model.DU_AN_ID.HasValue)
                    {
                        DuAn da = _duAnService.GetDuAnById(model.DU_AN_ID.Value);
                        if (da != null)
                            model.DB_DU_AN_ID = da.DB_ID;
                    }
                    model.TEN_DON_VI = item.donvi.TEN;
                    if (item.donvibophan != null)
                    {
                        model.DON_VI_BO_PHAN_ID = item.donvibophan.ID;
                        model.DB_DON_VI_BO_PHAN_ID = item.donvibophan.DB_ID;
                    }
                    model.CHUNG_TU_NGAY = item.CHUNG_TU_NGAY != null ? CommonHelper.toDateKhoString(item.CHUNG_TU_NGAY) : null;
                    model.CHUNG_TU_SO = item.CHUNG_TU_SO;
                    model.NGUYEN_GIA_BAN_DAU = item.NGUYEN_GIA_BAN_DAU;
                    model.GIA_MUA_TIEP_NHAN = item.GIA_MUA_TIEP_NHAN;
                    model.NGAY_SU_DUNG = CommonHelper.toDateKhoString(item.NGAY_SU_DUNG);
                    model.NUOC_SAN_XUAT_ID = item.NUOC_SAN_XUAT_ID;
                    model.NGAY_NHAP = CommonHelper.toDateKhoString(item.NGAY_NHAP);
                    model.MIEN_THUE_SO_TIEN = item.MIEN_THUE_SO_TIEN;
                    model.GIA_HOA_DON = item.GIA_HOA_DON;
                    model.NAM_SAN_XUAT = item.NAM_SAN_XUAT.ToString();
                    if (item.DON_VI_BO_PHAN_ID > 0)
                    {
                        model.DB_DON_VI_BO_PHAN_ID = item.donvibophan.DB_ID;
                    }
                    if (item.HM_TY_LE > 0)
                    {
                        model.HM_TY_LE = item.HM_TY_LE;
                    }
                    else
                    {
                        model.HM_TY_LE = loaiTaiSan.HM_TY_LE;
                    }
                    #endregion
                    #region Chi tiết tài sản
                    switch (item.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                TaiSanDat taiSanDat = _taiSanDatService.GetTaiSanDatByTaiSanId(item.ID);
                                model.TS_DAT = new TaiSanDatDBModel();
                                if (taiSanDat.DIA_BAN_ID > 0)
                                {
                                    model.TS_DAT.DB_DIA_BAN_ID = (long?)taiSanDat.diaban.DB_ID;
                                }
                                model.TS_DAT.DIA_BAN_MA = taiSanDat.diaban != null ? taiSanDat.diaban.MA : null;
                                model.TS_DAT.DIEN_TICH = taiSanDat.DIEN_TICH;
                                model.TS_DAT.DIA_CHI = model.TEN;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                TaiSanNha taiSanNha = _taiSanNhaService.GetTaiSanNhaByTaiSanId(item.ID);
                                model.TS_NHA = new TaiSanNhaDBModel();
                                model.TS_NHA.NAM_XAY_DUNG = taiSanNha.NAM_XAY_DUNG;
                                model.TS_NHA.DIEN_TICH_XAY_DUNG = taiSanNha.DIEN_TICH_XAY_DUNG.GetValueOrDefault(0);
                                model.TS_NHA.DIEN_TICH_SAN_XAY_DUNG = taiSanNha.DIEN_TICH_SAN_XAY_DUNG;
                                model.TS_NHA.NGAY_SU_DUNG = taiSanNha.NGAY_SU_DUNG.toDateVNString();
                                if (LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
                                    LoaiBienDong == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                                {
                                    if (taiSanNha.TAI_SAN_DAT_ID.GetValueOrDefault(0) > 0)
                                    {
                                        TaiSan ts = _taiSanService.GetTaiSanById(taiSanNha.TAI_SAN_DAT_ID.Value);
                                        if (String.IsNullOrEmpty(ts.MA_DB) || ts.MA_DB == ts.MA_QLDKTS40)
                                        {
                                            TaiSan tsDat = _taiSanService.GetTaiSanById(ts.ID);
                                            model.TS_NHA.DIA_CHI = tsDat.TEN;
                                        }
                                    }
                                    else
                                        model.TS_NHA.DIA_CHI = taiSanNha.DIA_CHI;
                                }
                                else
                                {
                                    model.TS_NHA.DIA_CHI = taiSanNha.DIA_CHI;
                                }
                                model.TS_NHA.TAI_SAN_DAT_MA = taiSanNha.TAI_SAN_DAT_ID > 0 ? _taiSanDatService.GetTaiSanDatById(taiSanNha.TAI_SAN_DAT_ID.Value).taisan.MA_DB : null;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                            {
                                model.TS_OTO = new TaiSanOtoDBModel();
                                TaiSanOto taiSanOto = _taiSanOtoService.GetTaiSanOtoById(item.ID);
                                model.TS_OTO.BIEN_KIEM_SOAT = taiSanOto.BIEN_KIEM_SOAT;
                                model.TS_OTO.SO_CHO_NGOI = taiSanOto.SO_CHO_NGOI;
                                model.TS_OTO.TAI_TRONG = taiSanOto.TAI_TRONG;
                                model.TS_OTO.SO_KHUNG = taiSanOto.SO_KHUNG;
                                model.TS_OTO.SO_MAY = taiSanOto.SO_MAY;
                                model.TS_OTO.NHAN_XE_MA = taiSanOto.nhanxe != null ? taiSanOto.nhanxe.TEN : null;
                                model.TS_OTO.NHAN_XE_ID = taiSanOto.nhanxe != null ? (int)taiSanOto.nhanxe.ID : default(int?);
                                model.TS_OTO.DUNG_TICH = taiSanOto.DUNG_TICH;
                                model.TS_OTO.CONG_XUAT = taiSanOto.CONG_XUAT;
                                model.TS_OTO.CHUC_DANH_TEN = taiSanOto.chucDanh != null ? taiSanOto.chucDanh.TEN_CHUC_DANH : null;
                                model.TS_OTO.CHUC_DANH_ID = taiSanOto.chucDanh != null ? (int)taiSanOto.chucDanh.ID : default(int?);
                                model.TS_OTO.CO_QUAN_CAP_DANG_KY = taiSanOto.CO_QUAN_CAP_DANG_KY;
                                model.TS_OTO.GCN_DANG_KY = taiSanOto.GCN_DANG_KY;
                                model.TS_OTO.SO_CAU = taiSanOto.SO_CAU_XE;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                            {
                                model.TS_PTK = new TaiSanOtoDBModel();
                                TaiSanOto taiSanPtk = _taiSanOtoService.GetTaiSanOtoById(item.ID);
                                model.TS_PTK.BIEN_KIEM_SOAT = taiSanPtk.BIEN_KIEM_SOAT;
                                model.TS_PTK.SO_CHO_NGOI = taiSanPtk.SO_CHO_NGOI;
                                model.TS_PTK.TAI_TRONG = taiSanPtk.TAI_TRONG;
                                model.TS_PTK.SO_KHUNG = taiSanPtk.SO_KHUNG;
                                model.TS_PTK.SO_MAY = taiSanPtk.SO_MAY;
                                model.TS_PTK.NHAN_XE_MA = taiSanPtk.nhanxe != null ? taiSanPtk.nhanxe.TEN : null;
                                model.TS_PTK.NHAN_XE_ID = taiSanPtk.nhanxe != null ? (int)taiSanPtk.nhanxe.ID : default(int?);
                                model.TS_PTK.DUNG_TICH = taiSanPtk.DUNG_TICH;
                                model.TS_PTK.CONG_XUAT = taiSanPtk.CONG_XUAT;
                                model.TS_PTK.CHUC_DANH_MA = taiSanPtk.chucDanh != null ? taiSanPtk.chucDanh.TEN_CHUC_DANH : null;
                                model.TS_PTK.CHUC_DANH_ID = taiSanPtk.chucDanh != null ? (int)taiSanPtk.chucDanh.ID : default(int?);

                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                            {
                                model.TS_MAY_MOC = new TaiSanMayMocDBModel();
                                TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(item.ID);
                                model.TS_MAY_MOC.THONG_SO_KY_THUAT = taiSanMayMoc.THONG_SO_KY_THUAT;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                            {
                                model.TS_DAC_THU = new TaiSanMayMocDBModel();
                                TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(item.ID);
                                model.TS_DAC_THU.THONG_SO_KY_THUAT = taiSanMayMoc.THONG_SO_KY_THUAT;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                            {
                                model.TS_HUU_HINH_KHAC = new TaiSanMayMocDBModel();
                                TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(item.ID);
                                model.TS_HUU_HINH_KHAC.THONG_SO_KY_THUAT = taiSanMayMoc.THONG_SO_KY_THUAT;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                            {
                                model.TS_VKT = new TaiSanVktDBModel();
                                TaiSanVkt taiSanVkt = _taiSanVktService.GetTaiSanVktByTaiSanId(item.ID);
                                model.TS_VKT.CHIEU_DAI = taiSanVkt.CHIEU_DAI;
                                model.TS_VKT.THE_TICH = taiSanVkt.THE_TICH;
                                model.TS_VKT.DIEN_TICH = taiSanVkt.DIEN_TICH;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                            {
                                model.TS_CLN = new TaiSanClnDBModel();
                                TaiSanCln taiSanCln = _taiSanClnService.GetTaiSanClnByTaiSanId(item.ID);
                                model.TS_CLN.NAM_SINH = taiSanCln.NAM_SINH;
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH:
                            {
                                model.TS_VO_HINH = new TaiSanVoHinhDBModel();
                                TaiSanVoHinh taiSanVoHinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(item.ID);
                                model.TS_VO_HINH.THONG_SO_KY_THUAT = taiSanVoHinh.THONG_SO_KY_THUAT;
                                break;
                            }
                        default:
                            break;
                    }
                    #endregion

                    #region biến động
                    List<BienDong> ListBienDong = new List<BienDong>();
                    ListBienDong = _bienDongService.GetBienDongsByTaiSanId(item.ID).ToList();
                    //ListBienDong = bienDongs.Where(c => c.TAI_SAN_ID == item.ID && c.LOAI_BIEN_DONG_ID == LoaiBienDong).ToList();
                    var BienDongTheoLoaiBienDong = ListBienDong.Where(m => m.LOAI_BIEN_DONG_ID == LoaiBienDong);
                    if (ListBienDong.Count == 0)
                    {
                        continue;
                    }
                    model.LST_BIEN_DONG = new List<BienDongDBModel>();
                    foreach (var bd in BienDongTheoLoaiBienDong)
                    {
                        BienDongDBModel bdModel = new BienDongDBModel();
                        bdModel.GUID = bd.GUID.ToString();
                        bdModel.ID = (long)bd.ID;
                        bdModel.NGAY_TAO = CommonHelper.toDateKhoString(bd.NGAY_TAO);
                        bdModel.CHUNG_TU_NGAY = CommonHelper.toDateKhoString(bd.CHUNG_TU_NGAY);
                        bdModel.CHUNG_TU_SO = bd.CHUNG_TU_SO;
                        bdModel.NGAY_SU_DUNG = CommonHelper.toDateKhoString(bd.NGAY_SU_DUNG);
                        if (bd.NGUYEN_GIA.HasValue)
                            bdModel.NGUYEN_GIA = Math.Abs(bd.NGUYEN_GIA.Value);
                        bdModel.NGAY_DUYET = CommonHelper.toDateKhoString(bd.NGAY_DUYET);
                        if (bd.nguoidung != null)
                            bdModel.NGUOI_DUYET = bd.nguoidung.TEN_DAY_DU;
                        bdModel.NGAY_BIEN_DONG = CommonHelper.toDateKhoString(bd.NGAY_BIEN_DONG);
                        if (bd.LY_DO_BIEN_DONG_ID > 0)
                        {
                            bdModel.DB_LY_DO_BIEN_DONG_ID = bd.lydobiendong.DB_ID;
                            bdModel.LY_DO_BIEN_DONG_MA = bd.lydobiendong.MA;
                            bdModel.LY_DO_BIEN_DONG_ID = bd.LY_DO_BIEN_DONG_ID;
                        }
                        bdModel.LOAI_BIEN_DONG_ID = bd.LOAI_BIEN_DONG_ID;
                        bdModel.QUYET_DINH_NGAY = CommonHelper.toDateKhoString(bd.QUYET_DINH_NGAY);
                        bdModel.QUYET_DINH_SO = bd.QUYET_DINH_SO;
                        bdModel.GHI_CHU = bd.GHI_CHU;
                        BienDongChiTiet bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bd.ID);
                        bdModel.DON_VI_NHAN_DIEU_CHUYEN_MA = bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID > 0 ? bienDongChiTiet.donvinhandieuchuyen.MA : null;
                        bdModel.HM_GIA_TRI_CON_LAI = bienDongChiTiet.HM_GIA_TRI_CON_LAI;
                        bdModel.HM_LUY_KE = bienDongChiTiet.HM_LUY_KE;
                        bdModel.HM_SO_NAM_CON_LAI = bienDongChiTiet.HM_SO_NAM_CON_LAI;
                        bdModel.HM_TY_LE_HAO_MON = bienDongChiTiet.HM_TY_LE_HAO_MON;
                        bdModel.KH_CON_LAI = bienDongChiTiet.KH_CON_LAI;
                        bdModel.KH_GIA_TINH_KHAU_HAO = bienDongChiTiet.KH_GIA_TINH_KHAU_HAO;
                        bdModel.KH_GIA_TRI_TRICH_THANG = bienDongChiTiet.KH_GIA_TRI_TRICH_THANG;
                        bdModel.KH_LUY_KE = bienDongChiTiet.KH_LUY_KE;
                        //  bdModel.KH_NGAY_BAT_DAU = bienDongChiTiet.KH_NGAY_BAT_DAU.toDateVNString();
                        bdModel.KH_THANG_CON_LAI = bienDongChiTiet.KH_THANG_CON_LAI;
                        var loaiTaiSanKhauHao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(model.LOAI_TAI_SAN_ID, model.DON_VI_ID);
                        if (loaiTaiSanKhauHao != null)
                        {
                            bdModel.KH_TY_LE = loaiTaiSanKhauHao.TY_LE_KHAU_HAO;
                        }
                        bdModel.MUC_DICH_SU_DUNG_MA = bienDongChiTiet.mucdichsudung != null ? bienDongChiTiet.mucdichsudung.MA : null;
                        if (bienDongChiTiet.DIA_BAN_ID > 0)
                        {
                            var DiaBan = _diaBanService.GetDiaBanById(bienDongChiTiet.DIA_BAN_ID.Value);
                            bdModel.DB_DIA_BAN_ID = DiaBan.DB_ID;
                            bdModel.DB_QUOC_GIA_ID = DiaBan.Quocgia.DB_ID;
                        }
                        else
                        {
                            var DiaBan = _diaBanService.GetDiaBanById(bd.donvi.DIA_BAN_ID.Value);
                            bdModel.DB_DIA_BAN_ID = DiaBan.DB_ID;
                            bdModel.DB_QUOC_GIA_ID = DiaBan.Quocgia.DB_ID;
                        }
                        if (bdModel.DB_QUOC_GIA_ID.GetValueOrDefault(0) == 0)
                        {
                            QuocGia quocGia = _quocGiaService.GetQuocGiaByMa("VN");
                            if (quocGia != null)
                                bdModel.DB_QUOC_GIA_ID = quocGia.DB_ID;
                        }
                        if (bienDongChiTiet.HINH_THUC_MUA_SAM_ID > 0)
                        {
                            bdModel.HINH_THUC_MUA_SAM_TEN = bienDongChiTiet.hinhthucmuasam.TEN;
                            bdModel.HINH_THUC_MUA_SAM_MA = bienDongChiTiet.hinhthucmuasam.MA;
                            bdModel.HINH_THUC_MUA_SAM_DB_ID = bienDongChiTiet.hinhthucmuasam.DB_ID;
                        }
                        #region Đất
                        if (item.LOAI_HINH_TAI_SAN_ID == (Decimal)enumLOAI_HINH_TAI_SAN.DAT)
                        {
                            if (bienDongChiTiet.DAT_TONG_DIEN_TICH.HasValue)
                            {
                                bdModel.DAT_TONG_DIEN_TICH = Math.Abs(bienDongChiTiet.DAT_TONG_DIEN_TICH.Value);
                            }
                            bdModel.DIA_CHI = bd.TAI_SAN_TEN ?? item.TEN;
                        }
                        // Hồ sơ giấy tờ                 
                        #endregion
                        #region Nhà
                        if (item.LOAI_HINH_TAI_SAN_ID == (Decimal)enumLOAI_HINH_TAI_SAN.NHA)
                        {
                            bdModel.NHA_DIEN_TICH_XD = bienDongChiTiet.NHA_DIEN_TICH_XD;
                            if (bienDongChiTiet.NHA_NAM_XAY_DUNG.HasValue)
                                bdModel.NHA_NAM_XAY_DUNG = Math.Abs(bienDongChiTiet.NHA_NAM_XAY_DUNG.Value);
                            bdModel.NHA_SO_TANG = bienDongChiTiet.NHA_SO_TANG;
                            if (bienDongChiTiet.NHA_TONG_DIEN_TICH_XD.HasValue)
                                bdModel.NHA_TONG_DIEN_TICH_XD = Math.Abs(bienDongChiTiet.NHA_TONG_DIEN_TICH_XD.Value);
                            model.TS_NHA.NHA_SO_TANG = bdModel.NHA_SO_TANG;
                        }
                        #endregion
                        #region vật kiến trúc
                        bdModel.VKT_CHIEU_DAI = bienDongChiTiet.VKT_CHIEU_DAI;
                        bdModel.VKT_DIEN_TICH = bienDongChiTiet.VKT_DIEN_TICH;
                        bdModel.VKT_THE_TICH = bienDongChiTiet.VKT_THE_TICH;
                        #endregion
                        #region OTo
                        bdModel.OTO_BIEN_KIEM_SOAT = bienDongChiTiet.OTO_BIEN_KIEM_SOAT;
                        bdModel.OTO_CHUC_DANH_ID = bienDongChiTiet.OTO_CHUC_DANH_ID;
                        if (bdModel.OTO_CHUC_DANH_ID.HasValue)
                        {
                            ChucDanh otoChucDanh = _chucDanhService.GetChucDanhById(bienDongChiTiet.OTO_CHUC_DANH_ID.Value);
                            if (otoChucDanh != null)
                            {
                                bdModel.OTO_CHUC_DANH_TEN = otoChucDanh.TEN_CHUC_DANH;
                                bdModel.OTO_CHUC_DANH_DB_ID = otoChucDanh.DB_ID;
                            }

                        }

                        bdModel.OTO_CONG_XUAT = bienDongChiTiet.OTO_CONG_XUAT;
                        bdModel.OTO_NHAN_XE_MA = bienDongChiTiet.OTO_NHAN_XE_ID.ToString();
                        if (bienDongChiTiet.OTO_NHAN_XE_ID.HasValue)
                        {
                            NhanXe otoNhanXe = _nhanXeService.GetNhanXeById(bienDongChiTiet.OTO_NHAN_XE_ID.Value);
                            if (otoNhanXe != null)
                            {
                                bdModel.OTO_NHAN_XE_TEN = otoNhanXe.TEN;
                                bdModel.OTO_NHAN_XE_DB_ID = otoNhanXe.DB_ID;
                            }
                        }
                        bdModel.OTO_SO_CHO_NGOI = bienDongChiTiet.OTO_SO_CHO_NGOI;
                        bdModel.OTO_SO_KHUNG = bienDongChiTiet.OTO_SO_KHUNG;
                        bdModel.OTO_SO_MAY = bienDongChiTiet.OTO_SO_MAY;
                        bdModel.OTO_TAI_TRONG = bienDongChiTiet.OTO_TAI_TRONG;
                        bdModel.OTO_XI_LANH = bienDongChiTiet.OTO_XI_LANH;
                        #endregion
                        #region Máy móc
                        bdModel.THONG_SO_KY_THUAT = bienDongChiTiet.THONG_SO_KY_THUAT;
                        #endregion
                        #region Cây lâu năm
                        bdModel.CLN_SO_NAM = bienDongChiTiet.CLN_SO_NAM;
                        #endregion
                        #region Vô hình
                        bdModel.THONG_SO_KY_THUAT = bienDongChiTiet.THONG_SO_KY_THUAT;
                        #endregion
                        #region Nguồn vốn
                        var ListTaiSanNguonVon = _taiSanNguonVonService.GetTaiSanNguonVonByBienDongId(bd.ID);
                        foreach (var nv in ListTaiSanNguonVon)
                        {
                            if (nv.NGUON_VON_ID == (decimal)enumNguonVon.NguonHoatDongSuNghiep)
                            {
                                bdModel.NV_HDSN = Math.Abs(nv.GIA_TRI);
                            }
                            if (nv.NGUON_VON_ID == (decimal)enumNguonVon.NguonKhac)
                            {
                                bdModel.NV_NGUON_KHAC = Math.Abs(nv.GIA_TRI);
                            }
                            if (nv.NGUON_VON_ID == (decimal)enumNguonVon.NguonNganSach)
                            {
                                bdModel.NV_NGAN_SACH = Math.Abs(nv.GIA_TRI);
                            }
                            if (nv.NGUON_VON_ID == (decimal)enumNguonVon.NguonVienTro)
                            {
                                bdModel.NV_VIEN_TRO = Math.Abs(nv.GIA_TRI);
                            }
                        }
                        #endregion
                        #region Lấy thông tin từ dataJson
                        if (bienDongChiTiet.DATA_JSON != null)
                        {
                            var yeuCauChiTiet = JsonConvert.DeserializeObject<YeuCauChiTiet>(bienDongChiTiet.DATA_JSON);
                            bdModel.DAT_GIA_TRI_QUYEN_SD_DAT = yeuCauChiTiet.DAT_GIA_TRI_QUYEN_SD_DAT;
                        }

                        //bdModel.HO_SO_GIAY_TO = PrepareThongTinHoSoGiayTo(bienDongChiTiet);

                        #endregion
                        // lấy thông tin trong dataJson
                        model.LST_BIEN_DONG.Add(bdModel);
                    }
                    var BienDongTangMoi = ListBienDong.Where(m => m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY).FirstOrDefault();
                    var chiTietTangMoi = _bienDongChiTietService.GetBienDongChiTietByBDId(BienDongTangMoi.ID);
                    model.HoSoGiayTo = PrepareThongTinHoSoGiayTo(chiTietTangMoi);
                    #endregion
                    ListTaiSanDongBo.Add(model);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return ListTaiSanDongBo;
        }
        HoSoGiayTo PrepareThongTinHoSoGiayTo(BienDongChiTiet bienDongChiTiet)
        {
            HoSoGiayTo hoSoGiayTo = new HoSoGiayTo();
            var yeuCauChiTiet = JsonConvert.DeserializeObject<YeuCauChiTiet>(bienDongChiTiet.DATA_JSON);
            hoSoGiayTo = new HoSoGiayTo();
            hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU = yeuCauChiTiet.HS_BIEN_BAN_NGHIEM_THU;
            hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_BIEN_BAN_NGHIEM_THU_NGAY);
            hoSoGiayTo.HS_CNQSD_SO = yeuCauChiTiet.HS_CNQSD_SO;
            hoSoGiayTo.HS_CNQSD_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_CNQSD_NGAY);
            hoSoGiayTo.HS_PHAP_LY_KHAC = yeuCauChiTiet.HS_PHAP_LY_KHAC;
            hoSoGiayTo.HS_PHAP_LY_KHAC_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_PHAP_LY_KHAC_NGAY);
            hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO = yeuCauChiTiet.HS_QUYET_DINH_BAN_GIAO;
            hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_QUYET_DINH_BAN_GIAO_NGAY);
            hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_SO = yeuCauChiTiet.HS_QUYET_DINH_CHO_THUE_SO;
            hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_QUYET_DINH_CHO_THUE_NGAY);
            hoSoGiayTo.HS_QUYET_DINH_GIAO_SO = yeuCauChiTiet.HS_QUYET_DINH_GIAO_SO;
            hoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_QUYET_DINH_GIAO_NGAY);
            hoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO = yeuCauChiTiet.HS_HOP_DONG_CHO_THUE_SO;
            hoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY = CommonHelper.toDateKhoString(yeuCauChiTiet.HS_HOP_DONG_CHO_THUE_NGAY);

            return hoSoGiayTo;
        }
        public async Task<Kho_TaiSan_BienDong> PrepareThongTinBienDong(BienDongDBModel item, TaiSanDBModel model, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0)
        {
            if (LoaiHinhTaiSanId == 0)
            {
                LoaiHinhTaiSanId = model.LOAI_HINH_TAI_SAN_ID.Value;
            }
            Kho_TaiSan_BienDong bienDong = new Kho_TaiSan_BienDong();
            bienDong.LOAI_HINH_TAI_SAN_ID = LoaiHinhTaiSanId;
            bienDong.assetCode = model.DB_MA;
            bienDong.syncSourceAssetId = model.MA;
            //bienDong.assetSyncSourceId = long.Parse(model.GUID.ToString());
            //bienDong.assetMutationTypeId = (int?)enumKho_Loai_Bien_Dong.TangMoi;
            if (!string.IsNullOrEmpty(item.NGAY_BIEN_DONG))
            {
                bienDong.mutationDate = item.NGAY_BIEN_DONG;
            }
            bienDong.mutationCauseId = (int)item.DB_LY_DO_BIEN_DONG_ID;
            bienDong.approvedDate = item.NGAY_DUYET;
            bienDong.approverName = item.NGUOI_DUYET;
            //Đơn vị bộ phận
            #region đơn vị bộ phận
            //if (!(model.DB_DON_VI_BO_PHAN_ID > 0) && model.DON_VI_BO_PHAN_ID > 0)
            //{
            //    DetaiModel detaiModel = new DetaiModel()
            //    {
            //        syncId = model.DON_VI_BO_PHAN_ID.ToString()
            //    };
            //    Kho_DonViSuDung kho_donViSuDung = await _gSAPIService.GetObjectGSApi<Kho_DonViSuDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan + CommonAction.ChiTiet, detaiModel, _commonFactory.GetTokenKhoCSDLQG());

            //    List<Kho_DonViSuDung> kho_DonViSuDungs = new List<Kho_DonViSuDung>();
            //    if (kho_donViSuDung == null)
            //    {
            //        DonViBoPhan donViBoPhan = _donViBoPhan.GetDonViBoPhanById(model.DON_VI_BO_PHAN_ID.Value);
            //        Kho_DonViSuDung kho_DonViSuDung = new Kho_DonViSuDung();
            //        kho_DonViSuDung.actionType = donViBoPhan.DB_ID > 0 ? 2 : 1;
            //        kho_DonViSuDung.name = donViBoPhan.TEN;
            //        kho_DonViSuDung.code = donViBoPhan.MA;
            //        kho_DonViSuDung.syncId = donViBoPhan.ID.ToString();
            //        kho_DonViSuDung.syncParentId = donViBoPhan.PARENT_ID > 0 ? donViBoPhan.PARENT_ID.ToString() : null;
            //        if (donViBoPhan.PARENT_ID > 0)
            //        {
            //            kho_DonViSuDung.parentId = (long)donViBoPhan.DonViBoPhanCha.DB_ID;
            //        }
            //        kho_DonViSuDung.phoneNumber = donViBoPhan.DIEN_THOAI;
            //        kho_DonViSuDung.fax = donViBoPhan.FAX;
            //        kho_DonViSuDung.unitId = donViBoPhan.DON_VI.DB_ID;
            //        kho_DonViSuDung.isActive = true;
            //        kho_DonViSuDung.address = donViBoPhan.DIA_CHI;
            //        //kho_DonViSuDungs.Add(kho_DonViSuDung);
            //        //var responseApi = await _gSAPIService.PostObjectGSApi<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan + CommonAction.DongBo, kho_DonViSuDungs, _commonFactory.GetTokenKhoCSDLQG());
            //        //if (responseApi.Status)
            //        //{
            //        //    InsertLogModel logModel = new InsertLogModel()
            //        //    {
            //        //        ResponseApi = responseApi,
            //        //        Data = kho_DonViSuDungs,
            //        //        LoaiDuLieu = "Danh mục đơn vị bộ phận"
            //        //    };
            //        //    _hoatDongService.InsertHoatDong(enumHoatDong.DbCho, "Đang chờ đồng bộ", logModel, "DonViBoPhan");
            //        //    kho_donViSuDung = await _gSAPIService.GetObjectGSApi<Kho_DonViSuDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.DonViBoPhan + CommonAction.ChiTiet, detaiModel, _commonFactory.GetTokenKhoCSDLQG());
            //        //    if (kho_donViSuDung != null)
            //        //    {
            //        //        donViBoPhan.DB_ID = kho_donViSuDung.id;
            //        //        bienDong.departmentId = (int?)kho_donViSuDung.id;
            //        //        _donViBoPhan.UpdateDonViBoPhan(donViBoPhan);
            //        //    }
            //        //}
            //    }
            //}
            //else
            //{
            //    bienDong.departmentId = (int?)model.DB_DON_VI_BO_PHAN_ID;
            //}
            #endregion
            bienDong.departmentId = (int?)model.DB_DON_VI_BO_PHAN_ID;
            //bienDong.amortizationRate = 0;
            decimal NguyenGiaTruocBienDong = _bienDongService.TinhNguyenGiaTaiSanOnlyBD(model.ID, item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"));
            GiaTriTaiSan giatriCu = new GiaTriTaiSan();
            if (LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            {
                giatriCu = _bienDongService.Tinh_GiaTriTaiSan(model.ID, item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"));

                bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
            }
            bienDong.originalValueOld = (long?)NguyenGiaTruocBienDong;
            bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
            switch (LoaiBienDong)
            {
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangMoi;
                        bienDong.originalValue = (long)(NguyenGiaTruocBienDong + (item.NGUYEN_GIA.GetValueOrDefault(0)));
                        bienDong.originalValueOld = 0;
                        bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangNguyenGia;
                        bienDong.originalValue = (long)(NguyenGiaTruocBienDong + (item.NGUYEN_GIA.GetValueOrDefault(0)));
                        bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.GiamNguyenGia;
                        bienDong.originalValue = (long)(NguyenGiaTruocBienDong - (item.NGUYEN_GIA.GetValueOrDefault(0)));
                        bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin;
                        bienDong.originalValue = (long)NguyenGiaTruocBienDong;
                        bienDong.changeReason = item.TEN_LY_DO;
                        bienDong.amortizationAccumulatedValue = 0;// (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.DieuChuyen;
                        DonVi donVi = _donViService.GetCacheDonViByMa(item.DON_VI_NHAN_DIEU_CHUYEN_MA);
                        bienDong.transferUnitCode = donVi.MA;
                        bienDong.transferUnitId = donVi.DB_ID;
                        bienDong.transferUnitName = donVi.TEN;
                        bienDong.originalValue = (long)(NguyenGiaTruocBienDong - (item.NGUYEN_GIA.GetValueOrDefault(0)));
                        bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
                        break;
                    }
                case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
                    {
                        bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.Giam;
                        if (item.LY_DO_BIEN_DONG_MA == enum_LY_DO_BIEN_DONG.DIEU_CHUYEN)
                        {
                            DonVi donVi = _donViService.GetCacheDonViByMa(item.DON_VI_NHAN_DIEU_CHUYEN_MA);
                            bienDong.transferUnitCode = donVi.MA;
                            bienDong.transferUnitId = donVi.DB_ID;
                            bienDong.transferUnitName = donVi.TEN;
                        }
                        bienDong.amortizationAccumulatedValue = (Double?)((Double)NguyenGiaTruocBienDong - bienDong.residualValueOld);
                        bienDong.originalValueIncreasement = -(long)NguyenGiaTruocBienDong;
                        bienDong.originalValue = 0;
                        break;
                    }
                default:
                    break;
            }
            
            bienDong.inputDate = model.NGAY_NHAP;
            bienDong.name = model.TEN;
            bienDong.syncSourceId = item.GUID.ToString();
            bienDong.projectId = (long?)model.DB_DU_AN_ID;
            bienDong.unitId = (int)model.DB_DON_VI_ID.Value;
            bienDong.unitName = model.TEN_DON_VI;
            bienDong.assetTypeId = model.DB_LOAI_TAI_SAN_ID.Value;
            bienDong.dateOfUse = item.NGAY_SU_DUNG == null ? model.NGAY_SU_DUNG : item.NGAY_SU_DUNG;
            bienDong.usagePurposeId = (long?)item.DB_MUC_DICH_SU_DUNG_ID;
            bienDong.syncUnitId = 1;//khoTaiSan.syncUnitId;
            bienDong.documentDecisionDate = item.QUYET_DINH_NGAY;
            bienDong.documentDecisionNumber = item.QUYET_DINH_SO;
            bienDong.amortizationRate = (float?)model.HM_TY_LE;

            bienDong.documentReceipt = item.CHUNG_TU_SO;
            bienDong.documentDateReceipt = item.CHUNG_TU_NGAY;
            bienDong.procurementForm = item.HINH_THUC_MUA_SAM_TEN;
            bienDong.notes = item.GHI_CHU;
            bienDong.countryOfOrigin = model.NUOC_SAN_XUAT_ID > 0 ? _quocGiaService.GetQuocGiaById(model.NUOC_SAN_XUAT_ID.Value).TEN : null;
            if (!string.IsNullOrEmpty(model.NAM_SAN_XUAT))
            {
                bienDong.plantYear = int.Parse(model.NAM_SAN_XUAT);
            }


            bienDong.residualValue = (long)item.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0);

            if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                if (LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    //var giatriCu = _bienDongService.Tinh_GiaTriTaiSan(model.ID, DateTime.Parse(item.NGAY_BIEN_DONG),false);
                    bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
                    // bienDong.landUseRightValue = (long?)giatriCu.DAT_GIA_TRI_QUYEN_SU_DUNG;
                    bienDong.landAreaOld = (double?)giatriCu.DAT_TONG_DIEN_TICH_CU;
                }
                //bienDong.landArea = (double)item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                bienDong.landUseRightValue = (long?)item.DAT_GIA_TRI_QUYEN_SD_DAT;
                bienDong.landAddress = item.TEN_TAI_SAN ?? model.TEN;
                if (bienDong.landAddress == null)
                {
                    DiaBan diaban = _diaBanService.GetDiaBanById(item.DIA_BAN_ID.GetValueOrDefault(0));
                    if (diaban != null)
                        bienDong.landAddress = diaban.TEN;
                }
                bienDong.landRegionId = (int?)item.DB_DIA_BAN_ID;
                bienDong.landCountryId = (long?)item.DB_QUOC_GIA_ID;
                if (bienDong.landCountryId.GetValueOrDefault(0) == 0)
                {
                    QuocGia quocGia = _quocGiaService.GetQuocGiaByMa("VN");
                    bienDong.landCountryId = quocGia != null ? quocGia.DB_ID : null;
                }
                if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
                {
                    bienDong.landArea = bienDong.landAreaOld - (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.Value);
                }
                else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
                {
                    bienDong.landArea = bienDong.landAreaOld.GetValueOrDefault(0) + (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.Value);
                }
                //else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                //{
                //    bienDong.landAreaIncreasement = 0;
                //}               
                bienDong.landAreaIncreasement = (double)item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                #region Hồ sơ giấy tờ
                bienDong.landDocumentLandUseRight = model.HoSoGiayTo.HS_CNQSD_SO;
                if (!string.IsNullOrEmpty(model.HoSoGiayTo.HS_CNQSD_NGAY))
                {
                    bienDong.landDocumentDateLandUseRightDate = model.HoSoGiayTo.HS_CNQSD_NGAY;
                }
                bienDong.landDocumentLandAllocate = model.HoSoGiayTo.HS_QUYET_DINH_GIAO_SO;
                if (!string.IsNullOrEmpty(model.HoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY))
                {
                    bienDong.landDocumentDateLandAllocate = model.HoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY;
                }
                //bienDong.landDocumentLandLeaseContract = model.HoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO;
                //if (!string.IsNullOrEmpty(model.HoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY))
                //{
                //    bienDong.landDocumentDateLandLeaseContract = model.HoSoGiayTo.HS_CHUYEN_NHUONG_SD_NGAY;
                //}
                bienDong.documentLeaseContract = model.HoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO;
                if (!string.IsNullOrEmpty(model.HoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY))
                    bienDong.documentDateLeaseContract = model.HoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY;

                //bienDong.documentDateOther = CommonHelper.toDateKhoString(item.CHUNG_TU_NGAY);
                bienDong.documentValue = (long?)item.GIA_MUA_TIEP_NHAN;

                //bienDong.delegatingUnitId = model.don
                bienDong.isTaxExemption = model.MIEN_THUE_SO_TIEN.GetValueOrDefault(0) > 0 ? true : false;
                bienDong.taxExamptionValue = (long?)model.MIEN_THUE_SO_TIEN;
                bienDong.originalValueDepreciation = (long?)item.KH_GIA_TRI_TRICH_THANG;
                bienDong.brandId = (int?)item.OTO_NHAN_XE_DB_ID;
                //bienDong.partnerId = 
                bienDong.procurementFormId = (int?)item.HINH_THUC_MUA_SAM_DB_ID;
                //bienDong.productLineId
                //bienDong.productLineName
                //bienDong.vehicleRegistrationDocumentDate = item.oto
                bienDong.vehicleUserTitleId = (int?)item.OTO_CHUC_DANH_DB_ID;
                //bienDong.assetHandlingFormSale
                //bienDong.assetHandlingFormValueObtained
                //bienDong.assetHandlingFormId = item.DB_MUC_DICH_SU_DUNG_ID;


                bienDong.landDocumentLandLease = model.HoSoGiayTo.HS_QUYET_DINH_CHO_THUE_SO;
                if (!string.IsNullOrEmpty(model.HoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY))
                {
                    bienDong.landDocumentDateLandLease = model.HoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY;
                }
                bienDong.documentOther = model.HoSoGiayTo.HS_PHAP_LY_KHAC;
                #endregion

            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
            {
                if (LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(giatriCu.NHA_TONG_DIEN_TICH_XD_CU.GetValueOrDefault(0));

                    bienDong.houseAreaFloorOld = (double)giatriCu.NHA_TONG_DIEN_TICH_XD_CU;
                }
                else
                {
                    bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                    bienDong.houseAreaFloorOld = 0;
                }
                bienDong.houseAddress = model.TS_NHA.DIA_CHI;
                bienDong.houseAreaBuilding = (double?)model.TS_NHA.DIEN_TICH_XAY_DUNG.GetValueOrDefault(0);

                bienDong.houseNumberOfFloor = (long)(item.NHA_SO_TANG ?? model.TS_NHA.NHA_SO_TANG);
                bienDong.houseBuiltYear = (int)(item.NHA_NAM_XAY_DUNG ?? model.TS_NHA.NAM_XAY_DUNG);
                bienDong.houseLandCode = model.TS_NHA.TAI_SAN_DAT_MA;

                if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
                {
                    bienDong.houseAreaFloor = bienDong.houseAreaFloorOld - (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.Value);
                }
                else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
                {
                    bienDong.houseAreaFloor = bienDong.houseAreaFloorOld + (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.Value);
                }
                else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
                {
                    bienDong.houseAreaFloor = 0;
                }
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.OTO || LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
            {
                TaiSanOtoDBModel tsPtvt = model.TS_OTO ?? model.TS_PTK;
                //bienDong.brandId = tsPtvt.NHAN_XE_ID;
                bienDong.vehicleRegistrationPlateNumber = item.OTO_BIEN_KIEM_SOAT ?? tsPtvt.BIEN_KIEM_SOAT;
                bienDong.enginePower = (double?)(item.OTO_CONG_XUAT ?? tsPtvt.CONG_XUAT);
                if (item.OTO_XI_LANH.HasValue)
                    bienDong.motorCylinder = item.OTO_XI_LANH.Value.ToString();
                else if (tsPtvt.DUNG_TICH.HasValue)
                {
                    bienDong.motorCylinder = tsPtvt.DUNG_TICH.Value.ToString();
                }
                bienDong.vehicleSize = (long?)(item.OTO_SO_CHO_NGOI ?? tsPtvt.SO_CHO_NGOI);
                if (item.OTO_TAI_TRONG > 0)
                {
                    bienDong.vehicleCapacity = (long)item.OTO_TAI_TRONG.Value;
                }
                else if (tsPtvt.TAI_TRONG > 0)
                {
                    bienDong.vehicleCapacity = (long)tsPtvt.TAI_TRONG.Value;
                }
                else
                {
                    bienDong.vehicleCapacity = 0;
                }
                bienDong.vehicleChassisNumber = tsPtvt.SO_MAY;
                bienDong.vehicleEngineNumber = tsPtvt.SO_KHUNG;
                bienDong.brandName = item.OTO_NHAN_XE_TEN;
                bienDong.vehicleUserTitle = tsPtvt.CHUC_DANH_TEN;

                bienDong.vehicleNumberOfWheelDrives = (int?)tsPtvt.SO_CAU;
                bienDong.vehicleRegistrationDocumentNumber = tsPtvt.GCN_DANG_KY;
                bienDong.vehicleRegistrationIssuedAuthority = tsPtvt.CO_QUAN_CAP_DANG_KY;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
            {
                if (LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                   LoaiBienDong != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    bienDong.volumeIncreasement = (double)item.VKT_THE_TICH.GetValueOrDefault(giatriCu.VKT_THE_TICH_CU.GetValueOrDefault(0));
                    bienDong.lengthIncreasement = (double)item.VKT_CHIEU_DAI.GetValueOrDefault(giatriCu.VKT_CHIEU_DAI_CU.GetValueOrDefault(0));
                    bienDong.volumeOld = (double)giatriCu.VKT_THE_TICH_CU;
                    bienDong.lengthOld = (double)giatriCu.VKT_CHIEU_DAI_CU;
                }
                else
                {
                    bienDong.volumeIncreasement = (double)item.VKT_THE_TICH.GetValueOrDefault(0);
                    bienDong.lengthIncreasement = (double)item.VKT_CHIEU_DAI.GetValueOrDefault(0);
                    bienDong.volumeOld = 0;
                    bienDong.lengthOld = 0;
                }
                bienDong.volume = (float?)model.TS_VKT.THE_TICH;
                bienDong.length = (float?)model.TS_VKT.CHIEU_DAI;
                bienDong.landArea = (double?)model.TS_VKT.DIEN_TICH;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI)
            {
                bienDong.specifications = model.TS_MAY_MOC.THONG_SO_KY_THUAT;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV)
            {
                bienDong.plantYear = (int?)model.TS_CLN.NAM_SINH;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
            {
                bienDong.specifications = model.TS_HUU_HINH_KHAC.THONG_SO_KY_THUAT;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                bienDong.specifications = model.TS_VO_HINH.THONG_SO_KY_THUAT;
            }
            else if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                bienDong.specifications = model.TS_DAC_THU.THONG_SO_KY_THUAT;
            }
            #region Hiện trạng sử dụng
            if (model.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA || model.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID);
                foreach (var ht in ListHienTrang)
                {
                    Kho_assetMutationAssetUsageStates khoHienTrang = new Kho_assetMutationAssetUsageStates();
                    if (ht.GIA_TRI_NUMBER > 0)
                    {
                        khoHienTrang.usageStateId = (int?)ht.HienTrang.DB_ID;
                        khoHienTrang.usageValue = (double)ht.GIA_TRI_NUMBER;
                        bienDong.assetMutationAssetUsageStates.Add(khoHienTrang);
                    }
                }
            }
            else
            {
                // bienDong.assetMutationAssetUsageStates = new List<Kho_assetMutationAssetUsageStates>();
                List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID);
                foreach (var ht in ListHienTrang)
                {
                    if (ht.GIA_TRI_CHECKBOX.HasValue && ht.GIA_TRI_CHECKBOX == true)
                    {
                        Kho_assetMutationAssetUsageStates khoHienTrang = new Kho_assetMutationAssetUsageStates();
                        khoHienTrang.usageStateId = (int?)ht.HienTrang.DB_ID;
                        khoHienTrang.usageValue = 1;
                        bienDong.assetMutationAssetUsageStates.Add(khoHienTrang);

                    }
                }
            }
            #endregion
            #region Nguồn vốn
            bienDong.originalValueSourceOtherIncreasement = (long)Math.Abs(item.NV_NGUON_KHAC.GetValueOrDefault(0));
            bienDong.originalValueSourceStateBudgetIncreasement = (long)Math.Abs(item.NV_NGAN_SACH.GetValueOrDefault(0));
            bienDong.originalValueSourceBusinessIncreasement = (long)Math.Abs(item.NV_HDSN.GetValueOrDefault(0));
            bienDong.originalValueSourceOdaIncreasement = (long)Math.Abs(item.NV_VIEN_TRO.GetValueOrDefault(0));
            List<TaiSanNguonVon> ListNguonVonTaiSan = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: model.ID).Where(m => m.biendong.NGAY_BIEN_DONG <= item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy")).ToList();
            decimal? TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonHoatDongSuNghiep).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceBusiness = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonNganSach).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceStateBudget = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonKhac).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceOther = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonVienTro).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceOda = (long)TongNguonVon.GetValueOrDefault(0);

            ///Ngồn cũ
            ListNguonVonTaiSan = ListNguonVonTaiSan.Where(m => m.biendong.NGAY_BIEN_DONG < item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy")).ToList();
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonHoatDongSuNghiep).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceBusinessOld = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonNganSach).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceStateBudgetOld = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonKhac).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceOtherOld = (long)TongNguonVon.GetValueOrDefault(0);
            TongNguonVon = ListNguonVonTaiSan.Where(m => m.NGUON_VON_ID == (decimal)enumNguonVon.NguonVienTro).Sum(m => m.GIA_TRI);
            bienDong.originalValueSourceOdaOld = (long)TongNguonVon.GetValueOrDefault(0);
            #endregion

            return bienDong;
        }
        #endregion
        #region Khai thác tài sản
        public List<Kho_KhaiThacTaiSan> PrepareDuLieuDongBoKhaiThacTaiSan(List<KhaiThacTaiSan> ListKhaiThacTaiSan, decimal HinhThucKhaiThacId = 0)
        {
            List<Kho_KhaiThacTaiSan> kho_KhaiTacTaiSans = new List<Kho_KhaiThacTaiSan>();
            List<KhaiThac> ListKhaiThac = ListKhaiThacTaiSan.Select(m => m.khaiThac).Distinct().ToList();
            foreach (var item in ListKhaiThac)
            {

                Kho_KhaiThacTaiSan data = new Kho_KhaiThacTaiSan();
                // data.assetCode = item.taiSan.MA_DB;
                data.decision.unitId = _donViService.GetDonViById(item.DON_VI_ID.Value).DB_ID;
                data.decision.startDate = CommonHelper.toDateKhoString(item.KHAI_THAC_NGAY_TU);
                data.decision.endDate = CommonHelper.toDateKhoString(item.KHAI_THAC_NGAY_DEN);
                data.decision.decisionNumber = item.QUYET_DINH_SO;
                data.decision.decisionDate = CommonHelper.toDateKhoString(item.QUYET_DINH_NGAY);
                data.decision.approver = item.NGUOI_DUYET;
                data.decision.contractValue = (long)item.SO_TIEN_TAM_TINH;
                data.decision.contractNumber = item.HOP_DONG_SO;
                data.decision.contractDate = CommonHelper.toDateKhoString(item.HOP_DONG_NGAY);
                data.decision.contractContent = item.NOI_DUNG;
                // Đợi thay đổi đồng bộ -- hungnt
                //data.decision.partner = item.DOI_TAC_ID;
                data.decision.syncSourceId = item.ID.ToString();
                data.decision.notes = item.GHI_CHU;
                // tài sản
                List<KhaiThacTaiSan> khaiThacTaiSans = ListKhaiThacTaiSan.Where(m => m.KHAI_THAC_ID == item.ID).ToList();
                List<assets> assets = new List<assets>();
                foreach (var ts in khaiThacTaiSans)
                {
                    assets _assets = new assets();
                    _assets.assetCode = ts.taiSan.MA_DB;
                    _assets.exploitingArea = (double?)ts.DIEN_TICH_KHAI_THAC;
                    data.assets.Add(_assets);
                }
                kho_KhaiTacTaiSans.Add(data);
            }
            return kho_KhaiTacTaiSans;
        }
        #endregion
        #region Khấu hao, hao mòn tài sản
        public DuLieuDongBoModel<Kho_HaoMon> PrepareDuLieuDongBoKhauHao()
        {
            var ListHaoMonTaiSan = _haoMonTaiSanService.GetAllHaoMonTaiSans(true);
            List<Kho_HaoMon> ListHaoMon = new List<Kho_HaoMon>();
            foreach (var item in ListHaoMonTaiSan)
            {
                if (item.DB_ID > 0)
                {
                    continue;
                }
                Kho_HaoMon kho_HaoMon = new Kho_HaoMon();
                kho_HaoMon.assetCode = item.MA_TAI_SAN;
                kho_HaoMon.year = (int?)item.NAM_HAO_MON;
                kho_HaoMon.residualValue = (long)item.TONG_GIA_TRI_CON_LAI.GetValueOrDefault(0);
                kho_HaoMon.originalValue = (long?)item.TONG_NGUYEN_GIA;
                kho_HaoMon.amortizationRate = (float?)item.TY_LE_HAO_MON;
                kho_HaoMon.amortizationValue = (long?)item.GIA_TRI_HAO_MON;
                kho_HaoMon.accumulatedAmortizationValue = (long?)item.TONG_HAO_MON_LUY_KE;
                ListHaoMon.Add(kho_HaoMon);
            }
            DuLieuDongBoModel<Kho_HaoMon> duLieu = new DuLieuDongBoModel<Kho_HaoMon>()
            {
                data = ListHaoMon
            };
            return duLieu;
        }
        #endregion
        public Data_XoaBienDong PrepareDuLieuXoaBienDong(ParameterXoaBienDong model)
        {
            Data_XoaBienDong kho_XoaBienDong = new Data_XoaBienDong();

            kho_XoaBienDong.assetCode = model.MaTaiSanDb;
            kho_XoaBienDong.syncSourceId = model.BienDongGuid;
            kho_XoaBienDong.unitId = model.DonViId;
            kho_XoaBienDong.mutationDate = CommonHelper.toDateKhoString(model.NgayBienDong);
            return kho_XoaBienDong;
        }
        public void Test()
        {

        }
        #region anhnt

        public Kho_DongBoTaiSan PrepareBienDongTangMoiCanDongBo(decimal donviId, List<BienDong> bienDongs = null, int TakeNumber = 100)
        {
            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            if (bienDongs == null || (bienDongs != null && bienDongs.Count == 0))
                bienDongs = _bienDongService.GetAllBienDongTangMoiCanDongBo(donViChaId: donviId, TakeNumber: TakeNumber);
            List<Kho_TaiSan_BienDong> kho_TaiSan_BienDongs = new List<Kho_TaiSan_BienDong>();
            Kho_TaiSan_BienDong kho_TaiSan_BienDong = new Kho_TaiSan_BienDong();
            DongBoApi_BienDongTaiSan dongBoApi_BienDongTaiSans = new DongBoApi_BienDongTaiSan();
            foreach (BienDong bienDong in bienDongs)
            {
                TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(bienDong.TAI_SAN_ID);
                if (tsnk != null && tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA)
                    continue;
                bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DANG_DONG_BO;
                _bienDongService.UpdateBienDong(bienDong);
                duLieuDongBo.CapNhatIDBienDongs.Add(new CapNhatIDBienDong() { TaiSanId = bienDong.TAI_SAN_ID, BienDongId = bienDong.ID });
                dongBoApi_BienDongTaiSans = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(bienDong.ID);
                if (dongBoApi_BienDongTaiSans == null)
                    continue;
                dongBoApi_BienDongTaiSans.ID = bienDong.ID;
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, bienDong.taisan.NGUON_TAI_SAN_ID);
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;
            return duLieuDongBo;
        }
        public Kho_DongBoTaiSan PrepareBienDongCanDongBo(List<BienDong> bienDongs = null)
        {
            Kho_DongBoTaiSan duLieuDongBo = new Kho_DongBoTaiSan();
            List<Kho_TaiSan_BienDong> kho_TaiSan_BienDongs = new List<Kho_TaiSan_BienDong>();
            Kho_TaiSan_BienDong kho_TaiSan_BienDong = new Kho_TaiSan_BienDong();
            DongBoApi_BienDongTaiSan dongBoApi_BienDongTaiSans = new DongBoApi_BienDongTaiSan();
            foreach (BienDong bienDong in bienDongs)
            {
                //TaiSanNhatKy tsnk = _taiSanNhatKyService.GetByTaiSanId(bienDong.TAI_SAN_ID);
                //if (tsnk != null && (tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.CHO_GET_MA || tsnk.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.DANG_DONG_BO))
                //    continue;
                if (bienDong.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
                    bienDong.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY &&
                    bienDong.taisan.MA_DB == null)
                    continue;
                bienDong.TRANG_THAI_DONG_BO = (decimal)enumDongBoBienDong.DANG_DONG_BO;
                _bienDongService.UpdateBienDong(bienDong);
                duLieuDongBo.CapNhatIDBienDongs.Add(new CapNhatIDBienDong() { TaiSanId = bienDong.TAI_SAN_ID, BienDongId = bienDong.ID });
                dongBoApi_BienDongTaiSans = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(bienDong.ID);
                if (dongBoApi_BienDongTaiSans == null)
                    continue;
                dongBoApi_BienDongTaiSans.ID = bienDong.ID;
                kho_TaiSan_BienDong = _dongBoServices.PrepareBienDongDongBo(dongBoApi_BienDongTaiSans, bienDong.taisan.NGUON_TAI_SAN_ID);
                if (kho_TaiSan_BienDong == null)
                    continue;
                kho_TaiSan_BienDongs.Add(kho_TaiSan_BienDong);
            }
            duLieuDongBo.data = kho_TaiSan_BienDongs;
            return duLieuDongBo;
        }
        //public Kho_TaiSan_BienDong PrepareBienDongDongBo(DongBoApi_BienDongTaiSan item)
        //{
        //    try
        //    {
        //        Kho_TaiSan_BienDong bienDong = new Kho_TaiSan_BienDong();
        //        bienDong.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
        //        bienDong.assetCode = item.MA_TAI_SAN_DB;
        //        bienDong.syncSourceAssetId = item.MA_TAI_SAN;
        //        if (!string.IsNullOrEmpty(item.NGAY_BIEN_DONG))
        //        {
        //            bienDong.mutationDate = item.NGAY_BIEN_DONG;
        //        }
        //        if (item.DB_LY_DO_BIEN_DONG_ID.HasValue)
        //            bienDong.mutationCauseId = (int)item.DB_LY_DO_BIEN_DONG_ID.Value;
        //        bienDong.approvedDate = item.NGAY_DUYET;
        //        bienDong.approverName = item.NGUOI_DUYET;
        //        bienDong.departmentId = (int?)item.DB_DON_VI_BO_PHAN_ID;
        //        bienDong.documentValue = item.GIA_MUA_TIEP_NHAN;
        //        //bienDong.amortizationRate = 0;
        //        decimal NguyenGiaTruocBienDong = 0;
        //        if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
        //            item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
        //            NguyenGiaTruocBienDong = (decimal)_bienDongService.ProcTinhNguyenGia(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"))?.NGUYEN_GIA.GetValueOrDefault(0);
        //        GiaTriTaiSan giatriCu = new GiaTriTaiSan();
        //        if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
        //            item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
        //        {
        //            giatriCu = _bienDongService.ProcTinhGiaTriTaiSan(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"), item.ID);
        //            if (giatriCu == null)
        //                return null;
        //            bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
        //        }
        //        bienDong.originalValueOld = (long?)NguyenGiaTruocBienDong;
        //        switch (item.LOAI_BIEN_DONG_ID)
        //        {
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO:
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangMoi;
        //                    bienDong.originalValue = (long)(NguyenGiaTruocBienDong + (item.NGUYEN_GIA.GetValueOrDefault(0)));
        //                    bienDong.originalValueOld = 0;
        //                    bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
        //                    bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
        //                    break;
        //                }
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.TangNguyenGia;
        //                    bienDong.originalValue = (long)(NguyenGiaTruocBienDong + Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
        //                    bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
        //                    bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
        //                    break;
        //                }
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.GiamNguyenGia;
        //                    bienDong.originalValue = (long)(NguyenGiaTruocBienDong - Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
        //                    bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
        //                    bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
        //                    break;
        //                }
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin;
        //                    bienDong.originalValue = (long)NguyenGiaTruocBienDong;
        //                    bienDong.changeReason = item.TEN_LY_DO;
        //                    bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);
        //                    bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
        //                    break;
        //                }
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.DieuChuyen;
        //                    bienDong.transferUnitCode = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
        //                    bienDong.transferUnitId = (int?)item.DON_VI_NHAN_DIEU_CHUYEN_DB_ID;
        //                    bienDong.transferUnitName = item.DON_VI_NHAN_DIEU_CHUYEN_TEN;
        //                    bienDong.originalValue = (long)(NguyenGiaTruocBienDong - Math.Abs(item.NGUYEN_GIA.GetValueOrDefault(0)));
        //                    bienDong.amortizationAccumulatedValue = (long?)(bienDong.originalValue - item.HM_GIA_TRI_CON_LAI);

        //                    bienDong.originalValueIncreasement = (long?)item.NGUYEN_GIA;
        //                    break;
        //                }
        //            case (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO:
        //                {
        //                    bienDong.assetMutationTypeId = (int)enumKho_Loai_Bien_Dong.Giam;
        //                    bienDong.originalValue = 0;
        //                    bienDong.originalValueIncreasement = (long)NguyenGiaTruocBienDong;
        //                    if (item.LY_DO_BIEN_DONG_MA == enum_LY_DO_BIEN_DONG.DIEU_CHUYEN)
        //                    {
        //                        bienDong.transferUnitCode = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
        //                        bienDong.transferUnitId = (int?)item.DON_VI_NHAN_DIEU_CHUYEN_DB_ID;
        //                        bienDong.transferUnitName = item.DON_VI_NHAN_DIEU_CHUYEN_TEN;
        //                    }
        //                    bienDong.amortizationAccumulatedValue = (Double?)((Double)NguyenGiaTruocBienDong - bienDong.residualValueOld);
        //                    break;
        //                }
        //            default:
        //                break;
        //        }
        //        bienDong.inputDate = item.NGAY_NHAP_TAI_SAN;
        //        bienDong.name = item.TEN_TAI_SAN_BD ?? item.TEN_TAI_SAN;
        //        bienDong.syncSourceId = item.GUID.ToString();
        //        bienDong.projectId = (long?)item.DB_DU_AN_ID;
        //        bienDong.unitId = (int)item.DB_DON_VI_ID.Value;
        //        bienDong.unitName = item.TEN_DON_VI;
        //        bienDong.assetTypeId = (int)item.DB_LOAI_TAI_SAN_ID.Value;
        //        bienDong.dateOfUse = item.NGAY_SU_DUNG;
        //        bienDong.usagePurposeId = (long?)item.DB_MUC_DICH_SU_DUNG_ID;
        //        bienDong.syncUnitId = 1;//khoTaiSan.syncUnitId;
        //        bienDong.documentDecisionDate = item.QUYET_DINH_NGAY;
        //        bienDong.documentDecisionNumber = item.QUYET_DINH_SO;
        //        bienDong.amortizationRate = (float?)item.HM_TY_LE_TAI_SAN;
        //        bienDong.documentReceipt = item.CHUNG_TU_SO;
        //        bienDong.documentDateReceipt = item.CHUNG_TU_NGAY;
        //        bienDong.procurementForm = item.HINH_THUC_MUA_SAM_TEN;
        //        bienDong.notes = item.GHI_CHU;
        //        bienDong.countryOfOrigin = item.NUOC_SAN_XUAT;
        //        bienDong.plantYear = (int?)item.NAM_SAN_XUAT;
        //        bienDong.residualValue = (long)item.HM_GIA_TRI_CON_LAI.GetValueOrDefault(0);
        //        if (item.IS_BAN_THANH_LY.GetValueOrDefault(0) == 1)
        //        {
        //            bienDong.assetHandlingFormSale = true;
        //            bienDong.assetHandlingFormValueObtained = item.PHI_THU;
        //        }

        //        if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
        //        {
        //            if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
        //                item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
        //            {
        //                bienDong.residualValueOld = (long)giatriCu.HM_GIA_TRI_CON_LAI_CU;
        //                bienDong.landAreaOld = (double?)giatriCu.DAT_TONG_DIEN_TICH_CU;
        //            }
        //            bienDong.landUseRightValue = (long?)item.NGUYEN_GIA_BAN_DAU;//(long?)item.DAT_GIA_TRI_QUYEN_SD_DAT;
        //            bienDong.landAddress = item.TEN_TAI_SAN_BD ?? item.TEN_TAI_SAN;
        //            if (bienDong.landAddress == null)
        //                bienDong.landAddress = item.DIA_BAN_TEN;
        //            bienDong.landRegionId = (int?)item.DB_DIA_BAN_ID;
        //            bienDong.landCountryId = (long?)item.DB_QUOC_GIA_ID;
        //            if (bienDong.landCountryId.GetValueOrDefault(0) == 0)
        //            {
        //                QuocGia quocGia = _quocGiaService.GetQuocGiaByMa("VN");
        //                bienDong.landCountryId = quocGia != null ? quocGia.DB_ID : null;
        //            }
        //            if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
        //            {
        //                bienDong.landArea = bienDong.landAreaOld.GetValueOrDefault(0) - (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
        //                bienDong.landAreaIncreasement = (double)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
        //            {
        //                bienDong.landArea = bienDong.landAreaOld.GetValueOrDefault(0) + (double?)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
        //                bienDong.landAreaIncreasement = (double)Math.Abs(item.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
        //            {
        //                bienDong.landAreaIncreasement = bienDong.landAreaOld;
        //                bienDong.landArea = 0;
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
        //            {
        //                bienDong.landAreaIncreasement = 0;
        //                bienDong.landArea = bienDong.landAreaOld;
        //            }
        //            //else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
        //            //{
        //            //    bienDong.landAreaIncreasement = 0;
        //            //}               

        //            #region Hồ sơ giấy tờ
        //            bienDong.landDocumentLandUseRight = item.HS_CNQSD_SO;
        //            if (!string.IsNullOrEmpty(item.HS_CNQSD_NGAY))
        //            {
        //                bienDong.landDocumentDateLandUseRightDate = item.HS_CNQSD_NGAY;
        //            }
        //            bienDong.landDocumentLandAllocate = item.HS_QUYET_DINH_GIAO_SO;
        //            if (!string.IsNullOrEmpty(item.HS_QUYET_DINH_GIAO_NGAY))
        //            {
        //                bienDong.landDocumentDateLandAllocate = item.HS_QUYET_DINH_GIAO_NGAY;
        //            }
        //            bienDong.documentLeaseContract = item.HS_HOP_DONG_CHO_THUE_SO;
        //            if (!string.IsNullOrEmpty(item.HS_HOP_DONG_CHO_THUE_NGAY))
        //                bienDong.documentDateLeaseContract = item.HS_HOP_DONG_CHO_THUE_NGAY;
        //            bienDong.documentValue = item.GIA_MUA_TIEP_NHAN;
        //            bienDong.isTaxExemption = item.MIEN_THUE_SO_TIEN.GetValueOrDefault(0) > 0 ? true : false;
        //            bienDong.taxExamptionValue = (long?)item.MIEN_THUE_SO_TIEN;
        //            bienDong.originalValueDepreciation = (long?)item.KH_GIA_TRI_TRICH_THANG;

        //            bienDong.procurementFormId = (int?)item.HINH_THUC_MUA_SAM_DB_ID;
        //            bienDong.landDocumentLandLease = item.HS_QUYET_DINH_CHO_THUE_SO;
        //            if (!string.IsNullOrEmpty(item.HS_QUYET_DINH_CHO_THUE_NGAY))
        //            {
        //                bienDong.landDocumentDateLandLease = item.HS_QUYET_DINH_CHO_THUE_NGAY;
        //            }
        //            bienDong.documentOther = item.HS_PHAP_LY_KHAC;
        //            #endregion

        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
        //        {
        //            if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
        //                item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
        //            {
        //                bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(giatriCu.NHA_TONG_DIEN_TICH_XD_CU.GetValueOrDefault(0));

        //                bienDong.houseAreaFloorOld = (double)giatriCu.NHA_TONG_DIEN_TICH_XD_CU;
        //            }
        //            else
        //            {
        //                bienDong.houseAreaFloorIncreasement = (double)item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
        //                bienDong.houseAreaFloorOld = 0;
        //            }
        //            bienDong.houseAddress = item.DIA_CHI;
        //            bienDong.houseAreaBuilding = (double?)item.TS_NHA_DIEN_TICH_XAY_DUNG.GetValueOrDefault(0);

        //            bienDong.houseNumberOfFloor = (long)item.NHA_SO_TANG.GetValueOrDefault(0);
        //            bienDong.houseBuiltYear = (int?)item.NHA_NAM_XAY_DUNG;
        //            bienDong.houseLandCode = item.TS_NHA_TAI_SAN_DAT_MA;

        //            if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.GiamNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.DieuChuyen)
        //            {
        //                bienDong.houseAreaFloor = bienDong.houseAreaFloorOld - (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
        //                bienDong.houseAreaFloorIncreasement = (double)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangNguyenGia || bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.TangMoi)
        //            {
        //                bienDong.houseAreaFloor = bienDong.houseAreaFloorOld + (double?)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
        //                bienDong.houseAreaFloorIncreasement = (double)Math.Abs(item.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
        //            {
        //                bienDong.houseAreaFloorIncreasement = bienDong.houseAreaFloorOld;
        //                bienDong.houseAreaFloor = 0;
        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
        //            {
        //                bienDong.houseAreaFloorIncreasement = 0;
        //                bienDong.houseAreaFloor = bienDong.houseAreaFloorOld;
        //            }
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.OTO || item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
        //        {
        //            bienDong.vehicleRegistrationPlateNumber = item.OTO_BIEN_KIEM_SOAT;
        //            bienDong.enginePower = (double?)item.OTO_CONG_XUAT;
        //            if (item.OTO_XI_LANH.HasValue)
        //                bienDong.motorCylinder = item.OTO_XI_LANH.GetValueOrDefault(0).ToString();
        //            else if (item.TS_OTO_DUNG_TICH.HasValue)
        //            {
        //                bienDong.motorCylinder = item.TS_OTO_DUNG_TICH.GetValueOrDefault(0).ToString();
        //            }
        //            bienDong.vehicleSize = (long?)item.OTO_SO_CHO_NGOI;

        //            bienDong.vehicleCapacity = (long)item.OTO_TAI_TRONG.GetValueOrDefault(0);

        //            bienDong.vehicleChassisNumber = item.TS_OTO_SO_MAY;
        //            bienDong.vehicleEngineNumber = item.TS_OTO_SO_KHUNG;
        //            bienDong.brandName = item.OTO_NHAN_XE_TEN;
        //            bienDong.vehicleUserTitle = item.TS_OTO_CHUC_DANH_TEN;
        //            bienDong.vehicleUserTitleId = (int?)item.OTO_CHUC_DANH_DB_ID;
        //            bienDong.brandId = (int?)item.OTO_NHAN_XE_DB_ID;
        //            bienDong.vehicleNumberOfWheelDrives = (int?)item.TS_OTO_SO_CAU;
        //            bienDong.vehicleRegistrationDocumentNumber = item.TS_OTO_GCN_DANG_KY;
        //            bienDong.vehicleRegistrationIssuedAuthority = item.TS_OTO_CO_QUAN_CAP_DANG_KY;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
        //        {
        //            if (item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO &&
        //             item.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
        //            {
        //                bienDong.volumeIncreasement = (double)Math.Abs(item.VKT_THE_TICH.GetValueOrDefault(giatriCu.VKT_THE_TICH_CU.GetValueOrDefault(0)));
        //                bienDong.lengthIncreasement = (double)Math.Abs(item.VKT_CHIEU_DAI.GetValueOrDefault(giatriCu.VKT_CHIEU_DAI_CU.GetValueOrDefault(0)));
        //                bienDong.volumeOld = (double?)giatriCu.VKT_THE_TICH_CU;
        //                bienDong.lengthOld = (double?)giatriCu.VKT_CHIEU_DAI_CU;
        //                bienDong.landAreaOld = (double?)giatriCu.VKT_DIEN_TICH_CU;
        //            }
        //            else
        //            {
        //                bienDong.volumeIncreasement = (double)Math.Abs(item.VKT_THE_TICH.GetValueOrDefault(0));
        //                bienDong.lengthIncreasement = (double)Math.Abs(item.VKT_CHIEU_DAI.GetValueOrDefault(0));
        //                bienDong.landAreaIncreasement = (double)Math.Abs(item.VKT_DIEN_TICH.GetValueOrDefault(0));
        //                bienDong.landAreaOld = 0;
        //                bienDong.volumeOld = 0;
        //                bienDong.lengthOld = 0;
        //            }
        //            if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.Giam)
        //            {
        //                bienDong.volumeIncreasement = bienDong.volumeOld;
        //                bienDong.volume = 0;
        //                bienDong.lengthIncreasement = bienDong.lengthOld;
        //                bienDong.length = 0;
        //                bienDong.landAreaIncreasement = bienDong.landAreaOld;
        //                bienDong.landArea = 0;

        //            }
        //            else if (bienDong.assetMutationTypeId == (int)enumKho_Loai_Bien_Dong.ThayDoiThongTin)
        //            {
        //                bienDong.volumeIncreasement = 0;
        //                bienDong.volume = (float?)bienDong.volumeOld;
        //                bienDong.lengthIncreasement = 0;
        //                bienDong.length = (float?)bienDong.lengthOld;
        //                bienDong.landAreaIncreasement = 0;
        //                bienDong.landArea = (float?)bienDong.landAreaOld;
        //            }
        //            bienDong.volume = (float?)item.VKT_THE_TICH;
        //            bienDong.length = (float?)item.TS_VKT_CHIEU_DAI;
        //            bienDong.landArea = (double?)item.TS_VKT_DIEN_TICH;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI)
        //        {
        //            bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV)
        //        {
        //            bienDong.plantYear = (int)item.TS_CLN_NAM_SINH;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
        //        {
        //            bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
        //        {
        //            bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
        //        }
        //        else if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU)
        //        {
        //            bienDong.specifications = item.TS_THONG_SO_KY_THUAT;
        //        }
        //        #region Hiện trạng sử dụng
        //        Kho_assetMutationAssetUsageStates khoHienTrang = new Kho_assetMutationAssetUsageStates();
        //        if (item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA || item.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
        //        {
        //            if (item.LST_HIEN_TRANG != null && item.LST_HIEN_TRANG.Count > 0)
        //            {
        //                bienDong.assetMutationAssetUsageStates = item.LST_HIEN_TRANG.Where(c => c.GIA_TRI_NUMBER > 0).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HIEN_TRANG_DB_ID, usageValue = (double)x.GIA_TRI_NUMBER }).ToList();
        //            }
        //            else
        //            {
        //                List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID.GetValueOrDefault(0));
        //                bienDong.assetMutationAssetUsageStates = ListHienTrang.Where(c => c.GIA_TRI_NUMBER > 0).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HienTrang.DB_ID, usageValue = (double)x.GIA_TRI_NUMBER }).ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (item.LST_HIEN_TRANG != null && item.LST_HIEN_TRANG.Count > 0)
        //            {
        //                bienDong.assetMutationAssetUsageStates = item.LST_HIEN_TRANG.Where(c => c.GIA_TRI_CHECKBOX.HasValue && c.GIA_TRI_CHECKBOX == 1).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HIEN_TRANG_DB_ID, usageValue = 1 }).ToList();
        //            }
        //            else
        //            {
        //                List<TaiSanHienTrangSuDung> ListHienTrang = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(item.ID.GetValueOrDefault(0));
        //                bienDong.assetMutationAssetUsageStates = ListHienTrang.Where(c => c.GIA_TRI_CHECKBOX.HasValue && c.GIA_TRI_CHECKBOX == true).Select(x => new Kho_assetMutationAssetUsageStates() { usageStateId = (int?)x.HienTrang.DB_ID, usageValue = 1 }).ToList();
        //            }
        //        }
        //        #endregion
        //        #region Nguồn vốn
        //        bienDong.originalValueSourceStateBudgetIncreasement = (long)Math.Abs(item.NV_NGAN_SACH.GetValueOrDefault(0));
        //        bienDong.originalValueSourceOtherIncreasement = (long)Math.Abs(item.NV_NGUON_KHAC.GetValueOrDefault(0));
        //        bienDong.originalValueSourceBusinessIncreasement = (long)Math.Abs(item.NV_HDSN.GetValueOrDefault(0));
        //        bienDong.originalValueSourceOdaIncreasement = (long)Math.Abs(item.NV_VIEN_TRO.GetValueOrDefault(0));

        //        GiaTriNguonVon giaTriNguonVon = _bienDongService.ProcTinhGiaTriNguonVon(item.TAI_SAN_ID.GetValueOrDefault(0), item.NGAY_BIEN_DONG.toDateSys("dd-MM-yyyy"));
        //        if (giaTriNguonVon == null)
        //            return null;
        //        bienDong.originalValueSourceBusiness = (long)giaTriNguonVon.NEW_NGUON_VON_SU_NGHIEP;
        //        bienDong.originalValueSourceStateBudget = (long)giaTriNguonVon.NEW_NGUON_VON_NS;
        //        bienDong.originalValueSourceOther = (long)giaTriNguonVon.NEW_NGUON_VON_KHAC;
        //        bienDong.originalValueSourceOda = (long)giaTriNguonVon.NEW_NGUON_VON_VIEN_TRO;

        //        ///Ngồn cũ
        //        bienDong.originalValueSourceBusinessOld = (long)giaTriNguonVon.OLD_NGUON_VON_SU_NGHIEP;
        //        bienDong.originalValueSourceStateBudgetOld = (long)giaTriNguonVon.OLD_NGUON_VON_NS;
        //        bienDong.originalValueSourceOtherOld = (long)giaTriNguonVon.OLD_NGUON_VON_KHAC;
        //        bienDong.originalValueSourceOdaOld = (long)giaTriNguonVon.OLD_NGUON_VON_VIEN_TRO;


        //        #endregion
        //        return bienDong;
        //    }
        //    catch (Exception ex)
        //    {
        //        //var st = new StackTrace(ex, true);
        //        //var frame = st.GetFrame(0);
        //        //var line = frame.GetFileLineNumber();
        //        //_hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Lỗi đồng bộ biến động dòng {line}", item, "BienDong");
        //        return null;
        //    }



        //}

        #endregion
    }

}
