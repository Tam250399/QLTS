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
  dienTich: string; // Diện tích (m²) - Bắt buộc
  hienTrangSuDung: {
    truSoLamViec: string; // Trụ sở làm việc (m²)
    hdSnKhongKd: string; // HDSN-Không KD (m²)
    hdSnKinhDoanh: string; // HDSN-Kinh doanh (m²)
    hdSnChoThue: string; // HDSN-Cho thuê (m²)
    hdSnLdLk: string; // HDSN-LDLK (m²)
    deO: string; // Để ở (m²)
    boTrong: string; // Bỏ trống (m²)
    biLanChiem: string; // Bị lấn chiếm (m²)
    suDungHonHop: string; // Sử dụng hỗn hợp (m²)
    suDungKhac: string; // Sử dụng khác (m²)
  };
  //Gía trị sd
  giaTriSdDat: {
    giaTriQSD: number;
    nguyenGia: number;
    nguonKhac: number;
  };
}
