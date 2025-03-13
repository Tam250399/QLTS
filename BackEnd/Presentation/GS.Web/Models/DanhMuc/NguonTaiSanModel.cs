using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.DanhMuc
{
    public class NguonTaiSanModel : BaseGSEntityModel
    {
        public NguonTaiSanModel()
        {

        }
        public String TEN { get; set; }
        public decimal? NGUOI_DUNG_ID { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }

       
    }
    public partial class NguonTaiSanSearchModel : BaseSearchModel
    {
        public NguonTaiSanSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class NguonTaiSanListModel : BasePagedListModel<NguonTaiSanModel>
    {

    }
}
