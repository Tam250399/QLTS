//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using DevExpress.XtraRichEdit.Fields;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DMDC;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.DanhMuc
{
    public class DonViModelFactory : IDonViModelFactory
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDonViService _itemService;
        private readonly INguoiDungDonViService _nguoidungDonViService;
        private readonly ILoaiDonViModelFactory _itemLoaiDonViModelFactory;
        private readonly IDiaBanService _itemDiaBanService;
        private readonly INguoiDungService _itemNguoiDungService;
        private readonly IDiaBanModelFactory _diaBanModelFactory;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;
        private readonly ICheDoHaoMonService _cheDoHaoMonService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly IRepository<DonVi> _donViRepository;
        private readonly ITaiSanService _taiSanService;
        private readonly IHienTrangService _hienTrangService;

        #endregion Fields

        #region Ctor

        public DonViModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDonViService itemService,
            INguoiDungDonViService nguoidungDonViService,
            ILoaiDonViModelFactory itemLoaiDonViModelFactory,
            IDiaBanService itemDiaBanService,
            INguoiDungService itemNguoiDungService,
            IDiaBanModelFactory diaBanModelFactory,
            ILoaiDonViService loaiDonViService,
            INhanHienThiService nhanHienThiService,
            IDonViService donViService,
            ICheDoHaoMonService cheDoHaoMonService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IRepository<DonVi> DonViRepository,
            ITaiSanService taiSanService,
            IHienTrangService hienTrangService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nguoidungDonViService = nguoidungDonViService;
            this._itemLoaiDonViModelFactory = itemLoaiDonViModelFactory;
            this._itemDiaBanService = itemDiaBanService;
            this._itemNguoiDungService = itemNguoiDungService;
            this._diaBanModelFactory = diaBanModelFactory;
            this._loaiDonViService = loaiDonViService;
            _nhanHienThiService = nhanHienThiService;
            this._donViService = donViService;
            this._cheDoHaoMonService = cheDoHaoMonService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            _donViRepository = DonViRepository;
            this._taiSanService = taiSanService;
            this._hienTrangService = hienTrangService;
        }

        #endregion Ctor

        #region DonVi

        public DonViSearchModel PrepareDonViSearchModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (_workContext.CurrentDonVi.ID != (decimal)enumDonVi.DPAC)
                searchModel.donViId = _workContext.CurrentDonVi.ID;
            //more
            searchModel.dllLoaiDonVi = _itemLoaiDonViModelFactory.PrepareSelectListLoaiDonVi(isAddFirst: true);
            var curentDonvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            //bỏ cục,chi cục, tổng cục
            int[] valuesToExcute = new int[] { (int)CapEnum.TongCuc, (int)CapEnum.ChiCuc, (int)CapEnum.Cuc };

            // vì BoCoQuanTrungUong enum =0 nên phải đặt giá trị khác để kendomultiselect không mặc định 0 là tất cả
            foreach (CapEnum _enum in (CapEnum[])Enum.GetValues(typeof(CapEnum)))
            {
                if (!valuesToExcute.Contains((int)_enum))
                {
                    if (_enum != CapEnum.BoCoQuanTrungUong)
                    {
                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = ((int)_enum).ToString(), Selected = false });
                    }
                    else
                    {

                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = "99", Selected = true });
                    }

                }
            }
            // selected cấp đơn vị hiện tại
            int capDonvi = (int)(curentDonvi.CAP_DON_VI_ID == 0 ? 99 : curentDonvi.CAP_DON_VI_ID);
            searchModel.SelectedCapDonVis = new List<int>() { capDonvi };
            //searchModel.dllCapDonVi.Items.
            searchModel.dllTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(CapDiaBan: (int)enumLOAI_DIABAN.TINH);
            searchModel.dllTinh.AddFirstRow("Chọn tỉnh", "0");
            searchModel.dllboNganh = PrepareDonViAvailabele();
            searchModel.dllboNganh.AddFirstRow("Chọn bộ ngành", "0");
            searchModel.DLLCapDiaPhuong = EnumCapDonViDiaPhuong.Tinh.ToSelectList().ToList();
            searchModel.DLLCapDiaPhuong.AddFirstRow("Chọn cấp địa phương", "-1");
            searchModel.isTinh = false;
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public DonViSearchModel PrepareDonViChuaNhapTaiSanSearchModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var curentDonvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            int capDonvi = (int)(curentDonvi.CAP_DON_VI_ID == 0 ? 99 : curentDonvi.CAP_DON_VI_ID);
            int[] valuesToExcute = new int[] { (int)CapEnum.TongCuc, (int)CapEnum.ChiCuc, (int)CapEnum.Cuc };

            // vì BoCoQuanTrungUong enum =0 nên phải đặt giá trị khác để kendomultiselect không mặc định 0 là tất cả
            foreach (CapEnum _enum in (CapEnum[])Enum.GetValues(typeof(CapEnum)))
            {
                if (!valuesToExcute.Contains((int)_enum))
                {
                    if (_enum != CapEnum.BoCoQuanTrungUong)
                    {
                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = ((int)_enum).ToString(), Selected = false });
                    }
                    else
                    {

                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = "99", Selected = true });
                    }

                }
            }
            searchModel.SelectedCapDonVis = new List<int>() { capDonvi };
            decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, cheDoId: CheDoId, isDisabled: false).ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public DonViListModel PrepareDonViChuaNhapTaiSanListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }

            var items = _taiSanService.SearchAllDonViChuaNhapTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ParentID: searchModel.ParentID, MaBo: searchModel.MaBo, CapDonViSearch: searchModel.CapDonViSearch, listCapDonVis: searchModel.SelectedCapDonVis, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, donViId: searchModel.donViId);

            //prepare list model
            //var model = new DonViListModel
            //{
            //    //fill in model values from the entity
            //    Data = items.Select(c => c.ToModel<DonViModel>()),

            //    Total = items.TotalCount
            //};
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViModel>();
                    m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }
        public DonViSearchModel PrepareKiemTraTaiSanSearchModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.dllLoaiDonVi = _itemLoaiDonViModelFactory.PrepareSelectListLoaiDonVi(isAddFirst: true);
            searchModel.DDLCompareSign = new enumCompare().ToSelectList().ToList();
            var curentDonvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            int capDonvi = (int)(curentDonvi.CAP_DON_VI_ID == 0 ? 99 : curentDonvi.CAP_DON_VI_ID);
            int[] valuesToExcute = new int[] { (int)CapEnum.TongCuc, (int)CapEnum.ChiCuc, (int)CapEnum.Cuc };

            // vì BoCoQuanTrungUong enum =0 nên phải đặt giá trị khác để kendomultiselect không mặc định 0 là tất cả
            foreach (CapEnum _enum in (CapEnum[])Enum.GetValues(typeof(CapEnum)))
            {
                if (!valuesToExcute.Contains((int)_enum))
                {
                    if (_enum != CapEnum.BoCoQuanTrungUong)
                    {
                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = ((int)_enum).ToString(), Selected = false });
                    }
                    else
                    {

                        searchModel.dllCapDonVi.Add(new SelectListItem { Text = _nhanHienThiService.GetGiaTriEnum(_enum), Value = "99", Selected = true });
                    }

                }
            }
            searchModel.SelectedCapDonVis = new List<int>() { capDonvi };
            decimal CheDoId = _cheDoHaoMonService.GetCheDoHaoMonByNgayNhap(DateTime.Now).ID;
            var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (int)enumLOAI_HINH_TAI_SAN.DAC_THU, (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
              (int)enumLOAI_HINH_TAI_SAN.VO_HINH, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO}).Select(c => c.Value.ToNumberInt32())).ToArray();
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListTaiSanDatNha(isAddFirst: true, cheDoId: CheDoId, isDisabled: false, listLoaiHinhTaiSanId: listEx).ToList();
            searchModel.DDLMucDichSuDung = new enumMucDichSuDung().ToSelectList().ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public DonViListModel PrepareKiemTraTaiSanListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }

            var items = _taiSanService.SearchAllDonViKiemTraTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ParentID: searchModel.ParentID, MaBo: searchModel.MaBo, CapDonViSearch: searchModel.CapDonViSearch, listCapDonVis: searchModel.SelectedCapDonVis, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, donViId: searchModel.donViId, LoaiDonViSearch: searchModel.LoaiDonViSearch, MucDichSuDungSearch: searchModel.MucDichSuDungSearch, DienTich_CompareSign: searchModel.DienTich_CompareSign, DienTich_Value1: searchModel.DienTich_Value1, DienTich_Value2: searchModel.DienTich_Value2);

            //prepare list model
            //var model = new DonViListModel
            //{
            //    //fill in model values from the entity
            //    Data = items.Select(c => c.ToModel<DonViModel>()),

            //    Total = items.TotalCount
            //};
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViModel>();
                    m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }
        public DonViSearchModel PrepareDonViSearchModel2(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.donViId = _workContext.CurrentDonVi.ID;
            //more
            searchModel.isTinh = false;
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public IList<SelectListItem> PrepareDonViAvailabele(string tenDonVi = null)
        {
            var tree = "";
            var selectlist = _itemService.SearchDonViDieuChuyens(Keysearch: tenDonVi, isTinh: false, ParentID: null).Take(10).OrderBy(c => c.MA).Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
            }).ToList();
            return selectlist;
        }

        public DonViListModel PrepareDonViListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }
            var items = _itemService.SearchDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
                TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: _workContext.CurrentCustomer.IS_QUAN_TRI, ischondonvi: searchModel.ischondonvi ?? false,
                CapDonViSearch: searchModel.CapDonViSearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu, donViId: searchModel.donViId, listCapDonVis: searchModel.SelectedCapDonVis);
            LoaiDonVi ldvBanQLDA = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN);
            //prepare list model
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViModel>();
                    m.isSelected = _nguoidungDonViService.KiemTraDaChon(NguoiDungId: searchModel.nguoiDungId, DonViId: m.ID);
                    m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;

                    if (ldvBanQLDA == null || (ldvBanQLDA != null && c.LOAI_DON_VI_ID != ldvBanQLDA.ID))
                    {
                        m.SO_DON_VI_CON = _itemService.GetSoDonViConByID(id: c.ID);
                    }
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }

        public List<DonViExport> PrepareDonViExport(DonViSearchModel searchModel)
        {
            List<DonViExport> rs = new List<DonViExport>();
            //var items = _itemService.SearchDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
            //    TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: searchModel.IsQuangTri ?? false, ischondonvi: searchModel.ischondonvi ?? false,
            //    CapDonViSearch: searchModel.CapDonViSearch, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu, donViId: searchModel.donViId).OrderBy(c => c.TREE_NODE);
            var items = _itemService.SearchDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
                TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: _workContext.CurrentCustomer.IS_QUAN_TRI, ischondonvi: searchModel.ischondonvi ?? false,
                CapDonViSearch: searchModel.CapDonViSearch, pageIndex: searchModel.Page - 1, pageSize: int.MaxValue, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu, donViId: searchModel.donViId, xuatExcel: searchModel.xuatExcel);
            List<DonViExport> lstDonVi = new List<DonViExport>();
            //foreach (var item in items)
            //{
            //    var lst = _itemService.GetListDonViChild(item.ID);
            //    if (lst != null && lst.Count > 0)
            //    {
            //        lstDonVi = lstDonVi.Concat(lst.Select(c =>
            //        {
            //            var m = new DonViExport()
            //            {
            //                MA = c.MA,
            //                TEN = c.TEN,
            //                DIA_CHI = c.DIA_CHI,
            //                TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty
            //            };
            //            return m;
            //        }).ToList()).ToList();
            //    }
            //}
            rs = items.Select(c =>
            {
                var m = new DonViExport()
                {
                    MA = c.MA,
                    TEN = c.TEN,
                    DIA_CHI = c.DIA_CHI,
                    MA_CHA = c.DonViCha != null ? c.DonViCha.MA : String.Empty,
                    TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty,
                    LOAI_HINH_DON_VI = c.LoaiDonVi != null ? c.LoaiDonVi.TEN : string.Empty
                };
                return m;
            }).ToList();
            rs = rs.Concat(lstDonVi).OrderBy(c => c.MA).ToList();
            return rs;

        }
        public List<DonViExport> PrepareDonViChuaNhapTaiSanExport(DonViSearchModel searchModel)
        {
            List<DonViExport> rs = new List<DonViExport>();
            //var items = _itemService.SearchDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
            //    TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: searchModel.IsQuangTri ?? false, ischondonvi: searchModel.ischondonvi ?? false,
            //    CapDonViSearch: searchModel.CapDonViSearch, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu, donViId: searchModel.donViId).OrderBy(c => c.TREE_NODE);
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }
            var items = _taiSanService.SearchAllDonViChuaNhapTaiSan(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
                CapDonViSearch: searchModel.CapDonViSearch, pageIndex: searchModel.Page - 1, pageSize: int.MaxValue, MaBo: searchModel.MaBo, donViId: searchModel.donViId, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, listCapDonVis: searchModel.SelectedCapDonVis);
            List<DonViExport> lstDonVi = new List<DonViExport>();
            //foreach (var item in items)
            //{
            //    var lst = _itemService.GetListDonViChild(item.ID);
            //    if (lst != null && lst.Count > 0)
            //    {
            //        lstDonVi = lstDonVi.Concat(lst.Select(c =>
            //        {
            //            var m = new DonViExport()
            //            {
            //                MA = c.MA,
            //                TEN = c.TEN,
            //                DIA_CHI = c.DIA_CHI,
            //                TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty
            //            };
            //            return m;
            //        }).ToList()).ToList();
            //    }
            //}
            rs = items.Select(c =>
            {
                var m = new DonViExport()
                {
                    MA = c.MA,
                    TEN = c.TEN,
                    DIA_CHI = c.DIA_CHI,
                    TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty
                };
                return m;
            }).ToList();
            rs = rs.Concat(lstDonVi).OrderBy(c => c.MA).ToList();
            return rs;

        }
        public List<DonViExport> PrepareDonViKiemTraTaiSanExport(DonViSearchModel searchModel)
        {
            List<DonViExport> rs = new List<DonViExport>();
            //var items = _itemService.SearchDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
            //    TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: searchModel.IsQuangTri ?? false, ischondonvi: searchModel.ischondonvi ?? false,
            //    CapDonViSearch: searchModel.CapDonViSearch, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu, donViId: searchModel.donViId).OrderBy(c => c.TREE_NODE);
            if (searchModel.SelectedCapDonVis != null && searchModel.SelectedCapDonVis.Count() > 0 && searchModel.SelectedCapDonVis.Contains(99))
            {
                searchModel.SelectedCapDonVis.Remove(99);
                searchModel.SelectedCapDonVis.Add(0);
            }
            var items = _taiSanService.SearchAllDonViKiemTraTaiSan(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: int.MaxValue, ParentID: searchModel.ParentID, MaBo: searchModel.MaBo, CapDonViSearch: searchModel.CapDonViSearch, listCapDonVis: searchModel.SelectedCapDonVis, LoaiTaiSanId: searchModel.LOAI_TAI_SAN_ID, donViId: searchModel.donViId, LoaiDonViSearch: searchModel.LoaiDonViSearch, MucDichSuDungSearch: searchModel.MucDichSuDungSearch, DienTich_CompareSign: searchModel.DienTich_CompareSign, DienTich_Value1: searchModel.DienTich_Value1, DienTich_Value2: searchModel.DienTich_Value2);
            List<DonViExport> lstDonVi = new List<DonViExport>();
            //foreach (var item in items)
            //{
            //    var lst = _itemService.GetListDonViChild(item.ID);
            //    if (lst != null && lst.Count > 0)
            //    {
            //        lstDonVi = lstDonVi.Concat(lst.Select(c =>
            //        {
            //            var m = new DonViExport()
            //            {
            //                MA = c.MA,
            //                TEN = c.TEN,
            //                DIA_CHI = c.DIA_CHI,
            //                TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty
            //            };
            //            return m;
            //        }).ToList()).ToList();
            //    }
            //}
            rs = items.Select(c =>
            {
                var m = new DonViExport()
                {
                    MA = c.MA,
                    TEN = c.TEN,
                    DIA_CHI = c.DIA_CHI,
                    TEN_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty
                };
                return m;
            }).ToList();
            rs = rs.Concat(lstDonVi).OrderBy(c => c.MA).ToList();
            return rs;

        }
        public DonViListModel PrepareDonViDieuChuyenListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDonViDieuChuyens(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID,
                TreeLevel: searchModel.TreeLevel, LoaiDonViSearch: searchModel.LoaiDonViSearch, IsQuanTri: _workContext.CurrentCustomer.IS_QUAN_TRI, ischondonvi: searchModel.ischondonvi ?? false,
                CapDonViSearch: searchModel.CapDonViSearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isSelectList: searchModel.isSelectList ?? false, isNhapLieuOnly: searchModel.isOnlyNhapLieu,
                tinhId: searchModel.tinhId, isTinh: searchModel.isTinh, boNganhId: searchModel.boNganhId);

            //prepare list model
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViModel>();
                    m.isSelected = _nguoidungDonViService.KiemTraDaChon(NguoiDungId: searchModel.nguoiDungId, DonViId: m.ID);
                    m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;
                    m.SO_DON_VI_CON = _itemService.GetSoDonViConByID(id: c.ID);
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }

        public DonViModel PrepareDonViModel(DonViModel model, DonVi item, bool excludeProperties = false)
        {

            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DonViModel>();
                if (model.PARENT_ID != null && model.PARENT_ID != 0)
                {
                    model.TEN_DON_VI_CHA = _itemService.GetDonViById(model.PARENT_ID ?? 0).TEN;
                    model.MA_DON_VI_CHA = _itemService.GetDonViById(model.PARENT_ID ?? 0).MA;
                    model.MA = String.Concat(model.MA.TakeLast(3));
                }

                if (model.DIA_BAN_ID != null && model.DIA_BAN_ID != 0)
                    model.TEN_DIA_BAN = _itemDiaBanService.GetDiaBanById(model.DIA_BAN_ID.GetValueOrDefault()).TEN;

            }
            else
            {
                model.PARENT_ID = _workContext.CurrentDonVi.ID;
                model.TEN_DON_VI_CHA = _donViService.GetDonViById(_workContext.CurrentDonVi.ID).TEN;
            }

            //more
            model.ddlLoaiCapDonVi = (new EnumLoaiCapDonVi()).ToSelectList();
            if (model.LOAI_CAP_DON_VI_ID == (int)EnumLoaiCapDonVi.CapDiaPhuong)
                model.dllCapDonVi = ((EnumCapDonViDiaPhuong)(model.CAP_DON_VI_ID ?? 0)).ToSelectList();
            else
                model.dllCapDonVi = ((EnumCapDonViTrungUong)(model.CAP_DON_VI_ID ?? 0)).ToSelectList();
            //model.dllCapDonVi = model.CapEnum.ToSelectList();
            model.dllLoaiDonVi = _itemLoaiDonViModelFactory.PrepareSelectListLoaiDonVi(isAddFirst: true, ValSelected: model.LOAI_DON_VI_ID);
            model.dllQLTSCD = model.CHE_DO_HACH_TOANEnum.ToSelectList().ToList();
            model.dllQLTSCD.AddFirstRow("-------Chọn chế độ QLTSCD------");

            //Địa bàn tỉnh huyện xã
            model.dllDiaBanTinh = _diaBanModelFactory.PrepareDiaBanAvailabele(CapDiaBan: (int)enumLOAI_DIABAN.TINH, IsListChaCon: false, IsAddFirst: true, strFirstRow: "-- Chọn tỉnh/thành phố --", Valselected: model.TINH_ID != null ? model.TINH_ID : 0);
            if (model.TINH_ID != null && model.TINH_ID != 0)
                model.dllDiaBanHuyen = _diaBanModelFactory.PrepareDiaBanAvailabele(CapDiaBan: (int)enumLOAI_DIABAN.HUYEN, IsListChaCon: false, IsAddFirst: true, strFirstRow: "-- Chọn quận/huyện --", ParentId: model.TINH_ID, Valselected: model.TINH_ID);
            if (model.HUYEN_ID != null && model.HUYEN_ID != 0)
                model.dllDiaBanXa = _diaBanModelFactory.PrepareDiaBanAvailabele(CapDiaBan: (int)enumLOAI_DIABAN.XA, IsListChaCon: false, IsAddFirst: true, strFirstRow: "-- Chọn xã/phường --", ParentId: model.HUYEN_ID, Valselected: model.HUYEN_ID);

            //get List người dùng trong đơn vị
            if (model.ID > 0)
            {
                var lstNguoiDung = _itemNguoiDungService.GetAllNguoiDungByDonViId(model.ID);
                foreach (var nguoiDung in lstNguoiDung)
                {
                    model.LstNguoiDungModel.Add(nguoiDung.ToModel<NguoiDungModel>());
                }
            }
            // lấy id đơn vị hiện tại
            //lấy số đơn vị con
            model.SO_DON_VI_CON = _itemService.GetSoDonViConByID(id: model.ID);

            return model;
        }



        public bool CheckMaDonVi(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaDonVi(id: id, ma: ma);
        }

        public void PrepareDonVi(DonViModel model, DonVi item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.MA_BO = model.MA_BO;
            item.MA_DIA_BAN = model.MA_DIA_BAN;
            item.MA_DVQHNS = model.MA_DVQHNS;
            item.DIA_CHI = model.DIA_CHI;
            item.DIEN_THOAI = model.DIEN_THOAI;
            item.FAX = model.FAX;
            item.MA_TINH = model.MA_TINH;
            item.NHOM_DON_VI_ID = model.NHOM_DON_VI_ID;
            item.CAP_DON_VI_ID = model.CAP_DON_VI_ID;
            item.MA_HUYEN = model.MA_HUYEN;
            item.CQTC_MA = model.CQTC_MA;
            item.CHE_DO_HACH_TOAN_ID = model.CHE_DO_HACH_TOAN_ID;
            item.SO_QUYET_DINH = model.SO_QUYET_DINH;
            item.NGAY_QUYET_DINH = model.NGAY_QUYET_DINH;
            item.SO_QUYET_DINH_GIAO_VON = model.SO_QUYET_DINH_GIAO_VON;
            item.NGAY_QUYET_DINH_GIAO_VON = model.NGAY_QUYET_DINH_GIAO_VON;
            item.PARENT_ID = model.PARENT_ID;
            item.LOAI_DON_VI_ID = model.LOAI_DON_VI_ID;
            item.TREE_LEVEL = model.TREE_LEVEL;
            item.TREE_NODE = model.TREE_NODE;
            item.DIA_BAN_ID = model.DIA_BAN_ID;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.DON_VI_HIEN_THI = model.DON_VI_HIEN_THI;
            item.TRANG_THAI_DONG_BO_ID = model.TRANG_THAI_DONG_BO_ID;
            item.LA_DON_VI_NHAP_LIEU = model.LA_DON_VI_NHAP_LIEU;
            item.TRANG_THAI_THAY_DOI_ID = model.TRANG_THAI_THAY_DOI_ID;
            item.CO_TAI_SAN = model.CO_TAI_SAN;
            item.KHONG_CHUYEN_MA = model.KHONG_CHUYEN_MA;
            item.LA_BAN_QL_DU_AN = model.LA_BAN_QL_DU_AN;
            item.LA_DON_VI_TU_CHU_TAI_CHINH = model.LA_DON_VI_TU_CHU_TAI_CHINH;
            item.DA_CO_QUYET_DINH_GIAO_VON = model.DA_CO_QUYET_DINH_GIAO_VON;
            item.LOAI_CAP_DON_VI_ID = model.LOAI_CAP_DON_VI_ID;
            item.TINH_ID = model.TINH_ID;
            item.HUYEN_ID = model.HUYEN_ID;
            item.XA_ID = model.XA_ID;
        }

        public DonViLichSu GetDonViLichSu(DonVi before, DonVi after)
        {
            if (before != null && after != null)
            {
                DonViLichSu donViLichSu = new DonViLichSu();
                donViLichSu.MA_DVQHNS = after.MA_DVQHNS;
                donViLichSu.TRUOC_MA_DVQHNS = before.MA_DVQHNS;

                donViLichSu.TEN = after.TEN;
                donViLichSu.TRUOC_TEN = before.TEN;

                donViLichSu.LOAI_DON_VI_ID = after.LOAI_DON_VI_ID;
                donViLichSu.TRUOC_LOAI_DON_VI_ID = before.LOAI_DON_VI_ID;

                donViLichSu.LOAI_DON_VI_ID = after.LOAI_DON_VI_ID;
                donViLichSu.TRUOC_LOAI_DON_VI_ID = before.LOAI_DON_VI_ID;

                donViLichSu.LA_DON_VI_NHAP_LIEU = after.LA_DON_VI_NHAP_LIEU;
                donViLichSu.TRUOC_LA_DON_VI_NHAP_LIEU = before.LA_DON_VI_NHAP_LIEU;

                donViLichSu.CAP_DON_VI_ID = after.CAP_DON_VI_ID;
                donViLichSu.TRUOC_CAP_DON_VI_ID = before.CAP_DON_VI_ID;

                donViLichSu.DON_VI_CHA_ID = after.PARENT_ID;
                donViLichSu.TRUOC_DON_VI_CHA_ID = before.PARENT_ID;

                donViLichSu.NGAY_CAP_NHAT = DateTime.Now;
                donViLichSu.LY_DO_THAY_DOI = "";
                donViLichSu.NGUOI_THAY_DOI_ID = _workContext.CurrentCustomer.ID;
                donViLichSu.DON_VI_ID = before.ID;
                return donViLichSu;
            }
            return null;
        }

        public IList<SelectListItem> PrepareSelectListDonVi(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "")
        {
            string tree = "";
            var _lstDvBp = _itemService.GetAllDonViUsingProc();
            var selectList = _lstDvBp.Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            return selectList;
        }

        public IList<SelectListItem> PrepareSelectListDonViUsingProc(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0)
        {
            string tree = "";
            if (!(IdDonVi > 0))
                IdDonVi = _workContext.CurrentDonVi.ID;
            if (IdDonVi == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                var _lstDvBp = _itemService.GetAllDonViBoNganhTinh().OrderBy(c => c.MA).ToList();
                var selectList = _lstDvBp.Select(c => new SelectListItem
                {
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-')+$"({c.MA})" + c.TEN,
                    Value = c.ID.ToString(),
                    Selected = valSelected == c.ID
                }).ToList();
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
                return selectList;
            }
            else
            {
                var _lstDvBp = _itemService.GetAllDonViChildUsingProc(IdDonVi).OrderBy(c => c.MA).ToList();
                var selectList = _lstDvBp.Select(c => new SelectListItem
                {
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + $"({c.MA})" + c.TEN,
                    Value = c.ID.ToString(),
                    Selected = valSelected == c.ID
                }).ToList();
                if (isAddFirst)
                    selectList.AddFirstRow(strFirstRow, valueFirstRow);
                return selectList;
            }
        }

        public async Task<IList<SelectListItem>> PrepareSelectListDonViForMultilSelectInput(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0, int take = 15, string keySearch = null, string ListSelectedId = null)
        {
            string tree = "";
            if (!(IdDonVi > 0))
                IdDonVi = _workContext.CurrentDonVi.ID;
            IQueryable<DonVi> _lstDvBp = _itemService.GetDonViForMultilSelectInput(IdDonVi, ListSelectedId);
            if (!string.IsNullOrEmpty(keySearch))
            {
                _lstDvBp = _lstDvBp.Where(c => c.MA.ToLower().Contains(keySearch.ToLower()) || c.TEN.ToLower().Contains(keySearch.ToLower()));
            }
            _lstDvBp = _lstDvBp.Take(take).OrderBy(c => c.MA);
            var items = await _lstDvBp.ToListAsync();
            var selectList = _lstDvBp.Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
            {
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            }

            return selectList;

        }

        public IList<SelectListItem> PrepareSelectListBoNganhTinh(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "")
        {
            string tree = "";

            var _lstDvBp = _itemService.GetAllDonViBoNganhTinh().Where(c => c.ID != (decimal)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG).OrderBy(c => c.MA).ToList();
            var selectList = _lstDvBp.Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            selectList.AddFirstRow(strFirstRow, valueFirstRow);
            return selectList;
        }

        public IList<SelectListItem> PrepareSelectListBoNganhTinhTrucThuoc(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0)
        {
            string tree = "";
            if (IdDonVi == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                var _lstDvBp = _itemService.GetAllDonViBoNganhTinh().OrderBy(c => c.MA).ToList();
                var selectList = _lstDvBp.Select(c => new SelectListItem
                {
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                    Value = c.ID.ToString(),
                    Selected = valSelected == c.ID
                }).ToList();
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
                return selectList;
            }
            else
            {
                var CurrenDonVi = _donViRepository.GetById(IdDonVi);
                var _lstDvBp = _itemService.GetAllDonViBoNganhTinh().Where(c => c.TREE_NODE.StartsWith(CurrenDonVi.TREE_NODE)).OrderBy(c => c.MA).ToList();
                var selectList = _lstDvBp.Select(c => new SelectListItem
                {
                    Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.TEN,
                    Value = c.ID.ToString(),
                    Selected = valSelected == c.ID
                }).ToList();
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
                return selectList;
            }
        }

        public DonVi GetDonViWithConditions(decimal? donViId = 0, bool? isBanQuanLyDuAn = false, bool? isCreateTSDA = false)
        {
            DonVi donVi = new DonVi();
            if (donViId <= 0)
            {
                donViId = _workContext.CurrentDonVi.ID;
            }
            donVi = _itemService.GetDonViById(donViId.Value);
            if ((!donVi.LA_BAN_QL_DU_AN.HasValue || donVi.LA_BAN_QL_DU_AN == false) && isBanQuanLyDuAn.Value)
            {
                donVi = _itemService.GetDonViBanQuanLyDuAn(donViId: donViId.Value, isDonViBanQuanLyDuAn: isCreateTSDA.Value);
            }
            else if ((donVi.LA_BAN_QL_DU_AN.HasValue && donVi.LA_BAN_QL_DU_AN == true) && !isCreateTSDA.Value)
            {
                var maDonViVp = donVi.MA + "001";
                DonVi donViVp = _donViService.GetDonViByMa(maDonViVp);
                if (donViVp != null && (!donViVp.LA_BAN_QL_DU_AN.HasValue || donViVp.LA_BAN_QL_DU_AN == false))
                {
                    donVi = donViVp;
                }
            }
            if (donVi == null)
            {
                donVi = _itemService.GetDonViById(donViId.Value);
            }
            return donVi;
        }

        public DonVi PrepareDonViConChoBQLDA(decimal parentId, bool? pLA_BAN_QL_DU_AN = false)
        {
            DonVi dv = new DonVi();
            DonVi donViParent = _itemService.GetDonViById(parentId);
            dv.LA_DON_VI_NHAP_LIEU = true;
            dv.CAP_DON_VI_ID = donViParent.CAP_DON_VI_ID;
            dv.PARENT_ID = donViParent.ID;
            dv.MA_BO = donViParent.MA_BO;
            dv.DIA_CHI = donViParent.DIA_CHI;
            dv.CHE_DO_HACH_TOAN_ID = donViParent.CHE_DO_HACH_TOAN_ID;
            dv.LOAI_CAP_DON_VI_ID = donViParent.LOAI_CAP_DON_VI_ID;
            dv.MA_DIA_BAN = donViParent.MA_DIA_BAN;
            dv.LA_BAN_QL_DU_AN = pLA_BAN_QL_DU_AN;
            dv.NGAY_TAO = new DateTime();
            dv.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            LoaiDonVi ldvQuanLyDuAn = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN);
            dv.LOAI_DON_VI_ID = ldvQuanLyDuAn.ID;
            if (pLA_BAN_QL_DU_AN.Value)
            {
                //đơn vị quản lý tsda
                dv.TEN = "Đơn vị dự án của " + donViParent.TEN;
                dv.MA = donViParent.MA + "002";
                dv.MA_DVQHNS = donViParent.MA + "002";
            }
            else
            {
                //đơn vị quản lý tsc
                dv.TEN = "Văn phòng " + donViParent.TEN;
                dv.MA = donViParent.MA + "001";
                dv.MA_DVQHNS = donViParent.MA + "001";
            }
            return dv;
        }

        public DonViListModel PrepareDonViListChonDonViModel(DonViSearchModel searchModel)
        {
            DonViListModel model = new DonViListModel();
            DonVi parentDonVi = _itemService.GetDonViById(searchModel.ParentID ?? 0);
            LoaiDonVi ldvBanQLDA = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN);
            if (!(parentDonVi != null && parentDonVi.LOAI_DON_VI_ID == ldvBanQLDA.ID))
            {
                model = PrepareDonViListModel(searchModel);
            }
            else
            {
                model.Data = new List<DonViModel>();
            }
            return model;
        }

        public List<SelectListItem> PrepareDdlDonVi(int DonViCha = 0)
        {
            var listDonVi = _itemService.GetListDonViChild(DonViCha).Where(m => m.PARENT_ID == DonViCha);
            var ddlDonVi = listDonVi.Select(m => new SelectListItem()
            {
                Text = m.TEN,
                Value = m.ID.ToString()
            }).ToList();
            return ddlDonVi;
        }

        public DonViSearchModel PrepareDonViXacNhanSearchModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }
        public DonViListModel PrepareDonViXacNhanListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchAllDonViXacNhanDuLieu(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, NGAY_XAC_NHAN: searchModel.NGAY_XAC_NHAN);

            //prepare list model
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DonViModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        #endregion DonVi

        #region Kiểm tra loại hình đơn vị

        public DonViListModel PrepareListKiemTraLoaiHinhDonVi(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchListKiemTraLoaiHinhDonVi(Keysearch: searchModel.KeySearch,
                 LoaiDonViSearch: searchModel.LoaiDonViSearch, NguoiDungID: searchModel.nguoiDungId, IsQuanTri: _workContext.CurrentCustomer.IS_QUAN_TRI,
                CapDonViSearch: searchModel.CapDonViSearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donViId: searchModel.donViId);
            LoaiDonVi ldvBanQLDA = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN);
            //prepare list model
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViModel>();
                    m.isSelected = _nguoidungDonViService.KiemTraDaChon(NguoiDungId: searchModel.nguoiDungId, DonViId: m.ID);
                    m.TEN_DON_VI_CHA = c.DonViCha != null ? c.DonViCha.TEN : String.Empty;

                    if (ldvBanQLDA == null || (ldvBanQLDA != null && c.LOAI_DON_VI_ID != ldvBanQLDA.ID))
                    {
                        m.SO_DON_VI_CON = _itemService.GetSoDonViConByID(id: c.ID);
                    }
                    m.TenCapDonVi = _nhanHienThiService.GetGiaTriEnum<CapEnum>((CapEnum)m.CAP_DON_VI_ID);
                    m.pageIndex = searchModel.Page;

                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }

        public KiemTraLoaiHinhDonViModel PrepareKiemTraLoaiHinhDonViModel(KiemTraLoaiHinhDonViModel model)
        {
            //more
            model.ddlLoaiCapDonVi = (new EnumLoaiCapDonVi()).ToSelectList();
            if (model.LOAI_CAP_DON_VI_ID == (int)EnumLoaiCapDonVi.CapDiaPhuong)
                model.dllCapDonVi = ((EnumCapDonViDiaPhuong)(model.CAP_DON_VI_ID ?? 0)).ToSelectList();
            else
                model.dllCapDonVi = ((EnumCapDonViTrungUong)(model.CAP_DON_VI_ID ?? 0)).ToSelectList();
            model.dllLoaiDonVi = _itemLoaiDonViModelFactory.PrepareSelectListLoaiDonVi(isAddFirst: true, ValSelected: model.LOAI_DON_VI_ID);

            return model;
        }

        public IList<DonViModel> PrepareListDonViMuaSamForInputSearch(string tenDonVi = null)
        {
            var items = _itemService.GetDonViMuaSamForInputSearch(tenDonVi, _workContext.CurrentDonVi.ID);
            var model = items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                m.TEN = m.TEN.PadLeft((int)((c.TREE_LEVEL - 1) * 2 + m.TEN.Length), '-');
                return m;
            }
            ).ToList();
            return model;
        }

        #endregion Kiểm tra loại hình đơn vị

        #region Cảnh báo đơn vị chưa có mã T
        public DonViModel PrepareDonViModelFromDMDC(DMDC_DonViNganSach dMDC_DonViNganSach)
        {
            var model = PrepareDonViModel(new DonViModel(), null);
            MapDonViModelFromDMDC(model, dMDC_DonViNganSach);
            return model;

        }
        public DonViModel MapDonViModelFromDMDC(DonViModel model, DMDC_DonViNganSach dMDC_DonViNganSach)
        {

            model.TEN = dMDC_DonViNganSach.TEN;
            model.DIA_CHI = dMDC_DonViNganSach.DIACHI;
            model.MA_DVQHNS = dMDC_DonViNganSach.MA;
            model.MA_CHUONG = dMDC_DonViNganSach.CHUONG;
            var donViCapTren = _itemService.GetDonViByMaDVQHNS(dMDC_DonViNganSach.DVCT_MA);
            if (donViCapTren != null)
            {
                model.PARENT_ID = donViCapTren.ID;
                model.TEN_DON_VI_CHA = donViCapTren.TEN;
            }
            return model;

        }
        public DonViListModel PrepareDonViChuaCoMaTListModel(DonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchListDonViChuaCapNhatMaT(Keysearch: searchModel.KeySearch,
                 pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new DonViListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = MapDonViModelFromDMDC(new DonViModel() { }, c);
                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }
        #endregion
    }
}