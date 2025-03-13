//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Controllers;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.HeThong;
using GS.Web.Models.HeThong;
using GS.Services.DanhMuc;
using System.Linq;
using GS.Core.Domain.DanhMuc;
using iTextSharp.text;
using System.Collections.Generic;
using GS.Services.TaiSans;
using GS.Web.Factories.TaiSans;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using GS.Data;
using GS.Core.Domain.BaoCaos.Widget;
using System.Text;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class WidgetController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IWidgetModelFactory _itemModelFactory;
        private readonly IWidgetService _itemService;
        private readonly IDbContext _dbContext;
        private readonly IVaiTroWidgetService _vaiTroWidgetService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        #endregion

        #region Ctor
        public WidgetController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IWidgetModelFactory itemModelFactory,
            IWidgetService itemService,
            ILoaiTaiSanService loaiTaiSanService,
            ITaiSanService taiSanService,
            IDbContext dbContext,
            IVaiTroWidgetService vaiTroWidgetService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._taiSanService = taiSanService;
            this._dbContext = dbContext;
            this._vaiTroWidgetService = vaiTroWidgetService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new WidgetSearchModel();
            var model = _itemModelFactory.PrepareWidgetSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(WidgetSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareWidgetListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareWidgetModel(new WidgetModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(WidgetModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<Widget>();
                _itemService.InsertWidget(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<WidgetModel>(), "Widget");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareWidgetModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();

            var item = _itemService.GetWidgetById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareWidgetModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(WidgetModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetWidgetById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareWidget(model, item);
                _itemService.UpdateWidget(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<WidgetModel>(), "Widget");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareWidgetModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetWidgetById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteWidget(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<WidgetModel>(), "Widget");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult SoLuongTaiSanTheoLoaiHinh()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;
            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_SoLuongTaiSanTheoLoaiHinh"))
            {
                return Json("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var ts = _dbContext.EntityFromStore<SoLuongTaiSanTheoLoaiHinh>("PK_DASHBOARD.SP_SO_TAISAN_THEO_LOAI_HINH", pDonVi, pOut);
                var data = new List<object>();
                var results = ts.ToList();
                var dat = new
                {
                    loaitaisan = "Đất",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.DAT
                };
                var nha = new
                {
                    loaitaisan = "Nhà",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.NHA
                };
                var oto = new
                {
                    loaitaisan = "Ô tô",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.OTO
                };
                var vatkientruc = new
                {
                    loaitaisan = "Vật kiến trúc",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
                };
                var phuongtienkhac = new
                {
                    loaitaisan = "Phương tiện khác",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                };
                var maymocthietbi = new
                {
                    loaitaisan = "Máy móc thiết bị",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI
                };
                var caylaunam = new
                {
                    loaitaisan = "Cây lâu năm",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
                };
                var huuhinhkhac = new
                {
                    loaitaisan = "Hữu hình khác",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC
                };
                var vohinh = new
                {
                    loaitaisan = "Vô hình",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.VO_HINH
                };
                var dacthu = new
                {
                    loaitaisan = "Đặc thù",
                    soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault().SOLUONG.ToNumberInt32(),
                    LoaiHinhTaiSanId = (int)(int)enumLOAI_HINH_TAI_SAN.DAC_THU
                };
                data.Add(dat);
                data.Add(nha);
                data.Add(vatkientruc);
                data.Add(oto);
                data.Add(phuongtienkhac);
                data.Add(caylaunam);
                data.Add(huuhinhkhac);
                data.Add(vohinh);
                data.Add(dacthu);
                data.Add(maymocthietbi);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IActionResult SoLuongLoaiTaiSanDuoi500()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;
            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_SoLuongLoaiTaiSanDuoi500"))
            {
                return Json("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var ts = _dbContext.EntityFromStore<SoLuongTaiSanTheoLoaiHinh>("PK_DASHBOARD.SP_SO_TAISAN_DUOI_500_THEO_LOAI_HINH", pDonVi, pOut);
                var data = new List<object>();
                var results = ts.ToList();
                var dat = new { loaitaisan = "Đất", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var nha = new { loaitaisan = "Nhà", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var oto = new { loaitaisan = "Ô tô", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var vatkientruc = new { loaitaisan = "Vật kiến trúc", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var phuongtienkhac = new { loaitaisan = "Phương tiện khác", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var maymocthietbi = new { loaitaisan = "Máy móc thiết bị", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var caylaunam = new { loaitaisan = "Cây lâu năm", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var huuhinhkhac = new { loaitaisan = "Hữu hình khác", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var vohinh = new { loaitaisan = "Vô hình", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var dacthu = new { loaitaisan = "Đặc thù", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault().SOLUONG.ToNumberInt32() };
                data.Add(dat);
                data.Add(nha);
                data.Add(vatkientruc);
                data.Add(oto);
                data.Add(phuongtienkhac);
                data.Add(caylaunam);
                data.Add(huuhinhkhac);
                data.Add(vohinh);
                data.Add(dacthu);
                data.Add(maymocthietbi);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IActionResult SoLuongLoaiTaiSanTren500()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;

            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_SoLuongLoaiTaiSanTren500"))
            {
                return Json("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var ts = _dbContext.EntityFromStore<SoLuongTaiSanTheoLoaiHinh>("PK_DASHBOARD.SP_SO_TAISAN_TREN_500_THEO_LOAI_HINH", pDonVi, pOut);
                var data = new List<object>();
                var results = ts.ToList();
                var dat = new { loaitaisan = "Đất", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var nha = new { loaitaisan = "Nhà", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var oto = new { loaitaisan = "Ô tô", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var vatkientruc = new { loaitaisan = "Vật kiến trúc", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var phuongtienkhac = new { loaitaisan = "Phương tiện khác", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var maymocthietbi = new { loaitaisan = "Máy móc thiết bị", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var caylaunam = new { loaitaisan = "Cây lâu năm", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var huuhinhkhac = new { loaitaisan = "Hữu hình khác", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var vohinh = new { loaitaisan = "Vô hình", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault().SOLUONG.ToNumberInt32() };
                var dacthu = new { loaitaisan = "Đặc thù", soluong = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault().SOLUONG.ToNumberInt32() };
                data.Add(dat);
                data.Add(nha);
                data.Add(vatkientruc);
                data.Add(oto);
                data.Add(phuongtienkhac);
                data.Add(caylaunam);
                data.Add(huuhinhkhac);
                data.Add(vohinh);
                data.Add(dacthu);
                data.Add(maymocthietbi);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IActionResult SoLuongTaiSanTheoDonVi()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;
            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_SoLuongTaiSanTheoDonVi"))
            {
                return Json("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var ts = _dbContext.EntityFromStore<SoLuongTaiSanTheoLoaiHinhVaDonVi>("PK_DASHBOARD.SP_SO_TAISAN_THEO_DON_VI", pDonVi, pOut);
                var results = ts.ToList();
                var data = new List<object>();
                Dictionary<string, bool> checkExist = new Dictionary<string, bool>();

                foreach (var t in ts)
                {
                    if (!checkExist.ContainsKey(t.TENDONVI))
                    {
                        var a = new
                        {
                            donvi = t.TENDONVI,
                            dat = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            nha = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            vatkientruc = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            oto = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            phuongtienkhac = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            caylaunam = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            huuhinhkhac = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            vohinh = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            dacthu = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG,
                            maymocthietbi = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI && c.TENDONVI == t.TENDONVI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI && c.TENDONVI == t.TENDONVI).FirstOrDefault().SOLUONG
                        };
                        data.Add(a);
                        checkExist.Add(t.TENDONVI, true);
                    }
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IActionResult GiaTriTaiSanTheoLoaiHinh()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;
            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_SoLuongTaiSanTheoDonVi"))
            {
                return Json("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var ts = _dbContext.EntityFromStore<GiaTriTaiSanTheoLoaiHinh>("PK_DASHBOARD.SP_GIA_TRI_TAISAN_THEO_LOAI_HINH", pDonVi, pOut);
                var data = new List<object>();
                var results = ts.ToList();
                var dat = new { loaitaisan = "Đất", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAT).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var nha = new { loaitaisan = "Nhà", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.NHA).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var oto = new { loaitaisan = "Ô tô", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.OTO).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var vatkientruc = new { loaitaisan = "Vật kiến trúc", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var phuongtienkhac = new { loaitaisan = "Phương tiện khác", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var maymocthietbi = new { loaitaisan = "Máy móc thiết bị", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var caylaunam = new { loaitaisan = "Cây lâu năm", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var huuhinhkhac = new { loaitaisan = "Hữu hình khác", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var vohinh = new { loaitaisan = "Vô hình", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.VO_HINH).FirstOrDefault().GIATRI.GetValueOrDefault() };
                var dacthu = new { loaitaisan = "Đặc thù", giatri = results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault() == null ? 0 : results.Where(c => c.LOAIHINHTAISAN == (int)enumLOAI_HINH_TAI_SAN.DAC_THU).FirstOrDefault().GIATRI.GetValueOrDefault() };
                data.Add(dat);
                data.Add(nha);
                data.Add(vatkientruc);
                data.Add(oto);
                data.Add(phuongtienkhac);
                data.Add(caylaunam);
                data.Add(huuhinhkhac);
                data.Add(vohinh);
                data.Add(dacthu);
                data.Add(maymocthietbi);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual IActionResult GiaTriSoLuongTaiSanTheoLoaiHinhVaDonVi()
        {
            var nguoiDung = _workContext.CurrentCustomer.ID;
            if (!_vaiTroWidgetService.AuthorizeWidget(nguoiDung, "_GiaTriSoLuongTaiSanTheoLoaiHinhVaDonVI"))
            {
                return JsonErrorMessage("no");
            }
            OracleParameter pDonVi = new OracleParameter("donvi", OracleDbType.Decimal, _workContext.CurrentDonVi.ID, ParameterDirection.Input);
            OracleParameter pOut = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            var ts = _dbContext.EntityFromStore<GiaTriSoLuongTaiSanTheoLoaiHinh>("PK_DASHBOARD.SP_GIA_TRI_SO_LUONG_TAISAN_THEO_LOAI_HINH_VA_DON_VI", pDonVi, pOut);
            decimal? sumSoLuong = ts.Sum(p => p.SO_LUONG);
            decimal? sumNguyenGia = ts.Sum(p => p.TONG_NGUYEN_GIA);
            var data = ts.ToList();
            if (sumSoLuong.GetValueOrDefault() <= 0)
                sumSoLuong = 1;
            if (sumNguyenGia.GetValueOrDefault() <= 0)
                sumNguyenGia = 1;
            var res = new List<object>();
            var listIgnore = new List<enumLOAI_HINH_TAI_SAN>
            {
                enumLOAI_HINH_TAI_SAN.ALL,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                enumLOAI_HINH_TAI_SAN.KHAC,
                enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD,
                enumLOAI_HINH_TAI_SAN.TSCD_KHAC
            };
            foreach (enumLOAI_HINH_TAI_SAN loaiHinhTS in (enumLOAI_HINH_TAI_SAN[])Enum.GetValues(typeof(enumLOAI_HINH_TAI_SAN)))
            {
                if (!listIgnore.Contains(loaiHinhTS))
                {
                    var _object = data.Where(p => p.LOAI_HINH_TAI_SAN_ID == (int)loaiHinhTS).Select(p => new
                    {
                        TEN_LOAI_HINH = _nhanHienThiService.GetGiaTriEnum((enumLOAI_HINH_TAI_SAN)p.LOAI_HINH_TAI_SAN_ID),
                        SO_LUONG = p.SO_LUONG,
                        TONG_NGUYEN_GIA = p.TONG_NGUYEN_GIA,
                        ToolTipSoLuong = p.SO_LUONG.ToVNStringNumber(),
                        ToolTipNguyenGia = p.TONG_NGUYEN_GIA.ToVNStringNumber(true),
                        PERCENT_SO_LUONG = (100 * p.SO_LUONG / sumSoLuong),
                        PERCENT_TONG_NGUYEN_GIA = (100 * p.TONG_NGUYEN_GIA / sumNguyenGia),
                        ToolTipTyTrongSoLuong = (100 * p.SO_LUONG / sumSoLuong).ToVNStringNumber(true),
                        ToolTipTyTrongNguyenGia = (100 * p.TONG_NGUYEN_GIA / sumNguyenGia).ToVNStringNumber(true),
                        LOAI_HINH_TAI_SAN_ID = p.LOAI_HINH_TAI_SAN_ID
                    }).FirstOrDefault();
                    if (_object != null)
                        res.Add(_object);
                    else
                        res.Add(new
                        {
                            TEN_LOAI_HINH = _nhanHienThiService.GetGiaTriEnum(loaiHinhTS),
                            SO_LUONG = 0,
                            TONG_NGUYEN_GIA = 0,
                            PERCENT_SO_LUONG = 0,
                            PERCENT_TONG_NGUYEN_GIA = 0,
                            LOAI_HINH_TAI_SAN_ID = (int)loaiHinhTS
                        });

                }
            }

            return JsonSuccessMessage(objdata: res);
        }
        #endregion
    }
}

