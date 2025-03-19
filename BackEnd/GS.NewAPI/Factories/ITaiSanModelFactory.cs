using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanModelFactory
    {
        void UpdateTaiSan(TaiSanModel entity);
        void UpdateTaiSan(List<TaiSanModel> entities);
    }
}
