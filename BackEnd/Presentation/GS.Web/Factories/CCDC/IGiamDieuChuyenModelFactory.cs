using GS.Web.Models.CCDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.CCDC
{
    public partial interface IGiamDieuChuyenModelFactory
    {
        GiamDieuChuyenListModel PrepareGiamDieuChuyenListModel(GiamDieuChuyenSearchModel searchModel);
        GiamDieuChuyenListModel PrepareForChonCongCu(GiamDieuChuyenSearchModel searchModel);
        GiamDieuChuyenModel PrepareDieuChuyen(string StringId, bool whenEdit = false);
        GiamDieuChuyenListModel PrepareGiamKhacListModel(GiamKhacSearchModel searchModel);
        GiamDieuChuyenSearchModel PrepareCongCuSearchModel(GiamDieuChuyenSearchModel searchModel);
    }
}
