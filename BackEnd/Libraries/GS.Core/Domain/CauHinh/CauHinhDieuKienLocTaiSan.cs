using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.CauHinh
{
	public enum ENUMLOAI_DU_LIEU_DIEU_KIEN_LOC_TS
	{
		//All =0,
		String = 1,
		Number = 2,
		Dropdownlist = 3
	}
	#region Danh mục điều kiện lọc
	/// <summary>
	/// class lưu vào bảng cấu hình
	/// </summary>
	public class CauHinhDieuKienLocTaiSan : ICauHinh
	{
		public string JsonValue { get; set; }
	}
	/// <summary>
	/// class chi tiết
	/// </summary>
	public class DieuKienLocTaiSan : BaseEntity
	{
		public string MA_LOC_TAI_SAN { get; set; }
		public int? LOAI_DU_LIEU_ID { get; set; }
		public string TEN_HIEN_THI { get; set; }
		public string VALUE { get; set; }
	} 
	#endregion
	public class ListCauHinhCanhBaoTaiSan:ICauHinh
	{
		/// <summary>
		/// Là một danh sách CauHinhCanhBaoTaiSan dạng json
		/// </summary>
		public string JsonValue { get; set; }
	}
	public class CauHinhCanhBaoTaiSan:ICauHinh
	{
		public string MA { get; set; }
		public string SelectStatementQuery { get; set; }
		/// <summary>
		/// Là một danh sách WhereStatement dạng json
		/// </summary>
		public string WhereStatementJson { get; set; }
		public string ActionUrl { get; set; }
		public string DisplayName { get; set; }
		/// <summary>
		/// Để đếm số tài sản thỏa mãn
		/// </summary>
		public int CountAlert { get; set; }
        /// <summary>
        /// Set có ẩn hiển thị số lượng thỏa mãn hay không
        /// </summary>
        public bool IsHideCount { get; set; }
    }
	public class WhereStatement
	{
		public string TEN_TRUONG { get; set; }
		public string MA_DIEU_KIEN { get; set; }
		public string GIA_TRI_SO_SANH { get; set; }
	}
	/// <summary>
	/// Chứa displayname của cảnh báo địa bàn để lấy tên trong nhãn hiển thị
	/// </summary>
	public static class CauHinhCanhBaoDiaBan
	{
        static CauHinhCanhBaoDiaBan()
        {
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.capnhatdiaban";
			ActionUrl = $"/TaiSan/ListTaiSanThayDoiDiaBan";
			StoreName = "TAI_SAN_THAY_DOI_DIA_BAN";

		}
		public static string DisplayName { get; set; }
        public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }
	}

	public static class CauHinhCanhBaoTaiSanDuoi10Trieu
	{
		static CauHinhCanhBaoTaiSanDuoi10Trieu()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.taisannguyengiaduoi10trieu";
			ActionUrl = $"/TaiSan/ListTaiSanNguyenGiaDuoi10Trieu";
			StoreName = "TAI_SAN_NGUYEN_GIA_DUOI_10_TRIEU";

		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }
	}
	public static class CauHinhCanhBaoHienTrang
	{
		static CauHinhCanhBaoHienTrang()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.capnhathientrang";
			ActionUrl = $"/TaiSan/ListTaiSanSaiHienTrang";
			StoreName = "TAI_SAN_SAI_HIEN_TRANG";
		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }	

	}
	public static class CauHinhCanhBaoLogicChung
	{
		static CauHinhCanhBaoLogicChung()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.CauHinhCanhBaoLogicChung";
			ActionUrl = $"/TaiSan/CanhBaoChung";
			/// Bao gồm: cảnh báo hiện trạng, cảnh báo địa bàn, cảnh báo nguyên giá dưới 10tr
			/// Không hiển thị do nếu count sẽ bị chậm khi đăng nhập
			Count = 3;
			CountStore = "SP_COUNT_";
			GetStore = "SP_GET_";
			DPAC = "_DPAC";
		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static int Count { get; set; }
		/// Dùng chung để quy ước tên store 
		public static string CountStore { get; set; }
		public static string GetStore { get; set; }
		public static string DPAC { get; set; }

		public static string GetStoreName(bool isCount = true, bool isDpac = false, string storeName = null)
		{
				return $"{(isCount?CountStore:GetStore)}{storeName}{(isDpac?DPAC:"")}";
		}

	}

	public static class CauHinhCanhBaoTaiSanDieuChuyen
	{
		static CauHinhCanhBaoTaiSanDieuChuyen()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.canhbaotaisannhandieuchuyen";
			ActionUrl = $"/TaiSan/ListTaiSanNhanDieuChuyen";
			StoreName = "TAI_SAN_DIEU_CHUYEN";
		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }

	}

	public static class CauHinhCanhBaoOtoSaiChoNgoi
	{
		static CauHinhCanhBaoOtoSaiChoNgoi()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.cauhinhcanhbaootosaichongoi";
			ActionUrl = $"/TaiSan/ListTaiSanOtoSaiSoChoNgoi";
			StoreName = "OTO_SAI_SO_CHO_NGOI";
		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }

	}

	public static class CauHinhCanhBaoTaiSanChuaTinhHaoMon
	{
		static CauHinhCanhBaoTaiSanChuaTinhHaoMon()
		{
			DisplayName = $"cauhinh.cauhinhcanhbaotaisan.cauhinhcanhbaotaisanchuatinhhaomon";
			ActionUrl = $"/TaiSan/ListTaiSanChuaTinhHaoMon";
			StoreName = "TAI_SAN_CHUA_TINH_HAO_MON";
		}
		public static string DisplayName { get; set; }
		public static string ActionUrl { get; set; }
		public static string StoreName { get; set; }

	}
}
