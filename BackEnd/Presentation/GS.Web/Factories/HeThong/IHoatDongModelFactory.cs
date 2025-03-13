using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.HeThong
{
    public partial interface IHoatDongModelFactory
    {
        IList<SelectListItem> PrepareSelectListLoaiHoatDong();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        HoatDongSearchModel PrepareHoatDongSearchModel(HoatDongSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel">Search conditions</param>
        /// <returns></returns>
        HoatDongListModel PrepareHoatDongListModel(HoatDongSearchModel searchModel);

        HoatDongModel PrepareHoatDongModel(HoatDongModel model, HoatDong item);
    }

}
