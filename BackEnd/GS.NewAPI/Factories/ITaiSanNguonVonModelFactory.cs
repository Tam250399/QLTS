using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanNguonVonModelFactory
    {
        void InsertTaiSanNguonVonFromBienDong(TaiSanModel model, BienDongModel bd);
        void UpDateTaiSanNguonVonFromBienDong(TaiSanModel model, List<TaiSanNguonVon> tsnv, BienDongModel bd);
    }
}
