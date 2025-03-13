using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.WebApi.Factories;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.SoapServices
{
    public class DanhMucSvc : IDanhMucSvc
    {
        #region Ctor
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IHienTrangService _hienTrangService;
        private readonly INguonVonService _nguonVonService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly IDonViService _donViService;
        private readonly INhanXeService _nhanXeService;
        private readonly IDanhMucSvcModelFactory _danhMucSvcModelFactory;
        private readonly IDongXeService _dongXeService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDuAnService _duAnService;
        private readonly IWorkContext _workContext;
        private readonly IHinhThucXuLyService hinhThucXuLyService;
        public DanhMucSvc(
            IQuocGiaService quocGiaService,
            IHienTrangService hienTrangService,
            INguonVonService nguonVonService,
            ILoaiDonViService loaiDonViService,
            IDonViService donViService,
            INhanXeService nhanXeService,
            IDanhMucSvcModelFactory danhMucSvcModelFactory,
            IDongXeService dongXeService,
            ILyDoBienDongService lyDoBienDongService,
            IDiaBanService diaBanService,
            ILoaiTaiSanService loaiTaiSanService,
            IChucDanhService chucDanhService,
            IDuAnService duAnService,
            IWorkContext workContext,
            IPhuongAnXuLyService phuongAnXuLyService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IHinhThucXuLyService hinhThucXuLyService

            )
        {
            this._quocGiaService = quocGiaService;
            this._hienTrangService = hienTrangService;
            this._nguonVonService = nguonVonService;
            this._loaiDonViService = loaiDonViService;
            this._donViService = donViService;
            this._nhanXeService = nhanXeService;
            this._danhMucSvcModelFactory = danhMucSvcModelFactory;
            this._dongXeService = dongXeService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._diaBanService = diaBanService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._chucDanhService = chucDanhService;
            this._duAnService = duAnService;
            this._workContext = workContext;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this.hinhThucXuLyService = hinhThucXuLyService;
        }
        #endregion
        #region method
        public string Ping(string s)
        {
            return string.Format("Hello {0}", s);
        }        
        #region kiên
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            return _danhMucSvcModelFactory.GetAllQuocGias(); 
        }
        public MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateQuocGia(model, currentUser);
        }
        public MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateListQuocGia(ListQuocGiaModel, currentUser);
            // lọc các quốc gia không đủ điều kiện         
        }
        #endregion
        #region Hiện trạng
        public IList<HienTrangModel> GetAllHienTrangs()
        {
            return _danhMucSvcModelFactory.GetAllHienTrangs();
        }
        public MessageReturn UpDateHienTrang(HienTrangModel model, NguoiDung currentUser)
        {
            // kiểm tra forent key
            return _danhMucSvcModelFactory.UpDateHienTrang(model,  currentUser);
        }
        public MessageReturn UpDateListHienTrang(List<HienTrangModel> ListHienTrangModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateListHienTrang(ListHienTrangModel,  currentUser);
        }
        #endregion
        #region LoaiDonVi
        public IList<LoaiDonViModel> GetAllLoaiDonVis()
        {
            return _danhMucSvcModelFactory.GetAllLoaiDonVis();
        }
        public MessageReturn UpDateLoaiDonVi(LoaiDonViModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateLoaiDonVi(model,  currentUser);           
        }
        public MessageReturn UpDateListLoaiDonVi(List<LoaiDonViModel> ListLoaiDonViModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateListLoaiDonVi(ListLoaiDonViModel,  currentUser);          
        }
        #endregion
        #region NguonVon
        public IList<NguonVonModel> GetAllNguonVons()
        {
            return _danhMucSvcModelFactory.GetAllNguonVons();
        }
        public MessageReturn UpDateNguonVon(NguonVonModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateNguonVon(model,  currentUser);         
        }
        public MessageReturn UpdateListNguonVon(List<NguonVonModel> ListNguonVonModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateListNguonVon(ListNguonVonModel,  currentUser);         
        }
        #endregion
        #region NhanXe
        public IList<NhanXeModel> GetAllNhanXe()
        {
            return _danhMucSvcModelFactory.GetAllNhanXe();
        }
        public MessageReturn UpDateNhanXe(NhanXeModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateNhanXe(model,  currentUser);       
        }
        public MessageReturn UpDateListNhanXe(List<NhanXeModel> ListNhanXeModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpDateListNhanXe(ListNhanXeModel,  currentUser);          
        }
        #endregion
        #region DonVi
        public IList<DonViModel> GetAllDonVis()
        {
            List<DonViModel> ListModel = new List<DonViModel>();
            var query = _donViService.GetAllDonVis();
            foreach (var item in query)
            {
                var model = item.ToModel<DonViModel>();
                model.DB_LOAI_DON_VI_ID = item.LOAI_DON_VI_ID != null ? _loaiDonViService.GetLoaiDonViById(item.LOAI_DON_VI_ID.Value).DB_ID : null;
                model.MA_CHA = item.PARENT_ID != null ? _donViService.GetDonViById(item.PARENT_ID.Value).MA : null;
                ListModel.Add(model);
            }
            return ListModel;
        }
        public MessageReturn UpdateDonVi(DonViModel model, NguoiDung currentUser)
        {
            decimal? PARENT_ID = null;
            decimal? LOAI_DON_VI_ID = null;
            DonVi donViCha = null;
            // kiểm tra mã loại đơn vị
            if (model.DB_LOAI_DON_VI_ID>0)
            {
                var loaiDonVi = _loaiDonViService.GetLoaiDonViByIdDb((int)model.DB_LOAI_DON_VI_ID.Value);
                if (loaiDonVi == null)
                {
                    return MessageReturn.CreateErrorMessage("MA_LOAI_DON_VI invalid");
                }
                else
                {
                    LOAI_DON_VI_ID = loaiDonVi.ID;
                }
            }
            if (!string.IsNullOrEmpty(model.MA_CHA))
            {
                donViCha = _donViService.GetDonViByMa(model.MA_CHA);
                if (donViCha == null)
                {
                    return MessageReturn.CreateErrorMessage("MA_CHA invalid");
                }
                else
                {
                    PARENT_ID = donViCha.ID;
                }
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("MA_CHA invalid");
            }
            // MaDiaBan
            decimal? DIA_BAN_ID = null;
            if (!string.IsNullOrEmpty(model.MA_DIA_BAN))
            {
                DiaBan diaBan = _diaBanService.GetDiaBanByMa(model.MA_DIA_BAN);
                if (diaBan == null)
                {
                    return MessageReturn.CreateErrorMessage("MA_DIA_BAN invalid");
                }
                else
                {
                    DIA_BAN_ID = diaBan.ID;
                }
            }
            var donVi = _donViService.GetDonViByMa(model.MA);
            if (donVi == null)
            {
                donVi = model.ToEntity<DonVi>();
                donVi.PARENT_ID = PARENT_ID;
                donVi.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                donVi.DIA_BAN_ID = DIA_BAN_ID;
                donVi.ID = 0;
                donVi.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                _donViService.InsertDonVi(donVi);

            }
            else
            {
                donVi.TEN = model.TEN;
                //donVi.MA_BO = model.MA_BO;
                donVi.MA_DIA_BAN = model.MA_DIA_BAN;
                donVi.MA_DVQHNS = model.MA_DVQHNS;
                donVi.DIA_CHI = model.DIA_CHI;
                donVi.DIEN_THOAI = model.DIEN_THOAI;
                donVi.FAX = model.FAX;
                //donVi.MA_TINH = model.MA_TINH;
                //donVi.NHOM_DON_VI_ID = model.NHOM_DON_VI_ID;
                //donVi.CAP_DON_VI_ID = model.CAP_DON_VI_ID;
                //donVi.MA_HUYEN = model.MA_HUYEN;
                //donVi.CQTC_MA = model.CQTC_MA;
                donVi.CHE_DO_HACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                //donVi.SO_QUYET_DINH = model.SO_QUYET_DINH;
                //donVi.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
                //donVi.SO_QUYET_DINH_GIAO_VON = model.SO_QUYET_DINH_GIAO_VON;
                //donVi.NGAY_QUYET_DINH_GIAO_VON = model.NGAY_QUYET_DINH_GIAO_VON;
                donVi.PARENT_ID = PARENT_ID;
                donVi.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                donVi.DIA_BAN_ID = DIA_BAN_ID;
                donVi.TRANG_THAI_ID = model.TRANG_THAI_ID;
                //donVi.TREE_LEVEL = model.TREE_LEVEL;
                //donVi.DON_VI_HIEN_THI = model.DON_VI_HIEN_THI;
                //donVi.TRANG_THAI_THAY_DOI_ID = model.TRANG_THAI_DONG_BO_ID;
                //donVi.LA_DON_VI_NHAP_LIEU = model.LA_DON_VI_NHAP_LIEU;
                //donVi.TRANG_THAI_THAY_DOI_ID = model.TRANG_THAI_THAY_DOI_ID;
                //donVi.CO_TAI_SAN = model.CO_TAI_SAN;
                //donVi.KHONG_CHUYEN_MA = model.KHONG_CHUYEN_MA;
                //donVi.LA_BAN_QL_DU_AN = model.LA_BAN_QL_DU_AN;
                //donVi.LA_DON_VI_TU_CHU_TAI_CHINH = model.LA_DON_VI_TU_CHU_TAI_CHINH;
                //donVi.DA_CO_QUYET_DINH_GIAO_VON = model.DA_CO_QUYET_DINH_GIAO_VON;
                donVi.NGUOI_CAP_NHAT_ID = _workContext.CurrentCustomer.ID;
                _donViService.UpdateDonVi(donVi);
            }
            return MessageReturn.CreateSuccessMessage("Success done");

        }
        public MessageReturn UpdateListDonVi(List<DonViModel> ListDonViModel, NguoiDung currentUser)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<DonVi> LstAdd = new List<DonVi>();
            List<DonVi> LstEdit = new List<DonVi>();
            foreach (var model in ListDonViModel)
            {
                // kiểm tra dữ liệu
                if (string.IsNullOrEmpty(model.MA))
                {
                    err += "\nMA invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    err += $"\nID: {model.ID}\t\tTEN invalid";
                    TotalErr++;
                    continue;
                }
                // mã cha
                decimal? PARENT_ID = null;
                if (!string.IsNullOrEmpty(model.MA_CHA))
                {
                    var PARENT = _donViService.GetDonViByMa(model.MA_CHA);
                    if (PARENT != null)
                        PARENT_ID = PARENT.ID;
                    else
                    {
                        err += $"\nMA: {model.MA}\t\tMA_CHA invalid";
                        TotalErr++;
                        continue;
                    }
                }
                // MaDiaBan
                decimal? DIA_BAN_ID = null;
                if (!string.IsNullOrEmpty(model.MA_DIA_BAN))
                {
                    DiaBan diaBan = _diaBanService.GetDiaBanByMa(model.MA_DIA_BAN);
                    if (diaBan == null)
                    {
                        err += $"\nMA: {model.MA}\t\tMA_DIA_BAN invalid";
                        TotalErr++;
                        continue;
                    }
                    else
                    {
                        DIA_BAN_ID = diaBan.ID;
                    }
                }
                decimal? LOAI_DON_VI_ID = null;
                if (model.DB_LOAI_DON_VI_ID>0)
                {
                    LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViByIdDb((int)model.DB_LOAI_DON_VI_ID.Value);
                    if (loaiDonVi == null)
                    {
                        err += $"\nMA: {model.MA}\t\tMA_LOAI_DON_VI invalid";
                        TotalErr++;
                        continue;
                    }
                    else
                    {
                        LOAI_DON_VI_ID = loaiDonVi.ID;
                    }
                }
                var entity = _donViService.GetDonViByMa(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                   // entity.MA_BO = model.MA_BO;
                    entity.MA_DIA_BAN = model.MA_DIA_BAN;
                    entity.MA_DVQHNS = model.MA_DVQHNS;
                    entity.DIA_CHI = model.DIA_CHI;
                    entity.DIEN_THOAI = model.DIEN_THOAI;
                    entity.FAX = model.FAX;
                    //entity.MA_TINH = model.MA_TINH;
                    //entity.NHOM_DON_VI_ID = model.NHOM_DON_VI_ID;
                    //entity.CAP_DON_VI_ID = model.CAP_DON_VI_ID;
                    //entity.MA_HUYEN = model.MA_HUYEN;
                    //entity.CQTC_MA = model.CQTC_MA;
                    entity.CHE_DO_HACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                    //entity.SO_QUYET_DINH = model.SO_QUYET_DINH;
                    //entity.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
                    //entity.SO_QUYET_DINH_GIAO_VON = model.SO_QUYET_DINH_GIAO_VON;
                    //entity.NGAY_QUYET_DINH_GIAO_VON = model.NGAY_QUYET_DINH_GIAO_VON;
                    entity.PARENT_ID = PARENT_ID;
                    entity.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                    entity.DIA_BAN_ID = DIA_BAN_ID;
                    entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    //entity.TREE_LEVEL = model.TREE_LEVEL;
                    //entity.DON_VI_HIEN_THI = model.DON_VI_HIEN_THI;
                    //entity.TRANG_THAI_THAY_DOI_ID = model.TRANG_THAI_DONG_BO_ID;
                    //entity.LA_DON_VI_NHAP_LIEU = model.LA_DON_VI_NHAP_LIEU;
                    //entity.TRANG_THAI_THAY_DOI_ID = model.TRANG_THAI_THAY_DOI_ID;
                    //entity.CO_TAI_SAN = model.CO_TAI_SAN;
                    //entity.KHONG_CHUYEN_MA = model.KHONG_CHUYEN_MA;
                    //entity.LA_BAN_QL_DU_AN = model.LA_BAN_QL_DU_AN;
                    //entity.LA_DON_VI_TU_CHU_TAI_CHINH = model.LA_DON_VI_TU_CHU_TAI_CHINH;
                    //entity.DA_CO_QUYET_DINH_GIAO_VON = model.DA_CO_QUYET_DINH_GIAO_VON;
                    //entity.NGUOI_CAP_NHAT_ID = _workContext.CurrentCustomer.ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<DonVi>();
                    entity.ID = 0;
                    entity.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                    entity.PARENT_ID = PARENT_ID;
                    entity.DIA_BAN_ID = DIA_BAN_ID;
                    //entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    entity.NGAY_TAO = DateTime.Now;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _donViService.InsertListDonVi(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _donViService.UpdateListDonVi(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region Hinh thức xử lý
        public IList<PhuongAnXuLyModel> GetAllPhuongAnXuLys()
        {
            var query = _phuongAnXuLyService.GetAllPhuongAnXuLys();
            return query.Select(m => m.ToModel<PhuongAnXuLyModel>()).ToList();
        }
        public MessageReturn UpdatePhuongAnXuLy(PhuongAnXuLyModel model, NguoiDung CurrentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("MA invalid");
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("TEN invalid");
            }
            else
            {
                var PhuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.MA);
                if (PhuongAnXuLy != null)// cập nhật
                {
                    PhuongAnXuLy.TEN = model.TEN;
                    PhuongAnXuLy.SAP_XEP = model.SAP_XEP;
                    PhuongAnXuLy.CONFIG_CAU_HINH = model.CONFIG_CAU_HINH;
                    _phuongAnXuLyService.UpdatePhuongAnXuLy(PhuongAnXuLy);
                }
                else// không có thì thêm mới
                {
                    PhuongAnXuLy = model.ToEntity<PhuongAnXuLy>();
                    PhuongAnXuLy.ID = 0;
                    _phuongAnXuLyService.InsertPhuongAnXuLy(PhuongAnXuLy);
                }
                return MessageReturn.CreateSuccessMessage("Success done");
            }

        }
        public MessageReturn UpDateListPhuongAnXuLy(List<PhuongAnXuLyModel> ListPhuongAnXuLyModel, NguoiDung CurrentUser)
        {
            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<PhuongAnXuLy> LstAdd = new List<PhuongAnXuLy>();
            List<PhuongAnXuLy> LstEdit = new List<PhuongAnXuLy>();
            foreach (var model in ListPhuongAnXuLyModel)
            {
                if (string.IsNullOrEmpty(model.MA))
                {
                    err += "\nMA invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    err += $"\nMA: {model.MA}\t\tTEN invalid";
                    TotalErr++;
                    continue;
                }
                var entity = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.SAP_XEP = model.SAP_XEP;
                    entity.CONFIG_CAU_HINH = model.CONFIG_CAU_HINH;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<PhuongAnXuLy>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _phuongAnXuLyService.InsertListPhuongAnXuLy(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _phuongAnXuLyService.UpdateListPhuongAnXuLy(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region Nguồn gốc tài sản
        public IList<QuocGiaModel> GetAllNgupnGocTaiSans()
        {
            var query = _quocGiaService.GetAllQuocGias();
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public MessageReturn UpdateNguonGocTaiSan(NguonGocTaiSanModel model, NguoiDung CurrentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("MA invalid");
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("TEN invalid");
            }
            else
            {
                var NguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(model.MA);
                if (NguonGocTaiSan != null)// cập nhật
                {
                    NguonGocTaiSan.TEN = model.TEN;
                    _nguonGocTaiSanService.UpdateNguonGocTaiSan(NguonGocTaiSan);
                }
                else// không có thì thêm mới
                {
                    NguonGocTaiSan = model.ToEntity<NguonGocTaiSan>();
                    NguonGocTaiSan.ID = 0;
                    _nguonGocTaiSanService.InsertNguonGocTaiSan(NguonGocTaiSan);
                }
                return MessageReturn.CreateSuccessMessage("Success done");
            }
        }
        public MessageReturn UpDateListNguonGocTaiSan(List<NguonGocTaiSanModel> ListNguonGocTaiSanModel, NguoiDung CurrentUser)
        {
            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<NguonGocTaiSan> LstAdd = new List<NguonGocTaiSan>();
            List<NguonGocTaiSan> LstEdit = new List<NguonGocTaiSan>();
            foreach (var model in ListNguonGocTaiSanModel)
            {
                if (string.IsNullOrEmpty(model.MA))
                {
                    err += "\nMA invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    err += $"\nMA: {model.MA}\t\tTEN invalid";
                    TotalErr++;
                    continue;
                }
                var entity = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<NguonGocTaiSan>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _nguonGocTaiSanService.InsertListNguonGocTaiSan(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _nguonGocTaiSanService.UpdateListNguonGocTaiSan(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region Phương án xử lý
        //public IList<HinhThucXuLyModel> GetAllHinhThucXuLys()
        //{
        //    var query = hinhThucXuLyService.GetAllHinhThucXuLys();
        //    return query.Select(m => new HinhThucXuLyModel()
        //    {
        //        TEN = m.TEN,
        //        MA = m.MA,
        //        MA_HINH_THUC_XU_LY = m.HINH_THUC_XU_LY_ID != null ? _phuongAnXuLyService.GetPhuongAnXuLyById(m.HINH_THUC_XU_LY_ID.Value).MA : null,
        //        SAP_XEP = m.SAP_XEP
        //    }).ToList();
        //}
        //public MessageReturn UpdateHinhThucXuLy(HinhThucXuLyModel model, NguoiDung CurrentUser)
        //{
        //    if (string.IsNullOrEmpty(model.MA))
        //    {
        //        return MessageReturn.CreateErrorMessage("MA invalid");
        //    }
        //    if (string.IsNullOrEmpty(model.TEN))
        //    {
        //        return MessageReturn.CreateErrorMessage("TEN invalid");
        //    }
        //    // hình thức xử lys
        //    decimal? PhuongAnXuLyId = null;
        //    if (!string.IsNullOrEmpty(model.MA_HINH_THUC_XU_LY))
        //    {
        //        PhuongAnXuLy phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.MA_HINH_THUC_XU_LY);
        //        if (phuongAn_XyLy != null)
        //        {
        //            PhuongAnXuLyId = phuongAn_XyLy.ID;
        //        }
        //        else
        //        {
        //            return MessageReturn.CreateErrorMessage("MA_HINH_THUC_XU_LY invalid");
        //        }
        //    }


        //    var HinhThucXuLy = hinhThucXuLyService.GetHinhThucXuLyByMa(model.MA);
        //    if (HinhThucXuLy != null)// cập nhật
        //    {
        //        HinhThucXuLy.TEN = model.TEN;
        //        HinhThucXuLy.SAP_XEP = model.SAP_XEP;
        //        HinhThucXuLy.HINH_THUC_XU_LY_ID = PhuongAnXuLyId;

        //        hinhThucXuLyService.UpdateHinhThucXuLy(HinhThucXuLy);
        //    }
        //    else// không có thì thêm mới
        //    {
        //        HinhThucXuLy = model.ToEntity<HinhThucXuLy>();
        //        HinhThucXuLy.ID = 0;
        //        HinhThucXuLy.HINH_THUC_XU_LY_ID = PhuongAnXuLyId;
        //        hinhThucXuLyService.InsertHinhThucXuLy(HinhThucXuLy);
        //    }
        //    return MessageReturn.CreateSuccessMessage("Success done");


        //}
        public MessageReturn UpDateListHinhThucXuLy(List<HinhThucXuLyModel> ListHinhThucXuLyModel, NguoiDung CurrentUser)
        {
            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<HinhThucXuLy> LstAdd = new List<HinhThucXuLy>();
            List<HinhThucXuLy> LstEdit = new List<HinhThucXuLy>();
            foreach (var model in ListHinhThucXuLyModel)
            {
                if (string.IsNullOrEmpty(model.MA))
                {
                    err += "\nMA invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    err += $"\nMA: {model.MA}\t\tTEN invalid";
                    TotalErr++;
                    continue;
                }
                decimal? PhuongAnXuLyId = null;
                //if (!string.IsNullOrEmpty(model.MA_HINH_THUC_XU_LY))
                //{
                //    PhuongAnXuLy phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.MA_HINH_THUC_XU_LY);
                //    if (phuongAn_XyLy != null)
                //    {
                //        PhuongAnXuLyId = phuongAn_XyLy.ID;
                //    }
                //    else
                //    {
                //        err += $"\nMA: {model.MA}\t\tMA_HINH_THUC_XU_LY invalid";
                //        TotalErr++;
                //        continue;
                //    }
                //}
                var entity = hinhThucXuLyService.GetHinhThucXuLyByMa(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.SAP_XEP = model.SAP_XEP;
                 //   entity.HINH_THUC_XU_LY_ID = PhuongAnXuLyId;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<HinhThucXuLy>();
                    entity.ID = 0;
                   // entity.HINH_THUC_XU_LY_ID = PhuongAnXuLyId;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                hinhThucXuLyService.InsertListHinhThucXuLy(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                hinhThucXuLyService.UpdateListHinhThucXuLy(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion

        #endregion   
        #region tuấn anh
        #region DongXe
        public IList<DongXeModel> GetAllDongXes()
        {
            return _danhMucSvcModelFactory.GetAllDongXes();
        }
        public MessageReturn UpdateDongXe(DongXeModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDongXe(model,  currentUser);       
        }
        public MessageReturn UpdateDongXes(List<DongXeModel> ListModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDongXes(ListModel,  currentUser);          
        }
        #endregion
        #region LyDoBienDong
        public IList<LyDoBienDongModel> GetAllLyDoBienDongs()
        {
            return _danhMucSvcModelFactory.GetAllLyDoBienDongs();       
        }
        public MessageReturn UpdateLyDoBienDong(LyDoBienDongModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateLyDoBienDong(model,  currentUser);
        }
        public MessageReturn UpdateListLyDoBienDong(List<LyDoBienDongModel> ListModel, NguoiDung currentUser)
        {
            var result=  _danhMucSvcModelFactory.UpdateListLyDoBienDong(ListModel,  currentUser);
            return result;
        }
       // public MessageReturn DeleteLyDoBienDong(decimal)
        #endregion
        #region DiaBan
        public IList<DiaBanModel> GetAllDiaBans()
        {
            return _danhMucSvcModelFactory.GetAllDiaBans();           
        }
        public MessageReturn UpdateDiaBan(DiaBanModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDiaBan(model,  currentUser);         
        }
        public MessageReturn UpdateDiaBans(List<DiaBanModel> ListModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDiaBans(ListModel,currentUser);
        }
        #endregion
        #region LoaiTaiSan
        public IList<LoaiTaiSanModel> GetAllLoaiTaiSanNhaNuocs()
        {
            return _danhMucSvcModelFactory.GetAllLoaiTaiSanNhaNuocs();

        }
        public MessageReturn UpdateLoaiTaiSan(LoaiTaiSanModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateLoaiTaiSan(model, currentUser);
        }
        public MessageReturn UpdateLoaiTaiSans(List<LoaiTaiSanModel> ListModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateListLoaiTaiSan(ListModel, currentUser);
        }
        #endregion
        #region ChucDanh
        public IList<ChucDanhModel> GetAllChucDanhs()
        {
            return _danhMucSvcModelFactory.GetAllChucDanhs();            
        }
        public MessageReturn UpdateChucDanh(ChucDanhModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateChucDanh(model,  currentUser);          
        }
        public MessageReturn UpdateChucDanhs(List<ChucDanhModel> ListModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateChucDanhs(ListModel,  currentUser);
           
        }
        #endregion
        #region DuAn
        public IList<DuAnModel> GetAllDuAns()
        {
            return _danhMucSvcModelFactory.GetAllDuAns();
        }
        public MessageReturn UpdateDuAn(DuAnModel model, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDuAn(model,  currentUser);
        }
        public MessageReturn UpdateDuAns(List<DuAnModel> ListModel, NguoiDung currentUser)
        {
            return _danhMucSvcModelFactory.UpdateDuAns(ListModel,  currentUser);
        }
        #endregion
        #endregion
        #endregion
    }
}
