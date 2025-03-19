using GS.Services.TaiSans;

namespace GS.NewAPI.Factories
{
    public class TaiSanModelFactory : ITaiSanModelFactory
    {
        private readonly ITaiSanService _taiSanService;
        public TaiSanModelFactory(ITaiSanService taiSanService)
        {
            _taiSanService = taiSanService;
        }
        public bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0)
        {
            var taisan = _taiSanService.GetTaiSanByTen(TenTS: ten, donViId: donViId);
            if (taisan != null && taisan.ID != id)
                return false;
            else return true;
        }
    }
}
