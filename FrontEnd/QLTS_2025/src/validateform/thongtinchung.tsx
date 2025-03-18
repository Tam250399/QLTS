export interface Thongtinchung {
  diachi: string | null;
  quocgia: string | null;
  tinhthanhpho: string | null;
  quanhuyen: string | null;
  xaphuong: string | null;
  lydotang: string | null;
  mucdich: string | null;
  ngaytang: Date;
  //Diện tích khuôn viên
  dienTich: number; // Diện tích (m²) - Bắt buộc
  hienTrangSuDung: {
    truSoLamViec: number; // Trụ sở làm việc (m²)
    hdSnKhongKd: number; // HDSN-Không KD (m²)
    hdSnKinhDoanh: number; // HDSN-Kinh doanh (m²)
    hdSnChoThue: number; // HDSN-Cho thuê (m²)
    hdSnLdLk: number; // HDSN-LDLK (m²)
    deO: number; // Để ở (m²)
    boTrong: number; // Bỏ trống (m²)
    biLanChiem: number; // Bị lấn chiếm (m²)
    suDungHonHop: number; // Sử dụng hỗn hợp (m²)
    suDungKhac: number; // Sử dụng khác (m²)
  };
  //Gía trị sd
  giaTriSdDat: {
    giaTriQSD: number;
    nguyenGia: number;
    nguonKhac: number;
    nguonNganSach: number;
  };

  //Hồ sơ giấy tờ
  hoSoGiayTo:{
    CNQSD?: string;
    quyetDinhGiaoDat?: string;
    quyetDinhChoThueDat?: string;
    hopDongChoThueDat?: string;
    giayToKhac?: string;
    ngayCap?: {
      CNQSD?: string;
      quyetDinhGiaoDat?: string;
      quyetDinhChoThueDat?: string;
      hopDongChoThueDat?: string;
      giayToKhac?: string;
    };
  }
}

export interface quocgia {
  ma: string;
  ten: string;
  id: number;
}

export interface Tinh {
  ma: string;
  ten: string;
  id: number;
}

export interface Huyen {
  ma: string;
  ten: string;
  id: number;
  mA_CHA: string;
}

export interface Phuong {
  ma: string;
  ten: string;
  id: number;
  mA_CHA: string;
}

export interface MucDichTS {
  ma: string;
  ten: string;
  id: number;
}

export interface LyDoTangDat {
  ma: string;
  ten: string;
  id: number;
  loaI_HINH_TAI_SAN_ID: number;
  loaI_LY_DO_ID: number;
  loaI_LY_DO_BIEN_DONG_ID: number;
}
