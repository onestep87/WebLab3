using backend.db;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        [HttpDelete]
        [Route("delete")]
        [EnableCors("DropdownCorsPolicy")]
        public async Task<IActionResult> Delete(int number)
        {
            try
            {
                DropdownManagment db = new DropdownManagment();

                await db.DeleteDropdown(number);
                return StatusCode(200);
            }
            catch(Exception e)
            {
                return StatusCode(400, "Details: " + e.Message);
            }
        }

        [HttpGet]
        [Route("getNumbers")]
        [EnableCors("DropdownCorsPolicy")]
        public async Task<IActionResult> GetNumbers()
        {
            try
            {
                DropdownManagment db = new DropdownManagment();

                var response = await db.GetNumbers();
                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                return StatusCode(404, "Details: " + e.Message);
            }
        }

        [HttpGet]
        [Route("get")]
        [EnableCors("DropdownCorsPolicy")]
        public async Task<IActionResult> GetDropdown(int number = -1)
        {
            try
            {
                DropdownManagment db = new DropdownManagment();
                IEnumerable result = await db.GetDropdown(number);

                return StatusCode(200, result);
            }
            catch(Exception e)
            {
                return StatusCode(404, "Details: " + e.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        [EnableCors("DropdownCorsPolicy")]
        public async Task<IActionResult> CreateDropdown([FromBody]List<string> data)
        {
            try
            {
                DropdownManagment db = new DropdownManagment();
                await db.CreateDropdown(data);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(404, "Details: " + e.Message);
            }
        }
    }
}
