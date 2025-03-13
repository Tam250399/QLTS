using GS.Core.Domain.HeThong;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface IVaiTroNguoiDungService
    {
        void InsertVaiTroNguoiDung(VaiTroNguoiDungMapping entity);
        void DeleteVaiTroNguoiDung(VaiTroNguoiDungMapping entity);
        IList<VaiTroNguoiDungMapping> GetMapByNguoiDungId(decimal nguoiDungId);
        IList<VaiTroNguoiDungMapping> GetMapByVaiTroId(decimal VaiTroId);
        IList<VaiTroNguoiDungMapping> GetMapByMaVaiTro(string MaVaiTro);
        bool KiemTraDaChon(decimal VaiTroId, decimal NguoiDungId);
        void DeleteVaiTroNguoiDung(decimal vaiTroId, decimal nguoiDungId);
        void DeleteVaiTroNguoiDung(List<VaiTroNguoiDungMapping> entity);
    }
}
