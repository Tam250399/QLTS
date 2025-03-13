using System;
using System.Collections.Generic;
using System.Linq;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Configuration;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DM;
using GS.Core.Domain.DMDC;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.DMDC;
using GS.Services.HeThong;
using GS.WebApi.Common;
using GS.WebApi.Infrastructure.Cache;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.DMDC;
using Newtonsoft.Json;
using static iTextSharp.text.pdf.AcroFields;

namespace GS.WebApi.Factories
{
    /// <summary>
    /// Represents the country model factory
    /// </summary>
    public partial class DanhMucSvcModelFactory : IDanhMucSvcModelFactory
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
        // private readonly IDanhMucFactory _danhMucModelFactory;
        private readonly IDongXeService _dongXeService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDuAnService _duAnService;
        private readonly IWorkContext _workContext;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly IHoatDongService _hoatDongService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IDMDC_DiaBanService _dMDC_DiaBanService;
        private readonly IDMDC_DonViDuAnService _dMDC_DonViDuAnService;
        private readonly IDMDC_DonViNganSachService _dMDC_DonViNganSachService;
        private readonly IDMDC_DuAnService _dMDC_DuAnService;
        private readonly IDMDC_QuocGiaService _dMDC_QuocGiaService;
        private readonly IDMDC_ToChucNganSachService _dMDC_ToChucNganSachService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ILoaiLyDoBienDongService _loaiLyDoBienDongService;
        private readonly GSConfig _gSConfig;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        public DanhMucSvcModelFactory(
            IQuocGiaService quocGiaService,
            IHienTrangService hienTrangService,
            INguonVonService nguonVonService,
            ILoaiDonViService loaiDonViService,
            IDonViService donViService,
            INhanXeService nhanXeService,
            //IDanhMucFactory danhMucModelFactory,
            IDongXeService dongXeService,
            ILyDoBienDongService lyDoBienDongService,
            IDiaBanService diaBanService,
            ILoaiTaiSanService loaiTaiSanService,
            IChucDanhService chucDanhService,
            IDuAnService duAnService,
            IWorkContext workContext,
            IPhuongAnXuLyService phuongAnXuLyService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IHinhThucXuLyService hinhThucXuLyService,
            IMucDichSuDungService mucDichSuDungService,
            INguoiDungService nguoiDungService,
            INguoiDungDonViService nguoiDungDonViService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            IHoatDongService hoatDongService,
            ICheDoHaoMonService cheDoHaoMonService,
            IDMDC_DiaBanService dMDC_DiaBanService,
            IDMDC_DonViDuAnService dMDC_DonViDuAnService,
            IDMDC_DonViNganSachService dMDC_DonViNganSachService,
            IDMDC_DuAnService dMDC_DuAnService,
            IDMDC_QuocGiaService dMDC_QuocGiaService,
            IDMDC_ToChucNganSachService dMDC_ToChucNganSachService,
            IDonViBoPhanService donViBoPhanService,
            ILoaiLyDoBienDongService loaiLyDoBienDongService,
            GSConfig gSConfig,
            IHinhThucMuaSamService hinhThucMuaSamService

            )
        {
            this._quocGiaService = quocGiaService;
            this._hienTrangService = hienTrangService;
            this._nguonVonService = nguonVonService;
            this._loaiDonViService = loaiDonViService;
            this._donViService = donViService;
            this._nhanXeService = nhanXeService;
            //this._danhMucModelFactory = danhMucModelFactory;
            this._dongXeService = dongXeService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._diaBanService = diaBanService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._chucDanhService = chucDanhService;
            this._duAnService = duAnService;
            this._workContext = workContext;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._hinhThucXuLyService = hinhThucXuLyService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._nguoiDungService = nguoiDungService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._hoatDongService = hoatDongService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._dMDC_DiaBanService = dMDC_DiaBanService;
            this._dMDC_DonViDuAnService = dMDC_DonViDuAnService;
            this._dMDC_DonViNganSachService = dMDC_DonViNganSachService;
            this._dMDC_DuAnService = dMDC_DuAnService;
            this._dMDC_QuocGiaService = dMDC_QuocGiaService;
            this._dMDC_ToChucNganSachService = dMDC_ToChucNganSachService;
            this._donViBoPhanService = donViBoPhanService;
            this._loaiLyDoBienDongService = loaiLyDoBienDongService;
            this._gSConfig = gSConfig;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
        }
        #endregion
        #region method
        public string Ping(string s)
        {
            return string.Format("Hello {0}", s);
        }
        //public IList<NhanXeModel> GetNhanXe()
        //{
        //    var items = _danhMucModelFactory.GetAllNhanXe();
        //    return items;
        //}
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            var query = _quocGiaService.GetAllQuocGias();
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<QuocGiaModel>() { model });
            }
            if (model.DB_ID == null)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<QuocGiaModel>() { model });
            }
            else
            {
                QuocGia quocGia = new QuocGia();
                if (model.ID == 0)
                {
                    quocGia = model.ToEntity<QuocGia>();
                    quocGia.ID = 0;
                    //quocGia.MA = null;
                    _quocGiaService.InsertQuocGia(quocGia);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", model);
                    model.ID = (long)quocGia.ID;
                    return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
                }
                else
                {
                    quocGia = _quocGiaService.GetQuocGiaDB(ID: model.ID);
                    if (quocGia != null)// cập nhật
                    {
                        quocGia.TEN = model.TEN;
                        quocGia.MO_TA = model.MO_TA;
                        _quocGiaService.UpdateQuocGia(quocGia);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", model);
                        return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
                    }
                    else
                    {
                        model.Error = "ID not exist";
                        return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<QuocGiaModel>() { model });
                    }
                }
            }

        }
        public MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            // lọc các quốc gia không đủ điều kiện           
            int TotalErr = 0;
            int TotalSuc = 0;
            List<QuocGia> LstAdd = new List<QuocGia>();
            List<QuocGia> LstEdit = new List<QuocGia>();
            List<QuocGia> quocGias = new List<QuocGia>();
            foreach (var model in ListQuocGiaModel)
            {
                if (model.DB_ID == null)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                if (model.ID > 0)
                {
                    var entity = _quocGiaService.GetQuocGiaById(model.ID);
                    if (entity == null)
                    {
                        model.Error = "ID not exist";
                        continue;
                    }
                    else
                    {
                        //entity = model.ToEntity<QuocGia>();
                        entity.TEN = model.TEN;
                        entity.MA = model.MA;
                        entity.DB_ID = model.DB_ID;
                        LstEdit.Add(entity);
                    }
                }
                else
                {
                    var entity = model.ToEntity<QuocGia>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _quocGiaService.InsertListQuocGia(LstAdd);
                if (currentUser == null)
                {
                    currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
                }
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", LstAdd);
                quocGias.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _quocGiaService.UpdateListQuocGia(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", LstEdit);
                quocGias.AddRange(LstEdit);
            }
            foreach (var item in ListQuocGiaModel)
            {
                var quocgia = quocGias.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (quocgia == null)
                    continue;
                item.ID = (long)quocgia.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {quocGias.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListQuocGiaModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    ObjectInfo = quocGias,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn DeleteQuocGia(decimal ID = 0)
        {
            QuocGia quocGia = _quocGiaService.GetQuocGiaById(Id: ID);
            try
            {
                if (quocGia.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                quocGia.DB_ID = null;
                _quocGiaService.UpdateQuocGia(quocGia);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }

        }
        #endregion
        #region Hiện trạng
        public IList<HienTrangModel> GetAllHienTrangs()
        {
            var query = _hienTrangService.GetHienTrangs();
            return query.Select(m => new HienTrangModel()
            {
                ID = (int)m.ID,
                TEN_HIEN_TRANG = m.TEN_HIEN_TRANG,
                LOAI_HINH_TAI_SAN_ID = m.LOAI_HINH_TAI_SAN_ID,
                DB_ID = (int?)m.DB_ID,
                NHOM_HIEN_TRANG_ID = m.NHOM_HIEN_TRANG_ID
            }).ToList();
        }
        public MessageReturn UpDateHienTrang(HienTrangModel model, NguoiDung currentUser)
        {
            // kiểm tra forent key
            if (model.LOAI_HINH_TAI_SAN_ID == 0)
            {
                model.Error = "LOAI_HINH_TAI_SAN_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_HINH_TAI_SAN_ID null", new List<HienTrangModel>() { model });
            };
            if (model.DB_ID == null || model.DB_ID == 0)
            {
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<HienTrangModel>() { model });
            };
            HienTrang hienTrang = new HienTrang();
            if (model.ID == 0)
            {
                if (model.KIEU_DU_LIEU_ID == null)
                {
                    model.KIEU_DU_LIEU_ID = (decimal)Models.DanhMuc.enumKIEU_DU_LIEU.CheckBox;
                }
                hienTrang = model.ToEntity<HienTrang>();
                _hienTrangService.InsertHienTrang(hienTrang);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới hiện trạng", 0, "HienTrang", hienTrang);
            }
            else
            {
                hienTrang = _hienTrangService.GetHienTrangById(model.ID);
                if (hienTrang != null)
                {
                    hienTrang.TEN_HIEN_TRANG = model.TEN_HIEN_TRANG;
                    hienTrang.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
                    hienTrang.KIEU_DU_LIEU_ID = model.KIEU_DU_LIEU_ID;
                    _hienTrangService.UpdateHienTrang(hienTrang);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hiện trạng", 0, "HienTrang", hienTrang);
                }
                else
                {
                    return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<HienTrangModel>() { model });
                }
            }
            model.ID = (long)hienTrang.ID;
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<HienTrangModel>() { model });
        }
        public MessageReturn UpDateListHienTrang(List<HienTrangModel> ListHienTrangModel, NguoiDung currentUser)
        {

            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            string succ = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<HienTrang> LstAdd = new List<HienTrang>();
            List<HienTrang> LstEdit = new List<HienTrang>();
            List<HienTrang> hienTrangs = new List<HienTrang>();
            foreach (var model in ListHienTrangModel)
            {
                if (model.DB_ID == null || model.DB_ID == 0)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                };
                if (string.IsNullOrEmpty(model.TEN_HIEN_TRANG))
                {
                    model.Error += "TEN_HIEN_TRANG null";
                    TotalErr++;
                    continue;
                }
                if (model.KIEU_DU_LIEU_ID == null)
                {
                    model.KIEU_DU_LIEU_ID = (decimal)Models.DanhMuc.enumKIEU_DU_LIEU.CheckBox;
                }
                var entity = _hienTrangService.GetHienTrangById(model.ID);
                if (entity != null)
                {
                    entity.TEN_HIEN_TRANG = model.TEN_HIEN_TRANG;
                    entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
                    entity.KIEU_DU_LIEU_ID = model.KIEU_DU_LIEU_ID;
                    LstEdit.Add(entity);
                }
                else
                {

                    if (model.ID > 0)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    entity = model.ToEntity<HienTrang>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _hienTrangService.InsertListHienTrang(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Hiện trạng", 0, "HienTrang", LstAdd);
                hienTrangs.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hienTrangService.UpdateListHienTrang(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hiện trạng", 0, "HienTrang", LstEdit);
                hienTrangs.AddRange(LstEdit);
            }
            foreach (var item in ListHienTrangModel)
            {
                var HienTrang = hienTrangs.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (HienTrang == null)
                    continue;
                item.ID = (long)HienTrang.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {hienTrangs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListHienTrangModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListHienTrangModel
                };
            }

        }
        public MessageReturn DeleteHienTrang(decimal ID = 0)
        {

            HienTrang hienTrang = _hienTrangService.GetHienTrangById(Id: ID);
            try
            {
                if (hienTrang.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                hienTrang.DB_ID = null;
                _hienTrangService.UpdateHienTrang(hienTrang);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }
        }
        #endregion
        #region LoaiDonVi
        public IList<LoaiDonViModel> GetAllLoaiDonVis()
        {
            var query = _loaiDonViService.GetAllLoaiDonVis();
            return query.Select(m => new LoaiDonViModel()
            {
                ID = Convert.ToInt64(m.ID),
                CHE_DO_HACH_TOAN_ID = m.CHE_DO_HOACH_TOAN_ID,
                MA = m.MA,
                //ID_CHA = m.PARENT_ID != null ? _loaiDonViService.GetLoaiDonViById(m.PARENT_ID.Value).ID_DB : null,
                MA_CHA = m.PARENT_ID != null ? _loaiDonViService.GetLoaiDonViById(m.PARENT_ID.Value).MA : null,
                TEN = m.TEN,
                DB_ID = (int?)m.DB_ID
            }).ToList();
        }
        public MessageReturn UpDateLoaiDonVi(LoaiDonViModel model, NguoiDung currentUser)
        {
            decimal? PARENT_ID = null;
            LoaiDonVi loaiDonViCha = null;
            if (model.DB_ID == null || model.DB_ID == 0)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<LoaiDonViModel>() { model });
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<LoaiDonViModel>() { model });
            }
            // kiểm tra forent key
            if (model.DB_PARENT_ID != null && model.DB_PARENT_ID > 0)
            {
                loaiDonViCha = _loaiDonViService.GetLoaiDonViByIdDb(model.DB_PARENT_ID.Value);
                if (loaiDonViCha == null)
                {
                    model.Error = "DB_PARENT_ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "DB_PARENT_ID not exist", new List<LoaiDonViModel>() { model });
                }
                else
                {
                    PARENT_ID = loaiDonViCha.ID;
                }
            }
            LoaiDonVi loaiDonVi = new LoaiDonVi();
            if (model.ID == 0)
            {
                loaiDonVi = model.ToEntity<LoaiDonVi>();
                loaiDonVi.PARENT_ID = PARENT_ID;
                loaiDonVi.CHE_DO_HOACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                _loaiDonViService.InsertLoaiDonVi(loaiDonVi);
                model.ID = (long)loaiDonVi.ID;
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới loại đơn vị", 0, "LoaiDonVi", loaiDonVi);

            }
            else
            {
                loaiDonVi = _loaiDonViService.GetLoaiDonViById(model.ID);
                if (loaiDonVi == null)
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<LoaiDonViModel>() { model });
                }
                else
                {
                    loaiDonVi.PARENT_ID = PARENT_ID;
                    loaiDonVi.CHE_DO_HOACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                    loaiDonVi.TEN = model.TEN;
                    _loaiDonViService.UpdateLoaiDonVi(loaiDonVi);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật loại đơn vị", 0, "LoaiDonVi", loaiDonVi);
                }
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<LoaiDonViModel>() { model });
        }
        public MessageReturn UpDateListLoaiDonVi(List<LoaiDonViModel> ListLoaiDonViModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<LoaiDonVi> LstAdd = new List<LoaiDonVi>();
            List<LoaiDonVi> LstEdit = new List<LoaiDonVi>();
            List<LoaiDonVi> loaiDonVis = new List<LoaiDonVi>();
            foreach (var model in ListLoaiDonViModel)
            {
                if (model.DB_ID == null || model.DB_ID == 0)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                decimal? PARENT_ID = null;
                if (model.DB_PARENT_ID != null && model.DB_PARENT_ID > 0)
                {
                    var PARENT = _loaiDonViService.GetLoaiDonViByIdDb(model.DB_PARENT_ID.Value);
                    if (PARENT != null)
                        PARENT_ID = PARENT.ID;
                    else
                    {
                        model.Error = "DB_PARENT_ID not exist";
                        TotalErr++;
                        continue;
                    }
                }
                var entity = _loaiDonViService.GetLoaiDonViById(model.ID);
                if (entity != null)
                {
                    entity = model.ToEntity<LoaiDonVi>();
                    entity.TEN = model.TEN;
                    entity.PARENT_ID = PARENT_ID;
                    entity.CHE_DO_HOACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    if (model.ID > 0)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    entity = model.ToEntity<LoaiDonVi>();
                    entity.ID = 0;
                    entity.PARENT_ID = PARENT_ID;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _loaiDonViService.InsertListLoaiDonVi(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới loại đơn vị", 0, "LoaiDonVi", LstAdd);
                loaiDonVis.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _loaiDonViService.UpdateListLoaiDonVi(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật loại đơn vị", 0, "LoaiDonVi", LstEdit);
                loaiDonVis.AddRange(LstEdit);
            }
            foreach (var item in ListLoaiDonViModel)
            {
                var loaiDonVi = loaiDonVis.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (loaiDonVi == null)
                {
                    continue;
                }
                else
                {
                    item.ID = (long)loaiDonVi.ID;
                }
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {loaiDonVis.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListLoaiDonViModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = $"Total {loaiDonVis.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListLoaiDonViModel
                };
            }
        }
        public MessageReturn DeleteLoaiDonVi(decimal ID = 0)
        {
            LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViById(Id: ID);
            try
            {
                if (loaiDonVi.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                loaiDonVi.DB_ID = null;
                _loaiDonViService.UpdateLoaiDonVi(loaiDonVi);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }
        }
        #endregion
        #region NguonVon
        public IList<NguonVonModel> GetAllNguonVons()
        {
            var query = _nguonVonService.GetAllNguonVon();
            return query.Select(m => m.ToModel<NguonVonModel>()).ToList();
        }
        public MessageReturn UpDateNguonVon(NguonVonModel model, NguoiDung currentUser)
        {
            if (!string.IsNullOrEmpty(model.TEN))
            {
                return MessageReturn.CreateNotFoundMessage("TEN invalid");
            }
            else
            {
                var NguonVon = _nguonVonService.GetNguonVonById(model.ID);
                if (NguonVon == null)
                {
                    NguonVon = new NguonVon();
                    NguonVon = model.ToEntity<NguonVon>();
                    NguonVon.ID = 0;
                    _nguonVonService.InsertNguonVon(NguonVon);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới Nguồn vốn", 0, "NguonVon", NguonVon);
                }
                else
                {
                    NguonVon.TEN = model.TEN;
                    _nguonVonService.UpdateNguonVon(NguonVon);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn vốn", 0, "NguonVon", NguonVon);
                }
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", NguonVon.ID.ToString());
            }
        }
        public MessageReturn UpdateListNguonVon(List<NguonVonModel> ListNguonVonModel, NguoiDung currentUser)
        {
            string err = "";
            string succ = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<NguonVon> LstAdd = new List<NguonVon>();
            List<NguonVon> LstEdit = new List<NguonVon>();
            foreach (var model in ListNguonVonModel)
            {
                if (model.ID == 0)
                {
                    err += "\nID invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    err += $"\nID: {model.ID}\t\tTEN invalid";
                    TotalErr++;
                    continue;
                }
                var entity = _nguonVonService.GetNguonVonById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.GHI_CHU = model.GHI_CHU;
                    entity.AP_DUNG_ID = model.AP_DUNG_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<NguonVon>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                    var ListObjectAdd = LstAdd.Select(m => new
                    {
                        ID = model.ID,
                        ID_DONG_BO = m.ID,
                        TEN = m.TEN
                    });
                }
            }

            if (LstAdd.Count > 0)
            {
                _nguonVonService.InsertListNguonVon(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới Nguồn vốn", 0, "NguonVon", LstAdd);
                TotalSuc += LstAdd.Count();
                var ListObjectAdd = LstAdd.Select(m => new
                {
                    ID = ListNguonVonModel.Where(c => c.TEN == m.TEN).FirstOrDefault().ID,
                    ID_DONG_BO = m.ID,
                    TEN = m.TEN
                });
                succ = ListObjectAdd.toStringJson();
            }
            if (LstEdit.Count > 0)
            {
                _nguonVonService.UpdateListNguonVon(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn vốn", 0, "NguonVon", LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""), succ);
        }
        #endregion
        #region NhanXe
        public IList<NhanXeModel> GetAllNhanXe()
        {
            var query = _nhanXeService.GetAllNhanXes();
            return query.Select(m => m.ToModel<NhanXeModel>()).ToList();
        }
        public MessageReturn UpDateNhanXe(NhanXeModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("MA invalid");
            }
            else if (string.IsNullOrEmpty(model.TEN))
            {
                return MessageReturn.CreateErrorMessage("TEN invalid");
            }
            else
            {
                var NhanXe = _nhanXeService.GetNhanXeByMaTen(model.MA);
                if (NhanXe == null)
                {
                    NhanXe = model.ToEntity<NhanXe>();
                    NhanXe.ID = 0;
                    _nhanXeService.InsertNhanXe(NhanXe);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới nhãn xe", 0, "NhanXe", NhanXe);
                }
                else
                {
                    NhanXe.MA = model.MA;
                    NhanXe.TEN = model.TEN;
                    _nhanXeService.UpdateNhanXe(NhanXe);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nhãn xe", 0, "NhanXe", NhanXe);
                }
                return MessageReturn.CreateSuccessMessage("Success done");
            }

        }
        public MessageReturn UpDateListNhanXe(List<NhanXeModel> ListNhanXeModel, NguoiDung currentUser)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<NhanXe> LstAdd = new List<NhanXe>();
            List<NhanXe> LstEdit = new List<NhanXe>();
            foreach (var model in ListNhanXeModel)
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
                var entity = _nhanXeService.GetNhanXeByMaTen(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<NhanXe>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _nhanXeService.InsertListNhanXe(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới Nhãn xe", 0, "NhanXe", LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _nhanXeService.UpdateListNhanXe(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nhãn xe", 0, "NhanXe", LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region DonVi
        public ResultDonVi GetAllDonVis(int pageSize = int.MaxValue, int pageIndex = 0)
        {
            List<DonViModel> ListModel = new List<DonViModel>();
            ResultDonVi resultDonVi = new ResultDonVi();
            var query = _donViService.SearchDonVis(pageIndex: pageIndex, pageSize: pageSize);
            foreach (var item in query)
            {
                if (item.CAP_DON_VI_ID == null)
                    continue;
                DonViModel model = item.ToModel<DonViModel>();
                model.ID = Convert.ToInt64(item.ID);
                model.MA_CAP_DON_VI = MapCapDonVi(item.CAP_DON_VI_ID != null ? item.CAP_DON_VI_ID.Value : 0);
                model.MA_CHA = item.DonViCha != null ? item.DonViCha.MA : null;
                // model.MA_LOAI_DON_VI = item.LOAI_DON_VI_ID != null ? _donViService.GetDonViById(item.LOAI_DON_VI_ID.Value).MA : null;
                ListModel.Add(model);
            }
            resultDonVi.ListDonVi = ListModel;
            resultDonVi.Total = query.TotalCount;
            resultDonVi.TotalPage = query.TotalPages;
            return resultDonVi;
        }
        public MessageReturn UpdateDonVi(DonViModel model, NguoiDung currentUser)
        {
            decimal? PARENT_ID = null;
            decimal? LOAI_DON_VI_ID = null;
            DonVi donViCha = null;
            // kiểm tra mã loại đơn vị
            if (model.DB_LOAI_DON_VI_ID > 0)
            {
                var loaiDonVi = _loaiDonViService.GetLoaiDonViByIdDb((int)model.DB_LOAI_DON_VI_ID.Value);
                if (loaiDonVi == null)
                {
                    model.Error = "DB_LOAI_DON_VI_ID not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_LOAI_DON_VI_ID not exist", new List<DonViModel>() { model });
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
                    model.Error = "MA_CHA not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_CHA not exist", new List<DonViModel>() { model });
                }
                else
                {
                    PARENT_ID = donViCha.ID;
                }
            }
            if (string.IsNullOrEmpty(model.MA) && string.IsNullOrEmpty(model.MA_DVQHNS))
            {
                model.Error = "MA-MA_DVQHNS null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA-MA_DVQHNS null", new List<DonViModel>() { model });
            }
            // MaDiaBan
            decimal? DIA_BAN_ID = null;
            if (!string.IsNullOrEmpty(model.MA_DIA_BAN))
            {
                DiaBan diaBan = _diaBanService.GetDiaBanByMa(model.MA_DIA_BAN);
                if (diaBan == null)
                {
                    model.Error = "MA_DIA_BAN null";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_DIA_BAN null", new List<DonViModel>() { model });

                }
                else
                {
                    DIA_BAN_ID = diaBan.ID;
                }
            }
            var donVi = _donViService.GetDonViById(model.ID);
            if (donVi == null)
            {
                donVi = model.ToEntity<DonVi>();
                donVi.PARENT_ID = PARENT_ID;
                donVi.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                donVi.DIA_BAN_ID = DIA_BAN_ID;
                donVi.ID = 0;
                donVi.NGUOI_CAP_NHAT_ID = null;
                _donViService.InsertDonVi(donVi);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Tạo mới đơn vị", 0, "DonVi", donVi);
            }
            else
            {
                if (donVi.MA == model.MA)
                {
                    model.Error = "MA Already exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA Already exist", new List<DonViModel>() { model });
                }
                if (donVi.MA_DVQHNS == model.MA_DVQHNS)
                {
                    model.Error = "MA_DVQHNS Already exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_DVQHNS Already exist", new List<DonViModel>() { model });
                }
                donVi.TEN = model.TEN;
                donVi.MA_DIA_BAN = model.MA_DIA_BAN;
                donVi.MA_DVQHNS = model.MA_DVQHNS;
                donVi.DIA_CHI = model.DIA_CHI;
                donVi.DIEN_THOAI = model.DIEN_THOAI;
                donVi.FAX = model.FAX;
                donVi.CHE_DO_HACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                donVi.PARENT_ID = PARENT_ID;
                donVi.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                donVi.DIA_BAN_ID = DIA_BAN_ID;
                donVi.TRANG_THAI_ID = model.TRANG_THAI_ID;
                _donViService.UpdateDonVi(donVi);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật đơn vị", 0, "DonVi", donVi);
            }
            model.ID = (long)donVi.ID;
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<DonViModel>() { model });

        }
        public MessageReturn UpdateListDonVi(List<DonViModel> ListDonViModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            int TotalErr = 0;
            List<DonVi> LstAdd = new List<DonVi>();
            List<DonVi> LstEdit = new List<DonVi>();
            List<DonVi> donVis = new List<DonVi>();
            foreach (var model in ListDonViModel)
            {
                // kiểm tra dữ liệu
                if (!(model.DB_ID > 0))
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.MA) && string.IsNullOrEmpty(model.MA_DVQHNS))
                {
                    model.Error = "MA-MA_DVQHNS null";
                    TotalErr++;
                    continue;
                }
                if (_donViService.CheckMaDonVi(model.ID, model.MA))
                {
                    model.Error = $"TRUNG MA {model.MA}";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
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
                        model.Error = "MA_CHA not exist";
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
                        //khóa vào do dmdc chưa khớp đồng bộ
                        /*model.Error = "MA_DIA_BAN not exist";
                        TotalErr++;
                        continue;*/
                        model.MA_DIA_BAN = null;
                    }
                    else
                    {
                        DIA_BAN_ID = diaBan.ID;
                    }
                }
                decimal? LOAI_DON_VI_ID = null;
                if (model.DB_LOAI_DON_VI_ID > 0)
                {
                    LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViByIdDb((int)model.DB_LOAI_DON_VI_ID.Value);
                    if (loaiDonVi == null)
                    {
                        model.Error = "MA_LOAI_DON_VI not exist";
                        TotalErr++;
                        continue;
                    }
                    else
                    {
                        LOAI_DON_VI_ID = loaiDonVi.ID;
                    }
                }
                DonVi entity = _donViService.GetDonViById(model.ID);
                if (entity != null)
                {
                    //if (entity.MA == model.MA)
                    //{
                    //    model.Error = "MA Already exist";
                    //    TotalErr++;
                    //    continue;
                    //}
                    //if (entity.MA_DVQHNS == model.MA_DVQHNS)
                    //{
                    //    model.Error = "MA_DVQHNS Already exist";
                    //    TotalErr++;
                    //    continue;
                    //}
                    entity.TEN = model.TEN;
                    entity.MA_DIA_BAN = model.MA_DIA_BAN;
                    entity.MA_DVQHNS = model.MA_DVQHNS;
                    entity.DIA_CHI = model.DIA_CHI;
                    entity.DIEN_THOAI = model.DIEN_THOAI;
                    entity.FAX = model.FAX;
                    entity.CHE_DO_HACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
                    entity.PARENT_ID = PARENT_ID;
                    entity.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                    entity.DIA_BAN_ID = DIA_BAN_ID;
                    entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    entity.CAP_DON_VI_ID = model.CAP_DON_VI_ID;
                    entity.MA = model.MA;
                    entity.DB_ID = model.DB_ID;
                    entity.LA_DON_VI_NHAP_LIEU = model.LA_DON_VI_NHAP_LIEU;

                    //entity.NGAY_CAP_NHAT = DateTime.Now;
                    LstEdit.Add(entity);
                }
                else
                {
                    if (model.ID > 0)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    entity = model.ToEntity<DonVi>();
                    entity.ID = 0;
                    entity.LOAI_DON_VI_ID = LOAI_DON_VI_ID;
                    entity.PARENT_ID = PARENT_ID;
                    entity.DIA_BAN_ID = DIA_BAN_ID;
                    //entity.NGUOI_CAP_NHAT_ID = null;
                    //entity.NGAY_TAO = DateTime.Now;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _donViService.InsertListDonVi(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới đơn vị", 0, "DonVi", LstAdd);
                donVis.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _donViService.UpdateListDonVi(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật đơn vị", 0, "DonVi", LstEdit);
                donVis.AddRange(LstEdit);
            }
            foreach (var item in ListDonViModel)
            {
                var donVi = donVis.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (donVi == null)
                    continue;
                item.ID = (long)donVi.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {donVis.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListDonViModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    ObjectInfo = ListDonViModel,
                    Message = "Success done"
                };
            }

        }
        public MessageReturn DeleteDonVi(decimal ID = 0)
        {
            DonVi donVi = _donViService.GetDonViById(Id: ID);
            try
            {
                donVi.DB_ID = null;
                _donViService.UpdateDonVi(donVi);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }
        }
        public string MapCapDonVi(decimal CapDonVi)
        {
            switch (CapDonVi)
            {
                case (decimal)CapEnum.BoCoQuanTrungUong:
                    return "001";
                case (decimal)CapEnum.Huyen:
                    return "002";
                case (decimal)CapEnum.Tinh:
                    return "001";
                case (decimal)CapEnum.Xa:
                    return "003";
                default:
                    return null;
            }
        }
        #endregion
        #region Hinh thức xử lý
        public IList<PhuongAnXuLyModel> GetAllPhuongAnXuLys()
        {
            var query = _phuongAnXuLyService.GetAllPhuongAnXuLys();
            return query.Select(m => m.ToModel<PhuongAnXuLyModel>()).ToList();
        }
        public MessageReturn UpdatePhuongAnXuLy(PhuongAnXuLyModel model, NguoiDung currentUser)
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
        public MessageReturn UpDateListHinhThucXuLy(List<HinhThucXuLyModel> ListHinhThucXuLyModel, NguoiDung currentUser)
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
                var entity = _hinhThucXuLyService.GetHinhThucXuLyById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.SAP_XEP = model.SAP_XEP;
                    entity.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<HinhThucXuLy>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                //_hinhThucXuLyService.InsertHinhThucXuLy(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                // _phuongAnXuLyService.UpdateListPhuongAnXuLy(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        public MessageReturn DeleteHinhThucXuLy(decimal ID = 0)
        {
            HinhThucXuLy hinhThucXuLy = _hinhThucXuLyService.GetHinhThucXuLyById(Id: ID);
            try
            {
                if (hinhThucXuLy == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                hinhThucXuLy.DB_ID = null;
                _hinhThucXuLyService.DeleteHinhThucXuLy(hinhThucXuLy);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Nguồn gốc tài sản
        public IList<NguonGocTaiSanModel> GetAllNguonGocTaiSans()
        {
            var query = _nguonGocTaiSanService.GetAllNguonGocTaiSans();
            return query.Select(m => m.ToModel<NguonGocTaiSanModel>()).ToList();
        }
        public MessageReturn UpdateNguonGocTaiSan(NguonGocTaiSanModel model, NguoiDung currentUser)
        {
            if (model.DB_ID == 0 || model.DB_ID == null)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_HINH_TAI_SAN_ID null", new List<NguonGocTaiSanModel>() { model });
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_HINH_TAI_SAN_ID null", new List<NguonGocTaiSanModel>() { model });
            }
            decimal? PARENT_ID = null;
            if (model.DB_PARENT_ID > 0)
            {
                var NguonGocCha = _nguonGocTaiSanService.GetNguonGocTaiSanByDbID(model.DB_PARENT_ID.Value);
                if (NguonGocCha != null)
                {
                    PARENT_ID = NguonGocCha.PARENT_ID;
                }
                else
                {
                    model.Error = "DB_PARENT_ID not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_PARENT_ID not exist", new List<NguonGocTaiSanModel>() { model });
                }
            }
            if (model.ID > 0)// cập nhật
            {
                var NguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanById(model.ID);
                if (NguonGocTaiSan == null)
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "ID not exist", new List<NguonGocTaiSanModel>() { model });
                }
                NguonGocTaiSan.TEN = model.TEN;
                NguonGocTaiSan.PARENT_ID = PARENT_ID;
                _nguonGocTaiSanService.UpdateNguonGocTaiSan(NguonGocTaiSan);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn gốc tài sản", 0, "NguonGocTaiSan", NguonGocTaiSan);
            }
            else// không có thì thêm mới
            {
                NguonGocTaiSan nguonGocTaiSan = model.ToEntity<NguonGocTaiSan>();
                nguonGocTaiSan.ID = 0;
                nguonGocTaiSan.PARENT_ID = PARENT_ID;
                _nguonGocTaiSanService.InsertNguonGocTaiSan(nguonGocTaiSan);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn gốc tài sản", 0, "NguonGocTaiSan", nguonGocTaiSan);
                model.ID = (long)nguonGocTaiSan.ID;
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<NguonGocTaiSanModel>() { model });
        }
        public MessageReturn UpDateListNguonGocTaiSan(List<NguonGocTaiSanModel> ListNguonGocTaiSanModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<NguonGocTaiSan> LstAdd = new List<NguonGocTaiSan>();
            List<NguonGocTaiSan> LstEdit = new List<NguonGocTaiSan>();
            List<NguonGocTaiSan> nguonGocTaiSans = new List<NguonGocTaiSan>();
            foreach (var model in ListNguonGocTaiSanModel)
            {
                if (model.DB_ID == 0 || model.DB_ID == null)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                decimal? PARENT_ID = null;
                if (model.DB_PARENT_ID > 0)
                {
                    var NguonGocCha = _nguonGocTaiSanService.GetNguonGocTaiSanByDbID(model.DB_PARENT_ID.Value);
                    if (NguonGocCha != null)
                    {
                        PARENT_ID = NguonGocCha.PARENT_ID;
                    }
                    else
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                }
                var entity = _nguonGocTaiSanService.GetNguonGocTaiSanById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    entity.PARENT_ID = PARENT_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    if (model.ID > 0)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    entity = model.ToEntity<NguonGocTaiSan>();
                    entity.ID = 0;
                    entity.PARENT_ID = PARENT_ID;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _nguonGocTaiSanService.InsertListNguonGocTaiSan(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới nguồn gốc tài sản", 0, "NguonGocTaiSan", LstAdd);
                nguonGocTaiSans.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _nguonGocTaiSanService.UpdateListNguonGocTaiSan(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật nguồn gốc tài sản", 0, "NguonGocTaiSan", LstEdit);
                nguonGocTaiSans.AddRange(LstEdit);
            }
            foreach (var item in ListNguonGocTaiSanModel)
            {
                var nguonGocTaiSan = nguonGocTaiSans.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (nguonGocTaiSan == null)
                    continue;
                item.ID = (long)nguonGocTaiSan.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {nguonGocTaiSans.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListNguonGocTaiSanModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListNguonGocTaiSanModel
                };
            }
        }
        public MessageReturn DeleteNguonGocTaiSan(decimal ID = 0)
        {

            NguonGocTaiSan nguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanById(Id: ID);
            try
            {
                if (nguonGocTaiSan.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                nguonGocTaiSan.DB_ID = null;
                _nguonGocTaiSanService.UpdateNguonGocTaiSan(nguonGocTaiSan);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Phương án xử lý
        public IList<HinhThucXuLyModel> GetAllHinhThucXuLys()
        {
            var query = _hinhThucXuLyService.GetAllHinhThucXuLys();
            return query.Select(m => new HinhThucXuLyModel()
            {
                TEN = m.TEN,
                MA = m.MA,
                // MA_HINH_THUC_XU_LY = m.HINH_THUC_XU_LY_ID != null ? _phuongAnXuLyService.GetPhuongAnXuLyById(m.HINH_THUC_XU_LY_ID.Value).MA : null,
                SAP_XEP = m.SAP_XEP
            }).ToList();
        }
        public MessageReturn UpdateHinhThucXuLy(HinhThucXuLyModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
            {
                return MessageReturn.CreateErrorMessage("MA invalid");
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                return MessageReturn.CreateErrorMessage("TEN invalid");
            }
            // hình thức xử lys
            decimal? PhuongAnXuLyId = null;
            if (model.PHUONG_AN_XU_LY_ID > 0)
            {
                PhuongAnXuLy phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyById(model.PHUONG_AN_XU_LY_ID.Value);
                if (phuongAn_XyLy != null)
                {
                    PhuongAnXuLyId = phuongAn_XyLy.ID;
                }
                else
                {
                    return MessageReturn.CreateErrorMessage("MA_HINH_THUC_XU_LY invalid");
                }
            }


            var HinhThucXuLy = _hinhThucXuLyService.GetHinhThucXuLyByMa(model.MA);
            if (HinhThucXuLy != null)// cập nhật
            {
                HinhThucXuLy.TEN = model.TEN;
                HinhThucXuLy.SAP_XEP = model.SAP_XEP;
                HinhThucXuLy.PHUONG_AN_XU_LY_ID = PhuongAnXuLyId;

                _hinhThucXuLyService.UpdateHinhThucXuLy(HinhThucXuLy);
            }
            else// không có thì thêm mới
            {
                HinhThucXuLy = model.ToEntity<HinhThucXuLy>();
                HinhThucXuLy.ID = 0;
                HinhThucXuLy.PHUONG_AN_XU_LY_ID = PhuongAnXuLyId;
                _hinhThucXuLyService.InsertHinhThucXuLy(HinhThucXuLy);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn UpDateListPhuongAnXuLy(List<PhuongAnXuLyModel> ListPhuongAnXuLyModel, NguoiDung currentUser)
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
                decimal? PhuongAnXuLyId = null;
                if (!string.IsNullOrEmpty(model.MA))
                {
                    PhuongAnXuLy phuongAn_XyLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(model.MA);
                    if (phuongAn_XyLy != null)
                    {
                        PhuongAnXuLyId = phuongAn_XyLy.ID;
                    }
                    else
                    {
                        err += $"\nMA: {model.MA}\t\tMA invalid";
                        TotalErr++;
                        continue;
                    }
                }
                var entity = _phuongAnXuLyService.GetPhuongAnXuLyById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.SAP_XEP = model.SAP_XEP;
                    // entity.Phuo = PhuongAnXuLyId;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<PhuongAnXuLy>();
                    entity.ID = 0;
                    //  entity.HINH_THUC_XU_LY_ID = PhuongAnXuLyId;
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
        public MessageReturn DeletePhuongAnXuLy(decimal ID = 0)
        {
            PhuongAnXuLy PhuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLyById(Id: ID);
            try
            {
                if (PhuongAnXuLy == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                PhuongAnXuLy.DB_ID = null;
                _phuongAnXuLyService.DeletePhuongAnXuLy(PhuongAnXuLy);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }

        #endregion
        #region Mục đích sử dụng
        public IList<MucDichSuDungModel> GetAllMucDichSuDungs()
        {
            var query = _mucDichSuDungService.GetMucDichSuDungs();
            return query.Select(m => new MucDichSuDungModel()
            {
                TEN = m.TEN,
                MA = m.MA,
            }).ToList();
        }
        public MessageReturn UpdateMucDichSuDung(MucDichSuDungModel model, NguoiDung currentUser)
        {
            if (model.DB_ID == null || model.DB_ID == 0)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<MucDichSuDungModel>() { model });
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<MucDichSuDungModel>() { model });
            }
            // hình thức xử lys
            MucDichSuDung mucDichSuDung = new MucDichSuDung();
            if (model.ID == 0)
            {
                mucDichSuDung = new MucDichSuDung();
                mucDichSuDung.TEN = model.TEN;
                mucDichSuDung.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID != null ? model.LOAI_HINH_TAI_SAN_ID.Value : (decimal)enumLOAI_HINH_TAI_SAN.ALL;
                mucDichSuDung.DB_ID = model.DB_ID;
                _mucDichSuDungService.InsertMucDichSuDung(mucDichSuDung);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Mục đích sử dụng", 0, "MucDichSuDung", mucDichSuDung);
                model.ID = (long)mucDichSuDung.ID;

            }
            else
            {
                mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(model.ID);
                if (mucDichSuDung != null)// cập nhật
                {
                    mucDichSuDung.TEN = model.TEN;
                    mucDichSuDung.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID != null ? model.LOAI_HINH_TAI_SAN_ID.Value : (decimal)enumLOAI_HINH_TAI_SAN.ALL;
                    _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Mục đích sử dụng", 0, "MucDichSuDung", mucDichSuDung);

                }
                else// không có thì thêm mới
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "ID not exist", new List<MucDichSuDungModel>() { model });
                }
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<MucDichSuDungModel>() { model });
        }
        public MessageReturn UpDateListMucDichSuDung(List<MucDichSuDungModel> ListMucDichSuDungModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<MucDichSuDung> LstAdd = new List<MucDichSuDung>();
            List<MucDichSuDung> LstEdit = new List<MucDichSuDung>();
            List<MucDichSuDung> mucDichSuDungs = new List<MucDichSuDung>();
            foreach (var model in ListMucDichSuDungModel)
            {
                if (model.DB_ID == null || model.DB_ID == 0)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                var entity = _mucDichSuDungService.GetMucDichSuDungById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID != null ? model.LOAI_HINH_TAI_SAN_ID.Value : (decimal)enumLOAI_HINH_TAI_SAN.KHAC;
                    LstEdit.Add(entity);
                }
                else
                {
                    if (model.ID > 0)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    entity = model.ToEntity<MucDichSuDung>();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _mucDichSuDungService.InsertListMucDichSuDung(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới mục đích sử dụng", 0, "MucDichSuDung", LstAdd);
                mucDichSuDungs.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _mucDichSuDungService.UpdateListMucDichSuDung(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật mục đích sử dụng", 0, "MucDichSuDung", LstAdd);
                mucDichSuDungs.AddRange(LstEdit);
            }
            foreach (var item in ListMucDichSuDungModel)
            {
                var mucDichSuDung = mucDichSuDungs.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (mucDichSuDung == null)
                    continue;
                item.ID = (long)mucDichSuDung.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {mucDichSuDungs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListMucDichSuDungModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListMucDichSuDungModel
                };
            }
        }
        public MessageReturn DeleteMucDichSuDung(decimal ID = 0)
        {
            MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungById(Id: ID);
            try
            {
                if (mucDichSuDung.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                mucDichSuDung.DB_ID = null;
                _mucDichSuDungService.UpdateMucDichSuDung(mucDichSuDung);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Người dùng
        public ResultNguoiDung GetAllNguoiDungs(int pageSize = int.MaxValue, int pageIndex = 0)
        {
            var query = _nguoiDungService.SearchNguoiDungs(pageIndex: pageIndex, pageSize: pageSize);

            List<NguoiDungModel> ListModel = new List<NguoiDungModel>();
            ResultNguoiDung result = new ResultNguoiDung();
            foreach (var item in query)
            {
                if (item.TEN_DANG_NHAP == "admin")
                {
                    continue;
                }
                NguoiDungModel model = new NguoiDungModel();
                model.TEN_DANG_NHAP = item.TEN_DANG_NHAP;
                model.TEN_DAY_DU = item.TEN_DAY_DU;
                model.EMAIL = item.EMAIL;
                model.MAT_KHAU = item.MAT_KHAU;
                model.PASSWORD_HASH = item.PASSWORD_HASH;
                model.IS_QUAN_TRI = item.IS_QUAN_TRI;
                model.MOBILE = item.MOBILE;
                //var ListDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(item.ID);
                //if (ListDonVi != null)
                //{
                //    model.LIST_DON_VI = ListDonVi.Where(m => m.donvi != null).Select(n => new
                //    {
                //        MA_DON_VI = n.donvi.MA,
                //        MA_DVQHNS = n.donvi.MA_DVQHNS
                //    }).toStringJson();
                //}
                model.TRANG_THAI_ID = item.TRANG_THAI_ID;
                ListModel.Add(model);
            }
            result.ListNguoiDung = ListModel;
            result.Total = query.TotalCount;
            result.TotalPage = query.TotalPages;
            return result;
        }
        public MessageReturn UpdateNguoiDung(NguoiDungModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.TEN_DANG_NHAP))
            {
                model.Error = "TEN_DANG_NHAP null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN_DANG_NHAP null", new List<NguoiDungModel>() { model });
            }
            if (model.LIST_DON_VI == null || (model.LIST_DON_VI != null && model.LIST_DON_VI.Count == 0))
            {
                model.Error = "LIST_DON_VI null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LIST_DON_VI null", new List<NguoiDungModel>() { model });
            }
            //if (string.IsNullOrEmpty(model.MAT_KHAU))
            //{
            //    return MessageReturn.CreateNotFoundMessage("MAT_KHAU invalid");
            //}
            NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(model.TEN_DANG_NHAP);

            if (nguoiDung == null)
            {
                nguoiDung = model.ToEntity<NguoiDung>();
                nguoiDung.ID = 0;
                nguoiDung.NGUON_ID = getNguonIdNguoiDung(currentUser);
                _nguoiDungService.InsertNguoiDung(nguoiDung);
                model.ID = (long)nguoiDung.ID;
                // người dùng đơn vị


            }
            else
            {
                nguoiDung.MAT_KHAU = model.MAT_KHAU;
                nguoiDung.PASSWORD_HASH = model.PASSWORD_HASH;
                nguoiDung.MAT_KHAU_KEY = model.MAT_KHAU_KEY;
                nguoiDung.EMAIL = model.EMAIL;
                //nguoiDung.IS_QUAN_TRI = model.IS_QUAN_TRI;
                nguoiDung.TRANG_THAI_ID = model.TRANG_THAI_ID;
                nguoiDung.MOBILE = model.MOBILE;
                nguoiDung.NGUON_ID = getNguonIdNguoiDung(currentUser);
                _nguoiDungService.UpdateNguoiDung(nguoiDung);
                // người dùng đơn vị
                var ListNguoiDungDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(nguoiDung.ID);
                foreach (var item in ListNguoiDungDonVi)
                {
                    _nguoiDungDonViService.DeleteNguoiDungDonVi(item);
                }
            }


            foreach (var item in model.LIST_DON_VI)
            {
                if (!string.IsNullOrEmpty(item.MA_DON_VI))
                {
                    DonVi donVi = _donViService.GetDonViByMa(item.MA_DON_VI);
                    if (donVi != null)
                    {
                        NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
                        nguoiDungDonVi.NGUOI_DUNG_ID = nguoiDung.ID;
                        nguoiDungDonVi.DON_VI_ID = donVi.ID;
                        _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
                    }
                }
                else if (!string.IsNullOrEmpty(item.MA_DVQHNS))
                {
                    DonVi donVi = _donViService.GetDonViByMaDVQHNS(item.MA_DVQHNS);
                    if (donVi != null)
                    {
                        NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
                        nguoiDungDonVi.NGUOI_DUNG_ID = nguoiDung.ID;
                        nguoiDungDonVi.DON_VI_ID = donVi.ID;
                        _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
                    }
                }
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<NguoiDungModel>() { model });
        }
        public MessageReturn UpdateListNguoiDung(List<NguoiDungModel> ListNguoiDungModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            //Nguồn tạo tài khoản:
            //0: QLTSC, 1: CSDLQGTSC, 2: NSNT, 3: HTGT

            // lọc các quốc gia không đủ điều kiện
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<NguoiDung> LstAdd = new List<NguoiDung>();
            List<NguoiDung> LstEdit = new List<NguoiDung>();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();
            foreach (var model in ListNguoiDungModel)
            {
                if (string.IsNullOrEmpty(model.DB_ID))
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN_DANG_NHAP))
                {
                    model.Error = "TEN_DANG_NHAP null";
                    TotalErr++;
                    continue;
                }
                if (model.LIST_DON_VI == null || (model.LIST_DON_VI != null && model.LIST_DON_VI.Count == 0))
                {
                    model.Error = "LIST_DON_VI null";
                    TotalErr++;
                    continue;
                }
                //if (string.IsNullOrEmpty(model.MAT_KHAU))
                //{
                //    err += $"\nTEN_DANG_NHAP: {model.TEN_DANG_NHAP}\t\tMAT_KHAU invalid";
                //    TotalErr++;
                //    continue;
                //}
                NguoiDung entity = new NguoiDung();
                entity = _nguoiDungService.GetNguoiDungByUsername(model.TEN_DANG_NHAP);
                if (entity != null)
                {
                    entity.EMAIL = model.EMAIL;
                    //entity.IS_QUAN_TRI = model.IS_QUAN_TRI;
                    entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    entity.MOBILE = model.MOBILE;
                    entity.TEN_DAY_DU = model.TEN_DAY_DU;
                    entity.GHI_CHU = model.GHI_CHU;
                    entity.NGUON_ID = getNguonIdNguoiDung(currentUser);
                    _nguoiDungService.UpdateNguoiDung(entity);
                    // người dùng đơn vị
                    var nddv = _nguoiDungDonViService.GetMapByNguoiDungId(entity.ID);
                    if (nddv != null)
                    {
                        foreach (var item in nddv)
                        {
                            _nguoiDungDonViService.DeleteNguoiDungDonVi(item);
                        }
                    }
                    foreach (var item in model.LIST_DON_VI)
                    {
                        // get don vị
                        DonVi donVi = _donViService.GetCacheDonViByMa(item.MA_DON_VI);
                        if (donVi == null)
                        {
                            donVi = _donViService.GetDonViByMaDVQHNS(item.MA_DVQHNS);
                            if (donVi == null)
                            {

                                //model.Error = "LST_DON_VI not exist";
                                //_nguoiDungService.DeleteNguoiDung(entity);
                                //TotalErr++;
                                continue;
                            }
                        }
                        NguoiDungDonViMapping nguoiDungDonViMapping = new NguoiDungDonViMapping();
                        nguoiDungDonViMapping.NGUOI_DUNG_ID = entity.ID;
                        nguoiDungDonViMapping.DON_VI_ID = donVi.ID;
                        _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonViMapping);
                    }
                }
                else
                {
                    entity = model.ToEntity<NguoiDung>();
                    entity.ID = 0;
                    entity.GUID = Guid.NewGuid();
                    entity.NGUON_ID = getNguonIdNguoiDung(currentUser);
                    _nguoiDungService.InsertNguoiDung(entity);
                    var nddv = _nguoiDungDonViService.GetMapByNguoiDungId(entity.ID);
                    if (nddv != null)
                    {
                        foreach (var item in nddv)
                        {
                            _nguoiDungDonViService.DeleteNguoiDungDonVi(item);
                        }
                    }
                    foreach (var item in model.LIST_DON_VI)
                    {
                        // get don vị
                        DonVi donVi = _donViService.GetCacheDonViByMa(item.MA_DON_VI);
                        if (donVi == null)
                        {
                            donVi = _donViService.GetDonViByMaDVQHNS();
                            if (donVi == null)
                            {
                                //model.Error = "LST_DON_VI not exist";
                                //_nguoiDungService.DeleteNguoiDung(entity);
                                //TotalErr++;
                                continue;
                            }
                        }
                        NguoiDungDonViMapping nguoiDungDonViMapping = new NguoiDungDonViMapping();
                        nguoiDungDonViMapping.NGUOI_DUNG_ID = entity.ID;
                        nguoiDungDonViMapping.DON_VI_ID = donVi.ID;
                        _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonViMapping);
                    }
                }
                nguoiDungs.Add(entity);
                model.GUID = entity.GUID.ToString();
            }
            #region cmt
            //if (LstAdd.Count > 0)
            //{
            //    _nguoiDungService.InsertListNguoiDung(LstAdd);
            //    TotalSuc += LstAdd.Count();
            //}
            //if (LstEdit.Count > 0)
            //{
            //    _nguoiDungService.UpdateListNguoiDung(LstEdit);
            //    TotalSuc += LstEdit.Count();
            //}
            //// cập nhật người dùng đơn vị
            //foreach (var item in LstAdd)
            //{
            //    string strListDonVi = ListNguoiDungModel.Where(m => m.TEN_DANG_NHAP == item.TEN_DANG_NHAP).FirstOrDefault().LIST_DON_VI;
            //    var ListDonVi = strListDonVi.toEntities<NguoiDungDonViModel>();
            //    foreach (var dv in ListDonVi)
            //    {
            //        if (!string.IsNullOrEmpty(dv.MA_DON_VI))
            //        {
            //            DonVi donVi = _donViService.GetDonViByMa(dv.MA_DON_VI);
            //            if (donVi != null)
            //            {
            //                NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
            //                nguoiDungDonVi.NGUOI_DUNG_ID = item.ID;
            //                nguoiDungDonVi.DON_VI_ID = donVi.ID;
            //                _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            //            }
            //        }
            //        else if (!string.IsNullOrEmpty(dv.MA_DVQHNS))
            //        {
            //            DonVi donVi = _donViService.GetDonViByMaDVQHNS(dv.MA_DVQHNS);
            //            if (donVi != null)
            //            {
            //                NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
            //                nguoiDungDonVi.NGUOI_DUNG_ID = item.ID;
            //                nguoiDungDonVi.DON_VI_ID = donVi.ID;
            //                _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            //            }
            //        }
            //    }
            //}
            //foreach (var item in LstEdit)
            //{
            //    var ListNguoiDungDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(item.ID);
            //    foreach (var nd in ListNguoiDungDonVi)
            //    {
            //        _nguoiDungDonViService.DeleteNguoiDungDonVi(nd);
            //    }
            //    string strListDonVi = ListNguoiDungModel.Where(m => m.TEN_DANG_NHAP == item.TEN_DANG_NHAP).FirstOrDefault().LIST_DON_VI;
            //    var ListDonVi = strListDonVi.toEntities<NguoiDungDonViModel>();
            //    foreach (var dv in ListDonVi)
            //    {
            //        if (!string.IsNullOrEmpty(dv.MA_DON_VI))
            //        {
            //            DonVi donVi = _donViService.GetDonViByMa(dv.MA_DON_VI);
            //            if (donVi != null)
            //            {
            //                NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
            //                nguoiDungDonVi.NGUOI_DUNG_ID = item.ID;
            //                nguoiDungDonVi.DON_VI_ID = donVi.ID;
            //                _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            //            }
            //        }
            //        else if (!string.IsNullOrEmpty(dv.MA_DVQHNS))
            //        {
            //            DonVi donVi = _donViService.GetDonViByMaDVQHNS(dv.MA_DVQHNS);
            //            if (donVi != null)
            //            {
            //                NguoiDungDonViMapping nguoiDungDonVi = new NguoiDungDonViMapping();
            //                nguoiDungDonVi.NGUOI_DUNG_ID = item.ID;
            //                nguoiDungDonVi.DON_VI_ID = donVi.ID;
            //                _nguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            //            }
            //        }
            //    }
            //}
            #endregion
            foreach (var item in ListNguoiDungModel)
            {
                NguoiDung nguoiDung = nguoiDungs.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (nguoiDung == null)
                    continue;
                item.ID = (long)nguoiDung.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {nguoiDungs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListNguoiDungModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListNguoiDungModel
                };
            }
        }
        public MessageReturn DeleteNguoiDung(string TEN_DANG_NHAP)
        {
            NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(TEN_DANG_NHAP);
            try
            {
                nguoiDung.DB_ID = null;
                _nguoiDungService.UpdateNguoiDung(nguoiDung);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("TEN_DANG_NHAP not exist");
            }
        }
        public decimal? getNguonIdNguoiDung(NguoiDung currentUser)
        {
            //Nguồn tạo tài khoản:
            //0: QLTSC, 1: CSDLQGTSC, 2: NSNT, 3: HTGT
            if (currentUser.TEN_DANG_NHAP == _gSConfig.UserNameKhoCSDL)
                return 1;
            else if (currentUser.TEN_DANG_NHAP == _gSConfig.UserNameCTNS)
                return 2;
            else if (currentUser.TEN_DANG_NHAP == _gSConfig.UserNameHTGTDB)
                return 3;
            else
                return 0;
        }
        #endregion
        #region Chế độ hao mòn
        public IList<CheDoHaoMonModel> GetAllCheDoHaoMons()
        {
            var query = _cheDoHaoMonService.GetAllCheDoHaoMons();
            return query.Select(m => m.ToModel<CheDoHaoMonModel>()).ToList();
        }
        public MessageReturn UpdateCheDoHaoMon(CheDoHaoMonModel model, NguoiDung currentUser)
        {
            if (model.DB_ID == null || model.DB_ID == 0)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<CheDoHaoMonModel>() { model });
            }
            CheDoHaoMon cheDoHaoMon = new CheDoHaoMon();
            if (model.ID == 0)
            {
                cheDoHaoMon = model.ToEntity<CheDoHaoMon>();
                if (string.IsNullOrEmpty(model.DEN_NGAY))
                {
                    cheDoHaoMon.DEN_NGAY = DateTime.Parse(model.DEN_NGAY);
                }
                if (string.IsNullOrEmpty(model.DEN_NGAY))
                {
                    cheDoHaoMon.TU_NGAY = DateTime.Parse(model.TU_NGAY);
                }
                cheDoHaoMon.ID = 0;
                _cheDoHaoMonService.InsertCheDoHaoMon(cheDoHaoMon);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Cập nhật Chế độ hao mòn", 0, "CheDoHaoMon", model);
            }
            else
            {
                cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(model.ID);
                if (cheDoHaoMon != null)// cập nhật
                {
                    cheDoHaoMon.TEN_CHE_DO = model.TEN_CHE_DO;
                    if (!string.IsNullOrEmpty(model.DEN_NGAY))
                    {
                        cheDoHaoMon.DEN_NGAY = DateTime.Parse(model.DEN_NGAY);
                    }
                    if (!string.IsNullOrEmpty(model.TU_NGAY))
                    {
                        cheDoHaoMon.TU_NGAY = DateTime.Parse(model.TU_NGAY);
                    }
                    _cheDoHaoMonService.UpdateCheDoHaoMon(cheDoHaoMon);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chế độ hao mòn", 0, "CheDoHaoMon", model);
                }
                else// không có thì thêm mới
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "ID not exist", new List<CheDoHaoMonModel>() { model });
                }
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<CheDoHaoMonModel>() { model });
        }
        public MessageReturn UpDateListCheDoHaoMon(List<CheDoHaoMonModel> ListCheDoHaoMonModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            // lọc các quốc gia không đủ điều kiện
            int TotalErr = 0;
            List<CheDoHaoMon> LstAdd = new List<CheDoHaoMon>();
            List<CheDoHaoMon> LstEdit = new List<CheDoHaoMon>();
            List<CheDoHaoMon> cheDoHaoMons = new List<CheDoHaoMon>();
            foreach (var model in ListCheDoHaoMonModel)
            {
                if (!(model.DB_ID > 0))
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN_CHE_DO))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                if (model.ID > 0)
                {
                    CheDoHaoMon entity = _cheDoHaoMonService.GetCheDoHaoMonById(model.ID);
                    if (entity == null)
                    {
                        model.Error = "ID not exist";
                        TotalErr++;
                        continue;
                    }
                    else
                    {
                        entity.TEN_CHE_DO = model.TEN_CHE_DO;
                        if (!string.IsNullOrEmpty(model.DEN_NGAY))
                        {
                            entity.DEN_NGAY = DateTime.Parse(model.DEN_NGAY);
                        }
                        if (!string.IsNullOrEmpty(model.TU_NGAY))
                        {
                            entity.TU_NGAY = DateTime.Parse(model.TU_NGAY);
                        }
                        LstEdit.Add(entity);
                    }
                }
                else
                {
                    CheDoHaoMon entity = new CheDoHaoMon();
                    entity.MA_CHE_DO = model.MA_CHE_DO;
                    entity.TEN_CHE_DO = model.TEN_CHE_DO;
                    if (!string.IsNullOrEmpty(model.DEN_NGAY))
                    {
                        entity.DEN_NGAY = DateTime.Parse(model.DEN_NGAY);
                    }
                    if (!string.IsNullOrEmpty(model.TU_NGAY))
                    {
                        entity.TU_NGAY = DateTime.Parse(model.TU_NGAY);
                    }
                    entity.DB_ID = model.DB_ID;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _cheDoHaoMonService.InsertListCheDoHaoMon(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Chế độ", 0, "CheDoHaoMon", LstAdd);
                cheDoHaoMons.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _cheDoHaoMonService.UpdateListCheDoHaoMon(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chế độ hao mòn", 0, "CheDoHaoMon", LstEdit);
                cheDoHaoMons.AddRange(LstEdit);
            }

            foreach (var item in ListCheDoHaoMonModel)
            {
                CheDoHaoMon cheDoHaoMon = cheDoHaoMons.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (cheDoHaoMon == null)
                    continue;
                item.ID = (long)cheDoHaoMon.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {cheDoHaoMons.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListCheDoHaoMonModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListCheDoHaoMonModel
                };
            }
        }
        public MessageReturn DeleteCheDoHaoMon(decimal ID = 0, NguoiDung currentUser = null)
        {
            CheDoHaoMon cheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(Id: ID);
            try
            {
                if (cheDoHaoMon.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                cheDoHaoMon.DB_ID = null;
                _cheDoHaoMonService.UpdateCheDoHaoMon(cheDoHaoMon);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID Error");
            }
        }
        #endregion
        #region DongXe
        public IList<DongXeModel> GetAllDongXes()
        {
            var query = _dongXeService.GetAllDongXes();
            if (query == null)
                return null;
            List<DongXeModel> rs = query.Select(c => { var nhanXe = c.ToModel<DongXeModel>(); nhanXe.MA_NHAN_XE = c.NhanXe != null ? c.NhanXe.MA : null; return nhanXe; }).ToList();
            return rs;
        }
        public MessageReturn UpdateDongXe(DongXeModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
                return MessageReturn.CreateErrorMessage("MA invalid");
            if (string.IsNullOrEmpty(model.TEN))
                return MessageReturn.CreateErrorMessage("TEN invalid");
            Decimal? NHAN_XE_ID = null;
            if (!string.IsNullOrEmpty(model.MA_NHAN_XE))
            {
                var NHAN_XE = _nhanXeService.GetNhanXeByMaTen(model.MA_NHAN_XE);
                if (NHAN_XE != null)
                    NHAN_XE_ID = NHAN_XE.ID;
                else
                    return MessageReturn.CreateErrorMessage("MA_NHAN_XE invalid");
            }
            var entity = _dongXeService.GetDongXeByMa(model.MA);
            if (entity != null)
            {
                entity.MO_TA = model.MO_TA;
                entity.TEN = model.TEN;
                entity.NHAN_XE_ID = NHAN_XE_ID;
                _dongXeService.UpdateDongXe(entity);
            }
            else
            {
                model.ID = 0;
                entity = model.ToEntity<DongXe>();
                entity.NHAN_XE_ID = NHAN_XE_ID;
                _dongXeService.InsertDongXe(entity);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn UpdateDongXes(List<DongXeModel> ListModel, NguoiDung currentUser)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<DongXe> LstAdd = new List<DongXe>();
            List<DongXe> LstEdit = new List<DongXe>();
            foreach (var model in ListModel)
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
                Decimal? NHAN_XE_ID = null;
                if (!string.IsNullOrEmpty(model.MA_NHAN_XE))
                {
                    var NHAN_XE = _nhanXeService.GetNhanXeByMaTen(model.MA_NHAN_XE);
                    if (NHAN_XE != null)
                        NHAN_XE_ID = NHAN_XE.ID;
                    else
                    {
                        err += $"\nMA: {model.MA}\t\tMA_NHAN_XE invalid";
                        TotalErr++;
                        continue;
                    }
                }
                var entity = _dongXeService.GetDongXeByMa(model.MA);
                if (entity != null)
                {
                    entity.MO_TA = model.MO_TA;
                    entity.TEN = model.TEN;
                    entity.NHAN_XE_ID = NHAN_XE_ID;
                    LstEdit.Add(entity);
                    //_dongXeService.UpdateDongXe(entity);
                }
                else
                {
                    model.ID = 0;
                    entity = model.ToEntity<DongXe>();
                    entity.NHAN_XE_ID = NHAN_XE_ID;
                    LstAdd.Add(entity);
                    //_dongXeService.InsertDongXe(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _dongXeService.InsertDongXe(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _dongXeService.UpdateDongXe(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region LyDoBienDong
        public IList<LyDoBienDongModel> GetAllLyDoBienDongs()
        {
            var query = _lyDoBienDongService.GetAllLyDoBienDongs();
            if (query == null)
                return null;
            return query.Select(c => c.ToModel<LyDoBienDongModel>()).ToList();
        }
        public MessageReturn UpdateLyDoBienDong(LyDoBienDongModel model, NguoiDung currentUser)
        {
            return new MessageReturn(MessageReturn.ERROR, "ID not exist", new List<LyDoBienDongModel>() { model });
        }
        public MessageReturn UpdateListLyDoBienDong(List<LyDoBienDongModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            int TotalErr = 0;
            List<LyDoBienDong> LstAdd = new List<LyDoBienDong>();
            List<LyDoBienDong> LstEdit = new List<LyDoBienDong>();
            List<LyDoBienDong> lyDoBienDongs = new List<LyDoBienDong>();
            foreach (var model in ListModel)
            {

                var entity = _lyDoBienDongService.GetLyDoBienDongById(model.ID);
                if (entity != null)
                {
                    entity.LOAI_HINH_TAI_SAN_AP_DUNG_ID = model.LST_LOAI_HINH_TAI_SAN_ID.toStringJson();
                    entity.LOAI_LY_DO_BIEN_DONG_ID = model.LOAI_LY_DO_BIEN_DONG_ID;
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<LyDoBienDong>();
                    entity.LOAI_HINH_TAI_SAN_AP_DUNG_ID = model.LST_LOAI_HINH_TAI_SAN_ID.toStringJson();
                    entity.ID = 0;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _lyDoBienDongService.InsertLyDoBienDong(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Lý do biến động", 0, "LyDoBienDong", LstAdd);
                lyDoBienDongs.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _lyDoBienDongService.UpdateLyDoBienDong(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật lý do biến động", 0, "LyDoBienDong", LstEdit);
                lyDoBienDongs.AddRange(LstEdit);
            }
            foreach (var item in ListModel)
            {
                LyDoBienDong lyDoBienDong = lyDoBienDongs.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (lyDoBienDong == null)
                    continue;
                item.ID = (long)lyDoBienDong.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {lyDoBienDongs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteLyDoBienDong(decimal ID = 0)
        {
            LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongById(Id: ID);
            try
            {
                if (lyDoBienDong.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                lyDoBienDong.DB_ID = null;
                _lyDoBienDongService.UpdateLyDoBienDong(lyDoBienDong);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Loại lý do biến động
        public MessageReturn UpdateLoaiLyDoBienDong(List<LoaiLyDoBienDongModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            int TotalErr = 0;
            List<LoaiLyDoBienDong> LstAdd = new List<LoaiLyDoBienDong>();
            List<LoaiLyDoBienDong> LstEdit = new List<LoaiLyDoBienDong>();
            List<LoaiLyDoBienDong> lyDoBienDongs = new List<LoaiLyDoBienDong>();
            foreach (var model in ListModel)
            {
                var entity = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    entity.PARENT_ID = model.PARENT_ID;
                    _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(entity);
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<LoaiLyDoBienDong>();
                    entity.ID = 0;
                    _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(entity);
                    model.ID = (long)entity.ID;
                }
            }
            if (LstAdd.Count > 0)
            {

                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Lý do biến động", 0, "LyDoBienDong", LstAdd);
                lyDoBienDongs.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật lý do biến động", 0, "LyDoBienDong", LstEdit);
                lyDoBienDongs.AddRange(LstEdit);
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {lyDoBienDongs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteLoaiLyDoBienDong(decimal ID = 0)
        {
            LoaiLyDoBienDong lyDoBienDong = _loaiLyDoBienDongService.GetLoaiLyDoBienDongById(Id: ID);
            try
            {
                if (lyDoBienDong.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                lyDoBienDong.DB_ID = null;
                _loaiLyDoBienDongService.UpdateLoaiLyDoBienDong(lyDoBienDong);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region DiaBan
        public IList<DiaBanModel> GetAllDiaBans()
        {
            var query = _diaBanService.GetDiaBans(QuocGiaId: 33);// lấy quốc gia mặc định là việt nam
            if (query == null)
                return null;
            List<DiaBanModel> rs = query.Select(c => new DiaBanModel()
            {
                MA = c.MA,
                TEN = c.TEN,
                MA_CHA = c.MA_CHA,
                LOAI_DIA_BAN_ID = c.LOAI_DIA_BAN_ID,
                TRANG_THAI_ID = c.TRANG_THAI_ID
            }).ToList();
            return rs;
        }
        public MessageReturn UpdateDiaBan(DiaBanModel model, NguoiDung currentUser)
        {
            if (!(model.DB_ID > 0))
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<DiaBanModel>() { model });
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                model.Error = "MA null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA null", new List<DiaBanModel>() { model });
            }

            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<DiaBanModel>() { model });
            }
            decimal? PARENT_ID = null;
            decimal? QUOC_GIA_ID = null;
            if (model.PARENT_ID > 0)
            {
                var PARENT = _diaBanService.GetDiaBanById(model.PARENT_ID.Value);
                if (PARENT != null)
                {
                    PARENT_ID = PARENT.ID;
                    model.MA_CHA = PARENT.MA;
                }
                else
                {
                    model.Error = "PARENT_ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "PARENT_ID not exist", new List<DiaBanModel>() { model });
                }
            }
            if (model.QUOC_GIA_ID > 0)
            {
                QuocGia QuocGia = _quocGiaService.GetQuocGiaById(model.QUOC_GIA_ID.Value);
                if (QuocGia != null)
                    QUOC_GIA_ID = QuocGia.ID;
                else
                {
                    model.Error = "QUOC_GIA_ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "QUOC_GIA_ID not exist", new List<DiaBanModel>() { model });
                }
            }
            if (model.ID > 0)
            {
                var entity = _diaBanService.GetDiaBanById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MO_TA = model.MO_TA;
                    entity.MA_CHA = model.MA_CHA;
                    entity.PARENT_ID = PARENT_ID;
                    entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    entity.LOAI_DIA_BAN_ID = model.LOAI_DIA_BAN_ID;
                    entity.QUOC_GIA_ID = QUOC_GIA_ID;
                    _diaBanService.UpdateDiaBan(entity);
                }
                else
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<DiaBanModel>() { model });
                }
            }

            else
            {

                DiaBan entity = model.ToEntity<DiaBan>();
                entity.ID = 0;
                entity.PARENT_ID = PARENT_ID;
                entity.QUOC_GIA_ID = QUOC_GIA_ID;
                _diaBanService.InsertDiaBan(entity);
                model.ID = (long)entity.ID;
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<DiaBanModel>() { model });
        }
        public MessageReturn UpdateDiaBans(List<DiaBanModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<DiaBan> LstAdd = new List<DiaBan>();
            List<DiaBan> LstEdit = new List<DiaBan>();
            List<DiaBan> diaBans = new List<DiaBan>();
            foreach (var model in ListModel)
            {
                if (!(model.DB_ID > 0))
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.MA))
                {
                    model.Error = "MA null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                decimal? PARENT_ID = null;
                decimal? QUOC_GIA_ID = null;
                if (model.PARENT_ID > 0)
                {
                    var PARENT = _diaBanService.GetDiaBanById(model.PARENT_ID.Value);
                    if (PARENT != null)
                    {
                        PARENT_ID = PARENT.ID;
                        model.MA_CHA = PARENT.MA;
                    }
                    else
                    {
                        model.Error = "PARENT_ID not exist";
                        TotalErr++;
                        continue;
                    }
                }
                if (model.QUOC_GIA_ID > 0)
                {
                    QuocGia QuocGia = _quocGiaService.GetQuocGiaById(model.QUOC_GIA_ID.Value);
                    if (QuocGia != null)
                        QUOC_GIA_ID = QuocGia.ID;
                    else
                    {
                        model.Error = "QUOC_GIA_ID not exist";
                        TotalErr++;
                        continue;
                    }
                }
                var entity = _diaBanService.GetDiaBanByMa(model.MA);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    entity.MA_CHA = model.MA_CHA;
                    entity.PARENT_ID = PARENT_ID;
                    entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    entity.QUOC_GIA_ID = QUOC_GIA_ID;
                    entity.MO_TA = model.MO_TA;
                    entity.DB_ID = model.DB_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    model.ID = 0;
                    entity = model.ToEntity<DiaBan>();
                    entity.PARENT_ID = PARENT_ID;
                    entity.QUOC_GIA_ID = QUOC_GIA_ID;
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _diaBanService.InsertDiaBan(LstAdd);
                diaBans.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _diaBanService.UpdateDiaBan(LstEdit);
                diaBans.AddRange(LstEdit);
            }
            foreach (var item in ListModel)
            {
                DiaBan diaBan = diaBans.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (diaBan == null)
                    continue;
                item.ID = (long)diaBan.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {diaBans.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }

        }
        public MessageReturn DeleteDiaBan(decimal ID = 0, NguoiDung currentUser = null)
        {
            DiaBan diaBan = _diaBanService.GetDiaBanById(Id: ID);
            try
            {
                if (diaBan.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                diaBan.DB_ID = null;
                _diaBanService.UpdateDiaBan(diaBan);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.Xoa, "Xóa địa bàn", 0, "DiaBan", diaBan);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID error");
            }
        }
        #endregion
        #region LoaiTaiSan
        public IList<LoaiTaiSanModel> GetAllLoaiTaiSanNhaNuocs()
        {
            var query = _loaiTaiSanService.GetAllLoaiTaiSans().Where(m => m.CHE_DO_HAO_MON_ID == 23);
            if (query == null)
                return null;
            List<LoaiTaiSanModel> ListLoaiTaiSanModel = new List<LoaiTaiSanModel>();
            foreach (var item in query)
            {
                LoaiTaiSanModel loaiTaiSanModel = new LoaiTaiSanModel();
                loaiTaiSanModel.ID = (long)item.ID;
                loaiTaiSanModel.TEN = item.TEN;
                loaiTaiSanModel.MA = item.MA;
                loaiTaiSanModel.MO_TA = item.MO_TA;
                loaiTaiSanModel.DB_ID = (int?)item.DB_ID;
                if (item.PARENT_ID > 0)
                {
                    loaiTaiSanModel.DB_PARENT_ID = (int?)item.LoaiTaiSanCha.DB_ID;

                }
                else
                {
                    loaiTaiSanModel.DB_PARENT_ID = (int)enumIdNhomTaiSanKho.CoQuanToChuc;
                }

                loaiTaiSanModel.DB_CHE_DO_HAO_MON_ID = item.CheDoHaoMon.DB_ID;
                loaiTaiSanModel.CHE_DO_HAO_MON_ID = item.CHE_DO_HAO_MON_ID;
                ListLoaiTaiSanModel.Add(loaiTaiSanModel);
            }
            // loại tài sản vô hình
            var LoaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetAllLoaiTaiSanVoHinhCons().Where(m => m.DON_VI_ID != null);
            foreach (var c in LoaiTaiSanVoHinh)
            {
                LoaiTaiSanModel model = new LoaiTaiSanModel();
                model.ID = Convert.ToInt64(c.ID);
                model.TEN = c.TEN;
                model.MA = c.MA;
                model.MO_TA = c.MO_TA;
                //model.DB_MA = c.OA_MA == null ? c.NPA_MA : c.OA_MA,
                // model.MA_CHA = c.PARENT_ID != null ? _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(c.PARENT_ID.Value).MA : null;
                model.DON_VI = c.DonVi != null ? new NguoiDungDonViModel()
                {
                    MA_DON_VI = c.DonVi.MA,
                    MA_DVQHNS = c.DonVi.MA_DVQHNS
                }.toStringJson() : null;
                model.MA_NHOM_TAI_SAN = "001";
                model.DB_CHE_DO_HAO_MON_ID = c.CheDoHaoMon != null ? c.CheDoHaoMon.DB_ID : null;
                ListLoaiTaiSanModel.Add(model);
            }
            return ListLoaiTaiSanModel;
        }
        public MessageReturn UpdateLoaiTaiSan(LoaiTaiSanModel model, NguoiDung currentUser)
        {
            if (model.DB_ID == 0 || model.DB_ID == null)
            {
                model.Error = "DB_ID null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<LoaiTaiSanModel>() { model });
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<LoaiTaiSanModel>() { model });
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                model.Error = "MA null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA null", new List<LoaiTaiSanModel>() { model });
            }
            else
            {
                LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(model.MA);
                if (loaiTaiSan != null)
                {
                    model.Error = "MA Already exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "MA Already exist", new List<LoaiTaiSanModel>() { model });
                }
            }
            if (string.IsNullOrEmpty(model.MA_NHOM_TAI_SAN))
            {
                model.Error = "MA_NHOM_TAI_SAN null";
                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_NHOM_TAI_SAN null", new List<LoaiTaiSanModel>() { model });
            }
            decimal? PARENT_ID = null;
            if (model.DB_PARENT_ID > 0)
            {
                var PARENT = _loaiTaiSanService.GetLoaiTaiSanByIdDb(model.DB_PARENT_ID.Value);
                if (PARENT != null)
                    PARENT_ID = PARENT.ID;
                else
                {
                    model.Error = "PARENT_ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "PARENT_ID not exist", new List<LoaiTaiSanModel>() { model });
                }

            }
            decimal? CHE_DO_HAO_MON_ID = null;
            if (model.CHE_DO_HAO_MON_ID > 0)
            {
                var CheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(model.CHE_DO_HAO_MON_ID.Value);
                if (CheDoHaoMon != null)
                    CHE_DO_HAO_MON_ID = CheDoHaoMon.ID;
                else
                {
                    model.Error = "CHE_DO_HAO_MON_ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "CHE_DO_HAO_MON_ID not exist", new List<LoaiTaiSanModel>() { model });
                }
            }
            LoaiTaiSan entity = new LoaiTaiSan();

            if (model.ID == 0)
            {
                entity = model.ToEntity<LoaiTaiSan>();
                entity.PARENT_ID = PARENT_ID;
                entity.ID = 0;
                entity.DB_ID = model.DB_ID;
                // chế độ hao mòn
                entity.CHE_DO_HAO_MON_ID = CHE_DO_HAO_MON_ID;
                _loaiTaiSanService.InsertLoaiTaiSan(entity);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Loại tài sản", 0, "LoaiTaiSan", model);
                model.ID = (long)entity.ID;
            }
            else
            {
                entity = _loaiTaiSanService.GetLoaiTaiSanById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MO_TA = model.MO_TA;
                    entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID ?? (decimal)enumLOAI_HINH_TAI_SAN.KHAC;
                    entity.MO_TA = model.MO_TA;
                    entity.PARENT_ID = PARENT_ID;
                    entity.DB_ID = model.DB_ID;
                    entity.CHE_DO_HAO_MON_ID = CHE_DO_HAO_MON_ID;
                    entity.PARENT_ID = PARENT_ID;
                    _loaiTaiSanService.UpdateLoaiTaiSan(entity);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại tài sản", 0, "LoaiTaiSan", model);
                }
                else
                {
                    model.Error = "ID not exist";
                    return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<LoaiTaiSanModel>() { model });
                }
            }
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", entity.ID.ToString() + model.MA_NHOM_TAI_SAN);
        }
        public MessageReturn UpdateListLoaiTaiSan(List<LoaiTaiSanModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<LoaiTaiSan> LstAdd = new List<LoaiTaiSan>();
            List<LoaiTaiSan> LstEdit = new List<LoaiTaiSan>();
            List<LoaiTaiSan> loaiTaiSans = new List<LoaiTaiSan>();
            foreach (var model in ListModel)
            {

                if (string.IsNullOrEmpty(model.MA))
                {
                    model.Error = "MA null";
                    TotalErr++;
                    continue;
                }
                else
                {
                    LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(model.MA);
                    if (loaiTaiSan != null)
                    {
                        model.Error = "MA Already exist";
                        TotalErr++;
                        continue;
                    }
                }
                if (model.DB_ID == 0 || model.DB_ID == null)
                {
                    model.Error = "DB_ID null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN))
                {
                    model.Error = "TEN null";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.MA_NHOM_TAI_SAN))
                {
                    model.Error = "MA_NHOM_TAI_SAN null";
                    TotalErr++;
                    continue;
                }
                decimal? PARENT_ID = null;
                if (model.DB_PARENT_ID > 0)
                {
                    var PARENT = _loaiTaiSanService.GetLoaiTaiSanByIdDb(model.DB_PARENT_ID.Value);
                    if (PARENT != null)
                        PARENT_ID = PARENT.ID;
                    else
                    {
                        model.Error = "DB_PARENT_ID not exist";
                        TotalErr++;
                        continue;
                    }
                }
                decimal? CHE_DO_HAO_MON_ID = null;
                if (model.CHE_DO_HAO_MON_ID > 0)
                {
                    var CheDoHaoMon = _cheDoHaoMonService.GetCheDoHaoMonById(model.CHE_DO_HAO_MON_ID.Value);
                    if (CheDoHaoMon != null)
                        CHE_DO_HAO_MON_ID = CheDoHaoMon.ID;
                    else
                    {
                        model.Error = "CHE_DO_HAO_MON_ID not exist";
                        TotalErr++;
                        continue;
                    }
                }

                if (model.ID > 0)
                {
                    decimal Id_QLTSC = model.ID;
                    var entity = _loaiTaiSanService.GetLoaiTaiSanById(Id_QLTSC);
                    if (entity != null)
                    {
                        entity.TEN = model.TEN;
                        entity.MA = model.MA;
                        entity.MO_TA = model.MO_TA;
                        entity.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID ?? (decimal)enumLOAI_HINH_TAI_SAN.KHAC;
                        entity.MO_TA = model.MO_TA;
                        entity.PARENT_ID = PARENT_ID;
                        entity.CHE_DO_HAO_MON_ID = CHE_DO_HAO_MON_ID;
                        entity.DB_ID = model.DB_ID;
                        entity.HM_THOI_HAN_SU_DUNG = model.HM_THOI_HAN_SU_DUNG;
                        entity.HM_TY_LE = model.HM_TY_LE;
                        entity.DON_VI_TINH = model.DON_VI_TINH;
                        LstEdit.Add(entity);
                    }
                }
                else
                {
                    LoaiTaiSan entity = model.ToEntity<LoaiTaiSan>();
                    entity.PARENT_ID = PARENT_ID;
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _loaiTaiSanService.InsertLoaiTaiSan(LstAdd);
                loaiTaiSans.AddRange(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Loại tài sản", 0, "LoaiTaiSan", LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _loaiTaiSanService.UpdateLoaiTaiSan(LstEdit);
                loaiTaiSans.AddRange(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật loại tài sản", 0, "LoaiTaiSan", LstEdit);
            }
            foreach (var item in ListModel)
            {
                if (item.DB_ID > 0)
                {
                    LoaiTaiSan loaiTaiSan = loaiTaiSans.Where(m => m.DB_ID == item.DB_ID).FirstOrDefault();
                    item.ID = (long)loaiTaiSan.ID;
                }

            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {loaiTaiSans.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success Done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteLoaiTaiSan(decimal ID)
        {
            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(Id: ID);
            try
            {
                if (loaiTaiSan.DB_ID_JSON == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                loaiTaiSan.DB_ID_JSON = null;
                _loaiTaiSanService.UpdateLoaiTaiSan(loaiTaiSan);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region ChucDanh
        public IList<ChucDanhModel> GetAllChucDanhs()
        {
            var query = _chucDanhService.GetAllChucDanhs();
            if (query == null)
                return null;
            return query.Select(c => c.ToModel<ChucDanhModel>()).ToList();
        }
        public MessageReturn UpdateChucDanh(ChucDanhModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.MA_CHUC_DANH))
                return MessageReturn.CreateErrorMessage("MA_CHUC_DANH invalid");
            if (string.IsNullOrEmpty(model.TEN_CHUC_DANH))
                return MessageReturn.CreateErrorMessage("TEN_CHUC_DANH invalid");
            var entity = _chucDanhService.GetChucDanhByMa(model.MA_CHUC_DANH);
            if (entity != null)
            {
                entity.TEN_CHUC_DANH = model.TEN_CHUC_DANH;
                entity.MO_TA = model.MO_TA;
                entity.KHOI_DON_VI_ID = model.KHOI_DON_VI_ID;
                entity.DINH_MUC = model.DINH_MUC;
                _chucDanhService.UpdateChucDanh(entity);
            }
            else
            {
                model.ID = 0;
                entity = model.ToEntity<ChucDanh>();
                _chucDanhService.InsertChucDanh(entity);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn UpdateChucDanhs(List<ChucDanhModel> ListModel, NguoiDung currentUser)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<ChucDanh> LstAdd = new List<ChucDanh>();
            List<ChucDanh> LstEdit = new List<ChucDanh>();
            foreach (var model in ListModel)
            {
                if (string.IsNullOrEmpty(model.MA_CHUC_DANH))
                {
                    err += "\nMA_CHUC_DANH invalid";
                    TotalErr++;
                    continue;
                }
                if (string.IsNullOrEmpty(model.TEN_CHUC_DANH))
                {
                    err += $"\nMA_CHUC_DANH: {model.MA_CHUC_DANH}\t\tTEN_CHUC_DANH invalid";
                    TotalErr++;
                    continue;
                }
                var entity = _chucDanhService.GetChucDanhByMa(model.MA_CHUC_DANH);
                if (entity != null)
                {
                    entity.TEN_CHUC_DANH = model.TEN_CHUC_DANH;
                    entity.MO_TA = model.MO_TA;
                    entity.KHOI_DON_VI_ID = model.KHOI_DON_VI_ID;
                    entity.DINH_MUC = model.DINH_MUC;
                    LstEdit.Add(entity);
                }
                else
                {
                    model.ID = 0;
                    entity = model.ToEntity<ChucDanh>();
                    LstAdd.Add(entity);
                }
            }

            if (LstAdd.Count > 0)
            {
                _chucDanhService.InsertChucDanh(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _chucDanhService.UpdateChucDanh(LstEdit);
                TotalSuc += LstEdit.Count();
            }

            return MessageReturn.CreateSuccessMessage($"Total {TotalSuc} success\nTotal {TotalErr} error" + (TotalErr > 0 ? $"\nList error:\n{err}" : ""));
        }
        #endregion
        #region DuAn
        public IList<DuAnModel> GetAllDuAns()
        {
            var query = _duAnService.GetAllDuAns();
            if (query == null)
                return null;
            return query.Select(c => c.ToModel<DuAnModel>()).ToList();
        }
        public MessageReturn UpdateDuAn(DuAnModel model, NguoiDung currentUser)
        {
            if (string.IsNullOrEmpty(model.MA))
                return MessageReturn.CreateErrorMessage("MA invalid");
            if (string.IsNullOrEmpty(model.TEN))
                return MessageReturn.CreateErrorMessage("TEN invalid");
            Decimal? DON_VI_ID = null;
            Decimal? DON_VI_CU_ID = null;
            var entity = _duAnService.GetDuAnByMA(model.MA);
            if (entity != null)
            {
                entity.TEN = model.TEN;
                entity.LOAI_DU_AN_ID = model.LOAI_DU_AN_ID;
                entity.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
                entity.NGAY_KET_THUC = model.NGAY_KET_THUC;
                entity.TONG_KINH_PHI = model.TONG_KINH_PHI;
                entity.HINH_THUC_ID = model.HINH_THUC_ID;
                entity.SO_QUYET_DINH_PHE_DUYET = model.SO_QUYET_DINH_PHE_DUYET;
                entity.CHU_DAU_TU = model.CHU_DAU_TU;
                entity.DIA_DIEM = model.DIA_DIEM;
                entity.NGUON_VON = model.NGUON_VON;
                entity.GHI_CHU = model.GHI_CHU;
                entity.NGUON_NS = model.NGUON_NS;
                entity.NGUON_ODA = model.NGUON_ODA;
                entity.NGUON_VIEN_TRO = model.NGUON_VIEN_TRO;
                entity.NGUON_KHAC = model.NGUON_KHAC;
                entity.KIEU = model.KIEU;
                entity.CO_QUAN_TAI_CHINH = model.CO_QUAN_TAI_CHINH;
                entity.MA_LOAI_DU_AN = model.MA_LOAI_DU_AN;
                entity.MA_NHOM_DU_AN = model.MA_NHOM_DU_AN;
                entity.TEN_NHOM_DU_AN = model.TEN_NHOM_DU_AN;
                entity.MA_HT = model.MA_HT;
                entity.TEN_HT = model.TEN_HT;
                entity.HT_QUANLY = model.HT_QUANLY;
                entity.HIEU_LUC = model.HIEU_LUC;
                //entity.MA_DVQHNS = model.MA_DVQHNS;
                entity.MA_DU_AN_CU = model.MA_DU_AN_CU;
                entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                entity.DON_VI_ID = DON_VI_ID;
                entity.DON_VI_CU_ID = DON_VI_CU_ID;
                _duAnService.UpdateDuAn(entity);
            }
            else
            {
                model.ID = 0;
                entity = model.ToEntity<DuAn>();
                _duAnService.InsertDuAn(entity);
            }
            return MessageReturn.CreateSuccessMessage("Success done");
        }
        public MessageReturn UpdateDuAns(List<DuAnModel> ListModel, NguoiDung currentUser)
        {
            string err = "";
            int TotalErr = 0;
            int TotalSuc = 0;
            List<DuAn> LstAdd = new List<DuAn>();
            List<DuAn> LstEdit = new List<DuAn>();
            List<DuAn> duAns = new List<DuAn>();
            foreach (var model in ListModel)
            {
                var entity = _duAnService.GetDuAnById(model.ID);
                if (entity != null)
                {
                    entity = model.ToEntity(entity);
                    //entity.DON_VI_CU_ID = DON_VI_CU_ID;
                    LstEdit.Add(entity);
                }
                else
                {
                    model.ID = 0;
                    entity = model.ToEntity<DuAn>();
                    LstAdd.Add(entity);
                }
            }
            if (LstAdd.Count > 0)
            {
                _duAnService.InsertDuAn(LstAdd);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "DuAn", 0, "DuAn", LstAdd);
                duAns.AddRange(LstAdd);
                TotalSuc += LstAdd.Count();
            }
            if (LstEdit.Count > 0)
            {
                _duAnService.UpdateDuAn(LstEdit);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "DuAn", 0, "DuAn", LstEdit);
                duAns.AddRange(LstAdd);
            }
            foreach (var item in ListModel)
            {
                DuAn duAn = duAns.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (duAn == null)
                    continue;
                item.ID = (long)duAn.ID;
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {duAns.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteDuAn(decimal ID = 0, NguoiDung currentUser = null)
        {
            DuAn duAn = _duAnService.GetDuAnById(Id: ID);
            try
            {
                if (duAn.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                duAn.DB_ID = null;
                _duAnService.UpdateDuAn(duAn);
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.Xoa, "Xóa địa bàn", 0, "DuAn", duAn);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID error");
            }
        }
        #endregion
        #region Đơn vị bộ phận
        public IList<DonViBoPhanModel> GetAllDonViBoPhan()
        {
            var ListDonViBoPhan = _donViBoPhanService.GetDonViBoPhans().ToList();
            return ListDonViBoPhan.Select(m => m.ToModel<DonViBoPhanModel>()).ToList();
        }
        public MessageReturn UpdateListDonViBoPhan(List<DonViBoPhanModel> ListModel, NguoiDung currentUser)
        {
            List<DonViBoPhanModel> ListError = new List<DonViBoPhanModel>();
            List<DonViBoPhanModel> ListSucc = new List<DonViBoPhanModel>();
            foreach (var item in ListModel)
            {
                //tạm thời để thêm check DB_GUID. 1 trong 2 phải # null
                if (item.DB_ID == null && item.DB_GUID == null)
                {
                    item.Error = "DB_ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                if (item.PARENT_ID > 0)
                {
                    DonViBoPhan boPhanCha = _donViBoPhanService.GetDonViBoPhanById(item.PARENT_ID.Value);
                    if (boPhanCha == null)
                    {
                        item.Error = "PARENT_ID not exist";
                        ListError.Add(item);
                    }
                }
                if (item.DON_VI_ID == null || item.DON_VI_ID == 0)
                {
                    item.Error = "DON_VI_ID null";
                    ListError.Add(item);
                    continue;
                }
                else
                {
                    DonVi donVi = _donViService.GetDonViById(item.DON_VI_ID.Value);
                    if (donVi == null)
                    {
                        item.Error = "DON_VI_ID not exist";
                        ListError.Add(item);
                        continue;
                    }
                    else
                    {
                        var checkmatrung = _donViBoPhanService.GetDonViBoPhans(DonViId: item.DON_VI_ID).Any(c => c.MA == item.MA);
                        if (checkmatrung)
                        {
                            item.Error = "MA already exists";
                            ListError.Add(item);
                            continue;
                        }
                    }
                }
                if (item.ID > 0)
                {
                    DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(item.ID);
                    if (donViBoPhan == null)
                    {
                        item.Error = "ID not exist";
                        ListError.Add(item);
                        continue;
                    }
                    else
                    {
                        donViBoPhan.MA = item.MA;
                        donViBoPhan.TEN = item.TEN;
                        donViBoPhan.DIA_CHI = item.DIA_CHI;
                        donViBoPhan.DIEN_THOAI = item.DIEN_THOAI;
                        donViBoPhan.FAX = item.FAX;
                        donViBoPhan.DON_VI_ID = item.DON_VI_ID;
                        donViBoPhan.PARENT_ID = item.PARENT_ID;
                        _donViBoPhanService.UpdateDonViBoPhan(donViBoPhan);
                        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật đơn vị bộ phận", item.ID, "DonViBoPhan", item);
                    }
                }
                else
                {
                    DonViBoPhan donViBoPhan = item.ToEntity<DonViBoPhan>();
                    _donViBoPhanService.InsertDonViBoPhan(donViBoPhan, false);
                    item.ID = (long)donViBoPhan.ID;
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục Đơn vị bộ phận", item.ID, "DonViBoPhan", item);
                }
                ListSucc.Add(item);
            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListSucc
                };
            }
        }
        public MessageReturn DeleteDonViBoPhan(decimal ID = 0)
        {
            DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanById(Id: ID);
            try
            {
                if (donViBoPhan == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                _donViBoPhanService.DeleteDonViBoPhan(donViBoPhan);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }
        }
        #endregion
        #region Loại tài sản đơn vị
        public MessageReturn UpdateListLoaiTaiSanDonVi(List<LoaiTaiSanDonViModel> ListLoaiTaiSanDonViModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            List<LoaiTaiSanDonVi> ListEdit = new List<LoaiTaiSanDonVi>();
            List<LoaiTaiSanDonVi> ListAdd = new List<LoaiTaiSanDonVi>();
            List<LoaiTaiSanDonVi> listResult = new List<LoaiTaiSanDonVi>();
            foreach (var model in ListLoaiTaiSanDonViModel)
            {
                if (model.ID > 0)
                {
                    LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(model.ID);
                    loaiTaiSanDonVi.TEN = model.TEN;
                    loaiTaiSanDonVi.MA = model.MA;
                    loaiTaiSanDonVi.DB_ID = model.DB_ID;
                    loaiTaiSanDonVi.PARENT_ID = model.PARENT_ID;
                    loaiTaiSanDonVi.HM_TY_LE = model.HM_TY_LE;
                    loaiTaiSanDonVi.HM_THOI_HAN_SU_DUNG = model.HM_TY_LE;
                    loaiTaiSanDonVi.CHE_DO_HAO_MON_ID = model.CHE_DO_HAO_MON_ID;
                    loaiTaiSanDonVi.KH_THOI_HAN_SU_DUNG = model.KH_THOI_HAN_SU_DUNG;
                    loaiTaiSanDonVi.KH_TY_LE = model.KH_TY_LE;
                    loaiTaiSanDonVi.DON_VI_ID = model.DON_VI_ID;
                    loaiTaiSanDonVi.DON_VI_TINH = model.DON_VI_TINH;
                    _loaiTaiSanDonViServices.UpdateLoaiTaiSanVoHinh(loaiTaiSanDonVi);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại tài sản đơn vị", 0, "LoaiTaiSanDonVi",
                        loaiTaiSanDonVi);
                    listResult.Add(loaiTaiSanDonVi);
                }
                else
                {
                    var entity = model.ToEntity<LoaiTaiSanDonVi>();
                    entity.ID = 0;

                    ListAdd.Add(entity);
                }
            }
            if (ListAdd.Count > 0)
            {
                _loaiTaiSanDonViServices.InsertLoaiTaiSanVoHinh(ListAdd);
                if (currentUser == null)
                {
                    currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
                }
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Loại tài sản đơn vị", 0, "LoaiTaiSanDonVi", ListAdd);
                listResult.AddRange(ListAdd);
            }
            foreach (var item in ListLoaiTaiSanDonViModel)
            {
                var quocgia = listResult.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
                if (quocgia == null)
                    continue;
                item.ID = (long)quocgia.ID;
            }

            return new MessageReturn()
            {
                Code = MessageReturn.SUCCESS_CODE,
                ObjectInfo = listResult,
                Message = "Success done"
            };

        }
        public MessageReturn DeleteLoaiTaiSanDonVi(decimal ID = 0)
        {
            LoaiTaiSanDonVi loaiTaiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(Id: ID);
            try
            {
                if (loaiTaiSanDonVi.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                loaiTaiSanDonVi.DB_ID = null;
                _loaiTaiSanDonViServices.UpdateLoaiTaiSanVoHinh(loaiTaiSanDonVi);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Danh mục dùng chung
        public MessageReturn UpdateListDMDC_DiaBan(List<DMDC_DiaBanModel> ListModel, NguoiDung currentUser)
        {
            // List<DMDC_DiaBanModel> ListSucc = new List<DMDC_DiaBanModel>();
            List<DMDC_DiaBanModel> ListError = new List<DMDC_DiaBanModel>();
            List<DMDC_DiaBanModel> ListSucc = new List<DMDC_DiaBanModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA_T))
                {
                    item.Error = "MA_T null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA_DB))
                {
                    item.Error = "MA_DB null";
                    ListError.Add(item);
                    continue;
                }
                if (item.LOAI == 0)
                {
                    item.Error = "LOAI null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA_THAMCHIEU))
                {
                    item.Error = "MA_THAMCHIEU null";
                    ListError.Add(item);
                    continue;
                }
                if (item.ID_CHA > 0)
                {
                    DMDC_DiaBan diaBanCha = _dMDC_DiaBanService.GetDMDC_DiaBanById(item.ID_CHA.Value);
                    if (diaBanCha == null)
                    {
                        item.Error = "ID_CHA not exist";
                        ListError.Add(item);
                    }
                }
                DMDC_DiaBan diaban = _dMDC_DiaBanService.GetDMDC_DiaBanById(item.ID);
                if (diaban == null)
                {
                    diaban = item.ToEntity<DMDC_DiaBan>();
                    _dMDC_DiaBanService.InsertDMDC_DiaBan(diaban);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Địa bàn", item.ID, "DMDC_DiaBan", item);
                }
                else
                {
                    diaban = item.ToEntity(diaban);
                    _dMDC_DiaBanService.UpdateDMDC_DiaBan(diaban);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Địa bàn", item.ID, "DMDC_DiaBan", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn UpdateListDMDC_DonViDuAn(List<DMDC_DonViDuAnModel> ListModel, NguoiDung currentUser)
        {

            List<DMDC_DonViDuAnModel> ListError = new List<DMDC_DonViDuAnModel>();
            List<DMDC_DonViDuAnModel> ListSucc = new List<DMDC_DonViDuAnModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                DMDC_DonViDuAn donViDuAn = _dMDC_DonViDuAnService.GetDMDC_DonViDuAnById(item.ID);
                if (donViDuAn == null)
                {
                    donViDuAn = item.ToEntity<DMDC_DonViDuAn>();
                    _dMDC_DonViDuAnService.InsertDMDC_DonViDuAn(donViDuAn);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Đơn vị dự án", item.ID, "DMDC_DonViDuAn", item);
                }
                else
                {
                    donViDuAn = item.ToEntity(donViDuAn);
                    _dMDC_DonViDuAnService.UpdateDMDC_DonViDuAn(donViDuAn);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Đơn vị dự án", item.ID, "DMDC_DonViDuAn", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn UpdateListDMDC_DonViNganSach(List<DMDC_DonViNganSachModel> ListModel, NguoiDung currentUser)
        {
            List<DMDC_DonViNganSachModel> ListError = new List<DMDC_DonViNganSachModel>();
            List<DMDC_DonViNganSachModel> ListSucc = new List<DMDC_DonViNganSachModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                DMDC_DonViNganSach donViNganSach = _dMDC_DonViNganSachService.GetDMDC_DonViNganSachById(item.ID);
                if (donViNganSach == null)
                {
                    donViNganSach = item.ToEntity<DMDC_DonViNganSach>();
                    _dMDC_DonViNganSachService.InsertDMDC_DonViNganSach(donViNganSach);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Đơn vị Ngân sách", item.ID, "DMDC_DonViNganSach", item);
                }
                else
                {
                    donViNganSach = item.ToEntity(donViNganSach);
                    _dMDC_DonViNganSachService.UpdateDMDC_DonViNganSach(donViNganSach);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Đơn vị Ngân sách", item.ID, "DMDC_DonViNganSach", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn UpdateListDMDC_DuAn(List<DMDC_DuAnModel> ListModel, NguoiDung currentUser)
        {
            List<DMDC_DuAnModel> ListError = new List<DMDC_DuAnModel>();
            List<DMDC_DuAnModel> ListSucc = new List<DMDC_DuAnModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.CQTC_MA))
                {
                    item.Error = "CQTC_MA null";
                    ListError.Add(item);
                    continue;
                }
                DMDC_DuAn duAn = _dMDC_DuAnService.GetDMDC_DuAnById(item.ID);
                if (duAn == null)
                {
                    duAn = item.ToEntity<DMDC_DuAn>();
                    _dMDC_DuAnService.InsertDMDC_DuAn(duAn);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Dự án", item.ID, "DMDC_DuAn", item);
                }
                else
                {
                    duAn = item.ToEntity(duAn);
                    _dMDC_DuAnService.UpdateDMDC_DuAn(duAn);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Dự án", item.ID, "DMDC_DuAn", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn UpdateListDMDC_QuocGia(List<DMDC_QuocGiaModel> ListModel, NguoiDung currentUser)
        {
            List<DMDC_QuocGiaModel> ListError = new List<DMDC_QuocGiaModel>();
            List<DMDC_QuocGiaModel> ListSucc = new List<DMDC_QuocGiaModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN_TA))
                {
                    item.Error = "TEN_TA null";
                    ListError.Add(item);
                    continue;
                }
                DMDC_QuocGia quocGia = _dMDC_QuocGiaService.GetDMDC_QuocGiaById(item.ID);
                if (quocGia == null)
                {
                    quocGia = item.ToEntity<DMDC_QuocGia>();
                    _dMDC_QuocGiaService.InsertDMDC_QuocGia(quocGia);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Quốc gia", item.ID, "DMDC_QuocGia", item);
                }
                else
                {
                    quocGia = item.ToEntity(quocGia);
                    _dMDC_QuocGiaService.UpdateDMDC_QuocGia(quocGia);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Quốc gia", item.ID, "DMDC_QuocGia", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public MessageReturn UpdateListDMDC_ToChucNganSach(List<DMDC_ToChucNganSachModel> ListModel, NguoiDung currentUser)
        {
            List<DMDC_ToChucNganSachModel> ListError = new List<DMDC_ToChucNganSachModel>();
            List<DMDC_ToChucNganSachModel> ListSucc = new List<DMDC_ToChucNganSachModel>();
            foreach (var item in ListModel)
            {
                if (item.ID == 0)
                {
                    item.Error = "ID null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListError.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListError.Add(item);
                    continue;
                }

                DMDC_ToChucNganSach toChucNganSach = _dMDC_ToChucNganSachService.GetDMDC_ToChucNganSachById(item.ID);
                if (toChucNganSach == null)
                {
                    toChucNganSach = item.ToEntity<DMDC_ToChucNganSach>();
                    _dMDC_ToChucNganSachService.InsertDMDC_ToChucNganSach(toChucNganSach);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Tổ chức ngân sách", item.ID, "DMDC_ToChucNganSach", item);
                }
                else
                {
                    toChucNganSach = item.ToEntity(toChucNganSach);
                    _dMDC_ToChucNganSachService.UpdateDMDC_ToChucNganSach(toChucNganSach);
                    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Tổ chức ngân sách", item.ID, "DMDC_ToChucNganSach", item);
                }
                ListSucc.Add(item);

            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        #endregion
        #region Danh mục Nhãn xe
        public MessageReturn UpdateNhanXe(List<NhanXeModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            int TotalErr = 0;
            List<NhanXe> LstAdd = new List<NhanXe>();
            List<NhanXe> LstEdit = new List<NhanXe>();
            List<NhanXe> nhanXes = new List<NhanXe>();
            foreach (var model in ListModel)
            {
                var entity = _nhanXeService.GetNhanXeById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;

                    _nhanXeService.UpdateNhanXe(entity);
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<NhanXe>();
                    entity.ID = 0;
                    _nhanXeService.UpdateNhanXe(entity);
                    model.ID = (long)entity.ID;
                }
            }
            if (LstAdd.Count > 0)
            {

                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Lý do biến động", 0, "LyDoBienDong", LstAdd);
                nhanXes.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật lý do biến động", 0, "LyDoBienDong", LstEdit);
                nhanXes.AddRange(LstEdit);
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {nhanXes.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteNhanXe(decimal ID = 0)
        {
            NhanXe nhanXe = _nhanXeService.GetNhanXeById(Id: ID);
            try
            {
                if (nhanXe.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                nhanXe.DB_ID = null;
                _nhanXeService.UpdateNhanXe(nhanXe);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Danh mục Dòng xe
        public MessageReturn UpdateDongXe(List<DongXeModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            int TotalErr = 0;
            List<DongXe> LstAdd = new List<DongXe>();
            List<DongXe> LstEdit = new List<DongXe>();
            List<DongXe> dongXes = new List<DongXe>();
            foreach (var model in ListModel)
            {
                var entity = _dongXeService.GetDongXeById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    _dongXeService.UpdateDongXe(entity);
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<DongXe>();
                    entity.ID = 0;
                    _dongXeService.UpdateDongXe(entity);
                    model.ID = (long)entity.ID;
                }
            }
            if (LstAdd.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Dòng xe", 0, "DongXe", LstAdd);
                dongXes.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dòng xe", 0, "DongXe", LstEdit);
                dongXes.AddRange(LstEdit);
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {dongXes.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteDongXe(decimal ID = 0)
        {
            NhanXe nhanXe = _nhanXeService.GetNhanXeById(Id: ID);
            try
            {
                if (nhanXe.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                nhanXe.DB_ID = null;
                _nhanXeService.UpdateNhanXe(nhanXe);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region Danh mục chức danh
        public MessageReturn UpdateChucDanh(List<ChucDanhModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            int TotalErr = 0;
            List<ChucDanh> LstAdd = new List<ChucDanh>();
            List<ChucDanh> LstEdit = new List<ChucDanh>();
            List<ChucDanh> chucDanhs = new List<ChucDanh>();
            foreach (var model in ListModel)
            {
                var entity = _chucDanhService.GetChucDanhById(model.ID);
                if (entity != null)
                {
                    entity.TEN_CHUC_DANH = model.TEN_CHUC_DANH;
                    entity.MA_CHUC_DANH = model.MA_CHUC_DANH;
                    entity.DINH_MUC = model.DINH_MUC;
                    entity.MO_TA = model.MO_TA;
                    entity.KHOI_DON_VI_ID = model.KHOI_DON_VI_ID;
                    _chucDanhService.UpdateChucDanh(entity);
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<ChucDanh>();
                    entity.ID = 0;
                    _chucDanhService.UpdateChucDanh(entity);
                    model.ID = (long)entity.ID;
                }
            }
            if (LstAdd.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Dòng xe", 0, "DongXe", LstAdd);
                chucDanhs.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dòng xe", 0, "DongXe", LstEdit);
                chucDanhs.AddRange(LstEdit);
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {chucDanhs.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteChucDanh(decimal ID = 0)
        {
            ChucDanh chucDanh = _chucDanhService.GetChucDanhById(Id: ID);
            try
            {
                if (chucDanh.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                chucDanh.DB_ID = null;
                _chucDanhService.UpdateChucDanh(chucDanh);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion
        #region hình thức mua sắm
        public MessageReturn UpdateHinhThucMuaSam(List<HinhThucMuaSamModel> ListModel, NguoiDung currentUser)
        {
            if (currentUser == null)
            {
                currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
            }
            int TotalErr = 0;
            List<HinhThucMuaSam> LstAdd = new List<HinhThucMuaSam>();
            List<HinhThucMuaSam> LstEdit = new List<HinhThucMuaSam>();
            List<HinhThucMuaSam> dongXes = new List<HinhThucMuaSam>();
            foreach (var model in ListModel)
            {
                var entity = _hinhThucMuaSamService.GetHinhThucMuaSamById(model.ID);
                if (entity != null)
                {
                    entity.TEN = model.TEN;
                    entity.MA = model.MA;
                    entity.DB_ID = model.DB_ID;
                    _hinhThucMuaSamService.UpdateHinhThucMuaSam(entity);
                    LstEdit.Add(entity);
                }
                else
                {
                    entity = model.ToEntity<HinhThucMuaSam>();
                    entity.ID = 0;
                    _hinhThucMuaSamService.UpdateHinhThucMuaSam(entity);
                    model.ID = (long)entity.ID;
                }
            }
            if (LstAdd.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới Dòng xe", 0, "DongXe", LstAdd);
                dongXes.AddRange(LstAdd);
            }
            if (LstEdit.Count > 0)
            {
                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dòng xe", 0, "DongXe", LstEdit);
                dongXes.AddRange(LstEdit);
            }
            if (TotalErr > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {dongXes.Count} success - Total {TotalErr} error",
                    ObjectInfo = ListModel
                };
            }
            else
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListModel
                };
            }
        }
        public MessageReturn DeleteHinhThucMuaSam(decimal ID = 0)
        {
            HinhThucMuaSam hinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamById(Id: ID);
            try
            {
                if (hinhThucMuaSam.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                hinhThucMuaSam.DB_ID = null;
                _hinhThucMuaSamService.UpdateHinhThucMuaSam(hinhThucMuaSam);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID not exist");
            }
        }
        #endregion

        #endregion
    }
}
