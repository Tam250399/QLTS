using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.CauHinh;
using GS.Data;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.BaoCaos
{
    public partial interface IBaoCaoCongKhaiService
    {
        IList<TS_BCCK_09A_CK_TSC> BaoCaoCongKhai_09A_CK_TSC(Decimal donViId, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, String strLyDoId = null, int donViTien = 1000);
        IList<TS_BCCK_09B_CK_TSC> BaoCaoCongKhai_09B_CK_TSC(Decimal donViId, Decimal namBaoCao = 0, String strLoaiTaiSanId = null, Decimal donViTien = 1000, Decimal donViDienTich = 1);
        IList<TS_BCCK_09C_CK_TSC> BaoCaoCongKhai_09C_CK_TSC(Decimal donViId, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000);
        IList<TS_BCCK_09D_CK_TSC> BaoCaoCongKhai_09D_CK_TSC(Decimal donViId, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000);
        IList<TS_BCCK_09DD_CK_TSC> BaoCaoCongKhai_09DD_CK_TSC(Decimal donViId, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000);
        IList<TS_BCCK_10A_CK_TSC> BaoCaoCongKhai_10A_CK_TSC(String stringDonVi = null, int capDonVi = 0, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi = 1);
        IList<TS_BCCK_10B_CK_TSC> BaoCaoCongKhai_10B_CK_TSC(String stringDonVi = null, int capDonVi = 0, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi = 1, decimal idQueueBaoCao = 0);
        IList<TS_BCCK_10C_CK_TSC> BaoCaoCongKhai_10C_CK_TSC(String stringDonVi = null, int capDonVi = 0, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi = 1);
        IList<TS_BCCK_10D_CK_TSC> BaoCaoCongKhai_10D_CK_TSC(String stringDonVi = null, int capDonVi = 0, Decimal? namBaoCao = 2020, String strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int bacDonVi = 1);
        IList<TS_BCCK_07_CK_TSC> BaoCaoCongKhai_07_CK_TSC(decimal donViId, decimal namBaoCao = 2020, string strLoaiTaiSanId= null, int donViTien = 1000);
        IList<TS_BCCK_TDMS> BaoCaoCongKhai_TDMS_TSC(decimal donViId, decimal namBaoCao = 0, string strLoaiTaiSanId = null, int donViTien = 1000);
        IList<TS_BCCK_THMS> BaoCaoCongKhai_THMS_TSC(decimal donViId, decimal namBaoCao = 0, string strLoaiTaiSanId = null, int donViTien = 1000, int bacTaiSan = 1);
		IList<TS_BCCK_DIEUCHUYEN_BAN_GIAO> BaoCaoCongKhai_DIEU_CHUYEN_BAN_GIAO_TSC(decimal donViId, int donViTien=1000, decimal namBaoCao=0);
        IList<TS_BCCK_11A_CK_TSC> BaoCaoCongKhai_11A_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int bacDonVi = 1, decimal idQueueBaoCao = 0);
        IList<TS_BCCK_11B_CK_TSC> BaoCaoCongKhai_11B_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int bacDonVi = 1);
        IList<TS_BCCK_11C_CK_TSC> BaoCaoCongKhai_11C_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int bacDonVi = 1, decimal idQueueBaoCao = 0);
        IList<TS_BCCK_11D_CK_TSC> BaoCaoCongKhai_11D_CK_TSC(string stringDonVi = null, decimal? namBaoCao = 2020, string strLoaiTaiSanId = null, int donViTien = 1000, int donViDienTich = 1, int MauSo = 1, int bacDonVi = 1);

    }
}
