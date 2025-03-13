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
    public partial interface ILoaiDonViModelFactory
    {
        #region LoaiDonVi

        LoaiDonViSearchModel PrepareLoaiDonViSearchModel(LoaiDonViSearchModel searchModel);

        LoaiDonViListModel PrepareLoaiDonViListModel(LoaiDonViSearchModel searchModel);

        LoaiDonViModel PrepareLoaiDonViModel(LoaiDonViModel model, LoaiDonVi item, bool excludeProperties = false);

        void PrepareLoaiDonVi(LoaiDonViModel model, LoaiDonVi item);


        IList<SelectListItem> PrepareSelectListLoaiDonVi(decimal? ValSelected = 0, bool isAddFirst = false, string valueFirst = "");

        /// <summary>
        ///  Phục vụ chọn loại đối tác từ  "Loại hình Đơn Vị"  (riêng "Tổ chức" chọn đến bậc 2; các loại hình khác chọn bậc 1)
        /// </summary>
        /// <param name="ValSelected"></param>
        /// <param name="isAddFirst"></param>
        /// <param name="valueFirst"></param>
        /// <returns></returns>
        IList<SelectListItem> PrepareSelectListLoaiDoiTac(decimal? ValSelected = 0, bool isAddFirst = false, string valueFirst = "");
        bool CheckMaLoaiDonVi(decimal id, string ma);
        IList<SelectListItem> PrepareMultiSelectLoaiDonVi(IList<int> valSelecteds = null, bool addItemAll = true);
        IList<SelectListItem> PrepareMultiSelectLoaiDonViForBaoCao(IList<int> valSelecteds = null);

        #endregion
    }
}
