//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanKhauHaoService 
    {    
    #region TaiSanKhauHao
        IList<TaiSanKhauHao> GetAllTaiSanKhauHaos();
        IPagedList<TaiSanKhauHao> SearchTaiSanKhauHaos(int pageIndex = 0, int pageSize = int.MaxValue, decimal? TaiSanId = 0);
        List<TaiSanKhauHao> GetListTaiSanKhauHaoByTaiSanId(decimal Id);
        TaiSanKhauHao GetTaiSanKhauHaoById(decimal Id);
        TaiSanKhauHao GetTaiSanKhauHaoMoiNhatByTaiSanId(decimal Id);
        TaiSanKhauHao GetTaiSanKhauHaoIdMaxByTaiSanId(decimal Id);
        TaiSanKhauHao GetListTaiSanKhauHaoByTaiSanIdAndNgayBatDau(decimal Id, DateTime? ngaybatdau);
        IList<TaiSanKhauHao> GetTaiSanKhauHaoByIds(decimal[] newsIds);
        void InsertTaiSanKhauHao(TaiSanKhauHao entity);
        void UpdateTaiSanKhauHao(TaiSanKhauHao entity);
        void DeleteTaiSanKhauHao(TaiSanKhauHao entity);
        bool CheckTsKhauHaobyIdandNgay(decimal? id, DateTime? NgayTinhKhauHao);
        bool CheckTrungNgayBatDau(DateTime? ngaybatdau, decimal taiSanid = 0, decimal taiSanKhauHaoId = 0);
        int CountTaiSanKhauHao(decimal tai_san_id = 0, DateTime? ngaytinhkhauhao = null);
     #endregion
    }
}
