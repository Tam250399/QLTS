using GS.Core.Domain.TaiSans;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models;
using GS.Services.TaiSans;
using System;

namespace GS.NewAPI.Factories
{
    public class TaiSanLichSuModelFactory: ITaiSanLichSuModelFactory
    {
        private readonly ITaiSanLichSuService _taiSanLichSuService;
        public TaiSanLichSuModelFactory(ITaiSanLichSuService taiSanLichSuService)
        {
            _taiSanLichSuService = taiSanLichSuService;
        }

        public void InsertTaiSanLichSu(decimal taiSanId, decimal? nguoiTaoId, string hoatDong)
        {
            //add theo kiểu map model
            //var model = new TaiSanLichSuModel()
            //{
            //    TAI_SAN_ID = taiSanId,
            //    NGUOI_TAO_ID = nguoiTaoId,
            //    HOAT_DONG = hoatDong,
            //    NGAY_TAO = DateTime.Now,
            //};
            //var taiSanLichSu = model.ToEntity<TaiSanLichSu>();
            var taiSanLichSu = new TaiSanLichSu()
            {
               // ID = 0,
                TAI_SAN_ID = taiSanId,
                NGUOI_TAO_ID = nguoiTaoId,
                HOAT_DONG = hoatDong,
                NGAY_TAO = DateTime.Now,
            };
            _taiSanLichSuService.InsertTaiSanLichSu(taiSanLichSu);
        }
    }
}
