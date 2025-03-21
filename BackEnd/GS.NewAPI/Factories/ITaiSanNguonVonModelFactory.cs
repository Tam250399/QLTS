using GS.Core.Domain.BienDongs;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanNguonVonModelFactory
    {
        void InsertTaiSanNguonVonFromBienDong(TaiSanModel model, BienDongModel bd);
    }
}
