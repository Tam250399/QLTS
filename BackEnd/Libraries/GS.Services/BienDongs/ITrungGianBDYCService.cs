using GS.Core;
using GS.Core.Domain.BienDongs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BienDongs
{
	public partial interface ITrungGianBDYCService
	{
        IPagedList<TrungGianBDYC> SearchYeuCauVaBienDong(int pageIndex = 0, int pageSize = int.MaxValue,
                                        string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
                                        decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
                                        decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
                                         IList<decimal?> loaiBienDongIds = null, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
                                         decimal? taisanId = 0, bool isIgnoreTraLai = false, string quyetDinhSo = null, bool isDuyet = false);
        IPagedList<TrungGianBDYC> SearchYCBDThanhLyChuaCapNhatTien(int pageIndex = 0, int pageSize = int.MaxValue,
                                        string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
                                        decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
                                        decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
                                         decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
                                         decimal? taisanId = 0, bool isIgnoreTraLai = false, string quyetDinhSo = null, bool isDuyet = false);
        /// <summary>
        /// lấy ra biến động gần nhất, nếu không có biến động thì lấy ra yêu cầu
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <returns></returns>
        TrungGianBDYC GetYCBDGanNhat(decimal TaiSanId);
        List<TrungGianBDYC> GetAllYCBD(decimal TaiSanId);
        List<TrungGianBDYC> GetAllYCBDKhacTuChoi(decimal TaiSanId);
        TrungGianBDYC GetYCBDorYCById(int ID, int BDorYC);
        /// <summary>
		/// Trả về HTSD_Json của tài sản
		/// Lấy theo
		///     - Biến động gần nhất
		///     - Nếu không có biến động thì lấy yêu cầu gần nhất
		/// </summary>
		/// <param name="taiSanId"></param>
		/// <returns></returns>
        string GetHTSD_JSON_of_TS(decimal taiSanId);
        int CountBDYCTaiSan(decimal taiSanId);
		decimal GetGTCLGanNhatDaDuyet(decimal taiSanId);
        decimal SumHTSDDienTich(decimal taiSanId);

    }
}
