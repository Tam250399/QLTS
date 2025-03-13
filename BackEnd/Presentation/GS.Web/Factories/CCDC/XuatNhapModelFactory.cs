//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CCDC;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.CCDC;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.CCDC;
using GS.Services.DanhMuc;
using GS.Services;
using GS.Web.Factories.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Factories.CCDC
{
    public class XuatNhapModelFactory : IXuatNhapModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IXuatNhapService _itemService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly ICongCuService _congCuService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly ICongCuModelFactory _congCuModelFactory;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViService _donViService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IChoThueService _choThueService;
        private readonly ISuaChuaBaoDuongService _suaChuaBaoDuongService;
        private readonly INguoiDungService _nguoiDungService;
        #endregion

        #region Ctor

        public XuatNhapModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IXuatNhapService itemService,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICongCuService congCuService,
            INhomCongCuService nhomCongCuService,
            IDonViBoPhanService donViBoPhanService,
            ICongCuModelFactory congCuModelFactory,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViService donViService,
            INhomCongCuModelFactory nhomCongCuModelFactory,
            INhanHienThiService nhanHienThi,
            IChoThueService choThueService,
            ISuaChuaBaoDuongService suaChuaBaoDuongService,
            INguoiDungService nguoiDungService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._congCuService = congCuService;
            this._nhomCongCuService = nhomCongCuService;
            this._donViBoPhanService = donViBoPhanService;
            this._congCuModelFactory = congCuModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViService = donViService;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
            this._nhanHienThiService = nhanHienThi;
            this._choThueService = choThueService;
            this._suaChuaBaoDuongService = suaChuaBaoDuongService;
            this._nguoiDungService = nguoiDungService;
        }
        #endregion

        #region XuatNhap
        public XuatNhapSearchModel PrepareXuatNhapSearchModel(XuatNhapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(donViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public LuanChuyenSearchModel PrepareLuanChuyenSearchModel(LuanChuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(donViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public XuatNhapListModel PrepareXuatNhapListModel(XuatNhapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchXuatNhaps(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isPhanBo: searchModel.IsPhanBo);
            var listModel = new List<XuatNhapModel>();
            foreach (var item in items)
            {
                var md = PrepareListXuatNhap(item);
                foreach (var m in md)
                {
                    listModel.Add(m);
                }
            }
            //prepare list model
            var model = new XuatNhapListModel
            {
                //fill in model values from the entity
                Data = listModel,
                Total = items.TotalCount
            };
            return model;
        }
        public XuatNhapListModel PrepareXuatNhapPhanBoListModel(XuatNhapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchXuatNhapPhanBos(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isPhanBo: searchModel.IsPhanBo, LoaiCongCu: searchModel.LoaiCongCu, DonViBoPhanId: searchModel.DonViBoPhanId, TuNgay: searchModel.TuNgay, DenNgay: searchModel.DenNgay);
            //prepare list model
            var model = new XuatNhapListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareXuatNhapModelForPhanBo(c)),
                Total = items.TotalCount
            };
            return model;
        }
        public XuatNhapModel PrepareXuatNhapModelForPhanBo(NhapXuatCongCu map)
        {
            var maBoPhan = "";
            if (map.XuatNhap.DON_VI_BO_PHAN_ID > 0)
            {
                maBoPhan = "-" + map.XuatNhap.DON_VI_BO_PHAN_ID.ToString();
            }
            var model = new XuatNhapModel()
            {
                MaLoCongCuText = map.XuatNhap.MA,
                TenCongCuText = map.CongCu.TEN,
                MaCongCuText = map.CongCu.MA + maBoPhan,
                NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((decimal)map.CongCu.NHOM_CONG_CU_ID).TEN,
                BoPhanText = map.XuatNhap.DON_VI_BO_PHAN_ID > 0 ? _donViBoPhanService.GetDonViBoPhanById((decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN : "",
                DonGiaText = map.DON_GIA.ToVNStringNumber(),
                SoLuongText = map.SO_LUONG.ToVNStringNumber(),
                NgayXuatNhapText = map.XuatNhap.NGAY_XUAT_NHAP.toDateVNString(),
                MapId = map.ID
            };
            //check xem nếu phân bổ mà đơn vị bộ phận ý đã sử dụng thì k cho sửa
            var xncheck = _itemService.GetXuatNhapForDieuChuyen(isXuat: true, FromXuatNhap: map.XuatNhap.ID);
            if (xncheck != null)
            {
                var nxcccheck = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: xncheck.ID).Count();
                if (nxcccheck > 0)
                {
                    model.IsEdit = false;
                }
            }
            return model;
        }
        public List<XuatNhapModel> PrepareListXuatNhap(XuatNhap item)
        {
            var listModel = new List<XuatNhapModel>();
            var listMap = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID);
            foreach (var map in listMap)
            {
                var firstItem = _itemService.GetXuatNhap(item.MA_LIEN_QUAN);
                var model = new XuatNhapModel()
                {
                    TenCongCuText = map.CongCu.TEN,
                    MaCongCuText = map.CongCu.MA,
                    NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((decimal)map.CongCu.NHOM_CONG_CU_ID).TEN,
                    BoPhanText = item.DON_VI_BO_PHAN_ID > 0 ? _donViBoPhanService.GetDonViBoPhanById((decimal)item.DON_VI_BO_PHAN_ID).TEN : "",
                    DonGiaText = map.DON_GIA.ToVNStringNumber(),
                    SoLuongText = map.SO_LUONG.ToVNStringNumber(),
                    NgayXuatNhapText = item.NGAY_XUAT_NHAP.toDateVNString(),
                    MapId = map.ID
                };
                listModel.Add(model);
            }

            return listModel;
        }
        public XuatNhapModel PrepareXuatNhapModel(XuatNhapModel model, XuatNhap item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<XuatNhapModel>();
            }
            //more
            model.DDLTrangThai = PrePareDDLTrangThai(1);

            return model;
        }
        public SelectList PrePareDDLTrangThai(int BoPhanId)
        {

            if (BoPhanId > 0)
            {
                var model = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHUA_SU_DUNG });
                return model;
            }
            else
            {
                var model = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHON, (int)enumTrangThaiCongCu.DANG_SU_DUNG, (int)enumTrangThaiCongCu.HONG_CHO_XU_LY });
                return model;
            }

        }
        public XuatNhapModel PreparePhanBoModel(string StringId, bool whenEdit = false)
        {
            var model = new XuatNhapModel();
            var listId = StringId.Split(',');
            model.NgayNhapKhoMin = DateTime.Now;
            foreach (var mapid in listId)
            {
                var item = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(Decimal.Parse(mapid));
                if (item.XuatNhap.NGAY_XUAT_NHAP < model.NgayNhapKhoMin)
                {
                    model.NgayNhapKhoMin = (DateTime)item.XuatNhap.NGAY_XUAT_NHAP;
                }
                var map = item.ToModel<NhapXuatCongCuModel>();
                map.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)item.CongCu.NHOM_CONG_CU_ID)?.TEN;
                map.DonGiaText = item.DON_GIA.ToVNStringNumber();
                map.TRANG_THAI_ID = item.TRANG_THAI_ID;
                map.TenCongCuText = item.CongCu != null ? item.CongCu.TEN : "";
                map.MaCongCuText = item.CongCu != null ? item.CongCu.MA : "";
                var soNhap = new NhapXuatCongCu();
                if (item.XuatNhap.MA_LIEN_QUAN != null)
                {
                    //lấy xuất nhập đầu tiên theo mã liên quan
                    var xnsoNhap = _itemService.GetXuatNhap(item.XuatNhap.MA_LIEN_QUAN);
                    if (xnsoNhap != null)
                    {
                        soNhap = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: xnsoNhap.ID, CongCuId: item.CONG_CU_ID, DonGia: (decimal)item.DON_GIA).FirstOrDefault();
                    }
                }
                else if (item.NHAP_KHO_ID == null && item.XuatNhap.IS_XUAT == false)
                {
                    soNhap = item;
                }
                else
                {
                    soNhap = _nhapXuatCongCuService.GetNhapXuatCongCuById((decimal)item.NHAP_KHO_ID);
                }
                if (soNhap != null)
                {
                    var soDaXuat = _nhapXuatCongCuService.GetSoLuongDaXuat(item.ID, true);
                    map.SoLuongCoThePhanBo = (decimal)soNhap.SO_LUONG - soDaXuat + (whenEdit == true ? (Decimal)item.SO_LUONG : 0);
                }
                //nếu là sửa chỉ lấy số lượng đã phân bổ
                if (whenEdit)
                {
                    map.SO_LUONG = item.SO_LUONG;
                }
                else
                {
                    map.SO_LUONG = map.SoLuongCoThePhanBo;
                }
                model.ListMap.Add(map);
            }
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            model.DDLTrangThai = PrePareDDLTrangThai(1);
            model.StringMapId = StringId;

            return model;
        }

        public void PrepareXuatNhap(XuatNhapModel model, XuatNhap item)
        {
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.FROM_XUAT_NHAP_ID = model.FROM_XUAT_NHAP_ID;
            item.NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP;
            item.DON_VI_ID = model.DON_VI_ID;
            item.NGUOI_DUNG_ID = model.NGUOI_DUNG_ID;
            item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
            item.MA = model.MA;
            item.MA_LIEN_QUAN = model.MA_LIEN_QUAN;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGAY_CAP_NHAP = model.NGAY_CAP_NHAP;
            item.GHI_CHU = model.GHI_CHU;
            item.IS_XUAT = model.IS_XUAT;
            item.GUID = model.GUID;
        }

        public XuatNhapListModel PrepareLuanChuyenCongCu(XuatNhapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh(searchModel.DonViBoPhanId, KeySearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new XuatNhapListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new XuatNhapModel(), c, searchModel.DonViBoPhanId)),
                Total = items.TotalCount
            };
            return model;
        }

        public XuatNhapModel PrepareCongCu(XuatNhapModel model, NhapXuatCongCu map, Decimal BoPhanId)
        {
            model.MaLoCongCuText = map.XuatNhap.MA;
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.DON_VI_BO_PHAN_ID = (decimal)map.XuatNhap.DON_VI_BO_PHAN_ID;
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN;

            return model;
        }

        public XuatNhapModel PrepareCongCuForLuanChuyen(XuatNhapModel model, Decimal MapId, Decimal BoPhanId, bool whenEdit = false)
        {
            var map = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(MapId);
            model.MapId = MapId;
            model.DonViText = _donViService.GetDonViById((Decimal)map.XuatNhap.DON_VI_ID).TEN;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById(BoPhanId).TEN;
            model.BoPhanId = BoPhanId;
            model.TongSoLuong = (Decimal)map.SoLuongCoThePhanBo;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(isAddFirst: true, DonViId: _workContext.CurrentDonVi.ID, strFirstRow: "Chọn đơn vị bộ phận", valSelected: model.DON_VI_BO_PHAN_ID).ToList();
            if (whenEdit)
            {
                model.SoLuongText = (map.SoLuongCoThePhanBo + map.SO_LUONG).ToVNStringNumber();
                model.TongSoLuong = (Decimal)map.SoLuongCoThePhanBo + (Decimal)map.SO_LUONG;
            }
            //nếu là nhập kho thì sẽ lấy phiếu xuất => từ xuất lấy ra phiếu nhập phân bổ vào phòng ban bộ phận đó
            if (map.XuatNhap.IS_XUAT == false)
            {
                var xuat = _itemService.GetXuatNhapById((decimal)map.XuatNhap.FROM_XUAT_NHAP_ID); // phiếu xuất
                var xnBefore = _itemService.GetXuatNhapById((decimal)xuat.FROM_XUAT_NHAP_ID);// phiếu nhận lúc phân bổ
                model.DateBefore = (DateTime)xnBefore.NGAY_XUAT_NHAP;
            }
            else
            {
                if (map.XuatNhap.FROM_XUAT_NHAP_ID != null)
                {
                    var xnBefore = _itemService.GetXuatNhapById((decimal)(map.XuatNhap.FROM_XUAT_NHAP_ID ?? map.NHAP_KHO_ID));// phiếu nhận lúc phân bổ
                    model.DateBefore = (DateTime)xnBefore.NGAY_XUAT_NHAP;
                }
                else
                {
                    var xuatccdc = _nhapXuatCongCuService.GetNhapXuatCongCuById((decimal)map.NHAP_KHO_ID);
                    var xnBefore = _itemService.GetXuatNhapById((decimal)(xuatccdc?.NHAP_XUAT_ID));// phiếu nhận lúc phân bổ
                    model.DateBefore = (DateTime)xnBefore.NGAY_XUAT_NHAP;
                }



            }
            //var xnBefore = _itemService.GetXuatNhapById((decimal)map.NHAP_KHO_ID);
            //if (xnBefore != null)
            //{
            //    model.DateBefore =  (DateTime)xnBefore.NGAY_XUAT_NHAP;
            //}

            return model;
        }

        public XuatNhapListModel PrepareLuanChuyenListModel(LuanChuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _itemService.SearchLuanChuyen(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, LoaiCongCu: searchModel.LoaiCongCu, TuNgay: searchModel.TuNgay, DenNgay: searchModel.DenNgay);

            //prepare list model
            var model = new XuatNhapListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareLuanChuyen(new XuatNhapModel(), c)).OrderByDescending(c => c.NGAY_XUAT_NHAP).ThenBy(c => c.MaCongCuText),
                Total = items.TotalCount
            };
            return model;
        }

        public XuatNhapModel PrepareLuanChuyen(XuatNhapModel model, XuatNhap item)
        {
            var map = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID).FirstOrDefault();
            var xnBefore = _itemService.GetXuatNhapById((Decimal)item.FROM_XUAT_NHAP_ID);
            model = item.ToModel<XuatNhapModel>();
            if (map != null)
            {
                model.MaCongCuText = map.CongCu.MA;
                model.TenCongCuText = map.CongCu.TEN;
                model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
                model.DonGiaText = map.DON_GIA.ToVNStringNumber();
                model.SoLuongText = map.SO_LUONG.ToVNStringNumber();
            }
            if (xnBefore != null)
            {
                model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)xnBefore.DON_VI_BO_PHAN_ID).TEN;
            }
            model.BoPhanSuDungDenText = _donViBoPhanService.GetDonViBoPhanById((decimal)item.DON_VI_BO_PHAN_ID).TEN;
            model.NgayXuatNhapText = item.NGAY_XUAT_NHAP.toDateVNString();

            return model;
        }

        public void PrepareLuanChuyenEdit(XuatNhapModel model, XuatNhap item)
        {
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
            item.NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP;
            item.GHI_CHU = model.GHI_CHU;
        }

        public ThongTinCongCuListModel PrepareThongTinCongCuModel(decimal NhapXuatId)
        {
            var thongTinCongCuListModel = new List<ThongTinCongCuModel>();
            // lấy nhập xuất
            var nhapXuat = _nhapXuatCongCuService.GetNhapXuatCongCuById(NhapXuatId);
            var xuatNhap = _itemService.GetXuatNhapById(nhapXuat.NHAP_XUAT_ID);

            // lấy mã công cụ
            var maBoPhan = "";
            if (xuatNhap.DON_VI_BO_PHAN_ID > 0)
            {
                maBoPhan = "-" + xuatNhap.DON_VI_BO_PHAN_ID.ToString();
            }
            string MaCongCu = $"{nhapXuat.CongCu?.MA}{maBoPhan}";

            // số thứ tự để sắp xếp
            decimal stt = 0;
            if (nhapXuat != null && xuatNhap != null)
            {
                // Lấy thông tin lần đầu nhập lô công cụ chèn vào 
                var xuatNhapBanDau = _itemService.GetXuatNhap(Ma: xuatNhap.MA_LIEN_QUAN);
                var NhapXuatBanDau = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(xuatNhapBanDau.ID).Where(c => c.CONG_CU_ID == nhapXuat.CONG_CU_ID && c.DON_GIA == nhapXuat.DON_GIA).FirstOrDefault();
                if (xuatNhapBanDau != null)
                {
                    stt += 1;
                    thongTinCongCuListModel.Add(GetThongTinByXuatNhap(xuatNhapBanDau, MaCongCu, stt, NhapXuatBanDau?.SO_LUONG));

                    
                }
                // lấy thông tin Nhập kho của chính nhập kho đó
                stt += 1;
                var thongTinNhapKho = GetThongTinByXuatNhap(xuatNhap,MaCongCu, stt, nhapXuat?.SO_LUONG);
              
                thongTinCongCuListModel.Add(thongTinNhapKho);
               


                // lấy thông tin xuất kho nếu có
                var listXuatKho = _itemService.GetXuatNhaps(isXuat: true, FromXuatNhap: xuatNhap.ID)?.Where(c => c.TRANG_THAI_ID != null).ToList();
                if (listXuatKho != null && listXuatKho.Count() >0)
                {
                    foreach (var xn in listXuatKho)
                    {
                        if (xn != null)
                        {
                            stt += 1;
                            var nx = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(xn.ID).FirstOrDefault();
                            thongTinCongCuListModel.Add(GetThongTinByXuatNhap(xn,MaCongCu,stt, nx.SO_LUONG));

                        }
                    }
                }
                // Lấy thông tin sửa chữa bảo dưỡng nếu có
                var listSuaChua = _suaChuaBaoDuongService.GetSuaChuaBaoDuongs(NhapXuatId);
                if (listSuaChua != null && listSuaChua.Count() > 0)
                {
                    foreach (var suaChua in listSuaChua)
                    {
                        if (suaChua != null)
                        {
                            stt += 1;
                            var ThongTinXuatNhap = new ThongTinCongCuModel();
                                ThongTinXuatNhap.ID = stt;
                                ThongTinXuatNhap.Soluong = suaChua.SO_LUONG_SUA_CHUA;
                                ThongTinXuatNhap.MaCongCu = MaCongCu;
                                ThongTinXuatNhap.TenLoaiBienDong = "Sửa chữa công cụ, dụng cụ";
                                ThongTinXuatNhap.StrNgayBienDong = suaChua.CHUNG_TU_NGAY?.toDateVNString();
                                ThongTinXuatNhap.NgayBienDong = suaChua.CHUNG_TU_NGAY;
                                ThongTinXuatNhap.StrNgayTao = suaChua.NGAY_TAO.toDateVNString();
                                ThongTinXuatNhap.GhiChu = suaChua.GHI_CHU;
                            ThongTinXuatNhap.NguoiTao = _nguoiDungService.GetNguoiDungById(suaChua.NGUOI_TAO_ID ?? 0)?.TEN_DAY_DU;
                            thongTinCongCuListModel.Add(ThongTinXuatNhap);
                        }
                    }
                }
                // Lấy thông tin cho thuê nếu có
                var listChoThue = _choThueService.GetChoThues(NhapXuatId);
                if (listChoThue != null && listChoThue.Count() > 0)
                {
                    foreach (var choThue in listChoThue)
                    {
                        if (choThue != null)
                        {
                            stt += 1;
                            var ThongTinXuatNhap = new ThongTinCongCuModel();
                            ThongTinXuatNhap.ID = stt;
                            ThongTinXuatNhap.Soluong = choThue.SO_LUONG;
                            ThongTinXuatNhap.MaCongCu = MaCongCu;
                            ThongTinXuatNhap.TenLoaiBienDong = "Cho thuê công cụ, dụng cụ";
                            ThongTinXuatNhap.StrNgayBienDong = choThue.QUYET_DINH_NGAY.toDateVNString();
                            ThongTinXuatNhap.NgayBienDong = choThue.QUYET_DINH_NGAY;
                            ThongTinXuatNhap.StrNgayTao = choThue.NGAY_TAO.toDateVNString();
                            ThongTinXuatNhap.GhiChu = choThue.GHI_CHU;
                            ThongTinXuatNhap.NguoiTao = _nguoiDungService.GetNguoiDungById(choThue.NGUOI_TAO_ID ?? 0)?.TEN_DAY_DU; 
                            thongTinCongCuListModel.Add(ThongTinXuatNhap);
                        }
                    }
                }

                // sắp xếp theo thời gian tăng dần
                thongTinCongCuListModel.OrderBy(c => c.NgayBienDong).ThenBy(c => c.ID);               
            }

            var model = new ThongTinCongCuListModel
            {
                //fill in model values from the entity
                Data = thongTinCongCuListModel,
                Total = thongTinCongCuListModel.Count()
            };
            return model;


        }
        private ThongTinCongCuModel GetThongTinByXuatNhap(XuatNhap xuatNhap = null, string MaCongCu = null, decimal stt = 0, decimal? soLuong = 0 )
        {
            var ThongTinXuatNhap = new ThongTinCongCuModel();
            if (xuatNhap != null)
            {
                ThongTinXuatNhap.ID = stt;
                ThongTinXuatNhap.MaCongCu = MaCongCu;
                ThongTinXuatNhap.StrNgayBienDong = xuatNhap.NGAY_XUAT_NHAP?.toDateVNString();
                ThongTinXuatNhap.NgayBienDong = xuatNhap.NGAY_XUAT_NHAP;
                ThongTinXuatNhap.StrNgayTao = xuatNhap.NGAY_TAO?.toDateVNString();
                ThongTinXuatNhap.GhiChu = xuatNhap.GHI_CHU;
                ThongTinXuatNhap.Soluong = soLuong;
                ThongTinXuatNhap.NguoiTao = _nguoiDungService.GetNguoiDungById(xuatNhap.NGUOI_DUNG_ID ?? 0)?.TEN_DAY_DU;
                if (xuatNhap.LOAI_XUAT_NHAP_ID == (decimal)enumLoaiXuatNhap.NHAP_KHO)
                {
                    ThongTinXuatNhap.TenLoaiBienDong = $"Nhập lô công cụ, dụng cụ: {_nhanHienThiService.GetGiaTriEnum((enumMucDichXuatNhap)xuatNhap.MUC_DICH_XUAT_NHAP_ID)}";
                }
                else if (xuatNhap.LOAI_XUAT_NHAP_ID == (decimal)enumLoaiXuatNhap.PHAN_BO)
                {
                    ThongTinXuatNhap.TenLoaiBienDong = "Phân bổ công cụ, dụng cụ";
                }
                else if (xuatNhap.LOAI_XUAT_NHAP_ID == (decimal)enumLoaiXuatNhap.LUAN_CHUYEN)
                {
                    ThongTinXuatNhap.TenLoaiBienDong = "Luân chuyển công cụ, dụng cụ";
                }
                else
                {
                    ThongTinXuatNhap.TenLoaiBienDong = _nhanHienThiService.GetGiaTriEnum((enumLyDoGiam)xuatNhap.LOAI_XUAT_NHAP_ID);
                }
            }
            return ThongTinXuatNhap;
        }
        #endregion
    }
}

