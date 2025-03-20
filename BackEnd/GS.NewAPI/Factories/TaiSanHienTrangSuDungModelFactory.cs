using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.Services.TaiSans;
using System.Linq;

namespace GS.NewAPI.Factories
{
    public class TaiSanHienTrangSuDungModelFactory: ITaiSanHienTrangSuDungModelFactory
    {
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        public TaiSanHienTrangSuDungModelFactory(ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService)
        {
            _taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
        }
        public void InsertHienTrangSuDungForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang)
        {
            var lstHT = jsonHienTrang.toEntity<HienTrangList>();
            if (lstHT != null && lstHT.lstObjHienTrang != null && lstHT.lstObjHienTrang.Count > 0)
            {
                var TaiSanHienTrangSuDungs = lstHT.lstObjHienTrang.Select(p =>
                {
                    var item = new TaiSanHienTrangSuDung
                    {
                        BIEN_DONG_ID = bienDongId,
                        TAI_SAN_ID = taiSanId,
                        GIA_TRI_CHECKBOX = p.GiaTriCheckbox,
                        GIA_TRI_NUMBER = p.GiaTriNumber,
                        GIA_TRI_TEXT = p.GiaTriText,
                        TEN_HIEN_TRANG = p.TenHienTrang,
                        KIEU_DU_LIEU_ID = p.KieuDuLieuId ?? 0,
                        NHOM_HIEN_TRANG_ID = p.NhomHienTrangId,
                        HIEN_TRANG_ID = p.HienTrangId ?? 0,
                        ID = 0
                    };
                    return item;
                }).ToList();
                _taiSanHienTrangSuDungService.InsertTaiSanHienTrangSuDungs(TaiSanHienTrangSuDungs);
            }
        }
    }
}
