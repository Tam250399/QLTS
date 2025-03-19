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
  dienTich: string | null; // Diện tích (m²) - Bắt buộc
  hienTrangSuDung: {
    truSoLamViec: string | null; // Trụ sở làm việc (m²)
    hdSnKhongKd: string | null; // HDSN-Không KD (m²)
    hdSnKinhDoanh: string | null; // HDSN-Kinh doanh (m²)
    hdSnChoThue: string | null; // HDSN-Cho thuê (m²)
    hdSnLdLk: string | null; // HDSN-LDLK (m²)
    deO: string | null; // Để ở (m²)
    boTrong: string | null; // Bỏ trống (m²)
    biLanChiem: string | null; // Bị lấn chiếm (m²)
    suDungHonHop: string | null; // Sử dụng hỗn hợp (m²)
    suDungKhac: string | null; // Sử dụng khác (m²)
  };
  //Gía trị sd
  giaTriSdDat: {
    giaTriQSD: number;
    nguyenGia: number;
    nguonKhac: number;
    nguonNganSach: number;
  };

  //Hồ sơ giấy tờ
  hoSoGiayTo: {
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
  };
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
