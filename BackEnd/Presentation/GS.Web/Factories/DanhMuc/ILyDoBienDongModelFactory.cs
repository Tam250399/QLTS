//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface ILyDoBienDongModelFactory
    {
        #region LyDoBienDong

        LyDoBienDongSearchModel PrepareLyDoBienDongSearchModel(LyDoBienDongSearchModel searchModel);

        LyDoBienDongListModel PrepareLyDoBienDongListModel(LyDoBienDongSearchModel searchModel);

        LyDoBienDongModel PrepareLyDoBienDongModel(LyDoBienDongModel model, LyDoBienDong item, bool excludeProperties = false);

        void PrepareLyDoBienDong(LyDoBienDongModel model, LyDoBienDong item);
        IList<SelectListItem> PrepareSelectListLyDoBienDong(decimal? valSelected = 0, decimal? loailydoId = 0, decimal? LoaiHinhTaiSanId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn lý do biến động --", bool? isTangMoi = null);
        IList<SelectListItem> PrepareSelectListLyDoBienDongByBaoCao(decimal? valSelected = 0, decimal? loailydoId = 0, decimal? LoaiHinhTaiSanId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn lý do biến động --");
        bool CheckMaLyDoBienDong(decimal id = 0, string ma = null);
        /// <summary>
        /// Check lý do biến động theo enum mã lý do
        /// </summary>
        /// <param name="id">id lý do cần check</param>
        /// <param name="ma">mã lý do enum</param>
        /// <returns></returns>
        bool CheckLyDoTheoEnum(decimal id = 0, string ma = null);
        #endregion
    }
}
