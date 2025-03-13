using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.Api;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.Security;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DB;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.KeToan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public class DongBoController : BaseWorksController
    {
        #region field
        private readonly IDBTaiSanService _dBTaiSanService;
        private readonly IWorkContext _workContext;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IXuLyService _xuLyService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDonViService _donViService;
        private readonly ITaiSanService _taiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly INhanXeService _nhanXeService;
        private readonly IChucDanhService _chucDanhService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly IGSFileProvider _fileProvider;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IGSAPIService _gSAPIService;
        private readonly GSConfig _gSConfig;
        private readonly IDbContext _dbContext;
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly IQuyenService _quyenService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        private readonly IHaoMonTaiSanModelFactory _haoMonTaiSanModelFactory;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanModelFactory _khauHaoTaiSanModelFactory;
        private readonly IKhauHaoTaiSanService _khauHaoTaiSanService;
        private readonly INhanHienThiService _nhanHienThiService;

        public DongBoController
            (
            IDBTaiSanService dBTaiSanService,
            IWorkContext workContext,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            ITaiSanTdService taiSanTdService,
            IXuLyService xuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ILoaiTaiSanService loaiTaiSanService,
            IQuocGiaService quocGiaService,
            ILyDoBienDongService lyDoBienDongService,
            IDonViService donViService,
            ITaiSanService taiSanService,
            IDiaBanService diaBanService,
            INhanXeService nhanXeService,
            IChucDanhService chucDanhService,
            ITaiSanClnService taiSanClnService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanVktService taiSanVktService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanDatService taiSanDatService,
            IDonViBoPhanService donViBoPhanService,
            INguoiDungService nguoiDungService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IMucDichSuDungService mucDichSuDungService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            IGSFileProvider fileProvider,
            IFileCongViecModelFactory fileCongViecModelFactory,
            IFileCongViecService fileCongViecService,
            ILoaiDonViService loaiDonViService,
            INguonGocTaiSanService nguonGocTaiSanService,
            ICheDoHaoMonService cheDoHaoMonService,
            IDongBoFactory dongBoFactory,
            IGSAPIService gSAPIService,
            GSConfig gSConfig,
            IDbContext dbContext,
            ITaiSanModelFactory taiSanModelFactory,
            IQuyenService quyenService,
            IDonViModelFactory donViModelFactory,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory,
            IHaoMonTaiSanModelFactory haoMonTaiSanModelFactory,
            IHaoMonTaiSanService haoMonTaiSanService,
            IKhauHaoTaiSanModelFactory khauHaoTaiSanModelFactory,
            IKhauHaoTaiSanService khauHaoTaiSanService,
            INhanHienThiService nhanHienThiService
            )
        {
            this._dBTaiSanService = dBTaiSanService;
            this._workContext = workContext;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._xuLyService = xuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._quocGiaService = quocGiaService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._donViService = donViService;
            this._taiSanService = taiSanService;
            this._diaBanService = diaBanService;
            this._nhanXeService = nhanXeService;
            this._chucDanhService = chucDanhService;
            this._taiSanClnService = taiSanClnService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanDatService = taiSanDatService;
            this._donViBoPhanService = donViBoPhanService;
            this._nguoiDungService = nguoiDungService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._yeuCauService = yeuCauService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._bienDongService = bienDongService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._fileProvider = fileProvider;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
            this._fileCongViecService = fileCongViecService;
            this._loaiDonViService = loaiDonViService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._dongBoFactory = dongBoFactory;
            this._gSAPIService = gSAPIService;
            this._gSConfig = gSConfig;
            this._dbContext = dbContext;
            this._taiSanModelFactory = taiSanModelFactory;
            this._quyenService = quyenService;
            this._donViModelFactory = donViModelFactory;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
            this._haoMonTaiSanModelFactory = haoMonTaiSanModelFactory;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._khauHaoTaiSanModelFactory = khauHaoTaiSanModelFactory;
            this._khauHaoTaiSanService = khauHaoTaiSanService;
            this._nhanHienThiService = nhanHienThiService;
        }
        #endregion
        public virtual IActionResult DongBoTaiSan()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            model.ddlLoaiTaiSan = new List<SelectListItem>(){
               new SelectListItem()
            {
                Text = "Tài sản Nhà nước",
                Value = "2"
            },
                new SelectListItem()
            {
                Text = "Tài sản Toàn dân",
                Value = "1"
            },
                new SelectListItem()
            {
                Text = "Tài sản Khác",
                Value = "3"
            }
            };
            model.LoaiTaiSan = 2;
            return View(model);
        }
        public virtual IActionResult ImportTaiSanCu()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            model.ddlLoaiTaiSan = new List<SelectListItem>(){
               new SelectListItem()
            {
                Text = "Tài sản Nhà nước",
                Value = "2"
            },
                new SelectListItem()
            {
                Text = "Tài sản Toàn dân",
                Value = "1"
            },
                new SelectListItem()
            {
                Text = "Tài sản Khác",
                Value = "3"
            }
            };
            model.LoaiTaiSan = 2;
            return View(model);
        }
        public virtual IActionResult TimKiemDongBo()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuTimKiemDongBo))
                return AccessDeniedView();
            return View();
        }
        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        // [RequestSizeLimit(209715200)]
        public async Task<IActionResult> DongBoTaiSanFromFile(IFormFile file, int LoaiTaiSan)
        {

            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file tài sản");
                return JsonErrorMessage("Bạn chưa Nhập file tài sản");
            }
            if (LoaiTaiSan == 0)
            {
                ErrorNotification("Bạn chưa chọn Loại tài sản");
                return JsonErrorMessage("Bạn chưa chọn Loại tài sản");
            }
            string dataString = "";
            using (var fileStream = file.OpenReadStream())
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    //dataString = reader.ReadToEnd();
                    dataString = await reader.ReadToEndAsync();
                }
                //using (var ms = new MemoryStream())
                //{
                //    fileStream.CopyTo(ms);

                //    StreamReader reader = new StreamReader(ms);
                //    text = reader.ReadToEnd();
                //}
            }
            //var dataByte = _fileCongViecService.GetWorkFileBits(file);
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            //if (!string.IsNullOrEmpty(fileExtension))
            //    fileExtension = fileExtension.ToLowerInvariant();
            ////  lưu file 
            //var contentType = file.ContentType;
            //var fwork = new FileCongViec
            //{
            //    GUID = Guid.NewGuid(),
            //    NOI_DUNG_FILE = null,
            //    LOAI_FILE = contentType,
            //    //we store filename without extension for downloads
            //    TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
            //    DUOI_FILE = fileExtension,
            //    NGAY_TAO = DateTime.Now,
            //    NGUOI_TAO = _workContext.CurrentCustomer.ID
            //};
            //_fileCongViecService.InsertFileCongViec(fwork);
            //_fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
            ////Đọc file
            ////var fileCongViec = _fileCongViecService.GetFileByGuid(fwork.GUID);
            //var DataImport = GetWorkFileContentOnDisk(fwork);
            //var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
            //string dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
            if (LoaiTaiSan == 3)
            {
                StringContent content = new StringContent(dataString, Encoding.UTF8, "application/json");
                var result = await _gSAPIService.PostObjectGSApiWithStringContent<MessageReturn>(enumSendRequest.DongBoListTaiSan, content, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(result);
            }
            else
            {
                List<TaiSanDongBoApi> ListTaiSanApi = new List<TaiSanDongBoApi>();
                List<ResultDongBoModel> ListResultError = new List<ResultDongBoModel>();
                List<ResultDongBoModel> ListResultSuccess = new List<ResultDongBoModel>();
                List<string> ListTaiSanLoi = new List<string>();

                List<TaiSanDongBoApi> ListTaiSanApiErr = new List<TaiSanDongBoApi>();

                if (LoaiTaiSan == 2)// tài sản nhà nước
                {
                    //if (fileExtension == ".xml")
                    //{
                    //    Stream stream = new MemoryStream(DataImport);
                    //    XmlSerializer ser = new XmlSerializer(typeof(List<TaiSanDongBoApi>), new XmlRootAttribute("root"));
                    //    ListTaiSanApi = (List<TaiSanDongBoApi>)ser.Deserialize(stream);

                    //    foreach (var item in ListTaiSanApi)
                    //    {
                    //        if (item.LST_BIEN_DONG.Count == 0 || item.LST_BIEN_DONG == null)
                    //        {
                    //            continue;
                    //        }
                    //        var result = _dBTaiSanService.InsertOrUpdateTaiSanNhaNuoc(item);
                    //        if (result.Code != MessageReturn.SUCCESS_CODE)
                    //        {
                    //            _dBTaiSanService.DelelteTaiSanDB(item.MA_TAI_SAN);
                    //            ResultDongBoModel resultDongBo = new ResultDongBoModel();
                    //            resultDongBo.TaiSanDongBoApi = item;
                    //            resultDongBo.Message = result.Message;
                    //            resultDongBo.QuyetDinhTichThuApi = null;
                    //            ListResultError.Add(resultDongBo);
                    //            ListTaiSanApiErr.Add(item);
                    //        }
                    //        else
                    //        {
                    //            ResultDongBoModel resultDongBo = new ResultDongBoModel();
                    //            resultDongBo.QuyetDinhTichThuApi = null;
                    //            resultDongBo.Message = result.Message;
                    //            resultDongBo.TaiSanDongBoApi = item;
                    //            ListResultSuccess.Add(resultDongBo);
                    //        }
                    //    }
                    //}
                    if (fileExtension == ".json")
                    {
                        var serializerSettings = new JsonSerializerSettings();
                        serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                        serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                        serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
                        serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        ListTaiSanApi = JsonConvert.DeserializeObject<List<TaiSanDongBoApi>>(dataString, serializerSettings);

                        int total = ListTaiSanApi != null ? ListTaiSanApi.Count() : 0;
                        int pageSize = 100;
                        int TotalPages = total / pageSize;
                        if (total % pageSize > 0)
                            TotalPages++;
                        if (TotalPages > 0)
                            for (int i = 0; i < TotalPages; i++)
                            {
                                var lst = ListTaiSanApi.Skip(i * pageSize).Take(pageSize).ToList();
                                foreach (var item in lst)
                                //foreach (var item in ListTaiSanApi)
                                {
                                    if (string.IsNullOrEmpty(item.MA_TAI_SAN))
                                        item.MA_TAI_SAN = item.DB_MA;
                                    else
                                        item.DB_MA = item.MA_TAI_SAN;
                                    if (item.LST_BIEN_DONG == null)
                                    {
                                        ListResultError.Add(new ResultDongBoModel()
                                        {
                                            TaiSanDongBoApi = item,
                                            QuyetDinhTichThuApi = null,
                                            Message = "Biến động null"
                                        });
                                        ListTaiSanApiErr.Add(item);
                                        continue;
                                    }
                                    if (item.LST_BIEN_DONG.Count == 0)
                                    {
                                        ListResultError.Add(new ResultDongBoModel()
                                        {
                                            TaiSanDongBoApi = item,
                                            QuyetDinhTichThuApi = null,
                                            Message = "Biến động empty"
                                        });
                                        ListTaiSanApiErr.Add(item);
                                        continue;
                                    }

                                    //var result = _dBTaiSanService.InsertOrUpdateTaiSanNhaNuoc(item);
                                    string jsonTaiSan = item.toStringJson();
                                    var result = _dBTaiSanService.insertOrupdateTaiSanByJson(jsonTaiSan, item.MA_TAI_SAN, false, item.CHE_DO_HAO_MON_ID);

                                    if (result.Code != MessageReturn.SUCCESS_CODE)
                                    {
                                        ListResultError.Add(new ResultDongBoModel()
                                        {
                                            TaiSanDongBoApi = item,
                                            QuyetDinhTichThuApi = null,
                                            Message = result.Message
                                        });
                                        ListTaiSanApiErr.Add(item);
                                    }
                                    else
                                    {
                                        item.MA_TAI_SAN = result.ObjectInfo;
                                        ListResultSuccess.Add(new ResultDongBoModel()
                                        {
                                            QuyetDinhTichThuApi = null,
                                            Message = result.Message,
                                            TaiSanDongBoApi = item
                                        });
                                    }
                                }

                            }
                    }
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        using (var fileStream = file.OpenReadStream())
                        {
                            var result = _dBTaiSanService.ImportExcel(fileStream);
                        }
                    }
                }
                List<QuyetDinhTichThuApi> ListQuyetDinhTichThuApiErr = new List<QuyetDinhTichThuApi>();
                if (LoaiTaiSan == 1)//tài sản toàn dân
                {
                    if (fileExtension == ".xml")
                    {
                        Stream stream = file.OpenReadStream();
                        XmlSerializer ser = new XmlSerializer(typeof(List<QuyetDinhTichThuApi>), new XmlRootAttribute("root"));
                        var ListQuyetDinhTichThuApi = (List<QuyetDinhTichThuApi>)ser.Deserialize(stream);
                        foreach (var item in ListQuyetDinhTichThuApi)
                        {
                            var result = _dBTaiSanService.InsertOrUpdateTaiSanToanDan(item);
                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ResultDongBoModel resultDongBo = new ResultDongBoModel();
                                resultDongBo.QuyetDinhTichThuApi = item;
                                resultDongBo.TaiSanDongBoApi = null;
                                resultDongBo.Message = result.Message;
                                ListQuyetDinhTichThuApiErr.Add(item);
                                ListResultError.Add(resultDongBo);
                            }
                            else
                            {
                                ResultDongBoModel resultDongBo = new ResultDongBoModel();
                                resultDongBo.QuyetDinhTichThuApi = item;
                                resultDongBo.Message = result.Message;
                                resultDongBo.TaiSanDongBoApi = null;
                                ListResultSuccess.Add(resultDongBo);
                            }
                        }
                    }
                    if (fileExtension == ".json")
                    {
                        var ListQuyetDinhTichThuApi = dataString.toEntities<QuyetDinhTichThuApi>();
                        foreach (var item in ListQuyetDinhTichThuApi)
                        {
                            var result = _dBTaiSanService.InsertOrUpdateTaiSanToanDan(item);
                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ResultDongBoModel resultDongBo = new ResultDongBoModel();
                                resultDongBo.QuyetDinhTichThuApi = item;
                                resultDongBo.Message = result.Message;
                                resultDongBo.TaiSanDongBoApi = null;
                                ListQuyetDinhTichThuApiErr.Add(item);
                                ListResultError.Add(resultDongBo);
                            }
                            else
                            {
                                ResultDongBoModel resultDongBo = new ResultDongBoModel();
                                resultDongBo.QuyetDinhTichThuApi = item;
                                resultDongBo.Message = result.Message;
                                resultDongBo.TaiSanDongBoApi = null;
                                ListResultSuccess.Add(resultDongBo);
                            }
                        }
                    }

                }
                if (ListResultError.Count > 0)
                {
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);

                    string fName = "";
                    string fullpath = "";
                    fName = string.Format("LOG_ERR_{0}_{1}({2}).json", LoaiTaiSan == 2 ? "TSNN" : "TSTD", LoaiTaiSan == 2 ? ListTaiSanApiErr.Count() : ListQuyetDinhTichThuApiErr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                    fullpath = _fileProvider.Combine(_pathStore, fName);
                    string json = "";
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    if (LoaiTaiSan == 2)
                        json = JsonConvert.SerializeObject(ListTaiSanApiErr, serializerSettings);
                    else
                        json = JsonConvert.SerializeObject(ListQuyetDinhTichThuApiErr, serializerSettings);

                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    return Json(new
                    {
                        success = false,
                        ListSuccess = ListResultSuccess,
                        ListError = ListResultError,
                        filePath = fullpath,
                        fileName = fName,
                        fileType = MimeTypes.ApplicationJson
                    });
                }

                else
                {
                    return Json(new
                    {
                        success = true,
                        ListSuccess = ListResultSuccess,
                    });
                }


            }


        }
        [HttpPost]
        public IActionResult DongBoTaiSanFromFileDirectory(string pathFolder)
        {
            if (string.IsNullOrEmpty(pathFolder))
            {
                ErrorNotification("Bạn chưa Nhập đường dẫn thư mục file tài sản");
                return RedirectToAction("DongBoTaiSan");
            }
            List<TaiSanDongBoApi> ListTaiSanApi = new List<TaiSanDongBoApi>();
            List<ResultDongBoModel> ListResultError = new List<ResultDongBoModel>();
            List<ResultDongBoModel> ListResultSuccess = new List<ResultDongBoModel>();
            List<TaiSanDongBoApi> ListTaiSanApiErr = new List<TaiSanDongBoApi>();
            string[] filePaths = Directory.GetFiles(pathFolder, "*.json", SearchOption.AllDirectories);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            FileCongViec fwork = new FileCongViec();
            String path = "";
            string dataString = "";
            string jsonTaiSan = "";
            string fileName = "";
            string fileExtension = "";
            MessageReturn result = new MessageReturn();
            foreach (string filePath in filePaths)
            {
                if (_fileProvider.FileExists(filePath))
                {
                    fileName = _fileProvider.GetFileName(filePath);
                    if (_fileCongViecService.CheckFileImpTaiSanDone(fileName))
                        continue;

                    fileExtension = _fileProvider.GetFileExtension(fileName);
                    if (!string.IsNullOrEmpty(fileExtension))
                        fileExtension = fileExtension.ToLowerInvariant();
                    //  lưu file 
                    fwork = new FileCongViec
                    {
                        GUID = Guid.NewGuid(),
                        NOI_DUNG_FILE = null,
                        LOAI_FILE = MimeTypes.ApplicationJson,
                        //we store filename without extension for downloads
                        TEN_FILE = fileName,
                        DUOI_FILE = fileExtension,
                        NGAY_TAO = DateTime.Now,
                        NGUOI_TAO = _workContext.CurrentCustomer.ID,
                        LOAI_FILE_ID = 0
                    };
                    _fileCongViecService.InsertFileCongViec(fwork);
                    _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, _fileProvider.ReadAllBytes(filePath));
                    //Đọc file
                    //var fileCongViec = _fileCongViecService.GetFileByGuid(fwork.GUID);
                    path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
                    dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
                    ListTaiSanApi = JsonConvert.DeserializeObject<List<TaiSanDongBoApi>>(dataString, serializerSettings);

                    foreach (TaiSanDongBoApi item in ListTaiSanApi)
                    {
                        if (string.IsNullOrEmpty(item.MA_TAI_SAN))
                            item.MA_TAI_SAN = item.DB_MA;
                        else
                            item.DB_MA = item.MA_TAI_SAN;
                        if (_taiSanService.CheckTonTaiByMaQLDKTS40(item.DB_MA))
                        {
                            ListResultSuccess.Add(new ResultDongBoModel()
                            {
                                QuyetDinhTichThuApi = null,
                                Message = result.Message,
                                TaiSanDongBoApi = item
                            });
                            continue;
                        }
                        if (item.LST_BIEN_DONG == null)
                        {
                            ListResultError.Add(new ResultDongBoModel()
                            {
                                TaiSanDongBoApi = item,
                                QuyetDinhTichThuApi = null,
                                Message = "Biến động null"
                            });
                            ListTaiSanApiErr.Add(item);
                            continue;
                        }
                        if (item.LST_BIEN_DONG.Count == 0)
                        {
                            ListResultError.Add(new ResultDongBoModel()
                            {
                                TaiSanDongBoApi = item,
                                QuyetDinhTichThuApi = null,
                                Message = "Biến động empty"
                            });
                            ListTaiSanApiErr.Add(item);
                            continue;
                        }

                        //var result = _dBTaiSanService.InsertOrUpdateTaiSanNhaNuoc(item);
                        jsonTaiSan = item.toStringJson();
                        result = _dBTaiSanService.insertOrupdateTaiSanByJson(jsonTaiSan, item.MA_TAI_SAN, false, item.CHE_DO_HAO_MON_ID);

                        if (result.Code != MessageReturn.SUCCESS_CODE)
                        {
                            ListResultError.Add(new ResultDongBoModel()
                            {
                                TaiSanDongBoApi = item,
                                QuyetDinhTichThuApi = null,
                                Message = result.Message
                            });
                            ListTaiSanApiErr.Add(item);
                        }
                        else
                        {
                            item.MA_TAI_SAN = result.ObjectInfo;
                            ListResultSuccess.Add(new ResultDongBoModel()
                            {
                                QuyetDinhTichThuApi = null,
                                Message = result.Message,
                                TaiSanDongBoApi = item
                            });
                        }
                    }
                    fwork.LOAI_FILE_ID = 11;//DONE
                    _fileCongViecService.UpdateFileCongViec(fwork);
                    fwork = null;
                }
            }
            if (ListResultError.Count > 0)
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);

                string fName = "";
                string fullpath = "";
                fName = string.Format("LOG_ERR_{0}_{1}({2}).json", "TSNN", ListTaiSanApiErr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                fullpath = _fileProvider.Combine(_pathStore, fName);
                string json = "";
                json = JsonConvert.SerializeObject(ListTaiSanApiErr, serializerSettings);

                _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                return Json(new
                {
                    success = false,
                    ListSuccess = ListResultSuccess,
                    ListError = ListResultError,
                    filePath = fullpath,
                    fileName = fName,
                    fileType = MimeTypes.ApplicationJson
                });
            }
            else
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                //SuccessNotification("Import tài sản thành công");
                return Json(new
                {
                    success = true,
                    ListSuccess = ListResultSuccess,
                    //ListError = ListResultError,
                });
            }
        }
        //[HttpPost]
        public virtual IActionResult DanhSachTaiSanChuaDongBo(IList<string> model, decimal LoaiTaiSan)
        {
            List<ResultDongBoModel> resultDongBos = new List<ResultDongBoModel>();
            foreach (var item in model)
            {
                var ts = item.Split('-');
                TaiSanDongBoApi taiSanDongBoApi = new TaiSanDongBoApi();
                taiSanDongBoApi.MA_TAI_SAN = ts[0];
                taiSanDongBoApi.TEN_TAI_SAN = ts[1];
                ResultDongBoModel resultDongBo = new ResultDongBoModel();
                resultDongBo.TaiSanDongBoApi = taiSanDongBoApi;
                resultDongBo.Message = ts[2];
                resultDongBos.Add(resultDongBo);
            }
            ViewBag.LoaiTaiSan = LoaiTaiSan;
            return View(resultDongBos);
        }
        public virtual IActionResult DongBoTaiSanToanDan()
        {
            int ErrCount = 0;
            var ListDongBo = _dBTaiSanService.GetAllTaiSans(true);
            foreach (var item in ListDongBo)
            {
                try
                {
                    // quyết định tịch thu
                    QuyetDinhTichThuApi quyetDinhTichThuApi = new QuyetDinhTichThuApi();
                    quyetDinhTichThuApi = item.DATA_JSON.toEntity<QuyetDinhTichThuApi>();
                    var reusult = _dBTaiSanService.InsertOrUpdateTaiSanToanDan(quyetDinhTichThuApi);

                    _dBTaiSanService.DeleteTaiSan(item);

                }
                catch (Exception ex)
                {
                    ErrCount++;
                }
            }
            return JsonSuccessMessage("Đã đồng bộ " + ErrCount + "/" + ListDongBo.Count + " Quyết định tịch thu");
        }
        public virtual IActionResult DongBoQuyetDinhTichThuTSTD()
        {
            int ErrCount = 0;
            var ListDongBo = _dBTaiSanService.GetAllTaiSans(true);
            foreach (var item in ListDongBo)
            {
                try
                {
                    // quyết định tịch thu
                    QuyetDinhTichThuApi quyetDinhTichThuApi = new QuyetDinhTichThuApi();
                    quyetDinhTichThuApi = item.DATA_JSON.toEntity<QuyetDinhTichThuApi>();
                    var reusult = _dBTaiSanService.InsertOrUpdateTaiSanToanDan(quyetDinhTichThuApi);

                    _dBTaiSanService.DeleteTaiSan(item);

                }
                catch (Exception ex)
                {
                    ErrCount++;
                }
            }
            return JsonSuccessMessage("Đã đồng bộ " + ErrCount + "/" + ListDongBo.Count + " Quyết định tịch thu");
        }
        //public virtual IActionResult DongBoTaiSanKhac()
        //{
        //    int ErrCount = 0;
        //    var ListDongBo = _dBTaiSanService.GetAllTaiSans();
        //    foreach (var item in ListDongBo)
        //    {
        //        try
        //        {
        //            // kiểm tra mã tài sản đã tồn tại                 
        //            TaiSanDongBoApi taiSanApi = new TaiSanDongBoApi();
        //            taiSanApi = item.DATA_JSON.toEntity<TaiSanDongBoApi>();
        //            var result = _dBTaiSanService.InsertOrUpdateTaiSanNhaNuoc(taiSanApi);
        //            if (result.Code == MessageReturn.SUCCESS_CODE)
        //            {
        //                _dBTaiSanService.DeleteTaiSan(item);
        //            }
        //            else
        //            {
        //                ErrCount++;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrCount++;
        //        }
        //    }
        //    return JsonSuccessMessage("Đã đồng bộ " + (ListDongBo.Count - ErrCount) + "/" + ListDongBo.Count + " Tài sản");
        //}
        public T Deserialize<T>(string input) where T : class
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "root";
            xRoot.IsNullable = true;
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("root"));
            XmlReaderSettings settings = new XmlReaderSettings();
            using (StringReader textReader = new StringReader(input))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    var data = (T)ser.Deserialize(xmlReader);
                    return data;
                }
            }

            //using (StringReader sr = new StringReader(input))
            //{
            //    return (T)ser.Deserialize(sr);
            //}
        }
        byte[] GetWorkFileContentOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            return _fileProvider.ReadAllBytes(_fileStore);
        }
        public async Task<IActionResult> DongBoDanhMuc()
        {
            Authen authen = new Authen()
            {
                Username = "admin",
                Password = "Gs2020@"
            };
            string apiResponse = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(authen), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://dkts.gsolution.vn:8889/api/" + "AuthenSvc/Login", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            MessageReturn messageReturn = JsonConvert.DeserializeObject<MessageReturn>(apiResponse);
            return View(messageReturn);
        }
        public virtual IActionResult ExportByUrl(string filePath, string fileName, string fileType)
        {
            byte[] bytes;
            bytes = _fileProvider.ReadAllBytes(filePath);
            _fileProvider.DeleteFile(filePath);
            return File(bytes, fileType, fileName);
        }
        //public async Task<IActionResult> DongBoTaiSan     
        public virtual IActionResult DongBoCCDC()
        {
            //var lstbdct = _bienDongChiTietService.GetAllBienDongChiTiets().Where(c => c.biendong.taisan.MA_DB != null);
            //foreach (BienDongChiTiet item in lstbdct)
            //{
            //    item.HS_CNQSD_NGAY = String.IsNullOrEmpty(item.HS_CNQSD_SO ) ? null : item.HS_CNQSD_NGAY;
            //    item.HS_QUYET_DINH_GIAO_NGAY = String.IsNullOrEmpty(item.HS_QUYET_DINH_GIAO_SO ) ? null : item.HS_QUYET_DINH_GIAO_NGAY;
            //    item.HS_CHUYEN_NHUONG_SD_NGAY = String.IsNullOrEmpty(item.HS_CHUYEN_NHUONG_SD_SO ) ? null : item.HS_CHUYEN_NHUONG_SD_NGAY;
            //    item.HS_QUYET_DINH_CHO_THUE_NGAY = String.IsNullOrEmpty(item.HS_QUYET_DINH_CHO_THUE_SO ) ? null : item.HS_QUYET_DINH_CHO_THUE_NGAY;
            //    item.HS_QUYET_DINH_BAN_GIAO_NGAY = String.IsNullOrEmpty(item.HS_QUYET_DINH_BAN_GIAO ) ? null : item.HS_QUYET_DINH_BAN_GIAO_NGAY;
            //    item.HS_BIEN_BAN_NGHIEM_THU_NGAY = String.IsNullOrEmpty(item.HS_BIEN_BAN_NGHIEM_THU ) ? null : item.HS_BIEN_BAN_NGHIEM_THU_NGAY;
            //    item.HS_PHAP_LY_KHAC_NGAY = String.IsNullOrEmpty(item.HS_PHAP_LY_KHAC ) ? null : item.HS_PHAP_LY_KHAC_NGAY;
            //    _bienDongChiTietService.UpdateBienDongChiTiet(item);
            //}
            return View();
        }
        public virtual IActionResult DongBoTaiSanDaNhan()
        {
            var result = _dongBoFactory.DongBoTaiSan();
            return Json(result);
        }
        // gọi service đồng bộ tài sản
        public virtual IActionResult GuiTaiSanSangKho()
        {
            //string Action = ""
            //var apiResponse=
            return View();
        }
        [HttpPost]
        public virtual async Task<IActionResult> DongBoThuCong(IList<decimal> ListTaiSanId, decimal LoaiBienDongId)
        {
            var result = await _dongBoFactory.DongBoTaiSanThuCong(ListTaiSanId.ToList(), LoaiBienDongId);
            return Json(new
            {
                ListTaiSanId = ListTaiSanId,
                Response = result
            });
        }

        public virtual async Task<IActionResult> CapNhatMaDb()
        {
            //List<string> ListMaTaiSan = ListTaiSanId.Select(m => _taiSanService.GetTaiSanById(m).MA).ToList();
            var result = await _dongBoFactory.GetMaDongBo();
            return Json(result);
        }
        /// <summary>
        ///  đồng bộ biến động tăng mới theo đơn vị
        /// </summary>
        /// <param name="LoaiBienDongId"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> DongBoTheoDonVi(decimal LoaiBienDongId)
        {
            var result = await _dongBoFactory.DongBoTaiSanTheoDonVi(_workContext.CurrentDonVi.ID, LoaiBienDongId);
            return Json(result);
        }
        [HttpPost]
        public virtual IActionResult ExcuteMapTSNhaDat()
        {
            //String sql = @" 
            //                UPDATE TS_TAI_SAN_NHA TSNHA
            //                SET
            //                    TSNHA.TAI_SAN_DAT_ID = (
            //                        SELECT
            //                            ID
            //                        FROM
            //                            TS_TAI_SAN
            //                        WHERE
            //                            MA_QLDKTS40 = TSNHA.MA_DB_DAT
            //                            AND NGUON_TAI_SAN_ID = 1
            //                        ORDER BY
            //                            ID
            //                        FETCH FIRST 1 ROW ONLY
            //                    )
            //                WHERE
            //                    TSNHA.MA_DB_DAT IS NOT NULL
            //                    AND TSNHA.TAI_SAN_DAT_ID IS NULL
            //                    AND TSNHA.TAI_SAN_ID IN (
            //                        SELECT
            //                            ID
            //                        FROM
            //                            TS_TAI_SAN
            //                        WHERE
            //                            LOAI_HINH_TAI_SAN_ID = 2
            //                            AND NGUON_TAI_SAN_ID = 1
            //                            AND TRANG_THAI_ID != 6
            //                    )";
            String sql = @" BEGIN
                                FOR R_TS IN (
                                    SELECT
                                        NHA.TAI_SAN_ID,
                                        NHA.MA_DB_DAT
                                    FROM
                                        TS_TAI_SAN_NHA   NHA
                                        LEFT JOIN TS_TAI_SAN       TS ON TS.ID = NHA.TAI_SAN_ID
                                    WHERE
                                        NHA.MA_DB_DAT IS NOT NULL
                                        AND NHA.TAI_SAN_DAT_ID IS NULL
                                        AND TS.NGUON_TAI_SAN_ID = 1
                                        AND TS.TRANG_THAI_ID != 6
                                        AND TS.LOAI_HINH_TAI_SAN_ID = 2
                                ) LOOP
                                    UPDATE TS_NHA
                                    SET
                                        TAI_SAN_DAT_ID = (
                                            SELECT
                                                ID
                                            FROM
                                                TS_TAI_SAN
                                            WHERE
                                                MA_QLDKTS40 = R_TS.MA_DB_DAT
                                                AND NGUON_TAI_SAN_ID = 1
                                                AND TRANG_THAI_ID != 6
                                                AND LOAI_HINH_TAI_SAN_ID = 1
                                        )
                                    WHERE
                                        TAI_SAN_ID = R_TS.TAI_SAN_ID;

                                    COMMIT;
                                END LOOP;
                            END";
            var rs = _dbContext.ExecuteSqlCommand(sql);
            return JsonSuccessMessage($"{rs} tài sản map thành công");
        }

        #region Đồng bộ tài sản dkts4.0 sang kho csdlqg
        //public virtual IActionResult DongBoTaiSanApi()
        //{
        //    //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
        //    //    return AccessDeniedView();
        //    //prepare model
        //    var searchmodel = new DongBoTaiSanSearchModel();
        //    searchmodel.ddlDonVi = new List<SelectListItem>();
        //    var donVis = _donViService.GetAllDonVis(TreeLevel: 1);
        //    if (donVis != null)
        //        searchmodel.ddlDonVi = donVis.OrderBy(c => c.TEN).Select(c => new SelectListItem()
        //        {
        //            Value = c.ID.ToString(),
        //            Text = c.TEN
        //        }).ToList();
        //    searchmodel.ddlDonVi.Insert(0, new SelectListItem() { Text = "---Tất cả---", Value = "0" });
        //    int[] valuesExclude = { (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
        //    searchmodel.LoaiHinhTaiSanAvailable = new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: valuesExclude);
        //    foreach (var lhtschecK in searchmodel.LoaiHinhTaiSanAvailable)
        //    {
        //        if (lhtschecK.Value.ToNumber() == (decimal)enumLOAI_HINH_TAI_SAN.ALL)
        //        {
        //            lhtschecK.Selected = true;
        //        }
        //    }
        //    return View(searchmodel);
        //}
        //[HttpPost]
        //public async Task<IActionResult> DongBoTaiSanCu(DongBoTaiSanSearchModel searchmodel)
        //{
        //    searchmodel.LoaiHinhTaiSanSelected = searchmodel.strLoaiHinhTSIds.Split(',').Select(c => Convert.ToInt32(c)).Where(c => c != 0).ToList();
        //    var taiSans = _taiSanService.GetAllTaiSans(arrLoaiHinhTaiSan: searchmodel.LoaiHinhTaiSanSelected, donViId: searchmodel.donViId, ngayTaoTu: searchmodel.fromDate, ngayTaoDen: searchmodel.toDate);
        //    List<decimal> ids = new List<decimal>();
        //    if (taiSans != null)
        //        ids = taiSans.Select(c => c.ID).ToList();
        //    if (ids != null && ids.Count > 0)
        //    {

        //        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/DongBoThuCong", new { ListTaiSanId = ids }, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
        //        return Json(new
        //        {
        //            success = true,
        //            Data = result.ObjectInfo,
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //            Data = "Không có dữ liệu đồng bộ",
        //        });
        //    }
        //}
        public virtual IActionResult DongBoTaiSanCu()
        {
            PrameterDongBoTaiSanModel model = new PrameterDongBoTaiSanModel();
            model.DdlDonViCha = _donViModelFactory.PrepareSelectListBoNganhTinh().ToList();
            model.DdlLoaiBienDong = new enumLOAI_LY_DO_TANG_GIAM().ToSelectList().ToList();
            return View(model);
        }
        public virtual IActionResult GetddlDonViCon(int DonViCha)
        {
            var ddlDonViCon = _donViModelFactory.PrepareDdlDonVi(DonViCha);
            return Json(ddlDonViCon);
        }
        [HttpPost]
        public virtual IActionResult DongBoTaiSanCu(PrameterDongBoTaiSanModel model)
        {
            // get tất cả tài sản của đơn vị và đơn vị con

            // list đơn vị con         
            List<ResponseApi> ResponseApis = new List<ResponseApi>();
            DonVi donVi = _donViService.GetDonViById(model.DonViCha.Value);
            //var listTaiSan = _taiSanService.GetListTaiSanTheoDonVi(DonViCha);           
            //    var ListTaiSanId = listTaiSan.Select(m => m.ID);
            var data = new
            {
                DonViId = model.DonViCha,
                LoaiBienDongId = model.LoaiBienDongId,
                NguonTaiSanId = model.NguonTaiSanId
                //ListTaiSanId = ListTaiSanId
            };
            var rs = _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/DongBoThuCong", data, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
            //_gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingTaiSan/PrepareDuLieuDongBo", data, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
            //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSan/DongBoTaiSanCu", data, _workContext.CurrentDonVi.ID);

            return JsonSuccessMessage("Cập nhật thành công", rs);

        }


        #endregion
        [HttpPost]
        public virtual IActionResult SaveTaiSanFromFileToTemp(String DirectoryPath)
        {
            //if (String.IsNullOrEmpty(DirectoryPath))
            //{
            //    ErrorNotification("Bạn chưa Nhập file tài sản");
            //    return RedirectToAction("DongBoTaiSan");
            //}
            //var filePaths = _fileProvider.GetFiles(DirectoryPath, "*.json");
            //foreach (string filePath in filePaths)
            //{
            //    string fileContent = _fileProvider.ReadAllText(filePath, Encoding.UTF8);
            //    var file = _fileProvider.GetFileInfo(filePath);
            //}

            return RedirectToAction("ImportTaiSanCu");
        }
        [HttpPost]
        public virtual IActionResult SearchTaiSanDongBo(String KeySearch)
        {
            if (String.IsNullOrEmpty(KeySearch))
                return null;
            KeySearch = KeySearch.ToUpper().Trim();
            var data = _taiSanService.SearchTaiSanDongBo(KeySearch).Select(x => new { MA_TAI_SAN = x.MA, MA_DB = x.MA_DB, MA_QLDKTS40 = x.MA_QLDKTS40, TEN_TAI_SAN = x.TEN, NGUON_TAI_SAN = _nhanHienThiService.GetGiaTriEnum((enumNguonTaiSan)x.NGUON_TAI_SAN_ID), TEN_TRANG_THAI = _nhanHienThiService.GetGiaTriEnum((enumTRANG_THAI_TAI_SAN)x.TRANG_THAI_ID) });
            return Json(data);
        }

        [HttpPost]
        public IActionResult ImportHaoMonFromFile(IFormFile file, int nguonId)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file tài sản");
                return RedirectToAction("ImportHaoMon");
            }
            if (nguonId == 0)
            {
                ErrorNotification("Bạn chưa chọn nguồn hao mòn");
                return RedirectToAction("ImportHaoMon");
            }
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            string dataString = ReadTextFromFile(file);

            List<HaoMonDBModel> ListHaoMonTaiSan = new List<HaoMonDBModel>();
            List<ResultDongBoModel> ListResultError = new List<ResultDongBoModel>();
            List<ResultDongBoModel> ListResultSuccess = new List<ResultDongBoModel>();
            List<string> ListHaoMonTaiSanLoi = new List<string>();

            List<HaoMonDBModel> ListHaoMonTaiSanErr = new List<HaoMonDBModel>();
            List<HaoMonDBModel> ListHaoMonTaiSanSuccess = new List<HaoMonDBModel>();

            if (fileExtension == ".json")
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                ListHaoMonTaiSan = JsonConvert.DeserializeObject<List<HaoMonDBModel>>(dataString, serializerSettings);
                int total = ListHaoMonTaiSan != null ? ListHaoMonTaiSan.Count() : 0;
                int pageSize = 100;
                int TotalPages = total / pageSize;
                if (total % pageSize > 0)
                    TotalPages++;
                if (TotalPages > 0)
                    for (int i = 0; i < TotalPages; i++)
                    {
                        var lst = ListHaoMonTaiSan.Skip(i * pageSize).Take(pageSize).ToList();
                        foreach (var item in lst)
                        {
                            if (string.IsNullOrEmpty(item.MA_TAI_SAN))
                                item.MA_TAI_SAN = item.MA_TAI_SAN_DB;
                            else
                                item.MA_TAI_SAN_DB = item.MA_TAI_SAN;
                            if (nguonId == (int)enumNguonTaiSan.DKTS40)
                            {
                                item.MA_TAI_SAN = item.MA_DKTS;
                            }
                            string jsonHaoMonTaiSan = item.toStringJson();

                            var result = _haoMonTaiSanModelFactory.InSertHaoMonDongBo(item, nguonId);

                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ListResultError.Add(new ResultDongBoModel()
                                {
                                    HaoMonDB = item,
                                    Message = result.Message
                                });
                                ListHaoMonTaiSanErr.Add(item);
                            }
                            else
                            {
                                ListResultSuccess.Add(new ResultDongBoModel()
                                {
                                    Message = result.Message,
                                    HaoMonDB = item
                                });
                                ListHaoMonTaiSanSuccess.Add(item);
                            }
                        }

                    }
                // import list thành công


                // update
                var listUpdateHaoMon = ListHaoMonTaiSanSuccess.Select(c =>
                {
                    var haoMonbyNam = _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(c.TAI_SAN_ID, c.NAM_HAO_MON);
                    if (haoMonbyNam != null)
                    {
                        _haoMonTaiSanModelFactory.PrepareHaoMonTaiSanFromHaoMonDB(c, haoMonbyNam);
                    }
                    return haoMonbyNam;
                }).Where(c => c != null);
                if (listUpdateHaoMon != null && listUpdateHaoMon.Count() > 0)
                {
                    _haoMonTaiSanService.UpdateHaoMonTaiSan(listUpdateHaoMon);
                }

                // create
                var listCreateHaoMon = ListHaoMonTaiSanSuccess.Where(c => _haoMonTaiSanService.GetHaoMonTaiSanByTSIdandNamBaoCao(c.TAI_SAN_ID, c.NAM_HAO_MON) == null).Select(c =>
                {
                    var haoMon = new HaoMonTaiSan();
                    _haoMonTaiSanModelFactory.PrepareHaoMonTaiSanFromHaoMonDB(c, haoMon);
                    return haoMon;
                });
                if (listCreateHaoMon != null && listCreateHaoMon.Count() > 0)
                {
                    _haoMonTaiSanService.InsertHaoMonTaiSan(listCreateHaoMon);
                }

            }
            if (ListResultError.Count > 0)
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);

                string fName = "";
                string fullpath = "";
                fName = string.Format("LOG_ERR_HAOMON_{0}_{1}({2}).json", nguonId == 1 ? "DKTS4.0" : "TSC", ListHaoMonTaiSanErr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                fullpath = _fileProvider.Combine(_pathStore, fName);
                string json = "";
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                json = JsonConvert.SerializeObject(ListHaoMonTaiSanErr, serializerSettings);

                _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                return Json(new
                {
                    success = false,
                    ListSuccess = ListResultSuccess,
                    ListError = ListResultError,
                    filePath = fullpath,
                    fileName = fName,
                    fileType = MimeTypes.ApplicationJson
                });
            }

            else
            {
                return Json(new
                {
                    success = true,
                    ListSuccess = ListResultSuccess,
                });
            }


        }


        [HttpPost]
        public IActionResult ImportKhauHaoFromFile(IFormFile file, int nguonId)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file khấu hao");
                return RedirectToAction("ImportHaoMon");
            }
            if (nguonId == 0)
            {
                ErrorNotification("Bạn chưa chọn nguồn khấu hao");
                return RedirectToAction("ImportHaoMon");
            }

            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            string dataString = ReadTextFromFile(file);

            List<KhauHaoDBModel> ListKhauHaoTaiSan = new List<KhauHaoDBModel>();
            List<ResultDongBoModel> ListResultError = new List<ResultDongBoModel>();
            List<ResultDongBoModel> ListResultSuccess = new List<ResultDongBoModel>();
            List<string> ListKhauHaoTaiSanLoi = new List<string>();

            List<KhauHaoDBModel> ListKhauHaoTaiSanErr = new List<KhauHaoDBModel>();
            List<KhauHaoDBModel> ListKhauHaoTaiSanSuccess = new List<KhauHaoDBModel>();

            if (fileExtension == ".json")
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                ListKhauHaoTaiSan = JsonConvert.DeserializeObject<List<KhauHaoDBModel>>(dataString, serializerSettings);
                int total = ListKhauHaoTaiSan != null ? ListKhauHaoTaiSan.Count() : 0;
                int pageSize = 100;
                int TotalPages = total / pageSize;
                if (total % pageSize > 0)
                    TotalPages++;
                if (TotalPages > 0)
                    for (int i = 0; i < TotalPages; i++)
                    {
                        var lst = ListKhauHaoTaiSan.Skip(i * pageSize).Take(pageSize).ToList();
                        foreach (var item in lst)
                        {
                            if (string.IsNullOrEmpty(item.MA_TAI_SAN))
                                item.MA_TAI_SAN = item.MA_TAI_SAN_DB;
                            else
                                item.MA_TAI_SAN_DB = item.MA_TAI_SAN;
                            if (nguonId == (int)enumNguonTaiSan.DKTS40)
                            {
                                item.MA_TAI_SAN = item.MA_DKTS;
                            }
                            string jsonHaoMonTaiSan = item.toStringJson();

                            var result = _khauHaoTaiSanModelFactory.PrepareInSertKhauHaoDongBo(item, nguonId);

                            if (result.Code != MessageReturn.SUCCESS_CODE)
                            {
                                ListResultError.Add(new ResultDongBoModel()
                                {
                                    KhauHaoDB = item,
                                    Message = result.Message
                                });
                                ListKhauHaoTaiSanErr.Add(item);
                            }
                            else
                            {
                                ListResultSuccess.Add(new ResultDongBoModel()
                                {
                                    Message = result.Message,
                                    KhauHaoDB = item
                                });
                                ListKhauHaoTaiSanSuccess.Add(item);
                            }
                        }

                    }
                // import list thành công


                // update
                var listUpdateKhauHao = ListKhauHaoTaiSanSuccess.Select(c =>
                {
                    var khauHaoByYearAnhMonth = _khauHaoTaiSanService.GetListKhauHaoTaiSans(c.TAI_SAN_ID, c.NAM_KHAU_HAO, c.THANG_KHAU_HAO).FirstOrDefault();
                    if (khauHaoByYearAnhMonth != null)
                    {
                        _khauHaoTaiSanModelFactory.PrepareKhauHaoTaiSanFromHaoMonDB(c, khauHaoByYearAnhMonth);
                    }
                    return khauHaoByYearAnhMonth;
                }).Where(c => c != null);
                if (listUpdateKhauHao != null && listUpdateKhauHao.Count() > 0)
                {
                    _khauHaoTaiSanService.UpdateKhauHaoTaiSan(listUpdateKhauHao);
                }

                // create
                var listCreateKhauHao = ListKhauHaoTaiSanSuccess.Where(c => _khauHaoTaiSanService.GetListKhauHaoTaiSans(c.TAI_SAN_ID, c.NAM_KHAU_HAO, c.THANG_KHAU_HAO).FirstOrDefault() == null).Select(c =>
                {
                    var khauHao = new KhauHaoTaiSan();
                    _khauHaoTaiSanModelFactory.PrepareKhauHaoTaiSanFromHaoMonDB(c, khauHao);
                    return khauHao;
                });
                if (listCreateKhauHao != null && listCreateKhauHao.Count() > 0)
                {
                    _khauHaoTaiSanService.InsertKhauHaoTaiSan(listCreateKhauHao);
                }

            }
            if (ListResultError.Count > 0)
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);

                string fName = "";
                string fullpath = "";
                fName = string.Format("LOG_ERR_HAOMON_{0}_{1}({2}).json", nguonId == 1 ? "DKTS4.0" : "TSC", ListKhauHaoTaiSanErr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                fullpath = _fileProvider.Combine(_pathStore, fName);
                string json = "";
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                json = JsonConvert.SerializeObject(ListKhauHaoTaiSanErr, serializerSettings);

                _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                return Json(new
                {
                    success = false,
                    ListSuccess = ListResultSuccess,
                    ListError = ListResultError,
                    filePath = fullpath,
                    fileName = fName,
                    fileType = MimeTypes.ApplicationJson
                });
            }

            else
            {
                return Json(new
                {
                    success = true,
                    ListSuccess = ListResultSuccess,
                });
            }
        }

        public virtual IActionResult DongBoBienDongFromFile()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            model.ddlLoaiBienDong = enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO.ToSelectList().ToList();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DongBoBienDongFromFile(IFormFile file, decimal loaiBienDongId)
        {
            if (file == null)
            {
                return Json(ResponseApi.CreateErrorMessage("File không có dữ liệu"));
            }
            try
            {
                string dataString = ReadTextFromFile(file);
                var param = new PrameterDongBoTaiSanModel();
                param.FileContent = dataString;
                param.LoaiBienDongId = loaiBienDongId;
                var respond = await _dongBoFactory.DongBoBienDongTuFile(param);
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage("Có lỗi xảy ra"));
            }

        }
        public virtual IActionResult DongBoHaoMonFromFile()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DongBoHaoMonFromFile(IFormFile file)
        {
            if (file == null)
            {
                return Json(ResponseApi.CreateErrorMessage("File không có dữ liệu"));
            }
            try
            {
                string dataString = ReadTextFromFile(file);
                var param = new PrameterDongBoTaiSanModel();
                param.FileContent = dataString;
                var respond = await _dongBoFactory.DongBoHaoMonTuFile(param);
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage("Có lỗi xảy ra"));
            }

        }
        [HttpPost]
        public string ReadTextFromFile(IFormFile file)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file tài sản");
                return null;
            }
            var dataByte = _fileCongViecService.GetWorkFileBits(file);
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //  lưu file 
            var contentType = file.ContentType;
            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = null,
                LOAI_FILE = contentType,
                //we store filename without extension for downloads
                TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
                DUOI_FILE = fileExtension,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID
            };
            _fileCongViecService.InsertFileCongViec(fwork);
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
            var DataImport = GetWorkFileContentOnDisk(fwork);
            var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
            return _fileProvider.ReadAllText(path, Encoding.UTF8);
        }
        public virtual IActionResult ImportHaoMonDKTS()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();
            return View(model);
        }
        public virtual IActionResult ImportKhauHaoDKTS()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();

            return View(model);
        }

        #region Tài sản toàn dân

        public virtual IActionResult DongBoTSTDSangKho()
        {
            DongBoTaiSanModel model = new DongBoTaiSanModel();

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DongBoQDTTSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoQuyetDinhTichThuTSTD();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> DongBoTaiSanTSTDSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoTaiSanTSTD();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }
        [HttpPost]
        public virtual async Task<IActionResult> DongBoPAXLSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoPAXL();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }
        [HttpPost]
        public virtual async Task<IActionResult> DongBoTSXLSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoTSXL();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }

        public virtual async Task<IActionResult> DongBoKQSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoKetQua();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }
        public virtual async Task<IActionResult> DongBoThuChiSangKho()
        {
            try
            {

                var respond = await _dongBoFactory.DongBoThuChi();
                return Json(respond);
            }
            catch (Exception e)
            {

                return Json(ResponseApi.CreateErrorMessage($"Có lỗi xảy ra: {e.Message}"));
            }
        }
        #endregion
        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        // [RequestSizeLimit(209715200)]
        public async Task<IActionResult> DongBoDbTaiSanFromFile(IFormFile file)
        {

            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file tài sản");
                return JsonErrorMessage("Bạn chưa Nhập file tài sản");
            }
            string dataString = "";
            using (var fileStream = file.OpenReadStream())
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    dataString = await reader.ReadToEndAsync();
                }
            }
            try
            {
                ModelServiceApi model = dataString.toEntity<ModelServiceApi>();
                if (!model.Key.Equals("sp8cHiLPlFKEdukzi7BhwrC60FwgxkAk"))
                    return Json(new { Code = "01", Status = "Key not valid" });
                List<DBTaiSan> dbTaiSan = model.Value.toEntities<DBTaiSan>();
                _dBTaiSanService.InsertTaiSan(dbTaiSan);
                return Json("done");
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

    }

}
