using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.CauHinh
{
	public class CauHinhSoTaiSan:ICauHinh
	{
		public string KhoaSoHeThong { get; set; }
	}
	public enum enumTrangThaiNamTaiSan
	{
		OPEN = 0,
		CLOSE = 1
	}
	public class TrangThaiNam : ICauHinh
	{
		public decimal Nam { get; set; }
		public decimal TrangThai { get; set; }
		public DateTime? NgayKhoaSo { get; set; }
	}
}
