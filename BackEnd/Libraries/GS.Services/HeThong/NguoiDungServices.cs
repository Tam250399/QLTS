//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
//
using GS.Core.Domain.HeThong;
using GS.Core.Domain.NghiepVu;
using GS.Data;
using GS.Services.DanhMuc;
using GS.Services.Security;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class NguoiDungService : INguoiDungService
    {
        #region Fields
        private readonly CauHinhNguoiDung _customerSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NguoiDung> _itemRepository;
        private readonly IRepository<VaiTroNguoiDungMapping> _vaiTroNguoiDungRepository;
        private readonly IVaiTroNguoiDungService _vaiTroNguoiDungService;
        private readonly IRepository<DonVi> _donViRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<NguoiDungDonViMapping> _nguoiDungDonViRepository;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public NguoiDungService(CauHinhNguoiDung customerSettings,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NguoiDung> itemRepository,
            IRepository<VaiTroNguoiDungMapping> vaiTroNguoiDungRepository,
            IEncryptionService encryptionService,
            IRepository<NguoiDungDonViMapping> nguoiDungDonViRepository,
            IVaiTroNguoiDungService vaiTroNguoiDungService,
            IRepository<DonVi> DonViRepository
            )
        {
            this._nguoiDungDonViRepository = nguoiDungDonViRepository;
            this._vaiTroNguoiDungRepository = vaiTroNguoiDungRepository;
            this._customerSettings = customerSettings;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._encryptionService = encryptionService;
            this._vaiTroNguoiDungService = vaiTroNguoiDungService;
            _donViRepository = DonViRepository;
        }

        #endregion

        #region Methods
        public virtual IList<NguoiDung> GetAllNguoiDungs(int take = 0)
        {
            var query = _itemRepository.Table;
            if (take > 0)
            {
                query = query.Take(take);
            }
            return query.ToList();
        }
        public virtual IList<NguoiDung> GetAllNguoiDungsChuaDb()
        {
            var query = _itemRepository.Table.Where(c => c.DB_ID == null && c.TEN_DANG_NHAP != "admin");
            return query.ToList();
        }
        public virtual IList<NguoiDung> GetAllNguoiDungByDonViId(decimal DonViId)
        {
            //var key = string.Format("TAT_CA_NGUOI_DUNG_TRONG_TOA_{0}", DonViId);
            //return _staticCacheManager.Get(key, () =>
            //{});
            var query = _itemRepository.Table.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
                .Where(z => z.DonViId == DonViId)
                .Select(z => z.NguoiDung).OrderBy(c => c.TEN_DAY_DU);
            return query.ToList();
        }
        public virtual IList<NguoiDung> GetAllNguoiDungByDonViIdAndMaVaiTro(decimal DonViId, string MaVaitro)
        {
            var key = string.Format("TAT_CA_NGUOI_DUNG_TRONG_TOA_{0}_CO_VAITRO_({1})", DonViId, MaVaitro);
            return _staticCacheManager.Get(key, () =>
            {
                var query = _itemRepository.Table.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
                   .Where(z => z.DonViId == DonViId)
                   .Select(z => z.NguoiDung);
                if (MaVaitro != null && !MaVaitro.Equals(""))
                {
                    var NguoiDungIds = _vaiTroNguoiDungService.GetMapByMaVaiTro(MaVaitro).Select(c => c.NGUOI_DUNG_ID).ToList();
                    query = query.Where(c => NguoiDungIds.Contains(c.ID));
                }
                return query.OrderBy(c => c.TEN_DAY_DU).ToList();
            });
        }
        public virtual IPagedList<NguoiDung> SearchNguoiDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string Tendaydu = null, string Macanbo = null, decimal DonViId = 0, decimal[] DonViIds = null, string MaVaiTro = null, IList<int> ListNguoiDungDaChon = null, decimal idVaiTro = 0, decimal donViId = 0, decimal donViBoTinhId = 0, decimal donViHuyenXaId = 0, decimal? nguoiTaoId = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Keysearch))
            {
                Keysearch = Keysearch.ToUpper().Trim();
                query = query.Where(c => c.TEN_DAY_DU.ToUpper().Contains(Keysearch)
                                    || c.TEN_DANG_NHAP.ToUpper().Contains(Keysearch)
                                    || c.MA_CAN_BO.ToUpper().Contains(Keysearch)
                                    || c.EMAIL.ToUpper().Contains(Keysearch));
            }


            if (!string.IsNullOrEmpty(Tendaydu))
            {
                query = query.Where(c => c.TEN_DAY_DU.Contains(Tendaydu));
            }
            if (!string.IsNullOrEmpty(Macanbo))
            {
                query = query.Where(c => c.MA_CAN_BO.Contains(Tendaydu));
            }
            if (DonViId > 0)
            {
                var treeNode = _donViRepository.GetById(DonViId)?.TREE_NODE;
                if (!string.IsNullOrEmpty(treeNode))
                {
                    if (nguoiTaoId > 0)
                    {
                        query = (from nd in query
                                 join mapnd in _nguoiDungDonViRepository.Table
                                 on nd.ID equals mapnd.NGUOI_DUNG_ID
                                 join dv in _donViRepository.Table
                                 on mapnd.DON_VI_ID equals dv.ID
                                 where dv.TREE_NODE.StartsWith(treeNode) && dv.TRANG_THAI_ID != false
                                 select nd).Union(from nd1 in query where nd1.NGUOI_TAO == nguoiTaoId select nd1).Distinct();
                    }
                    else
                    {
                        query = (from nd in query
                                 join mapnd in _nguoiDungDonViRepository.Table
                                 on nd.ID equals mapnd.NGUOI_DUNG_ID
                                 join dv in _donViRepository.Table
                                 on mapnd.DON_VI_ID equals dv.ID
                                 where dv.TREE_NODE.StartsWith(treeNode) && dv.TRANG_THAI_ID != false
                                 select nd).Distinct();
                    }
                }
               
            }
            /*if (nguoiTaoId > 0)
            { query = query.Where(m => m.NGUOI_TAO == nguoiTaoId); }*/
            //if (DonViId > 0)
            //{
            //    query = query.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
            //        .Where(z => z.DonViId == DonViId)
            //        .Select(z => z.NguoiDung);
            //}
            if (donViHuyenXaId > 0)
            {
                var donViHuyenXa = _donViRepository.GetById(donViHuyenXaId);
                query = query.Where(dv => dv.NguoiDungDonViMappings.Any(map => map.DON_VI_ID == donViHuyenXaId || map.donvi.TREE_NODE.StartsWith(donViHuyenXa.TREE_NODE + "-")));
            }
            else
            {
                if (donViBoTinhId > 0 && donViBoTinhId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && donViHuyenXaId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var dvBoTinh = _donViRepository.GetById(donViBoTinhId);
                    query = query.Where(dv => dv.NguoiDungDonViMappings.Any(map => map.DON_VI_ID == dvBoTinh.ID || map.donvi.TREE_NODE.StartsWith(dvBoTinh.TREE_NODE + "-")));
                }
            }
            if (DonViIds != null && DonViIds.Length > 0)
            {
                query = query.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
                    .Where(z => DonViIds.Contains(z.DonViId))
                    .Select(z => z.NguoiDung);
            }
            if (!string.IsNullOrEmpty(MaVaiTro))
            {
                query = query.Join(_vaiTroNguoiDungRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, MaVaiTro = y.vaiTro.MA })
                    .Where(z => z.MaVaiTro == MaVaiTro)
                    .Select(z => z.NguoiDung);
            }
            if (idVaiTro > 0)
            {
                query = query.Join(_vaiTroNguoiDungRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, idVaiTro = y.vaiTro.ID })
                    .Where(z => z.idVaiTro == idVaiTro)
                    .Select(z => z.NguoiDung);
            }
            if (ListNguoiDungDaChon != null && ListNguoiDungDaChon.Count > 0)
            {
                query = query.Where(m => !ListNguoiDungDaChon.Contains(Convert.ToInt32(m.ID)));
            }
            return new PagedList<NguoiDung>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<NguoiDung> SearchDuyetNguoiDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string Tendaydu = null, string Macanbo = null, decimal TrangThaiId = 1, decimal DonViId = 0, decimal[] DonViIds = null, string MaVaiTro = null, IList<int> ListNguoiDungDaChon = null, decimal idVaiTro = 0, decimal donViId = 0, decimal donViBoTinhId = 0, decimal donViHuyenXaId = 0, decimal? nguoiTaoId = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN_DAY_DU.ToUpper().Contains(Keysearch.ToUpper()) || c.TEN_DANG_NHAP.ToUpper().Contains(Keysearch.ToUpper()) || c.MA_CAN_BO.ToUpper().Contains(Keysearch.ToUpper()) || c.EMAIL.ToUpper().Contains(Keysearch.ToUpper()));
            }
            if (!string.IsNullOrEmpty(Tendaydu))
            {
                query = query.Where(c => c.TEN_DAY_DU.Contains(Tendaydu));
            }
            if (!string.IsNullOrEmpty(Macanbo))
            {
                query = query.Where(c => c.MA_CAN_BO.Contains(Tendaydu));
            }
            if (DonViId > 0)
            {
                //query = query.Where(m => m.IS_QUAN_TRI == false);
                var CurrenDonVi = _donViRepository.GetById(DonViId);
                //var donVis = new List<decimal>();
                //if (CurrenDonVi != null)
                //{
                //    donVis = _donViRepository.Table.Where(c => c.TREE_NODE.StartsWith(CurrenDonVi.TREE_NODE) && c.TRANG_THAI_ID != false).Select(c => c.ID).ToList();
                //}

                //var nddv = _nguoiDungDonViRepository.Table.Where(c => donVis.Contains(c.DON_VI_ID)).Select(c => c.NGUOI_DUNG_ID).Distinct().ToList();
                //query = query.Where(c => nddv.Contains(c.ID));

                query = (from nd in query
                        join mapnd in _nguoiDungDonViRepository.Table
                        on nd.ID equals mapnd.NGUOI_DUNG_ID
                        join dv in _donViRepository.Table
                        on mapnd.DON_VI_ID equals dv.ID
                        where dv.TREE_NODE.StartsWith(CurrenDonVi.TREE_NODE) && dv.TRANG_THAI_ID != false
                        select nd).Distinct();
            }

            //if (DonViId > 0)
            //{
            //    query = query.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
            //        .Where(z => z.DonViId == DonViId)
            //        .Select(z => z.NguoiDung);
            //}
            if (donViHuyenXaId > 0)
            {
                var donViHuyenXa = _donViRepository.GetById(donViHuyenXaId);
                query = query.Where(dv => dv.NguoiDungDonViMappings.Any(map => map.DON_VI_ID == donViHuyenXaId || map.donvi.TREE_NODE.StartsWith(donViHuyenXa.TREE_NODE + "-")));
            }
            else
            {
                if (donViBoTinhId > 0 && donViBoTinhId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && donViHuyenXaId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    var dvBoTinh = _donViRepository.GetById(donViBoTinhId);
                    query = query.Where(dv => dv.NguoiDungDonViMappings.Any(map => map.DON_VI_ID == dvBoTinh.ID || map.donvi.TREE_NODE.StartsWith(dvBoTinh.TREE_NODE + "-")));
                }
            }
            if (DonViIds != null && DonViIds.Length > 0)
            {
                query = query.Join(_nguoiDungDonViRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, DonViId = y.DON_VI_ID })
                    .Where(z => DonViIds.Contains(z.DonViId))
                    .Select(z => z.NguoiDung);
            }
            if (!string.IsNullOrEmpty(MaVaiTro))
            {
                query = query.Join(_vaiTroNguoiDungRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, MaVaiTro = y.vaiTro.MA })
                    .Where(z => z.MaVaiTro == MaVaiTro)
                    .Select(z => z.NguoiDung);
            }
            if (idVaiTro > 0)
            {
                query = query.Join(_vaiTroNguoiDungRepository.Table, x => x.ID, y => y.NGUOI_DUNG_ID, (x, y) => new { NguoiDung = x, idVaiTro = y.vaiTro.ID })
                    .Where(z => z.idVaiTro == idVaiTro)
                    .Select(z => z.NguoiDung);
            }
            if (ListNguoiDungDaChon != null && ListNguoiDungDaChon.Count > 0)
            {
                query = query.Where(m => !ListNguoiDungDaChon.Contains(Convert.ToInt32(m.ID)));
            }
            if (nguoiTaoId > 0)
            {
                query = query.Where(m => m.NGUOI_TAO == nguoiTaoId);
            }
            if (TrangThaiId >= 0)
            {
                query = query.Where(m => m.TRANG_THAI_ID == TrangThaiId);
            }

            return new PagedList<NguoiDung>(query, pageIndex, pageSize);
        }

        public virtual NguoiDung GetNguoiDungById(decimal? Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual NguoiDung GetNguoiDungByMa(string Ma)
        {
            if (Ma == null || Ma.Equals(""))
                return new NguoiDung();
            return _itemRepository.Table.Where(c => c.MA_CAN_BO == Ma).FirstOrDefault();
        }
        public virtual void InsertNguoiDung(NguoiDung entity, decimal? DonViId = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            if (entity.NGAY_TAO == null)
                entity.NGAY_TAO = DateTime.Now;

            if (DonViId > 0)
            {
                var nguoiDungDonVi = new NguoiDungDonViMapping();
                nguoiDungDonVi.DON_VI_ID = DonViId.Value;
                nguoiDungDonVi.NGUOI_DUNG_ID = entity.ID;
                _nguoiDungDonViRepository.Insert(nguoiDungDonVi);
            }
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        //
        public virtual void InsertNguoiDungV2(NguoiDung entity, List<decimal> ListDonViId = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            if (entity.NGAY_TAO == null)
                entity.NGAY_TAO = DateTime.Now;

            if (ListDonViId != null)
            {
                foreach (var DonViId in ListDonViId)
                {
                    var nguoiDungDonVi = new NguoiDungDonViMapping();
                    nguoiDungDonVi.DON_VI_ID = DonViId;
                    nguoiDungDonVi.NGUOI_DUNG_ID = entity.ID;
                    _nguoiDungDonViRepository.Insert(nguoiDungDonVi);
                }
            }
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }

        public virtual void UpdateNguoiDung(NguoiDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNguoiDung(NguoiDung entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual NguoiDungKetQuaDangNhap ValidateNguoiDung(string username, string password)
        {
            var customer = GetNguoiDungByUsername(username);

            if (customer == null)
                return NguoiDungKetQuaDangNhap.CustomerNotExist;
            if (customer.TRANG_THAI_ID == (int)NguoiDungStatusID.Lock || customer.TRANG_THAI_ID == (int)NguoiDungStatusID.choduyet)
                return NguoiDungKetQuaDangNhap.LockedOut;
            if (!PasswordsMatch(customer, password))
                return NguoiDungKetQuaDangNhap.WrongPassword;

            return NguoiDungKetQuaDangNhap.Successful;
        }
        public virtual NguoiDung GetNguoiDungByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _itemRepository.Table
                        orderby c.ID
                        where c.TEN_DANG_NHAP == username
                        select c;
            var customer = query.ToList().FirstOrDefault();
            return customer;
        }
        public NguoiDungCache ToNguoiDungCache(NguoiDung nguoiDung)
        {
            var nguoiDungCache = new NguoiDungCache();
            nguoiDungCache.ID = nguoiDung.ID;
            nguoiDungCache.GUID = nguoiDung.GUID;
            nguoiDungCache.TEN_DANG_NHAP = nguoiDung.TEN_DANG_NHAP;
            nguoiDungCache.MAT_KHAU = nguoiDung.MAT_KHAU;
            nguoiDungCache.TEN_DAY_DU = nguoiDung.TEN_DAY_DU;
            nguoiDungCache.EMAIL = nguoiDung.EMAIL;
            nguoiDungCache.NGAY_TAO = nguoiDung.NGAY_TAO;
            nguoiDungCache.TRANG_THAI_ID = nguoiDung.TRANG_THAI_ID;
            nguoiDungCache.NGAY_TRUY_CAP = nguoiDung.NGAY_TRUY_CAP;
            nguoiDungCache.IP_TRUY_CAP = nguoiDung.IP_TRUY_CAP;
            nguoiDungCache.GHI_CHU = nguoiDung.GHI_CHU;
            nguoiDungCache.IS_QUAN_TRI = nguoiDung.IS_QUAN_TRI;
            nguoiDungCache.UNG_DUNG = nguoiDung.UNG_DUNG;
            nguoiDungCache.MOBILE = nguoiDung.MOBILE;
            nguoiDungCache.MAT_KHAU_KEY = nguoiDung.MAT_KHAU_KEY;
            nguoiDungCache.MA_CAN_BO = nguoiDung.MA_CAN_BO;
            nguoiDungCache.PASSWORD_HASH = nguoiDung.PASSWORD_HASH;
            return nguoiDungCache;

        }
        public virtual NguoiDung GetNguoiDungByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _itemRepository.Table
                        orderby c.ID
                        where c.EMAIL == email
                        select c;
            var customer = query.ToList().FirstOrDefault();
            return customer;
        }
        public virtual NguoiDung GetNguoiDungByGuid(Guid guid)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == guid);
            return query.FirstOrDefault();
        }
        protected bool PasswordsMatch(NguoiDung nguoiDung, string enteredPassword)
        {
            if (nguoiDung == null || string.IsNullOrEmpty(enteredPassword))
                return false;


            //if (nguoiDung.MAT_KHAU == null)
            //    return false;
            if (nguoiDung.PASSWORD_HASH == null)
                return false;

            //return nguoiDung.MAT_KHAU.Equals(savedPassword);
            return _encryptionService.VerifyHashedPasswordV3(nguoiDung.PASSWORD_HASH, enteredPassword);
        }
        public virtual IList<NguoiDung> GetNguoiDungByListMaCanBo(decimal DonViId, List<string> lstMaCanBo)
        {
            var query = GetAllNguoiDungByDonViId(DonViId).Where(c => lstMaCanBo.Contains(c.MA_CAN_BO));
            return query.ToList();
        }
        public bool KiemTraTrungMa(string MA_CAN_BO, decimal Id)
        {

            return _itemRepository.Table.Any(c => c.MA_CAN_BO == MA_CAN_BO && c.ID != Id);
        }
        public bool KiemTraTrungTen(string TEN_DANG_NHAP, decimal Id)
        {
            return _itemRepository.Table.Any(c => c.TEN_DANG_NHAP.ToLower() == TEN_DANG_NHAP.ToLower() && c.ID != Id);
        }

        public virtual IList<NguoiDung> GetNguoiDungByVaiTroId(decimal vaiTro)
        {
            var query = from nd in _itemRepository.Table
                        join ndvt in _vaiTroNguoiDungRepository.Table on nd.ID equals ndvt.NGUOI_DUNG_ID
                        where ndvt.VAI_TRO_ID == vaiTro
                        orderby nd.ID
                        select nd;
            return query.ToList();
        }

        public virtual NguoiDungKetQuaDangNhap validateResetUser(string username)
        {
            var customer = GetNguoiDungByUsername(username);

            if (customer == null)
                return NguoiDungKetQuaDangNhap.CustomerNotExist;
            if (customer.TRANG_THAI_ID == (int)NguoiDungStatusID.Lock)
                return NguoiDungKetQuaDangNhap.LockedOut;
            return NguoiDungKetQuaDangNhap.Successful;
        }
        public virtual void InsertListNguoiDung(List<NguoiDung> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
        }
        public virtual void UpdateListNguoiDung(List<NguoiDung> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual IList<NguoiDung> getNguoiDungByDonVi(decimal donViId)
        {
            var donVi = _donViRepository.GetById(donViId);
            var treeNode = donVi.TREE_NODE + "-";
            if (donVi == null)
                return null;
            var query = _itemRepository.Table
                .Where(p => p.TRANG_THAI_ID == (int)NguoiDungStatusID.ok && p.NguoiDungDonViMappings.Any(map => map.DON_VI_ID == donViId || map.donvi.TREE_NODE.StartsWith(treeNode)));
            return query.ToList();
        }
        public Boolean XoaNguoiDungChuaDuyet(decimal? NguoiDungId = 0) 
        {
            OracleParameter p1 = new OracleParameter("p_nguoi_dung_id", OracleDbType.Int32, NguoiDungId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("tbl_out", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var model = _dbContext.EntityFromStore<ResultCallStore>("PK_TS_NGHIEPVU.SP_DELETE_NGUOI_DUNG", p1, p2).FirstOrDefault();
                if (model != null)
                {
                    return model.Result;
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
        #endregion
    }
}

