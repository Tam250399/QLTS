using GS.Core.Domain.DanhMuc;

namespace GS.Core.Domain.HeThong
{
    public partial class NguoiDungDonViMapping : BaseEntity
    {
        public decimal NGUOI_DUNG_ID { get; set; }
        public decimal DON_VI_ID { get; set; }
        public virtual NguoiDung nguoiDung { get; set; }
        public virtual DonVi donvi { get; set; }
    }
}
