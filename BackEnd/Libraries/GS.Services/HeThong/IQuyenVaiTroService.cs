using GS.Core.Domain.HeThong;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface IQuyenVaiTroService
    {
        void InsertQuyenVaiTro(QuyenVaiTroMapping entity);
        void DeleteQuyenVaiTro(QuyenVaiTroMapping entity);
        IList<QuyenVaiTroMapping> GetMapByVaiTroId(decimal vaiTroId);
        void DeleteQuyenVaiTroId(decimal vaiTroId, decimal quyenId);
    }
}
