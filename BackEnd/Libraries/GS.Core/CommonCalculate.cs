using System;
namespace GS.Core
{
    public static class CommonCalculate
    {
        /// <summary>
        ///Description: Ham tinh ty le khau hao theo thoi han su dung(thang)
        /// </summary>
        /// <param name="numberOfMonth">Thời hạn sử dụng</param>
        /// <returns></returns>
        public static Decimal? TyLeKhauHao(Decimal? numberOfMonth = 0)
        {
            if (numberOfMonth > 0)
            {
                return 1 / numberOfMonth;
            }
            return 0;
        }
        /// <summary>
        /// Description: Tính  Giá trị/khoảng thời gian.vd: KH/tháng, HM/năm,...
        /// </summary>
        /// <param name="tyLe">Tỷ lệ khấu hao or Tỷ lệ hao mòn</param>
        /// <param name="giaTriTS">giá trị tài sản cần khấu hao</param>
        /// <returns></returns>
        public static Decimal? ValuePerTime(Decimal? tyLe = 0, Decimal? giaTriTS = 0)
        {
            return tyLe * giaTriTS;
        }

        /// <summary>
        /// Description: Tính lũy kế
        /// </summary>
        /// <param name="thoiGian">thời gian tính lũy kế</param>
        /// <param name="valuePerTime">giá trị/đơn vị thời gian</param>
        /// <returns></returns>
        public static Decimal? TinhLuyKe(Decimal? thoiGian = 0, Decimal? valuePerTime = 0)
        {
            return thoiGian * valuePerTime;
        }

        /// <summary>
        /// Description: Tính giá trị trích KH, tính HM
        /// </summary>
        /// <param name="tyLePhanBo">tỷ lệ trích KH, tính hao mòn</param>
        /// <param name="giaTriTS">Giá trị tại thời điêm tính</param>
        /// <returns></returns>        
        public static Decimal? PhanBoGiaTri(Decimal? tyLePhanBo = 0, Decimal? giaTriTS = 0)
        {
            return tyLePhanBo * giaTriTS;
        }

        /// <summary>
        /// Description: Tinh khoang cach thang giua 2 moc thoi gian
        /// </summary>
        /// <param name="lValue">Ngay bat dau tinh</param>
        /// <param name="rValue">ngay ket thuc</param>
        /// <returns></returns>
        public static Decimal? MonthsOfUse(DateTime? lValue, DateTime? rValue)
        {
            var time = lValue.Value - rValue.Value;
            var months = time.Days / 30;
            var ngayDu = time.Days % 30;
            if (ngayDu >= 15)
            {
                months++;
            }
            return months;
        }

        /// <summary>
        /// Description: Tính số năm giữa 2 mốc thời gian
        /// </summary>
        /// <param name="lValue">ngày nhập,(or ngày chốt giá trị tài sản)</param>
        /// <param name="rValue">ngày sử dụng</param>
        /// <returns></returns>
        public static Decimal? YearsOfUse(DateTime? lValue, DateTime? rValue)
        {
            var years = lValue.Value.Year - rValue.Value.Year;
            if (lValue.Value.Month == 12 && lValue.Value.Day == 31)
            {
                years++;
            }
            if (rValue.Value.Month == 12 && rValue.Value.Day == 31)
            {
                years--;
            }
            if (years > 0)
            {
                return years;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Description: Tính tỷ lệ
        /// </summary>
        /// <param name="value">số tháng sử dụng, số năm sử dụng</param>
        /// <returns></returns>
        public static Decimal? Rate(this Decimal? value)
        {
            if (value > 0)
                return 1 / value;
            else
                return 0;
        }

        /// <summary>
        /// Description: Tinh gia tri con lai sau hao mon
        /// </summary>
        /// <param name="giaTriTinh">gia tri tinh hao mon</param>
        /// <param name="tyLeHM">ty le hao mon(vd: 12.5%/nam)</param>
        /// <param name="YearsOfUse">so nam su dung</param>
        /// <returns></returns>
        public static Decimal? GiaTriSauHaoMon(Decimal? giaTriTinh = 0, Decimal? tyLeHM = 0, Decimal? YearsOfUse = 0, Decimal? nguyenGiaTinh = 0)
        {
            return giaTriTinh - (nguyenGiaTinh * tyLeHM * YearsOfUse);
        }

        /// <summary>
        /// Description: Gia tri con lai sau khi trich khau hao
        /// </summary>
        /// <param name="giaTriTinh">gia tri trich khau hao</param>
        /// <param name="tyLeKH">ty le khau hao(cách tính: 1/sothang)</param>
        /// <param name="MonthsOfUse">so thang khau hao</param>
        /// <returns></returns>
        public static Decimal? GiaTriSauKhauHao(Decimal? giaTriTinh = 0, Decimal? tyLeKH = 0, Decimal? MonthsOfUse = 0)
        {
            return giaTriTinh - (giaTriTinh * tyLeKH * MonthsOfUse);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GTTinh"></param>
        /// <param name="NamSuDung"></param>
        /// <param name="GTHaoMonMotNam"></param>
        /// <returns></returns>
        public static Decimal? GiaTriConLaiCu(decimal GTTinh,DateTime NgayBienDongMoi,DateTime NgayBienDongCu, decimal NguyenGia,decimal TyLeHaoMon)
        {
            var HM_NamSuDung = CommonCalculate.YearsOfUse(NgayBienDongMoi, NgayBienDongCu);
            var GTHaoMonMotNam = NguyenGia * TyLeHaoMon/100;
            var HM_LuyKe = CommonCalculate.TinhLuyKe(thoiGian: HM_NamSuDung, valuePerTime: GTHaoMonMotNam);
            var res = GTTinh - HM_LuyKe;
            return res;
        }
    }
}
