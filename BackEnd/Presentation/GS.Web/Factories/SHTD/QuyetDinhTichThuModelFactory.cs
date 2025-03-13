//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Core.Domain.SHTD;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.SHTD;
using GS.Web.Factories.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Web.Factories.DB;

namespace GS.Web.Factories.SHTD
{
    public class QuyetDinhTichThuModelFactory : IQuyetDinhTichThuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IQuyetDinhTichThuService _itemService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IDonViService _donViService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IXuLyService _xuLyService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly INhatKyService _nhatKyService;
        private readonly INhatKyTaiSanToanDanService _nhatKyTaiSanToanDanService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        #endregion

        #region Ctor

        public QuyetDinhTichThuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IQuyetDinhTichThuService itemService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ITaiSanTdService taiSanTdService,
            IDonViService donViService,
            ILoaiTaiSanService loaiTaiSanService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IXuLyService xuLyService,
            IDonViModelFactory donViModelFactory,
            IHoatDongService hoatDongService,
            INhatKyService nhatKyService,
            INhatKyTaiSanToanDanService nhatKyTaiSanToanDanService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._donViService = donViService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._xuLyService = xuLyService;
            this._donViModelFactory = donViModelFactory;
            _hoatDongService = hoatDongService;
            _nhatKyService = nhatKyService;
            _nhatKyTaiSanToanDanService = nhatKyTaiSanToanDanService;
            _taiSanNhatKyService = taiSanNhatKyService;
            _dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
        }
        #endregion

        #region QuyetDinhTichThu
        public QuyetDinhTichThuSearchModel PrepareQuyetDinhTichThuSearchModel(QuyetDinhTichThuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: false).ToList();
            //searchModel.DDLNguonGocTaiSan = _nguonGocTaiSanModelFactory.PrepareSelectListNguonGocTaiSan();
            searchModel.SetGridPageSize();
            searchModel.DDLTrangThai = new enumTRANGTHAI_QUYETDINH_TSTD().ToSelectList(valuesToExclude:new int[] {(int)enumTRANGTHAI_QUYETDINH_TSTD.XOA, (int)enumTRANGTHAI_QUYETDINH_TSTD.NHAP }).ToList();
            searchModel.DDLLoaiHinhTaiSan = new enumLOAI_HINH_TAI_SAN_TOAN_DAN().ToSelectList().ToList();

            return searchModel;
        }

        public QuyetDinhTichThuListModel PrepareQuyetDinhTichThuListModel(QuyetDinhTichThuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchQuyetDinhTichThus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, SoQuyetDinh: searchModel.SoQuyetDinh, NgayQuyetDinhTu: searchModel.NgayQuyetDinhTu, NgayQuyetDinhDen: searchModel.NgayQuyetDinhDen, LoaiTaiSan: searchModel.LoaiTaiSanId, NguonGocTaiSan: searchModel.NguonGocTaiSanId,TrangThaiId:searchModel.TrangThaiId,LoaiHinhTaiSanId:searchModel.LoaiHinhTaiSanId,isTichThu:searchModel.is_tichthu);

            //prepare list model
            var model = new QuyetDinhTichThuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareQuyetDinhTichThuModelForList(null, c)),
                Total = items.TotalCount
            };
            return model;
        }
        public QuyetDinhTichThuModel PrepareQuyetDinhTichThuModelForList(QuyetDinhTichThuModel model, QuyetDinhTichThu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<QuyetDinhTichThuModel>();
                model.TEN = model.TEN + " (" + model.QUYET_DINH_SO + "--" + model.QUYET_DINH_NGAY.toDateVNString() + ")";
                var listTSTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID).Select(c => c.ID).ToList();
                if (model.TRANG_THAI_ID==(int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET || model.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI)
                {
                    model.is_delete = true;
                }
                else
                {
                    model.is_delete = false;
                }
                if (model.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI)
                {
                    model.is_tuchoi = true;
                }
                if (model.CO_QUAN_BAN_HANH_ID != null)
                {
                    model.CO_QUAN_BAN_HANH_TEN = item.BoNganhTinh.TEN;
                }
                else if(model.CO_QUAN_BAN_HANH_ID == null)
                {
                    model.CO_QUAN_BAN_HANH_TEN = "";
                }
                if (item.NguonGocTaiSan != null)
                {
                    model.TenNguonGoc = item.NguonGocTaiSan.TEN;
                }
                //check xem tài sản trong quyết định ý đã có trong phương án xử lý chưa;
                //lấy tài sản
                var listTS = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID).Select(c => c.ID).ToList();
                model.is_boduyet = !_taiSanTdXuLyService.GetTaiSanTdXuLys(ListTSTDId: listTSTD).Any();
            }
            //more

            return model;
        }
        public bool CheckDaTonTaiKetQuaTheoTaiSan(decimal QuyetDinhID)
        {
            var listTSTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(QuyetDinhID).Select(c => c.ID).ToList();
            if (_taiSanTdXuLyService.GetTaiSanTdXuLys(ListTSTDId: listTSTD, LoaiXuLy: (int)enumLoaiXuLy.KetQua).Count() > 0)
            {
                return false;
            }
            return true;
        }
        public QuyetDinhTichThuModel PrepareQuyetDinhTichThuModel(QuyetDinhTichThuModel model, QuyetDinhTichThu item, bool excludeProperties = false,bool prepareDDL = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<QuyetDinhTichThuModel>();
                if (model.NGUON_GOC_ID != null)
                {
                    var nguongoc = _nguonGocTaiSanService.GetNguonGocTaiSanById((decimal)item.NGUON_GOC_ID);
                    if (nguongoc != null)
                    {
                        var parentID = nguongoc.TREE_NODE.Split('-').Select(c => Convert.ToInt32(c)).FirstOrDefault();
                        var parent = _nguonGocTaiSanService.GetNguonGocTaiSanById(parentID);
                        if (parent != null)
                        {
                            if (parent.MA == enumMaLoaiQuyetDinh.TichThu)
                            {
                                model.is_tichthu = true;
                            }
                            else
                            {
                                model.is_tichthu = false;
                            }
                        }
                    }
                }
                if(item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET)
                {
                    model.is_detail = true;
                }
                else
                {
                    model.is_detail = false;
                }
            }
            //more
            if (prepareDDL)
            {                
                model.DDLBoNganh = _donViModelFactory.PrepareSelectListBoNganhTinh(valSelected:model.CO_QUAN_BAN_HANH_ID!= null? model.CO_QUAN_BAN_HANH_ID:0, isAddFirst:true);
            }
            return model;
        }
        public List<SelectListItem> PrepareListModelForBaoCao()
        {
            var listqd = _itemService.GetQuyetDinhTichThus().Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN + '(' + c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString() + ')'
            }).ToList();
            listqd.Insert(0, new SelectListItem { Value = "0", Text = "Chọn quyết định" });
            return listqd;
        }
        public List<SelectListItem> PrepareDDLQuyetDinhForPhuongAn(bool isAddFirst = true, List<int> ListQuyetDinhId = null)
        {
            if(ListQuyetDinhId== null)
            {
                ListQuyetDinhId = new List<int>();
            }
            var listqd = _itemService.GetQuyetDinhTichThuByIds(_taiSanTdService.GetQuyetDinhTichThuCon(_workContext.CurrentDonVi.ID, ListQuyetDinhId).ToArray()).Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN + " (" + c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString() + ')',
                Selected = (ListQuyetDinhId.Exists(m=>m == c.ID))
            }).ToList();
            if(isAddFirst)
            listqd.Insert(0, new SelectListItem { Value = "0", Text = "Chọn quyết định" });
            return listqd;
        }
        public void PrepareQuyetDinhTichThu(QuyetDinhTichThuModel model, QuyetDinhTichThu item)
        {
            item.GUID = model.GUID;
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            item.TEN = model.TEN;
            item.GHI_CHU = model.GHI_CHU;
            item.DON_VI_ID = model.DON_VI_ID;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.NGAY_TAO = model.NGAY_TAO;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            //item.CO_QUAN_BAN_HANH_TEN = model.CO_QUAN_BAN_HANH_TEN;
            item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
            item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
            item.NGUON_GOC_ID = model.NGUON_GOC_ID;
            item.TONG_GIA_TRI = model.TONG_GIA_TRI;

    }
        public List<TaiSanToanDanModel> ImportExcel(List<ImportTaiSanToanDanModel> ListImport)
        {
            List<TaiSanToanDanModel> ListSuccess = new List<TaiSanToanDanModel>();
            List<TaiSanToanDanModel> ListQuyetDinhTichThu = new List<TaiSanToanDanModel>();
            string MaDonVi = ListImport.Select(m => m.MA_DON_VI).Distinct().FirstOrDefault();
            DonVi donVi = _donViService.GetCacheDonViByMa(MaDonVi);
            if (donVi == null)
                return null;
            // insert Quyết định xử lý
            var ListQuyetDinhXuLy = ListImport.Select(m => new XuLy()
            {
                QUYET_DINH_SO = m.XL_SO_QUYET_DINH,
                QUYET_DINH_NGAY = m.XL_NGAY_QUYET_DINH,
                DON_VI_ID = donVi.ID,
                //LOAI_XU_LY_ID = (decimal)enumLoaiXuLy.KetQua,
                //CO_QUAN_BAN_HANH_TEN = m.XL_TEN_DON_VI,
                TRANG_THAI_ID = (int)enumTrangThaiXuLy.TonTai
            });
            foreach (var item in ListQuyetDinhXuLy)
            {
                _xuLyService.InsertXuLy(item);
            }      
            ListQuyetDinhXuLy = ListQuyetDinhXuLy.Distinct();
            foreach (var item in ListImport)
            {
                TaiSanToanDanModel model = ListQuyetDinhTichThu.Last();

                if (!string.IsNullOrEmpty(item.SO_QUYET_DINH))
                {
                    model = new TaiSanToanDanModel();
                    model.MA_DON_VI = item.MA_DON_VI;
                    model.TEN_QUYET_DINH = item.TEN_QUYET_DINH;
                    model.SO_QUYET_DINH = item.SO_QUYET_DINH;
                    model.NGAY_QUYET_DINH = item.NGAY_QUYET_DINH;
                    model.TEN_DON_VI = item.TEN_DON_VI;
                    ListQuyetDinhTichThu.Add(model);
                }
                else if (model == null)
                {
                    continue;
                }
                model.ListTaiSan.Add(new ChiTietTaiSanModel()
                {
                    TS_TEN_TAI_SAN = item.TS_TEN_TAI_SAN,
                    TS_LOAI_TAI_SAN = item.TS_LOAI_TAI_SAN,
                    TS_NGUON_GOC_TAI_SAN = item.TS_NGUON_GOC_TAI_SAN,
                    TS_NGUYEN_GIA = item.TS_NGUYEN_GIA,
                    TS_SO_LUONG = item.TS_SO_LUONG,
                    TS_GIA_TRI = item.TS_GIA_TRI,
                    TS_DIEN_TICH = item.TS_DIEN_TICH,
                    TS_KHOI_LUONG = item.TS_KHOI_LUONG,
                    XL_SO_QUYET_DINH = item.XL_SO_QUYET_DINH,
                    XL_NGAY_QUYET_DINH = item.XL_NGAY_QUYET_DINH,
                    XL_TEN_DON_VI = item.XL_TEN_DON_VI,
                    XL_HINH_THUC_XU_LY = item.XL_HINH_THUC_XU_LY,
                    XL_NGAY_XU_LY = item.XL_NGAY_XU_LY,
                    XL_HO_SO_GIAY_TO = item.XL_HO_SO_GIAY_TO,
                    XL_CHI_PHI_XU_LY = item.XL_CHI_PHI_XU_LY,
                    XL_SO_LUONG = item.XL_SO_LUONG,
                    XL_DON_VI_CHUYEN = item.XL_DON_VI_CHUYEN,
                    XL_NGAY_CHUYEN = item.XL_NGAY_CHUYEN,
                    XL_SO_TIEN_THU_DUOC = item.XL_SO_TIEN_THU_DUOC,
                    XL_SO_TIEN_NOP_TKTG = item.XL_SO_TIEN_NOP_TKTG,
                    XL_DON_VI_DUOC_GIAO = item.XL_DON_VI_DUOC_GIAO,
                    XL_NGAY_BAN_GIAO = item.XL_NGAY_BAN_GIAO,
                    XL_NOP_NGAN_SACH = item.XL_NOP_NGAN_SACH
                });              
            }
            foreach (var item in ListQuyetDinhTichThu)
            {
                QuyetDinhTichThu quyetDinhTichThu = _itemService.GetQuyetDinhTichThu(SoQuyetDinh: item.SO_QUYET_DINH, NgayQuyetDinh: item.NGAY_QUYET_DINH, MaDonVi: MaDonVi);
                if (quyetDinhTichThu != null)
                    continue;
                quyetDinhTichThu = new QuyetDinhTichThu();
                quyetDinhTichThu.QUYET_DINH_SO = item.SO_QUYET_DINH;
                quyetDinhTichThu.QUYET_DINH_NGAY = item.NGAY_QUYET_DINH;
                quyetDinhTichThu.TEN = item.TEN_QUYET_DINH;
                //quyetDinhTichThu.CO_QUAN_BAN_HANH_TEN = item.TEN_DON_VI;
                quyetDinhTichThu.DON_VI_ID = donVi.ID;
                quyetDinhTichThu.TRANG_THAI_ID = (decimal)enumTRANGTHAI_QUYETDINH_TSTD.DUYET;
                _itemService.InsertQuyetDinhTichThu(quyetDinhTichThu);
                // insert tài sản
                foreach (var ts in item.ListTaiSan)
                {
                    TaiSanTd taiSanTd = new TaiSanTd();
                    taiSanTd.QUYET_DINH_TICH_THU_ID = quyetDinhTichThu.ID;
                    taiSanTd.GIA_TRI_TINH = ts.TS_DIEN_TICH;
                    taiSanTd.GIA_TRI_XAC_LAP = ts.TS_GIA_TRI;
                    //taiSanTd.KHOI_LUONG = ts.TS_KHOI_LUONG;
                    //taiSanTd.NGUYEN_GIA = ts.TS_NGUYEN_GIA;
                    taiSanTd.SO_LUONG = ts.TS_SO_LUONG;
                    taiSanTd.TEN = ts.TS_TEN_TAI_SAN;
                    taiSanTd.TRANG_THAI_ID = (decimal)enumTRANGTHAI_QUYETDINH_TSTD.DUYET;
                    //loại tài sản
                    string MaLoaiTaiSan = ts.TS_LOAI_TAI_SAN.Split('-').First().Trim();
                    LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(MaLoaiTaiSan);
                    if (loaiTaiSan == null)
                        continue;
                    else
                    {
                        taiSanTd.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                    }
                    //nguồn gốc tài sản
                    string NguonGocTaiSan = ts.TS_NGUON_GOC_TAI_SAN.Split('-').First().Trim();
                    NguonGocTaiSan nguon = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(NguonGocTaiSan);
                    if (nguon == null)
                        continue;
                    else
                    {
                        taiSanTd.quyetdinh.NGUON_GOC_ID = loaiTaiSan.ID;
                    }
                    taiSanTd.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                    _taiSanTdService.InsertTaiSanTd(taiSanTd);
                    // insert xử lý tài sản
                    TaiSanTdXuLy taiSanTdXuLy = new TaiSanTdXuLy();
                    taiSanTdXuLy.TAI_SAN_ID = taiSanTd.ID;
                  //  taiSanTdXuLy.XU_LY_ID = ListQuyetDinhXuLy.Where(m=>m.QUYET_DINH_SO = ts)
                }                
            }
            return ListSuccess;
        }
        public bool CheckTongGiaTriTichThu(Guid Guid, decimal TongGiaTriTichThu)
        {
            var item = _itemService.GetQuyetDinhTichThuByGuid(Guid);

            if (item == null)
                return false;
            // tìm ra list tài sản
            var listTaiSan = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
            if (listTaiSan != null)
            {
                var TongGiaTriTaiSan = listTaiSan.Sum(c => c.GIA_TRI_XAC_LAP ?? 0);
                if (TongGiaTriTichThu >= TongGiaTriTaiSan)
                {
                    return true;
                }
                return false;
            }
            return true;
        }


        public int DuyetListQuyetDinhTichThu(string strListQuyetDinhID)
        {
            var count = 0;
            if (!string.IsNullOrEmpty(strListQuyetDinhID))
            {
                var listQDId = strListQuyetDinhID.ToListInt();
                foreach (var quyetDinhID in listQDId)
                {
                    try
                    {
                        var item = DuyetQuyetDinhTichThu(quyetDinhID);
                        if (item != null)
                        {
                            count += 1;
                        }
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                 

                }
            }

            return count;
        }

        public QuyetDinhTichThu DuyetQuyetDinhTichThu(decimal quyetDinhId)
        {
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuById(quyetDinhId);
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET;
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatDongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                var nhatky = new NhatKyTaiSanToanDan()
                {
                    DATA_JSON = item.toStringJson(),
                    QUYET_DINH_ID = item.ID,
                    NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                    TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET
                };
                _nhatKyTaiSanToanDanService.InsertNhatKyTaiSanToanDan(nhatky);
                _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Thêm mới thông tin ", nhatky.ToModel<NhatKyTaiSanToanDanModel>(), "NhatKyTaiSanToanDan");
                //nhật ký đồng bộ
                //quyết định tịch thu
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKyByQuyetDinhTichThu(item.ID);
                if (taiSanNhatKy == null)
                {
                    taiSanNhatKy = new TaiSanNhatKy();
                    taiSanNhatKy.QUYET_DINH_TICH_THU_ID = item.ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
                }
                else
                {
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
                //đồng bộ quyết định tịch thu sang kho
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateQuyetDinhTichThu", new List<QuyetDinhTichThuDongBoModel>() { item.ToModel<QuyetDinhTichThuDongBoModel>() }, _workContext.CurrentDonVi.ID, (int)enumLevelQueueProcessDB.HIGH);
                //tài sản toàn dân
                var listTSTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
                if (listTSTD.Count() > 0)
                {
                    //đồng bộ tài sản toàn dân sang kho
                    _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateTaiSanTd", listTSTD.Select(c => c.ToModel<TaiSanTdDongBoModel>()), _workContext.CurrentDonVi.ID);
                }
                return item;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}

