using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
	public class KhaiThacChiTietServices : IKhaiThacChiTietServices
	{
		private readonly IRepository<KhaiThacChiTiet> _itemRepository;
        private readonly IRepository<KhaiThac> _khaiThacRepository;
        private readonly IRepository<KhaiThacTaiSan> _khaiThacTaiSanRepository;

        public KhaiThacChiTietServices(IRepository<KhaiThacChiTiet> itemRepository,
		IRepository<KhaiThac> khaiThacRepository, IRepository<KhaiThacTaiSan> khaiThacTaiSanRepository)
		{
			_itemRepository = itemRepository;
            this._khaiThacRepository = khaiThacRepository;
            this._khaiThacTaiSanRepository = khaiThacTaiSanRepository;
        }
		public void DeleteKhaiThacChiTiet(KhaiThacChiTiet entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			//Trước tiên phải xóa hết khai thác tài sản
			var khaiThacTaiSans = _khaiThacTaiSanRepository.Table.Where(c => c.KHAI_THAC_CHI_TIET_ID == entity.ID).ToList();
            if (khaiThacTaiSans != null && khaiThacTaiSans.Count() >0)
            {
				_khaiThacTaiSanRepository.Delete(khaiThacTaiSans);
            }
			_itemRepository.Delete(entity);
		}

		public void DeleteKhaiThacChiTiets(IEnumerable<KhaiThacChiTiet> entities)
		{
			if (entities == null )
				throw new ArgumentNullException(nameof(entities));
			if (entities.Count()>0)
				_itemRepository.Delete(entities);

		}

		public IList<KhaiThacChiTiet> GetAllKhaiThacChiTiets()
		{
			return _itemRepository.Table.ToList();
		}

		public KhaiThacChiTiet GetKhaiThacChiTietById(decimal Id)
		{
			return _itemRepository.GetById(id: Id);
		}

		public IList<KhaiThacChiTiet> GetKhaiThacChiTietByIds(decimal[] newsIds)
		{
			return _itemRepository.Table.Where(x => newsIds.Contains(x.ID)).ToList();
		}

		public IList<KhaiThacChiTiet> GetKhaiThacChiTietByKhaiThacId(decimal? khaiThacId = null)
		{
			return _itemRepository.Table.Where(x => x.KHAI_THAC_ID == khaiThacId).ToList();
		}

		public void InsertKhaiThacChiTiet(KhaiThacChiTiet entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
		}

		public IPagedList<KhaiThacChiTiet> SearchKhaiThacChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? khaiThacId = null)
		{
			var query = _itemRepository.Table;
		
			if (!string.IsNullOrEmpty(Keysearch))
				query = query.Where(p => p.GHI_CHU.Contains(Keysearch));
			if (khaiThacId!= null)
				query = query.Where(p => p.KHAI_THAC_ID == khaiThacId);

			return new PagedList<KhaiThacChiTiet>(query, pageIndex, pageSize);
		}
		public IPagedList<KhaiThacChiTiet> SearchKhaiThacChiTietsForCapNhatSoTien(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string QUYET_DINH_SO = null, decimal LOAI_HINH_KHAI_THAC_ID = 0, DateTime? QUYET_DINH_NGAY = null, string HOP_DONG_SO = null, DateTime? HOP_DONG_NGAY = null, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null, decimal donviId = 0)
		{
			var khaiThacChitiet = _itemRepository.Table;
			var khaiThac = _khaiThacRepository.Table;
			var query = from ktct in khaiThacChitiet
						join kt in khaiThac on ktct.KHAI_THAC_ID equals kt.ID
						where kt.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET
					select new { kt,ktct};
			if (LOAI_HINH_KHAI_THAC_ID > 0)
				query = query.Where(c => c.kt.LOAI_HINH_KHAI_THAC_ID == LOAI_HINH_KHAI_THAC_ID);
			if (donviId > 0)
				query = query.Where(c => c.kt.DON_VI_ID == donviId);
			if (!string.IsNullOrEmpty(QUYET_DINH_SO))
				query = query.Where(c => c.kt.QUYET_DINH_SO.ToUpper().Contains(QUYET_DINH_SO.ToUpper()));
			if (QUYET_DINH_NGAY != null)
				query = query.Where(c => c.kt.QUYET_DINH_NGAY == QUYET_DINH_NGAY.Value.Date);
			if (!string.IsNullOrEmpty(HOP_DONG_SO))
				query = query.Where(c => c.kt.HOP_DONG_SO.ToUpper().Contains(HOP_DONG_SO.ToUpper()) 
				|| c.ktct.HOP_DONG_SO.ToUpper().Contains(HOP_DONG_SO.ToUpper()));
			if (HOP_DONG_NGAY != null)
				query = query.Where(c => c.kt.HOP_DONG_NGAY == HOP_DONG_NGAY || c.ktct.HOP_DONG_NGAY == HOP_DONG_NGAY);
			if (KHAI_THAC_NGAY_TU.HasValue)
			{
				var _tungay = KHAI_THAC_NGAY_TU.Value.Date;
                query = query.Where(x => x.ktct.NGAY_KHAI_THAC >= _tungay);				
			}
			if (KHAI_THAC_NGAY_DEN.HasValue && KHAI_THAC_NGAY_DEN != DateTime.MinValue)
			{
				var _denngay = KHAI_THAC_NGAY_DEN.Value.Date.AddDays(1);
				query = query.Where(x => x.ktct.NGAY_KHAI_THAC_DEN < _denngay);
			}
			var result = query.Select(c => c.ktct).OrderByDescending(c => c.KHAI_THAC_ID);

			return new PagedList<KhaiThacChiTiet>(result, pageIndex, pageSize);
		}

		public void UpdateKhaiThacChiTiet(KhaiThacChiTiet entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
		}
		public virtual KhaiThacChiTiet GetMapByKhaiThacIdAndTaiSanId(decimal khaiThacId, decimal taiSanId)
		{
			var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == khaiThacId && c.TAI_SAN_ID == taiSanId);
			return query.FirstOrDefault();
		}
		public bool checkTrungKhaiThacTaiSan(decimal KhaiThacId, decimal TaiSanId)
		{
			var query = _itemRepository.Table.Where(c => c.KHAI_THAC_ID == KhaiThacId && c.TAI_SAN_ID == TaiSanId);
			if (query.Count() > 0)
			{ return true; }
			else
				return false;
		}
	}
}
