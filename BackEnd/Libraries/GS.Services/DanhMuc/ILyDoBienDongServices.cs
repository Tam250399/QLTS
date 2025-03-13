//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface ILyDoBienDongService
    {
        #region LyDoBienDong
        IList<LyDoBienDong> GetAllLyDoBienDongs();
        IList<LyDoBienDong> GetAllLyDoBienDongsChuaDb();
        IList<LyDoBienDong> GetLyDoBienDongs(decimal? LoaiHinhTaiSanId = 0, decimal? loailydoId = 0);
        IPagedList<LyDoBienDong> SearchLyDoBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? loaiHinhTSId = -1, Decimal? loaiLyDoId = 0, string strLoaiHinhTSIds = null);
        LyDoBienDong GetLyDoBienDongById(decimal Id);
        IList<LyDoBienDong> GetLyDoBienDongByIds(decimal[] newsIds);
        void InsertLyDoBienDong(LyDoBienDong entity);
        void InsertLyDoBienDong(List<LyDoBienDong> entities);
        void UpdateLyDoBienDong(LyDoBienDong entity);
        void UpdateLyDoBienDong(List<LyDoBienDong> entities);
        void DeleteLyDoBienDong(LyDoBienDong entity);
        LyDoBienDong GetLyDoBienDongByMa(string Ma, int loai_hinh_tai_san = 0);
        LyDoBienDong GetLyDoBienDongByTEN_LOAI_HINH_TS(string ten, int loai_hinh_tai_san);
        bool CheckMaLyDoBienDong(decimal id = 0, string ma = null);
		List<LyDoBienDong> GetLyDoBienDongByLoaiLyDoBienDong(string MA_LOAI_LY_DO_BIEN_DONG);
		decimal? GetIdLyDoBienDongByMa(string MA_LY_DO_BIEN_DONG);
		#endregion
	}
}
