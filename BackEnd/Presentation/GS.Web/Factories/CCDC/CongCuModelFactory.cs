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
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;
using DevExpress.Charts.Model;

namespace GS.Web.Factories.CCDC
{
    public class CongCuModelFactory : ICongCuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ICongCuService _itemService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IDonViService _donViService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly IChoThueService _choThueService;
        private readonly ISuaChuaBaoDuongService _suaChuaBaoDuongService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        private readonly IDoiTacService _doiTacService;
        private readonly IGiamHongmatService _giamHongmatService;
        private readonly IHoatDongService _hoatdongService;
        private readonly IKiemKeService _kiemKeService;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly IKiemKeHoiDongService _kiemKeHoiDongService;

        #endregion

        #region Ctor

        public CongCuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ICongCuService itemService,
            INhomCongCuService nhomCongCuService,
            IDonViService donViService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IXuatNhapService xuatNhapService,
            IChoThueService choThueService,
            ISuaChuaBaoDuongService suaChuaBaoDuongService,
            INhomCongCuModelFactory nhomCongCuModelFactory,
            IDonViBoPhanService donViBoPhanService,
            IDoiTacService doiTacService,
            IGiamHongmatService giamHongmatService,
            IHoatDongService hoatdongService,
            IKiemKeService kiemKeService,
            IKiemKeCongCuService kiemKeCongCuService,
            IKiemKeHoiDongService kiemKeHoiDongService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhomCongCuService = nhomCongCuService;
            this._donViService = donViService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._xuatNhapService = xuatNhapService;
            this._choThueService = choThueService;
            this._suaChuaBaoDuongService = suaChuaBaoDuongService;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
            this._donViBoPhanService = donViBoPhanService;
            this._doiTacService = doiTacService;
            this._giamHongmatService = giamHongmatService;
            this._hoatdongService = hoatdongService;
            this._kiemKeService = kiemKeService;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._kiemKeHoiDongService = kiemKeHoiDongService;
        }
        #endregion

        #region CongCu
        public CongCuSearchModel PrepareCongCuSearchModel(CongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ListLoaiCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(donViId: _workContext.CurrentDonVi.ID, isAddFirst: true);
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public CongCuListModel PrepareCongCuListModel(CongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var xuatnhap = _xuatNhapService.GetXuatNhapByGuid(searchModel.XuatNhapGuid);
            //get items
            var items = _nhapXuatCongCuService.SearchNhapXuatCongCus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, NhapXuatId: xuatnhap != null ? xuatnhap.ID : 0);

            //prepare list model
            var model = new CongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareListCongCuModelFromNXCC(c, new CongCuModel())),
                Total = items.TotalCount
            };
            return model;
        }
        public XuatNhapListModel PrepareXuatNhapListModel(CongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _xuatNhapService.SearchXuatNhaps(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, LoaiCongCu: searchModel.LoaiCongCu, NgayNhapTu: searchModel.NgayNhapTu, NgayNhapDen: searchModel.NgayNhapDen, SoChungTu: searchModel.SoChungTu);

            //prepare list model
            var model = new XuatNhapListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareViewXuatNhapModel(c, new XuatNhapModel())),
                Total = items.TotalCount
            };
            return model;
        }
        public XuatNhapModel PrepareViewXuatNhapModel(XuatNhap item, XuatNhapModel model)
        {
            model = item.ToModel<XuatNhapModel>();
            model.IsEdit = true;
            var map = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID).OrderBy(c => c.ID).FirstOrDefault();
            if (map != null)
            {
                model.NgayXuatNhapText = item.NGAY_XUAT_NHAP.toDateVNString();
                model.SoCongCuText = _xuatNhapService.GetCountCongCuByXuatNhapId(item.ID);
                model.GUID = item.GUID;
                model.ChungTuNgayText = map.CHUNG_TU_NGAY.toDateVNString();
                model.ChungTuSoText = map.CHUNG_TU_SO;
            }
            // kiểm tra xem nếu là phân bổ hết ngay khi nhập lo và đơn vị phân bổ chưa sử dụng thì ms cho sửa
            var xk = _xuatNhapService.GetXuatNhaps(isXuat: true, fromId: model.ID);
            if (xk.Count() > 0)
            {
                var xkf = xk.Where(c => c.IS_PHAN_BO_FIRST == true);
                if (xkf.Count() == 0)
                {
                    model.IsEdit = false;
                }
                else
                {
                    var nk = _xuatNhapService.GetXuatNhaps(isXuat: false, fromId: xkf.FirstOrDefault().ID, LoaiXuatNhap: (int)enumLoaiXuatNhap.PHAN_BO).FirstOrDefault();
                    if (nk != null)
                    {
                        // sau khi xuất kho sang bộ phận mà chưa sử dụng thì có thể sửa
                        if (_xuatNhapService.GetXuatNhapForDieuChuyen(FromXuatNhap: nk.ID) != null) { model.IsEdit = false; };
                        if (_choThueService.GetChoThues(XuatNhapId: nk.ID).Count() > 0) { model.IsEdit = false; };
                        if (_suaChuaBaoDuongService.GetSuaChuaBaoDuongs(XuatNhapId: nk.ID).Count() > 0) { model.IsEdit = false; };
                    }


                }
            }
            return model;
        }
        public CongCuModel PrepareListCongCuModelFromNXCC(NhapXuatCongCu nxcc, CongCuModel model)
        {

            model = nxcc.CongCu.ToModel<CongCuModel>();
            model.SO_LUONG = nxcc.SO_LUONG;
            model.ChungTuNgayText = nxcc.CHUNG_TU_NGAY.toDateVNString();
            model.NhomCongCuText = _nhomCongCuService.GetNhomCongCuById((decimal)nxcc.CongCu.NHOM_CONG_CU_ID).TEN;
            model.DonViText = _donViService.GetDonViById(nxcc.CongCu.DON_VI_ID).TEN;
            model.DonGiaText = nxcc.DON_GIA.ToVNStringNumber();
            model.ThanhTienText = nxcc.THANH_TIEN.ToVNStringNumber();
            return model;
        }
        public CongCuModel PrepareCongCuModel(CongCuModel model, CongCu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<CongCuModel>();
            }
            //more

            return model;
        }

        public void PrepareCongCu(CongCu item, LoCongCuModel LCCModel)
        {
            item.NHOM_CONG_CU_ID = LCCModel.NHOM_CONG_CU_ID;
            item.TEN = LCCModel.TEN;
        }

        public void PrepareLoCongCu(NhapXuatCongCu map, LoCongCuModel LCCModel)
        {
            LCCModel.ID = map.CONG_CU_ID;
            LCCModel.GUID = map.CongCu.GUID;
            LCCModel.NHOM_CONG_CU_ID = map.CongCu.NHOM_CONG_CU_ID;
            LCCModel.MapId = map.ID;
        }

        public void PrepareUpdateCongCu(CongCu item, CongCuModel model)
        {
            item.NHOM_CONG_CU_ID = model.NHOM_CONG_CU_ID;
            //item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            //item.CHUNG_TU_SO = model.CHUNG_TU_SO;
            //item.CHUNG_TU_NGAY = model.CHUNG_TU_NGAY;
            item.GHI_CHU = model.GHI_CHU;
            item.TEN = model.TEN;
            //item.DON_GIA = model.DON_GIA;
            //item.THANH_TIEN = model.SO_LUONG * model.DON_GIA;
        }

        public virtual List<SelectListItem> PrepareDDLCongCuByNhom(Decimal nhomId)
        {
            var item = _itemService.GetCongCus(NhomCongCuId: nhomId);
            var ddl = item.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString()
            }).ToList();
            ddl.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "-- Chọn công cụ --"
            });
            return ddl;
        }
        public virtual List<SelectListItem> PrepareDDLCongCuDonVi(Decimal donviId)
        {
            var item = _itemService.GetCongCus(DonViId: donviId).OrderBy(c => c.TEN);
            var ddl = item.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString()
            }).ToList();
            ddl.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "-- Chọn công cụ --"
            });
            return ddl;
        }


        public virtual MessageReturn importCCDC(IMP_CCDCModel model)
        {
            MessageReturn rs = new MessageReturn();
            #region Check
            CongCu congCu = new CongCu();
            XuatNhap nhapKho = new XuatNhap();
            NhapXuatCongCu nhapXuatCongCu = new NhapXuatCongCu();
            DonVi donVi = new DonVi();
            NhomCongCu nhomCongCu = new NhomCongCu();
            if (string.IsNullOrEmpty(model.CONG_CU.DON_VI_MA))
            {
                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = "DON_VI_MA IS NULL";
                return rs;
            }
            else
            {
                donVi = _donViService.GetDonViByMa(model.CONG_CU.DON_VI_MA);
                if (donVi == null)
                {
                    rs.Code = MessageReturn.NOT_FOUND_CODE;
                    rs.Message = $"DON_VI_MA '{model.CONG_CU.DON_VI_MA}' NOT FOUND";
                    return rs;
                }
            }

            if (model.CONG_CU.NGAY_XUAT_NHAP == null)
            {
                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = "NGAY_XUAT_NHAP IS NULL";
                return rs;
            }
            if (model.CONG_CU.LY_DO_ID == null)
            {
                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = "LY_DO_ID IS NULL";
                return rs;
            }


            #region công cụ
            if (model.CONG_CU == null)
            {
                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = "CONG_CU IS NULL";
                return rs;
            }
            else
            {
                if (string.IsNullOrEmpty(model.CONG_CU.MA))
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - MA IS NULL";
                    return rs;
                }
                else if (_itemService.GetCongCuByMaDB(model.CONG_CU.MA) != null)
                {
                    rs.Code = MessageReturn.SUCCESS_CODE;
                    rs.Message = "Done";
                    return rs;
                }
                if (string.IsNullOrEmpty(model.CONG_CU.TEN))
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - TEN IS NULL";
                    return rs;
                }
                #region nhóm ccdc
                if (string.IsNullOrEmpty(model.CONG_CU.NHOM_CCDC_MA))
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - NHOM_CCDC_MA IS NULL";
                    return rs;
                }
                else
                {
                    nhomCongCu = _nhomCongCuService.GetReadOnlyNhomCongCu(donViId: donVi.ID, ma: model.CONG_CU.NHOM_CCDC_MA);
                    if (nhomCongCu == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = $"CONG_CU - NHOM_CCDC_MA '{model.CONG_CU.NHOM_CCDC_MA}' NOT FOUND";
                        return rs;
                    }

                }
                #endregion
                #region
                //if (model.CONG_CU.NHOM_CCDC == null)
                //{
                //    rs.Code = MessageReturn.ERROR_CODE;
                //    rs.Message = "CONG_CU - NHOM_CCDC IS NULL";
                //    return rs;
                //}
                //else
                //{
                //    nhomCongCu = _nhomCongCuService.GetReadOnlyNhomCongCu(donVi.ID, model.CONG_CU.NHOM_CCDC.TEN, model.CONG_CU.NHOM_CCDC.MA);
                //    if (nhomCongCu == null)
                //    {
                //        if (string.IsNullOrEmpty(model.CONG_CU.NHOM_CCDC.MA))
                //        {
                //            rs.Code = MessageReturn.ERROR_CODE;
                //            rs.Message = "CONG_CU - NHOM_CCDC - MA IS NULL";
                //            return rs;
                //        }
                //        if (string.IsNullOrEmpty(model.CONG_CU.NHOM_CCDC.TEN))
                //        {
                //            rs.Code = MessageReturn.ERROR_CODE;
                //            rs.Message = "CONG_CU - NHOM_CCDC - TEN IS NULL";
                //            return rs;
                //        }
                //        if (model.CONG_CU.NHOM_CCDC.THOI_HAN_PHAN_BO == null)
                //        {
                //            rs.Code = MessageReturn.ERROR_CODE;
                //            rs.Message = "CONG_CU - NHOM_CCDC - THOI_HAN_PHAN_BO IS NULL";
                //            return rs;
                //        }
                //        if (string.IsNullOrEmpty(model.CONG_CU.NHOM_CCDC.DON_VI_TINH))
                //        {
                //            rs.Code = MessageReturn.ERROR_CODE;
                //            rs.Message = "CONG_CU - NHOM_CCDC - DON_VI_TINH IS NULL";
                //            return rs;
                //        }
                //    }

                //}
                #endregion
                if (model.CONG_CU.TRANG_THAI_ID == null)
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - TRANH_THAI_ID IS NULL";
                    return rs;
                }

                if (model.CONG_CU.SO_LUONG.GetValueOrDefault(0) == 0)
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - SO_LUONG IS NULL";
                    return rs;
                }

                if (model.CONG_CU.DON_GIA.GetValueOrDefault(0) == 0)
                {
                    rs.Code = MessageReturn.ERROR_CODE;
                    rs.Message = "CONG_CU - DON_GIA IS NULL";
                    return rs;
                }

                congCu = new CongCu()
                {
                    TEN = model.CONG_CU.TEN,
                    DON_VI_ID = donVi.ID,
                };
            }


            #endregion
            #region phân bổ
            if (model.PHAN_BO != null && model.PHAN_BO.Count > 0)
            {
                foreach (IMP_PhanBo phanBo in model.PHAN_BO)
                {
                    if (phanBo.NGAY_TANG == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "PHAN_BO - NGAY_TANG IS NULL";
                        return rs;
                    }
                    if (phanBo.TRANG_THAI.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "PHAN_BO - TRANG_THAI IS NULL";
                        return rs;
                    }
                    #region đơn vị phân bổ
                    if (string.IsNullOrEmpty(phanBo.DON_VI_PHAN_BO_MA))
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "PHAN_BO - DON_VI_PHAN_BO_MA IS NULL";
                        return rs;
                    }
                    else
                    {

                        if (!_donViBoPhanService.CheckMaDonViBoPhan(donVi.ID, 0, phanBo.DON_VI_PHAN_BO_MA))
                        {
                            rs.Code = MessageReturn.ERROR_CODE;
                            rs.Message = $"PHAN_BO - DON_VI_PHAN_BO_MA '{phanBo.DON_VI_PHAN_BO_MA}' NOT FOUND";
                            return rs;
                        }
                    }
                    #endregion
                    #region
                    //if (phanBo.DON_VI_PHAN_BO == null)
                    //{
                    //    rs.Code = MessageReturn.ERROR_CODE;
                    //    rs.Message = "PHAN_BO - DON_VI_PHAN_BO IS NULL";
                    //    return rs;
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(phanBo.DON_VI_PHAN_BO.MA))
                    //    {
                    //        rs.Code = MessageReturn.ERROR_CODE;
                    //        rs.Message = "PHAN_BO - DON_VI_PHAN_BO - MA IS NULL";
                    //        return rs;
                    //    }
                    //    else
                    //    {
                    //        if (!_donViBoPhanService.CheckMaDonViBoPhan(donVi.ID, 0, phanBo.DON_VI_PHAN_BO.MA))
                    //        {
                    //            if (string.IsNullOrEmpty(phanBo.DON_VI_PHAN_BO.TEN))
                    //            {
                    //                rs.Code = MessageReturn.ERROR_CODE;
                    //                rs.Message = "PHAN_BO - DON_VI_PHAN_BO - TEN IS NULL";
                    //                return rs;
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                }
            }
            #endregion
            #region sửa chữa
            if (model.THONG_TIN_KHAC != null && model.THONG_TIN_KHAC.SUA_CHUA != null && model.THONG_TIN_KHAC.SUA_CHUA.Count > 0)
            {
                foreach (IMP_SuaChua suaChua in model.THONG_TIN_KHAC.SUA_CHUA)
                {
                    if (suaChua.NGAY_SUA_CHUA_TU == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "SUA_CHUA - NGAY_SUA_CHUA_TU IS NULL";
                        return rs;
                    }
                    if (suaChua.NGAY_SUA_CHUA_DEN == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "SUA_CHUA - NGAY_SUA_CHUA_DEN IS NULL";
                        return rs;
                    }
                    if (suaChua.SO_LUONG.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "SUA_CHUA - SO_LUONG IS NULL";
                        return rs;
                    }
                    if (suaChua.GIA_TRI.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "SUA_CHUA - GIA_TRI IS NULL";
                        return rs;
                    }
                }
            }
            #endregion
            #region cho thuê
            if (model.THONG_TIN_KHAC != null && model.THONG_TIN_KHAC.CHO_THUE != null && model.THONG_TIN_KHAC.CHO_THUE.Count > 0)
            {
                foreach (IMP_ChoThue choThue in model.THONG_TIN_KHAC.CHO_THUE)
                {
                    if (choThue.NGAY_CHO_THUE_TU == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "CHO_THUE - NGAY_CHO_THUE_TU IS NULL";
                        return rs;
                    }
                    if (choThue.NGAY_CHO_THUE_DEN == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "CHO_THUE - NGAY_CHO_THUE_DEN IS NULL";
                        return rs;
                    }
                    if (choThue.SO_LUONG.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "CHO_THUE - SO_LUONG IS NULL";
                        return rs;
                    }
                    if (choThue.GIA_TRI.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "CHO_THUE - GIA_TRI IS NULL";
                        return rs;
                    }
                }
            }
            #endregion
            #region luân chuyển
            if (model.LUAN_CHUYEN != null && model.LUAN_CHUYEN.Count > 0)
            {
                foreach (IMP_LuanChuyen luanChuyen in model.LUAN_CHUYEN)
                {
                    if (luanChuyen.NGAY_LUAN_CHUYEN == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "LUAN_CHUYEN - NGAY_LUAN_CHUYEN IS NULL";
                        return rs;
                    }
                    if (luanChuyen.SO_LUONG.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "LUAN_CHUYEN - SO_LUONG IS NULL";
                        return rs;
                    }
                    if (string.IsNullOrEmpty(luanChuyen.DON_VI_NHAN_DIEU_CHUYEN_MA))
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "LUAN_CHUYEN - DON_VI_NHAN_DIEU_CHUYEN_MA IS NULL";
                        return rs;
                    }
                }
            }
            #endregion
            #region giảm công cụ
            if (model.GIAM_CCDC != null && model.GIAM_CCDC.Count > 0)
            {
                foreach (IMP_GiamCC giamCC in model.GIAM_CCDC)
                {
                    if (giamCC.NGAY_DIEU_CHUYEN == null)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "GIAM_CCDC - NGAY_DIEU_CHUYEN IS NULL";
                        return rs;
                    }
                    if (string.IsNullOrEmpty(giamCC.DON_VI_TIEP_NHAN) && giamCC.LY_DO_GIAM_ID == 4)
                    {
                        rs.Code = MessageReturn.ERROR_CODE;
                        rs.Message = "GIAM_CCDC - DON_VI_TIEP_NHAN IS NULL";
                        return rs;
                    }
                }
            }
            #endregion
            #endregion

            #region prepare
            try
            {
                decimal? sttnhapkho = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                nhapKho = new XuatNhap()
                {
                    TEN = model.CONG_CU.TEN,
                    DON_VI_ID = donVi.ID,
                    IS_XUAT = false,
                    NGAY_XUAT_NHAP = model.CONG_CU.NGAY_XUAT_NHAP,
                    MA = donVi.MA + "-" + sttnhapkho,
                    MA_LIEN_QUAN = donVi.MA + "-" + sttnhapkho,
                    GHI_CHU = model.CONG_CU.GHI_CHU,
                    MUC_DICH_XUAT_NHAP_ID = model.CONG_CU.LY_DO_ID,
                    LOAI_XUAT_NHAP_ID = (decimal)enumLoaiXuatNhap.NHAP_KHO
                };
                _xuatNhapService.InsertXuatNhap(nhapKho);
                #region nhóm công cụ

                //if (nhomCongCu == null)
                //{
                //    nhomCongCu = new NhomCongCu()
                //    {
                //        MA = model.CONG_CU.NHOM_CCDC.MA,
                //        DON_VI_ID = donVi.ID,
                //        TEN = model.CONG_CU.NHOM_CCDC.TEN,
                //        THOI_HAN_PHAN_BO = model.CONG_CU.NHOM_CCDC.THOI_HAN_PHAN_BO,
                //        DON_VI_TINH_ID = model.CONG_CU.NHOM_CCDC.DON_VI_TINH,
                //    };
                //    if (!string.IsNullOrEmpty(model.CONG_CU.NHOM_CCDC.TEN_CHA))
                //    {
                //        NhomCongCu nhomCongCuCha = _nhomCongCuService.GetReadOnlyNhomCongCu(donViId: donVi.ID, ma: model.CONG_CU.NHOM_CCDC.MA);
                //        if (nhomCongCuCha != null)
                //            nhomCongCu.PARENT_ID = nhomCongCuCha.ID;
                //    }
                //    _nhomCongCuService.InsertNhomCongCu(nhomCongCu);

                //}
                #endregion
                #region công cụ
                //insert công cụ
                Decimal? sttMaCongCu = _itemService.GetValueIdMax().GetValueOrDefault(1);
                congCu = _itemService.GetCongCu(madb: model.CONG_CU.MA, donViId: donVi.ID);
                if (congCu != null)
                    _itemService.DelelteCongCuAndThongTinLienQuan(congCu.ID, 1);
                congCu = new CongCu()
                {
                    DON_VI_ID = donVi.ID,
                    TEN = model.CONG_CU.TEN,
                    NHOM_CONG_CU_ID = nhomCongCu.ID,
                    MA = donVi.MA + "-" + nhomCongCu.ID + "-" + sttMaCongCu,
                    MA_DB = model.CONG_CU.MA
                };
                _itemService.InsertCongCu(congCu);
                model.CONG_CU.ID = congCu.ID;


                nhapXuatCongCu = new NhapXuatCongCu()
                {
                    NHAP_XUAT_ID = nhapKho.ID,
                    CONG_CU_ID = congCu.ID,
                    CHUNG_TU_SO = model.CONG_CU.CHUNG_TU_SO,
                    CHUNG_TU_NGAY = model.CONG_CU.CHUNG_TU_NGAY,
                    DON_GIA = model.CONG_CU.DON_GIA,
                    SO_LUONG = model.CONG_CU.SO_LUONG,
                    TRANG_THAI_ID = model.CONG_CU.TRANG_THAI_ID,
                    THANH_TIEN = model.CONG_CU.DON_GIA * model.CONG_CU.SO_LUONG,
                };
                _nhapXuatCongCuService.InsertNhapXuatCongCu(nhapXuatCongCu);
                #endregion
                #region phân bổ
                if (model.PHAN_BO != null && model.PHAN_BO.Count() > 0)
                {
                    foreach (IMP_PhanBo phanBo in model.PHAN_BO)
                    {
                        DonViBoPhan donViPhanBo = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, phanBo.DON_VI_PHAN_BO_MA);
                        var sttXuatKho = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                        XuatNhap xuatKho = new XuatNhap()
                        {
                            FROM_XUAT_NHAP_ID = nhapKho.ID,
                            TEN = congCu.TEN,
                            DON_VI_ID = donVi.ID,
                            IS_XUAT = true,
                            NGAY_XUAT_NHAP = phanBo.NGAY_TANG,
                            MA = donVi.MA + "-" + sttXuatKho,
                            MA_LIEN_QUAN = nhapKho.MA_LIEN_QUAN,
                            GHI_CHU = phanBo.GHI_CHU,
                            //MUC_DICH_XUAT_NHAP_ID = phanBo.l,
                            LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO,
                            IS_PHAN_BO_FIRST = true,
                        };

                        _xuatNhapService.InsertXuatNhap(xuatKho);
                        // nhập đơn vị bộ phận
                        var sstNhapDVBP = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                        XuatNhap nhapDonViPB = new XuatNhap()
                        {
                            FROM_XUAT_NHAP_ID = xuatKho.ID,
                            TEN = congCu.TEN,
                            DON_VI_BO_PHAN_ID = donViPhanBo.ID,
                            DON_VI_ID = donVi.ID,
                            IS_XUAT = false,
                            NGAY_XUAT_NHAP = phanBo.NGAY_TANG,
                            MA = donVi.MA + "-" + sstNhapDVBP,
                            MA_LIEN_QUAN = nhapKho.MA_LIEN_QUAN,
                            GHI_CHU = phanBo.GHI_CHU,
                            //MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId,
                            LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO,
                        };
                        _xuatNhapService.InsertXuatNhap(nhapDonViPB);

                        // insert xuất/nhập đơn vị bộ phận
                        // mapping
                        var mappingXuatKho = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = congCu.ID,
                            NHAP_XUAT_ID = xuatKho.ID,
                            SO_LUONG = phanBo.SO_LUONG,
                            DON_GIA = model.CONG_CU.DON_GIA,
                            TRANG_THAI_ID = phanBo.TRANG_THAI,
                            NHAP_KHO_ID = nhapXuatCongCu.ID
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuatKho);
                        var mappingNhapDonVi = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = congCu.ID,
                            NHAP_XUAT_ID = nhapDonViPB.ID,
                            SO_LUONG = phanBo.SO_LUONG,
                            DON_GIA = model.CONG_CU.DON_GIA,
                            TRANG_THAI_ID = phanBo.TRANG_THAI
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingNhapDonVi);
                        phanBo.nhapXuatMap = mappingNhapDonVi;
                    }
                    #region sửa chữa
                    if (model.THONG_TIN_KHAC != null && model.THONG_TIN_KHAC.SUA_CHUA != null && model.THONG_TIN_KHAC.SUA_CHUA.Count() > 0)
                    {

                        foreach (IMP_SuaChua suaChua in model.THONG_TIN_KHAC.SUA_CHUA)
                        {
                            IMP_PhanBo iMP_PhanBo = model.PHAN_BO.Where(c => c.MA_PHAN_BO == suaChua.MA_PHAN_BO).FirstOrDefault();
                            DonViBoPhan donViBoPhan = new DonViBoPhan();
                            if (iMP_PhanBo == null)
                            {
                                rs.Code = MessageReturn.ERROR_CODE;
                                rs.Message = "THONG_TIN_KHAC - SUA_CHUA - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO";
                                return rs;
                            }
                            else
                                donViBoPhan = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, iMP_PhanBo.DON_VI_PHAN_BO_MA);
                            if (donViBoPhan == null)
                                continue;
                            DoiTac khachHang = new DoiTac();
                            if (!string.IsNullOrEmpty(suaChua.MA_DOI_TAC))
                            {
                                khachHang = _doiTacService.GetReadOnlyDoiTac(Ma: suaChua.MA_DOI_TAC, donViBoPhanId: donViBoPhan.ID, loaiDoiTac: (Decimal)enumLOAI_DOI_TAC.KhachHang);
                                if (khachHang == null)
                                {
                                    rs.Code = MessageReturn.ERROR_CODE;
                                    rs.Message = $"THONG_TIN_KHAC - SUA_CHUA - MA_DOI_TAC '{suaChua.MA_DOI_TAC}' NOT FOUND";
                                    return rs;
                                }
                            }
                            SuaChuaBaoDuong item = new SuaChuaBaoDuong()
                            {
                                CAP_QUYET_DINH = suaChua.CAP_QUYET_DINH,
                                CHUNG_TU_NGAY = suaChua.CHUNG_TU_NGAY,
                                CHUNG_TU_SO = suaChua.CHUNG_TU_SO,
                                CONG_CU_ID = congCu.ID,
                                GHI_CHU = suaChua.GHI_CHU,
                                NGAY_QUYET_DINH = suaChua.NGAY_QUYET_DINH,
                                NGAY_SUA_CHUA_FROM = suaChua.NGAY_SUA_CHUA_TU.GetValueOrDefault(DateTime.MinValue),
                                NGAY_SUA_CHUA_TO = suaChua.NGAY_SUA_CHUA_DEN.GetValueOrDefault(DateTime.MinValue),
                                SO_QUYET_DINH = suaChua.SO_QUYET_DINH,
                                GIA_TRI_SUA_CHUA = suaChua.GIA_TRI.GetValueOrDefault(0),
                                SO_LUONG_SUA_CHUA = suaChua.SO_LUONG.GetValueOrDefault(0),
                                NHAP_XUAT_ID = iMP_PhanBo.nhapXuatMap.NHAP_XUAT_ID,
                                KHACH_HANG_ID = khachHang != null ? khachHang.ID : default(decimal?)
                            };
                            _suaChuaBaoDuongService.InsertSuaChuaBaoDuong(item);
                        }
                    }
                    #endregion
                    #region cho thuê
                    if (model.THONG_TIN_KHAC != null && model.THONG_TIN_KHAC.CHO_THUE != null && model.THONG_TIN_KHAC.CHO_THUE.Count > 0)
                    {
                        foreach (IMP_ChoThue choThue in model.THONG_TIN_KHAC.CHO_THUE)
                        {
                            IMP_PhanBo iMP_PhanBo = model.PHAN_BO.Where(c => c.MA_PHAN_BO == choThue.MA_PHAN_BO).FirstOrDefault();
                            DonViBoPhan donViBoPhan = new DonViBoPhan();
                            if (iMP_PhanBo == null)
                            {
                                rs.Code = MessageReturn.ERROR_CODE;
                                rs.Message = "THONG_TIN_KHAC - CHO_THUE - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO";
                                return rs;
                            }
                            else
                                donViBoPhan = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, iMP_PhanBo.DON_VI_PHAN_BO_MA);
                            if (donViBoPhan == null)
                                continue;
                            DoiTac khachHang = new DoiTac();
                            if (!string.IsNullOrEmpty(choThue.MA_DOI_TAC))
                            {
                                khachHang = _doiTacService.GetReadOnlyDoiTac(Ma: choThue.MA_DOI_TAC, donViBoPhanId: donViBoPhan.ID, loaiDoiTac: (Decimal)enumLOAI_DOI_TAC.KhachHang);
                                if (khachHang == null)
                                {
                                    rs.Code = MessageReturn.ERROR_CODE;
                                    rs.Message = $"THONG_TIN_KHAC - SUA_CHUA - MA_DOI_TAC '{choThue.MA_DOI_TAC}' NOT FOUND";
                                    return rs;
                                }
                            }
                            ChoThue item = new ChoThue()
                            {
                                QUYET_DINH_SO = choThue.SO_QUYET_DINH,
                                QUYET_DINH_NGAY = choThue.NGAY_QUYET_DINH,
                                CAP_QUYET_DINH = choThue.CAP_QUYET_DINH,
                                NGAY_CHO_THUE_TO = choThue.NGAY_CHO_THUE_TU,
                                NGAY_CHO_THUE_FROM = choThue.NGAY_CHO_THUE_DEN,
                                SO_TIEN_THU_DUOC = choThue.GIA_TRI,
                                KHACH_HANG_ID = khachHang != null ? khachHang.ID : default(decimal?),
                                HOP_DONG_SO = choThue.HOP_DONG_SO,
                                HOP_DONG_NGAY = choThue.HOP_DONG_NGAY,
                                SO_LUONG = choThue.SO_LUONG,
                                GHI_CHU = choThue.GHI_CHU,
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = iMP_PhanBo.nhapXuatMap.NHAP_XUAT_ID
                            };
                            _choThueService.InsertChoThue(item);
                        }
                    }
                    #endregion
                    #region luân chuyển
                    if (model.LUAN_CHUYEN != null && model.LUAN_CHUYEN.Count > 0)
                    {
                        foreach (IMP_LuanChuyen luanChuyen in model.LUAN_CHUYEN)
                        {
                            DonViBoPhan luanChuyenDVPB = new DonViBoPhan();
                            NhapXuatCongCu nhapXuatCongCuPB = new NhapXuatCongCu();
                            IMP_PhanBo iMP_PhanBo = model.PHAN_BO.Where(c => c.MA_PHAN_BO == luanChuyen.MA_PHAN_BO).FirstOrDefault();
                            if (iMP_PhanBo == null)
                            {
                                rs.Code = MessageReturn.ERROR_CODE;
                                rs.Message = "LUAN_CHUYEN - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO";
                                return rs;
                            }
                            else
                            {
                                nhapXuatCongCuPB = iMP_PhanBo.nhapXuatMap;
                                luanChuyenDVPB = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, iMP_PhanBo.DON_VI_PHAN_BO_MA);
                            }

                            if (luanChuyenDVPB == null)
                                continue;
                            XuatNhap xuatNhapPB = _xuatNhapService.GetXuatNhapById(iMP_PhanBo.nhapXuatMap.NHAP_XUAT_ID);
                            decimal? sttxuat = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                            XuatNhap xuat = new XuatNhap()
                            {
                                FROM_XUAT_NHAP_ID = xuatNhapPB.ID,
                                DON_VI_ID = donVi.ID,
                                MA = donVi.MA + "-" + sttxuat,
                                MA_LIEN_QUAN = xuatNhapPB.MA_LIEN_QUAN,
                                IS_XUAT = true,
                                DON_VI_BO_PHAN_ID = luanChuyenDVPB.ID,
                                LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.LUAN_CHUYEN,
                            };
                            _xuatNhapService.InsertXuatNhap(xuat);
                            // mapping xuat
                            var mapxuat = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = xuat.ID,
                                SO_LUONG = luanChuyen.SO_LUONG,
                                DON_GIA = iMP_PhanBo.nhapXuatMap.DON_GIA,
                                TRANG_THAI_ID = iMP_PhanBo.nhapXuatMap.TRANG_THAI_ID,
                                NHAP_KHO_ID = iMP_PhanBo.nhapXuatMap.ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mapxuat);
                            // nhap
                            var sttnhap = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                            DonViBoPhan donViNhanLuanChuyen = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, luanChuyen.DON_VI_NHAN_DIEU_CHUYEN_MA);
                            var nhap = new XuatNhap
                            {
                                FROM_XUAT_NHAP_ID = xuat.ID,
                                DON_VI_ID = donVi.ID,
                                MA = donVi.MA + "-" + sttnhap,
                                MA_LIEN_QUAN = xuatNhapPB.MA_LIEN_QUAN,
                                IS_XUAT = false,
                                LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.LUAN_CHUYEN,
                                DON_VI_BO_PHAN_ID = donViNhanLuanChuyen != null ? donViNhanLuanChuyen.ID : default(decimal?),
                                GHI_CHU = luanChuyen.GHI_CHU,
                                QUYET_DINH_NGAY = luanChuyen.NGAY_QUYET_DINH,
                                QUYET_DINH_SO = luanChuyen.SO_QUYET_DINH,
                                NGAY_XUAT_NHAP = luanChuyen.NGAY_LUAN_CHUYEN
                            };
                            _xuatNhapService.InsertXuatNhap(nhap);
                            // mapping nhap
                            var mapnhap = new NhapXuatCongCu()
                            {
                                SO_LUONG = luanChuyen.SO_LUONG,
                                NHAP_XUAT_ID = nhap.ID,
                                CONG_CU_ID = congCu.ID,
                                DON_GIA = iMP_PhanBo.nhapXuatMap.DON_GIA,
                                TRANG_THAI_ID = iMP_PhanBo.nhapXuatMap.TRANG_THAI_ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mapnhap);
                        }
                    }
                    #endregion
                    #region giảm ccdc
                    if (model.GIAM_CCDC != null && model.GIAM_CCDC.Count > 0)
                    {
                        foreach (IMP_GiamCC giamCC in model.GIAM_CCDC)
                        {
                            var sstXuat = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                            DonViBoPhan nhapXuatPB = new DonViBoPhan();
                            IMP_PhanBo iMP_PhanBo = model.PHAN_BO.Where(c => c.MA_PHAN_BO == giamCC.MA_PHAN_BO).FirstOrDefault();
                            if (iMP_PhanBo == null)
                            {
                                rs.Code = MessageReturn.ERROR_CODE;
                                rs.Message = "GIAM_CCDC - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO";
                                return rs;
                            }
                            else
                                nhapXuatPB = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donVi.ID, iMP_PhanBo.DON_VI_PHAN_BO_MA);
                            if (nhapXuatPB == null)
                                continue;
                            XuatNhap xuatNhapPB = _xuatNhapService.GetXuatNhapById(iMP_PhanBo.nhapXuatMap.NHAP_XUAT_ID);
                            var madonvifrom = _donViService.GetDonViById(xuatNhapPB.DON_VI_ID.Value).MA;
                            var xuat = new XuatNhap
                            {
                                DON_VI_BO_PHAN_ID = xuatNhapPB.DON_VI_BO_PHAN_ID,
                                DON_VI_ID = xuatNhapPB.DON_VI_ID,
                                FROM_XUAT_NHAP_ID = iMP_PhanBo.nhapXuatMap.NHAP_XUAT_ID,
                                GHI_CHU = giamCC.GHI_CHU,
                                IS_XUAT = true,
                                MA_LIEN_QUAN = xuatNhapPB.MA_LIEN_QUAN,
                                QUYET_DINH_NGAY = giamCC.NGAY_QUYET_DINH,
                                QUYET_DINH_SO = giamCC.SO_QUYET_DINH,
                                MA = madonvifrom + "-" + sstXuat,
                                NGAY_XUAT_NHAP = giamCC.NGAY_DIEU_CHUYEN,
                                LOAI_XUAT_NHAP_ID = giamCC.LY_DO_GIAM_ID
                            };
                            _xuatNhapService.InsertXuatNhap(xuat);
                            // mapping xuất
                            var mapXuat = new NhapXuatCongCu
                            {
                                CHUNG_TU_NGAY = giamCC.CHUNG_TU_NGAY,
                                CHUNG_TU_SO = giamCC.CHUNG_TU_SO,
                                CONG_CU_ID = congCu.ID,
                                DON_GIA = iMP_PhanBo.nhapXuatMap.DON_GIA,
                                NHAP_XUAT_ID = xuat.ID,
                                SO_LUONG = giamCC.SO_LUONG,
                                TRANG_THAI_ID = iMP_PhanBo.nhapXuatMap.TRANG_THAI_ID,
                                NHAP_KHO_ID = iMP_PhanBo.nhapXuatMap.ID,
                                THANH_TIEN = iMP_PhanBo.nhapXuatMap.DON_GIA * giamCC.SO_LUONG
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mapXuat);
                            if (giamCC.LY_DO_GIAM_ID == (int)enumLoaiXuatNhap.DIEU_CHUYEN)
                            {
                                //thêm mới nhóm và công cụ cho đơn vị ngoài
                                var nhomcc = new NhomCongCu();
                                DonVi donViDieuChuyen = _donViService.GetReadOnlyDonViByMa(giamCC.DON_VI_TIEP_NHAN);
                                if (donViDieuChuyen == null)
                                {
                                    rs.Code = MessageReturn.ERROR_CODE;
                                    rs.Message = $"GIAM_CCDC - DON_VI_TIEP_NHAN '{giamCC.DON_VI_TIEP_NHAN}' NOT FOUND";
                                    return rs;
                                }
                                var cc = new CongCu();
                                if (nhomCongCu != null)
                                {
                                    // nhóm công cụ
                                    nhomcc.DON_VI_TINH_ID = nhomCongCu.DON_VI_TINH_ID;
                                    nhomcc.TEN = nhomCongCu.TEN;
                                    nhomcc.THOI_HAN_PHAN_BO = nhomCongCu.THOI_HAN_PHAN_BO;
                                    _nhomCongCuService.InsertNhomCongCu(nhomcc);
                                    //công cụ
                                    decimal? sttMaCongCu2 = _itemService.GetValueIdMax().GetValueOrDefault(1);
                                    cc.TEN = congCu.TEN;
                                    cc.DON_VI_ID = donViDieuChuyen.ID;
                                    cc.NHOM_CONG_CU_ID = nhomcc.ID;
                                    cc.MA = donVi.MA + "-" + cc.NHOM_CONG_CU_ID + "-" + sttMaCongCu2;
                                    _itemService.InsertCongCu(cc);
                                }
                                // nhập
                                var sstNhap = _xuatNhapService.GetValueIdMax().GetValueOrDefault(1);
                                var nhap = new XuatNhap
                                {
                                    DON_VI_ID = donViDieuChuyen.ID,
                                    FROM_XUAT_NHAP_ID = xuat.ID,
                                    GHI_CHU = giamCC.GHI_CHU,
                                    IS_XUAT = false,
                                    LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN,
                                    MA_LIEN_QUAN = xuatNhapPB.MA_LIEN_QUAN,
                                    QUYET_DINH_NGAY = giamCC.NGAY_QUYET_DINH,
                                    QUYET_DINH_SO = giamCC.SO_QUYET_DINH,
                                    MA = donViDieuChuyen.MA + "-" + sstNhap,
                                    NGAY_XUAT_NHAP = giamCC.NGAY_DIEU_CHUYEN
                                };
                                _xuatNhapService.InsertXuatNhap(nhap);
                                // mapping nhập
                                var mapNhap = new NhapXuatCongCu
                                {
                                    CHUNG_TU_NGAY = giamCC.CHUNG_TU_NGAY,
                                    CHUNG_TU_SO = giamCC.CHUNG_TU_SO,
                                    CONG_CU_ID = cc != null ? cc.ID : nhapXuatCongCu.CONG_CU_ID,
                                    DON_GIA = iMP_PhanBo.nhapXuatMap.DON_GIA,
                                    NHAP_XUAT_ID = nhap.ID,
                                    SO_LUONG = giamCC.SO_LUONG,
                                    TRANG_THAI_ID = iMP_PhanBo.nhapXuatMap.TRANG_THAI_ID,
                                    THANH_TIEN = iMP_PhanBo.nhapXuatMap.DON_GIA * giamCC.SO_LUONG,
                                };
                                _nhapXuatCongCuService.InsertNhapXuatCongCu(mapNhap);
                            }
                            //giấy báo hỏng mất
                            if (giamCC.LY_DO_GIAM_ID == (int)enumLyDoGiam.GIAM_HONG_MAT)
                            {
                                var itemHM = new GiamHongmat()
                                {
                                    DON_VI_ID = donVi.ID,
                                    CONG_CU_ID = congCu.ID,
                                    NHAP_XUAT_ID = xuat.ID,
                                    DON_VI_BO_PHAN_ID = nhapKho.DON_VI_BO_PHAN_ID,
                                    //LY_DO = giamCC.LY_DO_GIAM_ID,
                                    NGAY_LAP = giamCC.NGAY_DIEU_CHUYEN.GetValueOrDefault(DateTime.Now),
                                    SO_LUONG = giamCC.SO_LUONG.GetValueOrDefault(0),
                                    NGAY_TAO = DateTime.Now,
                                };
                                _giamHongmatService.InsertGiamHongmat(itemHM);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = ex.Message;
                return rs;
            }
            #endregion
            rs.Code = MessageReturn.SUCCESS_CODE;
            rs.Message = "SUCCESS DONE";
            return rs;
        }

        public virtual MessageReturn importKiemKe(IMP_KEMKE_CCDC model)
        {
            MessageReturn rs = new MessageReturn();
            #region check

            if (String.IsNullOrEmpty(model.DON_VI_MA))
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = "DON_VI_MA IS NULL";
                return rs;
            }
            DonVi donVi = _donViService.GetReadOnlyDonViByMa(model.DON_VI_MA);
            if (donVi == null)
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = $"DON_VI_MA '{model.DON_VI_MA}' NOT FOUND";
                return rs;
            }
            if (model.NGAY_KIEM_KE == null)
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = "NGAY_KIEM_KE IS NULL";
                return rs;
            }
            //if (model.ARR_MA_CCDC == null || model.ARR_MA_CCDC.Count == 0)
            //{
            //    rs.Code = MessageReturn.NOT_FOUND_CODE;
            //    rs.Message = "ARR_MA_CCDC IS NULL";
            //    return rs;
            //}

            if (model.CCDC_KIEMKE != null)
                foreach (IMP_CCDCKIEM_KE item in model.CCDC_KIEMKE)
                {
                    if (string.IsNullOrEmpty(item.MA))
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_KIEMKE - MA IS NULL";
                        return rs;
                    }
                    else
                    {
                        CongCu congCu = _itemService.GetAllCongCus(ma_db: item.MA, donViId: donVi.ID).FirstOrDefault();
                        if (congCu == null)
                        {
                            rs.Code = MessageReturn.NOT_FOUND_CODE;
                            rs.Message = $"CCDC_KIEMKE - MA IS '{item.MA}' NOT FOUND";
                            return rs;
                        }
                        else
                        {
                            item.ID = congCu.ID;
                            item.DON_GIA = _itemService.GetDonGiaCCDC(congCu.ID);
                        }

                    }

                    if (item.SO_LUONG == null)
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_KIEMKE - SO_LUONG IS NULL";
                        return rs;
                    }

                }
            if (model.THANH_VIEN_HOI_DONG != null)
                foreach (IMP_THANH_VIEN_HD item in model.THANH_VIEN_HOI_DONG)
                {
                    if (string.IsNullOrEmpty(item.HO_TEN))
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "THANH_VIEN_HOI_DONG - HO_TEN IS NULL";
                        return rs;
                    }
                    if (string.IsNullOrEmpty(item.CHUC_VU))
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "THANH_VIEN_HOI_DONG - CHUC_VU IS NULL";
                        return rs;
                    }
                }
            if (model.CCDC_THUA != null)
                foreach (IMP_CCDC_THUA item in model.CCDC_THUA)
                {
                    if (string.IsNullOrEmpty(item.TEN))
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_THUA - TEN IS NULL";
                        return rs;
                    }
                    if (string.IsNullOrEmpty(item.NHOM_CCDC_MA))
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_THUA - NHOM_CCDC_MA IS NULL";
                        return rs;
                    }
                    else
                    {
                        NhomCongCu nhomCCDC = _nhomCongCuService.GetReadOnlyNhomCongCu(donViId: donVi.ID, ma: item.NHOM_CCDC_MA);
                        if (nhomCCDC == null)
                        {
                            rs.Code = MessageReturn.NOT_FOUND_CODE;
                            rs.Message = $"CCDC_THUA - NHOM_CCDC_MA '{item.NHOM_CCDC_MA}' NOT FOUND";
                            return rs;
                        }
                        item.NHOM_CCDC_ID = nhomCCDC.ID;
                    }
                    if (item.DON_GIA.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_THUA - DON_GIA IS NULL";
                        return rs;
                    }
                    if (item.SO_LUONG.GetValueOrDefault(0) == 0)
                    {
                        rs.Code = MessageReturn.NOT_FOUND_CODE;
                        rs.Message = "CCDC_THUA - SO_LUONG IS NULL";
                        return rs;
                    }
                }
            #endregion
            #region prepare
            try
            {
                var maxId = _kiemKeService.GetKiemKeIdMax(_workContext.CurrentDonVi.ID);
                DonViBoPhan phongBan = new DonViBoPhan();
                if (!string.IsNullOrEmpty(model.PHONG_BAN_MA))
                {
                    phongBan = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donViId: donVi.ID, MA: model.PHONG_BAN_MA);
                }
                KiemKe kiemKe = new KiemKe()
                {
                    SO_KIEM_KE = maxId != null ? (Convert.ToInt32(maxId.SO_KIEM_KE) + 1).ToString().PadLeft(5, '0') : "00001",
                    DON_VI_ID = donVi.ID,
                    DON_VI_BO_PHAN_ID = (phongBan != null && phongBan.ID > 0) ? phongBan.ID : default(decimal?),
                    NGAY_KIEM_KE = model.NGAY_KIEM_KE,
                    DAI_DIEN_BO_PHAN = model.DAI_DIEN_BO_PHAN,
                    NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU,
                    TRUONG_BAN = model.TRUONG_BAN
                };
                _kiemKeService.InsertKiemKe(kiemKe);
                model.ID = kiemKe.ID;
                model.NGAY_KIEM_KE1 = kiemKe.NGAY_KIEM_KE;
                model.DON_VI_SU_DUNG = donVi.TEN;
                model.BO_PHAN_KIEM_KE = phongBan != null ? phongBan.TEN : "";
                // kiểm kê công cụ
                //var listCongCu = _nhapXuatCongCuService.GetMapForKiemKeCongCu(DonViBoPhanId: phongBan.ID, NgayKiemKe: model.NGAY_KIEM_KE);
                //var listCongCu = _nhapXuatCongCuService.GetReadOnlyNhapXuatCongCusByMaCCDC_DB(model.ARR_MA_CCDC, phongBan.ID, model.NGAY_KIEM_KE);
                //foreach (var item in listCongCu)
                //{
                //    var kkcc = new KiemKeCongCu
                //    {
                //        CONG_CU_ID = item.CONG_CU_ID,
                //        IS_PHAT_HIEN_THUA = false,
                //        KIEM_KE_ID = kiemKe.ID,
                //        SO_LUONG_KIEM_KE = item.SoLuongCoThePhanBo,
                //        SO_LUONG_SO_SACH = item.SoLuongCoThePhanBo,
                //        XUAT_NHAP_ID = item.NHAP_XUAT_ID,
                //        DON_VI_BO_PHAN_ID = item.XuatNhap.DON_VI_BO_PHAN_ID,
                //        DON_GIA = item.DON_GIA
                //    };
                //    _kiemKeCongCuService.InsertKiemKeCongCu(kkcc);
                //}
                if (model.CCDC_KIEMKE != null)
                    foreach (IMP_CCDCKIEM_KE item in model.CCDC_KIEMKE)
                    {

                        var kkcc = new KiemKeCongCu
                        {
                            CONG_CU_ID = item.ID,
                            IS_PHAT_HIEN_THUA = false,
                            KIEM_KE_ID = kiemKe.ID,
                            SO_LUONG_KIEM_KE = item.SO_LUONG,
                            DON_GIA = item.DON_GIA
                        };
                        _kiemKeCongCuService.InsertKiemKeCongCu(kkcc);
                    }
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", kiemKe.ToModel<KiemKeModel>(), "KiemKe");
                if (model.THANH_VIEN_HOI_DONG != null)
                    foreach (IMP_THANH_VIEN_HD item in model.THANH_VIEN_HOI_DONG)
                    {
                        KiemKeHoiDong kiemKeHoiDong = new KiemKeHoiDong()
                        {
                            HO_TEN = item.HO_TEN,
                            CHUC_VU = item.CHUC_VU,
                            DAI_DIEN = item.DAI_DIEN,
                            VI_TRI_ID = item.VI_TRI.GetValueOrDefault(1),
                            KIEM_KE_ID = kiemKe.ID
                        };
                        _kiemKeHoiDongService.InsertKiemKeHoiDong(kiemKeHoiDong);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", kiemKeHoiDong.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                    }
                if (model.CCDC_THUA != null)
                    foreach (IMP_CCDC_THUA item in model.CCDC_THUA)
                    {
                        var kkcc = new KiemKeCongCu
                        {
                            TEN_CONG_CU_THUA = item.TEN,
                            NHOM_CONG_CU_ID = item.NHOM_CCDC_ID,
                            IS_PHAT_HIEN_THUA = true,
                            KIEM_KE_ID = kiemKe.ID,
                            SO_LUONG_KIEM_KE = item.SO_LUONG,
                            SO_LUONG_SO_SACH = item.SO_LUONG,
                            DON_VI_BO_PHAN_ID = (phongBan != null && phongBan.ID > 0) ? phongBan.ID : default(decimal?),
                            DON_GIA = item.DON_GIA
                        };
                        _kiemKeCongCuService.InsertKiemKeCongCu(kkcc);
                    }
            }
            catch (Exception ex)
            {
                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = ex.Message;
                return rs;
            }
            #endregion
            rs.Code = MessageReturn.SUCCESS_CODE;
            rs.Message = "Done";
            return rs;
        }
        #endregion
    }
}

