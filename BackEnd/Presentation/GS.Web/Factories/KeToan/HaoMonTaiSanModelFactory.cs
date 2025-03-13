using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.HeThong;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.KeToan;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.KeToan
{
    public class HaoMonTaiSanModelFactory : IHaoMonTaiSanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly IBienDongService _bienDongService;
        private readonly IHaoMonTaiSanService _itemService;
        private readonly ICauHinhService _cauHinhService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly ITaiSanService _taiSanService;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;

        // private readonly ITaiSanModelFactory _taiSanModelFactory;
        #endregion
        #region Ctor
        public HaoMonTaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            IBienDongService bienDongService,
            IHaoMonTaiSanService itemService,
            ICauHinhService cauHinhService,
            ICauHinhModelFactory cauHinhModelFactory,
            ITaiSanService taiSanService,
            IHaoMonTaiSanService haoMonTaiSanService
        )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._bienDongService = bienDongService;
            this._itemService = itemService;
            this._cauHinhService = cauHinhService;
            this._cauHinhModelFactory = cauHinhModelFactory;
            this._taiSanService = taiSanService;
            this._haoMonTaiSanService = haoMonTaiSanService;
        }

        public void InsertUpdateHaoMonTaiSanModelFromTangMoiTS(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, HaoMonTaiSanModel haoMonTaiSanModel)
        {
            //Ghi bảng chốt giá trị tăng mới tài sản
            var ktHaoMonTaiSan = PrepareHaoMonTaiSanModel(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, haoMonTaiSanModel: new HaoMonTaiSanModel());
            //Xóa bảng chốt cũ 
            _haoMonTaiSanService.DeleteHmtsByIdTaiSan(yeuCau.TAI_SAN_ID);
            //----
            var moment = DateTime.Now;
            var _kt_old = new HaoMonTaiSan();
            var nam_bien_dong = yeuCau.NGAY_BIEN_DONG.Value.Year;
            if (nam_bien_dong < moment.Year)
            {
                for (int namHaoMon = nam_bien_dong; namHaoMon <= moment.Year; namHaoMon++)
                {
                    if (namHaoMon > nam_bien_dong)
                    {
                        if (ktHaoMonTaiSan.TONG_GIA_TRI_CON_LAI <= ktHaoMonTaiSan.GIA_TRI_HAO_MON)
                        {
                            ktHaoMonTaiSan.GIA_TRI_HAO_MON = ktHaoMonTaiSan.TONG_GIA_TRI_CON_LAI;
                            ktHaoMonTaiSan.TONG_HAO_MON_LUY_KE = ktHaoMonTaiSan.TONG_NGUYEN_GIA;
                            ktHaoMonTaiSan.TONG_GIA_TRI_CON_LAI = 0;
                        }
                        else
                        {
                            ktHaoMonTaiSan.TONG_HAO_MON_LUY_KE = ktHaoMonTaiSan.TONG_HAO_MON_LUY_KE + ktHaoMonTaiSan.GIA_TRI_HAO_MON;
                            ktHaoMonTaiSan.TONG_GIA_TRI_CON_LAI = Convert.ToDecimal(ktHaoMonTaiSan.TONG_NGUYEN_GIA - ktHaoMonTaiSan.TONG_HAO_MON_LUY_KE);
                        }
                    }
                    ktHaoMonTaiSan.NAM_HAO_MON = namHaoMon;
                    _kt_old = _itemService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: ktHaoMonTaiSan.TAI_SAN_ID, namBaoCao: ktHaoMonTaiSan.NAM_HAO_MON);
                    if (_kt_old != null)
                    {
                        PrepareHaoMonTaiSan(model: ktHaoMonTaiSan, item: _kt_old);
                        _itemService.UpdateHaoMonTaiSan(_kt_old);
                    }
                    else
                    {
                        _itemService.InsertHaoMonTaiSan(ktHaoMonTaiSan.ToEntity<HaoMonTaiSan>());
                    }
                }
            }
            else
            {
                _kt_old = _itemService.GetHaoMonTaiSanByTSIdandNamBaoCao(tsId: ktHaoMonTaiSan.TAI_SAN_ID, namBaoCao: ktHaoMonTaiSan.NAM_HAO_MON);
                if (_kt_old != null)
                {
                    PrepareHaoMonTaiSan(model: ktHaoMonTaiSan, item: _kt_old);
                    _itemService.UpdateHaoMonTaiSan(_kt_old);
                }
                else
                {
                    _itemService.InsertHaoMonTaiSan(ktHaoMonTaiSan.ToEntity<HaoMonTaiSan>());
                }
            }
        }

        public void PrepareHaoMonTaiSan(HaoMonTaiSanModel model, HaoMonTaiSan item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.MA_TAI_SAN = model.MA_TAI_SAN;
            item.NAM_HAO_MON = model.NAM_HAO_MON;
            item.GIA_TRI_HAO_MON = model.GIA_TRI_HAO_MON;
            item.TONG_HAO_MON_LUY_KE = model.TONG_HAO_MON_LUY_KE;
            item.TONG_GIA_TRI_CON_LAI = model.TONG_GIA_TRI_CON_LAI;
            item.TY_LE_HAO_MON = model.TY_LE_HAO_MON;
            item.TONG_NGUYEN_GIA = model.TONG_NGUYEN_GIA;
        }
        #endregion
        public HaoMonTaiSanModel PrepareHaoMonTaiSanModel(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, HaoMonTaiSanModel haoMonTaiSanModel)
        {
            haoMonTaiSanModel.TAI_SAN_ID = yeuCau.TAI_SAN_ID;
            haoMonTaiSanModel.MA_TAI_SAN = yeuCau.TAI_SAN_MA;
            haoMonTaiSanModel.NAM_HAO_MON = yeuCau.NGAY_BIEN_DONG.Value.Year;
            if (yeuCau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU || yeuCau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = yeuCau.NGUYEN_GIA??0;
            else
                haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = yeuCauChiTiet.HM_GIA_TRI_CON_LAI??0;

            if (yeuCau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var lts_vh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(yeuCau.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                if (lts_vh != null)
                    haoMonTaiSanModel.TY_LE_HAO_MON = lts_vh.HM_TY_LE;
            }

            else
            {
                var lts = _loaiTaiSanService.GetLoaiTaiSanById(yeuCau.LOAI_TAI_SAN_ID ?? 0);
                if (lts != null)
                    haoMonTaiSanModel.TY_LE_HAO_MON = lts.HM_TY_LE;

            }


            haoMonTaiSanModel.TONG_NGUYEN_GIA = _bienDongService.TinhNguyenGiaTaiSan(taiSanId: yeuCau.TAI_SAN_ID, To_Date_BienDong: yeuCau.NGAY_BIEN_DONG);
            //Giá trị tỷ lệ hao mòn đang tính theo công thức tỷ lệ %
            // Nếu tỷ lệ không phải theo giá trị % cần thay đổi công thức
            haoMonTaiSanModel.GIA_TRI_HAO_MON = (haoMonTaiSanModel.TONG_NGUYEN_GIA * haoMonTaiSanModel.TY_LE_HAO_MON) / 100;
            if (yeuCau.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = yeuCauChiTiet.HM_LUY_KE;
            else
                haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = yeuCauChiTiet.HM_LUY_KE + haoMonTaiSanModel.GIA_TRI_HAO_MON;
            if (yeuCau.NGAY_BIEN_DONG.Value.Month != 12 && yeuCau.NGAY_BIEN_DONG.Value.Day != 31)
            {
                if (haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI <= haoMonTaiSanModel.GIA_TRI_HAO_MON)
                {
                    haoMonTaiSanModel.GIA_TRI_HAO_MON = haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI;
                    haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = haoMonTaiSanModel.TONG_NGUYEN_GIA;
                    haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = 0;
                }
                else
                {
                    haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = haoMonTaiSanModel.TONG_HAO_MON_LUY_KE + haoMonTaiSanModel.GIA_TRI_HAO_MON;
                    haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = Convert.ToDecimal(haoMonTaiSanModel.TONG_NGUYEN_GIA - haoMonTaiSanModel.TONG_HAO_MON_LUY_KE);
                }
            }
            return haoMonTaiSanModel;
        }

        public HaoMonTaiSanModel PrepareHaoMonTaiSanImport(BienDong bienDong, BienDongChiTiet bienDongChiTiet, HaoMonTaiSanModel haoMonTaiSanModel)
        {
            haoMonTaiSanModel.TAI_SAN_ID = bienDong.TAI_SAN_ID;
            haoMonTaiSanModel.MA_TAI_SAN = bienDong.TAI_SAN_MA;
            haoMonTaiSanModel.NAM_HAO_MON = bienDong.NGAY_BIEN_DONG.Value.Year;
            if (bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU || bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = bienDong.NGUYEN_GIA ?? 0;
            else
                haoMonTaiSanModel.TONG_GIA_TRI_CON_LAI = bienDongChiTiet.HM_GIA_TRI_CON_LAI ?? 0;

            if (bienDong.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var lts_vh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(bienDong.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                if (lts_vh != null)
                    haoMonTaiSanModel.TY_LE_HAO_MON = lts_vh.HM_TY_LE;
            }

            else
            {
                var lts = _loaiTaiSanService.GetLoaiTaiSanById(bienDong.LOAI_TAI_SAN_ID ?? 0);
                if (lts != null)
                    haoMonTaiSanModel.TY_LE_HAO_MON = lts.HM_TY_LE;

            }


            haoMonTaiSanModel.TONG_NGUYEN_GIA = _bienDongService.TinhNguyenGiaTaiSan(taiSanId: bienDong.TAI_SAN_ID, To_Date_BienDong: bienDong.NGAY_BIEN_DONG);
            //Giá trị tỷ lệ hao mòn đang tính theo công thức tỷ lệ %
            // Nếu tỷ lệ không phải theo giá trị % cần thay đổi công thức
            haoMonTaiSanModel.GIA_TRI_HAO_MON = (haoMonTaiSanModel.TONG_NGUYEN_GIA * haoMonTaiSanModel.TY_LE_HAO_MON) / 100;
            if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = bienDongChiTiet.HM_LUY_KE;
            else
                haoMonTaiSanModel.TONG_HAO_MON_LUY_KE = bienDongChiTiet.HM_LUY_KE + haoMonTaiSanModel.GIA_TRI_HAO_MON;
            return haoMonTaiSanModel;
        }

        public HaoMonTaiSanSearchModel PrepareHaoMonTaiSanSearchModel(HaoMonTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var list_except = new List<int>
            {
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
                (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,
                (int)enumLOAI_HINH_TAI_SAN.KHAC,
                (int)enumLOAI_HINH_TAI_SAN.DAC_THU,
                (int)enumLOAI_HINH_TAI_SAN.DAT
            };
            searchModel.ddlLoaiHinhTaiSan = ((enumLOAI_HINH_TAI_SAN)searchModel.LoaiHinhTaiSan).ToSelectList(valuesToExclude: list_except.ToArray()).ToList();
            searchModel.NamHaoMon = DateTime.Now.Year;
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public HaoMonTaiSanListModel PrepareHaoMonTaiSanListModel(HaoMonTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var donViId = _workContext.CurrentDonVi.ID;
            //get items
            var items = _itemService.SearchHaoMonTaiSans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, NamHaoMon: searchModel.NamHaoMon, LoaiHinhTaiSan: searchModel.LoaiHinhTaiSan, DonViId: donViId);

            //prepare list model
            var model = new HaoMonTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareHaoMonForEditModel(null, c)),
                Total = items.TotalCount
            };
            return model;
        }
        public HaoMonTaiSanModel PrepareHaoMonForEditModel(HaoMonTaiSanModel model, HaoMonTaiSan item)
        {
            if (item != null)
            {
                model = item.ToModel<HaoMonTaiSanModel>();
            }
            //lấy ra hao mòn năm trước
            if (model != null)
            {
                model.TONG_HAO_MON_LUY_KE_NAM_TRUOC = _itemService.GetLKHMTruocDo(model.ToEntity<HaoMonTaiSan>());
                model.IsLock = _cauHinhModelFactory.CheckYearIsLockSoTaiSan(model.NAM_HAO_MON);
            }
            return model;
        }

        public MessageReturn InSertHaoMonDongBo(HaoMonDBModel model, int nguonId)
        {
            var taiSan = new TaiSan();
            if (string.IsNullOrEmpty(model.MA_TAI_SAN)) 
            {
                return MessageReturn.CreateErrorMessage("Mã tài sản không được bỏ trống");
            }
            // Hao mòn dkts 4.0
            if (nguonId == (int)enumNguonTaiSan.DKTS40)
            {
                taiSan = _taiSanService.GetTaiSanByMaDKTS(model.MA_TAI_SAN);
                if (taiSan == null)
                {
                    return MessageReturn.CreateErrorMessage("Tài sản không tồn tại");
                }
                model.TAI_SAN_ID = taiSan.ID;
                model.MA_TSC = taiSan.MA;
                var listError = ValidateHaoMonDBModel(model);
                if (listError != null && listError.Count() > 0)
                {
                    var message = string.Join("; ", listError);
                    return MessageReturn.CreateErrorMessage(message);
                }
                // Trường hợp tồn tại thì update
               

                // Trường hợp không tồn tại thì thêm mới
                var haoMon = new HaoMonTaiSan();              
                return MessageReturn.CreateSuccessMessage("Đồng bộ thành công", model);

            }
            
            return MessageReturn.CreateSuccessMessage("", model);
        }
        public void PrepareHaoMonTaiSanFromHaoMonDB(HaoMonDBModel model, HaoMonTaiSan item)
        {
            if (item != null)
            {
                item.TAI_SAN_ID = model.TAI_SAN_ID ?? 0;
                item.MA_TAI_SAN = model.MA_TSC;
                item.NAM_HAO_MON = model.NAM_HAO_MON ?? 0;
                item.GIA_TRI_HAO_MON = model.GIA_TRI_HAO_MON;
                item.TONG_HAO_MON_LUY_KE = model.TONG_HAO_MON_LUY_KE;
                item.TONG_GIA_TRI_CON_LAI = model.TONG_GIA_TRI_CON_LAI;
                item.TY_LE_HAO_MON = model.TY_LE_HAO_MON;
                item.TONG_NGUYEN_GIA = model.TONG_NGUYEN_GIA;
            }
            
        }
        public List<string> ValidateHaoMonDBModel(HaoMonDBModel model)
        {
            var ListError = new List<string>();

            if (model.NAM_HAO_MON == null || model.NAM_HAO_MON <= 0)
            {
                ListError.Add("Năm hao mòn không hợp lệ");
            }
            
            
            if (!(model.TAI_SAN_ID >0))
            {                
                ListError.Add("Tài sản không tồn tại");
            }
           
            return ListError;
        }

    }
}
