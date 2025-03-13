using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GS.Api.Infrastructure.Api;
using GS.Core.Domain.Common;
using GS.Core.Domain.DB;

namespace GS.WebApi.Controllers
{
    
    public class BaseApiController: ControllerBase
    {
        #region JsonMessageData return
        protected virtual IActionResult OkSuccessMessage(string msg = "Ok", object objdata = null)
        {
            return Ok(MessageReturn.CreateSuccessMessage(msg, objdata));
        }
        protected virtual IActionResult OkErrorMessage(string msg = "Error", object objdata = null)
        {
            return Ok(MessageReturn.CreateErrorMessage(msg, objdata));
        }
        protected virtual IActionResult OkNotFoundMessage(string msg = "Not Found", object objdata = null)
        {
            return Ok(MessageReturn.CreateErrorMessage(msg, objdata));
        }
        protected IActionResult NullModel()
        {
            return Ok(MessageReturn.CreateErrorMessage("Input is not correct"));
        }
        #endregion
        #region ResponseApi return
        protected virtual IActionResult OkSuccessResponseApi(string msg = "Ok", object objdata = null)
        {
            return Ok(ResponseApi.CreateSuccessMessage(msg, objdata));
        }
        protected virtual IActionResult OkErrorResponseApi(string msg = "Error", object objdata = null)
        {
            return Ok(ResponseApi.CreateErrorMessage(msg, objdata));
        }
        protected IActionResult NullModelResponseApi()
        {
            return Ok(ResponseApi.CreateErrorMessage("Input is not correct"));
        }
        #endregion
    }
}
