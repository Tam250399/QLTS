using GS.Core.Domain.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.DB
{
    public interface ITempDongBoTaiSanCuService
    {
        IList<TempDongBoTaiSanCu> GetAllTempDongBoTaiSanCus();
        IList<TempDongBoTaiSanCu> GetTempDongBoTaiSanCus(decimal? TrangThaiId = null, DateTime? NgayTao = null, decimal LoaiHinhTaiSanId = 0, decimal LoaiBienDong = 0, decimal DonViId = 0);
        void InsertTempDongBoTaiSanCu(TempDongBoTaiSanCu entity);
        void UpdateTempDongBoTaiSanCu(TempDongBoTaiSanCu entity);
        void DeleteTempDongBoTaiSanCu(TempDongBoTaiSanCu entity);


    }
}
