export interface Thongtinchung {
  TEN: string;
  LOAI_TAI_SAN_ID: number;
  LOAI_HINH_TAI_SAN_ID: number;
  DIEN_TICH: number;
  LOAI_TAI_SAN_DON_VI_ID: number;
  DON_VI_ID: number;
  DU_AN_ID: number;
  TRANG_THAI_ID: number;
  NGAY_TAO: string;
  NGAY_TANG: string;
  DON_VI_BO_PHAN_ID: number;
  NGAY_SU_DUNG: string;
  LY_DO_TANG_ID: number;
  MUC_DICH_ID: number;
  DIA_CHI: string;
  QUOC_GIA_ID: number;
  TINH_THANH_PHO_ID: number;
  QUAN_HUYEN_ID: number;
  XA_PHUONG_ID: number;
  NGUYEN_GIA: number;
  //Gía trị sd
  GIA_TRI_SU_DUNG_DAT: {
    GIA_TRI_QUYEN_SD_DAT: number;
    NGUON_KHAC: number;
    NGUON_NGAN_SACH: number;
  };
  //Diện tích khuôn viên
  HIEN_TRANG_SU_DUNG: {
    TRU_SO_LAM_VIEC: number; // Trụ sở làm việc (m²)
    HD_SN_KHONG_KINH_DOANH: number; // HDSN-Không KD (m²)
    HD_SD_KINH_DOANH: number; // HDSN-Kinh doanh (m²)
    HD_SD_CHO_THUE: number; // HDSN-Cho thuê (m²)
    HD_SD_KINH_DOANH_LK: number; // HDSN-LDLK (m²)
    DE_O?: number; // Để ở (m²)
    BO_TRONG: number; // Bỏ trống (m²)
    BI_LAN_CHIEM: number; // Bị lấn chiếm (m²)
    SU_DUNG_HON_HOP: number; // Sử dụng hỗn hợp (m²)
    SU_DUNG_KHAC: number; // Sử dụng khác (m²)
  };

  //Hồ sơ giấy tờ
  HO_SO_GIAY_TO: {
    CHUNG_NHAN_QUYEN_SD_DAT?: string;
    QD_GIAO_DAT?: string;
    QD_CHO_THUE_DAT?: string;
    HOP_DONG_CHO_THUE_DAT?: string;
    GIAY_TO_KHAC?: string;
    NGAY_CAP?: {
      CHUNG_NHAN_QUYEN_SD_DAT?: string;
      QD_GIAO_DAT?: string;
      QD_CHO_THUE_DAT?: string;
      HOP_DONG_CHO_THUE_DAT?: string;
      GIAY_TO_KHAC?: string;
    };
  };
}

export interface quocgia {
  MA: string;
  TEN: string;
  ID: number;
}

export interface Tinh {
  MA: string;
  TEN: string;
  ID: number;
}

export interface Huyen {
  MA: string;
  TEN: string;
  ID: number;
  MA_CHA: string;
}

export interface Phuong {
  MA: string;
  TEN: string;
  ID: number;
  MA_CHA: string;
}

export interface MucDichTS {
  MA: string;
  TEN: string;
  ID: number;
}

export interface LyDoTangDat {
  MA: string;
  TEN: string;
  ID: number;
  loaI_HINH_TAI_SAN_ID: number;
  loaI_LY_DO_ID: number;
  loaI_LY_DO_BIEN_DONG_ID: number;
}
