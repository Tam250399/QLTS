using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanModelFactory
    {
        bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0);
        TaiSanModel InsertTaiSan(TaiSanModel model);
        TaiSan GetTaiSanById(decimal Id);
        void UpdateTaiSan(TaiSan entity);
    }
}
