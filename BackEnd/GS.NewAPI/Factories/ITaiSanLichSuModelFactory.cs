using GS.Core.Domain.TaiSans;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanLichSuModelFactory
    {
        void InsertTaiSanLichSu(decimal taiSanId, decimal? nguoiTaoId, string hoatDong);
    }
}
