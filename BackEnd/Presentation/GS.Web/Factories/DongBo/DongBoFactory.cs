using AutoMapper.Internal;
using Castle.DynamicProxy.Generators;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.Api.BaoCao;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.BaoCaos;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DB;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.HeThong;
using GS.Web.Models.ImportTaiSan;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Web.Factories.DongBo
{
    public partial class DongBoFactory : IDongBoFactory
    {
        #region field
        private readonly ITaiSanService _taiSanService;
        private readonly IDBTaiSanService _dBTaiSanService;
        private readonly IDonViService _donViService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly IHienTrangService _hienTrangService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViService;
        private readonly IGSAPIService _gSAPIService;
        private readonly GSConfig _gsConfig;
        private readonly IHoatDongService _hoatDongService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        private readonly IWorkContext _workContext;
        private readonly IDiaBanService _diaBanService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IDongXeService _dongXeService;
        private readonly INhanXeService _nhanXeService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IQueueProcessService _queueProcessService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly ILogQueueProcessService _logQueueProcessService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        //  private readonly ITaiSanDongBoEntityService _taiSanDongBoEntityService;
        #endregion
        #region ctor
        public DongBoFactory(
            IDBTaiSanService dBTaiSanService,
            ITaiSanService taiSanService,
            IDonViService donViService,
            IDonViBoPhanService donViBoPhanService,
            ITaiSanDatService taiSanDatService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanVktService taiSanVktService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanClnService taiSanClnService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ITaiSanNguonVonService taiSanNguonVonService,
            IHienTrangService hienTrangService,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiTaiSanDonViServices loaiTaiSanDonViService,
            IGSAPIService gSAPIService,
            GSConfig gSConfig,
            IHoatDongService hoatDongService,
            ILyDoBienDongService lyDoBienDongService,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory,
            IWorkContext workContext,
            IDiaBanService diaBanService,
            ICauHinhService cauHinhService,
            IQuocGiaService quocGiaService,
            INhanXeService nhanXeService,
            IDongXeService dongXeService,
            IGSFileProvider gSFileProvider,
            IQueueProcessService queueProcessService,
            INhanHienThiService nhanHienThiService,
            ILogQueueProcessService logQueueProcessService,
            ILoaiDonViService loaiDonViService,
            IChucDanhService chucDanhService,
            IMucDichSuDungService mucDichSuDungService,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            ITaiSanTdService taiSanTdService,
            IPhuongAnXuLyService phuongAnXuLyService,
            INguoiDungService nguoiDungService,
            INguoiDungDonViService nguoiDungDonViService

            // ITaiSanDongBoEntityService taiSanDongBoEntityService
            )
        {
            this._dBTaiSanService = dBTaiSanService;
            this._donViService = donViService;
            this._taiSanService = taiSanService;
            this._donViBoPhanService = donViBoPhanService;
            this._taiSanDatService = taiSanDatService;
            this._taiSanClnService = taiSanClnService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVktService = taiSanVktService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._bienDongService = bienDongService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._hienTrangService = hienTrangService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanDonViService = loaiTaiSanDonViService;
            this._gSAPIService = gSAPIService;
            this._gsConfig = gSConfig;
            this._hoatDongService = hoatDongService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
            this._workContext = workContext;
            this._diaBanService = diaBanService;
            this._cauHinhService = cauHinhService;
            this._nhanXeService = nhanXeService;
            this._dongXeService = dongXeService;
            this._quocGiaService = quocGiaService;
            this._fileProvider = gSFileProvider;
            this._queueProcessService = queueProcessService;
            this._nhanHienThiService = nhanHienThiService;
            this._logQueueProcessService = logQueueProcessService;
            this._loaiDonViService = loaiDonViService;
            this._chucDanhService = chucDanhService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._taiSanTdService = taiSanTdService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._nguoiDungService = nguoiDungService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            //  this._taiSanDongBoEntityService = taiSanDongBoEntityService
        }
        #endregion
        #region method
        #region Nhận tài sản
        /// <summary>
        /// Insert du lieu tu bang tam ve bang that
        /// </summary>
        public MessageReturn DongBoTaiSan(String maDonVi = null, int? TakeNumber = 200)
        {
            List<DBTaiSanView> dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(maDonVi, TakeNumber.Value);
            if (dBTaiSans == null || (dBTaiSans != null && dBTaiSans.Count == 0))
                return MessageReturn.CreateErrorMessage("No data found");

            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(dBTaiSans, (decimal)enumTrangThaiDbTaiSan.DangChayJob);
            MessageReturn result = new MessageReturn();
            List<MessageReturn> ListError = new List<MessageReturn>();
            List<MessageReturn> ListSuccess = new List<MessageReturn>();
            BienDongDBModel bienDongDBModel = new BienDongDBModel();
            foreach (var item in dBTaiSans)
            {
                item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DangChayJob;
                DBTaiSan dbTaiSan = _dBTaiSanService.GetTaiSanById(item.ID);
                if (dbTaiSan.TRANG_THAI_ID == (decimal)enumTrangThaiDbTaiSan.DangChayJob)
                    try
                    {
                        if (!dbTaiSan.IS_BIEN_DONG.Value)
                        {
                            result = InsertTaiSanDongBoByStore(dbTaiSan);
                        }
                        else
                        {
                            bienDongDBModel = JsonConvert.DeserializeObject<BienDongDBModel>(item.DATA_JSON);
                            result = InsertBienDong(bienDongDBModel, null);
                        }
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            //_dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(new List<DBTaiSanView>() { item }, (decimal)enumTrangThaiDbTaiSan.DaInsert);
                            ListSuccess.Add(result);
                        }
                        else
                        {
                            result.IdRecord = item.DB_MA;
                            result.ObjectInfo = item.DATA_JSON;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                            dbTaiSan.RESPONSE = result.toStringJson();
                            _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            ListError.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = MessageReturn.CreateErrorMessage(ex.Message);
                        item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        item.RESPONSE = result.toStringJson();
                        dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        dbTaiSan.RESPONSE = result.toStringJson();
                        _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                    }
            }
            return result;
        }
        public MessageReturn DBTaiSan(String maDonVi = null, int? TakeNumber = 200)
        {
            List<DBTaiSanView> dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(maDonVi, TakeNumber.Value);
            if (dBTaiSans == null || (dBTaiSans != null && dBTaiSans.Count == 0))
                return MessageReturn.CreateErrorMessage("No data found");

            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(dBTaiSans, (decimal)enumTrangThaiDbTaiSan.DangChayJob);
            MessageReturn result = new MessageReturn();
            List<MessageReturn> ListError = new List<MessageReturn>();
            List<MessageReturn> ListSuccess = new List<MessageReturn>();
            BDDBModel bienDongDBModel = new BDDBModel();
            foreach (var item in dBTaiSans)
            {
                item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DangChayJob;
                DBTaiSan dbTaiSan = _dBTaiSanService.GetTaiSanById(item.ID);
                if (dbTaiSan.TRANG_THAI_ID == (decimal)enumTrangThaiDbTaiSan.DangChayJob)
                    try
                    {
                        if (!dbTaiSan.IS_BIEN_DONG.Value)
                        {
                            result = InsertTaiSanDBByStore(dbTaiSan);
                        }
                        else
                        {
                            bienDongDBModel = JsonConvert.DeserializeObject<BDDBModel>(item.DATA_JSON);
                            result = InsertBienDongDB(bienDongDBModel, null);
                        }
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            //_dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(new List<DBTaiSanView>() { item }, (decimal)enumTrangThaiDbTaiSan.DaInsert);
                            ListSuccess.Add(result);
                        }
                        else
                        {
                            result.IdRecord = item.DB_MA;
                            result.ObjectInfo = item.DATA_JSON;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                            dbTaiSan.RESPONSE = result.toStringJson();
                            _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            ListError.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = MessageReturn.CreateErrorMessage(ex.Message);
                        item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        item.RESPONSE = result.toStringJson();
                        dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        dbTaiSan.RESPONSE = result.toStringJson();
                        _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                    }
            }
            return result;
        }
        public MessageReturn DongBoBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200)
        {
            List<DBTaiSanView> dBTaiSans = new List<DBTaiSanView>();
            if (String.IsNullOrEmpty(DBId))
                dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(null, TakeNumber.Value, 1);
            else
                dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(DBId, TakeNumber.Value, 1);
            if (dBTaiSans == null || (dBTaiSans != null && dBTaiSans.Count == 0))
                return MessageReturn.CreateErrorMessage("No data found");

            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(dBTaiSans, (decimal)enumTrangThaiDbTaiSan.DangChayJob);
            MessageReturn result = new MessageReturn();
            List<MessageReturn> ListError = new List<MessageReturn>();
            List<MessageReturn> ListSuccess = new List<MessageReturn>();
            BienDongDBModel bienDongDBModel = new BienDongDBModel();
            foreach (var item in dBTaiSans)
            {
                item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DangChayJob;
                DBTaiSan dbTaiSan = _dBTaiSanService.GetTaiSanById(item.ID);
                if (dbTaiSan.TRANG_THAI_ID == (decimal)enumTrangThaiDbTaiSan.DangChayJob)
                    try
                    {
                        if (!dbTaiSan.IS_BIEN_DONG.Value)
                        {
                            result = InsertTaiSanDongBoByStore(dbTaiSan);
                        }
                        else
                        {
                            bienDongDBModel = JsonConvert.DeserializeObject<BienDongDBModel>(item.DATA_JSON);
                            result = InsertBienDong(bienDongDBModel, null);
                        }
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            //_dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(new List<DBTaiSanView>() { item }, (decimal)enumTrangThaiDbTaiSan.DaInsert);
                            ListSuccess.Add(result);
                        }
                        else
                        {
                            result.IdRecord = item.DB_MA;
                            result.ObjectInfo = item.DATA_JSON;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                            dbTaiSan.RESPONSE = result.toStringJson();
                            _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            ListError.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = MessageReturn.CreateErrorMessage(ex.Message);
                        item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        item.RESPONSE = result.toStringJson();
                        dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        dbTaiSan.RESPONSE = result.toStringJson();
                        _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                    }
            }
            return result;
        }
        public MessageReturn DBBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200)
        {
            List<DBTaiSanView> dBTaiSans = new List<DBTaiSanView>();
            if (String.IsNullOrEmpty(DBId))
                dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(null, TakeNumber.Value, 1);
            else
                dBTaiSans = _dBTaiSanService.GET_TAI_SAN_CAN_DONG_BO(DBId, TakeNumber.Value, 1);
            if (dBTaiSans == null || (dBTaiSans != null && dBTaiSans.Count == 0))
                return MessageReturn.CreateErrorMessage("No data found");

            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(dBTaiSans, (decimal)enumTrangThaiDbTaiSan.DangChayJob);
            MessageReturn result = new MessageReturn();
            List<MessageReturn> ListError = new List<MessageReturn>();
            List<MessageReturn> ListSuccess = new List<MessageReturn>();
            BDDBModel bienDongDBModel = new BDDBModel();
            foreach (var item in dBTaiSans)
            {
                item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DangChayJob;
                DBTaiSan dbTaiSan = _dBTaiSanService.GetTaiSanById(item.ID);
                if (dbTaiSan.TRANG_THAI_ID == (decimal)enumTrangThaiDbTaiSan.DangChayJob)
                    try
                    {
                        if (!dbTaiSan.IS_BIEN_DONG.Value)
                        {
                            result = InsertTaiSanDBByStore(dbTaiSan);
                        }
                        else
                        {
                            bienDongDBModel = JsonConvert.DeserializeObject<BDDBModel>(item.DATA_JSON);
                            result = InsertBienDongDB(bienDongDBModel, null);
                        }
                        if (result.Code == MessageReturn.SUCCESS_CODE)
                        {
                            item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.DaInsert;
                            //_dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            _dBTaiSanService.UPDATE_TRANG_THAI_DB_TAI_SAN(new List<DBTaiSanView>() { item }, (decimal)enumTrangThaiDbTaiSan.DaInsert);
                            ListSuccess.Add(result);
                        }
                        else
                        {
                            result.IdRecord = item.DB_MA;
                            result.ObjectInfo = item.DATA_JSON;
                            dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiDL;
                            dbTaiSan.RESPONSE = result.toStringJson();
                            _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                            ListError.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = MessageReturn.CreateErrorMessage(ex.Message);
                        item.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        item.RESPONSE = result.toStringJson();
                        dbTaiSan.TRANG_THAI_ID = (decimal)enumTrangThaiDbTaiSan.LoiHT;
                        dbTaiSan.RESPONSE = result.toStringJson();
                        _dBTaiSanService.UpdateTaiSan(dbTaiSan);
                    }
            }
            return result;
        }
        void InsertTaiSan(DBTaiSan item)
        {
            ThongTinTaiSanDongBoModel dbModel = JsonConvert.DeserializeObject<ThongTinTaiSanDongBoModel>(item.DATA_JSON);
            // update Thong tin tai san 
            TaiSan taiSan = _taiSanService.GetTaiSanByMa(item.QLDKTS_MA);
            taiSan.TEN = dbModel.TEN;
            taiSan.LOAI_TAI_SAN_ID = item.LOAI_TAI_SAN_ID;
            taiSan.LOAI_HINH_TAI_SAN_ID = dbModel.LOAI_HINH_TAI_SAN_ID;
            taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            taiSan.QUYET_DINH_SO = dbModel.QUYET_DINH_SO;
            if (!(string.IsNullOrEmpty(dbModel.QUYET_DINH_NGAY)))
            {
                taiSan.QUYET_DINH_NGAY = dbModel.QUYET_DINH_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            }
            if (!(string.IsNullOrEmpty(dbModel.NGAY_NHAP)))
            {
                taiSan.NGAY_NHAP = dbModel.NGAY_NHAP.toDateSys("yyyy-MM-dd HH:mm:ss");
            }
            if (!(string.IsNullOrEmpty(dbModel.NGAY_SU_DUNG)))
            {
                taiSan.NGAY_SU_DUNG = dbModel.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
            }
            taiSan.GHI_CHU = dbModel.GHI_CHU;
            // dơn vị
            DonVi donVi = _donViService.GetCacheDonViByMa(dbModel.MA_DON_VI);
            taiSan.DON_VI_ID = donVi.ID;
            // dơn vị bộ phận
            DonViBoPhan donViBoPhan = _donViBoPhanService.GetDonViBoPhanByTen(dbModel.DON_VI_BO_PHAN_TEN);
            if (donViBoPhan == null)
            {
                donViBoPhan = new DonViBoPhan();
                donViBoPhan.TEN = dbModel.DON_VI_BO_PHAN_TEN;
                donViBoPhan.DON_VI_ID = donVi.ID;
                _donViBoPhanService.InsertDonViBoPhan(donViBoPhan);
            }
            else
            {
                taiSan.DON_VI_BO_PHAN_ID = donViBoPhan.ID;
            }
            taiSan.NGUOI_TAO_ID = item.TAI_KHOAN_DONG_BO_ID;
            taiSan.DON_VI_BO_PHAN_ID = donViBoPhan.ID;
            taiSan.CHUNG_TU_SO = dbModel.CHUNG_TU_SO;
            if (!(string.IsNullOrEmpty(dbModel.CHUNG_TU_NGAY)))
            {
                taiSan.NGAY_SU_DUNG = dbModel.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            }
            taiSan.MA_DB = item.DB_MA;
            _taiSanService.UpdateTaiSan(taiSan);
            #region insert các Loai Hinh Tai San
            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        TaiSanDat taiSanDat = new TaiSanDat();
                        taiSanDat.DIA_CHI = dbModel.TS_DAT.DIA_CHI;
                        taiSanDat.DIEN_TICH = dbModel.TS_DAT.DIEN_TICH.Value;
                        taiSanDat.TAI_SAN_ID = taiSan.ID;
                        taiSanDat.DIA_BAN_ID = dbModel.TS_DAT.DIA_BAN_ID;
                        _taiSanDatService.InsertTaiSanDat(taiSanDat);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        TaiSanNha taiSanNha = new TaiSanNha();
                        taiSanNha.DIA_CHI = dbModel.TS_NHA.DIA_CHI;
                        // tài sản đất 
                        TaiSanDat taiSanDat = _taiSanDatService.GetTaiSanDatByMaTSAndDonVi(dbModel.TS_NHA.TAI_SAN_DAT_MA);
                        if (taiSanDat != null)
                        {
                            taiSanNha.TAI_SAN_DAT_ID = taiSanDat.ID;
                        }
                        taiSanNha.DIEN_TICH_XAY_DUNG = dbModel.TS_NHA.DIEN_TICH_XAY_DUNG;
                        taiSanNha.DIEN_TICH_SAN_XAY_DUNG = dbModel.TS_NHA.DIEN_TICH_SAN_XAY_DUNG;
                        taiSanNha.NAM_XAY_DUNG = dbModel.TS_NHA.NAM_XAY_DUNG;
                        taiSanNha.NAM_XAY_DUNG = dbModel.TS_NHA.NAM_XAY_DUNG;
                        taiSanNha.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                        taiSanNha.TAI_SAN_ID = taiSan.ID;
                        _taiSanNhaService.InsertTaiSanNha(taiSanNha);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    {
                        TaiSanVkt taiSanVkt = new TaiSanVkt();
                        taiSanVkt.TAI_SAN_ID = taiSan.ID;
                        taiSanVkt.DIEN_TICH = dbModel.TS_VKT.DIEN_TICH;
                        taiSanVkt.CHIEU_DAI = dbModel.TS_VKT.CHIEU_DAI;
                        taiSanVkt.THE_TICH = dbModel.TS_VKT.THE_TICH;
                        _taiSanVktService.InsertTaiSanVkt(taiSanVkt);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    {
                        TaiSanMayMoc taiSanMayMoc = new TaiSanMayMoc();
                        taiSanMayMoc.THONG_SO_KY_THUAT = dbModel.TS_MAY_MOC.THONG_SO_KY_THUAT;
                        taiSanMayMoc.TAI_SAN_ID = taiSan.ID;
                        _taiSanMayMocService.InsertTaiSanMayMoc(taiSanMayMoc);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    {
                        TaiSanCln taiSanCln = new TaiSanCln();
                        taiSanCln.NAM_SINH = dbModel.TS_CLN.NAM_SINH;
                        taiSanCln.TAI_SAN_ID = taiSan.ID;
                        _taiSanClnService.InsertTaiSanCln(taiSanCln);
                        break;
                    }
                default:
                    break;
            }
            #endregion
            #region Insert biến động
            dbModel.LST_BIEN_DONG = dbModel.LST_BIEN_DONG.OrderBy(m => m.NGAY_BIEN_DONG).ToList();
            foreach (var bd in dbModel.LST_BIEN_DONG)
            {
                InsertBienDong(bd, taiSan);
            }
            #endregion
        }
        MessageReturn InsertBienDong(BienDongDBModel bd, TaiSan taiSan)
        {
            try
            {
                BienDong bienDong = new BienDong();
                if (taiSan == null)
                {
                    taiSan = _taiSanService.GetTaiSanByMaDB(Ma: bd.MA_TAI_SAN_DB, NguonTaiSanId: bd.NGUON_TAI_SAN_ID);
                }
                bienDong.TAI_SAN_ID = taiSan.ID;
                bienDong.TAI_SAN_MA = taiSan.MA;
                bienDong.TAI_SAN_TEN = bd.TEN_TAI_SAN;
                bienDong.LOAI_TAI_SAN_ID = bd.LOAI_TAI_SAN_ID;
                bienDong.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                bienDong.NGUYEN_GIA = bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI ? -bd.NGUYEN_GIA : bd.NGUYEN_GIA;
                bienDong.DON_VI_BO_PHAN_ID = taiSan.DON_VI_BO_PHAN_ID;
                bienDong.CHUNG_TU_NGAY = taiSan.CHUNG_TU_NGAY;
                bienDong.CHUNG_TU_SO = taiSan.CHUNG_TU_SO;
                if (!string.IsNullOrEmpty(bd.NGAY_BIEN_DONG))
                {
                    bienDong.NGAY_BIEN_DONG = bd.NGAY_BIEN_DONG.toDateSys("yyyy-MM-dd HH:mm:ss");
                }
                bienDong.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                bienDong.LOAI_BIEN_DONG_ID = bd.LOAI_BIEN_DONG_ID;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                {
                    // biến động tăng mới thì cập nhật nguyên giá ban đầu của tài sản
                    taiSan.NGUYEN_GIA_BAN_DAU = bienDong.NGUYEN_GIA;
                    _taiSanService.UpdateTaiSan(taiSan);
                }
                bienDong.LY_DO_BIEN_DONG_ID = bd.LY_DO_BIEN_DONG_ID;
                bienDong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                bienDong.DON_VI_ID = taiSan.DON_VI_ID;
                bienDong.NGUOI_TAO_ID = taiSan.NGUOI_TAO_ID;
                bienDong.NGAY_TAO = DateTime.Now;
                if (!string.IsNullOrEmpty(bd.NGAY_DUYET))
                {
                    bienDong.NGAY_DUYET = bd.NGAY_DUYET.toDateSys("yyyy-MM-dd HH:mm:ss");
                }
                bienDong.ID_DB = bd.ID_DB;
                bienDong.GUID = Guid.Parse(bd.GUID);
                _bienDongService.InsertBienDong(bienDong, true);
                // Biến động chi tiết
                BienDongChiTiet bienDongChiTiet = new BienDongChiTiet();
                bienDongChiTiet.BIEN_DONG_ID = bienDong.ID;
                bienDongChiTiet.MUC_DICH_SU_DUNG_ID = bd.MUC_DICH_SU_DUNG_ID;
                bienDongChiTiet.NHAN_HIEU = bd.NHAN_HIEU;
                bienDongChiTiet.SO_HIEU = bd.SO_HIEU;
                bienDongChiTiet.DIA_BAN_ID = bd.DIA_BAN_ID;
                bienDongChiTiet.DIA_CHI = bd.DIA_CHI;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && bd.DAT_TONG_DIEN_TICH > 0)
                {
                    bienDongChiTiet.DAT_TONG_DIEN_TICH = -bd.DAT_TONG_DIEN_TICH;
                }
                else if (bd.DAT_TONG_DIEN_TICH > 0)
                {
                    bienDongChiTiet.DAT_TONG_DIEN_TICH = bd.DAT_TONG_DIEN_TICH;
                }
                // biến động thay đổi thông tin
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    // cập nhật tên tài sản và loại tài snả
                    taiSan.LOAI_TAI_SAN_ID = bd.LOAI_TAI_SAN_ID;
                    taiSan.TEN = bd.TEN_TAI_SAN;
                    _taiSanService.UpdateTaiSan(taiSan);
                    switch (bienDong.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                TaiSanDat taiSanDat = _taiSanDatService.GetTaiSanDatByTaiSanId(taiSan.ID);
                                taiSanDat.DIA_BAN_ID = bienDongChiTiet.DIA_BAN_ID;
                                taiSanDat.DIA_CHI = bienDongChiTiet.DIA_CHI;
                                _taiSanDatService.UpdateTaiSanDat(taiSanDat);
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                            {
                                TaiSanOto taiSanOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(taiSan.ID);
                                taiSanOto.BIEN_KIEM_SOAT = bienDongChiTiet.OTO_BIEN_KIEM_SOAT;
                                taiSanOto.CONG_XUAT = bienDongChiTiet.OTO_CONG_XUAT;
                                taiSanOto.SO_CHO_NGOI = bienDongChiTiet.OTO_SO_CHO_NGOI;
                                taiSanOto.TAI_TRONG = bienDongChiTiet.OTO_TAI_TRONG;
                                _taiSanOtoService.UpdateTaiSanOto(taiSanOto);
                                break;
                            }
                        default:
                            break;
                    }
                }
                bienDongChiTiet.HM_SO_NAM_CON_LAI = bd.HM_SO_NAM_CON_LAI;
                bienDongChiTiet.HM_LUY_KE = bd.HM_LUY_KE;
                bienDongChiTiet.HM_GIA_TRI_CON_LAI = bd.HM_GIA_TRI_CON_LAI;
                bienDongChiTiet.HM_LUY_KE = bd.HM_LUY_KE;
                bienDongChiTiet.NHA_SO_TANG = bd.NHA_SO_TANG;
                bienDongChiTiet.NHA_NAM_XAY_DUNG = bd.NHA_NAM_XAY_DUNG;
                bienDongChiTiet.NHA_DIEN_TICH_XD = bd.NHA_DIEN_TICH_XD;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && bd.NHA_TONG_DIEN_TICH_XD > 0)
                {
                    bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = -bd.NHA_TONG_DIEN_TICH_XD;
                }
                else if (bd.NHA_TONG_DIEN_TICH_XD > 0)
                {
                    bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = bd.NHA_TONG_DIEN_TICH_XD;
                }
                bienDongChiTiet.VKT_DIEN_TICH = bd.VKT_DIEN_TICH;
                bienDongChiTiet.VKT_THE_TICH = bd.VKT_THE_TICH;
                bienDongChiTiet.VKT_CHIEU_DAI = bd.VKT_CHIEU_DAI;
                bienDongChiTiet.OTO_BIEN_KIEM_SOAT = bd.OTO_BIEN_KIEM_SOAT;
                bienDongChiTiet.OTO_SO_CHO_NGOI = bd.OTO_SO_CHO_NGOI;
                bienDongChiTiet.OTO_TAI_TRONG = bd.OTO_TAI_TRONG;
                bienDongChiTiet.OTO_SO_CHO_NGOI = bd.OTO_SO_CHO_NGOI;
                bienDongChiTiet.OTO_CONG_XUAT = bd.OTO_CONG_XUAT;
                bienDongChiTiet.OTO_XI_LANH = bd.OTO_XI_LANH;
                bienDongChiTiet.OTO_SO_KHUNG = bd.OTO_SO_KHUNG;
                bienDongChiTiet.OTO_SO_MAY = bd.OTO_SO_MAY;
                bienDongChiTiet.THONG_SO_KY_THUAT = bd.THONG_SO_KY_THUAT;
                bienDongChiTiet.CLN_SO_NAM = bd.CLN_SO_NAM;
                var idLyDoDieuChuyen = _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN || (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && bd.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen))
                {
                    if (!string.IsNullOrEmpty(bd.DON_VI_NHAN_DIEU_CHUYEN_MA))
                    {
                        DonVi donViNhanDieuChuyen = _donViService.GetDonViByMa(bd.DON_VI_NHAN_DIEU_CHUYEN_MA);
                        bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID = donViNhanDieuChuyen.ID;
                    }
                }
                bienDongChiTiet.HINH_THUC_XU_LY_ID = bd.HINH_THUC_XU_LY_ID;
                bienDongChiTiet.IS_BAN_THANH_LY = bd.IS_BAN_THANH_LY;
                _bienDongChiTietService.InsertBienDongChiTiet(bienDongChiTiet);
                if (bd.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    #region insert Hiện trạng
                    List<TaiSanHienTrangSuDung> ListHienTrang = new List<TaiSanHienTrangSuDung>();
                    foreach (var item in bd.LST_HIEN_TRANG)
                    {
                        TaiSanHienTrangSuDung taiSanHienTrang = new TaiSanHienTrangSuDung();
                        taiSanHienTrang.HIEN_TRANG_ID = item.HIEN_TRANG_ID;
                        taiSanHienTrang.KIEU_DU_LIEU_ID = taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox;
                        taiSanHienTrang.GIA_TRI_CHECKBOX = item.GIA_TRI_CHECKBOX;
                        taiSanHienTrang.GIA_TRI_NUMBER = item.GIA_TRI_NUMBER;
                        taiSanHienTrang.TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(item.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG;
                        taiSanHienTrang.TAI_SAN_ID = taiSan.ID;
                        taiSanHienTrang.BIEN_DONG_ID = bienDong.ID;
                        ListHienTrang.Add(taiSanHienTrang);
                    }
                    _taiSanHienTrangSuDungService.InsertTaiSanHienTrangSuDungs(ListHienTrang);
                    var ListHienTrangObj = ListHienTrang.Select(m =>
                             new ObjHienTrang_Entity()
                             {
                                 HienTrangId = m.HIEN_TRANG_ID,
                                 GiaTriText = m.GIA_TRI_TEXT,
                                 GiaTriNumber = m.GIA_TRI_NUMBER,
                                 GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
                                 NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                                 TenHienTrang = m.TEN_HIEN_TRANG,
                                 KieuDuLieuId = m.KIEU_DU_LIEU_ID
                             }).ToList();
                    var HienTrangList = new HienTrangList_Entity()
                    {
                        TaiSanId = taiSan.ID,
                        lstObjHienTrang = ListHienTrangObj
                    };
                    bienDongChiTiet.HTSD_JSON = HienTrangList.toStringJson();
                    #endregion
                    #region Insert Nguồn vốn
                    if (bd.NV_HDSN > 0)
                    {
                        InsertNguonVon(bd.NV_HDSN.Value, (decimal)enumNguonVon.NguonHoatDongSuNghiep, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_NGAN_SACH > 0)
                    {
                        InsertNguonVon(bd.NV_NGAN_SACH.Value, (decimal)enumNguonVon.NguonNganSach, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_VIEN_TRO > 0)
                    {
                        InsertNguonVon(bd.NV_VIEN_TRO.Value, (decimal)enumNguonVon.NguonVienTro, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_NGUON_KHAC > 0)
                    {
                        InsertNguonVon(bd.NV_NGUON_KHAC.Value, (decimal)enumNguonVon.NguonKhac, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    #endregion
                }

                return MessageReturn.CreateSuccessMessage("Done");
            }
            catch (Exception ex)
            {

                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }
        MessageReturn InsertBienDongDB(BDDBModel bd, TaiSan taiSan)
        {
            try
            {
                BienDong bienDong = new BienDong();
                if (taiSan == null)
                {
                    taiSan = _taiSanService.GetTaiSanByMaDB(Ma: bd.MA_TAI_SAN_DB, NguonTaiSanId: bd.NGUON_TAI_SAN_ID);
                }
                bienDong.TAI_SAN_ID = taiSan.ID;
                bienDong.TAI_SAN_MA = taiSan.MA;
                bienDong.TAI_SAN_TEN = bd.TEN_TAI_SAN;
                bienDong.LOAI_TAI_SAN_ID = bd.LOAI_TAI_SAN_ID;
                bienDong.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                bienDong.NGUYEN_GIA = bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI ? -bd.NGUYEN_GIA : bd.NGUYEN_GIA;
                bienDong.DON_VI_BO_PHAN_ID = taiSan.DON_VI_BO_PHAN_ID;
                bienDong.CHUNG_TU_NGAY = taiSan.CHUNG_TU_NGAY;
                bienDong.CHUNG_TU_SO = taiSan.CHUNG_TU_SO;
                if (!string.IsNullOrEmpty(bd.NGAY_BIEN_DONG))
                {
                    bienDong.NGAY_BIEN_DONG = bd.NGAY_BIEN_DONG.toDateSys("yyyy-MM-dd HH:mm:ss");
                }
                bienDong.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                bienDong.LOAI_BIEN_DONG_ID = bd.LOAI_BIEN_DONG_ID;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                {
                    // biến động tăng mới thì cập nhật nguyên giá ban đầu của tài sản
                    taiSan.NGUYEN_GIA_BAN_DAU = bienDong.NGUYEN_GIA;
                    _taiSanService.UpdateTaiSan(taiSan);
                }
                bienDong.LY_DO_BIEN_DONG_ID = bd.LY_DO_BIEN_DONG_ID;
                bienDong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                bienDong.DON_VI_ID = taiSan.DON_VI_ID;
                bienDong.NGUOI_TAO_ID = taiSan.NGUOI_TAO_ID;
                bienDong.NGAY_TAO = DateTime.Now;
                if (!string.IsNullOrEmpty(bd.NGAY_DUYET))
                {
                    bienDong.NGAY_DUYET = bd.NGAY_DUYET.toDateSys("yyyy-MM-dd HH:mm:ss");
                }
                bienDong.ID_DB = bd.ID_DB;
                bienDong.GUID = Guid.Parse(bd.GUID);
                _bienDongService.InsertBienDong(bienDong, true);
                // Biến động chi tiết
                BienDongChiTiet bienDongChiTiet = new BienDongChiTiet();
                bienDongChiTiet.BIEN_DONG_ID = bienDong.ID;
                bienDongChiTiet.MUC_DICH_SU_DUNG_ID = bd.MUC_DICH_SU_DUNG_ID;
                bienDongChiTiet.NHAN_HIEU = bd.NHAN_HIEU;
                bienDongChiTiet.SO_HIEU = bd.SO_HIEU;
                bienDongChiTiet.DIA_BAN_ID = bd.DIA_BAN_ID;
                bienDongChiTiet.DIA_CHI = bd.DIA_CHI;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && bd.DAT_TONG_DIEN_TICH > 0)
                {
                    bienDongChiTiet.DAT_TONG_DIEN_TICH = -bd.DAT_TONG_DIEN_TICH;
                }
                else if (bd.DAT_TONG_DIEN_TICH > 0)
                {
                    bienDongChiTiet.DAT_TONG_DIEN_TICH = bd.DAT_TONG_DIEN_TICH;
                }
                // biến động thay đổi thông tin
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    // cập nhật tên tài sản và loại tài snả
                    taiSan.LOAI_TAI_SAN_ID = bd.LOAI_TAI_SAN_ID;
                    taiSan.TEN = bd.TEN_TAI_SAN;
                    _taiSanService.UpdateTaiSan(taiSan);
                    switch (bienDong.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                TaiSanDat taiSanDat = _taiSanDatService.GetTaiSanDatByTaiSanId(taiSan.ID);
                                taiSanDat.DIA_BAN_ID = bienDongChiTiet.DIA_BAN_ID;
                                taiSanDat.DIA_CHI = bienDongChiTiet.DIA_CHI;
                                _taiSanDatService.UpdateTaiSanDat(taiSanDat);
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                            {
                                TaiSanOto taiSanOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(taiSan.ID);
                                taiSanOto.BIEN_KIEM_SOAT = bienDongChiTiet.OTO_BIEN_KIEM_SOAT;
                                taiSanOto.CONG_XUAT = bienDongChiTiet.OTO_CONG_XUAT;
                                taiSanOto.SO_CHO_NGOI = bienDongChiTiet.OTO_SO_CHO_NGOI;
                                taiSanOto.TAI_TRONG = bienDongChiTiet.OTO_TAI_TRONG;
                                _taiSanOtoService.UpdateTaiSanOto(taiSanOto);
                                break;
                            }
                        default:
                            break;
                    }
                }
                bienDongChiTiet.HM_SO_NAM_CON_LAI = bd.HM_SO_NAM_CON_LAI;
                bienDongChiTiet.HM_LUY_KE = bd.HM_LUY_KE;
                bienDongChiTiet.HM_GIA_TRI_CON_LAI = bd.HM_GIA_TRI_CON_LAI;
                bienDongChiTiet.HM_LUY_KE = bd.HM_LUY_KE;
                bienDongChiTiet.NHA_SO_TANG = bd.NHA_SO_TANG;
                bienDongChiTiet.NHA_NAM_XAY_DUNG = bd.NHA_NAM_XAY_DUNG;
                bienDongChiTiet.NHA_DIEN_TICH_XD = bd.NHA_DIEN_TICH_XD;
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && bd.NHA_TONG_DIEN_TICH_XD > 0)
                {
                    bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = -bd.NHA_TONG_DIEN_TICH_XD;
                }
                else if (bd.NHA_TONG_DIEN_TICH_XD > 0)
                {
                    bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = bd.NHA_TONG_DIEN_TICH_XD;
                }
                bienDongChiTiet.VKT_DIEN_TICH = bd.VKT_DIEN_TICH;
                bienDongChiTiet.VKT_THE_TICH = bd.VKT_THE_TICH;
                bienDongChiTiet.VKT_CHIEU_DAI = bd.VKT_CHIEU_DAI;
                bienDongChiTiet.OTO_BIEN_KIEM_SOAT = bd.OTO_BIEN_KIEM_SOAT;
                bienDongChiTiet.OTO_SO_CHO_NGOI = bd.OTO_SO_CHO_NGOI;
                bienDongChiTiet.OTO_TAI_TRONG = bd.OTO_TAI_TRONG;
                bienDongChiTiet.OTO_SO_CHO_NGOI = bd.OTO_SO_CHO_NGOI;
                bienDongChiTiet.OTO_CONG_XUAT = bd.OTO_CONG_XUAT;
                bienDongChiTiet.OTO_XI_LANH = bd.OTO_XI_LANH;
                bienDongChiTiet.OTO_SO_KHUNG = bd.OTO_SO_KHUNG;
                bienDongChiTiet.OTO_SO_MAY = bd.OTO_SO_MAY;
                bienDongChiTiet.THONG_SO_KY_THUAT = bd.THONG_SO_KY_THUAT;
                bienDongChiTiet.CLN_SO_NAM = bd.CLN_SO_NAM;
                var idLyDoDieuChuyen = _lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.DIEU_CHUYEN);
                if (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN || (bd.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && bd.LY_DO_BIEN_DONG_ID == idLyDoDieuChuyen))
                {
                    if (!string.IsNullOrEmpty(bd.DON_VI_NHAN_DIEU_CHUYEN_MA))
                    {
                        DonVi donViNhanDieuChuyen = _donViService.GetDonViByMa(bd.DON_VI_NHAN_DIEU_CHUYEN_MA);
                        bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID = donViNhanDieuChuyen.ID;
                    }
                }
                bienDongChiTiet.HINH_THUC_XU_LY_ID = bd.HINH_THUC_XU_LY_ID;
                bienDongChiTiet.IS_BAN_THANH_LY = bd.IS_BAN_THANH_LY;
                _bienDongChiTietService.InsertBienDongChiTiet(bienDongChiTiet);
                if (bd.LOAI_BIEN_DONG_ID != (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    #region insert Hiện trạng
                    List<TaiSanHienTrangSuDung> ListHienTrang = new List<TaiSanHienTrangSuDung>();
                    foreach (var item in bd.LST_HIEN_TRANG)
                    {
                        TaiSanHienTrangSuDung taiSanHienTrang = new TaiSanHienTrangSuDung();
                        taiSanHienTrang.HIEN_TRANG_ID = item.HIEN_TRANG_ID;
                        taiSanHienTrang.KIEU_DU_LIEU_ID = taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox;
                        taiSanHienTrang.GIA_TRI_CHECKBOX = item.GIA_TRI_CHECKBOX;
                        taiSanHienTrang.GIA_TRI_NUMBER = item.GIA_TRI_NUMBER;
                        taiSanHienTrang.TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(item.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG;
                        taiSanHienTrang.TAI_SAN_ID = taiSan.ID;
                        taiSanHienTrang.BIEN_DONG_ID = bienDong.ID;
                        ListHienTrang.Add(taiSanHienTrang);
                    }
                    _taiSanHienTrangSuDungService.InsertTaiSanHienTrangSuDungs(ListHienTrang);
                    var ListHienTrangObj = ListHienTrang.Select(m =>
                             new ObjHienTrang_Entity()
                             {
                                 HienTrangId = m.HIEN_TRANG_ID,
                                 GiaTriText = m.GIA_TRI_TEXT,
                                 GiaTriNumber = m.GIA_TRI_NUMBER,
                                 GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
                                 NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                                 TenHienTrang = m.TEN_HIEN_TRANG,
                                 KieuDuLieuId = m.KIEU_DU_LIEU_ID
                             }).ToList();
                    var HienTrangList = new HienTrangList_Entity()
                    {
                        TaiSanId = taiSan.ID,
                        lstObjHienTrang = ListHienTrangObj
                    };
                    bienDongChiTiet.HTSD_JSON = HienTrangList.toStringJson();
                    #endregion
                    #region Insert Nguồn vốn
                    if (bd.NV_HDSN > 0)
                    {
                        InsertNguonVon(bd.NV_HDSN.Value, (decimal)enumNguonVon.NguonHoatDongSuNghiep, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_NGAN_SACH > 0)
                    {
                        InsertNguonVon(bd.NV_NGAN_SACH.Value, (decimal)enumNguonVon.NguonNganSach, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_VIEN_TRO > 0)
                    {
                        InsertNguonVon(bd.NV_VIEN_TRO.Value, (decimal)enumNguonVon.NguonVienTro, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    if (bd.NV_NGUON_KHAC > 0)
                    {
                        InsertNguonVon(bd.NV_NGUON_KHAC.Value, (decimal)enumNguonVon.NguonKhac, bienDong.ID, taiSan.ID, bd.LOAI_BIEN_DONG_ID.Value);
                    }
                    #endregion
                }

                return MessageReturn.CreateSuccessMessage("Done");
            }
            catch (Exception ex)
            {

                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }
        void InsertHienTrangCheckbox(bool value, decimal IdHienTrang, decimal BienDongId)
        {
            TaiSanHienTrangSuDung taiSanHienTrang = new TaiSanHienTrangSuDung();
            taiSanHienTrang.GIA_TRI_CHECKBOX = value;
            taiSanHienTrang.BIEN_DONG_ID = BienDongId;
            taiSanHienTrang.HIEN_TRANG_ID = IdHienTrang;
        }
        void InsertNguonVon(decimal value, decimal IdNguonVon = 0, decimal BienDongId = 0, decimal TaiSanId = 0, decimal LoaiBiendongId = 0)
        {
            TaiSanNguonVon taiSanNguonVon = new TaiSanNguonVon();
            taiSanNguonVon.BIEN_DONG_ID = BienDongId;
            taiSanNguonVon.NGUON_VON_ID = IdNguonVon;
            if (LoaiBiendongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || LoaiBiendongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
            {
                taiSanNguonVon.GIA_TRI = -value;
            }
            else
            {
                taiSanNguonVon.GIA_TRI = value;
            }
            taiSanNguonVon.TAI_SAN_ID = TaiSanId;
            _taiSanNguonVonService.InsertTaiSanNguonVon(taiSanNguonVon);
        }
        public void DongBoTaiSanByStore()
        {
            List<DBTaiSan> dBTaiSans = _dBTaiSanService.GetAllTaiSans().ToList();
            List<MessageReturn> ListError = new List<MessageReturn>();
            foreach (var item in dBTaiSans)
            {
                try
                {
                    if (!item.IS_BIEN_DONG.Value)
                    {
                        var result = InsertTaiSanDongBoByStore(item);
                        if (result.Code != MessageReturn.SUCCESS_CODE)
                        {
                            ListError.Add(result);
                        }
                    }
                    else
                    {
                        BienDongDBModel bienDongDBModel = JsonConvert.DeserializeObject<BienDongDBModel>(item.DATA_JSON);
                        InsertBienDong(bienDongDBModel, null);
                    }
                    _dBTaiSanService.DeleteTaiSan(item);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


        }
        public MessageReturn InsertTaiSanDongBoByStore(DBTaiSan dBTaiSan)
        {
            ThongTinTaiSanDongBoModel taiSanDongBoModel = JsonConvert.DeserializeObject<ThongTinTaiSanDongBoModel>(dBTaiSan.DATA_JSON);
            TaiSanDongBoApi taiSan = new TaiSanDongBoApi();
            taiSan.NGUON_TAI_SAN_ID = taiSanDongBoModel.NGUON_TAI_SAN_ID;
            taiSan.ID = taiSanDongBoModel.ID;
            taiSan.TEN_TAI_SAN = taiSanDongBoModel.TEN;
            taiSan.MA_TAI_SAN = dBTaiSan.QLDKTS_MA;
            taiSan.DB_MA = dBTaiSan.DB_MA;
            taiSan.NGAY_SU_DUNG = taiSanDongBoModel.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.MA_LOAI_TAI_SAN = taiSanDongBoModel.LOAI_TAI_SAN_MA;
            taiSan.LOAI_TAI_SAN_ID = taiSanDongBoModel.LOAI_TAI_SAN_ID;
            taiSan.LOAI_TAI_SAN_DON_VI_ID = taiSanDongBoModel.LOAI_TAI_SAN_DON_VI_ID;
            taiSan.DON_VI_BO_PHAN_ID = taiSanDongBoModel.DON_VI_BO_PHAN_ID;
            if (!String.IsNullOrEmpty(taiSanDongBoModel.NGAY_NHAP))
                taiSan.NGAY_NHAP = taiSanDongBoModel.NGAY_NHAP.toDateSys("yyyy-MM-dd HH:mm:ss");
            else
                taiSan.NGAY_NHAP = taiSanDongBoModel.NGAY_DANG_KY.toDateSys("yyyy-MM-dd HH:mm:ss");
            //taiSan.NGAY_DUYET = taiSanDongBoModel.Ngay.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.LY_DO_BIEN_DONG_MA = taiSanDongBoModel.LY_DO_BIEN_DONG_MA;
            taiSan.LOAI_HINH_TAI_SAN_ID = taiSanDongBoModel.LOAI_HINH_TAI_SAN_ID;
            taiSan.NGUYEN_GIA_BAN_DAU = taiSanDongBoModel.NGUYEN_GIA_BAN_DAU;
            taiSan.MA_DON_VI = taiSanDongBoModel.MA_DON_VI;
            taiSan.GHI_CHU = taiSanDongBoModel.GHI_CHU;
            taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;// trạng thái mặc định là đã duyệt
            if (!string.IsNullOrEmpty(taiSanDongBoModel.NAM_SAN_XUAT))
            {
                taiSan.NAM_SAN_XUAT = decimal.Parse(taiSanDongBoModel.NAM_SAN_XUAT);
            }
            taiSan.NUOC_SAN_XUAT_ID = taiSanDongBoModel.NUOC_SAN_XUAT_ID;
            taiSan.GIA_MUA_TIEP_NHAN = taiSanDongBoModel.GIA_MUA_TIEP_NHAN;
            taiSan.GIA_HOA_DON = taiSanDongBoModel.GIA_HOA_DON;
            taiSan.LY_DO_BIEN_DONG_MA = taiSanDongBoModel.LY_DO_BIEN_DONG_MA;
            taiSan.QUYET_DINH_SO = taiSanDongBoModel.QUYET_DINH_SO;
            taiSan.QUYET_DINH_NGAY = taiSanDongBoModel.QUYET_DINH_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.CHUNG_TU_SO = taiSanDongBoModel.CHUNG_TU_SO;
            taiSan.CHUNG_TU_NGAY = taiSanDongBoModel.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.MIEN_THUE_SO_TIEN = taiSanDongBoModel.MIEN_THUE_SO_TIEN;
            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        taiSan.TS_DAT = taiSanDongBoModel.TS_DAT.ToTaiSan<TaiSanDatDBModel, TaiSanDatApi>(taiSan.TS_DAT);
                        if (taiSan.TS_DAT.DIA_BAN_ID > 0)
                        {
                            var diaBan = _diaBanService.GetDiaBanById(taiSan.TS_DAT.DIA_BAN_ID.Value);
                            if (diaBan == null)
                            {
                                taiSan.TS_DAT.DIA_BAN_ID = null;
                            }
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        taiSan.TS_NHA = taiSanDongBoModel.TS_NHA.ToTaiSan<TaiSanNhaDBModel, TaiSanNhaApi>(taiSan.TS_NHA);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        taiSan.TS_OTO = taiSanDongBoModel.TS_OTO.ToTaiSan<TaiSanOtoDBModel, TaiSanOtoApi>(taiSan.TS_OTO);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    {
                        taiSan.TS_VKT = taiSanDongBoModel.TS_VKT.ToTaiSan<TaiSanVktDBModel, TaiSanVktApi>(taiSan.TS_VKT);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    {
                        taiSan.TS_MAY_MOC = taiSanDongBoModel.TS_MAY_MOC.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_MAY_MOC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    {
                        taiSan.TS_PTK = taiSanDongBoModel.TS_PTK.ToTaiSan<TaiSanOtoDBModel, TaiSanOtoApi>(taiSan.TS_PTK);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    {
                        taiSan.TS_HUU_HINH_KHAC = taiSanDongBoModel.TS_HUU_HINH_KHAC.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_HUU_HINH_KHAC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    {
                        taiSan.TS_DAC_THU = taiSanDongBoModel.TS_DAC_THU.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_MAY_MOC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    {
                        taiSan.TS_CLN = taiSanDongBoModel.TS_CLN.ToTaiSan<TaiSanClnDBModel, TaiSanClnApi>(taiSan.TS_CLN);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    {
                        taiSan.TS_VO_HINH = taiSanDongBoModel.TS_VO_HINH.ToTaiSan<TaiSanVoHinhDBModel, TaiSanVoHinhApi>(taiSan.TS_VO_HINH);
                        break;
                    }
                default:
                    break;
            }
            // Biến động
            taiSan.LST_BIEN_DONG = new List<BienDongApi>();
            foreach (var item in taiSanDongBoModel.LST_BIEN_DONG)
            {
                BienDongApi bienDongApi = new BienDongApi();
                bienDongApi.THU_TU_BIEN_DONG_ID = item.THU_TU_BIEN_DONG_ID;
                bienDongApi.ID_DB = item.ID_DB;
                bienDongApi.LOAI_TAI_SAN_DON_VI_ID = item.LOAI_TAI_SAN_DON_VI_ID;
                bienDongApi.LOAI_TAI_SAN_ID = item.LOAI_TAI_SAN_ID;
                bienDongApi.TEN_TAI_SAN = item.TEN_TAI_SAN;
                bienDongApi.NGUYEN_GIA = item.NGUYEN_GIA;
                bienDongApi.DON_VI_BO_PHAN_ID = item.DON_VI_BO_PHAN_ID;
                bienDongApi.CHUNG_TU_SO = item.CHUNG_TU_SO;
                bienDongApi.CHUNG_TU_NGAY = item.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.NGAY_BIEN_DONG = item.NGAY_BIEN_DONG.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.NGAY_SU_DUNG = item.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.LOAI_BIEN_DONG_ID = item.LOAI_BIEN_DONG_ID;
                bienDongApi.GHI_CHU = item.GHI_CHU;
                bienDongApi.LY_DO_BIEN_DONG_ID = item.LY_DO_BIEN_DONG_ID;
                if (bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG
                    || bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.DIEU_CHUYEN
                    || bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO;
                bienDongApi.LY_DO_BIEN_DONG_MA = item.LY_DO_BIEN_DONG_MA;
                bienDongApi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;// mặc định =2 đã duyệt.
                bienDongApi.NGAY_TAO = DateTime.Now;
                bienDongApi.QUYET_DINH_NGAY = item.QUYET_DINH_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.QUYET_DINH_SO = item.QUYET_DINH_SO;
                bienDongApi.NGAY_DUYET = item.NGAY_DUYET.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.DON_VI_MA = taiSan.MA_DON_VI;
                //biến động chi tiết
                bienDongApi.MUC_DICH_SU_DUNG_MA = item.MUC_DICH_SU_DUNG_MA;
                bienDongApi.MUC_DICH_SU_DUNG_ID = item.MUC_DICH_SU_DUNG_ID;
                bienDongApi.NHAN_HIEU = item.NHAN_HIEU;
                bienDongApi.SO_HIEU = item.SO_HIEU;
                bienDongApi.DIA_BAN_MA = item.DIA_BAN_MA;
                bienDongApi.DIA_BAN_ID = item.DIA_BAN_ID;
                bienDongApi.DIA_CHI = item.DIA_CHI;
                bienDongApi.DAT_TONG_DIEN_TICH = item.DAT_TONG_DIEN_TICH;
                bienDongApi.HM_TY_LE_HAO_MON = item.HM_TY_LE_HAO_MON;
                bienDongApi.GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
                bienDongApi.NHA_SO_TANG = item.NHA_SO_TANG;
                bienDongApi.NHA_NAM_XAY_DUNG = item.NHA_NAM_XAY_DUNG;
                bienDongApi.NHA_DIEN_TICH_XD = item.NHA_DIEN_TICH_XD;
                bienDongApi.NHA_TONG_DIEN_TICH_XD = item.NHA_TONG_DIEN_TICH_XD;
                bienDongApi.VKT_DIEN_TICH = item.VKT_DIEN_TICH;
                bienDongApi.VKT_THE_TICH = item.VKT_THE_TICH;
                bienDongApi.VKT_CHIEU_DAI = item.VKT_CHIEU_DAI;
                bienDongApi.OTO_BIEN_KIEM_SOAT = item.OTO_BIEN_KIEM_SOAT;
                bienDongApi.OTO_SO_CHO_NGOI = item.OTO_SO_CHO_NGOI;
                bienDongApi.OTO_CHUC_DANH_MA = item.OTO_CHUC_DANH_MA;
                bienDongApi.OTO_NHAN_XE_MA = item.OTO_NHAN_XE_MA;
                bienDongApi.OTO_TAI_TRONG = item.OTO_TAI_TRONG;
                bienDongApi.OTO_XI_LANH = item.OTO_XI_LANH;
                bienDongApi.OTO_CONG_XUAT = item.OTO_CONG_XUAT;
                bienDongApi.OTO_SO_KHUNG = item.OTO_SO_KHUNG;
                bienDongApi.OTO_SO_MAY = item.OTO_SO_MAY;
                bienDongApi.THONG_SO_KY_THUAT = item.THONG_SO_KY_THUAT;
                bienDongApi.CLN_SO_NAM = item.CLN_SO_NAM;
                bienDongApi.DON_VI_NHAN_DIEU_CHUYEN_MA = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
                bienDongApi.HINH_THUC_XU_LY_MA = item.HINH_THUC_XU_LY_MA;
                bienDongApi.IS_BAN_THANH_LY = item.IS_BAN_THANH_LY;
                #region hồ sơ giấy tờ
                if (item.HO_SO_GIAY_TO != null)
                {
                    bienDongApi = PrepareDataJson(bienDongApi, item.HO_SO_GIAY_TO);
                    bienDongApi.DATA_JSON = new
                    {
                        HS_CNQSD_SO = bienDongApi.HS_CNQSD_SO,
                        HS_CNQSD_NGAY = bienDongApi.HS_CNQSD_SO == null ? null : bienDongApi.HS_CNQSD_NGAY,
                        HS_QUYET_DINH_GIAO_SO = bienDongApi.HS_QUYET_DINH_GIAO_SO,
                        HS_QUYET_DINH_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_GIAO_SO == null ? null : bienDongApi.HS_QUYET_DINH_GIAO_NGAY,
                        HS_CHUYEN_NHUONG_SD_SO = bienDongApi.HS_CHUYEN_NHUONG_SD_SO,
                        HS_CHUYEN_NHUONG_SD_NGAY = bienDongApi.HS_CHUYEN_NHUONG_SD_SO == null ? null : bienDongApi.HS_CHUYEN_NHUONG_SD_NGAY,
                        HS_QUYET_DINH_CHO_THUE_SO = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO,
                        HS_QUYET_DINH_CHO_THUE_NGAY = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO == null ? null : bienDongApi.HS_QUYET_DINH_CHO_THUE_NGAY,
                        HS_KHAC = bienDongApi.HS_KHAC,
                        HS_QUYET_DINH_BAN_GIAO = bienDongApi.HS_QUYET_DINH_BAN_GIAO,
                        HS_QUYET_DINH_BAN_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_BAN_GIAO == null ? null : bienDongApi.HS_QUYET_DINH_BAN_GIAO_NGAY,
                        HS_HOP_DONG_CHO_THUE_SO = bienDongApi.HS_HOP_DONG_CHO_THUE_SO,
                        HS_HOP_DONG_CHO_THUE_NGAY = bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY == null ? null : bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY,
                        HS_PHAP_LY_KHAC = bienDongApi.HS_PHAP_LY_KHAC,
                        HS_PHAP_LY_KHAC_NGAY = bienDongApi.HS_PHAP_LY_KHAC == null ? null : bienDongApi.HS_PHAP_LY_KHAC_NGAY,
                        HS_BIEN_BAN_NGHIEM_THU = bienDongApi.HS_BIEN_BAN_NGHIEM_THU,
                        HS_BIEN_BAN_NGHIEM_THU_NGAY = bienDongApi.HS_BIEN_BAN_NGHIEM_THU == null ? null : bienDongApi.HS_BIEN_BAN_NGHIEM_THU_NGAY
                    }.toStringJson(isIgnoreNull: false, formatDate: "yyyy-MM-dd HH:mm:ss");
                }
                #endregion
                #region Hiện trạng sử dụng
                bienDongApi.HIEN_TRANG_BD = new List<TaiSanHienTrangSuDungApi>();
                foreach (var hienTrang in item.LST_HIEN_TRANG)
                {
                    if (hienTrang.HIEN_TRANG_ID > 0)
                    {
                        TaiSanHienTrangSuDungApi hienTrangSuDungApi = new TaiSanHienTrangSuDungApi()
                        {
                            HIEN_TRANG_ID = hienTrang.HIEN_TRANG_ID,
                            GIA_TRI_CHECKBOX = hienTrang.GIA_TRI_CHECKBOX,
                            GIA_TRI_NUMBER = hienTrang.GIA_TRI_NUMBER,
                            TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(hienTrang.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG,
                            KIEU_DU_LIEU_ID = (taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox
                        };
                        bienDongApi.HIEN_TRANG_BD.Add(hienTrangSuDungApi);
                    }

                }
                var ListHienTrangDaCo = bienDongApi.HIEN_TRANG_BD.Select(m => m.HIEN_TRANG_ID);
                var ListHienTrang = _hienTrangService.GetHienTrangs(taiSan.LOAI_HINH_TAI_SAN_ID);
                foreach (var ht in ListHienTrang)
                {
                    if (!ListHienTrangDaCo.Contains(ht.ID))
                    {
                        TaiSanHienTrangSuDungApi htsd = new TaiSanHienTrangSuDungApi();
                        htsd.HIEN_TRANG_ID = ht.ID;
                        htsd.TEN_HIEN_TRANG = ht.TEN_HIEN_TRANG;
                        htsd.KIEU_DU_LIEU_ID = taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox;
                        if (taiSan.LOAI_HINH_TAI_SAN_ID != (decimal)enumLOAI_HINH_TAI_SAN.DAT && taiSan.LOAI_HINH_TAI_SAN_ID != (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                        {
                            htsd.GIA_TRI_CHECKBOX = false;
                        }
                        bienDongApi.HIEN_TRANG_BD.Add(htsd);
                    }
                }
                var ListHienTrangObj = bienDongApi.HIEN_TRANG_BD.Select(m =>
                    new ObjHienTrang_Entity()
                    {
                        HienTrangId = m.HIEN_TRANG_ID,
                        GiaTriText = m.GIA_TRI_TEXT,
                        GiaTriNumber = m.GIA_TRI_NUMBER,
                        GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
                        NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                        TenHienTrang = m.TEN_HIEN_TRANG,
                        KieuDuLieuId = m.KIEU_DU_LIEU_ID
                    }).ToList();
                var HienTrangList = new HienTrangList_Entity()
                {
                    // TaiSanId = taiSan.ID,
                    lstObjHienTrang = ListHienTrangObj
                };
                bienDongApi.HTSD_JSON = HienTrangList.toStringJson();
                #endregion
                #region Nguồn vốn
                bienDongApi.NGUON_VON_BD = new List<TaiSanNguonVonApi>();
                if (item.NV_HDSN.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_HDSN,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonHoatDongSuNghiep,
                        TEN_NGUON_VON = "Quỹ phát triển hoạt động sự nghiệp",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_NGAN_SACH.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_NGAN_SACH,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonNganSach,
                        TEN_NGUON_VON = "Nguồn ngân sách",

                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_NGUON_KHAC.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_NGUON_KHAC,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonKhac,
                        TEN_NGUON_VON = "Nguồn khác",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_VIEN_TRO.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_VIEN_TRO,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonVienTro,
                        TEN_NGUON_VON = "Nguồn viện trợ",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                #endregion
                taiSan.LST_BIEN_DONG.Add(bienDongApi);
            }
            if (taiSanDongBoModel.LST_HAO_MON != null)
            {
                taiSan.LST_HAO_MON = taiSanDongBoModel.LST_HAO_MON.Select(c => new DB_KT_HaoMonModel()
                {
                    DB_ID = c.DB_ID,
                    GIA_TRI_HAO_MON = c.GIA_TRI_HAO_MON,
                    ID = c.ID,
                    MA_TAI_SAN = c.MA_TAI_SAN,
                    MA_TAI_SAN_DB = c.MA_TAI_SAN_DB,
                    NAM_HAO_MON = c.NAM_HAO_MON,
                    TAI_SAN_ID = c.TAI_SAN_ID,
                    TONG_GIA_TRI_CON_LAI = c.TONG_GIA_TRI_CON_LAI,
                    TONG_HAO_MON_LUY_KE = c.TONG_HAO_MON_LUY_KE,
                    TONG_NGUYEN_GIA = c.TONG_NGUYEN_GIA,
                    TY_LE_HAO_MON = c.TY_LE_HAO_MON
                }).ToList();
            }
            if (taiSanDongBoModel.LST_KHAU_HAO != null)
            {
                taiSan.LST_KHAU_HAO = taiSanDongBoModel.LST_KHAU_HAO.Select(c => new DB_KT_KhauHaoModel()
                {
                    ID = c.ID,
                    TONG_NGUYEN_GIA = c.TONG_NGUYEN_GIA,
                    TONG_GIA_TRI_CON_LAI = c.TONG_GIA_TRI_CON_LAI,
                    DB_ID = c.DB_ID,
                    GIA_TRI_KHAU_HAO = c.GIA_TRI_KHAU_HAO,
                    MA_TAI_SAN = c.MA_TAI_SAN,
                    MA_TAI_SAN_DB = c.MA_TAI_SAN_DB,
                    NAM_KHAU_HAO = c.NAM_KHAU_HAO,
                    THANG_KHAU_HAO = c.THANG_KHAU_HAO,
                    TONG_KHAU_HAO_LUY_KE = c.TONG_KHAU_HAO_LUY_KE,
                    TY_LE_KHAU_HAO = c.TY_LE_KHAU_HAO
                }).ToList();
            }

            var taiSanJson = taiSan.toStringJson(isIgnoreNull: false);
            //  taiSan.
            var result = _dBTaiSanService.insertOrupdateTaiSanByJson(taiSanJson, null, true);
            return result;
        }
        //hoanglv
        public MessageReturn InsertTaiSanDBByStore(DBTaiSan dBTaiSan)
        {
            ThongTinTaiSanDBModel taiSanDongBoModel = JsonConvert.DeserializeObject<ThongTinTaiSanDBModel>(dBTaiSan.DATA_JSON);
            TaiSanDongBoApi taiSan = new TaiSanDongBoApi();
            taiSan.NGUON_TAI_SAN_ID = taiSanDongBoModel.NGUON_TAI_SAN_ID;
            taiSan.ID = taiSanDongBoModel.ID;
            taiSan.TEN_TAI_SAN = taiSanDongBoModel.TEN;
            taiSan.MA_TAI_SAN = dBTaiSan.QLDKTS_MA;
            taiSan.DB_MA = dBTaiSan.DB_MA;
            taiSan.NGAY_SU_DUNG = taiSanDongBoModel.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.MA_LOAI_TAI_SAN = taiSanDongBoModel.LOAI_TAI_SAN_MA;
            if (taiSanDongBoModel.LOAI_TAI_SAN_MA != null)
            {
                taiSan.LOAI_TAI_SAN_ID = _loaiTaiSanService.GetLoaiTaiSanByMa(taiSanDongBoModel.LOAI_TAI_SAN_MA).ID;
            }
            if (taiSanDongBoModel.LOAI_TAI_SAN_DON_VI_MA != null)
            {
                taiSan.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViService.GetLoaiTaiSanVoHinhByMa(taiSanDongBoModel.LOAI_TAI_SAN_DON_VI_MA).ID;
            }
            if (taiSanDongBoModel.DON_VI_BO_PHAN_MA != null)
            {
                taiSan.DON_VI_BO_PHAN_ID = _donViBoPhanService.GetDonViBoPhanByMa(taiSanDongBoModel.DON_VI_BO_PHAN_MA).ID;
            }
            if (!String.IsNullOrEmpty(taiSanDongBoModel.NGAY_NHAP))
                taiSan.NGAY_NHAP = taiSanDongBoModel.NGAY_NHAP.toDateSys("yyyy-MM-dd HH:mm:ss");
            else
                taiSan.NGAY_NHAP = taiSanDongBoModel.NGAY_DANG_KY.toDateSys("yyyy-MM-dd HH:mm:ss");
            //taiSan.NGAY_DUYET = taiSanDongBoModel.Ngay.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.LY_DO_BIEN_DONG_MA = taiSanDongBoModel.LY_DO_BIEN_DONG_MA;
            taiSan.LOAI_HINH_TAI_SAN_ID = taiSanDongBoModel.LOAI_HINH_TAI_SAN_ID;
            taiSan.NGUYEN_GIA_BAN_DAU = taiSanDongBoModel.NGUYEN_GIA_BAN_DAU;
            taiSan.MA_DON_VI = taiSanDongBoModel.MA_DON_VI;
            taiSan.GHI_CHU = taiSanDongBoModel.GHI_CHU;
            taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;// trạng thái mặc định là đã duyệt
            if (!string.IsNullOrEmpty(taiSanDongBoModel.NAM_SAN_XUAT))
            {
                taiSan.NAM_SAN_XUAT = decimal.Parse(taiSanDongBoModel.NAM_SAN_XUAT);
            }
            taiSan.NUOC_SAN_XUAT_ID = taiSanDongBoModel.NUOC_SAN_XUAT_ID;
            taiSan.GIA_MUA_TIEP_NHAN = taiSanDongBoModel.GIA_MUA_TIEP_NHAN;
            taiSan.GIA_HOA_DON = taiSanDongBoModel.GIA_HOA_DON;
            taiSan.LY_DO_BIEN_DONG_MA = taiSanDongBoModel.LY_DO_BIEN_DONG_MA;
            taiSan.QUYET_DINH_SO = taiSanDongBoModel.QUYET_DINH_SO;
            taiSan.QUYET_DINH_NGAY = taiSanDongBoModel.QUYET_DINH_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.CHUNG_TU_SO = taiSanDongBoModel.CHUNG_TU_SO;
            taiSan.CHUNG_TU_NGAY = taiSanDongBoModel.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
            taiSan.MIEN_THUE_SO_TIEN = taiSanDongBoModel.MIEN_THUE_SO_TIEN;
            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
            {
                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    {
                        taiSan.TS_DAT = taiSanDongBoModel.TS_DAT.ToTaiSan<TaiSanDatDBModel, TaiSanDatApi>(taiSan.TS_DAT);
                        if (taiSan.TS_DAT.DIA_BAN_ID > 0)
                        {
                            var diaBan = _diaBanService.GetDiaBanById(taiSan.TS_DAT.DIA_BAN_ID.Value);
                            if (diaBan == null)
                            {
                                taiSan.TS_DAT.DIA_BAN_ID = null;
                            }
                        }
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    {
                        taiSan.TS_NHA = taiSanDongBoModel.TS_NHA.ToTaiSan<TaiSanNhaDBModel, TaiSanNhaApi>(taiSan.TS_NHA);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        taiSan.TS_OTO = taiSanDongBoModel.TS_OTO.ToTaiSan<TaiSanOtoDBModel, TaiSanOtoApi>(taiSan.TS_OTO);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    {
                        taiSan.TS_VKT = taiSanDongBoModel.TS_VKT.ToTaiSan<TaiSanVktDBModel, TaiSanVktApi>(taiSan.TS_VKT);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    {
                        taiSan.TS_MAY_MOC = taiSanDongBoModel.TS_MAY_MOC.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_MAY_MOC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    {
                        taiSan.TS_PTK = taiSanDongBoModel.TS_PTK.ToTaiSan<TaiSanOtoDBModel, TaiSanOtoApi>(taiSan.TS_PTK);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    {
                        taiSan.TS_HUU_HINH_KHAC = taiSanDongBoModel.TS_HUU_HINH_KHAC.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_HUU_HINH_KHAC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    {
                        taiSan.TS_DAC_THU = taiSanDongBoModel.TS_DAC_THU.ToTaiSan<TaiSanMayMocDBModel, TaiSanMayMocApi>(taiSan.TS_MAY_MOC);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    {
                        taiSan.TS_CLN = taiSanDongBoModel.TS_CLN.ToTaiSan<TaiSanClnDBModel, TaiSanClnApi>(taiSan.TS_CLN);
                        break;
                    }
                case (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    {
                        taiSan.TS_VO_HINH = taiSanDongBoModel.TS_VO_HINH.ToTaiSan<TaiSanVoHinhDBModel, TaiSanVoHinhApi>(taiSan.TS_VO_HINH);
                        break;
                    }
                default:
                    break;
            }
            // Biến động
            taiSan.LST_BIEN_DONG = new List<BienDongApi>();
            foreach (var item in taiSanDongBoModel.LST_BIEN_DONG)
            {
                BienDongApi bienDongApi = new BienDongApi();
                bienDongApi.THU_TU_BIEN_DONG_ID = item.THU_TU_BIEN_DONG_ID;
                bienDongApi.ID_DB = item.ID_DB;
                if (item.LOAI_TAI_SAN_MA != null)
                {
                    bienDongApi.LOAI_TAI_SAN_ID = _loaiTaiSanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA).ID;
                }
                if (item.LOAI_TAI_SAN_DON_VI_MA != null)
                {
                    bienDongApi.LOAI_TAI_SAN_DON_VI_ID = _loaiTaiSanDonViService.GetLoaiTaiSanVoHinhByMa(item.LOAI_TAI_SAN_DON_VI_MA).ID;
                }
                if (item.DON_VI_BO_PHAN_MA != null)
                {
                    bienDongApi.DON_VI_BO_PHAN_ID = _donViBoPhanService.GetDonViBoPhanByMa(item.DON_VI_BO_PHAN_MA).ID;
                }
                bienDongApi.TEN_TAI_SAN = item.TEN_TAI_SAN;
                bienDongApi.NGUYEN_GIA = item.NGUYEN_GIA;
                bienDongApi.CHUNG_TU_SO = item.CHUNG_TU_SO;
                bienDongApi.CHUNG_TU_NGAY = item.CHUNG_TU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.NGAY_BIEN_DONG = item.NGAY_BIEN_DONG.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.NGAY_SU_DUNG = item.NGAY_SU_DUNG.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.LOAI_BIEN_DONG_ID = item.LOAI_BIEN_DONG_ID;
                bienDongApi.GHI_CHU = item.GHI_CHU;
                if (item.LY_DO_BIEN_DONG_MA != null)
                {
                    bienDongApi.LY_DO_BIEN_DONG_ID = _lyDoBienDongService.GetLyDoBienDongByMa(item.LY_DO_BIEN_DONG_MA).ID;
                }
                if (bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG
                    || bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.DIEU_CHUYEN
                    || bienDongApi.LY_DO_BIEN_DONG_ID == (decimal)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO;
                //bienDongApi.LY_DO_BIEN_DONG_MA = item.LY_DO_BIEN_DONG_MA;
                bienDongApi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;// mặc định =2 đã duyệt.
                bienDongApi.NGAY_TAO = DateTime.Now;
                bienDongApi.QUYET_DINH_NGAY = item.QUYET_DINH_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.QUYET_DINH_SO = item.QUYET_DINH_SO;
                bienDongApi.NGAY_DUYET = item.NGAY_DUYET.toDateSys("yyyy-MM-dd HH:mm:ss");
                bienDongApi.DON_VI_MA = taiSan.MA_DON_VI;
                //biến động chi tiết
                bienDongApi.MUC_DICH_SU_DUNG_MA = item.MUC_DICH_SU_DUNG_MA;
                bienDongApi.MUC_DICH_SU_DUNG_ID = item.MUC_DICH_SU_DUNG_ID;
                bienDongApi.NHAN_HIEU = item.NHAN_HIEU;
                bienDongApi.SO_HIEU = item.SO_HIEU;
                bienDongApi.DIA_BAN_MA = item.DIA_BAN_MA;
                bienDongApi.DIA_BAN_ID = item.DIA_BAN_ID;
                bienDongApi.DIA_CHI = item.DIA_CHI;
                bienDongApi.DAT_TONG_DIEN_TICH = item.DAT_TONG_DIEN_TICH;
                bienDongApi.HM_TY_LE_HAO_MON = item.HM_TY_LE_HAO_MON;
                bienDongApi.GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
                bienDongApi.NHA_SO_TANG = item.NHA_SO_TANG;
                bienDongApi.NHA_NAM_XAY_DUNG = item.NHA_NAM_XAY_DUNG;
                bienDongApi.NHA_DIEN_TICH_XD = item.NHA_DIEN_TICH_XD;
                bienDongApi.NHA_TONG_DIEN_TICH_XD = item.NHA_TONG_DIEN_TICH_XD;
                bienDongApi.VKT_DIEN_TICH = item.VKT_DIEN_TICH;
                bienDongApi.VKT_THE_TICH = item.VKT_THE_TICH;
                bienDongApi.VKT_CHIEU_DAI = item.VKT_CHIEU_DAI;
                bienDongApi.OTO_BIEN_KIEM_SOAT = item.OTO_BIEN_KIEM_SOAT;
                bienDongApi.OTO_SO_CHO_NGOI = item.OTO_SO_CHO_NGOI;
                bienDongApi.OTO_CHUC_DANH_MA = item.OTO_CHUC_DANH_MA;
                bienDongApi.OTO_NHAN_XE_MA = item.OTO_NHAN_XE_MA;
                bienDongApi.OTO_TAI_TRONG = item.OTO_TAI_TRONG;
                bienDongApi.OTO_XI_LANH = item.OTO_XI_LANH;
                bienDongApi.OTO_CONG_XUAT = item.OTO_CONG_XUAT;
                bienDongApi.OTO_SO_KHUNG = item.OTO_SO_KHUNG;
                bienDongApi.OTO_SO_MAY = item.OTO_SO_MAY;
                bienDongApi.THONG_SO_KY_THUAT = item.THONG_SO_KY_THUAT;
                bienDongApi.CLN_SO_NAM = item.CLN_SO_NAM;
                bienDongApi.DON_VI_NHAN_DIEU_CHUYEN_MA = item.DON_VI_NHAN_DIEU_CHUYEN_MA;
                bienDongApi.HINH_THUC_XU_LY_MA = item.HINH_THUC_XU_LY_MA;
                bienDongApi.IS_BAN_THANH_LY = item.IS_BAN_THANH_LY;
                #region hồ sơ giấy tờ
                if (item.HO_SO_GIAY_TO != null)
                {
                    bienDongApi = PrepareDataJson(bienDongApi, item.HO_SO_GIAY_TO);
                    bienDongApi.DATA_JSON = new
                    {
                        HS_CNQSD_SO = bienDongApi.HS_CNQSD_SO,
                        HS_CNQSD_NGAY = bienDongApi.HS_CNQSD_SO == null ? null : bienDongApi.HS_CNQSD_NGAY,
                        HS_QUYET_DINH_GIAO_SO = bienDongApi.HS_QUYET_DINH_GIAO_SO,
                        HS_QUYET_DINH_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_GIAO_SO == null ? null : bienDongApi.HS_QUYET_DINH_GIAO_NGAY,
                        HS_CHUYEN_NHUONG_SD_SO = bienDongApi.HS_CHUYEN_NHUONG_SD_SO,
                        HS_CHUYEN_NHUONG_SD_NGAY = bienDongApi.HS_CHUYEN_NHUONG_SD_SO == null ? null : bienDongApi.HS_CHUYEN_NHUONG_SD_NGAY,
                        HS_QUYET_DINH_CHO_THUE_SO = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO,
                        HS_QUYET_DINH_CHO_THUE_NGAY = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO == null ? null : bienDongApi.HS_QUYET_DINH_CHO_THUE_NGAY,
                        HS_KHAC = bienDongApi.HS_KHAC,
                        HS_QUYET_DINH_BAN_GIAO = bienDongApi.HS_QUYET_DINH_BAN_GIAO,
                        HS_QUYET_DINH_BAN_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_BAN_GIAO == null ? null : bienDongApi.HS_QUYET_DINH_BAN_GIAO_NGAY,
                        HS_HOP_DONG_CHO_THUE_SO = bienDongApi.HS_HOP_DONG_CHO_THUE_SO,
                        HS_HOP_DONG_CHO_THUE_NGAY = bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY == null ? null : bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY,
                        HS_PHAP_LY_KHAC = bienDongApi.HS_PHAP_LY_KHAC,
                        HS_PHAP_LY_KHAC_NGAY = bienDongApi.HS_PHAP_LY_KHAC == null ? null : bienDongApi.HS_PHAP_LY_KHAC_NGAY,
                        HS_BIEN_BAN_NGHIEM_THU = bienDongApi.HS_BIEN_BAN_NGHIEM_THU,
                        HS_BIEN_BAN_NGHIEM_THU_NGAY = bienDongApi.HS_BIEN_BAN_NGHIEM_THU == null ? null : bienDongApi.HS_BIEN_BAN_NGHIEM_THU_NGAY
                    }.toStringJson(isIgnoreNull: false, formatDate: "yyyy-MM-dd HH:mm:ss");
                }
                #endregion
                #region Hiện trạng sử dụng
                bienDongApi.HIEN_TRANG_BD = new List<TaiSanHienTrangSuDungApi>();
                foreach (var hienTrang in item.LST_HIEN_TRANG)
                {
                    if (hienTrang.HIEN_TRANG_ID > 0)
                    {
                        TaiSanHienTrangSuDungApi hienTrangSuDungApi = new TaiSanHienTrangSuDungApi()
                        {
                            HIEN_TRANG_ID = hienTrang.HIEN_TRANG_ID,
                            GIA_TRI_CHECKBOX = hienTrang.GIA_TRI_CHECKBOX,
                            GIA_TRI_NUMBER = hienTrang.GIA_TRI_NUMBER,
                            TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(hienTrang.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG,
                            KIEU_DU_LIEU_ID = (taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox
                        };
                        bienDongApi.HIEN_TRANG_BD.Add(hienTrangSuDungApi);
                    }

                }
                var ListHienTrangDaCo = bienDongApi.HIEN_TRANG_BD.Select(m => m.HIEN_TRANG_ID);
                var ListHienTrang = _hienTrangService.GetHienTrangs(taiSan.LOAI_HINH_TAI_SAN_ID);
                foreach (var ht in ListHienTrang)
                {
                    if (!ListHienTrangDaCo.Contains(ht.ID))
                    {
                        TaiSanHienTrangSuDungApi htsd = new TaiSanHienTrangSuDungApi();
                        htsd.HIEN_TRANG_ID = ht.ID;
                        htsd.TEN_HIEN_TRANG = ht.TEN_HIEN_TRANG;
                        htsd.KIEU_DU_LIEU_ID = taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox;
                        if (taiSan.LOAI_HINH_TAI_SAN_ID != (decimal)enumLOAI_HINH_TAI_SAN.DAT && taiSan.LOAI_HINH_TAI_SAN_ID != (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                        {
                            htsd.GIA_TRI_CHECKBOX = false;
                        }
                        bienDongApi.HIEN_TRANG_BD.Add(htsd);
                    }
                }
                var ListHienTrangObj = bienDongApi.HIEN_TRANG_BD.Select(m =>
                    new ObjHienTrang_Entity()
                    {
                        HienTrangId = m.HIEN_TRANG_ID,
                        GiaTriText = m.GIA_TRI_TEXT,
                        GiaTriNumber = m.GIA_TRI_NUMBER,
                        GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
                        NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                        TenHienTrang = m.TEN_HIEN_TRANG,
                        KieuDuLieuId = m.KIEU_DU_LIEU_ID
                    }).ToList();
                var HienTrangList = new HienTrangList_Entity()
                {
                    // TaiSanId = taiSan.ID,
                    lstObjHienTrang = ListHienTrangObj
                };
                bienDongApi.HTSD_JSON = HienTrangList.toStringJson();
                #endregion
                #region Nguồn vốn
                bienDongApi.NGUON_VON_BD = new List<TaiSanNguonVonApi>();
                if (item.NV_HDSN.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_HDSN,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonHoatDongSuNghiep,
                        TEN_NGUON_VON = "Quỹ phát triển hoạt động sự nghiệp",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_NGAN_SACH.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_NGAN_SACH,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonNganSach,
                        TEN_NGUON_VON = "Nguồn ngân sách",

                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_NGUON_KHAC.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_NGUON_KHAC,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonKhac,
                        TEN_NGUON_VON = "Nguồn khác",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                if (item.NV_VIEN_TRO.HasValue)
                {
                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                    {
                        GIA_TRI = item.NV_VIEN_TRO,
                        NGUON_VON_ID = (decimal)enumNguonVon.NguonVienTro,
                        TEN_NGUON_VON = "Nguồn viện trợ",
                    };
                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                }
                #endregion
                taiSan.LST_BIEN_DONG.Add(bienDongApi);
            }
            if (taiSanDongBoModel.LST_HAO_MON != null)
            {
                taiSan.LST_HAO_MON = taiSanDongBoModel.LST_HAO_MON.Select(c => new DB_KT_HaoMonModel()
                {
                    DB_ID = c.DB_ID,
                    GIA_TRI_HAO_MON = c.GIA_TRI_HAO_MON,
                    ID = c.ID,
                    MA_TAI_SAN = c.MA_TAI_SAN,
                    MA_TAI_SAN_DB = c.MA_TAI_SAN_DB,
                    NAM_HAO_MON = c.NAM_HAO_MON,
                    TAI_SAN_ID = c.TAI_SAN_ID,
                    TONG_GIA_TRI_CON_LAI = c.TONG_GIA_TRI_CON_LAI,
                    TONG_HAO_MON_LUY_KE = c.TONG_HAO_MON_LUY_KE,
                    TONG_NGUYEN_GIA = c.TONG_NGUYEN_GIA,
                    TY_LE_HAO_MON = c.TY_LE_HAO_MON
                }).ToList();
            }
            if (taiSanDongBoModel.LST_KHAU_HAO != null)
            {
                taiSan.LST_KHAU_HAO = taiSanDongBoModel.LST_KHAU_HAO.Select(c => new DB_KT_KhauHaoModel()
                {
                    ID = c.ID,
                    TONG_NGUYEN_GIA = c.TONG_NGUYEN_GIA,
                    TONG_GIA_TRI_CON_LAI = c.TONG_GIA_TRI_CON_LAI,
                    DB_ID = c.DB_ID,
                    GIA_TRI_KHAU_HAO = c.GIA_TRI_KHAU_HAO,
                    MA_TAI_SAN = c.MA_TAI_SAN,
                    MA_TAI_SAN_DB = c.MA_TAI_SAN_DB,
                    NAM_KHAU_HAO = c.NAM_KHAU_HAO,
                    THANG_KHAU_HAO = c.THANG_KHAU_HAO,
                    TONG_KHAU_HAO_LUY_KE = c.TONG_KHAU_HAO_LUY_KE,
                    TY_LE_KHAU_HAO = c.TY_LE_KHAU_HAO
                }).ToList();
            }

            var taiSanJson = taiSan.toStringJson(isIgnoreNull: false);
            //  taiSan.
            var result = _dBTaiSanService.insertOrupdateTaiSanByJson(taiSanJson, null, true);
            return result;
        }
        #endregion
        #region Gửi dữ liệu sang kho
        public MessageReturn GuiTaiSanSangKho()
        {
            return MessageReturn.CreateErrorMessage("Ok");
        }
        public void DongBoDanhMuc<TModel>(List<TModel> model) where TModel : BaseGSModel
        {
            ResponseApi response = new ResponseApi();

            string Name = typeof(TModel).Name;
            Name = Name.Replace("Model", "");
            _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingDanhMuc/Update" + Name, model, _workContext.CurrentDonVi.ID, (int)enumLevelQueueProcessDB.HIGHEST);
            //PostObjectDanhMuc("ConsumingDanhMuc/Update" + Name, model);
        }
        public async Task<ResponseApi> DongBoThuCongDanhMuc(List<NguoiDungModel> model)
        {
            ResponseApi response = new ResponseApi();

            string Name = typeof(NguoiDungModel).Name;
            Name = Name.Replace("Model", "");
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingDanhMuc/Update" + Name, model, _workContext.CurrentDonVi.ID);
            response = await PostObjectDanhMucRes("ConsumingDanhMuc/Update" + Name, model);
            return response;
        }
        public async void DongBoThuCongDanhMuc2<TModel>(List<TModel> model) where TModel : BaseGSModel
        {
            string Name = typeof(TModel).Name;
            Name = Name.Replace("Model", "");
            await PostObjectDanhMucRes("ConsumingDanhMuc/Update" + Name, model);
        }
        private async Task<ResponseApi> PostObjectDanhMucRes(string action, object obj)
        {
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>(action, obj, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }

        void PostObjectDanhMuc(string action, object obj)
        {
            var response = _gSAPIService.PostObjectGSApi<ResponseApi>(action, obj, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);

        }
        public void XoaDanhMuc<TModel>(TModel model) where TModel : BaseGSEntityModel
        {
            string Name = model.GetType().Name.Replace("Model", "");
            _hoatDongService.InsertHoatDong(enumHoatDong.DbCho, "Gửi dữ liệu xóa danh mục", model, Name);
            _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingDanhMuc/Delete" + Name, model, _workContext.CurrentDonVi.ID);
            //PostObjectDanhMuc("ConsumingDanhMuc/Delete" + Name, model);

        }
        public async Task<ResponseApi> DongBoTaiSanThuCong(List<decimal> ListTaiSanId, decimal LoaiBienDongId = 0)
        {
            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanId = ListTaiSanId,
                LoaiBienDongId = LoaiBienDongId,
                DonViId = _workContext.CurrentDonVi.ID
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoThuCong", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }

        public async Task<ResponseApi> ResetMatKhauNguoiDung(List<decimal> ListTaiSanId, decimal LoaiBienDongId = 0)
        {
            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanId = ListTaiSanId,
                LoaiBienDongId = LoaiBienDongId,
                DonViId = _workContext.CurrentDonVi.ID
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoThuCong", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }

        public async Task<MessageReturn> GetMaDongBo(DateTime? NgayDongBo = null, List<string> ListMaTaiSan = null)
        {

            var obj = new
            {
                NgayDongBo = NgayDongBo,
                ListMaTaiSan = ListMaTaiSan
            };
            var response = await _gSAPIService.GetObjectGSApi<MessageReturn>("ConsumingTaiSan/CapNhatMaTaiSanKho", null, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public void DongBoBienDong(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel)
        {
            // insert Queue
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong" , prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            _hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ biến động từ web => api tsc {_nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)prameterDongBoTaiSanModel.LoaiBienDongId)} ngày {DateTime.Now}", prameterDongBoTaiSanModel);
            if (prameterDongBoTaiSanModel.LoaiBienDongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO || prameterDongBoTaiSanModel.LoaiBienDongId == (decimal)enumLOAI_LY_DO_TANG_GIAM.NHAP_SO_DU_DAU_KY)
            {
                //_gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/UpdateBienDong", prameterDongBoTaiSanModel, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong", prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID, (int)enumLevelQueueProcessDB.HIGHEST);
            }
            else
            {
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong", prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            }
        }
        public async Task<ResponseApi> DongBoBienDongTuFile(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel)
        {
            // insert Queue
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong" , prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            _hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ biến động từ file => api tsc{_nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)prameterDongBoTaiSanModel.LoaiBienDongId)} ngày {DateTime.Now}", prameterDongBoTaiSanModel);
            var respond = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoBienDongFromFile", prameterDongBoTaiSanModel, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return respond;

        }
        public async Task<ResponseApi> DongBoHaoMonTuFile(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel)
        {
            // insert Queue
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong" , prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            //_hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ hao mòn từ file => api tsc{_nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)prameterDongBoTaiSanModel.LoaiBienDongId)} ngày { DateTime.Now}", prameterDongBoTaiSanModel);
            var respond = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoHaoMonFromFile", prameterDongBoTaiSanModel, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return respond;

        }
        public void XoaBienDong(ParameterXoaBienDong parameterXoaBienDong)
        {
            _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/XoaBienDong", parameterXoaBienDong, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
        }
        public async Task<ResponseApi> DongBoTaiSanTheoDonVi(decimal DonViId, decimal LoaiBienDongId)
        {
            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                LoaiBienDongId = LoaiBienDongId,
                DonViId = DonViId
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoThuCong", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public void DongBoTSTD<TModel>(List<TModel> model) where TModel : BaseGSModel
        {
            ResponseApi response = new ResponseApi();

            string Name = typeof(TModel).Name;
            Name = Name.Replace("Model", "");
            _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/Update" + Name, model, _workContext.CurrentDonVi.ID);
            //PostObjectDanhMuc("ConsumingDanhMuc/Update" + Name, model);
        }
        #endregion
        #region ImportExcel
        public List<ImportExcelTaiSanDatNhaModel> ImportFileExcel(List<ImportExcelTaiSanDatNhaModel> DatNha = null)
        {
            List<ImportExcelTaiSanDatNhaModel> ListSuccess = new List<ImportExcelTaiSanDatNhaModel>();
            List<ImportExcelTaiSanDatNhaModel> ListError = new List<ImportExcelTaiSanDatNhaModel>();
            var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            bool IsDuyet = true;
            //if (donViCauHinh != null)
            //{
            //    IsDuyet = donViCauHinh.IsAutoDuyetTaiSanImport;
            //}
            foreach (var item in DatNha)
            {
                try
                {
                    List<string> Errors = new List<string>();
                    TaiSanDongBoApi taiSanImport = new TaiSanDongBoApi();
                    //insert tài sản để cập nhật mã
                    // Loại tài sản 
                    #region validate
                    string MaLoaiTaiSan = item.LOAI_TAI_SAN_MA.Split('-')[0].Trim();
                    LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(MaLoaiTaiSan);
                    if (loaiTaiSan == null)
                    {
                        Errors.Add("Loại tài sản không đúng");
                    }
                    // thông tin địa bàn
                    decimal? DiaBanId = null;
                    if (!string.IsNullOrEmpty(item.MA_TINH))
                    {
                        DiaBan diaBan = _diaBanService.GetCacheDiaBanByMa(item.MA_TINH);
                        if (diaBan == null)
                        {
                            Errors.Add("Mã tỉnh không tồn tại");
                        }
                        else
                        {
                            DiaBanId = diaBan.ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(item.MA_HUYEN))
                    {
                        DiaBan diaBan = _diaBanService.GetCacheDiaBanByMa(item.MA_HUYEN);
                        if (diaBan == null)
                        {
                            Errors.Add("Mã huyện không tồn tại");
                        }
                        else
                        {
                            DiaBanId = diaBan.ID;
                        }

                    }
                    if (!string.IsNullOrEmpty(item.MA_XA))
                    {
                        DiaBan diaBan = _diaBanService.GetCacheDiaBanByMa(item.MA_XA);
                        if (diaBan == null)
                        {
                            Errors.Add("Mã xã không tồn tại");
                        }
                        else
                        {
                            DiaBanId = diaBan.ID;
                        }

                    }
                    item.LY_DO_BIEN_DONG_MA = item.LY_DO_BIEN_DONG_MA.Split('-')[0].Trim();
                    if (string.IsNullOrEmpty(item.LY_DO_BIEN_DONG_MA))
                    {
                        Errors.Add("Mã lý do tăng  bắt buộc nhập");
                    }
                    LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByMa(item.LY_DO_BIEN_DONG_MA);
                    if (lyDoBienDong == null)
                    {
                        Errors.Add("Mã lý do tăng  không tồn tại");
                    }
                    if (string.IsNullOrEmpty(item.NGAY_SU_DUNG))
                    {
                        Errors.Add("Ngày sử dụng bắt buộc nhập");
                    }
                    else
                    {
                        DateTime? NgaySuDung = item.NGAY_SU_DUNG.toDateSys("dd/MM/yyyy");
                        if (NgaySuDung == null)
                        {
                            Errors.Add("Ngày sử dụng không đúng định dạng");
                        }
                    }
                    if (string.IsNullOrEmpty(item.NGAY_BIEN_DONG))
                    {
                        Errors.Add("Ngày tăng không đúng");
                    }
                    else
                    {
                        DateTime? NgayBienDong = item.NGAY_BIEN_DONG.toDateSys("dd/MM/yyyy");
                        if (NgayBienDong == null)
                        {
                            Errors.Add("Ngày Biến động không đúng định dạng");
                        }
                    }
                    decimal TongHienTrang = 0;
                    var ListProp = item.GetType().GetProperties();
                    foreach (var prop in ListProp)
                    {
                        if (prop.Name.Contains("HTSD"))
                        {
                            TongHienTrang += ((decimal?)prop.GetValue(item)).GetValueOrDefault(0);
                        }
                    }
                    TaiSan taisandat = new TaiSan();
                    switch (loaiTaiSan.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                if (!(item.DIEN_TICH > 0))
                                {
                                    Errors.Add("Diện tích đất không đúng");
                                }
                                if (!(DiaBanId > 0))
                                {
                                    Errors.Add("Đất chưa có Địa bàn");
                                }
                                // hiện trạng sử dụng
                                if (TongHienTrang != item.DIEN_TICH)
                                {
                                    Errors.Add("Hiện trạng phải bằng diện tích đất");
                                }
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                if (!(item.DIEN_TICH_SAN_XAY_DUNG > 0))
                                {
                                    Errors.Add("Diện tích sàn sử dụng Nhà không đúng");
                                }
                                if (!(item.NHA_SO_TANG > 0))
                                {
                                    Errors.Add("Số tầng Nhà không đúng");
                                }
                                if (TongHienTrang != item.DIEN_TICH_SAN_XAY_DUNG)
                                {
                                    Errors.Add("Hiện trạng phải bằng diện tích sàn sử dụng");
                                }
                                if (!string.IsNullOrEmpty(item.TAI_SAN_DAT_MA))
                                {
                                    taisandat = _taiSanService.GetTaiSanByMa(item.TAI_SAN_DAT_MA);
                                    if (taisandat == null)
                                    {
                                        Errors.Add("Tài sản đất không tồn tại");
                                    }
                                }
                                break;
                            }
                    }
                    if (Errors.Count > 0)
                    {
                        item.Error = string.Join("<br />", Errors);
                        ListError.Add(item);
                        continue;
                    }

                    #endregion
                    TaiSan taiSan = new TaiSan();
                    taiSan.NGUON_TAI_SAN_ID = (decimal)enumNguonTaiSan.KHAC;
                    _taiSanService.InsertTaiSan(taiSan);
                    taiSan.MA = CommonHelper.GenMaTaiSan(_workContext.CurrentDonVi.MA_DON_VI, loaiTaiSan.MA, taiSan.ID);
                    taiSan.TEN = item.TEN;
                    _taiSanService.UpdateTaiSan(taiSan);
                    //prepareduliệu
                    taiSanImport.ID = taiSan.ID;
                    taiSanImport.TEN_TAI_SAN = item.TEN;
                    taiSanImport.MA_TAI_SAN = taiSan.MA;
                    taiSanImport.MA_LOAI_TAI_SAN = loaiTaiSan.MA;
                    taiSanImport.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                    taiSanImport.NGAY_NHAP = DateTime.Now;
                    taiSanImport.NGAY_SU_DUNG = item.NGAY_SU_DUNG.toDateSys();
                    taiSanImport.LOAI_HINH_TAI_SAN_ID = loaiTaiSan.LOAI_HINH_TAI_SAN_ID;
                    //nguyên giá = tổng nguồn vốn;
                    decimal NguyenGia = (item.NV_NGAN_SACH ?? 0) + (item.NV_NGUON_KHAC ?? 0);
                    taiSanImport.NGUYEN_GIA_BAN_DAU = NguyenGia;
                    taiSanImport.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
                    taiSanImport.TRANG_THAI = 2;
                    switch (taiSanImport.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                taiSanImport.TS_DAT = new TaiSanDatApi()
                                {
                                    DIEN_TICH = item.DIEN_TICH.Value,
                                    DIA_BAN_ID = DiaBanId,
                                    DIA_BAN_MA = item.TEN
                                };
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                taiSanImport.TS_NHA = new TaiSanNhaApi()
                                {
                                    NHA_SO_TANG = item.NHA_SO_TANG,
                                    DIEN_TICH_SAN_XAY_DUNG = item.DIEN_TICH_SAN_XAY_DUNG,
                                    DIEN_TICH_XAY_DUNG = item.DIEN_TICH_XAY_DUNG,
                                    NAM_XAY_DUNG = item.NAM_XAY_DUNG,
                                    TAI_SAN_DAT_MA = taisandat.MA,
                                    TAI_SAN_DAT_ID = taisandat.ID
                                };
                                break;
                            }
                    }
                    taiSanImport.LST_BIEN_DONG = new List<BienDongApi>();
                    BienDongApi bienDongApi = new BienDongApi();
                    bienDongApi.GUID = Guid.NewGuid();
                    bienDongApi.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                    bienDongApi.TEN_TAI_SAN = item.TEN;
                    bienDongApi.NGUYEN_GIA = NguyenGia;
                    bienDongApi.NGAY_BIEN_DONG = item.NGAY_BIEN_DONG.toDateSys();
                    bienDongApi.NGAY_SU_DUNG = taiSanImport.NGAY_SU_DUNG;
                    bienDongApi.LOAI_BIEN_DONG_ID = (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                    bienDongApi.LY_DO_BIEN_DONG_ID = lyDoBienDong.ID;
                    bienDongApi.LY_DO_BIEN_DONG_MA = lyDoBienDong.MA;
                    bienDongApi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
                    bienDongApi.NGAY_TAO = DateTime.Now;
                    bienDongApi.DON_VI_MA = taiSanImport.MA_DON_VI;
                    bienDongApi.GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
                    bienDongApi.DIA_BAN_ID = DiaBanId;
                    bienDongApi.DAT_GIA_TRI_QUYEN_SD_DAT = item.DAT_GIA_TRI_QUYEN_SD_DAT;
                    switch (taiSanImport.LOAI_HINH_TAI_SAN_ID)
                    {
                        case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                            {
                                bienDongApi.DIA_CHI = taiSanImport.TS_DAT.DIA_CHI;
                                bienDongApi.DIA_BAN_ID = taiSanImport.TS_DAT.DIA_BAN_ID;
                                bienDongApi.DAT_TONG_DIEN_TICH = taiSanImport.TS_DAT.DIEN_TICH;
                                bienDongApi.NGUYEN_GIA = item.NV_NGAN_SACH;
                                #region Hồ sơ giấy tờ                 
                                var chungNhanQuyenSuDungDat = !string.IsNullOrEmpty(item.DAT_CHUNGNHAN_QUYENSUDUNG) ? item.DAT_CHUNGNHAN_QUYENSUDUNG.Split(';') : new string[] { };
                                var quyetDinhGiaoDat = !string.IsNullOrEmpty(item.DAT_QUYETDINH_GIAODAT) ? item.DAT_QUYETDINH_GIAODAT.Split(';') : new string[] { };
                                var quyetDinhChoThueDat = !string.IsNullOrEmpty(item.DAT_QUYETDINH_CHOTHUE) ? item.DAT_QUYETDINH_CHOTHUE.Split(';') : new string[] { };
                                var hopDongChoThueDat = !string.IsNullOrEmpty(item.DAT_HOPDONG_THUE) ? item.DAT_HOPDONG_THUE.Split(';') : new string[] { };
                                HoSoGiayTo hoSoGiayTo = new HoSoGiayTo();
                                if (chungNhanQuyenSuDungDat.Length == 2)
                                {
                                    hoSoGiayTo.HS_CNQSD_SO = !string.IsNullOrEmpty(chungNhanQuyenSuDungDat[0]) ? chungNhanQuyenSuDungDat[0].Trim() : null;
                                    hoSoGiayTo.HS_CNQSD_NGAY = string.IsNullOrEmpty(chungNhanQuyenSuDungDat[1]) ? chungNhanQuyenSuDungDat[1].Trim() : null;
                                }
                                if (quyetDinhGiaoDat.Length == 2)
                                {
                                    hoSoGiayTo.HS_QUYET_DINH_GIAO_SO = !string.IsNullOrEmpty(quyetDinhGiaoDat[0]) ? quyetDinhGiaoDat[0].Trim() : null;
                                    hoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY = !string.IsNullOrEmpty(quyetDinhGiaoDat[1]) ? quyetDinhGiaoDat[1].Trim() : null;
                                }
                                if (quyetDinhChoThueDat.Length == 2)
                                {
                                    hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_SO = !string.IsNullOrEmpty(quyetDinhChoThueDat[0]) ? quyetDinhChoThueDat[0].Trim() : null;
                                    hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY = !string.IsNullOrEmpty(quyetDinhChoThueDat[1]) ? quyetDinhChoThueDat[1].Trim() : null;
                                }
                                if (hopDongChoThueDat.Length == 2)
                                {
                                    hoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO = !string.IsNullOrEmpty(hopDongChoThueDat[0]) ? hopDongChoThueDat[0].Trim() : null;
                                    hoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY = !string.IsNullOrEmpty(hopDongChoThueDat[1]) ? hopDongChoThueDat[1].Trim() : null;
                                }
                                hoSoGiayTo.HS_PHAP_LY_KHAC = item.DAT_GIAY_TO_KHAC;
                                bienDongApi = PrepareDataJson(bienDongApi, hoSoGiayTo, false);

                                #endregion
                                break;
                            }
                        case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                            {
                                bienDongApi.DIA_CHI = taiSanImport.TS_NHA.DIA_CHI;
                                bienDongApi.NHA_SO_TANG = taiSanImport.TS_NHA.NHA_SO_TANG;
                                bienDongApi.NHA_DIEN_TICH_XD = taiSanImport.TS_NHA.DIEN_TICH_XAY_DUNG;
                                bienDongApi.NHA_TONG_DIEN_TICH_XD = taiSanImport.TS_NHA.DIEN_TICH_SAN_XAY_DUNG;
                                bienDongApi.NHA_NAM_XAY_DUNG = taiSanImport.TS_NHA.NAM_XAY_DUNG;
                                break;
                            }
                    }
                    #region hiện trạng sử dụng
                    decimal GiaTriNumber = 0;
                    TaiSanHienTrangSuDung hienTrangSD = new TaiSanHienTrangSuDung();
                    List<TaiSanHienTrangSuDung> ListTSHienTrang = new List<TaiSanHienTrangSuDung>();
                    if (taiSanImport.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        // Diện tích làm việc
                        GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC != null ? item.HTSD_TRU_SO_LAM_VIEC.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.DienTichLamViec, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        hienTrangSD.BIEN_DONG_ID = taiSan.ID;
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - Không KD
                        GiaTriNumber = item.HTSD_HDSN_KINH_DOANH_KHONG != null ? item.HTSD_HDSN_KINH_DOANH_KHONG.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.KhongKinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);

                        ListTSHienTrang.Add(hienTrangSD);
                        //  SD hỗn hợp
                        GiaTriNumber = item.HTSD_SU_DUNG_HON_HOP != null ? item.HTSD_SU_DUNG_HON_HOP.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.SuDungHonHop, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - Cho thuê
                        GiaTriNumber = item.HTSD_HDSN_CHO_THUE != null ? item.HTSD_HDSN_CHO_THUE.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.ChoThue, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - KD
                        GiaTriNumber = item.HTSD_HDSN_KINH_DOANH != null ? item.HTSD_HDSN_KINH_DOANH.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.KinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  Liên doanh liên kết
                        GiaTriNumber = item.HTSD_HDSN_LIEN_DOANH != null ? item.HTSD_HDSN_LIEN_DOANH.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.LienDoanhLienKet, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  bỏ trống
                        GiaTriNumber = item.HTSD_BO_TRONG != null ? item.HTSD_BO_TRONG.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.BoTrong, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  bị lấn chiếm
                        GiaTriNumber = item.HTSD_BI_LAN_CHIEM != null ? item.HTSD_BI_LAN_CHIEM.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.BiLanChiem, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  để ở
                        GiaTriNumber = item.HTSD_DE_O != null ? item.HTSD_DE_O.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.DeO, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                    }
                    if (taiSanImport.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                    {
                        //Diện tích làm việc
                        GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC != null ? item.HTSD_TRU_SO_LAM_VIEC.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.DienTichLamViec, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - Không KD
                        GiaTriNumber = item.HTSD_HDSN_KINH_DOANH_KHONG != null ? item.HTSD_HDSN_KINH_DOANH_KHONG.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.KhongKinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  SD hỗn hợp
                        GiaTriNumber = item.HTSD_SU_DUNG_HON_HOP != null ? item.HTSD_SU_DUNG_HON_HOP.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.SuDungHonHop, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - Cho thuê
                        GiaTriNumber = item.HTSD_HDSN_CHO_THUE != null ? item.HTSD_HDSN_CHO_THUE.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.ChoThue, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        //  HĐSN - KD
                        GiaTriNumber = item.HTSD_HDSN_KINH_DOANH != null ? item.HTSD_HDSN_KINH_DOANH.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.KinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        // Liên doanh liên kết
                        GiaTriNumber = item.HTSD_HDSN_LIEN_DOANH != null ? item.HTSD_HDSN_LIEN_DOANH.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.LienDoanhLienKet, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        // bỏ trống
                        GiaTriNumber = item.HTSD_BO_TRONG != null ? item.HTSD_BO_TRONG.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.BoTrong, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        // bị lấn chiếm
                        GiaTriNumber = item.HTSD_BI_LAN_CHIEM != null ? item.HTSD_BI_LAN_CHIEM.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.BiLanChiem, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                        // để ở
                        GiaTriNumber = item.HTSD_DE_O != null ? item.HTSD_DE_O.Value : 0;
                        hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.DeO, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
                        ListTSHienTrang.Add(hienTrangSD);
                    }
                    foreach (var hienTrang in ListTSHienTrang)
                    {
                        TaiSanHienTrangSuDungApi hienTrangSuDungApi = new TaiSanHienTrangSuDungApi()
                        {
                            HIEN_TRANG_ID = hienTrang.HIEN_TRANG_ID,
                            GIA_TRI_CHECKBOX = hienTrang.GIA_TRI_CHECKBOX,
                            GIA_TRI_NUMBER = hienTrang.GIA_TRI_NUMBER,
                            TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(hienTrang.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG,
                            KIEU_DU_LIEU_ID = (loaiTaiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || loaiTaiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox
                        };
                        bienDongApi.HIEN_TRANG_BD.Add(hienTrangSuDungApi);
                    };
                    // hiện trạng sử dụng json
                    var ListHienTrangObj = bienDongApi.HIEN_TRANG_BD.Select(m =>
                      new ObjHienTrang_Entity()
                      {
                          HienTrangId = m.HIEN_TRANG_ID,
                          GiaTriText = m.GIA_TRI_TEXT,
                          GiaTriNumber = m.GIA_TRI_NUMBER,
                          GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
                          NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
                          TenHienTrang = m.TEN_HIEN_TRANG,
                          KieuDuLieuId = m.KIEU_DU_LIEU_ID
                      }).ToList();
                    var HienTrangList = new HienTrangList_Entity()
                    {
                        // TaiSanId = taiSan.ID,
                        lstObjHienTrang = ListHienTrangObj
                    };
                    bienDongApi.HTSD_JSON = HienTrangList.toStringJson();
                    #endregion
                    #region Nguồn vốn
                    bienDongApi.NGUON_VON_BD = new List<TaiSanNguonVonApi>();
                    if (item.NV_NGAN_SACH.HasValue)
                    {
                        TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                        {
                            GIA_TRI = item.NV_NGAN_SACH,
                            NGUON_VON_ID = (decimal)enumNguonVon.NguonNganSach,
                            TEN_NGUON_VON = "Nguồn ngân sách",

                        };
                        bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                    }

                    if (item.NV_NGUON_KHAC.HasValue)
                    {
                        TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
                        {
                            GIA_TRI = item.NV_NGUON_KHAC,
                            NGUON_VON_ID = (decimal)enumNguonVon.NguonKhac,
                            TEN_NGUON_VON = "Nguồn khác",
                        };
                        bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
                    }
                    #endregion

                    taiSanImport.LST_BIEN_DONG.Add(bienDongApi);
                    var taiSanJson = taiSanImport.toStringJson();
                    //  taiSan.
                    var result = _dBTaiSanService.insertOrupdateTaiSanByJson(taiSanImport.toStringJson(), null, true);
                    if (result.Code == MessageReturn.SUCCESS_CODE)
                    {
                        ListSuccess.Add(item);
                        // chốt khấu hao, hao mòn tài sản
                        _bienDongChiTietService.ChotHMKHTaiSan(taiSanId: taiSan.ID, bienDongId: _bienDongService.GetBienDongByGuid(bienDongApi.GUID).ID);
                    }
                    else
                    {
                        item.Error = result.Message;
                        ListError.Add(item);
                    }
                }
                catch (Exception ex)
                {

                    item.Error = ex.toStringJson();
                    ListError.Add(item);
                }
            }
            return ListError;
        }
        #region importexcel tài sản khác
        //public List<ImportExcelTaiSanKhacModel> ImportFileExcel(List<ImportExcelTaiSanKhacModel> TaiSanKhac = null)
        //{
        //    List<ImportExcelTaiSanDatNhaModel> ListSuccess = new List<ImportExcelTaiSanDatNhaModel>();
        //    List<ImportExcelTaiSanDatNhaModel> ListError = new List<ImportExcelTaiSanDatNhaModel>();
        //    var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
        //    bool IsDuyet = true;
        //    //if (donViCauHinh != null)
        //    //{
        //    //    IsDuyet = donViCauHinh.IsAutoDuyetTaiSanImport;
        //    //}

        //    foreach (var item in TaiSanKhac)
        //    {
        //        try
        //        {
        //            List<string> Errors = new List<string>();
        //            TaiSanDongBoApi taiSanImport = new TaiSanDongBoApi();
        //            //insert tài sản để cập nhật mã
        //            // Loại tài sản 
        //            #region validate
        //            string MaLoaiTaiSan = item.LOAI_TAI_SAN_MA.Split('-')[0].Trim();
        //            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByMa(MaLoaiTaiSan);
        //            if (loaiTaiSan == null)
        //            {
        //                Errors.Add("Loại tài sản không đúng");
        //            }


        //            // thông tin Nhãn xe
        //            decimal? NhanXe = null;
        //            if (!string.IsNullOrEmpty(item.NHAN_HIEU))
        //            {
        //                NhanXe nhanXe = _nhanXeService.GetNhanXeByMa(Ten:item.NHAN_HIEU.Trim());
        //                if (nhanXe == null)
        //                {
        //                    Errors.Add("Nhãn xe không tồn tại");
        //                }
        //                else
        //                {
        //                    NhanXe = nhanXe.ID;
        //                }
        //            }
        //            // thông tin loại xe
        //            decimal? LoaiXeId    = null;
        //            if (!string.IsNullOrEmpty(item.LOAI_XE))
        //            {
        //                DongXe dongXe = _dongXeService.GetDongXeByMa(Ten:item.LOAI_XE.Trim());
        //                if (dongXe == null)
        //                {
        //                    Errors.Add("Loại xe không tồn tại");
        //                }
        //                else
        //                {
        //                    LoaiXeId = dongXe.ID;
        //                }

        //            }
        //            // thông tin nước sản xuất
        //            decimal? QuocGiaId = null;
        //            if (!string.IsNullOrEmpty(item.NUOC_SAN_XUAT))
        //            {
        //                QuocGia quocGia = _quocGiaService.GetQuocGia(Ten:item.NUOC_SAN_XUAT.Trim());
        //                if (quocGia == null)
        //                {
        //                    Errors.Add("Nước sản xuất không tồn tại");
        //                }
        //                else
        //                {
        //                    QuocGiaId = quocGia.ID;
        //                }

        //            }
        //            item.LY_DO_BIEN_DONG_MA = item.LY_DO_BIEN_DONG_MA.Split('-')[0].Trim();
        //            if (string.IsNullOrEmpty(item.LY_DO_BIEN_DONG_MA))
        //            {
        //                Errors.Add("Mã lý do tăng  bắt buộc nhập");
        //            }
        //            LyDoBienDong lyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByMa(item.LY_DO_BIEN_DONG_MA);
        //            if (lyDoBienDong == null)
        //            {
        //                Errors.Add("Mã lý do tăng  không tồn tại");
        //            }
        //            if (string.IsNullOrEmpty(item.NGAY_SU_DUNG))
        //            {
        //                Errors.Add("Ngày sử dụng bắt buộc nhập");
        //            }
        //            else
        //            {
        //                DateTime? NgaySuDung = item.NGAY_SU_DUNG.toDateSys("dd/MM/yyyy");
        //                if (NgaySuDung == null)
        //                {
        //                    Errors.Add("Ngày sử dụng không đúng định dạng");
        //                }
        //            }
        //            if (string.IsNullOrEmpty(item.NGAY_BIEN_DONG))
        //            {
        //                Errors.Add("Ngày tăng không đúng");
        //            }
        //            else
        //            {
        //                DateTime? NgayBienDong = item.NGAY_BIEN_DONG.toDateSys("dd/MM/yyyy");
        //                if (NgayBienDong == null)
        //                {
        //                    Errors.Add("Ngày Biến động không đúng định dạng");
        //                }
        //            }
        //            decimal TongHienTrang = 0;
        //            var ListProp = item.GetType().GetProperties();
        //            foreach (var prop in ListProp)
        //            {
        //                if (prop.Name.Contains("HTSD"))
        //                {
        //                    TongHienTrang += ((decimal?)prop.GetValue(item)).GetValueOrDefault(0);
        //                }
        //            }
        //            switch (loaiTaiSan.LOAI_HINH_TAI_SAN_ID)
        //            {
        //                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
        //                    {
        //                        if (string.IsNullOrEmpty(item.BIEN_KIEM_SOAT))
        //                        {
        //                            Errors.Add("Biển kiểm soát không được để trống");
        //                        }
        //                        if(!(item.CHO_NGOI_TAI_TRONG>0))
        //                        { 
        //                            Errors.Add("Số chỗ ngồi/tải trọng không được để trống");
        //                        }

        //                        break;
        //                    }
        //                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
        //                    {


        //                        break;
        //                    }
        //                    // check các trường ngày

        //            }
        //            if (Errors.Count > 0)
        //            {
        //                item.Error = string.Join("<br />", Errors);
        //                ListError.Add(item);
        //                continue;
        //            }

        //            #endregion
        //            TaiSan taiSan = new TaiSan();
        //            _taiSanService.InsertTaiSan(taiSan);
        //            taiSan.MA = CommonHelper.GenMaTaiSan(_workContext.CurrentDonVi.MA_DON_VI, loaiTaiSan.MA, taiSan.ID);
        //            taiSan.TEN = item.TEN;
        //            _taiSanService.UpdateTaiSan(taiSan);
        //            //prepareduliệu
        //            taiSanImport.ID = taiSan.ID;
        //            taiSanImport.TEN_TAI_SAN = item.TEN;
        //            taiSanImport.MA_TAI_SAN = taiSan.MA;
        //            taiSanImport.MA_LOAI_TAI_SAN = loaiTaiSan.MA;
        //            taiSanImport.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
        //            taiSanImport.NGAY_NHAP = DateTime.Now;
        //            taiSanImport.NGAY_SU_DUNG = item.NGAY_SU_DUNG.toDateSys("dd/MM/yyyy"); ;
        //            taiSanImport.LOAI_HINH_TAI_SAN_ID = loaiTaiSan.LOAI_HINH_TAI_SAN_ID;
        //            //nguyên giá = tổng nguồn vốn;
        //            decimal NguyenGia = (item.NV_NGAN_SACH ?? 0) + (item.NV_NGUON_KHAC ?? 0);
        //            taiSanImport.NGUYEN_GIA_BAN_DAU = NguyenGia;
        //            taiSanImport.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
        //            taiSanImport.TRANG_THAI = 2;
        //            switch (taiSanImport.LOAI_HINH_TAI_SAN_ID)
        //            {

        //            }
        //            taiSanImport.LST_BIEN_DONG = new List<BienDongApi>();
        //            BienDongApi bienDongApi = new BienDongApi();
        //            bienDongApi.GUID = Guid.NewGuid();
        //            bienDongApi.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
        //            bienDongApi.TEN_TAI_SAN = item.TEN;
        //            bienDongApi.NGUYEN_GIA = NguyenGia;
        //            bienDongApi.NGAY_BIEN_DONG = item.NGAY_BIEN_DONG.toDateSys("dd/MM/yyyy");
        //            bienDongApi.NGAY_SU_DUNG = taiSanImport.NGAY_SU_DUNG;
        //            bienDongApi.LOAI_BIEN_DONG_ID = (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
        //            bienDongApi.LY_DO_BIEN_DONG_ID = lyDoBienDong.ID;
        //            bienDongApi.LY_DO_BIEN_DONG_MA = lyDoBienDong.MA;
        //            bienDongApi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
        //            bienDongApi.NGAY_TAO = DateTime.Now;
        //            bienDongApi.DON_VI_MA = taiSanImport.MA_DON_VI;
        //            bienDongApi.GIA_TRI_CON_LAI = item.GIA_TRI_CON_LAI;
        //            bienDongApi.DIA_BAN_ID = DiaBanId;
        //            switch (taiSanImport.LOAI_HINH_TAI_SAN_ID)
        //            {
        //                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
        //                    {
        //                        bienDongApi.DIA_CHI = taiSanImport.TS_DAT.DIA_CHI;
        //                        bienDongApi.DIA_BAN_ID = taiSanImport.TS_DAT.DIA_BAN_ID;
        //                        bienDongApi.DAT_TONG_DIEN_TICH = taiSanImport.TS_DAT.DIEN_TICH;
        //                        bienDongApi.NGUYEN_GIA = item.NV_NGAN_SACH;
        //                        #region Hồ sơ giấy tờ                 
        //                        var chungNhanQuyenSuDungDat = !string.IsNullOrEmpty(item.DAT_CHUNGNHAN_QUYENSUDUNG) ? item.DAT_CHUNGNHAN_QUYENSUDUNG.Split(';') : new string[] { };
        //                        var quyetDinhGiaoDat = !string.IsNullOrEmpty(item.DAT_QUYETDINH_GIAODAT) ? item.DAT_QUYETDINH_GIAODAT.Split(';') : new string[] { };
        //                        var quyetDinhChoThueDat = !string.IsNullOrEmpty(item.DAT_QUYETDINH_CHOTHUE) ? item.DAT_QUYETDINH_CHOTHUE.Split(';') : new string[] { };
        //                        var hopDongChoThueDat = !string.IsNullOrEmpty(item.DAT_HOPDONG_THUE) ? item.DAT_HOPDONG_THUE.Split(';') : new string[] { };
        //                        HoSoGiayTo hoSoGiayTo = new HoSoGiayTo();
        //                        if (chungNhanQuyenSuDungDat.Length == 2)
        //                        {
        //                            hoSoGiayTo.HS_CNQSD_SO = !string.IsNullOrEmpty(chungNhanQuyenSuDungDat[0]) ? chungNhanQuyenSuDungDat[0].Trim() : null;
        //                            hoSoGiayTo.HS_CNQSD_NGAY = ConvertStringDateExcel(!string.IsNullOrEmpty(chungNhanQuyenSuDungDat[1]) ? chungNhanQuyenSuDungDat[1].Trim() : null);
        //                        }
        //                        if (quyetDinhGiaoDat.Length == 2)
        //                        {
        //                            hoSoGiayTo.HS_QUYET_DINH_GIAO_SO = !string.IsNullOrEmpty(quyetDinhGiaoDat[0]) ? quyetDinhGiaoDat[0].Trim() : null;
        //                            hoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY = !string.IsNullOrEmpty(quyetDinhGiaoDat[1]) ? quyetDinhGiaoDat[1].Trim() : null;
        //                        }
        //                        if (quyetDinhChoThueDat.Length == 2)
        //                        {
        //                            hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_SO = !string.IsNullOrEmpty(quyetDinhChoThueDat[0]) ? quyetDinhChoThueDat[0].Trim() : null;
        //                            hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY = !string.IsNullOrEmpty(quyetDinhChoThueDat[1]) ? quyetDinhChoThueDat[1].Trim() : null;
        //                        }
        //                        if (hopDongChoThueDat.Length == 2)
        //                        {
        //                            hoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO = !string.IsNullOrEmpty(hopDongChoThueDat[0]) ? hopDongChoThueDat[0].Trim() : null;
        //                            hoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY = !string.IsNullOrEmpty(hopDongChoThueDat[1]) ? hopDongChoThueDat[1].Trim() : null;
        //                        }
        //                        hoSoGiayTo.HS_PHAP_LY_KHAC = item.DAT_GIAY_TO_KHAC;
        //                        bienDongApi = PrepareHoSoGiayTo(bienDongApi, hoSoGiayTo, false);
        //                        bienDongApi.DATA_JSON = new
        //                        {
        //                            HS_CNQSD_SO = bienDongApi.HS_CNQSD_SO,
        //                            HS_CNQSD_NGAY = bienDongApi.HS_CNQSD_SO == null ? null : bienDongApi.HS_CNQSD_NGAY,
        //                            HS_QUYET_DINH_GIAO_SO = bienDongApi.HS_QUYET_DINH_GIAO_SO,
        //                            HS_QUYET_DINH_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_GIAO_SO == null ? null : bienDongApi.HS_QUYET_DINH_GIAO_NGAY,
        //                            HS_CHUYEN_NHUONG_SD_SO = bienDongApi.HS_CHUYEN_NHUONG_SD_SO,
        //                            HS_CHUYEN_NHUONG_SD_NGAY = bienDongApi.HS_CHUYEN_NHUONG_SD_SO == null ? null : bienDongApi.HS_CHUYEN_NHUONG_SD_NGAY,
        //                            HS_QUYET_DINH_CHO_THUE_SO = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO,
        //                            HS_QUYET_DINH_CHO_THUE_NGAY = bienDongApi.HS_QUYET_DINH_CHO_THUE_SO == null ? null : bienDongApi.HS_QUYET_DINH_CHO_THUE_NGAY,
        //                            HS_KHAC = bienDongApi.HS_KHAC,
        //                            HS_QUYET_DINH_BAN_GIAO = bienDongApi.HS_QUYET_DINH_BAN_GIAO,
        //                            HS_QUYET_DINH_BAN_GIAO_NGAY = bienDongApi.HS_QUYET_DINH_BAN_GIAO == null ? null : bienDongApi.HS_QUYET_DINH_BAN_GIAO_NGAY,
        //                            HS_HOP_DONG_CHO_THUE_SO = bienDongApi.HS_HOP_DONG_CHO_THUE_SO,
        //                            HS_HOP_DONG_CHO_THUE_NGAY = bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY == null ? null : bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY,
        //                            HS_PHAP_LY_KHAC = bienDongApi.HS_PHAP_LY_KHAC,
        //                            HS_PHAP_LY_KHAC_NGAY = bienDongApi.HS_PHAP_LY_KHAC == null ? null : bienDongApi.HS_PHAP_LY_KHAC_NGAY,
        //                            DAT_GIA_TRI_QUYEN_SD_DAT = item.DAT_GIA_TRI_QUYEN_SD_DAT
        //                        }.toStringJson();
        //                        #endregion
        //                        break;
        //                    }
        //                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
        //                    {
        //                        bienDongApi.DIA_CHI = taiSanImport.TS_NHA.DIA_CHI;
        //                        bienDongApi.NHA_SO_TANG = taiSanImport.TS_NHA.NHA_SO_TANG;
        //                        bienDongApi.NHA_DIEN_TICH_XD = taiSanImport.TS_NHA.DIEN_TICH_XAY_DUNG;
        //                        bienDongApi.NHA_TONG_DIEN_TICH_XD = taiSanImport.TS_NHA.DIEN_TICH_SAN_XAY_DUNG;
        //                        bienDongApi.NHA_NAM_XAY_DUNG = taiSanImport.TS_NHA.NAM_XAY_DUNG;
        //                        break;
        //                    }
        //            }
        //            #region hiện trạng sử dụng
        //            decimal GiaTriNumber = 0;
        //            TaiSanHienTrangSuDung hienTrangSD = new TaiSanHienTrangSuDung();
        //            List<TaiSanHienTrangSuDung> ListTSHienTrang = new List<TaiSanHienTrangSuDung>();
        //            if (taiSanImport.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
        //            {
        //                // Diện tích làm việc
        //                GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC != null ? item.HTSD_TRU_SO_LAM_VIEC.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.DienTichLamViec, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                hienTrangSD.BIEN_DONG_ID = taiSan.ID;
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - Không KD
        //                GiaTriNumber = item.HTSD_HDSN_KINH_DOANH_KHONG != null ? item.HTSD_HDSN_KINH_DOANH_KHONG.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.KhongKinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);

        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  SD hỗn hợp
        //                GiaTriNumber = item.HTSD_SU_DUNG_HON_HOP != null ? item.HTSD_SU_DUNG_HON_HOP.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.SuDungHonHop, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - Cho thuê
        //                GiaTriNumber = item.HTSD_HDSN_CHO_THUE != null ? item.HTSD_HDSN_CHO_THUE.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.ChoThue, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - KD
        //                GiaTriNumber = item.HTSD_HDSN_KINH_DOANH != null ? item.HTSD_HDSN_KINH_DOANH.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.KinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  Liên doanh liên kết
        //                GiaTriNumber = item.HTSD_HDSN_LIEN_DOANH != null ? item.HTSD_HDSN_LIEN_DOANH.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung(79, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  bỏ trống
        //                GiaTriNumber = item.HTSD_BO_TRONG != null ? item.HTSD_BO_TRONG.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung(79, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  bị lấn chiếm
        //                GiaTriNumber = item.HTSD_BI_LAN_CHIEM != null ? item.HTSD_BI_LAN_CHIEM.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.BiLanChiem, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  để ở
        //                GiaTriNumber = item.HTSD_DE_O != null ? item.HTSD_DE_O.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungDat.DeO, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //            }
        //            if (taiSanImport.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
        //            {
        //                //Diện tích làm việc
        //                GiaTriNumber = item.HTSD_TRU_SO_LAM_VIEC != null ? item.HTSD_TRU_SO_LAM_VIEC.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.DienTichLamViec, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - Không KD
        //                GiaTriNumber = item.HTSD_HDSN_KINH_DOANH_KHONG != null ? item.HTSD_HDSN_KINH_DOANH_KHONG.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.KhongKinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  SD hỗn hợp
        //                GiaTriNumber = item.HTSD_SU_DUNG_HON_HOP != null ? item.HTSD_SU_DUNG_HON_HOP.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.SuDungHonHop, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - Cho thuê
        //                GiaTriNumber = item.HTSD_HDSN_CHO_THUE != null ? item.HTSD_HDSN_CHO_THUE.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.ChoThue, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                //  HĐSN - KD
        //                GiaTriNumber = item.HTSD_HDSN_KINH_DOANH != null ? item.HTSD_HDSN_KINH_DOANH.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.KinhDoanh, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                // Liên doanh liên kết
        //                GiaTriNumber = item.HTSD_HDSN_LIEN_DOANH != null ? item.HTSD_HDSN_LIEN_DOANH.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.LienDoanhLienKet, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                // bỏ trống
        //                GiaTriNumber = item.HTSD_BO_TRONG != null ? item.HTSD_BO_TRONG.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.BoTrong, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                // bị lấn chiếm
        //                GiaTriNumber = item.HTSD_BI_LAN_CHIEM != null ? item.HTSD_BI_LAN_CHIEM.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.BiLanChiem, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //                // để ở
        //                GiaTriNumber = item.HTSD_DE_O != null ? item.HTSD_DE_O.Value : 0;
        //                hienTrangSD = PrepareTaiSanHienTrangSuDung((decimal)enumHienTrangSuDungNha.DeO, null, GiaTriNumber: GiaTriNumber, TaiSanID: taiSan.ID);
        //                ListTSHienTrang.Add(hienTrangSD);
        //            }
        //            foreach (var hienTrang in ListTSHienTrang)
        //            {
        //                TaiSanHienTrangSuDungApi hienTrangSuDungApi = new TaiSanHienTrangSuDungApi()
        //                {
        //                    HIEN_TRANG_ID = hienTrang.HIEN_TRANG_ID,
        //                    GIA_TRI_CHECKBOX = hienTrang.GIA_TRI_CHECKBOX,
        //                    GIA_TRI_NUMBER = hienTrang.GIA_TRI_NUMBER,
        //                    TEN_HIEN_TRANG = _hienTrangService.GetHienTrangById(hienTrang.HIEN_TRANG_ID.Value).TEN_HIEN_TRANG,
        //                    KIEU_DU_LIEU_ID = (loaiTaiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT || loaiTaiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? (decimal)enumKIEU_DU_LIEU.Numberic : (decimal)enumKIEU_DU_LIEU.CheckBox
        //                };
        //                bienDongApi.HIEN_TRANG_BD.Add(hienTrangSuDungApi);
        //            };
        //            // hiện trạng sử dụng json
        //            var ListHienTrangObj = bienDongApi.HIEN_TRANG_BD.Select(m =>
        //              new ObjHienTrang_Entity()
        //              {
        //                  HienTrangId = m.HIEN_TRANG_ID,
        //                  GiaTriText = m.GIA_TRI_TEXT,
        //                  GiaTriNumber = m.GIA_TRI_NUMBER,
        //                  GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
        //                  NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
        //                  TenHienTrang = m.TEN_HIEN_TRANG,
        //                  KieuDuLieuId = m.KIEU_DU_LIEU_ID
        //              }).ToList();
        //            var HienTrangList = new HienTrangList_Entity()
        //            {
        //                // TaiSanId = taiSan.ID,
        //                lstObjHienTrang = ListHienTrangObj
        //            };
        //            bienDongApi.HTSD_JSON = HienTrangList.toStringJson();
        //            #endregion
        //            #region Nguồn vốn
        //            bienDongApi.NGUON_VON_BD = new List<TaiSanNguonVonApi>();
        //            if (item.NV_NGAN_SACH > 0)

        //                if (item.NV_NGAN_SACH > 0)
        //                {
        //                    TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
        //                    {
        //                        GIA_TRI = item.NV_NGAN_SACH,
        //                        NGUON_VON_ID = (decimal)enumNguonVon.NguonNganSach,
        //                        TEN_NGUON_VON = "Nguồn ngân sách",

        //                    };
        //                    bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
        //                }
        //            if (item.NV_NGUON_KHAC > 0)
        //            {
        //                TaiSanNguonVonApi nguonVonApi = new TaiSanNguonVonApi()
        //                {
        //                    GIA_TRI = item.NV_NGUON_KHAC,
        //                    NGUON_VON_ID = (decimal)enumNguonVon.NguonKhac,
        //                    TEN_NGUON_VON = "Nguồn khác",
        //                };
        //                bienDongApi.NGUON_VON_BD.Add(nguonVonApi);
        //            }
        //            #endregion

        //            taiSanImport.LST_BIEN_DONG.Add(bienDongApi);
        //            var taiSanJson = taiSanImport.toStringJson();
        //            //  taiSan.
        //            var result = _dBTaiSanService.insertOrupdateTaiSanByJson(taiSanImport.toStringJson(), null, true);
        //            if (result.Code == MessageReturn.SUCCESS_CODE)
        //            {
        //                ListSuccess.Add(item);
        //                // chốt khấu hao, hao mòn tài sản
        //                _bienDongChiTietService.ChotHMKHTaiSan(taiSanId: taiSan.ID, bienDongId: _bienDongService.GetBienDongByGuid(bienDongApi.GUID).ID);
        //            }
        //            else
        //            {
        //                item.Error = result.Message;
        //                ListError.Add(item);
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            item.Error = ex.toStringJson();
        //            ListError.Add(item);
        //        }
        //    }
        //    return ListError;
        //}
        #endregion

        TaiSanHienTrangSuDung PrepareTaiSanHienTrangSuDung(decimal HienTrangId, bool? GiaTriCheckbox, decimal? GiaTriNumber, decimal TaiSanID = 0)
        {
            TaiSanHienTrangSuDung item = new TaiSanHienTrangSuDung();
            item.HIEN_TRANG_ID = HienTrangId;
            if (GiaTriCheckbox != null)
            {
                item.GIA_TRI_CHECKBOX = GiaTriCheckbox.Value;
            }
            if (GiaTriNumber != null)
            {
                item.GIA_TRI_NUMBER = GiaTriNumber;
            }
            item.TAI_SAN_ID = TaiSanID;
            return item;
        }
        BienDongApi PrepareDataJson(BienDongApi bienDongApi, HoSoGiayTo hoSoGiayTo, bool usingFormat = true, decimal? GiaTriQuyenSuDungDat = null)
        {
            #region Hồ sơ giấy tờ
            bienDongApi.HS_HOP_DONG_CHO_THUE_SO = hoSoGiayTo.HS_HOP_DONG_CHO_THUE_SO;
            bienDongApi.HS_HOP_DONG_CHO_THUE_NGAY = usingFormat ? hoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_HOP_DONG_CHO_THUE_NGAY.toDateSys();
            bienDongApi.HS_CNQSD_SO = hoSoGiayTo.HS_CNQSD_SO;
            bienDongApi.HS_CNQSD_NGAY = usingFormat ? hoSoGiayTo.HS_CNQSD_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_CNQSD_NGAY.toDateSys();
            bienDongApi.HS_QUYET_DINH_GIAO_SO = hoSoGiayTo.HS_QUYET_DINH_GIAO_SO;
            bienDongApi.HS_QUYET_DINH_GIAO_NGAY = usingFormat ? hoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_QUYET_DINH_GIAO_NGAY.toDateSys();
            bienDongApi.HS_QUYET_DINH_CHO_THUE_SO = hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_SO;
            bienDongApi.HS_QUYET_DINH_CHO_THUE_NGAY = usingFormat ? hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_QUYET_DINH_CHO_THUE_NGAY.toDateSys();
            bienDongApi.HS_PHAP_LY_KHAC = hoSoGiayTo.HS_PHAP_LY_KHAC;
            bienDongApi.HS_PHAP_LY_KHAC_NGAY = usingFormat ? hoSoGiayTo.HS_PHAP_LY_KHAC_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_PHAP_LY_KHAC_NGAY.toDateSys();
            bienDongApi.HS_QUYET_DINH_BAN_GIAO = hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO;
            bienDongApi.HS_QUYET_DINH_BAN_GIAO_NGAY = hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO == null ? null : usingFormat ? hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_QUYET_DINH_BAN_GIAO_NGAY.toDateSys();
            bienDongApi.HS_BIEN_BAN_NGHIEM_THU = hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU;
            bienDongApi.HS_BIEN_BAN_NGHIEM_THU_NGAY = hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU == null ? null : usingFormat ? hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_BIEN_BAN_NGHIEM_THU_NGAY.toDateSys();
            bienDongApi.HS_CHUYEN_NHUONG_SD_SO = hoSoGiayTo.HS_CHUYEN_NHUONG_SD_SO;
            bienDongApi.HS_CHUYEN_NHUONG_SD_NGAY = hoSoGiayTo.HS_CHUYEN_NHUONG_SD_NGAY == null ? null : usingFormat ? hoSoGiayTo.HS_CHUYEN_NHUONG_SD_NGAY.toDateSys("yyyy-MM-dd HH:mm:ss") : hoSoGiayTo.HS_CHUYEN_NHUONG_SD_NGAY.toDateSys();

            #endregion
            bienDongApi.DATA_JSON = bienDongApi.toStringJson();
            return bienDongApi;
        }
        #endregion

        #region Tài sản toàn dân
        public async Task<ResponseApi> DongBoQuyetDinhTichThuTSTD()
        {
            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListQuyetDinhTichThu = null
            };
            //var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateQuyetDinhTichThu", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateQuyetDinhTichThu", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public async Task<ResponseApi> DongBoTaiSanTSTD()
        {
            var listTSTD = _taiSanTdService.GetAllTaiSanTdsChuaDongBo();
            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanToanDan = listTSTD
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateTaiSanTd", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public async Task<ResponseApi> DongBoPAXL()
        {

            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanToanDan = null
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateXuLy", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public async Task<ResponseApi> DongBoTSXL()
        {

            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanToanDan = null
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateTaiSanXuLy", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public async Task<ResponseApi> DongBoKetQua()
        {

            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanToanDan = null
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateKetQua", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        public async Task<ResponseApi> DongBoThuChi()
        {

            PrameterDongBoTaiSanModel param = new PrameterDongBoTaiSanModel()
            {
                ListTaiSanToanDan = null
            };
            var response = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingTaiSanXacLap/UpdateThuChi", param, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return response;
        }
        #endregion

        public MessageReturn PrepareReport(decimal? QueueProcessID)
        {
            QueueProcess queueProcess = _queueProcessService.GetQueueProcessById(QueueProcessID.Value);
            if (queueProcess == null)
                return MessageReturn.CreateErrorMessage("QueueProcess not found");
            if (string.IsNullOrEmpty(queueProcess.DATA_JSON))
                return MessageReturn.CreateErrorMessage("DATA_JSON is null");
            try
            {
                BaoCaoTaiSanDongBoSearch modelSearch = new BaoCaoTaiSanDongBoSearch();
                PropertyRenameAndIgnoreSerializerContractResolver jsonResolverReportData = new PropertyRenameAndIgnoreSerializerContractResolver();
                PropertyRenameAndIgnoreSerializerContractResolver jsonResolverBodyData = new PropertyRenameAndIgnoreSerializerContractResolver();
                Type _typeBodyData = typeof(BaoCaoTaiSanDongBoSearch);
                var serializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.None,
                    DateFormatString = "dd-MM-yyyy",
                    NullValueHandling = NullValueHandling.Ignore
                };


                //string maBaoCao = queueProcess.MA_BAO_CAO;
                //String jsonData = queueProcess.DATA_JSON;

                string jsonReportData = "";

                var cauHinh = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
                var cauHinhBaoCaos = cauHinh.BaoCao.toEntities<CauHinhMapBaoCao>();
                CauHinhMapBaoCao cauHinhMapBaoCao = cauHinhBaoCaos.Where(c => c.MaBaoCao == queueProcess.MA_BAO_CAO).FirstOrDefault();
                #region get Type Model báo cáo
                Type _typeReportData = Type.GetType(cauHinhMapBaoCao.Model + ", GS.Core");
                Type _typeReportDatas = Type.GetType($"System.Collections.Generic.List`1[[{cauHinhMapBaoCao.Model}, GS.Core]]");
                #endregion get Type Model báo cáo

                modelSearch = queueProcess.SEARCH_MODEL_JSON.toEntity<BaoCaoTaiSanDongBoSearch>();
                if (modelSearch.LoaiHinhTaiSan.HasValue)
                    modelSearch.LoaiHinhTaiSan = CommonHelper.toLoaiHinhTaiSanKho(modelSearch.LoaiHinhTaiSan.Value);
                modelSearch.MaBaoCao = cauHinhMapBaoCao.MaBaoCaoKho;
                if (modelSearch.DonVi > 0)
                {
                    DonVi donvi = _donViService.GetDonViById(modelSearch.DonVi);
                    if (donvi != null)
                    {
                        modelSearch.DonVi = donvi.DB_ID ?? modelSearch.DonVi;
                    }
                }
                modelSearch.FileGuid = queueProcess.GUID;

                #region Body
                if (cauHinhMapBaoCao.Body != null)
                {
                    Dictionary<string, string> pros = JsonConvert.DeserializeObject<Dictionary<string, string>>(cauHinhMapBaoCao.Body.toStringJson());
                    List<string> Listprosetting = pros.Select(c => c.Key).ToList();
                    List<string> ListPro = _typeBodyData.GetProperties().Select(c => c.Name).ToList();
                    string[] ignores = ListPro.Where(c => !Listprosetting.Contains(c)).ToArray();
                    if (ignores.Count() > 0)
                        jsonResolverBodyData.IgnoreProperty(_typeBodyData, ignores);//ignore những trường thông tin không cần thiết
                    foreach (var pro in pros)//rename theo format của kho
                    {
                        jsonResolverBodyData.RenameProperty(_typeBodyData, pro.Key, pro.Value);
                    }
                    serializerSettings.ContractResolver = jsonResolverBodyData;
                }
                string jsonBodyData = JsonConvert.SerializeObject(modelSearch, serializerSettings);
                #endregion Body

                #region Report Data
                Dictionary<string, string> reportDataSetting = JsonConvert.DeserializeObject<Dictionary<string, string>>(cauHinhMapBaoCao.reportData.toStringJson());
                List<string> keysReportDataSetting = reportDataSetting.Select(c => c.Key).ToList();
                List<string> ListProTypeReportDataSetting = _typeReportData.GetProperties().Select(c => c.Name).ToList();
                string[] ignoresProTypeReportDataSetting = ListProTypeReportDataSetting.Where(c => !keysReportDataSetting.Contains(c)).ToArray();
                if (ignoresProTypeReportDataSetting.Count() > 0)//ignore những trường thông tin không cần thiết
                    jsonResolverReportData.IgnoreProperty(_typeReportData, ignoresProTypeReportDataSetting);
                foreach (var item in reportDataSetting)//rename theo format của kho
                {
                    jsonResolverReportData.RenameProperty(_typeReportData, item.Key, item.Value);
                }
                serializerSettings.ContractResolver = jsonResolverReportData;
                //serializerSettings.
                var reportData = JsonConvert.DeserializeObject(queueProcess.DATA_JSON, _typeReportDatas);
                jsonReportData = JsonConvert.SerializeObject(reportData, serializerSettings);

                #endregion Report Data

                List<JObject> jsonObjectReportData = JsonConvert.DeserializeObject<List<JObject>>(jsonReportData);
                JObject jsonObjectReport = JsonConvert.DeserializeObject<JObject>(jsonBodyData);
                jsonObjectReport["reportData"] = JToken.FromObject(jsonObjectReportData);

                DB_BAOCAO data = new DB_BAOCAO() { MA_BAO_CAO = queueProcess.MA_BAO_CAO, MA_BAO_CAO_KHO = cauHinhMapBaoCao.MaBaoCaoKho, DATA_JSON = jsonObjectReport.ToString(Formatting.None).Replace(".0,", ",").Replace(".0}", "}") };
                var queueID = _dB_QueueProcessModelFactory.InsertActionToQueueReturnId(action: enumSendRequest.DongBoBaoCao, obj: data, DonViId: queueProcess.DON_VI_ID, level: (int)enumLevelQueueProcessDB.HIGHEST, DoiTuongId: QueueProcessID);
                queueProcess.RESPONSE = null;
                queueProcess.DB_QUEUE_PROCESS_ID = queueID;
                _queueProcessService.UpdateQueueProcess(queueProcess);
                //return MessageReturn.CreateSuccessMessage("Done", jsonObjectReport);
                return MessageReturn.CreateSuccessMessage("Done", queueID);
            }
            catch (Exception ex)
            {
                queueProcess.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_LOI;
                _queueProcessService.UpdateQueueProcess(queueProcess);
                LogQueueProcess log = new LogQueueProcess()
                {
                    ACTION = "Prepare Báo cáo",
                    //STATUS="ERROR",
                    QUEUE_ID = QueueProcessID,
                    OUTPUT = ex.Message
                };
                _logQueueProcessService.InsertLogQueueProcess(log);
                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }

        public async Task<ResponseApi> UpdateDanhMuc(string nameDanhMuc, bool isThemMoi = false)
        {
            ResponseApi rs = new ResponseApi();
            if (nameDanhMuc == "NguoiDung")
            {

                var lst = _nguoiDungService.GetAllNguoiDungs();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateNguoiDung", lst.Select(c => c.ToModel<NguoiDungModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else
            if (nameDanhMuc == "DonVi")
            {

                var lst = _donViService.GetAllDonVis();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).OrderBy(c => c.TREE_LEVEL).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).OrderBy(c => c.TREE_LEVEL).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateDonVi", lst.Select(c => c.ToModel<DonViModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "DiaBan")
            {

                var lst = _diaBanService.GetDiaBans();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).OrderBy(c => c.TREE_LEVEL).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).OrderBy(c => c.TREE_LEVEL).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateDiaBan", lst.Select(c => c.ToModel<DiaBanModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "HienTrang")
            {

                var lst = _hienTrangService.GetHienTrangs();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateHienTrang", lst.Select(c => c.ToModel<HienTrangModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "LoaiDonVi")
            {
                var lst = _loaiDonViService.GetAllLoaiDonVis();
                if (isThemMoi)
                    lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateLoaiDonVi", lst.Select(c => c.ToModel<LoaiDonViModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "LyDoBienDong")
            {
                var lst = _lyDoBienDongService.GetAllLyDoBienDongs();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateLyDoBienDong", lst.Select(c => c.ToModel<LyDoBienDongModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "ChucDanh")
            {
                var lst = _chucDanhService.GetAllChucDanhs();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateChucDanh", lst.Select(c => c.ToModel<ChucDanhModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "LoaiTaiSan")
            {
                var lst = _loaiTaiSanService.GetAllLoaiTaiSans();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateLoaiTaiSan", lst.Select(c => c.ToModel<LoaiTaiSanModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "NhanXe")
            {
                var lst = _nhanXeService.GetAllNhanXes();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateNhanXe", lst.Select(c => c.ToModel<NhanXeModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            else if (nameDanhMuc == "MucDichSuDung")
            {
                var lst = _mucDichSuDungService.GetMucDichSuDungs();
                if (isThemMoi)
                    lst = lst.Where(c => c.DB_ID == null).ToList();
                else
                    lst = lst.Where(c => c.DB_ID != null).ToList();
                rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateMucDichSuDung", lst.Select(c => c.ToModel<MucDichSuDungModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            }
            //else if (nameDanhMuc == "PhuongAnXuLy")
            //{
            //    var lst = _phuongAnXuLyService.GetAllPhuongAnXuLys();
            //    if (isThemMoi)
            //        lst.Where(c => c.DB_ID == null).ToList();
            //    else
            //        lst.Where(c => c.DB_ID != null).ToList();
            //    rs = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/UpdateMucDichSuDung", lst.Select(c => c.ToModel<MucDichSuDungModel>()).ToList(), _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            //}
            return rs;
        }
        #endregion
        #region
        public async Task<ResponseApi> GetNguoiDungByKhoCSDL(string name)//<ResponseApi>("ConsumingDanhMuc/GetNguoiDungByName");
        {
            var detail = new
            {
                name = name
            };
            //var respond = await _gSAPIService.GetObjectGSApi<ResponseApi>("ConsumingDanhMuc/GetNguoiDungByName", detail, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            ResponseApi respond = new ResponseApi();
            var token = _gSAPIService.GetTokenKhoCSDLQG();
            if (token == "1")
            {
                respond = new ResponseApi()
                {
                    Status = false,
                    Message = "Lỗi truy xuất dữ liệu bên kho",
                    Date = DateTime.Now,
                    Data = 1
                };
                return respond;
            }
            var responseApi = await _gSAPIService.GetObjectGSApiKho<ResponseApi>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
            if (responseApi != null)
            {
                respond = new ResponseApi()
                {
                    Status = true,
                    Message = "Tên đã tồn tại",
                    Date = DateTime.Now,
                    Data = respond
                };
            }
            else
            {
                respond = new ResponseApi()
                {
                    Status = false,
                    Message = "Ok",
                    Date = DateTime.Now,
                    Data = null
                };
            }
            return respond;
        }
        public async Task<ResponseApi> UpdateNguoiDungs(List<NguoiDungModel> model)
        {
            ResponseApi response = new ResponseApi();

            string Name = typeof(NguoiDungModel).Name;
            if (model == null)
            {
                response = new ResponseApi()
                {
                    Status = false,
                    Message = "Dữ liệu không đúng",
                    Date = DateTime.Now,
                    Data = null
                };
                return response;
            }
            List<Kho_NguoiDung> kho_NguoiDungs = new List<Kho_NguoiDung>();
            foreach (var m in model)
            {
                try
                {
                    NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(m.TEN_DANG_NHAP);
                    Kho_NguoiDung kho_NguoiDung = new Kho_NguoiDung();
                    var detail = new
                    {
                        username = nguoiDung.TEN_DANG_NHAP
                    };
                    var token = _gSAPIService.GetTokenKhoCSDLQG();
                    if (token == "1")
                    {
                        response = new ResponseApi()
                        {
                            Status = false,
                            Message = "Lỗi truy xuất dữ liệu bên kho",
                            Date = DateTime.Now,
                            Data = 1
                        };
                        return response;
                    }
                    Kho_NguoiDung Check_kho_NguoiDung = await _gSAPIService.GetObjectGSApiKho<Kho_NguoiDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
                    if (Check_kho_NguoiDung != null)
                    {
                        kho_NguoiDung.id = Check_kho_NguoiDung.id;
                    }
                    kho_NguoiDung.actionType = nguoiDung.DB_ID == "0" ? 1 : 2;
                    kho_NguoiDung.username = m.TEN_DANG_NHAP;
                    kho_NguoiDung.email = m.EMAIL;
                    kho_NguoiDung.phoneNumber = m.MOBILE;
                    kho_NguoiDung.passwordHash = nguoiDung.PASSWORD_HASH;
                    if (m.TMP_MAT_KHAU != null)
                    {
                        kho_NguoiDung.password = m.TMP_MAT_KHAU;
                    }
                    kho_NguoiDung.fullName = m.TEN_DAY_DU;
                    kho_NguoiDung.lockoutEnabled = m.TRANG_THAI_ID == 1 ? false : true;
                    kho_NguoiDung.isAdministrator = m.IS_QUAN_TRI;
                    List<NguoiDungDonViMapping> nguoiDungDonVis = _nguoiDungDonViService.GetMapByNguoiDungId(nguoiDung.ID).OrderBy(c => c.donvi.TREE_LEVEL).ToList();
                    kho_NguoiDung.unitId = nguoiDungDonVis.Count > 0 ? nguoiDungDonVis.FirstOrDefault().donvi.DB_ID : 1;
                    kho_NguoiDungs.Add(kho_NguoiDung);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            if (kho_NguoiDungs.Count() > 0)
            {
                response = await PostDanhMuc(CommonDanhMuc.NguoiDung, kho_NguoiDungs, "NguoiDung");
                if (response.Status)
                {
                    foreach (var item in model)
                    {

                        var detail = new
                        {
                            username = item.TEN_DANG_NHAP
                        };
                        var token = _gSAPIService.GetTokenKhoCSDLQG();
                        if (token == "1")
                        {
                            response = new ResponseApi()
                            {
                                Status = false,
                                Message = "Lỗi truy xuất dữ liệu bên kho",
                                Date = DateTime.Now,
                                Data = 1
                            };
                            return response;
                        }
                        Kho_NguoiDung kho_NguoiDung = await _gSAPIService.GetObjectGSApiKho<Kho_NguoiDung>(CommonDanhMuc.RequestDanhMuc + CommonDanhMuc.NguoiDung + CommonAction.ChiTiet, detail, token);
                        if (kho_NguoiDung != null)
                        {
                            NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByUsername(item.TEN_DANG_NHAP);
                            nguoiDung.TRANG_THAI_ID = (int)GS.Core.Domain.HeThong.NguoiDungStatusID.ok;
                            nguoiDung.DB_ID = kho_NguoiDung.id.ToString();
                            nguoiDung.TMP_MAT_KHAU = null;
                            _nguoiDungService.UpdateNguoiDung(nguoiDung);
                            _hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công người dùng", nguoiDung.ToModel<NguoiDungModel>(), "NguoiDung");
                        }
                        else
                        {
                            response = new ResponseApi()
                            {
                                Status = false,
                                Message = "Đồng bộ sang kho thất bại",
                                Date = DateTime.Now,
                                Data = null
                            };
                            return response;
                        }
                    }
                }
            }
            else
            {
                response = new ResponseApi()
                {
                    Status = false,
                    Message = "Thông tin tài khoản không hợp lệ",
                    Date = DateTime.Now,
                    Data = null
                };
            }
            return response;
        }
        async Task<ResponseApi> PostDanhMuc(string action, Object obj, string TenDanhMuc)
        {
            string _action = CommonDanhMuc.RequestDanhMuc + action + CommonAction.DongBo;
            _hoatDongService.InsertHoatDong(_workContext.CurrentCustomer, enumHoatDong.DbCho, $"Gửi dữ liệu Đồng bộ Danh mục {TenDanhMuc}", 0, TenDanhMuc, obj);
            ResponseApi response = new ResponseApi();
            var token = _gSAPIService.GetTokenKhoCSDLQG();
            if (token == "1")
            {
                response = new ResponseApi()
                {
                    Status = false,
                    Message = "Lỗi truy xuất dữ liệu bên kho",
                    Date = DateTime.Now,
                    Data = 1
                };
                return response;
            }
            response = await _gSAPIService.PostObjectGSApi<ResponseApi>(_action, obj, token);
            InsertLogModel model = new InsertLogModel()
            {
                ResponseApi = response,
                Data = obj,
                LoaiDuLieu = "Danh mục"
            };
            if (response != null && response.Status)
            {
                _hoatDongService.InsertHoatDong(_workContext.CurrentCustomer, enumHoatDong.DaGuiDuLieu, "Đã Gửi dữ liệu Đồng bộ danh mục", 0, TenDanhMuc, model);
                return response;
            }
            else
            {
                _hoatDongService.InsertHoatDong(_workContext.CurrentCustomer, enumHoatDong.GuiDuLieuLoi, "Đã Gửi dữ liệu Đồng bộ danh mục bị lỗi", 0, TenDanhMuc, model);
                response = new ResponseApi()
                {
                    Status = false,
                    Message = "Mất kết nối máy chủ bên kho",
                    Date = DateTime.Now,
                    Data = 1
                };
                return response;
            }
        }
        #endregion
        #region Reset Mật khẩu
        public async Task<ResponseApi> ChangePasswordByKhoCSDL(PasswordRequestModel prameter)
        {
            // insert Queue
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong" , prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            //_hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ hao mòn từ file => api tsc{_nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)prameterDongBoTaiSanModel.LoaiBienDongId)} ngày { DateTime.Now}", prameterDongBoTaiSanModel);
            var respond = await _gSAPIService.PostObjectGSApi<ResponseApi>("ConsumingDanhMuc/ChangePasswordByKhoCSDL", prameter, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            return respond;

        }
        public async Task<ResponseApi> ResetPasswordByKhoCSDL(PasswordRequestModel prameter)
        {
            // insert Queue
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/UpdateBienDong" , prameterDongBoTaiSanModel, _workContext.CurrentDonVi.ID);
            //_hoatDongService.InsertHoatDong(enumHoatDong.DongBoTaiSan, $"Gửi đồng bộ hao mòn từ file => api tsc{_nhanHienThiService.GetGiaTriEnum<enumLOAI_LY_DO_TANG_GIAM>((enumLOAI_LY_DO_TANG_GIAM)prameterDongBoTaiSanModel.LoaiBienDongId)} ngày { DateTime.Now}", prameterDongBoTaiSanModel);
            var respond = await _gSAPIService.PostObjectGSApi<bool>("ConsumingDanhMuc/ResetPasswordByKhoCSDL", prameter, _gSAPIService.GetToken(true), _gsConfig.ApiUrlWebApi);
            if (respond)
            {
                return new ResponseApi() { Status = true, Message = "Đặt lại mật khẩu thành công" };
            }
            else
            {
                return new ResponseApi() { Status = false, Message = "Đặt lại mật khẩu không thành công" }; ;
            }


        }
        #endregion
    }
}
