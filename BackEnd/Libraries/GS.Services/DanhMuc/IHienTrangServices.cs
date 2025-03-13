//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface IHienTrangService
    {
        #region HienTrang
        IList<HienTrang> GetHienTrangs(decimal? LoaiHinhTsId = 0, bool? isTSDA = null);
        IList<HienTrang> GetHienTrangsChuaDb();
        IPagedList<HienTrang> SearchHienTrangs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? LoaiHinhTS = 0, Decimal? kieuDuLieuID = 0, Decimal? nhomHienTrangId = 0);
        IList<HienTrang> ListHienTrangsByFields(Decimal? id = 0, string tenHienTrang = null, Decimal? loaiHinhTSId = 0, Decimal? kieuDuLieuID = 0, Decimal? nhomHienTrangId = 0);
        HienTrang GetHienTrangById(decimal Id);
        IList<HienTrang> GetHienTrangByIds(decimal[] newsIds);
        void InsertHienTrang(HienTrang entity);
        void UpdateHienTrang(HienTrang entity);
        void DeleteHienTrang(HienTrang entity);
        void InsertListHienTrang(List<HienTrang> entities);
        void UpdateListHienTrang(List<HienTrang> entities);
        HienTrang GetHienTrangByIdDB(int Id_DB=0, decimal Id=0 );
        bool CheckHienTrangTheoLoaiDonVi(decimal donViID, decimal HienTrangId);

        /// <summary>
        /// Truyền vào List tài sản hiện trạng  và donViID, check xem list hiện trạng đó có bất kỳ hiện trạng nào bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="listTaiSanHienTrang"></param>
        /// <returns></returns>
        bool CheckIsListHienTrangNhapSai(decimal donViID, IList<TaiSanHienTrangSuDung> listTaiSanHienTrang = null);
        /// <summary>
        /// Truyền vào hiện trạng hiện trạng và donViID, check xem hiện trạng đó có bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="ObjHienTrang"></param>
        /// <returns></returns>
        bool CheckIsHienTrangNhapSai(decimal donViID, TaiSanHienTrangSuDung ObjHienTrang = null);
        #endregion
    }
}
