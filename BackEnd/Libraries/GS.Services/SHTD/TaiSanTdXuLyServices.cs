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
    public partial class TaiSanTdXuLyService:ITaiSanTdXuLyService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanTdXuLy> _itemRepository;
            private readonly ITaiSanNhatKyService _taiSanNhatKyService;
            private readonly IKetQuaService _ketQuaService;
         #endregion
         
         #region Ctor

        public TaiSanTdXuLyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<TaiSanTdXuLy> itemRepository,
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
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._ketQuaService = ketQuaService;
        }

        #endregion
        
        #region Methods
        public virtual IList<TaiSanTdXuLy> GetAllTaiSanTdXuLys(){
            var query = _itemRepository.Table.Where(c => c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
             return query.ToList();
         }
        public virtual IList<TaiSanTdXuLy> GetAllTaiSanTdXuLyChuaDongBo()
        {
            var query = _itemRepository.Table.Where(c => c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai && c.DB_ID == null);
            return query.ToList();
        }
        public virtual IList<TaiSanTdXuLy> GetTaiSanTdXuLys(int HinhThucXuLy=0, int PhuongAnXuLy =0, List<decimal> ListTSTDId = null,int LoaiXuLy =0,int TaiSanId = 0,decimal quyetDinhId = 0)
        {
            var query = _itemRepository.Table.Where(c=>c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            if (HinhThucXuLy > 0)
            {
                query = query.Where(c => c.HINH_THUC_XU_LY_ID == HinhThucXuLy);
            }
            if (PhuongAnXuLy > 0)
            {
                query = query.Where(c => c.PHUONG_AN_XU_LY_ID == PhuongAnXuLy);
            }
            if(ListTSTDId!= null && ListTSTDId.Count()>0)
            {
                query = query.Where(c => ListTSTDId.Contains(c.TAI_SAN_ID));
            }
            if (TaiSanId > 0)
            {
                query = query.Where(c => TaiSanId==(c.TAI_SAN_ID));
            }
            if (quyetDinhId > 0)
            {
                query = query.Where(c => quyetDinhId == (c.taisantd.quyetdinh.ID));
            }
            //if (LoaiXuLy > 0)
            //{
            //    query = query.Where(c => c.xuly.LOAI_XU_LY_ID == LoaiXuLy);
            //}
            return query.ToList();
        }
        /// <summary>
        /// check xem còn tồn tại các tài sản chưa được chọn paxl, htxl, số lượng không
        /// </summary>
        /// <returns></returns>
        public virtual IList<decimal> CheckTaiSanXuLy(decimal XuLyId)
        {
            //var query = _itemRepository.Table.Where(c=>c.XU_LY_ID == XuLyId && (c.PHUONG_AN_XU_LY_ID == null || c.PHUONG_AN_XU_LY_ID <= 0 || c.HINH_THUC_XU_LY_ID == null || c.HINH_THUC_XU_LY_ID <= 0 || c.SO_LUONG == null || c.SO_LUONG <= 0)).Select(c=>c.ID).ToList();
            var query = _itemRepository.Table.Where(c => c.XU_LY_ID == XuLyId && ((c.PHUONG_AN_XU_LY_ID == null || c.PHUONG_AN_XU_LY_ID <= 0)  && ( c.SO_LUONG != null || c.SO_LUONG >= 0))).Select(c => c.ID).ToList();
            return query;
        }
        /// <summary>
        /// check xem đã tạo tài sản xử lý chưa
        /// </summary>
        /// <param name="XuLyId"></param>
        /// <returns></returns>
        public virtual IList<decimal> CheckDaTaoTSXL(decimal XuLyId)
        {
            var query = _itemRepository.Table.Where(c => c.XU_LY_ID == XuLyId).Select(c => c.ID).ToList();
            return query;
        }
        public virtual IPagedList <TaiSanTdXuLy> SearchTaiSanTdXuLys(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null,int XuLyId =0,bool is_null = false,decimal DonViId = 0, bool isKetQua = false)
        {
             var query = _itemRepository.Table.Where(c => c.xuly.TRANG_THAI_ID == (decimal)enumTrangThaiXuLy.TonTai && c.xuly.DON_VI_ID == DonViId); ;
            if (XuLyId > 0)
            {
                query = query.Where(c => c.XU_LY_ID == XuLyId);
            }
            if (isKetQua)
            {
                var kq = _ketQuaService.GetKetQuaBys(ListTaiSanTDXuLy: query.Select(c => c.ID).ToList()).Select(c => c.TAI_SAN_TD_XU_LY_ID).ToList();
                query = query.Where(c => kq.Contains(c.ID));

            }
            //trả về list rỗng
            if (is_null)
            {
                query = query.Where(c => c.XU_LY_ID == 0);
            }
             return new PagedList<TaiSanTdXuLy>(query.OrderByDescending(c=>c.xuly.QUYET_DINH_NGAY), pageIndex, pageSize);;
         }
        public virtual IPagedList<TaiSanTdXuLy> SearchTaiSanTdXuLysForPhuongAn(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int XuLyId = 0, bool is_null = false, decimal TrangThai = (decimal)enumTrangThaiXuLy.TonTai, decimal DonViId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.XU_LY_ID == XuLyId && c.xuly.DON_VI_ID == DonViId);
            if(TrangThai == (decimal)enumTrangThaiXuLy.Nhap)
            {
                query = query.Where(c => c.xuly.TRANG_THAI_ID == TrangThai);
            }
            else
            {
                query = query.Where(c => c.xuly.TRANG_THAI_ID == (decimal)enumTrangThaiXuLy.TonTai);
            }
            return new PagedList<TaiSanTdXuLy>(query.OrderBy(c => c.taisantd.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.TAI_SAN_ID).ThenBy(c=>c.taisantd.TEN).ThenBy(c => c.ID), pageIndex, pageSize); ;
        }
        public virtual TaiSanTdXuLy GetTaiSanTdXuLyById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual TaiSanTdXuLy GetTaiSanTdXuLyByDB_ID(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == DB_Id).FirstOrDefault();
        }
        public virtual TaiSanTdXuLy GetTaiSanTdXuLyByGuId(Guid Guid)
        {
           
            return _itemRepository.Table.Where(c=>Guid == c.GUID).FirstOrDefault();
        }
        public virtual IList<TaiSanTdXuLy> GetTaiSanTdXuLyByTaiSanIdAndXuLyId(decimal TaiSanId=0,decimal XuLyId =0)
        {
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId && c.XU_LY_ID == XuLyId && c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai).ToList();
        }
        public virtual IList<TaiSanTdXuLy> GetTaiSanTdsXuLyByXuLyId(decimal XuLyId = 0, decimal TrangThaiId = (int)enumTrangThaiXuLy.TonTai, decimal? soluong = null)
        {
            if (XuLyId == 0)
                return null;
            if (TrangThaiId != (int)enumTrangThaiXuLy.Nhap)
            {
                TrangThaiId = (int)enumTrangThaiXuLy.TonTai;
            }
            var query =  _itemRepository.Table.Where(c => c.XU_LY_ID == XuLyId && c.xuly.TRANG_THAI_ID == TrangThaiId);
            if (soluong != null)
            {
                query = query.Where(c => c.SO_LUONG == soluong);
            }
            return query.ToList();
        }
        public virtual IList<TaiSanTdXuLy> GetTaiSanTdXuLyByIds(decimal[] Ids){
            var query = _itemRepository.Table.Where(c => c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }
        public virtual IList<TaiSanTdXuLy> GetTaiSanTdXuLysByTaiSanId(decimal TaiSanId=0)
        {
            if(TaiSanId==0)
            {
                return null;
            }
            var query = _itemRepository.Table.Where(c => c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            return query.Where(c => c.TAI_SAN_ID == TaiSanId).ToList();
        }

        public virtual void InsertTaiSanTdXuLy(TaiSanTdXuLy entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            // tài sản nhật ký
            var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)entity.TAI_SAN_ID, isTaiSanToanDan: true).FirstOrDefault();
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
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanTdXuLy(TaiSanTdXuLy entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            // tài sản nhật ký
            var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)entity.TAI_SAN_ID, isTaiSanToanDan: true).FirstOrDefault();
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
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanTdXuLy(TaiSanTdXuLy entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            // tài sản nhật ký
            var taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKys(taiSanID: (int)entity.TAI_SAN_ID, isTaiSanToanDan: true).FirstOrDefault();
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

