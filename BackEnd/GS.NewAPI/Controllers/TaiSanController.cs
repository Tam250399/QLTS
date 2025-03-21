using DevExpress.DataProcessing;
using FluentValidation;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.NewAPI.Factories;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Infrastruture.Response;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using GS.NewAPI.Validators.TaiSanValidator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GS.NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSanController : BaseApiController
    { 
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly ITaiSanDatModelFactory _taiSanDatModelFactory;
        private readonly IBienDongModelFactory _bienDongModelFactory;
        private readonly IBienDongChiTietModelFactory _bienDongChiTietModelFactory;
        private readonly ITaiSanNguonVonModelFactory _taiSanNguonVonModelFactory;
        private readonly ITaiSanHienTrangSuDungModelFactory _taiSanHienTrangSuDungModelFactory;
        private readonly ITaiSanLichSuModelFactory _taiSanLichSuModelFactory;
        private readonly ITaiSanNhaModelFactory _taiSanNhaModelFactory;
        public TaiSanController(
            ITaiSanModelFactory taiSanModelFactory, 
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory, 
            ITaiSanDatModelFactory taiSanDatModelFactory,
            IBienDongModelFactory bienDongModelFactory,
            IBienDongChiTietModelFactory bienDongChiTietModelFactory,
            ITaiSanNguonVonModelFactory taiSanNguonVonModelFactory,
            ITaiSanHienTrangSuDungModelFactory taiSanHienTrangSuDungModelFactory,
            ITaiSanLichSuModelFactory taiSanLichSuModelFactory,
            ITaiSanNhaModelFactory taiSanNhaModelFactory) 
        {
            _taiSanModelFactory = taiSanModelFactory;
            _loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            _taiSanDatModelFactory = taiSanDatModelFactory;
            _bienDongModelFactory = bienDongModelFactory;
            _bienDongChiTietModelFactory = bienDongChiTietModelFactory;
            _taiSanNguonVonModelFactory = taiSanNguonVonModelFactory;
            _taiSanHienTrangSuDungModelFactory = taiSanHienTrangSuDungModelFactory;
            _taiSanLichSuModelFactory = taiSanLichSuModelFactory;
            _taiSanNhaModelFactory = taiSanNhaModelFactory;
        }
        // GET: api/<TaiSanController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaiSanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaiSanController>
        [HttpPost]
        public IActionResult Post([FromBody] TaiSanModel model)
        {
            // check validator TaiSanModel
            var validator = new TaiSanValidator(_taiSanModelFactory, _loaiTaiSanModelFactory);
            var validationResult =  validator.Validate(model);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var taiSanModel = _taiSanModelFactory.InsertTaiSan(model);
            //save tsdat
            switch (taiSanModel.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = model.ToEntity<TaiSanDat>();
                    TsDat.TAI_SAN_ID = (decimal)taiSanModel.ID;
                    TsDat.TINH_ID = model.TINH_THANH_PHO_ID;
                    TsDat.HUYEN_ID = model.QUAN_HUYEN_ID;
                    TsDat.XA_ID = model.XA_PHUONG_ID;
                    TsDat.DIA_BAN_ID = model.XA_PHUONG_ID;
                    _taiSanDatModelFactory.InsertTaiSanDat(TsDat);
                    break;
            }
            // lưu biến động
            var taiSanEntity = _taiSanModelFactory.GetTaiSanById(taiSanModel.ID ?? 0);
            _bienDongModelFactory.InsertToBienDong(taiSanEntity, model, new BienDongModel());
            var biendong = _bienDongModelFactory.GetBienDongCuoiByTaiSanId(taiSanEntity.ID).ToModel<BienDongModel>();
            var biendongchitiet = _bienDongChiTietModelFactory.InsertToBienDongChiTiet(model, new BienDongChiTietModel(), biendong);
            _taiSanNguonVonModelFactory.InsertTaiSanNguonVonFromBienDong(model, biendong);
            _taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong((decimal)biendong.ID, taiSanEntity.ID, biendongchitiet.HTSD_JSON);
            _taiSanLichSuModelFactory.InsertTaiSanLichSu(taiSanEntity.ID, null, "Tạo mới");
            return OkSuccessMessage("Tạo mới tài sản thành công", model);
        }

        // PUT api/<TaiSanController>/5: 'Error in the application.'

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaiSanController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
            //    return AccessDeniedView();
            ////try to get a store with the specified guid
            var item = _taiSanModelFactory.GetTaiSanById(id);
            if (item == null)
                return OkNotFoundMessage("Không tìm thấy tài sản để xóa!", item);
            if (item.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || item.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                if (item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    var taiSanNhas = _taiSanNhaModelFactory.GetTaiSanNhasByDatId(item.ID);
                    if (taiSanNhas.Any())
                    {
                        return OkErrorMessage("Có tài sản nhà trên tài sản này.", item.ID);
                    }
                }
                // xử lí update biến động ở trạng thái chờ duyệt 
                var bienDongs = _bienDongModelFactory.GetBienDongsByTaiSanId(item.ID);
                if (bienDongs.Any())
                {
                    bienDongs.ForEach(x => x.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.XOA);
                    _bienDongModelFactory.UpdateBienDongs(bienDongs);
                }               
                // xử lí update lại tài sản 
                item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.XOA;
                item.NGAY_CAP_NHAT = DateTime.Now;
                _taiSanModelFactory.UpdateTaiSan(item);
                // Lưu log vào tài sản lịch sử
                _taiSanLichSuModelFactory.InsertTaiSanLichSu(item.ID, null, "Xóa");
                return OkSuccessMessage("Đã xóa tài sản thành công.", item.ID);
            }
            else
                return OkErrorMessage("Tài sản này không được xóa.", item.ID);
        }
    }
}
