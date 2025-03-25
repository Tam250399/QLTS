using GS.Core.Caching;
using GS.Core;
using GS.Services.DanhMuc;
using GS.Services.HeThong;

namespace GS.NewAPI.Factories
{
    public class LoaiTaiSanModelFactory : ILoaiTaiSanModelFactory
    {
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        public LoaiTaiSanModelFactory(ILoaiTaiSanService loaiTaiSanService)
        {
            _loaiTaiSanService = loaiTaiSanService;

        }
        public bool CheckLoaiTaiSanCha(decimal id)
        {
            var countSub = _loaiTaiSanService.GetCountSub(id);
            if (countSub > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
