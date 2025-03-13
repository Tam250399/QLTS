using GS.Core.Domain.Common;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.NoMatter)]
    public abstract partial class BasePublicController : BaseController
    {
        protected virtual IActionResult InvokeHttp404()
        {
            Response.StatusCode = 404;
            return new EmptyResult();
        }
		protected virtual JsonResult JsonSuccessMessage(string msg = "Ok", dynamic objdata = null)
		{
			return Json(MessageReturn.CreateSuccessMessage(msg, objdata));
		}
		protected virtual JsonResult JsonErrorMessage(string msg = "Error", dynamic objdata = null)
		{
			return Json(MessageReturn.CreateErrorMessage(msg, objdata));
		}
	}
}