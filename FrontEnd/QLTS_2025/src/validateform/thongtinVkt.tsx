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
  MO_TA: string;

  //giá trị hao mòn
  GIA_TRI_HAO_MON: {
    NGUYEN_GIA: number;
    NGUON_KHAC: number;
    NGUON_NGAN_SACH: number;
    GIA_TRI_CON_LAI: number;
    TY_LE_HAO_MON: number;
  };
}
export interface LyDoTangDat {
  ma: string;
  ten: string;
  id: number;
  loaI_HINH_TAI_SAN_ID: number;
  loaI_LY_DO_ID: number;
  loaI_LY_DO_BIEN_DONG_ID: number;
}
export interface quocgia {
  ma: string;
  ten: string;
  id: number;
}
