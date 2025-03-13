using GS.Core.Data;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class NguoiDungDonViService : INguoiDungDonViService
    {
        #region Fields
        private readonly IRepository<NguoiDungDonViMapping> _itemRepository;
        private readonly IRepository<DonVi> _donviRepository;
        #endregion

        #region Ctor
        public NguoiDungDonViService(IRepository<NguoiDungDonViMapping> itemRepository,
            IRepository<DonVi> donviRepository
            )
        {
            this._itemRepository = itemRepository;
            this._donviRepository = donviRepository;
        }
        #endregion

        public virtual void InsertNguoiDungDonVi(NguoiDungDonViMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }
        public virtual void DeleteNguoiDungDonVi(NguoiDungDonViMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }
        public virtual void DeleteNguoiDungDonVi(List<NguoiDungDonViMapping> entity)
        {
            if (entity == null || (entity != null && entity.Count == 0))
                throw new ArgumentNullException(nameof(NguoiDungDonViMapping));
            _itemRepository.Delete(entity);
        }
        public virtual IList<NguoiDungDonViMapping> GetMapByNguoiDungId(decimal nguoiDungId)
        {
            var query = _itemRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiDungId);
            if (query != null && query.Count() > 0)
                return query.OrderBy(c => c.donvi.TREE_LEVEL).ToList();
            return null;
        }
        public virtual IList<DonVi> GetDonViByNguoiDungId(decimal nguoiDungId)
        {
            var query = _itemRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiDungId);
            if (query != null && query.Count() > 0)
                return query.Select(x => x.donvi).OrderBy(c => c.TREE_LEVEL).ToList();
            return null;
        }
        public virtual IList<NguoiDungDonViMapping> GetMapByDonViId(decimal DonViId)
        {
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == DonViId);
            return query.ToList();
        }
        public virtual bool KiemTraDaChon(decimal NguoiDungId, decimal DonViId)
        {
            return _itemRepository.Table.Any(c => c.DON_VI_ID == DonViId && c.NGUOI_DUNG_ID == NguoiDungId);
        }
        public NguoiDungDonViMapping GetNguoiDungDonViMapping(decimal NguoiDungId, decimal DonViId)
        {
            return _itemRepository.Table.Where(c => c.DON_VI_ID == DonViId && c.NGUOI_DUNG_ID == NguoiDungId).FirstOrDefault();
        }

        public void DeleteNguoiDungDonViMapping(decimal DonViId = 0, decimal NguoiDungId = 0)
        {
            var query = _itemRepository.Table.Where(m => m.DON_VI_ID == DonViId && m.NGUOI_DUNG_ID == NguoiDungId);
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            _itemRepository.Delete(query);
        }
    }
}
