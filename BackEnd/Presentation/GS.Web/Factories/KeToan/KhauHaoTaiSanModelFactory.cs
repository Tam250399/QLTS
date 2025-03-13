//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
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
using GS.Core.Domain.KT;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.KeToan;
using GS.Web.Models.DongBoTaiSan;
using GS.Core.Domain.TaiSans;
using GS.Services.TaiSans;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Core.Domain.NghiepVu;
using GS.Services.DanhMuc;
using GS.Web.Models.ImportTaiSan;

namespace GS.Web.Factories.KeToan
{
    public class KhauHaoTaiSanModelFactory : IKhauHaoTaiSanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKhauHaoTaiSanService _itemService;
        private readonly ITaiSanService _taiSanService;
        private readonly ITaiSanKhauHaoService _taiSanKhauHaoService;
        #endregion

        #region Ctor

        public KhauHaoTaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IKhauHaoTaiSanService itemService,
            ITaiSanService taiSanService,
            ITaiSanKhauHaoService taiSanKhauHaoService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._taiSanService = taiSanService;
            this._taiSanKhauHaoService = taiSanKhauHaoService;
        }
        #endregion

        #region KhauHaoTaiSan
        public KhauHaoTaiSanSearchModel PrepareKhauHaoTaiSanSearchModel(KhauHaoTaiSanSearchModel searchModel)
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
                (int)enumLOAI_HINH_TAI_SAN.DAC_THU
            };
            searchModel.ddlLoaiHinhTaiSan = ((enumLOAI_HINH_TAI_SAN)searchModel.LoaiHinhTaiSan).ToSelectList(valuesToExclude: list_except.ToArray()).ToList();

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KhauHaoTaiSanListModel PrepareKhauHaoTaiSanListModel(KhauHaoTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var donViId = _workContext.CurrentDonVi.ID;
            //get items
            var items = _itemService.SearchKhauHaoTaiSans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ThangKhauHao: searchModel.ThangKhauHao, NamKhauHao: searchModel.NamKhauHao, LoaiHinhTaiSan: searchModel.LoaiHinhTaiSan, DonViId: donViId);

            //prepare list model
            var model = new KhauHaoTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<KhauHaoTaiSanModel>()),
                Total = items.TotalCount
            };
            return model;
        }

        public List<KhauHaoExport> PrepareKhauHaoTaiSanExport(KhauHaoTaiSanSearchModel searchModel)
        {

            List<KhauHaoExport> rs = new List<KhauHaoExport>();
            var donViId = _workContext.CurrentDonVi.ID;
            var items = _itemService.SearchKhauHaoTaiSans(Keysearch: searchModel.KeySearch,ThangKhauHao: searchModel.ThangKhauHao, NamKhauHao: searchModel.NamKhauHao, LoaiHinhTaiSan: searchModel.LoaiHinhTaiSan, DonViId: donViId);
            List<KhauHaoExport> lstKhauHao = new List<KhauHaoExport>();
            if (items == null || items.Count() == 0)
                return new List<KhauHaoExport>();
            var allTaiSan = _taiSanService.GetAllTaiSanByMaBo(mabo: _workContext.CurrentDonVi.MA_DON_VI.Substring(0, 3), donViId: _workContext.CurrentDonVi.ID);
            return items.Select(c =>
            {
                var ts = allTaiSan.FirstOrDefault(x => x.ID == c.TAI_SAN_ID);
                var m = new KhauHaoExport()
                {
                    MA = c.MA_TAI_SAN,
                    THANG_KHAU_HAO = c.THANG_KHAU_HAO,
                    NAM_KHAU_HAO = c.NAM_KHAU_HAO,
                    GIA_TRI_KHAU_HAO = c.GIA_TRI_KHAU_HAO,
                    TONG_KHAU_HAO_LUY_KE = c.TONG_KHAU_HAO_LUY_KE,
                    TONG_GIA_TRI_CON_LAI = c.TONG_GIA_TRI_CON_LAI,
                    TI_LE_KHAU_HAO = c.TY_LE_KHAU_HAO,
                    TONG_NGUYEN_GIA = c.TONG_NGUYEN_GIA,
                    MA_FAST = ts?.MA_DB
                };
                return m;
            }).ToList();

        }
        public KhauHaoTaiSanModel PrepareKhauHaoTaiSanModel(KhauHaoTaiSanModel model, KhauHaoTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhauHaoTaiSanModel>();
            }
            //more

            return model;
        }
        public MessageReturn PrepareInSertKhauHaoDongBo(KhauHaoDBModel model, int nguonId)
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
                var listError = ValidateKhauHaoDBModel(model);
                if (listError != null && listError.Count() > 0)
                {
                    var message = string.Join("; ", listError);
                    return MessageReturn.CreateErrorMessage(message);
                }

                return MessageReturn.CreateSuccessMessage("Đồng bộ thành công", model);

            }

            return MessageReturn.CreateSuccessMessage("", model);
        }
        public List<string> ValidateKhauHaoDBModel(KhauHaoDBModel model)
        {
            var ListError = new List<string>();

            if (model.NAM_KHAU_HAO == null || model.NAM_KHAU_HAO <= 0)
            {
                ListError.Add("Năm khấu hao không hợp lệ");
            }
            if (model.THANG_KHAU_HAO == null || model.THANG_KHAU_HAO <= 0)
            {
                ListError.Add("Tháng khấu hao không hợp lệ");
            }
            if (!(model.TAI_SAN_ID > 0))
            {
                ListError.Add("Tài sản không tồn tại");
            }

            return ListError;
        }
        public void PrepareKhauHaoTaiSan(KhauHaoTaiSanModel model, KhauHaoTaiSan item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.MA_TAI_SAN = model.MA_TAI_SAN;
            item.NAM_KHAU_HAO = model.NAM_KHAU_HAO;
            item.GIA_TRI_KHAU_HAO = model.GIA_TRI_KHAU_HAO;
            item.TONG_KHAU_HAO_LUY_KE = model.TONG_KHAU_HAO_LUY_KE;
            item.TONG_GIA_TRI_CON_LAI = model.TONG_GIA_TRI_CON_LAI;
            item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
            item.TONG_NGUYEN_GIA = model.TONG_NGUYEN_GIA;
            item.THANG_KHAU_HAO = model.THANG_KHAU_HAO;

        }
        public void PrepareKhauHaoTaiSanFromHaoMonDB(KhauHaoDBModel model, KhauHaoTaiSan item)
        {
            if (item != null)
            {
                item.TAI_SAN_ID = model.TAI_SAN_ID ?? 0;
                item.MA_TAI_SAN = model.MA_TSC;
                item.NAM_KHAU_HAO = model.NAM_KHAU_HAO ?? 0;
                item.THANG_KHAU_HAO = model.THANG_KHAU_HAO ?? 0;
                item.GIA_TRI_KHAU_HAO = model.GIA_TRI_KHAU_HAO;
                item.TONG_KHAU_HAO_LUY_KE = model.TONG_KHAU_HAO_LUY_KE;
                item.TONG_GIA_TRI_CON_LAI = model.TONG_GIA_TRI_CON_LAI;
                item.TY_LE_KHAU_HAO = model.TY_LE_KHAU_HAO;
                item.TONG_NGUYEN_GIA = model.TONG_NGUYEN_GIA;
            }

        }

        public void InsertUpdateKhauHaoTaiSanModelFromTangMoiTS(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, KhauHaoTaiSanModel khauHaoTaiSanModel)
        {
            //Ghi bảng chốt giá trị tăng mới tài sản
            var ktKhauHaoTaiSan = PrepareKhauHaoTaiSanModelFromYeuCau(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, khauHaoTaiSanModel: new KhauHaoTaiSanModel());
            //----
            //var moment = DateTime.Now;
            var _kt_old = new KhauHaoTaiSan();

            _kt_old = _itemService.GetListKhauHaoTaiSans(taiSanId: ktKhauHaoTaiSan.TAI_SAN_ID, namKhauHao: ktKhauHaoTaiSan.NAM_KHAU_HAO, thangKhauHao: ktKhauHaoTaiSan.THANG_KHAU_HAO).FirstOrDefault();
            if (_kt_old != null)
            {
                PrepareKhauHaoTaiSan(model: ktKhauHaoTaiSan, item: _kt_old);
                _itemService.UpdateKhauHaoTaiSan(_kt_old);
            }
            else
            {
                _itemService.InsertKhauHaoTaiSan(ktKhauHaoTaiSan.ToEntity<KhauHaoTaiSan>());
            }
            if (yeuCauChiTiet.KH_THANG_CON_LAI > 0)
            {
                for (int thangKH = 1; thangKH <= yeuCauChiTiet.KH_THANG_CON_LAI; thangKH++)
                {
                    if (ktKhauHaoTaiSan.THANG_KHAU_HAO + 1 > 12)
                    {
                        ktKhauHaoTaiSan.THANG_KHAU_HAO = 1;
                        ktKhauHaoTaiSan.NAM_KHAU_HAO = ktKhauHaoTaiSan.NAM_KHAU_HAO + 1;
                    }
                    else
                    {
                        ktKhauHaoTaiSan.THANG_KHAU_HAO = ktKhauHaoTaiSan.THANG_KHAU_HAO + 1;
                        ktKhauHaoTaiSan.NAM_KHAU_HAO = ktKhauHaoTaiSan.NAM_KHAU_HAO;
                    }
                    if (ktKhauHaoTaiSan.GIA_TRI_KHAU_HAO > ktKhauHaoTaiSan.TONG_GIA_TRI_CON_LAI)
                    {
                        ktKhauHaoTaiSan.GIA_TRI_KHAU_HAO = ktKhauHaoTaiSan.TONG_GIA_TRI_CON_LAI;
                        ktKhauHaoTaiSan.TONG_GIA_TRI_CON_LAI = 0;
                    }
                    else
                    {
                        ktKhauHaoTaiSan.TONG_GIA_TRI_CON_LAI = ktKhauHaoTaiSan.TONG_GIA_TRI_CON_LAI - ktKhauHaoTaiSan.GIA_TRI_KHAU_HAO;
                    }
                    ktKhauHaoTaiSan.TONG_KHAU_HAO_LUY_KE = ktKhauHaoTaiSan.TONG_KHAU_HAO_LUY_KE + ktKhauHaoTaiSan.GIA_TRI_KHAU_HAO;

                    _kt_old = _itemService.GetListKhauHaoTaiSans(taiSanId: ktKhauHaoTaiSan.TAI_SAN_ID, namKhauHao: ktKhauHaoTaiSan.NAM_KHAU_HAO, thangKhauHao: ktKhauHaoTaiSan.THANG_KHAU_HAO).FirstOrDefault();
                    if (_kt_old != null)
                    {
                        PrepareKhauHaoTaiSan(model: ktKhauHaoTaiSan, item: _kt_old);
                        _itemService.UpdateKhauHaoTaiSan(_kt_old);
                    }
                    else
                    {
                        _itemService.InsertKhauHaoTaiSan(ktKhauHaoTaiSan.ToEntity<KhauHaoTaiSan>());
                    }
                }
            }
            // xóa khấu hao thừa khi sửa lại yêu cầu
            _itemService.DeleteKhauHaoTaiSanByNgay(ktKhauHaoTaiSan.TAI_SAN_ID, ktKhauHaoTaiSan.NAM_KHAU_HAO, ktKhauHaoTaiSan.THANG_KHAU_HAO);
        }
        public KhauHaoTaiSanModel PrepareKhauHaoTaiSanModelFromYeuCau(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, KhauHaoTaiSanModel khauHaoTaiSanModel)
        {
            khauHaoTaiSanModel.TAI_SAN_ID = yeuCau.TAI_SAN_ID;
            khauHaoTaiSanModel.MA_TAI_SAN = yeuCau.TAI_SAN_MA;
            khauHaoTaiSanModel.NAM_KHAU_HAO = yeuCau.NGAY_BIEN_DONG.Value.Year;
            khauHaoTaiSanModel.THANG_KHAU_HAO = yeuCau.NGAY_BIEN_DONG.Value.Month;
            if (yeuCau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU || yeuCau.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                khauHaoTaiSanModel.TONG_GIA_TRI_CON_LAI = yeuCau.NGUYEN_GIA ?? 0;
            else
            {
                khauHaoTaiSanModel.GIA_TRI_KHAU_HAO = yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG;
                khauHaoTaiSanModel.TONG_KHAU_HAO_LUY_KE = yeuCauChiTiet.KH_LUY_KE;
                khauHaoTaiSanModel.TONG_GIA_TRI_CON_LAI = yeuCauChiTiet.KH_CON_LAI ?? 0;
                var tyleKH = _taiSanKhauHaoService.GetListTaiSanKhauHaoByTaiSanIdAndNgayBatDau((decimal)yeuCau.TAI_SAN_ID, yeuCauChiTiet.KH_NGAY_BAT_DAU)?.TY_LE_KHAU_HAO;
                khauHaoTaiSanModel.TY_LE_KHAU_HAO = tyleKH ?? 0;
                khauHaoTaiSanModel.TONG_NGUYEN_GIA = yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO;
            }
            return khauHaoTaiSanModel;
        }
        public KhauHaoTaiSan InsertToKhauHaoTaiSan(ImportKhTaiSanModel item, TaiSan taiSan)
        {
            KhauHaoTaiSanModel model = new KhauHaoTaiSanModel();
            model.TAI_SAN_ID = taiSan.ID;
            model.MA_TAI_SAN = taiSan.MA;
            model.NAM_KHAU_HAO = item.NAM_KHAU_HAO;
            model.GIA_TRI_KHAU_HAO = item.GIA_TRI_KHAU_HAO;
            model.TONG_KHAU_HAO_LUY_KE = item.TONG_KHAU_HAO_LUY_KE;
            model.TONG_GIA_TRI_CON_LAI = item.TONG_GIA_TRI_CON_LAI;
            model.TY_LE_KHAU_HAO = item.TY_LE_KHAU_HAO * 100;
            model.TONG_NGUYEN_GIA = item.TONG_NGUYEN_GIA;
            model.THANG_KHAU_HAO = 12;

            KhauHaoTaiSan khauHaoTaiSan = model.ToEntity<KhauHaoTaiSan>();
            _itemService.InsertKhauHaoTaiSan(khauHaoTaiSan);
            return khauHaoTaiSan;
        }
        #endregion
    }
}

