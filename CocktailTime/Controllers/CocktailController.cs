using CocktailTime.Controllers.DTO;
using CocktailTime.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CocktailTime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CocktailController: Controller
    {
        private readonly ICocktailRepository _repo;
        public CocktailController(ICocktailRepository repo)
            => _repo = repo;

        [HttpPost]
        public async Task<ActionResult> SavePhoneNumber([FromBody] CocktailDTO dto) 
        {
            try
            {
                await dto.SavePhoneNumber(_repo);
                return Ok("Successfully saved phone number");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPhoneNumber([FromBody] CocktailDTO dto)
        {
            try
            {
                var phoneNumber = await dto.GetPhoneNumber(_repo);
                if (phoneNumber != null)
                    return Ok(phoneNumber);
                else
                    return NotFound("Phone number not found");
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePhoneNumber([FromBody] CocktailDTO dto) 
        {
            try
            {
                var phoneNumber = await dto.GetPhoneNumber(_repo);
                if (phoneNumber != null)
                {
                    await CocktailDTO.DeletePhoneNumber(_repo, phoneNumber.ID);
                    return Ok("Deletion was successful");
                }
                else
                    return NotFound("The phone number to delete was not found");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
