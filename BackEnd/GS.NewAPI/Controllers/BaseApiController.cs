using GS.NewAPI.Infrastruture.Response;
using GS.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GS.NewAPI.Controllers
{
    public class BaseApiController : BaseController
    {
        #region JsonMessageData return
        protected virtual IActionResult OkSuccessMessage<T>(string msg = "Ok", T objdata = default(T))
        {
            return Ok(BaseResponse<T>.CreateSuccessMessage(msg, objdata));
        }
        protected virtual IActionResult OkErrorMessage<T>(string msg = "Error", T objdata = default(T))
        {
            return Ok(BaseResponse<T>.CreateErrorMessage(msg, objdata));
        }
        protected virtual IActionResult OkNotFoundMessage<T>(string msg = "Not Found", T objdata = default(T))
        {
            return Ok(BaseResponse<T>.CreateErrorMessage(msg, objdata));
        }
        protected IActionResult NullModel<T>()
        {
            return Ok(BaseResponse<T>.CreateErrorMessage("Input is not correct"));
        }
        #endregion
    }
}
