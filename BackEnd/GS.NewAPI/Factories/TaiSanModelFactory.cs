using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;
using System;

namespace GS.NewAPI.Factories
{
    public class TaiSanModelFactory : ITaiSanModelFactory
    {
        private readonly ITaiSanService _taiSanService;
        private readonly IWorkContext _workContext;
        private readonly IDonViService _donViService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ITaiSanDatService _taiSanDatService;
        public TaiSanModelFactory(
            ITaiSanService taiSanService, 
            IWorkContext workContext, 
            IDonViService donViService,
            ILoaiTaiSanService loaiTaiSanService,
            ITaiSanDatService taiSanDatService)
        {
            _taiSanService = taiSanService;
            _workContext = workContext;
            _donViService = donViService;
            _loaiTaiSanService = loaiTaiSanService;
            _taiSanDatService = taiSanDatService;
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
