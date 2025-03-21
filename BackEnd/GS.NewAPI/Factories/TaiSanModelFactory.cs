using AutoMapper;
using GS.Core.Domain.DanhMuc;
using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.BienDongs;
using System;
using GS.Services.BienDongs;

namespace GS.NewAPI.Factories
{
    public class TaiSanModelFactory : ITaiSanModelFactory
    {
        private readonly ITaiSanService _taiSanService;
        private readonly IWorkContext _workContext;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IDonViService _donViService;
        private readonly ITaiSanDatService _taisandatService;
        private readonly ITaiSanNhaService _taisannhaService;
        private readonly IBienDongService _bienDongService;
        public TaiSanModelFactory(
            ITaiSanService taiSanService,
            IMapper mapper ,
            IWorkContext workContext,
            ILoaiTaiSanDonViServices loaiTaiSanDonViService,
            ILoaiTaiSanService loaiTaiSanService,
            IDonViService donViService,
            ITaiSanDatService taisandatService,
            ITaiSanNhaService taisannhaService,
             IBienDongService bienDongService)
        {
            _taiSanService = taiSanService;
            _workContext = workContext;
            _loaiTaiSanDonViServices = loaiTaiSanDonViService;
            _loaiTaiSanService = loaiTaiSanService;
            _donViService = donViService;
            _taisandatService = taisandatService;
            _taisannhaService = taisannhaService;
            _bienDongService = bienDongService;
        }
        public class UpdateTaiSanResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
        public UpdateTaiSanResult UpdateTaiSan(TaiSanModel models)
        {
            var item = _taiSanService.GetTaiSanById(models.ID ?? 0);
            models.NGUOI_TAO_ID = item.NGUOI_TAO_ID;
            if (item == null)
                return new UpdateTaiSanResult
                {
                    Success = false,
                    Message = "Tài sản không tồn tại"
                };

            if (item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET ||
                item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                return new UpdateTaiSanResult
                {
                    Success = false,
                    Message = "Tài sản đã được duyệt, không thể chỉnh sửa"
                };
            }
            //if (entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
            //{
            //    entity.TEN = _taiSanOtoModelFactory.genTenTaiSanOto(entity.taisanOtoModel.NHAN_XE_ID, entity.taisanOtoModel.DONG_XE_ID, entity.taisanOtoModel.BIEN_KIEM_SOAT);
            //}

            //if (entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            //    entity.MA = LoadMaTaiSan(_workContext.CurrentDonVi.ID, entity.ID, entity.LOAI_TAI_SAN_DON_VI_ID, entity.LOAI_HINH_TAI_SAN_ID);
            //else
            //    entity.MA = LoadMaTaiSan(0, entity.ID, entity.LOAI_TAI_SAN_ID, entity.LOAI_HINH_TAI_SAN_ID);
            item = models.ToEntity<TaiSan>();

            _taiSanService.UpdateTaiSan(item);
            switch (item.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = _taisandatService.GetTaiSanDatByTaiSanId(models.ID ?? 0);
                    PrepareTaiSanDat(models.taisandatModel, TsDat);
                    _taisandatService.UpdateTaiSanDat(TsDat);
                    var listNha = _taisannhaService.GetTaiSanNhaByDatId(TsDat.TAI_SAN_ID);
                    if (listNha != null)
                    {
                        //update lại địa chỉ của tài sản nhà được gắn trên đất
                        foreach (var itemNha in listNha)
                        {
                            //itemNha.DIA_CHI = TsDat.DIA_CHI;
                            itemNha.DIA_CHI = models.TEN;
                            _taisannhaService.UpdateTaiSanNha(itemNha);
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
            //update session
            var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(models.ID);
            //insert biendong

            //khởi tạo biến động từ yêu cầu
            var biendong = _bienDongService.GetBienDongById(models.ID ?? 0);
            if (biendong != null)
            {
                biendong.NGAY_DUYET = DateTime.Now;
                biendong.NGUOI_DUYET_ID = _workContext.CurrentCustomer.ID;
                biendong.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.DA_DUYET;
            }
            //gán lại giá trị mặc định
        

            //nếu có biến động trước là thay đổi thông tin
            //gán lại giá trị đã thay đổi của biến động
            //_bienDongModelFactory.PrepareBienDongFromBDTDTT(biendong, bienDongTruoc);

            ////khởi tạo biến động chi tiết từ yêu cầu chi tiết
            //var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            //var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            //var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();
            ////gán giá trị diện tích sang bảng biến động
            //biendong.DAT_TONG_DIEN_TICH = biendongchitiet.DAT_TONG_DIEN_TICH;
            //biendong.NHA_TONG_DIEN_TICH_XD = biendongchitiet.NHA_TONG_DIEN_TICH_XD;
            //biendong.VKT_DIEN_TICH = biendongchitiet.VKT_DIEN_TICH;

            ////hiện trạng của biến động chi tiết
            //if (biendongchitiet.HTSD_JSON == null) biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(ts.ID);

            ////tính lại giá trị còn lại của biến động
            //_bienDongModelFactory.TinhGiaTriConLaiBienDong(biendong, biendongchitiet);
            _bienDongService.UpdateBienDong(biendong);
            return new UpdateTaiSanResult
            {
                Success = true,
                Message = "Cập nhật tài sản thành công"
            };

        }

        public string LoadMaTaiSan(decimal? DonViId = 0, decimal? TaiSanId = 0, decimal? LoaiTaiSanId = 0, decimal? loaiHinhTaiSanId = 0)
        {
            var donVi = _donViService.GetDonViById(DonViId ?? 0);
            var loaiTS = new LoaiTaiSanModel();
            if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                // get tài sản vô hình, đặc thù  gốc
                decimal? parentId = LoaiTaiSanId;
                decimal? tree_level = 0;
                LoaiTaiSanDonVi taiSanDonVi = new LoaiTaiSanDonVi();
                do
                {
                    if (parentId == null)
                        break;
                    taiSanDonVi = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    tree_level = taiSanDonVi.TREE_LEVEL;
                    parentId = taiSanDonVi.PARENT_ID;
                } while (tree_level > 2);
                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                loaiTS = taiSanDonVi.ToModel<LoaiTaiSanModel>();
            }
            else
                loaiTS = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSanId ?? 0).ToModel<LoaiTaiSanModel>();
            var MaTs = "";

            if (donVi != null && loaiTS != null)
            {
                MaTs = CommonHelper.GenMaTaiSan(donVi.MA, loaiTS.MA, TaiSanId ?? 0);
            }
            return MaTs;
        }
        public void PrepareTaiSanDat(TaiSanDatModel model, TaiSanDat item)
        {
            if (model != null && item != null)
            {
                item.DIA_CHI = model.DIA_CHI;
                item.DIA_BAN_ID = model.DIA_BAN_ID;
                item.DIEN_TICH = model.DIEN_TICH;
                item.DIEN_TICH_XAY_NHA = model.DIEN_TICH_XAY_NHA;
                item.TINH_ID = model.TinhId;
                item.HUYEN_ID = model.HuyenId;
                item.XA_ID = model.XaId;

            }

        }

        public bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0)
        {
            var taisan = _taiSanService.GetTaiSanByTen(TenTS: ten, donViId: donViId);
            if (taisan != null && taisan.ID != id)
                return false;
            else return true;
        }

        public TaiSan GetTaiSanById(decimal Id)
        {
            return _taiSanService.GetTaiSanById(Id);
        }

        public TaiSanModel InsertTaiSan(TaiSanModel model)
        {
            // check đơn vị có tồn tại không
            //var donViId = _workContext.CurrentCustomer.CURRENT_DON_VI_ID;
            // gắn trực tiếp đơn vị bằng 3 sau khi có token thì sẽ lấy từ token
            var donViId = 3;
            if (donViId == null)
            {
                throw new Exception("Đơn vị không tồn tại!");
            }
            var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById((decimal)model.LOAI_TAI_SAN_ID);
            if (loaiTaiSan == null)
            {
                throw new Exception("Loại tài sản không tồn tại!");
            }
            var taiSanEntity = model.ToEntity<TaiSan>();
            taiSanEntity.TRANG_THAI_ID = (decimal?)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
            taiSanEntity.LY_DO_BIEN_DONG_ID = model.LY_DO_TANG_ID;
            taiSanEntity.NGUYEN_GIA_BAN_DAU = model.NGUYEN_GIA;
            taiSanEntity.NGAY_NHAP = model.NGAY_TANG;
            taiSanEntity.DON_VI_ID = donViId;
            // chưa có chờ dùng sau khi có các property này
            //if (item.PHUONG_THUC_MUA_SAM != null)
            //{
            //    model.PHUONG_THUC_MUA_SAM_ID = Convert.ToDecimal(item.PHUONG_THUC_MUA_SAM);
            //}
            //if (item.HINH_THUC_MUA_SAM != null)
            //{
            //    model.HinhThucMuaSamId = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(item.HINH_THUC_MUA_SAM).ID;
            //}
            //if (item.DON_VI_MUA_SAM != null)
            //{
            //    model.DON_VI_MUA_SAM_TAP_TRUNG_ID = _donViService.GetDonViByMa(item.DON_VI_MUA_SAM).ID;
            //}
            //model.NAM_SAN_XUAT = item.NAM_SX ?? 0;
            //if (item.NUOC_SX != null)
            //{
            //    model.NUOC_SAN_XUAT_ID = _quocGiaService.GetQuocGiaById(Convert.ToInt32(item.NUOC_SX)).ID;
            //}

            _taiSanService.InsertTaiSan(taiSanEntity, true);
            var donVi = _donViService.GetDonViById((decimal)donViId);
            taiSanEntity.MA = CommonHelper.GenMaTaiSan(donVi.MA, loaiTaiSan.MA, taiSanEntity.ID);
            _taiSanService.UpdateTaiSan(taiSanEntity);
            return taiSanEntity.ToModel<TaiSanModel>();

        }
    }
}
