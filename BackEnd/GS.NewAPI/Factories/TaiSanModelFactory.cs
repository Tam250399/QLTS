using AutoMapper;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;
using System.Collections.Generic;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace GS.NewAPI.Factories
{
    public class TaiSanModelFactory : ITaiSanModelFactory
    {
        private readonly ITaiSanService _taiSanService;
        private readonly IMapper _mapper;
        public TaiSanModelFactory(ITaiSanService taiSanService, IMapper mapper)
        {
            _taiSanService = taiSanService;
            _mapper = mapper;

        }

        public void UpdateTaiSan(TaiSanModel entity)
        {
            
            var taiSan = _mapper.Map<TaiSanModel, TaiSan>(entity);
            _taiSanService.UpdateTaiSan(taiSan);

        }

        public void UpdateTaiSan(List<TaiSanModel> entities)
        {
            var taiSanList = _mapper.Map<List<TaiSanModel>, List<TaiSan>>(entities);
            _taiSanService.UpdateTaiSan(taiSanList);

        }
    }
}
