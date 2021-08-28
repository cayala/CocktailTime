using CocktailTime.Controllers.DTO;
using CocktailTime.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CocktailTime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CocktailController : Controller
    {
        private readonly ICocktailRepository _repo;
        public CocktailController(ICocktailRepository repo)
            => _repo = repo;

        /// <summary>
        /// Saves a phone number and timezone to Cosmos DB so it can recieve a cocktail recipe
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SavePhoneNumber([FromBody] CocktailDTO dto)
        {
            try
            {
                await dto.SavePhoneNumber(_repo);
                return Ok("Successfully saved phone number");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the document from the cosmos db. Returns 404 error if phone number was not found
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("get")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Deletes the phone number from the cosmos db. Returns a 404 if it doesn't find it
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Loads the json file with all the timezones
        /// </summary>
        /// <returns></returns>
        [HttpGet("gettimezones")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult GetTimezones()
        {
            string json = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/TimeZones.json");
            if (string.IsNullOrWhiteSpace(json))
                return NotFound();
            return Json(json);
        }
    }
}
