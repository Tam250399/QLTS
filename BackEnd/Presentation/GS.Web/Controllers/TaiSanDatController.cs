using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class TaiSanDatController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ITaiSanDatModelFactory _itemModelFactory;
        private readonly ITaiSanDatService _itemService;
        public readonly INguonVonService _nguonVonService;
        public readonly IHienTrangService _hienTrangService;

        #endregion
        #region Ctor
        public TaiSanDatController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanDatModelFactory itemModelFactory,
            ITaiSanDatService itemService,
            INguonVonService nguonVonService,
            IHienTrangService hienTrangService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._nguonVonService = nguonVonService;
            this._hienTrangService = hienTrangService;
        }
        #endregion
        #region Method
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanDatSearchModel();
            var model = _itemModelFactory.PrepareTaiSanDatSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanDatSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanDatListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanDatModel(new TaiSanDatModel(), null);
            model.TaiSanModel = new TaiSanModel();
            model.YeuCauChiTietModel = new YeuCauChiTietModel();
            model.YeuCauModel = new YeuCauModel();
            model.TaiSanModel.LoaiTaiSanAvailable.AddFirstRow("--Chọn loại tài sản");
            //Prepare add nguon von id,
            //1: Nguồn ngân sách
            //3: Nguồn khác
            model.TaiSanModel.SelectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
            var _listNV = _nguonVonService.GetNguonVonByIds(model.TaiSanModel.SelectedNguonVonIds.Select(c => (decimal)c).ToArray());
            if (_listNV != null)
            {
                foreach (var _nguonVon in _listNV)
                {
                    model.TaiSanModel.lstNguonVonModel.Add(new NguonVonModel()
                    {
                        ID = _nguonVon.ID,
                        TEN = _nguonVon.TEN
                    });
                }
            }
            //prepare hiện trạng
            var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: (int)enumLOAI_HINH_TAI_SAN.DAT);
            var htsdkhac = lstHienTrang.Where(c => c.ID == 132).FirstOrDefault();
            lstHienTrang.Remove(htsdkhac);
            var lstObjHienTrang = new List<ObjHienTrang>();
            foreach (var ht in lstHienTrang)
            {
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                obj.GiaTriCheckbox = false;
                lstObjHienTrang.Add(obj);
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = null,
                lstObjHienTrang = lstObjHienTrang
            };
            model.YeuCauChiTietModel.lstHienTrang = hientrangList.lstObjHienTrang;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(TaiSanDatModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<TaiSanDat>();
                _itemService.InsertTaiSanDat(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanDatModel>(), "Dat");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return View(model);
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanDatModel(model, null);
            return View(model);
        }


        public virtual IActionResult CreateTSK()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = new TaiSanMayMocModel();
            model.TaiSanModel = new TaiSanModel();
            model.NVYeuCauChiTietModel = new YeuCauChiTietModel();
            model.TaiSanModel.LoaiTaiSanAvailable.AddFirstRow("--Chọn loại tài sản");
            //Prepare add nguon von id,
            //1: Nguồn ngân sách
            //3: Nguồn khác
            model.TaiSanModel.SelectedNguonVonIds = ((enumNGUON_VON_DEFAULT[])Enum.GetValues(typeof(enumNGUON_VON_DEFAULT))).Select(c => (int)c).ToList();
            var _listNV = _nguonVonService.GetNguonVonByIds(model.TaiSanModel.SelectedNguonVonIds.Select(c => (decimal)c).ToArray());
            if (_listNV != null)
            {
                foreach (var _nguonVon in _listNV)
                {
                    model.TaiSanModel.lstNguonVonModel.Add(new NguonVonModel()
                    {
                        ID = _nguonVon.ID,
                        TEN = _nguonVon.TEN
                    });
                }
            }
            //prepare hiện trạng
            var lstHienTrang = _hienTrangService.GetHienTrangs(LoaiHinhTsId: (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI);
            var htsdkhac = lstHienTrang.Where(c => c.ID == 132).FirstOrDefault();
            lstHienTrang.Remove(htsdkhac);
            var lstObjHienTrang = new List<ObjHienTrang>();
            foreach (var ht in lstHienTrang)
            {
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                obj.GiaTriCheckbox = false;
                lstObjHienTrang.Add(obj);
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = null,
                lstObjHienTrang = lstObjHienTrang
            };
            model.NVYeuCauChiTietModel.lstHienTrang = hientrangList.lstObjHienTrang;
            return View(model);
        }



        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            var item = _itemService.GetTaiSanDatById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanDatModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(TaiSanDatModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanDatById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanDat(model, item);
                _itemService.UpdateTaiSanDat(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanDatModel>(), "Dat");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanDatModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanDatById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteTaiSanDat(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanDatModel>(), "Dat");
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
        #endregion
    }
}
