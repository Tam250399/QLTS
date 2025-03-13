using GS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Common
{
    public static class CommonHelperApi
    {
       
        //public static string MaLoaiTaiSanKho(string Ma, string IDNhomTaiSanKho)
        //{
        //    if (IDNhomTaiSanKho == enumCSDLQG_MA_NHOM_TAI_SAN.CoQuanToChuc)
        //    {
        //        return "OA_Id" + "_" + Ma;
        //    }
        //    if (IDNhomTaiSanKho == enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan)
        //    {
        //        return "NPA_Id" + "_" + Ma;
        //    }
        //    return null;
        //}
        //public static string TenLoaiTaiSanKho(string Ten, string IDNhomTaiSanKho)
        //{
        //    if (IDNhomTaiSanKho == enumCSDLQG_MA_NHOM_TAI_SAN.CoQuanToChuc)
        //    {
        //        return "OA_Id" + "_" + Ten;
        //    }
        //    if (IDNhomTaiSanKho == enumCSDLQG_MA_NHOM_TAI_SAN.XacLapToanDan)
        //    {
        //        return "NPA_Id" + "_" + Ten;
        //    }
        //    return null;
        //}
        /// <summary>
        /// lấy Id thực tế trong csdl do Id gửi sang khi đồng bộ đã bị thay đổi
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public static decimal GetIdDanhMucForNhanTaiSan(decimal Id)
        //{
        //    int LoaiTaiSanId = 0;
        //    if (Id > 0)
        //    {
        //        LoaiTaiSanId = (int)Id / 10;
        //    }            
        //    return LoaiTaiSanId;
        //}
    }
}
