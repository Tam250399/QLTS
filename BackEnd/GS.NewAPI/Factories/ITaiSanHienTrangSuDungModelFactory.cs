using DevExpress.Office.Utils;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanHienTrangSuDungModelFactory
    {
        void InsertHienTrangSuDungForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang);
        void UpDateHienTrangSuDungForBienDong(BienDong bienDong, List<TaiSanHienTrangSuDung> tshtsd, string jsonHienTrang);

    }
}
