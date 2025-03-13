//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
using System;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface INguoiDungService
    {
        #region NguoiDung
        IList<NguoiDung> GetAllNguoiDungs(int take = 0);
        IList<NguoiDung> GetAllNguoiDungsChuaDb();
        IList<NguoiDung> GetAllNguoiDungByDonViId(decimal DonViId);
        IPagedList<NguoiDung> SearchNguoiDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string Tendaydu = null, string Macanbo = null, decimal DonViId = 0, decimal[] DonViIds = null, string MaVaiTro = null, IList<int> ListNguoiDungDaChon = null, decimal idVaiTro = 0, decimal donViId = 0, decimal donViBoTinhId = 0, decimal donViHuyenXaId = 0, decimal? nguoiTaoId = null);
        IPagedList<NguoiDung> SearchDuyetNguoiDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string Tendaydu = null, string Macanbo = null, decimal TrangThaiId = 0, decimal DonViId = 0, decimal[] DonViIds = null, string MaVaiTro = null, IList<int> ListNguoiDungDaChon = null, decimal idVaiTro = 0, decimal donViId = 0, decimal donViBoTinhId = 0, decimal donViHuyenXaId = 0, decimal? nguoiTaoId = null);
        NguoiDung GetNguoiDungById(decimal? Id);
        NguoiDung GetNguoiDungByMa(string Ma);
        void InsertNguoiDung(NguoiDung entity, decimal? DonViId = null);
        void InsertNguoiDungV2(NguoiDung entity, List<decimal> ListDonViId = null);
        void UpdateNguoiDung(NguoiDung entity);
        void DeleteNguoiDung(NguoiDung entity);
        NguoiDung GetNguoiDungByUsername(string username);
        NguoiDungCache ToNguoiDungCache(NguoiDung nguoiDung);
        NguoiDung GetNguoiDungByEmail(string email);
        NguoiDungKetQuaDangNhap ValidateNguoiDung(string username, string password);
        NguoiDung GetNguoiDungByGuid(Guid guid);
        IList<NguoiDung> GetNguoiDungByListMaCanBo(decimal DonViId, List<string> lstMaCanBo);

        IList<NguoiDung> GetAllNguoiDungByDonViIdAndMaVaiTro(decimal DonViId, string MaVaitro);
        bool KiemTraTrungMa(string MA_CAN_BO, decimal Id);
        bool KiemTraTrungTen(string TEN_DANG_NHAP, decimal Id);
        IList<NguoiDung> GetNguoiDungByVaiTroId(decimal vaiTro);
        NguoiDungKetQuaDangNhap validateResetUser(string username);
        void InsertListNguoiDung(List<NguoiDung> entities);
        void UpdateListNguoiDung(List<NguoiDung> entities);
        IList<NguoiDung> getNguoiDungByDonVi(decimal donViId);
        Boolean XoaNguoiDungChuaDuyet(decimal? NguoiDungId = 0);
        #endregion
    }
}
