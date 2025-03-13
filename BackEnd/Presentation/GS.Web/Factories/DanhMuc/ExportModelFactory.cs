using GS.Core;
using GS.Core.Domain.Api.DanhMuc;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.ExportImport;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Models.BaoCaos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GS.Web.Factories.DanhMuc
{
    public class ExportModelFactory : IExportModelFactory
    {
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDonViService _DonViService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IHienTrangService _hienTrangService;
        private readonly IDuAnService _duAnService;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiLyDoBienDongService _loailyDoBienDongService;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IExportManager _exportManager;

        public ExportModelFactory(
          INhanHienThiService NhanHienThiService,
         IDonViService DonViService,
         ILyDoBienDongService lyDoBienDongService,
         IQuocGiaService quocGiaService,
         ICheDoHaoMonService cheDoHaoMonService,
         IMucDichSuDungService mucDichSuDungService,
         IHienTrangService hienTrangService,
         IDuAnService duAnService,
         ILoaiTaiSanService loaiTaiSanService,
         INguoiDungDonViService nguoiDungDonViService,
         ILoaiDonViService loaiDonViService,
         INguonGocTaiSanService nguonGocTaiSanService,
         IDiaBanService diaBanService,
         ILoaiLyDoBienDongService loailyDoBienDongService,
         IHinhThucXuLyService hinhThucXuLyService,
         IPhuongAnXuLyService phuongAnXuLyService,
         IExportManager exportManager
         )
        {
            this._exportManager = exportManager;
            this._NhanHienThiService = NhanHienThiService;
            this._DonViService = DonViService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._quocGiaService = quocGiaService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._hienTrangService = hienTrangService;
            this._duAnService = duAnService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._loaiDonViService = loaiDonViService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._diaBanService = diaBanService;
            this._loailyDoBienDongService = loailyDoBienDongService;
            this._hinhThucXuLyService = hinhThucXuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
        }

        public FileContentResult ExportDanhMuc(String name, string keySearch = null, decimal? donViId = null, int type = 1)
        {
            try
            {
                int total = 0;
                MemoryStream stream = new MemoryStream();
                String json = "";
                bool addSTT = true;

                #region prepare

                if (name == "DonVi")
                {
                    var donVis = _DonViService.GetAllDonVisChuaDb()/*.Where(c => c.MA.StartsWith("T07"))*/;
                    if (donVis != null)
                    {
                        List<Kho_DonVi_Api> lst = new List<Kho_DonVi_Api>();
                        foreach (var item in donVis)
                        {
                            if (item == null)
                            {
                                lst = new List<Kho_DonVi_Api>();
                            }
                            var x = new Kho_DonVi_Api();
                            x.actionType = 1;
                            x.id = null;
                            if (item.PARENT_ID > 0)
                            {
                                DonVi donViCha = _DonViService.GetDonViById(item.PARENT_ID.Value);
                                x.parentId = donViCha.DB_ID;
                            }

                            x.name = item.TEN;
                            x.nationalBudgetCode = item.MA_DVQHNS;
                            x.code = item.MA;
                            x.unitTypeId = item.LoaiDonVi != null ? (int?)item.LoaiDonVi.DB_ID : null;
                            x.address = item.DIA_CHI;
                            x.fax = item.FAX;
                            x.accountingStandard = (item.CHE_DO_HACH_TOAN_ID > 0) ? (int)item.CHE_DO_HACH_TOAN_ID.Value : (int)enumCHE_DO_HACH_TOAN.HAO_MON;
                            x.isActive = item.TRANG_THAI_ID;
                            x.syncId = item.ID.ToString();
                            x.syncParentId = item.PARENT_ID > 0 ? item.PARENT_ID.ToString() : null;
                            x.phoneNumber = item.DIEN_THOAI;
                            x.administrativeLevel = (int)item.LOAI_CAP_DON_VI_ID.GetValueOrDefault(1);
                            x.functionalUnitType = item.LA_DON_VI_NHAP_LIEU == false ? 1 : 2;

                            lst.Add(x);
                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_DonVi_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "LoaiDonVi")
                {
                    var loaiDonVis = _loaiDonViService.GetAllLoaiDonVisChuaDb();
                    if (loaiDonVis != null)
                    {
                        var lst = loaiDonVis.Select(c => new Kho_LoaiDonVi_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA != null ? c.MA : "LDV" + c.ID.ToString(),
                            syncId = c.ID.ToString(),
                            syncParentId = c.PARENT_ID > 0 ? c.PARENT_ID.ToString() : null,
                            parentId = null,
                            accountingStandard = c.CHE_DO_HOACH_TOAN_ID == null ? (long)enumCHE_DO_HACH_TOAN.HAO_MON : (long)c.CHE_DO_HOACH_TOAN_ID,
                            displayOrder = (long?)c.SAP_XEP,
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_LoaiDonVi_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "LoaiTaiSanToanDan")
                {
                    var loaiTaiSan = _loaiTaiSanService.GetAllLoaiTaiSanToanDansChuaDb();
                    if (loaiTaiSan != null)
                    {
                        var lst = loaiTaiSan.Select(c => new Kho_LoaiTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            parentId = null,
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                            code = c.MA,
                            treeId = _cheDoHaoMonService.GetCheDoHaoMonById(c.CHE_DO_HAO_MON_ID.Value).DB_ID,
                            syncTreeId = c.CHE_DO_HAO_MON_ID.ToString(),
                            syncParentId = c.PARENT_ID != null ? c.PARENT_ID.ToString() : null,
                            isActive = true,
                            amortizationPeriod = (long?)c.HM_THOI_HAN_SU_DUNG,
                            amortizationRate = (long?)c.HM_TY_LE,
                            calculationUnit = c.DON_VI_TINH,
                            assetTypeGroupId = 1
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_LoaiTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "LoaiTaiSan")
                {
                    var loaiTaiSan = _loaiTaiSanService.GetAllLoaiTaiSansChuaDb();
                    if (loaiTaiSan != null)
                    {
                        var lst = loaiTaiSan.Select(c => new Kho_LoaiTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            parentId = null,
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                            code = c.MA,
                            treeId = _cheDoHaoMonService.GetCheDoHaoMonById(c.CHE_DO_HAO_MON_ID.Value).DB_ID,
                            syncTreeId = c.CHE_DO_HAO_MON_ID.ToString(),
                            syncParentId = c.PARENT_ID != null ? c.PARENT_ID.ToString() : null,
                            isActive = true,
                            amortizationPeriod = (long?)c.HM_THOI_HAN_SU_DUNG,
                            amortizationRate = (long?)c.HM_TY_LE,
                            calculationUnit = c.DON_VI_TINH,
                            assetTypeGroupId = 1
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_LoaiTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "NguonGocTaiSan")
                {
                    var nguonGocTaiSans = _nguonGocTaiSanService.GetAllNguonGocTaiSansChuaDb();
                    if (nguonGocTaiSans != null)
                    {
                        var lst = nguonGocTaiSans.Select(c => new Kho_NguonGocTaiSan_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                            syncParentId = c.PARENT_ID > 0 ? c.PARENT_ID.ToString() : null,
                            parentId = null
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_NguonGocTaiSan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "QuocGia")
                {
                    var quocGias = _quocGiaService.GetAllQuocGiaChuaDB();
                    if (quocGias != null)
                    {
                        var lst = quocGias.Select(c => new Kho_QuocGia_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.ID.ToString(),
                            description = c.MO_TA,
                            syncId = c.ID.ToString(),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_QuocGia_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "DiaBan")
                {
                    var diaBans = _diaBanService.GetDiaBansChuaDb();
                    if (diaBans != null)
                    {
                        List<Kho_DiaBan_Api> lst = new List<Kho_DiaBan_Api>();

                        foreach (var m in diaBans)
                        {
                            Kho_DiaBan_Api kho_DiaBan = new Kho_DiaBan_Api();
                            kho_DiaBan.id = null;
                            kho_DiaBan.actionType = 1;
                            kho_DiaBan.name = m.TEN;
                            kho_DiaBan.code = m.MA;
                            kho_DiaBan.isActive = m.TRANG_THAI_ID == 1 ? true : false;
                            kho_DiaBan.syncParentId = m.PARENT_ID > 0 ? m.PARENT_ID.ToString() : null;
                            kho_DiaBan.districtId = null;
                            kho_DiaBan.provinceId = null;
                            kho_DiaBan.syncId = m.ID.ToString();
                            lst.Add(kho_DiaBan);
                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_DiaBan_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "CheDoHaoMon")
                {
                    var cheDoHaoMons = _cheDoHaoMonService.GetAllCheDoHaoMonChuaDb();
                    if (cheDoHaoMons != null)
                    {
                        var lst = cheDoHaoMons.Select(c => new Kho_CheDoHaoMon_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN_CHE_DO,
                            code = c.MA_CHE_DO,
                            syncId = c.ID.ToString(),
                            startDate = CommonHelper.toDateKhoString(c.TU_NGAY),
                            endDate = CommonHelper.toDateKhoString(c.DEN_NGAY),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_CheDoHaoMon_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "MucDichSuDung")
                {
                    //var mucDichSuDungs = _mucDichSuDungService.GetMucDichSuDungChuaDb();
                    //if (mucDichSuDungs != null)
                    //{
                    //    var lst = mucDichSuDungs.Select(c => new Kho_QuocGia_Api()
                    //    {
                    //    }).ToList();
                    //    if (type == 1)
                    //        stream = prepareExcelEntity<Kho_QuocGia_Api>(stream, lst, name);
                    //    else
                    //        json = lst.toStringJson();
                    //}
                    //else
                    //{
                    //    return Json(new
                    //    {
                    //        success = false,
                    //        Data = "Không có dữ liệu",
                    //    });
                    //}
                }
                else if (name == "HienTrang")
                {
                    var hienTrangs = _hienTrangService.GetHienTrangsChuaDb();
                    if (hienTrangs != null)
                    {
                        var lst = hienTrangs.Select(c => new Kho_HienTrang_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN_HIEN_TRANG,
                            code = c.ID.ToString(),
                            syncId = c.ID.ToString(),
                            assetTypeIds = new List<long>() { (long)CommonHelper.toLoaiHinhTaiSanKho(c.LOAI_HINH_TAI_SAN_ID) },
                            isAreaUsage = (c.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT
                                            || c.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA) ? true : false
                        }).ToList();
                        total = lst.Count();

                        if (type == 1)
                            stream = prepareExcelEntity<Kho_HienTrang_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "LyDoBienDong")
                {
                    var LyDoBienDongs = _lyDoBienDongService.GetAllLyDoBienDongsChuaDb();
                    if (LyDoBienDongs != null)
                    {
                        List<Kho_LyDoBienDong_Api> lst = new List<Kho_LyDoBienDong_Api>();
                        foreach (var c in LyDoBienDongs)
                        {
                            var item = new Kho_LyDoBienDong_Api()
                            {
                                id = null,
                                actionType = 1,
                                name = c.TEN,
                                code = c.MA,
                                causeTypeId = (long?)c.LOAI_LY_DO_ID,
                                //causeTypeId = null,
                                syncId = c.ID.ToString(),
                                displayOrder = (long?)c.TRUONG_SAP_XEP,
                            };
                            if (!string.IsNullOrEmpty(c.LOAI_HINH_TAI_SAN_AP_DUNG_ID))
                            {
                                var x = c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.toEntities<decimal>();
                                if (x != null)
                                {
                                    item.assetTypeIds = x.Select(m => (long)CommonHelper.toLoaiHinhTaiSanKho(m)).ToList();
                                }
                            }
                            else
                                item.assetTypeIds = new List<long>() { (long)enumIdNhomTaiSanKho.CoQuanToChuc };
                            lst.Add(item);
                        }
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_LyDoBienDong_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "LoaiLyDoBienDong")
                {
                    var loailyDoBienDongs = _loailyDoBienDongService.GetAllLoaiLyDoBienDongsChuaDb();
                    if (loailyDoBienDongs != null)
                    {
                        var lst = loailyDoBienDongs.Select(c => new Kho_LoaiLyDoBienDong_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                        }).ToList();
                        total = lst.Count();

                        if (type == 1)
                            stream = prepareExcelEntity<Kho_LoaiLyDoBienDong_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "NguoiDung")
                {
                    var NguoiDungs = _nguoiDungService.GetAllNguoiDungsChuaDb();
                    if (NguoiDungs != null)
                    {
                        var lst = NguoiDungs.Select(c => new Kho_NguoiDung_Api()
                        {
                            id = null,
                            actionType = 1,
                            username = c.TEN_DANG_NHAP,
                            email = c.EMAIL,
                            //phoneNumber = c.MOBILE,
                            //passwordHash = c.MAT_KHAU,
                            passwordHash = "null",
                            fullName = c.TEN_DAY_DU,
                            lockoutEnabled = c.TRANG_THAI_ID == 1 ? true : false,
                            isAdministrator = c.IS_QUAN_TRI,
                            //unitId = GetUnitForNguoiDung(c.ID),
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_NguoiDung_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "DuAn")
                {
                    var DuAns = _duAnService.GetAllDuAnsChuaDb();
                    if (DuAns != null)
                    {
                        var lst = DuAns.Select(c => new Kho_DuAn_Api()
                        {
                            id = null,
                            actionType = 1,
                            name = c.TEN,
                            code = c.MA,
                            syncId = c.ID.ToString(),
                            unitId = c.donVi.DB_ID,
                            decisionNumber = c.SO_QUYET_DINH_PHE_DUYET,
                            expense = (long?)c.TONG_KINH_PHI,
                            expenseStateBudget = (long?)c.NGUON_NS,
                            expenseOda = (long?)c.NGUON_ODA,
                            expenseAid = (long?)c.NGUON_VIEN_TRO,
                            expenseOther = (long?)c.NGUON_KHAC,
                            startDate = CommonHelper.toDateKhoString(c.NGAY_BAT_DAU),
                            endDate = CommonHelper.toDateKhoString(c.NGAY_KET_THUC),
                            investor = c.CHU_DAU_TU,
                            location = c.DIA_DIEM,
                            syncDate = CommonHelper.toDateKhoString(DateTime.Now),
                            description = c.GHI_CHU,
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_DuAn_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "HinhThucXuLy")
                {
                    var hinhThucXuLys = _hinhThucXuLyService.GetAllHinhThucXuLysChuaDb();
                    if (hinhThucXuLys != null)
                    {
                        var lst = hinhThucXuLys.Select(c => new Kho_HinhThucXuLy_Api()
                        {
                            id = null,
                            actionType = 1,
                            syncId = (long)c.ID,
                            code = c.MA,
                            displayOrder = (long?)c.SAP_XEP,
                            name = c.TEN
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_HinhThucXuLy_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (name == "PhuongAnXuLy")
                {
                    var phuongAnXuLys = _phuongAnXuLyService.GetAllPhuongAnXuLysChuaDb();
                    if (phuongAnXuLys != null)
                    {
                        var lst = phuongAnXuLys.Select(c => new Kho_PhuongAnXuLy_Api()
                        {
                            id = null,
                            actionType = 1,
                            syncId = (long)c.ID,
                            code = c.MA,
                            displayOrder = (long?)c.SAP_XEP,
                            name = c.TEN
                        }).ToList();
                        total = lst.Count();
                        if (type == 1)
                            stream = prepareExcelEntity<Kho_PhuongAnXuLy_Api>(stream, lst, name, addSTT);
                        else
                            json = lst.toStringJson();
                    }
                    else
                    {
                        return null;
                    }
                }

                #endregion prepare

                byte[] bytes;
                string fName = $"{name}_{total}({DateTime.Now.ToString("ddMMyyyyhhmm")})";
                if (type == 1)
                {
                    bytes = stream.ToArray();
                    return new FileContentResult(bytes, MimeTypes.TextXlsx)
                    {
                        FileDownloadName = fName + ".xlsx"
                    };
                }
                else
                {
                    return new FileContentResult(Encoding.UTF8.GetBytes(json), MimeTypes.ApplicationJson)
                    {
                        FileDownloadName = fName + ".json"
                    };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual MemoryStream prepareExcelEntity<T>(MemoryStream stream, List<T> listData, string workSheetName, bool addSTT)
        {
            List<InputHeaderReportModel> dataObj = new List<InputHeaderReportModel>();
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    DisplayNameAttribute DisplayName = attr as DisplayNameAttribute;
                    if (DisplayName != null)
                    {
                        dataObj.Add(new InputHeaderReportModel() { Name = DisplayName.DisplayName });
                    }
                }
            }
            if (dataObj == null || (dataObj != null && dataObj.Count == 0))
                dataObj = typeof(T).GetProperties().Select(c => new InputHeaderReportModel() { Name = c.Name }).ToList();
            var listValue = new List<ThongTinBaoCaoValueModel>();
            foreach (var data in listData)
            {
                var val = GetPropertiesToObject(data);
                listValue.Add(val);
            }
            //dataObj.Insert(0, new InputHeaderReportModel() { Name = "STT" });
            var a = new LayoutReport()
            {
                hasColumnIndex = false,
                hasRowIndex = true,
                ListvalueContent = listValue.Select(c => c.ToEntity<ThongTinBaoCaoValue>()).ToList(),
                ListColumeHeader = PrepareLayoutExport(dataObj).Select(c => c.ToEntity<ThongTinBaoCaoColume>()).ToList()
            };
            using (stream = new MemoryStream())
            {
                _exportManager.ExportXlsxEntity(stream, a, $"{workSheetName}", addSTT);
            }
            return stream;
        }

        public virtual ThongTinBaoCaoValueModel GetPropertiesToObject<T>(T obj)
        {
            var model = new ThongTinBaoCaoValueModel();
            foreach (var prop in typeof(T).GetProperties())
            {
                // key of object
                var key = typeof(T).Name + "." + prop.Name;
                // value of object
                var value = prop.GetValue(obj, null);
                //to do
                if (value == null)
                    value = " ";
                if (prop.PropertyType.Name == "String")
                {
                    if (TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                        model.ValueContent.Add(value.ToString());
                }
                else if (prop.PropertyType.Name == "Int32")
                {
                    //if (TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(Int32)))
                    //{
                    //    int valueInt = Convert.ToInt32(value);
                    //    model.ValueContent.Add(valueInt.ToVNStringNumber());
                    //}
                    int valueInt = Convert.ToInt32(value);
                    model.ValueContent.Add(valueInt.ToVNStringNumber());
                }
                else if (prop.PropertyType.Name == "DateTime")
                {
                    if (TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(DateTime)))
                    {
                        DateTime valueDateTime = Convert.ToDateTime(value);
                        model.ValueContent.Add(valueDateTime.toDateVNString());
                    }
                }
                else
                {
                    model.ValueContent.Add(value.ToString());
                }
            }

            return model;
        }

        public virtual List<LayoutReportModel> PrepareLayoutExport(List<InputHeaderReportModel> ListModel)
        {
            var ListLayout = new List<LayoutReportModel>();
            foreach (var model in ListModel)
            {
                int i = 1;
                var layoutModel = new LayoutReportModel();
                PrepareLayoutHeader(model, layoutModel, ref i);
                ListLayout.Add(layoutModel);
            }
            int max = 1;
            int levelMax = GetMaxColLevel(ListLayout, ref max);
            PrepareRowLayoutheader(ListLayout, levelMax);
            return ListLayout;
        }

        public void PrepareLayoutHeader(InputHeaderReportModel inputHeader, LayoutReportModel LayoutModel, ref int levelColume)
        {
            int col = 1;
            LayoutModel.ColumeHeaderName = inputHeader.Name;
            LayoutModel.TypeLastRowId = inputHeader.TypeLastRowId;
            LayoutModel.ColumeHeaderLevel = levelColume;
            if (inputHeader.ListSubModel != null && inputHeader.ListSubModel.Count() > 0)
            {
                LayoutModel.ColumeHeaderColspan = PrepareColspanHeader(inputHeader, ref col);
                foreach (var a in inputHeader.ListSubModel)
                {
                    var sub = new LayoutReportModel();
                    var i = levelColume + 1;
                    PrepareLayoutHeader(a, sub, ref i);
                    LayoutModel.ListSubLayout.Add(sub);
                }
            }
            else
            {
                LayoutModel.ColumeHeaderColspan = col;
            }
        }
        public int PrepareColspanHeader(InputHeaderReportModel listmodel, ref int count)
        {
            count = listmodel.ListSubModel.Count();
            foreach (var n in listmodel.ListSubModel)
            {
                if (n.ListSubModel != null && n.ListSubModel.Count() > 0)
                {
                    count = count + PrepareColspanHeader(n, ref count) - 1;
                }
            }
            return count;
        }
        public void PrepareRowLayoutheader(List<LayoutReportModel> layoutReportModel, int levelMax)
        {
            foreach (var model in layoutReportModel)
            {
                if (model.ColumeHeaderColspan == model.ColumeHeaderLevel && model.ColumeHeaderColspan == 1)
                    model.ColumeHeaderRowspan = levelMax;
                else if (model.ColumeHeaderColspan == 1 && model.ColumeHeaderLevel < levelMax)
                    model.ColumeHeaderRowspan = levelMax - model.ColumeHeaderLevel + 1;
                else
                    model.ColumeHeaderRowspan = 1;
                if (model.ListSubLayout != null && model.ListSubLayout.Count() > 0)
                {
                    PrepareRowLayoutheader(model.ListSubLayout, levelMax);
                }
            }
        }

        public int GetMaxColLevel(List<LayoutReportModel> ListModel, ref int i)
        {
            int a = ListModel.Select(c => c.ColumeHeaderLevel).Max();
            i = a > i ? a : i;
            foreach (var model in ListModel)
            {
                if (model.ListSubLayout != null && model.ListSubLayout.Count() > 0)
                {
                    GetMaxColLevel(model.ListSubLayout, ref i);
                }
            }
            return i;
        }
    }
}