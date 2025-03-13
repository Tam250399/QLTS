namespace GS.Core.Domain.HeThong
{
    /// <summary>
    /// Represents an activity log type record
    /// </summary>
    public partial class LoaiHoatDong : BaseEntity
    {
        /// <summary>
        /// Gets or sets the system keyword
        /// </summary>
        public string TU_KHOA_HE_THONG { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        public string TEN { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activity log type is enabled
        /// </summary>
        public bool TINH_KHA_DUNG { get; set; }
    }
}
