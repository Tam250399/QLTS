//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial interface IDeNghiXuLyTaiSanService
    {
        #region DeNghiXuLyTaiSan
        IList<DeNghiXuLyTaiSan> GetAllDeNghiXuLyTaiSans(Decimal? DeNghiXuLyID = null);
        IPagedList<DeNghiXuLyTaiSan> SearchDeNghiXuLyTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? DE_NGHI_XU_LY_ID = null);
        DeNghiXuLyTaiSan GetDeNghiXuLyTaiSanById(decimal Id);
        IList<DeNghiXuLyTaiSan> GetDeNghiXuLyTaiSanByIds(decimal[] newsIds);
        void InsertDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity);
        void UpdateDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity);
        void DeleteDeNghiXuLyTaiSan(DeNghiXuLyTaiSan entity);
        void DeleteDeNghiXuLyTaiSan(List<DeNghiXuLyTaiSan> entity);
        #endregion
    }
}
