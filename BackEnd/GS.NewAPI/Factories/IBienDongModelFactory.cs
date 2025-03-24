using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface IBienDongModelFactory
    {
        BienDongModel InsertToBienDong(TaiSan item, TaiSanModel tsModel, BienDongModel model);
        BienDong UpDateBienDong(TaiSanModel tsModel, BienDong bienDong);
        BienDong GetBienDongCuoiByTaiSanId(decimal? taiSanId = 0);
        IList<BienDong> GetBienDongsByTaiSanId(decimal? taiSanId = 0);
        void UpdateBienDongs(IList<BienDong> entities);
    }
}
