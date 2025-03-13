using GS.Core;
using GS.Core.Domain.SHTD;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.SHTD
{
    public partial interface IXuLyKetQuaServices
    {
        IList<XuLyKetQua> GetAllXuLyKetQuas();
        IPagedList<XuLyKetQua> SearchXuLyKetQuas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        XuLyKetQua GetXuLyKetQuaById(decimal Id);
        IList<XuLyKetQua> GetXuLyKetQuaByIds(decimal[] Ids);
        IList<XuLyKetQua> GetXuLyKetQuas();
        void InsertXuLyKetQua(XuLyKetQua entity);
        void UpdateXuLyKetQua(XuLyKetQua entity);
        void DeleteXuLyKetQua(XuLyKetQua entity);
    }
}
