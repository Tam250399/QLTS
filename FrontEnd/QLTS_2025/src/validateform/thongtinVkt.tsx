export interface HienTrangSuDung {
  TRU_SO_LAM_VIEC: number;
  HD_SN_KHONG_KINH_DOANH: number;
  HD_SD_KINH_DOANH: number;
  HD_SD_KINH_DOANH_LK: number;
  HD_SD_CHO_THUE: number;
  SU_DUNG_KHAC: number;
}

export interface ThongtinchungVkt {
  TEN: string;
  LY_DO_TANG_ID: number;
  LOAI_TAI_SAN_ID: number;
  CHIEU_DAI: number;
  DON_VI_ID: number;
  NUOC_SX: string;
  NGAY_DU_VAO_SD: string;
  NGAY_TANG: string;
  BO_PHAN_ID: number;
  DIEN_TICH: number;
  NAM_SX: number;
  THE_TICH: number;
  MO_TA: string;
  HIEN_TRANG_SU_DUNG: HienTrangSuDung;
}
export interface LyDoTangDat {
  MA: string;
  TEN: string;
  ID: number;
  loaI_HINH_TAI_SAN_ID: number;
  loaI_LY_DO_ID: number;
  loaI_LY_DO_BIEN_DONG_ID: number;
}
export interface quocgia {
  MA: string;
  TEN: string;
  ID: number;
}
