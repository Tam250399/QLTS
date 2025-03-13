//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial interface INhapXuatCongCuService
    {
        #region NhapXuatCongCu
        IList<NhapXuatCongCu> GetAllNhapXuatCongCus();
        IList<NhapXuatCongCu> GetNhapXuatCongCuByNhapXuatId(decimal NhapXuatId);
        IPagedList<NhapXuatCongCu> SearchNhapXuatCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal NhapXuatId = 0);
        NhapXuatCongCu GetNhapXuatCongCuById(decimal Id);
        IList<NhapXuatCongCu> GetNhapXuatCongCuByIds(decimal[] newsIds);
        IList<NhapXuatCongCu> GetNhapXuatCongCus(decimal? CongCuId = 0, int nhapOrXuat = 0, bool isPhanBo = false, decimal? NhapXuatId = 0, decimal[] ListNhapXuatId = null, decimal TrangThai = 0, decimal DonGia = 0, decimal NhapKhoId = 0);
        NhapXuatCongCu GetNhapXuatCongCu(decimal CongCuId = 0, decimal NhapXuatId = 0);
        IPagedList<NhapXuatCongCu> GetNhapXuatCongCusByBaoHanh(Decimal DonViBoPhanId, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, string listCongCuDaChon = null, Decimal? DonViId = 0);
        IList<NhapXuatCongCu> GetXuatByCongCu(Decimal CongCuId, Decimal NhapId, Decimal? BoPhanId = 0, bool isKho = false);
        IPagedList<NhapXuatCongCu> GetByKiemKeCongCu(int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, Decimal? BoPhanId = 0);
        IPagedList<NhapXuatCongCu> SearchDieuChuyen(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        IPagedList<NhapXuatCongCu> SearchPhanBo(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        NhapXuatCongCu GetSoLuongDangCoByMapId(Decimal MapId);
        List<NhapXuatCongCu> GetMapForKiemKeCongCu(Decimal? DonViBoPhanId = 0, string KeySearch = null, DateTime? NgayKiemKe = null);
        Decimal GetSoLuongDaXuat(Decimal mapId, Boolean IsPhanBo = false);
        IPagedList<NhapXuatCongCu> SeaechForGiamDieuChuyen(Decimal LoaiDieuChuyen = 0, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal LoaiCongCu = 0, Decimal DonViBoPhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        void InsertNhapXuatCongCu(NhapXuatCongCu entity);
        void UpdateNhapXuatCongCu(NhapXuatCongCu entity);
        void DeleteNhapXuatCongCu(NhapXuatCongCu entity);
        void DeleteNhapXuatCongCus(IList<NhapXuatCongCu> entitys);
        IPagedList<NhapXuatCongCu> SearchForGiamKhac(Decimal LoaiDieuChuyen, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        #endregion
        #region read only
        List<NhapXuatCongCu> GetReadOnlyNhapXuatCongCusByMaCCDC_DB(List<string> lstMaCCDC_DB, decimal? DonViBoPhanId, DateTime? ngayKiemKe = null);
        #endregion
    }
}
