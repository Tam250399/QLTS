using DevExpress.DataProcessing;
using FluentValidation;
using GS.Core.Domain.BienDongs;
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
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly GSObjectContext _context;
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
            ITaiSanService taiSanService,
            ITaiSanDatService taiSanDatService,
            ITaiSanNhaService taiSanNhaService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            GSObjectContext context, 
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
            _taiSanService = taiSanService;
            _taiSanDatService = taiSanDatService;
            _taiSanNhaService = taiSanNhaService;
            _bienDongService = bienDongService;
            _bienDongChiTietService = bienDongChiTietService;
            _taiSanNguonVonService = taiSanNguonVonService;
            _taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            _context = context;
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

        // PUT api/<TaiSanController>/5
        [HttpPut]
        public virtual IActionResult SuaTaiSan([FromBody] TaiSanModel model)
        {
            //var validator = new TaiSanValidator(_taiSanModelFactory, _loaiTaiSanModelFactory);
            //var validationResult = validator.Validate(value);

            //if (validationResult.Errors.Count > 0)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}
            var item = _taiSanService.GetTaiSanById(model.ID ?? 0);
            _taiSanModelFactory.UpdateTaiSan(model);
            switch (item.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = _taiSanDatService.GetTaiSanDatByTaiSanId(model.ID ?? 0);
                    _taiSanModelFactory.PrepareTaiSanDat(model, TsDat);
                    _taiSanDatService.UpdateTaiSanDat(TsDat);
                    var listNha = _taiSanNhaService.GetTaiSanNhaByDatId(TsDat.TAI_SAN_ID);
                    if (listNha != null)
                    {
                        //update lại địa chỉ của tài sản nhà được gắn trên đất
                        foreach (var itemNha in listNha)
                        {
                            //itemNha.DIA_CHI = TsDat.DIA_CHI;
                            itemNha.DIA_CHI = model.TEN;
                            _taiSanNhaService.UpdateTaiSanNha(itemNha);
                        }
                    }
                    //yeuCauChiTiet.DIA_CHI = model.TEN; //địa chỉ đẩy đủ cả tỉnh, huyện, xã
                    //yeuCauChiTiet.DIA_CHI = TsDat.DIA_CHI;//địa chỉ nguyên bản chưa xử lý
                    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    //    var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID);
                    //    _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                    //    TsNha.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
                    //    _taisannhaService.UpdateTaiSanNha(TsNha);
                    //    yeuCauChiTiet.DIA_CHI = TsNha.DIA_CHI;
                    //    if ((model.taisannhaModel.TAI_SAN_DAT_ID ?? 0) <= 0)
                    //    {
                    //        // lưu địa chỉ đầy đủ của nhà không đất trên ycct.Dia_CHI
                    //        // địa chỉ nguyên bản lưu trên taisannha, ycct.NHA_DIA_CHI
                    //        yeuCauChiTiet.DIA_CHI = _taiSanNhaModelFactory.PrepareDiaChiNhaByDiaBan(TsNha.DIA_CHI.Trim(), model.taisannhaModel.DIA_BAN_ID);
                    //        yeuCauChiTiet.NHA_DIA_CHI = TsNha.DIA_CHI;
                    //    }
                    //    // thêm lưu địa chỉ nhà
                    //    yeuCauChiTiet.DIA_BAN_ID = model.taisannhaModel.DIA_BAN_ID;

                    //    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    //case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    //    var TsOto = _taisanOtoService.GetTaiSanOtoById(model.ID);
                    //    _taiSanOtoModelFactory.PrepareTaiSanOto(model.taisanOtoModel, TsOto);
                    //    _taisanOtoService.UpdateTaiSanOto(TsOto);
                    //    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    //    model.taisanClnModel = new TaiSanClnModel();
                    //    model.taisanClnModel.TAI_SAN_ID = model.ID;
                    //    model.taisanClnModel.NAM_SINH = model.NAM_SAN_XUAT;
                    //    var TsCayLauNam = _taisanClnService.GetTaiSanClnByTaiSanId(model.ID);
                    //    _taiSanClnModelFactory.PrepareTaiSanCln(model.taisanClnModel, TsCayLauNam);
                    //    _taisanClnService.UpdateTaiSanCln(TsCayLauNam);
                    //    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    //case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    //case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    //    model.taisanmaymocModel.TAI_SAN_ID = model.ID;
                    //    model.taisanmaymocModel.PHU_KIEN_JSON = model.taisanmaymocModel.ListPhuKienHuuHinh.toStringJson();
                    //    var TsMayMoc = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID);
                    //    _taiSanMayMocModelFactory.PrepareTaiSanMayMoc(model.taisanmaymocModel, TsMayMoc);
                    //    _taisanmaymocService.UpdateTaiSanMayMoc(TsMayMoc);
                    //    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    //    model.taisanVktModel.TAI_SAN_ID = model.ID;
                    //    var TsVatKienTruc = _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID);
                    //    _taiSanVktModelFactory.PrepareTaiSanVkt(model.taisanVktModel, TsVatKienTruc);
                    //    _taisanVKTService.UpdateTaiSanVkt(TsVatKienTruc);
                    //    break;

                    //case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    //    model.taisanvohinhModel.TAI_SAN_ID = model.ID;
                    //    var taisanvohinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID);
                    //    _taiSanVoHinhModelFactory.PrepareTaiSanVoHinh(model.taisanvohinhModel, taisanvohinh);
                    //    _taiSanVoHinhService.UpdateTaiSanVoHinh(taisanvohinh);
                    //    break;
            }
            var bienDong = _bienDongService.GetBienDongCuNhatByTaiSanId(model.ID??0);
            _bienDongModelFactory.UpDateBienDong(model, bienDong);
            var biendongchitiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
            var bdct = _bienDongChiTietModelFactory.UpdateBienDongChiTiet(model, biendongchitiet, bienDong);
            var tsnv = _taiSanNguonVonService.GetTaiSanNguonVonByBienDongId(bienDong.ID);
            _taiSanNguonVonModelFactory.UpDateTaiSanNguonVonFromBienDong(model, tsnv, bienDong.ToModel<BienDongModel>());
            var tshtsd = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByBienDongId(bienDong.ID);
            _taiSanHienTrangSuDungModelFactory.UpDateHienTrangSuDungForBienDong(bienDong,tshtsd, bdct.HTSD_JSON);
            _taiSanLichSuModelFactory.InsertTaiSanLichSu(item.ID, null, "Tạo mới");
            return OkSuccessMessage("Cập nhật tài sản thành công!", model);

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
