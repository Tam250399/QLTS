using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;

namespace GS.NewAPI.Factories
{
    public interface IBienDongChiTietModelFactory
    {
        BienDongChiTietModel InsertToBienDongChiTiet(TaiSanModel item, BienDongChiTietModel model, BienDongModel bd);
    }
}
