using GS.Core.Configuration;
using GS.Core;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.DMDC;
using GS.Services.HeThong;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using System.Collections.Generic;
using System.Linq;
using System;
using GS.NewAPI.Models.DanhMuc;
using GS.WebApi.Infrastructure.Mapper.Extensions;

namespace GS.NewAPI.Factories
{
    public class DanhMucModelFactory: IDanhMucModelFactory
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
        public DanhMucModelFactory(
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
        #region
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            var query = _quocGiaService.GetAllQuocGias();
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        //public MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser)
        //{
        //    if (string.IsNullOrEmpty(model.TEN))
        //    {
        //        model.Error = "TEN null";
        //        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<QuocGiaModel>() { model });
        //    }
        //    if (model.DB_ID == null)
        //    {
        //        model.Error = "DB_ID null";
        //        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<QuocGiaModel>() { model });
        //    }
        //    else
        //    {
        //        QuocGia quocGia = new QuocGia();
        //        if (model.ID == 0)
        //        {
        //            quocGia = model.ToEntity<QuocGia>();
        //            quocGia.ID = 0;
        //            //quocGia.MA = null;
        //            _quocGiaService.InsertQuocGia(quocGia);
        //            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", model);
        //            model.ID = (long)quocGia.ID;
        //            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
        //        }
        //        else
        //        {
        //            quocGia = _quocGiaService.GetQuocGiaDB(ID: model.ID);
        //            if (quocGia != null)// cập nhật
        //            {
        //                quocGia.TEN = model.TEN;
        //                quocGia.MO_TA = model.MO_TA;
        //                _quocGiaService.UpdateQuocGia(quocGia);
        //                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", model);
        //                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
        //            }
        //            else
        //            {
        //                model.Error = "ID not exist";
        //                return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<QuocGiaModel>() { model });
        //            }
        //        }
        //    }

        //}
        //public MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser)
        //{
        //    if (currentUser == null)
        //    {
        //        currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
        //    }
        //    // lọc các quốc gia không đủ điều kiện           
        //    int TotalErr = 0;
        //    int TotalSuc = 0;
        //    List<QuocGia> LstAdd = new List<QuocGia>();
        //    List<QuocGia> LstEdit = new List<QuocGia>();
        //    List<QuocGia> quocGias = new List<QuocGia>();
        //    foreach (var model in ListQuocGiaModel)
        //    {
        //        if (model.DB_ID == null)
        //        {
        //            model.Error = "DB_ID null";
        //            TotalErr++;
        //            continue;
        //        }
        //        if (string.IsNullOrEmpty(model.TEN))
        //        {
        //            model.Error = "TEN null";
        //            TotalErr++;
        //            continue;
        //        }
        //        if (model.ID > 0)
        //        {
        //            var entity = _quocGiaService.GetQuocGiaById(model.ID);
        //            if (entity == null)
        //            {
        //                model.Error = "ID not exist";
        //                continue;
        //            }
        //            else
        //            {
        //                //entity = model.ToEntity<QuocGia>();
        //                entity.TEN = model.TEN;
        //                entity.MA = model.MA;
        //                entity.DB_ID = model.DB_ID;
        //                LstEdit.Add(entity);
        //            }
        //        }
        //        else
        //        {
        //            var entity = model.ToEntity<QuocGia>();
        //            entity.ID = 0;
        //            LstAdd.Add(entity);
        //        }
        //    }
        //    if (LstAdd.Count > 0)
        //    {
        //        _quocGiaService.InsertListQuocGia(LstAdd);
        //        if (currentUser == null)
        //        {
        //            currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
        //        }
        //        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", LstAdd);
        //        quocGias.AddRange(LstAdd);
        //    }
        //    if (LstEdit.Count > 0)
        //    {
        //        _quocGiaService.UpdateListQuocGia(LstEdit);
        //        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", LstEdit);
        //        quocGias.AddRange(LstEdit);
        //    }
        //    foreach (var item in ListQuocGiaModel)
        //    {
        //        var quocgia = quocGias.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
        //        if (quocgia == null)
        //            continue;
        //        item.ID = (long)quocgia.ID;
        //    }
        //    if (TotalErr > 0)
        //    {
        //        return new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_PARTIAL_CODE,
        //            Message = $"Total {quocGias.Count} success - Total {TotalErr} error",
        //            ObjectInfo = ListQuocGiaModel
        //        };
        //    }
        //    else
        //    {
        //        return new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_CODE,
        //            ObjectInfo = quocGias,
        //            Message = "Success done"
        //        };
        //    }
        //}
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
        #endregion
    }
}
