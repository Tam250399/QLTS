using System;

namespace GS.Core.Domain.HeThong
{
    /// <summary>
    /// Represents an activity log record
    /// </summary>
    public partial class HoatDong : BaseEntity
    {
        /// <summary>
        /// Gets or sets the activity log type identifier
        /// </summary>
        public decimal LOAI_HOAT_DONG_ID { get; set; }

        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string TEN_DOI_TUONG { get; set; }
        public decimal? DOI_TUONG_ID { get; set; }
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public decimal? NGUOI_DUNG_ID { get; set; }

        /// <summary>
        /// Gets or sets the activity comment
        /// </summary>
        public string MO_TA { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime? NGAY_TAO { get; set; }

        /// <summary>
        /// Gets the activity log type
        /// </summary>
        public virtual LoaiHoatDong loaihoatdong { get; set; }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual NguoiDung nguoidung { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IP_TRUY_CAP { get; set; }
        public String DU_LIEU { get; set; }
        public String SESSION_ID { get; set; }
        public String CONNECTION { get; set; }
    }
}
