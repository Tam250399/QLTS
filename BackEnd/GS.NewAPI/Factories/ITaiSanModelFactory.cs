using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using System.Collections.Generic;
using static GS.NewAPI.Factories.TaiSanModelFactory;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanModelFactory
    {
        bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0);
        UpdateTaiSanResult UpdateTaiSan(TaiSanModel entity);
        void UpdateTaiSan(List<TaiSanModel> entities);
    }
}
