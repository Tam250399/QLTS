using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GS.WebApi.Models.CCDC;
using Microsoft.AspNetCore.Mvc;

namespace GS.WebApi.Controllers
{
    public class CCDCController : BaseAdminController
    {
        [HttpPost]
        public IActionResult UpdateListCCDC([FromBody]List<CCDCModel> model)
        {
            foreach (var item in model)
            {

            }
            return OkSuccessMessage("done");
        }
    }
}