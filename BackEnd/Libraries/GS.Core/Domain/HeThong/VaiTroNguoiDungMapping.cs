namespace GS.Core.Domain.HeThong
{
    public partial class VaiTroNguoiDungMapping : BaseEntity
    {
        public decimal VAI_TRO_ID { get; set; }
        public decimal NGUOI_DUNG_ID { get; set; }
        public virtual VaiTro vaiTro { get; set; }
        public virtual NguoiDung nguoiDung { get; set; }
    }
}
