using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.CCDC;
using GS.Services;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.CCDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.CCDC
{
    public class GiamDieuChuyenModelFactory : IGiamDieuChuyenModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly ICongCuService _congCuService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IDonViService _donViService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        #endregion

        #region Ctor

        public GiamDieuChuyenModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICongCuService congCuService,
            INhomCongCuService nhomCongCuService,
            IDonViBoPhanService donViBoPhanService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViModelFactory donViModelFactory,
            IDonViService donViService,
            INhomCongCuModelFactory nhomCongCuModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._congCuService = congCuService;
            this._nhomCongCuService = nhomCongCuService;
            this._donViBoPhanService = donViBoPhanService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViModelFactory = donViModelFactory;
            this._donViService = donViService;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
        }
        #endregion

        #region Methods
        public GiamDieuChuyenSearchModel PrepareCongCuSearchModel(GiamDieuChuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(donViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public virtual GiamDieuChuyenListModel PrepareGiamDieuChuyenListModel(GiamDieuChuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.SeaechForGiamDieuChuyen(LoaiDieuChuyen: searchModel.LoaiDieuChuyen, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,LoaiCongCu : searchModel.LoaiCongCu, TuNgay : searchModel.TuNgay, DenNgay :searchModel.DenNgay,DonViBoPhanId:searchModel.DonViBoPhanId);
            var model = new GiamDieuChuyenListModel
            {
                Data = items.Select(c => PrepareGiamDieuChuyenModelForList(new GiamDieuChuyenModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public virtual GiamDieuChuyenListModel PrepareGiamKhacListModel(GiamKhacSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.SearchForGiamKhac(LoaiDieuChuyen: searchModel.LoaiDieuChuyen, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var model = new GiamDieuChuyenListModel
            {
                Data = items.Select(c => PrepareGiamDieuChuyenModelForList(new GiamDieuChuyenModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public GiamDieuChuyenModel PrepareGiamDieuChuyenModelForList(GiamDieuChuyenModel model, NhapXuatCongCu item)
        {
            var congcu = _congCuService.GetCongCuById(item.CONG_CU_ID);
            if (item.XuatNhap.DON_VI_BO_PHAN_ID > 0)
                model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((Decimal)item.XuatNhap.DON_VI_BO_PHAN_ID).TEN;
            model.MaCongCuText = congcu.MA;
            model.TenCongCuText = congcu.TEN;
            if(congcu.NHOM_CONG_CU_ID > 0)
                model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)congcu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = item.DON_GIA.ToVNStringNumber();
            model.SoLuongText = item.SO_LUONG.ToVNStringNumber();
            model.NgayXuatNhapText = item.XuatNhap.NGAY_XUAT_NHAP.toDateVNString();
            model.TenLoaiXuatNhap = ((int)item.XuatNhap.LOAI_XUAT_NHAP_ID).ToStringLoaiXuatNhap();
            model.MapId = item.ID;

            return model;
        }
        public virtual GiamDieuChuyenListModel PrepareForChonCongCu(GiamDieuChuyenSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _nhapXuatCongCuService.GetNhapXuatCongCusByBaoHanh(searchModel.DonViBoPhanId, KeySearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,DonViId:_workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new GiamDieuChuyenListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareCongCu(new GiamDieuChuyenModel(), c, searchModel.DonViBoPhanId)),
                Total = items.TotalCount
            };
            return model;
        }

        public GiamDieuChuyenModel PrepareCongCu(GiamDieuChuyenModel model, NhapXuatCongCu map, Decimal BoPhanId)
        {
            model.MapId = map.ID;
            model.MaCongCuText = map.CongCu.MA;
            model.TenCongCuText = map.CongCu.TEN;
            model.SoLuongText = map.SoLuongCoThePhanBo.ToVNStringNumber();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonGiaText = map.DON_GIA.ToVNStringNumber();
            model.BoPhanSuDungText = _donViBoPhanService.GetDonViBoPhanById((decimal)map.XuatNhap.DON_VI_BO_PHAN_ID).TEN;
            model.MaLoCongCuText = map.XuatNhap.MA;

            return model;
        }

        public virtual GiamDieuChuyenModel PrepareDieuChuyen(string StringId, bool whenEdit = false)
        {
            var model = new GiamDieuChuyenModel();

            var listMapId = StringId.Split(',');
            foreach (var mapId in listMapId)
            {
                var map = _nhapXuatCongCuService.GetSoLuongDangCoByMapId(decimal.Parse(mapId));
                // lấy xuất nhập đầu tiên 
                var xn_first = _nhapXuatCongCuService.GetNhapXuatCongCuById((decimal)(map.NHAP_KHO_ID??map.ID));
                var ccDieuChuyen = new CongCuDieuChuyenModel
                {
                    MaCongCu = map.CongCu.MA,   
                    MapId = decimal.Parse(mapId),
                    NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((Decimal)map.CongCu.NHOM_CONG_CU_ID).TEN,
                    SoLuongCoTheChuyen = map.SoLuongCoThePhanBo,
                    SoLuongCoTheChuyenText = map.SoLuongCoThePhanBo.ToVNStringNumber(),
                    TenCongCu = map.CongCu.TEN,
                    DonGia = map.DON_GIA,
                    DonGiaText = map.DON_GIA.ToVNStringNumber(),
                    SoLuongDieuChuyen = map.SoLuongCoThePhanBo,
                    NgayPhanBo = xn_first.XuatNhap.NGAY_XUAT_NHAP
                };
                if (whenEdit)
                {
                    var nhap = _xuatNhapService.GetXuatNhapForDieuChuyen(FromXuatNhap: map.XuatNhap.ID, isXuat: false, LoaiXuatNhap: (decimal)map.XuatNhap.LOAI_XUAT_NHAP_ID);
                    if (nhap != null)
                    {
                        model.TenDonViTo = _donViService.GetDonViById((decimal)nhap.DON_VI_ID).TEN;
                        if (map.XuatNhap.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN) model.DON_VI_ID = (Decimal)nhap.DON_VI_ID;
                    }                   
                    ccDieuChuyen.SoLuongDieuChuyen = (Decimal)map.SO_LUONG;
                    ccDieuChuyen.SoLuongCoTheChuyen = map.SoLuongCoThePhanBo + (Decimal)map.SO_LUONG;
                    ccDieuChuyen.SoLuongCoTheChuyenText = (map.SoLuongCoThePhanBo + (Decimal)map.SO_LUONG).ToVNStringNumber();
                    ccDieuChuyen.NgayPhanBo = xn_first.XuatNhap.NGAY_XUAT_NHAP;
                }
                model.ListCongCuDieuChuyenModel.Add(ccDieuChuyen);
            }
            model.stringMapId = StringId;
            //
           
            model.DDLLoaiXuatNhap = (new enumLyDoGiam()).ToSelectList(valuesToExclude: new int[] {5,6}).ToList(); 
            // Lý do giảm để  giống tài sản (bỏ Điều chuyển ngoài hệ thống, Lập giấy báo hỏng mất, Chuyển giao về địa phương) -- hungnt
            model.DDLLoaiXuatNhap.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Chọn lý do giảm", Value = "0" });            
            return model;
        }
        #endregion
    }
}
