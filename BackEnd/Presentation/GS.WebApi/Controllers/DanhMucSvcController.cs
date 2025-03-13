using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Factories;
using GS.WebApi.Factories.Common;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.DMDC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GS.WebApi.Controllers
{
    public class DanhMucSvcController : BaseAdminController
    {
        #region Ctor

        private readonly IDanhMucSvcModelFactory _danhMucSvcModelFactory;
        private readonly IHoatDongService _hoatDongService;
        public DanhMucSvcController(IDanhMucSvcModelFactory danhMucSvcModelFactory,
            IHoatDongService hoatDongService)
        {
            this._danhMucSvcModelFactory = danhMucSvcModelFactory;
            this._hoatDongService = hoatDongService;
        }
        #endregion
        #region method
        #region quốc gia
        [HttpGet]
        public IActionResult GetAllQuocGias()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllQuocGias();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateQuocGia([FromBody] QuocGiaModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", model);
            var result = _danhMucSvcModelFactory.UpdateQuocGia(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListQuocGia([FromBody] List<QuocGiaModel> ListQuocGiaModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", ListQuocGiaModel);
            var result = _danhMucSvcModelFactory.UpDateListQuocGia(ListQuocGiaModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteQuocGia([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.DeleteQuocGia(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListQuocGia([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteQuocGia(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Hiện trạng
        [HttpGet]
        public IActionResult GetAllHienTrangs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllHienTrangs();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateHienTrang([FromBody] HienTrangModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            //  _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hiện trạng", 0, "HienTrang", model);
            var result = _danhMucSvcModelFactory.UpDateHienTrang(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListHienTrang([FromBody] List<HienTrangModel> ListHienTrangModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hiện trạng", 0, "HienTrang", ListHienTrangModel);
            var result = _danhMucSvcModelFactory.UpDateListHienTrang(ListHienTrangModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteHienTrang([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.DeleteHienTrang(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListHienTrang([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteHienTrang(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region LoaiDonVi
        [HttpGet]
        public IActionResult GetAllLoaiDonVis()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLoaiDonVis();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateLoaiDonVi([FromBody] LoaiDonViModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại đơn vị", 0, "LoaiDonVi", model);
            var result = _danhMucSvcModelFactory.UpDateLoaiDonVi(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListLoaiDonVi([FromBody] List<LoaiDonViModel> ListLoaiDonViModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (ListLoaiDonViModel == null)
                return this.NullModel();
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại đơn vị", 0, "LoaiDonVi", ListLoaiDonViModel);
            var result = _danhMucSvcModelFactory.UpDateListLoaiDonVi(ListLoaiDonViModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteLoaiDonVi([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            var result = _danhMucSvcModelFactory.DeleteLoaiDonVi(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListLoaiDonVi([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteLoaiDonVi(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion        
        #region DonVi
        [HttpGet]
        public IActionResult GetAllDonVis(int? pageSize, int? pageIndex)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (pageSize != null && pageIndex != null)
            {
                var result = _danhMucSvcModelFactory.GetAllDonVis(pageSize.Value, pageIndex.Value);
                return Ok(result);
            }
            else
            {
                var result = _danhMucSvcModelFactory.GetAllDonVis();
                return Ok(result);
            }
        }
        [HttpPost]
        public IActionResult UpdateDonVi([FromBody] DonViModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Đơn vị", 0, "DonVi", model);
            model.NGUOI_CAP_NHAT_ID = this.currentUser.ID;
            var result = _danhMucSvcModelFactory.UpdateDonVi(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListDonVi([FromBody] List<DonViModel> ListDonViModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (ListDonViModel == null)
                return this.NullModel();
            foreach (var item in ListDonViModel)
            {
                item.NGUOI_CAP_NHAT_ID = this.currentUser.ID;
            }
            //  _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Đơn vị", 0, "DonVi", ListDonViModel);
            var result = _danhMucSvcModelFactory.UpdateListDonVi(ListDonViModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteDonVi([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.DeleteDonVi(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListDonVi([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteDonVi(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion     
        #region Nguồn gốc tài sản
        [HttpGet]
        public IActionResult GetAllNguonGocTaiSans()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNguonGocTaiSans();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateNguonGocTaiSan([FromBody] NguonGocTaiSanModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "NguonDocTaiSan", model);
            var result = _danhMucSvcModelFactory.UpdateNguonGocTaiSan(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListNguonGocTaiSan([FromBody] List<NguonGocTaiSanModel> ListNguonGocTaiSanModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn gốc tài sản", 0, "NguonGocTaiSan", ListNguonGocTaiSanModel);
            var result = _danhMucSvcModelFactory.UpDateListNguonGocTaiSan(ListNguonGocTaiSanModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteNguonGocTaiSan([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            var result = _danhMucSvcModelFactory.DeleteNguonGocTaiSan(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListNguonGocTaiSan([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteNguonGocTaiSan(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion     
        #region MucDichSuDung
        //[HttpGet]
        //public IActionResult GetAllMucDichSuDungs()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllMucDichSuDungs();
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult UpdateMucDichSuDung([FromBody]MucDichSuDungModel model)
        //{
        //    if (model == null)
        //        return this.NullModel();
        //    // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "MucDichSuDung", model);
        //    var result = _danhMucSvcModelFactory.UpdateMucDichSuDung(model, currentUser);

        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult UpDateListMucDichSuDung([FromBody]List<MucDichSuDungModel> ListMucDichSuDungModel)
        //{
        //    if (ListMucDichSuDungModel == null)
        //        return this.NullModel();
        //    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "MucDichSuDung", ListMucDichSuDungModel);
        //    var result = _danhMucSvcModelFactory.UpDateListMucDichSuDung(ListMucDichSuDungModel, currentUser);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult DeleteMucDichSuDung([FromBody]DeleteModel model)
        //{
        //    if (model == null)
        //        return this.NullModel();
        //    var result = _danhMucSvcModelFactory.DeleteMucDichSuDung(model.ID);
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult DeleteListMucDichSuDung([FromBody]List<DeleteModel> model)
        //{
        //    int TotalSucc = 0;
        //    int TotalError = 0;
        //    if (!ModelState.IsValid)
        //    {
        //        var ListError = ModelState.SerializeErrors();
        //        return OkErrorMessage("Error", ListError);
        //    }
        //    List<DeleteModel> ListResult = new List<DeleteModel>();
        //    foreach (var item in model)
        //    {
        //        var result = _danhMucSvcModelFactory.DeleteMucDichSuDung(item.ID);
        //        if (result.Code != MessageReturn.SUCCESS_CODE)
        //        {
        //            TotalError++;
        //        }
        //        else
        //        {
        //            TotalSucc++;
        //        }
        //        item.Message = result.Message;
        //        ListResult.Add(item);
        //    }
        //    if (TotalError > 0)
        //    {
        //        return Ok(new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_PARTIAL_CODE,
        //            Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
        //            ObjectInfo = ListResult
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_CODE,
        //            Message = "Success done",
        //            ObjectInfo = ListResult
        //        });
        //    }
        //}
        #endregion
        #region Chế độ hao mòn
        [HttpGet]
        public IActionResult GetAllCheDoHaoMons()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllCheDoHaoMons();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateCheDoHaoMon([FromBody] CheDoHaoMonModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "CheDoHaoMon", model);
            var result = _danhMucSvcModelFactory.UpdateCheDoHaoMon(model, currentUser);

            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListCheDoHaoMon([FromBody] List<CheDoHaoMonModel> ListCheDoHaoMonModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "CheDoHaoMon", ListCheDoHaoMonModel);
            var result = _danhMucSvcModelFactory.UpDateListCheDoHaoMon(ListCheDoHaoMonModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteCheDoHaoMon([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            var result = _danhMucSvcModelFactory.DeleteCheDoHaoMon(model.ID, currentUser);
            return Ok(result);
        }

        #endregion
        #region LoaiTaiSan
        [HttpGet]
        public IActionResult GetAllLoaiTaiSanNhaNuocs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLoaiTaiSanNhaNuocs();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateLoaiTaiSan([FromBody] LoaiTaiSanModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại tài sản", 0, "LoaiTaiSan", model);
            var result = _danhMucSvcModelFactory.UpdateLoaiTaiSan(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListLoaiTaiSan([FromBody] List<LoaiTaiSanModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại tài sản", 0, "LoaiTaiSan", model);
            var result = _danhMucSvcModelFactory.UpdateListLoaiTaiSan(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteLoaiTaiSan([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            var result = _danhMucSvcModelFactory.DeleteLoaiTaiSan(model.ID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListLoaiTaiSan([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteLoaiTaiSan(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }
            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region LyDoBienDong
        [HttpGet]
        public IActionResult GetAllLyDoBienDongs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllLyDoBienDongs();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateLyDoBienDong([FromBody] LyDoBienDongModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Lý do biến động", 0, "LyDoBienDong", model);
            var result = _danhMucSvcModelFactory.UpdateLyDoBienDong(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListLyDoBienDong([FromBody] List<LyDoBienDongModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            //  _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Lý do biến động", 0, "LyDoBienDong", model);
            var result = _danhMucSvcModelFactory.UpdateListLyDoBienDong(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListLyDoBienDong([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteLyDoBienDong(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Loại lý do biến động      
        [HttpPost]
        public IActionResult UpdateLoaiLyDoBienDong([FromBody] List<LoaiLyDoBienDongModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Lý do biến động", 0, "LyDoBienDong", model);
            var result = _danhMucSvcModelFactory.UpdateLoaiLyDoBienDong(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteLoaiLyDoBienDong([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteLoaiLyDoBienDong(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Người dùng
        [HttpGet]
        public IActionResult GetAllNguoiDungs(int? pageSize, int? pageIndex)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (pageSize != null && pageIndex != null)
            {
                var result = _danhMucSvcModelFactory.GetAllNguoiDungs(pageSize.Value, pageIndex.Value);
                return Ok(result);
            }
            else
            {
                var result = _danhMucSvcModelFactory.GetAllNguoiDungs();
                return Ok(result);
            }
        }
        [HttpPost]
        public IActionResult UpdateNguoiDung([FromBody] NguoiDungModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Nhận đồng bộ Người dùng", 0, "NguoiDung", model);
            var result = _danhMucSvcModelFactory.UpdateNguoiDung(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListNguoiDung([FromBody] List<NguoiDungModel> ListNguoiDungModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (ListNguoiDungModel == null)
                return this.NullModel();
            HoatDong hoatDong = _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Nhận đồng bộ Người dùng", 0, "NguoiDung", ListNguoiDungModel);
            var result = _danhMucSvcModelFactory.UpdateListNguoiDung(ListNguoiDungModel, currentUser);
            hoatDong.MO_TA += " Response : " + result.Message;
            _hoatDongService.UpdateHoatDong(hoatDong);
            return Ok(result);
        }
        #endregion
        #region DiaBan
        [HttpGet]
        public IActionResult GetAllDiaBans()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDiaBans();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateDiaBan([FromBody] DiaBanModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Địa bàn", 0, "DiaBan", model);
            var result = _danhMucSvcModelFactory.UpdateDiaBan(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListDiaBan([FromBody] List<DiaBanModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Địa bàn", 0, "DiaBan", model);
            var result = _danhMucSvcModelFactory.UpdateDiaBans(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteDiaBan([FromBody] DeleteModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            var result = _danhMucSvcModelFactory.DeleteDiaBan(model.ID, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListDiaBan([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteDiaBan(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region DuAn
        //[HttpGet]
        //public IActionResult GetAllDuAns()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllDuAns();
        //    return Ok(result);
        //}
        [HttpPost]
        public IActionResult UpdateDuAn([FromBody] DuAnModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dự án", 0, "DuAn", model);
            var result = _danhMucSvcModelFactory.UpdateDuAn(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListDuAn([FromBody] List<DuAnModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (model == null)
                return this.NullModel();
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dự án", 0, "DuAn", model);
            var result = _danhMucSvcModelFactory.UpdateDuAns(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteListDuAn([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteDiaBan(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion

        #region DongXe
        [HttpGet]
        public IActionResult GetAllDongXes()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDongXes();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateDongXe([FromBody] List<DongXeModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Dòng xe", 0, "DongXe", model);
            var result = _danhMucSvcModelFactory.UpdateDongXes(model, currentUser);
            return Ok(result);
        }
        #endregion

        #region ChucDanh
        [HttpGet]
        public IActionResult GetAllChucDanhs()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllChucDanhs();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateChucDanh([FromBody] ChucDanhModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chức danh", 0, "ChucDanh", model);
            var result = _danhMucSvcModelFactory.UpdateChucDanh(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateChucDanhs([FromBody] List<ChucDanhModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chức danh", 0, "ChucDanh", model);
            var result = _danhMucSvcModelFactory.UpdateChucDanhs(model, currentUser);
            return Ok(result);
        }
        #endregion

        #region NhanXe
        [HttpGet]
        public IActionResult GetAllNhanXes()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNhanXe();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateNhanXe([FromBody] List<NhanXeModel> ListNhanXeModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nhãn xe", 0, "NhanXe", ListNhanXeModel);
            var result = _danhMucSvcModelFactory.UpDateListNhanXe(ListNhanXeModel, currentUser);
            return Ok(result);
        }
        #region NguonVon
        [HttpGet]
        public IActionResult GetAllNguonVons()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllNguonVons();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateNguonVon([FromBody] NguonVonModel model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Nguồn vốn", 0, "NguonVon", model);
            var result = _danhMucSvcModelFactory.UpDateNguonVon(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListNguonVon([FromBody] List<NguonVonModel> ListNguonVonModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật nguồn vốn", 0, "NguonVon", ListNguonVonModel);
            var result = _danhMucSvcModelFactory.UpdateListNguonVon(ListNguonVonModel, currentUser);
            return Ok(result);
        }
        #endregion
        #region Bộ phận sử dụng
        [HttpGet]
        public IActionResult GetAllDonViBoPhan()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllDonViBoPhan();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListDonViBoPhan([FromBody] List<DonViBoPhanModel> ListDonViBoPhanModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _danhMucSvcModelFactory.UpdateListDonViBoPhan(ListDonViBoPhanModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteDonViBoPhan([FromBody] List<DeleteModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in ListModel)
            {
                var result = _danhMucSvcModelFactory.DeleteDonViBoPhan(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #endregion

        #region Danh mục dùng chung
        [HttpPost]
        public IActionResult UpDateListDMDC_Diaban([FromBody] List<DMDC_DiaBanModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _danhMucSvcModelFactory.UpdateListDMDC_DiaBan(ListModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListDMDC_DonViDuAn([FromBody] List<DMDC_DonViDuAnModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _danhMucSvcModelFactory.UpdateListDMDC_DonViDuAn(ListModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListDMDC_DonViNganSach([FromBody] List<DMDC_DonViNganSachModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _danhMucSvcModelFactory.UpdateListDMDC_DonViNganSach(ListModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListDMDC_DuAn([FromBody] List<DMDC_DuAnModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            var result = _danhMucSvcModelFactory.UpdateListDMDC_DuAn(ListModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListDMDC_QuocGia([FromBody] List<DMDC_QuocGiaModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }

            var result = _danhMucSvcModelFactory.UpdateListDMDC_QuocGia(ListModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpDateListDMDC_ToChucNganSach([FromBody] List<DMDC_ToChucNganSachModel> ListModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }

            var result = _danhMucSvcModelFactory.UpdateListDMDC_ToChucNganSach(ListModel, currentUser);
            return Ok(result);
        }
        #endregion
        #region Loại tài sản theo đơn vị        
        [HttpPost]
        public IActionResult UpdateLoaiTaiSanDonVi([FromBody] List<LoaiTaiSanDonViModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            if (model == null)
                return this.NullModel();
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Loại tài sản đơn vị", 0, "LoaiTaiSanDonVi", model);
            var result = _danhMucSvcModelFactory.UpdateListLoaiTaiSanDonVi(model, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteLoaiTaiSanDonVi([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteLyDoBienDong(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Phương án xử lý
        [HttpGet]
        public IActionResult GetAllPhuongAnXuLys()
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            var result = _danhMucSvcModelFactory.GetAllPhuongAnXuLys();
            return Ok(result);
        }
        //[HttpPost]
        //public IActionResult UpdatePhuongAnXuLy([FromBody]PhuongAnXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hình thức xử lý", 0, "PhuongAnXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdatePhuongAnXuLy(model, currentUser);
        //    return Ok(result);
        //}
        [HttpPost]
        public IActionResult UpDateListPhuongAnXuLy([FromBody] List<PhuongAnXuLyModel> ListPhuongAnXuLyModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Phương án xử lý", 0, "PhuongAnXuLy", ListPhuongAnXuLyModel);
            var result = _danhMucSvcModelFactory.UpDateListPhuongAnXuLy(ListPhuongAnXuLyModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeletePhuongAnXuLy([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeletePhuongAnXuLy(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }

            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Hình thức xử lý
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}
        [HttpPost]
        public IActionResult UpdateListHinhThucXuLy([FromBody] List<HinhThucXuLyModel> ListHinhThucXuLyModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Hình thức xử lý", 0, "HinhThucXuLy", ListHinhThucXuLyModel);
            var result = _danhMucSvcModelFactory.UpDateListHinhThucXuLy(ListHinhThucXuLyModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteHinhThucXuLy([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteHinhThucXuLy(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }
            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Chức Danh
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}
        [HttpPost]
        public IActionResult UpdateChucDanh([FromBody] List<ChucDanhModel> ListChucDanhModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chức danh", 0, "ChucDanh", ListChucDanhModel);
            var result = _danhMucSvcModelFactory.UpdateChucDanhs(ListChucDanhModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteChucDanh([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteHinhThucXuLy(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }
            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #region Hình thức mua sắm
        //[HttpGet]
        //public IActionResult GetAllHinhThucXuLys()
        //{
        //    var result = _danhMucSvcModelFactory.GetAllHinhThucXuLys();
        //    return Ok(result);

        //}
        //[HttpPost]
        //public IActionResult UpdateHinhThucXuLy([FromBody]HinhThucXuLyModel model)
        //{
        //    _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật  Phương án xử lý", 0, "HinhThucXuLy", model);
        //    var result = _danhMucSvcModelFactory.UpdateHinhThucXuLy(model, currentUser);
        //    return Ok(result);

        //}
        [HttpPost]
        public IActionResult UpdateHinhThucMuaSam([FromBody] List<ChucDanhModel> ListChucDanhModel)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật Chức danh", 0, "ChucDanh", ListChucDanhModel);
            var result = _danhMucSvcModelFactory.UpdateChucDanhs(ListChucDanhModel, currentUser);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteHinhThucMuaSam([FromBody] List<DeleteModel> model)
        {
            #region check token
            if (!CheckCurrentUser())
                return OkErrorMessage("Token hết hạn");
            #endregion
            int TotalSucc = 0;
            int TotalError = 0;
            if (!ModelState.IsValid)
            {
                var ListError = ModelState.SerializeErrors();
                return OkErrorMessage("Error", ListError);
            }
            List<DeleteModel> ListResult = new List<DeleteModel>();
            foreach (var item in model)
            {
                var result = _danhMucSvcModelFactory.DeleteHinhThucXuLy(item.ID);
                if (result.Code != MessageReturn.SUCCESS_CODE)
                {
                    TotalError++;
                }
                else
                {
                    TotalSucc++;
                }
                item.Message = result.Message;
                ListResult.Add(item);
            }
            if (TotalError > 0)
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = "Total " + TotalSucc.ToString() + " success - Total " + TotalError.ToString() + " error",
                    ObjectInfo = ListResult
                });
            }
            else
            {
                return Ok(new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done",
                    ObjectInfo = ListResult
                });
            }
        }
        #endregion
        #endregion

    }
}
