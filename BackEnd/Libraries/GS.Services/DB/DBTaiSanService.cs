//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.DB;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Services.SHTD;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.Common;
using GS.Core.Domain.Api;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.NghiepVu;
using System.IO;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.FileProviders;
using System.Text;

namespace GS.Services.DB
{
    public partial class DBTaiSanService : IDBTaiSanService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DBTaiSan> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IXuLyService _xuLyService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        private readonly IDonViService _donViService;
        private readonly ITaiSanService _taiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly INhanXeService _nhanXeService;
        private readonly IChucDanhService _chucDanhService;
        private readonly ITaiSanDatService _taiSanDatService;
        private readonly ITaiSanNhaService _taiSanNhaService;
        private readonly ITaiSanOtoService _taiSanOtoService;
        private readonly ITaiSanVktService _taiSanVktService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly ITaiSanMayMocService _taiSanMayMocService;
        private readonly ITaiSanClnService _taiSanClnService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IYeuCauService _yeuCauService;
        private readonly IYeuCauChiTietService _yeuCauChiTietService;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IHinhThucXuLyService _hinhThucXuLyServiceService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly ITaiSanHienTrangSuDungService _taiSanHienTrangSuDungService;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly IGSFileProvider _fileProvider;
        #endregion

        #region Ctor

        public DBTaiSanService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DBTaiSan> itemRepository,
            IWorkContext workContext,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            ITaiSanTdService taiSanTdService,
            IXuLyService xuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ILoaiTaiSanService loaiTaiSanService,
            IQuocGiaService quocGiaService,
            ILyDoBienDongService lyDoBienDongService,
            IDonViService donViService,
            ITaiSanService taiSanService,
            IDiaBanService diaBanService,
            INhanXeService nhanXeService,
            IChucDanhService chucDanhService,
            ITaiSanClnService taiSanClnService,
            ITaiSanMayMocService taiSanMayMocService,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanVktService taiSanVktService,
            ITaiSanOtoService taiSanOtoService,
            ITaiSanNhaService taiSanNhaService,
            ITaiSanDatService taiSanDatService,
            IDonViBoPhanService donViBoPhanService,
            INguoiDungService nguoiDungService,
            IYeuCauService yeuCauService,
            IYeuCauChiTietService yeuCauChiTietService,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IMucDichSuDungService mucDichSuDungService,
            IHinhThucXuLyService hinhThucXuLyServiceService,
            IBienDongService bienDongService,
            IBienDongChiTietService bienDongChiTietService,
            ITaiSanNguonVonService taiSanNguonVonService,
            ITaiSanHienTrangSuDungService taiSanHienTrangSuDungService,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IPhuongAnXuLyService phuongAnXuLyService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IGSFileProvider fileProvider
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._xuLyService = xuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._quocGiaService = quocGiaService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._donViService = donViService;
            this._taiSanService = taiSanService;
            this._diaBanService = diaBanService;
            this._nhanXeService = nhanXeService;
            this._chucDanhService = chucDanhService;
            this._taiSanClnService = taiSanClnService;
            this._taiSanMayMocService = taiSanMayMocService;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanVktService = taiSanVktService;
            this._taiSanOtoService = taiSanOtoService;
            this._taiSanNhaService = taiSanNhaService;
            this._taiSanDatService = taiSanDatService;
            this._donViBoPhanService = donViBoPhanService;
            this._nguoiDungService = nguoiDungService;
            this._yeuCauChiTietService = yeuCauChiTietService;
            this._yeuCauService = yeuCauService;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._hinhThucXuLyServiceService = hinhThucXuLyServiceService;
            this._bienDongService = bienDongService;
            this._taiSanNguonVonService = taiSanNguonVonService;
            this._taiSanHienTrangSuDungService = taiSanHienTrangSuDungService;
            this._bienDongChiTietService = bienDongChiTietService;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._fileProvider = fileProvider;
        }

        #endregion

        #region Methods
        /// <summary>
        /// lấy all tài sản cần đồng bộ
        /// 
        /// </summary>
        /// <param name="IsTaiSanToanDan"> true nếu muốn lấy tài sản toàn dân</param>
        /// <returns></returns>
        public virtual IList<DBTaiSan> GetAllTaiSans(bool IsTaiSanToanDan = false, decimal TrangThaiId = 0, List<decimal> ListTrangThai = null, int? TakeNumber = null, String maDonVi = null)
        {
            var query = _itemRepository.Table;
            if (IsTaiSanToanDan)
                query = query.Where(m => m.IS_TAI_SAN_TOAN_DAN == true);
            if (TrangThaiId > 0)
            {
                query = query.Where(m => m.TRANG_THAI_ID == TrangThaiId);
            }
            if (ListTrangThai != null && ListTrangThai.Count > 0)
            {
                query = query.Where(m => ListTrangThai.Contains(m.TRANG_THAI_ID.GetValueOrDefault(0)));
            }
            if (!String.IsNullOrEmpty(maDonVi))
                query = query.Where(c => c.QLDKTS_MA.StartsWith(maDonVi));
            if (TakeNumber.GetValueOrDefault(0) > 0)
                return query.Take(TakeNumber.Value).ToList();
            else
                return query.ToList();
        }

        public virtual IPagedList<DBTaiSan> SearchTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donViId = null, string MaTaiSan = null, string MaTaiSanDB = null, decimal? LoaiHinhTaiSan = null, decimal? LoaiTaiSan = null, decimal? TrangThai = null, DateTime? NgayDB = null)
        {
            var query = _itemRepository.Table;
            //if(donViId.GetValueOrDefault(0)!=0)
            //    query = query.Where(c=>c.)
            if (!string.IsNullOrEmpty(MaTaiSan))
            {
                MaTaiSan = MaTaiSan.ToUpper().Trim();
                query = query.Where(c => c.QLDKTS_MA != null && c.QLDKTS_MA.Contains(MaTaiSan));
            }
            if (!string.IsNullOrEmpty(MaTaiSanDB))
            {
                MaTaiSanDB = MaTaiSanDB.ToUpper().Trim();
                query = query.Where(c => c.DB_MA != null && c.DB_MA.Contains(MaTaiSanDB));
            }
            if (LoaiHinhTaiSan.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            if (LoaiTaiSan.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.LOAI_TAI_SAN_ID == LoaiTaiSan);
            if (TrangThai != null)
                query = query.Where(c => c.TRANG_THAI_ID == TrangThai);
            if (NgayDB != null)
            {
                string day = NgayDB.Value.ToString(CommonHelper.DATE_FORMAT_VN);
                query = query.Where(c => c.NGAY_DONG_BO.ToString(CommonHelper.DATE_FORMAT_VN) == day);
            }

            return new PagedList<DBTaiSan>(query.OrderByDescending(c => c.NGAY_DONG_BO), pageIndex, pageSize); ;
        }
        public virtual DBTaiSan GetTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual DBTaiSan GetTaiSanByMa(string QLDKTS_MA = null, string DB_MA = null, string DON_VI_DONG_BO_ID = null,decimal? nguonTaiSanId=null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(QLDKTS_MA))
                query = query.Where(c => c.QLDKTS_MA == QLDKTS_MA);
            if (!string.IsNullOrEmpty(DB_MA))
                query = query.Where(c => c.DB_MA == DB_MA);
            if (nguonTaiSanId.HasValue)
                query = query.Where(c => c.PHAN_MEM_DONG_BO_ID == nguonTaiSanId);
            //if (!string.IsNullOrEmpty(DON_VI_DONG_BO_ID))
            //    query = query.Where(c => c.DON_VI_DONG_BO_ID == DON_VI_DONG_BO_ID);
            return query.FirstOrDefault();
        }

        public virtual IList<DBTaiSan> GetTaiSanByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSan(DBTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.GUID = Guid.NewGuid();
            entity.NGAY_DONG_BO = DateTime.Now;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void InsertTaiSan(List<DBTaiSan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSan(DBTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            //entity.NGAY_DONG_BO = DateTime.Now;
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateTaiSan(List<DBTaiSan> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            //entity.NGAY_DONG_BO = DateTime.Now;
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSan(DBTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        //public virtual MessageReturn InsertOrUpdateTaiSanNhaNuoc(TaiSanDongBoApi taiSanApi)
        //{
        //    string filePath = GSDataSettingsDefaults.FolderWorkFiles + $"/LogUpdateTaiSan_{taiSanApi.MA_TAI_SAN}_({DateTime.Now.ToString("dd-MM-yyyy hh-mm")}).txt";
        //    filePath = _fileProvider.MapPath(filePath);
        //    _fileProvider.CreateFile(filePath);
        //    StringBuilder logs = new StringBuilder();
        //    var watch = new System.Diagnostics.Stopwatch();
        //    watch.Start();
        //    logs.AppendLine("begin");
        //    #region InsertTaiSan
        //    //TaiSan taiSan = _taiSanService.GetTaiSanByMa(taiSanApi.MA_TAI_SAN);
        //    //taiSan = taiSan ?? _taiSanService.GetTaiSanByMaDB(taiSanApi.MA_TAI_SAN);
        //    TaiSan taiSan = _taiSanService.GetTaiSanByMaDB(taiSanApi.MA_TAI_SAN);
        //    if (taiSan != null)
        //    {
        //        return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        //        //var bds = _bienDongService.GetBienDongsByTaiSanId(taiSanId: taiSan.ID);
        //        //int countBienDong = bds != null ? bds.Count() : 0;
        //        //var nvs = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: taiSan.ID);
        //        //int countNguonVon = nvs != null ? nvs.Count() : 0;
        //        //var hts = _taiSanHienTrangSuDungService.GetTaiSanHienTrangSuDungByTaiSanId(taiSanId: taiSan.ID);
        //        //int countHienTrang = hts != null ? hts.Count() : 0;
        //        //if (countBienDong == (taiSanApi.LST_BIEN_DONG != null ? taiSanApi.LST_BIEN_DONG.Count : 0)
        //        //    && countNguonVon == (taiSanApi.LST_NGUON_VON != null ? taiSanApi.LST_NGUON_VON.Count : 0)
        //        //    && countHienTrang == (taiSanApi.LST_HIEN_TRANG != null ? taiSanApi.LST_HIEN_TRANG.Count : 0))
        //        //    return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        //        //var result = PrepareTaiSanDongBo(taiSan, taiSanApi);
        //        //if (taiSan == null || result.Code != MessageReturn.SUCCESS_CODE)
        //        //    return result;
        //        //_taiSanService.UpdateTaiSan(taiSan);
        //    }
        //    else
        //    {
        //        taiSan = new TaiSan();
        //        //taiSan.GUID = Guid.NewGuid();
        //        var result = PrepareTaiSanDongBo(taiSan, taiSanApi);
        //        if (taiSan == null || result.Code != MessageReturn.SUCCESS_CODE)
        //        {
        //            return result;
        //        }
        //        //taiSan.DB_MA = taiSanApi.DB_MA = taiSanApi.MA_TAI_SAN;
        //        taiSan.NGAY_NHAP = taiSanApi.NGAY_NHAP ?? DateTime.Now;
        //        _taiSanService.InsertTaiSan(taiSan, true);
        //        //_taiSanService.InsertTaiSanByProcedure(taiSan, true);
        //        TaiSanNhatKy taiSanNhatKy = new TaiSanNhatKy();
        //        taiSanNhatKy.TAI_SAN_ID = taiSan.ID;
        //        taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
        //        //taiSanNhatKy.IS_NHAT_KY_CUOI = true;
        //        taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
        //        taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = false;
        //        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
        //        _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
        //        // update Mã tài sản
        //        if (taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
        //        {
        //            // get tài sản vô hình  gốc
        //            decimal? parentId = taiSan.LOAI_TAI_SAN_DON_VI_ID;
        //            LoaiTaiSanDonVi taiSanVoHinh = new LoaiTaiSanDonVi();
        //            do
        //            {
        //                taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
        //                parentId = taiSanVoHinh.PARENT_ID;
        //            } while (parentId != null);
        //            //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
        //            taiSan.MA = CommonHelper.GenMaTaiSan(taiSanApi.MA_DON_VI, taiSanVoHinh.MA, taiSan.ID);
        //        }
        //        else
        //            taiSan.MA = CommonHelper.GenMaTaiSan(taiSanApi.MA_DON_VI, taiSanApi.MA_LOAI_TAI_SAN, taiSan.ID);
        //        taiSanApi.MA_TAI_SAN = taiSan.MA;
        //        _taiSanService.UpdateTaiSan(taiSan);
        //        // nhật ký tài sản                             
        //    }
        //    taiSanApi.MA_TAI_SAN = taiSan.MA;
        //    #endregion
        //    try
        //    {
        //        #region  tài sản
        //        if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
        //        {
        //            if (taiSanApi.TS_DAT == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_DAT");
        //            TaiSanDat taiSanDat = _taiSanDatService.GetTaiSanDatByTaiSanId(taiSan.ID);
        //            if (taiSanDat == null)
        //            {
        //                taiSanDat = new TaiSanDat();
        //                var result = PrepareTaiSanDat(taiSanDat, taiSanApi);
        //                if (taiSanDat == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                taiSanDat.TAI_SAN_ID = taiSan.ID;
        //                _taiSanDatService.InsertTaiSanDat(taiSanDat);
        //            }
        //            else
        //            {
        //                var result = PrepareTaiSanDat(taiSanDat, taiSanApi);
        //                if (taiSanDat == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                _taiSanDatService.UpdateTaiSanDat(taiSanDat);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV)
        //        {
        //            if (taiSanApi.TS_CLN == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_CLN");
        //            if (taiSanApi.TS_CLN.NAM_SINH == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_CLN-NAM_SINH");
        //            TaiSanCln taiSanCln = _taiSanClnService.GetTaiSanClnByTaiSanId(taiSan.ID);
        //            if (taiSanCln == null)
        //            {
        //                taiSanCln = new TaiSanCln();
        //                taiSanCln.TAI_SAN_ID = taiSan.ID;
        //                taiSanCln.NAM_SINH = taiSanApi.TS_CLN.NAM_SINH;
        //                _taiSanClnService.InsertTaiSanCln(taiSanCln);
        //            }
        //            else
        //            {
        //                taiSanCln.NAM_SINH = taiSanApi.TS_CLN.NAM_SINH;
        //                _taiSanClnService.UpdateTaiSanCln(taiSanCln);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
        //        {
        //            if (taiSanApi.TS_NHA == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA");
        //            TaiSanNha taiSanNha = _taiSanNhaService.GetTaiSanNhaByTaiSanId(taiSan.ID);
        //            if (taiSanApi.TS_NHA == null)
        //                return new MessageReturn(MessageReturn.ERROR, "TS_NHA");
        //            if (taiSanNha == null)
        //            {
        //                taiSanNha = new TaiSanNha();
        //                var result = PrepareTaiSanNha(taiSanNha, taiSanApi);
        //                if (taiSanNha == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                taiSanNha.TAI_SAN_ID = taiSan.ID;
        //                _taiSanNhaService.InsertTaiSanNha(taiSanNha);
        //            }
        //            else
        //            {
        //                var result = PrepareTaiSanNha(taiSanNha, taiSanApi);
        //                if (taiSanNha == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                _taiSanNhaService.UpdateTaiSanNha(taiSanNha);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.OTO)
        //        {
        //            if (taiSanApi.TS_OTO == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_OTO");
        //            TaiSanOto taiSanOto = _taiSanOtoService.GetTaiSanOtoByTaiSanId(taiSan.ID);
        //            if (taiSanOto == null)
        //            {
        //                taiSanOto = new TaiSanOto();
        //                var result = PrepareTaiSanOto(taiSanOto, taiSanApi);
        //                if (taiSanOto == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                taiSanOto.TAI_SAN_ID = taiSan.ID;
        //                _taiSanOtoService.InsertTaiSanOto(taiSanOto);
        //            }
        //            else
        //            {
        //                var result = PrepareTaiSanOto(taiSanOto, taiSanApi);
        //                if (taiSanOto == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                _taiSanOtoService.UpdateTaiSanOto(taiSanOto);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
        //        {
        //            if (taiSanApi.TS_PTK == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_PTK");
        //            TaiSanOto taiSanPtk = _taiSanOtoService.GetTaiSanOtoByTaiSanId(taiSan.ID);
        //            if (taiSanPtk == null)
        //            {
        //                taiSanPtk = new TaiSanOto();
        //                var result = PrepareTaiSanPTK(taiSanPtk, taiSanApi);
        //                if (taiSanPtk == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                taiSanPtk.TAI_SAN_ID = taiSan.ID;
        //                _taiSanOtoService.InsertTaiSanOto(taiSanPtk);
        //            }
        //            else
        //            {
        //                var result = PrepareTaiSanPTK(taiSanPtk, taiSanApi);
        //                if (taiSanPtk == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                    return result;
        //                _taiSanOtoService.UpdateTaiSanOto(taiSanPtk);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
        //        {
        //            if (taiSanApi.TS_VKT == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_VKT");
        //            if (taiSanApi.TS_VKT.DIEN_TICH == null && taiSanApi.TS_VKT.THE_TICH == null && taiSanApi.TS_VKT.CHIEU_DAI == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_VKT-CHIEU_DAI; TS_VKT-THE_TICH; TS_VKT-DIEN_TICH");
        //            TaiSanVkt taiSanVkt = _taiSanVktService.GetTaiSanVktByTaiSanId(taiSan.ID);
        //            if (taiSanVkt == null)
        //            {
        //                taiSanVkt = new TaiSanVkt();
        //                taiSanVkt.TAI_SAN_ID = taiSan.ID;
        //                taiSanVkt.DIEN_TICH = taiSanApi.TS_VKT.DIEN_TICH;
        //                taiSanVkt.THE_TICH = taiSanApi.TS_VKT.THE_TICH;
        //                taiSanVkt.CHIEU_DAI = taiSanApi.TS_VKT.CHIEU_DAI;
        //                _taiSanVktService.InsertTaiSanVkt(taiSanVkt);
        //            }
        //            else
        //            {
        //                taiSanVkt.DIEN_TICH = taiSanApi.TS_VKT.DIEN_TICH;
        //                taiSanVkt.THE_TICH = taiSanApi.TS_VKT.THE_TICH;
        //                taiSanVkt.CHIEU_DAI = taiSanApi.TS_VKT.CHIEU_DAI;
        //                _taiSanVktService.UpdateTaiSanVkt(taiSanVkt);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU)
        //        {
        //            if (taiSanApi.TS_DAC_THU == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_DAC_THU");
        //            TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(taiSan.ID);
        //            if (taiSanMayMoc == null)
        //            {
        //                taiSanMayMoc = new TaiSanMayMoc();
        //                taiSanMayMoc.TAI_SAN_ID = taiSan.ID;
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_MAY_MOC.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_MAY_MOC.PHU_KIEN_JSON;
        //                _taiSanMayMocService.InsertTaiSanMayMoc(taiSanMayMoc);
        //            }
        //            else
        //            {
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_DAC_THU.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_DAC_THU.PHU_KIEN_JSON;
        //                _taiSanMayMocService.UpdateTaiSanMayMoc(taiSanMayMoc);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
        //        {
        //            if (taiSanApi.TS_HUU_HINH_KHAC == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_HUU_HINH");
        //            TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(taiSan.ID);
        //            if (taiSanMayMoc == null)
        //            {
        //                taiSanMayMoc = new TaiSanMayMoc();
        //                taiSanMayMoc.TAI_SAN_ID = taiSan.ID;
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_HUU_HINH_KHAC.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_HUU_HINH_KHAC.PHU_KIEN_JSON;
        //                _taiSanMayMocService.InsertTaiSanMayMoc(taiSanMayMoc);
        //            }
        //            else
        //            {
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_HUU_HINH_KHAC.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_HUU_HINH_KHAC.PHU_KIEN_JSON;
        //                _taiSanMayMocService.UpdateTaiSanMayMoc(taiSanMayMoc);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI)
        //        {
        //            if (taiSanApi.TS_MAY_MOC == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_MAY_MOC");
        //            TaiSanMayMoc taiSanMayMoc = _taiSanMayMocService.GetTaiSanMaymocByTaiSanId(taiSan.ID);
        //            if (taiSanMayMoc == null)
        //            {
        //                taiSanMayMoc = new TaiSanMayMoc();
        //                taiSanMayMoc.TAI_SAN_ID = taiSan.ID;
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_MAY_MOC.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_MAY_MOC.PHU_KIEN_JSON;
        //                _taiSanMayMocService.InsertTaiSanMayMoc(taiSanMayMoc);
        //            }
        //            else
        //            {
        //                taiSanMayMoc.THONG_SO_KY_THUAT = taiSanApi.TS_MAY_MOC.THONG_SO_KY_THUAT;
        //                taiSanMayMoc.PHU_KIEN_JSON = taiSanApi.TS_MAY_MOC.PHU_KIEN_JSON;
        //                _taiSanMayMocService.UpdateTaiSanMayMoc(taiSanMayMoc);
        //            }
        //        }
        //        else if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
        //        {
        //            if (taiSanApi.TS_VO_HINH == null)
        //                return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_VO_HINH");
        //            TaiSanVoHinh taiSanVoHinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(taiSan.ID);
        //            if (taiSanVoHinh == null)
        //            {
        //                taiSanVoHinh = new TaiSanVoHinh();
        //                taiSanVoHinh.TAI_SAN_ID = taiSan.ID;
        //                taiSanVoHinh.THONG_SO_KY_THUAT = taiSanApi.TS_VO_HINH.THONG_SO_KY_THUAT;
        //                _taiSanVoHinhService.InsertTaiSanVoHinh(taiSanVoHinh);
        //            }
        //            else
        //            {
        //                taiSanVoHinh.THONG_SO_KY_THUAT = taiSanApi.TS_VO_HINH.THONG_SO_KY_THUAT;
        //                _taiSanVoHinhService.UpdateTaiSanVoHinh(taiSanVoHinh);
        //            }
        //        }

        //        #endregion
        //        #region Delete All NguonVon
        //        var ListTaiSanNguonVonDel = _taiSanNguonVonService.GetTaiSanNguonVons(taisanId: taiSan.ID);
        //        if (ListTaiSanNguonVonDel != null && ListTaiSanNguonVonDel.Count > 0)
        //            _taiSanNguonVonService.DeleteTaiSanNguonVons(ListTaiSanNguonVonDel);
        //        #endregion
        //        #region Delete All HienTrang
        //        var ListHienTrangDel = _taiSanHienTrangSuDungService.GetHienTrangSuDungs(TaiSanId: taiSan.ID);
        //        if (ListHienTrangDel != null && ListHienTrangDel.Count > 0)
        //            _taiSanHienTrangSuDungService.DeleteTaiSanHienTrangSuDungs(ListHienTrangDel);

        //        #endregion
        //        #region Delete All BienDong
        //        var ListBienDongDel = _bienDongService.GetBienDongsByTaiSanId(taiSanId: taiSan.ID);
        //        if (ListBienDongDel != null && ListBienDongDel.Count > 0)
        //            _bienDongService.DeleteBienDongs(ListBienDongDel);
        //        #endregion
        //        #region Biến động
        //        IEnumerable<TaiSanHienTrangSuDungApi> ListHienTrang = new List<TaiSanHienTrangSuDungApi>();
        //        IEnumerable<TaiSanNguonVonApi> ListNguonVon = new List<TaiSanNguonVonApi>();
        //        List<ObjHienTrang_Entity> ListHienTrangObj = new List<ObjHienTrang_Entity>();
        //        HienTrangList_Entity HienTrangList = new HienTrangList_Entity();
        //        BienDong bienDong = new BienDong();
        //        BienDongChiTiet bienDongChiTiet = new BienDongChiTiet();
        //        List<TaiSanNguonVon> taiSanNguonVons = new List<TaiSanNguonVon>();
        //        List<TaiSanHienTrangSuDung> taiSanHienTrangs = new List<TaiSanHienTrangSuDung>();
        //        YeuCauChiTiet yeuCauChiTiet = new YeuCauChiTiet();
        //        foreach (var biendongApi in taiSanApi.LST_BIEN_DONG)
        //        {
        //            ListHienTrang = taiSanApi.LST_HIEN_TRANG.Where(c => c.BIEN_DONG_GUID == biendongApi.GUID);
        //            ListNguonVon = taiSanApi.LST_NGUON_VON.Where(m => m.BIEN_DONG_GUID == biendongApi.GUID);
        //            if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT && (biendongApi.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || biendongApi.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN))
        //                ListHienTrangObj = ListHienTrang.Select(m =>
        //                new ObjHienTrang_Entity()
        //                {
        //                    HienTrangId = m.HIEN_TRANG_ID,
        //                    GiaTriText = m.GIA_TRI_TEXT,
        //                    GiaTriNumber = -Math.Abs(m.GIA_TRI_NUMBER.GetValueOrDefault(0)),
        //                    GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
        //                    NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
        //                    TenHienTrang = m.TEN_HIEN_TRANG,
        //                    KieuDuLieuId = m.KIEU_DU_LIEU_ID
        //                }).ToList();
        //            else
        //                ListHienTrangObj = ListHienTrang.Select(m =>
        //                new ObjHienTrang_Entity()
        //                {
        //                    HienTrangId = m.HIEN_TRANG_ID,
        //                    GiaTriText = m.GIA_TRI_TEXT,
        //                    GiaTriNumber = m.GIA_TRI_NUMBER,
        //                    GiaTriCheckbox = m.GIA_TRI_CHECKBOX != null ? m.GIA_TRI_CHECKBOX.Value : false,
        //                    NhomHienTrangId = m.NHOM_HIEN_TRANG_ID,
        //                    TenHienTrang = m.TEN_HIEN_TRANG,
        //                    KieuDuLieuId = m.KIEU_DU_LIEU_ID
        //                }).ToList();
        //            HienTrangList = new HienTrangList_Entity()
        //            {
        //                TaiSanId = taiSan.ID,
        //                lstObjHienTrang = ListHienTrangObj
        //            };
        //            var dataNguonVon = ListNguonVon.Select(m => new
        //            {
        //                ID = m.NGUON_VON_ID,
        //                TEN = m.TEN_NGUON_VON,
        //                LoaiHinhTaiSanId = taiSan.LOAI_HINH_TAI_SAN_ID,
        //                GiaTri = m.GIA_TRI,
        //            });
        //            if (biendongApi.TRANG_THAI == 2)// đã duyệt
        //            {
        //                bienDong = _bienDongService.GetBienDongByID_DB(biendongApi.GUID.ToString());
        //                if (bienDong == null)
        //                {
        //                    bienDong = new BienDong();
        //                    var result = PrepareBienDong(bienDong, biendongApi, taiSan);
        //                    if (bienDong == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    bienDong.ID_DB = biendongApi.GUID.ToString();
        //                    bienDong.GUID = Guid.NewGuid();
        //                    _bienDongService.DB_InsertBienDong(bienDong);
        //                    biendongApi.ID = bienDong.ID;
        //                    // bien dong chi tiet
        //                    bienDongChiTiet = new BienDongChiTiet();
        //                    bienDongChiTiet.BIEN_DONG_ID = bienDong.ID;
        //                    result = PrepareBienDongChitiet(bienDongChiTiet, biendongApi);
        //                    // HTSD_JSON

        //                    bienDongChiTiet.HTSD_JSON = HienTrangList.toStringJson();
        //                    if (bienDongChiTiet == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    _bienDongChiTietService.InsertBienDongChiTiet(bienDongChiTiet, false);
        //                }
        //                else
        //                {
        //                    var result = PrepareBienDong(bienDong, biendongApi, taiSan);
        //                    if (bienDong == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    else
        //                    {
        //                        // bien dong chi tiet
        //                        bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDong.ID);
        //                        if (bienDongChiTiet == null)
        //                        {
        //                            bienDongChiTiet = new BienDongChiTiet();
        //                            bienDongChiTiet.BIEN_DONG_ID = bienDong.ID;
        //                            result = PrepareBienDongChitiet(bienDongChiTiet, biendongApi);
        //                            if (bienDongChiTiet == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                                return result;
        //                            bienDongChiTiet.HTSD_JSON = HienTrangList.toStringJson();
        //                            _bienDongChiTietService.InsertBienDongChiTiet(bienDongChiTiet, false);

        //                        }
        //                        else
        //                        {
        //                            result = PrepareBienDongChitiet(bienDongChiTiet, biendongApi);
        //                            if (bienDongChiTiet == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                                return result;

        //                            bienDongChiTiet.HTSD_JSON = HienTrangList.toStringJson();
        //                            _bienDongService.DB_UpdateBienDong(bienDong);
        //                            _bienDongChiTietService.UpdateBienDongChiTiet(bienDongChiTiet, false);
        //                        }

        //                        biendongApi.ID = bienDong.ID;
        //                    }
        //                }
        //                #region Insert Hiện trang sử dụng
        //                if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT && (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN))
        //                    taiSanHienTrangs = ListHienTrang.Select(c => new TaiSanHienTrangSuDung()
        //                    {
        //                        TAI_SAN_ID = taiSan.ID,
        //                        GIA_TRI_CHECKBOX = c.GIA_TRI_CHECKBOX,
        //                        GIA_TRI_TEXT = c.GIA_TRI_TEXT,
        //                        HIEN_TRANG_ID = c.HIEN_TRANG_ID,
        //                        KIEU_DU_LIEU_ID = c.KIEU_DU_LIEU_ID.GetValueOrDefault(1),
        //                        NHOM_HIEN_TRANG_ID = c.NHOM_HIEN_TRANG_ID,
        //                        TEN_HIEN_TRANG = c.TEN_HIEN_TRANG,
        //                        BIEN_DONG_ID = bienDong.ID,
        //                        GIA_TRI_NUMBER = -Math.Abs(c.GIA_TRI_NUMBER.GetValueOrDefault(0))
        //                    }).ToList();
        //                else
        //                    taiSanHienTrangs = ListHienTrang.Select(c => new TaiSanHienTrangSuDung()
        //                    {
        //                        TAI_SAN_ID = taiSan.ID,
        //                        GIA_TRI_CHECKBOX = c.GIA_TRI_CHECKBOX,
        //                        GIA_TRI_TEXT = c.GIA_TRI_TEXT,
        //                        HIEN_TRANG_ID = c.HIEN_TRANG_ID,
        //                        KIEU_DU_LIEU_ID = c.KIEU_DU_LIEU_ID.GetValueOrDefault(1),
        //                        NHOM_HIEN_TRANG_ID = c.NHOM_HIEN_TRANG_ID,
        //                        TEN_HIEN_TRANG = c.TEN_HIEN_TRANG,
        //                        BIEN_DONG_ID = bienDong.ID,
        //                        GIA_TRI_NUMBER = c.GIA_TRI_NUMBER
        //                    }).ToList();
        //                _taiSanHienTrangSuDungService.InsertTaiSanHienTrangSuDungs(taiSanHienTrangs);
        //                #endregion
        //                #region tài sản Nguon Von
        //                taiSanNguonVons = ListNguonVon.Select(c => new TaiSanNguonVon()
        //                {
        //                    TAI_SAN_ID = taiSan.ID,
        //                    NGUON_VON_ID = c.NGUON_VON_ID.Value,
        //                    GIA_TRI = c.GIA_TRI.GetValueOrDefault(0),
        //                    BIEN_DONG_ID = bienDong.ID,
        //                }).ToList();
        //                _taiSanNguonVonService.InsertTaiSanNguonVons(taiSanNguonVons);
        //                #endregion
        //            }
        //            else
        //            {
        //                YeuCau yeuCau = _yeuCauService.GetYeuCauByDB_Guid(biendongApi.GUID);
        //                if (yeuCau == null)
        //                {
        //                    yeuCau = new YeuCau();
        //                    var result = PrepareYeuCau(yeuCau, biendongApi, taiSan);
        //                    logs.AppendLine("end PrepareYeuCau: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    if (yeuCau == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    yeuCau.GUID = biendongApi.GUID;
        //                    // nguồn vốn Json

        //                    yeuCau.NGUON_VON_JSON = dataNguonVon.toStringJson();
        //                    logs.AppendLine("begin InsertYeuCau: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    _yeuCauService.InsertYeuCau(yeuCau);
        //                    logs.AppendLine("end InsertYeuCau: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    biendongApi.ID = yeuCau.ID;
        //                    // yeu cau chi tiet
        //                    yeuCauChiTiet = new YeuCauChiTiet();
        //                    yeuCauChiTiet.YEU_CAU_ID = yeuCau.ID;
        //                    logs.AppendLine("begin PrepareYeuCauChiTiet: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    result = PrepareYeuCauChiTiet(yeuCauChiTiet, biendongApi);
        //                    logs.AppendLine("end PrepareYeuCauChiTiet: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    yeuCauChiTiet.HTSD_JSON = HienTrangList.toStringJson();
        //                    if (yeuCauChiTiet == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    logs.AppendLine("begin InsertYeuCauChiTiet: Time: " + watch.ElapsedMilliseconds + " ms");
        //                    _yeuCauChiTietService.InsertYeuCauChiTiet(yeuCauChiTiet);
        //                    logs.AppendLine("end InsertYeuCauChiTiet: Time: " + watch.ElapsedMilliseconds + " ms");
        //                }
        //                else
        //                {
        //                    var result = PrepareYeuCau(yeuCau, biendongApi, taiSan);
        //                    if (yeuCau == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                        return result;
        //                    else
        //                    {
        //                        // nguồn vốn Json
        //                        yeuCau.NGUON_VON_JSON = dataNguonVon.toStringJson();
        //                        // bien dong chi tiet
        //                        yeuCauChiTiet = _yeuCauChiTietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
        //                        if (yeuCauChiTiet == null)
        //                            return new MessageReturn(MessageReturn.ERROR, "Lỗi xử lý");
        //                        result = PrepareYeuCauChiTiet(yeuCauChiTiet, biendongApi);
        //                        if (yeuCauChiTiet == null || result.Code != MessageReturn.SUCCESS_CODE)
        //                            return result;
        //                        _yeuCauService.UpdateYeuCau(yeuCau);
        //                        _yeuCauChiTietService.UpdateYeuCauChiTiet(yeuCauChiTiet);
        //                        biendongApi.ID = yeuCau.ID;
        //                    }
        //                }
        //            }
        //        }
        //        #endregion
        //    }
        //    catch
        //    {

        //        return new MessageReturn(MessageReturn.ERROR, "Lỗi xử lý");
        //    }
        //    watch.Stop();
        //    logs.AppendLine("end: Time: " + watch.ElapsedMilliseconds + " ms");
        //    _fileProvider.WriteAllText(filePath, logs.ToString(), Encoding.UTF8);
        //    return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        //}
        public MessageReturn DelelteTaiSanDB(string MaTaiSan)
        {
            if (string.IsNullOrEmpty(MaTaiSan))
                return MessageReturn.CreateNotFoundMessage("MA_DB");
            try
            {
                OracleParameter p_MA_DB = new OracleParameter("p_MA_DB", OracleDbType.Varchar2, MaTaiSan, ParameterDirection.Input);
                var result = _dbContext.ExecuteSqlCommand("BEGIN DEL_TAI_SAN_DONG_BO(:p_MA_DB); END;", false, null, p_MA_DB);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }
        public MessageReturn PrepareTaiSanDongBo(TaiSan taiSan = null, TaiSanDongBoApi taiSanApi = null)
        {

            if (taiSanApi != null)
            {
                taiSan = taiSan ?? new TaiSan();
                //if (string.IsNullOrEmpty(taiSan.MA))
                //{
                //    taiSan = null;
                //    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_TAI_SAN");
                //}
                taiSan.TEN = taiSanApi.TEN_TAI_SAN;
                taiSan.MA_DB = taiSanApi.DB_MA = taiSanApi.MA_TAI_SAN;
                if (string.IsNullOrEmpty(taiSanApi.MA_LOAI_TAI_SAN))
                    if (string.IsNullOrEmpty(taiSanApi.LOAI_TAI_SAN_TEN))
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_TAI_SAN_TEN");
                    else
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_LOAI_TAI_SAN");


                //if (string.IsNullOrEmpty(taiSanApi.MA_LOAI_TAI_SAN))
                //{
                //    //ErrCount++;
                //    taiSan = null;
                //    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_LOAI_TAI_SAN");
                //}
                else
                {
                    if (taiSanApi.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
                    {

                        if (string.IsNullOrEmpty(taiSanApi.MA_LOAI_TAI_SAN))
                            return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_LOAI_TAI_SAN");
                        if (string.IsNullOrEmpty(taiSanApi.LOAI_TAI_SAN_TEN))
                            return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_TAI_SAN_VO_HINH_MA");
                        var loaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaLTS(taiSanApi.MA_LOAI_TAI_SAN);
                        if (loaiTaiSanVoHinh == null)
                            loaiTaiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByTen(taiSanApi.LOAI_TAI_SAN_TEN.Trim());
                        if (loaiTaiSanVoHinh != null)
                            taiSan.LOAI_TAI_SAN_DON_VI_ID = loaiTaiSanVoHinh.ID;
                        else
                        {
                            taiSan = null;
                            return new MessageReturn(MessageReturn.ERROR_CODE, "LOAI_TAI_SAN_VO_HINH_MA");
                        }
                    }
                    else
                    {
                        var loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(taiSanApi.MA_LOAI_TAI_SAN);
                        if (loaiTaiSan == null)
                            loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByTen(taiSanApi.LOAI_TAI_SAN_TEN);
                        //var loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(taiSanApi.MA_LOAI_TAI_SAN);
                        if (loaiTaiSan != null)
                            taiSan.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                        else
                        {
                            taiSan = null;
                            return new MessageReturn(MessageReturn.ERROR_CODE, "MA_LOAI_TAI_SAN");
                        }
                    }
                }
                if (string.IsNullOrEmpty(taiSanApi.MA_DON_VI))
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_DON_VI");
                else
                {
                    var donVi = _donViService.GetCacheDonViByMa(taiSanApi.MA_DON_VI);
                    if (donVi != null)
                        taiSan.DON_VI_ID = donVi.ID;
                    else
                        return new MessageReturn(MessageReturn.ERROR_CODE, "MA_DON_VI");
                }
                if (taiSanApi.LOAI_HINH_TAI_SAN_ID == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "LOAI_HINH_TAI_SAN_ID");
                else
                    taiSan.LOAI_HINH_TAI_SAN_ID = taiSanApi.LOAI_HINH_TAI_SAN_ID;
                // kiểm tra trạng thái tài sản
                var ListBienDongDaDuyet = taiSanApi.LST_BIEN_DONG.Where(m => m.TRANG_THAI == 2);//bien dongda duoc duyet
                if (ListBienDongDaDuyet.Count() > 0)
                    taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                else
                    taiSan.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
                taiSan.QUYET_DINH_SO = taiSanApi.QUYET_DINH_SO;
                taiSan.QUYET_DINH_NGAY = taiSanApi.QUYET_DINH_NGAY;
                if (!string.IsNullOrEmpty(taiSanApi.NUOC_SAN_XUAT_MA))
                {
                    var NuocSanXuat = _quocGiaService.GetQuocGiaDB(Ma: taiSanApi.NUOC_SAN_XUAT_MA);
                    if (NuocSanXuat != null)
                        taiSan.NUOC_SAN_XUAT_ID = NuocSanXuat.ID;
                    else
                        taiSan.NUOC_SAN_XUAT_ID = null;
                    //return new MessageReturn(MessageReturn.ERROR_CODE, "NUOC_SAN_XUAT_MA");
                }

                if (string.IsNullOrEmpty(taiSanApi.LY_DO_BIEN_DONG_MA))
                    if (string.IsNullOrEmpty(taiSanApi.LY_DO_BIEN_DONG_TEN))
                    {
                        taiSan = null;
                        return new MessageReturn(MessageReturn.ERROR_CODE, "LY_DO_BIEN_DONG_MA");
                    }
                var LyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByMa(taiSanApi.LY_DO_BIEN_DONG_MA, (int)taiSanApi.LOAI_HINH_TAI_SAN_ID.Value);
                if (LyDoBienDong == null)
                    LyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByTEN_LOAI_HINH_TS(taiSanApi.LY_DO_BIEN_DONG_TEN, (int)taiSanApi.LOAI_HINH_TAI_SAN_ID.Value);
                if (LyDoBienDong != null)
                    taiSan.LY_DO_BIEN_DONG_ID = LyDoBienDong.ID;
                else
                {
                    taiSan = null;
                    return new MessageReturn(MessageReturn.ERROR_CODE, "LY_DO_BIEN_DONG_MA");
                }
                taiSan.NAM_SAN_XUAT = taiSanApi.NAM_SAN_XUAT;
                if (taiSanApi.NGAY_NHAP == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "NGAY_NHAP");
                else
                    taiSan.NGAY_NHAP = taiSanApi.NGAY_NHAP;
                if (taiSanApi.NGAY_SU_DUNG == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "NGAY_SU_DUNG");
                else
                    taiSan.NGAY_SU_DUNG = taiSanApi.NGAY_SU_DUNG;
                taiSan.GHI_CHU = taiSanApi.GHI_CHU;
                taiSan.CHUNG_TU_NGAY = taiSanApi.CHUNG_TU_NGAY;
                taiSan.CHUNG_TU_SO = taiSanApi.CHUNG_TU_SO;
                taiSan.NGUYEN_GIA_BAN_DAU = taiSanApi.NGUYEN_GIA_BAN_DAU;
                // nếu nguyên giá ban đầu null thì lấy nguyên giá của biến động tăng mới đầu tiên
                if (taiSan.NGUYEN_GIA_BAN_DAU == null)
                {
                    var BienDongDauTien = taiSanApi.LST_BIEN_DONG.Where(m => m.LOAI_BIEN_DONG_ID == (decimal)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO).OrderBy(m => m.NGAY_BIEN_DONG).FirstOrDefault();
                    taiSan.NGUYEN_GIA_BAN_DAU = BienDongDauTien.NGUYEN_GIA;
                }
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
            }
            else
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");

        }
        public MessageReturn PrepareTaiSanDat(TaiSanDat taiSanDat = null, TaiSanDongBoApi taiSanApi = null)
        {
            if (taiSanApi != null)
            {
                if (taiSanDat == null)
                {
                    taiSanDat = new TaiSanDat();
                }
                taiSanDat.DIA_CHI = taiSanApi.TS_DAT.DIA_CHI;
                taiSanDat.DIEN_TICH = taiSanApi.TS_DAT.DIEN_TICH;
                //taiSanDat.HS_QUYET_DINH_GIAO_SO = taiSanApi.TS_DAT.HS_QUYET_DINH_GIAO_SO;
                //taiSanDat.HS_QUYET_DINH_GIAO_NGAY = taiSanApi.TS_DAT.HS_QUYET_DINH_GIAO_NGAY;
                //taiSanDat.HS_QUYET_DINH_CHO_THUE_SO = taiSanApi.TS_DAT.HS_QUYET_DINH_CHO_THUE_SO;
                //taiSanDat.HS_QUYET_DINH_CHO_THUE_NGAY = taiSanApi.TS_DAT.HS_QUYET_DINH_CHO_THUE_NGAY;
                //taiSanDat.HS_CNQSD_SO = taiSanApi.TS_DAT.HS_CNQSD_SO;
                //taiSanDat.HS_CNQSD_NGAY = taiSanApi.TS_DAT.HS_CNQSD_NGAY;
                //taiSanDat.HS_CHUYEN_NHUONG_SD_SO = taiSanApi.TS_DAT.HS_CHUYEN_NHUONG_SD_SO;
                //taiSanDat.HS_CHUYEN_NHUONG_SD_NGAY = taiSanApi.TS_DAT.HS_CHUYEN_NHUONG_SD_NGAY;
                if (!string.IsNullOrEmpty(taiSanApi.TS_DAT.DIA_BAN_MA))
                {
                    var DiaBan = _diaBanService.GetCacheDiaBanByMa(taiSanApi.TS_DAT.DIA_BAN_MA);
                    if (DiaBan != null)
                    {
                        taiSanDat.DIA_BAN_ID = DiaBan.ID;
                    }
                    else
                    {
                        taiSanDat.DIA_BAN_ID = null;
                        //taiSanDat = null;
                        //return new MessageReturn(MessageReturn.ERROR_CODE, "TS_DAT-DIA_BAN_MA");

                    }
                }
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
            }
            else
            {
                taiSanDat = null;
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");
            }
        }
        public MessageReturn PrepareTaiSanNha(TaiSanNha taiSanNha = null, TaiSanDongBoApi taiSanApi = null)
        {
            try
            {
                if (taiSanApi != null)
                {
                    if (taiSanApi.TS_NHA.DIEN_TICH_SAN_XAY_DUNG == null)
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA-DIEN_TICH_SAN_XAY_DUNG");
                    if (taiSanApi.TS_NHA.NHA_SO_TANG == null)
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA-NHA_SO_TANG");
                    if (taiSanApi.TS_NHA.NAM_XAY_DUNG == null)
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA-NAM_XAY_DUNG");
                    if (!string.IsNullOrEmpty(taiSanApi.TS_NHA.TAI_SAN_DAT_MA))
                    {
                        var tsDat = _taiSanService.GetTaiSanByMaDB(taiSanApi.TS_NHA.TAI_SAN_DAT_MA);
                        if (tsDat != null)
                            taiSanNha.TAI_SAN_DAT_ID = tsDat.ID;
                        else
                            return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA-TS_DAT_NOT_FOUND");
                    }
                    else
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_NHA-TAI_SAN_DAT_MA");


                    if (taiSanApi.TS_NHA.DIA_CHI == null)
                        taiSanNha.DIA_CHI = "Việt Nam";

                    if (taiSanNha == null)
                        taiSanNha = new TaiSanNha();
                    taiSanNha.DIEN_TICH_XAY_DUNG = taiSanApi.TS_NHA.DIEN_TICH_XAY_DUNG;
                    taiSanNha.DIEN_TICH_SAN_XAY_DUNG = taiSanApi.TS_NHA.DIEN_TICH_SAN_XAY_DUNG;
                    taiSanNha.NAM_XAY_DUNG = taiSanApi.TS_NHA.NAM_XAY_DUNG;
                    taiSanNha.NGAY_SU_DUNG = taiSanApi.TS_NHA.NGAY_SU_DUNG;
                    taiSanNha.DIA_CHI = taiSanApi.TS_NHA.DIA_CHI;

                    return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
                }
                else
                {
                    taiSanNha = null;
                    return new MessageReturn(MessageReturn.ERROR, "Tài sản");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public MessageReturn PrepareTaiSanOto(TaiSanOto taiSanOto = null, TaiSanDongBoApi taiSanApi = null)
        {
            if (taiSanApi != null)
            {
                if (taiSanOto == null)
                {
                    taiSanOto = new TaiSanOto();
                }
                if (string.IsNullOrEmpty(taiSanApi.TS_OTO.BIEN_KIEM_SOAT))
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_OTO-BIEN_KIEM_SOAT");
                taiSanOto.BIEN_KIEM_SOAT = taiSanApi.TS_OTO.BIEN_KIEM_SOAT;
                if (taiSanApi.TS_OTO.SO_CHO_NGOI == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_OTO-SO_CHO_NGOI");
                taiSanOto.SO_CHO_NGOI = taiSanApi.TS_OTO.SO_CHO_NGOI;
                taiSanOto.DUNG_TICH = taiSanApi.TS_OTO.DUNG_TICH;
                if (taiSanApi.TS_OTO.TAI_TRONG == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_OTO-TAI_TRONG");
                taiSanOto.TAI_TRONG = taiSanApi.TS_OTO.TAI_TRONG;
                taiSanOto.CONG_XUAT = taiSanApi.TS_OTO.CONG_XUAT;
                taiSanOto.SO_KHUNG = taiSanApi.TS_OTO.SO_KHUNG;
                taiSanOto.SO_MAY = taiSanApi.TS_OTO.SO_MAY;
                if (!string.IsNullOrEmpty(taiSanApi.TS_OTO.NHAN_XE_MA))
                {
                    var NhanXe = _nhanXeService.GetNhanXeByMaTen(taiSanApi.TS_OTO.NHAN_XE_MA);
                    if (NhanXe != null)
                        taiSanOto.NHAN_XE_ID = NhanXe.ID;
                }
                if (!string.IsNullOrEmpty(taiSanApi.TS_OTO.CHUC_DANH_MA))
                {
                    var ChucDanh = _chucDanhService.GetChucDanhByMa(taiSanApi.TS_OTO.CHUC_DANH_MA);
                    if (ChucDanh != null)
                        taiSanOto.CHUC_DANH_ID = ChucDanh.ID;
                }
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
            }
            else
            {
                taiSanOto = null;
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");
            }
        }
        public MessageReturn PrepareTaiSanPTK(TaiSanOto taiSanOto = null, TaiSanDongBoApi taiSanApi = null)
        {
            if (taiSanApi != null)
            {
                if (taiSanOto == null)
                    taiSanOto = new TaiSanOto();
                taiSanOto.BIEN_KIEM_SOAT = taiSanApi.TS_PTK.BIEN_KIEM_SOAT;
                if (taiSanApi.TS_PTK.SO_CHO_NGOI == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_PTK-SO_CHO_NGOI");
                taiSanOto.SO_CHO_NGOI = taiSanApi.TS_PTK.SO_CHO_NGOI;
                taiSanOto.DUNG_TICH = taiSanApi.TS_PTK.DUNG_TICH;
                if (taiSanApi.TS_PTK.TAI_TRONG == null)
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TS_PTK-TAI_TRONG");
                taiSanOto.TAI_TRONG = taiSanApi.TS_PTK.TAI_TRONG;
                taiSanOto.CONG_XUAT = taiSanApi.TS_PTK.CONG_XUAT;
                taiSanOto.SO_KHUNG = taiSanApi.TS_PTK.SO_KHUNG;
                taiSanOto.SO_MAY = taiSanApi.TS_PTK.SO_MAY;
                if (!string.IsNullOrEmpty(taiSanApi.TS_PTK.NHAN_XE_MA))
                {
                    var NhanXe = _nhanXeService.GetNhanXeByMaTen(taiSanApi.TS_PTK.NHAN_XE_MA);
                    if (NhanXe != null)
                        taiSanOto.NHAN_XE_ID = NhanXe.ID;
                }
                if (!string.IsNullOrEmpty(taiSanApi.TS_PTK.CHUC_DANH_MA))
                {
                    var ChucDanh = _chucDanhService.GetChucDanhByMa(taiSanApi.TS_PTK.CHUC_DANH_MA);
                    if (ChucDanh != null)
                        taiSanOto.CHUC_DANH_ID = ChucDanh.ID;
                }
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
            }
            else
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");
        }
        public MessageReturn PrepareBienDong(BienDong bienDong = null, BienDongApi biendongApi = null, TaiSan taiSan = null)
        {
            if (biendongApi != null)
            {
                if (bienDong == null)
                    bienDong = new BienDong();
                bienDong.TAI_SAN_ID = taiSan.ID;
                bienDong.TAI_SAN_MA = taiSan.MA;
                bienDong.TAI_SAN_TEN = taiSan.TEN;
                bienDong.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                bienDong.LOAI_TAI_SAN_DON_VI_ID = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                bienDong.DON_VI_ID = taiSan.DON_VI_ID;
                bienDong.LOAI_TAI_SAN_ID = taiSan.LOAI_TAI_SAN_ID;
                bienDong.NGUYEN_GIA = biendongApi.NGUYEN_GIA;
                bienDong.CHUNG_TU_SO = biendongApi.CHUNG_TU_SO;
                bienDong.CHUNG_TU_NGAY = biendongApi.CHUNG_TU_NGAY;
                bienDong.NGAY_BIEN_DONG = biendongApi.NGAY_BIEN_DONG;
                bienDong.NGAY_DUYET = biendongApi.NGAY_DUYET;
                bienDong.NGAY_SU_DUNG = biendongApi.NGAY_SU_DUNG;
                bienDong.LOAI_BIEN_DONG_ID = biendongApi.LOAI_BIEN_DONG_ID;
                bienDong.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                bienDong.NGAY_TAO = biendongApi.NGAY_TAO != null ? biendongApi.NGAY_TAO.Value : DateTime.Now;
                bienDong.GHI_CHU = biendongApi.GHI_CHU;
                bienDong.QUYET_DINH_NGAY = biendongApi.QUYET_DINH_NGAY;
                bienDong.QUYET_DINH_SO = biendongApi.QUYET_DINH_SO;
                bienDong.IS_BIENDONG_CUOI = biendongApi.IS_BIENDONG_CUOI;
                if (string.IsNullOrEmpty(biendongApi.LY_DO_BIEN_DONG_MA))
                    if (string.IsNullOrEmpty(biendongApi.LY_DO_BIEN_DONG_TEN))
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "BIEN_DONG-LY_DO_BIEN_DONG_MA");
                var LyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByMa(biendongApi.LY_DO_BIEN_DONG_MA, (int)taiSan.LOAI_HINH_TAI_SAN_ID.Value);
                if (LyDoBienDong == null)
                    LyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByTEN_LOAI_HINH_TS(biendongApi.LY_DO_BIEN_DONG_TEN, (int)taiSan.LOAI_HINH_TAI_SAN_ID.Value);
                if (LyDoBienDong == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-LY_DO_BIEN_DONG_MA");
                bienDong.LY_DO_BIEN_DONG_ID = LyDoBienDong.ID;
                if (!string.IsNullOrEmpty(biendongApi.DON_VI_BO_PHAN_MA))
                {
                    var donViBoPhan = _donViBoPhanService.GetCacheDonViBoPhanByMa(biendongApi.DON_VI_BO_PHAN_MA);
                    if (donViBoPhan == null)
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "BIEN_DONG-DON_VI_BO_PHAN_MA");
                    else
                        bienDong.DON_VI_BO_PHAN_ID = biendongApi.ID;
                }
                bienDong.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                //_bienDongService.InsertBienDong(bienDong);
                //biendongApi.ID = bienDong.ID;
                // bien dong chi tiet
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");

            }
            else
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");
        }
        public MessageReturn PrepareBienDongChitiet(BienDongChiTiet bienDongChiTiet = null, BienDongApi biendongApi = null)
        {
            if (!string.IsNullOrEmpty(biendongApi.HINH_THUC_MUA_SAM_MA))
            {
                var HinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(biendongApi.HINH_THUC_MUA_SAM_MA);
                if (HinhThucMuaSam == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_MUA_SAM_MA");
                bienDongChiTiet.HINH_THUC_MUA_SAM_ID = HinhThucMuaSam.ID;
            }
            if (!string.IsNullOrEmpty(biendongApi.MUC_DICH_SU_DUNG_MA))
            {
                var MucDichSuDung = _mucDichSuDungService.GetMucDichSuDungByMa(biendongApi.MUC_DICH_SU_DUNG_MA);
                if (MucDichSuDung == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-MUC_DICH_SU_DUNG_MA");
                bienDongChiTiet.MUC_DICH_SU_DUNG_ID = MucDichSuDung.ID;
            }
            bienDongChiTiet.NHAN_HIEU = biendongApi.NHAN_HIEU;
            bienDongChiTiet.SO_HIEU = biendongApi.SO_HIEU;
            if (!string.IsNullOrEmpty(biendongApi.DIA_BAN_MA))
            {
                var DiaBan = _diaBanService.GetCacheDiaBanByMa(biendongApi.DIA_BAN_MA);
                if (DiaBan == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DIA_BAN_MA");
                bienDongChiTiet.DIA_BAN_ID = DiaBan.ID;
            }
            bienDongChiTiet.DATA_JSON = biendongApi.DATA_JSON;
            bienDongChiTiet.HTSD_JSON = biendongApi.HTSD_JSON;
            bienDongChiTiet.NGUYEN_GIA = biendongApi.NGUYEN_GIA;
            bienDongChiTiet.DAT_TONG_DIEN_TICH = biendongApi.DAT_TONG_DIEN_TICH;
            bienDongChiTiet.HTSD_QUAN_LY_NHA_NUOC = biendongApi.HTSD_QUAN_LY_NHA_NUOC;
            bienDongChiTiet.HTSD_HDSN_KINH_DOANH_KHONG = biendongApi.HTSD_HDSN_KINH_DOANH_KHONG;
            bienDongChiTiet.HTSD_HDSN_KINH_DOANH = biendongApi.HTSD_HDSN_KINH_DOANH;
            bienDongChiTiet.HTSD_CHO_THUE = biendongApi.HTSD_CHO_THUE;
            bienDongChiTiet.HTSD_LIEN_DOANH = biendongApi.HTSD_LIEN_DOANH;
            bienDongChiTiet.HTSD_SU_DUNG_HON_HOP = biendongApi.HTSD_SU_DUNG_HON_HOP;
            bienDongChiTiet.HTSD_SU_DUNG_KHAC = biendongApi.HTSD_SU_DUNG_KHAC;
            bienDongChiTiet.HM_SO_NAM_CON_LAI = biendongApi.HM_SO_NAM_CON_LAI;
            bienDongChiTiet.HM_TY_LE_HAO_MON = biendongApi.HM_TY_LE_HAO_MON;
            bienDongChiTiet.HM_LUY_KE = biendongApi.HM_LUY_KE;
            bienDongChiTiet.HM_GIA_TRI_CON_LAI = biendongApi.GIA_TRI_CON_LAI;
            bienDongChiTiet.KH_NGAY_BAT_DAU = biendongApi.KH_NGAY_BAT_DAU;
            bienDongChiTiet.KH_THANG_CON_LAI = biendongApi.KH_THANG_CON_LAI;
            bienDongChiTiet.KH_GIA_TINH_KHAU_HAO = biendongApi.KH_GIA_TINH_KHAU_HAO;
            bienDongChiTiet.KH_GIA_TRI_TRICH_THANG = biendongApi.KH_GIA_TRI_TRICH_THANG;
            bienDongChiTiet.KH_LUY_KE = biendongApi.KH_LUY_KE;
            bienDongChiTiet.KH_CON_LAI = biendongApi.KH_CON_LAI;
            bienDongChiTiet.NHA_SO_TANG = biendongApi.NHA_SO_TANG;
            bienDongChiTiet.NHA_NAM_XAY_DUNG = biendongApi.NHA_NAM_XAY_DUNG;
            bienDongChiTiet.NHA_DIEN_TICH_XD = biendongApi.NHA_DIEN_TICH_XD;
            bienDongChiTiet.NHA_TONG_DIEN_TICH_XD = biendongApi.NHA_TONG_DIEN_TICH_XD;
            bienDongChiTiet.VKT_DIEN_TICH = biendongApi.VKT_DIEN_TICH != null ? biendongApi.VKT_DIEN_TICH.Value : 0;
            bienDongChiTiet.VKT_THE_TICH = biendongApi.VKT_THE_TICH;
            bienDongChiTiet.VKT_CHIEU_DAI = biendongApi.VKT_CHIEU_DAI;
            bienDongChiTiet.OTO_BIEN_KIEM_SOAT = biendongApi.OTO_BIEN_KIEM_SOAT;
            bienDongChiTiet.OTO_SO_CHO_NGOI = biendongApi.OTO_SO_CHO_NGOI;
            if (!string.IsNullOrEmpty(biendongApi.OTO_CHUC_DANH_MA))
            {
                var ChucDanh = _chucDanhService.GetChucDanhByMa(biendongApi.OTO_CHUC_DANH_MA);
                if (ChucDanh == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_CHUC_DANH_MA");
                bienDongChiTiet.OTO_CHUC_DANH_ID = ChucDanh.ID;
            }

            bienDongChiTiet.THONG_SO_KY_THUAT = biendongApi.THONG_SO_KY_THUAT;
            if (!string.IsNullOrEmpty(biendongApi.OTO_NHAN_XE_MA))
            {
                var nhanXe = _nhanXeService.GetNhanXeByMaTen(biendongApi.OTO_NHAN_XE_MA);
                if (nhanXe == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_NHAN_XE_MA");
                bienDongChiTiet.OTO_NHAN_XE_ID = nhanXe.ID;
            }
            bienDongChiTiet.OTO_TAI_TRONG = biendongApi.OTO_TAI_TRONG;
            bienDongChiTiet.OTO_CONG_XUAT = biendongApi.OTO_CONG_XUAT;
            bienDongChiTiet.OTO_XI_LANH = biendongApi.OTO_XI_LANH;
            bienDongChiTiet.OTO_SO_KHUNG = biendongApi.OTO_SO_KHUNG;
            bienDongChiTiet.OTO_SO_MAY = biendongApi.OTO_SO_MAY;
            bienDongChiTiet.THONG_SO_KY_THUAT = biendongApi.THONG_SO_KY_THUAT;
            bienDongChiTiet.CLN_SO_NAM = biendongApi.CLN_SO_NAM;
            bienDongChiTiet.PHI_THU = biendongApi.PHI_THU;
            bienDongChiTiet.PHI_BU_DAP = biendongApi.PHI_BU_DAP;
            bienDongChiTiet.PHI_NOP_NGAN_SACH = biendongApi.PHI_NOP_NGAN_SACH;
            bienDongChiTiet.PHI_KHAC = biendongApi.PHI_KHAC;
            if (!string.IsNullOrEmpty(biendongApi.DON_VI_NHAN_DIEU_CHUYEN_MA))
            {
                var DonVi = _donViService.GetCacheDonViByMa(biendongApi.DON_VI_NHAN_DIEU_CHUYEN_MA);
                if (DonVi == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA");
                bienDongChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID = DonVi.ID;
            }
            if (!string.IsNullOrEmpty(biendongApi.HINH_THUC_XU_LY_MA))
            {
                var HinhThucXuLy = _hinhThucXuLyServiceService.GetHinhThucXuLyByMa(biendongApi.HINH_THUC_XU_LY_MA);
                if (HinhThucXuLy == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_XU_LY_MA");
                bienDongChiTiet.HINH_THUC_XU_LY_ID = HinhThucXuLy.ID;
            }
            bienDongChiTiet.IS_BAN_THANH_LY = biendongApi.IS_BAN_THANH_LY;
            bienDongChiTiet.HS_CNQSD_SO = biendongApi.HS_CNQSD_SO;
            bienDongChiTiet.HS_CNQSD_NGAY = biendongApi.HS_CNQSD_NGAY;
            bienDongChiTiet.HS_QUYET_DINH_GIAO_SO = biendongApi.HS_QUYET_DINH_GIAO_SO;
            bienDongChiTiet.HS_QUYET_DINH_GIAO_NGAY = biendongApi.HS_QUYET_DINH_GIAO_NGAY;
            bienDongChiTiet.HS_CHUYEN_NHUONG_SD_SO = biendongApi.HS_CHUYEN_NHUONG_SD_SO;
            bienDongChiTiet.HS_CHUYEN_NHUONG_SD_NGAY = biendongApi.HS_CHUYEN_NHUONG_SD_NGAY;
            bienDongChiTiet.HS_QUYET_DINH_CHO_THUE_SO = biendongApi.HS_QUYET_DINH_CHO_THUE_SO;
            bienDongChiTiet.HS_QUYET_DINH_CHO_THUE_NGAY = biendongApi.HS_QUYET_DINH_CHO_THUE_NGAY;
            bienDongChiTiet.HS_KHAC = biendongApi.HS_KHAC;
            bienDongChiTiet.HS_QUYET_DINH_BAN_GIAO = biendongApi.HS_QUYET_DINH_BAN_GIAO;
            bienDongChiTiet.HS_QUYET_DINH_BAN_GIAO_NGAY = biendongApi.HS_QUYET_DINH_BAN_GIAO_NGAY;
            bienDongChiTiet.HS_HOP_DONG_CHO_THUE_SO = biendongApi.HS_HOP_DONG_CHO_THUE_SO;
            bienDongChiTiet.HS_HOP_DONG_CHO_THUE_NGAY = biendongApi.HS_HOP_DONG_CHO_THUE_NGAY;
            bienDongChiTiet.HS_PHAP_LY_KHAC = biendongApi.HS_PHAP_LY_KHAC;
            bienDongChiTiet.HS_PHAP_LY_KHAC_NGAY = biendongApi.HS_PHAP_LY_KHAC_NGAY;
            bienDongChiTiet.DIA_CHI = biendongApi.DIA_CHI;
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        }
        public MessageReturn PrepareYeuCau(YeuCau yeuCau = null, BienDongApi biendongApi = null, TaiSan taiSan = null)
        {
            if (biendongApi != null)
            {
                if (yeuCau == null)
                    yeuCau = new YeuCau();
                yeuCau.TAI_SAN_ID = taiSan.ID;
                yeuCau.TAI_SAN_MA = taiSan.MA;
                yeuCau.TAI_SAN_TEN = taiSan.TEN;
                yeuCau.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                yeuCau.LOAI_TAI_SAN_DON_VI_ID = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                yeuCau.DON_VI_ID = taiSan.DON_VI_ID;
                yeuCau.LOAI_TAI_SAN_ID = taiSan.LOAI_TAI_SAN_ID;
                yeuCau.NGUYEN_GIA = biendongApi.NGUYEN_GIA;
                yeuCau.CHUNG_TU_SO = biendongApi.CHUNG_TU_SO;
                yeuCau.CHUNG_TU_NGAY = biendongApi.CHUNG_TU_NGAY;
                yeuCau.NGAY_BIEN_DONG = biendongApi.NGAY_BIEN_DONG;
                //yeuCau.NGAY_DUYET = biendongApi.NGAY_DUYET;
                yeuCau.NGAY_SU_DUNG = biendongApi.NGAY_SU_DUNG;
                yeuCau.LOAI_BIEN_DONG_ID = biendongApi.LOAI_BIEN_DONG_ID;
                if (biendongApi.TRANG_THAI == 1)// chờ duyệt
                    yeuCau.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                else if (biendongApi.TRANG_THAI == 3)// từ chối
                    yeuCau.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.TU_CHOI;
                else
                    yeuCau.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                yeuCau.NGAY_TAO = biendongApi.NGAY_TAO != null ? biendongApi.NGAY_TAO.Value : DateTime.Now;
                yeuCau.GHI_CHU = biendongApi.GHI_CHU;
                yeuCau.QUYET_DINH_NGAY = biendongApi.QUYET_DINH_NGAY;
                yeuCau.QUYET_DINH_SO = biendongApi.QUYET_DINH_SO;
                yeuCau.IS_BIENDONG_CUOI = biendongApi.IS_BIENDONG_CUOI;
                if (string.IsNullOrEmpty(biendongApi.LY_DO_BIEN_DONG_TEN))
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "BIEN_DONG-LY_DO_BIEN_DONG_TEN");
                else
                {
                    var LyDoBienDong = _lyDoBienDongService.GetLyDoBienDongByTEN_LOAI_HINH_TS(biendongApi.LY_DO_BIEN_DONG_TEN, (int)taiSan.LOAI_HINH_TAI_SAN_ID.Value);
                    if (LyDoBienDong == null)
                        return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-LY_DO_BIEN_DONG_TEN");
                    yeuCau.LY_DO_BIEN_DONG_ID = LyDoBienDong.ID;
                }
                if (!string.IsNullOrEmpty(biendongApi.DON_VI_BO_PHAN_MA))
                {
                    var donViBoPhan = _donViBoPhanService.GetCacheDonViBoPhanByMa(biendongApi.DON_VI_BO_PHAN_MA);
                    if (donViBoPhan == null)
                        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "BIEN_DONG-DON_VI_BO_PHAN_MA");
                    else
                        yeuCau.DON_VI_BO_PHAN_ID = biendongApi.ID;
                }
                yeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                //_bienDongService.InsertBienDong(bienDong);
                //biendongApi.ID = bienDong.ID;
                // bien dong chi tiet
                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");

            }
            else
                return new MessageReturn(MessageReturn.ERROR, "Tài sản");
        }
        public MessageReturn PrepareYeuCauChiTiet(YeuCauChiTiet yeuCauChiTiet = null, BienDongApi biendongApi = null)
        {
            if (!string.IsNullOrEmpty(biendongApi.HINH_THUC_MUA_SAM_MA))
            {
                var HinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(biendongApi.HINH_THUC_MUA_SAM_MA);
                if (HinhThucMuaSam == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_MUA_SAM_MA");
                yeuCauChiTiet.HINH_THUC_MUA_SAM_ID = HinhThucMuaSam.ID;
            }
            if (!string.IsNullOrEmpty(biendongApi.MUC_DICH_SU_DUNG_MA))
            {
                var MucDichSuDung = _mucDichSuDungService.GetMucDichSuDungByMa(biendongApi.MUC_DICH_SU_DUNG_MA);
                if (MucDichSuDung == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-MUC_DICH_SU_DUNG_MA");
                yeuCauChiTiet.MUC_DICH_SU_DUNG_ID = MucDichSuDung.ID;
            }
            yeuCauChiTiet.NHAN_HIEU = biendongApi.NHAN_HIEU;
            yeuCauChiTiet.SO_HIEU = biendongApi.SO_HIEU;
            if (!string.IsNullOrEmpty(biendongApi.DIA_BAN_MA))
            {
                var DiaBan = _diaBanService.GetCacheDiaBanByMa(biendongApi.DIA_BAN_MA);
                if (DiaBan == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DIA_BAN_MA");
                yeuCauChiTiet.DIA_BAN_ID = DiaBan.ID;
            }
            yeuCauChiTiet.DATA_JSON = biendongApi.DATA_JSON;
            yeuCauChiTiet.HTSD_JSON = biendongApi.HTSD_JSON;
            yeuCauChiTiet.NGUYEN_GIA = biendongApi.NGUYEN_GIA;
            yeuCauChiTiet.DAT_TONG_DIEN_TICH = biendongApi.DAT_TONG_DIEN_TICH;
            yeuCauChiTiet.HTSD_QUAN_LY_NHA_NUOC = biendongApi.HTSD_QUAN_LY_NHA_NUOC;
            yeuCauChiTiet.HTSD_HDSN_KINH_DOANH_KHONG = biendongApi.HTSD_HDSN_KINH_DOANH_KHONG;
            yeuCauChiTiet.HTSD_HDSN_KINH_DOANH = biendongApi.HTSD_HDSN_KINH_DOANH;
            yeuCauChiTiet.HTSD_CHO_THUE = biendongApi.HTSD_CHO_THUE;
            yeuCauChiTiet.HTSD_LIEN_DOANH = biendongApi.HTSD_LIEN_DOANH;
            yeuCauChiTiet.HTSD_SU_DUNG_HON_HOP = biendongApi.HTSD_SU_DUNG_HON_HOP;
            yeuCauChiTiet.HTSD_SU_DUNG_KHAC = biendongApi.HTSD_SU_DUNG_KHAC;
            yeuCauChiTiet.HM_SO_NAM_CON_LAI = biendongApi.HM_SO_NAM_CON_LAI;
            yeuCauChiTiet.HM_TY_LE_HAO_MON = biendongApi.HM_TY_LE_HAO_MON;
            yeuCauChiTiet.HM_LUY_KE = biendongApi.HM_LUY_KE;
            yeuCauChiTiet.HM_GIA_TRI_CON_LAI = biendongApi.GIA_TRI_CON_LAI;
            yeuCauChiTiet.KH_NGAY_BAT_DAU = biendongApi.KH_NGAY_BAT_DAU;
            yeuCauChiTiet.KH_THANG_CON_LAI = biendongApi.KH_THANG_CON_LAI;
            yeuCauChiTiet.KH_GIA_TINH_KHAU_HAO = biendongApi.KH_GIA_TINH_KHAU_HAO;
            yeuCauChiTiet.KH_GIA_TRI_TRICH_THANG = biendongApi.KH_GIA_TRI_TRICH_THANG;
            yeuCauChiTiet.KH_LUY_KE = biendongApi.KH_LUY_KE;
            yeuCauChiTiet.KH_CON_LAI = biendongApi.KH_CON_LAI;
            yeuCauChiTiet.NHA_SO_TANG = biendongApi.NHA_SO_TANG;
            yeuCauChiTiet.NHA_NAM_XAY_DUNG = biendongApi.NHA_NAM_XAY_DUNG;
            yeuCauChiTiet.NHA_DIEN_TICH_XD = biendongApi.NHA_DIEN_TICH_XD;
            yeuCauChiTiet.NHA_TONG_DIEN_TICH_XD = biendongApi.NHA_TONG_DIEN_TICH_XD;
            yeuCauChiTiet.VKT_DIEN_TICH = biendongApi.VKT_DIEN_TICH != null ? biendongApi.VKT_DIEN_TICH.Value : 0;
            yeuCauChiTiet.VKT_THE_TICH = biendongApi.VKT_THE_TICH;
            yeuCauChiTiet.VKT_CHIEU_DAI = biendongApi.VKT_CHIEU_DAI;
            yeuCauChiTiet.OTO_BIEN_KIEM_SOAT = biendongApi.OTO_BIEN_KIEM_SOAT;
            yeuCauChiTiet.OTO_SO_CHO_NGOI = biendongApi.OTO_SO_CHO_NGOI;
            if (!string.IsNullOrEmpty(biendongApi.OTO_CHUC_DANH_MA))
            {
                var ChucDanh = _chucDanhService.GetChucDanhByMa(biendongApi.OTO_CHUC_DANH_MA);
                if (ChucDanh == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_CHUC_DANH_MA");
                yeuCauChiTiet.OTO_CHUC_DANH_ID = ChucDanh.ID;
            }

            yeuCauChiTiet.THONG_SO_KY_THUAT = biendongApi.THONG_SO_KY_THUAT;
            if (!string.IsNullOrEmpty(biendongApi.OTO_NHAN_XE_MA))
            {
                var nhanXe = _nhanXeService.GetNhanXeByMaTen(biendongApi.OTO_NHAN_XE_MA);
                if (nhanXe == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-OTO_NHAN_XE_MA");
                yeuCauChiTiet.OTO_NHAN_XE_ID = nhanXe.ID;
            }
            yeuCauChiTiet.OTO_TAI_TRONG = biendongApi.OTO_TAI_TRONG;
            yeuCauChiTiet.OTO_CONG_XUAT = biendongApi.OTO_CONG_XUAT;
            yeuCauChiTiet.OTO_XI_LANH = biendongApi.OTO_XI_LANH;
            yeuCauChiTiet.OTO_SO_KHUNG = biendongApi.OTO_SO_KHUNG;
            yeuCauChiTiet.OTO_SO_MAY = biendongApi.OTO_SO_MAY;
            yeuCauChiTiet.THONG_SO_KY_THUAT = biendongApi.THONG_SO_KY_THUAT;
            yeuCauChiTiet.CLN_SO_NAM = biendongApi.CLN_SO_NAM;
            yeuCauChiTiet.PHI_THU = biendongApi.PHI_THU;
            yeuCauChiTiet.PHI_BU_DAP = biendongApi.PHI_BU_DAP;
            yeuCauChiTiet.PHI_NOP_NGAN_SACH = biendongApi.PHI_NOP_NGAN_SACH;
            yeuCauChiTiet.PHI_KHAC = biendongApi.PHI_KHAC;
            if (!string.IsNullOrEmpty(biendongApi.DON_VI_NHAN_DIEU_CHUYEN_MA))
            {
                var DonVi = _donViService.GetCacheDonViByMa(biendongApi.DON_VI_NHAN_DIEU_CHUYEN_MA);
                if (DonVi == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA");
                yeuCauChiTiet.DON_VI_NHAN_DIEU_CHUYEN_ID = DonVi.ID;
            }
            if (!string.IsNullOrEmpty(biendongApi.HINH_THUC_XU_LY_MA))
            {
                var HinhThucXuLy = _hinhThucXuLyServiceService.GetHinhThucXuLyByMa(biendongApi.HINH_THUC_XU_LY_MA);
                if (HinhThucXuLy == null)
                    return new MessageReturn(MessageReturn.ERROR_CODE, "BIEN_DONG-HINH_THUC_XU_LY_MA");
                yeuCauChiTiet.HINH_THUC_XU_LY_ID = HinhThucXuLy.ID;
            }
            yeuCauChiTiet.IS_BAN_THANH_LY = biendongApi.IS_BAN_THANH_LY;
            yeuCauChiTiet.HS_CNQSD_SO = biendongApi.HS_CNQSD_SO;
            yeuCauChiTiet.HS_CNQSD_NGAY = biendongApi.HS_CNQSD_NGAY;
            yeuCauChiTiet.HS_QUYET_DINH_GIAO_SO = biendongApi.HS_QUYET_DINH_GIAO_SO;
            yeuCauChiTiet.HS_QUYET_DINH_GIAO_NGAY = biendongApi.HS_QUYET_DINH_GIAO_NGAY;
            yeuCauChiTiet.HS_CHUYEN_NHUONG_SD_SO = biendongApi.HS_CHUYEN_NHUONG_SD_SO;
            yeuCauChiTiet.HS_CHUYEN_NHUONG_SD_NGAY = biendongApi.HS_CHUYEN_NHUONG_SD_NGAY;
            yeuCauChiTiet.HS_QUYET_DINH_CHO_THUE_SO = biendongApi.HS_QUYET_DINH_CHO_THUE_SO;
            yeuCauChiTiet.HS_QUYET_DINH_CHO_THUE_NGAY = biendongApi.HS_QUYET_DINH_CHO_THUE_NGAY;
            yeuCauChiTiet.HS_KHAC = biendongApi.HS_KHAC;
            yeuCauChiTiet.HS_QUYET_DINH_BAN_GIAO = biendongApi.HS_QUYET_DINH_BAN_GIAO;
            yeuCauChiTiet.HS_QUYET_DINH_BAN_GIAO_NGAY = biendongApi.HS_QUYET_DINH_BAN_GIAO_NGAY;
            yeuCauChiTiet.HS_HOP_DONG_CHO_THUE_SO = biendongApi.HS_HOP_DONG_CHO_THUE_SO;
            yeuCauChiTiet.HS_HOP_DONG_CHO_THUE_NGAY = biendongApi.HS_HOP_DONG_CHO_THUE_NGAY;
            yeuCauChiTiet.HS_PHAP_LY_KHAC = biendongApi.HS_PHAP_LY_KHAC;
            yeuCauChiTiet.HS_PHAP_LY_KHAC_NGAY = biendongApi.HS_PHAP_LY_KHAC_NGAY;
            yeuCauChiTiet.DIA_CHI = biendongApi.DIA_CHI;
            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        }
        public virtual MessageReturn InsertOrUpdateTaiSanToanDan(QuyetDinhTichThuApi quyetDinhTichThuApi)
        {
            if (quyetDinhTichThuApi == null)
            {
                return new MessageReturn(MessageReturn.ERROR, "Quyết định Lỗi");
            }
            QuyetDinhTichThu dataTichThu = _quyetDinhTichThuService.GetQuyetDinhTichThu(quyetDinhTichThuApi.QUYET_DINH_SO, quyetDinhTichThuApi.QUYET_DINH_NGAY);
            if (dataTichThu != null)
            {
                var TaiSan = _taiSanTdService.GetTaiSanTdByQuyetDinhId(dataTichThu.ID);
                List<XuLy> ListXuLyDelelete = new List<XuLy>();
                foreach (var ts in TaiSan)
                {
                    var _listXuLyTs = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(ts.ID);
                    var _listXuLy = _listXuLyTs.Select(m => m.xuly);
                    foreach (var item in _listXuLy)
                    {
                        ListXuLyDelelete.Add(item);
                    }
                    //delete xử lý tài sản
                    foreach (var xlts in _listXuLyTs)
                    {
                        _taiSanTdXuLyService.DeleteTaiSanTdXuLy(xlts);
                    }
                    //delete tài sản
                    _taiSanTdService.DeleteTaiSanTd(ts);
                }
                ListXuLyDelelete = ListXuLyDelelete.Distinct().ToList();
                foreach (var item in ListXuLyDelelete)
                {
                    _xuLyService.DeleteXuLy(item);
                }
                _quyetDinhTichThuService.DeleteQuyetDinhTichThu(dataTichThu);
            }

            dataTichThu = new QuyetDinhTichThu();
            if (!string.IsNullOrEmpty(quyetDinhTichThuApi.MA_DON_VI))
            {
                var donvi = _donViService.GetCacheDonViByMa(quyetDinhTichThuApi.MA_DON_VI);
                if (donvi == null)
                {
                    return new MessageReturn(MessageReturn.ERROR_CODE, "MA_DON_VI");
                }
                dataTichThu.DON_VI_ID = donvi.ID;
            }
            dataTichThu.GHI_CHU = quyetDinhTichThuApi.GHI_CHU;
            dataTichThu.QUYET_DINH_NGAY = quyetDinhTichThuApi.QUYET_DINH_NGAY;
            dataTichThu.QUYET_DINH_SO = quyetDinhTichThuApi.QUYET_DINH_SO;
            dataTichThu.TEN = quyetDinhTichThuApi.TEN;
            dataTichThu.TRANG_THAI_ID = 0;
            dataTichThu.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            dataTichThu.NGAY_TAO = DateTime.Now;
            _quyetDinhTichThuService.InsertQuyetDinhTichThu(dataTichThu);
            // Tài sản toàn dân
            foreach (var ts in quyetDinhTichThuApi.ListTaiSanToanDanModels)
            {
                //tài sản toàn dân
                TaiSanTd dataTaiSanToanDan = new TaiSanTd();
                dataTaiSanToanDan.QUYET_DINH_TICH_THU_ID = dataTichThu.ID;
                dataTaiSanToanDan.TEN = ts.TEN;
                if (!string.IsNullOrEmpty(ts.MA_NGUON_GOC_TAI_SAN))
                {
                    var NguonGocTaiSan = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(ts.MA_NGUON_GOC_TAI_SAN);
                    if (NguonGocTaiSan == null)
                    {
                        return new MessageReturn(MessageReturn.ERROR_CODE, "DON_VI_MA");
                    }
                    dataTaiSanToanDan.quyetdinh.NGUON_GOC_ID = NguonGocTaiSan.ID;
                }
                else
                {
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DON_VI_MA");

                }
                if (!string.IsNullOrEmpty(ts.MA_LOAI_TAI_SAN))
                {
                    var LoaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(ts.MA_LOAI_TAI_SAN);
                    if (LoaiTaiSan == null)
                    {
                        return new MessageReturn(MessageReturn.ERROR_CODE, "MA_LOAI_TAI_SAN");
                    }
                    dataTaiSanToanDan.LOAI_TAI_SAN_ID = LoaiTaiSan.ID;
                }
                else
                {
                    return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "MA_LOAI_TAI_SAN");
                }
                //dataTaiSanToanDan.NGUYEN_GIA = ts.NGUYEN_GIA;
                //dataTaiSanToanDan.LOAI_TAI_SAN_ID = ts.LOAI_TAI_SAN_ID;
                //dataTaiSanToanDan.NGUYEN_GIA = ts.NGUYEN_GIA;
                dataTaiSanToanDan.SO_LUONG = ts.SO_LUONG;
                //dataTaiSanToanDan.KHOI_LUONG = ts.KHOI_LUONG;
                dataTaiSanToanDan.GIA_TRI_XAC_LAP = ts.GIA_TRI;
                dataTaiSanToanDan.GHI_CHU = ts.GHI_CHU;
                //dataTaiSanToanDan.DIEN_TICH = ts.DIEN_TICH;
                dataTaiSanToanDan.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                _taiSanTdService.InsertTaiSanTd(dataTaiSanToanDan);
                ts.ID = dataTaiSanToanDan.ID;
            }
            //xử lý
            var ListXuLy = quyetDinhTichThuApi.ListTaiSanToanDanModels.Select(m => m.ListXuLy);
            List<XuLy> ListDataXuLy = new List<XuLy>();
            foreach (var lst in ListXuLy)
            {
                foreach (var data in lst)
                {
                    XuLy dataXuLy = new XuLy();
                    dataXuLy.QUYET_DINH_SO = data.QUYET_DINH_SO;
                    dataXuLy.QUYET_DINH_NGAY = data.QUYET_DINH_NGAY;
                    if (!string.IsNullOrEmpty(data.MA_DON_VI))
                    {
                        var donvi = _donViService.GetCacheDonViByMa(data.MA_DON_VI);
                        if (donvi == null)
                        {
                            return new MessageReturn(MessageReturn.ERROR_CODE, "MA_DON_VI");
                        }
                        dataXuLy.DON_VI_ID = donvi.ID;
                    }
                    //dataXuLy.HINH_THUC = data.HINH_THUC;
                    //dataXuLy.CHI_PHI = data.CHI_PHI;
                    dataXuLy.GHI_CHU = data.GHI_CHU;
                    dataXuLy.NGAY_TAO = DateTime.Now.Date;
                    //dataXuLy.LOAI_XU_LY_ID = 2;
                    ListDataXuLy.Add(dataXuLy);
                }
            }
            List<XuLy> _ListDataXuLy = new List<XuLy>();
            foreach (var qdxl in ListDataXuLy)
            {
                var _xuLy = _ListDataXuLy.Where(m => m.DON_VI_ID == qdxl.DON_VI_ID && m.QUYET_DINH_SO == qdxl.QUYET_DINH_SO && m.QUYET_DINH_NGAY.Value.Date == qdxl.QUYET_DINH_NGAY.Value.Date).FirstOrDefault();
                if (_xuLy == null)
                {
                    _ListDataXuLy.Add(qdxl);
                }
            }

            foreach (var data in _ListDataXuLy)
            {

                _xuLyService.InsertXuLy(data);
            }
            // insert tài sản toàn dân xử lý
            foreach (var data in quyetDinhTichThuApi.ListTaiSanToanDanModels)
            {
                foreach (var xuly in data.ListXuLy)
                {
                    TaiSanTdXuLy dataTaiSanXuLy = new TaiSanTdXuLy();
                    dataTaiSanXuLy.TAI_SAN_ID = data.ID;
                    dataTaiSanXuLy.SO_LUONG = xuly.SO_LUONG;
                    //dataTaiSanXuLy.DIEN_TICH = xuly.DIEN_TICH;
                    //dataTaiSanXuLy.NGUYEN_GIA = xuly.NGUYEN_GIA;
                    //dataTaiSanXuLy.GIA_TRI = xuly.GIA_TRI;
                    //dataTaiSanXuLy.GIA_TRI_GHI_TANG = xuly.GIA_TRI_GHI_TANG;
                    //dataTaiSanXuLy.GIA_TRI_NSNN = xuly.GIA_TRI_NSNN;
                    //dataTaiSanXuLy.GIA_TRI_TKTG = xuly.GIA_TRI_TKTG;

                    if (!string.IsNullOrEmpty(xuly.MA_PHUONG_AN_XU_LY))
                    {
                        var PhuongAnXuLy = _phuongAnXuLyService.GetPhuongAnXuLyByMa(xuly.MA_PHUONG_AN_XU_LY);
                        if (PhuongAnXuLy == null)
                        {
                            return new MessageReturn(MessageReturn.ERROR_CODE, "PHUONG_AN_XU_LY_MA");
                        }
                        dataTaiSanXuLy.PHUONG_AN_XU_LY_ID = PhuongAnXuLy.ID;
                    }
                    if (!string.IsNullOrEmpty(xuly.MA_HINH_THUC_XU_LY))
                    {
                        var HinhThucXuLy = _hinhThucXuLyServiceService.GetHinhThucXuLyByMa(xuly.MA_HINH_THUC_XU_LY);
                        if (HinhThucXuLy == null)
                        {
                            return new MessageReturn(MessageReturn.ERROR_CODE, "HINH_THUC_XU_LY_MA");
                        }
                        dataTaiSanXuLy.HINH_THUC_XU_LY_ID = HinhThucXuLy.ID;
                    }
                    //dataTaiSanXuLy.CHI_PHI_XU_LY = xuly.CHI_PHI_XU_LY;
                    //dataTaiSanXuLy.HOP_DONG_SO = xuly.HOP_DONG_SO;
                    //dataTaiSanXuLy.HOP_DONG_NGAY = xuly.HOP_DONG_NGAY;
                    dataTaiSanXuLy.GHI_CHU = xuly.GHI_CHU_CHI_TIET;
                    dataTaiSanXuLy.XU_LY_ID = ListDataXuLy.Where(m => m.QUYET_DINH_SO == xuly.QUYET_DINH_SO && m.QUYET_DINH_NGAY.Value.Date == xuly.QUYET_DINH_NGAY.Value.Date && m.DON_VI_ID == _donViService.GetCacheDonViByMa(xuly.MA_DON_VI).ID).FirstOrDefault().ID;
                    _taiSanTdXuLyService.InsertTaiSanTdXuLy(dataTaiSanXuLy);
                }
            }

            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Done");
        }
        public virtual List<MessageReturn> ImportExcel(Stream stream)
        {
            var package = new ExcelPackage(stream);
            var workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            #region Tài sản khác đất nhà
            var item = workSheets[1];

            for (var rowNumber = 2; rowNumber <= item.Dimension.End.Row; rowNumber++)
            {
                var row = item.Cells[rowNumber, 1, rowNumber, item.Dimension.End.Column];
                //Ma loại tài sản
                int SoLuong = int.Parse(row[rowNumber, 5].Text);
                {
                    for (int i = 0; i < SoLuong; i++)
                    {
                        TaiSan taiSan = new TaiSan();
                        var LoaiTaiSanValue = row[rowNumber, 1].Text;
                        string MaLoaiTaiSan = LoaiTaiSanValue.Split('-')[0];
                        if (!string.IsNullOrEmpty(MaLoaiTaiSan))
                        {
                            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(MaLoaiTaiSan.Trim());
                            if (loaiTaiSan == null)
                            {
                                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "LOAI_TAI_SAN"));
                                break;
                            }
                            taiSan.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                            taiSan.LOAI_HINH_TAI_SAN_ID = loaiTaiSan.LOAI_HINH_TAI_SAN_ID;
                            taiSan.TEN = row[rowNumber, 2].Text;
                            if (string.IsNullOrEmpty(taiSan.TEN))
                            {
                                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                                break;
                            }
                            // nguyên giá
                            string NguyenGia = row[rowNumber, 6].Text;
                            if (!string.IsNullOrEmpty(NguyenGia))
                            {
                                taiSan.NGUYEN_GIA_BAN_DAU = decimal.Parse(row[rowNumber, 6].Text);
                            }
                            //ngày bắt đầu sử dụng
                            string NgayBatDauSuDung = row[rowNumber, 13].Text;
                            if (!string.IsNullOrEmpty(NgayBatDauSuDung))
                            {
                                taiSan.NGAY_SU_DUNG = DateTime.Parse(NgayBatDauSuDung);
                            }
                            //Ngay nhập
                            string NgayNhap = row[rowNumber, 12].Text;
                            if (!string.IsNullOrEmpty(NgayNhap))
                            {
                                taiSan.NGAY_NHAP = DateTime.Parse(NgayBatDauSuDung);
                            }
                            taiSan.DON_VI_ID = _workContext.CurrentDonVi.ID;
                            taiSan.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                            // đơn vị bộ phận
                            string TenBoPhan = row[rowNumber, 4].Text.Trim();
                            if (string.IsNullOrEmpty(TenBoPhan.Trim()))
                            {
                                DonViBoPhan donViBoPhan = _donViBoPhanService.GetCacheDonViBoPhanByTen(TenBoPhan);
                                if (donViBoPhan == null)
                                {
                                    donViBoPhan = new DonViBoPhan();
                                    donViBoPhan.TEN = TenBoPhan;
                                    donViBoPhan.DON_VI_ID = _workContext.CurrentDonVi.ID;
                                    _donViBoPhanService.InsertDonViBoPhan(donViBoPhan);
                                }
                                taiSan.DON_VI_BO_PHAN_ID = donViBoPhan.ID;
                            }
                            taiSan.NGAY_NHAP = taiSan.NGAY_NHAP ?? DateTime.Now;
                            taiSan.NGUON_TAI_SAN_ID = (decimal)enumNguonTaiSan.KHAC;
                            _taiSanService.InsertTaiSan(taiSan, true);
                            // cập nhật mã tài sản
                            if (taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
                            {
                                // get tài sản vô hình  gốc
                                decimal? parentId = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                                LoaiTaiSanDonVi taiSanVoHinh = new LoaiTaiSanDonVi();
                                do
                                {
                                    taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                                    parentId = taiSanVoHinh.PARENT_ID;
                                } while (parentId != null);
                                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                                taiSan.MA = CommonHelper.GenMaTaiSan(taiSan.donvi.MA, taiSanVoHinh.MA, taiSan.ID);
                            }
                            else
                            {
                                taiSan.MA = CommonHelper.GenMaTaiSan(taiSan.donvi.MA, taiSan.loaitaisan.MA, taiSan.ID);
                            }
                            // loại hình tài sản
                            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
                            {
                                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                                    break;
                                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                                    break;
                                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                                    break;
                                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                                    break;
                                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                                    break;
                                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                                    break;
                                default:
                                    break;
                            }
                            // yêu cầu
                            YeuCau yeuCau = new YeuCau();
                            yeuCau.TAI_SAN_ID = taiSan.ID;
                            yeuCau.TAI_SAN_MA = taiSan.MA;
                            yeuCau.TAI_SAN_TEN = taiSan.TEN;
                            yeuCau.LOAI_TAI_SAN_ID = taiSan.LOAI_TAI_SAN_ID;
                            yeuCau.LOAI_TAI_SAN_DON_VI_ID = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                            yeuCau.DON_VI_BO_PHAN_ID = taiSan.DON_VI_BO_PHAN_ID;
                            yeuCau.NGAY_BIEN_DONG = taiSan.NGAY_NHAP;
                            yeuCau.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                            yeuCau.NGUYEN_GIA = taiSan.NGUYEN_GIA_BAN_DAU;
                            yeuCau.TAI_SAN_MA = taiSan.MA;
                            yeuCau.TAI_SAN_TEN = taiSan.TEN;
                            yeuCau.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                            yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                            yeuCau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                            //nguồn vốn
                            List<NguonVonJson> nguonVonJsons = new List<NguonVonJson>();
                            string NguonVonNganSach = row[rowNumber, 9].Text;
                            if (!string.IsNullOrEmpty(NguonVonNganSach))// nguồn vốn ngân sách
                            {
                                nguonVonJsons.Add(new NguonVonJson
                                {
                                    TEN = "Nguồn ngân sách",
                                    GiaTri = decimal.Parse(row[rowNumber, 9].Text),
                                    ID = 1
                                });
                            }
                            string NguonVonPTHDSN = row[rowNumber, 10].Text;
                            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                            {
                                nguonVonJsons.Add(new NguonVonJson
                                {
                                    TEN = "Quỹ phát triển hoạt động sự nghiệp",
                                    GiaTri = decimal.Parse(row[rowNumber, 10].Text),
                                    ID = 2
                                });
                            }
                            string NguonVonVienTro = row[rowNumber, 11].Text;
                            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                            {
                                nguonVonJsons.Add(new NguonVonJson
                                {
                                    TEN = "Nguồn viện trợ (ODA,...)",
                                    GiaTri = decimal.Parse(row[rowNumber, 11].Text),
                                    ID = 4
                                });
                            }
                            string NguonKhac = row[rowNumber, 11].Text;
                            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                            {
                                nguonVonJsons.Add(new NguonVonJson
                                {
                                    TEN = "Nguồn viện trợ (ODA,...)",
                                    GiaTri = decimal.Parse(row[rowNumber, 11].Text),
                                    ID = 4
                                });
                            }

                            //yeuCau.NGUON_VON_JSON = 
                            //yeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                            _yeuCauService.InsertYeuCau(yeuCau);
                            YeuCauChiTiet yeuCauChiTiet = new YeuCauChiTiet();
                            string MucDichDuocGiao = row[rowNumber, 15].Text;
                            if (string.IsNullOrEmpty(MucDichDuocGiao))
                            {
                                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                                _yeuCauService.DeleteYeuCau(yeuCau);
                                _taiSanService.DeleteTaiSan(taiSan);
                                break;
                            }
                            else
                            {
                                MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungByTen(MucDichDuocGiao);
                                if (mucDichSuDung == null)
                                {
                                    ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                                    _yeuCauService.DeleteYeuCau(yeuCau);
                                    _taiSanService.DeleteTaiSan(taiSan);
                                }
                                yeuCauChiTiet.MUC_DICH_SU_DUNG_ID = mucDichSuDung.ID;
                                _yeuCauChiTietService.InsertYeuCauChiTiet(yeuCauChiTiet);

                            }

                        }
                    }
                }


            }
            #endregion
            #region Tài sản đất, nhà

            #endregion
            return ListResult;
        }
        public virtual MessageReturn insertOrupdateTaiSanByJson(string json, string ma_db, bool IsTaiSanKho = false, decimal cheDoHaoMon = 23)
        {
            OracleParameter P_JSON_STRING = new OracleParameter("P_JSON_STRING", OracleDbType.Clob, json, ParameterDirection.Input);
            OracleParameter P_CHE_DO_HAO_MON = new OracleParameter("P_CHE_DO_HAO_MON", OracleDbType.Decimal, cheDoHaoMon, ParameterDirection.Input);
            OracleParameter MSS_OUT = new OracleParameter("MSS_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter STR_OUT = new OracleParameter("MSS_OUT", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                if (!IsTaiSanKho)
                {
                    var result = _dbContext.EntityFromStore<MssReturn>("INSERT_TAI_SAN", P_JSON_STRING, P_CHE_DO_HAO_MON, MSS_OUT).ToList();
                    MessageReturn mss = new MessageReturn();
                    if (result != null && result.Count() > 0)
                        mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                    P_JSON_STRING.Dispose();
                    P_CHE_DO_HAO_MON.Dispose();
                    MSS_OUT.Dispose();
                    STR_OUT.Dispose();
                    return mss;
                }
                else
                {
                    var result = _dbContext.EntityFromStore<MssReturn>("INSERT_TAISAN_KHO", P_JSON_STRING, MSS_OUT).ToList();
                    MessageReturn mss = new MessageReturn();
                    mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                    P_JSON_STRING.Dispose();
                    P_CHE_DO_HAO_MON.Dispose();
                    MSS_OUT.Dispose();
                    STR_OUT.Dispose();
                    return mss;
                    //var result = _dbContext.ExecuteScalarSql("INSERT_TAISAN_KHO_TEST", P_JSON_STRING, STR_OUT);
                    //MessageReturn mss = new MessageReturn();
                    //mss = result.toEntity<MessageReturn>();
                    //P_JSON_STRING.Dispose();
                    //P_CHE_DO_HAO_MON.Dispose();
                    //MSS_OUT.Dispose();
                    //STR_OUT.Dispose();
                    //return mss;
                }
            }
            catch (Exception ex)
            {
                P_JSON_STRING.Dispose();
                P_CHE_DO_HAO_MON.Dispose();
                MSS_OUT.Dispose();
                STR_OUT.Dispose();
                //DelelteTaiSanDB(ma_db);
                return new MessageReturn(MessageReturn.ERROR_CODE, ex.Message);
            }

        }
        public virtual MessageReturn insertOrupdateTaiSanDBByJson(string json, string ma_db, bool IsTaiSanKho = false, decimal cheDoHaoMon = 23)
        {
            OracleParameter P_JSON_STRING = new OracleParameter("P_JSON_STRING", OracleDbType.Clob, json, ParameterDirection.Input);
            OracleParameter P_CHE_DO_HAO_MON = new OracleParameter("P_CHE_DO_HAO_MON", OracleDbType.Decimal, cheDoHaoMon, ParameterDirection.Input);
            OracleParameter MSS_OUT = new OracleParameter("MSS_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter STR_OUT = new OracleParameter("MSS_OUT", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                if (!IsTaiSanKho)
                {
                    var result = _dbContext.EntityFromStore<MssReturn>("INSERT_TAI_SAN_DONGBO", P_JSON_STRING, P_CHE_DO_HAO_MON, MSS_OUT).ToList();
                    MessageReturn mss = new MessageReturn();
                    if (result != null && result.Count() > 0)
                        mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                    P_JSON_STRING.Dispose();
                    P_CHE_DO_HAO_MON.Dispose();
                    MSS_OUT.Dispose();
                    STR_OUT.Dispose();
                    return mss;
                }
                else
                {
                    var result = _dbContext.EntityFromStore<MssReturn>("INSERT_TAISAN_KHO", P_JSON_STRING, MSS_OUT).ToList();
                    MessageReturn mss = new MessageReturn();
                    mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                    P_JSON_STRING.Dispose();
                    P_CHE_DO_HAO_MON.Dispose();
                    MSS_OUT.Dispose();
                    STR_OUT.Dispose();
                    return mss;
                    //var result = _dbContext.ExecuteScalarSql("INSERT_TAISAN_KHO_TEST", P_JSON_STRING, STR_OUT);
                    //MessageReturn mss = new MessageReturn();
                    //mss = result.toEntity<MessageReturn>();
                    //P_JSON_STRING.Dispose();
                    //P_CHE_DO_HAO_MON.Dispose();
                    //MSS_OUT.Dispose();
                    //STR_OUT.Dispose();
                    //return mss;
                }
            }
            catch (Exception ex)
            {
                P_JSON_STRING.Dispose();
                P_CHE_DO_HAO_MON.Dispose();
                MSS_OUT.Dispose();
                STR_OUT.Dispose();
                //DelelteTaiSanDB(ma_db);
                return new MessageReturn(MessageReturn.ERROR_CODE, ex.Message);
            }

        }
        public void InsertOrUpdateTaiSanNhaNuoc()
        {
            // get all tài sản chưa đồng bộ
            var query = _itemRepository.Table;
            foreach (var item in query)
            {
                // thông tin đồng bộ

                //Update TaiSan 
                TaiSan taiSan = _taiSanService.GetTaiSanByMa(item.QLDKTS_MA);

            }
        }
        public DBTaiSan GetDuLieuDongBoByGuid(string GUID)
        {
            if (string.IsNullOrEmpty(GUID))
                return null;
            var query = _itemRepository.Table;
            Guid guid = Guid.Parse(GUID);
            return query.Where(m => m.GUID == guid).FirstOrDefault();
        }
        public virtual List<DBTaiSanView> GET_TAI_SAN_CAN_DONG_BO(string maDonVi = null, int TakeNumber = 500,int isBienDong = 0)
        {
            OracleParameter P_MA_DON_VI = new OracleParameter("P_MA_DON_VI", OracleDbType.Varchar2, maDonVi, ParameterDirection.Input);
            OracleParameter P_TAKE_NUMBER = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, TakeNumber, ParameterDirection.Input);
            OracleParameter P_IS_BIEN_DONG = new OracleParameter("P_IS_BIEN_DONG", OracleDbType.Decimal, isBienDong, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<DBTaiSanView>("PKG_DONG_BO.GET_TAI_SAN_DA_NHAN_CAN_DONG_BO", P_MA_DON_VI, P_IS_BIEN_DONG, P_TAKE_NUMBER, TBL_OUT);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual List<DBTaiSanView> GET_BIEN_DONG_CAN_DONG_BO(string DBId = null, int TakeNumber = 500)
        {
            OracleParameter P_MA_DON_VI = new OracleParameter("P_DB_MA", OracleDbType.Varchar2, DBId, ParameterDirection.Input);
            OracleParameter P_TAKE_NUMBER = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, TakeNumber, ParameterDirection.Input);
            OracleParameter TBL_OUT = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromStore<DBTaiSanView>("PKG_DONG_BO.GET_TAI_SAN_DA_NHAN_CAN_DONG_BO", P_MA_DON_VI, P_TAKE_NUMBER, TBL_OUT);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual void UPDATE_TRANG_THAI_DB_TAI_SAN(List<DBTaiSanView> dBTaiSans,decimal trangThai)
        {
            String sql = $"UPDATE DB_TAI_SAN SET TRANG_THAI_ID = {trangThai} WHERE ";
            List<string> lstIds = dBTaiSans.Select(x => String.Format("(1,{0})", x.ID)).ToList();
            sql += string.Format("(1,ID) IN ({0})", string.Join(",", lstIds));
            try
            {
                var rs = _dbContext.ExecuteSqlCommand(sql);
            }
            catch (Exception ex)
            {
                var mss = ex;
            }

            //OracleParameter P_CUR = new OracleParameter("P_CUR", OracleDbType.RefCursor, dBTaiSans, ParameterDirection.Input);
            //OracleParameter P_TRANG_THAI_ID = new OracleParameter("P_TAKE_NUMBER", OracleDbType.Decimal, trangThai, ParameterDirection.Input);
            //try
            //{
            //    _dbContext.EntityFromStore<DBTaiSanView>("PKG_DONG_BO.UPDATE_TRANG_THAI_DB_TAI_SAN", P_CUR, P_TRANG_THAI_ID);
            //}
            //catch (Exception ex)
            //{
            //    var mss = ex;
            //}
        }
        #endregion
    }
}

