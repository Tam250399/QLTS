using GS.Core.Configuration;

namespace GS.Core.Domain.CauHinh
{
    public class XacThucChungThucSoCauHinh : ICauHinh
    {
        //Kiểm tra xác thực chữ ký số:
        public bool KiemTraXacThuChuKySo { get; set; }
        public bool KiemTraXacThuChuKySoTacNghiep { get; set; }

    }
}
