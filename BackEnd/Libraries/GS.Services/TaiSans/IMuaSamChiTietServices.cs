//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
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
    public partial interface IMuaSamChiTietService 
    {    
    #region MuaSamChiTiet
        IList<MuaSamChiTiet> GetAllMuaSamChiTiets();
        IList<MuaSamChiTiet> GetMuaSamChiTiets(decimal LoaiTaiSanId = 0, decimal? LoaiHinhTaiSanId = 0);
        IPagedList <MuaSamChiTiet> SearchMuaSamChiTiets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal? muaSamId = 0);
        MuaSamChiTiet GetMuaSamChiTietById(decimal Id);
        IList<MuaSamChiTiet> GetMapByMuaSamId(decimal muaSamId);
        IList<MuaSamChiTiet> GetMuaSamChiTietByIds(decimal[] newsIds);
        void InsertMuaSamChiTiet(MuaSamChiTiet entity);
        void UpdateMuaSamChiTiet(MuaSamChiTiet entity);
        void DeleteMuaSamChiTiet(MuaSamChiTiet entity);
        void DeleteMuaSamChiTietsByMuaSamId(decimal muaSamId);
     #endregion
    }
}
