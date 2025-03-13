namespace GS.Core.Domain.HeThong
{
    public partial class QuyenVaiTroMapping : BaseEntity
    {
        public decimal VAI_TRO_ID { get; set; }
        public decimal QUYEN_ID { get; set; }
        public virtual VaiTro vaiTro { get; set; }
        public virtual Quyen quyen { get; set; }
    }
}
