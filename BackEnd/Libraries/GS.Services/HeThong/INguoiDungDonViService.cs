using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface INguoiDungDonViService
    {
        void InsertNguoiDungDonVi(NguoiDungDonViMapping entity);
        void DeleteNguoiDungDonVi(NguoiDungDonViMapping entity);
        IList<NguoiDungDonViMapping> GetMapByNguoiDungId(decimal nguoiDungId);
        IList<NguoiDungDonViMapping> GetMapByDonViId(decimal DonViId);

        bool KiemTraDaChon(decimal NguoiDungId, decimal DonViId);
        NguoiDungDonViMapping GetNguoiDungDonViMapping(decimal NguoiDungId, decimal DonViId);
        void DeleteNguoiDungDonViMapping(decimal DonViId = 0, decimal NguoiDungId = 0);
        IList<DonVi> GetDonViByNguoiDungId(decimal nguoiDungId);
        void DeleteNguoiDungDonVi(List<NguoiDungDonViMapping> entity);
    }
}
