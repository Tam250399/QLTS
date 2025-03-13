//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface IDuAnModelFactory
    {
        #region DuAn

        DuAnSearchModel PrepareDuAnSearchModel(DuAnSearchModel searchModel);

        DuAnListModel PrepareDuAnListModel(DuAnSearchModel searchModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <param name="excludeProperties"></param>
        /// <param name="isCreateChildDV"> prepare trường hợp tạo dự án kèm theo đơn vị dự án quản lý tsda</param>
        /// <returns></returns>
        DuAnModel PrepareDuAnModel(DuAnModel model, DuAn item, bool excludeProperties = false, bool? isCreateChildDV = false);

        void PrepareDuAn(DuAnModel model, DuAn item);

		bool CheckMaDuAn(decimal id = 0, string ma = null, decimal? donViID = null);
		DateTime? CheckNgayDuAn(decimal id = 0);
        IList<SelectListItem> PrepareSelectListDuAn(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "--Chọn dự án", string valueFirstRow = "", Decimal? donViId = 0);
		#endregion
	}

}
