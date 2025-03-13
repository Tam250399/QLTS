using GS.Core;
using GS.Core.Domain.SHTD;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.SHTD
{
    public partial interface IKetQuaTaiSanServices
    {
        IList<KetQuaTaiSan> GetAllKetQuaTaiSans();
        IPagedList<KetQuaTaiSan> SearchKetQuaTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal XuLyKetQuaId = 0, bool Is_Create = false);
        KetQuaTaiSan GetKetQuaTaiSanById(decimal Id);
        IList<KetQuaTaiSan> GetKetQuaTaiSanByIds(decimal[] Ids);
        IList<KetQuaTaiSan> GetKetQuaTaiSans(decimal XuLyKetQuaId = 0, decimal TaiSanTdId = 0, decimal TaiSanTDXuLyId = 0);
        void InsertKetQuaTaiSan(KetQuaTaiSan entity);
        void UpdateKetQuaTaiSan(KetQuaTaiSan entity);
        void DeleteKetQuaTaiSan(KetQuaTaiSan entity);
    }
}
