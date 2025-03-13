//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Services.DanhMuc
{
    public partial interface ILoaiTaiSanDonViServices
    {    
    #region LoaiTaiSanVoHinh
        IList<LoaiTaiSanDonVi> GetAllLoaiTaiSanVoHinhs();
        IPagedList<LoaiTaiSanDonVi> SearchLoaiTaiSanVoHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? PARENTID = 0, decimal? forDonVi = null, decimal? TREELEVEL = null);
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhById(decimal Id);
        IList<LoaiTaiSanDonVi> GetLoaiTaiSanVoHinhByIds(decimal[] newsIds);
        void InsertLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity);
        void InsertLoaiTaiSanVoHinh(List<LoaiTaiSanDonVi> entities);
        void UpdateLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity);
        void DeleteLoaiTaiSanVoHinh(LoaiTaiSanDonVi entity);
        IList<LoaiTaiSanDonVi> GetForInputSearch(string KeySearch = null, decimal? donViId = 0);
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMa(string ma);
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaAndDonVi(string ma, decimal donViID);//truyền vào dvID
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaAndDonVi(string ma);
        int GetCountSub(decimal? ParentId = 0, decimal? donViId = 0);
        IList<LoaiTaiSanDonVi> GetListLoaiTaiSanVoHinhTreeNodeByRoot(decimal? cheDoHaoMonId = 0, decimal? LoaiHinhTaiSanId = 0, Decimal? donViId = 0);
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByTen(string Ten);
        IList<LoaiTaiSanDonVi> GetAllLoaiTaiSanVoHinhCons();
        LoaiTaiSanDonVi GetLoaiTaiSanVoHinhByMaLTS(string maLts);
        #endregion
        #region LoaiTaiSanDacThu
        /// <summary>
        /// Check xem đơn vị có loại tài sản đặc thù không True là có , false là không có
        /// </summary>
        /// <returns></returns>
        bool CheckLoaiTaiSanDacThu(decimal? donViId = 0);
        #endregion
    }
}
