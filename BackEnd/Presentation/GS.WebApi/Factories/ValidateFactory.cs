using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DM;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.TaiSans;
using GS.WebApi.Common;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GS.WebApi.Factories
{

    public class ValidateFactory : IValidateFactory
    {
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IHienTrangService _hienTrangService;
        private readonly INguonVonService _nguonVonService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly IDonViService _donViService;
        private readonly INhanXeService _nhanXeService;
        // private readonly IDanhMucFactory _danhMucModelFactory;
        private readonly IDongXeService _dongXeService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDuAnService _duAnService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ITaiSanService _taiSanService;
        private readonly IKhaiThacService _khaiThacService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
        private readonly ILoaiLyDoBienDongService _loaiLyDoBienDongService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        public ValidateFactory(
             IQuocGiaService quocGiaService,
            IHienTrangService hienTrangService,
            INguonVonService nguonVonService,
            ILoaiDonViService loaiDonViService,
            IDonViService donViService,
            INhanXeService nhanXeService,
            IDongXeService dongXeService,
            ILyDoBienDongService lyDoBienDongService,
            IDiaBanService diaBanService,
            ILoaiTaiSanService loaiTaiSanService,
            IChucDanhService chucDanhService,
            IDuAnService duAnService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IMucDichSuDungService mucDichSuDungService,
            INguoiDungService nguoiDungService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            ITaiSanService taiSanService,
            IDonViBoPhanService donViBoPhanService,
            IKhaiThacService khaiThacService,
            IBienDongService bienDongService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ICheDoHaoMonService cheDoHaoMonService,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhauHaoTaiSanService khauHaoTaiSanService,
            ILoaiLyDoBienDongService loaiLyDoBienDongService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IBienDongChiTietService bienDongChiTietService
            )
        {
            this._quocGiaService = quocGiaService;
            this._hienTrangService = hienTrangService;
            this._nguonVonService = nguonVonService;
            this._loaiDonViService = loaiDonViService;
            this._donViService = donViService;
            this._nhanXeService = nhanXeService;
            this._dongXeService = dongXeService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._diaBanService = diaBanService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._chucDanhService = chucDanhService;
            this._duAnService = duAnService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._nguoiDungService = nguoiDungService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._taiSanService = taiSanService;
            this._donViBoPhanService = donViBoPhanService;
            this._khaiThacService = khaiThacService;
            this._bienDongService = bienDongService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khauHaoTaiSanService = khauHaoTaiSanService;
            this._loaiLyDoBienDongService = loaiLyDoBienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
        }
        public bool CheckTonTaiDonVi(decimal DonViId = 0, string MaDonVi = null, string MaDVQHNS = null)
        {
            DonVi donVi = new DonVi();
            if (DonViId > 0)
            {
                donVi = _donViService.GetDonViById(DonViId);
            }
            if (!string.IsNullOrEmpty(MaDonVi))
            {
                donVi = _donViService.GetDonViByMa(MaDonVi);
            }
            else if (!string.IsNullOrEmpty(MaDVQHNS))
            {
                donVi = _donViService.GetDonViByMaDVQHNS(MaDVQHNS);
            }
            if (donVi == null)
            {
                return false;
            }

            return true;
        }
        public List<string> CheckTonTaiTaiSan(List<string> ListMaTaiSan)
        {
            List<string> ListError = new List<string>();
            foreach (var item in ListMaTaiSan)
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item);
                if (taiSan == null)
                {
                    ListError.Add(item + ": Không tồn tại");
                }
            }
            return ListError;
        }
        public bool CheckTonTaiKhaiThac(decimal? Id)
        {
            if (Id > 0)
            {
                KhaiThac khaiThac = _khaiThacService.GetKhaiThacById(Id.Value);
                if (khaiThac == null)
                    return false;
            }
            return true;
        }
        public bool CheckTonTaiLoaiTaiSan(decimal? LoaiTaiSanId)
        {
            if (LoaiTaiSanId > 0)
            {
                LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSanId.Value);
                if (loaiTaiSan == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckTonTaiDonViBoPhan(decimal? DonViBoPhanId)
        {
            if (DonViBoPhanId > 0)
            {
                DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(DonViBoPhanId.Value);
                if (donViBoPhan == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckTonTaiQuocGia(decimal? QuocGiaId)
        {
            if (QuocGiaId > 0)
            {
                QuocGia quocGia = _quocGiaService.GetQuocGiaById(QuocGiaId.Value);
                if (quocGia == null)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        ///  kiểm tra loại tài sản có khớp với thông tin tài sản đi kèm hay ko 
        /// </summary>
        /// <param name="TaiSan"></param>
        /// <param name="LoaiTaiSanId"></param>
        /// <returns></returns>
        public List<string> CheckThongTinTaiSanChiTiet(TaiSanDBModel dbModel = null, decimal? LoaiTaiSanId = null, decimal? LoaiTaiSanDonViId = null)
        {
            List<string> ListError = new List<string>();
            if (dbModel.TS_CLN == null && dbModel.TS_DAC_THU == null && dbModel.TS_DAT == null && dbModel.TS_NHA == null && dbModel.TS_MAY_MOC == null && dbModel.TS_HUU_HINH_KHAC == null && dbModel.TS_OTO == null && dbModel.TS_PTK == null && dbModel.TS_VO_HINH == null && dbModel.TS_KHAC == null && dbModel.TS_VKT == null)
            {
                ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Thông tin chi tiết tài sản null");
            }
            decimal LoaiHinhTaiSanId = 0;
            if (LoaiTaiSanId > 0)
            {
                //var LoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(CommonHelperApi.GetIdDanhMucForNhanTaiSan(LoaiTaiSanId.Value));
                LoaiTaiSan LoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSanId.Value);
                if (LoaiTaiSan == null)
                {
                    return null;
                }
                LoaiHinhTaiSanId = LoaiTaiSan.LOAI_HINH_TAI_SAN_ID.Value;
            }
            if (LoaiTaiSanDonViId > 0)
            {
                var LoaiTaiSan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(LoaiTaiSanDonViId.Value);
                if (LoaiTaiSan == null)
                {
                    return null;
                }
                LoaiHinhTaiSanId = LoaiTaiSan.LOAI_HINH_TAI_SAN_ID.Value;
            }

            switch (LoaiHinhTaiSanId)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        if (dbModel.TS_DAT != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_DAT.DIA_CHI))
                                ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_DAT.DIA_CHI null");
                            if (!(dbModel.TS_DAT.DIEN_TICH > 0))
                                ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_DAT.DIEN_TICH null");
                            if (dbModel.TS_DAT.DIA_BAN_ID > 0)
                            {
                                var diaban = _diaBanService.GetDiaBanById(dbModel.TS_DAT.DIA_BAN_ID.Value);
                                if (diaban == null)
                                {
                                    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_DAT.DIA_BAN_ID= { dbModel.TS_DAT.DIA_BAN_ID.Value } không tồn tại");

                                }
                            }
                        }
                        else
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        if (dbModel.TS_NHA != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_NHA.DIA_CHI))
                                ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_NHA.DIA_CHI null");
                            //if (!dbModel.TS_NHA.NAM_XAY_DUNG.HasValue || (dbModel.TS_NHA.NAM_XAY_DUNG.HasValue && dbModel.TS_NHA.NAM_XAY_DUNG == 0))
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_NHA.NAM_XAY_DUNG null");
                            //if (!dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue || (dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG.HasValue && dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG == 0))
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_NHA.DIEN_TICH_SAN_XAY_DUNG null");
                            //if (!dbModel.TS_NHA.NHA_SO_TANG.HasValue || (dbModel.TS_NHA.NHA_SO_TANG.HasValue && dbModel.TS_NHA.NHA_SO_TANG == 0))
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_NHA.NHA_SO_TANG null");
                            //if (!string.IsNullOrEmpty(dbModel.TS_NHA.TAI_SAN_DAT_MA))
                            //{
                            //    TaiSan _taiSanDat = _taiSanService.GetTaiSanByMaDB(dbModel.TS_NHA.TAI_SAN_DAT_MA);
                            //    if (_taiSanDat == null)
                            //    {
                            //        ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_NHA.TAI_SAN_DAT_MA Không tồn tại");
                            //    }
                            //}
                        }
                        else
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        if (dbModel.TS_OTO != null)
                        {
                            if (string.IsNullOrEmpty(dbModel.TS_OTO.BIEN_KIEM_SOAT))
                                ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_OTO.BIEN_KIEM_SOAT null");
                            //if (dbModel.TS_OTO.SO_CHO_NGOI > 0 && dbModel.TS_OTO.TAI_TRONG > 0)
                            //{
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_OTO.SO_CHO_NGOI - TS_OTO.TAI_TRONG invalid");
                            //}
                            //if (!(dbModel.TS_OTO.SO_CHO_NGOI > 0 || dbModel.TS_OTO.TAI_TRONG > 0))
                            //{
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_OTO.SO_CHO_NGOI - TS_OTO.TAI_TRONG null");
                            //}tạm bỏ
                            if (!string.IsNullOrEmpty(dbModel.TS_OTO.NHAN_XE_MA))
                            {
                                NhanXe nhanXe = _nhanXeService.GetNhanXeByMaTen(ma: dbModel.TS_OTO.NHAN_XE_MA);
                                if (nhanXe == null)
                                {
                                    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_OTO.NHAN_XE_MA = {dbModel.TS_OTO.NHAN_XE_MA} - Nhãn xe không tồn tại");
                                }
                            }
                            // kiểm tra giá hóa đơn

                            BienDongDBModel bienDongDB = dbModel.LST_BIEN_DONG.Where(m => m.LOAI_BIEN_DONG_ID != null && (m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)).FirstOrDefault();
                            if (bienDongDB != null)
                            {
                                LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(bienDongDB.LY_DO_BIEN_DONG_ID.GetValueOrDefault(0));
                                if (lyDoBienDong != null)
                                {
                                    if (lyDoBienDong.MA == enum_LY_DO_BIEN_DONG.MUA_SAM)
                                    {
                                        //if (!(dbModel.GIA_HOA_DON > 0))
                                        //{
                                        //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: GIA_HOA_DON bắt buộc >0");
                                        //}tạm bỏ
                                    }
                                }
                            }

                        }
                        else
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    {
                        if (dbModel.TS_PTK != null)
                        {
                            //if (dbModel.TS_PTK.SO_CHO_NGOI > 0 && dbModel.TS_PTK.TAI_TRONG > 0)
                            //{
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_PTK.SO_CHO_NGOI - TS_PTK.TAI_TRONG invalid");
                            //}
                            //if (!(dbModel.TS_PTK.SO_CHO_NGOI > 0 || dbModel.TS_PTK.TAI_TRONG > 0))
                            //{
                            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: TS_PTK.SO_CHO_NGOI - TS_PTK.TAI_TRONG null");
                            //}tạm bỏ
                        }
                        else
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    {
                        if (dbModel.TS_VKT == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    {
                        if (dbModel.TS_CLN == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    {
                        if (dbModel.TS_MAY_MOC == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    {
                        if (dbModel.TS_DAC_THU == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    {
                        if (dbModel.TS_HUU_HINH_KHAC == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    {
                        if (dbModel.TS_VO_HINH == null)
                        {
                            ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Loại tài sản và thông tin tài sản không đúng");
                        }
                        break;
                    }
                default:
                    break;
            }
            // Kiểm tra trùng ngày biến động
            //var ListNgayBienDong = dbModel.LST_BIEN_DONG.Select(m => m.NGAY_BIEN_DONG.toDateSys().Value.Date);
            //int SoNgayBienDong = ListNgayBienDong.Distinct().Count();
            //if (SoNgayBienDong < dbModel.LST_BIEN_DONG.Count())
            //{
            //    ListError.Add($"TAI_SAN:DB_MA= { dbModel.DB_MA}: Ngày biến động trùng");
            //}
            return ListError;
        }
        public bool CheckTonTaiLyDoBienDong(decimal? LyDoBienDongId)
        {
            if (LyDoBienDongId > 0)
            {
                LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(LyDoBienDongId.Value);
                if (lyDoBienDong == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckTonTaiDiaBan(decimal? DiaBanId)
        {
            if (DiaBanId > 0)
            {
                DiaBan diaBan = _diaBanService.GetDiaBanById(DiaBanId.Value);
                if (diaBan == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckDonViNhanDieuChuyen(string MaDonViNhanDieuChuyen)
        {
            return CheckTonTaiDonVi(MaDonVi: MaDonViNhanDieuChuyen);
        }
        public bool CheckTonTaiMucDichSuDung(decimal? MucDichSuDungId)
        {
            if (MucDichSuDungId > 0)
            {
                MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(MucDichSuDungId.Value);
                if (mucDichSuDung == null)
                {
                    return false;
                }
            }
            return true;
        }
        public List<string> CheckBienDongTheoTaiSan(TaiSanDBModel taiSanDB)
        {
            List<string> ListError = new List<string>();
            if (taiSanDB.LST_BIEN_DONG == null || taiSanDB.LST_BIEN_DONG.Count == 0)
            {
                ListError.Add($"TAI_SAN:DB_MA= { taiSanDB.DB_MA}: LST_BIEN_DONG null");
                return ListError;
            }
            decimal LoaiHinhTaiSanId = 0;
            if (taiSanDB.LOAI_TAI_SAN_ID > 0)
            {
                var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(taiSanDB.LOAI_TAI_SAN_ID.Value);
                if (loaiTaiSan == null)
                {
                    return new List<string>();
                }
                else
                {
                    LoaiHinhTaiSanId = loaiTaiSan.LOAI_HINH_TAI_SAN_ID.Value;
                }
            }
            else
                taiSanDB.LOAI_TAI_SAN_ID = null;
            MessageReturn result;
            List<BienDongDBModel> ListBienDong = taiSanDB.LST_BIEN_DONG.OrderBy(m => m.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB)).ToList();
            BienDongDBModel bienDongFirst = ListBienDong.First();
            if (bienDongFirst.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO
                && bienDongFirst.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            {
                ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG: Biến động đầu tiên phải là biến động tăng mới/ nhập số dư");
            }
            decimal TongHienTrang = 0;
            foreach (var model in ListBienDong)
            {
                if (!CheckTonTaiLyDoBienDong(model.LY_DO_BIEN_DONG_ID))
                {
                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-LY_DO_BIEN_DONG_ID không tồn tại");
                }
                #region Biến động tăng mới
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
                {
                    //if (!(model.NGUYEN_GIA > 0))
                    //{
                    //    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-NGUYEN_GIA null");
                    //}
                    switch (LoaiHinhTaiSanId)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                if (string.IsNullOrEmpty(model.DIA_CHI))
                                {
                                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DIA_CHI null");
                                }
                                //if (!(model.DAT_TONG_DIEN_TICH > 0))
                                //{
                                //    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH null");
                                //}tạm bỏ
                                if (model.DIA_BAN_ID > 0)
                                {
                                    DiaBan diaBan = _diaBanService.GetDiaBanById(model.DIA_BAN_ID.Value);
                                    if (diaBan == null)
                                    {
                                        ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DIA_BAN_ID Không tồn tại");
                                    }
                                }
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                if (string.IsNullOrEmpty(model.DIA_CHI))
                                {
                                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DIA_CHI null");
                                }
                                if (model.DIA_BAN_ID > 0)
                                {
                                    DiaBan diaBan = _diaBanService.GetDiaBanById(model.DIA_BAN_ID.Value);
                                    if (diaBan == null)
                                    {
                                        ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DIA_BAN_ID Không tồn tại");
                                    }
                                }
                                //if (!(model.NHA_TONG_DIEN_TICH_XD > 0))
                                //{
                                //    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD null");
                                //}tạm bỏ
                                //if (!(model.NHA_NAM_XAY_DUNG > 0))
                                //{
                                //    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_NAM_XAY_DUNG null");
                                //}tạm bỏ
                                //if (!(model.NHA_SO_TANG > 0))
                                //{
                                //    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_SO_TANG null");
                                //}tạm bỏ
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                            {
                                if (string.IsNullOrEmpty(model.OTO_BIEN_KIEM_SOAT))
                                {
                                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB} - OTO_BIEN_KIEM_SOAT null");
                                }
                                if (model.OTO_SO_CHO_NGOI > 0 && model.OTO_TAI_TRONG > 0)
                                {
                                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB} -OTO_SO_CHO_NGOI, OTO_TAI_TRONG Chỉ một trường được >0");
                                }
                                //if (!(model.OTO_SO_CHO_NGOI > 0 || model.OTO_TAI_TRONG > 0))
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB} -OTO_SO_CHO_NGOI, OTO_TAI_TRONG TAI_TRONG null");
                                //}tạm bỏ
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                            {
                                if (model.OTO_SO_CHO_NGOI > 0 && model.OTO_TAI_TRONG > 0)
                                {
                                    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB} -OTO_SO_CHO_NGOI, OTO_TAI_TRONG Chỉ một trong 2 trường >0");
                                }
                                //if (!(model.OTO_SO_CHO_NGOI > 0 || model.OTO_TAI_TRONG > 0))
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB} -OTO_SO_CHO_NGOI hoặc OTO_TAI_TRONG TAI_TRONG bắt buộc >0");
                                //}tạm bỏ
                                break;
                            }
                        default:
                            break;
                    }
                    // validate nguồn vốn
                    result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                    if (result.Code != MessageReturn.SUCCESS_CODE)
                    {
                        ListError.Add(result.Message);
                    }
                    // validate Hiện trạng
                    var ErrorHienTrang = ValidateHienTrang(model, LoaiHinhTaiSanId, TongHienTrang);

                    ListError.AddRange(ErrorHienTrang);
                }
                #endregion
                #region biến động tăng nguyên giá
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
                {
                    //if (!(model.NGUYEN_GIA > 0))
                    //{
                    //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NGUYEN_GIA null");
                    //}
                    switch (LoaiHinhTaiSanId)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                //if (!(model.DAT_TONG_DIEN_TICH > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.TangDienTichDat)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH bắt buộc >0 với lý do tăng diện tích đất");
                                //}
                                //if (model.DAT_TONG_DIEN_TICH > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.TangGiaDat)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH bắt buộc  null với lý do Tăng giá đất");
                                //}tạm bỏ
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                //if (!(model.NHA_TONG_DIEN_TICH_XD > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.NangCapMoRongNha)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD bắt buộc >0 với lý do Nâng cấp mở rộng nhà");
                                //}
                                //if (model.NHA_TONG_DIEN_TICH_XD > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.DanhGiaLaiNguyenGiaNha)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD bắt buộc null với lý do đánh giá lại nguyên giá");
                                //}tạm bỏ
                                break;
                            }
                        default:
                            break;
                    }
                    // validate nguồn vốn
                    result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                    if (result.Code != MessageReturn.SUCCESS_CODE)
                    {
                        ListError.Add(result.Message);
                    }
                    // validate Hiện trạng              
                    var ErrorHienTrang = ValidateHienTrang(model, LoaiHinhTaiSanId, TongHienTrang);
                    ListError.AddRange(ErrorHienTrang);
                }
                #endregion
                #region biến động giảm nguyên giá
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI)
                {
                    //if (!(model.NGUYEN_GIA > 0))
                    //{
                    //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NGUYEN_GIA null");
                    //}
                    switch (LoaiHinhTaiSanId)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                //if (!(model.DAT_TONG_DIEN_TICH > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamDienTichDat)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH phải >0 với lý do biến động giảm diện tích đất");
                                //}
                                //if (model.DAT_TONG_DIEN_TICH > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamGiaDat)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH phải null với lý do biến động giảm giá đất");
                                //}tạm bỏ
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                //if (!(model.NHA_TONG_DIEN_TICH_XD > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.CaiTaoThuHepDienTichNha)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD phải >0 với lý do biến động cải tạo thu hẹp diện tích nhà");
                                //}
                                //if (model.NHA_TONG_DIEN_TICH_XD > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.Giam_DanhGiaLaiNguyenGiaNha)
                                //{
                                //    ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD phải null với lý do biến động Đánh giá lại nguyên giá");
                                //}tạm bỏ
                                break;
                            }
                        default:
                            break;
                    }
                    // validate nguồn vốn
                    result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                    if (result.Code != MessageReturn.SUCCESS_CODE)
                    {
                        ListError.Add(result.Message);
                    }
                    // validate Hiện trạng
                    var ErrorHienTrang = ValidateHienTrang(model, LoaiHinhTaiSanId, TongHienTrang, null, true);

                    ListError.AddRange(ErrorHienTrang);
                }
                #endregion
                #region Biến động thay đổi thông tin
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {

                    // validate Hiện trạng
                    var ErrorHienTrang = ValidateHienTrang(model, LoaiHinhTaiSanId, TongHienTrang);

                    ListError.AddRange(ErrorHienTrang);
                }
                #endregion
                #region Biến động điều chuyển
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                {
                    if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                    {
                        ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DON_VI_NHAN_DIEU_CHUYEN_MA null");
                    }
                    result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                    if (result.Code != MessageReturn.SUCCESS_CODE)
                    {
                        ListError.Add(result.Message);
                    }
                    // validate Hiện trạng
                    var ErrorHienTrang = ValidateHienTrang(model, LoaiHinhTaiSanId, TongHienTrang, null, true);

                    ListError.AddRange(ErrorHienTrang);
                }
                // validate nguồn vốn

                #endregion
                #region Biến động giảm tài sản
                if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                    if (model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamTaiSan_DieuChuyen)
                    {
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                        {
                            if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                            {
                                ListError.Add($"DB_MA: {taiSanDB.DB_MA} - LST_BIEN_DONG:ID_DB= { model.ID_DB}-DON_VI_NHAN_DIEU_CHUYEN_MA null");
                            }
                        }
                    }
                #endregion
                //if (ListError.Count == 0)
                //{
                if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    if (model.DAT_TONG_DIEN_TICH > 0)
                    {
                        if (model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamTaiSan_DieuChuyen
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.ChuyenNhuong
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.Ban
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.ThanhLy
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamDienTichDat
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamGiaDat)
                            TongHienTrang -= model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                        else
                            TongHienTrang += model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                    }
                }
                if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    if (model.NHA_TONG_DIEN_TICH_XD > 0)
                    {

                        if (model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamTaiSan_DieuChuyen
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.ChuyenNhuong
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.Ban
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.ThanhLy
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.Giam_DanhGiaLaiNguyenGiaNha
                            || model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.CaiTaoThuHepDienTichNha)
                            TongHienTrang -= model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                        else
                            TongHienTrang += model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                    }
                }
                //}
            }
            return ListError;
        }
        public MessageReturn ValidateNguonVon(BienDongDBModel model, decimal LoaiHinhTaiSanId)
        {
            if (LoaiHinhTaiSanId == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
            {
                if (model.NV_HDSN > 0 || model.NV_VIEN_TRO > 0)
                {
                    return MessageReturn.CreateErrorMessage($"LST_BIEN_DONG:ID_DB= { model.ID_DB} -Tài sản đất chỉ có nguồn vốn ngân sách và nguồn khác");
                }
            }
            decimal TongNguonVon = model.NV_VIEN_TRO.GetValueOrDefault(0) + model.NV_NGUON_KHAC.GetValueOrDefault(0) + model.NV_HDSN.GetValueOrDefault(0) + model.NV_NGAN_SACH.GetValueOrDefault(0);
            ////if (!(TongNguonVon > 0) && model.NGUYEN_GIA > 0)
            ////{
            ////    return MessageReturn.CreateErrorMessage($"LST_BIEN_DONG:ID_DB= { model.ID_DB} -Tổng nguồn vốn phải >0");
            ////}
            //else
            //{
            //    if (TongNguonVon != model.NGUYEN_GIA)
            //    {
            //        return MessageReturn.CreateErrorMessage($"LST_BIEN_DONG:ID_DB= { model.ID_DB} -Nguồn vốn phải bằng nguyên giá");
            //    }
            //}
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public List<string> ValidateHienTrang(BienDongDBModel model, decimal LoaiHinhTaiSanId, decimal TongGiaTriTruocBienDong = 0, string MaTaiSan = null, bool IsGiam = false)
        {
            List<string> ListError = new List<string>();
            if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
            {
                var TaiSan = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN);
                if (TaiSan == null)
                {
                    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB} - MA_TAI_SAN Không tồn tại");
                    return ListError;
                }
            }
            foreach (var item in model.LST_HIEN_TRANG)
            {
                if (item.HIEN_TRANG_ID > 0)
                {
                    var HienTrang = _hienTrangService.GetHienTrangById(item.HIEN_TRANG_ID.Value);
                    if (HienTrang == null)
                    {
                        ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB} - LST_HEN_TRANG:ID= { item.HIEN_TRANG_ID} Không tồn tại");
                    }
                }
                if (item.HIEN_TRANG_ID == 0)
                {
                    ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB} - LST_HEN_TRANG[{model.LST_HIEN_TRANG.IndexOf(item)}]:ID phải lớn hơn 0");
                }

            }
            switch (LoaiHinhTaiSanId)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
                        {
                            decimal TaiSanId = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN).ID;
                            List<decimal> bdIds = _bienDongService.GetBienDongsByTaiSanId(taiSanId: TaiSanId, NgayBDDen: model.NGAY_BIEN_DONG.toDateSys()).Select(c => c.ID).ToList();
                            if (bdIds != null && bdIds.Count() > 0)
                                TongGiaTriTruocBienDong += _bienDongChiTietService.GetBienDongChiTietByBDIds(bdIds).Sum(c => c.DAT_TONG_DIEN_TICH.GetValueOrDefault(0));
                        }
                        var ListHientrang = model.LST_HIEN_TRANG;//ListHienTrangNumber(model.HTSD_DAT, LoaiHinhTaiSanId);
                        decimal TongHienTrang = ListHientrang.Sum(m => m.GIA_TRI_NUMBER.GetValueOrDefault(0));
                        decimal TongHienTrangBienDong = 0;
                        if (IsGiam)
                        {
                            TongHienTrangBienDong = TongGiaTriTruocBienDong - model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0);
                        }
                        else
                        {
                            TongHienTrangBienDong = model.DAT_TONG_DIEN_TICH.GetValueOrDefault(0) + TongGiaTriTruocBienDong;
                        }
                        //TongGiaTriTruocBienDong = TongHienTrangBienDong;
                        if (TongHienTrang != TongHienTrangBienDong)
                        {
                            ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB} - Tổng DAT_TONG_DIEN_TICH các biến động trước phải bằng tổng hiện trạng sử dụng");
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
                        {
                            decimal TaiSanId = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN).ID;
                            List<decimal> bdIds = _bienDongService.GetBienDongsByTaiSanId(taiSanId: TaiSanId, NgayBDDen: model.NGAY_BIEN_DONG.toDateSys()).Select(c => c.ID).ToList();
                            if (bdIds != null && bdIds.Count() > 0)
                                TongGiaTriTruocBienDong += _bienDongChiTietService.GetBienDongChiTietByBDIds(bdIds).Sum(c => c.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0));

                        }
                        // var ListHienTrang = ListHienTrangNumber(model.HTSD_DAT, LoaiHinhTaiSanId);
                        var ListHienTrang = model.LST_HIEN_TRANG;
                        decimal TongHienTrang = ListHienTrang.Sum(m => m.GIA_TRI_NUMBER.GetValueOrDefault(0));
                        decimal TongHienTrangBienDong = 0;
                        if (IsGiam)
                        {
                            TongHienTrangBienDong = TongGiaTriTruocBienDong - model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0);
                        }
                        else
                        {
                            TongHienTrangBienDong = model.NHA_TONG_DIEN_TICH_XD.GetValueOrDefault(0) + TongGiaTriTruocBienDong;
                        }
                        //TongGiaTriTruocBienDong = TongHienTrangBienDong;
                        if (TongHienTrang != TongHienTrangBienDong)
                        {
                            ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB} - Tổng NHA_TONG_DIEN_TICH_XD các biến động trước phải bằng tổng hiện trạng sử dụng");
                        }
                        break;
                    }
                default:
                    break;
            }
            return ListError;
        }
        public bool CheckTonTaiTaiSan(string MaTaiSan)
        {
            if (!string.IsNullOrEmpty(MaTaiSan))
            {
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(MaTaiSan);
                if (taiSan == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckTonTaiBienDong(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                try
                {
                    var bienDong = _bienDongService.GetBienDongByGuid(Guid.Parse(guid));
                    if (bienDong == null)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }

            }
            return true;
        }
        public List<string> CheckBienDong(BienDongDBModel model)
        {
            List<string> ListError = new List<string>();
            MessageReturn result = new MessageReturn();
            //if (!string.IsNullOrEmpty(model.MA_TAI_SAN))
            if (!string.IsNullOrEmpty(model.MA_TAI_SAN_DB))
            {
                //TaiSan taiSan = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN);
                TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(Ma: model.MA_TAI_SAN_DB, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);

                if (taiSan != null)
                {
                    if (taiSan.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.CHO_DONG_BO)
                    {
                        ListError.Add($"Biến động:ID_DB= { model.ID_DB} -Tài sản đang chờ đồng bộ chưa thể cập nhật biến động");
                       
                    }
                    else
                    {
                        model.MA_TAI_SAN = taiSan.MA;
                        decimal LoaiHinhTaiSanId = taiSan.LOAI_HINH_TAI_SAN_ID.Value;
                        #region biến động tăng nguyên giá

                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI)
                        {
                            //if (!(model.NGUYEN_GIA > 0))
                            //{
                            //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NGUYEN_GIA null");
                            //}
                            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
                            {
                                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                                    {
                                        if (!(model.DAT_TONG_DIEN_TICH > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.TangDienTichDat)
                                        {
                                            ListError.Add($"LST_BIEN_DONG:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH bắt buộc >0 với lý do tăng diện tích đất");
                                        }
                                        //if (model.DAT_TONG_DIEN_TICH > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.TangGiaDat)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH bắt buộc  null với lý do Tănh giá đất");
                                        //}tạm bỏ

                                        break;
                                    }
                                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                                    {
                                        //if (!(model.NHA_TONG_DIEN_TICH_XD > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.NangCapMoRongNha)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD bắt buộc >0 với lý do Nâng cấp mở rộng nhà");
                                        //}tạm bỏ
                                        //if (model.NHA_TONG_DIEN_TICH_XD > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.DanhGiaLaiNguyenGiaNha)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD bắt buộc null với lý do đánh giá lại nguyên giá");
                                        //}tạm bỏ
                                        break;
                                    }
                                default:
                                    break;
                            }
                            // validate nguồn vốn
                            result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ListError.Add(result.Message);
                            }
                        }
                        #endregion
                        #region biến động giảm nguyên giá
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI)
                        {
                            //if (!(model.NGUYEN_GIA > 0))
                            //{
                            //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NGUYEN_GIA null");
                            //}
                            switch (LoaiHinhTaiSanId)
                            {
                                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                                    {
                                        //if (!(model.DAT_TONG_DIEN_TICH > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamDienTichDat)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH phải >0 với lý do biến động giảm diện tích đất");
                                        //}
                                        //if (model.DAT_TONG_DIEN_TICH > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.GiamGiaDat)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DAT_TONG_DIEN_TICH phải null với lý do biến động giảm giá đất");
                                        //}tạm bỏ
                                        break;
                                    }
                                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                                    {
                                        //if (!(model.NHA_TONG_DIEN_TICH_XD > 0) && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.CaiTaoThuHepDienTichNha)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD phải >0 với lý do biến động cải tạo thu hẹp diện tích nhà");
                                        //}
                                        //if (model.NHA_TONG_DIEN_TICH_XD > 0 && model.LY_DO_BIEN_DONG_ID == (decimal)enumLyDoBienDong.Giam_DanhGiaLaiNguyenGiaNha)
                                        //{
                                        //    ListError.Add($"Biến động:ID_DB= { model.ID_DB}-NHA_TONG_DIEN_TICH_XD phải null với lý do biến động Đánh giá lại nguyên giá");
                                        //}tạm bỏ
                                        break;
                                    }
                                default:
                                    break;
                            }
                            // validate nguồn vốn
                            result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ListError.Add(result.Message);
                            }
                        }
                        #endregion
                        #region Biến động thay đổi thông tin
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                        {


                        }
                        #endregion
                        #region Biến động điều chuyển
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                        {
                            if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                            {
                                ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DON_VI_NHAN_DIEU_CHUYEN_MA null");
                            }
                            result = ValidateNguonVon(model, LoaiHinhTaiSanId);
                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ListError.Add(result.Message);
                            }
                        }
                        // validate nguồn vốn

                        #endregion
                        #region Biến động giảm tài sản
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                        {
                            if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                            {
                                ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DON_VI_NHAN_DIEU_CHUYEN_MA null");
                            }
                        }
                        if (model.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                            switch (model.LY_DO_BIEN_DONG_ID)
                            {
                                case (decimal)enumLyDoBienDong.GiamTaiSan_DieuChuyen:
                                    {
                                        if (string.IsNullOrEmpty(model.DON_VI_NHAN_DIEU_CHUYEN_MA))
                                        {
                                            ListError.Add($"Biến động:ID_DB= { model.ID_DB}-DON_VI_NHAN_DIEU_CHUYEN_MA null");
                                        }
                                        break;
                                    }
                                //case (decimal)enumLyDoBienDong.GiamTaiSan_DieuChuyen:
                                //    {

                                //    }
                                default:
                                    break;
                            }

                        #endregion
                    }

                }
                else
                {
                    ListError.Add($"Biến động BD_ID={model.ID_DB} MA_TAI_SAN_DB không tồn tại");
                }
            }
            return ListError;
        }
        public string CheckTonTaiLoaiTaiSanDonViCha(decimal IdCha)
        {
            string result = null;
            // check loại tài sản: 
            LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(IdCha);
            if (loaiTaiSanDonVi == null)
            {
                result = "Loại tài sản cha không tồn tại";
            }
            return result;
        }
        public bool CheckTonTaiCheDoHaoMon(decimal CheDoHaoMonId)
        {
            CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(CheDoHaoMonId);
            if (cheDoHaoMon == null)
            {
                return false;
            }
            return true;
        }
        public string CheckLoaiTaisanForNhanTaiSan(decimal? LoaiTaiSanId, decimal? LoaiTaiSanDonViId)
        {
            string Error = null;
            if (LoaiTaiSanId == null && LoaiTaiSanDonViId == null)
            {
                Error = "LOAI_TAI_SAN_ID và LOAI_TAI_SAN_DON_VI null";
            }
            if (LoaiTaiSanId > 0)
            {
                //LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(CommonHelperApi.GetIdDanhMucForNhanTaiSan(LoaiTaiSanId.Value));
                LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSanId.Value);
                if (loaiTaiSan == null)
                {
                    Error = "LOAI_TAI_SAN_ID không tồn tại";
                }
            }
            if (LoaiTaiSanDonViId > 0)
            {
                LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(LoaiTaiSanDonViId.Value);
                if (loaiTaiSanDonVi == null)
                {
                    Error = "LOAI_TAI_SAN_DON_VI_ID không tồn tại";
                }
            }
            return Error;
        }
        public bool CheckTonTaiTaiSanDongBo(string MaDb)
        {
            //TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(Ma: MaDb, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
            //if (taiSan == null)
            //    return false;
            //return true;
            var rs = _taiSanService.CheckTonTaiByMaDB(MaDb, (decimal)enumNguonTaiSan.CSDLQGTSC);
            return rs;
        }
        public List<string> CheckChiTietHaoMonTaiSan(HaoMonDBModel haoMonDBModel, bool isCheckMaTaiSan = true)
        {
            List<string> ListError = new List<string>();
            if (isCheckMaTaiSan)
            {
                if (!CheckTonTaiTaiSanDongBo(haoMonDBModel.MA_TAI_SAN_DB))
                {
                    ListError.Add($"MA_TAI_SAN_DB = {haoMonDBModel.MA_TAI_SAN_DB} không tồn tại");
                }
            }


            if (haoMonDBModel.NAM_HAO_MON == 0)
            {
                ListError.Add($"MA_TAI_SAN_DB = {haoMonDBModel.MA_TAI_SAN_DB} - NAM_HAO_MON Bắt buộc >0");
            }
            //if (haoMonDBModel.DB_ID == 0)
            //{
            //    ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}- DB_ID Bắt buộc >0");
            //}
            // check năm hao mòn tài sản
            //if (haoMonDBModel.ID == 0)
            //{
            TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(haoMonDBModel.MA_TAI_SAN_DB, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
            if (taiSan != null)
            {
                haoMonDBModel.TAI_SAN_ID = taiSan.ID;

                HaoMonTaiSan haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(namBaoCao: haoMonDBModel.NAM_HAO_MON, tsId: taiSan.ID);
                if (haoMonTaiSan != null)
                {
                    ListError.Add($"MA_TAI_SAN_DB = {haoMonDBModel.MA_TAI_SAN_DB} - Tài sản đã có hao mòn ở năm  {haoMonDBModel.NAM_HAO_MON} (Không được thêm mới)");
                }
            }

            //}
            //else
            //{
            //    HaoMonTaiSan haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanById(haoMonDBModel.ID);
            //    if (haoMonTaiSan == null)
            //    {
            //        ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}- ID Không tồn tại");
            //    }
            //}
            return ListError;
        }

        public List<string> CheckChiTietHaoMonInTaiSan(HaoMonInTaiSanDBModel haoMonDBModel)
        {
            List<string> ListError = new List<string>();

            if (haoMonDBModel.NAM_HAO_MON == 0)
            {
                ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}- NAM_HAO_MON Bắt buộc >0");
            }
            //if (haoMonDBModel.DB_ID == 0)
            //{
            //    ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}- DB_ID Bắt buộc >0");
            //}
            // check năm hao mòn tài sản
            //if (haoMonDBModel.ID == 0)
            //{
            //    TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(haoMonDBModel.MA_TAI_SAN_DB, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
            //    if (taiSan != null)
            //    {
            //        haoMonDBModel.TAI_SAN_ID = taiSan.ID;

            //        HaoMonTaiSan haoMonTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(namBaoCao: haoMonDBModel.NAM_HAO_MON, tsId: taiSan.ID);
            //        if (haoMonTaiSan != null)
            //        {
            //            ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}-Tài sản đã có hao mòn ở năm  {haoMonDBModel.NAM_HAO_MON} (Không được thêm mới)");
            //        }
            //    }

            //}
            return ListError;
        }
        public List<string> CheckHaoMonTaiSan(HaoMonTaiSanDBModel haoMonDBModel)
        {
            List<string> ListError = new List<string>();

            if (haoMonDBModel.NAM_HAO_MON == 0)
            {
                ListError.Add($"NAM_HAO_MON = {haoMonDBModel.NAM_HAO_MON}- NAM_HAO_MON Bắt buộc >0");
            }
            return ListError;
        }
        public List<string> CheckChiTietKhauHaoTaiSan(KhauHaoDBModel khauHaoDBModel, bool isCheckMaTaiSan = true)
        {
            List<string> ListError = new List<string>();
            if (isCheckMaTaiSan)
            {
                if (!CheckTonTaiTaiSanDongBo(khauHaoDBModel.MA_TAI_SAN_DB))
                {
                    ListError.Add($"MA_TAI_SAN_DB = {khauHaoDBModel.MA_TAI_SAN_DB} không tồn tại");
                }
            }
            if (khauHaoDBModel.NAM_KHAU_HAO == 0)
            {
                ListError.Add($"MA_TAI_SAN_DB = {khauHaoDBModel.MA_TAI_SAN_DB} - NAM_KHAU_HAO Bắt buộc >0");
            }
            //if (khauHaoDBModel.DB_ID == 0)
            //{
            //    ListError.Add($"NAM_KHAU_HAO = {khauHaoDBModel.NAM_KHAU_HAO}- DB_ID Bắt buộc >0");
            //}
            // check tháng năm khấu hao tài sản
            //if (khauHaoDBModel.ID == 0)
            //{
            TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(khauHaoDBModel.MA_TAI_SAN_DB, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
            if (taiSan != null)
            {
                khauHaoDBModel.MA_TAI_SAN = taiSan.MA;
                var khauHaoTaiSan = _khauHaoTaiSanService.GetListKhauHaoTaiSans(namKhauHao: khauHaoDBModel.NAM_KHAU_HAO, MaTaiSan: khauHaoDBModel.MA_TAI_SAN, thangKhauHao: khauHaoDBModel.THANG_KHAU_HAO).FirstOrDefault();
                if (khauHaoTaiSan != null)
                {
                    ListError.Add($"MA_TAI_SAN_DB = {khauHaoDBModel.MA_TAI_SAN_DB} -Tài sản đã có Khấu hao ở tháng  {khauHaoDBModel.THANG_KHAU_HAO} năm {khauHaoDBModel.NAM_KHAU_HAO } (Không được thêm mới)");
                }
            }
            //}
            //else
            //{
            //    HaoMonTaiSan khauHaoTaiSan = _haoMonTaiSanService.GetHaoMonTaiSanById(khauHaoDBModel.ID);
            //    if (khauHaoTaiSan == null)
            //    {
            //        ListError.Add($"NAM_KHAU_HAO = {khauHaoDBModel.NAM_KHAU_HAO}- ID Không tồn tại");
            //    }
            //}
            return ListError;
        }
        
        public List<string> CheckChiTietKhauHaoInTaiSan(KhauHaoInTaiSanDBModel khauHaoDBModel)
        {
            List<string> ListError = new List<string>();
            if (khauHaoDBModel.NAM_KHAU_HAO == 0)
            {
                ListError.Add($"NAM_KHAU_HAO Bắt buộc >0");
            }
            //if (khauHaoDBModel.DB_ID == 0)
            //{
            //    ListError.Add($"NAM_KHAU_HAO = {khauHaoDBModel.NAM_KHAU_HAO}- DB_ID Bắt buộc >0");
            //}
            // check tháng năm khấu hao tài sản
            //if (khauHaoDBModel.ID == 0)
            //{
            //    TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(khauHaoDBModel.MA_TAI_SAN_DB, NguonTaiSanId: (decimal)enumNguonTaiSan.CSDLQGTSC);
            //    if (taiSan != null)
            //    {
            //        khauHaoDBModel.MA_TAI_SAN = taiSan.MA;
            //        var khauHaoTaiSan = _khauHaoTaiSanService.GetListKhauHaoTaiSans(namKhauHao: khauHaoDBModel.NAM_KHAU_HAO, MaTaiSan: khauHaoDBModel.MA_TAI_SAN, thangKhauHao: khauHaoDBModel.THANG_KHAU_HAO).FirstOrDefault();
            //        if (khauHaoTaiSan != null)
            //        {
            //            ListError.Add($"NAM_KHAU_HAO = {khauHaoDBModel.NAM_KHAU_HAO}-Tài sản đã có Khấu hao ở tháng  {khauHaoDBModel.THANG_KHAU_HAO} năm {khauHaoDBModel.NAM_KHAU_HAO } (Không được thêm mới)");
            //        }
            //    }
            //}
            return ListError;
        }
        public List<string> CheckKhauHaoTaiSan(KhauHaoTaiSanDBModel khauHaoDBModel)
        {
            List<string> ListError = new List<string>();
            if (khauHaoDBModel.NAM_KHAU_HAO == 0)
            {
                ListError.Add($"NAM_KHAU_HAO Bắt buộc >0");
            }
            
            return ListError;
        }
        public string CheckDuAn(DuAnModel model)
        {
            if (model.ID > 0)
            {
                DuAn duAn = _duAnService.GetDuAnById(model.ID);
                if (duAn == null)
                    return $"DB_ID = {model.DB_ID} ID không tồn tại";
            }
            if (!string.IsNullOrEmpty(model.MA))
            {
                DuAn DuAn = _duAnService.GetDuAnByMA(model.MA);
                if (DuAn != null && model.ID != DuAn.ID)
                    return $"DB_ID = {model.DB_ID} MA đã tồn tại";
            }

            return null;
        }
        public string CheckLoaiLyDoBienDong(LoaiLyDoBienDongModel model)
        {
            string result = null;
            if (model.ID > 0)
            {
                LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(model.ID);
                if (loaiLyDo == null)
                    result = $"DB_ID = {model.DB_ID} ID không tồn tại";
            }
            if (!string.IsNullOrEmpty(model.MA))
            {
                LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongByMa(model.MA);
                if (loaiLyDo != null && model.ID != loaiLyDo.ID)
                    result = result + "; " + $"DB_ID = {model.DB_ID} MA đã tồn tại";
            }
            if (model.PARENT_ID > 0)
            {
                LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(model.PARENT_ID.Value);
                if (loaiLyDo == null)
                    result = result + "; " + $"DB_ID = {model.DB_ID} PARENT_ID không tồn tại";
            }
            return result;
        }
        public string CheckChiTietLyDoBienDong(LyDoBienDongModel model)
        {
            string result = null;
            if (model.ID > 0)
            {
                LoaiLyDoBienDong loaiLyDo = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(model.ID);
                if (loaiLyDo == null)
                    result = $"DB_ID = {model.DB_ID} ID không tồn tại; ";
            }
            if (!string.IsNullOrEmpty(model.MA))
            {
                LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByMa(model.MA);
                if (lyDoBienDong != null && model.ID != lyDoBienDong.ID)
                {
                    result = result + $"DB_ID = {model.DB_ID} MA đã tồn tại; ";
                }
            }

            return result;
        }
        public bool CheckPhuongAnXuLy(decimal? PhuongAnXuLyId)
        {
            if (PhuongAnXuLyId > 0)
            {
                PhuongAnXuLy phuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLyById(PhuongAnXuLyId.Value);
                if (phuongAnXuLy == null) return false;
            }
            return true;
        }

        public bool CheckListDonViBaoCao(List<Decimal> lstDonVi)
        {
            foreach (Decimal idDonVi in lstDonVi)
            {
                DonVi donVi = _donViService.GetDonViById(idDonVi);
                if (donVi == null)
                    return false;
            }
            return true;
        }
    }
}
