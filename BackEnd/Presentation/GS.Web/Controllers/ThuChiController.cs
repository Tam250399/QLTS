//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.SHTD;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.SHTD;
using GS.Web.Controllers;
using GS.Web.Factories.SHTD;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Linq;
using System.Collections.Generic;
using GS.Web.Factories.DB;
using GS.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class ThuChiController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IThuChiModelFactory _itemModelFactory;
        private readonly IThuChiService _itemService;
        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        #endregion

        #region Ctor
        public ThuChiController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IThuChiModelFactory itemModelFactory,
            IThuChiService itemService,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ThuChiSearchModel();
            var model = _itemModelFactory.PrepareThuChiSearchModel(searchmodel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListKetQua(ThuChiSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThuChiKetQuaListModel(searchModel);
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult List(ThuChiSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThuChiListModel(searchModel);
            return Json(model);
        }

        /// <summary>
        /// List thu chi cho Form nhập kiểu popup
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult ListThuChiByXuLy(ThuChiSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedKendoGridJson();

            var model = _itemModelFactory.PrepareThuChiListModel(searchModel);

            //prepare model

            return Json(model);
        }

        public virtual IActionResult _List(decimal KetQuaId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ThuChiSearchModel();
            var model = _itemModelFactory.PrepareThuChiSearchModel(searchmodel);
            model.KetQuaId = KetQuaId;
            return PartialView(model);
        }
        //public virtual IActionResult Create(decimal XuLyId = 0, string ListXuLyId = null)
        //{
        //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
        //        return AccessDeniedView();
        //    //prepare model
        //    var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() {XU_LY_ID= XuLyId }, null);
        //    return View(model);
        //}
        public virtual IActionResult Create(string ListXuLyId = null)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() { LIST_XU_LY_ID = ListXuLyId, DON_VI_ID = _workContext.CurrentDonVi.ID }, null);
            return View(model);
        }

        /// <summary>
        /// Form nhập popup
        /// </summary>
        /// <returns></returns>
        #region Form nhập popup
        public virtual IActionResult CreateThuChiTong()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() { LIST_XU_LY_ID = null, DON_VI_ID = _workContext.CurrentDonVi.ID }, null);
            return View(model);
        }
        public virtual IActionResult EditThuChiTong(string ListXuLyIdString)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() { LIST_XU_LY_ID = ListXuLyIdString, DON_VI_ID = _workContext.CurrentDonVi.ID }, null);
            return View(model);
        }
        public virtual IActionResult _CreateOrUpdate(string ListXuLyIdString, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareThuChiModelForCreateOrUpdate(ListXuLyIdString, Id);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _CreateOrUpdate(ThuChiModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            try
            {
                model.DON_VI_ID = _workContext.CurrentDonVi.ID;
                //_itemModelFactory.CheckValidNgayThuDauTien(ModelState, model);
                if (ModelState.IsValid)
                {
                    if (model.ID > 0)
                    {
                        var item = _itemService.GetThuChiById(model.ID);// model.ToEntity<ThuChi>();
                        if (item != null)
                        {
                            _itemModelFactory.PrepareThuChi(model, item);
                            _itemService.UpdateThuChi(item);
                            // cập nhật lại số tiền thu chi của các thu chi khác
                            _itemService.UpdateSoTienThuChi(model.LIST_XU_LY_ID);
                            return JsonSuccessMessage("Cập nhật thành công");
                        }
                        return JsonErrorMessage("Có lỗi xảy ra, xin vui lòng thử lại!", new List<ModelStateEntry>() { });
                    }
                    else
                    {
                        var item = new ThuChi() { };
                        _itemModelFactory.PrepareThuChi(model, item);
                        _itemService.InsertThuChi(item);
                        _itemService.UpdateSoTienThuChi(model.LIST_XU_LY_ID);
                        return JsonSuccessMessage("Thêm mới thành công");
                    }

                }

                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Error", list);
            }
            catch (Exception e)
            {

                return JsonErrorMessage("Có lỗi xảy ra, xin vui lòng thử lại!", new List<ModelStateEntry>() { });
            }

        }

        [HttpPost]
        public virtual IActionResult CreateThuChiTong(string ListThuChiFirst, string ListThuChi)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            try
            {
                _itemModelFactory.UpdateListXuLyIdWhenCreate(ListThuChiFirst, ListThuChi);
                return JsonSuccessMessage("Thêm mới thành công");
            }
            catch (Exception e)
            {

                return JsonErrorMessage("Có lỗi xảy ra, xin vui lòng thử lại!", new List<ModelStateEntry>() { });
            }

        }
        #endregion

        #region Comment
        //[HttpPost]
        //public virtual IActionResult Create(ThuChiModel model, bool continueEditing)
        //{
        //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
        //        return AccessDeniedView();

        //    if (ModelState.IsValid)
        //    {
        //        if(model.listModel!=null && model.listModel.Count() > 0)
        //        {
        //            var listID = new List<decimal>();
        //            foreach (var tc in model.listModel)
        //            {                        
        //                if (tc.ID <= 0)
        //                {
        //                    var item = tc.ToEntity<ThuChi>();
        //                    _itemService.InsertThuChi(item);
        //                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ThuChiModel>(), "ThuChi");
        //                    listID.Add(item.ID);
        //                }
        //                else
        //                {
        //                    var item = _itemService.GetThuChiById(tc.ID);
        //                    if (item != null)
        //                    {
        //                        _itemModelFactory.PrepareThuChi(tc,item);
        //                        _itemService.UpdateThuChi(item);
        //                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ThuChiModel>(), "ThuChi");
        //                        listID.Add(item.ID);
        //                    };
        //                }
        //            };
        //            //xóa những thằng k tồn tại trong list;
        //            var listItem = _itemService.GetThuChis(KetQuaId: (decimal)model.XU_LY_ID);
        //            var listEx = listItem.Where(c => !listID.Contains(c.ID));
        //            foreach(var ItemEx in listEx)
        //            {
        //                _itemService.DeleteThuChi(ItemEx);
        //                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", ItemEx.ToModel<ThuChiModel>(), "ThuChi");
        //            }
        //            //đồng bộ
        //            var listDongBo = listItem.Where(c => listID.Contains(c.ID));
        //            _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateThuChi", listDongBo.Select(c=>c.ToModel<ThuChiDongBoModel>()), _workContext.CurrentDonVi.ID);

        //        }               
        //        return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
        //    }

        //    var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
        //    return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        //}
        //public virtual IActionResult Edit(decimal XuLyId)
        //{
        //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
        //        return AccessDeniedView();
        //    //prepare model
        //    var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() { XU_LY_ID = XuLyId }, null);
        //    return View(model);
        //}
        #endregion


        [HttpPost]
        public virtual IActionResult Create(ThuChiModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                if (model.listModel != null && model.listModel.Count() > 0)
                {
                    // xóa hết list cũ
                    var ListIdOld = _itemService.GetThuChis(ListThuChiId: model.SelectedXuLyIds.ToListString());
                    if (ListIdOld != null && ListIdOld.Count() > 0)
                    {
                        foreach (var ThuChiOld in ListIdOld)
                        {
                            if (ThuChiOld != null)
                            {
                                _itemService.DeleteThuChi(ThuChiOld);
                                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", ThuChiOld.ToModel<ThuChiModel>(), "ThuChi");
                            }

                        }
                    }
                    // thêm list mới

                    var firstRow = model.listModel.Where(c => c.Is_First).FirstOrDefault();
                    // Đảm bảo dòng đầu tiên luôn có ID nhỏ nhất khi thêm
                    firstRow.LIST_XU_LY_ID = model.SelectedXuLyIds.ToListString();
                    var item = firstRow.ToEntity<ThuChi>();
                    _itemService.InsertThuChi(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ThuChiModel>(), "ThuChi");


                    foreach (var tc in model.listModel.Where(c => !c.Is_First))
                    {
                        if (tc != null)
                        {
                            // cập nhật ListX
                            tc.LIST_XU_LY_ID = model.SelectedXuLyIds.ToListString();
                            var thuChi = tc.ToEntity<ThuChi>();
                            _itemService.InsertThuChi(thuChi);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", thuChi.ToModel<ThuChiModel>(), "ThuChi");
                        }

                    };

                    /// Lưu Ý: Không cần xóa nữa nhưng chưa xử lý phần đồng bộ dữ liệu -- HưngNT
                    /// 
                    ////xóa những thằng k tồn tại trong list;
                    // var listItem = _itemService.GetThuChis(KetQuaId: (decimal)model.XU_LY_ID);
                    //var listEx = listItem.Where(c => !listID.Contains(c.ID));
                    //foreach (var ItemEx in listEx)
                    //{
                    //    _itemService.DeleteThuChi(ItemEx);
                    //    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", ItemEx.ToModel<ThuChiModel>(), "ThuChi");
                    //}
                    ////đồng bộ
                    //var listDongBo = listItem.Where(c => listID.Contains(c.ID));
                    //_dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateThuChi", listDongBo.Select(c => c.ToModel<ThuChiDongBoModel>()), _workContext.CurrentDonVi.ID);

                }
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }

            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }
        
        public virtual IActionResult Edit(string ListXuLyIdString)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //prepare model

            var model = _itemModelFactory.PrepareThuChiModel(new ThuChiModel() { LIST_XU_LY_ID = ListXuLyIdString }, null);
            return View(model);
        }

        public virtual IActionResult _Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();

            var item = _itemService.GetThuChiById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareThuChiModel(null, item);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _Edit(ThuChiModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuChiById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareThuChi(model, item);
                _itemService.UpdateThuChi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ThuChiModel>(), "ThuChi");
                //đồng bộ
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateThuChi", new List<ThuChiDongBoModel>() { item.ToModel<ThuChiDongBoModel>() }, _workContext.CurrentDonVi.ID);
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !", listError);
        }
        public virtual IActionResult GetSoTien(Decimal KetQuaId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();

            var item = _itemService.GetThuChis(KetQuaId: KetQuaId);
            if (item != null && item.Count() > 0)
            {
                var sotien = item.OrderBy(c => c.NGAY_THU).FirstOrDefault().SO_TIEN_PHAI_THU;
                return JsonSuccessMessage("Tồn tại", sotien);
            }
            return JsonErrorMessage("Không tồn tại", 0);
        }
        public virtual IActionResult _ThuChiTable(string ListXuLyString, bool AddColumn)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            var model = new ThuChiModel() { DON_VI_ID = _workContext.CurrentDonVi.ID };
            if (!string.IsNullOrEmpty(ListXuLyString))
            {
                var listItem = _itemService.GetAllThuChis().Where(c => c.LIST_XU_LY_ID == ListXuLyString);
                var MinId = listItem.Min(c => c.ID); // min id là id của lần nhập đầu tiên
                var listModel = listItem.Select(c =>
                {
                    var m = c.ToModel<ThuChiModel>();
                    if (c.ID == MinId)
                    {
                        m.Is_First = true;
                        m.Is_Disable = false;
                    }

                    return m;
                }).OrderBy(c => c.ID).ToList();

                if (listModel.Count() > 0)
                {
                    model.listModel = listModel;
                }
                else if (AddColumn)
                {
                    model.listModel.Add(new ThuChiModel() { Is_Disable = false, Is_First = true });
                }
            }
            else if (AddColumn)
            {
                model.listModel.Add(new ThuChiModel() { Is_Disable = false, Is_First = true });
            }
            return PartialView(model);
        }
        public virtual IActionResult _ThuChiRow(Decimal KetQuaId, Decimal? SoTienThu, Decimal? SoTienPhaiThu)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            var model = new ThuChiModel();
            model.SO_TIEN_PHAI_THU = SoTienThu;
            model.Is_Disable = true;
            //if (KetQuaId > 0)
            //{
            //    var thuchi = _itemService.GetThuChis(KetQuaId: KetQuaId).FirstOrDefault();
            //    if (thuchi != null)
            //    {
            //        model.SO_TIEN_PHAI_THU = thuchi.SO_TIEN_PHAI_THU;
            //        model.Is_Disable = true;
            //    };               
            //}
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuChiById(id);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", id);
            try
            { 
                var ListXuLyId = item.LIST_XU_LY_ID;
                _itemService.DeleteThuChi(item);
                _itemService.UpdateSoTienThuChi(ListXuLyId);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ThuChiModel>(), "ThuChi");
                //activity log  
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", exc);
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteByXuLyId(string ListXuLyId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            //try to get a store with the specified id           
            if (string.IsNullOrEmpty(ListXuLyId))
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", ListXuLyId);
            var listItem = _itemService.GetThuChis(ListThuChiId: ListXuLyId);
            foreach (var item in listItem)
            {
                try
                {
                    _itemService.DeleteThuChi(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ThuChiModel>(), "ThuChi");
                }
                catch (Exception exc)
                {
                    return JsonErrorMessage("Cập nhật dữ liệu không thành công !", exc);
                }
            }
            return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
        }
        #endregion
    }
}

