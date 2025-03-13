//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
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

namespace GS.Services.SHTD
{
    public partial class ThuChiService:IThuChiService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<ThuChi> _itemRepository;
        private readonly IRepository<XuLy> _xuLyRepository;
        private readonly IWorkContext _workContext;
        private readonly IXuLyService _xuLyService;
        #endregion

        #region Ctor

        public ThuChiService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<ThuChi> itemRepository,
            IRepository<XuLy> xuLyRepository,
            IWorkContext workContext, 
            IXuLyService xuLyService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._xuLyRepository = xuLyRepository;
            this._workContext =workContext;
            this._xuLyService = xuLyService;
        }

        #endregion
        
        #region Methods
        public virtual IList<ThuChi> GetAllThuChis(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        public virtual IList<ThuChi> GetAllThuChiChuaDongBo()
        {
            var query = _itemRepository.Table;
            var xuLy = _xuLyRepository.Table;
            query = from q in query
                    join xl in xuLy on q.XU_LY_ID equals xl.ID
                    where xl.DB_ID != null
                    select q;
            return query.ToList();
        }
        public virtual IList<ThuChi> GetThuChis(decimal KetQuaId = 0, string ListThuChiId = null)
        {
            var query = _itemRepository.Table;
            if (KetQuaId > 0)
            {
                query = query.Where(c => c.XU_LY_ID == KetQuaId);
            }
            if (!string.IsNullOrEmpty(ListThuChiId))
            {
                query = query.Where(c => c.LIST_XU_LY_ID == ListThuChiId);
            }
            return query.ToList();
        }
        public virtual ThuChi GetThuChiDauTien(string ListThuChiId = null)
        {
            var ListThuChi = GetThuChis(ListThuChiId: ListThuChiId);

            if (ListThuChi != null && ListThuChi.Count() > 0)
            {
                var ThuChiDauTien = ListThuChi.OrderBy(c => c.ID).FirstOrDefault();
                return ThuChiDauTien;
            }
            return null;
         }

             public virtual void UpdateSoTienThuChi(string ListThuChiId = null)
        {
            var ListThuChi = GetThuChis(ListThuChiId: ListThuChiId);

            if (ListThuChi != null && ListThuChi.Count() > 1)
            {
                ListThuChi = ListThuChi.OrderBy(c => c.NGAY_THU).ThenBy(c => c.ID).ToList();
                for (int i = 0; i < ListThuChi.Count(); i++)
                {
                    if (i != 0)
                    {
                        var ThuChiUpdate = ListThuChi[i];
                        var ThuChiBefore = ListThuChi[i - 1];
                        ThuChiUpdate.SO_TIEN_PHAI_THU = ThuChiBefore.SO_TIEN_CON_PHAI_THU ?? 0;
                        var SoTienConPhaiThu = (ThuChiUpdate.SO_TIEN_PHAI_THU - ThuChiUpdate.SO_TIEN_THU ?? 0);
                        if (SoTienConPhaiThu < 0)
                        {
                            ThuChiUpdate.SO_TIEN_CON_PHAI_THU = 0;
                            ThuChiUpdate.SO_TIEN_THU = ThuChiUpdate.SO_TIEN_PHAI_THU;
                        }
                        else
                        {
                            ThuChiUpdate.SO_TIEN_CON_PHAI_THU = SoTienConPhaiThu;
                        }
                        _itemRepository.Update(ThuChiUpdate);

                    }
                }
            }
        }



        public virtual IPagedList <ThuChi> SearchThuChis(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null,decimal KetQuaId = 0, string ListXuLyIdString = null){

            //var query = _itemRepository.Table.Where(c=>c.xuLy.DON_VI_ID == _workContext.CurrentDonVi.ID && c.xuLy.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai);
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID);
                                                                                  
            //if (Keysearch!= null)
            //{
            //    //query = query.Where(c => c.xuLy.QUYET_DINH_SO.Contains(Keysearch));
            //    var ListXyLyFilter = ListXuLyDonVi.Where(c => c.QUYET_DINH_SO.Contains(Keysearch)).Select(c => c.ID);
            //    query = query.Where(c => c.LIST_XU_LY_ID.ToListInt().Any(t => ListXyLyFilter.Contains(t)));
            //}
            //if (KetQuaId >0)
            //{
            //    query = query.Where(c => c.XU_LY_ID == KetQuaId);
            //}

                query = query.Where(c => c.LIST_XU_LY_ID == ListXuLyIdString);

            return new PagedList<ThuChi>(query.OrderByDescending(c=>c.NGAY_THU), pageIndex, pageSize);;
         }
        public virtual IPagedList<ThuChi> SearchThuChiKetQuas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal KetQuaId = 0, string ListXuLyIdString = null)
        {

            //var query = _itemRepository.Table.Where(c => c.xuLy.DON_VI_ID == _workContext.CurrentDonVi.ID && c.xuLy.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai).GroupBy(c=>c.XU_LY_ID).Select(c=>new ThuChi() {
            //    XU_LY_ID = c.Key,
            //    SO_TIEN_CON_PHAI_THU = c.Select(n=>n.SO_TIEN_CON_PHAI_THU).Sum(),
            //    SO_TIEN_THU = c.Select(n => n.SO_TIEN_THU).Sum(),
            //    SO_TIEN_PHAI_THU = c.Select(n => n.SO_TIEN_PHAI_THU).Sum(),
            //    CHI_PHI = c.Select(n => n.CHI_PHI).Sum(),
            //    TG_SO_TIEN = c.Select(n => n.TG_SO_TIEN).Sum()
            //});

            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID)
              .GroupBy(c => c.LIST_XU_LY_ID.ToLower()).Select(c => new ThuChi()
              {
                  LIST_XU_LY_ID = c.Key,
                  SO_TIEN_PHAI_THU = c.OrderBy(n => n.ID).FirstOrDefault().SO_TIEN_PHAI_THU, // lấy số tiền phải thu của Chứng từ đầu tiên
                  SO_TIEN_THU = c.Select(n => n.SO_TIEN_THU).Sum(),
                  SO_TIEN_CON_PHAI_THU = c.OrderBy(n => n.ID).FirstOrDefault().SO_TIEN_PHAI_THU - c.Select(n => n.SO_TIEN_THU).Sum(),
                  CHI_PHI = c.Select(n => n.CHI_PHI).Sum(),
                  TG_SO_TIEN = c.Select(n => n.TG_SO_TIEN).Sum()
              });
          
            if (KetQuaId > 0)
            {
                query = query.Where(c => c.XU_LY_ID == KetQuaId);
            }
            if (!string.IsNullOrEmpty(ListXuLyIdString))
            {
                query = query.Where(c => c.LIST_XU_LY_ID == ListXuLyIdString);
            }
            //if (Keysearch != null)
            //{              
            //    query = query.Where(c => c.LIST_XU_LY_ID.ToListInt().Any(t => ListXyLyFilter.Contains(t))).AsQueryable();
            //}
            return new PagedList<ThuChi>(query.OrderByDescending(c => c.LIST_XU_LY_ID), pageIndex, pageSize); 
        }
        //public virtual IPagedList<ThuChi> SearchThuChiKetQuasByListXuLyId(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal KetQuaId = 0)
        //{
        //    var listXuLy = _xuLyService.GetAllXuLys().Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID)
        //    var query = _itemRepository.Table.Where(c => _xuLyService.GetXuLyById(c.LIST_XU_LY_ID.ToListInt().FirstOrDefault()).DON_VI_ID == _workContext.CurrentDonVi.ID && c.xuLy.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai).GroupBy(c => c.XU_LY_ID).Select(c => new ThuChi()
        //    {
        //        XU_LY_ID = c.Key,
        //        SO_TIEN_CON_PHAI_THU = c.Select(n => n.SO_TIEN_CON_PHAI_THU).Sum(),
        //        SO_TIEN_THU = c.Select(n => n.SO_TIEN_THU).Sum(),
        //        SO_TIEN_PHAI_THU = c.Select(n => n.SO_TIEN_PHAI_THU).Sum(),
        //        CHI_PHI = c.Select(n => n.CHI_PHI).Sum(),
        //        TG_SO_TIEN = c.Select(n => n.TG_SO_TIEN).Sum()
        //    });
        //    if (Keysearch != null)
        //    {
        //        query = query.Where(c => c.xuLy.QUYET_DINH_SO.Contains(Keysearch));
        //    }
        //    if (KetQuaId > 0)
        //    {
        //        query = query.Where(c => c.XU_LY_ID == KetQuaId);
        //    }
        //    return new PagedList<ThuChi>(query.OrderByDescending(c => c.XU_LY_ID), pageIndex, pageSize); ;
        //}
        public virtual ThuChi GetThuChiById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
        public virtual ThuChi GetThuChiByDB_ID(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            return _itemRepository.Table.Where(c => c.DB_ID == DB_Id).FirstOrDefault();
        }
        public virtual IList<ThuChi> GetThuChiByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertThuChi(ThuChi entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateThuChi(ThuChi entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteThuChi(ThuChi entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

