using DevExpress.DataAccess.Native;
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
        public TaiSanModel UpdateTaiSan(TaiSanModel model)
        {
            var item = _taiSanService.GetTaiSanById(model.ID ?? 0);
            if (item == null)
            {
                throw new Exception("Tài sản không tồn tại!");
            }
            model.NGUOI_TAO_ID = item.NGUOI_TAO_ID;
            if (item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET ||
                item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                {
                    throw new Exception("Tài sản đã duyệt không thể sửa!");

                }
            }
            model.NGAY_TAO = DateTime.Now;
            //if (entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
            //{
            //    entity.TEN = _taiSanOtoModelFactory.genTenTaiSanOto(entity.taisanOtoModel.NHAN_XE_ID, entity.taisanOtoModel.DONG_XE_ID, entity.taisanOtoModel.BIEN_KIEM_SOAT);
            //}

            //if (entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || entity.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            //    entity.MA = LoadMaTaiSan(_workContext.CurrentDonVi.ID, entity.ID, entity.LOAI_TAI_SAN_DON_VI_ID, entity.LOAI_HINH_TAI_SAN_ID);
            //else
            //    entity.MA = LoadMaTaiSan(0, entity.ID, entity.LOAI_TAI_SAN_ID, entity.LOAI_HINH_TAI_SAN_ID);
            item.TEN= model.TEN;
            //item = model.ToEntity<TaiSan>();
            _taiSanService.UpdateTaiSan(item);
            //update session
            //var bienDongTruoc = _bienDongService.GetBienDongCuoiByTaiSanId(model.ID);
            ////insert biendong

            ////khởi tạo biến động từ yêu cầu
            //var biendong = _bienDongService.GetBienDongById(model.ID ?? 0);
            //if (biendong != null)
            //{
            //    biendong.NGAY_DUYET = DateTime.Now;
            //    biendong.NGUOI_DUYET_ID = _workContext.CurrentCustomer.ID;
            //    biendong.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.DA_DUYET;
            //}
            ////gán lại giá trị mặc định
        

            ////nếu có biến động trước là thay đổi thông tin
            ////gán lại giá trị đã thay đổi của biến động
            ////_bienDongModelFactory.PrepareBienDongFromBDTDTT(biendong, bienDongTruoc);

            //////khởi tạo biến động chi tiết từ yêu cầu chi tiết
            ////var yeucauchitiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            ////var yeucauchitietModel = yeucauchitiet.ToModel<YeuCauChiTietModel>();
            ////var biendongchitiet = yeucauchitietModel.ToEntity<BienDongChiTiet>();
            //////gán giá trị diện tích sang bảng biến động
            ////biendong.DAT_TONG_DIEN_TICH = biendongchitiet.DAT_TONG_DIEN_TICH;
            ////biendong.NHA_TONG_DIEN_TICH_XD = biendongchitiet.NHA_TONG_DIEN_TICH_XD;
            ////biendong.VKT_DIEN_TICH = biendongchitiet.VKT_DIEN_TICH;

            //////hiện trạng của biến động chi tiết
            ////if (biendongchitiet.HTSD_JSON == null) biendongchitiet.HTSD_JSON = _trungGianBDYCService.GetHTSD_JSON_of_TS(ts.ID);

            //////tính lại giá trị còn lại của biến động
            ////_bienDongModelFactory.TinhGiaTriConLaiBienDong(biendong, biendongchitiet);
            //_bienDongService.UpdateBienDong(biendong);
            return item.ToModel<TaiSanModel>();
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
        public void PrepareTaiSanDat(TaiSanModel model, TaiSanDat item)
        {
            if (model != null && item != null)
            {
                item.DIA_CHI = model.DIA_CHI;
                item.DIA_BAN_ID = model.XA_PHUONG_ID;
                item.DIEN_TICH = model.DIEN_TICH ?? 0;
                //item.DIEN_TICH_XAY_NHA = model.DIEN_TICH_XAY_NHA;
                item.TINH_ID = model.TINH_THANH_PHO_ID;
                item.HUYEN_ID = model.QUAN_HUYEN_ID;
                item.XA_ID = model.XA_PHUONG_ID;

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
        public void UpdateTaiSan(TaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _taiSanService.UpdateTaiSan(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

    }
}
