using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;

namespace GS.NewAPI.Factories
{
    public interface IBienDongModelFactory
    {
        BienDongModel InsertToBienDong(TaiSan item, TaiSanModel tsModel, BienDongModel model);
        BienDong GetBienDongCuoiByTaiSanId(decimal? taiSanId = 0);
    }
}
