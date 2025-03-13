//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.TaiSans;

namespace GS.Core.Domain.DB
{
    public enum enumTrangThaiTaiSanNhatKy
    {
        /// <summary>
        /// chu dong bo
        /// </summary>
        CHUA_DONG_BO = 0,
        /// <summary>
        /// da dong bo
        /// </summary>
        DA_DONG_BO = 1,
        /// <summary>
        /// co thay doi cho dong bo lai
        /// </summary>
        CO_THAY_DOI = 2,
        /// <summary>
        /// đồng bộ thất bại
        /// </summary>
        DONG_BO_THAT_BAI = 3,
        /// <summary>
        /// đã gửi dữ liệu sang thành công nhưng chưa kiểm tra đã insert hay chưa
        /// </summary>
        CHO_DONG_BO = 4,
        /// <summary>
        /// đã đồng bộ biến động tăng mới chờ để get lại mã tài sản
        /// </summary>
        CHO_GET_MA = 5,
        /// <summary>
        /// biến động tăng mới bị bỏ duyệt thì không đồng bộ
        /// </summary>
        KHONG_DONG_BO = 6,
        LOI_DU_LIEU = 7,
        DANG_DONG_BO = 8

    }
    public partial class TaiSanNhatKy : BaseEntity
    {
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_DONG_BO { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public Decimal? QUYET_DINH_TICH_THU_ID { get; set; }
        public Boolean? IS_TAI_SAN_TOAN_DAN { get; set; }
        public String BD_IDS { get; set; }
        public String BD_IDS_DANG_DB { get; set; }
        public String RESPONSE { get; set; }
        public virtual TaiSan Taisan { get; set; }
        public String LOG_DETAIL { get; set; }
    }
}



