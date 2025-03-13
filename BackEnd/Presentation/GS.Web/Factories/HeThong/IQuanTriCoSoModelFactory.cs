using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.HeThong
{
    public partial interface IQuanTriCoSoModelFactory
    {
        void ChuanBiLoaiHoatDong(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null);
    }
}
