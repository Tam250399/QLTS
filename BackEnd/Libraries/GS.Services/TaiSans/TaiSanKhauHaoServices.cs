//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
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
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial class TaiSanKhauHaoService:ITaiSanKhauHaoService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<TaiSanKhauHao> _itemRepository;
         #endregion
         
         #region Ctor

        public TaiSanKhauHaoService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<TaiSanKhauHao> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
        }

        #endregion
        
        #region Methods
        public virtual IList<TaiSanKhauHao> GetAllTaiSanKhauHaos(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <TaiSanKhauHao> SearchTaiSanKhauHaos(int pageIndex = 0, int pageSize = int.MaxValue,decimal? TaiSanId = 0){
             var query = _itemRepository.Table;
            if (TaiSanId > 0)
            {
                query = query.Where(c => c.TAI_SAN_ID == TaiSanId).OrderByDescending(c=>c.NGAY_BAT_DAU);
            }
             return new PagedList<TaiSanKhauHao>(query, pageIndex, pageSize);;
         }

        public virtual List<TaiSanKhauHao> GetListTaiSanKhauHaoByTaiSanId(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.Table.Where(c=>c.TAI_SAN_ID == Id).OrderByDescending(c=>c.NGAY_BAT_DAU).ToList();
        }
        public virtual TaiSanKhauHao GetListTaiSanKhauHaoByTaiSanIdAndNgayBatDau(decimal Id,DateTime? ngaybatdau = null)
        {
            if (Id == 0)
                return null;
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == Id);
            if (ngaybatdau!=null) { 
                query = query.Where(c=> c.NGAY_BAT_DAU == ngaybatdau).OrderByDescending(c => c.ID);
            }
            return query.FirstOrDefault();
        }
        public virtual TaiSanKhauHao GetTaiSanKhauHaoMoiNhatByTaiSanId(decimal Id)
        {
            if (Id == 0)
                return null;
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == Id).OrderByDescending(c=>c.NGAY_BAT_DAU).FirstOrDefault();
            return query;
        }
        public virtual TaiSanKhauHao GetTaiSanKhauHaoIdMaxByTaiSanId(decimal Id)
        {
            if (Id == 0)
                return null;
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == Id).OrderByDescending(c => c.ID).FirstOrDefault();
            return query;
        }
        public virtual TaiSanKhauHao GetTaiSanKhauHaoById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<TaiSanKhauHao> GetTaiSanKhauHaoByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertTaiSanKhauHao(TaiSanKhauHao entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateTaiSanKhauHao(TaiSanKhauHao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanKhauHao(TaiSanKhauHao entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public bool CheckTsKhauHaobyIdandNgay(decimal? id, DateTime? NgayTinhKhauHao)
        {
            if (id == 0 || id == null)
                return false;
            if (NgayTinhKhauHao == null)
                return false;
            return _itemRepository.Table.Any(c => c.TAI_SAN_ID == id && c.NGAY_BAT_DAU <= NgayTinhKhauHao);
        }
        public bool CheckTrungNgayBatDau(DateTime? ngaybatdau, decimal taiSanid = 0, decimal taiSanKhauHaoId =0)
        {
            if (ngaybatdau == null)
            {
                return false;
            }
            var tskhMax = GetTaiSanKhauHaoMoiNhatByTaiSanId(taiSanid);
            //nếu tồn tại 1 chế độ khấu hao rồi thì quy định ngày bắt đầu và kết thúc.
            if (taiSanKhauHaoId == 0)
            {
                if (tskhMax != null)
                {
                    // Nếu là 
                    if (tskhMax.ID == taiSanKhauHaoId)
                    {
                        return true;
                    }
                    // nếu đã quy định ngày kết thúc thì ngày bắt đầu phải > ngày kết thúc
                    if (tskhMax.NGAY_KET_THUC != null)
                    {
                        return (ngaybatdau > (tskhMax.NGAY_KET_THUC.Value.AddDays(-1)));
                    }
                    else //coi ngày kết thúc = today -1;
                    {
                        return (ngaybatdau > DateTime.Today.AddDays(-1));
                    }
                }
            }
            else
            {
                
                if (taiSanKhauHaoId == tskhMax?.ID)
                {
                    var item = GetTaiSanKhauHaoById(taiSanKhauHaoId);
                    return !(_itemRepository.Table.Any(c => c.NGAY_BAT_DAU >= item.NGAY_BAT_DAU));
                }
            }
   
            return true;
        }
        public int CountTaiSanKhauHao(decimal tai_san_id = 0 ,DateTime? ngaytinhkhauhao = null)
        {
            var query = _itemRepository.Table.Where(c => c.TAI_SAN_ID == tai_san_id);
            if (ngaytinhkhauhao.HasValue)
            {
                query = query.Where(c => c.NGAY_BAT_DAU >= ngaytinhkhauhao);
            }
            return query.Count();
        }
        #endregion
    }
}		

