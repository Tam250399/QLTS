using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.KhaiThacTaiSan;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common.ConsumingApi
{
    public interface IKho_TaiSanFactory
    {
        #region biến đọng tăng mới
        Task<Kho_DongBoTaiSan> GetListTaiSanDongBo(int DonViId = 0, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0, decimal DonViChaId = 0, decimal TaiSanId = 0, List<decimal> ListTaiSanId = null, List<TaiSan> _ListTaiSan = null, decimal BienDongId = 0);
        Task<List<Kho_TaiSan_BienDong>> PrepareDuLieuDongBo(TaiSanDBModel model, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0);
        #endregion
        List<Kho_KhaiThacTaiSan> PrepareDuLieuDongBoKhaiThacTaiSan(List<KhaiThacTaiSan> ListKhaiThacTaiSan, decimal LoaiKhaiThac = 0);
        Kho_DongBoTaiSan GetTaiSanDongBoByTaiSanId(decimal taiSanId = 0, decimal loaiBienDong = 0, decimal loaiHinhTaiSanId = 0);
        Data_XoaBienDong PrepareDuLieuXoaBienDong(ParameterXoaBienDong model);
        Task<Kho_TaiSan_BienDong> PrepareThongTinBienDong(BienDongDBModel item, TaiSanDBModel model, decimal LoaiBienDong = 0, decimal LoaiHinhTaiSanId = 0);
        Kho_DongBoTaiSan PrepareBienDongTangMoiCanDongBo(decimal donviId, List<BienDong> bienDongs = null, int TakeNumber = 1000);
        Kho_DongBoTaiSan PrepareBienDongCanDongBo(List<BienDong> bienDongs = null);
        //Kho_TaiSan_BienDong PrepareBienDongDongBo(DongBoApi_BienDongTaiSan item);
    }
}
