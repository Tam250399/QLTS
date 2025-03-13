//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using DevExpress.CodeParser;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DMDC;
using GS.Core.Domain.HeThong;
using GS.Data;
using GS.Services.HeThong;
using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace GS.Services.DanhMuc
{
    public partial class DonViService : IDonViService
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DonVi> _itemRepository;

        //private readonly IRepository<LoaiDonVi> _loaiDonViRepository;
        private readonly IRepository<NguoiDungDonViMapping> _nguoiDungDonViRepository;

        private readonly IRepository<DMDC_DonViNganSach> _dMDC_DonViNganSachrepository;
        private readonly INguoiDungDonViService _nguoiDungDonViService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly ILoaiDonViService _loaiDonViService;
        //private readonly ITaiSanService _tsService;
        //private readonly IWorkContext _workContext;

        #endregion Fields

        #region Ctor

        public DonViService(
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DonVi> itemRepository,
            IRepository<NguoiDungDonViMapping> nguoiDungDonViRepository,
            IRepository<DMDC_DonViNganSach> dMDC_DonViNganSachrepository,
            INguoiDungDonViService nguoiDungDonViService,
            INguoiDungService nguoiDungService,
            ILoaiDonViService loaiDonViService
            //ITaiSanService taiSanService
            //IWorkContext workContext
            )
        {
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._nguoiDungDonViRepository = nguoiDungDonViRepository;
            _dMDC_DonViNganSachrepository = dMDC_DonViNganSachrepository;
            this._nguoiDungDonViService = nguoiDungDonViService;
            this._nguoiDungService = nguoiDungService;
            this._loaiDonViService = loaiDonViService;
            this._loaiDonViService = loaiDonViService;
            //this._tsService = taiSanService;
            //this._workContext = workContext;
        }

        #endregion Ctor

        #region Methods

        public virtual bool CheckQuyen(decimal currentDonViId, decimal currentNguoiDungId, decimal? donViChaID = null, decimal? donViID = null)
        {
            if (currentNguoiDungId == 0)
                return false;
            if (currentDonViId == 0)
                return false;
            var nguoiDung = _nguoiDungService.GetNguoiDungById(currentNguoiDungId);
            if (nguoiDung == null)
                return false;
            //người dùng là quản trị thì bỏ qua
            if (nguoiDung.IS_QUAN_TRI)
                return true;
            var listDonViNguoiDung = GetAllDonViByNguoiDungId(currentNguoiDungId);
            //Check đơn vị người dùng quán lý
            if (listDonViNguoiDung == null || (listDonViNguoiDung != null && listDonViNguoiDung.Count == 0))
                return false;

            var currentDonVi = GetDonViById(currentDonViId);
            if (currentDonVi == null)
                return false;
            var listDonViCon = GetListDonViChild(currentDonViId);
            var listDonViConId = listDonViCon.Select(x => x.ID).ToList();
            //Kiểm tra đơn vị hiện tại có quyền quản lý donViChaID 
            if (donViChaID > 0 && !listDonViConId.Contains(donViChaID.Value))
                return false;
            //Kiểm tra đơn vị hiện tại có quyền quản lý donViID 
            if (donViID > 0 && !listDonViConId.Contains(donViID.Value))
                return false;
            List<DonVi> listAllDonViNguoiDung = listDonViNguoiDung.ToList();
            foreach (var item in listDonViNguoiDung)
            {
                var itemchild = GetListDonViChild(item.ID);
                if (itemchild == null) continue;
                listAllDonViNguoiDung = listAllDonViNguoiDung.Concat(itemchild.ToList()).ToList();
            }
            var listAllDonViNguoiDungId = listAllDonViNguoiDung.Select(x => x.ID).ToList();
            //Kiểm tra người dùng có quyền quản lý donViChaID
            if (donViChaID > 0 && !listAllDonViNguoiDungId.Contains(donViChaID.Value))
                return false;
            //Kiểm tra người dùng có quyền quản lý donViID
            if (donViID > 0 && !listAllDonViNguoiDungId.Contains(donViID.Value))
                return false;
            //Kiểm tra donViChaID có được quản lý donViID
            if (donViID > 0 && donViChaID > 0)
            {
                var listIdChild = GetListIdDonViChild(donViChaID.Value);
                if (listIdChild == null) return false;
                if (!listIdChild.Contains(donViID.Value))
                    return false;
            }

            return true;
        }

        public virtual IList<DonVi> GetAllDonVis(decimal TreeLevel = 0)
        {
            var query = _itemRepository.Table;
            if (TreeLevel > 0)
            {
                query = query.Where(m => m.TREE_LEVEL == TreeLevel);
            }
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }

        public virtual List<DonVi> GetAllDonVis2(bool db_id_null = false, decimal TakeNumber = 100)
        {
            var query = _itemRepository.Table;
            if (db_id_null)
                query = query.Where(c => c.DB_ID == null);
            else
                query = query.Where(c => c.DB_ID != null);
            return query.OrderBy(c => c.TREE_NODE).Take((int)TakeNumber).ToList();
        }

        public virtual IList<DonVi> GetAllDonVisChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null);
            return query.OrderBy(c => c.TREE_NODE).ToList();
        }

        public virtual List<decimal> GetAllDonViIds(bool db_id_null = false)
        {
            var query = _itemRepository.Table;
            if (db_id_null)
                query = query.Where(c => c.DB_ID == null);
            else
                query = query.Where(c => c.DB_ID != null);
            return query.OrderBy(c => c.TREE_NODE).Select(x => x.ID).ToList();
        }

        public virtual IPagedList<DonVi> SearchDonViDieuChuyens(int pageIndex = 0, int pageSize = 10, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? NguoiDungID = 0, bool IsQuanTri = false,
            decimal? ParentID = 0, decimal? TreeLevel = 1, bool ischondonvi = false, bool isSelectList = false, DateTime? NGAY_CAP_NHAT_TU = null,
            DateTime? NGAY_CAP_NHAT_DEN = null, bool isNhapLieuOnly = false, decimal? tinhId = 0, bool isTinh = false, decimal? boNganhId = 0)
        {
            List<string> param = new List<string>();
            //var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    param.Add(string.Format("(UPPER(DONVI.TEN) = '{0}' OR UPPER(DONVI.MA) = '{1}')", Keysearch, Keysearch));
                    //query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    param.Add(string.Format("(UPPER(DONVI.TEN) LIKE '%{0}%' OR UPPER(DONVI.MA) LIKE '%{1}%')", Keysearch, Keysearch));
            }
            if (ParentID == null)
            {
                //chưa hiểu ý tác giả tại sao lại để như vậy=> tạm thời để thêm check keysearch để tránh k tìm đc đơn vị khi keysearch có giá trị
                if (String.IsNullOrEmpty(Keysearch))
                {
                    //query = query.Where(c => c.PARENT_ID == null && c.MA == c.MA_BO);
                    param.Add("DONVI.PARENT_ID IS NULL AND DONVI.MA = DONVI.MA_BO");
                }
            }
            else if (ParentID > 0)
                param.Add(string.Format("DONVI.PARENT_ID = {0}", ParentID));
            //query = query.Where(c => c.PARENT_ID == ParentID);

            if (boNganhId > 0)
                param.Add(string.Format("DONVI.ID = {0}", boNganhId));
            //query = query.Where(c => c.ID == boNganhId);

            //Loại đơn vị
            if (LoaiDonViSearch > 0)
            {
                var listLoaiDonVi = _loaiDonViService.GetListDonViByTreeNode(LoaiDonViSearch);
                LoaiDonVi loaiDonVi = _loaiDonViService.GetLoaiDonViById(LoaiDonViSearch.Value);
                //var arrayID = listLoaiDonVi.Select(i => i.ID).ToArray();
                //query = query.Where(c => arrayID.Contains(c.LOAI_DON_VI_ID ?? 0));
                if (loaiDonVi != null)
                    param.Add(string.Format("DONVI.LOAI_DON_VI_ID IN (SELECT ID FROM DM_LOAI_DON_VI WHERE TREE_NODE LIKE '{0}%')", loaiDonVi.TREE_NODE));
            }

            if (!isTinh)
            {
                CapDonViSearch = 0;
                param.Add(string.Format("DONVI. CAP_DON_VI_ID in ({0})", CapDonViSearch));
            }
            else
            {
                if (CapDonViSearch < 0)
                {
                    // search theo tỉnh và Cấp đơn vi = -1 => chọn hết địa phương
                    var listCapTrungUong = EnumCapDonViDiaPhuong.Tinh.ToSelectList().Select(c => int.Parse(c.Value)).ToList();
                    var listCapTrungUongString = string.Join(',', listCapTrungUong);
                    param.Add(string.Format("DONVI. CAP_DON_VI_ID in ({0})", listCapTrungUongString));
                }
                else
                {
                    param.Add(string.Format("DONVI. CAP_DON_VI_ID in ({0})", CapDonViSearch));
                }
            }

            //query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            //query = query.OrderBy(c => c.MA);
            string strsql = "SELECT DONVI.* FROM DM_DON_VI DONVI WHERE DONVI.TRANG_THAI_ID <> 0 AND " + String.Join(" AND ", param) + " ORDER BY DONVI.TREE_NODE, DONVI.TREE_LEVEL";

            var query = _dbContext.EntityFromSqlNoParams<DonVi>(strsql);
            return new PagedList<DonVi>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<DonVi> SearchDonVis(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? NguoiDungID = 0, bool IsQuanTri = false,
            decimal? ParentID = 0, decimal? TreeLevel = 1, bool ischondonvi = false, bool isSelectList = false, DateTime? NGAY_CAP_NHAT_TU = null, DateTime? NGAY_CAP_NHAT_DEN = null, bool isNhapLieuOnly = false, decimal? donViId = null, IList<int> listCapDonVis = null, bool xuatExcel = false)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID != false);

            if (!String.IsNullOrEmpty(Keysearch))
            {
                if (donViId > 0)
                {
                    var donVi = GetDonViById(donViId.Value);
                    if (donVi != null)
                    {
                        string treeNode = donVi.TREE_NODE + "-";
                        query = query.Where(dv => dv.ID == donViId || dv.TREE_NODE.StartsWith(treeNode));
                    }
                }
                if (isSelectList && NguoiDungID > 0 && !IsQuanTri && ParentID.GetValueOrDefault() == 0)
                {
                    var l_list_idDV_nguoiDung = _nguoiDungDonViRepository.Table.Where(p => p.NGUOI_DUNG_ID == NguoiDungID).Select(p => p.DON_VI_ID).ToList();
                    var l_list_idDVCon = GetListIdDonViChild(l_list_idDV_nguoiDung);
                    query = from q in query
                            join idDVs in l_list_idDVCon
                            on q.ID equals idDVs
                            select q;
                }
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().StartsWith(Keysearch));
            }
            else
            {
                //if (donViId > 0) query = query.Where(dv => dv.ID == donViId);
                if (donViId > 0)
                {
                    var donVi = GetDonViById(donViId.Value);
                    if (donVi != null)
                    {
                        string treeNode = donVi.TREE_NODE + "-";
                        query = query.Where(dv => dv.ID == donViId || dv.TREE_NODE.StartsWith(treeNode));
                    }
                }
                if (isSelectList && NguoiDungID > 0 && !IsQuanTri && ParentID.GetValueOrDefault() == 0 && !xuatExcel)
                {
                    query = query.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.DON_VI_ID, (x, y)
                        => new { Donvi = x, nguoiDung = y })
                        .Where(z => z.nguoiDung.NGUOI_DUNG_ID == NguoiDungID)
                        .Select(z => z.Donvi);
                }
            }
            //chỉ lấy đơn vị nhập liệu. Không lấy đơn vị tổng hợp
            if (isNhapLieuOnly)
                query = query.Where(p => p.LA_DON_VI_NHAP_LIEU == isNhapLieuOnly);
            //Loại đơn vị
            if (LoaiDonViSearch > 0)
            {
                var listLoaiDonVi = _loaiDonViService.GetListDonViByTreeNode(LoaiDonViSearch);
                var arrayID = listLoaiDonVi.Select(i => i.ID).ToArray();
                query = query.Where(c => arrayID.Contains(c.LOAI_DON_VI_ID ?? 0));
            }
            if (CapDonViSearch > 0)
                query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            if (listCapDonVis != null && listCapDonVis.Count() > 0)
            {
                query = query.Where(c => listCapDonVis.Contains((int)(c.CAP_DON_VI_ID ?? 0)));
            }
            //Load đơn vị con
            if (ParentID > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentID);
            }
            else if (IsQuanTri && String.IsNullOrEmpty(Keysearch) && !(donViId > 0))
            {
                query = query.Where(c => c.MA == c.MA_BO);
            }
            if (NGAY_CAP_NHAT_TU != null)
            {
                query = query.Where(m => m.NGAY_CAP_NHAT.Value.Date >= NGAY_CAP_NHAT_TU.Value.Date);
            }
            if (NGAY_CAP_NHAT_DEN != null)
            {
                query = query.Where(m => m.NGAY_CAP_NHAT.Value.Date <= NGAY_CAP_NHAT_DEN.Value.Date);
            }
            //Loại bỏ đơn vị con của đơn vị quản lý dự án
            query = query.Where(c => !(c.DonViCha != null && c.DonViCha.LOAI_DON_VI_ID == _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN).ID));
            // nếu từ khóa != null và có thể convert chữ thứ 2 sang int thì xác định là search theo mã (mã có dạng 0001, T012)
            if (Keysearch != null && Keysearch.Length > 1 && int.TryParse(Keysearch.Substring(1, 1), out int result))
            {
                query = query.OrderBy(c => c.MA);
            }
            else if (Keysearch == null || Keysearch.Length < 1)
            {
                query = query.OrderBy(c => c.MA);
            }
            else
            {
                // còn lại thì coi như tìm theo tên và order theo độ dài của tên
                query = query.OrderBy(c => c.TEN.Length);
            }

            return new PagedList<DonVi>(query, pageIndex, pageSize);
        }

        public virtual DonVi GetDonViById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DonVi> GetDonViByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        /// <summary>
        /// lấy 10 bản cho combobox
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public virtual IList<DonVi> GetDonVisForCBB(string Ten = null, decimal ID = 0)
        {
            var query = _itemRepository.Table;
            if (ID > 0)
            {
                query = query.Where(c => c.ID == ID);
            }
            if (!String.IsNullOrEmpty(Ten))
            {
                query.Where(c => c.TEN.Contains(Ten));
            }
            return query.Take(10).ToList();
        }

        public virtual void InsertDonVi(DonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.MA_BO = entity.MA.Substring(0, 3);
            _itemRepository.Insert(entity);
            entity.NGAY_TAO = DateTime.Now;
            entity.TRANG_THAI_ID = entity.TRANG_THAI_ID ?? true;
            // entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            GenTreeNode(entity);
            _staticCacheManager.Remove("GET_ALL_DON_VI");
            //event notification
            //_eventPublisher.EntityInserted(entity);
            //Gen TreeNode
        }

        public virtual void UpdateDonVi(DonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.MA_BO = entity.MA.Substring(0, 3);
            _itemRepository.Update(entity);
            entity.NGAY_CAP_NHAT = DateTime.Now;
            // entity.NGUOI_CAP_NHAT_ID = _workContext.CurrentCustomer.ID;
            GenTreeNode(entity);
            _staticCacheManager.Remove("GET_ALL_DON_VI");
            //event notification
            //_eventPublisher.EntityUpdated(entity);
        }

        public Boolean CheckDonVi(decimal DonViId, decimal NguoiDungId)
        {
            //var NguoiDungId = 9563;
            OracleParameter p1 = new OracleParameter("p_don_vi_id", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("p_nguoi_dung_id", OracleDbType.Int32, NguoiDungId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("tbl_out", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var model = _dbContext.EntityFromStore<DecimalViewQueryType>("PK_TS_NGHIEPVU.SP_CHECK_DON_VI", p1, p2, p3).FirstOrDefault();
                if (model != null && model.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void DeleteDonVi(DonVi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove("GET_ALL_DON_VI");
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public virtual IList<DonVi> GetAllDonViByNguoiDungId(decimal nguoiDungId)
        {
            //var key = string.Format(GSSecurityDefaults.PermissionsAllByCustomerRoleIdCacheKey, vaiTro);
            //return _cacheManager.Get(key, () =>
            //{
            var query = from dv in _itemRepository.Table
                        join nddv in _nguoiDungDonViRepository.Table on dv.ID equals nddv.DON_VI_ID
                        where nddv.NGUOI_DUNG_ID == nguoiDungId
                        orderby dv.ID
                        select dv;
            return query.ToList();
            //});
        }

        public virtual InfoCacheDonvi GetProfileUser(Guid guidNguoiDung, decimal? donviId = 0)
        {
            var _infoCache = new InfoCacheDonvi();
            if (donviId > 0)
            {
                var nguoiDung = _nguoiDungService.GetNguoiDungByGuid(guidNguoiDung);

                var donVi = _itemRepository.GetById(donviId);
                _infoCache = donViToInfoCacheDonVi(donVi);
                nguoiDung.CURRENT_DON_VI_ID = donVi.ID;
                _nguoiDungService.UpdateNguoiDung(nguoiDung);
            }
            else
            {
                var nguoiDung = _nguoiDungService.GetNguoiDungByGuid(guidNguoiDung);
                donviId = nguoiDung?.CURRENT_DON_VI_ID;
                if (donviId > 0)
                {
                    var donVi = _itemRepository.GetById(donviId);
                    if (donVi == null)
                    {
                        if (nguoiDung.IS_QUAN_TRI)
                        {
                            donVi = _itemRepository.Table.Where(x => x.ID == (decimal)enumDonVi.DPAC).FirstOrDefault();
                        }
                        else
                        {
                            donVi = _nguoiDungDonViService.GetDonViByNguoiDungId(nguoiDung.ID).FirstOrDefault();
                        }
                        nguoiDung.CURRENT_DON_VI_ID = donVi.ID;
                        _nguoiDungService.UpdateNguoiDung(nguoiDung);
                    }
                    _infoCache = donViToInfoCacheDonVi(donVi);
                }
                else
                {
                    var donvis = _itemRepository.Table;
                    var donvi = new DonVi();
                    if (nguoiDung.IS_QUAN_TRI)
                    {
                        donvi = donvis.Where(x => x.ID == (decimal)enumDonVi.DPAC).FirstOrDefault();
                    }
                    else
                    {
                        donvi = _nguoiDungDonViService.GetDonViByNguoiDungId(nguoiDung.ID).FirstOrDefault();
                    }
                    nguoiDung.CURRENT_DON_VI_ID = donvi.ID;
                    _nguoiDungService.UpdateNguoiDung(nguoiDung);
                    _infoCache = donViToInfoCacheDonVi(donvi);
                }
            }
            return _infoCache;
        }

        private InfoCacheDonvi donViToInfoCacheDonVi(DonVi donVi)
        {
            InfoCacheDonvi _infoCache = new InfoCacheDonvi();
            if (donVi != null)
            {
                _infoCache.ID = donVi.ID;
                _infoCache.MA_DON_VI = donVi.MA;
                _infoCache.TEN_DON_VI = donVi.TEN;
                _infoCache.IS_LA_DON_VI_NHAP_LIEU = donVi.LA_DON_VI_NHAP_LIEU;
                _infoCache.IS_LA_DON_VI_TU_CHU_TAI_CHINH = donVi.LA_DON_VI_TU_CHU_TAI_CHINH;
                _infoCache.TEN_DON_VI_CHA = GetDonViLonNhat(donVi.ID, donVi.TREE_NODE)?.TEN.ToUpper();
                _infoCache.LA_BAN_QL_DU_AN = donVi.LA_BAN_QL_DU_AN;
                _infoCache.TREE_LEVEL = donVi.TREE_LEVEL;
            }
            return _infoCache;
        }

        public DonVi GetDonViLonNhat(decimal DonViId, string TREE_NODE = null)
        {
            // không tồn tại tree node
            if (string.IsNullOrEmpty(TREE_NODE))
            {
                var donVi = _itemRepository.GetById(DonViId);
                if (donVi == null)
                    return null;
                TREE_NODE = donVi.TREE_NODE;
            }
            var TREE_NODE_LON_NHAT = TREE_NODE.Split('-').FirstOrDefault();
            return _itemRepository.Table.Where(p => p.TREE_NODE == TREE_NODE_LON_NHAT && p.TRANG_THAI_ID != false).FirstOrDefault();
        }

        public DonVi GetDonViLonNhatByMa(string DonViMa, string TREE_NODE = null)
        {
            // không tồn tại tree node
            if (string.IsNullOrEmpty(TREE_NODE))
            {
                var donVi = GetDonViByMa(DonViMa);
                if (donVi == null)
                    return null;
                TREE_NODE = donVi.TREE_NODE;
            }
            var TREE_NODE_LON_NHAT = TREE_NODE.Split('-').FirstOrDefault();
            return _itemRepository.Table.Where(p => p.TREE_NODE == TREE_NODE_LON_NHAT && p.TRANG_THAI_ID != false).FirstOrDefault();
        }

        public DonVi GetDonViTrucThuoc(decimal DonViId, string TREE_NODE = null)
        {
            // không tồn tại tree node
            if (string.IsNullOrEmpty(TREE_NODE))
            {
                var donVi = _itemRepository.GetById(DonViId);
                if (donVi == null)
                    return null;
                TREE_NODE = donVi.TREE_NODE;
            }
            return _itemRepository.Table.Where(p => p.TREE_NODE == TREE_NODE && p.TRANG_THAI_ID != false).FirstOrDefault();
        }

        public int GetSoDonViConByID(decimal? ParentId)
        {
            return _itemRepository.Table.Count(c => c.PARENT_ID == ParentId && c.TRANG_THAI_ID != false);
        }

        public List<string> GetListGoiYMaDonViCon(decimal ParentId)
        {
            var query = _itemRepository.Table.Where(c => c.PARENT_ID == ParentId && c.PARENT_ID != null)
                                          .Select(c => c.MA).ToList();
            if (query.Count() < 1)
            {
                return new List<string>() { "001", "002", "003" };
            }
            var ListDecimalID = query.Select(c =>
            {
                Decimal.TryParse(String.Concat(c.TakeLast(3)), out decimal result);
                return result;
            }).OrderBy(c => c).ToArray();
            var MaxID = ListDecimalID.Max();
            List<string> ListMaGoiY = new List<string>();
            // nếu max id = 998,.... thì phải tìm mã < 1000
            if (MaxID > 997 && ListDecimalID.Count() < 998)
            {
                int count = 0;
                for (int i = 1; i < 1000; i++)
                {
                    if (!ListDecimalID.Contains(i))
                    {
                        count = count + 1;
                        ListMaGoiY.Add(i.ToString().PadLeft(3, '0'));
                    }

                    if (count >= 3)
                    {
                        break;
                    }
                }
                return ListMaGoiY;
            }
            // nếu maxid khác 998,999,1000,...
            for (int i = 1; i < 4; i++)
            {
                var MaGoiY = MaxID + i;
                ListMaGoiY.Add(MaGoiY.ToString().PadLeft(3, '0'));
            }

            return ListMaGoiY;
        }

        public IList<DonVi> GetDonViForInputSearch(string tenDonVi = null, decimal? NguoiDungID = 0, decimal CurrentDV_ID = 0)
        {
            //var query = _itemRepository.Table;
            //var q = query;
            //if (!String.IsNullOrEmpty(tenDonVi))
            //	query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA).Take(10);
            //if (CurrentDV_ID > 0)
            //	query = query.Where(p => p.ID != CurrentDV_ID);
            //if (String.IsNullOrEmpty(tenDonVi))
            //	query = query.Take(10);
            //q = q.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.DON_VI_ID, (x, y) => new { Donvi = x, nguoiDung = y })
            //	.Where(z => z.nguoiDung.NGUOI_DUNG_ID == NguoiDungID)
            //	.Select(z => z.Donvi);
            //query = query.Where(z => !q.Contains(z));
            //query = q.Concat(query);

            var CurrenDonVi = _itemRepository.GetById(CurrentDV_ID);
            if (CurrenDonVi != null)
            {
                var query = _itemRepository.Table;
                var nguoiDung = _nguoiDungService.GetNguoiDungById(NguoiDungID);
                if (CurrenDonVi.ID != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && !nguoiDung.IS_QUAN_TRI)
                {
                    query = from a in query
                            join b in _nguoiDungDonViRepository.Table
                            on a.ID equals b.DON_VI_ID
                            where b.NGUOI_DUNG_ID == NguoiDungID
                            select a;
                }
                query = query.Where(c => c.TRANG_THAI_ID != false);
                if (!String.IsNullOrEmpty(tenDonVi))
                    query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
                query = query.OrderBy(p => p.MA).Take(10);
                return query.ToList();
            }
            return null;
        }

        public IList<DonVi> GetDonViMuaSamForInputSearch(string tenDonVi = null, Decimal CurrentDV_ID = 0)
        {
            var currentDonVi = GetDonViById(CurrentDV_ID) ?? new DonVi() { };
            //var NODE = string.Format("{0:D8}", Convert.ToInt32(currentDonVi.ID));
            //var query = _itemRepository.Table.Where(p => p.TREE_NODE.EndsWith(NODE) && p.TRANG_THAI_ID != false);

            var query = _itemRepository.Table.Where(p => p.TRANG_THAI_ID != false && p.MA.Substring(0, 3) == currentDonVi.MA.Substring(0, 3));
            query = query.Where(c => (c.CAP_DON_VI_ID == 0 || c.CAP_DON_VI_ID == 1 || c.CAP_DON_VI_ID == 2) && (c.TREE_LEVEL == 1 || c.TREE_LEVEL == 2));
            if (!string.IsNullOrEmpty(tenDonVi))
            {
                query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
            }
            query = query.OrderBy(p => p.MA).ThenBy(c => c.TREE_LEVEL);
            return query.ToList();
        }

        public IList<DonVi> GetDonViCapDuoiTrucThuocForInputSearch(string tenDonVi = null, decimal? NguoiDungID = 0, decimal CurrentDV_ID = 0)
        {
            //var query = _itemRepository.Table;
            //var q = query;
            //if (!String.IsNullOrEmpty(tenDonVi))
            //	query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA).Take(10);
            //if (CurrentDV_ID > 0)
            //	query = query.Where(p => p.ID != CurrentDV_ID);
            //if (String.IsNullOrEmpty(tenDonVi))
            //	query = query.Take(10);
            //q = q.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.DON_VI_ID, (x, y) => new { Donvi = x, nguoiDung = y })
            //	.Where(z => z.nguoiDung.NGUOI_DUNG_ID == NguoiDungID)
            //	.Select(z => z.Donvi);
            //query = query.Where(z => !q.Contains(z));
            //query = q.Concat(query);

            var CurrenDonVi = _itemRepository.GetById(CurrentDV_ID);
            if (CurrenDonVi != null)
            {
                var query = _itemRepository.Table;
                var nguoiDung = _nguoiDungService.GetNguoiDungById(NguoiDungID);
                if (CurrenDonVi.ID != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && !nguoiDung.IS_QUAN_TRI)
                {
                    query = query.Where(c => c.TREE_NODE.StartsWith(CurrenDonVi.TREE_NODE));
                }
                query = query.Where(c => c.TRANG_THAI_ID != false);

                if (!String.IsNullOrEmpty(tenDonVi))
                    query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
                query = query.OrderBy(p => p.MA).Take(10);
                return query.ToList();
            }
            return null;
        }

        public IList<DonVi> GetDonViTongHopForInputSearch(string tenDonVi = null)
        {
            var query = _itemRepository.Table.Where(c => !(c.LA_DON_VI_NHAP_LIEU ?? false));
            if (!String.IsNullOrEmpty(tenDonVi))
                query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
            query = query.OrderBy(p => p.MA).Take(10);
            return query.ToList();
        }

        public IList<DonVi> GetDonViCap1ForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? NguoiDungID = 0)
        {
            var CurrenDonVi = _itemRepository.GetById(donViId);
            if (CurrenDonVi != null)
            {
                var query = _itemRepository.Table.Where(p => p.CAP_DON_VI_ID == 1 && p.TRANG_THAI_ID != false);
                var nguoiDung = _nguoiDungService.GetNguoiDungById(NguoiDungID);
                if (CurrenDonVi.ID != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && !nguoiDung.IS_QUAN_TRI)
                {
                    query = from a in query
                            join b in _nguoiDungDonViRepository.Table
                            on a.ID equals b.DON_VI_ID
                            where b.NGUOI_DUNG_ID == NguoiDungID
                            select a;
                }
                if (!String.IsNullOrEmpty(tenDonVi))
                    query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
                query = query.OrderBy(p => p.MA).Take(10);
                return query.ToList();
            }
            return null;
        }

        public IList<DonVi> GetDonViConForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? donViSelected = 0)
        {
            var donvi = _itemRepository.GetById(donViId.Value);
            if (donvi == null)
            {
                return new List<DonVi>();
            }
            var query = _itemRepository.Table.Where(p => p.TREE_NODE.StartsWith(donvi.TREE_NODE + "-") && p.TRANG_THAI_ID != false);
            var first = query.Where(c => c.ID == donViSelected).ToList();
            if (!String.IsNullOrEmpty(tenDonVi))
                query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA).Take(10);
            if (String.IsNullOrEmpty(tenDonVi))
                query = query.Take(10);
            if (donViSelected > 0)
            {
                var result = first.Concat(query.ToList()).ToList();
                return result;
            }
            return query.ToList();
        }

        public bool CheckMaDonVi(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id );
        }

        public void GenTreeNode(DonVi entity)
        {
            if (entity.PARENT_ID > 0)
            {
                var parent = GetDonViById(entity.PARENT_ID ?? 0);
                entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
                entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
                _itemRepository.Update(entity);
            }
            else
            {
                entity.TREE_LEVEL = 1;
                entity.TREE_NODE = CommonHelper.GenTreeNode(null, entity.ID, null);
                _itemRepository.Update(entity);
            }
        }

        public IList<decimal> GetListIdDonViChild(decimal DonViId, bool isIncludeItself = true)
        {
            if (DonViId == 0)
                return new List<decimal>();
            var donVi = _itemRepository.GetById(DonViId);
            if (donVi == null)
                return new List<decimal>();
            var query = _itemRepository.Table.Where(p => p.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            if (isIncludeItself)
            {
                var list_return = query.Select(p => p.ID).ToList();
                list_return.Insert(0, DonViId);
                return list_return;
            }
            return query.Select(p => p.ID).ToList();
        }

        public IQueryable<decimal> GetIQueryableIdDonViChild(decimal DonViId, bool isIncludeItself = true)
        {
            if (DonViId == 0)
                return null;
            var donVi = _itemRepository.GetById(DonViId);
            if (donVi == null)
                return null;
            if (isIncludeItself)
            {
                var query = _itemRepository.Table.Where(p => p.ID == DonViId || p.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                var list_return = query.Select(p => p.ID);
                return list_return;
            }
            else
            {
                var query = _itemRepository.Table.Where(p => p.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                return query.Select(p => p.ID);
            }
        }

        public IList<decimal> GetListIdDonViChild(List<decimal> idDonVis, bool isIncludeItself = true)
        {
            if (idDonVis == null || idDonVis.Count == 0)
                return new List<decimal>();
            var l_list_TreeNode = _itemRepository.Table.Where(p => idDonVis.Contains(p.ID)).Select(p => p.TREE_NODE);
            if (l_list_TreeNode == null || l_list_TreeNode.Count() == 0)
                return new List<decimal>();
            var query = _itemRepository.Table.Where(p => l_list_TreeNode.Any(a => p.TREE_NODE.StartsWith(a + "-")));
            var count = query.Count();
            if (isIncludeItself)
            {
                var list_return = query.Select(p => p.ID).ToList();
                list_return.AddRange(idDonVis);
                return list_return.Distinct().ToList();
            }
            return query.Select(p => p.ID).Distinct().ToList();
        }

        public IList<DonVi> GetListDonViChild(decimal parentId, bool isIncludeItself = true)
        {
            if (parentId == 0)
                return new List<DonVi>();
            var donVi = _itemRepository.GetById(parentId);
            if (donVi == null)
                return new List<DonVi>();
            var query = _itemRepository.Table.Where(p => p.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            if (isIncludeItself)
            {
                var list_return = query.ToList();
                var donvi = _itemRepository.GetById(parentId);
                list_return.Insert(0, donvi);
                return list_return;
            }
            return query.ToList();
        }

        public IList<DonVi> GetAllDonViUsingProc()
        {
            try
            {
                var result = _dbContext.EntityFromSqlNoParams<DonVi>("select * from dm_don_vi;").ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<DonVi> GetAllDonViChildUsingProc(decimal ParentId)
        {
            OracleParameter p1 = new OracleParameter("p_ID_DON_VI_CHA", OracleDbType.Decimal, ParentId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromSql<DonVi>("PK_TS_NGHIEPVU.SP_GET_DONVIS_CON", p1, p2).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<DonVi> GetIQueryableAllDonViChildUsingProc(decimal ParentId)
        {
            OracleParameter p1 = new OracleParameter("p_ID_DON_VI_CHA", OracleDbType.Decimal, ParentId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromSql<DonVi>("PK_TS_NGHIEPVU.SP_GET_DONVIS_CON", p1, p2);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// chỉ lấy các 64 tỉnh đầu ngành
        /// </summary>
        /// <returns></returns>
        public IList<DonVi> GetAllDonViBoNganhTinh()
        {
            OracleParameter p1 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromSql<DonVi>("PK_TS_NGHIEPVU.SP_GET_ALL_DONVIS_BO_NGANH_TINH", p1).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<DonVi> GetIQueryableAllDonViBoNganhTinh()
        {
            OracleParameter p1 = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityFromSql<DonVi>("PK_TS_NGHIEPVU.SP_GET_ALL_DONVIS_BO_NGANH_TINH", p1);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<DonVi> GetDonViForMultilSelectInput(decimal IdDonVi = 0, string ListSelectedId = null)
        {
            IQueryable<DonVi> _lstDvBp;
            if (IdDonVi == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                _lstDvBp = GetIQueryableAllDonViBoNganhTinh();
            }
            else
            {
                _lstDvBp = GetIQueryableAllDonViChildUsingProc(IdDonVi);
            }
            if (!string.IsNullOrEmpty(ListSelectedId))
            {
                var listId = ListSelectedId.Split(',').Where(c => !string.IsNullOrEmpty(c)).Select(c => decimal.Parse(c)).ToList();
                if (listId != null && listId.Count() > 0)
                {
                    var listDVSelected = _itemRepository.Table.Where(c => listId.Contains(c.ID));
                    _lstDvBp = _lstDvBp.Union(listDVSelected);
                }
            }
            return _lstDvBp.Distinct();
        }

        public DonVi GetDonViByMa(string Ma = null)
        {
            var query = _itemRepository.Table;
            if (string.IsNullOrEmpty(Ma))
            {
                return null;
            }
            return query.Where(m => m.MA == Ma).FirstOrDefault();
        }

        public virtual void InsertListDonVi(List<DonVi> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            entities = entities.Select(c =>
            {
                c.MA_BO = c.MA.Substring(0, 3);
                return c;
            }).ToList();
            _itemRepository.Insert(entities);
            //tree node
            UpdateListDonVi(entities);
            _staticCacheManager.Remove("GET_ALL_DON_VI");
        }

        public virtual void UpdateListDonVi(List<DonVi> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));

            //foreach (var entity in entities)
            //    if (entity.PARENT_ID != null)
            //    {
            //        var parent = GetDonViById(entity.PARENT_ID.Value);
            //        entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
            //        entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
            //        // entity.NGUOI_CAP_NHAT_ID = _workContext.CurrentCustomer.ID;
            //        entity.NGAY_CAP_NHAT = DateTime.Now;
            //    }
            //    else
            //    {
            //        entity.TREE_LEVEL = 1;
            //        entity.TREE_NODE = CommonHelper.GenTreeNode(null, entity.ID, null);
            //    }
            entities = entities.Select(c =>
            {
                GenTreeNode(c);
                c.MA_BO = c.MA.Substring(0, 3);
                return c;
            }).ToList();
            _itemRepository.Update(entities);
            _staticCacheManager.Remove("GET_ALL_DON_VI");
        }

        public virtual DonVi GetDonViByMaDVQHNS(string MaDVQHNS = null)
        {
            var query = _itemRepository.Table;
            if (string.IsNullOrEmpty(MaDVQHNS))
            {
                return null;
            }
            return query.Where(m => m.MA_DVQHNS == MaDVQHNS).FirstOrDefault();
        }

        #region Cache

        public virtual IList<DonVi> GetCacheDonVis()
        {
            return _staticCacheManager.Get("GET_ALL_DON_VI", () =>
            {
                var query = _itemRepository.Table;
                return query.ToList();
            });
        }

        public DonVi GetCacheDonViByMa(string Ma = null)
        {
            var query = GetCacheDonVis();
            if (string.IsNullOrEmpty(Ma))
            {
                return null;
            }
            return query.Where(m => m.MA == Ma).FirstOrDefault();
        }

        #endregion Cache

        public bool isDonViBanQuanLyDuAn(decimal donViId)
        {
            var donVi = _itemRepository.Table.Where(p => (p.ID == donViId && p.LOAI_DON_VI_ID == (int)enumLoaiDonVi_TheoId.BAN_QUAN_LY_DU_AN) || (p.PARENT_ID == donViId && p.LA_BAN_QL_DU_AN != null && p.LA_BAN_QL_DU_AN.Value == true));
            return donVi.Count() > 0;
        }

        public DonVi GetDonViBanQuanLyDuAn(decimal donViId, bool? isDonViBanQuanLyDuAn = false)
        {
            DonVi donVi = GetDonViById(donViId);
            if (donVi.LA_BAN_QL_DU_AN ?? false)
            {
                if (isDonViBanQuanLyDuAn.Value)
                {
                    string maDonViDa = donVi.MA + "002";
                    DonVi donViDa = GetDonViByMa(maDonViDa);
                    if (donViDa != null && (donViDa.LA_BAN_QL_DU_AN ?? false) == true)
                    {
                        return donViDa;
                    }
                }
                else
                {
                    string maDonViVp = donVi.MA + "001";
                    DonVi donViVp = GetDonViByMa(maDonViVp);
                    if (donViVp != null && (donViVp.LA_BAN_QL_DU_AN ?? false) == false)
                    {
                        return donViVp;
                    }
                }
            }
            return donVi;
        }

        public bool isHasChildDonViBanQLDA(decimal donViId)
        {
            var donVi = _itemRepository.Table.Where(p => (p.PARENT_ID == donViId && p.LA_BAN_QL_DU_AN == true) || (p.ID == donViId && p.LOAI_DON_VI_ID == (int)enumLoaiDonVi_TheoId.BAN_QUAN_LY_DU_AN) || (p.ID == donViId && p.LA_BAN_QL_DU_AN == true));
            return donVi.Count() > 0;
        }

        public bool IsOnlyTaiSanDuAn(decimal donViId)
        {
            var donVi = _itemRepository.Table.Where(p => p.ID == donViId && p.LA_BAN_QL_DU_AN == true);
            return donVi.Count() > 0;
        }

        public bool isDonViSuNghiep(decimal donViId)
        {
            var donVi = _itemRepository.Table.Where(p => p.ID == donViId && p.LoaiDonVi.TREE_NODE.Contains("00000012"));
            return donVi.Count() > 0;
        }

        public IPagedList<DonVi> SearchListKiemTraLoaiHinhDonVi(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? donViId = null, bool IsQuanTri = false, decimal? NguoiDungID = null)
        {
            var query = _itemRepository.Table;
            var donVi = _itemRepository.GetById(donViId);
            if (donVi == null) return null;
            //query = query.Where(dv => dv.ID == donVi.ID || dv.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            if (donViId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
            {
                query = query.Where(c => c.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
            }
            if (NguoiDungID > 0 && !IsQuanTri)
            {
                var l_list_idDV_nguoiDung = _nguoiDungDonViRepository.Table.Where(p => p.NGUOI_DUNG_ID == NguoiDungID).Select(p => p.DON_VI_ID).ToList();
                var l_list_idDVCon = GetListIdDonViChild(l_list_idDV_nguoiDung);
                query = from q in query
                        join idDVs in l_list_idDVCon
                        on q.ID equals idDVs
                        select q;
            }

            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (LoaiDonViSearch > 0)
            {
                var listLoaiDonVi = _loaiDonViService.GetListDonViByTreeNode(LoaiDonViSearch);
                var arrayID = listLoaiDonVi.Select(i => i.ID).ToArray();
                query = query.Where(c => arrayID.Contains(c.LOAI_DON_VI_ID ?? 0));
            }
            if (CapDonViSearch > 0)
                query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            //Loại bỏ đơn vị con của đơn vị quản lý dự án
            query = query.Where(c => !(c.DonViCha != null && c.DonViCha.LOAI_DON_VI_ID == _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_BAN_QUAN_LY_DU_AN).ID));
            query = query.OrderBy(c => c.TREE_NODE);
            return new PagedList<DonVi>(query, pageIndex, pageSize);
        }

        public IPagedList<DonVi> SearchAllDonViXacNhanDuLieu(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, DateTime? NGAY_XAC_NHAN = null)
        {
            var query = _itemRepository.Table.Where(c => c.IS_XAC_NHAN_DU_LIEU == true && c.MA.Length == 3);
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (NGAY_XAC_NHAN != null)
            {
                query = query.Where(m => m.NGAY_XAC_NHAN.Value.Date >= NGAY_XAC_NHAN.Value.Date);
            }
            query = query.OrderByDescending(c => c.NGAY_XAC_NHAN);
            return new PagedList<DonVi>(query, pageIndex, pageSize);
        }

        public IPagedList<DonVi> SearchAllDonViChuaNhapTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID != false);
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper();
                if (Keysearch.StartsWith("\"") && Keysearch.EndsWith("\""))
                {
                    Keysearch = Keysearch.Trim('"');
                    query = query.Where(c => c.TEN.ToUpper().Equals(Keysearch) || c.MA.ToUpper().Equals(Keysearch));
                }
                else
                    query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
            }
            if (ParentID > 0)
            {
                query = query.Where(c => c.PARENT_ID == ParentID);
            }
            //if (!string.IsNullOrEmpty(MaBo))
            //{
            //    query = query.Where(c => c.MA_BO == MaBo);
            //}
            if (CapDonViSearch > 0)
                query = query.Where(c => c.CAP_DON_VI_ID == CapDonViSearch);
            if (listCapDonVis != null && listCapDonVis.Count() > 0)
            {
                query = query.Where(c => listCapDonVis.Contains((int)(c.CAP_DON_VI_ID ?? 0)));
            }
            if (donViId > 0)
            {
                if (donViId.Value != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var donVi = _itemRepository.GetById(donViId.Value);
                    query = query.Where(c => c.ID == donViId.Value || c.TREE_NODE.StartsWith(donVi.TREE_NODE + "-"));
                }
                //var ts = _taiSanService.GetTaiSanByDonViIdAndLoaiTaiSanId(donViId.Value, LoaiTaiSanId.Value);
            }
            query = query.Where(c => c.CO_TAI_SAN == true);
            query = query.OrderBy(c => c.MA);
            return new PagedList<DonVi>(query, pageIndex, pageSize);
        }

        #region read only

        public DonVi GetReadOnlyDonViByMa(string Ma = null)
        {
            var query = _itemRepository.TableNoTracking;
            if (string.IsNullOrEmpty(Ma))
            {
                return null;
            }
            return query.Where(m => m.MA == Ma).FirstOrDefault();
        }

        #endregion read only

        #region Cập nhật mã T

        public IQueryable<DMDC_DonViNganSach> GetListDonViChuaCapNhatMaT(decimal donViId)
        {
            var maChuong = GetDonViById(donViId)?.MA_CHUONG;
            if (!string.IsNullOrEmpty(maChuong))
            {
                var listDonViDMDC = _dMDC_DonViNganSachrepository.Table.Where(c => c.CHUONG == maChuong);
                var dmDonVi = _itemRepository.Table.Where(c => c.MA_CHUONG == maChuong);
                var query = from dvdc in listDonViDMDC
                            join dv in dmDonVi on dvdc.MA equals dv.MA_DVQHNS into Group
                            from dvNullAble in Group.DefaultIfEmpty()
                            select new { dvdc, dvNullAble };
                var result = query.Where(c => c.dvNullAble == null).Select(c => c.dvdc);
                return result;
            }
            return null;
        }

        public virtual IPagedList<DMDC_DonViNganSach> SearchListDonViChuaCapNhatMaT(int pageIndex = 0, int pageSize = 10, string Keysearch = null, decimal donViId = 0
           )
        {
            var query = GetListDonViChuaCapNhatMaT(donViId);
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) || c.MA.ToUpper().Contains(Keysearch.ToUpper())).OrderBy(p => p.MA);
            }
            return new PagedList<DMDC_DonViNganSach>(query, pageIndex, pageSize);
        }

        public bool CheckTaiKhoanDuocCapNhatMaT(decimal donViId, decimal nguoiDungID, bool isQuanTri = false)
        {
            var listDonViBoNganh = GetIQueryableAllDonViDuocNhapLoaiTaiSanDonVi().Select(c => c.ID).ToList();
            var listDonViNguoiDung = _nguoiDungDonViRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiDungID).Select(c => c.DON_VI_ID).Distinct().ToList();
            var IsHaveDonViBoNganh = listDonViNguoiDung.Any(c => listDonViBoNganh.Contains(c));
            var countDonViChuyenMa = GetListDonViChuaCapNhatMaT(donViId)?.Count();
            // Nếu tài khoản có gán đơn vị tổng hợp + có đơn vị chưa cập nhật mã T thì return true
            if (isQuanTri)
            {
                return countDonViChuyenMa > 0;
            }
            return IsHaveDonViBoNganh && countDonViChuyenMa > 0;
        }

        #endregion Cập nhật mã T

        #region Don Vị được phép nhập loại TSDV

        public IQueryable<DonVi> GetIQueryableAllDonViDuocNhapLoaiTaiSanDonVi()
        {
            return _itemRepository.Table.Where(c => c.MA.Length == 3 && (c.TRANG_THAI_ID ?? false));
        }

        public IList<DonVi> GetDonViDuocPhepNhapTSDVForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? NguoiDungID = 0)
        {
            var CurrenDonVi = _itemRepository.GetById(donViId);
            if (CurrenDonVi != null)
            {
                var query = GetIQueryableAllDonViDuocNhapLoaiTaiSanDonVi();
                var nguoiDung = _nguoiDungService.GetNguoiDungById(NguoiDungID);
                query = query.Where(c => c.ID == donViId);
                //if (CurrenDonVi.ID != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG )
                //{
                //    query = query.Where(c => c.ID == donViId);
                //}
                if (!String.IsNullOrEmpty(tenDonVi))
                    query = query.Where(c => c.TEN.ToUpper().Contains(tenDonVi.ToUpper()) || c.MA.ToUpper().Contains(tenDonVi.ToUpper())).OrderBy(p => p.MA);
                query = query.OrderBy(p => p.MA).Take(10);
                return query.ToList();
            }
            return null;
        }

        #endregion Don Vị được phép nhập loại TSDV

        #endregion Methods
    }
}