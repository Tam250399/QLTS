using DevExpress.Office.Drawing;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GS.Core
{
    public static class CommonExtention
    {

        #region Public extension methods...

        /// <summary>
        /// Chuyen dang datetime sang string co dang yyyy-MM-dd
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string toStringApi(this DateTime? input)
        {
            return input.HasValue ? input.Value.ToString("yyyy-MM-dd") : "";
        }
       
        public static int MonthDifference(this DateTime? lValue, DateTime? rValue)
        {
            if (lValue.HasValue && rValue.HasValue)
                return (lValue.Value.Month - rValue.Value.Month) + 12 * (lValue.Value.Year - rValue.Value.Year);
            return 0;
        }
        /// <summary>
        /// Chuyen dang datetime sang string co dang yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string toStringApiWithTime(this DateTime? input)
        {
            return input.HasValue ? input.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            //return input.HasValue ? input.Value.ToString("dd-MM-yyyy HH:mm:ss") : "";
        }
        public static string toStringApiWithTime(this DateTime input)
        {
            return input != DateTime.MinValue ? input.ToString("yyyy-MM-dd HH:mm:ss") : "";
            //return input.HasValue ? input.Value.ToString("dd-MM-yyyy HH:mm:ss") : "";
        }
        /// <summary>
        /// Chuyen dang string yyyy-MM-dd sang datetime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime? toDateSys(this string input, string format = "")
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    DateTime ret;
                    if (string.IsNullOrEmpty(format))
                    {
                        ret = Convert.ToDateTime(input); //DateTime.ParseExact(input, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        return ret;
                    }
                    if (DateTime.TryParseExact(input, format, new CultureInfo("vi-VN"), DateTimeStyles.None, out ret))
                    {
                        return ret;
                    }
                }
                catch { }
            }
            return null;
        }
        public static string toDateVNString(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    DateTime ret = Convert.ToDateTime(input);
                    return ret.toDateVNString();
                }
                catch { }
            }
            return "";
        }
        public static string toDateVNString(this DateTime input, bool isShowTime = false)
        {
            if (isShowTime)
                return input.ToString("dd/MM/yyyy HH:mm");
            return input.ToString("dd/MM/yyyy");
        }
        public static string toDateVNString(this DateTime? input, bool isShowTime = false)
        {
            if (input.HasValue)
            {
                return input.Value.toDateVNString(isShowTime);

            }
            return "";
        }
        /// <summary>
        /// chuyen ngay thuong sang ngay nguoc
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDistanceTime(this DateTime input)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - input.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "vừa xong" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "1 phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "1 giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "Hôm qua";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 năm trước" : years + " năm trước";
            }
        }
        public static Decimal toLoaiHinhTaiSanTDKho(this Decimal loaiHinhTaiSanId)
        {
            enumLOAI_HINH_TAI_SAN_TOAN_DAN enumLoaiHinhTaiSan = (enumLOAI_HINH_TAI_SAN_TOAN_DAN)loaiHinhTaiSanId;
            decimal result;
            switch (enumLoaiHinhTaiSan)
            {
                case enumLOAI_HINH_TAI_SAN_TOAN_DAN.ALL:
                    result = 0;
                    break;
                case enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT:
                    result = (int)enumLOAI_HINH_TAI_SAN_KHO.TAI_SAN_TOAN_DAN_DAT;
                    break;
                case enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA:
                    result = (int)enumLOAI_HINH_TAI_SAN_KHO.TAI_SAN_TOAN_DAN_NHA;
                    break;
                case enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO:
                    result = (int)enumLOAI_HINH_TAI_SAN_KHO.TAI_SAN_TOAN_DAN_OTO;
                    break;
                case enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC:
                    result = (int)enumLOAI_HINH_TAI_SAN_KHO.TAI_SAN_TOAN_DAN_KHAC;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }
        #endregion

        #region  Object to json
        public static string toStringJson(this Object obj, bool isLowerCase = false, bool isIgnoreNull = false, string formatDate = null)
        {
            if (obj == null)
                return "";
            try
            {
                if (!isLowerCase)
                {
                    var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                    if (isIgnoreNull)
                        serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    if (!String.IsNullOrEmpty(formatDate))
                        serializerSettings.DateFormatString = formatDate;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(obj, serializerSettings);
                    //return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                }
                else
                {
                    var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    serializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    return Newtonsoft.Json.JsonConvert.SerializeObject(obj, serializerSettings);
                }
            }
            catch (Exception e)
            {
                string err = e.Message;
            }
            return "";
        }
        /// <summary>
        /// Chuyen doi chuoi json thanh dang entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strjson"></param>
        /// <returns></returns>
        public static T toEntity<T>(this string strjson)
        {
            if (string.IsNullOrEmpty(strjson))
                return default(T);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strjson);
            }
            catch (Exception ex)
            {
                var err = ex;
            }
            return default(T);


        }
        /// <summary>
        /// Prepare row excel to entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="indexRow"></param>
        /// <param name="dataSettings"></param>
        /// <returns></returns>
        public static T toEntity<T>(this ExcelRange dataRow, T entity, int indexRow, List<ExcelModel> dataSettings)
        {

            foreach (ExcelModel item in dataSettings)
            {
                ExcelRange ex = dataRow[indexRow, item.COL];
                if (ex == null)
                    continue;
                PropertyInfo pro = entity.GetType().GetProperty(item.NAME);
                if (pro.PropertyType == typeof(string))
                {
                    if (ex.GetValue<string>() != null)
                        pro.SetValue(entity, ex.GetValue<string>().Trim());
                    else
                    {
                        pro.SetValue(entity, ex.GetValue<string>());
                    }
                }
                else if (pro.PropertyType == typeof(decimal))
                    pro.SetValue(entity, ex.GetValue<decimal>());
                else if (pro.PropertyType == typeof(decimal?))
                    pro.SetValue(entity, ex.GetValue<decimal?>());
                else if (pro.PropertyType == typeof(int))
                    pro.SetValue(entity, ex.GetValue<int>());
                else if (pro.PropertyType == typeof(int?))
                    pro.SetValue(entity, ex.GetValue<int?>());
                else if (pro.PropertyType == typeof(DateTime))
                    pro.SetValue(entity, ex.GetValue<DateTime>());
                else if (pro.PropertyType == typeof(DateTime?))
                    pro.SetValue(entity, ex.GetValue<DateTime?>());
                //if (pro.PropertyType == typeof(string))
                //{
                //    pro.SetValue(item, ex.Value.ToString());
                //}
                //else if (pro.PropertyType == typeof(decimal))
                //{
                //    decimal rs;
                //    Decimal.TryParse(ex.Value.ToString(), out rs);
                //    pro.SetValue(item, rs)    ;
                //}

            }
            return entity;
        }
        /// <summary>
        /// Chuyen doi chuoi json thanh dang list(entity)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strjson"></param>
        /// <returns></returns>
        public static List<T> toEntities<T>(this string strjson)
        {
            if (string.IsNullOrEmpty(strjson))
                return default(List<T>);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(strjson);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return default(List<T>);


        }
        public static T toObject<T>(this object obj1, string jsonSetting)
        {
            T obj2 = default(T);
            MapObject mapObject = new MapObject();
            if (obj1.GetType().Name == mapObject.ObjectMap.nameObj1)
            {
                foreach (RowMap item in mapObject.ListMap)
                {
                    PropertyInfo _propertyInfo1 = obj1.GetType().GetProperty(item.name1);
                    PropertyInfo _propertyInfo2 = obj2.GetType().GetProperty(item.name2);
                    _propertyInfo2.SetValue(obj2, _propertyInfo1.GetValue(obj1));
                    //if (_propertyInfo2.PropertyType == typeof(string))
                    //    _propertyInfo2.SetValue(obj2, _propertyInfo1.GetValue(obj1).ToString());
                    //else if (_propertyInfo2.PropertyType == typeof(decimal))
                    //    _propertyInfo2.SetValue(obj2, (decimal)_propertyInfo1.GetValue(obj1));
                    //else if (_propertyInfo2.PropertyType == typeof(decimal?))
                    //    _propertyInfo2.SetValue(obj2, (decimal?)_propertyInfo1.GetValue(obj1));
                    //else if (_propertyInfo2.PropertyType == typeof(int))
                    //    _propertyInfo2.SetValue(obj2, (int)_propertyInfo1.GetValue(obj1));
                    //else if (_propertyInfo2.PropertyType == typeof(int?))
                    //    _propertyInfo2.SetValue(obj2, (int?)_propertyInfo1.GetValue(obj1));
                    //else if (_propertyInfo2.PropertyType == typeof(DateTime))
                    //    _propertyInfo2.SetValue(obj2, (DateTime)_propertyInfo1.GetValue(obj1));
                    //else if (_propertyInfo2.PropertyType == typeof(DateTime?))
                    //    _propertyInfo2.SetValue(obj2, (DateTime?)_propertyInfo1.GetValue(obj1));
                }
            }
            else if (obj1.GetType().Name == mapObject.ObjectMap.nameObj2)
            {
                foreach (RowMap item in mapObject.ListMap)
                {
                    PropertyInfo _propertyInfo1 = obj1.GetType().GetProperty(item.name1);
                    PropertyInfo _propertyInfo2 = obj2.GetType().GetProperty(item.name2);
                    _propertyInfo1.SetValue(obj1, _propertyInfo2.GetValue(obj2));
                }
            }
            return obj2;
        }
        #endregion

        #region Khac
        private static Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string RandomString(int length)
        {

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string toCode(this decimal Id, int len = 11)
        {
            return Id.ToString().PadLeft(len, '0');
        }
        static Regex ConvertToUnsign_rg = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        /// <summary>
        /// Chuyen thanh tieng Viet khong dau
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string toNoSign(this string strInput, bool isUpper = true)
        {
            var temp = strInput.Normalize(NormalizationForm.FormD);
            string _input = ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D");
            if (isUpper)
                return _input.ToUpper();
            return _input;
        }
        /// <summary>
        /// Chuyen doi du lieu so sang dang format string co ngan cach theo so cua VN
        /// Chi ap dung cho du lieu dang so
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToVNStringNumber(this Object val, bool isFloat = false)
        {
            if (isFloat)
            {
                return (Math.Round(Convert.ToDecimal(val), 2)).ToString("###,###,##0.#0");
            }
            return Convert.ToDecimal(val).ToString("###,###,##0");
        }
        /// <summary>
        /// Chuyển decimal về dạng string theo format
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public string ConvertDecimalNullToString(this decimal? val, string format = "N2")
        {
            decimal value = val ?? 0;
            CultureInfo culture1 = CultureInfo.CreateSpecificCulture("vi-VN");
            if (value == Math.Round(value, 0))
            {
                return value.ToString("N0", culture1);
            }
            return value.ToString(format, culture1);
        }
        /// <summary>
        /// Chuyển decimal về dạng string theo format
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public string ConvertDecimalToString(this decimal value, string format = "N2", int decimals = 2)
        {
            CultureInfo culture1 = CultureInfo.CreateSpecificCulture("vi-VN");
            if (Math.Round(value, decimals) == Math.Round(value, 0))
            {
                return value.ToString("N0", culture1);
            }
            return value.ToString(format, culture1);
        }
        /// <summary>
        /// Chuyển đổi ký tự dạng số (có mask) sang số
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToNumber(this String val)
        {
            if (val == null)
                return 0;
            return Convert.ToDecimal(val.Replace(".", ""));
        }
        /// <summary>
        /// Chuyển đổi ký tự dạng số (có mask) sang số
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToNumberInt32(this object val)
        {
            if (val == null)
                return 0;
            int rout = 0;
            Int32.TryParse(val.ToString().Replace(".", ""), out rout);
            return rout;
        }
        public static string ToPercentString(this Object val)
        {
            return Convert.ToDecimal(val).ToString("0.00%");
        }
        /// <summary>
        /// Tao duong dan cho viec luu tru file theo datetime yyyy\mm\dd
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToPathFolderStore(this DateTime val)
        {
            return string.Format("{0}/{1}/{2}", val.ToString("yyyy"), val.ToString("MM"), val.ToString("dd"));
        }
        public static string ToStringTrangThaiCongCuPhatHienThua(this decimal T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Đang sử dụng";
                    break;
                case 2:
                    val = "Chưa sử dụng";
                    break;
                case 3:
                    val = "Hỏng chờ xử lý";
                    break;
            }
            return val;
        }
        public static string ToStringCapDonVi(this decimal T)
        {
            var val = "";
            switch (T)
            {
                case (int)CapEnum.BoCoQuanTrungUong:
                    val = "Bộ cơ quan trung ương";
                    break;
                case (int)CapEnum.Tinh:
                    val = "Tỉnh";
                    break;
                case (int)CapEnum.Huyen:
                    val = "Huyện";
                    break;
                case (int)CapEnum.Xa:
                    val = "Xã";
                    break;
                case (int)CapEnum.TongCuc:
                    val = "Tổng cục";
                    break;
                case (int)CapEnum.Cuc:
                    val = "Cục";
                    break;
                case (int)CapEnum.ChiCuc:
                    val = "Chi cục";
                    break;
                default:
                    break;
            }

            return val;
        }
        public static string ConvertIntToRomanNumber(this int T)
        {
            string strRet = "";
            try
            {
                decimal _Number = T;
                Boolean _Flag = true;
                string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (_Flag)
                {
                    while (_Number >= ArrNumber[i])
                    {
                        _Number -= ArrNumber[i];
                        strRet += ArrLama[i];
                        if (_Number < 1)
                            _Flag = false;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                strRet = "";
            }
            return strRet;
        }
        public static int ToLoaiIntDuLieu(this string T)
        {
            var val = 0;
            switch (T)
            {
                case "CheckBox":
                    val = (int)enumLoaiDuLieuCauHinh.CB;
                    break;
                case "DropDownList":
                    val = (int)enumLoaiDuLieuCauHinh.DDL;
                    break;
                case "DateTime":
                    val = (int)enumLoaiDuLieuCauHinh.DT;
                    break;
                case "MultiSelect":
                    val = (int)enumLoaiDuLieuCauHinh.MS;
                    break;
                case "Number":
                    val = (int)enumLoaiDuLieuCauHinh.NB;
                    break;
                case "Object":
                    val = (int)enumLoaiDuLieuCauHinh.OBJ;
                    break;
                case "Radio":
                    val = (int)enumLoaiDuLieuCauHinh.RD;
                    break;
                case "String":
                    val = (int)enumLoaiDuLieuCauHinh.ST;
                    break;
            }
            return val;
        }
        public static string ToLoaiStringDuLieu(this int T)
        {
            var val = "";
            switch (T)
            {
                case (int)enumLoaiDuLieuCauHinh.CB:
                    val = "CheckBox";
                    break;
                case (int)enumLoaiDuLieuCauHinh.DDL:
                    val = "DropDownList";
                    break;
                case (int)enumLoaiDuLieuCauHinh.DT:
                    val = "DateTime";
                    break;
                case (int)enumLoaiDuLieuCauHinh.MS:
                    val = "MultiSelect";
                    break;
                case (int)enumLoaiDuLieuCauHinh.NB:
                    val = "Number";
                    break;
                case (int)enumLoaiDuLieuCauHinh.OBJ:
                    val = "Object";
                    break;
                case (int)enumLoaiDuLieuCauHinh.RD:
                    val = "Radio";
                    break;
                case (int)enumLoaiDuLieuCauHinh.ST:
                    val = "String";
                    break;
            }
            return val;
        }
        public static string ToLoaiTaiSanBaoCao(this int T)
        {
            var val = "";
            switch (T)
            {
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.DAT:
                    val = "Đất";
                    break;
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.NHA:
                    val = "Nhà";
                    break;
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.OTO:
                    val = "Ô tô";
                    break;
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.KHAC:
                    val = "Tài sản khác";
                    break;
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.TREN_500:
                    val = "Tài sản khác có nguyên giá từ 500 triệu đồng trở lên";
                    break;
                case (int)enumLOAI_HINH_TAI_SAN_BAO_CAO.DUOI_500:
                    val = "Tài sản khác có nguyên giá dưới 500 triệu đồng";
                    break;
            }
            return val;
        }
        /// <summary>
        /// enumLyDoTangGiamForBaoCao => tiếng việt
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static string ToStringLoaiLyDoTangGiamBaoCao(this int T)
        {
            var val = "";
            switch (T)
            {
                case (int)enumLyDoTangGiamForBaoCao.TAT_CA:
                    val = "Tất cả";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DANG_KY_LAN_DAU:
                    val = "Đăng ký lần đầu";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DAT_DUOC_TIEP_NHAN:
                    val = "Đất được tiếp nhận";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.NHAN_CHUYEN_NHUONG:
                    val = "Nhận chuyển nhượng";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.MUA_SAM:
                    val = "Mua sắm";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DAT_DI_THUE:
                    val = "Đất đi thuê";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DAT_DUOC_GIAO_MOI:
                    val = "Đất được giao mới";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.XAY_DUNG_MOI:
                    val = "Xây dựng mới";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.TIEP_NHAN:
                    val = "Tiếp nhận";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.TIEP_NHAN_TU_DIEU_CHUYEN:
                    val = "Tiếp nhận từ điều chuyển";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.MUA_MOI:
                    val = "Mua mới";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.GIAO_VON_CHO_DON_VI_CHU_TAI_CHINH:
                    val = "Giao vốn cho đơn vị tự chủ tài chính";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.BAN:
                    val = "Bán";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.CHUYEN_NHUONG:
                    val = "Chuyển nhượng";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.TIEU_HUY:
                    val = "Tiêu hủy";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DIEU_CHUYEN_NGOAI_HE_THONG:
                    val = "Điều chuyển ngoài hệ thống";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.THANH_LY:
                    val = "Thanh lý";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.DIEU_CHUYEN:
                    val = "Điều chuyển";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.BI_THU_HOI:
                    val = "Bị thu hồi";
                    break;
                case (int)enumLyDoTangGiamForBaoCao.KHAC:
                    val = "Khác";
                    break;
            }
            return val;
        }
        /// <summary>
        /// chuyển số thành số roman
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRomanNumBer(int number)
        {
            Dictionary<char, int> NumberRomanDictionary = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };
            var roman = new StringBuilder();
            foreach (var item in NumberRomanDictionary)
            {
                while (number >= item.Key)
                {
                    roman.Append(item.Value);
                    number -= item.Key;
                }
            }

            return roman.ToString();

        }
        /// <summary>
        /// chuyển số roman thành số
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        public static int RomanNumBerTo(string roman)
        {
            Dictionary<char, int> RomanNumberDictionary = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };
            int total = 0;

            int current, previous = 0;
            char currentRoman, previousRoman = '\0';

            for (int i = 0; i < roman.Length; i++)
            {
                currentRoman = roman[i];

                previous = previousRoman != '\0' ? RomanNumberDictionary[previousRoman] : '\0';
                current = RomanNumberDictionary[currentRoman];

                if (previous != 0 && current > previous)
                {
                    total = total - (2 * previous) + current;
                }
                else
                {
                    total += current;
                }

                previousRoman = currentRoman;
            }

            return total;
        }
        public static string ToStringLoaiHinhTaiSanFromString(this string T)
        {
            var val = "";
            if (T == null || T.Length <= 0)
            {
                val = "Loại tài sản: Tất cả";
                return val;
            }
            var stringListLoaiTaiSan = new List<string>();
            foreach (var ts in T.Split(',').Select(c => c.ToNumberInt32()).ToList())
            {
                stringListLoaiTaiSan.Add(ts.ToStringLoaiHinhTaiSan());
            }
            val = "Loại tài sản: " + String.Join(", ", stringListLoaiTaiSan);
            return val;
        }
        public static string ToStringLoaiHinhTaiSanByListId(this string T)
        {
            var val = "";
            if (T == null || T.Length <= 0)
            {
                val = "Tất cả";
                return val;
            }
            var stringListLoaiTaiSan = new List<string>();
            foreach (var ts in T.Split(',').Select(c => c.ToNumberInt32()).ToList())
            {
                stringListLoaiTaiSan.Add(ts.ToStringLoaiHinhTaiSan());
            }
            val = String.Join(", ", stringListLoaiTaiSan);
            return val;
        }
        public static string ToStringLoaiHinhTaiSan(this int T)
        {
            var val = "";
            switch (T)
            {
                case 0:
                    val = "Tất cả";
                    break;
                case 1:
                    val = "Đất";
                    break;
                case 2:
                    val = "Nhà";
                    break;
                case 3:
                    val = "Tài sản vật kiến trúc";
                    break;
                case 4:
                    val = "Ô tô";
                    break;
                case 5:
                    val = "Phương tiện khác";
                    break;
                case 6:
                    val = "Tài sản máy móc thiết bị";
                    break;
                case 7:
                    val = "Tài sản cây lâu năm/súc vật làm việc";
                    break;
                case 8:
                    val = "Tài sản hữu hình khác";
                    break;
                case 9:
                    val = "Tài sản vô hình";
                    break;
                case 10:
                    val = "Đặc thù";
                    break;
            }
            return val;
        }
        public static string ToStringLoaiHinhTaiSanForTSTD(this int T)
        {
            var val = "";
            switch (T)
            {
                case 0:
                    val = "Tất cả";
                    break;
                case 1:
                    val = "Đất";
                    break;
                case 2:
                    val = "Nhà";
                    break;
                case 4:
                    val = "Ô tô";
                    break;
                case 100:
                    val = "Tài sản khác";
                    break;
            }
            return val;
        }
        public static string ToStringDonViTienTSTD(this int T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Nghìn đồng";
                    break;
                case 2:
                    val = "Triệu đồng";
                    break;
                case 3:
                    val = "Tỷ đồng";
                    break;
            }
            return val;
        }
        public static int ToIntDonViTien(this int T)
        {
            var val = 1000;
            switch (T)
            {
                case 1:
                    val = 1000;
                    break;
                case 2:
                    val = 1000000;
                    break;
                case 3:
                    val = 1000000000;
                    break;
            }
            return val;
        }
        public static string ToStringDonViDienTichTSTD(this int T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Mét vuông";
                    break;
                case 2:
                    val = "Nghìn mét vuông";
                    break;
                case 3:
                    val = "Mười nghìn mét vuông";
                    break;
            }
            return val;
        }
        public static int ToIntDonViDienTich(this int T)
        {
            var val = 1;
            switch (T)
            {
                case 1:
                    val = 1;
                    break;
                case 2:
                    val = 1000;
                    break;
                case 3:
                    val = 10000;
                    break;
            }
            return val;
        }
        public static string ToStringDonViTien(this int T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Đồng";
                    break;
                case 1000:
                    val = "Nghìn đồng";
                    break;
                case 1000000:
                    val = "Triệu đồng";
                    break;
                case 1000000000:
                    val = "Tỷ đồng";
                    break;
            }
            return val;
        }
        public static string ToStringDonViDienTich(this int T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Mét vuông";
                    break;
                case 1000:
                    val = "Nghìn mét vuông";
                    break;
                case 10000:
                    val = "Mười nghìn mét vuông";
                    break;
            }
            return val;
        }
        public static string ToStringTrangThaiQueueBaoCao(this int T)
        {
            var val = "";
            switch (T)
            {

                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.TRANG_THAI_CHO:
                    return "Chờ xử lý";

                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU:
                    return "Đang tổng hợp dữ liệu";

                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_XUAT_FILE:
                    return "Đang xuất file";

                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_EXPORT_FILE:
                    return "Hoàn thành";

                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_LUU_FILE:
                    return "Hoàn thành";
                case (int)enumTRANG_THAI_QUEUE_BAO_CAO.LOI:
                    return "Lỗi";
                default:
                    break;

            }
            return val;
        }
        /// <summary>
        /// Bán or thanh lý
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static string ToStringBanOrThanhLy(this int T)
        {
            var val = "";
            switch (T)
            {
                case 0:
                    val = "Tất cả";
                    break;
                case 1:
                    val = "Bán";
                    break;
                case 2:
                    val = "Thanh lý";
                    break;
            }
            return val;
        }
        public static string ToStringLoaiXuatNhap(this int T)
        {
            var val = "";
            switch (T)
            {
                case 4:
                    val = "Điều chuyển";
                    break;
                case 5:
                    val = "Điều chuyển ngoài";
                    break;
                case 6:
                    val = "Giảm hỏng mất";
                    break;
                case 7:
                    val = "Bán, chuyển nhượng";
                    break;
                case 8:
                    val = "Tiêu hủy";
                    break;
                case 10:
                    val = "Bị thu hồi";
                    break;
                case 11:
                    val = "Thanh lý";
                    break;
                case 12:
                    val = "Giảm do bị mất, hủy hoại";
                    break;
                case 13:
                    val = "Khác";
                    break;
            }
            return val;
        }
        public static string ToStringViTriHoiDongKiemKe(this decimal T)
        {
            var val = "";
            switch (T)
            {
                case 1:
                    val = "Trưởng ban";
                    break;
                case 2:
                    val = "Ủy viên";
                    break;
            }
            return val;
        }
        public static string ToStringTrangThaiCongCu(this decimal? T)
        {
            var val = "";
            if (T == null)
            {
                return val;
            }
            switch (T)
            {
                case 1:
                    val = "Đang sử dụng";
                    break;
                case 2:
                    val = "Chưa sử dụng";
                    break;
                case 3:
                    val = "Hỏng chờ xử lý";
                    break;
                case 4:
                    val = "Thiếu";
                    break;
                case 5:
                    val = "Phát hiện thừa";
                    break;
            }
            return val;
        }
        public static string ToLyDoGiamBC(this int T)
        {
            var val = "";
            switch (T)
            {
                case (int)enumLyDoGiamBC.TatCa:
                    val = "Tất cả";
                    break;
                case (int)enumLyDoGiamBC.ThanhLy:
                    val = "Thanh lý";
                    break;
                case (int)enumLyDoGiamBC.BanChuyenNhuong:
                    val = "Bán, chuyển nhượng";
                    break;
                case (int)enumLyDoGiamBC.DieuChuyen:
                    val = "Điều chuyển";
                    break;
                case (int)enumLyDoGiamBC.Khac:
                    val = "Khác";
                    break;
            }
            return val;
        }
        public static string ToLyDoTangBC(this int T)
        {
            var val = "";
            switch (T)
            {
                case (int)enumLyDoTangBC.TatCa:
                    val = "Tất cả";
                    break;
                case (int)enumLyDoTangBC.MuaSam:
                    val = "Mua sắm";
                    break;
                case (int)enumLyDoTangBC.TiepNhap:
                    val = "Tiếp nhận";
                    break;
            }
            return val;
        }
        #endregion
    }
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {
        #region Fields
        public const string DATE_FORMAT_VN = "dd/MM/yyyy";
        public const string Month_FORMAT_VN = "MM/yyyy";
        public const string DATE_FORMAT_DB = "yyyy-MM-dd HH:mm:ss";//định dạnh ngày đồng bộ
        //we use EmailValidator from FluentValidation. So let's keep them sync - https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/Validators/EmailValidator.cs
        private const string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

        private static readonly Regex _emailRegex;
        private const string NODE_FORMAT_ADD = "{0}-{1:D8}";
        private const string NODE_FORMAT_SINGLE = "{0:D8}";
        private const string FORMAT_MATAISAN = "{0}-{1}-{2}";
        private const string FORMAT_MADONVIBOPHAN = "{0}-{1}";

        #endregion

        #region Ctor

        static CommonHelper()
        {
            _emailRegex = new Regex(_emailExpression, RegexOptions.IgnoreCase);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ensures the subscriber email or throw.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            var output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new GSException("Email is not valid.");
            }

            return output;
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();

            return _emailRegex.IsMatch(email);
        }

        /// <summary>
        /// Verifies that string is an valid IP-Address
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out IPAddress _);
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random integer number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its length is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length <= maxLength)
                return str;

            var pLen = postfix?.Length ?? 0;

            var result = str.Substring(0, maxLength - pLen);
            if (!string.IsNullOrEmpty(postfix))
            {
                result += postfix;
            }

            return result;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            return stringsToValidate.Any(string.IsNullOrEmpty);
        }

        /// <summary>
        /// Compare two arrays
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
        }

        /// <summary>
        /// Sets a property on an object to a value.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var instanceType = instance.GetType();
            var pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new GSException("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType);
            if (!pi.CanWrite)
                throw new GSException("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value == null)
                return null;

            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);

            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }


        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            var result = string.Empty;
            foreach (var c in str)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();

            //ensure no spaces (e.g. when the first letter is upper case)
            result = result.TrimStart();
            return result;
        }

        /// <summary>
        /// Set Telerik (Kendo UI) culture
        /// </summary>
        public static void SetTelerikCulture(string lang = "en-US")
        {
            //little hack here
            //always set culture to 'en-US' (Kendo UI has a bug related to editing decimal values in other cultures)
            var culture = new CultureInfo(lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        /// <summary>
        /// Get difference in years
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //source: http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
            //this assumes you are looking for the western idea of age and not using East Asian reckoning.
            var age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="IsRequired"></param>
        /// <returns></returns>
        public static bool ValidatePhoneNumber(string phone, bool IsRequired)
        {
            if (string.IsNullOrEmpty(phone) & !IsRequired)
                return true;

            if (string.IsNullOrEmpty(phone) & IsRequired)
                return false;

            var cleaned = Regex.Replace(phone, @"[^0-9]+", "");
            if (IsRequired)
            {
                if (cleaned.Length == 10)
                    return true;
                else
                    return false;
            }
            else
            {
                if (cleaned.Length == 0)
                    return true;
                else if (cleaned.Length > 0 & cleaned.Length < 10)
                    return false;
                else if (cleaned.Length == 10)
                    return true;
                else
                    return false; // should never get here
            }
        }
        /// <summary>
        /// Get private fields property value
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Value</returns>
        public static object GetPrivateFieldValue(object target, string fieldName)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target", "The assignment target cannot be null.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("fieldName", "The field name cannot be null or empty.");
            }

            var t = target.GetType();
            FieldInfo fi = null;

            while (t != null)
            {
                fi = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

                if (fi != null) break;

                t = t.BaseType;
            }

            if (fi == null)
            {
                throw new Exception($"Field '{fieldName}' not found in type hierarchy.");
            }

            return fi.GetValue(target);
        }
        public static int ProgressNumByDatetime(DateTime? startDate, DateTime? endDate)
        {
            if ((startDate == null) || (endDate == null))
            {
                return 0;
            }
            var percent = (DateTime.Now - startDate) / (endDate - startDate);
            if (percent >= 1)
            {
                return 100;
            }
            else if ((percent > 0) && (percent < 1))
            {
                return (int)(Math.Round((decimal)percent, 2) * 100);
            }
            return 0;
        }

        /// <summary>
        /// Tạo Tree node cho danh mục
        /// Author: AnPX
        /// Create Date: 30/12/2019
        /// Update Date:
        /// </summary>
        public static string GenTreeNode(string nodeParent, decimal? id, decimal? treeLevelParent)
        {
            if (treeLevelParent > 0)
                return string.Format(NODE_FORMAT_ADD, nodeParent, Convert.ToInt32(id));

            return string.Format(NODE_FORMAT_SINGLE, Convert.ToInt32(id));
        }
        /// <summary>
        /// Tự sinh mã tài sản
        /// Author: Dungnt
        /// Create Date: 24/02/2020
        /// Update Date:
        /// </summary>
        public static string GenMaTaiSan(string maDonVi, string maLoaiTaiSan, decimal TaiSanId)
        {
            return string.Format(FORMAT_MATAISAN, maDonVi, maLoaiTaiSan, TaiSanId);
        }
        public static string GenMaDonViBoPhan(string maDonVi, decimal boPhanId)
        {
            return string.Format(FORMAT_MADONVIBOPHAN, maDonVi, boPhanId);
        }





        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default file provider
        /// </summary>
        public static IGSFileProvider DefaultFileProvider { get; set; }

        #endregion
        #region KHO đông bộ       
        public static string toDateKhoString(DateTime input, bool isShowTime = false)
        {
            if (isShowTime)
                return input.ToString("dd-MM-yyyy HH:mm");
            return input.ToString("dd-MM-yyyy");
        }
        public static string toDateKhoString(DateTime? input, bool isShowTime = false)
        {
            if (input.HasValue)
            {
                return toDateKhoString(input: input.Value, isShowTime: isShowTime);
            }
            return null;
        }
        public static Decimal toLoaiHinhTaiSanKho( Decimal loaiHinhTaiSanId)
        {
            enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan = (enumLOAI_HINH_TAI_SAN)loaiHinhTaiSanId;
            enumLOAI_HINH_TAI_SAN_KHO rs = new enumLOAI_HINH_TAI_SAN_KHO();
            if (Enum.TryParse(enumLoaiHinhTaiSan.ToString(), true, out rs))
                return (int)rs;
            else
                return (int)enumLOAI_HINH_TAI_SAN_KHO.DAT;
        }

        #endregion

    }
}
