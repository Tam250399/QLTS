using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public partial interface ILoaiHoatDongModelFactory
    {
        #region LoaiHoatDong
        LoaiHoatDongSearchModel PrepareLoaiHoatDongSearchModel(LoaiHoatDongSearchModel searchModel);

        LoaiHoatDongListModel PrepareLoaiHoatDongListModel(LoaiHoatDongSearchModel searchModel);

        LoaiHoatDongModel PrepareLoaiHoatDongModel(LoaiHoatDongModel model, LoaiHoatDong item, bool excludeProperties = false);

        void PrepareLoaiHoatDong(LoaiHoatDongModel model, LoaiHoatDong item);
        bool CheckTrungMa(string Ma, decimal id = 0);
        #endregion
    }
}
