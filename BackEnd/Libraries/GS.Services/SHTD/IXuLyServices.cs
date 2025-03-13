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
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial interface IXuLyService 
    {    
    #region XuLy
        IList<XuLy> GetAllXuLys();
        IPagedList <XuLy> SearchXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int LoaiXuLy = 0, string SoQuyetDinh = "", DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, DateTime? NgayXuLyTu = null, DateTime? NgayXuLyDen = null, int LoaiTaiSan = 0, int HinhThucXuLy = 0, int PhuongAnXuLy = 0, decimal DonViId = 0);
        XuLy GetXuLyById(decimal Id);
        IList<XuLy> GetXuLyByIds(IList<decimal> newsIds);
        void InsertXuLy(XuLy entity);
        void UpdateXuLy(XuLy entity);
        void DeleteXuLy(XuLy entity);
        XuLy GetXuLyByGuid(Guid Guid);
        XuLy GetXuLyByDB_Id(string DB_Id);
        IList<XuLy> GetPhuongAnXuLyTaiSans(decimal DonViId);
        IList<XuLy> GetKetQuaXuLyTaiSans(decimal DonViId);
        IList<XuLy> GetAllXuLyChuaDongBo();
     #endregion
    }
}
