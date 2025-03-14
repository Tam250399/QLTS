using DevExpress.DataProcessing;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models.DanhMuc;
using GS.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.NewAPI.Factories
{
    public class DanhMucModelFactory: IDanhMucModelFactory
    {
        #region Ctor
        private readonly IQuocGiaService _quocGiaService;
       
        public DanhMucModelFactory(
            IQuocGiaService quocGiaService           
            )
        {
            this._quocGiaService = quocGiaService;
           
        }
        #endregion
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            var query = _quocGiaService.GetAllQuocGias();
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public IList<QuocGiaModel> SearchQuocGiasByName(string tenQuocGia)
        {
            var query = _quocGiaService.SearchQuocGias(Keysearch: tenQuocGia);
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public MessageReturn DeleteQuocGia(decimal ID = 0)
        {
            QuocGia quocGia = _quocGiaService.GetQuocGiaById(Id: ID);
            try
            {
                if (quocGia.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                quocGia.DB_ID = null;
                _quocGiaService.UpdateQuocGia(quocGia);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }

        }
        #endregion

        #region Địa bàn

        #endregion
    }
}
