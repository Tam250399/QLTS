using GS.Services.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GS.NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDoiTacService _doiTacService;
        private readonly IQuocGiaService _quocGiaService;
        public ValuesController(IDoiTacService doiTacService, IQuocGiaService quocGiaService)
        {
            _doiTacService = doiTacService;
            _quocGiaService = quocGiaService;
        }
        // GET api/values
        [HttpGet]
 
        public ActionResult<IEnumerable<string>> Get()
        {
            //test thử dữ liệu xem đã nhận gọi được chưa
            var a = _doiTacService.GetAllDoiTacs();
            var b = _quocGiaService.GetAllQuocGias();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
