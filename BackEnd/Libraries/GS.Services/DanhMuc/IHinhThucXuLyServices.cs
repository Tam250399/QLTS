//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial interface IHinhThucXuLyService
    {
        #region HinhThucXuLy
        IList<HinhThucXuLy> GetAllHinhThucXuLys();
        IList<HinhThucXuLy> GetAllHinhThucXuLysChuaDb();
        IList<HinhThucXuLy> GetHinhThucXuLys(string Ma = null, int PhuongAnId = 0);
        IPagedList<HinhThucXuLy> SearchHinhThucXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        HinhThucXuLy GetHinhThucXuLyById(decimal Id);
        IList<HinhThucXuLy> GetHinhThucXuLyByIds(decimal[] newsIds);
        void InsertHinhThucXuLy(HinhThucXuLy entity);
        void UpdateHinhThucXuLy(HinhThucXuLy entity);
        void DeleteHinhThucXuLy(HinhThucXuLy entity);
        HinhThucXuLy GetHinhThucXuLyByMa(string Ma = null);
        void InsertListHinhThucXuLy(List<HinhThucXuLy> entities);
        void UpdateListHinhThucXuLy(List<HinhThucXuLy> entities);
        #endregion
    }
}
