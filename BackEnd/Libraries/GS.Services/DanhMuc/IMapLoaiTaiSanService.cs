using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.DanhMuc
{
    public partial interface IMappingLoaiTaiSanService
    {
        #region MappingLoaiTaiSan      
        /// <summary>
        /// map loai tai san 2 thong tu 45 va 162
        /// </summary>
        /// <param name="OldLoaiTaiSanId">Loai tai san o thong tu 162</param>
        /// <param name="NewLoaiTaiSanId">Loai tai san o thong tu 45</param>
        /// <returns></returns>
        MappingLoaiTaiSan GetMappingLoaiTaiSanById(decimal OldLoaiTaiSanId=0, decimal NewLoaiTaiSanId = 0);
        #endregion
    }
}
