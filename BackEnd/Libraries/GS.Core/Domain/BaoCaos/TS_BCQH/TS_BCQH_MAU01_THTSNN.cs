using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public enum enumLoaiDonVi
    {
        CO_QUAN_NN = 11,
        DON_VI_SU_NGHIEP = 12,
        TO_CHUC = 13,
        DOANH_NGHIEP = 14,
        BAN_QUAN_LY_DU_AN = 15
    }
    public enum enumNhanKieuTaiSanBaoCao
    {
        TAI_SAN_CONG = 1,
        TAI_SAN_KET_CAU_HA_TANG = 2,
        TAI_SAN_TOAN_DAN = 3
    }
    public class TS_BCQH_MAU01_THTSNN : BaseViewEntity
    {
        public String TEN_LOAI_HINH_TAI_SAN { get; set; }
        public Decimal? KIEU_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? SO_LUONG_TRUNG_UONG { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG { get; set; }
        public Decimal? SO_LUONG_DIA_PHUONG { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG { get; set; }
        //more phục vụ đồng bộ kho
        public int? LOAI_CAP_DON_VI_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_DB_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI { get; set; }
        public Decimal? LOAI_KET_CAU_HA_TANG { get; set; }
        public Decimal? LOAI_TAI_SAN_SHTD { get; set; }


    }
}
