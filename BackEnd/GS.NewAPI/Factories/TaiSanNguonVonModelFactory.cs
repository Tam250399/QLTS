using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;
using static iTextSharp.text.pdf.AcroFields;
using System.Collections.Generic;
using System;
using GS.Core;
using System.Linq;
using GS.Core.Domain.BienDongs;

namespace GS.NewAPI.Factories
{
    public class TaiSanNguonVonModelFactory : ITaiSanNguonVonModelFactory
    {
        private readonly ITaiSanNguonVonService _taiSanNguonVonService;
        private readonly INguonVonService _nguonVonService;
        public TaiSanNguonVonModelFactory(ITaiSanNguonVonService taiSanNguonVonService, INguonVonService nguonVonService)
        {
            _taiSanNguonVonService = taiSanNguonVonService;
            _nguonVonService = nguonVonService;
        }
        public void InsertTaiSanNguonVonFromBienDong(TaiSanModel model, BienDongModel bd)
        {

            var nguonVon = ((enumNguonVon[])Enum.GetValues(typeof(enumNguonVon))).Select(c => (int)c).ToList();
            var _listNV = _nguonVonService.GetNguonVonByIds(nguonVon.Select(c => (decimal)c).ToArray());
            var lstNguonVonModel = new List<NguonVonModel>();
            if (_listNV != null)
            {
                foreach (var _nguonVon in _listNV)
                {
                    lstNguonVonModel.Add(new NguonVonModel()
                    {
                        ID = _nguonVon.ID,
                        TEN = _nguonVon.TEN
                    });
                }
            }
            var nguonVonJson = lstNguonVonModel.toStringJson();
            var lstNguonVon = nguonVonJson.toEntities<NguonVonModel>();
            if (lstNguonVon != null && lstNguonVon.Count > 0)
            {
                List<TaiSanNguonVon> lst = new List<TaiSanNguonVon>();
                foreach (var nv in lstNguonVon)
                {
                    var tsnv = new TaiSanNguonVon();
                    tsnv.TAI_SAN_ID = bd.TAI_SAN_ID;
                    tsnv.NGUON_VON_ID = nv.ID;
                    switch (nv.ID)
                    {
                        case 1:
                            tsnv.GIA_TRI = (decimal)model.GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH ;
                            break;
                        case 3:
                            tsnv.GIA_TRI = (decimal)model.GIA_TRI_SU_DUNG_DAT.NGUON_KHAC;
                            break;
                         // chưa dùng đến các trường hợp n
                        //case 4:
                        //    tsnv.GIA_TRI = (decimal)item.NV_VIEN_TRO;
                        //    break;
                        //case 17:
                        //    tsnv.GIA_TRI = (decimal)item.NV_QUY_HDSN;
                        //    break;
                    }
                    tsnv.BIEN_DONG_ID = (decimal)bd.ID;
                    lst.Add(tsnv);
                    _taiSanNguonVonService.InsertTaiSanNguonVon(tsnv);
                }
            }
        }
    }
}
