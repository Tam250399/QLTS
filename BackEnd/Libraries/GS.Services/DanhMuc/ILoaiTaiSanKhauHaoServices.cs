//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
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
    public partial interface ILoaiTaiSanKhauHaoService 
    {    
    #region LoaiTaiSanKhauHao
        IList<LoaiTaiSanKhauHao> GetAllLoaiTaiSanKhauHaos();
        IPagedList <LoaiTaiSan> SearchLoaiTaiSanKhauHaos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0);
        /// <summary>
        /// Description:Lấy bản ghi loại tài sản khấu hao theo loại tài sản id và đơn vị id
        /// </summary>
        /// <param name="loaiTaiSanId"></param>
        /// <param name="donViId"></param>
        /// <returns></returns>
        LoaiTaiSanKhauHao GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(decimal? loaiTaiSanId = 0, decimal? donViId = 0);
        LoaiTaiSanKhauHao GetLoaiTaiSanKhauHaoById(decimal Id);
        IList<LoaiTaiSanKhauHao> GetLoaiTaiSanKhauHaoByIds(decimal[] newsIds);
        void InsertLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity);
        void UpdateLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity);
        void DeleteLoaiTaiSanKhauHao(LoaiTaiSanKhauHao entity);    
     #endregion
	}
}
