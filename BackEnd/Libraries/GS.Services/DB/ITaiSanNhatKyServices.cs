//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DB;

namespace GS.Services.DB
{
    public partial interface ITaiSanNhatKyService
    {
        #region TaiSanNhatKy
        IList<TaiSanNhatKy> GetAllTaiSanNhatKys(decimal? trangThai = null, List<decimal> trangThais = null, DateTime? ngayCapNhatTu = null, DateTime? ngayCapNhatDen = null, DateTime? ngayDongBo = null);
        IPagedList<TaiSanNhatKy> SearchTaiSanNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? TrangThai = null, string MaTaiSan = null, string MaTaiSanDB = null, decimal? LoaiHinhTaiSan = null, DateTime? NgayDB = null, decimal? DonViId = null, bool? isTaiSanToanDan = null);
        IList<TaiSanNhatKy> GetTaiSanNhatKys(int? trangThai = null, int taiSanID = 0, Boolean? isTaiSanToanDan = false, int? QuyetDinhID = 0);
        TaiSanNhatKy GetTaiSanNhatKyById(decimal Id);
        IList<TaiSanNhatKy> GetTaiSanNhatKyByIds(decimal[] newsIds);
        void InsertTaiSanNhatKy(TaiSanNhatKy entity);
        void UpdateTaiSanNhatKy(TaiSanNhatKy entity);
        void UpdateTaiSanNhatKy(List<TaiSanNhatKy> entities);
        void DeleteTaiSanNhatKy(TaiSanNhatKy entity);
        TaiSanNhatKy GetByTaiSanId(decimal idTaiSan);
        TaiSanNhatKy GetByTaiSanTdId(decimal idTaiSanTd);
        TaiSanNhatKy GetByMaTaiSan(string maTaiSan);
       // void UpdateTrangThaiTaiSanNhatKy(decimal idTaiSan);
        IList<TaiSanNhatKy> GetTaiSanToaDanDB();
        TaiSanNhatKy GetTaiSanNhatKyByQuyetDinhTichThu(string SoQuyetDinh, DateTime? NgayQuyetDinh, string MaDonVi);
        TaiSanNhatKy GetTaiSanNhatKyByQuyetDinhTichThu(decimal QuyetDinhId);
        void UpdateQuyetDinhTichThuNhatKY(decimal QuyetDinhId);
        string GenArrBienDongId(string StringArrBDID = null, List<decimal> idDel = null, List<decimal> idAdd = null);
        List<TaiSanNhatKy> GetTaiSanCanDongBo(decimal donViID = 0, int TakeNumber = 1000);
        #endregion
    }
}
