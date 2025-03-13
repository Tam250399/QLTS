using GS.Core;
using GS.Core.Domain.TaiSans;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanHienTrangSuDungService
    {
        #region TaiSanHienTrangSuDung
        IList<TaiSanHienTrangSuDung> GetAllTaiSanHienTrangSuDungs();
        IPagedList<TaiSanHienTrangSuDung> SearchTaiSanHienTrangSuDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);

        List<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByBienDongId(decimal BienDongId);
        TaiSanHienTrangSuDung GetTaiSanHienTrangSuDungById(decimal Id);
        List<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByTaiSanId(decimal taiSanId);
        IList<TaiSanHienTrangSuDung> GetTaiSanHienTrangSuDungByIds(decimal[] newsIds);
        void InsertTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity);
        void InsertTaiSanHienTrangSuDungs(List<TaiSanHienTrangSuDung> entities);
        void UpdateTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity);
        void DeleteTaiSanHienTrangSuDung(TaiSanHienTrangSuDung entity);
        void DeleteTaiSanHienTrangSuDungs(IList<TaiSanHienTrangSuDung> entities);
        IList<TaiSanHienTrangSuDung> GetHienTrangSuDungs(decimal TaiSanId = 0, decimal BienDongId = 0);
        #endregion
    }
}
