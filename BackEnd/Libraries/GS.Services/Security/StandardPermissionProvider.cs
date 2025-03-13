using GS.Core.Domain.HeThong;

namespace GS.Services.Security
{
	/// <summary>
	/// Standard permission provider
	/// </summary>
	public partial class StandardPermissionProvider : IPermissionProvider
	{
		//App works HeThong

		#region Hethong

		public static readonly Quyen ADMINTruyCapUngDung = new Quyen { TEN = "Hệ thống quản trị", MA = "ADMIN.TruyCapUngDung", PHAN_HE = "HeThong" };
		public static readonly Quyen ADMINQLNhatKy = new Quyen { TEN = "Nhật ký hệ thống", MA = "ADMIN.QLNhatKy", PHAN_HE = "HeThong" };
		public static readonly Quyen ADMINQLNhatKyHoatDong = new Quyen { TEN = "Quản lý nhật ký hoạt động người dùng", MA = "ADMIN.ADMINQLNhatKyHoatDong", PHAN_HE = "HeThong" };
		public static readonly Quyen ADMINQLLoaiHoatDong = new Quyen { TEN = "Quản lý loại hoạt động người dùng", MA = "ADMIN.ADMINQLLoaiHoatDong", PHAN_HE = "HeThong" };

		#endregion Hethong

		#region Cau hinh

		public static readonly Quyen ADMINQLCauHinh = new Quyen { TEN = "Cấu hình hệ thống", MA = "ADMIN.QLCauHinh", PHAN_HE = "HeThong" };
		public static readonly Quyen ADMINQLNhanHienThi = new Quyen { TEN = "Quản lý nhãn hiển thị", MA = "ADMIN.QLNhanHienThi", PHAN_HE = "HeThong" };
		public static readonly Quyen ADMINQLDinhMuc = new Quyen { TEN = "Quản lý định mức tài sản", MA = "ADMIN.QLDinhMuc", PHAN_HE = "HeThong" };

		#endregion Cau hinh

		#region NguoiDung

		public static readonly Quyen ADMINQLQuyen = new Quyen { TEN = "Quản lý quyền", MA = "ADMIN.QLQuyen", PHAN_HE = "NguoiDung" };
		public static readonly Quyen ADMINQLVaiTro = new Quyen { TEN = "Quản lý vai trò", MA = "ADMIN.QLVaiTro", PHAN_HE = "NguoiDung" };
		public static readonly Quyen ADMINQLNguoiDung = new Quyen { TEN = "Quản lý người dùng", MA = "ADMIN.QLNguoiDung", PHAN_HE = "NguoiDung" };
		public static readonly Quyen ADMINQLNguoiDungDonVi = new Quyen { TEN = "Quản lý người dùng", MA = "ADMIN.QLNguoiDungDonVi", PHAN_HE = "NguoiDung" };
		public static readonly Quyen ADMINQLDuyetNguoiDung = new Quyen { TEN = "Duyệt người dùng", MA = "ADMIN.QLDuyetNguoiDung", PHAN_HE = "NguoiDung" };

		#endregion NguoiDung

		#region Quan trị đơn vị - các quyền áp dụng cho cán bộ quản trị hệ thống cấp đơn vị

		public static readonly Quyen USERCauHinhBaoCao = new Quyen { TEN = "Cấu hình báo cáo", MA = "USER.CauHinhBaoCao", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERCauHinhBaoCaoDB = new Quyen { TEN = "Cấu hình báo cáo đồng bộ", MA = "USER.CauHinhBaoCaoDB", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERQLNguoiDungNoiBo = new Quyen { TEN = "Quản lý người dùng nội bộ", MA = "USER.QLNguoiDungNoiBo", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERQLDonViBoPhan = new Quyen { TEN = "Quản lý phòng ban", MA = "USER.QLDonViBoPhan", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERQLDanhMucDuAn = new Quyen { TEN = "Quản lý dự án", MA = "USER.QLDanhMucDuAn", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERQLDanhMucDoiTac = new Quyen { TEN = "Quản lý đối tác", MA = "USER.QLDanhMucDoiTac", PHAN_HE = "QuanTriDonVi" };
		public static readonly Quyen USERQLDanhMucLoaiTaiSanKhauHao = new Quyen { TEN = "Quản lý loại tài sản khấu hao", MA = "USER.QLDanhMucLoaiTaiSanKhauHao", PHAN_HE = "DanhMuc" };

		#endregion Quan trị đơn vị - các quyền áp dụng cho cán bộ quản trị hệ thống cấp đơn vị

		#region Danhmuc

		public static readonly Quyen ADMINQLDonVi = new Quyen { TEN = "Quản lý đơn vị", MA = "ADMIN.QLDonVi", PHAN_HE = "DanhMuc" };
		public static readonly Quyen ADMINQLUngDung = new Quyen { TEN = "Quản lý ứng dụng", MA = "ADMIN.QLUngDung", PHAN_HE = "DanhMuc" };
		public static readonly Quyen ADMINQLDiaBan = new Quyen { TEN = "Quản lý địa bàn", MA = "ADMIN.QLDiaBan", PHAN_HE = "DanhMuc" };
		public static readonly Quyen USERQLDanhMuc = new Quyen { TEN = "Quản lý danh mục", MA = "USER.QLDanhMuc", PHAN_HE = "DanhMuc" };
		public static readonly Quyen USERQLDanhSachDanhMuc = new Quyen { TEN = "Danh sách danh mục", MA = "USER.QLDanhSachDanhMuc", PHAN_HE = "DanhMuc" };
		public static readonly Quyen USERQLNhomCongCu = new Quyen { TEN = "Quản lý Nhóm công cụ", MA = "USER.QLNhomCongCu", PHAN_HE = "DanhMuc" };



		public static readonly Quyen USERQLDMQuocGia = new Quyen { TEN = "Quản lý danh mục quốc gia", MA = "USER.QLDMQuocGia", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMLoaiTaiSan = new Quyen { TEN = "Quản lý danh mục loại tài sản", MA = "USER.QLDMLoaiTaiSan", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMLoaiTaiSanToanDan = new Quyen { TEN = "Quản lý danh mục loại tài sản toàn dân", MA = "USER.QLDMLoaiTaiSanToanDan", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMLoaiTaiSanVoHinh = new Quyen { TEN = "Quản lý danh mục loại tài sản vô hình", MA = "USER.QLDMLoaiTaiSanVoHinh", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMCheDoHaoMon = new Quyen { TEN = "Quản lý danh mục chế độ khấu hao", MA = "USER.QLDMCheDoHaoMon", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMLoaiDonVi = new Quyen { TEN = "Quản lý danh mục loại đơn vị", MA = "USER.QLDMLoaiDonVi", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMDongXe = new Quyen { TEN = "Quản lý danh mục dòng xe", MA = "USER.QLDMDongXe", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMNhanXe = new Quyen { TEN = "Quản lý danh mục nhãn xe", MA = "USER.QLDMNhanXe", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMChucDanh = new Quyen { TEN = "Quản lý danh mục chức danh", MA = "USER.QLDMChucDanh", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMNguonVon = new Quyen { TEN = "Quản lý danh mục nguồn vốn", MA = "USER.QLDMNguonVon", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMHienTrang = new Quyen { TEN = "Quản lý danh mục hiện trạng", MA = "USER.QLDMHienTrang", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMMucDichSuDung = new Quyen { TEN = "Quản lý danh mục đích sử dụng", MA = "USER.QLDMMucDichSuDung", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMHinhThucMuaSam = new Quyen { TEN = "Quản lý danh mục hình thức mua sắm", MA = "USER.QLDMHinhThucMuaSam", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMLyDoBienDong = new Quyen { TEN = "Quản lý danh mục lý do biến động", MA = "USER.QLDMLyDoBienDong", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMHinhThucXuLy = new Quyen { TEN = "Quản lý danh mục hình thức xử lý", MA = "USER.QLDMHinhThucXuLy", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMPhuongAnXuLy = new Quyen { TEN = "Quản lý danh mục phương án xử lý", MA = "USER.QLDMPhuongAnXuLy", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMNguonGocTaiSan = new Quyen { TEN = "Quản lý danh mục nguồn gốc tài sản", MA = "USER.QLDMNguonGocTaiSan", PHAN_HE = "USER" };
		public static readonly Quyen USERQLDMWidget = new Quyen { TEN = "Quản lý danh mục widget", MA = "USER.USERQLDMWidget", PHAN_HE = "USER" };

		#endregion Danhmuc

		#region Công Cụ Dụng cụ

		public static readonly Quyen USERQLCongCuDungCu = new Quyen { TEN = "Công cụ dụng cụ", MA = "USER.QLCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLNhapLoCongCuDungCu = new Quyen { TEN = "Nhập lô công cụ dụng cụ", MA = "USER.QLNhapLoCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLPhanBoCongCuDungCu = new Quyen { TEN = "Phân bổ công cụ dụng cụ", MA = "USER.QLPhanBoCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLSuaChuaCongCuDungCu = new Quyen { TEN = "Sửa chữa công cụ dụng cụ", MA = "USER.QLSuaChuaCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLChoThueCongCuDungCu = new Quyen { TEN = "Cho thuê công cụ dụng cụ", MA = "USER.QLChoThueCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLLuanChuyenCongCuDungCu = new Quyen { TEN = "Luân chuyển công cụ dụng cụ", MA = "USER.QLLuanChuyenCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLKiemKeCongCuDungCu = new Quyen { TEN = "Kiểm kê công cụ dụng cụ", MA = "USER.QLKiemKeCongCuDungCu", PHAN_HE = "CCDC" };
		public static readonly Quyen QLGiamCongCuDungCu = new Quyen { TEN = "Giảm công cụ dụng cụ", MA = "USER.QLGiamCongCuDungCu", PHAN_HE = "CCDC" };
        public static readonly Quyen QLCongCuTaiSanChuaTinhHaoMon = new Quyen { TEN = "Lọc tài sản chưa tính hao mòn", MA = "USER.QLCongCuTaiSanChuaTinhHaoMon", PHAN_HE = "CCDC" };
        #endregion Công Cụ Dụng cụ

        #region TaiSan

        public static readonly Quyen USERQLTraCuuTaiSan = new Quyen { TEN = "Tra cứu tài sản", MA = "USER.TraCuuTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLXoaTaiSan = new Quyen { TEN = "Xóa tài sản", MA = "USER.XoaTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLTaiSan = new Quyen { TEN = "Quản lý tài sản", MA = "USER.QLTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLDieuChinhHaoMon = new Quyen { TEN = "Điều chỉnh hao mòn", MA = "USER.DieuChinhHaoMon", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLDieuChinhKhauHao = new Quyen { TEN = "Điều chỉnh khấu hao", MA = "USER.DieuChinhKhauHao", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDNhapSoDu = new Quyen { TEN = "Nhập số dư đầu kỳ", MA = "USER.QLBDNhapSoDu", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDNhapSoDu500Trieu = new Quyen { TEN = "Nhập số dư ban đầu Tài sản khác dưới 500 triệu đồng", MA = "USER.QLBDNhapSoDu500Trieu", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDTangMoiTaiSanDuoi500Tr = new Quyen { TEN = "Tăng mới tài sản khác dưới 500 triệu đồng", MA = "USER.QLBDTangMoiTaiSanDuoi500Tr", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDTangMoiTsDacBiet = new Quyen { TEN = "Tăng mới tài sản đặc biệt", MA = "USER.QLBDTangMoiTsDacBiet", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDThayDoiThongTin = new Quyen { TEN = "Thay đổi thông tin", MA = "USER.QLBDThayDoiThongTin", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDTangGiamHangNam = new Quyen { TEN = "Tăng, giảm hàng năm", MA = "USER.QLBDBDTangGiamHangNam", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDTangNguyenGia = new Quyen { TEN = "Tăng nguyên giá", MA = "USER.QLBDBDTangNguyenGia", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDGiamNguyenGia = new Quyen { TEN = "Giảm nguyên giá", MA = "USER.QLBDBDGiamNguyenGia", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDCapNhatTien = new Quyen { TEN = "Cập nhật số tiền bán, thanh lý", MA = "USER.QLBDCapNhatTien", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDDieuChuyenMotPhan = new Quyen { TEN = "Điều chuyển một phần", MA = "USER.QLBDDieuChuyenMotPhan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDGiamTaiSan = new Quyen { TEN = "Giảm tài sản", MA = "USER.QLBDBDGiamTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBDChoThueTaiSan = new Quyen { TEN = "Cho thuê tài sản", MA = "USER.QLBDBDChoThueTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLKiemKeTaiSan = new Quyen { TEN = "Kiểm kê tài sản", MA = "USER.QLKiemKeTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLDuyetDangKy = new Quyen { TEN = "Duyệt đăng ký", MA = "USER.QLDuyetDangKy", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLBoDuyetDangKy = new Quyen { TEN = "Bỏ duyệt đăng ký", MA = "USER.QLBoDuyetDangKy", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLCongCu = new Quyen { TEN = "Công cụ", MA = "USER.QLCongCuTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLDeNghiXuLy = new Quyen { TEN = "Đề nghị xử lý tài sản", MA = "USER.QLDeNghiXuLy", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLSoTaiSan = new Quyen { TEN = "Thiết lập sổ tài sản", MA = "USER.QLSoTaiSan", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLKeHoachMuaSam = new Quyen { TEN = "Kế hoạch mua sắm", MA = "USER.QLKeHoachMuaSam", PHAN_HE = "TaiSan" };

		#region Quan Ly Tai San Toan Dan

		public static readonly Quyen USERQLTaiSanToanDan = new Quyen { TEN = "Quản lý tài sản toàn dân", MA = "USER.QLTaiSanToanDan", PHAN_HE = "TaiSanToanDan" };
		public static readonly Quyen USERQLTSTDXuLy = new Quyen { TEN = "Xử lý tài sản toàn dân", MA = "USER.QLTSTDXuLy", PHAN_HE = "TaiSanToanDan" };
		public static readonly Quyen USERQLTSTDQuyetDinhTichThu = new Quyen { TEN = "Quản lý quyết định tịch thu tài sản toàn dân", MA = "USER.QLTSTDQuyetDinhTichThu", PHAN_HE = "TaiSanToanDan" };
		public static readonly Quyen USERQLDuyetQuyetDinhTichThu = new Quyen { TEN = "Quản lý duyệt quyết định tịch thu tài sản toàn dân", MA = "USER.QLDuyetQuyetDinhTichThu", PHAN_HE = "TaiSanToanDan" };
		public static readonly Quyen USERQuanLyThuChi = new Quyen { TEN = "Quản lý thu chi tài sản toàn dân", MA = "USER.USERQuanLyThuChi", PHAN_HE = "TaiSanToanDan" };

		#endregion Quan Ly Tai San Toan Dan

		#region Khai Thac - Khai thac tai san

		public static readonly Quyen USERQLKhaiThac = new Quyen { TEN = "Khai thác", MA = "USER.QLKhaiThac", PHAN_HE = "TaiSan" };
		public static readonly Quyen USERQLKhaiThacTaiSan = new Quyen { TEN = "Khai Thác tài sản", MA = "USER.QLKhaiThacTaiSan", PHAN_HE = "TaiSan" };

		#endregion Khai Thac - Khai thac tai san

		#endregion TaiSan

		#region Bao Cao

		public static readonly Quyen USERQLBaoCao = new Quyen { TEN = "Báo cáo", MA = "USER.QLBaoCao", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoTSC = new Quyen { TEN = "Báo cáo tài sản công", MA = "USER.QLBaoCaoTSC", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoCu = new Quyen { TEN = "Báo cáo cũ", MA = "USER.QLBaoCaoOld", PHAN_HE = "BaoCao" };
		//---------------------------------------------------------
		public static readonly Quyen USERQLBaoCaoChiTietTaiSan = new Quyen { TEN = "Báo cáo chi tiết tài sản", MA = "USER.QLBaoCaoCTTS", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoTongHopTaiSan = new Quyen { TEN = "Báo cáo tổng hợp tài sản", MA = "USER.QLBaoCaoTHTS", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoQuocHoiTaiSan = new Quyen { TEN = "Báo cáo quốc hội tài sản", MA = "USER.QLBaoCaoQHTS", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoCongKhaiTaiSan = new Quyen { TEN = "Báo cáo công khai tài sản", MA = "USER.QLBaoCaoCKTS", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoKeKhaiTaiSan = new Quyen { TEN = "Báo cáo kê khai tài sản", MA = "USER.QLBaoCaoKKTS", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoCheDoKeToan = new Quyen { TEN = "Báo cáo chế độ kế toán", MA = "USER.QLBaoCaoCheDoKeToan", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoTraCuuSoLieu = new Quyen { TEN = "Báo cáo tra cứu số liệu", MA = "USER.QLBaoCaoTraCuuSolieu", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoCungCapThongTinTaiChinh = new Quyen { TEN = "Báo cáo cung cấp thông tin tài chính", MA = "USER.QLBaoCaoCCTTTC", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoDuAn = new Quyen { TEN = "Báo cáo dự án", MA = "USER.QLBaoCaoDuAn", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoQLDA = new Quyen { TEN = "Báo cáo ban quản lý dự án", MA = "USER.QLBaoCaoBQLDA", PHAN_HE = "BaoCao" };
		public static readonly Quyen USERQLBaoCaoDoiChieuDuLieu = new Quyen { TEN = "Báo cáo đối chiếu dữ liệu", MA = "USER.QLBaoCaoDoiChieuData", PHAN_HE = "BaoCao" };
		//thêm quyền bc tổng hợp bc 8a, 8b cho đơn vị nhập liệu
		public static readonly Quyen USERQLBaoCaoTongHopTS = new Quyen { TEN = "Báo cáo tổng hợp", MA = "USER.QLBaoCaoTongHopTS", PHAN_HE = "BaoCao" };

		//---------------------------------------------------------
		public static readonly Quyen USERQLBaoCaoCCDC = new Quyen { TEN = "Báo cáo công cụ dụng cụ", MA = "USER.QLBaoCaoCCDC", PHAN_HE = "BaoCao" };
		//---------------------------------------------------------
		public static readonly Quyen USERQLBaoCaoTSTD = new Quyen { TEN = "Báo cáo tài sản toàn dân", MA = "USER.QLBaoCaoTSTD", PHAN_HE = "TaiSanToanDan" };

		#endregion Bao Cao
		#region Tool
		public static readonly Quyen USERQLCongCuTimKiemDongBo = new Quyen { TEN = "Tìm kiếm đồng bộ", MA = "USER.QLCongCuTimKiemDongBo", PHAN_HE = "CongCu" };
		public static readonly Quyen USERQLCongCuChuyenQuyenXuLyTaiSan = new Quyen { TEN = "Chuyển quyền xử lý tài sản", MA = "USER.QLCongCuChuyenQuyenXuLyTaiSan", PHAN_HE = "CongCu" };
		public static readonly Quyen USERQLCongCuKiemTraLoaiHinhDonVi = new Quyen { TEN = "Kiểm tra loại hình đơn vị", MA = "USER.QLCongCuKiemTraLoaiHinhDonVi", PHAN_HE = "CongCu" };
		#endregion
		public static readonly Quyen USERQLHoSo = new Quyen { TEN = "Quản lý hồ sơ", MA = "USER.QLHoSo", PHAN_HE = "HoSo" };
		public static readonly Quyen USERXacNhanDuLieu = new Quyen { TEN = "Xác nhận dữ liệu", MA = "USER.XacNhanDuLieu", PHAN_HE = "HoSo" };
		public static readonly Quyen USERBaoCaoDienTu = new Quyen { TEN = "Báo cáo điện tử", MA = "USER.BaoCaoDienTu", PHAN_HE = "HoSo" };
	}
}