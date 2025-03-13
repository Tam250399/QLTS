using GS.Core.Configuration;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using GS.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [Authorize]
    public class BaseAdminController : BaseApiController
    {

        private NguoiDung _currentUser = null;
        private decimal? _currentSource = null;
        protected NguoiDung currentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;
                _currentUser = new NguoiDung();
                try
                {

                    var val = this.User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).First().Value;
                    _currentUser.ID = !string.IsNullOrEmpty(val) ? Convert.ToDecimal(val) : 0m;

                    val = this.User.Claims.Where(c => c.Type == ClaimTypes.PrimaryGroupSid).First().Value;
                    if (!string.IsNullOrEmpty(val))
                        _currentUser.GUID = new Guid(val);
                    _currentUser.TEN_DANG_NHAP = this.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
                    _currentUser.TEN_DAY_DU = this.User.Claims.Where(c => c.Type == ClaimTypes.Name).First().Value;
                    return _currentUser;
                }
                catch (Exception)
                {

                    return null;
                }

            }

        }
        protected decimal currentSource
        {
            get
            {
                if (_currentSource != null)
                    return _currentSource.Value;
                var _gsConfig = EngineContext.Current.Resolve<GSConfig>();
                if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameKhoCSDL)
                {
                    _currentSource = (decimal)enumNguonTaiSan.CSDLQGTSC;
                }
                else if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameQLTSNN)
                {
                    _currentSource = (decimal)enumNguonTaiSan.QLTSNN;
				}
				else if (currentUser.TEN_DANG_NHAP == _gsConfig.UserNameQLTSNoiNganh)
				{
					_currentSource = (decimal)enumNguonTaiSan.TSNoiNganh;
				}
				else
                {
                    _currentSource = (decimal)enumNguonTaiSan.KHAC;
                }
                return _currentSource.Value;
            }
        }
        public bool CheckCurrentUser()
        {
            var _nguoiDungService = EngineContext.Current.Resolve<INguoiDungService>();
            _currentUser = currentUser;
            if (_currentUser == null)
                return false;
            NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungById(_currentUser.ID);
            if (nguoiDung == null)
                return false;

            if (nguoiDung.TEN_DANG_NHAP != _currentUser.TEN_DANG_NHAP)
                return false;
            return true;
        }
    }
}
