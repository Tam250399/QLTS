//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.HeThong;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class NguoiDungModelFactory : INguoiDungModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INguoiDungService _itemService;
        private readonly IVaiTroService _vaiTroService;
        private readonly IVaiTroNguoiDungService _vaiTroNguoiDungService;
        private readonly IDonViService _DonViService;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly INhanHienThiService _nhanHienThiService;
		private readonly IDonViModelFactory _donViModelFactory;
        private readonly ICauHinhService _cauHinhService;
        #endregion

        #region Ctor

        public NguoiDungModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            INguoiDungService itemService,
            IVaiTroService vaiTroService,
            IVaiTroNguoiDungService vaiTroNguoiDungService,
            IDonViService DonViService,
            INguoiDungDonViService nguoiDungDonViService,
            INhanHienThiService nhanHienThiService,
            IDonViModelFactory donViModelFactory,
            ICauHinhService cauHinhService
            )
        {
            this._vaiTroService = vaiTroService;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._vaiTroNguoiDungService = vaiTroNguoiDungService;
            this._DonViService = DonViService;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._nhanHienThiService = nhanHienThiService;
			_donViModelFactory = donViModelFactory;
            this._cauHinhService = cauHinhService;
		}
        #endregion

        #region NguoiDung
        public NguoiDungSearchModel PrepareNguoiDungSearchModel(NguoiDungSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ListNguoiDungDaChon.Count();
            searchModel.SetGridPageSize();
            searchModel.ddlDonViBoTinh = _donViModelFactory.PrepareSelectListBoNganhTinhTrucThuoc(isAddFirst:true, IdDonVi: _workContext.CurrentDonVi.ID).ToList();
            //searchModel.DonViBoTinhId = _workContext.CurrentDonVi.ID;
            //searchModel.DonViId = _workContext.CurrentDonVi.ID;
            //searchModel.ddlDonViQuanHuyen = _donViModelFactory.PrepareSelectListDonViUsingProc(isAddFirst: true, IdDonVi: searchModel.DonViBoTinhId).ToList();
            return searchModel;
        }

        public NguoiDungSearchModel PrepareNguoiDungSearchModelDuToanCap2(NguoiDungSearchModel searchModel) 
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ListNguoiDungDaChon.Count();
            searchModel.SetGridPageSize();
            searchModel.ddlDonViBoTinh = _donViModelFactory.PrepareSelectListBoNganhTinh(isAddFirst: true).ToList();
            searchModel.DonViId = _workContext.CurrentDonVi.ID;
            searchModel.ddlDonViQuanHuyen = _donViModelFactory.PrepareSelectListDonViUsingProc(isAddFirst: true, IdDonVi: searchModel.DonViId).ToList();
            return searchModel;
        }

        public NguoiDungListModel PrepareNguoiDungListModel(NguoiDungSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
			//get items
			var items = _itemService.SearchNguoiDungs(Keysearch: searchModel.KeySearch, Tendaydu: searchModel.Tendaydu, Macanbo: searchModel.Macanbo, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ListNguoiDungDaChon: searchModel.ListNguoiDungDaChon, DonViId: searchModel.DonViId, donViBoTinhId:searchModel.DonViBoTinhId, donViHuyenXaId: searchModel.DonViQuanHuyenId, nguoiTaoId: _workContext.CurrentCustomer.ID);
            var model = new NguoiDungListModel();
            model.Data = items.Select(c =>
            {
                var m = c.ToModel<NguoiDungModel>();
                m.MAT_KHAU = null;
                m.MAT_KHAU_KEY = null;
                m.TMP_MAT_KHAU = null;
                //trong truong hop hien thi danh sach nguoi dung o chuc nang vai tro
                if (searchModel.idVaiTro > 0)
                {
                    m.IsDaChon = _vaiTroNguoiDungService.KiemTraDaChon(searchModel.idVaiTro, m.ID);
                }
                //Hiển thị danh sách người dùng ở chức năng đơn vị
                if (searchModel.DonViId > 0)
                {
                    m.IsDaChon = _nguoiDungDonViService.KiemTraDaChon(m.ID, searchModel.DonViId);
                }
                m.Ten_TRANG_THAI = _nhanHienThiService.GetGiaTriEnum(c.nguoiDungStatusID);
                var _NguoiDung = _workContext.CurrentCustomer;
                return m;
            }).ToList();
            model.Total = items.TotalCount;
            return model;
        }
        public NguoiDungListModel PrepareDuyetNguoiDungListModel(NguoiDungSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //get items
            var items = _itemService.SearchDuyetNguoiDungs(Keysearch: searchModel.KeySearch, Tendaydu: searchModel.Tendaydu, Macanbo: searchModel.Macanbo, TrangThaiId: searchModel.TrangThaiId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ListNguoiDungDaChon: searchModel.ListNguoiDungDaChon, DonViId: searchModel.DonViId, donViBoTinhId: searchModel.DonViBoTinhId, donViHuyenXaId: searchModel.DonViQuanHuyenId); 
            var model = new NguoiDungListModel();
            model.Data = items.Select(c =>
            {
                var m = c.ToModel<NguoiDungModel>();
                //trong truong hop hien thi danh sach nguoi dung o chuc nang vai tro
                if (searchModel.idVaiTro > 0)
                {
                    m.IsDaChon = _vaiTroNguoiDungService.KiemTraDaChon(searchModel.idVaiTro, m.ID);
                }
                //Hiển thị danh sách người dùng ở chức năng đơn vị
                if (searchModel.DonViId > 0)
                {
                    m.IsDaChon = _nguoiDungDonViService.KiemTraDaChon(m.ID, searchModel.DonViId);
                }
                m.Ten_TRANG_THAI = _nhanHienThiService.GetGiaTriEnum(c.nguoiDungStatusID);
                var _NguoiDung = _workContext.CurrentCustomer;
                return m;
            }).ToList();
            model.Total = items.TotalCount;
            return model;
        }
        public NguoiDungModel PrepareNguoiDungModel(NguoiDungModel model, NguoiDung item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity model ??
                model = item.ToModel<NguoiDungModel>();
                //lay thong tin vai tro da dc gan cho nguoi dung nay
                model.SelectedVaiTro = _vaiTroNguoiDungService.GetMapByNguoiDungId(model.ID).Select(c => Convert.ToInt32(c.VAI_TRO_ID)).ToList();
            }
            int[] trangThaiNhapId = { (int)GS.Core.Domain.HeThong.NguoiDungStatusID.Delete };
            model.LIST_TRANG_THAI = model.nguoiDungStatusID.ToSelectList(valuesToExclude: trangThaiNhapId);
            model.Ten_TRANG_THAI = _nhanHienThiService.GetGiaTriEnum(model.nguoiDungStatusID);

            var listVaiTro = _vaiTroService.GetAllVaiTros();
            model.ListVaiTroIds = listVaiTro.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN,
                Selected = model.SelectedVaiTro.Contains(Convert.ToInt32(c.ID))
            }).ToList();
            #region Them nguoi dung dc chon vao ListDonViModel
            var listDonVi = _DonViService.GetAllDonViByNguoiDungId(model.ID);
            if (model == null)
            {
                listDonVi = _DonViService.GetAllDonVis();
            }
            var nddv = _nguoiDungDonViService.GetMapByNguoiDungId(model.ID);
            if(nddv!=null)
            {
                model.lstDonVi = nddv.Select(c => Convert.ToInt32(c.DON_VI_ID)).ToList();
                var ListDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(model.ID).Select(m => m.donvi).ToList();
                foreach (var DonVi in ListDonVi)
                {
                    DonViModel DonViModel = new DonViModel();
                    DonViModel = DonVi.ToModel<DonViModel>();
                    model.ListDonViModel.Add(DonViModel);
                }
            }
            
            
            #endregion
            return model;
        }
        public NguoiDungModel PrepareNguoiDungModelDuToanCap2(NguoiDungModel model, NguoiDung item, bool excludeProperties = false)
        {
            if (item != null)
            { 
                //fill in model values from the entity model ??
                model = item.ToModel<NguoiDungModel>();
                //lay thong tin vai tro da dc gan cho nguoi dung nay
                model.SelectedVaiTro = _vaiTroNguoiDungService.GetMapByNguoiDungId(model.ID).Select(c => Convert.ToInt32(c.VAI_TRO_ID)).ToList();
            }
            int[] trangThaiNhapId = { (int)GS.Core.Domain.HeThong.NguoiDungStatusID.Delete };
            model.LIST_TRANG_THAI = model.nguoiDungStatusID.ToSelectList(valuesToExclude: trangThaiNhapId);
            model.Ten_TRANG_THAI = _nhanHienThiService.GetGiaTriEnum(model.nguoiDungStatusID);

            NguoiDung nguoiDungModel = new NguoiDung();
            nguoiDungModel = _workContext.CurrentCustomer;

            IList<VaiTroNguoiDungMapping> vaiTroNguoiDungMappings = _vaiTroNguoiDungService.GetMapByNguoiDungId(nguoiDungModel.ID);
            List<int> vaiTroID = new List<int>();

            foreach (var vaiTroNguoiDungId in vaiTroNguoiDungMappings)
            {
                vaiTroID.Add(Convert.ToInt32(vaiTroNguoiDungId.VAI_TRO_ID));
            }
            //if (vaiTroID.Contains(357) || vaiTroID.Contains(396))
            //{
                //int[] tmp = vaiTroID.ToArray();
                var vaitros = _cauHinhService.GetCauHinh("cauhinh.danhsachvaitro").GIA_TRI.toEntities<int>();
                //IList<int> a = new List<int>() { 477, 478, 479 };
                var listVaiTroThuocQuyen = _vaiTroService.GetVaiTros(vaitros);
                if (item != null)
                {
                    foreach(var i in item.VaiTro)
                    {
                        if (!listVaiTroThuocQuyen.Contains(i))
                        {
                            listVaiTroThuocQuyen.Add(i);
                        }
                    }
                }
                model.ListVaiTroIds = listVaiTroThuocQuyen.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN,
                    Selected = model.SelectedVaiTro.Contains(Convert.ToInt32(c.ID))
                }).ToList();
            //}
            #region Them nguoi dung dc chon vao ListDonViModel
            var listDonVi = _DonViService.GetAllDonViByNguoiDungId(model.ID);
            if (model == null)
            {
                listDonVi = _DonViService.GetAllDonVis();
            }
            var nddv = _nguoiDungDonViService.GetMapByNguoiDungId(model.ID);
            if (nddv != null)
            {
                model.lstDonVi = nddv.Select(c => Convert.ToInt32(c.DON_VI_ID)).ToList();
                var ListDonVi = _nguoiDungDonViService.GetMapByNguoiDungId(model.ID).Select(m => m.donvi).ToList();
                foreach (var DonVi in ListDonVi)
                {
                    DonViModel DonViModel = new DonViModel();
                    DonViModel = DonVi.ToModel<DonViModel>();
                    model.ListDonViModel.Add(DonViModel);
                }
            }


            #endregion
            return model;
        }

        public void PrepareNguoiDung(NguoiDungModel model, Core.Domain.HeThong.NguoiDung item)
        {
            item.TEN_DANG_NHAP = model.TEN_DANG_NHAP;
            item.TEN_DAY_DU = model.TEN_DAY_DU;
            item.MA_CAN_BO = model.MA_CAN_BO;
            item.EMAIL = model.EMAIL;
            item.NGAY_TAO = model.NGAY_TAO;
            //item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.GHI_CHU = model.GHI_CHU;
            item.IS_QUAN_TRI = model.IS_QUAN_TRI;
            item.UNG_DUNG = model.UNG_DUNG;
            item.NGUOI_TAO = model.NGUOI_TAO;
            item.MOBILE = model.MOBILE;
        }
        public bool CheckTenDangNhap(string TEN_DANG_NHAP, decimal id = 0)
        {
            return !_itemService.KiemTraTrungTen(TEN_DANG_NHAP, id);
        }
        public bool CheckMaCanbo(string MA_CAN_BO, decimal id = 0)
        {
            return !_itemService.KiemTraTrungMa(MA_CAN_BO, id);
        }

        public bool CheckResetPassword(string MAT_KHAU_CU, bool isResetmk = true)
        {
            if (MAT_KHAU_CU == null)
            {
                if (isResetmk == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public ResultAction DuyetTaiKhoan(decimal Id, NguoiDung nguoiDung = null)
        {
            if (nguoiDung == null)
            {
                nguoiDung = _itemService.GetNguoiDungById(Id);
            }


            nguoiDung.TRANG_THAI_ID = (int)GS.Core.Domain.HeThong.NguoiDungStatusID.ok;
            //nguoiDung.TMP_MAT_KHAU = null;
            _itemService.UpdateNguoiDung(nguoiDung);
            return new ResultAction(true, "Duyệt thành công");
        }

        public ResultAction HuyDuyetTaiKhoan(decimal Id, NguoiDung nguoiDung = null)
        {
            if (nguoiDung == null)
            {
                nguoiDung = _itemService.GetNguoiDungById(Id);
            }


            nguoiDung.TRANG_THAI_ID = (int)GS.Core.Domain.HeThong.NguoiDungStatusID.Lock;
            _itemService.UpdateNguoiDung(nguoiDung);
            return new ResultAction(true, "Hủy duyệt thành công");
        }
    }
   
    #endregion
}

