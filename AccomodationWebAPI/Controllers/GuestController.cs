using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : CustomControllerBase<GuestLogic, GuestDTO, GuestPostDTO, GuestPutDTO>
    {
        public GuestController(GuestLogic logic, ILoggingHelper loggingHelper) 
            : base(logic, loggingHelper)
        {
        }

        [HttpPost("add-allergenics")]
        public async Task<IActionResult> AddAllergenics([FromBody] GuestAllergenicSeterPutDTO dto)
        {
            try
            {
                loggingHelper.LogObjectPassedAsParameter(GetType().Name, nameof(AddAllergenics), dto);
                var result = await logic.AddAllergenicsAsync(dto);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "AddAllergenics");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "AddAllergenicsAsync");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-allergenics")]
        public async Task<IActionResult> RemoveAllergenics([FromBody] GuestAllergenicSeterPutDTO dto)
        {
            try
            {
                loggingHelper.LogObjectPassedAsParameter(GetType().Name, nameof(RemoveAllergenics), dto);
                var result = await logic.RemoveAllergenicsAsync(dto);
                return Ok(result);
            }
            catch (HTTPStatusException ex)
            {
                loggingHelper.LogHttpException(ex, GetType().Name, "RemoveAllergenics");
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                loggingHelper.LogGeneralException(ex, GetType().Name, "RemoveAllergenics");
                return BadRequest(ex.Message);
            }
        }
    }
}
