using FluentValidation;
using GS.NewAPI.Factories;
using GS.NewAPI.Models;
using GS.NewAPI.Validators.TaiSanValidator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GS.NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSanController : ControllerBase
    {
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        public TaiSanController(ITaiSanModelFactory taiSanModelFactory, ILoaiTaiSanModelFactory loaiTaiSanModelFactory) 
        {
            _taiSanModelFactory = taiSanModelFactory;
            _loaiTaiSanModelFactory = loaiTaiSanModelFactory;
        }
        // GET: api/<TaiSanController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaiSanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaiSanController>
        [HttpPost]
        public IActionResult Post([FromBody] TaiSanModel model)
        {
            var validator = new TaiSanValidator(_taiSanModelFactory, _loaiTaiSanModelFactory);
            var validationResult =  validator.Validate(model);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return Ok();
        }

        // PUT api/<TaiSanController>/5
        [HttpPut]
        public async Task<IActionResult> SuaTaiSan([FromBody] TaiSanModel value)
        {
           _taiSanModelFactory.UpdateTaiSan(value);
           return Ok();
           
        }
        [HttpPut("SuaDanhSachTaiSan")]
        public async Task<IActionResult> SuaDanhSachTaiSan([FromBody] List<TaiSanModel> value)
        {
            _taiSanModelFactory.UpdateTaiSan(value);
            return Ok();

        }
        // DELETE api/<TaiSanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
