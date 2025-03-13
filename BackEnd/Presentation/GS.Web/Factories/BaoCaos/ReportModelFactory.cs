using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Fields;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.BaoCaoDoiChieus;
using GS.Services.DanhMuc;
using GS.Services.ExportImport;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.SHTD;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace GS.Web.Factories.BaoCaos
{
    public class ReportModelFactory : IReportModelFactory
    {
        #region Fields

        private readonly IBaoCaoService _baoCaoService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IWorkContext _workContext;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly IDonViService _donViService;
        private readonly IDuAnModelFactory _duAnModelFactory;
        private readonly ILoaiDonViModelFactory _loaiDonViModelFactory;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
        private readonly IHinhThucXuLyModelFactory _hinhThucXuLyModelFactory;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDuAnService _duAnService;
        private readonly INhanXeModelFactory _nhanXeModelFactory;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly IExportManager _exportManager;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IChucDanhModelFactory _chucdanhModelFactory;
        private readonly IBaoCaoDoiChieuService _baoCaoDoiChieuService;
        private readonly IDonViChuyenDoiService _donViChuyenDoiService;

        #endregion Fields

        #region Ctor

        public ReportModelFactory(IBaoCaoService baoCaoService,
            ICauHinhService cauHinhService,
            IWorkContext workContext,
            IDonViModelFactory donViModelFactory,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ICheDoHaoMonService cheDoHaoMonService,
            IDonViService donViService,
            IDuAnModelFactory duAnModelFactory,
            ILoaiDonViModelFactory loaiDonViModelFactory,
            INhomCongCuModelFactory nhomCongCuModelFactory,
            INhomCongCuService nhomCongCuService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory,
            IHinhThucXuLyModelFactory hinhThucXuLyModelFactory,
            IHinhThucXuLyService hinhThucXuLyService,
            INhanHienThiService nhanHienThiService,
            IDuAnService duAnService,
            INhanXeModelFactory nhanXeModelFactory,
            ILoaiDonViService loaiDonViService,
            IExportManager exportManager,
            IDonViBoPhanService donViBoPhanService,
            ILoaiTaiSanService loaiTaiSanService,
            IChucDanhModelFactory chucdanhModelFactory,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            IChucDanhService chucDanhService,
            IBaoCaoDoiChieuService baoCaoDoiChieuService,
            IDonViChuyenDoiService donViChuyenDoiService
            )
        {
            this._baoCaoService = baoCaoService;
            this._cauHinhService = cauHinhService;
            this._workContext = workContext;
            this._donViModelFactory = donViModelFactory;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._donViService = donViService;
            this._duAnModelFactory = duAnModelFactory;
            this._loaiDonViModelFactory = loaiDonViModelFactory;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
            this._nhomCongCuService = nhomCongCuService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
            this._hinhThucXuLyModelFactory = hinhThucXuLyModelFactory;
            this._hinhThucXuLyService = hinhThucXuLyService;
            this._nhanHienThiService = nhanHienThiService;
            this._duAnService = duAnService;
            this._nhanXeModelFactory = nhanXeModelFactory;

            this._chucdanhModelFactory = chucdanhModelFactory;

            this._loaiDonViService = loaiDonViService;
            this._exportManager = exportManager;
            this._donViBoPhanService = donViBoPhanService;
            _loaiTaiSanService = loaiTaiSanService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._chucDanhService = chucDanhService;
            this._baoCaoDoiChieuService = baoCaoDoiChieuService;
            this._donViChuyenDoiService = donViChuyenDoiService;
        }

        #endregion Ctor

        #region Report

        public virtual ThongTinBaoCaoModel ReportStatistical(List<object> listObject)
        {
            var ListModel = new ThongTinBaoCaoModel();
            // prepare value
            foreach (var obj in listObject)
            {
                var a = GetPropertiesToObject(obj);
                ListModel.ListValue.Add(a);
            }

            return ListModel;
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

        public virtual ThongTinBaoCaoModel PrepareHeaderReport(List<InputHeaderReportModel> ListModel)
        {
            var model = new ThongTinBaoCaoModel();
            foreach (var inputModel in ListModel)
            {
                int i = 1;
                PrepareThongTinHeader(inputModel, model.ListColumeHeader, ref i);
            }
            int levelMax = model.ListColumeHeader.Select(c => c.ColumeHeaderLevel).Max();
            foreach (var a in model.ListColumeHeader)
            {
                PrepareRowspanHeader(a, levelMax);
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

        public void PrepareThongTinHeader(InputHeaderReportModel InputModel, List<ThongTinBaoCaoColumeModel> listmodel, ref int levelColume)
        {
            var model = new ThongTinBaoCaoColumeModel();
            if (InputModel.ListSubModel != null && InputModel.ListSubModel.Count() > 0)
            {
                int col = 1;
                model.ColumeHeaderName = InputModel.Name;
                model.ColumeHeaderIndex = InputModel.Index;
                model.ColumeHeaderLevel = levelColume;
                model.ColumeHeaderColspan = PrepareColspanHeader(InputModel, ref col);
                listmodel.Add(model);
                int i = levelColume + 1;
                foreach (var chil in InputModel.ListSubModel)
                {
                    PrepareThongTinHeader(chil, listmodel, ref i);
                }
            }
            else
            {
                model.ColumeHeaderName = InputModel.Name;
                model.ColumeHeaderIndex = InputModel.Index;
                model.ColumeHeaderLevel = levelColume;
                model.ColumeHeaderColspan = 1;
                listmodel.Add(model);
            }
        }

        public void PrepareRowspanHeader(ThongTinBaoCaoColumeModel model, int levelMax)
        {
            if (model.ColumeHeaderColspan == model.ColumeHeaderLevel && model.ColumeHeaderColspan == 1)
                model.ColumeHeaderRowspan = levelMax;
            else if (model.ColumeHeaderColspan == 1 && model.ColumeHeaderLevel < levelMax)
                model.ColumeHeaderRowspan = levelMax - model.ColumeHeaderLevel + 1;
            else
                model.ColumeHeaderRowspan = 1;
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

        public ThongTinBaoCaoModel PrepareViewreport(string maBaoCaoEnum, ThongTinBaoCaoHeaderModel thongTinBaoCaoHeaderModel)
        {
            var model = new ThongTinBaoCaoModel();
            //var dataJson = _baoCaoService.GetBaoCaoByMa(ma: maBaoCaoEnum).NOI_DUNG;
            //var dataObj = dataJson.toEntities<InputHeaderReportModel>();
            //model = PrepareHeaderReport(dataObj);
            //model.ThongTinHeader = thongTinBaoCaoHeaderModel;
            return model;
        }

        public bool CheckMaCauHinhBaoCao(string maBaoCao)
        {
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == maBaoCao);

            if (cauHinhModel != null && cauHinhModel.Count() > 0)
            {
                return false;
            }

            return true;
        }
        public bool CheckMaCauHinhBaoCaoDB(string maBaoCao)
        {
            var cauHinh = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
            var cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoDBModel>().Where(c => c.MaBaoCao == maBaoCao);

            if (cauHinhModel != null && cauHinhModel.Count() > 0)
            {
                return false;
            }

            return true;
        }

        #endregion Report
        #region entity
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
        #endregion
        #region Prepare Model
        int[] listLyDoTang = new int[] {
            (int)enumLyDoBienDongBC.TatCa,
            (int)enumLyDoBienDongBC.MuaSam,
            (int)enumLyDoBienDongBC.TiepNhan,
            (int)enumLyDoBienDongBC.DangKyLanDau,
            (int)enumLyDoBienDongBC.NhaNuocGiaoDat,
            (int)enumLyDoBienDongBC.NhaNuocChoThueDat,
            (int)enumLyDoBienDongBC.DauTuXayDung,
            (int)enumLyDoBienDongBC.KiemKePhatHienThua
        };
        int[] listLyDoGiam = new int[] {
            (int)enumLyDoBienDongBC.TatCa,
            (int)enumLyDoBienDongBC.BanChuyenNhuong,
            (int)enumLyDoBienDongBC.BiThuHoi,
            (int)enumLyDoBienDongBC.ChuyenGiao,
            (int)enumLyDoBienDongBC.DieuChuyen,
            (int)enumLyDoBienDongBC.ThanhLy,
            (int)enumLyDoBienDongBC.TieuHuy,
            (int)enumLyDoBienDongBC.HuyHoai,
            (int)enumLyDoBienDongBC.Khac
        };
        int Dpac = 99284;
        int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.ALL, (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD };
        public virtual BaoCaoTaiSanTongHopSearchModel PrepareBaoCaoTaiSanBanQLDuAnSearchModel(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = _workContext.CurrentDonVi.TEN_DON_VI, Value = _workContext.CurrentDonVi.ID.ToString() });
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            var hasDonViQLDA = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: true);
                searchModel.DonVi = donvi.ID;
            }
            else
            {
                searchModel.DonVi = _workContext.CurrentDonVi.ID;
            }
            searchModel.DDLDuAn = _duAnModelFactory.PrepareSelectListDuAn(isAddFirst: true, donViId: searchModel.DonVi).ToList();
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            return searchModel;
        }
        public virtual BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanChiTietSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            //searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = _workContext.CurrentDonVi.TEN_DON_VI, Value = _workContext.CurrentDonVi.ID.ToString() });
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            //decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            searchModel.DDLLyDoBienDong = PrePareDDLLyDoBienDong(new int[] { (int)enumLyDoBienDongBC.TatCa });
            searchModel.DDLLyDoGiam = PrePareDDLLyDoBienDong(listLyDoTang);
            searchModel.DDLLyDoTang = PrePareDDLLyDoBienDong(listLyDoGiam);
            //searchModel.DDLNamBaoCao = new enumNamBaoCao().ToSelectList().ToList();
            int ynow = DateTime.Now.Year;
            List<SelectListItem> lstNam = new List<SelectListItem>();
            for (int i = 2018; i <= ynow; i++)
            {
                lstNam.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                    Selected = i == ynow
                });
            }
            searchModel.DDLNamBaoCao = lstNam;
            searchModel.DDLHeThong = new enumHeThong().ToSelectList().ToList();
            //searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, cheDoId: CheDoId, isDisabled:false,strFirstRow:"-- Tất cả --",valueFirstRow:"0").ToList();
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLCapBaoCao = GetSelectListsCapBaoCao(DateTime.Now);
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            searchModel.DDLBanThanhLy = new BanOrThanhLy().ToSelectList().ToList();
            searchModel.DDLBacDonVi = new enumCapDonVi().ToSelectList().ToList();
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: 0, valueFirstRow: "0").ToList();
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public virtual BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanCongKhaiSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            searchModel.ListDonViId = new List<int> { Convert.ToInt32(_workContext.CurrentDonVi.ID) };
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            //decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            searchModel.DDLLyDoBienDong = PrePareDDLLyDoBienDong();
            searchModel.DDLLyDoGiam = PrePareDDLLyDoBienDong(listLyDoTang);
            searchModel.DDLLyDoTang = PrePareDDLLyDoBienDong(listLyDoGiam);
            // searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, cheDoId: CheDoId, isDisabled:false,strFirstRow:"-- Tất cả --",valueFirstRow:"0").ToList();
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLCapBaoCao = GetSelectListsCapBaoCao(DateTime.Now);
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            searchModel.DDLBanThanhLy = new BanOrThanhLy().ToSelectList().ToList();
            searchModel.DDLBacDonVi = new enumCapDonVi().ToSelectList().ToList();
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: 0, valueFirstRow: "0").ToList();
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public virtual BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanTDSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = _workContext.CurrentDonVi.TEN_DON_VI, Value = _workContext.CurrentDonVi.ID.ToString() });
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            searchModel.DDLDonViKhoiLuong = new enumDonViKhoiLuong().ToSelectList().ToList();
            searchModel.DDLHinhThucXuLy = _hinhThucXuLyModelFactory.PrepareSelectListHinhThucXuLy(isAddFirst: true).ToList();
            decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: false, cheDoId: CheDoId, isDisabled: false).ToList();
            searchModel.DDLCapBaoCao = (new enumCapBaoCao()).ToSelectList(valuesToExclude: new int[] { 3, 4, 5, 6 }).ToList();
            searchModel.DDLNguonGocTaiSan = _nguonGocTaiSanModelFactory.PrepareSelectListNguonGocTaiSan(isAddFirst: false);
            searchModel.DDLBacDonVi = enumCapDonVi.Cap_1.ToSelectList().ToList();
            var dv = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (_workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXaTrungUong().ToSelectList().ToList();
            }
            else if (dv != null && dv.LOAI_CAP_DON_VI_ID == 2)
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXa().ToSelectList().ToList();
            }
            //searchModel.DDLCapDonVi.Insert(0, new SelectListItem { Value = "-1", Text = "Tất cả", Selected = true });
            var item = new QuyetDinhTichThuSearchModel();
            //searchModel.DDLQuyetDinhTichThuTSTD = _quyetDinhTichThuService.PrepareQuyetDinhTichThu(item, searchModel);
            if (_workContext.CurrentDonVi.ID == Dpac)
                searchModel.BacDonVi = 1;
            else
                searchModel.BacDonVi = 2;

            searchModel.SetGridPageSize();

            return searchModel;
        }

        public BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoCheDoKeToanSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, bool IsMulti = false)
        {
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: 0, valueFirstRow: "0").ToList();
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            searchModel.DDLLyDoBienDong = PrePareDDLLyDoBienDong();
            searchModel.DDLLyDoGiam = PrePareDDLLyDoBienDong(listLyDoTang);
            searchModel.DDLLyDoTang = PrePareDDLLyDoBienDong(listLyDoGiam);
            // searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, cheDoId: CheDoId, isDisabled:false,strFirstRow:"-- Tất cả --",valueFirstRow:"0").ToList();
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLCapBaoCao = GetSelectListsCapBaoCao(DateTime.Now);
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            searchModel.DDLBanThanhLy = new BanOrThanhLy().ToSelectList().ToList();
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            return searchModel;
        }

        public virtual BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanTraCuuSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var cd = _cheDoHaoMonService.GetCheDoHaoMonByMa(enumCheDoHaoMon_ThongTu.CDHM_TT45);
            List<decimal> listIgnoreLTS = new List<decimal>
            {
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
            };

            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isDisabled: false, cheDoId: cd.ID, listLoaiHinhTaiSanIgnore: listIgnoreLTS);
            searchModel.DDLCapBaoCao = GetSelectListsCapBaoCao(DateTime.Now);
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            searchModel.DDLCompareSign = new enumCompare().ToSelectList().ToList();
            searchModel.DDLBacDonVi = new enumCapDonVi().ToSelectList().ToList();
            if (_workContext.CurrentDonVi.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXaTrungUong().ToSelectList().ToList();
            }
            else
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXa().ToSelectList().ToList();
            }
            //searchModel.DDLCapDonVi.Insert(0, new SelectListItem() { Value = "-1", Text = "Tất cả", Selected = true });
            //đặc biệt
            searchModel.DDLLoaiDonVi = _loaiDonViModelFactory.PrepareMultiSelectLoaiDonViForBaoCao().ToList();
            searchModel.DDLNhanXe = _nhanXeModelFactory.PrepareSelectListNhanXe(isAddFirst: true).ToList();

            searchModel.DDLChucDanh = _chucdanhModelFactory.PrepareSelectListChucDanh(isAddFirst: true).ToList();

            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            searchModel.SetGridPageSize();

            return searchModel;
        }
        public BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoCCDCSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, bool IsMulti = false)
        {
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID, strFirstRow: "Tất cả");
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, strFirstRow: "Tất cả").ToList();
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 1, 1);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.ListDonViId = new List<int> { (int)_workContext.CurrentDonVi.ID };
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            searchModel.DDLLyDoGiam = (new enumLyDoGiam()).ToSelectList(valuesToExclude: new int[] { 5, 6 }).ToList();
            searchModel.DDLLyDoTang = (new enumMucDichXuatNhap()).ToSelectList(valuesToExclude: new int[] { 0 }).ToList();
            searchModel.DDLTangOrGiam = Enum.GetValues(typeof(enumLyDoGiam)).Cast<enumLyDoGiam>().Select(c => new SelectListItem
            {
                Value = $"G-{(int)c}",
                Text = _nhanHienThiService.GetGiaTriEnum(c)
            }).Where(xxx => xxx.Value != "5" && xxx.Value != "6").ToList();
            var MucDichXuatNhap = Enum.GetValues(typeof(enumMucDichXuatNhap)).Cast<enumMucDichXuatNhap>().Select(c => new SelectListItem
            {
                Value = $"T-{(int)c}",
                Text = _nhanHienThiService.GetGiaTriEnum(c)
            }).ToList();
            searchModel.DDLTangOrGiam.AddRange(MucDichXuatNhap);
            searchModel.DDLTangOrGiam.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "-- Chọn lý do tăng / giảm --"
            });
            //searchModel.DDLTangOrGiam.Insert(1, new SelectListItem
            //{
            //    Value = 1.ToString(),
            //    Text = "Báo cáo tăng"
            //});
            //searchModel.DDLTangOrGiam.Insert(2, new SelectListItem
            //{
            //    Value = 2.ToString(),
            //    Text = "Báo cáo giảm"
            //});
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            return searchModel;
        }
        public virtual BaoCaoTaiSanTongHopSearchModel PrepareBaoCaoTaiSanTongHopSearchModel(BaoCaoTaiSanTongHopSearchModel searchModel, Boolean IsMulti = false)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.ListDonViId = new List<int> { (int)_workContext.CurrentDonVi.ID };
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            searchModel.DDLLyDoBienDong = PrePareDDLLyDoBienDong();
            searchModel.DDLLyDoGiam = PrePareDDLLyDoBienDong(listLyDoTang);
            searchModel.DDLLyDoTang = PrePareDDLLyDoBienDong(listLyDoGiam);
            //decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            searchModel.DDLLoaiDonVi = _loaiDonViModelFactory.PrepareMultiSelectLoaiDonViForBaoCao().ToList();
            searchModel.DDLDuAn = _duAnModelFactory.PrepareSelectListDuAn(isAddFirst: true, donViId: _workContext.CurrentDonVi.ID).ToList();
            //searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, cheDoId: CheDoId, isDisabled: false, strFirstRow: "-- Tất cả --", valueFirstRow: "0").ToList();
            //if (IsMulti)
            //{
            //    searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: new int[] {
            //    (int)enumLOAI_HINH_TAI_SAN.ALL,
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_HA_TANG_NONG_THON,
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CONG_TRINH_NUOC_SACH,
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, });
            //}
            //else
            //{
            //    searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: new int[] {
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_HA_TANG_NONG_THON,
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CONG_TRINH_NUOC_SACH,
            //    (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, });
            //}
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.DDLCapBaoCao = GetSelectListsCapBaoCao(DateTime.Now);
            searchModel.DDLSo_luong_Object = new enumSoLuong().ToSelectList().ToList();
            searchModel.DDLBanThanhLy = new BanOrThanhLy().ToSelectList().ToList();
            searchModel.DDLBacDonVi = new enumCapDonVi().ToSelectList().ToList();
            int ynow = DateTime.Now.Year;
            List<SelectListItem> lstNam = new List<SelectListItem>();
            for (int i = 2018; i <= ynow; i++)
            {
                lstNam.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                    Selected = i == ynow
                });
            }
            searchModel.DDLNamBaoCao = lstNam;
            //searchModel.DDLNamBaoCao = new enumNamBaoCao().ToSelectList().ToList();
            searchModel.ddlHeThong = new enumHeThong().ToSelectList().ToList();
            var dv = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            //là địa phương mới có
            if (dv.ID == (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXaTrungUong().ToSelectList().ToList();
                searchModel.IsDPAC = true;
                searchModel.IsHideDetail = true;
            }
            else if (dv != null && dv.LOAI_CAP_DON_VI_ID == 2)
            {
                searchModel.DDLCapDonVi = new enumTinhHuyenXa().ToSelectList().ToList();
            }
            //searchModel.DDLCapDonVi.Insert(0, new SelectListItem { Value = "-1", Text = "", Selected = true });
            if (_workContext.CurrentDonVi.ID == Dpac)
            {
                searchModel.BacDonVi = 1;
            }
            else
            {
                searchModel.BacDonVi = 2;
            }
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public virtual List<SelectListItem> GetSelectListsCapBaoCao(DateTime NgayBaoCao)
        {
            var cdhm = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(NgayBaoCao);
            if (cdhm != null && cdhm.MA_CHE_DO == "TT162")
            {
                return new enumCapBaoCao().ToSelectList().ToList();
            }
            else if (cdhm != null && cdhm.MA_CHE_DO == "TT45")
            {
                var lst_exclude = new List<int> { (int)enumCapBaoCao.Cap_6 };
                return new enumCapBaoCao().ToSelectList(valuesToExclude: lst_exclude.ToArray()).ToList();
            }
            else
            {
                return new enumCapBaoCao().ToSelectList().ToList();
            }
        }
        /// <summary>
        /// vì lấy theo mã nên phải chuyển value => mã
        /// </summary>
        /// <param name="arryExclude"></param>
        /// <returns></returns>
        public virtual List<SelectListItem> PrePareDDLLyDoBienDong(int[] arryExclude = null)
        {
            return new enumLyDoBienDongBC().ToSelectList(valuesToExclude: arryExclude).Select(c => new SelectListItem
            {
                Value = (c.Value != "0" ? c.Value.PadLeft(3, '0') : "0"),
                Text = c.Text,
                Selected = c.Value == "0"
            }).ToList();
        }
        public virtual void CauHinhChungBaoCao(object sender, CauHinhBaoCaoChungModel _cauHinhBaoCaoChungModel, IEnumerable<XRControl> listControl)
        {
            //cấu hình chung
            if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.Color))
            {
                _cauHinhBaoCaoChungModel.Color = "#FFF";
            }
            if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.ColorBorder))
            {
                _cauHinhBaoCaoChungModel.ColorBorder = "#000";
            }
            if (String.IsNullOrEmpty(_cauHinhBaoCaoChungModel.Font))
            {
                _cauHinhBaoCaoChungModel.Font = "Times New Roman";
            }
            if (_cauHinhBaoCaoChungModel.WidthBorder == 0)
            {
                _cauHinhBaoCaoChungModel.WidthBorder = 1;
            }
            var report = (sender as XtraReport);
            report.BackColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.Color);
            foreach (var item in listControl)
            {
                item.Font = new System.Drawing.Font(_cauHinhBaoCaoChungModel.Font, item.Font.Size, item.Font.Style);
                item.BorderColor = ColorTranslator.FromHtml(_cauHinhBaoCaoChungModel.ColorBorder);
                item.BorderWidth = _cauHinhBaoCaoChungModel.WidthBorder;
                item.BorderDashStyle = (BorderDashStyle)_cauHinhBaoCaoChungModel.StyleBorder;
            }
            if (_cauHinhBaoCaoChungModel.MarginTop > 0)
            {
                report.Margins.Top = Convert.ToInt32(_cauHinhBaoCaoChungModel.MarginTop * 0.3937 * 100);
            }
            if (_cauHinhBaoCaoChungModel.MarginBottom > 0)
            {
                report.Margins.Bottom = Convert.ToInt32(_cauHinhBaoCaoChungModel.MarginBottom * 0.3937 * 100);
            }
        }

        public void PrePareDonViBaoCao(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (searchModel.DonVi < 1)
            {
                searchModel.DonVi = _workContext.CurrentDonVi.ID;
            }
            var donviBC = _donViService.GetDonViById(searchModel.DonVi);
            if (donviBC.TREE_LEVEL == 1)
            {
                searchModel.TEN_BO_NGANH = donviBC.TEN;
                searchModel.TEN_DON_VI_CHA = donviBC.TEN;
            }
            else
            {
                var donviBoNganh = _donViService.GetDonViById(donviBC.TREE_NODE.Split('-')[0].ToNumberInt32());
                searchModel.TEN_BO_NGANH = donviBoNganh?.TEN;
                var donvicha = _donViService.GetDonViById(donviBC.PARENT_ID.GetValueOrDefault(0));
                searchModel.TEN_DON_VI_CHA = donvicha?.TEN;

            }
            if (donviBC != null)
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(donviBC.ID, searchModel.NgayBatDau ?? searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = donviBC.TEN;
                }
                searchModel.MA_DON_VI = donviBC.MA;
                searchModel.MA_QUAN_HE_NGAN_SACH = donviBC.MA_DVQHNS;
                searchModel.TenLoaiHinhDonVi = donviBC.LoaiDonVi?.TEN;
            }
            else
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(_workContext.CurrentDonVi.ID, searchModel.NgayBatDau ?? searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
                }
                searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            }
            if (searchModel.DuAnId > 0)
            {
                searchModel.tenDuAn = _duAnService.GetDuAnById(searchModel.DuAnId)?.TEN;
            }

            //if (searchModel.CapDonVi < 0)
            //{
            //    searchModel.TenCapHanhChinh = "Tất cả";
            //}
            //else
            //{
            //    if (searchModel.MA_DON_VI == "018999")
            //    {
            //        searchModel.TenCapHanhChinh = _nhanHienThiService.GetGiaTriEnum((enumTinhHuyenXaTrungUong)(searchModel.CapDonVi));

            //    }
            //    else
            //    {
            //        searchModel.TenCapHanhChinh = _nhanHienThiService.GetGiaTriEnum((enumTinhHuyenXa)(searchModel.CapDonVi));
            //    }

            //}
            if (string.IsNullOrEmpty(searchModel.StringLoaiDonVi))
            {
                searchModel.TenLoaiDonVi = "Tất cả";
            }
            else
            {
                var ListTenLoaiDonVi = new List<string>();
                var listLoaiDonViId = searchModel.StringLoaiDonVi.ToListInt();
                if (listLoaiDonViId.Count() > 0)
                {
                    foreach (var loaiDonVi in listLoaiDonViId)
                    {
                        var donvi = _loaiDonViService.GetLoaiDonViById(loaiDonVi)?.TEN ?? "";
                        ListTenLoaiDonVi.Add(donvi);
                    }
                }
                searchModel.TenLoaiDonVi = string.Join("; ", ListTenLoaiDonVi);
            }

            if (string.IsNullOrEmpty(searchModel.StringLoaiTaiSan))
            {
                searchModel.TenLoaiHinhTaiSanPrint = "Tất cả";
            }
            else
            {
                var ListTenLoaiHinhTaiSan = new List<string>();
                var ListLoaiHinhTaiSanId = searchModel.StringLoaiTaiSan.ToListInt();
                if (ListLoaiHinhTaiSanId.Count() > 0)
                {
                    foreach (var loaiHinhTaiSan in ListLoaiHinhTaiSanId)
                    {
                        var donvi = _nhanHienThiService.GetGiaTriEnum((enumLOAI_HINH_TAI_SAN)loaiHinhTaiSan);
                        ListTenLoaiHinhTaiSan.Add(donvi);
                    }
                }
                searchModel.TenLoaiHinhTaiSanPrint = string.Join("; ", ListTenLoaiHinhTaiSan);
            }

            PrepareEnumeBaoCaoTongHop(searchModel);
        }
        public void PrePareDonViBaoCaoCT(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var ppt = searchModel.GetType().GetProperties();
            //foreach (var t in ppt)
            //{				
            //	var type = Nullable.GetUnderlyingType(t.PropertyType) ?? t.PropertyType;			
            //	if (type.GetProperty("Item")!= null)
            //	{
            //		if (type.GetProperty("Item").PropertyType.IsEnum)
            //		{
            //			var GT = t.GetValue(searchModel, null);						
            //		}
            //	}
            //	if (type.IsEnum)
            //	{
            //		var GT = t.GetValue(searchModel, null);
            //		searchModel.lstNhanHienThi.Add(type.Name,"Nhãn");
            //	}
            //}

            if (searchModel.DonVi < 1)
            {
                searchModel.DonVi = _workContext.CurrentDonVi.ID;
            }
            var donviBC = _donViService.GetDonViById(searchModel.DonVi);
            searchModel.CURRENT_CAP_DON_VI = donviBC.CAP_DON_VI_ID;
            searchModel.DIA_CHI_DON_VI = donviBC.DIA_CHI;
            if (donviBC.TREE_LEVEL == 1)
            {
                searchModel.TEN_BO_NGANH = donviBC.TEN;
                searchModel.TEN_DON_VI_CAP_TREN = donviBC.TEN;
                searchModel.MA_DON_VI_CAP_TREN = donviBC.MA;
            }
            else
            {
                var donviBoNganh = _donViService.GetDonViById(donviBC.TREE_NODE.Split('-')[0].ToNumberInt32());
                searchModel.TEN_BO_NGANH = donviBoNganh != null ? donviBoNganh.TEN : "";
                var donvicha = _donViService.GetDonViById((decimal)donviBC.PARENT_ID);
                searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : "";
                searchModel.MA_DON_VI_CAP_TREN = donvicha != null ? donvicha.MA : "";
            }
            if (donviBC != null)
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(donviBC.ID, searchModel.NgayBatDau ?? searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = donviBC.TEN;
                }
                searchModel.MA_DON_VI = donviBC.MA;
                searchModel.MA_QUAN_HE_NGAN_SACH = donviBC.MA_DVQHNS;
                searchModel.TenLoaiHinhDonVi = donviBC.LoaiDonVi.TEN;
            }
            else
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(_workContext.CurrentDonVi.ID, searchModel.NgayBatDau ?? searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
                }
                searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            }
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            if (searchModel.DU_AN_ID > 0)
            {
                searchModel.tenDuAn = _duAnService.GetDuAnById(searchModel.DU_AN_ID.Value)?.TEN;
            }
            if (!string.IsNullOrEmpty(searchModel.ListStringDonVi))
            {
                var listDonViBoPhanId = searchModel.ListStringDonVi.Split(",").Select(p => decimal.Parse(p)).ToList();
                searchModel.TenListDonViBoPhan = _donViBoPhanService.GetStringTenDonViBoPhan(listDonViBoPhanId);
            }
            else
            {
                searchModel.TenListDonViBoPhan = "Tất cả";
            }
            if (!string.IsNullOrEmpty(searchModel.ListStringCongCu))
            {
                var listCongCuId = searchModel.ListStringCongCu.Split(",").Select(p => decimal.Parse(p)).ToList();
                searchModel.TenListNhomCongCu = _nhomCongCuService.GetStringNhomCongCu(listCongCuId);
            }
            if (!string.IsNullOrEmpty(searchModel.StringLoaiTaiSan))
            {
                var list_LtsId = searchModel.StringLoaiTaiSan.Split(',').Select(p => decimal.Parse(p)).ToList();
                if (list_LtsId != null && list_LtsId.Count > 0)
                {
                    var tenLTS = _loaiTaiSanService.GetTenLoaiTaiSan(list_LtsId);
                    if (tenLTS != null && tenLTS.Count > 0)
                    {
                        searchModel.StringDisplayListLoaiTaiSan = string.Join(", ", tenLTS);
                    }
                }

            }
            else
            {
                searchModel.StringDisplayListLoaiTaiSan = "Tất cả";
            }
            // Thông tin quyết định tịch thu cho báo cáo tài sản toàn dân
            if (searchModel.QuyetDinhTichThuTSTD > 0)
            {
                var quyetDinhTichThu = _quyetDinhTichThuService.GetQuyetDinhTichThuById(searchModel.QuyetDinhTichThuTSTD);
                if (quyetDinhTichThu != null)
                {
                    searchModel.SoQuyetDinhTichThu = quyetDinhTichThu.QUYET_DINH_SO;
                    searchModel.NguoiQuyetDinh = quyetDinhTichThu.NGUOI_QUYET_DINH;
                    searchModel.NgayQuyetDinhTichThuString = quyetDinhTichThu.QUYET_DINH_NGAY.toDateVNString();
                }
            }
            if (searchModel.SoCau_CompareSign != null)
            {
                switch (searchModel.SoCau_CompareSign)
                {
                    case ((int)enumCompare.All):
                        searchModel.TenCauXePrint = _nhanHienThiService.GetGiaTriEnum(enumCompare.All);
                        break;
                    case ((int)enumCompare.Larger):
                        searchModel.TenCauXePrint = $"{_nhanHienThiService.GetGiaTriEnum(enumCompare.Larger)} {searchModel.SoCau_Value1}";
                        break;
                    case ((int)enumCompare.LargerOrEqual):
                        searchModel.TenCauXePrint = $"{_nhanHienThiService.GetGiaTriEnum(enumCompare.LargerOrEqual)} {searchModel.SoCau_Value1}";
                        break;
                    case ((int)enumCompare.Smaller):
                        searchModel.TenCauXePrint = $"{_nhanHienThiService.GetGiaTriEnum(enumCompare.Smaller)} {searchModel.SoCau_Value1}";
                        break;
                    case ((int)enumCompare.SmallerOrEqual):
                        searchModel.TenCauXePrint = $"{_nhanHienThiService.GetGiaTriEnum(enumCompare.SmallerOrEqual)} {searchModel.SoCau_Value1}";
                        break;
                    case ((int)enumCompare.Equal):
                        searchModel.TenCauXePrint = $"{searchModel.SoCau_Value1}";
                        break;
                    case ((int)enumCompare.InRange):
                        searchModel.TenCauXePrint = $" >= {searchModel.SoCau_Value1} và <=  {searchModel.SoCau_Value2} ";
                        break;
                    default:
                        break;
                }
            }
            if (searchModel.CHUC_DANH_ID != null)
            {
                searchModel.TenChucDanhPrint = _chucDanhService.GetChucDanhById(searchModel.CHUC_DANH_ID ?? 0)?.TEN_CHUC_DANH;
            }
            else
            {
                searchModel.TenChucDanhPrint = "Tất cả";
            }
            if (searchModel.HinhThucXuLyId != null)
            {
                searchModel.TenHinhThucXuLy = _hinhThucXuLyService.GetHinhThucXuLyById(searchModel.HinhThucXuLyId ?? 0)?.TEN;
            }
            else
            {
                searchModel.TenHinhThucXuLy = "Tất cả";
            }
            if (!string.IsNullOrEmpty(searchModel.StringCapHanhChinh))
            {
                searchModel.TenCapDonViPrint = "";
            }
            else
            {
                searchModel.TenCapDonViPrint = "Tất cả";
            }

            PrepareEnumeBaoCaoChiTiet(searchModel);
        }
        public void PrePareDonViBaoCaoTraCuu(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (searchModel.DonVi < 1)
            {
                searchModel.DonVi = _workContext.CurrentDonVi.ID;
            }
            var donviBC = _donViService.GetDonViById(searchModel.DonVi);
            if (donviBC.TREE_LEVEL == 1)
            {
                searchModel.TEN_BO_NGANH = donviBC.TEN;
                searchModel.TEN_DON_VI_CAP_TREN = donviBC.TEN;
                searchModel.MA_DON_VI_CAP_TREN = donviBC.MA;
            }
            else
            {
                var donviBoNganh = _donViService.GetDonViById(donviBC.TREE_NODE.Split('-')[0].ToNumberInt32());
                searchModel.TEN_BO_NGANH = donviBoNganh != null ? donviBoNganh.TEN : "";
                var donvicha = _donViService.GetDonViById((decimal)donviBC.PARENT_ID);
                searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : "";
                searchModel.MA_DON_VI_CAP_TREN = donvicha != null ? donvicha.MA : "";
            }
            if (donviBC != null)
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(donviBC.ID, searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = donviBC.TEN;
                }
                searchModel.MA_DON_VI = donviBC.MA;
                searchModel.MA_QUAN_HE_NGAN_SACH = donviBC.MA_DVQHNS;
                searchModel.TenLoaiHinhDonVi = donviBC.LoaiDonVi.TEN;
            }
            else
            {
                var dvChuyenDoi = _donViChuyenDoiService.GetDonViChuyenDoiByDonViId(_workContext.CurrentDonVi.ID, searchModel.NgayKetThuc, searchModel.NamBaoCao);
                if (dvChuyenDoi != null)
                {
                    searchModel.TEN_DON_VI = dvChuyenDoi.TEN != null ? dvChuyenDoi.TEN : donviBC.TEN;
                }
                else
                {
                    searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
                }
                searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            }
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            if (searchModel.DU_AN_ID > 0)
            {
                searchModel.tenDuAn = _duAnService.GetDuAnById(searchModel.DU_AN_ID.Value)?.TEN;
            }
            if (!string.IsNullOrEmpty(searchModel.StringLoaiDonVi))
            {
                var list_idLoaiDonVi = searchModel.StringLoaiDonVi.Split(",").Select(p => decimal.Parse(p)).ToList();
                searchModel.TenLoaiDonVi = _loaiDonViService.GetTenLoaiDonViByIds(list_idLoaiDonVi);
            }

            if (searchModel.CapDonVi == null)
            {
                searchModel.TenCapDonViTinhHuyenXa = "Tất cả";
            }
            //else
            //{
            //    searchModel.TenCapDonViTinhHuyenXa = _nhanHienThiService.GetGiaTriEnum<enumTinhHuyenXaTrungUong>((enumTinhHuyenXaTrungUong)searchModel.CapDonVi);
            //}
            PrepareEnumeBaoCaoChiTiet(searchModel);
        }
        public CauHinhBaoCaoModel PrePareCauHinhBaoCaoModel(CauHinhBaoCaoModel model, string MaBaoCao)
        {
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            if (cauHinh.BaoCao != null)
            {
                model = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == MaBaoCao).FirstOrDefault();
                if (model != null)
                {
                    CheckCauHinhBaoCao(model);
                    return model;
                }
            }
            model = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: 0).BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == MaBaoCao).FirstOrDefault() ?? new CauHinhBaoCaoModel();
            CheckCauHinhBaoCao(model);
            return model;
        }

        public CauHinhBaoCaoChungModel PrePareCauHinhBaoCaoChungModel(CauHinhBaoCaoChungModel model)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            if (cauHinhChung.BaoCao != null)
            {
                model = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault();
            }
            else
            {
                model = new CauHinhBaoCaoChungModel();
            }
            return model;
        }

        public string PrePareStringDonViBoPhanModel(string model)
        {
            if (model != null)
            {
                var stringdvbp = new List<string>();
                var listbophanid = model.Split(',').ToList();
                foreach (var bophanid in listbophanid)
                {
                    var bophan = _donViService.GetDonViById(bophanid.ToNumberInt32());
                    if (bophan != null)
                    {
                        stringdvbp.Add(bophan.TEN);
                    }
                }
                model = string.Join(',', stringdvbp);
            }
            return model;
        }
        public string PrePareListLyDoGiamStringModel(IList<int> model)
        {
            string result = null;
            if (model != null)
            {
                var stringLyDos = new List<string>();
                if (model != null && model.Count() > 0)
                {
                    foreach (var lyDoId in model)
                    {
                        var tenLyDo = $" {_nhanHienThiService.GetGiaTriEnum((enumLyDoGiam)lyDoId)}";
                        if (tenLyDo != null)
                        {
                            stringLyDos.Add(tenLyDo);
                        }
                    }
                    result = string.Join(';', stringLyDos);
                }


            }
            return result;
        }

        public string PrePareListLyDoTangStringModel(IList<int> model)
        {
            string result = null;
            if (model != null)
            {
                var stringLyDos = new List<string>();
                if (model != null && model.Count() > 0)
                {
                    foreach (var lyDoId in model)
                    {
                        var tenLyDo = $" {_nhanHienThiService.GetGiaTriEnum((enumMucDichXuatNhap)lyDoId)}";
                        if (tenLyDo != null)
                        {
                            stringLyDos.Add(tenLyDo);
                        }
                    }
                    result = string.Join(';', stringLyDos);
                }
            }
            return result;
        }
        public string PrePareListLyDoTangGiamStringModel(string ListLyDoIdString)
        {
            string result = null;
            var ListLyDoGiamSelected = new List<int> { };
            var ListLyDoTangSelected = new List<int> { };
            if (!string.IsNullOrEmpty(ListLyDoIdString))
            {
                ListLyDoGiamSelected = ListLyDoIdString.Split(',').Select(c => c.Split('-')).Where(c => c[0] == "G").Select(c => int.Parse(c[1])).ToList();
                ListLyDoTangSelected = ListLyDoIdString.Split(',').Select(c => c.Split('-')).Where(c => c[0] == "T").Select(c => int.Parse(c[1])).ToList();
            }
            var stringLyDos = new List<string>();
            if (ListLyDoGiamSelected != null && ListLyDoGiamSelected.Count() > 0)
            {
                string tenLyDo = "";
                foreach (var lyDoGiam in ListLyDoGiamSelected)
                {
                    tenLyDo = $" {_nhanHienThiService.GetGiaTriEnum((enumLyDoGiam)lyDoGiam)}";
                    if (tenLyDo != null)
                    {
                        stringLyDos.Add(tenLyDo);
                    }
                }

            }

            if (ListLyDoTangSelected != null && ListLyDoTangSelected.Count() > 0)
            {
                string tenLyDo = ""; foreach (var lyDoGiam in ListLyDoTangSelected)
                {
                    tenLyDo = $" {_nhanHienThiService.GetGiaTriEnum((enumMucDichXuatNhap)lyDoGiam)}";
                    if (tenLyDo != null)
                    {
                        stringLyDos.Add(tenLyDo);
                    }
                }
            }
            result = string.Join(';', stringLyDos);

            return result;
        }

        /// <summary>
        /// Trường hợp báo cáo kết hợp tăng và giảm => chuẩn bị cả List lý do tăng  và Giảm cho searchmodel
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public void PrePareLyDoTangVaLyDoGiamSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var ListLyDoGiamSelected = new List<int> { };
            var ListLyDoTangSelected = new List<int> { };
            if (!string.IsNullOrEmpty(searchModel.ListLyDoIdString))
            {
                ListLyDoGiamSelected = searchModel.ListLyDoIdString.Split(',').Select(c => c.Split('-')).Where(c => c[0] == "G")?.Select(c => int.Parse(c[1])).ToList();
                ListLyDoTangSelected = searchModel.ListLyDoIdString.Split(',').Select(c => c.Split('-')).Where(c => c[0] == "T")?.Select(c => int.Parse(c[1])).ToList();
            }

            if (ListLyDoGiamSelected != null && ListLyDoGiamSelected.Count() > 0)
            {
                searchModel.StringListLyDoGiam = string.Join(',', ListLyDoGiamSelected);
            }
            if (ListLyDoTangSelected != null && ListLyDoTangSelected.Count() > 0)
            {
                searchModel.StringListLyDoTang = string.Join(',', ListLyDoTangSelected);
            }

        }
        public void CheckCauHinhBaoCao(CauHinhBaoCaoModel model)
        {
            foreach (var t in model.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(t.PropertyType) ?? t.PropertyType;
                if (type == typeof(String))
                {
                    var GT = t.GetValue(model, null);
                    if (GT != null)
                    {
                        if (String.IsNullOrEmpty(GT.ToString()))
                        {
                            t.SetValue(model, null, null);
                        }
                    }
                }
            }
        }

        public void PrePareCauHinhBaoCao(CauHinhBaoCaoModel ch, CauHinhBaoCaoChungModel chc, string MaBaoCao)
        {
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            bool isCH = false;
            if (cauHinh.BaoCao != null)
            {
                var model = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == MaBaoCao).FirstOrDefault();
                if (model != null)
                {
                    ch = model;
                    isCH = true;
                }
            }
            else if (cauHinh.BaoCao == null || isCH == false)
                ch = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: 0).BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == MaBaoCao).FirstOrDefault() ?? new CauHinhBaoCaoModel();

            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            if (cauHinhChung.BaoCao != null)
            {
                chc = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault();
            }
        }

        public BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoKeKhaiSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, bool IsMulti = false, Boolean isTaiSanDuAn = false)
        {

            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);

            searchModel.NgayBatDau = new DateTime(DateTime.Now.Year, 01, 01);
            searchModel.NgayKetThuc = DateTime.Now;
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonViTien = new enumDonViTien().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTich().ToSelectList().ToList();
            //searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = _workContext.CurrentDonVi.TEN_DON_VI, Value = _workContext.CurrentDonVi.ID.ToString() });
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            if (isTaiSanDuAn)
            {
                var hasDonViQLDA = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
                if (hasDonViQLDA)
                {
                    var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: true);
                    searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = donvi.TEN, Value = donvi.ID.ToString() });
                    searchModel.DonVi = donvi.ID;
                }
                else
                {
                    searchModel.DonVi = _workContext.CurrentDonVi.ID;
                }
                searchModel.DDLDuAn = _duAnModelFactory.PrepareSelectListDuAn(isAddFirst: true, donViId: searchModel.DonVi).ToList();

            }
            else
            {
                searchModel.DDLDuAn = _duAnModelFactory.PrepareSelectListDuAn(donViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
                searchModel.DDLDonVi.Insert(0, new SelectListItem { Text = _workContext.CurrentDonVi.TEN_DON_VI, Value = _workContext.CurrentDonVi.ID.ToString() });
            }
            return searchModel;
        }
        public void PrepareEnumeBaoCaoChiTiet(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var ppt = searchModel.GetType().GetProperties();
            foreach (var t in ppt)
            {
                var type = Nullable.GetUnderlyingType(t.PropertyType) ?? t.PropertyType;
                if (type.GetProperty("Item") != null)
                {
                    if (type.GetProperty("Item").PropertyType.IsEnum)
                    {
                        var GTs = t.GetValue(searchModel, null);
                        var stringEnum = type.GetProperty("Item").PropertyType.FullName;
                        if (GTs != null)
                        {
                            var x = (IList)GTs;
                            var result = "";
                            var listResult = "";
                            foreach (var GT in x)
                            {
                                var sourceEnum = "enums." + stringEnum + "." + GT;
                                result = _nhanHienThiService.GetGiaTri(sourceEnum);
                                if (listResult != "")
                                {
                                    listResult = listResult + ", " + result;
                                }
                                else
                                {
                                    listResult = result;
                                }
                                searchModel.lstNhanHienThi.Add(stringEnum + "." + GT, result);
                            }
                            searchModel.lstNhanHienThi.Add("List." + stringEnum, listResult);
                        }
                        else
                            searchModel.lstNhanHienThi.Add("List." + stringEnum, "Tất cả");
                    }
                }
                if (type.IsEnum)
                {
                    var GT = t.GetValue(searchModel, null);
                    if (GT != null)
                    {
                        var sourceEnum = "enums." + type.FullName.ToString() + "." + GT;
                        var result = _nhanHienThiService.GetGiaTri(sourceEnum);
                        searchModel.lstNhanHienThi.Add(type.FullName.ToString(), result);
                    }
                    else
                    {
                        searchModel.lstNhanHienThi.Add(type.FullName.ToString(), "Tất cả");
                    }
                }
            }
        }
        public void PrepareEnumeBaoCaoTongHop(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var ppt = searchModel.GetType().GetProperties();
            foreach (var t in ppt)
            {
                var type = Nullable.GetUnderlyingType(t.PropertyType) ?? t.PropertyType;
                if (type.GetProperty("Item") != null)
                {
                    if (type.GetProperty("Item").PropertyType.IsEnum)
                    {
                        var GTs = t.GetValue(searchModel, null);
                        var stringEnum = type.GetProperty("Item").PropertyType.FullName;
                        if (GTs != null)
                        {
                            var x = (IList)GTs;
                            var result = "";
                            var listResult = "";
                            foreach (var GT in x)
                            {
                                var sourceEnum = "enums." + stringEnum + "." + GT;
                                result = _nhanHienThiService.GetGiaTri(sourceEnum);
                                if (listResult != "")
                                {
                                    listResult = listResult + ", " + result;
                                }
                                else
                                {
                                    listResult = result;
                                }
                                searchModel.lstNhanHienThi.Add(stringEnum + "." + GT, result);
                            }
                            searchModel.lstNhanHienThi.Add("List." + stringEnum, listResult);
                        }
                        else
                            searchModel.lstNhanHienThi.Add("List." + stringEnum, "Tất cả");
                    }
                }
                if (type.IsEnum)
                {
                    var GT = t.GetValue(searchModel, null);
                    if (GT != null)
                    {
                        var sourceEnum = "enums." + type.FullName.ToString() + "." + GT;
                        var result = _nhanHienThiService.GetGiaTri(sourceEnum);
                        searchModel.lstNhanHienThi.Add(type.FullName.ToString(), result);
                    }
                    else
                    {
                        searchModel.lstNhanHienThi.Add(type.FullName.ToString(), "Tất cả");
                    }
                }
            }
        }
        #endregion

        #region group báo cáo đồng bộ
        //public List<TS_BCTH_02A_DK_TSNN> Group_TS_BCTH_02A_DK_TSNN(List<TS_BCTH_02A_DK_TSNN> models)
        //{
        //    if (models == null || (models != null && models.Count() == 0))
        //        return null;
        //    List<TS_BCTH_02A_DK_TSNN> result = new List<TS_BCTH_02A_DK_TSNN>();
        //    var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
        //    foreach (var loaiTaiSan in loaiTaiSans)
        //    {
        //        TS_BCTH_02A_DK_TSNN item = new TS_BCTH_02A_DK_TSNN();
        //        item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
        //        var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
        //        item.SO_LUONG = lstTaiSan.Count();
        //        item.NGUYEN_GIA_NGAN_SACH = lstTaiSan.Sum(x => x.NGUYEN_GIA_NGAN_SACH);
        //        item.NGUYEN_GIA_KHAC = lstTaiSan.Sum(x => x.NGUYEN_GIA_KHAC);
        //        item.NGUYEN_GIA = item.NGUYEN_GIA_NGAN_SACH + item.NGUYEN_GIA_KHAC;
        //        item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
        //        item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
        //        result.Add(item);
        //    }
        //    return result;

        //}
        public List<TS_BCTH_02B_DK_TSNN> Group_TS_BCTH_02B_DK_TSNN(List<TS_BCTH_02B_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_02B_DK_TSNN> result = new List<TS_BCTH_02B_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_02B_DK_TSNN item = new TS_BCTH_02B_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
                item.QUAN_LY_NHA_NUOC = lstTaiSan.Sum(x => x.QUAN_LY_NHA_NUOC);
                item.KHONG_KINH_DOANH = lstTaiSan.Sum(x => x.KHONG_KINH_DOANH);
                item.KINH_DOANH = lstTaiSan.Sum(x => x.KINH_DOANH);
                item.CHO_THUE = lstTaiSan.Sum(x => x.CHO_THUE);
                item.LDLK = lstTaiSan.Sum(x => x.LDLK);
                item.SU_DUNG_HON_HOP = lstTaiSan.Sum(x => x.SU_DUNG_HON_HOP);
                item.KHAC = lstTaiSan.Sum(x => x.KHAC);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCTH_02C_DK_TSNN> Group_TS_BCTH_02C_DK_TSNN(List<TS_BCTH_02C_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_02C_DK_TSNN> result = new List<TS_BCTH_02C_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_02C_DK_TSNN item = new TS_BCTH_02C_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.DAU_KY_SO_LUONG = lstTaiSan.Sum(x => x.DAU_KY_SO_LUONG);
                item.DAU_KY_DIEN_TICH = lstTaiSan.Sum(x => x.DAU_KY_DIEN_TICH);
                item.DAU_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.DAU_KY_NGUYEN_GIA);
                item.TANG_TRONG_KY_SO_LUONG = lstTaiSan.Sum(x => x.TANG_TRONG_KY_SO_LUONG);
                item.TANG_TRONG_KY_DIEN_TICH = lstTaiSan.Sum(x => x.TANG_TRONG_KY_DIEN_TICH);
                item.TANG_TRONG_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.TANG_TRONG_KY_NGUYEN_GIA);
                item.GIAM_TRONG_KY_SO_LUONG = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_SO_LUONG);
                item.GIAM_TRONG_KY_DIEN_TICH = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_DIEN_TICH);
                item.GIAM_TRONG_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_NGUYEN_GIA);
            }
            return result;
        }
        public List<TS_BCTH_02D_DK_TSNN> Group_TS_BCTH_02D_DK_TSNN(List<TS_BCTH_02D_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_02D_DK_TSNN> result = new List<TS_BCTH_02D_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_02D_DK_TSNN item = new TS_BCTH_02D_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.NGUYEN_GIA_NGAN_SACH = lstTaiSan.Sum(x => x.NGUYEN_GIA_NGAN_SACH);
                item.NGUYEN_GIA_KHAC = lstTaiSan.Sum(x => x.NGUYEN_GIA_KHAC);
                item.NGUYEN_GIA = item.NGUYEN_GIA_NGAN_SACH + item.NGUYEN_GIA_KHAC;
                item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
                item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
                result.Add(item);
            }
            return result;

        }
        public List<TS_BCTH_02E_DK_TSNN> Group_TS_BCTH_02E_DK_TSNN(List<TS_BCTH_02E_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_02E_DK_TSNN> result = new List<TS_BCTH_02E_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_02E_DK_TSNN item = new TS_BCTH_02E_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.NGUYEN_GIA_NGAN_SACH = lstTaiSan.Sum(x => x.NGUYEN_GIA_NGAN_SACH);
                item.NGUYEN_GIA_KHAC = lstTaiSan.Sum(x => x.NGUYEN_GIA_KHAC);
                item.NGUYEN_GIA = item.NGUYEN_GIA_NGAN_SACH + item.NGUYEN_GIA_KHAC;
                item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
                item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
                result.Add(item);
            }
            return result;

        }
        public List<TS_BCTH_02F_DK_TSNN> Group_TS_BCTH_02F_DK_TSNN(List<TS_BCTH_02F_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_02F_DK_TSNN> result = new List<TS_BCTH_02F_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_02F_DK_TSNN item = new TS_BCTH_02F_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.NGUYEN_GIA = lstTaiSan.Sum(x => x.NGUYEN_GIA);
                item.KH_HM_TRONG_NAM = lstTaiSan.Sum(x => x.KH_HM_TRONG_NAM);
                item.LUY_KE = lstTaiSan.Sum(x => x.LUY_KE);
                result.Add(item);
            }
            return result;
        }
        //public List<TS_BCTH_02G_DK_TNSS> Group_TS_BCTH_02G_DK_TNSS(List<TS_BCTH_02G_DK_TNSS> models)
        //{
        //    if (models == null || (models != null && models.Count() == 0))
        //        return null;
        //    List<TS_BCTH_02G_DK_TNSS> result = new List<TS_BCTH_02G_DK_TNSS>();
        //    var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
        //    foreach (var loaiTaiSan in loaiTaiSans)
        //    {
        //        TS_BCTH_02G_DK_TNSS item = new TS_BCTH_02G_DK_TNSS();
        //        item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
        //        var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
        //        item.SO_LUONG = lstTaiSan.Count();
        //        item.NGUYEN_GIA = lstTaiSan.Sum(x => x.NGUYEN_GIA);
        //        item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
        //        result.Add(item);
        //    }
        //    return result;
        //}
        //public List<TS_BCTH_02H_DK_TSNN> Group_TS_BCTH_02H_DK_TSNN(List<TS_BCTH_02H_DK_TSNN> models)
        //{
        //    if (models == null || (models != null && models.Count() == 0))
        //        return null;
        //    List<TS_BCTH_02H_DK_TSNN> result = new List<TS_BCTH_02H_DK_TSNN>();
        //    var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
        //    foreach (var loaiTaiSan in loaiTaiSans)
        //    {
        //        TS_BCTH_02H_DK_TSNN item = new TS_BCTH_02H_DK_TSNN();
        //        item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
        //        var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
        //        item.SO_LUONG = lstTaiSan.Count();
        //        item.NGUYEN_GIA = lstTaiSan.Sum(x => x.NGUYEN_GIA);
        //        item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
        //        result.Add(item);
        //    }
        //    return result;
        //}
        public List<TS_BCTH_08A_DK_TSC> Group_TS_BCTH_08A_DK_TSC(List<TS_BCTH_08A_DK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_08A_DK_TSC> result = new List<TS_BCTH_08A_DK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_08A_DK_TSC item = new TS_BCTH_08A_DK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
                item.QUAN_LY_NHA_NUOC = lstTaiSan.Sum(x => x.QUAN_LY_NHA_NUOC);
                item.KHONG_KINH_DOANH = lstTaiSan.Sum(x => x.KHONG_KINH_DOANH);
                item.KINH_DOANH = lstTaiSan.Sum(x => x.KINH_DOANH);
                item.CHO_THUE = lstTaiSan.Sum(x => x.CHO_THUE);
                item.LIEN_DOANH = lstTaiSan.Sum(x => x.LIEN_DOANH);
                item.SU_DUNG_HON_HOP = lstTaiSan.Sum(x => x.SU_DUNG_HON_HOP);
                item.KHAC = lstTaiSan.Sum(x => x.KHAC);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCTH_08B_DK_TSC> Group_TS_BCTH_08B_DK_TSC(List<TS_BCTH_08B_DK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTH_08B_DK_TSC> result = new List<TS_BCTH_08B_DK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTH_08B_DK_TSC item = new TS_BCTH_08B_DK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.DAU_KY_SO_LUONG = lstTaiSan.Sum(x => x.DAU_KY_SO_LUONG);
                item.DAU_KY_DIEN_TICH = lstTaiSan.Sum(x => x.DAU_KY_DIEN_TICH);
                item.DAU_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.DAU_KY_NGUYEN_GIA);
                item.TANG_TRONG_KY_SO_LUONG = lstTaiSan.Sum(x => x.TANG_TRONG_KY_SO_LUONG);
                item.TANG_TRONG_KY_DIEN_TICH = lstTaiSan.Sum(x => x.TANG_TRONG_KY_DIEN_TICH);
                item.TANG_TRONG_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.TANG_TRONG_KY_NGUYEN_GIA);
                item.GIAM_TRONG_KY_SO_LUONG = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_SO_LUONG);
                item.GIAM_TRONG_KY_DIEN_TICH = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_DIEN_TICH);
                item.GIAM_TRONG_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.GIAM_TRONG_KY_NGUYEN_GIA);
                item.CUOI_KY_SO_LUONG = lstTaiSan.Sum(x => x.CUOI_KY_SO_LUONG);
                item.CUOI_KY_DIEN_TICH = lstTaiSan.Sum(x => x.CUOI_KY_DIEN_TICH);
                item.CUOI_KY_NGUYEN_GIA = lstTaiSan.Sum(x => x.CUOI_KY_NGUYEN_GIA);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCTC_03_DK_TSNN> Group_TS_BCTC_03_DK_TSNN(List<TS_BCTC_03_DK_TSNN> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCTC_03_DK_TSNN> result = new List<TS_BCTC_03_DK_TSNN>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCTC_03_DK_TSNN item = new TS_BCTC_03_DK_TSNN();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.DIEN_TICH = lstTaiSan.Sum(x => x.DIEN_TICH);
                item.NGUYEN_GIA_NGAN_SACH = lstTaiSan.Sum(x => x.NGUYEN_GIA_NGAN_SACH);
                item.NGUYEN_GIA_KHAC = lstTaiSan.Sum(x => x.NGUYEN_GIA_KHAC);
                item.NGUYEN_GIA = item.NGUYEN_GIA_NGAN_SACH + item.NGUYEN_GIA_KHAC;
                item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_09A_CK_TSC> Group_TS_BCCK_09A_CK_TSC(List<TS_BCCK_09A_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_09A_CK_TSC> result = new List<TS_BCCK_09A_CK_TSC>();
            //var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            //foreach (var loaiTaiSan in loaiTaiSans)
            //{
            //    TS_BCCK_09A_CK_TSC item = new TS_BCCK_09A_CK_TSC();
            //    item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
            //    var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
            //    item.SO_LUONG = lstTaiSan.Count();
            //    item.GIA_MUA_THUE = lstTaiSan.Sum(x => x.GIA_MUA_THUE);
            //    result.Add(item);
            //}

            var lydotanggiams = models.Select(x => x.LY_DO_TANG_DB_ID).Distinct().ToList();
            foreach (var lydotanggiam in lydotanggiams)
            {
                TS_BCCK_09A_CK_TSC item = new TS_BCCK_09A_CK_TSC();
                item.LY_DO_TANG_DB_ID = lydotanggiam;
                var lstTaiSan = models.Where(x => x.LY_DO_TANG_DB_ID == lydotanggiam);
                item.SO_LUONG = lstTaiSan.Count();
                item.GIA_MUA_THUE = lstTaiSan.Sum(x => x.GIA_MUA_THUE);
                result.Add(item);
            }
            return result;
        }
        //public List<TS_BCCK_09B_CK_TSC> Group_TS_BCCK_09B_CK_TSC(List<TS_BCCK_09B_CK_TSC> models)
        //{
        //    if (models == null || (models != null && models.Count() == 0))
        //        return null;
        //    List<TS_BCCK_09B_CK_TSC> result = new List<TS_BCCK_09B_CK_TSC>();
        //    List<string> loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_TEN).Distinct().ToList();
        //    foreach (var loaiTaiSan in loaiTaiSans)
        //    {
        //        TS_BCCK_09B_CK_TSC item = new TS_BCCK_09B_CK_TSC();
        //        item.LOAI_TAI_SAN_TEN = loaiTaiSan;
        //        var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_TEN == loaiTaiSan);
        //        item = lstTaiSan.Count();
        //        item.HTSD_KINH_DOANH = lstTaiSan.Sum(x => x.HTSD_KINH_DOANH);
        //        item.HTSD_KHONG_KINH_DOANH = lstTaiSan.Sum(x => x.HTSD_KHONG_KINH_DOANH);
        //        item.HTSD_LD_LK = lstTaiSan.Sum(x => x.HTSD_LD_LK);
        //        item.HTSD_CHO_THUE = lstTaiSan.Sum(x => x.HTSD_CHO_THUE);
        //        item.HTSD_QLNN = lstTaiSan.Sum(x => x.HTSD_QLNN);
        //        item.HTSD_SU_DUNG_HON_HOP = lstTaiSan.Sum(x => x.HTSD_SU_DUNG_HON_HOP);
        //        item.HTSD_KHAC = lstTaiSan.Sum(x => x.HTSD_KHAC);
        //        result.Add(item);
        //    }
        //    return result;
        //}
        public List<TS_BCCK_09C_CK_TSC> Group_TS_BCCK_09C_CK_TSC(List<TS_BCCK_09C_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_09C_CK_TSC> result = new List<TS_BCCK_09C_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_09C_CK_TSC item = new TS_BCCK_09C_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.NGUON_NGAN_SACH = lstTaiSan.Sum(x => x.NGUON_NGAN_SACH);
                item.NGUON_KHAC = lstTaiSan.Sum(x => x.NGUON_KHAC);
                item.NGUYEN_GIA = item.NGUON_NGAN_SACH + item.NGUON_KHAC;
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_09D_CK_TSC> Group_TS_BCCK_09D_CK_TSC(List<TS_BCCK_09D_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_09D_CK_TSC> result = new List<TS_BCCK_09D_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_09D_CK_TSC item = new TS_BCCK_09D_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.NGUON_NGAN_SACH = lstTaiSan.Sum(x => x.NGUON_NGAN_SACH);
                item.NGUON_KHAC = lstTaiSan.Sum(x => x.NGUON_KHAC);
                item.GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.GIA_TRI_CON_LAI);
                item.PHI_NOP_NGAN_SACH = lstTaiSan.Sum(x => x.PHI_NOP_NGAN_SACH);
                item.PHI_THU = lstTaiSan.Sum(x => x.PHI_THU);
                item.PHI_BU_DAP = lstTaiSan.Sum(x => x.PHI_BU_DAP);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_09DD_CK_TSC> Group_TS_BCCK_09DD_CK_TSC(List<TS_BCCK_09DD_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_09DD_CK_TSC> result = new List<TS_BCCK_09DD_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_09DD_CK_TSC item = new TS_BCCK_09DD_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.KD_SO_LUONG_DIEN_TICH = lstTaiSan.Sum(x => x.KD_SO_LUONG_DIEN_TICH);
                item.KD_SO_TIEN_THU_TRONG_NAM = lstTaiSan.Sum(x => x.KD_SO_TIEN_THU_TRONG_NAM);
                item.CT_SO_LUONG_DIEN_TICH = lstTaiSan.Sum(x => x.CT_SO_LUONG_DIEN_TICH);
                item.CT_DON_GIA_THUE = lstTaiSan.Sum(x => x.CT_DON_GIA_THUE);
                item.CT_SO_TIEN_THU_TRONG_NAM = lstTaiSan.Sum(x => x.CT_SO_TIEN_THU_TRONG_NAM);
                item.LDLK_SO_LUONG_DIEN_TICH = lstTaiSan.Sum(x => x.LDLK_SO_LUONG_DIEN_TICH);
                item.LDLK_SO_TIEN_THU_TRONG_NAM = lstTaiSan.Sum(x => x.LDLK_SO_TIEN_THU_TRONG_NAM);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_10A_CK_TSC> Group_TS_BCCK_10A_CK_TSC(List<TS_BCCK_10A_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_10A_CK_TSC> result = new List<TS_BCCK_10A_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_10A_CK_TSC item = new TS_BCCK_10A_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.MUA_SAM_SO_LUONG = lstTaiSan.Sum(x => x.MUA_SAM_SO_LUONG);
                item.MUA_SAM_DIEN_TICH = lstTaiSan.Sum(x => x.MUA_SAM_DIEN_TICH);
                item.MUA_SAM_NGUYEN_GIA = lstTaiSan.Sum(x => x.MUA_SAM_NGUYEN_GIA);
                item.THUE_SO_LUONG = lstTaiSan.Sum(x => x.THUE_SO_LUONG);
                item.THUE_DIEN_TICH = lstTaiSan.Sum(x => x.THUE_DIEN_TICH);
                item.THUE_NGUYEN_GIA = lstTaiSan.Sum(x => x.THUE_NGUYEN_GIA);
                item.TIEP_NHAN_SO_LUONG = lstTaiSan.Sum(x => x.TIEP_NHAN_SO_LUONG);
                item.TIEP_NHAN_DIEN_TICH = lstTaiSan.Sum(x => x.TIEP_NHAN_DIEN_TICH);
                item.TIEP_NHAN_NGUYEN_GIA = lstTaiSan.Sum(x => x.TIEP_NHAN_NGUYEN_GIA);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_10B_CK_TSC> Group_TS_BCCK_10B_CK_TSC(List<TS_BCCK_10B_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_10B_CK_TSC> result = new List<TS_BCCK_10B_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_10B_CK_TSC item = new TS_BCCK_10B_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.HTSD_KINH_DOANH = lstTaiSan.Sum(x => x.HTSD_KINH_DOANH);
                item.HTSD_KHONG_KINH_DOANH = lstTaiSan.Sum(x => x.HTSD_KHONG_KINH_DOANH);
                item.HTSD_LD_LK = lstTaiSan.Sum(x => x.HTSD_LD_LK);
                item.HTSD_CHO_THUE = lstTaiSan.Sum(x => x.HTSD_CHO_THUE);
                item.HTSD_QLNN = lstTaiSan.Sum(x => x.HTSD_QLNN);
                item.HTSD_SU_DUNG_HON_HOP = lstTaiSan.Sum(x => x.HTSD_SU_DUNG_HON_HOP);
                item.HTSD_KHAC = lstTaiSan.Sum(x => x.HTSD_KHAC);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_10C_CK_TSC> Group_TS_BCCK_10C_CK_TSC(List<TS_BCCK_10C_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_10C_CK_TSC> result = new List<TS_BCCK_10C_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_10C_CK_TSC item = new TS_BCCK_10C_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.BAN_SO_LUONG = lstTaiSan.Sum(x => x.BAN_SO_LUONG);
                item.BAN_DIEN_TICH = lstTaiSan.Sum(x => x.BAN_DIEN_TICH);
                item.BAN_NGUYEN_GIA = lstTaiSan.Sum(x => x.BAN_NGUYEN_GIA);
                item.BAN_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.BAN_GIA_TRI_CON_LAI);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_SO_LUONG = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_SO_LUONG);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_DIEN_TICH = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_DIEN_TICH);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_NGUYEN_GIA = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_NGUYEN_GIA);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_GIA_TRI_CON_LAI);
                item.DIEU_CHUYEN_SO_LUONG = lstTaiSan.Sum(x => x.DIEU_CHUYEN_SO_LUONG);
                item.DIEU_CHUYEN_DIEN_TICH = lstTaiSan.Sum(x => x.DIEU_CHUYEN_DIEN_TICH);
                item.DIEU_CHUYEN_NGUYEN_GIA = lstTaiSan.Sum(x => x.DIEU_CHUYEN_NGUYEN_GIA);
                item.DIEU_CHUYEN_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.DIEU_CHUYEN_GIA_TRI_CON_LAI);
                item.MAT_HUY_HOAI_SO_LUONG = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_SO_LUONG);
                item.MAT_HUY_HOAI_DIEN_TICH = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_DIEN_TICH);
                item.MAT_HUY_HOAI_NGUYEN_GIA = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_NGUYEN_GIA);
                item.MAT_HUY_HOAI_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_GIA_TRI_CON_LAI);
                item.THANH_LY_SO_LUONG = lstTaiSan.Sum(x => x.THANH_LY_SO_LUONG);
                item.THANH_LY_DIEN_TICH = lstTaiSan.Sum(x => x.THANH_LY_DIEN_TICH);
                item.THANH_LY_NGUYEN_GIA = lstTaiSan.Sum(x => x.THANH_LY_NGUYEN_GIA);
                item.THANH_LY_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.THANH_LY_GIA_TRI_CON_LAI);
                item.THU_HOI_SO_LUONG = lstTaiSan.Sum(x => x.THU_HOI_SO_LUONG);
                item.THU_HOI_DIEN_TICH = lstTaiSan.Sum(x => x.THU_HOI_DIEN_TICH);
                item.THU_HOI_NGUYEN_GIA = lstTaiSan.Sum(x => x.THU_HOI_NGUYEN_GIA);
                item.THU_HOI_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.THU_HOI_GIA_TRI_CON_LAI);
                item.TIEU_HUY_SO_LUONG = lstTaiSan.Sum(x => x.TIEU_HUY_SO_LUONG);
                item.TIEU_HUY_DIEN_TICH = lstTaiSan.Sum(x => x.TIEU_HUY_DIEN_TICH);
                item.TIEU_HUY_NGUYEN_GIA = lstTaiSan.Sum(x => x.TIEU_HUY_NGUYEN_GIA);
                item.TIEU_HUY_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.TIEU_HUY_GIA_TRI_CON_LAI);
                item.KHAC_SO_LUONG = lstTaiSan.Sum(x => x.KHAC_SO_LUONG);
                item.KHAC_DIEN_TICH = lstTaiSan.Sum(x => x.KHAC_DIEN_TICH);
                item.KHAC_NGUYEN_GIA = lstTaiSan.Sum(x => x.KHAC_NGUYEN_GIA);
                item.KHAC_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.KHAC_GIA_TRI_CON_LAI);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_10D_CK_TSC> Group_TS_BCCK_10D_CK_TSC(List<TS_BCCK_10D_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_10D_CK_TSC> result = new List<TS_BCCK_10D_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_10D_CK_TSC item = new TS_BCCK_10D_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.KD_DIEN_TICH = lstTaiSan.Sum(x => x.KD_DIEN_TICH);
                item.KD_SO_LUONG = lstTaiSan.Sum(x => x.KD_SO_LUONG);
                item.KD_NGUYEN_GIA = lstTaiSan.Sum(x => x.KD_NGUYEN_GIA);
                item.KD_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.KD_GIA_TRI_CON_LAI);
                item.KD_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.KD_TIEN_THU_DUOC);
                item.LDLK_DIEN_TICH = lstTaiSan.Sum(x => x.LDLK_DIEN_TICH);
                item.LDLK_SO_LUONG = lstTaiSan.Sum(x => x.LDLK_SO_LUONG);
                item.LDLK_NGUYEN_GIA = lstTaiSan.Sum(x => x.LDLK_NGUYEN_GIA);
                item.LDLK_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.LDLK_GIA_TRI_CON_LAI);
                item.LDLK_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.LDLK_TIEN_THU_DUOC);
                item.CT_DIEN_TICH = lstTaiSan.Sum(x => x.CT_DIEN_TICH);
                item.CT_SO_LUONG = lstTaiSan.Sum(x => x.CT_SO_LUONG);
                item.CT_NGUYEN_GIA = lstTaiSan.Sum(x => x.CT_NGUYEN_GIA);
                item.CT_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.CT_GIA_TRI_CON_LAI);
                item.CT_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.CT_TIEN_THU_DUOC);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_11A_CK_TSC> Group_TS_BCCK_11A_CK_TSC(List<TS_BCCK_11A_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_11A_CK_TSC> result = new List<TS_BCCK_11A_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_11A_CK_TSC item = new TS_BCCK_11A_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.MUA_SAM_SO_LUONG = lstTaiSan.Sum(x => x.MUA_SAM_SO_LUONG);
                item.MUA_SAM_DIEN_TICH = lstTaiSan.Sum(x => x.MUA_SAM_DIEN_TICH);
                item.MUA_SAM_NGUYEN_GIA = lstTaiSan.Sum(x => x.MUA_SAM_NGUYEN_GIA);
                item.CHO_THUE_DIEN_TICH = lstTaiSan.Sum(x => x.CHO_THUE_DIEN_TICH);
                item.CHO_THUE_SO_LUONG = lstTaiSan.Sum(x => x.CHO_THUE_SO_LUONG);
                item.CHO_THUE_NGUYEN_GIA = lstTaiSan.Sum(x => x.CHO_THUE_NGUYEN_GIA);
                item.TIEP_NHAN_SO_LUONG = lstTaiSan.Sum(x => x.TIEP_NHAN_SO_LUONG);
                item.TIEP_NHAN_DIEN_TICH = lstTaiSan.Sum(x => x.TIEP_NHAN_DIEN_TICH);
                item.TIEP_NHAN_NGUYEN_GIA = lstTaiSan.Sum(x => x.TIEP_NHAN_NGUYEN_GIA);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_11B_CK_TSC> Group_TS_BCCK_11B_CK_TSC(List<TS_BCCK_11B_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_11B_CK_TSC> result = new List<TS_BCCK_11B_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_11B_CK_TSC item = new TS_BCCK_11B_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.SO_LUONG = lstTaiSan.Count();
                item.TSLV = lstTaiSan.Sum(x => x.TSLV);
                item.KD = lstTaiSan.Sum(x => x.KD);
                item.KHONG_KD = lstTaiSan.Sum(x => x.KHONG_KD);
                item.CHO_THUE = lstTaiSan.Sum(x => x.CHO_THUE);
                item.LDLK = lstTaiSan.Sum(x => x.LDLK);
                item.SDHH = lstTaiSan.Sum(x => x.SDHH);
                item.KHAC = lstTaiSan.Sum(x => x.KHAC);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_11C_CK_TSC> Group_TS_BCCK_11C_CK_TSC(List<TS_BCCK_11C_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_11C_CK_TSC> result = new List<TS_BCCK_11C_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_11C_CK_TSC item = new TS_BCCK_11C_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.BAN_SO_LUONG = lstTaiSan.Sum(x => x.BAN_SO_LUONG);
                item.BAN_DIEN_TICH = lstTaiSan.Sum(x => x.BAN_DIEN_TICH);
                item.BAN_NGUYEN_GIA = lstTaiSan.Sum(x => x.BAN_NGUYEN_GIA);
                item.BAN_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.BAN_GIA_TRI_CON_LAI);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_SO_LUONG = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_SO_LUONG);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_DIEN_TICH = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_DIEN_TICH);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_NGUYEN_GIA = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_NGUYEN_GIA);
                item.CHUYEN_GIAO_VE_DIA_PHUONG_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.CHUYEN_GIAO_VE_DIA_PHUONG_GIA_TRI_CON_LAI);
                item.DIEU_CHUYEN_SO_LUONG = lstTaiSan.Sum(x => x.DIEU_CHUYEN_SO_LUONG);
                item.DIEU_CHUYEN_DIEN_TICH = lstTaiSan.Sum(x => x.DIEU_CHUYEN_DIEN_TICH);
                item.DIEU_CHUYEN_NGUYEN_GIA = lstTaiSan.Sum(x => x.DIEU_CHUYEN_NGUYEN_GIA);
                item.DIEU_CHUYEN_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.DIEU_CHUYEN_GIA_TRI_CON_LAI);
                item.MAT_HUY_HOAI_SO_LUONG = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_SO_LUONG);
                item.MAT_HUY_HOAI_DIEN_TICH = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_DIEN_TICH);
                item.MAT_HUY_HOAI_NGUYEN_GIA = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_NGUYEN_GIA);
                item.MAT_HUY_HOAI_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.MAT_HUY_HOAI_GIA_TRI_CON_LAI);
                item.THANH_LY_SO_LUONG = lstTaiSan.Sum(x => x.THANH_LY_SO_LUONG);
                item.THANH_LY_DIEN_TICH = lstTaiSan.Sum(x => x.THANH_LY_DIEN_TICH);
                item.THANH_LY_NGUYEN_GIA = lstTaiSan.Sum(x => x.THANH_LY_NGUYEN_GIA);
                item.THANH_LY_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.THANH_LY_GIA_TRI_CON_LAI);
                item.THU_HOI_SO_LUONG = lstTaiSan.Sum(x => x.THU_HOI_SO_LUONG);
                item.THU_HOI_DIEN_TICH = lstTaiSan.Sum(x => x.THU_HOI_DIEN_TICH);
                item.THU_HOI_NGUYEN_GIA = lstTaiSan.Sum(x => x.THU_HOI_NGUYEN_GIA);
                item.THU_HOI_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.THU_HOI_GIA_TRI_CON_LAI);
                item.TIEU_HUY_SO_LUONG = lstTaiSan.Sum(x => x.TIEU_HUY_SO_LUONG);
                item.TIEU_HUY_DIEN_TICH = lstTaiSan.Sum(x => x.TIEU_HUY_DIEN_TICH);
                item.TIEU_HUY_NGUYEN_GIA = lstTaiSan.Sum(x => x.TIEU_HUY_NGUYEN_GIA);
                item.TIEU_HUY_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.TIEU_HUY_GIA_TRI_CON_LAI);
                item.KHAC_SO_LUONG = lstTaiSan.Sum(x => x.KHAC_SO_LUONG);
                item.KHAC_DIEN_TICH = lstTaiSan.Sum(x => x.KHAC_DIEN_TICH);
                item.KHAC_NGUYEN_GIA = lstTaiSan.Sum(x => x.KHAC_NGUYEN_GIA);
                item.KHAC_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.KHAC_GIA_TRI_CON_LAI);
                result.Add(item);
            }
            return result;
        }
        public List<TS_BCCK_11D_CK_TSC> Group_TS_BCCK_11D_CK_TSC(List<TS_BCCK_11D_CK_TSC> models)
        {
            if (models == null || (models != null && models.Count() == 0))
                return null;
            List<TS_BCCK_11D_CK_TSC> result = new List<TS_BCCK_11D_CK_TSC>();
            var loaiTaiSans = models.Select(x => x.LOAI_TAI_SAN_DB_ID).Distinct().ToList();
            foreach (var loaiTaiSan in loaiTaiSans)
            {
                TS_BCCK_11D_CK_TSC item = new TS_BCCK_11D_CK_TSC();
                item.LOAI_TAI_SAN_DB_ID = loaiTaiSan;
                var lstTaiSan = models.Where(x => x.LOAI_TAI_SAN_DB_ID == loaiTaiSan);
                item.KD_DIEN_TICH = lstTaiSan.Sum(x => x.KD_DIEN_TICH);
                item.KD_SO_LUONG = lstTaiSan.Sum(x => x.KD_SO_LUONG);
                item.KD_NGUYEN_GIA = lstTaiSan.Sum(x => x.KD_NGUYEN_GIA);
                item.KD_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.KD_GIA_TRI_CON_LAI);
                item.KD_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.KD_TIEN_THU_DUOC);
                item.LDLK_DIEN_TICH = lstTaiSan.Sum(x => x.LDLK_DIEN_TICH);
                item.LDLK_SO_LUONG = lstTaiSan.Sum(x => x.LDLK_SO_LUONG);
                item.LDLK_NGUYEN_GIA = lstTaiSan.Sum(x => x.LDLK_NGUYEN_GIA);
                item.LDLK_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.LDLK_GIA_TRI_CON_LAI);
                item.LDLK_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.LDLK_TIEN_THU_DUOC);
                item.CT_DIEN_TICH = lstTaiSan.Sum(x => x.CT_DIEN_TICH);
                item.CT_SO_LUONG = lstTaiSan.Sum(x => x.CT_SO_LUONG);
                item.CT_NGUYEN_GIA = lstTaiSan.Sum(x => x.CT_NGUYEN_GIA);
                item.CT_GIA_TRI_CON_LAI = lstTaiSan.Sum(x => x.CT_GIA_TRI_CON_LAI);
                item.CT_TIEN_THU_DUOC = lstTaiSan.Sum(x => x.CT_TIEN_THU_DUOC);
                result.Add(item);
            }
            return result;
        }
        #endregion
        #region Đôi chiếu
        #endregion
    }
}
