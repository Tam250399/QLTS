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
using GS.Core.Domain.SHTD;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Services.DanhMuc;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.SHTD
{
    public partial class TaiSanTdService:ITaiSanTdService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanTd> _itemRepository;
            private readonly IRepository<TaiSanTdXuLy> _itemRepositoryTaiSanTdXuLy;
        private readonly IRepository<QuyetDinhTichThu> _quyetDinhTichThuRepository;
        private readonly IRepository<XuLy> _itemRepositoryXuLy;
        private readonly IRepository<DonVi> _donViRepository;
            private readonly ITaiSanNhatKyService _taiSanNhatKyService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly INguonGocTaiSanService _nguonGocTaiSanService;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IDonViService _donViService;

        #endregion

        #region Ctor

        public TaiSanTdService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanTd> itemRepository,
            IRepository<TaiSanTdXuLy> itemRepositoryTaiSanTdXuLy,
            IRepository<QuyetDinhTichThu> quyetDinhTichThuRepository,
            IRepository<DonVi> donViRepository,
            ITaiSanNhatKyService taiSanNhatKyService,
            IWorkContext workContext,
            ILoaiTaiSanService loaiTaiSanService,
            INguonGocTaiSanService nguonGocTaiSanService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IDonViService donViService,
            IRepository<XuLy> itemRepositoryXuLy
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._itemRepositoryTaiSanTdXuLy = itemRepositoryTaiSanTdXuLy;
            this._quyetDinhTichThuRepository = quyetDinhTichThuRepository;
            this._donViRepository = donViRepository;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._workContext = workContext;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._donViService = donViService;
            this._itemRepositoryXuLy = itemRepositoryXuLy;
        }

        #endregion
        
        #region Methods
        public virtual IList<TaiSanTd> GetAllTaiSanTds(){
            var query = _itemRepository.Table.Where(c=>c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI);
             return query.ToList();
         }
        public virtual IList<TaiSanTd> GetAllTaiSanTdsChuaDongBo()
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI 
                                                    && (c.DB_ID == null || c.DB_ID =="0")
                                                    && c.QUYET_DINH_TICH_THU_ID != null);
            var quyetDinhTichTHu = _quyetDinhTichThuRepository.Table;
            query = from q in query
                    join qdtt in quyetDinhTichTHu on q.QUYET_DINH_TICH_THU_ID equals qdtt.ID
                    where qdtt.DB_ID != null
                    select q;

            return query.ToList();
        }
        public virtual IList<TaiSanTd> GetTaiSanNhaNhapTrenDats(Decimal TaiSanDatId = 0,List<decimal> ListNhaId = null)
        {
            var query =  _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.NHAP);
            if (TaiSanDatId > 0)
            {
                query = query.Where(c => c.TAI_SAN_DAT_ID == TaiSanDatId);
            }
            if(ListNhaId!= null && ListNhaId.Count() > 0)
            {
                query = query.Where(c => ListNhaId.Contains(c.ID));
            }
            return query.ToList();
        }
        public virtual IList<TaiSanTd> GetTaiSanTds(List<int> ListTaiSanKhongChon = null,Decimal? LoaiHinhTaiSan = null,Decimal TaiSanDatId = 0,Decimal QuyetDinhId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI && c.quyetdinh.DON_VI_ID ==_workContext.CurrentDonVi.ID);
            if(ListTaiSanKhongChon!= null && ListTaiSanKhongChon.Count() > 0)
            {
                query = query.Where(c => !ListTaiSanKhongChon.Contains((int)c.ID));
            }
            if(LoaiHinhTaiSan!= null)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            }
            if (TaiSanDatId >0)
            {
                query = query.Where(c => c.TAI_SAN_DAT_ID == TaiSanDatId);
            }
            if (QuyetDinhId > 0)
            {
                query = query.Where(c => c.QUYET_DINH_TICH_THU_ID == QuyetDinhId);
            }
            return query.ToList();
        }
        public virtual IList<TaiSanTd> GetTaiSanTdByPhuongAn(Decimal QuyetDinhId = 0)
        {
            var listTSTD = new List<TaiSanTd>();
            //lấy tài sản theo quyết định
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI && c.quyetdinh.DON_VI_ID == _workContext.CurrentDonVi.ID && c.QUYET_DINH_TICH_THU_ID == QuyetDinhId);
            // lấy tài sản xử lý
            var listTSTDXL = _taiSanTdXuLyService.GetTaiSanTdXuLys(ListTSTDId: query.Select(c => c.ID).ToList()).GroupBy(c => c.TAI_SAN_ID).Select(c => new TaiSanTdXuLy()
            {
                ID = c.Key,
                SO_LUONG = c.Sum(m => m.SO_LUONG != null ? m.SO_LUONG : 0)
            }).ToList();
            
            //var x = query.Where(c => !(listTSTDXL.Where(m => m.ID == c.ID).Select(n => n.SO_LUONG).FirstOrDefault() >= c.GIA_TRI_TINH)).Select(c=> new TaiSanTd() { 
            //    ID=c.ID,
            //    SO_LUONG = c.GIA_TRI_TINH - listTSTDXL.Where(m => m.ID == c.ID).Select(n => n.SO_LUONG).FirstOrDefault()??0
            //}).ToList();
            foreach (var ts in query)
            {
                var tstd = new TaiSanTd() { ID = ts.ID, SO_LUONG = ts.GIA_TRI_TINH };
                var tsxl = listTSTDXL.Where(c => c.ID == ts.ID).FirstOrDefault();
                if (tsxl != null)
                {
                    var soluongcon = ts.GIA_TRI_TINH - tsxl.SO_LUONG;
                    if (soluongcon > 0)
                    {
                        tstd.SO_LUONG = soluongcon;
                        listTSTD.Add(tstd);
                    }
                }
                else
                {
                    listTSTD.Add(tstd);
                }
            }
            //var tsDaXL = (from tstd in query
            //              join tstdxl in _itemRepositoryTaiSanTdXuLy.Table on tstd.ID equals tstdxl.TAI_SAN_ID into ps
            //              from tstdxl in ps.DefaultIfEmpty()
            //              group tstdxl by tstdxl.TAI_SAN_ID into key
            //              where key.Select(c => c.SO_LUONG != null ? c.SO_LUONG : 0).Sum() < query.Where(c => c.ID == key.Select(m => m.TAI_SAN_ID).FirstOrDefault()).Select(c => c.SO_LUONG != null ? c.SO_LUONG : 0).FirstOrDefault()
            //              select new TaiSanTd() {ID = key.Key }).ToList();


            return listTSTD;
        }
        public virtual IList<TaiSanTd> GetTaiSansChuaFullSoLuongForKetQua(List<ListSoLuong> listSL = null,List<int> listTaiSanTd = null)
        {
            var listtsxl = (from tsxl in _itemRepositoryTaiSanTdXuLy.Table 
                            where tsxl.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai
                            group tsxl by tsxl.TAI_SAN_ID into g
                            select new TaiSanTdXuLy
                            {
                                TAI_SAN_ID = g.Key,
                                SO_LUONG = g.Select(c => c.SO_LUONG).Sum()
                            }).ToList();
            if(listSL!= null && listSL.Count() > 0)
            {
                //cộng thêm số lượng nếu đã có
                foreach(var tsxl in listtsxl)
                {
                    var SL = listSL.Where(c => c.TAI_SAN_ID == tsxl.TAI_SAN_ID).FirstOrDefault();
                    if (SL != null)
                    {
                        tsxl.SO_LUONG += SL.SO_LUONG;
                        listSL.Remove(SL);
                    };                    
                }
                //thêm TaiSanTdXuLy nếu chưa tồn tại trong list
                foreach (var SL in listSL)
                {
                    listtsxl.Add(new TaiSanTdXuLy { TAI_SAN_ID = SL.TAI_SAN_ID, SO_LUONG = SL.SO_LUONG });
                }
            }           
            if (listtsxl.Count() > 0)
            {
                var list = (from ts in _itemRepository.Table
                             join tsxl in listtsxl on ts.ID equals tsxl.TAI_SAN_ID                             
                             where ts.SO_LUONG <= tsxl.SO_LUONG && ts.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI
                            select ts.ID);
                var query = _itemRepository.Table.Where(c => !list.Contains(c.ID) && listTaiSanTd.Contains((int)c.ID) && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI).ToList();
                return query;
            }
            else
            {
                var query = _itemRepository.Table.Where(c=>listTaiSanTd.Contains((int)c.ID) && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI).ToList();
                return query;
            }            
        }
        public IList<TaiSanTd> GetTaiSansChuaFullSoLuongForDeXuat(List<ListSoLuong> listSL = null, List<int> listTaiSanTd = null) 
        {
            if (listSL.Count() > 0)
            {
                var list = (from ts in _itemRepository.Table
                            join tsxl in listSL on ts.ID equals tsxl.TAI_SAN_ID
                            where ts.SO_LUONG <= tsxl.SO_LUONG && ts.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI
                            select ts.ID);
                var query = _itemRepository.Table.Where(c => !list.Contains(c.ID) && listTaiSanTd.Contains((int)c.ID) && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI).ToList();
                return query;
            }
            else
            {
                var query = _itemRepository.Table.Where(c => listTaiSanTd.Contains((int)c.ID) && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI).ToList();
                return query;
            }
        }
        public virtual IPagedList <TaiSanTd> SearchTaiSanTds(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null,int QuyetDinhId =0 ,int NguonGocTaiSan =0,int LoaiTaiSan=0,string TenTaiSan = null, DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, string SoQuyetDinh = null,int TaiSanDatId = 0, int TrangThaiID= (int)enumTRANGTHAITSTD.TONTAI,List<decimal> ListNhaNhapId = null)
        {
            
            var donVi = _donViRepository.GetById(_workContext.CurrentDonVi.ID);
            var query = _itemRepository.Table.Where(c=> c.quyetdinh.DON_VI_ID == _workContext.CurrentDonVi.ID || c.quyetdinh.DonVi.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            if(TrangThaiID > 0 && TrangThaiID == (int)enumTRANGTHAITSTD.NHAP)
            {
                query = query.Where(c => c.TRANG_THAI_ID == TrangThaiID);
            }
            else
            {
                query = query.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI);
            }
            if (TaiSanDatId > 0)
            {
                query = query.Where(c => c.TAI_SAN_DAT_ID == TaiSanDatId);
            }
            if (ListNhaNhapId !=null && ListNhaNhapId.Count()>0)
            {
                query = query.Where(c => ListNhaNhapId.Contains(c.ID));
            }
            if (!String.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            if (!String.IsNullOrEmpty(SoQuyetDinh))
            {
                query = query.Where(c => c.quyetdinh.QUYET_DINH_SO.ToLower().Contains(SoQuyetDinh.ToLower()));
            }
            if (QuyetDinhId > 0)
            {
                query = query.Where(c => c.QUYET_DINH_TICH_THU_ID == QuyetDinhId);
            }
            if(!String.IsNullOrEmpty(TenTaiSan))
            {
                query = query.Where(c => c.TEN.ToLower().Contains(TenTaiSan.ToLower()));
            }
            if (LoaiTaiSan > 0)
            {
                var loaiTS = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSan);
                var listidTS = _loaiTaiSanService.GetLoaiTaiSans(TreeNode: loaiTS.TREE_NODE).Select(c => (decimal?)c.ID).ToList();
                query = query.Where(c => listidTS.Contains(c.LOAI_TAI_SAN_ID));
            }
            if (NguonGocTaiSan > 0)
            {
                var nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanById(NguonGocTaiSan);
                var listIdNG = _nguonGocTaiSanService.GetNguonGocTaiSans(TreeNode: nguonGoc.TREE_NODE).Select(c => (decimal?)c.ID).ToList();
                query = query.Where(c => listIdNG.Contains(c.quyetdinh.NGUON_GOC_ID));
            }
            if(NgayQuyetDinhTu != null)
            {
                query = query.Where(c => c.quyetdinh != null && c.quyetdinh.QUYET_DINH_NGAY.Value >= NgayQuyetDinhTu);
            }
            if(NgayQuyetDinhDen != null)
            {
                query = query.Where(c => c.quyetdinh != null && c.quyetdinh.QUYET_DINH_NGAY.Value < NgayQuyetDinhDen);
            }
            return new PagedList<TaiSanTd>(query.OrderBy(c=>c.LOAI_TAI_SAN_ID), pageIndex, pageSize);;
         }
        public virtual decimal? GetSoLuongConByTaiSanId(decimal Id,decimal SoLuong = 0,int LoaiXuLy = 0,decimal xulyid = 0)
        {
            if (Id == 0)
                return null;
            decimal soluongTSXL = 0;
            // lấy tất cả các tsxl đã lưu trước đó 
            var listTSXL = _itemRepositoryTaiSanTdXuLy.Table.Where(c => c.TAI_SAN_ID == Id && c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            if (listTSXL.Count() > 0)
            {
                soluongTSXL = (decimal)(from tsxl in listTSXL
                                    group tsxl by tsxl.TAI_SAN_ID into g
                                    select g.Select(c => c.SO_LUONG).Sum()
                                ).FirstOrDefault();
            }            
            // lấy trong xử lý nháp
            if (xulyid > 0)
            {
                var listTSXL_Nhap = _itemRepositoryTaiSanTdXuLy.Table.Where(c => c.TAI_SAN_ID == Id && c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.Nhap && c.xuly.ID == xulyid);
                if (listTSXL_Nhap.Count() > 0)
                {
                    soluongTSXL += (decimal)(from tsxl in listTSXL_Nhap
                                        group tsxl by tsxl.TAI_SAN_ID into g
                                        select g.Select(c => c.SO_LUONG).Sum()
                                    ).FirstOrDefault();
                }
            }
            var sl = (decimal)(from ts in _itemRepository.Table                   
                     where ts.ID == Id && ts.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI
                           select ts.GIA_TRI_TINH-(soluongTSXL+ SoLuong)).FirstOrDefault();
            return sl;
        }
        public virtual TaiSanTd GetTaiSanTdById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual TaiSanTd GetTaiSanTdByDB_ID(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == DB_Id && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI).FirstOrDefault();
        }
        /// <summary>
        /// quyết định tịch thu còn tài sản chưa xử lý
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual List<decimal> GetQuyetDinhTichThuCon(Decimal DonViId, List<int> ListQuyetDinhId =null)
        {
            var lisTongSoTSTD = _itemRepository.Table.Where(c => c.quyetdinh.DON_VI_ID == DonViId && c.quyetdinh.TRANG_THAI_ID == (int)GS.Core.Domain.SHTD.QuyetDinhTichThu.enumTRANGTHAI_QUYETDINH_TSTD.DUYET && c.TRANG_THAI_ID == (int)enumTRANGTHAITSTD.TONTAI)
                                                     .GroupBy(c => c.QUYET_DINH_TICH_THU_ID)
                                                     .Select(m => new TaiSanTd() {
                QUYET_DINH_TICH_THU_ID = m.Key,
                SO_LUONG = m.Sum(n=>n.GIA_TRI_TINH)
            }).ToList();
            var listTongSoTSXL = _itemRepositoryTaiSanTdXuLy.Table.Where(c => c.xuly.DON_VI_ID == DonViId && c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai)
                .GroupBy(c => c.taisantd.QUYET_DINH_TICH_THU_ID)
                .Select(m => new TaiSanTd()
            {
                QUYET_DINH_TICH_THU_ID = m.Key,
                SO_LUONG = m.Sum(n => n.SO_LUONG)
            }).ToList();
          
           var listID = lisTongSoTSTD.Where(c => !(listTongSoTSXL.Where(m => m.QUYET_DINH_TICH_THU_ID == c.QUYET_DINH_TICH_THU_ID && m.SO_LUONG >= c.SO_LUONG)
                                     .FirstOrDefault() != null))
                                     .Select(c => (decimal)c.QUYET_DINH_TICH_THU_ID)
                                     .ToList();
                
            return listID;
        }
        public virtual TaiSanTd GetTaiSanTdByGuid(Guid guid)
        {
            return _itemRepository.Table.Where(c=>c.GUID == guid).FirstOrDefault();
        }
        public virtual IList<TaiSanTd> GetTaiSanTdByQuyetDinhId(decimal QuyetDinhId=0)
        {
            if (QuyetDinhId == 0)
                return null;
            var query = _itemRepository.Table.Where(c=>c.QUYET_DINH_TICH_THU_ID == QuyetDinhId);
            return query.ToList();
        }
        public virtual IList<TaiSanTd> GetTaiSanTdByListQuyetDinhId(List<int> ListQuyetDinhId)
        {
            var query = _itemRepository.Table.Where(c => ListQuyetDinhId.Contains((int)c.QUYET_DINH_TICH_THU_ID));
            return query.ToList();
        }

        public virtual IList<TaiSanTd> GetTaiSanTdByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertTaiSanTd(TaiSanTd entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            if (entity.TRANG_THAI_ID != (int)enumTRANGTHAITSTD.NHAP)
            {
                // tài sản nhật ký          
                var taiSanNhatKy = new TaiSanNhatKy()
                {
                    //TAI_SAN_ID = entity.ID,
                    NGAY_CAP_NHAT = DateTime.Now,
                    IS_TAI_SAN_TOAN_DAN = true,
                    TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO,
                    QUYET_DINH_TICH_THU_ID = entity.QUYET_DINH_TICH_THU_ID
                };
                _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
            }
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanTd(TaiSanTd entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            if (entity.TRANG_THAI_ID != (int)enumTRANGTHAITSTD.NHAP)
            {
                // tài sản nhật ký
                var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)entity.ID, isTaiSanToanDan: true).FirstOrDefault();
                if (taiSanNhatKy != null)
                {
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
                    {
                        taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                    }
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
            }
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanTd(TaiSanTd entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)entity.ID, isTaiSanToanDan: true).FirstOrDefault();
            if (taiSanNhatKy != null)
            {
                taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
                {
                    taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                }
                _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            }
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

