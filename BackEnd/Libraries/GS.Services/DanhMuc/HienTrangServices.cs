//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class HienTrangService : IHienTrangService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<HienTrang> _itemRepository;
        private readonly IDonViService _donViService;
        private readonly ILoaiDonViService _loaiDonViService;
        #endregion

        #region Ctor

        public HienTrangService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<HienTrang> itemRepository,
            IDonViService donViService,
            ILoaiDonViService loaiDonViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._donViService = donViService;
            this._loaiDonViService = loaiDonViService;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoaiHinhTsId"></param>
        /// <param name="isBanQuanLyDuAn">Check loại tài sản sản dự án</param>
        /// <returns></returns>
        public virtual IList<HienTrang> GetHienTrangs(decimal? LoaiHinhTsId = 0, bool? isTSDA = null)
        {
            var query = _itemRepository.Table;
            if (LoaiHinhTsId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTsId);
                
            }

            if (isTSDA != null && isTSDA.Value)
            {
                query = query.Where(c => c.NHOM_HIEN_TRANG_ID == (int)enumNHOM_HIEN_TRANG.BAN_QL_DU_AN);


            }
            else
            {
                query = query.Where(c => c.NHOM_HIEN_TRANG_ID != (int)enumNHOM_HIEN_TRANG.BAN_QL_DU_AN);
            }
            return query.OrderBy(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.SAP_XEP).ToList();
        }
        public virtual IList<HienTrang> GetHienTrangsChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.ToList();
        }
        public virtual IPagedList<HienTrang> SearchHienTrangs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? LoaiHinhTSId = 0, Decimal? kieuDuLieuID = 0, Decimal? nhomHienTrangId = 0)
        {
            var query = _itemRepository.Table;
            if (Keysearch != null)
            {
                query = query.Where(c => c.TEN_HIEN_TRANG.ToLower().Contains(Keysearch.ToLower()));
            }
            if (LoaiHinhTSId != null && LoaiHinhTSId > 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTSId);
            }
            if (kieuDuLieuID != null && kieuDuLieuID > 0)
            {
                query = query.Where(c => c.KIEU_DU_LIEU_ID == kieuDuLieuID);
            }
            //order
            query = query.OrderBy(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.SAP_XEP);
            return new PagedList<HienTrang>(query, pageIndex, pageSize); ;
        }

        public virtual HienTrang GetHienTrangById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<HienTrang> GetHienTrangByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertHienTrang(HienTrang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.KIEU_DU_LIEU_ID == null)
                entity.KIEU_DU_LIEU_ID = 0;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateHienTrang(HienTrang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteHienTrang(HienTrang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public IList<HienTrang> ListHienTrangsByFields(decimal? id = 0, string tenHienTrang = null, decimal? loaiHinhTSId = 0, decimal? kieuDuLieuID = 0, Decimal? nhomHienTrangId = 0)
        {
            var query = _itemRepository.Table;
            if (id != null && id != 0)
            {
                query = query.Where(c => c.ID == id.Value);
            }
            if (tenHienTrang != null && tenHienTrang.Trim() != "")
            {
                query = query.Where(c => c.TEN_HIEN_TRANG.ToLower().Contains(tenHienTrang.ToLower()));
            }
            if (loaiHinhTSId != null && loaiHinhTSId != 0)
            {
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
            }
            if (kieuDuLieuID != null && kieuDuLieuID != 0)
            {
                query = query.Where(c => c.KIEU_DU_LIEU_ID == kieuDuLieuID);
            }

            return query.OrderBy(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.SAP_XEP).ToList();
        }
        //public IList<SelectListItem> ListHienTrangsByLoaiHinhTaiSan(decimal? loaiHinhTSId = 0)
        //{
        //    var ListHTSD = new List<SelectListItem>() { };
        //    if (loaiHinhTSId != null && loaiHinhTSId != 0)
        //    {
        //        query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
        //    }

        //    return query.OrderBy(c => c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.SAP_XEP).ToSelectList();
        //}
        public virtual void InsertListHienTrang(List<HienTrang> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateListHienTrang(List<HienTrang> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual HienTrang GetHienTrangByIdDB(int Id_DB = 0, decimal Id = 0)
        {
            if (Id == 0)
                return null;
            if (Id_DB == 0)
                return null;
            return _itemRepository.Table.Where(m => m.DB_ID == Id_DB).FirstOrDefault();
        }
        /// <summary>
        /// Check xem đơn vị có nhập được hiện trạng này hay không (true là được, false là không được nhập)
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="HienTrangId"></param>
        /// <returns></returns>
        public bool CheckHienTrangTheoLoaiDonVi(decimal donViID, decimal HienTrangId)
        {
            if (donViID <=0 || HienTrangId <= 0)
            {
                return false;
            }
            // Xác định loại đơn vị
            var LoaiHinhDonViId = _donViService.GetDonViById(donViID)?.LOAI_DON_VI_ID;
            var MaLoaiHinhDonVi = _loaiDonViService.GetLoaiDonViById(LoaiHinhDonViId ?? 0)?.TREE_NODE;
            var ListTreeNode = MaLoaiHinhDonVi?.Split('-').Select(c => int.Parse(c)).FirstOrDefault(); // mã cấp 1 
            var IdLoaiDonViSuNgiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_SU_NGHIEP).ID;//12
            var IdLDV_CO_QUAN_NHA_NUOC = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_CO_QUAN_NHA_NUOC).ID;//11
            var IdLDV_TO_CHUC = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_TO_CHUC).ID;//13


            var ListHoatDongSuNghiep = new List<decimal>();


            var ListQuanLyNhaNuoc = new List<decimal>();

            var ListTruSoHoatDong = new List<decimal>();

            ListHoatDongSuNghiep = _itemRepository.Table
                                        .Where(c => c.TEN_HIEN_TRANG.ToLower().Contains(NHOM_HIEN_TRANG_ID.MaHoatDongSuNghiep)).Select(c => c.ID).ToList();
            ListQuanLyNhaNuoc = _itemRepository.Table
                                        .Where(c => c.TEN_HIEN_TRANG.ToLower().Contains(NHOM_HIEN_TRANG_ID.MaQuanLyNhaNuoc)).Select(c => c.ID).ToList();
            ListTruSoHoatDong = _itemRepository.Table
                                        .Where(c => c.TEN_HIEN_TRANG.ToLower().Contains(NHOM_HIEN_TRANG_ID.MaTruSoLamViec)).Select(c => c.ID).ToList();
            if (ListTreeNode != null)
            {
                if (ListTreeNode == IdLoaiDonViSuNgiep)
                {
                    // nếu loại hình đơn vị sự nghiệp và hiện trạng thuộc nhóm trụ sở hoạt động hoặc qlnn thì không cho nhập
                    if (ListTruSoHoatDong.Contains(HienTrangId) || ListQuanLyNhaNuoc.Contains(HienTrangId))
                    {
                        return false;
                    }
                    return true;
                    
                }
                if (ListTreeNode == IdLDV_CO_QUAN_NHA_NUOC || ListTreeNode == IdLDV_TO_CHUC)
                {
                    // nếu loại hình đơn vị cqnn hoặc tổ chức  và hiện trạng thuộc nhóm hoạt động sự nghiệp thì không cho nhập 
                    if (ListHoatDongSuNghiep.Contains(HienTrangId))
                    {
                        return false; // false là invalid
                    }
                    return true; // true là valid, có thể nhập giá trị

                }
                // các loại đơn vị khác thì nhập thoải mái
                return true;
            }
            return false;
        }

        /// <summary>
        /// Truyền vào List tài sản hiện trạng  và donViID, check xem list hiện trạng đó có bất kỳ hiện trạng nào bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="listTaiSanHienTrang"></param>
        /// <returns></returns>
        public bool CheckIsListHienTrangNhapSai(decimal donViID, IList<TaiSanHienTrangSuDung> listTaiSanHienTrang = null)
        {
            if (donViID <= 0 || listTaiSanHienTrang == null)
            {
                return true; // Coi như nhập sai
            }

            return listTaiSanHienTrang.Any(c => (!CheckHienTrangTheoLoaiDonVi(donViID, c.HIEN_TRANG_ID??0) && (c.GIA_TRI_CHECKBOX == true || c.GIA_TRI_NUMBER > 0 || c.GIA_TRI_NUMBER < 0)));
        }

        /// <summary>
        /// Truyền vào hiện trạng hiện trạng và donViID, check xem hiện trạng đó có bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="ObjHienTrang"></param>
        /// <returns></returns>
        public bool CheckIsHienTrangNhapSai(decimal donViID, TaiSanHienTrangSuDung ObjHienTrang = null)
        {
            if (donViID <= 0 || ObjHienTrang == null)
            {
                return true; // Coi như nhập sai
            }

            return (!CheckHienTrangTheoLoaiDonVi(donViID, ObjHienTrang.HIEN_TRANG_ID ?? 0) && (ObjHienTrang.GIA_TRI_CHECKBOX == true || ObjHienTrang.GIA_TRI_NUMBER > 0 || ObjHienTrang.GIA_TRI_NUMBER < 0));
        }
        #endregion
    }
}

