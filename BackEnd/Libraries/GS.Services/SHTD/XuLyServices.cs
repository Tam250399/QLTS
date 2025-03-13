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

namespace GS.Services.SHTD
{
    public partial class XuLyService:IXuLyService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<XuLy> _itemRepository;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
            private readonly ITaiSanTdService _taiSanTdService;
            private readonly ITaiSanNhatKyService _taiSanNhatKyService;
            private readonly IKetQuaService _ketQuaService;
         #endregion
         
         #region Ctor

        public XuLyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<XuLy> itemRepository,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ITaiSanTdService taiSanTdService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IKetQuaService ketQuaService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._ketQuaService = ketQuaService;
        }

        #endregion
        
        #region Methods
        public virtual IList<XuLy> GetAllXuLys(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<XuLy> GetAllXuLyChuaDongBo()
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai && c.DB_ID == null);
            return query.ToList();
        }

        public virtual IPagedList <XuLy> SearchXuLys(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null,int LoaiXuLy =0,string SoQuyetDinh = null,DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, DateTime? NgayXuLyTu = null, DateTime? NgayXuLyDen = null, int LoaiTaiSan = 0,int HinhThucXuLy=0,int PhuongAnXuLy = 0,decimal DonViId = 0)
        {
            
             var query = _itemRepository.Table.Where(c=>c.DON_VI_ID != null && c.DON_VI_ID == DonViId && c.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            if (Keysearch != null)
            {
                query = query.Where(c => c.QUYET_DINH_SO.ToUpper().Contains(Keysearch.ToUpper()));
            }
            if (LoaiXuLy >0)
            {
                var listKetQuaId = _ketQuaService.GetAllKetQuas().Select(c => c.taiSanTdXuLy.XU_LY_ID).Distinct().ToList();
                if(LoaiXuLy== (int)enumLoaiXuLy.PhuongAn)
                {
                    query = query.Where(c => !listKetQuaId.Contains(c.ID));
                }
                if (LoaiXuLy == (int)enumLoaiXuLy.KetQua)
                {
                    query = query.Where(c => listKetQuaId.Contains(c.ID));
                }

            }
            if (SoQuyetDinh != null && SoQuyetDinh.Trim() != "")
            {
                query = query.Where(c => c.QUYET_DINH_SO.Contains(SoQuyetDinh));
            }
            if (NgayQuyetDinhTu != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY >= NgayQuyetDinhTu);
            }
            if (NgayQuyetDinhDen != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY <= NgayQuyetDinhDen);
            }
            if (NgayXuLyTu != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY >= NgayXuLyTu);
            }
            if (NgayXuLyDen != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY <= NgayXuLyDen);
            }
            if (HinhThucXuLy >0)
            {
                var listTSXL = _taiSanTdXuLyService.GetTaiSanTdXuLys(HinhThucXuLy: HinhThucXuLy).Select(c=>c.XU_LY_ID).ToList();
                query = query.Where(c => listTSXL.Contains(c.ID));
            }
            if (PhuongAnXuLy > 0)
            {
                var listTSXL = _taiSanTdXuLyService.GetTaiSanTdXuLys(PhuongAnXuLy: PhuongAnXuLy).Select(c => c.XU_LY_ID).ToList();
                query = query.Where(c => listTSXL.Contains(c.ID));
            }
            if (NgayXuLyDen != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY <= NgayXuLyDen);
            }
            if (LoaiTaiSan >0)
            {
                var listqd = (from ts in _taiSanTdService.GetAllTaiSanTds() join tsxl in _taiSanTdXuLyService.GetAllTaiSanTdXuLys() on ts.ID equals tsxl.TAI_SAN_ID where ts.LOAI_TAI_SAN_ID == LoaiTaiSan select tsxl.XU_LY_ID).ToList();
                query = query.Where(c => listqd.Contains(c.ID));
            }
            return new PagedList<XuLy>(query.OrderByDescending(c=>c.QUYET_DINH_NGAY), pageIndex, pageSize);;
         }    
        
        public virtual XuLy GetXuLyById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual XuLy GetXuLyByDB_Id(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == DB_Id && c.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai).FirstOrDefault();
        }
        public virtual XuLy GetXuLyByGuid(Guid Guid)
        {
            if(Guid == new Guid())
            {
                return null;
            }
            return _itemRepository.Table.Where(c => c.GUID == Guid).FirstOrDefault();
        }
        public virtual IList<XuLy> GetXuLyByIds(IList<decimal> Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual IList<XuLy> GetPhuongAnXuLyTaiSans(decimal DonViId)
        {
            var listKetQuaId = _ketQuaService.GetAllKetQuas().Select(c => c.taiSanTdXuLy.XU_LY_ID).Distinct().ToList();
            var query = _itemRepository.Table.Where(c=>c.DON_VI_ID== DonViId && c.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai && !listKetQuaId.Contains(c.ID));
            return query.ToList();
        }
        public virtual IList<XuLy> GetKetQuaXuLyTaiSans(decimal DonViId)
        {
            var listKetQuaId = _ketQuaService.GetAllKetQuas().Select(c => c.taiSanTdXuLy.XU_LY_ID).Distinct().ToList();
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == DonViId && c.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai && listKetQuaId.Contains(c.ID));
            return query.ToList();
        }
        public virtual void InsertXuLy(XuLy entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateXuLy(XuLy entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            // tài sản nhật ký
            var tsxltds = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(entity.ID);
            if (tsxltds.Count() > 0)
            {
                foreach (var tsxltd in tsxltds)
                {
                    var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)tsxltd.TAI_SAN_ID, isTaiSanToanDan: true).FirstOrDefault();
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
            }
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteXuLy(XuLy entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            // tài sản nhật ký
            var tsxltds = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(entity.ID);
            if (tsxltds.Count() > 0)
            {
                foreach (var tsxltd in tsxltds)
                {
                    var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)tsxltd.TAI_SAN_ID, isTaiSanToanDan: true).FirstOrDefault();
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
            }
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        #endregion
    }
}		

