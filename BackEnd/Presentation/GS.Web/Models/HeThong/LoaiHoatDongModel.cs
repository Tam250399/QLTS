using GS.Web.Framework.Models;

namespace GS.Web.Models.HeThong
{
    public partial class LoaiHoatDongModel : BaseGSEntityModel
    {
        #region Properties
        public string TU_KHOA_HE_THONG { get; set; }
        public string TEN { get; set; }
        public bool TINH_KHA_DUNG { get; set; }

        #endregion
    }
    public partial class LoaiHoatDongSearchModel : BaseSearchModel
    {
        public LoaiHoatDongSearchModel()
        {

        }
        public string TU_KHOA_HE_THONG { get; set; }
        public string TEN { get; set; }
        public bool TINH_KHA_DUNG { get; set; }
    }
    public partial class LoaiHoatDongListModel : BasePagedListModel<LoaiHoatDongModel>
    {

    }
}
