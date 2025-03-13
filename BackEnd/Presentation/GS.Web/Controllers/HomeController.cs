using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.Security;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Authentication;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Logging;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security.Captcha;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.Common;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GS.Core.Domain.BaoCaos;
using GS.Core.Data;
using GS.Core.Domain.Api.DanhMuc;
using Newtonsoft.Json;
using DevExpress.Pdf.Native;
using GS.Services.DM;
using DevExpress.PivotGrid.Criteria;
using GS.Web.Factories.DongBo;
using GS.Services.DB;
using GS.Web.Models.DongBoTaiSan;
using GS.Core.Domain.DB;
using GS.Web.Factories.DanhMuc;
using GS.Services.TaiSans;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using Org.BouncyCastle.Asn1.X509.Qualified;
using DevExpress.DataProcessing;
using GS.Web.Models.BienDongs;
using GS.Core.Domain.Api.BaoCao;

namespace GS.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        #region fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CauHinhNguoiDung _nguoiDungCauHinh;
        private readonly CauHinhChung _cauHinhChung;
        private readonly XacThucChungThucSoCauHinh _cauHinhXacThuc;

        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWebHelper _webHelper;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IDonViService _DonViService;
        private readonly IWorkContext _workContext;
        private readonly IHoatDongService _hoatdongService;
        private readonly ILogger _logger;
        private readonly IQuyenService _quyenService;
        private readonly IBienDongService _bienDongService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGSAPIService _gSAPIService;
        private readonly GSConfig _gSConfig;
        private readonly IDbContext _dbContext;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IDuAnService _duAnService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiLyDoBienDongService _loailyDoBienDongService;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        private readonly IDB_QueueProcessHistoryService _dB_QueueProcessHistoryService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly ITaiSanService _taiSanService;
        #endregion fields

        #region ctor

        public HomeController(
            XacThucChungThucSoCauHinh cauHinhXacThuc,
            CauHinhChung cauHinhChung,
            CaptchaSettings captchaSettings,
             CauHinhNguoiDung customerSettings,
             INhanHienThiService NhanHienThiService,
             IWebHelper webHelper,
            INguoiDungService nguoiDungService,
            IAuthenticationService authenticationService,
            ICauHinhService cauHinhService,
            IDonViService DonViService,
            IHoatDongService hoatDongService,
            IWorkContext workContext,
            ILogger logger,
            IBienDongService bienDongService,
            IQuyenService quyenService,
            IHttpContextAccessor httpContextAccessor,
            IGSAPIService gSAPIService,
            GSConfig gSConfig,
            IDbContext dbContext,
            ILyDoBienDongService lyDoBienDongService,
            IQuocGiaService quocGiaService,
            ICheDoHaoMonService cheDoHaoMonService,
            IMucDichSuDungService mucDichSuDungService,
            IHienTrangService hienTrangService,
            IDuAnService duAnService,
            IGSFileProvider fileProvider,
            IReportModelFactory reportModelFactory,
            ILoaiTaiSanService loaiTaiSanService,
            INguoiDungDonViService nguoiDungDonViService,
            ILoaiDonViService loaiDonViService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IDiaBanService diaBanService,
            ILoaiLyDoBienDongService loailyDoBienDongService,
            IHinhThucXuLyService hinhThucXuLyService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IDongBoFactory dongBoFactory,
            IDB_QueueProcessService dB_QueueProcessService,
            IDB_QueueProcessHistoryService dB_QueueProcessHistoryService,
            IDonViModelFactory donViModelFactory,
            ITaiSanService taiSanService
            )
        {
            this._cauHinhXacThuc = cauHinhXacThuc;
            this._cauHinhChung = cauHinhChung;
            this._authenticationService = authenticationService;
            this._webHelper = webHelper;
            this._captchaSettings = captchaSettings;
            this._nguoiDungCauHinh = customerSettings;
            this._NhanHienThiService = NhanHienThiService;
            this._nguoiDungService = nguoiDungService;
            this._cauHinhService = cauHinhService;
            this._DonViService = DonViService;
            this._hoatdongService = hoatDongService;
            this._workContext = workContext;
            this._logger = logger;
            this._quyenService = quyenService;
            this._bienDongService = bienDongService;
            this._httpContextAccessor = httpContextAccessor;
            this._gSAPIService = gSAPIService;
            this._gSConfig = gSConfig;
            this._dbContext = dbContext;
            this._lyDoBienDongService = lyDoBienDongService;
            this._quocGiaService = quocGiaService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._hienTrangService = hienTrangService;
            this._duAnService = duAnService;
            this._fileProvider = fileProvider;
            this._reportModelFactory = reportModelFactory;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._loaiDonViService = loaiDonViService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._diaBanService = diaBanService;
            this._loailyDoBienDongService = loailyDoBienDongService;
            this._hinhThucXuLyService = hinhThucXuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._dongBoFactory = dongBoFactory;
            this._dB_QueueProcessService = dB_QueueProcessService;
            this._dB_QueueProcessHistoryService = dB_QueueProcessHistoryService;
            this._donViModelFactory = donViModelFactory;
            this._taiSanService = taiSanService;
        }

        #endregion ctor

        //#region   Utilities

        //[HttpsRequirement(SslRequirement.Yes)]

        public virtual IActionResult Index(string ReturnUrl)
        {
            //if (_gSConfig.IsUsingSSO)
            //{
            //	return RedirectToAction("IndexSSO");
            //}
            var currentUser = _workContext.CurrentCustomer;
            if (currentUser != null)
            {
                if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
                    return Redirect("/AppWork/Index");

                return Redirect(ReturnUrl);
            }
            return View();
        }
        public virtual IActionResult VideoHuongDan()
        {
            return View();
        }

        public virtual IActionResult IndexSSO(string ReturnUrl)
        {
            // nếu sử dụng single sign on
            if (_gSConfig.IsUsingSSO)
            {
                return View();
            }
            else return RedirectToAction("Index", new { ReturnUrl });
        }

        public virtual IActionResult CallBackSSO()
        {
            return View();
        }

        [HttpPost]
        [PublicAntiForgery]
        public virtual IActionResult CallBackSSO([FromBody] NguoiDungSSOCallback nguoiDungSSOCallback)
        {
            if (!string.IsNullOrEmpty(nguoiDungSSOCallback.access_token))
            {
                var customer = _nguoiDungService.GetNguoiDungByUsername(nguoiDungSSOCallback.profile.name);
                if (customer != null)
                {
                    var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                    //sign in new customer
                    _authenticationService.DangNhap(customer, false);
                    HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                    var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                    HttpContext.Session.SetObject(donViKey, DonVi);
                    //activity log
                    _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", new LoginModel { Username = customer.TEN_DANG_NHAP }, "Home");
                    return JsonSuccessMessage();
                }
            }
            return JsonErrorMessage();
        }
        [HttpPost]
        [PublicAntiForgery]
        public virtual IActionResult AuthSSO([FromBody] NguoiDungSSOCallback nguoiDungSSOCallback)
        {
            if (!string.IsNullOrEmpty(nguoiDungSSOCallback.access_token))
            {
                var customer = _nguoiDungService.GetNguoiDungByUsername(nguoiDungSSOCallback.profile.name);
                if (customer != null)
                {
                    var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                    //sign in new customer
                    _authenticationService.DangNhap(customer, false);
                    HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                    var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                    HttpContext.Session.SetObject(donViKey, DonVi);
                    //activity log
                    _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", new LoginModel { Username = customer.TEN_DANG_NHAP }, "Home");
                    return JsonSuccessMessage();
                }
            }
            return JsonErrorMessage();
        }

        [HttpPost]
        [PublicAntiForgery]
        public virtual IActionResult CallBackSSO2([FromBody] UserSSO userSSO)
        {
            if (userSSO != null && !string.IsNullOrEmpty(userSSO.name))
            {
                var customer = _nguoiDungService.GetNguoiDungByUsername(userSSO.name);
                if (customer != null)
                {
                    var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                    //sign in new customer
                    _authenticationService.DangNhap(customer, false);
                    HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                    var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                    HttpContext.Session.SetObject(donViKey, DonVi);
                    //activity log
                    _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", new LoginModel { Username = customer.TEN_DANG_NHAP }, "Home");
                    return JsonSuccessMessage();
                }
            }
            return JsonErrorMessage();
        }
        [HttpGet]
        [PublicAntiForgery]
        public virtual IActionResult AuthUser(string u)
        {
            if (!string.IsNullOrEmpty(u))
            {
                var customer = _nguoiDungService.GetNguoiDungByUsername(u);
                if (customer != null)
                {
                    var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                    //sign in new customer
                    _authenticationService.DangNhap(customer, false);
                    HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                    var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                    HttpContext.Session.SetObject(donViKey, DonVi);
                    //activity log
                    _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", new LoginModel { Username = customer.TEN_DANG_NHAP }, "Home");
                    return JsonSuccessMessage();
                }
            }
            return JsonErrorMessage();
        }

        public virtual IActionResult Test()
        {
            //var lstbd = _bienDongService.GetAllBienDongs().Where(c => c.ID != null);
            //foreach(var itm in lstbd)
            //{
            //	itm.ID_DB = itm.DB_GUID.ToString();
            //	_bienDongService.UpdateBienDong(itm);
            //}

            var e = new LoaiDonVi();
            e.ID = 10;

            var m = e.ToModel<LoaiDonViModel>();

            var model = new TestModel();
            //model.WorkFileIds = "78,79,96";
            //model.WorkDate = DateTime.Now.AddDays(-3);
            //model.WorkNumber = 10001.25m;
            //model.WorkCurrency = "10000000";
            //var i = _tuyendiabanService.GetAllTuyenDiaBans();
            //for(int i=0;i<100;i++)
            //{
            //    var ch1 = new CauHinh("abc_"+i.ToString(), "123:"+i.ToString());
            //    _cauHinhService.InsertCauHinh(ch1);
            //}
            //_cauHinhChung.SMTPServerIP = "mof.org.vn";
            //_cauHinhChung.SMTPPort = 389;
            //_cauHinhChung.IPTruyCapHeThong = DateTime.Now.ToString();
            //_cauHinhService.SaveCauHinh(_cauHinhChung);
            //model.KiemTraXacThuChuKySo = _cauHinhXacThuc.KiemTraXacThuChuKySo;
            //_nguoiDungService.KiemTraTrungMa("abc", 0);
            //test tốc độ
            //test đồng bộ tài sản cũ


            return View(model);
        }

        public async Task<IActionResult> GetTokenCSDLQG(ApiKhoCSDL apiKhoCSDL)
        {
            try
            {
                var Response = await _gSAPIService.GetObjectGSApi<ResponseApi>("Test/GetTokenCSDLQG", apiKhoCSDL, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(new
                {
                    success = true,
                    Data = Response.Data.ToString()
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    Error = ex.ToString()
                });
            }
        }
        public async Task<IActionResult> GetInfoApiKhoCSDL()
        {
            try
            {
                var Response = await _gSAPIService.GetObjectGSApi<MessageReturn>("Test/GetInfoApiKhoCSDL", null, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(new
                {
                    success = true,
                    Data = Response != null ? Response.ObjectInfo : null
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    Error = ex.ToString()
                });
            }
        }

        public IActionResult TestDongBoQuocGia(ApiKhoCSDL apiKhoCSDL)
        {
            try
            {
                QuocGiaModel quocGiaModel = new QuocGiaModel()
                {
                    MA = "TestDB",
                    TEN = "Test DB",
                    ID = 1000
                };
                _dongBoFactory.DongBoDanhMuc<QuocGiaModel>(new List<QuocGiaModel>() { quocGiaModel });
                //var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("Test/TestDongBoQuocGia2", new { QuocGiaModel = quocGiaModel, ApiKhoCSDL = apiKhoCSDL }, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                //// var Response = await _gSAPIService.GetObjectGSApi<OkObjectResult>("ConsumingDanhMuc/TestUpdate", null, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(new
                {
                    success = true,
                    //Data = result.ObjectInfo.ToString(),
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    Error = ex.ToString()
                });
            }
        }
        public async Task<IActionResult> TestDongBoQuocGiaAwait(ApiKhoCSDL apiKhoCSDL)
        {
            try
            {
                QuocGiaModel quocGiaModel = new QuocGiaModel()
                {
                    MA = "TestDB",
                    TEN = "Test DB",
                    ID = 1000
                };
                var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("Test/TestDongBoQuocGia2", new { QuocGiaModel = quocGiaModel, ApiKhoCSDL = apiKhoCSDL }, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                // var Response = await _gSAPIService.GetObjectGSApi<OkObjectResult>("ConsumingDanhMuc/TestUpdate", null, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(new
                {
                    success = true,
                    Data = result.ObjectInfo.ToString(),
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    Error = ex.ToString()
                });
            }
        }
        #region Đồng bộ danh mục
        public async Task<IActionResult> DongBoDanhMuc(String name, Decimal TreeLevel)
        {
            try
            {
                if (name == "QuocGia")
                {
                    var quocGias = _quocGiaService.GetAllQuocGiaChuaDB();
                    if (quocGias != null)
                    {
                        List<QuocGiaModel> quocGiaModels = quocGias.Select(c => c.ToModel<QuocGiaModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateQuocGia", quocGiaModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "CheDoHaoMon")
                {
                    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                    if (cheDoHaoMons != null)
                    {
                        List<CheDoHaoMonModel> cheDoHaoMonModels = cheDoHaoMons.Select(c => c.ToModel<CheDoHaoMonModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateCheDoHaoMon", cheDoHaoMonModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "MucDichSuDung")
                {
                    //var mucDichSuDungs = _mucDichSuDungService.GetMucDichSuDungChuaDb();
                    //if (mucDichSuDungs != null)
                    //{
                    //    List<MucDichSuDungModel> mucDichSuDungModels = mucDichSuDungs.Select(c => c.ToModel<MucDichSuDungModel>()).ToList();
                    //    var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", mucDichSuDungModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                    //    return Json(new
                    //    {
                    //        success = true,
                    //        Data = result.ObjectInfo,
                    //    });
                    //}
                    //else
                    //{
                    //    return Json(new
                    //    {
                    //        success = false,
                    //        Data = "Không có dữ liệu đồng bộ",
                    //    });
                    //}
                }
                else if (name == "HienTrang")
                {
                    var hienTrangs = _hienTrangService.GetHienTrangsChuaDb();
                    if (hienTrangs != null)
                    {
                        List<HienTrangModel> hienTrangModels = hienTrangs.Select(c => c.ToModel<HienTrangModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateHienTrang", hienTrangModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "LyDoBienDong")
                {
                    var LyDoBienDongs = _lyDoBienDongService.GetAllLyDoBienDongsChuaDb();
                    if (LyDoBienDongs != null)
                    {
                        List<LyDoBienDongModel> LyDoBienDongModels = LyDoBienDongs.Select(c => c.ToModel<LyDoBienDongModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateLyDoBienDong", LyDoBienDongModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "NguoiDung")
                {
                    var NguoiDungs = _nguoiDungService.GetAllNguoiDungsChuaDb();
                    if (NguoiDungs != null)
                    {
                        List<NguoiDungModel> NguoiDungModels = NguoiDungs.Select(c => c.ToModel<NguoiDungModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateNguoiDung", NguoiDungModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "DuAn")
                {
                    var DuAns = _duAnService.GetAllDuAnsChuaDb();
                    if (DuAns != null)
                    {
                        List<DuAnModel> DuAnModels = DuAns.Select(c => c.ToModel<DuAnModel>()).ToList();
                        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/UpdateDuAn", DuAnModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                        return Json(new
                        {
                            success = true,
                            Data = result.ObjectInfo,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu đồng bộ",
                        });
                    }
                }
                else if (name == "HinhThucXuLy")
                {
                    //var HinhThucXuLys = _HinhThucXuLyService.GetAllHinhThucXuLyChuaDb();
                    //if (HinhThucXuLys != null)
                    //{
                    //    List<HinhThucXuLyModel> HinhThucXuLyModels = HinhThucXuLys.Select(c => c.ToModel<HinhThucXuLyModel>()).ToList();
                    //    var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", HinhThucXuLyModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                    //    return Json(new
                    //    {
                    //        success = true,
                    //        Data = result.ObjectInfo,
                    //    });
                    //}
                    //else
                    //{
                    //    return Json(new
                    //    {
                    //        success = false,
                    //        Data = "Không có dữ liệu đồng bộ",
                    //    });
                    //}
                }
                else if (name == "PhuongAnXuLy")
                {
                    //var PhuongAnXuLys = _PhuongAnXuLyService.GetAllPhuongAnXuLyChuaDb();
                    //if (PhuongAnXuLys != null)
                    //{
                    //    List<PhuongAnXuLyModel> PhuongAnXuLyModels = PhuongAnXuLys.Select(c => c.ToModel<PhuongAnXuLyModel>()).ToList();
                    //    var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", PhuongAnXuLyModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                    //    return Json(new
                    //    {
                    //        success = true,
                    //        Data = result.ObjectInfo,
                    //    });
                    //}
                    //else
                    //{
                    //    return Json(new
                    //    {
                    //        success = false,
                    //        Data = "Không có dữ liệu đồng bộ",
                    //    });
                    //}
                }
                //else if (name == "CheDoHaoMon")
                //{
                //    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                //    if (cheDoHaoMons != null)
                //    {
                //        List<CheDoHaoMonModel> cheDoHaoMonModels = cheDoHaoMons.Select(c => c.ToModel<CheDoHaoMonModel>()).ToList();
                //        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", cheDoHaoMonModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
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
                //else if (name == "CheDoHaoMon")
                //{
                //    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                //    if (cheDoHaoMons != null)
                //    {
                //        List<CheDoHaoMonModel> cheDoHaoMonModels = cheDoHaoMons.Select(c => c.ToModel<CheDoHaoMonModel>()).ToList();
                //        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", cheDoHaoMonModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
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
                //else if (name == "CheDoHaoMon")
                //{
                //    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                //    if (cheDoHaoMons != null)
                //    {
                //        List<CheDoHaoMonModel> cheDoHaoMonModels = cheDoHaoMons.Select(c => c.ToModel<CheDoHaoMonModel>()).ToList();
                //        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", cheDoHaoMonModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
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
                //else if (name == "CheDoHaoMon")
                //{
                //    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                //    if (cheDoHaoMons != null)
                //    {
                //        List<CheDoHaoMonModel> cheDoHaoMonModels = cheDoHaoMons.Select(c => c.ToModel<CheDoHaoMonModel>()).ToList();
                //        var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("ConsumingDanhMuc/ConsumingDanhMucDongBoQuocGia2", cheDoHaoMonModels, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
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
                return null;

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    Error = ex.ToString()
                });
            }
        }
        //[HttpPost]
        public virtual IActionResult ExportExcelDongBoDanhMuc(String name, int type = 1)
        {
            try
            {
                int total = 0;
                MemoryStream stream = new MemoryStream();
                String json = "";
                bool addSTT = true;
                #region prepare
                if (name == "DonVi")
                {
                    var donVis = _DonViService.GetAllDonVisChuaDb()/*.Where(c => c.MA.StartsWith("T07"))*/;
                    if (donVis != null)
                    {
                        List<Kho_DonVi_Api> lst = new List<Kho_DonVi_Api>();
                        foreach (var item in donVis)
                        {
                            if (item == null)
                            {
                                lst = new List<Kho_DonVi_Api>();
                            }
                            var x = new Kho_DonVi_Api();
                            x.actionType = 1;
                            x.id = null;
                            if (item.PARENT_ID > 0)
                            {
                                DonVi donViCha = _DonViService.GetDonViById(item.PARENT_ID.Value);
                                x.parentId = donViCha.DB_ID;
                            }

                            x.name = item.TEN;
                            x.nationalBudgetCode = item.MA_DVQHNS;
                            x.code = item.MA;
                            x.unitLevelId = MapCapDonVi(item.CAP_DON_VI_ID, item.LOAI_CAP_DON_VI_ID ?? 0);
                            x.unitTypeId = item.LoaiDonVi != null ? (int?)item.LoaiDonVi.DB_ID : null;
                            x.address = item.DIA_CHI;
                            x.fax = item.FAX;
                            x.accountingStandard = (item.CHE_DO_HACH_TOAN_ID > 0) ? (int)item.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
                            x.isActive = item.TRANG_THAI_ID;
                            x.syncId = item.ID.ToString();
                            x.syncParentId = item.PARENT_ID > 0 ? item.PARENT_ID.ToString() : null;
                            x.phoneNumber = item.DIEN_THOAI;
                            x.administrativeLevel = (int)item.LOAI_CAP_DON_VI_ID.GetValueOrDefault(1);
                            x.functionalUnitType = item.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;

                            lst.Add(x);
                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_DonVi_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "LoaiDonVi")
                {
                    var loaiDonVis = _loaiDonViService.GetAllLoaiDonVisChuaDb();
                    if (loaiDonVis != null)
                    {
                        var lst = loaiDonVis.Select(c => new Kho_LoaiDonVi_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA != null ? c.MA : "LDV" + c.ID.ToString(),
                            syncId = c.ID.ToString(),
                            syncParentId = c.PARENT_ID > 0 ? c.PARENT_ID.ToString() : null,
                            parentId = null,
                            accountingStandard = c.CHE_DO_HOACH_TOAN_ID == null ? (long)enumCHE_DO_HACH_TOAN.HAO_MON : (long)c.CHE_DO_HOACH_TOAN_ID,
                            displayOrder = (long?)c.SAP_XEP,

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_LoaiDonVi_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "LoaiTaiSanToanDan")
                {
                    var loaiTaiSan = _loaiTaiSanService.GetAllLoaiTaiSanToanDansChuaDb();
                    if (loaiTaiSan != null)
                    {
                        var lst = loaiTaiSan.Select(c => new Kho_LoaiTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            parentId = null,
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                            code = c.MA,
                            treeId = _cheDoHaoMonService.GetCheDoHaoMonById(c.CHE_DO_HAO_MON_ID.Value).DB_ID,
                            syncTreeId = c.CHE_DO_HAO_MON_ID.ToString(),
                            syncParentId = c.PARENT_ID != null ? c.PARENT_ID.ToString() : null,
                            isActive = true,
                            amortizationPeriod = (long?)c.HM_THOI_HAN_SU_DUNG,
                            amortizationRate = (long?)c.HM_TY_LE,
                            calculationUnit = c.DON_VI_TINH,
                            assetTypeGroupId = 1

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_LoaiTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "LoaiTaiSan")
                {
                    var loaiTaiSan = _loaiTaiSanService.GetAllLoaiTaiSansChuaDb();
                    if (loaiTaiSan != null)
                    {
                        var lst = loaiTaiSan.Select(c => new Kho_LoaiTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            parentId = null,
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                            code = c.MA,
                            treeId = _cheDoHaoMonService.GetCheDoHaoMonById(c.CHE_DO_HAO_MON_ID.Value).DB_ID,
                            syncTreeId = c.CHE_DO_HAO_MON_ID.ToString(),
                            syncParentId = c.PARENT_ID != null ? c.PARENT_ID.ToString() : null,
                            isActive = true,
                            amortizationPeriod = (long?)c.HM_THOI_HAN_SU_DUNG,
                            amortizationRate = (long?)c.HM_TY_LE,
                            calculationUnit = c.DON_VI_TINH,
                            assetTypeGroupId = 1

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_LoaiTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "NguonGocTaiSan")
                {
                    var nguonGocTaiSans = _nguonGocTaiSanService.GetAllNguonGocTaiSansChuaDb();
                    if (nguonGocTaiSans != null)
                    {
                        var lst = nguonGocTaiSans.Select(c => new Kho_NguonGocTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                            syncParentId = c.PARENT_ID > 0 ? c.PARENT_ID.ToString() : null,
                            parentId = null

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_NguonGocTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "QuocGia")
                {
                    var quocGias = _quocGiaService.GetAllQuocGiaChuaDB();
                    if (quocGias != null)
                    {
                        var lst = quocGias.Select(c => new Kho_QuocGia_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.ID.ToString(),
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_QuocGia_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "DiaBan")
                {
                    var diaBans = _diaBanService.GetDiaBansChuaDb();
                    if (diaBans != null)
                    {
                        List<Kho_DiaBan_Api> lst = new List<Kho_DiaBan_Api>();

                        foreach (var m in diaBans)
                        {
                            Kho_DiaBan_Api kho_DiaBan = new Kho_DiaBan_Api();
                            kho_DiaBan.id = null;
                            kho_DiaBan.actionType = 1;
                            kho_DiaBan.name = m.TEN;
                            kho_DiaBan.code = m.MA;
                            kho_DiaBan.isActive = m.TRANG_THAI_ID == 1 ? true : false;
                            kho_DiaBan.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                            kho_DiaBan.districtId = null;
                            kho_DiaBan.provinceId = null;
                            kho_DiaBan.syncId = m.ID.ToString();
                            lst.Add(kho_DiaBan);
                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_DiaBan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "CheDoHaoMon")
                {
                    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                    if (cheDoHaoMons != null)
                    {
                        var lst = cheDoHaoMons.Select(c => new Kho_CheDoHaoMon_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN_CHE_DO,
                            code = c.MA_CHE_DO,
                            syncId = c.ID.ToString(),
                            startDate = CommonHelper.toDateKhoString(c.TU_NGAY),
                            endDate = CommonHelper.toDateKhoString(c.DEN_NGAY),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_CheDoHaoMon_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "MucDichSuDung")
                {
                    //var mucDichSuDungs = _mucDichSuDungService.GetMucDichSuDungChuaDb();
                    //if (mucDichSuDungs != null)
                    //{
                    //    var lst = mucDichSuDungs.Select(c => new Kho_QuocGia_Api()
                    //    {

                    //    }).ToList();
                    //    if (type == 1)
                    //        stream = _reportModelFactory.prepareExcelEntity<Kho_QuocGia_Api>(stream, lst, name);
                    //    else
                    //        json = lst.toStringJson();
                    //}
                    //else
                    //{
                    //    return Json(new
                    //    {
                    //        success = false,
                    //        Data = "Không có dữ liệu",
                    //    });
                    //}
                }
                else if (name == "HienTrang")
                {
                    var hienTrangs = _hienTrangService.GetHienTrangsChuaDb();
                    if (hienTrangs != null)
                    {
                        var lst = hienTrangs.Select(c => new Kho_HienTrang_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN_HIEN_TRANG,
                            code = c.ID.ToString(),
                            syncId = c.ID.ToString(),
                            assetTypeIds = new List<long>() { (long)CommonHelper.toLoaiHinhTaiSanKho(c.LOAI_HINH_TAI_SAN_ID) },
                            isAreaUsage = (c.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT
                                            || c.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? true : false
                        }).ToList();
                        total = lst.Count();

                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_HienTrang_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "LyDoBienDong")
                {
                    var LyDoBienDongs = _lyDoBienDongService.GetAllLyDoBienDongsChuaDb();
                    if (LyDoBienDongs != null)
                    {
                        List<Kho_LyDoBienDong_Api> lst = new List<Kho_LyDoBienDong_Api>();
                        foreach (var c in LyDoBienDongs)
                        {
                            var item = new Kho_LyDoBienDong_Api()
                            {
                                id = null,
                                actionType = 1,
                                name = c.TEN,
                                code = c.MA,
                                causeTypeId = (long?)c.LOAI_LY_DO_ID,
                                //causeTypeId = null,
                                syncId = c.ID.ToString(),
                                displayOrder = (long?)c.TRUONG_SAP_XEP,
                            };
                            if (!string.IsNullOrEmpty(c.LOAI_HINH_TAI_SAN_AP_DUNG_ID))
                            {
                                var x = c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.toEntities<decimal>();
                                if (x != null)
                                {
                                    item.assetTypeIds = x.Select(m => (long)CommonHelper.toLoaiHinhTaiSanKho(m)).ToList();
                                }
                            }
                            else
                                item.assetTypeIds = new List<long>() { (long)enumIdNhomTaiSanKho.CoQuanToChuc };
                            lst.Add(item);

                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_LyDoBienDong_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "LoaiLyDoBienDong")
                {
                    var loailyDoBienDongs = _loailyDoBienDongService.GetAllLoaiLyDoBienDongsChuaDb();
                    if (loailyDoBienDongs != null)
                    {
                        var lst = loailyDoBienDongs.Select(c => new Kho_LoaiLyDoBienDong_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                        }).ToList();
                        total = lst.Count();

                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_LoaiLyDoBienDong_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "NguoiDung")
                {
                    var NguoiDungs = _nguoiDungService.GetAllNguoiDungsChuaDb();
                    if (NguoiDungs != null)
                    {
                        var lst = NguoiDungs.Select(c => new Kho_NguoiDung_Api()
                        {
                            id = null,
                            actionType = 1,
                            username = c.TEN_DANG_NHAP,
                            email = c.EMAIL,
                            //phoneNumber = c.MOBILE,
                            //passwordHash = c.MAT_KHAU,
                            passwordHash = "null",
                            fullName = c.TEN_DAY_DU,
                            lockoutEnabled = c.TRANG_THAI_ID == 1 ? true : false,
                            isAdministrator = c.IS_QUAN_TRI,
                            unitId = GetUnitForNguoiDung(c.ID),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_NguoiDung_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "DuAn")
                {
                    var DuAns = _duAnService.GetAllDuAnsChuaDb();
                    if (DuAns != null)
                    {
                        var lst = DuAns.Select(c => new Kho_DuAn_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                            unitId = c.donVi.DB_ID,
                            decisionNumber = c.SO_QUYET_DINH_PHE_DUYET,
                            expense = (long?)c.TONG_KINH_PHI,
                            expenseStateBudget = (long?)c.NGUON_NS,
                            expenseOda = (long?)c.NGUON_ODA,
                            expenseAid = (long?)c.NGUON_VIEN_TRO,
                            expenseOther = (long?)c.NGUON_KHAC,
                            startDate = CommonHelper.toDateKhoString(c.NGAY_BAT_DAU),
                            endDate = CommonHelper.toDateKhoString(c.NGAY_KET_THUC),
                            investor = c.CHU_DAU_TU,
                            location = c.DIA_DIEM,
                            syncDate = CommonHelper.toDateKhoString(DateTime.Now),
                            description = c.GHI_CHU,
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_DuAn_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "HinhThucXuLy")
                {
                    var hinhThucXuLys = _hinhThucXuLyService.GetAllHinhThucXuLysChuaDb();
                    if (hinhThucXuLys != null)
                    {

                        var lst = hinhThucXuLys.Select(c => new Kho_HinhThucXuLy_Api()
                        {
                            id = null,
                            actionType = 1,
                            syncId = (long)c.ID,
                            code = c.MA,
                            displayOrder = (long?)c.SAP_XEP,
                            name = c.TEN

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_HinhThucXuLy_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                else if (name == "PhuongAnXuLy")
                {
                    var phuongAnXuLys = _phuongAnXuLyService.GetAllPhuongAnXuLysChuaDb();
                    if (phuongAnXuLys != null)
                    {
                        var lst = phuongAnXuLys.Select(c => new Kho_PhuongAnXuLy_Api()
                        {
                            id = null,
                            actionType = 1,
                            syncId = (long)c.ID,
                            code = c.MA,
                            displayOrder = (long?)c.SAP_XEP,
                            name = c.TEN

                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = _reportModelFactory.prepareExcelEntity<Kho_PhuongAnXuLy_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            Data = "Không có dữ liệu",
                        });
                    }
                }
                #endregion
                byte[] bytes;
                string fName = $"{name}_{total}({DateTime.Now.ToString("ddMMyyyyhhmm")})";
                if (type == 1)
                {
                    bytes = stream.ToArray();
                    return new FileContentResult(bytes, MimeTypes.TextXlsx)
                    {
                        FileDownloadName = fName + ".xlsx"
                    };
                }
                else
                {
                    return new FileContentResult(Encoding.UTF8.GetBytes(json), MimeTypes.ApplicationJson)
                    {
                        FileDownloadName = fName + ".json"
                    };
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    Error = ex.ToString()
                });
            }
        }
        public int GetUnitForNguoiDung(decimal nguoiDungId)
        {
            var nguoiDungDonVis = _nguoiDungDonViService.GetMapByNguoiDungId(nguoiDungId);
            if (nguoiDungDonVis != null && nguoiDungDonVis.Count > 0)
                return (int)nguoiDungDonVis.FirstOrDefault().donvi.DB_ID;
            return 1;
        }
        public int MapCapDonVi(decimal? CapDonViQLTS, decimal LoaiCapDonVi)
        {
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapTrungUong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.BoCoQuanTrungUong:
                        return 1;
                    //case (int)CapEnum.TongCuc:
                    //    return 2;
                    //case (int)CapEnum.Cuc:
                    //    return 3;
                    //case (int)CapEnum.ChiCuc:
                    //    return 4;
                    default:
                        return 1;
                }
            }
            if (LoaiCapDonVi == (decimal)EnumLoaiCapDonVi.CapDiaPhuong)
            {
                switch (CapDonViQLTS)
                {
                    case (int)CapEnum.Tinh:
                        return 1;
                    case (int)CapEnum.Huyen:
                        return 2;
                    case (int)CapEnum.Xa:
                        return 3;
                    default:
                        return 3;
                }
            }
            return 0;
        }
        public virtual IActionResult ExportByUrl(string filePath, string fileName, string fileType)
        {
            byte[] bytes;
            bytes = _fileProvider.ReadAllBytes(filePath);
            _fileProvider.DeleteFile(filePath);
            return File(bytes, fileType, fileName);
        }

        public async Task<IActionResult> SendRequestDongBo()
        {
            var queue = _dB_QueueProcessService.GetDB_QueueProcessByIdNeedToSendRequest();
            if (queue == null)
                return Json(new
                {
                    success = false,
                    Data = "Không có dữ liệu đồng bộ",
                });
            queue.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.DA_GUI_REQUEST;
            queue.LAST_SEND_REQUEST = DateTime.Now;
            _dB_QueueProcessService.UpdateDB_QueueProcess(queue);

            var history = new DB_QueueProcessHistory()
            {
                QUEUE_PROCESS_ID = queue.ID,
                TIME_REQUEST = DateTime.Now,
            };
            _dB_QueueProcessHistoryService.InsertDB_QueueProcessHistory(history);
            var result = await _gSAPIService.PostObjectGSApi<ResponseApi>(queue.URL_CALL, queue.DATA_INPUT, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
            if (result.Status)
            {
                history.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.GUI_REQUEST_THANH_CONG;
                history.GUID_RESPONSE = result.Data;
                history.RESPONSE = result.toStringJson();
                _dB_QueueProcessHistoryService.UpdateDB_QueueProcessHistory(history);
                queue.GUID_RESPONSE = result.Data;
                queue.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.GUI_REQUEST_THANH_CONG;
                _dB_QueueProcessService.UpdateDB_QueueProcess(queue);
                return Json(new
                {
                    success = true,
                    Data = result.Data,
                });
            }
            else
            {
                history.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                history.GUID_RESPONSE = result.Data;
                history.RESPONSE = result.toStringJson();
                _dB_QueueProcessHistoryService.UpdateDB_QueueProcessHistory(history);
                queue.TRANG_THAI_ID = (int)enumTrangThaiQueueProcessDB.GUI_REQUEST_THAT_BAI;
                _dB_QueueProcessService.UpdateDB_QueueProcess(queue);
                return Json(new
                {
                    success = false,
                    Data = "Dữ liệu lỗi",
                });
            }
        }
        #endregion
        #region Test đồng bộ báo cáo
        public async Task<IActionResult> GetReportByGuid(string fileGuid)
        {
            try
            {
                var result = await _gSAPIService.PostObjectGSApi<MessageReturn>("BaoCaoSvc/GetBaoCao", new { fileGuid = fileGuid }, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                if (result != null && result.Code == "00" && result.ObjectInfo != null)
                {
                    var objstr = JsonConvert.SerializeObject(result.ObjectInfo);
                    var objobj = JsonConvert.DeserializeObject<ReturnBaoCaoSvc>(objstr);

                    return new FileContentResult(objobj.BinaryValue, objobj.LOAI_FILE)
                    {
                        FileDownloadName = objobj.MA_BAO_CAO + objobj.DUOI_FILE
                    };

                }
                ErrorNotification(result.Message);
                return RedirectToAction("Test");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.ToString());
                return RedirectToAction("Test");
            }
        }
        #endregion

        [HttpPost]
        public virtual IActionResult Test(TestModel model)
        {
            //var inti = 0;
            _cauHinhXacThuc.KiemTraXacThuChuKySo = model.KiemTraXacThuChuKySo;
            _cauHinhService.SaveCauHinh(_cauHinhXacThuc);

            var _i = model.WorkCurrency.ToNumber();
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult TestReport()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult TestReportDev()
        {
            return View(new DevExpress.XtraReports.UI.XtraReport());
        }

        public virtual IActionResult _TestPopUp(int Id)
        {
            var model = new TestModel();
            model.TestId = string.Format("Hello {0}", Id);
            return PartialView(model);
        }

        [HttpPost]
        //[ValidateCaptcha]
        //available even when a store is closed
        //[CheckAccessClosedStore(true)]
        //available even when navigation is not allowed
        //[CheckAccessPublicStore(true)]
        [PublicAntiForgery]
        public virtual IActionResult Index(LoginModel model, string ReturnUrl, bool captchaValid)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_NhanHienThiService));
            }

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(model.Username))
                {
                    model.Username = model.Username.Trim();
                    //model.Username = model.Username.ToLower();
                }
                var loginResult = _nguoiDungService.ValidateNguoiDung(model.Username, model.Password);
                switch (loginResult)
                {
                    case NguoiDungKetQuaDangNhap.Successful:
                        {
                            var customer = _nguoiDungService.GetNguoiDungByUsername(model.Username);
                            //var toaanID = _nguoiDungToaAnService.GetByNguoiDungId(customer.ID).FirstOrDefault().DON_VI_ID;
                            //var toaan = _toaAnService.GetToaAnById(toaanID);
                            var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                            //sign in new customer
                            _authenticationService.DangNhap(customer, model.RememberMe);
                            HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                            //
                            //CookieOptions option = new CookieOptions();
                            //option.Expires = DateTime.Now.AddMinutes(120);
                            ////Add cookie customer
                            //Response.Cookies.Append("CurrentCustomer", customer.ToModelCache<NguoiDungCache>().toStringJson(), option);
                            //
                            var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                            HttpContext.Session.SetObject(donViKey, DonVi);
                            //Add cookie CurrentDonVi
                            //Response.Cookies.Append(donViKey, customer.ToModelCache<NguoiDungCache>().toStringJson(), option);
                            //
                            //HttpContext.Session.SetObject("CurrentToaAn", toaan);

                            //raise event
                            //_eventPublisher.Publish(new CustomerLoggedinEvent(customer));

                            //Clean Password firt activity log
                            model.Password = String.Empty;
                            //activity log
                            _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", model, "Home");

                            //_nhanHienThiService.GetGiaTri("ActivityLog.PublicStore.Login"), customer);
                            //tao notification
                            //var notifyItem = Notification.CreateSimple(customer.Id
                            //    , _nhanHienThiService.GetGiaTri("Account.Login.Notification.Subject")
                            //    , string.Format(_nhanHienThiService.GetGiaTri("Account.Login.Notification.Body"), DateTime.Now.toDateVNString(true), _webHelper.GetCurrentIpAddress())
                            //    );
                            //_notificationService.InsertNotification(notifyItem);

                            if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
                                return Redirect("/AppWork/Index");

                            return Redirect(ReturnUrl);
                        }
                    case NguoiDungKetQuaDangNhap.CustomerNotExist:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        model.messageResult = "Tài khoản không tồn tại";
                        break;

                    case NguoiDungKetQuaDangNhap.LockedOut:
                        ModelState.AddModelError("", "Tài khoản bị khóa");
                        model.messageResult = "Tài khoản bị khóa";
                        break;

                    case NguoiDungKetQuaDangNhap.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Mật khẩu sai");
                        model.messageResult = "Mật khẩu sai";
                        break;
                }
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult TestArray(TestArrayModel[] models)
        {
            //var inti = 0;
            foreach (var m in models)
            {
            }
            return this.Json(models.Length);
        }

        public virtual IActionResult AccessDenied(string pageUrl)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null)
            {
                _logger.Information($"Access denied to anonymous request on {pageUrl}");
                return View();
            }

            _logger.Information($"Access denied to user #{currentCustomer.EMAIL} '{currentCustomer.EMAIL}' on {pageUrl}");

            return View();
        }
        public virtual IActionResult TestMapJson()
        {
            Type _type = typeof(enumTENCAUHINH);
            var _field = _type?.GetField("DON_VI_IS_AUTO_DUYET1");
            string value = _field?.GetValue(null).ToString();
            string jsonReportData = "";

            //Assembly.Get
            Assembly assemblie = null;
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*GS.Core.dll"))
            {
                if (path.Contains("GS.Core.dll"))
                {
                    assemblie = Assembly.LoadFrom(path);
                    break;
                }

            }
            Type _typeReportData = Type.GetType("GS.Core.Domain.BaoCaos.TS_BCTH.TS_BCTH_08A_DK_TSC," + assemblie.FullName);
            List<TS_BCTH_08A_DK_TSC> baoCaos = new List<TS_BCTH_08A_DK_TSC>();
            baoCaos.Add(new TS_BCTH_08A_DK_TSC()
            {
                LOAI_TAI_SAN_ID = 1,
                SO_LUONG = 100,
                NGAY_SU_DUNG = DateTime.Now
            });
            baoCaos.Add(new TS_BCTH_08A_DK_TSC()
            {
                LOAI_TAI_SAN_ID = 2,
                SO_LUONG = 200,
                NGAY_SU_DUNG = DateTime.Now
            });

            String jsonData = baoCaos.toStringJson();

            String jsonSetting = "{\"TenBaoCao\": \"BÁO CÁO TÀI SẢN NHÀ NƯỚC CỦA ĐƠN VỊ TRỰC TIẾP SỬ DỤNG\",\"MaBaoCao\": \"TS_BCTH_08A_DK_TSC_P1\",\"MaBaoCaoKho\": \"RPT_08A_DK_TSC_01\",\"Model\": \"GS.Core.Domain.BaoCaos.TS_BCTH.TS_BCTH_08A_DK_TSC\",\"Body\": {\"NgayKetThuc\": \"reportEndDate\",\"MaBaoCao\": \"reportTypeCode\"},\"reportData\": {\"LOAI_TAI_SAN_ID\": \"assetTypeId\",\"SO_LUONG\": \"quantity\",\"DIEN_TICH\": \"area\",\"QUAN_LY_NHA_NUOC\": \"stateManagementUsage\",\"KHONG_KINH_DOANH\": \"noneBusinessUsage\",\"KINH_DOANH\": \"businessUsage\",\"CHO_THUE\": \"leaseUsage\",\"LIEN_DOANH\": \"ventureUsage\",\"SU_DUNG_HON_HOP\": \"mixUsage\",\"KHAC\": \"otherUsage\",\"NGAY_SU_DUNG\": \"dateUse\"}}";
            CauHinhMapBaoCao cauHinhMapBaoCao = jsonSetting.toEntity<CauHinhMapBaoCao>();
            BaoCaoTaiSanDongBoSearch modelSearch = new BaoCaoTaiSanDongBoSearch();
            modelSearch.MaBaoCao = cauHinhMapBaoCao.MaBaoCaoKho;
            modelSearch.NgayKetThuc = DateTime.Now;


            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Formatting = Formatting.None;
            serializerSettings.DateFormatString = "dd-MM-yyyy";
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;

            var jsonResolverReportData = new PropertyRenameAndIgnoreSerializerContractResolver();
            var jsonResolverBodyData = new PropertyRenameAndIgnoreSerializerContractResolver();

            Type _typeBodyData = typeof(BaoCaoTaiSanDongBoSearch);

            #region Body
            if (cauHinhMapBaoCao.Body != null)
            {
                Dictionary<string, string> pros = JsonConvert.DeserializeObject<Dictionary<string, string>>(cauHinhMapBaoCao.Body.toStringJson());
                List<string> Listprosetting = pros.Select(c => c.Key).ToList();
                List<string> ListPro = _typeBodyData.GetProperties().Select(c => c.Name).ToList();
                string[] ignores = ListPro.Where(c => !Listprosetting.Contains(c)).ToArray();
                jsonResolverBodyData.IgnoreProperty(_typeBodyData, ignores);
                foreach (var pro in pros)
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
            if (ignoresProTypeReportDataSetting.Count() > 0)
                jsonResolverReportData.IgnoreProperty(_typeReportData, ignoresProTypeReportDataSetting);
            foreach (var item in reportDataSetting)
            {
                jsonResolverReportData.RenameProperty(_typeReportData, item.Key, item.Value);
            }
            serializerSettings.ContractResolver = jsonResolverReportData;
            if (cauHinhMapBaoCao.Model.ToString() == _typeReportData.FullName)
            {
                var reportData = jsonData.toEntities<TS_BCTH_08A_DK_TSC>();
                jsonReportData = JsonConvert.SerializeObject(reportData, serializerSettings);
            }

            #endregion Report Data


            List<JObject> jsonObjectReportData = JsonConvert.DeserializeObject<List<JObject>>(jsonReportData);
            JObject jsonObjectReport = JsonConvert.DeserializeObject<JObject>(jsonBodyData);
            jsonObjectReport["reportData"] = JToken.FromObject(jsonObjectReportData);

            return Json(jsonObjectReport);
        }
        public virtual IActionResult TestBienDong(decimal id)
        {
            var rs = _bienDongService.GET_INFO_BIEN_DONG_BY_ID(id);

            return Json(rs);
            //DateTime d = "31/12/2020".toDateSys("dd/mm/yyyy").Value;
            //DateTime d2 = "01/01/2020".toDateSys("dd/mm/yyyy").Value;
            //List<BienDongModel> lstBienDong = new List<BienDongModel>(){
            //    new BienDongModel(){NGAY_BIEN_DONG = d,ID = 1},
            //    new BienDongModel(){NGAY_BIEN_DONG = d.AddDays(-1),ID = 2},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2.AddDays(+2),ID = 3},
            //    new BienDongModel(){NGAY_BIEN_DONG = d.AddDays(-2),ID = 4},
            //    new BienDongModel(){NGAY_BIEN_DONG = d,ID = 5},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2.AddDays(+3),ID = 6},
            //    new BienDongModel(){NGAY_BIEN_DONG = d.AddDays(-2),ID = 7},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2,ID = 8},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2.AddDays(+1),ID = 9},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2,ID = 10},
            //    new BienDongModel(){NGAY_BIEN_DONG = d2.AddDays(+1),ID = 11},
            //    new BienDongModel(){NGAY_BIEN_DONG = d,ID = 12}
            //};

            //var lstexists = lstBienDong.GroupBy(x => x.NGAY_BIEN_DONG.toDateVNString()).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            //int checkExists = lstexists.Count();
            ////foreach(var dateStr in lstexists)
            ////{
            ////    foreach(var itm in )
            ////}    
            //return Json(lstBienDong);
        }
        [HttpPost]
        public virtual IActionResult ExcuteSql(String querySql)
        {
            if (String.IsNullOrEmpty(querySql))
                return Json("");
            var rs = _dbContext.ExecuteScalarSql(querySql);
            return Json(rs);
        }

        public virtual IActionResult GetTrue()
        {
            return Json(true);
        }

        public virtual IActionResult GetTLHD()
        {
            var TLHD = _cauHinhService.GetCauHinh("cauhinh.tailieuhuongdan");
            if (TLHD == null)
                return Json("");
            return Json(TLHD.GIA_TRI);
        }
        public virtual IActionResult GetVideoHuongDan()
        {
            var VideoJson = _cauHinhService.GetCauHinh("videohuongdan");
            return Json(VideoJson.GIA_TRI);
        }
        public IActionResult DongBoTaiSan()
        {
            return View(new TestModel());
        }
        [HttpPost]
        public virtual async Task<IActionResult> DongBoTaiSan(string jsonData)
        {
            try
            {
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var rs = await _gSAPIService.PostObjectGSApiWithStringContent<ResponseApi>(enumSendRequest.DongBoBienDongThuCong, content, _gSAPIService.GetToken(true), _gSConfig.ApiUrlWebApi);
                return Json(new
                {
                    success = true,
                    Data = rs.Data.ToString()
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    Error = ex.ToString()
                });
            }
        }
    }
}