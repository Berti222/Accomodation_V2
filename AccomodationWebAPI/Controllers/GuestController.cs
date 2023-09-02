using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : CustomControllerBase<GuestLogic, GuestDTO, GuestPostDTO, GuestPutDTO>
    {
        public GuestController(GuestLogic logic) : base(logic)
        {
        }

        [HttpPost("add-allergenics")]
        public async Task<IActionResult> AddAllergenics([FromBody] GuestAllergenicSeterPutDTO dto)
        {
            try
            {
                var result = await logic.AddAllergenicsAsync(dto);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-allergenics")]
        public async Task<IActionResult> RemoveAllergenics([FromBody] GuestAllergenicSeterPutDTO dto)
        {
            try
            {
                var result = await logic.RemoveAllergenicsAsync(dto);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
