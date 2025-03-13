using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCQH
{
    public class TS_BCQH_PL02_TANG_GIAM_TSNN:BaseViewEntity
    {
        //trung uong đầu kỳ
        public Decimal? SO_LUONG_TRUNG_UONG_DK { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG_DK { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG_DK { get; set; }
        //trung uong tăng trong kỳ
        public Decimal? SO_LUONG_TRUNG_UONG_TTK { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG_TTK { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG_TTK { get; set; }
        //trung uong giảm trong kỳ
        public Decimal? SO_LUONG_TRUNG_UONG_GTK { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG_GTK { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG_GTK { get; set; }
        //trung uong cuoi ky
        public Decimal? SO_LUONG_TRUNG_UONG_CK { get; set; }
        public Decimal? DIEN_TICH_TRUNG_UONG_CK { get; set; }
        public Decimal? NGUYEN_GIA_TRUNG_UONG_CK { get; set; }
        //địa phương đầu kỳ
        public Decimal? SO_LUONG_DIA_PHUONG_DK { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG_DK { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG_DK { get; set; }
        //dia phuong tăng trong kỳ
        public Decimal? SO_LUONG_DIA_PHUONG_TTK { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG_TTK { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG_TTK { get; set; }
        //dia phuon giảm trong kỳ
        public Decimal? SO_LUONG_DIA_PHUONG_GTK { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG_GTK { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG_GTK { get; set; }
        //dia phuon cuoi ky
        public Decimal? SO_LUONG_DIA_PHUONG_CK { get; set; }
        public Decimal? DIEN_TICH_DIA_PHUONG_CK { get; set; }
        public Decimal? NGUYEN_GIA_DIA_PHUONG_CK { get; set; }
        //
        public String TEN_LOAI_HINH_DON_VI { get; set; }
        public Decimal? LOAI_HINH_DON_VI { get; set; }
        public String TEN_LOAI_HINH_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }     
        //tổng cộng đầu kỳ
        public Decimal? TONG_SO_LUONG_DK { get; set; }
        public Decimal? TONG_DIEN_TICH_DK { get; set; }
        public Decimal? TONG_NGUYEN_GIA_DK { get; set; }
        //tổng cộng tăng trong kỳ
        public Decimal? TONG_SO_LUONG_TTK { get; set; }
        public Decimal? TONG_DIEN_TICH_TTK { get; set; }
        public Decimal? TONG_NGUYEN_GIA_TTK { get; set; }
        //tổng cộng giảm trong kỳ
        public Decimal? TONG_SO_LUONG_GTK { get; set; }
        public Decimal? TONG_DIEN_TICH_GTK { get; set; }
        public Decimal? TONG_NGUYEN_GIA_GTK { get; set; }
        //tổng cộng cuối kỳ
        public Decimal? TONG_SO_LUONG_CK { get; set; }
        public Decimal? TONG_DIEN_TICH_CK { get; set; }
        public Decimal? TONG_NGUYEN_GIA_CK { get; set; }
    }
}
