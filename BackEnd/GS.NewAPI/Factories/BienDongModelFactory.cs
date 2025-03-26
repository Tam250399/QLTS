using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using GS.Services.BienDongs;
using System;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public class BienDongModelFactory : IBienDongModelFactory
    {
        private readonly IBienDongService _bienDongService;
        public BienDongModelFactory(IBienDongService bienDongService)
        {
            _bienDongService = bienDongService;
        }

        public BienDong GetBienDongCuoiByTaiSanId(decimal? taiSanId = 0)
        {
            return _bienDongService.GetBienDongCuoiByTaiSanId(taiSanId);
        }
        public IList<BienDong> GetBienDongsByTaiSanId(decimal? taiSanId = 0)
        {
            return _bienDongService.GetBienDongsByTaiSanId(taiSanId)
                //.Where( x => x.TRANG_THAI_ID == (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET)
                //.ToList()
                ;
        }

        public BienDongModel InsertToBienDong(TaiSan item, TaiSanModel tsModel, BienDongModel model)
        {
            if (item != null)
            {
                model.TAI_SAN_TEN = item.TEN;
                model.TAI_SAN_ID = item.ID;
                model.TAI_SAN_MA = item.MA;
                model.NGUYEN_GIA = item.NGUYEN_GIA_BAN_DAU;
                model.LY_DO_BIEN_DONG_ID = item.LY_DO_BIEN_DONG_ID;
                model.LOAI_TAI_SAN_ID = item.LOAI_TAI_SAN_ID;
                //set trạng thái biến động là xóa
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET;

                model.DON_VI_ID = item.DON_VI_ID;
                model.NGAY_SU_DUNG = item.NGAY_SU_DUNG;
                model.NGAY_BIEN_DONG = Convert.ToDateTime(item.NGAY_NHAP);
                model.NGAY_TAO = DateTime.Now;
                model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                model.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                model.TINH_ID = tsModel.TINH_THANH_PHO_ID;
                model.HUYEN_ID = tsModel.QUAN_HUYEN_ID;
                model.XA_ID = tsModel.XA_PHUONG_ID;
            }
            BienDong bd = new BienDong();
            bd = model.ToEntity<BienDong>();
            _bienDongService.InsertBienDong(bd);
            return model;
        }

        public BienDong UpDateBienDong( TaiSanModel tsModel, BienDong bienDong)
        {
            if (tsModel != null)
            {
                bienDong.TAI_SAN_TEN = tsModel.TEN;
                bienDong.NGUYEN_GIA = tsModel.NGUYEN_GIA;
                bienDong.LY_DO_BIEN_DONG_ID = tsModel.LY_DO_TANG_ID;
                bienDong.LOAI_TAI_SAN_ID = tsModel.LOAI_TAI_SAN_ID;
                //set trạng thái biến động là chờ duyệt
                bienDong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                bienDong.DON_VI_ID = tsModel.DON_VI_ID;
                bienDong.NGAY_SU_DUNG = tsModel.NGAY_SU_DUNG;
                bienDong.NGAY_BIEN_DONG = Convert.ToDateTime(tsModel.NGAY_TANG);
                bienDong.NGAY_TAO = DateTime.Now;
                bienDong.LOAI_HINH_TAI_SAN_ID = tsModel.LOAI_HINH_TAI_SAN_ID;
                bienDong.TINH_ID = tsModel.TINH_THANH_PHO_ID;
                bienDong.HUYEN_ID = tsModel.QUAN_HUYEN_ID;
                bienDong.XA_ID = tsModel.XA_PHUONG_ID;
            }
            _bienDongService.UpdateBienDong(bienDong);
            return bienDong;
        }

        public void UpdateBienDongs(IList<BienDong> entities)
        {
            _bienDongService.UpdateBienDong(entities);
        }
    }
}
