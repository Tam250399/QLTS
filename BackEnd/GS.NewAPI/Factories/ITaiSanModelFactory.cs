using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanModelFactory
    {
        bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0);
        void UpdateTaiSan(TaiSanModel entity);
        void UpdateTaiSan(List<TaiSanModel> entities);
    }
}
